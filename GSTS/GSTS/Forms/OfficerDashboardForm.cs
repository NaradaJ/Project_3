using System.Data;
using GSTS.Helpers;
using GSTS.Services;

namespace GSTS.Forms;

// The officer's main screen: their tickets (or all open tickets), and the
// buttons for the four functionalities and the status lookup.
public partial class OfficerDashboardForm : Form
{
    public OfficerDashboardForm()
    {
        InitializeComponent();
    }

    private void OfficerDashboardForm_Load(object sender, EventArgs e)
    {
        lblWelcome.Text = "Welcome, " + Session.CurrentOfficer!.Name + " (" + Session.CurrentOfficer.Designation + ")";
        RefreshData();
    }

    private void RefreshData()
    {
        DataTable rows = rdoMine.Checked
            ? TicketService.GetMyTickets(Session.OfficerId)
            : TicketService.GetOpenTickets();
        UiHelper.FillGrid(gridTickets, rows, "ticket_id");
        lblEmpty.Visible = rows.Rows.Count == 0;
        btnNotifications.Text = "Notifications (" + NotificationService.UnreadCount(Session.OfficerId) + " unread)";
    }

    // F1 – create a new ticket.
    private void btnNewTicket_Click(object sender, EventArgs e)
    {
        using var form = new NewTicketForm();
        if (form.ShowDialog(this) == DialogResult.OK && form.CreatedTicketId != null)
        {
            OpenTicket(form.CreatedTicketId.Value);
        }
        RefreshData();
    }

    // F2, F3, F4 – open the selected ticket to work on it.
    private void btnOpen_Click(object sender, EventArgs e)
    {
        OpenSelectedTicket();
    }

    private void gridTickets_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            OpenSelectedTicket();
        }
    }

    private void OpenSelectedTicket()
    {
        int? ticketId = UiHelper.SelectedId(gridTickets, "ticket_id");
        if (ticketId == null)
        {
            Messages.ShowError("Please select a ticket");
            return;
        }
        OpenTicket(ticketId.Value);
    }

    private void OpenTicket(int ticketId)
    {
        using var form = new TicketForm(ticketId);
        form.ShowDialog(this);
        RefreshData();
    }

    // FR11 – look up any ticket by its ID.
    private void btnLookup_Click(object sender, EventArgs e)
    {
        using var form = new StatusReportForm(null);
        form.ShowDialog(this);
    }

    // FR8 – notifications sent to this officer.
    private void btnNotifications_Click(object sender, EventArgs e)
    {
        using var form = new NotificationsForm();
        form.ShowDialog(this);
        RefreshData();
    }

    private void rdoMine_CheckedChanged(object sender, EventArgs e)
    {
        RefreshData();
    }

    private void btnRefresh_Click(object sender, EventArgs e)
    {
        RefreshData();
    }

    private void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Close();
    }
}
