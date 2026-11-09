namespace GSTS.Forms;

partial class TicketPickerForm
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
        lblCount = new Label();
        gridTickets = new DataGridView();
        btnView = new Button();
        btnClose = new Button();
        ((System.ComponentModel.ISupportInitialize)gridTickets).BeginInit();
        SuspendLayout();
        //
        // lblTitle
        //
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblTitle.Location = new Point(15, 12);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(160, 25);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Tickets";
        //
        // lblCount
        //
        lblCount.AutoSize = true;
        lblCount.ForeColor = Color.DimGray;
        lblCount.Location = new Point(18, 45);
        lblCount.Name = "lblCount";
        lblCount.Size = new Size(50, 15);
        lblCount.TabIndex = 1;
        lblCount.Text = "0 tickets";
        //
        // gridTickets
        //
        gridTickets.AllowUserToAddRows = false;
        gridTickets.AllowUserToDeleteRows = false;
        gridTickets.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        gridTickets.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridTickets.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        gridTickets.Location = new Point(15, 68);
        gridTickets.MultiSelect = false;
        gridTickets.Name = "gridTickets";
        gridTickets.ReadOnly = true;
        gridTickets.RowHeadersVisible = false;
        gridTickets.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridTickets.Size = new Size(850, 330);
        gridTickets.TabIndex = 2;
        gridTickets.CellDoubleClick += gridTickets_CellDoubleClick;
        //
        // btnView
        //
        btnView.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnView.Location = new Point(635, 412);
        btnView.Name = "btnView";
        btnView.Size = new Size(110, 32);
        btnView.TabIndex = 3;
        btnView.Text = "View Report";
        btnView.UseVisualStyleBackColor = true;
        btnView.Click += btnView_Click;
        //
        // btnClose
        //
        btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnClose.Location = new Point(755, 412);
        btnClose.Name = "btnClose";
        btnClose.Size = new Size(110, 32);
        btnClose.TabIndex = 4;
        btnClose.Text = "Close";
        btnClose.UseVisualStyleBackColor = true;
        btnClose.Click += btnClose_Click;
        //
        // TicketPickerForm
        //
        AcceptButton = btnView;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnClose;
        ClientSize = new Size(880, 458);
        Controls.Add(lblTitle);
        Controls.Add(lblCount);
        Controls.Add(gridTickets);
        Controls.Add(btnView);
        Controls.Add(btnClose);
        MinimizeBox = false;
        MinimumSize = new Size(600, 300);
        Name = "TicketPickerForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "GSTS – Select a Ticket";
        Load += TicketPickerForm_Load;
        ((System.ComponentModel.ISupportInitialize)gridTickets).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblTitle;
    private Label lblCount;
    private DataGridView gridTickets;
    private Button btnView;
    private Button btnClose;
}
