namespace GSTS.Forms;

partial class TicketDetailsForm
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
        lblStatus = new Label();
        lblStatusValue = new Label();
        lblCitizen = new Label();
        lblCitizenValue = new Label();
        lblCreated = new Label();
        lblCreatedValue = new Label();
        lblCurrentStep = new Label();
        lblCurrentStepValue = new Label();
        lblDetails = new Label();
        txtDetails = new TextBox();
        lblResubmitHint = new Label();
        btnResubmit = new Button();
        lblHistory = new Label();
        gridHistory = new DataGridView();
        btnClose = new Button();
        ((System.ComponentModel.ISupportInitialize)gridHistory).BeginInit();
        SuspendLayout();
        //
        // lblTicketNo
        //
        lblTicketNo.AutoSize = true;
        lblTicketNo.Location = new Point(20, 20);
        lblTicketNo.Name = "lblTicketNo";
        lblTicketNo.Size = new Size(61, 15);
        lblTicketNo.TabIndex = 0;
        lblTicketNo.Text = "Ticket no:";
        //
        // lblTicketNoValue
        //
        lblTicketNoValue.AutoSize = true;
        lblTicketNoValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTicketNoValue.Location = new Point(130, 20);
        lblTicketNoValue.Name = "lblTicketNoValue";
        lblTicketNoValue.Size = new Size(12, 15);
        lblTicketNoValue.TabIndex = 1;
        lblTicketNoValue.Text = "-";
        //
        // lblService
        //
        lblService.AutoSize = true;
        lblService.Location = new Point(20, 45);
        lblService.Name = "lblService";
        lblService.Size = new Size(76, 15);
        lblService.TabIndex = 2;
        lblService.Text = "Service type:";
        //
        // lblServiceValue
        //
        lblServiceValue.AutoSize = true;
        lblServiceValue.Location = new Point(130, 45);
        lblServiceValue.Name = "lblServiceValue";
        lblServiceValue.Size = new Size(12, 15);
        lblServiceValue.TabIndex = 3;
        lblServiceValue.Text = "-";
        //
        // lblStatus
        //
        lblStatus.AutoSize = true;
        lblStatus.Location = new Point(20, 70);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(42, 15);
        lblStatus.TabIndex = 4;
        lblStatus.Text = "Status:";
        //
        // lblStatusValue
        //
        lblStatusValue.AutoSize = true;
        lblStatusValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblStatusValue.Location = new Point(130, 70);
        lblStatusValue.Name = "lblStatusValue";
        lblStatusValue.Size = new Size(12, 15);
        lblStatusValue.TabIndex = 5;
        lblStatusValue.Text = "-";
        //
        // lblCitizen
        //
        lblCitizen.AutoSize = true;
        lblCitizen.Location = new Point(400, 20);
        lblCitizen.Name = "lblCitizen";
        lblCitizen.Size = new Size(47, 15);
        lblCitizen.TabIndex = 6;
        lblCitizen.Text = "Citizen:";
        //
        // lblCitizenValue
        //
        lblCitizenValue.AutoSize = true;
        lblCitizenValue.Location = new Point(480, 20);
        lblCitizenValue.Name = "lblCitizenValue";
        lblCitizenValue.Size = new Size(12, 15);
        lblCitizenValue.TabIndex = 7;
        lblCitizenValue.Text = "-";
        //
        // lblCreated
        //
        lblCreated.AutoSize = true;
        lblCreated.Location = new Point(400, 45);
        lblCreated.Name = "lblCreated";
        lblCreated.Size = new Size(66, 15);
        lblCreated.TabIndex = 8;
        lblCreated.Text = "Submitted:";
        //
        // lblCreatedValue
        //
        lblCreatedValue.AutoSize = true;
        lblCreatedValue.Location = new Point(480, 45);
        lblCreatedValue.Name = "lblCreatedValue";
        lblCreatedValue.Size = new Size(12, 15);
        lblCreatedValue.TabIndex = 9;
        lblCreatedValue.Text = "-";
        //
        // lblCurrentStep
        //
        lblCurrentStep.AutoSize = true;
        lblCurrentStep.Location = new Point(20, 95);
        lblCurrentStep.Name = "lblCurrentStep";
        lblCurrentStep.Size = new Size(77, 15);
        lblCurrentStep.TabIndex = 10;
        lblCurrentStep.Text = "Current step:";
        //
        // lblCurrentStepValue
        //
        lblCurrentStepValue.AutoSize = true;
        lblCurrentStepValue.Location = new Point(130, 95);
        lblCurrentStepValue.Name = "lblCurrentStepValue";
        lblCurrentStepValue.Size = new Size(12, 15);
        lblCurrentStepValue.TabIndex = 11;
        lblCurrentStepValue.Text = "-";
        //
        // lblDetails
        //
        lblDetails.AutoSize = true;
        lblDetails.Location = new Point(20, 125);
        lblDetails.Name = "lblDetails";
        lblDetails.Size = new Size(93, 15);
        lblDetails.TabIndex = 12;
        lblDetails.Text = "Request details:";
        //
        // txtDetails
        //
        txtDetails.Location = new Point(20, 145);
        txtDetails.MaxLength = 2000;
        txtDetails.Multiline = true;
        txtDetails.Name = "txtDetails";
        txtDetails.ReadOnly = true;
        txtDetails.ScrollBars = ScrollBars.Vertical;
        txtDetails.Size = new Size(760, 80);
        txtDetails.TabIndex = 13;
        //
        // lblResubmitHint
        //
        lblResubmitHint.AutoSize = true;
        lblResubmitHint.ForeColor = Color.DarkRed;
        lblResubmitHint.Location = new Point(20, 238);
        lblResubmitHint.Name = "lblResubmitHint";
        lblResubmitHint.Size = new Size(420, 15);
        lblResubmitHint.TabIndex = 14;
        lblResubmitHint.Text = "This ticket was returned. Correct the details above (see remarks below) and resubmit.";
        lblResubmitHint.Visible = false;
        //
        // btnResubmit
        //
        btnResubmit.Location = new Point(630, 231);
        btnResubmit.Name = "btnResubmit";
        btnResubmit.Size = new Size(150, 30);
        btnResubmit.TabIndex = 15;
        btnResubmit.Text = "Resubmit Ticket";
        btnResubmit.UseVisualStyleBackColor = true;
        btnResubmit.Visible = false;
        btnResubmit.Click += btnResubmit_Click;
        //
        // lblHistory
        //
        lblHistory.AutoSize = true;
        lblHistory.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblHistory.Location = new Point(20, 272);
        lblHistory.Name = "lblHistory";
        lblHistory.Size = new Size(150, 15);
        lblHistory.TabIndex = 16;
        lblHistory.Text = "History of completed steps";
        //
        // gridHistory
        //
        gridHistory.AllowUserToAddRows = false;
        gridHistory.AllowUserToDeleteRows = false;
        gridHistory.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        gridHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        gridHistory.Location = new Point(20, 292);
        gridHistory.MultiSelect = false;
        gridHistory.Name = "gridHistory";
        gridHistory.ReadOnly = true;
        gridHistory.RowHeadersVisible = false;
        gridHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridHistory.Size = new Size(760, 170);
        gridHistory.TabIndex = 17;
        //
        // btnClose
        //
        btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnClose.Location = new Point(670, 474);
        btnClose.Name = "btnClose";
        btnClose.Size = new Size(110, 32);
        btnClose.TabIndex = 18;
        btnClose.Text = "Close";
        btnClose.UseVisualStyleBackColor = true;
        btnClose.Click += btnClose_Click;
        //
        // TicketDetailsForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnClose;
        ClientSize = new Size(800, 520);
        Controls.Add(lblTicketNo);
        Controls.Add(lblTicketNoValue);
        Controls.Add(lblService);
        Controls.Add(lblServiceValue);
        Controls.Add(lblStatus);
        Controls.Add(lblStatusValue);
        Controls.Add(lblCitizen);
        Controls.Add(lblCitizenValue);
        Controls.Add(lblCreated);
        Controls.Add(lblCreatedValue);
        Controls.Add(lblCurrentStep);
        Controls.Add(lblCurrentStepValue);
        Controls.Add(lblDetails);
        Controls.Add(txtDetails);
        Controls.Add(lblResubmitHint);
        Controls.Add(btnResubmit);
        Controls.Add(lblHistory);
        Controls.Add(gridHistory);
        Controls.Add(btnClose);
        MinimumSize = new Size(816, 450);
        Name = "TicketDetailsForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "GSTS – Ticket Details";
        Load += TicketDetailsForm_Load;
        ((System.ComponentModel.ISupportInitialize)gridHistory).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblTicketNo;
    private Label lblTicketNoValue;
    private Label lblService;
    private Label lblServiceValue;
    private Label lblStatus;
    private Label lblStatusValue;
    private Label lblCitizen;
    private Label lblCitizenValue;
    private Label lblCreated;
    private Label lblCreatedValue;
    private Label lblCurrentStep;
    private Label lblCurrentStepValue;
    private Label lblDetails;
    private TextBox txtDetails;
    private Label lblResubmitHint;
    private Button btnResubmit;
    private Label lblHistory;
    private DataGridView gridHistory;
    private Button btnClose;
}
