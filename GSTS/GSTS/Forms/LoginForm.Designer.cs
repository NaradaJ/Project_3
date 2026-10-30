namespace GSTS.Forms;

partial class LoginForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
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
        lblSubtitle = new Label();
        lblUsername = new Label();
        txtUsername = new TextBox();
        lblPassword = new Label();
        txtPassword = new TextBox();
        btnLogin = new Button();
        btnRegister = new Button();
        lblHint = new Label();
        SuspendLayout();
        //
        // lblTitle
        //
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTitle.Location = new Point(30, 20);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(380, 25);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Government Service Ticketing System";
        //
        // lblSubtitle
        //
        lblSubtitle.AutoSize = true;
        lblSubtitle.Location = new Point(32, 52);
        lblSubtitle.Name = "lblSubtitle";
        lblSubtitle.Size = new Size(150, 15);
        lblSubtitle.TabIndex = 1;
        lblSubtitle.Text = "Please log in to continue";
        //
        // lblUsername
        //
        lblUsername.AutoSize = true;
        lblUsername.Location = new Point(32, 95);
        lblUsername.Name = "lblUsername";
        lblUsername.Size = new Size(63, 15);
        lblUsername.TabIndex = 2;
        lblUsername.Text = "Username:";
        //
        // txtUsername
        //
        txtUsername.Location = new Point(130, 92);
        txtUsername.MaxLength = 50;
        txtUsername.Name = "txtUsername";
        txtUsername.Size = new Size(260, 23);
        txtUsername.TabIndex = 3;
        //
        // lblPassword
        //
        lblPassword.AutoSize = true;
        lblPassword.Location = new Point(32, 135);
        lblPassword.Name = "lblPassword";
        lblPassword.Size = new Size(60, 15);
        lblPassword.TabIndex = 4;
        lblPassword.Text = "Password:";
        //
        // txtPassword
        //
        txtPassword.Location = new Point(130, 132);
        txtPassword.Name = "txtPassword";
        txtPassword.Size = new Size(260, 23);
        txtPassword.TabIndex = 5;
        txtPassword.UseSystemPasswordChar = true;
        //
        // btnLogin
        //
        btnLogin.Location = new Point(130, 175);
        btnLogin.Name = "btnLogin";
        btnLogin.Size = new Size(120, 32);
        btnLogin.TabIndex = 6;
        btnLogin.Text = "Log In";
        btnLogin.UseVisualStyleBackColor = true;
        btnLogin.Click += btnLogin_Click;
        //
        // btnRegister
        //
        btnRegister.Location = new Point(260, 175);
        btnRegister.Name = "btnRegister";
        btnRegister.Size = new Size(130, 32);
        btnRegister.TabIndex = 7;
        btnRegister.Text = "Register (Citizen)";
        btnRegister.UseVisualStyleBackColor = true;
        btnRegister.Click += btnRegister_Click;
        //
        // lblHint
        //
        lblHint.AutoSize = true;
        lblHint.ForeColor = Color.DimGray;
        lblHint.Location = new Point(32, 225);
        lblHint.Name = "lblHint";
        lblHint.Size = new Size(300, 15);
        lblHint.TabIndex = 8;
        lblHint.Text = "Citizens and officers log in with their NIC number.";
        //
        // LoginForm
        //
        AcceptButton = btnLogin;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(430, 260);
        Controls.Add(lblTitle);
        Controls.Add(lblSubtitle);
        Controls.Add(lblUsername);
        Controls.Add(txtUsername);
        Controls.Add(lblPassword);
        Controls.Add(txtPassword);
        Controls.Add(btnLogin);
        Controls.Add(btnRegister);
        Controls.Add(lblHint);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "LoginForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "GSTS – Login";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblTitle;
    private Label lblSubtitle;
    private Label lblUsername;
    private TextBox txtUsername;
    private Label lblPassword;
    private TextBox txtPassword;
    private Button btnLogin;
    private Button btnRegister;
    private Label lblHint;
}
