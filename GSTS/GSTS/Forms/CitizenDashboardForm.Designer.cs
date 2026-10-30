namespace GSTS.Forms;

partial class CitizenDashboardForm
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
        btnNewRequest = new Button();
        btnViewDetails = new Button();
        btnSearch = new Button();
        btnNotifications = new Button();
        btnRefresh = new Button();
        lblMyTickets = new Label();
        lblEmpty = new Label();
        gridTickets = new DataGridView();
        btnChangePassword = new Button();
        btnLogout = new Button();
        ((System.ComponentModel.ISupportInitialize)gridTickets).BeginInit();
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
        lblRole.Text = "Citizen dashboard";
        //
        // btnNewRequest
        //
        btnNewRequest.Location = new Point(15, 68);
        btnNewRequest.Name = "btnNewRequest";
        btnNewRequest.Size = new Size(150, 34);
        btnNewRequest.TabIndex = 2;
        btnNewRequest.Text = "New Service Request";
        btnNewRequest.UseVisualStyleBackColor = true;
        btnNewRequest.Click += btnNewRequest_Click;
        //
        // btnViewDetails
        //
        btnViewDetails.Location = new Point(175, 68);
        btnViewDetails.Name = "btnViewDetails";
        btnViewDetails.Size = new Size(150, 34);
        btnViewDetails.TabIndex = 3;
        btnViewDetails.Text = "Track Selected Ticket";
        btnViewDetails.UseVisualStyleBackColor = true;
        btnViewDetails.Click += btnViewDetails_Click;
        //
        // btnSearch
        //
        btnSearch.Location = new Point(335, 68);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(120, 34);
        btnSearch.TabIndex = 4;
        btnSearch.Text = "Search Tickets";
        btnSearch.UseVisualStyleBackColor = true;
        btnSearch.Click += btnSearch_Click;
        //
        // btnNotifications
        //
        btnNotifications.Location = new Point(465, 68);
        btnNotifications.Name = "btnNotifications";
        btnNotifications.Size = new Size(190, 34);
        btnNotifications.TabIndex = 5;
        btnNotifications.Text = "Notifications";
        btnNotifications.UseVisualStyleBackColor = true;
        btnNotifications.Click += btnNotifications_Click;
        //
        // btnRefresh
        //
        btnRefresh.Location = new Point(665, 68);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(90, 34);
        btnRefresh.TabIndex = 6;
        btnRefresh.Text = "Refresh";
        btnRefresh.UseVisualStyleBackColor = true;
        btnRefresh.Click += btnRefresh_Click;
        //
        // lblMyTickets
        //
        lblMyTickets.AutoSize = true;
        lblMyTickets.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblMyTickets.Location = new Point(15, 115);
        lblMyTickets.Name = "lblMyTickets";
        lblMyTickets.Size = new Size(65, 15);
        lblMyTickets.TabIndex = 7;
        lblMyTickets.Text = "My tickets";
        //
        // lblEmpty
        //
        lblEmpty.AutoSize = true;
        lblEmpty.ForeColor = Color.DimGray;
        lblEmpty.Location = new Point(100, 115);
        lblEmpty.Name = "lblEmpty";
        lblEmpty.Size = new Size(330, 15);
        lblEmpty.TabIndex = 8;
        lblEmpty.Text = "You have no tickets yet. Click \"New Service Request\" to start.";
        lblEmpty.Visible = false;
        //
        // gridTickets
        //
        gridTickets.AllowUserToAddRows = false;
        gridTickets.AllowUserToDeleteRows = false;
        gridTickets.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        gridTickets.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridTickets.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        gridTickets.Location = new Point(15, 135);
        gridTickets.MultiSelect = false;
        gridTickets.Name = "gridTickets";
        gridTickets.ReadOnly = true;
        gridTickets.RowHeadersVisible = false;
        gridTickets.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridTickets.Size = new Size(905, 340);
        gridTickets.TabIndex = 9;
        gridTickets.CellDoubleClick += gridTickets_CellDoubleClick;
        //
        // btnChangePassword
        //
        btnChangePassword.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnChangePassword.Location = new Point(15, 488);
        btnChangePassword.Name = "btnChangePassword";
        btnChangePassword.Size = new Size(150, 32);
        btnChangePassword.TabIndex = 10;
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
        btnLogout.TabIndex = 11;
        btnLogout.Text = "Log Out";
        btnLogout.UseVisualStyleBackColor = true;
        btnLogout.Click += btnLogout_Click;
        //
        // CitizenDashboardForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(935, 535);
        Controls.Add(lblWelcome);
        Controls.Add(lblRole);
        Controls.Add(btnNewRequest);
        Controls.Add(btnViewDetails);
        Controls.Add(btnSearch);
        Controls.Add(btnNotifications);
        Controls.Add(btnRefresh);
        Controls.Add(lblMyTickets);
        Controls.Add(lblEmpty);
        Controls.Add(gridTickets);
        Controls.Add(btnChangePassword);
        Controls.Add(btnLogout);
        MinimumSize = new Size(800, 400);
        Name = "CitizenDashboardForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "GSTS – Citizen";
        Load += CitizenDashboardForm_Load;
        ((System.ComponentModel.ISupportInitialize)gridTickets).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblWelcome;
    private Label lblRole;
    private Button btnNewRequest;
    private Button btnViewDetails;
    private Button btnSearch;
    private Button btnNotifications;
    private Button btnRefresh;
    private Label lblMyTickets;
    private Label lblEmpty;
    private DataGridView gridTickets;
    private Button btnChangePassword;
    private Button btnLogout;
}
