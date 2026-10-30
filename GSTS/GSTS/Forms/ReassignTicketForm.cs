using GSTS.Helpers;
using GSTS.Services;

namespace GSTS.Forms;

// FR14 – Reassign Ticket (Report §3.4 ReassignTicket, Figure 6)
public partial class ReassignTicketForm : Form
{
    public ReassignTicketForm()
    {
        InitializeComponent();
    }

    private void ReassignTicketForm_Load(object sender, EventArgs e)
    {
        cmbOfficer.DisplayMember = "display";
        cmbOfficer.ValueMember = "officer_id";
        cmbOfficer.DataSource = AdminService.GetActiveOfficers();
        cmbOfficer.SelectedIndex = -1;
        LoadTickets();
    }

    private void LoadTickets()
    {
        UiHelper.FillGrid(gridTickets, AdminService.GetOpenTickets(), "ticket_id");
    }

    private void btnReassign_Click(object sender, EventArgs e)
    {
        int? ticketId = UiHelper.SelectedId(gridTickets, "ticket_id");
        if (ticketId == null)
        {
            Messages.ShowError("Please select a ticket");
            return;
        }
        if (cmbOfficer.SelectedValue == null)
        {
            Messages.ShowError("Please select the new officer");
            return;
        }

        int newOfficerId = Convert.ToInt32(cmbOfficer.SelectedValue);
        Result result = AdminService.ReassignTicket(Session.PersonId, ticketId.Value, newOfficerId);
        if (!result.Success)
        {
            Messages.ShowError(result.Message);
            return;
        }

        Messages.ShowInfo(result.Message);
        LoadTickets();
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}
