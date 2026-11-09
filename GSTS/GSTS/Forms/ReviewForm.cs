using System.Data;
using GSTS.Helpers;
using GSTS.Services;

namespace GSTS.Forms;

// FR6, FR7, FR8 – Review another officer's work (Report §3.4.4, Flowchart 4).
// Shown when the officer opening a ticket is not the officer who last acted on it.
public partial class ReviewForm : Form
{
    private readonly int ticketId;

    public ReviewForm(int ticketId, string ticketNo)
    {
        InitializeComponent();
        this.ticketId = ticketId;
        lblTitle.Text = "Review ticket " + ticketNo;
    }

    private void ReviewForm_Load(object sender, EventArgs e)
    {
        // Show who acted last, so the officer knows whose work they are reviewing.
        DataTable log = TicketService.GetActivityLog(ticketId);
        if (log.Rows.Count > 0)
        {
            DataRow last = log.Rows[0];
            lblLastAction.Text = "Last action: " + last["Action"] + " by " + last["Officer"]
                + " on " + Convert.ToDateTime(last["Date and Time"]).ToString("yyyy-MM-dd HH:mm");
        }
    }

    private void btnApprove_Click(object sender, EventArgs e)
    {
        Review(approve: true);
    }

    private void btnFlag_Click(object sender, EventArgs e)
    {
        Review(approve: false);
    }

    private void Review(bool approve)
    {
        Result result = ReviewService.ReviewTicket(Session.OfficerId, ticketId, approve, txtNote.Text);
        if (!result.Success)
        {
            Messages.ShowError(result.Message);
            return;
        }

        Messages.ShowInfo(result.Message);
        DialogResult = DialogResult.OK;
        Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }
}
