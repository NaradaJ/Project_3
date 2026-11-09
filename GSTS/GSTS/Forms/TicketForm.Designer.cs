namespace GSTS.Forms;

partial class TicketForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        lblTicketNo = new Label();
        lblElapsed = new Label();
        lblCitizen = new Label();
        lblReason = new Label();
        lblStatus = new Label();
        lblReadOnly = new Label();
        btnReview = new Button();
        grpRequirement = new GroupBox();
        txtRequirement = new TextBox();
        btnSaveRequirement = new Button();
        grpSteps = new GroupBox();
        gridSteps = new DataGridView();
        btnAddStep = new Button();
        btnEditStep = new Button();
        btnMoveUp = new Button();
        btnMoveDown = new Button();
        btnCompleteStep = new Button();
        btnSkipStep = new Button();
        btnProgress = new Button();
        grpAssign = new GroupBox();
        cboAssign = new ComboBox();
        btnAssign = new Button();
        lblNote = new Label();
        txtNote = new TextBox();
        btnStatusReport = new Button();
        btnClose = new Button();
        timerElapsed = new System.Windows.Forms.Timer(components);
        grpRequirement.SuspendLayout();
        grpSteps.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)gridSteps).BeginInit();
        grpAssign.SuspendLayout();
        SuspendLayout();
        //
        // lblTicketNo
        //
        lblTicketNo.AutoSize = true;
        lblTicketNo.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblTicketNo.Location = new Point(15, 12);
        lblTicketNo.Name = "lblTicketNo";
        lblTicketNo.Size = new Size(80, 25);
        lblTicketNo.TabIndex = 0;
        lblTicketNo.Text = "Ticket";
        //
        // lblElapsed
        //
        lblElapsed.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblElapsed.ForeColor = Color.DarkBlue;
        lblElapsed.Location = new Point(520, 14);
        lblElapsed.Name = "lblElapsed";
        lblElapsed.Size = new Size(365, 22);
        lblElapsed.TabIndex = 1;
        lblElapsed.Text = "Elapsed:";
        lblElapsed.TextAlign = ContentAlignment.MiddleRight;
        //
        // lblCitizen
        //
        lblCitizen.AutoSize = true;
        lblCitizen.Location = new Point(18, 47);
        lblCitizen.Name = "lblCitizen";
        lblCitizen.Size = new Size(50, 15);
        lblCitizen.TabIndex = 2;
        lblCitizen.Text = "Citizen:";
        //
        // lblReason
        //
        lblReason.AutoEllipsis = true;
        lblReason.Location = new Point(18, 70);
        lblReason.Name = "lblReason";
        lblReason.Size = new Size(865, 18);
        lblReason.TabIndex = 3;
        lblReason.Text = "Reason for visit:";
        //
        // lblStatus
        //
        lblStatus.AutoSize = true;
        lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblStatus.Location = new Point(18, 93);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(45, 15);
        lblStatus.TabIndex = 4;
        lblStatus.Text = "Status:";
        //
        // lblReadOnly
        //
        lblReadOnly.AutoSize = true;
        lblReadOnly.ForeColor = Color.Firebrick;
        lblReadOnly.Location = new Point(18, 118);
        lblReadOnly.Name = "lblReadOnly";
        lblReadOnly.Size = new Size(60, 15);
        lblReadOnly.TabIndex = 5;
        lblReadOnly.Text = "View only";
        lblReadOnly.Visible = false;
        //
        // btnReview
        //
        btnReview.Location = new Point(520, 111);
        btnReview.Name = "btnReview";
        btnReview.Size = new Size(200, 28);
        btnReview.TabIndex = 6;
        btnReview.Text = "Approve or Flag/Change...";
        btnReview.UseVisualStyleBackColor = true;
        btnReview.Visible = false;
        btnReview.Click += btnReview_Click;
        //
        // grpRequirement
        //
        grpRequirement.Controls.Add(txtRequirement);
        grpRequirement.Controls.Add(btnSaveRequirement);
        grpRequirement.Location = new Point(15, 145);
        grpRequirement.Name = "grpRequirement";
        grpRequirement.Size = new Size(870, 62);
        grpRequirement.TabIndex = 7;
        grpRequirement.TabStop = false;
        grpRequirement.Text = "Verified requirement (confirmed with the citizen)";
        //
        // txtRequirement
        //
        txtRequirement.Location = new Point(12, 24);
        txtRequirement.MaxLength = 500;
        txtRequirement.Name = "txtRequirement";
        txtRequirement.Size = new Size(690, 23);
        txtRequirement.TabIndex = 0;
        //
        // btnSaveRequirement
        //
        btnSaveRequirement.Location = new Point(712, 21);
        btnSaveRequirement.Name = "btnSaveRequirement";
        btnSaveRequirement.Size = new Size(145, 28);
        btnSaveRequirement.TabIndex = 1;
        btnSaveRequirement.Text = "Save Requirement";
        btnSaveRequirement.UseVisualStyleBackColor = true;
        btnSaveRequirement.Click += btnSaveRequirement_Click;
        //
        // grpSteps
        //
        grpSteps.Controls.Add(gridSteps);
        grpSteps.Controls.Add(btnAddStep);
        grpSteps.Controls.Add(btnEditStep);
        grpSteps.Controls.Add(btnMoveUp);
        grpSteps.Controls.Add(btnMoveDown);
        grpSteps.Controls.Add(btnCompleteStep);
        grpSteps.Controls.Add(btnSkipStep);
        grpSteps.Controls.Add(btnProgress);
        grpSteps.Location = new Point(15, 215);
        grpSteps.Name = "grpSteps";
        grpSteps.Size = new Size(870, 285);
        grpSteps.TabIndex = 8;
        grpSteps.TabStop = false;
        grpSteps.Text = "Steps and documents (* = compulsory)";
        //
        // gridSteps
        //
        gridSteps.AllowUserToAddRows = false;
        gridSteps.AllowUserToDeleteRows = false;
        gridSteps.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridSteps.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        gridSteps.Location = new Point(12, 24);
        gridSteps.MultiSelect = false;
        gridSteps.Name = "gridSteps";
        gridSteps.ReadOnly = true;
        gridSteps.RowHeadersVisible = false;
        gridSteps.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridSteps.Size = new Size(690, 248);
        gridSteps.TabIndex = 0;
        //
        // btnAddStep
        //
        btnAddStep.Location = new Point(712, 24);
        btnAddStep.Name = "btnAddStep";
        btnAddStep.Size = new Size(145, 30);
        btnAddStep.TabIndex = 1;
        btnAddStep.Text = "Add Step";
        btnAddStep.UseVisualStyleBackColor = true;
        btnAddStep.Click += btnAddStep_Click;
        //
        // btnEditStep
        //
        btnEditStep.Location = new Point(712, 60);
        btnEditStep.Name = "btnEditStep";
        btnEditStep.Size = new Size(145, 30);
        btnEditStep.TabIndex = 2;
        btnEditStep.Text = "Edit Step";
        btnEditStep.UseVisualStyleBackColor = true;
        btnEditStep.Click += btnEditStep_Click;
        //
        // btnMoveUp
        //
        btnMoveUp.Location = new Point(712, 96);
        btnMoveUp.Name = "btnMoveUp";
        btnMoveUp.Size = new Size(70, 30);
        btnMoveUp.TabIndex = 3;
        btnMoveUp.Text = "Up";
        btnMoveUp.UseVisualStyleBackColor = true;
        btnMoveUp.Click += btnMoveUp_Click;
        //
        // btnMoveDown
        //
        btnMoveDown.Location = new Point(787, 96);
        btnMoveDown.Name = "btnMoveDown";
        btnMoveDown.Size = new Size(70, 30);
        btnMoveDown.TabIndex = 4;
        btnMoveDown.Text = "Down";
        btnMoveDown.UseVisualStyleBackColor = true;
        btnMoveDown.Click += btnMoveDown_Click;
        //
        // btnCompleteStep
        //
        btnCompleteStep.Location = new Point(712, 150);
        btnCompleteStep.Name = "btnCompleteStep";
        btnCompleteStep.Size = new Size(145, 30);
        btnCompleteStep.TabIndex = 5;
        btnCompleteStep.Text = "Mark Completed";
        btnCompleteStep.UseVisualStyleBackColor = true;
        btnCompleteStep.Click += btnCompleteStep_Click;
        //
        // btnSkipStep
        //
        btnSkipStep.Location = new Point(712, 186);
        btnSkipStep.Name = "btnSkipStep";
        btnSkipStep.Size = new Size(145, 30);
        btnSkipStep.TabIndex = 6;
        btnSkipStep.Text = "Skip (optional only)";
        btnSkipStep.UseVisualStyleBackColor = true;
        btnSkipStep.Click += btnSkipStep_Click;
        //
        // btnProgress
        //
        btnProgress.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnProgress.Location = new Point(712, 238);
        btnProgress.Name = "btnProgress";
        btnProgress.Size = new Size(145, 34);
        btnProgress.TabIndex = 7;
        btnProgress.Text = "Progress Ticket";
        btnProgress.UseVisualStyleBackColor = true;
        btnProgress.Click += btnProgress_Click;
        //
        // grpAssign
        //
        grpAssign.Controls.Add(cboAssign);
        grpAssign.Controls.Add(btnAssign);
        grpAssign.Location = new Point(15, 508);
        grpAssign.Name = "grpAssign";
        grpAssign.Size = new Size(870, 60);
        grpAssign.TabIndex = 9;
        grpAssign.TabStop = false;
        grpAssign.Text = "Assign the ticket (to yourself or another officer)";
        //
        // cboAssign
        //
        cboAssign.DropDownStyle = ComboBoxStyle.DropDownList;
        cboAssign.Location = new Point(12, 24);
        cboAssign.Name = "cboAssign";
        cboAssign.Size = new Size(320, 23);
        cboAssign.TabIndex = 0;
        //
        // btnAssign
        //
        btnAssign.Location = new Point(342, 21);
        btnAssign.Name = "btnAssign";
        btnAssign.Size = new Size(120, 28);
        btnAssign.TabIndex = 1;
        btnAssign.Text = "Assign";
        btnAssign.UseVisualStyleBackColor = true;
        btnAssign.Click += btnAssign_Click;
        //
        // lblNote
        //
        lblNote.AutoSize = true;
        lblNote.Location = new Point(18, 578);
        lblNote.Name = "lblNote";
        lblNote.Size = new Size(260, 15);
        lblNote.TabIndex = 10;
        lblNote.Text = "Note for your next action (optional, saved in the log):";
        //
        // txtNote
        //
        txtNote.Location = new Point(15, 597);
        txtNote.MaxLength = 1000;
        txtNote.Multiline = true;
        txtNote.Name = "txtNote";
        txtNote.ScrollBars = ScrollBars.Vertical;
        txtNote.Size = new Size(600, 48);
        txtNote.TabIndex = 11;
        //
        // btnStatusReport
        //
        btnStatusReport.Location = new Point(635, 605);
        btnStatusReport.Name = "btnStatusReport";
        btnStatusReport.Size = new Size(130, 34);
        btnStatusReport.TabIndex = 12;
        btnStatusReport.Text = "Status Report";
        btnStatusReport.UseVisualStyleBackColor = true;
        btnStatusReport.Click += btnStatusReport_Click;
        //
        // btnClose
        //
        btnClose.Location = new Point(775, 605);
        btnClose.Name = "btnClose";
        btnClose.Size = new Size(110, 34);
        btnClose.TabIndex = 13;
        btnClose.Text = "Close";
        btnClose.UseVisualStyleBackColor = true;
        btnClose.Click += btnClose_Click;
        //
        // timerElapsed
        //
        timerElapsed.Interval = 1000;
        timerElapsed.Tick += timerElapsed_Tick;
        //
        // TicketForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnClose;
        ClientSize = new Size(900, 660);
        Controls.Add(lblTicketNo);
        Controls.Add(lblElapsed);
        Controls.Add(lblCitizen);
        Controls.Add(lblReason);
        Controls.Add(lblStatus);
        Controls.Add(lblReadOnly);
        Controls.Add(btnReview);
        Controls.Add(grpRequirement);
        Controls.Add(grpSteps);
        Controls.Add(grpAssign);
        Controls.Add(lblNote);
        Controls.Add(txtNote);
        Controls.Add(btnStatusReport);
        Controls.Add(btnClose);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "TicketForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "GSTS – Ticket";
        FormClosed += TicketForm_FormClosed;
        Load += TicketForm_Load;
        Shown += TicketForm_Shown;
        grpRequirement.ResumeLayout(false);
        grpRequirement.PerformLayout();
        grpSteps.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)gridSteps).EndInit();
        grpAssign.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblTicketNo;
    private Label lblElapsed;
    private Label lblCitizen;
    private Label lblReason;
    private Label lblStatus;
    private Label lblReadOnly;
    private Button btnReview;
    private GroupBox grpRequirement;
    private TextBox txtRequirement;
    private Button btnSaveRequirement;
    private GroupBox grpSteps;
    private DataGridView gridSteps;
    private Button btnAddStep;
    private Button btnEditStep;
    private Button btnMoveUp;
    private Button btnMoveDown;
    private Button btnCompleteStep;
    private Button btnSkipStep;
    private Button btnProgress;
    private GroupBox grpAssign;
    private ComboBox cboAssign;
    private Button btnAssign;
    private Label lblNote;
    private TextBox txtNote;
    private Button btnStatusReport;
    private Button btnClose;
    private System.Windows.Forms.Timer timerElapsed;
}
