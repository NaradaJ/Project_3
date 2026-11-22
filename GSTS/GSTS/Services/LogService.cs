using GSTS.Data;
using MySqlConnector;

namespace GSTS.Services;

// Helper functions used by the other services (Report §3.4).
// They take the caller's open connection and transaction, so that the log entry
// or notification is saved in the SAME transaction as the change it describes.
public static class LogService
{
    // The actions written to TicketOfficerLog. The report's examples (created, checked,
    // approved, flagged) are extended with assignment, step and progression actions,
    // as the report's review finding 3 requires.
    public const string Created = "CREATED";
    public const string Assigned = "ASSIGNED";
    public const string RequirementVerified = "REQUIREMENT_VERIFIED";
    public const string StepAdded = "STEP_ADDED";
    public const string StepEdited = "STEP_EDITED";
    public const string StepReordered = "STEP_REORDERED";
    public const string StepCompleted = "STEP_COMPLETED";
    public const string StepSkipped = "STEP_SKIPPED";
    public const string StageProgressed = "STAGE_PROGRESSED";
    public const string TicketCompleted = "COMPLETED";
    public const string Approved = "APPROVED";
    public const string Flagged = "FLAGGED";
    public const string FlagAccepted = "FLAG_ACCEPTED";
    public const string FlagDenied = "FLAG_DENIED";

    // FR7, FR9 – APPEND an action with the officer ID, the note and the time.
    // The log is never updated or deleted (database triggers refuse it).
    public static void WriteLog(MySqlConnection conn, MySqlTransaction tx,
        int ticketId, int officerId, string action, string? note)
    {
        Db.Insert(conn, tx,
            @"INSERT INTO TicketOfficerLog (ticket_id, officer_id, action, note, logged_at)
              VALUES (@ticket, @officer, @action, @note, NOW())",
            Db.P("@ticket", ticketId),
            Db.P("@officer", officerId),
            Db.P("@action", action),
            Db.P("@note", string.IsNullOrWhiteSpace(note) ? null : note));
    }

    // FR8 – CREATE a notification from one officer to another about a ticket.
    // needsReply = true for a flag notification, which the receiver accepts or denies (D15).
    public static void Notify(MySqlConnection conn, MySqlTransaction tx,
        int ticketId, int toOfficerId, int fromOfficerId, string message, bool needsReply = false)
    {
        Db.Insert(conn, tx,
            @"INSERT INTO Notifications (ticket_id, to_officer_id, from_officer_id, message, is_read, needs_reply, created_at)
              VALUES (@ticket, @to, @from, @message, FALSE, @needsReply, NOW())",
            Db.P("@ticket", ticketId),
            Db.P("@to", toOfficerId),
            Db.P("@from", fromOfficerId),
            Db.P("@message", message),
            Db.P("@needsReply", needsReply));
    }

    // FR6 – the officer who last acted on the ticket (the newest log entry),
    // or null when the ticket has no log entries yet.
    public static int? LastActor(MySqlConnection conn, MySqlTransaction? tx, int ticketId)
    {
        object? id = Db.Scalar(conn, tx,
            "SELECT officer_id FROM TicketOfficerLog WHERE ticket_id = @ticket ORDER BY log_id DESC LIMIT 1",
            Db.P("@ticket", ticketId));
        return id == null ? null : Convert.ToInt32(id);
    }

    // Joins the system's description of an action with the officer's optional note,
    // e.g. "Stage 1: Birth certificate copy * | Note: original seen".
    public static string WithNote(string detail, string? note)
    {
        return string.IsNullOrWhiteSpace(note) ? detail : detail + " | Note: " + note.Trim();
    }

    // FR10 – elapsed time = completed_at - created_at for a completed ticket,
    // otherwise current time - created_at.
    public static TimeSpan ElapsedTime(DateTime createdAt, DateTime? completedAt)
    {
        DateTime end = completedAt ?? DateTime.Now;
        TimeSpan elapsed = end - createdAt;
        return elapsed < TimeSpan.Zero ? TimeSpan.Zero : elapsed;
    }

    // Shows an elapsed time as days, hours, minutes and seconds, e.g. "2 d 03 h 15 min 08 s".
    public static string FormatElapsed(TimeSpan elapsed)
    {
        return elapsed.Days + " d " + elapsed.Hours.ToString("00") + " h "
             + elapsed.Minutes.ToString("00") + " min " + elapsed.Seconds.ToString("00") + " s";
    }
}
