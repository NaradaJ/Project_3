using System.Data;

namespace GSTS.Models;

// One row of the Tickets table, with the citizen's details and the
// names of the assigned officer and the officer who defined the workflow.
public class Ticket
{
    public int TicketId { get; set; }
    public string TicketNo { get; set; } = "";
    public int PersonId { get; set; }
    public string CitizenName { get; set; } = "";
    public string IdNumber { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public string Reason { get; set; } = "";
    public string VerifiedRequirement { get; set; } = "";
    public string Status { get; set; } = "";
    public int CurrentStage { get; set; }
    public int AssignedOfficerId { get; set; }
    public string AssignedOfficerName { get; set; } = "";
    public int CreatedBy { get; set; }
    public int? WorkflowDefinedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }

    public bool IsCompleted => Status == "Completed";

    // Expects a row from TicketService.GetTicket.
    public static Ticket FromRow(DataRow row)
    {
        return new Ticket
        {
            TicketId = Convert.ToInt32(row["ticket_id"]),
            TicketNo = Convert.ToString(row["ticket_no"]) ?? "",
            PersonId = Convert.ToInt32(row["person_id"]),
            CitizenName = Convert.ToString(row["name"]) ?? "",
            IdNumber = Convert.ToString(row["id_number"]) ?? "",
            PhoneNumber = Convert.ToString(row["phone_number"]) ?? "",
            Reason = Convert.ToString(row["reason"]) ?? "",
            VerifiedRequirement = Convert.ToString(row["verified_requirement"]) ?? "",
            Status = Convert.ToString(row["status"]) ?? "",
            CurrentStage = Convert.ToInt32(row["current_stage"]),
            AssignedOfficerId = Convert.ToInt32(row["assigned_officer_id"]),
            AssignedOfficerName = Convert.ToString(row["assigned_officer_name"]) ?? "",
            CreatedBy = Convert.ToInt32(row["created_by"]),
            WorkflowDefinedBy = row["workflow_defined_by"] is DBNull ? null : Convert.ToInt32(row["workflow_defined_by"]),
            CreatedAt = Convert.ToDateTime(row["created_at"]),
            CompletedAt = row["completed_at"] is DBNull ? null : Convert.ToDateTime(row["completed_at"])
        };
    }
}
