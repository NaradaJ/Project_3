namespace GSTS.Forms;

partial class ManageServiceStepsForm
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
        lblService = new Label();
        cmbServiceType = new ComboBox();
        btnShow = new Button();
        lblServiceHint = new Label();
        gridSteps = new DataGridView();
        lblStepName = new Label();
        txtStepName = new TextBox();
        lblStepOrder = new Label();
        txtStepOrder = new TextBox();
        btnAdd = new Button();
        btnEdit = new Button();
        btnRemove = new Button();
        btnClose = new Button();
        lblInfo = new Label();
        ((System.ComponentModel.ISupportInitialize)gridSteps).BeginInit();
        SuspendLayout();
        //
        // lblService
        //
        lblService.AutoSize = true;
        lblService.Location = new Point(15, 18);
        lblService.Name = "lblService";
        lblService.Size = new Size(76, 15);
        lblService.TabIndex = 0;
        lblService.Text = "Service type:";
        //
        // cmbServiceType
        //
        cmbServiceType.Location = new Point(110, 15);
        cmbServiceType.MaxLength = 100;
        cmbServiceType.Name = "cmbServiceType";
        cmbServiceType.Size = new Size(300, 23);
        cmbServiceType.TabIndex = 1;
        cmbServiceType.SelectedIndexChanged += cmbServiceType_SelectedIndexChanged;
        //
        // btnShow
        //
        btnShow.Location = new Point(420, 13);
        btnShow.Name = "btnShow";
        btnShow.Size = new Size(110, 27);
        btnShow.TabIndex = 2;
        btnShow.Text = "Show Steps";
        btnShow.UseVisualStyleBackColor = true;
        btnShow.Click += btnShow_Click;
        //
        // lblServiceHint
        //
        lblServiceHint.AutoSize = true;
        lblServiceHint.ForeColor = Color.DimGray;
        lblServiceHint.Location = new Point(110, 42);
        lblServiceHint.Name = "lblServiceHint";
        lblServiceHint.Size = new Size(380, 15);
        lblServiceHint.TabIndex = 3;
        lblServiceHint.Text = "Select a service type, or type a new name and click \"Show Steps\".";
        //
        // gridSteps
        //
        gridSteps.AllowUserToAddRows = false;
        gridSteps.AllowUserToDeleteRows = false;
        gridSteps.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridSteps.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        gridSteps.Location = new Point(15, 68);
        gridSteps.MultiSelect = false;
        gridSteps.Name = "gridSteps";
        gridSteps.ReadOnly = true;
        gridSteps.RowHeadersVisible = false;
        gridSteps.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridSteps.Size = new Size(670, 220);
        gridSteps.TabIndex = 4;
        gridSteps.CellClick += gridSteps_CellClick;
        //
        // lblStepName
        //
        lblStepName.AutoSize = true;
        lblStepName.Location = new Point(15, 305);
        lblStepName.Name = "lblStepName";
        lblStepName.Size = new Size(66, 15);
        lblStepName.TabIndex = 5;
        lblStepName.Text = "Step name:";
        //
        // txtStepName
        //
        txtStepName.Location = new Point(110, 302);
        txtStepName.MaxLength = 100;
        txtStepName.Name = "txtStepName";
        txtStepName.Size = new Size(300, 23);
        txtStepName.TabIndex = 6;
        //
        // lblStepOrder
        //
        lblStepOrder.AutoSize = true;
        lblStepOrder.Location = new Point(430, 305);
        lblStepOrder.Name = "lblStepOrder";
        lblStepOrder.Size = new Size(40, 15);
        lblStepOrder.TabIndex = 7;
        lblStepOrder.Text = "Order:";
        //
        // txtStepOrder
        //
        txtStepOrder.Location = new Point(480, 302);
        txtStepOrder.MaxLength = 3;
        txtStepOrder.Name = "txtStepOrder";
        txtStepOrder.Size = new Size(60, 23);
        txtStepOrder.TabIndex = 8;
        //
        // btnAdd
        //
        btnAdd.Location = new Point(15, 342);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(120, 32);
        btnAdd.TabIndex = 9;
        btnAdd.Text = "Add Step";
        btnAdd.UseVisualStyleBackColor = true;
        btnAdd.Click += btnAdd_Click;
        //
        // btnEdit
        //
        btnEdit.Location = new Point(145, 342);
        btnEdit.Name = "btnEdit";
        btnEdit.Size = new Size(200, 32);
        btnEdit.TabIndex = 10;
        btnEdit.Text = "Save Changes (Edit / Reorder)";
        btnEdit.UseVisualStyleBackColor = true;
        btnEdit.Click += btnEdit_Click;
        //
        // btnRemove
        //
        btnRemove.Location = new Point(355, 342);
        btnRemove.Name = "btnRemove";
        btnRemove.Size = new Size(120, 32);
        btnRemove.TabIndex = 11;
        btnRemove.Text = "Remove Step";
        btnRemove.UseVisualStyleBackColor = true;
        btnRemove.Click += btnRemove_Click;
        //
        // btnClose
        //
        btnClose.Location = new Point(575, 342);
        btnClose.Name = "btnClose";
        btnClose.Size = new Size(110, 32);
        btnClose.TabIndex = 12;
        btnClose.Text = "Close";
        btnClose.UseVisualStyleBackColor = true;
        btnClose.Click += btnClose_Click;
        //
        // lblInfo
        //
        lblInfo.AutoSize = true;
        lblInfo.ForeColor = Color.DarkBlue;
        lblInfo.Location = new Point(15, 388);
        lblInfo.Name = "lblInfo";
        lblInfo.Size = new Size(380, 15);
        lblInfo.TabIndex = 13;
        lblInfo.Text = "Changes apply to new tickets only. Existing tickets keep their steps.";
        //
        // ManageServiceStepsForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnClose;
        ClientSize = new Size(700, 420);
        Controls.Add(lblService);
        Controls.Add(cmbServiceType);
        Controls.Add(btnShow);
        Controls.Add(lblServiceHint);
        Controls.Add(gridSteps);
        Controls.Add(lblStepName);
        Controls.Add(txtStepName);
        Controls.Add(lblStepOrder);
        Controls.Add(txtStepOrder);
        Controls.Add(btnAdd);
        Controls.Add(btnEdit);
        Controls.Add(btnRemove);
        Controls.Add(btnClose);
        Controls.Add(lblInfo);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "ManageServiceStepsForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "GSTS – Manage Service Types and Steps";
        Load += ManageServiceStepsForm_Load;
        ((System.ComponentModel.ISupportInitialize)gridSteps).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblService;
    private ComboBox cmbServiceType;
    private Button btnShow;
    private Label lblServiceHint;
    private DataGridView gridSteps;
    private Label lblStepName;
    private TextBox txtStepName;
    private Label lblStepOrder;
    private TextBox txtStepOrder;
    private Button btnAdd;
    private Button btnEdit;
    private Button btnRemove;
    private Button btnClose;
    private Label lblInfo;
}
