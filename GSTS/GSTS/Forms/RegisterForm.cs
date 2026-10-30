using GSTS.Helpers;
using GSTS.Services;

namespace GSTS.Forms;

// FR1 – User Registration (Figure 1)
public partial class RegisterForm : Form
{
    public RegisterForm()
    {
        InitializeComponent();
    }

    private void btnRegister_Click(object sender, EventArgs e)
    {
        Result result = AuthService.RegisterUser(
            txtName.Text, txtNic.Text, txtContact.Text, txtAddress.Text, txtPassword.Text);

        if (!result.Success)
        {
            // Invalid input or a duplicate NIC: stay on the form (Figure 1).
            Messages.ShowError(result.Message);
            return;
        }

        Messages.ShowInfo(result.Message + "\n\nYour username is your NIC: " + txtNic.Text.Trim().ToUpper());
        Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }
}
