using System.Data;
using GSTS.Helpers;
using GSTS.Models;
using GSTS.Services;

namespace GSTS.Forms;

// FR5 – View Assigned Tickets (Report §3.4 ViewAssignedTickets, Figure 4)
public partial class OfficerDashboardForm : Form
{
    public OfficerDashboardForm()
    {
        InitializeComponent();
    }

    private void OfficerDashboardForm_Load(object sender, EventArgs e)
    {
        lblWelcome.Text = "Welcome, " + Session.CurrentUser!.Name;
        RefreshData();
    }

    private void RefreshData()
    {
        DataTable rows = TicketService.ViewAssignedTickets(Session.OfficerId!.Value);
        UiHelper.FillGrid(gridAssigned, rows, "ticket_id");
        lblEmpty.Visible = rows.Rows.Count == 0; // "No tickets are assigned to you"
        btnNotifications.Text = "Notifications (" + NotificationService.UnreadCount(Session.PersonId) + " unread)";
    }

    // FR6 – open the selected ticket for processing.
    private void ProcessSelectedTicket()
    {
        int? ticketId = UiHelper.SelectedId(gridAssigned, "ticket_id");
        if (ticketId == null)
        {
            Messages.ShowError("Please select a ticket");
            return;
        }

        Step? step = TicketService.GetActiveStepForOfficer(Session.OfficerId!.Value, ticketId.Value);
        if (step == null)
        {
            Messages.ShowError("This ticket is not assigned to you");
            RefreshData();
            return;
        }

        using var form = new ProcessStepForm(ticketId.Value, step);
        form.ShowDialog(this);
        RefreshData();
    }

    private void btnProcess_Click(object sender, EventArgs e)
    {
        ProcessSelectedTicket();
    }

    private void gridAssigned_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            ProcessSelectedTicket();
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

    // FR16 – Logout
    private void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Close();
    }
}
