namespace GSTS.Forms;

partial class ReviewForm
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
        lblExplain = new Label();
        lblLastAction = new Label();
        lblNote = new Label();
        txtNote = new TextBox();
        btnApprove = new Button();
        btnFlag = new Button();
        btnCancel = new Button();
        SuspendLayout();
        //
        // lblTitle
        //
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblTitle.Location = new Point(15, 12);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(120, 25);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Review ticket";
        //
        // lblExplain
        //
        lblExplain.Location = new Point(18, 48);
        lblExplain.Name = "lblExplain";
        lblExplain.Size = new Size(440, 36);
        lblExplain.TabIndex = 1;
        lblExplain.Text = "Another officer acted last on this ticket. Before making further edits, approve their work or flag/change the workflow.";
        //
        // lblLastAction
        //
        lblLastAction.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblLastAction.Location = new Point(18, 90);
        lblLastAction.Name = "lblLastAction";
        lblLastAction.Size = new Size(440, 20);
        lblLastAction.TabIndex = 2;
        lblLastAction.Text = "Last action:";
        //
        // lblNote
        //
        lblNote.AutoSize = true;
        lblNote.Location = new Point(18, 122);
        lblNote.Name = "lblNote";
        lblNote.Size = new Size(95, 15);
        lblNote.TabIndex = 3;
        lblNote.Text = "Note (optional):";
        //
        // txtNote
        //
        txtNote.Location = new Point(18, 142);
        txtNote.MaxLength = 1000;
        txtNote.Multiline = true;
        txtNote.Name = "txtNote";
        txtNote.ScrollBars = ScrollBars.Vertical;
        txtNote.Size = new Size(440, 70);
        txtNote.TabIndex = 4;
        //
        // btnApprove
        //
        btnApprove.Location = new Point(18, 228);
        btnApprove.Name = "btnApprove";
        btnApprove.Size = new Size(120, 32);
        btnApprove.TabIndex = 5;
        btnApprove.Text = "Approve";
        btnApprove.UseVisualStyleBackColor = true;
        btnApprove.Click += btnApprove_Click;
        //
        // btnFlag
        //
        btnFlag.Location = new Point(148, 228);
        btnFlag.Name = "btnFlag";
        btnFlag.Size = new Size(120, 32);
        btnFlag.TabIndex = 6;
        btnFlag.Text = "Flag/Change";
        btnFlag.UseVisualStyleBackColor = true;
        btnFlag.Click += btnFlag_Click;
        //
        // btnCancel
        //
        btnCancel.Location = new Point(338, 228);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(120, 32);
        btnCancel.TabIndex = 7;
        btnCancel.Text = "View Only";
        btnCancel.UseVisualStyleBackColor = true;
        btnCancel.Click += btnCancel_Click;
        //
        // ReviewForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancel;
        ClientSize = new Size(476, 278);
        Controls.Add(lblTitle);
        Controls.Add(lblExplain);
        Controls.Add(lblLastAction);
        Controls.Add(lblNote);
        Controls.Add(txtNote);
        Controls.Add(btnApprove);
        Controls.Add(btnFlag);
        Controls.Add(btnCancel);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "ReviewForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "GSTS – Review";
        Load += ReviewForm_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblTitle;
    private Label lblExplain;
    private Label lblLastAction;
    private Label lblNote;
    private TextBox txtNote;
    private Button btnApprove;
    private Button btnFlag;
    private Button btnCancel;
}
