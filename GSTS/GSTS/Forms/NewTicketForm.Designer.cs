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
        lblName = new Label();
        txtName = new TextBox();
        lblIdNumber = new Label();
        txtIdNumber = new TextBox();
        lblIdHint = new Label();
        lblPhone = new Label();
        txtPhone = new TextBox();
        lblPhoneHint = new Label();
        lblReason = new Label();
        txtReason = new TextBox();
        lblAssignTo = new Label();
        cboAssignTo = new ComboBox();
        lblNote = new Label();
        txtNote = new TextBox();
        btnCreate = new Button();
        btnCancel = new Button();
        SuspendLayout();
        //
        // lblTitle
        //
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblTitle.Location = new Point(15, 12);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(110, 25);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "New Ticket";
        //
        // lblName
        //
        lblName.AutoSize = true;
        lblName.Location = new Point(18, 58);
        lblName.Name = "lblName";
        lblName.Size = new Size(90, 15);
        lblName.TabIndex = 1;
        lblName.Text = "Citizen's name:";
        //
        // txtName
        //
        txtName.Location = new Point(150, 55);
        txtName.MaxLength = 100;
        txtName.Name = "txtName";
        txtName.Size = new Size(320, 23);
        txtName.TabIndex = 2;
        //
        // lblIdNumber
        //
        lblIdNumber.AutoSize = true;
        lblIdNumber.Location = new Point(18, 93);
        lblIdNumber.Name = "lblIdNumber";
        lblIdNumber.Size = new Size(70, 15);
        lblIdNumber.TabIndex = 3;
        lblIdNumber.Text = "ID number:";
        //
        // txtIdNumber
        //
        txtIdNumber.Location = new Point(150, 90);
        txtIdNumber.MaxLength = 12;
        txtIdNumber.Name = "txtIdNumber";
        txtIdNumber.Size = new Size(160, 23);
        txtIdNumber.TabIndex = 4;
        //
        // lblIdHint
        //
        lblIdHint.AutoSize = true;
        lblIdHint.ForeColor = Color.DimGray;
        lblIdHint.Location = new Point(318, 93);
        lblIdHint.Name = "lblIdHint";
        lblIdHint.Size = new Size(160, 15);
        lblIdHint.TabIndex = 5;
        lblIdHint.Text = "e.g. 901234567V or 199012345678";
        //
        // lblPhone
        //
        lblPhone.AutoSize = true;
        lblPhone.Location = new Point(18, 128);
        lblPhone.Name = "lblPhone";
        lblPhone.Size = new Size(90, 15);
        lblPhone.TabIndex = 6;
        lblPhone.Text = "Phone number:";
        //
        // txtPhone
        //
        txtPhone.Location = new Point(150, 125);
        txtPhone.MaxLength = 10;
        txtPhone.Name = "txtPhone";
        txtPhone.Size = new Size(160, 23);
        txtPhone.TabIndex = 7;
        //
        // lblPhoneHint
        //
        lblPhoneHint.AutoSize = true;
        lblPhoneHint.ForeColor = Color.DimGray;
        lblPhoneHint.Location = new Point(318, 128);
        lblPhoneHint.Name = "lblPhoneHint";
        lblPhoneHint.Size = new Size(100, 15);
        lblPhoneHint.TabIndex = 8;
        lblPhoneHint.Text = "e.g. 0771234567";
        //
        // lblReason
        //
        lblReason.AutoSize = true;
        lblReason.Location = new Point(18, 163);
        lblReason.Name = "lblReason";
        lblReason.Size = new Size(100, 15);
        lblReason.TabIndex = 9;
        lblReason.Text = "Reason for visit:";
        //
        // txtReason
        //
        txtReason.Location = new Point(150, 160);
        txtReason.MaxLength = 500;
        txtReason.Multiline = true;
        txtReason.Name = "txtReason";
        txtReason.ScrollBars = ScrollBars.Vertical;
        txtReason.Size = new Size(320, 70);
        txtReason.TabIndex = 10;
        //
        // lblAssignTo
        //
        lblAssignTo.AutoSize = true;
        lblAssignTo.Location = new Point(18, 248);
        lblAssignTo.Name = "lblAssignTo";
        lblAssignTo.Size = new Size(60, 15);
        lblAssignTo.TabIndex = 11;
        lblAssignTo.Text = "Assign to:";
        //
        // cboAssignTo
        //
        cboAssignTo.DropDownStyle = ComboBoxStyle.DropDownList;
        cboAssignTo.Location = new Point(150, 245);
        cboAssignTo.Name = "cboAssignTo";
        cboAssignTo.Size = new Size(320, 23);
        cboAssignTo.TabIndex = 12;
        //
        // lblNote
        //
        lblNote.AutoSize = true;
        lblNote.Location = new Point(18, 285);
        lblNote.Name = "lblNote";
        lblNote.Size = new Size(110, 15);
        lblNote.TabIndex = 13;
        lblNote.Text = "Note (optional):";
        //
        // txtNote
        //
        txtNote.Location = new Point(150, 282);
        txtNote.MaxLength = 1000;
        txtNote.Multiline = true;
        txtNote.Name = "txtNote";
        txtNote.ScrollBars = ScrollBars.Vertical;
        txtNote.Size = new Size(320, 55);
        txtNote.TabIndex = 14;
        //
        // btnCreate
        //
        btnCreate.Location = new Point(240, 355);
        btnCreate.Name = "btnCreate";
        btnCreate.Size = new Size(110, 32);
        btnCreate.TabIndex = 15;
        btnCreate.Text = "Create Ticket";
        btnCreate.UseVisualStyleBackColor = true;
        btnCreate.Click += btnCreate_Click;
        //
        // btnCancel
        //
        btnCancel.Location = new Point(360, 355);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(110, 32);
        btnCancel.TabIndex = 16;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = true;
        btnCancel.Click += btnCancel_Click;
        //
        // NewTicketForm
        //
        AcceptButton = btnCreate;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancel;
        ClientSize = new Size(490, 405);
        Controls.Add(lblTitle);
        Controls.Add(lblName);
        Controls.Add(txtName);
        Controls.Add(lblIdNumber);
        Controls.Add(txtIdNumber);
        Controls.Add(lblIdHint);
        Controls.Add(lblPhone);
        Controls.Add(txtPhone);
        Controls.Add(lblPhoneHint);
        Controls.Add(lblReason);
        Controls.Add(txtReason);
        Controls.Add(lblAssignTo);
        Controls.Add(cboAssignTo);
        Controls.Add(lblNote);
        Controls.Add(txtNote);
        Controls.Add(btnCreate);
        Controls.Add(btnCancel);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "NewTicketForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "GSTS – New Ticket";
        Load += NewTicketForm_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblTitle;
    private Label lblName;
    private TextBox txtName;
    private Label lblIdNumber;
    private TextBox txtIdNumber;
    private Label lblIdHint;
    private Label lblPhone;
    private TextBox txtPhone;
    private Label lblPhoneHint;
    private Label lblReason;
    private TextBox txtReason;
    private Label lblAssignTo;
    private ComboBox cboAssignTo;
    private Label lblNote;
    private TextBox txtNote;
    private Button btnCreate;
    private Button btnCancel;
}
