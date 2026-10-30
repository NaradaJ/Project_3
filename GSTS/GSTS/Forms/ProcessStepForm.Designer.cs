namespace GSTS.Forms;

partial class ProcessStepForm
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
        lblTicketNo = new Label();
        lblTicketNoValue = new Label();
        lblService = new Label();
        lblServiceValue = new Label();
        lblCitizen = new Label();
        lblCitizenValue = new Label();
        lblStep = new Label();
        lblStepValue = new Label();
        lblDetails = new Label();
        txtDetails = new TextBox();
        lblSteps = new Label();
        gridSteps = new DataGridView();
        grpDecision = new GroupBox();
        rdoApprove = new RadioButton();
        rdoReturn = new RadioButton();
        rdoReject = new RadioButton();
        lblRemarks = new Label();
        txtRemarks = new TextBox();
        btnSave = new Button();
        btnCancel = new Button();
        ((System.ComponentModel.ISupportInitialize)gridSteps).BeginInit();
        grpDecision.SuspendLayout();
        SuspendLayout();
        //
        // lblTicketNo
        //
        lblTicketNo.AutoSize = true;
        lblTicketNo.Location = new Point(20, 18);
        lblTicketNo.Name = "lblTicketNo";
        lblTicketNo.Size = new Size(61, 15);
        lblTicketNo.TabIndex = 0;
        lblTicketNo.Text = "Ticket no:";
        //
        // lblTicketNoValue
        //
        lblTicketNoValue.AutoSize = true;
        lblTicketNoValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTicketNoValue.Location = new Point(120, 18);
        lblTicketNoValue.Name = "lblTicketNoValue";
        lblTicketNoValue.Size = new Size(12, 15);
        lblTicketNoValue.TabIndex = 1;
        lblTicketNoValue.Text = "-";
        //
        // lblService
        //
        lblService.AutoSize = true;
        lblService.Location = new Point(20, 43);
        lblService.Name = "lblService";
        lblService.Size = new Size(76, 15);
        lblService.TabIndex = 2;
        lblService.Text = "Service type:";
        //
        // lblServiceValue
        //
        lblServiceValue.AutoSize = true;
        lblServiceValue.Location = new Point(120, 43);
        lblServiceValue.Name = "lblServiceValue";
        lblServiceValue.Size = new Size(12, 15);
        lblServiceValue.TabIndex = 3;
        lblServiceValue.Text = "-";
        //
        // lblCitizen
        //
        lblCitizen.AutoSize = true;
        lblCitizen.Location = new Point(400, 18);
        lblCitizen.Name = "lblCitizen";
        lblCitizen.Size = new Size(47, 15);
        lblCitizen.TabIndex = 4;
        lblCitizen.Text = "Citizen:";
        //
        // lblCitizenValue
        //
        lblCitizenValue.AutoSize = true;
        lblCitizenValue.Location = new Point(480, 18);
        lblCitizenValue.Name = "lblCitizenValue";
        lblCitizenValue.Size = new Size(12, 15);
        lblCitizenValue.TabIndex = 5;
        lblCitizenValue.Text = "-";
        //
        // lblStep
        //
        lblStep.AutoSize = true;
        lblStep.Location = new Point(400, 43);
        lblStep.Name = "lblStep";
        lblStep.Size = new Size(57, 15);
        lblStep.TabIndex = 6;
        lblStep.Text = "Your step:";
        //
        // lblStepValue
        //
        lblStepValue.AutoSize = true;
        lblStepValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblStepValue.Location = new Point(480, 43);
        lblStepValue.Name = "lblStepValue";
        lblStepValue.Size = new Size(12, 15);
        lblStepValue.TabIndex = 7;
        lblStepValue.Text = "-";
        //
        // lblDetails
        //
        lblDetails.AutoSize = true;
        lblDetails.Location = new Point(20, 72);
        lblDetails.Name = "lblDetails";
        lblDetails.Size = new Size(93, 15);
        lblDetails.TabIndex = 8;
        lblDetails.Text = "Request details:";
        //
        // txtDetails
        //
        txtDetails.Location = new Point(20, 92);
        txtDetails.Multiline = true;
        txtDetails.Name = "txtDetails";
        txtDetails.ReadOnly = true;
        txtDetails.ScrollBars = ScrollBars.Vertical;
        txtDetails.Size = new Size(760, 70);
        txtDetails.TabIndex = 9;
        //
        // lblSteps
        //
        lblSteps.AutoSize = true;
        lblSteps.Location = new Point(20, 172);
        lblSteps.Name = "lblSteps";
        lblSteps.Size = new Size(180, 15);
        lblSteps.TabIndex = 10;
        lblSteps.Text = "All steps and earlier remarks:";
        //
        // gridSteps
        //
        gridSteps.AllowUserToAddRows = false;
        gridSteps.AllowUserToDeleteRows = false;
        gridSteps.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridSteps.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        gridSteps.Location = new Point(20, 192);
        gridSteps.MultiSelect = false;
        gridSteps.Name = "gridSteps";
        gridSteps.ReadOnly = true;
        gridSteps.RowHeadersVisible = false;
        gridSteps.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridSteps.Size = new Size(760, 130);
        gridSteps.TabIndex = 11;
        //
        // grpDecision
        //
        grpDecision.Controls.Add(rdoApprove);
        grpDecision.Controls.Add(rdoReturn);
        grpDecision.Controls.Add(rdoReject);
        grpDecision.Location = new Point(20, 332);
        grpDecision.Name = "grpDecision";
        grpDecision.Size = new Size(220, 110);
        grpDecision.TabIndex = 12;
        grpDecision.TabStop = false;
        grpDecision.Text = "Decision";
        //
        // rdoApprove
        //
        rdoApprove.AutoSize = true;
        rdoApprove.Location = new Point(15, 25);
        rdoApprove.Name = "rdoApprove";
        rdoApprove.Size = new Size(70, 19);
        rdoApprove.TabIndex = 0;
        rdoApprove.Text = "Approve";
        rdoApprove.UseVisualStyleBackColor = true;
        //
        // rdoReturn
        //
        rdoReturn.AutoSize = true;
        rdoReturn.Location = new Point(15, 52);
        rdoReturn.Name = "rdoReturn";
        rdoReturn.Size = new Size(150, 19);
        rdoReturn.TabIndex = 1;
        rdoReturn.Text = "Return for correction";
        rdoReturn.UseVisualStyleBackColor = true;
        //
        // rdoReject
        //
        rdoReject.AutoSize = true;
        rdoReject.Location = new Point(15, 79);
        rdoReject.Name = "rdoReject";
        rdoReject.Size = new Size(60, 19);
        rdoReject.TabIndex = 2;
        rdoReject.Text = "Reject";
        rdoReject.UseVisualStyleBackColor = true;
        //
        // lblRemarks
        //
        lblRemarks.AutoSize = true;
        lblRemarks.Location = new Point(260, 338);
        lblRemarks.Name = "lblRemarks";
        lblRemarks.Size = new Size(300, 15);
        lblRemarks.TabIndex = 13;
        lblRemarks.Text = "Remarks (required when returning or rejecting):";
        //
        // txtRemarks
        //
        txtRemarks.Location = new Point(260, 358);
        txtRemarks.MaxLength = 1000;
        txtRemarks.Multiline = true;
        txtRemarks.Name = "txtRemarks";
        txtRemarks.ScrollBars = ScrollBars.Vertical;
        txtRemarks.Size = new Size(520, 84);
        txtRemarks.TabIndex = 14;
        //
        // btnSave
        //
        btnSave.Location = new Point(530, 455);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(130, 32);
        btnSave.TabIndex = 15;
        btnSave.Text = "Save Decision";
        btnSave.UseVisualStyleBackColor = true;
        btnSave.Click += btnSave_Click;
        //
        // btnCancel
        //
        btnCancel.Location = new Point(670, 455);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(110, 32);
        btnCancel.TabIndex = 16;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = true;
        btnCancel.Click += btnCancel_Click;
        //
        // ProcessStepForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancel;
        ClientSize = new Size(800, 505);
        Controls.Add(lblTicketNo);
        Controls.Add(lblTicketNoValue);
        Controls.Add(lblService);
        Controls.Add(lblServiceValue);
        Controls.Add(lblCitizen);
        Controls.Add(lblCitizenValue);
        Controls.Add(lblStep);
        Controls.Add(lblStepValue);
        Controls.Add(lblDetails);
        Controls.Add(txtDetails);
        Controls.Add(lblSteps);
        Controls.Add(gridSteps);
        Controls.Add(grpDecision);
        Controls.Add(lblRemarks);
        Controls.Add(txtRemarks);
        Controls.Add(btnSave);
        Controls.Add(btnCancel);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "ProcessStepForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "GSTS – Process Ticket Step";
        Load += ProcessStepForm_Load;
        ((System.ComponentModel.ISupportInitialize)gridSteps).EndInit();
        grpDecision.ResumeLayout(false);
        grpDecision.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblTicketNo;
    private Label lblTicketNoValue;
    private Label lblService;
    private Label lblServiceValue;
    private Label lblCitizen;
    private Label lblCitizenValue;
    private Label lblStep;
    private Label lblStepValue;
    private Label lblDetails;
    private TextBox txtDetails;
    private Label lblSteps;
    private DataGridView gridSteps;
    private GroupBox grpDecision;
    private RadioButton rdoApprove;
    private RadioButton rdoReturn;
    private RadioButton rdoReject;
    private Label lblRemarks;
    private TextBox txtRemarks;
    private Button btnSave;
    private Button btnCancel;
}
