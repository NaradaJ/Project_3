using System.Data;
using GSTS.Helpers;
using GSTS.Services;

namespace GSTS.Forms;

// Citizen home screen: own tickets (FR9), new request (FR3), notifications (FR8).
public partial class CitizenDashboardForm : Form
{
    public CitizenDashboardForm()
    {
        InitializeComponent();
    }

    private void CitizenDashboardForm_Load(object sender, EventArgs e)
    {
        lblWelcome.Text = "Welcome, " + Session.CurrentUser!.Name;
        RefreshData();
    }

    // TrackTicket: SearchTickets(CurrentUser) with no filters = all of my tickets.
    private void RefreshData()
    {
        DataTable rows = TicketService.SearchTickets("", "", "", null, null);
        UiHelper.FillGrid(gridTickets, rows, "ticket_id");
        lblEmpty.Visible = rows.Rows.Count == 0;
        btnNotifications.Text = "Notifications (" + NotificationService.UnreadCount(Session.PersonId) + " unread)";
    }

    private void btnNewRequest_Click(object sender, EventArgs e)
    {
        using var form = new NewTicketForm();
        form.ShowDialog(this);
        RefreshData();
    }

    private void OpenSelectedTicket()
    {
        int? ticketId = UiHelper.SelectedId(gridTickets, "ticket_id");
        if (ticketId == null)
        {
            Messages.ShowError("Please select a ticket");
            return;
        }
        using var form = new TicketDetailsForm(ticketId.Value);
        form.ShowDialog(this);
        RefreshData();
    }

    private void btnViewDetails_Click(object sender, EventArgs e)
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

    private void btnSearch_Click(object sender, EventArgs e)
    {
        using var form = new SearchTicketsForm();
        form.ShowDialog(this);
        RefreshData();
    }

    private void btnNotifications_Click(object sender, EventArgs e)
    {
        using var form = new NotificationsForm();
        form.ShowDialog(this);
        RefreshData();
    }

    private void btnRefresh_Click(object sender, EventArgs e)
    {
        RefreshData();
    }

    private void btnChangePassword_Click(object sender, EventArgs e)
    {
        using var form = new ChangePasswordForm();
        form.ShowDialog(this);
    }

    // FR16 – Logout: CLEAR SESSION, CLOSE current windows, OpenLoginScreen()
    private void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Close();
    }
}
