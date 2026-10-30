using GSTS.Services;

namespace GSTS.Forms;

// Administrator home screen: links to FR10, FR12–FR15.
public partial class AdminDashboardForm : Form
{
    public AdminDashboardForm()
    {
        InitializeComponent();
    }

    private void AdminDashboardForm_Load(object sender, EventArgs e)
    {
        lblWelcome.Text = "Welcome, " + Session.CurrentUser!.Name;
        RefreshData();
    }

    private void RefreshData()
    {
        btnNotifications.Text = "Notifications (" + NotificationService.UnreadCount(Session.PersonId) + " unread)";
    }

    // Opens a screen as a dialog and refreshes the dashboard afterwards.
    private void OpenDialog(Form form)
    {
        using (form)
        {
            form.ShowDialog(this);
        }
        RefreshData();
    }

    private void btnOfficers_Click(object sender, EventArgs e)
    {
        OpenDialog(new ManageOfficersForm());
    }

    private void btnServiceSteps_Click(object sender, EventArgs e)
    {
        OpenDialog(new ManageServiceStepsForm());
    }

    private void btnReassign_Click(object sender, EventArgs e)
    {
        OpenDialog(new ReassignTicketForm());
    }

    private void btnReports_Click(object sender, EventArgs e)
    {
        OpenDialog(new ReportsForm());
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
        OpenDialog(new SearchTicketsForm());
    }

    private void btnNotifications_Click(object sender, EventArgs e)
    {
        OpenDialog(new NotificationsForm());
    }

    private void btnChangePassword_Click(object sender, EventArgs e)
    {
        OpenDialog(new ChangePasswordForm());
    }

    // FR16 – Logout
    private void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Close();
    }
}
