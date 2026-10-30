namespace GSTS.Forms;

partial class AdminDashboardForm
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
        lblWelcome = new Label();
        lblRole = new Label();
        btnOfficers = new Button();
        btnServiceSteps = new Button();
        btnReassign = new Button();
        btnReports = new Button();
        btnSearch = new Button();
        btnNotifications = new Button();
        btnChangePassword = new Button();
        btnLogout = new Button();
        SuspendLayout();
        //
        // lblWelcome
        //
        lblWelcome.AutoSize = true;
        lblWelcome.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblWelcome.Location = new Point(25, 15);
        lblWelcome.Name = "lblWelcome";
        lblWelcome.Size = new Size(100, 25);
        lblWelcome.TabIndex = 0;
        lblWelcome.Text = "Welcome";
        //
        // lblRole
        //
        lblRole.AutoSize = true;
        lblRole.ForeColor = Color.DimGray;
        lblRole.Location = new Point(27, 43);
        lblRole.Name = "lblRole";
        lblRole.Size = new Size(130, 15);
        lblRole.TabIndex = 1;
        lblRole.Text = "Administrator dashboard";
        //
        // btnOfficers
        //
        btnOfficers.Location = new Point(25, 80);
        btnOfficers.Name = "btnOfficers";
        btnOfficers.Size = new Size(230, 45);
        btnOfficers.TabIndex = 2;
        btnOfficers.Text = "Manage Officers";
        btnOfficers.UseVisualStyleBackColor = true;
        btnOfficers.Click += btnOfficers_Click;
        //
        // btnServiceSteps
        //
        btnServiceSteps.Location = new Point(270, 80);
        btnServiceSteps.Name = "btnServiceSteps";
        btnServiceSteps.Size = new Size(230, 45);
        btnServiceSteps.TabIndex = 3;
        btnServiceSteps.Text = "Manage Service Types && Steps";
        btnServiceSteps.UseVisualStyleBackColor = true;
        btnServiceSteps.Click += btnServiceSteps_Click;
        //
        // btnReassign
        //
        btnReassign.Location = new Point(25, 135);
        btnReassign.Name = "btnReassign";
        btnReassign.Size = new Size(230, 45);
        btnReassign.TabIndex = 4;
        btnReassign.Text = "Reassign Ticket";
        btnReassign.UseVisualStyleBackColor = true;
        btnReassign.Click += btnReassign_Click;
        //
        // btnReports
        //
        btnReports.Location = new Point(270, 135);
        btnReports.Name = "btnReports";
        btnReports.Size = new Size(230, 45);
        btnReports.TabIndex = 5;
        btnReports.Text = "Reports";
        btnReports.UseVisualStyleBackColor = true;
        btnReports.Click += btnReports_Click;
        //
        // btnSearch
        //
        btnSearch.Location = new Point(25, 190);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(230, 45);
        btnSearch.TabIndex = 6;
        btnSearch.Text = "Search All Tickets";
        btnSearch.UseVisualStyleBackColor = true;
        btnSearch.Click += btnSearch_Click;
        //
        // btnNotifications
        //
        btnNotifications.Location = new Point(270, 190);
        btnNotifications.Name = "btnNotifications";
        btnNotifications.Size = new Size(230, 45);
        btnNotifications.TabIndex = 7;
        btnNotifications.Text = "Notifications";
        btnNotifications.UseVisualStyleBackColor = true;
        btnNotifications.Click += btnNotifications_Click;
        //
        // btnChangePassword
        //
        btnChangePassword.Location = new Point(25, 260);
        btnChangePassword.Name = "btnChangePassword";
        btnChangePassword.Size = new Size(150, 32);
        btnChangePassword.TabIndex = 8;
        btnChangePassword.Text = "Change Password";
        btnChangePassword.UseVisualStyleBackColor = true;
        btnChangePassword.Click += btnChangePassword_Click;
        //
        // btnLogout
        //
        btnLogout.Location = new Point(390, 260);
        btnLogout.Name = "btnLogout";
        btnLogout.Size = new Size(110, 32);
        btnLogout.TabIndex = 9;
        btnLogout.Text = "Log Out";
        btnLogout.UseVisualStyleBackColor = true;
        btnLogout.Click += btnLogout_Click;
        //
        // AdminDashboardForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(525, 310);
        Controls.Add(lblWelcome);
        Controls.Add(lblRole);
        Controls.Add(btnOfficers);
        Controls.Add(btnServiceSteps);
        Controls.Add(btnReassign);
        Controls.Add(btnReports);
        Controls.Add(btnSearch);
        Controls.Add(btnNotifications);
        Controls.Add(btnChangePassword);
        Controls.Add(btnLogout);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "AdminDashboardForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "GSTS – Administrator";
        Load += AdminDashboardForm_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblWelcome;
    private Label lblRole;
    private Button btnOfficers;
    private Button btnServiceSteps;
    private Button btnReassign;
    private Button btnReports;
    private Button btnSearch;
    private Button btnNotifications;
    private Button btnChangePassword;
    private Button btnLogout;
}
