using System.Data;
using GSTS.Data;
using GSTS.Models;
using MySqlConnector;

namespace GSTS.Services;

// F4 – Officer review, flagging, notes and logging (FR6, FR7, FR8, FR9)
// Report §3.4.4, Flowchart 4.
public static class ReviewService
{
    // FR6 – the officer opening the ticket chooses Approve or Flag/Change.
    // Approve: the previous officer's work is accepted (a Flagged ticket returns to In Progress).
    // Flag/Change: the ticket is set to Flagged and the officer who originally
    //              defined the workflow is notified (FR8).
    // Either way the decision and the optional note are appended to the log (FR7, FR9).
    public static Result ReviewTicket(int officerId, int ticketId, bool approve, string note)
    {
        note = note.Trim();
        if (note.Length > TicketService.MaxNote)
        {
            return Result.Fail("The note is too long");
        }

        using var conn = Db.GetConnection();
        using var tx = conn.BeginTransaction();
        try
        {
            Ticket? ticket = TicketService.LoadTicket(conn, tx, ticketId, forUpdate: true);
            if (ticket == null)
            {
                tx.Rollback();
                return Result.Fail("Ticket not found");
            }
            if (ticket.IsCompleted)
            {
                tx.Rollback();
                return Result.Fail("This ticket is completed and can no longer be changed");
            }

            if (approve)
            {
                Db.Execute(conn, tx,
                    "UPDATE Tickets SET status = 'In Progress' WHERE ticket_id = @ticket",
                    Db.P("@ticket", ticketId));
                LogService.WriteLog(conn, tx, ticketId, officerId, LogService.Approved, note);
                tx.Commit();
                return Result.Ok("You approved the previous work on this ticket");
            }

            // Flag/Change
            Db.Execute(conn, tx,
                "UPDATE Tickets SET status = 'Flagged' WHERE ticket_id = @ticket",
                Db.P("@ticket", ticketId));
            LogService.WriteLog(conn, tx, ticketId, officerId, LogService.Flagged, note);

            // CREATE a notification for the officer who originally defined the workflow.
            // If no steps have been defined yet, the officer who created the ticket is notified.
            int originator = ticket.WorkflowDefinedBy ?? ticket.CreatedBy;
            string message = "Workflow flagged on ticket " + ticket.TicketNo + " by " + OfficerName(conn, tx, officerId)
                + (note == "" ? "" : ": " + note);

            bool notified = originator != officerId;
            if (notified)
            {
                LogService.Notify(conn, tx, ticketId, originator, officerId, message, needsReply: true);
            }

            tx.Commit();
            return Result.Ok(notified
                ? "The ticket is flagged and the originating officer has been notified"
                : "The ticket is flagged (you defined this workflow, so no notification was needed)");
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    // FR7, FR8 – the officer who received a flag notification replies to it with a note (D15).
    // Accept: the flag is agreed with; the ticket stays Flagged while the officer changes the steps.
    // Deny:   the workflow is kept as it is; the ticket returns to In Progress.
    // The reply is logged, the flag notification is marked as answered, and the officer
    // who raised the flag is notified of the decision - all in one transaction.
    public static Result ReplyToFlag(int officerId, int notificationId, bool accept, string note)
    {
        note = note.Trim();
        if (note == "")
        {
            return Result.Fail("Please enter a note explaining your reply");
        }
        if (note.Length > TicketService.MaxNote)
        {
            return Result.Fail("The note is too long");
        }

        using var conn = Db.GetConnection();
        using var tx = conn.BeginTransaction();
        try
        {
            // The officer can only reply to a flag notification sent to them, and only once.
            DataRow? notification = Db.QueryRow(conn, tx,
                @"SELECT ticket_id, from_officer_id, needs_reply, replied_at FROM Notifications
                  WHERE notification_id = @id AND to_officer_id = @officer FOR UPDATE",
                Db.P("@id", notificationId),
                Db.P("@officer", officerId));
            if (notification == null || !Convert.ToBoolean(notification["needs_reply"]))
            {
                tx.Rollback();
                return Result.Fail("Only a flag notification sent to you can be answered");
            }
            if (notification["replied_at"] != DBNull.Value)
            {
                tx.Rollback();
                return Result.Fail("You have already replied to this flag");
            }

            int ticketId = Convert.ToInt32(notification["ticket_id"]);
            int flaggedBy = Convert.ToInt32(notification["from_officer_id"]);
            Ticket? ticket = TicketService.LoadTicket(conn, tx, ticketId, forUpdate: true);
            if (ticket == null)
            {
                tx.Rollback();
                return Result.Fail("Ticket not found");
            }
            if (ticket.IsCompleted)
            {
                tx.Rollback();
                return Result.Fail("This ticket is completed and can no longer be changed");
            }
            if (ticket.Status != "Flagged")
            {
                tx.Rollback();
                return Result.Fail("This flag has already been resolved: the ticket is no longer Flagged");
            }

            if (!accept)
            {
                Db.Execute(conn, tx,
                    "UPDATE Tickets SET status = 'In Progress' WHERE ticket_id = @ticket",
                    Db.P("@ticket", ticketId));
            }
            LogService.WriteLog(conn, tx, ticketId, officerId,
                accept ? LogService.FlagAccepted : LogService.FlagDenied, note);

            Db.Execute(conn, tx,
                "UPDATE Notifications SET replied_at = NOW(), is_read = TRUE WHERE notification_id = @id",
                Db.P("@id", notificationId));

            string message = "Your flag on ticket " + ticket.TicketNo + " was "
                + (accept ? "accepted" : "denied") + " by " + OfficerName(conn, tx, officerId) + ": " + note;
            LogService.Notify(conn, tx, ticketId, flaggedBy, officerId, message);

            tx.Commit();
            return Result.Ok(accept
                ? "You accepted the flag. The ticket stays Flagged while you change the steps"
                : "You denied the flag. The ticket is back In Progress");
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    private static string OfficerName(MySqlConnection conn, MySqlTransaction tx, int officerId)
    {
        return Convert.ToString(Db.Scalar(conn, tx,
            "SELECT name FROM Officers WHERE officer_id = @officer", Db.P("@officer", officerId))) ?? "";
    }
}
