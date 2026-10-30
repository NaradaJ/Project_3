using System.Data;

namespace GSTS.Models;

// One row of the Steps table (a ticket step or a template step).
public class Step
{
    public int StepId { get; set; }
    public int? TicketId { get; set; }
    public string ServiceType { get; set; } = "";
    public int StepOrder { get; set; }
    public string StepName { get; set; } = "";
    public int? OfficerId { get; set; }
    public string Status { get; set; } = "";
    public string Remarks { get; set; } = "";

    public static Step FromRow(DataRow row)
    {
        return new Step
        {
            StepId = Convert.ToInt32(row["step_id"]),
            TicketId = row["ticket_id"] is DBNull ? null : Convert.ToInt32(row["ticket_id"]),
            ServiceType = Convert.ToString(row["service_type"]) ?? "",
            StepOrder = Convert.ToInt32(row["step_order"]),
            StepName = Convert.ToString(row["step_name"]) ?? "",
            OfficerId = row["officer_id"] is DBNull ? null : Convert.ToInt32(row["officer_id"]),
            Status = Convert.ToString(row["status"]) ?? "",
            Remarks = Convert.ToString(row["remarks"]) ?? ""
        };
    }
}
