using GSTS.Helpers;
using GSTS.Models;
using GSTS.Services;

namespace GSTS.Forms;

// FR11 – Look up ticket status (Report §3.4.5, Flowchart 5).
// Any officer enters a ticket ID and sees the consolidated status report:
// citizen details and visit reason, status, elapsed time (FR10), current handler,
// previous handlers with their first action and date, the ordered steps with
// compulsory indicators, and the full activity log with the newest entry first.
public partial class StatusReportForm : Form
{
    private Ticket? ticket;
    private readonly int? startTicketId;

    // ticketId = show this ticket straight away, or null to start with an empty search.
    public StatusReportForm(int? ticketId)
    {
        InitializeComponent();
        startTicketId = ticketId;
    }

    private void StatusReportForm_Load(object sender, EventArgs e)
    {
        if (startTicketId != null)
        {
            ShowReport(startTicketId.Value);
        }
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
        // INPUT ticket_id; SEARCH; IF ticket does not exist THEN DISPLAY Ticket not found
        int? ticketId = TicketService.FindTicket(txtTicketId.Text);
        if (ticketId == null)
        {
            Messages.ShowError("Ticket not found. Please check the ticket ID (e.g. GST-2026-000001)");
            txtTicketId.Focus();
            return;
        }
        ShowReport(ticketId.Value);
    }

    private void ShowReport(int ticketId)
    {
        ticket = TicketService.GetTicket(ticketId);
        if (ticket == null)
        {
            return;
        }

        txtTicketId.Text = ticket.TicketNo;
        lblDetails.Text =
            "Ticket ID: " + ticket.TicketNo + "      Status: " + ticket.Status + "      Current stage: " + ticket.CurrentStage + "\n" +
            "Citizen: " + ticket.CitizenName + "      ID number: " + ticket.IdNumber + "      Phone: " + ticket.PhoneNumber + "\n" +
            "Reason for visit: " + ticket.Reason + "\n" +
            "Verified requirement: " + (ticket.VerifiedRequirement == "" ? "(not yet recorded)" : ticket.VerifiedRequirement) + "\n" +
            "Current handler: " + ticket.AssignedOfficerName + "      Created: " + ticket.CreatedAt.ToString("yyyy-MM-dd HH:mm")
            + (ticket.CompletedAt == null ? "" : "      Completed: " + ticket.CompletedAt.Value.ToString("yyyy-MM-dd HH:mm"));

        UiHelper.FillGrid(gridHandlers, TicketService.GetPreviousHandlers(ticketId));
        UiHelper.FillGrid(gridSteps, StepService.GetSteps(ticketId), "step_id");
        UiHelper.FillGrid(gridLog, TicketService.GetActivityLog(ticketId));
        UpdateElapsed();
        timerElapsed.Start();
    }

    // FR10 – recalculated every second while the report is open.
    private void timerElapsed_Tick(object sender, EventArgs e)
    {
        UpdateElapsed();
    }

    private void UpdateElapsed()
    {
        if (ticket != null)
        {
            TimeSpan elapsed = LogService.ElapsedTime(ticket.CreatedAt, ticket.CompletedAt);
            lblElapsed.Text = (ticket.IsCompleted ? "Total time: " : "Elapsed: ") + LogService.FormatElapsed(elapsed);
        }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void StatusReportForm_FormClosed(object sender, FormClosedEventArgs e)
    {
        timerElapsed.Stop();
    }
}
