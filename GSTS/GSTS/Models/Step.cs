using System.Data;

namespace GSTS.Models;

// One row of the Steps table: a step or document needed for a ticket (FR4).
public class Step
{
    public int StepId { get; set; }
    public int TicketId { get; set; }
    public int Stage { get; set; }
    public int StepOrder { get; set; }
    public string Description { get; set; } = "";
    public bool IsCompulsory { get; set; }
    public string Status { get; set; } = "";
    public int DefinedBy { get; set; }

    public static Step FromRow(DataRow row)
    {
        return new Step
        {
            StepId = Convert.ToInt32(row["step_id"]),
            TicketId = Convert.ToInt32(row["ticket_id"]),
            Stage = Convert.ToInt32(row["stage"]),
            StepOrder = Convert.ToInt32(row["step_order"]),
            Description = Convert.ToString(row["description"]) ?? "",
            IsCompulsory = Convert.ToBoolean(row["is_compulsory"]),
            Status = Convert.ToString(row["status"]) ?? "",
            DefinedBy = Convert.ToInt32(row["defined_by"])
        };
    }
}
