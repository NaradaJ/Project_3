using GSTS.Helpers;
using GSTS.Services;

namespace GSTS.Forms;

// FR3 – Submit a Service Request (Report §3.4 CreateTicket, Figure 3)
public partial class NewTicketForm : Form
{
    public NewTicketForm()
    {
        InitializeComponent();
    }

    private void NewTicketForm_Load(object sender, EventArgs e)
    {
        foreach (string service in TicketService.GetServiceTypes())
        {
            cmbServiceType.Items.Add(service);
        }
    }

    private void btnSubmit_Click(object sender, EventArgs e)
    {
        Result result = TicketService.CreateTicket(Session.PersonId, cmbServiceType.Text, txtDetails.Text);
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
