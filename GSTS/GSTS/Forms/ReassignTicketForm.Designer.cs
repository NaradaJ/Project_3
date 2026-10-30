namespace GSTS.Forms;

partial class ReassignTicketForm
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
        lblTickets = new Label();
        gridTickets = new DataGridView();
        lblOfficer = new Label();
        cmbOfficer = new ComboBox();
        btnReassign = new Button();
        lblHint = new Label();
        btnClose = new Button();
        ((System.ComponentModel.ISupportInitialize)gridTickets).BeginInit();
        SuspendLayout();
        //
        // lblTickets
        //
        lblTickets.AutoSize = true;
        lblTickets.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblTickets.Location = new Point(15, 15);
        lblTickets.Name = "lblTickets";
        lblTickets.Size = new Size(200, 15);
        lblTickets.TabIndex = 0;
        lblTickets.Text = "Open tickets (select one to reassign)";
        //
        // gridTickets
        //
        gridTickets.AllowUserToAddRows = false;
        gridTickets.AllowUserToDeleteRows = false;
        gridTickets.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridTickets.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        gridTickets.Location = new Point(15, 35);
        gridTickets.MultiSelect = false;
        gridTickets.Name = "gridTickets";
        gridTickets.ReadOnly = true;
        gridTickets.RowHeadersVisible = false;
        gridTickets.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridTickets.Size = new Size(770, 280);
        gridTickets.TabIndex = 1;
        //
        // lblOfficer
        //
        lblOfficer.AutoSize = true;
        lblOfficer.Location = new Point(15, 333);
        lblOfficer.Name = "lblOfficer";
        lblOfficer.Size = new Size(73, 15);
        lblOfficer.TabIndex = 2;
        lblOfficer.Text = "New officer:";
        //
        // cmbOfficer
        //
        cmbOfficer.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbOfficer.Location = new Point(110, 330);
        cmbOfficer.Name = "cmbOfficer";
        cmbOfficer.Size = new Size(480, 23);
        cmbOfficer.TabIndex = 3;
        //
        // btnReassign
        //
        btnReassign.Location = new Point(605, 326);
        btnReassign.Name = "btnReassign";
        btnReassign.Size = new Size(180, 32);
        btnReassign.TabIndex = 4;
        btnReassign.Text = "Reassign Ticket";
        btnReassign.UseVisualStyleBackColor = true;
        btnReassign.Click += btnReassign_Click;
        //
        // lblHint
        //
        lblHint.AutoSize = true;
        lblHint.ForeColor = Color.DimGray;
        lblHint.Location = new Point(110, 358);
        lblHint.Name = "lblHint";
        lblHint.Size = new Size(400, 15);
        lblHint.TabIndex = 5;
        lblHint.Text = "The new officer must be active and handle the ticket's active step (shown in brackets).";
        //
        // btnClose
        //
        btnClose.Location = new Point(675, 390);
        btnClose.Name = "btnClose";
        btnClose.Size = new Size(110, 32);
        btnClose.TabIndex = 6;
        btnClose.Text = "Close";
        btnClose.UseVisualStyleBackColor = true;
        btnClose.Click += btnClose_Click;
        //
        // ReassignTicketForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnClose;
        ClientSize = new Size(800, 435);
        Controls.Add(lblTickets);
        Controls.Add(gridTickets);
        Controls.Add(lblOfficer);
        Controls.Add(cmbOfficer);
        Controls.Add(btnReassign);
        Controls.Add(lblHint);
        Controls.Add(btnClose);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "ReassignTicketForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "GSTS – Reassign Ticket";
        Load += ReassignTicketForm_Load;
        ((System.ComponentModel.ISupportInitialize)gridTickets).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblTickets;
    private DataGridView gridTickets;
    private Label lblOfficer;
    private ComboBox cmbOfficer;
    private Button btnReassign;
    private Label lblHint;
    private Button btnClose;
}
