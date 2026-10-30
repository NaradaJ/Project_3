using GSTS.Data;
using MySqlConnector;

namespace GSTS.Services;

// Report §3.4 – Common helper functions.
// They take the open connection and transaction of the caller, so that the
// log entry / notification is saved in the SAME transaction as the main change.
public static class CommonService
{
    // FR7 – InsertLog: adds one entry to the audit trail.
    // performedBy = person who did the action (see DEVIATIONS.md, D1).
    public static void InsertLog(MySqlConnection conn, MySqlTransaction? tx,
        int? ticketId, int? stepId, int? officerId, int performedBy, string action, string remarks)
    {
        Db.Insert(conn, tx,
            @"INSERT INTO TicketOfficerLog (ticket_id, step_id, officer_id, performed_by, action, remarks, logged_at)
              VALUES (@ticket, @step, @officer, @by, @action, @remarks, NOW())",
            Db.P("@ticket", ticketId),
            Db.P("@step", stepId),
            Db.P("@officer", officerId),
            Db.P("@by", performedBy),
            Db.P("@action", action),
            Db.P("@remarks", remarks));
    }

    // FR8 – CreateNotification: stores an unread message for a user.
    public static void CreateNotification(MySqlConnection conn, MySqlTransaction? tx,
        int personId, int? ticketId, string message)
    {
        Db.Insert(conn, tx,
            @"INSERT INTO Notifications (person_id, ticket_id, message, is_read, created_at)
              VALUES (@person, @ticket, @message, FALSE, NOW())",
            Db.P("@person", personId),
            Db.P("@ticket", ticketId),
            Db.P("@message", message));
    }

    // GenerateTicketNo: 'GST-' + year + '-' + (count of this year's tickets + 1) padded to 6 digits.
    // Example: GST-2026-000001. The UNIQUE constraint on ticket_no guarantees that
    // two tickets can never be saved with the same number (see DEVIATIONS.md, D4).
    public static string GenerateTicketNo(MySqlConnection conn, MySqlTransaction? tx)
    {
        int year = DateTime.Now.Year;
        long count = Convert.ToInt64(Db.Scalar(conn, tx,
            "SELECT COUNT(*) FROM Tickets WHERE YEAR(created_at) = @year",
            Db.P("@year", year)));

        return "GST-" + year + "-" + (count + 1).ToString().PadLeft(6, '0');
    }

    // CitizenOf(ticket_id): the person_id of the citizen who owns the ticket.
    public static int CitizenOf(MySqlConnection conn, MySqlTransaction? tx, int ticketId)
    {
        return Convert.ToInt32(Db.Scalar(conn, tx,
            "SELECT citizen_id FROM Tickets WHERE ticket_id = @ticket",
            Db.P("@ticket", ticketId)));
    }

    // PersonOf(officer_id): the person_id that belongs to an officer.
    public static int PersonOf(MySqlConnection conn, MySqlTransaction? tx, int officerId)
    {
        return Convert.ToInt32(Db.Scalar(conn, tx,
            "SELECT person_id FROM Officers WHERE officer_id = @officer",
            Db.P("@officer", officerId)));
    }
}
