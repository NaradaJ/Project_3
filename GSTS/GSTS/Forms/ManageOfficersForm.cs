using GSTS.Helpers;
using GSTS.Models;
using GSTS.Services;

namespace GSTS.Forms;

// FR12 – Manage Officers: add, edit, deactivate and search (Report §3.4 ManageOfficer, Figure 8)
public partial class ManageOfficersForm : Form
{
    // The officer currently loaded into the fields, or null when adding a new one.
    private int? selectedOfficerId = null;

    public ManageOfficersForm()
    {
        InitializeComponent();
    }

    private void ManageOfficersForm_Load(object sender, EventArgs e)
    {
        foreach (string stepName in AdminService.GetStepNames())
        {
            cmbHandlesStep.Items.Add(stepName);
        }
        LoadOfficers();
        ClearFields();
    }

    private void ManageOfficersForm_Shown(object sender, EventArgs e)
    {
        gridOfficers.ClearSelection(); // start in "add new" mode with no row highlighted
    }

    private void LoadOfficers()
    {
        UiHelper.FillGrid(gridOfficers, AdminService.SearchOfficers(txtSearch.Text), "officer_id");
    }

    // "New" mode: empty fields, NIC and password can be entered.
    private void ClearFields()
    {
        selectedOfficerId = null;
        txtName.Clear();
        txtNic.Clear();
        txtContact.Clear();
        txtDesignation.Clear();
        txtDepartment.Clear();
        txtPassword.Clear();
        cmbHandlesStep.SelectedIndex = -1;
        txtNic.ReadOnly = false;
        txtPassword.Enabled = true;
        btnAdd.Enabled = true;
        btnSave.Enabled = false;
        btnDeactivate.Enabled = false;
        lblMode.Text = "Adding a new officer";
    }

    // "Edit" mode: load the clicked officer into the fields.
    private void gridOfficers_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return; // header clicked
        }

        int? officerId = UiHelper.SelectedId(gridOfficers, "officer_id");
        if (officerId == null)
        {
            return;
        }

        Officer? officer = AdminService.GetOfficer(officerId.Value);
        if (officer == null)
        {
            return;
        }

        selectedOfficerId = officer.OfficerId;
        txtName.Text = officer.Name;
        txtNic.Text = officer.Nic;
        txtContact.Text = officer.Contact;
        txtDesignation.Text = officer.Designation;
        txtDepartment.Text = officer.Department;
        if (!cmbHandlesStep.Items.Contains(officer.HandlesStep))
        {
            cmbHandlesStep.Items.Add(officer.HandlesStep);
        }
        cmbHandlesStep.SelectedItem = officer.HandlesStep;
        txtPassword.Clear();
        txtNic.ReadOnly = true;       // the NIC is the username, so it is not changed
        txtPassword.Enabled = false;  // officers change their own password (FR16)
        btnAdd.Enabled = false;
        btnSave.Enabled = officer.IsActive;
        btnDeactivate.Enabled = officer.IsActive;
        lblMode.Text = officer.IsActive ? "Editing: " + officer.Name : officer.Name + " (inactive)";
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
        LoadOfficers();
    }

    private void btnNew_Click(object sender, EventArgs e)
    {
        gridOfficers.ClearSelection();
        ClearFields();
    }

    // ManageOfficer('ADD')
    private void btnAdd_Click(object sender, EventArgs e)
    {
        Result result = AdminService.AddOfficer(Session.PersonId, txtName.Text, txtNic.Text, txtContact.Text,
            txtDesignation.Text, txtDepartment.Text, cmbHandlesStep.Text, txtPassword.Text);
        ShowResult(result);
    }

    // ManageOfficer('EDIT')
    private void btnSave_Click(object sender, EventArgs e)
    {
        if (selectedOfficerId == null)
        {
            Messages.ShowError("Please select an officer");
            return;
        }
        Result result = AdminService.EditOfficer(Session.PersonId, selectedOfficerId.Value, txtName.Text,
            txtContact.Text, txtDesignation.Text, txtDepartment.Text, cmbHandlesStep.Text);
        ShowResult(result);
    }

    // ManageOfficer('DEACTIVATE')
    private void btnDeactivate_Click(object sender, EventArgs e)
    {
        if (selectedOfficerId == null)
        {
            Messages.ShowError("Please select an officer");
            return;
        }
        if (!Messages.Confirm("Deactivate " + txtName.Text + "? The officer will no longer be able to log in."))
        {
            return;
        }
        ShowResult(AdminService.DeactivateOfficer(Session.PersonId, selectedOfficerId.Value));
    }

    private void ShowResult(Result result)
    {
        if (!result.Success)
        {
            Messages.ShowError(result.Message);
            return;
        }
        Messages.ShowInfo(result.Message);
        LoadOfficers();
        gridOfficers.ClearSelection();
        ClearFields();
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}
