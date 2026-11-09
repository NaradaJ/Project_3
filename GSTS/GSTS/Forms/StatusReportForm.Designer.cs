namespace GSTS.Forms;

partial class StatusReportForm
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
        components = new System.ComponentModel.Container();
        lblTitle = new Label();
        lblTicketId = new Label();
        txtTicketId = new TextBox();
        btnSearch = new Button();
        lblElapsed = new Label();
        lblDetails = new Label();
        lblHandlers = new Label();
        gridHandlers = new DataGridView();
        lblSteps = new Label();
        gridSteps = new DataGridView();
        lblLog = new Label();
        gridLog = new DataGridView();
        btnClose = new Button();
        timerElapsed = new System.Windows.Forms.Timer(components);
        ((System.ComponentModel.ISupportInitialize)gridHandlers).BeginInit();
        ((System.ComponentModel.ISupportInitialize)gridSteps).BeginInit();
        ((System.ComponentModel.ISupportInitialize)gridLog).BeginInit();
        SuspendLayout();
        //
        // lblTitle
        //
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblTitle.Location = new Point(15, 12);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(200, 25);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Ticket Status Report";
        //
        // lblTicketId
        //
        lblTicketId.AutoSize = true;
        lblTicketId.Location = new Point(18, 53);
        lblTicketId.Name = "lblTicketId";
        lblTicketId.Size = new Size(57, 15);
        lblTicketId.TabIndex = 1;
        lblTicketId.Text = "Search:";
        //
        // txtTicketId
        //
        txtTicketId.Location = new Point(90, 50);
        txtTicketId.MaxLength = 100;
        txtTicketId.Name = "txtTicketId";
        txtTicketId.Size = new Size(200, 23);
        txtTicketId.TabIndex = 2;
        //
        // btnSearch
        //
        btnSearch.Location = new Point(300, 47);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(100, 28);
        btnSearch.TabIndex = 3;
        btnSearch.Text = "Search";
        btnSearch.UseVisualStyleBackColor = true;
        btnSearch.Click += btnSearch_Click;
        //
        // lblElapsed
        //
        lblElapsed.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblElapsed.ForeColor = Color.DarkBlue;
        lblElapsed.Location = new Point(520, 50);
        lblElapsed.Name = "lblElapsed";
        lblElapsed.Size = new Size(365, 22);
        lblElapsed.TabIndex = 4;
        lblElapsed.TextAlign = ContentAlignment.MiddleRight;
        //
        // lblDetails
        //
        lblDetails.BorderStyle = BorderStyle.FixedSingle;
        lblDetails.Location = new Point(15, 85);
        lblDetails.Name = "lblDetails";
        lblDetails.Padding = new Padding(6);
        lblDetails.Size = new Size(870, 95);
        lblDetails.TabIndex = 5;
        lblDetails.Text = "Type a ticket ID, or a citizen's name or NIC, and click Search.\nLeave the box empty and click Search to list the completed tickets.";
        //
        // lblHandlers
        //
        lblHandlers.AutoSize = true;
        lblHandlers.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblHandlers.Location = new Point(15, 190);
        lblHandlers.Name = "lblHandlers";
        lblHandlers.Size = new Size(250, 15);
        lblHandlers.TabIndex = 6;
        lblHandlers.Text = "Officers who handled the ticket (first action)";
        //
        // gridHandlers
        //
        gridHandlers.AllowUserToAddRows = false;
        gridHandlers.AllowUserToDeleteRows = false;
        gridHandlers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridHandlers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        gridHandlers.Location = new Point(15, 210);
        gridHandlers.Name = "gridHandlers";
        gridHandlers.ReadOnly = true;
        gridHandlers.RowHeadersVisible = false;
        gridHandlers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridHandlers.Size = new Size(420, 150);
        gridHandlers.TabIndex = 7;
        //
        // lblSteps
        //
        lblSteps.AutoSize = true;
        lblSteps.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblSteps.Location = new Point(450, 190);
        lblSteps.Name = "lblSteps";
        lblSteps.Size = new Size(160, 15);
        lblSteps.TabIndex = 8;
        lblSteps.Text = "Steps (* = compulsory)";
        //
        // gridSteps
        //
        gridSteps.AllowUserToAddRows = false;
        gridSteps.AllowUserToDeleteRows = false;
        gridSteps.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridSteps.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        gridSteps.Location = new Point(450, 210);
        gridSteps.Name = "gridSteps";
        gridSteps.ReadOnly = true;
        gridSteps.RowHeadersVisible = false;
        gridSteps.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridSteps.Size = new Size(435, 150);
        gridSteps.TabIndex = 9;
        //
        // lblLog
        //
        lblLog.AutoSize = true;
        lblLog.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblLog.Location = new Point(15, 370);
        lblLog.Name = "lblLog";
        lblLog.Size = new Size(180, 15);
        lblLog.TabIndex = 10;
        lblLog.Text = "Activity log (newest first)";
        //
        // gridLog
        //
        gridLog.AllowUserToAddRows = false;
        gridLog.AllowUserToDeleteRows = false;
        gridLog.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridLog.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        gridLog.Location = new Point(15, 390);
        gridLog.Name = "gridLog";
        gridLog.ReadOnly = true;
        gridLog.RowHeadersVisible = false;
        gridLog.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridLog.Size = new Size(870, 225);
        gridLog.TabIndex = 11;
        //
        // btnClose
        //
        btnClose.Location = new Point(775, 625);
        btnClose.Name = "btnClose";
        btnClose.Size = new Size(110, 32);
        btnClose.TabIndex = 12;
        btnClose.Text = "Close";
        btnClose.UseVisualStyleBackColor = true;
        btnClose.Click += btnClose_Click;
        //
        // timerElapsed
        //
        timerElapsed.Interval = 1000;
        timerElapsed.Tick += timerElapsed_Tick;
        //
        // StatusReportForm
        //
        AcceptButton = btnSearch;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnClose;
        ClientSize = new Size(900, 670);
        Controls.Add(lblTitle);
        Controls.Add(lblTicketId);
        Controls.Add(txtTicketId);
        Controls.Add(btnSearch);
        Controls.Add(lblElapsed);
        Controls.Add(lblDetails);
        Controls.Add(lblHandlers);
        Controls.Add(gridHandlers);
        Controls.Add(lblSteps);
        Controls.Add(gridSteps);
        Controls.Add(lblLog);
        Controls.Add(gridLog);
        Controls.Add(btnClose);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "StatusReportForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "GSTS – Ticket Status Report";
        FormClosed += StatusReportForm_FormClosed;
        Load += StatusReportForm_Load;
        ((System.ComponentModel.ISupportInitialize)gridHandlers).EndInit();
        ((System.ComponentModel.ISupportInitialize)gridSteps).EndInit();
        ((System.ComponentModel.ISupportInitialize)gridLog).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblTitle;
    private Label lblTicketId;
    private TextBox txtTicketId;
    private Button btnSearch;
    private Label lblElapsed;
    private Label lblDetails;
    private Label lblHandlers;
    private DataGridView gridHandlers;
    private Label lblSteps;
    private DataGridView gridSteps;
    private Label lblLog;
    private DataGridView gridLog;
    private Button btnClose;
    private System.Windows.Forms.Timer timerElapsed;
}
