using System.Data;
using GSTS.Helpers;
using GSTS.Services;

namespace GSTS.Forms;

// FR10 – Search and Filter Tickets (Figure 5)
// The same screen is used by all roles; TicketService.SearchTickets adds the role restriction.
public partial class SearchTicketsForm : Form
{
    private const string AllItems = "(All)";

    public SearchTicketsForm()
    {
        InitializeComponent();
    }

    private void SearchTicketsForm_Load(object sender, EventArgs e)
    {
        cmbStatus.Items.Add(AllItems);
        cmbStatus.Items.AddRange(new object[] { "Open", "In progress", "Returned", "Completed", "Rejected" });
        cmbStatus.SelectedIndex = 0;

        cmbService.Items.Add(AllItems);
        foreach (string service in TicketService.GetServiceTypes())
        {
            cmbService.Items.Add(service);
        }
        cmbService.SelectedIndex = 0;

        dtpFrom.Value = DateTime.Today.AddMonths(-1);
        dtpTo.Value = DateTime.Today;
        chkDate_CheckedChanged(sender, e);

        RunSearch();
    }

    private void RunSearch()
    {
        string status = cmbStatus.Text == AllItems ? "" : cmbStatus.Text;
        string service = cmbService.Text == AllItems ? "" : cmbService.Text;
        DateTime? from = null;
        DateTime? to = null;

        if (chkDate.Checked)
        {
            if (dtpFrom.Value.Date > dtpTo.Value.Date)
            {
                Messages.ShowError("Please enter a valid date range");
                return;
            }
            from = dtpFrom.Value;
            to = dtpTo.Value;
        }

        DataTable rows = TicketService.SearchTickets(txtKeyword.Text, status, service, from, to);
        UiHelper.FillGrid(gridTickets, rows, "ticket_id");
        lblResult.Text = rows.Rows.Count == 0 ? "No tickets found" : rows.Rows.Count + " ticket(s) found";
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
        RunSearch();
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        txtKeyword.Clear();
        cmbStatus.SelectedIndex = 0;
        cmbService.SelectedIndex = 0;
        chkDate.Checked = false;
        RunSearch();
    }

    private void chkDate_CheckedChanged(object sender, EventArgs e)
    {
        dtpFrom.Enabled = chkDate.Checked;
        dtpTo.Enabled = chkDate.Checked;
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
        RunSearch();
    }

    private void btnView_Click(object sender, EventArgs e)
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

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}
