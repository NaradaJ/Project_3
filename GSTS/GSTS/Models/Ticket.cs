using System.Data;

namespace GSTS.Models;

// One row of the Tickets table, together with the citizen's name and NIC.
public class Ticket
{
    public int TicketId { get; set; }
    public string TicketNo { get; set; } = "";
    public int CitizenId { get; set; }
    public string CitizenName { get; set; } = "";
    public string CitizenNic { get; set; } = "";
    public string ServiceType { get; set; } = "";
    public string Details { get; set; } = "";
    public string Status { get; set; } = "";
    public int? CurrentStep { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ClosedAt { get; set; }

    // Expects a row from: SELECT t.*, p.name, p.nic FROM Tickets t JOIN Persons p ...
    public static Ticket FromRow(DataRow row)
    {
        return new Ticket
        {
            TicketId = Convert.ToInt32(row["ticket_id"]),
            TicketNo = Convert.ToString(row["ticket_no"]) ?? "",
            CitizenId = Convert.ToInt32(row["citizen_id"]),
            CitizenName = Convert.ToString(row["name"]) ?? "",
            CitizenNic = Convert.ToString(row["nic"]) ?? "",
            ServiceType = Convert.ToString(row["service_type"]) ?? "",
            Details = Convert.ToString(row["details"]) ?? "",
            Status = Convert.ToString(row["status"]) ?? "",
            CurrentStep = row["current_step"] is DBNull ? null : Convert.ToInt32(row["current_step"]),
            CreatedAt = Convert.ToDateTime(row["created_at"]),
            ClosedAt = row["closed_at"] is DBNull ? null : Convert.ToDateTime(row["closed_at"])
        };
    }
}
