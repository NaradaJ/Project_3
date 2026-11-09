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
        rdoMine = new RadioButton();
        rdoAll = new RadioButton();
        gridTickets = new DataGridView();
        lblEmpty = new Label();
        btnNewTicket = new Button();
        btnOpen = new Button();
        btnLookup = new Button();
        btnNotifications = new Button();
        btnRefresh = new Button();
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
        // rdoMine
        //
        rdoMine.AutoSize = true;
        rdoMine.Checked = true;
        rdoMine.Location = new Point(18, 50);
        rdoMine.Name = "rdoMine";
        rdoMine.Size = new Size(160, 19);
        rdoMine.TabIndex = 1;
        rdoMine.TabStop = true;
        rdoMine.Text = "Tickets assigned to me";
        rdoMine.UseVisualStyleBackColor = true;
        rdoMine.CheckedChanged += rdoMine_CheckedChanged;
        //
        // rdoAll
        //
        rdoAll.AutoSize = true;
        rdoAll.Location = new Point(200, 50);
        rdoAll.Name = "rdoAll";
        rdoAll.Size = new Size(130, 19);
        rdoAll.TabIndex = 2;
        rdoAll.Text = "All open tickets";
        rdoAll.UseVisualStyleBackColor = true;
        //
        // gridTickets
        //
        gridTickets.AllowUserToAddRows = false;
        gridTickets.AllowUserToDeleteRows = false;
        gridTickets.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        gridTickets.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridTickets.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        gridTickets.Location = new Point(15, 80);
        gridTickets.MultiSelect = false;
        gridTickets.Name = "gridTickets";
        gridTickets.ReadOnly = true;
        gridTickets.RowHeadersVisible = false;
        gridTickets.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridTickets.Size = new Size(850, 380);
        gridTickets.TabIndex = 3;
        gridTickets.CellDoubleClick += gridTickets_CellDoubleClick;
        //
        // lblEmpty
        //
        lblEmpty.AutoSize = true;
        lblEmpty.ForeColor = Color.DimGray;
        lblEmpty.Location = new Point(25, 125);
        lblEmpty.Name = "lblEmpty";
        lblEmpty.Size = new Size(150, 15);
        lblEmpty.TabIndex = 4;
        lblEmpty.Text = "No open tickets to show";
        lblEmpty.Visible = false;
        //
        // btnNewTicket
        //
        btnNewTicket.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnNewTicket.Location = new Point(15, 475);
        btnNewTicket.Name = "btnNewTicket";
        btnNewTicket.Size = new Size(120, 32);
        btnNewTicket.TabIndex = 5;
        btnNewTicket.Text = "New Ticket";
        btnNewTicket.UseVisualStyleBackColor = true;
        btnNewTicket.Click += btnNewTicket_Click;
        //
        // btnOpen
        //
        btnOpen.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnOpen.Location = new Point(145, 475);
        btnOpen.Name = "btnOpen";
        btnOpen.Size = new Size(120, 32);
        btnOpen.TabIndex = 6;
        btnOpen.Text = "Open Ticket";
        btnOpen.UseVisualStyleBackColor = true;
        btnOpen.Click += btnOpen_Click;
        //
        // btnLookup
        //
        btnLookup.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnLookup.Location = new Point(275, 475);
        btnLookup.Name = "btnLookup";
        btnLookup.Size = new Size(140, 32);
        btnLookup.TabIndex = 7;
        btnLookup.Text = "Look Up Ticket ID";
        btnLookup.UseVisualStyleBackColor = true;
        btnLookup.Click += btnLookup_Click;
        //
        // btnNotifications
        //
        btnNotifications.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnNotifications.Location = new Point(425, 475);
        btnNotifications.Name = "btnNotifications";
        btnNotifications.Size = new Size(180, 32);
        btnNotifications.TabIndex = 8;
        btnNotifications.Text = "Notifications";
        btnNotifications.UseVisualStyleBackColor = true;
        btnNotifications.Click += btnNotifications_Click;
        //
        // btnRefresh
        //
        btnRefresh.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnRefresh.Location = new Point(655, 475);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(100, 32);
        btnRefresh.TabIndex = 9;
        btnRefresh.Text = "Refresh";
        btnRefresh.UseVisualStyleBackColor = true;
        btnRefresh.Click += btnRefresh_Click;
        //
        // btnLogout
        //
        btnLogout.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnLogout.Location = new Point(765, 475);
        btnLogout.Name = "btnLogout";
        btnLogout.Size = new Size(100, 32);
        btnLogout.TabIndex = 10;
        btnLogout.Text = "Log Out";
        btnLogout.UseVisualStyleBackColor = true;
        btnLogout.Click += btnLogout_Click;
        //
        // OfficerDashboardForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(880, 520);
        Controls.Add(lblEmpty);
        Controls.Add(lblWelcome);
        Controls.Add(rdoMine);
        Controls.Add(rdoAll);
        Controls.Add(gridTickets);
        Controls.Add(btnNewTicket);
        Controls.Add(btnOpen);
        Controls.Add(btnLookup);
        Controls.Add(btnNotifications);
        Controls.Add(btnRefresh);
        Controls.Add(btnLogout);
        MinimumSize = new Size(896, 559);
        Name = "OfficerDashboardForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "GSTS – Officer Dashboard";
        Load += OfficerDashboardForm_Load;
        ((System.ComponentModel.ISupportInitialize)gridTickets).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblWelcome;
    private RadioButton rdoMine;
    private RadioButton rdoAll;
    private DataGridView gridTickets;
    private Label lblEmpty;
    private Button btnNewTicket;
    private Button btnOpen;
    private Button btnLookup;
    private Button btnNotifications;
    private Button btnRefresh;
    private Button btnLogout;
}
