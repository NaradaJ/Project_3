using System.Data;
using GSTS.Helpers;
using GSTS.Models;
using GSTS.Services;

namespace GSTS.Forms;

// The screen where an officer works on one ticket:
//   FR6        – review first if another officer acted last (Flowchart 4)
//   FR4        – verify the requirement and define steps (Flowchart 2)
//   FR5        – complete or skip steps and progress the ticket (Flowchart 3)
//   FR3        – assign the ticket to self or another officer
//   FR7        – an optional note for every action
//   FR10       – the elapsed time, updated every second while the screen is open
public partial class TicketForm : Form
{
    private readonly int ticketId;
    private Ticket? ticket;

    public TicketForm(int ticketId)
    {
        UiHelper.FitOnScreen(this);
        InitializeComponent();
        this.ticketId = ticketId;
    }

    private void TicketForm_Load(object sender, EventArgs e)
    {
        LoadAssignList();
        LoadTicket();
        if (ticket == null)
        {
            Messages.ShowError("Ticket not found");
            Close();
            return;
        }

        timerElapsed.Start();
    }

    // FR6 – IF the current officer differs from the last officer who acted
    // THEN require Approve or Flag/Change before further edits.
    // (Asked once the ticket screen is visible, so the officer can see the ticket.)
    private void TicketForm_Shown(object sender, EventArgs e)
    {
        if (ticket != null && TicketService.NeedsReview(ticketId, Session.OfficerId))
        {
            ShowReview();
        }
    }

    private void LoadAssignList()
    {
        cboAssign.DisplayMember = "display_name";
        cboAssign.ValueMember = "officer_id";
        cboAssign.DataSource = TicketService.GetAvailableOfficers();
    }

    // Reads the ticket again and refreshes everything on the screen.
    private void LoadTicket()
    {
        ticket = TicketService.GetTicket(ticketId);
        if (ticket == null)
        {
            return;
        }

        lblTicketNo.Text = "Ticket " + ticket.TicketNo;
        lblCitizen.Text = "Citizen: " + ticket.CitizenName + "    ID number: " + ticket.IdNumber
                        + "    Phone: " + ticket.PhoneNumber;
        lblReason.Text = "Reason for visit: " + ticket.Reason;
        lblStatus.Text = "Status: " + ticket.Status + "    Current stage: " + ticket.CurrentStage
                       + "    Assigned to: " + ticket.AssignedOfficerName;
        txtRequirement.Text = ticket.VerifiedRequirement;
        cboAssign.SelectedValue = ticket.AssignedOfficerId;

        UiHelper.FillGrid(gridSteps, StepService.GetSteps(ticketId), "step_id");
        UpdateElapsed();

        // The officer can edit only when the ticket is open and no review is needed.
        bool needsReview = TicketService.NeedsReview(ticketId, Session.OfficerId);
        bool canEdit = !ticket.IsCompleted && !needsReview;
        SetEditing(canEdit);

        btnReview.Visible = needsReview;
        lblReadOnly.Visible = !canEdit;
        lblReadOnly.Text = ticket.IsCompleted
            ? "This ticket is completed. It can be viewed but no longer changed."
            : "View only: approve or flag/change the previous officer's work to make edits.";
    }

    private void SetEditing(bool enabled)
    {
        txtRequirement.ReadOnly = !enabled;
        btnSaveRequirement.Enabled = enabled;
        btnAddStep.Enabled = enabled;
        btnEditStep.Enabled = enabled;
        btnMoveUp.Enabled = enabled;
        btnMoveDown.Enabled = enabled;
        btnCompleteStep.Enabled = enabled;
        btnSkipStep.Enabled = enabled;
        btnProgress.Enabled = enabled;
        cboAssign.Enabled = enabled;
        btnAssign.Enabled = enabled;
        txtNote.ReadOnly = !enabled;
    }

    private void ShowReview()
    {
        using var form = new ReviewForm(ticketId, ticket!.TicketNo);
        form.ShowDialog(this);
        LoadTicket();
    }

    // Shows a service result; on success the note is cleared and the screen refreshed.
    private void AfterAction(Result result, bool showSuccess = false)
    {
        if (!result.Success)
        {
            Messages.ShowError(result.Message);
            LoadTicket();
            return;
        }

        if (showSuccess)
        {
            Messages.ShowInfo(result.Message);
        }
        txtNote.Clear();
        LoadTicket();
    }

    private int? SelectedStepId()
    {
        int? stepId = UiHelper.SelectedId(gridSteps, "step_id");
        if (stepId == null)
        {
            Messages.ShowError("Please select a step");
        }
        return stepId;
    }

    // ---------------- FR4 – requirement and steps ----------------

    private void btnSaveRequirement_Click(object sender, EventArgs e)
    {
        AfterAction(StepService.SaveVerifiedRequirement(Session.OfficerId, ticketId, txtRequirement.Text, txtNote.Text), showSuccess: true);
    }

    private void btnAddStep_Click(object sender, EventArgs e)
    {
        using var form = new StepForm(null, ticket!.CurrentStage);
        if (form.ShowDialog(this) == DialogResult.OK)
        {
            AfterAction(StepService.AddStep(Session.OfficerId, ticketId, form.Description, form.Stage, form.IsCompulsory, txtNote.Text));
        }
    }

    private void btnEditStep_Click(object sender, EventArgs e)
    {
        int? stepId = SelectedStepId();
        if (stepId == null)
        {
            return;
        }

        Step? step = StepService.GetStep(stepId.Value);
        if (step == null || step.Status != "Pending")
        {
            Messages.ShowError("Only a pending step can be edited");
            return;
        }

        using var form = new StepForm(step, ticket!.CurrentStage);
        if (form.ShowDialog(this) == DialogResult.OK)
        {
            AfterAction(StepService.EditStep(Session.OfficerId, stepId.Value, form.Description, form.Stage, form.IsCompulsory, txtNote.Text));
        }
    }

    private void btnMoveUp_Click(object sender, EventArgs e)
    {
        MoveStep(up: true);
    }

    private void btnMoveDown_Click(object sender, EventArgs e)
    {
        MoveStep(up: false);
    }

    private void MoveStep(bool up)
    {
        int? stepId = SelectedStepId();
        if (stepId != null)
        {
            AfterAction(StepService.MoveStep(Session.OfficerId, stepId.Value, up, txtNote.Text));
            SelectStep(stepId.Value);
        }
    }

    // Keeps the moved step selected in the grid.
    private void SelectStep(int stepId)
    {
        foreach (DataGridViewRow row in gridSteps.Rows)
        {
            if (Convert.ToInt32(row.Cells["step_id"].Value) == stepId)
            {
                gridSteps.CurrentCell = row.Cells[1];
                break;
            }
        }
    }

    // ---------------- FR5 – complete, skip, progress ----------------

    private void btnCompleteStep_Click(object sender, EventArgs e)
    {
        int? stepId = SelectedStepId();
        if (stepId != null)
        {
            AfterAction(StepService.CompleteStep(Session.OfficerId, stepId.Value, txtNote.Text));
        }
    }

    private void btnSkipStep_Click(object sender, EventArgs e)
    {
        int? stepId = SelectedStepId();
        if (stepId != null)
        {
            AfterAction(StepService.SkipStep(Session.OfficerId, stepId.Value, txtNote.Text));
        }
    }

    private void btnProgress_Click(object sender, EventArgs e)
    {
        AfterAction(StepService.ProgressTicket(Session.OfficerId, ticketId, txtNote.Text), showSuccess: true);
    }

    // ---------------- FR3 – assignment ----------------

    private void btnAssign_Click(object sender, EventArgs e)
    {
        int? assigneeId = cboAssign.SelectedValue == null ? null : Convert.ToInt32(cboAssign.SelectedValue);
        AfterAction(TicketService.AssignTicket(Session.OfficerId, ticketId, assigneeId, txtNote.Text), showSuccess: true);
    }

    // ---------------- FR6 – review ----------------

    private void btnReview_Click(object sender, EventArgs e)
    {
        ShowReview();
    }

    // ---------------- FR10 – elapsed time ----------------

    private void timerElapsed_Tick(object sender, EventArgs e)
    {
        UpdateElapsed();
    }

    private void UpdateElapsed()
    {
        if (ticket != null)
        {
            TimeSpan elapsed = LogService.ElapsedTime(ticket.CreatedAt, ticket.CompletedAt);
            lblElapsed.Text = (ticket.IsCompleted ? "Total time: " : "Elapsed: ") + LogService.FormatElapsed(elapsed);
        }
    }

    // ---------------- FR11 – status report ----------------

    private void btnStatusReport_Click(object sender, EventArgs e)
    {
        using var form = new StatusReportForm(ticketId);
        form.ShowDialog(this);
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void TicketForm_FormClosed(object sender, FormClosedEventArgs e)
    {
        timerElapsed.Stop();
    }
}
