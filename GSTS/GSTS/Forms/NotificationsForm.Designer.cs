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
        lblMessage = new Label();
        txtMessage = new TextBox();
        lblReply = new Label();
        txtReply = new TextBox();
        btnMarkRead = new Button();
        btnAcceptFlag = new Button();
        btnDenyFlag = new Button();
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
        gridNotifications.Size = new Size(750, 230);
        gridNotifications.TabIndex = 2;
        gridNotifications.CellDoubleClick += gridNotifications_CellDoubleClick;
        gridNotifications.SelectionChanged += gridNotifications_SelectionChanged;
        //
        // lblMessage
        //
        lblMessage.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblMessage.AutoSize = true;
        lblMessage.Location = new Point(15, 290);
        lblMessage.Name = "lblMessage";
        lblMessage.Size = new Size(80, 15);
        lblMessage.TabIndex = 3;
        lblMessage.Text = "Full message:";
        //
        // txtMessage
        //
        txtMessage.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        txtMessage.Location = new Point(15, 310);
        txtMessage.Multiline = true;
        txtMessage.Name = "txtMessage";
        txtMessage.ReadOnly = true;
        txtMessage.ScrollBars = ScrollBars.Vertical;
        txtMessage.Size = new Size(750, 70);
        txtMessage.TabIndex = 4;
        //
        // lblReply
        //
        lblReply.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblReply.AutoSize = true;
        lblReply.Location = new Point(15, 390);
        lblReply.Name = "lblReply";
        lblReply.Size = new Size(300, 15);
        lblReply.TabIndex = 5;
        lblReply.Text = "Reply note (required to accept or deny a flag):";
        //
        // txtReply
        //
        txtReply.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        txtReply.Location = new Point(15, 410);
        txtReply.MaxLength = 1000;
        txtReply.Multiline = true;
        txtReply.Name = "txtReply";
        txtReply.ScrollBars = ScrollBars.Vertical;
        txtReply.Size = new Size(750, 50);
        txtReply.TabIndex = 6;
        //
        // btnMarkRead
        //
        btnMarkRead.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnMarkRead.Location = new Point(15, 472);
        btnMarkRead.Name = "btnMarkRead";
        btnMarkRead.Size = new Size(150, 32);
        btnMarkRead.TabIndex = 7;
        btnMarkRead.Text = "Mark as Read";
        btnMarkRead.UseVisualStyleBackColor = true;
        btnMarkRead.Click += btnMarkRead_Click;
        //
        // btnAcceptFlag
        //
        btnAcceptFlag.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnAcceptFlag.Enabled = false;
        btnAcceptFlag.Location = new Point(175, 472);
        btnAcceptFlag.Name = "btnAcceptFlag";
        btnAcceptFlag.Size = new Size(150, 32);
        btnAcceptFlag.TabIndex = 8;
        btnAcceptFlag.Text = "Accept Flag";
        btnAcceptFlag.UseVisualStyleBackColor = true;
        btnAcceptFlag.Click += btnAcceptFlag_Click;
        //
        // btnDenyFlag
        //
        btnDenyFlag.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnDenyFlag.Enabled = false;
        btnDenyFlag.Location = new Point(335, 472);
        btnDenyFlag.Name = "btnDenyFlag";
        btnDenyFlag.Size = new Size(150, 32);
        btnDenyFlag.TabIndex = 9;
        btnDenyFlag.Text = "Deny Flag";
        btnDenyFlag.UseVisualStyleBackColor = true;
        btnDenyFlag.Click += btnDenyFlag_Click;
        //
        // btnClose
        //
        btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnClose.Location = new Point(655, 472);
        btnClose.Name = "btnClose";
        btnClose.Size = new Size(110, 32);
        btnClose.TabIndex = 10;
        btnClose.Text = "Close";
        btnClose.UseVisualStyleBackColor = true;
        btnClose.Click += btnClose_Click;
        //
        // NotificationsForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnClose;
        ClientSize = new Size(780, 520);
        Controls.Add(lblTitle);
        Controls.Add(lblCount);
        Controls.Add(gridNotifications);
        Controls.Add(lblMessage);
        Controls.Add(txtMessage);
        Controls.Add(lblReply);
        Controls.Add(txtReply);
        Controls.Add(btnMarkRead);
        Controls.Add(btnAcceptFlag);
        Controls.Add(btnDenyFlag);
        Controls.Add(btnClose);
        MinimumSize = new Size(620, 480);
        Name = "NotificationsForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "GSTS – Notifications";
        Load += NotificationsForm_Load;
        Shown += NotificationsForm_Shown;
        ((System.ComponentModel.ISupportInitialize)gridNotifications).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblTitle;
    private Label lblCount;
    private DataGridView gridNotifications;
    private Label lblMessage;
    private TextBox txtMessage;
    private Label lblReply;
    private TextBox txtReply;
    private Button btnMarkRead;
    private Button btnAcceptFlag;
    private Button btnDenyFlag;
    private Button btnClose;
}
