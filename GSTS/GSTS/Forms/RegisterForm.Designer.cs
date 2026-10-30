namespace GSTS.Forms;

partial class RegisterForm
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
        lblNic = new Label();
        txtNic = new TextBox();
        lblNicHint = new Label();
        lblContact = new Label();
        txtContact = new TextBox();
        lblContactHint = new Label();
        lblAddress = new Label();
        txtAddress = new TextBox();
        lblPassword = new Label();
        txtPassword = new TextBox();
        lblPasswordHint = new Label();
        btnRegister = new Button();
        btnCancel = new Button();
        SuspendLayout();
        //
        // lblTitle
        //
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblTitle.Location = new Point(25, 18);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(200, 25);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Citizen Registration";
        //
        // lblName
        //
        lblName.AutoSize = true;
        lblName.Location = new Point(28, 65);
        lblName.Name = "lblName";
        lblName.Size = new Size(64, 15);
        lblName.TabIndex = 1;
        lblName.Text = "Full name:";
        //
        // txtName
        //
        txtName.Location = new Point(140, 62);
        txtName.MaxLength = 100;
        txtName.Name = "txtName";
        txtName.Size = new Size(280, 23);
        txtName.TabIndex = 2;
        //
        // lblNic
        //
        lblNic.AutoSize = true;
        lblNic.Location = new Point(28, 100);
        lblNic.Name = "lblNic";
        lblNic.Size = new Size(76, 15);
        lblNic.TabIndex = 3;
        lblNic.Text = "NIC number:";
        //
        // txtNic
        //
        txtNic.Location = new Point(140, 97);
        txtNic.MaxLength = 12;
        txtNic.Name = "txtNic";
        txtNic.Size = new Size(280, 23);
        txtNic.TabIndex = 4;
        //
        // lblNicHint
        //
        lblNicHint.AutoSize = true;
        lblNicHint.ForeColor = Color.DimGray;
        lblNicHint.Location = new Point(140, 122);
        lblNicHint.Name = "lblNicHint";
        lblNicHint.Size = new Size(260, 15);
        lblNicHint.TabIndex = 5;
        lblNicHint.Text = "9 digits + V/X (e.g. 901234567V) or 12 digits";
        //
        // lblContact
        //
        lblContact.AutoSize = true;
        lblContact.Location = new Point(28, 150);
        lblContact.Name = "lblContact";
        lblContact.Size = new Size(97, 15);
        lblContact.TabIndex = 6;
        lblContact.Text = "Contact number:";
        //
        // txtContact
        //
        txtContact.Location = new Point(140, 147);
        txtContact.MaxLength = 10;
        txtContact.Name = "txtContact";
        txtContact.Size = new Size(280, 23);
        txtContact.TabIndex = 7;
        //
        // lblContactHint
        //
        lblContactHint.AutoSize = true;
        lblContactHint.ForeColor = Color.DimGray;
        lblContactHint.Location = new Point(140, 172);
        lblContactHint.Name = "lblContactHint";
        lblContactHint.Size = new Size(200, 15);
        lblContactHint.TabIndex = 8;
        lblContactHint.Text = "10 digits starting with 0 (e.g. 0771234567)";
        //
        // lblAddress
        //
        lblAddress.AutoSize = true;
        lblAddress.Location = new Point(28, 200);
        lblAddress.Name = "lblAddress";
        lblAddress.Size = new Size(52, 15);
        lblAddress.TabIndex = 9;
        lblAddress.Text = "Address:";
        //
        // txtAddress
        //
        txtAddress.Location = new Point(140, 197);
        txtAddress.MaxLength = 255;
        txtAddress.Multiline = true;
        txtAddress.Name = "txtAddress";
        txtAddress.Size = new Size(280, 50);
        txtAddress.TabIndex = 10;
        //
        // lblPassword
        //
        lblPassword.AutoSize = true;
        lblPassword.Location = new Point(28, 263);
        lblPassword.Name = "lblPassword";
        lblPassword.Size = new Size(60, 15);
        lblPassword.TabIndex = 11;
        lblPassword.Text = "Password:";
        //
        // txtPassword
        //
        txtPassword.Location = new Point(140, 260);
        txtPassword.Name = "txtPassword";
        txtPassword.Size = new Size(280, 23);
        txtPassword.TabIndex = 12;
        txtPassword.UseSystemPasswordChar = true;
        //
        // lblPasswordHint
        //
        lblPasswordHint.AutoSize = true;
        lblPasswordHint.ForeColor = Color.DimGray;
        lblPasswordHint.Location = new Point(140, 285);
        lblPasswordHint.Name = "lblPasswordHint";
        lblPasswordHint.Size = new Size(140, 15);
        lblPasswordHint.TabIndex = 13;
        lblPasswordHint.Text = "At least 8 characters";
        //
        // btnRegister
        //
        btnRegister.Location = new Point(140, 318);
        btnRegister.Name = "btnRegister";
        btnRegister.Size = new Size(130, 32);
        btnRegister.TabIndex = 14;
        btnRegister.Text = "Register";
        btnRegister.UseVisualStyleBackColor = true;
        btnRegister.Click += btnRegister_Click;
        //
        // btnCancel
        //
        btnCancel.Location = new Point(290, 318);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(130, 32);
        btnCancel.TabIndex = 15;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = true;
        btnCancel.Click += btnCancel_Click;
        //
        // RegisterForm
        //
        AcceptButton = btnRegister;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancel;
        ClientSize = new Size(460, 375);
        Controls.Add(lblTitle);
        Controls.Add(lblName);
        Controls.Add(txtName);
        Controls.Add(lblNic);
        Controls.Add(txtNic);
        Controls.Add(lblNicHint);
        Controls.Add(lblContact);
        Controls.Add(txtContact);
        Controls.Add(lblContactHint);
        Controls.Add(lblAddress);
        Controls.Add(txtAddress);
        Controls.Add(lblPassword);
        Controls.Add(txtPassword);
        Controls.Add(lblPasswordHint);
        Controls.Add(btnRegister);
        Controls.Add(btnCancel);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "RegisterForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "GSTS – Register";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblTitle;
    private Label lblName;
    private TextBox txtName;
    private Label lblNic;
    private TextBox txtNic;
    private Label lblNicHint;
    private Label lblContact;
    private TextBox txtContact;
    private Label lblContactHint;
    private Label lblAddress;
    private TextBox txtAddress;
    private Label lblPassword;
    private TextBox txtPassword;
    private Label lblPasswordHint;
    private Button btnRegister;
    private Button btnCancel;
}
