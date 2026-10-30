using System.Data;
using GSTS.Helpers;
using GSTS.Services;

namespace GSTS.Forms;

// FR13 – Manage Service Types and Steps (Report §3.4 ManageServiceSteps, Figure 8)
// Changes the template steps only, so they apply to new tickets only.
public partial class ManageServiceStepsForm : Form
{
    // The template step clicked in the grid (for Edit / Remove), or null.
    private int? selectedStepId = null;

    public ManageServiceStepsForm()
    {
        InitializeComponent();
    }

    private void ManageServiceStepsForm_Load(object sender, EventArgs e)
    {
        LoadServiceTypes();
        if (cmbServiceType.Items.Count > 0)
        {
            cmbServiceType.SelectedIndex = 0;
        }
    }

    private void LoadServiceTypes()
    {
        string current = cmbServiceType.Text;
        cmbServiceType.Items.Clear();
        foreach (string service in TicketService.GetServiceTypes())
        {
            cmbServiceType.Items.Add(service);
        }
        cmbServiceType.Text = current;
    }

    // DISPLAY current ordered step list for service_type
    private void ShowSteps()
    {
        DataTable steps = AdminService.GetTemplateSteps(cmbServiceType.Text.Trim());
        UiHelper.FillGrid(gridSteps, steps, "step_id");
        gridSteps.ClearSelection();

        selectedStepId = null;
        txtStepName.Clear();
        txtStepOrder.Text = (steps.Rows.Count + 1).ToString(); // suggest the next order number
        lblInfo.Text = steps.Rows.Count == 0
            ? "No steps yet. Add the first step to create this service type."
            : "Changes apply to new tickets only. Existing tickets keep their steps.";
    }

    private void cmbServiceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowSteps();
    }

    private void btnShow_Click(object sender, EventArgs e)
    {
        ShowSteps();
    }

    private void gridSteps_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }
        selectedStepId = UiHelper.SelectedId(gridSteps, "step_id");
        txtStepName.Text = UiHelper.SelectedText(gridSteps, "Step Name");
        txtStepOrder.Text = UiHelper.SelectedText(gridSteps, "Order");
    }

    // ADD
    private void btnAdd_Click(object sender, EventArgs e)
    {
        ShowResult(AdminService.AddTemplateStep(cmbServiceType.Text, txtStepName.Text, txtStepOrder.Text));
    }

    // EDIT / REORDER – change the name and/or order of the selected step
    private void btnEdit_Click(object sender, EventArgs e)
    {
        if (selectedStepId == null)
        {
            Messages.ShowError("Please select a step in the list");
            return;
        }
        ShowResult(AdminService.EditTemplateStep(selectedStepId.Value, cmbServiceType.Text, txtStepName.Text, txtStepOrder.Text));
    }

    // REMOVE
    private void btnRemove_Click(object sender, EventArgs e)
    {
        if (selectedStepId == null)
        {
            Messages.ShowError("Please select a step in the list");
            return;
        }
        if (!Messages.Confirm("Remove the step \"" + txtStepName.Text + "\"?"))
        {
            return;
        }
        ShowResult(AdminService.RemoveTemplateStep(selectedStepId.Value));
    }

    private void ShowResult(Result result)
    {
        if (!result.Success)
        {
            Messages.ShowError(result.Message);
            return;
        }
        Messages.ShowInfo(result.Message);
        LoadServiceTypes();
        ShowSteps();
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}
