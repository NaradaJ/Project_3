using GSTS.Helpers;
using GSTS.Models;
using GSTS.Services;

namespace GSTS.Forms;

// FR9 – Track Ticket Status (Report §3.4 TrackTicket, Figure 5)
// FR11 – Respond to a Returned Ticket (Report §3.4 ResubmitTicket)
public partial class TicketDetailsForm : Form
{
    private readonly int ticketId;

    public TicketDetailsForm(int ticketId)
    {
        InitializeComponent();
        this.ticketId = ticketId;
    }

    private void TicketDetailsForm_Load(object sender, EventArgs e)
    {
        LoadTicket();
    }

    private void LoadTicket()
    {
        Ticket? ticket = TicketService.GetTicket(ticketId);
        if (ticket == null)
        {
            Messages.ShowError("This ticket was not found");
            BeginInvoke(Close);
            return;
        }

        lblTicketNoValue.Text = ticket.TicketNo;
        lblServiceValue.Text = ticket.ServiceType;
        lblStatusValue.Text = ticket.Status;
        lblCitizenValue.Text = ticket.CitizenName + " (" + ticket.CitizenNic + ")";
        lblCreatedValue.Text = ticket.CreatedAt.ToString("yyyy-MM-dd HH:mm");
        lblCurrentStepValue.Text = TicketService.GetCurrentStepText(ticketId);
        txtDetails.Text = ticket.Details;

        UiHelper.FillGrid(gridHistory, TicketService.GetCompletedSteps(ticketId));

        // Only the owning citizen can correct a ticket, and only when it was returned (FR11).
        bool canResubmit = Session.Role == "Citizen" && ticket.Status == "Returned";
        txtDetails.ReadOnly = !canResubmit;
        btnResubmit.Visible = canResubmit;
        lblResubmitHint.Visible = canResubmit;
        ActiveControl = canResubmit ? txtDetails : btnClose;
    }

    private void btnResubmit_Click(object sender, EventArgs e)
    {
        Result result = TicketService.ResubmitTicket(Session.PersonId, ticketId, txtDetails.Text);
        if (!result.Success)
        {
            Messages.ShowError(result.Message);
            return;
        }

        Messages.ShowInfo(result.Message);
        LoadTicket();
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}
