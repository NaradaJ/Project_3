using System.Data;
using GSTS.Helpers;
using GSTS.Services;

namespace GSTS.Forms;

// FR1, FR2, FR3 – Create and assign a ticket (Report §3.4.1, Flowchart 1)
public partial class NewTicketForm : Form
{
    // The new ticket's id, so the dashboard can open it straight away.
    public int? CreatedTicketId { get; private set; }

    public NewTicketForm()
    {
        UiHelper.FitOnScreen(this);
        InitializeComponent();
    }

    private void NewTicketForm_Load(object sender, EventArgs e)
    {
        // FR3 – assign to self or another available officer (self is selected first).
        DataTable officers = TicketService.GetAvailableOfficers();
        cboAssignTo.DisplayMember = "display_name";
        cboAssignTo.ValueMember = "officer_id";
        cboAssignTo.DataSource = officers;
        cboAssignTo.SelectedValue = Session.OfficerId;
    }

    private void btnCreate_Click(object sender, EventArgs e)
    {
        int? assigneeId = cboAssignTo.SelectedValue == null ? null : Convert.ToInt32(cboAssignTo.SelectedValue);

        Result result = TicketService.CreateTicket(Session.OfficerId, txtName.Text, txtIdNumber.Text,
            txtPhone.Text, txtReason.Text, assigneeId, txtNote.Text);

        if (!result.Success)
        {
            // Details not valid: show the message and stay on the form to correct them.
            Messages.ShowError(result.Message);
            return;
        }

        // DISPLAY ticket ID
        Messages.ShowInfo(result.Message);
        CreatedTicketId = result.Id;
        DialogResult = DialogResult.OK;
        Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }
}
