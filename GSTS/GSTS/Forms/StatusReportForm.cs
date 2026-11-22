using System.Data;
using GSTS.Helpers;
using GSTS.Models;
using GSTS.Services;

namespace GSTS.Forms;

// FR11 – Look up ticket status (Report §3.4.5, Flowchart 5).
// The search box also finds tickets by citizen name or NIC, and an empty search
// lists the completed tickets (see DEVIATIONS.md, D12).
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
        UiHelper.FitOnScreen(this);
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

    // FR11 – one search box:
    //   empty                  -> list the completed tickets to choose from
    //   a ticket ID or number  -> show that ticket's report
    //   anything else          -> search by the citizen's name or NIC
    private void btnSearch_Click(object sender, EventArgs e)
    {
        string text = txtTicketId.Text.Trim();

        if (text == "")
        {
            DataTable completed = TicketService.GetCompletedTickets();
            if (completed.Rows.Count == 0)
            {
                Messages.ShowInfo("There are no completed tickets yet");
                return;
            }
            PickAndShow("Completed tickets", completed);
            return;
        }

        int? ticketId = TicketService.FindTicket(text);
        if (ticketId != null)
        {
            ShowReport(ticketId.Value);
            return;
        }

        DataTable found = TicketService.SearchByCitizen(text);
        if (found.Rows.Count == 0)
        {
            // IF ticket does not exist THEN DISPLAY Ticket not found
            Messages.ShowError("Ticket not found. Enter a ticket ID (e.g. GST-2026-000001), a citizen's name or NIC");
            txtTicketId.Focus();
            return;
        }
        if (found.Rows.Count == 1)
        {
            ShowReport(Convert.ToInt32(found.Rows[0]["ticket_id"]));
            return;
        }
        PickAndShow("Tickets for \"" + text + "\"", found);
    }

    // Lets the officer choose one ticket from a list, then shows its report.
    private void PickAndShow(string title, DataTable tickets)
    {
        using var picker = new TicketPickerForm(title, tickets);
        if (picker.ShowDialog(this) == DialogResult.OK && picker.SelectedTicketId != null)
        {
            ShowReport(picker.SelectedTicketId.Value);
        }
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
