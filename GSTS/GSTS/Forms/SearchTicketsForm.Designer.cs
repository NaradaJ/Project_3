namespace GSTS.Forms;

partial class SearchTicketsForm
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
        lblKeyword = new Label();
        txtKeyword = new TextBox();
        lblStatus = new Label();
        cmbStatus = new ComboBox();
        lblService = new Label();
        cmbService = new ComboBox();
        chkDate = new CheckBox();
        dtpFrom = new DateTimePicker();
        lblTo = new Label();
        dtpTo = new DateTimePicker();
        btnSearch = new Button();
        btnClear = new Button();
        lblResult = new Label();
        gridTickets = new DataGridView();
        btnView = new Button();
        btnClose = new Button();
        ((System.ComponentModel.ISupportInitialize)gridTickets).BeginInit();
        SuspendLayout();
        //
        // lblKeyword
        //
        lblKeyword.AutoSize = true;
        lblKeyword.Location = new Point(15, 18);
        lblKeyword.Name = "lblKeyword";
        lblKeyword.Size = new Size(180, 15);
        lblKeyword.TabIndex = 0;
        lblKeyword.Text = "Ticket no / citizen name / NIC:";
        //
        // txtKeyword
        //
        txtKeyword.Location = new Point(200, 15);
        txtKeyword.MaxLength = 100;
        txtKeyword.Name = "txtKeyword";
        txtKeyword.Size = new Size(230, 23);
        txtKeyword.TabIndex = 1;
        //
        // lblStatus
        //
        lblStatus.AutoSize = true;
        lblStatus.Location = new Point(450, 18);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(42, 15);
        lblStatus.TabIndex = 2;
        lblStatus.Text = "Status:";
        //
        // cmbStatus
        //
        cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbStatus.Location = new Point(510, 15);
        cmbStatus.Name = "cmbStatus";
        cmbStatus.Size = new Size(140, 23);
        cmbStatus.TabIndex = 3;
        //
        // lblService
        //
        lblService.AutoSize = true;
        lblService.Location = new Point(15, 55);
        lblService.Name = "lblService";
        lblService.Size = new Size(76, 15);
        lblService.TabIndex = 4;
        lblService.Text = "Service type:";
        //
        // cmbService
        //
        cmbService.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbService.Location = new Point(200, 52);
        cmbService.Name = "cmbService";
        cmbService.Size = new Size(230, 23);
        cmbService.TabIndex = 5;
        //
        // chkDate
        //
        chkDate.AutoSize = true;
        chkDate.Location = new Point(450, 54);
        chkDate.Name = "chkDate";
        chkDate.Size = new Size(110, 19);
        chkDate.TabIndex = 6;
        chkDate.Text = "Created from:";
        chkDate.UseVisualStyleBackColor = true;
        chkDate.CheckedChanged += chkDate_CheckedChanged;
        //
        // dtpFrom
        //
        dtpFrom.Format = DateTimePickerFormat.Short;
        dtpFrom.Location = new Point(565, 52);
        dtpFrom.Name = "dtpFrom";
        dtpFrom.Size = new Size(110, 23);
        dtpFrom.TabIndex = 7;
        //
        // lblTo
        //
        lblTo.AutoSize = true;
        lblTo.Location = new Point(683, 55);
        lblTo.Name = "lblTo";
        lblTo.Size = new Size(20, 15);
        lblTo.TabIndex = 8;
        lblTo.Text = "to";
        //
        // dtpTo
        //
        dtpTo.Format = DateTimePickerFormat.Short;
        dtpTo.Location = new Point(710, 52);
        dtpTo.Name = "dtpTo";
        dtpTo.Size = new Size(110, 23);
        dtpTo.TabIndex = 9;
        //
        // btnSearch
        //
        btnSearch.Location = new Point(200, 88);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(110, 30);
        btnSearch.TabIndex = 10;
        btnSearch.Text = "Search";
        btnSearch.UseVisualStyleBackColor = true;
        btnSearch.Click += btnSearch_Click;
        //
        // btnClear
        //
        btnClear.Location = new Point(320, 88);
        btnClear.Name = "btnClear";
        btnClear.Size = new Size(110, 30);
        btnClear.TabIndex = 11;
        btnClear.Text = "Clear";
        btnClear.UseVisualStyleBackColor = true;
        btnClear.Click += btnClear_Click;
        //
        // lblResult
        //
        lblResult.AutoSize = true;
        lblResult.Location = new Point(450, 96);
        lblResult.Name = "lblResult";
        lblResult.Size = new Size(0, 15);
        lblResult.TabIndex = 12;
        //
        // gridTickets
        //
        gridTickets.AllowUserToAddRows = false;
        gridTickets.AllowUserToDeleteRows = false;
        gridTickets.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        gridTickets.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridTickets.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        gridTickets.Location = new Point(15, 130);
        gridTickets.MultiSelect = false;
        gridTickets.Name = "gridTickets";
        gridTickets.ReadOnly = true;
        gridTickets.RowHeadersVisible = false;
        gridTickets.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridTickets.Size = new Size(905, 330);
        gridTickets.TabIndex = 13;
        gridTickets.CellDoubleClick += gridTickets_CellDoubleClick;
        //
        // btnView
        //
        btnView.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnView.Location = new Point(15, 472);
        btnView.Name = "btnView";
        btnView.Size = new Size(150, 32);
        btnView.TabIndex = 14;
        btnView.Text = "View Details";
        btnView.UseVisualStyleBackColor = true;
        btnView.Click += btnView_Click;
        //
        // btnClose
        //
        btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnClose.Location = new Point(810, 472);
        btnClose.Name = "btnClose";
        btnClose.Size = new Size(110, 32);
        btnClose.TabIndex = 15;
        btnClose.Text = "Close";
        btnClose.UseVisualStyleBackColor = true;
        btnClose.Click += btnClose_Click;
        //
        // SearchTicketsForm
        //
        AcceptButton = btnSearch;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnClose;
        ClientSize = new Size(935, 520);
        Controls.Add(lblKeyword);
        Controls.Add(txtKeyword);
        Controls.Add(lblStatus);
        Controls.Add(cmbStatus);
        Controls.Add(lblService);
        Controls.Add(cmbService);
        Controls.Add(chkDate);
        Controls.Add(dtpFrom);
        Controls.Add(lblTo);
        Controls.Add(dtpTo);
        Controls.Add(btnSearch);
        Controls.Add(btnClear);
        Controls.Add(lblResult);
        Controls.Add(gridTickets);
        Controls.Add(btnView);
        Controls.Add(btnClose);
        MinimumSize = new Size(860, 400);
        Name = "SearchTicketsForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "GSTS – Search Tickets";
        Load += SearchTicketsForm_Load;
        ((System.ComponentModel.ISupportInitialize)gridTickets).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblKeyword;
    private TextBox txtKeyword;
    private Label lblStatus;
    private ComboBox cmbStatus;
    private Label lblService;
    private ComboBox cmbService;
    private CheckBox chkDate;
    private DateTimePicker dtpFrom;
    private Label lblTo;
    private DateTimePicker dtpTo;
    private Button btnSearch;
    private Button btnClear;
    private Label lblResult;
    private DataGridView gridTickets;
    private Button btnView;
    private Button btnClose;
}
