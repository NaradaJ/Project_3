using System.Data;
using GSTS.Helpers;
using GSTS.Services;

namespace GSTS.Forms;

// FR15 – Reports (Report §3.4 GenerateReport, Figure 7)
public partial class ReportsForm : Form
{
    // The report names shown in the list, and the report_type code for each (same order).
    private readonly string[] reportNames =
    {
        "Tickets by status",
        "Tickets by service type",
        "Average processing time (days)",
        "Tickets pending longer than N days"
    };
    private readonly string[] reportCodes = { "BY_STATUS", "BY_SERVICE", "AVG_TIME", "PENDING_LONG" };

    public ReportsForm()
    {
        InitializeComponent();
    }

    private void ReportsForm_Load(object sender, EventArgs e)
    {
        cmbReport.Items.AddRange(reportNames);
        cmbReport.SelectedIndex = 0;
        dtpFrom.Value = DateTime.Today.AddMonths(-1);
        dtpTo.Value = DateTime.Today;
    }

    // The date range is used by the first three reports; the number of days by the last one.
    private void cmbReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool pendingReport = reportCodes[cmbReport.SelectedIndex] == "PENDING_LONG";
        numDays.Enabled = pendingReport;
        dtpFrom.Enabled = !pendingReport;
        dtpTo.Enabled = !pendingReport;
    }

    private void btnGenerate_Click(object sender, EventArgs e)
    {
        string reportType = reportCodes[cmbReport.SelectedIndex];
        Result result = AdminService.GenerateReport(reportType, dtpFrom.Value, dtpTo.Value, (int)numDays.Value, out DataTable rows);

        UiHelper.FillGrid(gridReport, rows);
        if (!result.Success)
        {
            // An invalid date range or an empty result gives a message, not a blank report (Figure 7).
            lblMessage.Text = result.Message;
            Messages.ShowError(result.Message);
            return;
        }
        lblMessage.Text = cmbReport.Text + " – " + rows.Rows.Count + " row(s)";
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}
