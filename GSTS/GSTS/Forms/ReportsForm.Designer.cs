namespace GSTS.Forms;

partial class ReportsForm
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
        lblReport = new Label();
        cmbReport = new ComboBox();
        lblFrom = new Label();
        dtpFrom = new DateTimePicker();
        lblTo = new Label();
        dtpTo = new DateTimePicker();
        lblDays = new Label();
        numDays = new NumericUpDown();
        btnGenerate = new Button();
        lblMessage = new Label();
        gridReport = new DataGridView();
        btnClose = new Button();
        ((System.ComponentModel.ISupportInitialize)numDays).BeginInit();
        ((System.ComponentModel.ISupportInitialize)gridReport).BeginInit();
        SuspendLayout();
        //
        // lblReport
        //
        lblReport.AutoSize = true;
        lblReport.Location = new Point(15, 18);
        lblReport.Name = "lblReport";
        lblReport.Size = new Size(45, 15);
        lblReport.TabIndex = 0;
        lblReport.Text = "Report:";
        //
        // cmbReport
        //
        cmbReport.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbReport.Location = new Point(100, 15);
        cmbReport.Name = "cmbReport";
        cmbReport.Size = new Size(290, 23);
        cmbReport.TabIndex = 1;
        cmbReport.SelectedIndexChanged += cmbReport_SelectedIndexChanged;
        //
        // lblFrom
        //
        lblFrom.AutoSize = true;
        lblFrom.Location = new Point(15, 53);
        lblFrom.Name = "lblFrom";
        lblFrom.Size = new Size(62, 15);
        lblFrom.TabIndex = 2;
        lblFrom.Text = "From date:";
        //
        // dtpFrom
        //
        dtpFrom.Format = DateTimePickerFormat.Short;
        dtpFrom.Location = new Point(100, 50);
        dtpFrom.Name = "dtpFrom";
        dtpFrom.Size = new Size(120, 23);
        dtpFrom.TabIndex = 3;
        //
        // lblTo
        //
        lblTo.AutoSize = true;
        lblTo.Location = new Point(240, 53);
        lblTo.Name = "lblTo";
        lblTo.Size = new Size(48, 15);
        lblTo.TabIndex = 4;
        lblTo.Text = "To date:";
        //
        // dtpTo
        //
        dtpTo.Format = DateTimePickerFormat.Short;
        dtpTo.Location = new Point(300, 50);
        dtpTo.Name = "dtpTo";
        dtpTo.Size = new Size(120, 23);
        dtpTo.TabIndex = 5;
        //
        // lblDays
        //
        lblDays.AutoSize = true;
        lblDays.Location = new Point(440, 53);
        lblDays.Name = "lblDays";
        lblDays.Size = new Size(110, 15);
        lblDays.TabIndex = 6;
        lblDays.Text = "Pending more than:";
        //
        // numDays
        //
        numDays.Location = new Point(560, 50);
        numDays.Maximum = new decimal(new int[] { 3650, 0, 0, 0 });
        numDays.Name = "numDays";
        numDays.Size = new Size(70, 23);
        numDays.TabIndex = 7;
        numDays.Value = new decimal(new int[] { 7, 0, 0, 0 });
        //
        // btnGenerate
        //
        btnGenerate.Location = new Point(655, 46);
        btnGenerate.Name = "btnGenerate";
        btnGenerate.Size = new Size(130, 30);
        btnGenerate.TabIndex = 8;
        btnGenerate.Text = "Generate Report";
        btnGenerate.UseVisualStyleBackColor = true;
        btnGenerate.Click += btnGenerate_Click;
        //
        // lblMessage
        //
        lblMessage.AutoSize = true;
        lblMessage.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblMessage.Location = new Point(15, 88);
        lblMessage.Name = "lblMessage";
        lblMessage.Size = new Size(0, 15);
        lblMessage.TabIndex = 9;
        //
        // gridReport
        //
        gridReport.AllowUserToAddRows = false;
        gridReport.AllowUserToDeleteRows = false;
        gridReport.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        gridReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        gridReport.Location = new Point(15, 110);
        gridReport.MultiSelect = false;
        gridReport.Name = "gridReport";
        gridReport.ReadOnly = true;
        gridReport.RowHeadersVisible = false;
        gridReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridReport.Size = new Size(770, 300);
        gridReport.TabIndex = 10;
        //
        // btnClose
        //
        btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnClose.Location = new Point(675, 422);
        btnClose.Name = "btnClose";
        btnClose.Size = new Size(110, 32);
        btnClose.TabIndex = 11;
        btnClose.Text = "Close";
        btnClose.UseVisualStyleBackColor = true;
        btnClose.Click += btnClose_Click;
        //
        // ReportsForm
        //
        AcceptButton = btnGenerate;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnClose;
        ClientSize = new Size(800, 468);
        Controls.Add(lblReport);
        Controls.Add(cmbReport);
        Controls.Add(lblFrom);
        Controls.Add(dtpFrom);
        Controls.Add(lblTo);
        Controls.Add(dtpTo);
        Controls.Add(lblDays);
        Controls.Add(numDays);
        Controls.Add(btnGenerate);
        Controls.Add(lblMessage);
        Controls.Add(gridReport);
        Controls.Add(btnClose);
        MinimumSize = new Size(816, 400);
        Name = "ReportsForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "GSTS – Reports";
        Load += ReportsForm_Load;
        ((System.ComponentModel.ISupportInitialize)numDays).EndInit();
        ((System.ComponentModel.ISupportInitialize)gridReport).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblReport;
    private ComboBox cmbReport;
    private Label lblFrom;
    private DateTimePicker dtpFrom;
    private Label lblTo;
    private DateTimePicker dtpTo;
    private Label lblDays;
    private NumericUpDown numDays;
    private Button btnGenerate;
    private Label lblMessage;
    private DataGridView gridReport;
    private Button btnClose;
}
