namespace GSTS.Forms;

partial class NewTicketForm
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
        lblTitle = new Label();
        lblServiceType = new Label();
        cmbServiceType = new ComboBox();
        lblDetails = new Label();
        txtDetails = new TextBox();
        btnSubmit = new Button();
        btnCancel = new Button();
        SuspendLayout();
        //
        // lblTitle
        //
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblTitle.Location = new Point(20, 15);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(210, 25);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "New Service Request";
        //
        // lblServiceType
        //
        lblServiceType.AutoSize = true;
        lblServiceType.Location = new Point(22, 62);
        lblServiceType.Name = "lblServiceType";
        lblServiceType.Size = new Size(76, 15);
        lblServiceType.TabIndex = 1;
        lblServiceType.Text = "Service type:";
        //
        // cmbServiceType
        //
        cmbServiceType.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbServiceType.Location = new Point(130, 59);
        cmbServiceType.Name = "cmbServiceType";
        cmbServiceType.Size = new Size(330, 23);
        cmbServiceType.TabIndex = 2;
        //
        // lblDetails
        //
        lblDetails.AutoSize = true;
        lblDetails.Location = new Point(22, 100);
        lblDetails.Name = "lblDetails";
        lblDetails.Size = new Size(93, 15);
        lblDetails.TabIndex = 3;
        lblDetails.Text = "Request details:";
        //
        // txtDetails
        //
        txtDetails.Location = new Point(130, 97);
        txtDetails.MaxLength = 2000;
        txtDetails.Multiline = true;
        txtDetails.Name = "txtDetails";
        txtDetails.ScrollBars = ScrollBars.Vertical;
        txtDetails.Size = new Size(330, 150);
        txtDetails.TabIndex = 4;
        //
        // btnSubmit
        //
        btnSubmit.Location = new Point(130, 262);
        btnSubmit.Name = "btnSubmit";
        btnSubmit.Size = new Size(150, 32);
        btnSubmit.TabIndex = 5;
        btnSubmit.Text = "Submit Request";
        btnSubmit.UseVisualStyleBackColor = true;
        btnSubmit.Click += btnSubmit_Click;
        //
        // btnCancel
        //
        btnCancel.Location = new Point(350, 262);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(110, 32);
        btnCancel.TabIndex = 6;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = true;
        btnCancel.Click += btnCancel_Click;
        //
        // NewTicketForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancel;
        ClientSize = new Size(490, 315);
        Controls.Add(lblTitle);
        Controls.Add(lblServiceType);
        Controls.Add(cmbServiceType);
        Controls.Add(lblDetails);
        Controls.Add(txtDetails);
        Controls.Add(btnSubmit);
        Controls.Add(btnCancel);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "NewTicketForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "GSTS – New Request";
        Load += NewTicketForm_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblTitle;
    private Label lblServiceType;
    private ComboBox cmbServiceType;
    private Label lblDetails;
    private TextBox txtDetails;
    private Button btnSubmit;
    private Button btnCancel;
}
