namespace GSTS.Forms;

partial class NotificationsForm
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
        gridNotifications = new DataGridView();
        btnMarkRead = new Button();
        btnClose = new Button();
        ((System.ComponentModel.ISupportInitialize)gridNotifications).BeginInit();
        SuspendLayout();
        //
        // lblTitle
        //
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblTitle.Location = new Point(15, 12);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(130, 25);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Notifications";
        //
        // lblCount
        //
        lblCount.AutoSize = true;
        lblCount.Location = new Point(180, 19);
        lblCount.Name = "lblCount";
        lblCount.Size = new Size(50, 15);
        lblCount.TabIndex = 1;
        lblCount.Text = "0 unread";
        //
        // gridNotifications
        //
        gridNotifications.AllowUserToAddRows = false;
        gridNotifications.AllowUserToDeleteRows = false;
        gridNotifications.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        gridNotifications.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridNotifications.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        gridNotifications.Location = new Point(15, 50);
        gridNotifications.MultiSelect = false;
        gridNotifications.Name = "gridNotifications";
        gridNotifications.ReadOnly = true;
        gridNotifications.RowHeadersVisible = false;
        gridNotifications.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridNotifications.Size = new Size(750, 330);
        gridNotifications.TabIndex = 2;
        gridNotifications.CellDoubleClick += gridNotifications_CellDoubleClick;
        //
        // btnMarkRead
        //
        btnMarkRead.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnMarkRead.Location = new Point(15, 392);
        btnMarkRead.Name = "btnMarkRead";
        btnMarkRead.Size = new Size(150, 32);
        btnMarkRead.TabIndex = 3;
        btnMarkRead.Text = "Mark as Read";
        btnMarkRead.UseVisualStyleBackColor = true;
        btnMarkRead.Click += btnMarkRead_Click;
        //
        // btnClose
        //
        btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnClose.Location = new Point(655, 392);
        btnClose.Name = "btnClose";
        btnClose.Size = new Size(110, 32);
        btnClose.TabIndex = 4;
        btnClose.Text = "Close";
        btnClose.UseVisualStyleBackColor = true;
        btnClose.Click += btnClose_Click;
        //
        // NotificationsForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnClose;
        ClientSize = new Size(780, 440);
        Controls.Add(lblTitle);
        Controls.Add(lblCount);
        Controls.Add(gridNotifications);
        Controls.Add(btnMarkRead);
        Controls.Add(btnClose);
        MinimumSize = new Size(600, 350);
        Name = "NotificationsForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "GSTS – Notifications";
        Load += NotificationsForm_Load;
        ((System.ComponentModel.ISupportInitialize)gridNotifications).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblTitle;
    private Label lblCount;
    private DataGridView gridNotifications;
    private Button btnMarkRead;
    private Button btnClose;
}
