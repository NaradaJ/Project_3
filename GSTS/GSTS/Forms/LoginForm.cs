using GSTS.Helpers;
using GSTS.Models;
using GSTS.Services;

namespace GSTS.Forms;

// Officer sign-in. Officer accounts are pre-registered (Report 1.4).
public partial class LoginForm : Form
{
    public LoginForm()
    {
        InitializeComponent();
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        if (txtUsername.Text.Trim() == "" || txtPassword.Text == "")
        {
            Messages.ShowError("Please enter your username and password");
            return;
        }

        Officer? officer = AuthService.Login(txtUsername.Text, txtPassword.Text);
        if (officer == null)
        {
            Messages.ShowError("Incorrect username or password");
            return;
        }

        Session.Start(officer);
        txtPassword.Clear();

        var dashboard = new OfficerDashboardForm();
        dashboard.FormClosed += Dashboard_FormClosed;
        Hide();
        dashboard.Show();
    }

    // When the dashboard closes: after Log Out show the login screen again,
    // otherwise (window closed with X) exit the application.
    private void Dashboard_FormClosed(object? sender, FormClosedEventArgs e)
    {
        if (Session.CurrentOfficer == null)
        {
            Show();
            txtUsername.Focus();
        }
        else
        {
            Close();
        }
    }
}
