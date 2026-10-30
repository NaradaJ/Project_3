using GSTS.Helpers;
using GSTS.Models;
using GSTS.Services;

namespace GSTS.Forms;

// FR2 – Login and Role-Based Access (Figure 2)
public partial class LoginForm : Form
{
    private const int MaxAttempts = 3;
    private int attempts = 0;

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

        Person? user = AuthService.Login(txtUsername.Text, txtPassword.Text, out int? officerId);
        if (user == null)
        {
            attempts++;
            if (attempts >= MaxAttempts)
            {
                // After three failed tries login is disabled for this session (Figure 2).
                btnLogin.Enabled = false;
                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
                Messages.ShowError("Too many failed attempts. Login is disabled for this session");
            }
            else
            {
                Messages.ShowError("Incorrect username or password");
            }
            return;
        }

        attempts = 0;
        Session.Start(user, officerId);
        OpenDashboard();
    }

    // CASE user.role OF Citizen / Officer / Admin
    private void OpenDashboard()
    {
        Form dashboard;
        switch (Session.Role)
        {
            case "Citizen":
                dashboard = new CitizenDashboardForm();
                break;
            case "Officer":
                dashboard = new OfficerDashboardForm();
                break;
            default:
                dashboard = new AdminDashboardForm();
                break;
        }

        txtPassword.Clear();
        dashboard.FormClosed += Dashboard_FormClosed;
        Hide();
        dashboard.Show();
    }

    // When the dashboard closes: after Log Out show the login screen again,
    // otherwise (window closed with X) exit the application.
    private void Dashboard_FormClosed(object? sender, FormClosedEventArgs e)
    {
        if (Session.CurrentUser == null)
        {
            Show();
            txtUsername.Focus();
        }
        else
        {
            Close();
        }
    }

    private void btnRegister_Click(object sender, EventArgs e)
    {
        using var form = new RegisterForm();
        form.ShowDialog(this);
    }
}
