using GSTS.Helpers;
using GSTS.Models;
using GSTS.Services;

namespace GSTS.Forms;

// FR6, FR7, FR8 – Process a Ticket Step (Report §3.4 ProcessStep, Figure 4)
public partial class ProcessStepForm : Form
{
    private readonly int ticketId;
    private readonly Step step;

    public ProcessStepForm(int ticketId, Step step)
    {
        InitializeComponent();
        this.ticketId = ticketId;
        this.step = step;
    }

    private void ProcessStepForm_Load(object sender, EventArgs e)
    {
        Ticket? ticket = TicketService.GetTicket(ticketId);
        if (ticket == null)
        {
            Messages.ShowError("This ticket was not found");
            BeginInvoke(Close);
            return;
        }

        // DISPLAY ticket details and remarks of earlier steps
        lblTicketNoValue.Text = ticket.TicketNo;
        lblServiceValue.Text = ticket.ServiceType;
        lblCitizenValue.Text = ticket.CitizenName + " (" + ticket.CitizenNic + ")";
        lblStepValue.Text = step.StepOrder + ". " + step.StepName;
        txtDetails.Text = ticket.Details;
        UiHelper.FillGrid(gridSteps, TicketService.GetAllSteps(ticketId));
        ActiveControl = txtRemarks;

        // FR7 – the "view" action is logged.
        TicketService.LogView(ticketId, step.StepId, Session.OfficerId!.Value, Session.PersonId);
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        string decision;
        if (rdoApprove.Checked)
        {
            decision = "APPROVE";
        }
        else if (rdoReturn.Checked)
        {
            decision = "RETURN";
        }
        else if (rdoReject.Checked)
        {
            decision = "REJECT";
        }
        else
        {
            Messages.ShowError("Please choose Approve, Return or Reject");
            return;
        }

        Result result = TicketService.ProcessStep(Session.OfficerId!.Value, Session.PersonId, ticketId, decision, txtRemarks.Text);
        if (!result.Success)
        {
            Messages.ShowError(result.Message);
            return;
        }

        Messages.ShowInfo(result.Message);
        Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }
}
