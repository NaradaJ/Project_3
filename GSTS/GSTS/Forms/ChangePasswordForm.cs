using GSTS.Helpers;
using GSTS.Services;

namespace GSTS.Forms;

// FR16 – Change Password
public partial class ChangePasswordForm : Form
{
    public ChangePasswordForm()
    {
        InitializeComponent();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        Result result = AuthService.ChangePassword(
            Session.PersonId, txtOldPassword.Text, txtNewPassword.Text, txtConfirmPassword.Text);

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
