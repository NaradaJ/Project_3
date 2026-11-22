using System.Data;
using GSTS.Helpers;

namespace GSTS.Forms;

// FR11 – shows the tickets found by a search (completed tickets, or tickets
// matching a citizen's name or NIC) so the officer can choose one to view.
public partial class TicketPickerForm : Form
{
    // The ticket the officer chose, or null if they closed the window.
    public int? SelectedTicketId { get; private set; }

    private readonly DataTable tickets;

    public TicketPickerForm(string title, DataTable tickets)
    {
        UiHelper.FitOnScreen(this);
        InitializeComponent();
        this.tickets = tickets;
        lblTitle.Text = title;
        lblCount.Text = tickets.Rows.Count + (tickets.Rows.Count == 1 ? " ticket" : " tickets");
    }

    // The grid is filled once the form has loaded, so its columns exist and the id can be hidden.
    private void TicketPickerForm_Load(object sender, EventArgs e)
    {
        UiHelper.FillGrid(gridTickets, tickets, "ticket_id");
    }

    private void ChooseSelected()
    {
        int? ticketId = UiHelper.SelectedId(gridTickets, "ticket_id");
        if (ticketId == null)
        {
            Messages.ShowError("Please select a ticket");
            return;
        }
        SelectedTicketId = ticketId;
        DialogResult = DialogResult.OK;
        Close();
    }

    private void btnView_Click(object sender, EventArgs e)
    {
        ChooseSelected();
    }

    private void gridTickets_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            ChooseSelected();
        }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}
