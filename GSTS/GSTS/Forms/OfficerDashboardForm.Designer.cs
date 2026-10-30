namespace GSTS.Forms;

partial class OfficerDashboardForm
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
        btnProcess = new Button();
        btnSearch = new Button();
        btnNotifications = new Button();
        btnRefresh = new Button();
        lblAssigned = new Label();
        lblEmpty = new Label();
        gridAssigned = new DataGridView();
        btnChangePassword = new Button();
        btnLogout = new Button();
        ((System.ComponentModel.ISupportInitialize)gridAssigned).BeginInit();
        SuspendLayout();
        //
        // lblWelcome
        //
        lblWelcome.AutoSize = true;
        lblWelcome.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblWelcome.Location = new Point(15, 12);
        lblWelcome.Name = "lblWelcome";
        lblWelcome.Size = new Size(100, 25);
        lblWelcome.TabIndex = 0;
        lblWelcome.Text = "Welcome";
        //
        // lblRole
        //
        lblRole.AutoSize = true;
        lblRole.ForeColor = Color.DimGray;
        lblRole.Location = new Point(17, 40);
        lblRole.Name = "lblRole";
        lblRole.Size = new Size(100, 15);
        lblRole.TabIndex = 1;
        lblRole.Text = "Officer dashboard";
        //
        // btnProcess
        //
        btnProcess.Location = new Point(15, 68);
        btnProcess.Name = "btnProcess";
        btnProcess.Size = new Size(170, 34);
        btnProcess.TabIndex = 2;
        btnProcess.Text = "Process Selected Ticket";
        btnProcess.UseVisualStyleBackColor = true;
        btnProcess.Click += btnProcess_Click;
        //
        // btnSearch
        //
        btnSearch.Location = new Point(195, 68);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(120, 34);
        btnSearch.TabIndex = 3;
        btnSearch.Text = "Search Tickets";
        btnSearch.UseVisualStyleBackColor = true;
        btnSearch.Click += btnSearch_Click;
        //
        // btnNotifications
        //
        btnNotifications.Location = new Point(325, 68);
        btnNotifications.Name = "btnNotifications";
        btnNotifications.Size = new Size(190, 34);
        btnNotifications.TabIndex = 4;
        btnNotifications.Text = "Notifications";
        btnNotifications.UseVisualStyleBackColor = true;
        btnNotifications.Click += btnNotifications_Click;
        //
        // btnRefresh
        //
        btnRefresh.Location = new Point(525, 68);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(90, 34);
        btnRefresh.TabIndex = 5;
        btnRefresh.Text = "Refresh";
        btnRefresh.UseVisualStyleBackColor = true;
        btnRefresh.Click += btnRefresh_Click;
        //
        // lblAssigned
        //
        lblAssigned.AutoSize = true;
        lblAssigned.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblAssigned.Location = new Point(15, 115);
        lblAssigned.Name = "lblAssigned";
        lblAssigned.Size = new Size(160, 15);
        lblAssigned.TabIndex = 6;
        lblAssigned.Text = "Tickets assigned to me";
        //
        // lblEmpty
        //
        lblEmpty.AutoSize = true;
        lblEmpty.ForeColor = Color.DimGray;
        lblEmpty.Location = new Point(180, 115);
        lblEmpty.Name = "lblEmpty";
        lblEmpty.Size = new Size(190, 15);
        lblEmpty.TabIndex = 7;
        lblEmpty.Text = "No tickets are assigned to you";
        lblEmpty.Visible = false;
        //
        // gridAssigned
        //
        gridAssigned.AllowUserToAddRows = false;
        gridAssigned.AllowUserToDeleteRows = false;
        gridAssigned.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        gridAssigned.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridAssigned.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        gridAssigned.Location = new Point(15, 135);
        gridAssigned.MultiSelect = false;
        gridAssigned.Name = "gridAssigned";
        gridAssigned.ReadOnly = true;
        gridAssigned.RowHeadersVisible = false;
        gridAssigned.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridAssigned.Size = new Size(905, 340);
        gridAssigned.TabIndex = 8;
        gridAssigned.CellDoubleClick += gridAssigned_CellDoubleClick;
        //
        // btnChangePassword
        //
        btnChangePassword.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnChangePassword.Location = new Point(15, 488);
        btnChangePassword.Name = "btnChangePassword";
        btnChangePassword.Size = new Size(150, 32);
        btnChangePassword.TabIndex = 9;
        btnChangePassword.Text = "Change Password";
        btnChangePassword.UseVisualStyleBackColor = true;
        btnChangePassword.Click += btnChangePassword_Click;
        //
        // btnLogout
        //
        btnLogout.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnLogout.Location = new Point(810, 488);
        btnLogout.Name = "btnLogout";
        btnLogout.Size = new Size(110, 32);
        btnLogout.TabIndex = 10;
        btnLogout.Text = "Log Out";
        btnLogout.UseVisualStyleBackColor = true;
        btnLogout.Click += btnLogout_Click;
        //
        // OfficerDashboardForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(935, 535);
        Controls.Add(lblWelcome);
        Controls.Add(lblRole);
        Controls.Add(btnProcess);
        Controls.Add(btnSearch);
        Controls.Add(btnNotifications);
        Controls.Add(btnRefresh);
        Controls.Add(lblAssigned);
        Controls.Add(lblEmpty);
        Controls.Add(gridAssigned);
        Controls.Add(btnChangePassword);
        Controls.Add(btnLogout);
        MinimumSize = new Size(800, 400);
        Name = "OfficerDashboardForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "GSTS – Officer";
        Load += OfficerDashboardForm_Load;
        ((System.ComponentModel.ISupportInitialize)gridAssigned).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblWelcome;
    private Label lblRole;
    private Button btnProcess;
    private Button btnSearch;
    private Button btnNotifications;
    private Button btnRefresh;
    private Label lblAssigned;
    private Label lblEmpty;
    private DataGridView gridAssigned;
    private Button btnChangePassword;
    private Button btnLogout;
}
