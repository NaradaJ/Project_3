using System.Data;
using GSTS.Data;
using GSTS.Helpers;
using GSTS.Models;
using MySqlConnector;

namespace GSTS.Services;

// F1 – Ticket intake and assignment (FR1, FR2, FR3), the ticket lists,
// and the ticket status lookup (FR11).
public static class TicketService
{
    // Field limits (they match the column sizes in 01_schema.sql).
    public const int MaxReason = 500;
    public const int MaxNote = 1000;

    // FR3 – the officers a ticket can be assigned to ("available" = active account).
    public static DataTable GetAvailableOfficers()
    {
        return Db.Query(
            @"SELECT officer_id, CONCAT(name, ' (', designation, ')') AS display_name
              FROM Officers WHERE is_active = TRUE ORDER BY name");
    }

    // FR1, FR2, FR3 – Create and Assign Ticket (Report §3.4.1, Flowchart 1)
    public static Result CreateTicket(int currentOfficerId, string citizenName, string idNumber,
        string phoneNumber, string reason, int? assigneeId, string note)
    {
        citizenName = citizenName.Trim();
        idNumber = idNumber.Trim().ToUpper();
        phoneNumber = phoneNumber.Trim();
        reason = reason.Trim();
        note = note.Trim();

        // IF any required field is missing THEN DISPLAY validation message
        if (citizenName == "" || idNumber == "" || phoneNumber == "" || reason == "")
        {
            return Result.Fail("Please enter the citizen's name, ID number, phone number and reason for visit");
        }
        if (!Validator.ValidIdNumber(idNumber))
        {
            return Result.Fail("The ID number is not valid (use 9 digits and V/X, or 12 digits)");
        }
        if (!Validator.ValidPhone(phoneNumber))
        {
            return Result.Fail("The phone number is not valid (10 digits starting with 0)");
        }
        if (citizenName.Length > 100 || reason.Length > MaxReason || note.Length > MaxNote)
        {
            return Result.Fail("The name, reason or note is too long");
        }

        using var conn = Db.GetConnection();
        using var tx = conn.BeginTransaction();
        try
        {
            // SELECT an available officer (self or another officer).
            // IF no officer is available THEN do not complete ticket creation.
            Officer? assignee = assigneeId == null ? null : GetActiveOfficer(conn, tx, assigneeId.Value);
            if (assignee == null)
            {
                tx.Rollback();
                return Result.Fail("Please select an available officer to assign the ticket to");
            }

            // SAVE citizen details: reuse the person if the ID number is already known.
            object? existing = Db.Scalar(conn, tx,
                "SELECT person_id FROM Persons WHERE id_number = @id",
                Db.P("@id", idNumber));
            int personId;
            if (existing == null)
            {
                personId = Db.Insert(conn, tx,
                    "INSERT INTO Persons (name, id_number, phone_number) VALUES (@name, @id, @phone)",
                    Db.P("@name", citizenName),
                    Db.P("@id", idNumber),
                    Db.P("@phone", phoneNumber));
            }
            else
            {
                personId = Convert.ToInt32(existing);
                Db.Execute(conn, tx,
                    "UPDATE Persons SET name = @name, phone_number = @phone WHERE person_id = @person",
                    Db.P("@name", citizenName),
                    Db.P("@phone", phoneNumber),
                    Db.P("@person", personId));
            }

            // SAVE the ticket with its creation timestamp and ASSIGN it to the selected officer.
            int ticketId = Db.Insert(conn, tx,
                @"INSERT INTO Tickets (person_id, reason, status, current_stage, assigned_officer_id, created_by, created_at)
                  VALUES (@person, @reason, 'In Progress', 1, @assignee, @creator, NOW())",
                Db.P("@person", personId),
                Db.P("@reason", reason),
                Db.P("@assignee", assignee.OfficerId),
                Db.P("@creator", currentOfficerId));

            // FR2 – GENERATE a unique ticket ID. It is built from the AUTO_INCREMENT id,
            // so two tickets can never get the same number.
            string ticketNo = GenerateTicketNo(ticketId);
            Db.Execute(conn, tx,
                "UPDATE Tickets SET ticket_no = @no WHERE ticket_id = @ticket",
                Db.P("@no", ticketNo),
                Db.P("@ticket", ticketId));

            // APPEND the ticket-created action, and RECORD the assignment as an action.
            LogService.WriteLog(conn, tx, ticketId, currentOfficerId, LogService.Created, note);
            LogService.WriteLog(conn, tx, ticketId, currentOfficerId, LogService.Assigned,
                "Assigned to " + assignee.Name);

            tx.Commit();
            return Result.Ok("Ticket created. Ticket ID: " + ticketNo, ticketId);
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    // FR3 – assign an existing ticket to the current officer or another available officer.
    public static Result AssignTicket(int currentOfficerId, int ticketId, int? assigneeId, string note)
    {
        note = note.Trim();
        if (note.Length > MaxNote)
        {
            return Result.Fail("The note is too long");
        }

        using var conn = Db.GetConnection();
        using var tx = conn.BeginTransaction();
        try
        {
            Ticket? ticket = LoadTicket(conn, tx, ticketId, forUpdate: true);
            string? problem = CheckEditable(conn, tx, ticket, currentOfficerId);
            if (problem != null)
            {
                tx.Rollback();
                return Result.Fail(problem);
            }

            Officer? assignee = assigneeId == null ? null : GetActiveOfficer(conn, tx, assigneeId.Value);
            if (assignee == null)
            {
                tx.Rollback();
                return Result.Fail("Please select an available officer");
            }
            if (assignee.OfficerId == ticket!.AssignedOfficerId)
            {
                tx.Rollback();
                return Result.Fail("The ticket is already assigned to " + assignee.Name);
            }

            Db.Execute(conn, tx,
                "UPDATE Tickets SET assigned_officer_id = @assignee WHERE ticket_id = @ticket",
                Db.P("@assignee", assignee.OfficerId),
                Db.P("@ticket", ticketId));

            LogService.WriteLog(conn, tx, ticketId, currentOfficerId, LogService.Assigned,
                LogService.WithNote("Assigned to " + assignee.Name, note));

            tx.Commit();
            return Result.Ok("The ticket is now assigned to " + assignee.Name);
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    // Ticket ID shown to the citizen, e.g. GST-2026-000001.
    public static string GenerateTicketNo(int ticketId)
    {
        return "GST-" + DateTime.Now.Year + "-" + ticketId.ToString().PadLeft(6, '0');
    }

    // ---------------------------------------------------------------------
    // Reading tickets
    // ---------------------------------------------------------------------

    // Returns the ticket, or null when it does not exist.
    public static Ticket? GetTicket(int ticketId)
    {
        using var conn = Db.GetConnection();
        return LoadTicket(conn, null, ticketId, forUpdate: false);
    }

    // FR11 – SEARCH for a ticket. Accepts the ticket ID as shown to the citizen
    // (GST-2026-000001) or just its number (1). Returns the internal id, or null.
    public static int? FindTicket(string text)
    {
        text = text.Trim().ToUpper();
        if (text == "")
        {
            return null;
        }

        object? id = Db.Scalar("SELECT ticket_id FROM Tickets WHERE ticket_no = @no", Db.P("@no", text));
        if (id == null && int.TryParse(text, out int number))
        {
            id = Db.Scalar("SELECT ticket_id FROM Tickets WHERE ticket_id = @id", Db.P("@id", number));
        }
        return id == null ? null : Convert.ToInt32(id);
    }

    // The officer's own open tickets (current handler = this officer).
    public static DataTable GetMyTickets(int officerId)
    {
        return Db.Query(TicketListSql("WHERE t.status <> 'Completed' AND t.assigned_officer_id = @officer"),
            Db.P("@officer", officerId));
    }

    // All tickets that are not completed, so any officer can review another officer's work (FR6).
    public static DataTable GetOpenTickets()
    {
        return Db.Query(TicketListSql("WHERE t.status <> 'Completed'"));
    }

    // FR11 – an empty search lists the completed tickets, newest first,
    // so an officer can find and view a ticket that is finished.
    public static DataTable GetCompletedTickets()
    {
        return Db.Query(TicketListSql("WHERE t.status = 'Completed'"));
    }

    // FR11 – find tickets (open or completed) by part of the citizen's name or ID number (NIC).
    public static DataTable SearchByCitizen(string text)
    {
        text = text.Trim();
        return Db.Query(TicketListSql("WHERE p.name LIKE @text OR p.id_number LIKE @text"),
            Db.P("@text", "%" + text + "%"));
    }

    private static string TicketListSql(string where)
    {
        return @"SELECT t.ticket_id, t.ticket_no AS `Ticket ID`, p.name AS `Citizen`, t.reason AS `Reason`,
                        t.status AS `Status`, t.current_stage AS `Stage`, o.name AS `Assigned To`,
                        t.created_at AS `Created`
                 FROM Tickets t
                 JOIN Persons p ON p.person_id = t.person_id
                 JOIN Officers o ON o.officer_id = t.assigned_officer_id
                 " + where + @"
                 ORDER BY t.ticket_id DESC";
    }

    // FR11 – previous handlers and each handler's first recorded action and date.
    public static DataTable GetPreviousHandlers(int ticketId)
    {
        return Db.Query(
            @"SELECT o.name AS `Officer`, l.action AS `First Action`, l.logged_at AS `First Action Date`
              FROM TicketOfficerLog l
              JOIN Officers o ON o.officer_id = l.officer_id
              WHERE l.log_id IN (SELECT MIN(log_id) FROM TicketOfficerLog
                                 WHERE ticket_id = @ticket GROUP BY officer_id)
              ORDER BY l.log_id",
            Db.P("@ticket", ticketId));
    }

    // FR9, FR11 – the full activity log, newest entry first.
    public static DataTable GetActivityLog(int ticketId)
    {
        return Db.Query(
            @"SELECT l.logged_at AS `Date and Time`, o.name AS `Officer`, l.action AS `Action`, l.note AS `Note`
              FROM TicketOfficerLog l
              JOIN Officers o ON o.officer_id = l.officer_id
              WHERE l.ticket_id = @ticket
              ORDER BY l.log_id DESC",
            Db.P("@ticket", ticketId));
    }

    // ---------------------------------------------------------------------
    // Helpers shared by TicketService, StepService and ReviewService
    // ---------------------------------------------------------------------

    // Loads a ticket with the citizen's details and the assigned officer's name.
    // forUpdate = lock the ticket row until the transaction ends, so two changes
    // to the same ticket are saved one after the other.
    internal static Ticket? LoadTicket(MySqlConnection conn, MySqlTransaction? tx, int ticketId, bool forUpdate)
    {
        DataRow? row = Db.QueryRow(conn, tx,
            @"SELECT t.*, p.name, p.id_number, p.phone_number, o.name AS assigned_officer_name
              FROM Tickets t
              JOIN Persons p ON p.person_id = t.person_id
              JOIN Officers o ON o.officer_id = t.assigned_officer_id
              WHERE t.ticket_id = @ticket" + (forUpdate ? " FOR UPDATE" : ""),
            Db.P("@ticket", ticketId));
        return row == null ? null : Ticket.FromRow(row);
    }

    // Returns why the officer may not change the ticket now, or null when they may.
    // FR6 – an officer who is not the last officer who acted must first choose
    // Approve or Flag/Change.
    internal static string? CheckEditable(MySqlConnection conn, MySqlTransaction? tx, Ticket? ticket, int officerId)
    {
        if (ticket == null)
        {
            return "Ticket not found";
        }
        if (ticket.IsCompleted)
        {
            return "This ticket is completed and can no longer be changed";
        }
        if (NeedsReview(conn, tx, ticket, officerId))
        {
            return "Another officer acted last on this ticket. Please choose Approve or Flag/Change before making further edits";
        }
        return null;
    }

    // FR6 – IF current officer differs from the last officer who acted THEN REQUIRE a review.
    public static bool NeedsReview(int ticketId, int officerId)
    {
        using var conn = Db.GetConnection();
        Ticket? ticket = LoadTicket(conn, null, ticketId, forUpdate: false);
        return ticket != null && NeedsReview(conn, null, ticket, officerId);
    }

    internal static bool NeedsReview(MySqlConnection conn, MySqlTransaction? tx, Ticket ticket, int officerId)
    {
        if (ticket.IsCompleted)
        {
            return false;
        }
        int? lastActor = LogService.LastActor(conn, tx, ticket.TicketId);
        return lastActor != null && lastActor.Value != officerId;
    }

    internal static Officer? GetActiveOfficer(MySqlConnection conn, MySqlTransaction? tx, int officerId)
    {
        DataRow? row = Db.QueryRow(conn, tx,
            "SELECT * FROM Officers WHERE officer_id = @officer AND is_active = TRUE",
            Db.P("@officer", officerId));
        return row == null ? null : Officer.FromRow(row);
    }
}
