namespace GSTS.Forms;

partial class ManageOfficersForm
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
        lblSearch = new Label();
        txtSearch = new TextBox();
        btnSearch = new Button();
        btnNew = new Button();
        gridOfficers = new DataGridView();
        lblMode = new Label();
        lblName = new Label();
        txtName = new TextBox();
        lblNic = new Label();
        txtNic = new TextBox();
        lblContact = new Label();
        txtContact = new TextBox();
        lblPassword = new Label();
        txtPassword = new TextBox();
        lblPasswordHint = new Label();
        lblDesignation = new Label();
        txtDesignation = new TextBox();
        lblDepartment = new Label();
        txtDepartment = new TextBox();
        lblHandlesStep = new Label();
        cmbHandlesStep = new ComboBox();
        btnAdd = new Button();
        btnSave = new Button();
        btnDeactivate = new Button();
        btnClose = new Button();
        ((System.ComponentModel.ISupportInitialize)gridOfficers).BeginInit();
        SuspendLayout();
        //
        // lblSearch
        //
        lblSearch.AutoSize = true;
        lblSearch.Location = new Point(15, 16);
        lblSearch.Name = "lblSearch";
        lblSearch.Size = new Size(45, 15);
        lblSearch.TabIndex = 0;
        lblSearch.Text = "Search:";
        //
        // txtSearch
        //
        txtSearch.Location = new Point(75, 13);
        txtSearch.MaxLength = 100;
        txtSearch.Name = "txtSearch";
        txtSearch.PlaceholderText = "name, NIC, designation or department";
        txtSearch.Size = new Size(250, 23);
        txtSearch.TabIndex = 1;
        //
        // btnSearch
        //
        btnSearch.Location = new Point(335, 11);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(100, 27);
        btnSearch.TabIndex = 2;
        btnSearch.Text = "Search";
        btnSearch.UseVisualStyleBackColor = true;
        btnSearch.Click += btnSearch_Click;
        //
        // btnNew
        //
        btnNew.Location = new Point(445, 11);
        btnNew.Name = "btnNew";
        btnNew.Size = new Size(120, 27);
        btnNew.TabIndex = 3;
        btnNew.Text = "New Officer";
        btnNew.UseVisualStyleBackColor = true;
        btnNew.Click += btnNew_Click;
        //
        // gridOfficers
        //
        gridOfficers.AllowUserToAddRows = false;
        gridOfficers.AllowUserToDeleteRows = false;
        gridOfficers.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gridOfficers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridOfficers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        gridOfficers.Location = new Point(15, 48);
        gridOfficers.MultiSelect = false;
        gridOfficers.Name = "gridOfficers";
        gridOfficers.ReadOnly = true;
        gridOfficers.RowHeadersVisible = false;
        gridOfficers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridOfficers.Size = new Size(905, 250);
        gridOfficers.TabIndex = 4;
        gridOfficers.CellClick += gridOfficers_CellClick;
        //
        // lblMode
        //
        lblMode.AutoSize = true;
        lblMode.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblMode.Location = new Point(15, 310);
        lblMode.Name = "lblMode";
        lblMode.Size = new Size(130, 15);
        lblMode.TabIndex = 5;
        lblMode.Text = "Adding a new officer";
        //
        // lblName
        //
        lblName.AutoSize = true;
        lblName.Location = new Point(15, 343);
        lblName.Name = "lblName";
        lblName.Size = new Size(64, 15);
        lblName.TabIndex = 6;
        lblName.Text = "Full name:";
        //
        // txtName
        //
        txtName.Location = new Point(120, 340);
        txtName.MaxLength = 100;
        txtName.Name = "txtName";
        txtName.Size = new Size(280, 23);
        txtName.TabIndex = 7;
        //
        // lblNic
        //
        lblNic.AutoSize = true;
        lblNic.Location = new Point(15, 378);
        lblNic.Name = "lblNic";
        lblNic.Size = new Size(76, 15);
        lblNic.TabIndex = 8;
        lblNic.Text = "NIC number:";
        //
        // txtNic
        //
        txtNic.Location = new Point(120, 375);
        txtNic.MaxLength = 12;
        txtNic.Name = "txtNic";
        txtNic.Size = new Size(280, 23);
        txtNic.TabIndex = 8;
        //
        // lblContact
        //
        lblContact.AutoSize = true;
        lblContact.Location = new Point(15, 413);
        lblContact.Name = "lblContact";
        lblContact.Size = new Size(97, 15);
        lblContact.TabIndex = 9;
        lblContact.Text = "Contact number:";
        //
        // txtContact
        //
        txtContact.Location = new Point(120, 410);
        txtContact.MaxLength = 10;
        txtContact.Name = "txtContact";
        txtContact.Size = new Size(280, 23);
        txtContact.TabIndex = 9;
        //
        // lblPassword
        //
        lblPassword.AutoSize = true;
        lblPassword.Location = new Point(15, 448);
        lblPassword.Name = "lblPassword";
        lblPassword.Size = new Size(100, 15);
        lblPassword.TabIndex = 10;
        lblPassword.Text = "Initial password:";
        //
        // txtPassword
        //
        txtPassword.Location = new Point(120, 445);
        txtPassword.Name = "txtPassword";
        txtPassword.Size = new Size(280, 23);
        txtPassword.TabIndex = 10;
        txtPassword.UseSystemPasswordChar = true;
        //
        // lblPasswordHint
        //
        lblPasswordHint.AutoSize = true;
        lblPasswordHint.ForeColor = Color.DimGray;
        lblPasswordHint.Location = new Point(120, 470);
        lblPasswordHint.Name = "lblPasswordHint";
        lblPasswordHint.Size = new Size(330, 15);
        lblPasswordHint.TabIndex = 11;
        lblPasswordHint.Text = "The officer logs in with their NIC and this password (min. 8)";
        //
        // lblDesignation
        //
        lblDesignation.AutoSize = true;
        lblDesignation.Location = new Point(460, 343);
        lblDesignation.Name = "lblDesignation";
        lblDesignation.Size = new Size(70, 15);
        lblDesignation.TabIndex = 12;
        lblDesignation.Text = "Designation:";
        //
        // txtDesignation
        //
        txtDesignation.Location = new Point(570, 340);
        txtDesignation.MaxLength = 100;
        txtDesignation.Name = "txtDesignation";
        txtDesignation.Size = new Size(300, 23);
        txtDesignation.TabIndex = 12;
        //
        // lblDepartment
        //
        lblDepartment.AutoSize = true;
        lblDepartment.Location = new Point(460, 378);
        lblDepartment.Name = "lblDepartment";
        lblDepartment.Size = new Size(73, 15);
        lblDepartment.TabIndex = 13;
        lblDepartment.Text = "Department:";
        //
        // txtDepartment
        //
        txtDepartment.Location = new Point(570, 375);
        txtDepartment.MaxLength = 100;
        txtDepartment.Name = "txtDepartment";
        txtDepartment.Size = new Size(300, 23);
        txtDepartment.TabIndex = 13;
        //
        // lblHandlesStep
        //
        lblHandlesStep.AutoSize = true;
        lblHandlesStep.Location = new Point(460, 413);
        lblHandlesStep.Name = "lblHandlesStep";
        lblHandlesStep.Size = new Size(77, 15);
        lblHandlesStep.TabIndex = 14;
        lblHandlesStep.Text = "Handles step:";
        //
        // cmbHandlesStep
        //
        cmbHandlesStep.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbHandlesStep.Location = new Point(570, 410);
        cmbHandlesStep.Name = "cmbHandlesStep";
        cmbHandlesStep.Size = new Size(300, 23);
        cmbHandlesStep.TabIndex = 14;
        //
        // btnAdd
        //
        btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnAdd.Location = new Point(15, 500);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(120, 32);
        btnAdd.TabIndex = 15;
        btnAdd.Text = "Add Officer";
        btnAdd.UseVisualStyleBackColor = true;
        btnAdd.Click += btnAdd_Click;
        //
        // btnSave
        //
        btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnSave.Location = new Point(145, 500);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(120, 32);
        btnSave.TabIndex = 16;
        btnSave.Text = "Save Changes";
        btnSave.UseVisualStyleBackColor = true;
        btnSave.Click += btnSave_Click;
        //
        // btnDeactivate
        //
        btnDeactivate.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnDeactivate.Location = new Point(275, 500);
        btnDeactivate.Name = "btnDeactivate";
        btnDeactivate.Size = new Size(120, 32);
        btnDeactivate.TabIndex = 17;
        btnDeactivate.Text = "Deactivate";
        btnDeactivate.UseVisualStyleBackColor = true;
        btnDeactivate.Click += btnDeactivate_Click;
        //
        // btnClose
        //
        btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnClose.Location = new Point(810, 500);
        btnClose.Name = "btnClose";
        btnClose.Size = new Size(110, 32);
        btnClose.TabIndex = 18;
        btnClose.Text = "Close";
        btnClose.UseVisualStyleBackColor = true;
        btnClose.Click += btnClose_Click;
        //
        // ManageOfficersForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnClose;
        ClientSize = new Size(935, 545);
        Controls.Add(lblSearch);
        Controls.Add(txtSearch);
        Controls.Add(btnSearch);
        Controls.Add(btnNew);
        Controls.Add(gridOfficers);
        Controls.Add(lblMode);
        Controls.Add(lblName);
        Controls.Add(txtName);
        Controls.Add(lblNic);
        Controls.Add(txtNic);
        Controls.Add(lblContact);
        Controls.Add(txtContact);
        Controls.Add(lblPassword);
        Controls.Add(txtPassword);
        Controls.Add(lblPasswordHint);
        Controls.Add(lblDesignation);
        Controls.Add(txtDesignation);
        Controls.Add(lblDepartment);
        Controls.Add(txtDepartment);
        Controls.Add(lblHandlesStep);
        Controls.Add(cmbHandlesStep);
        Controls.Add(btnAdd);
        Controls.Add(btnSave);
        Controls.Add(btnDeactivate);
        Controls.Add(btnClose);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "ManageOfficersForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "GSTS – Manage Officers";
        Load += ManageOfficersForm_Load;
        Shown += ManageOfficersForm_Shown;
        ((System.ComponentModel.ISupportInitialize)gridOfficers).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblSearch;
    private TextBox txtSearch;
    private Button btnSearch;
    private Button btnNew;
    private DataGridView gridOfficers;
    private Label lblMode;
    private Label lblName;
    private TextBox txtName;
    private Label lblNic;
    private TextBox txtNic;
    private Label lblContact;
    private TextBox txtContact;
    private Label lblPassword;
    private TextBox txtPassword;
    private Label lblPasswordHint;
    private Label lblDesignation;
    private TextBox txtDesignation;
    private Label lblDepartment;
    private TextBox txtDepartment;
    private Label lblHandlesStep;
    private ComboBox cmbHandlesStep;
    private Button btnAdd;
    private Button btnSave;
    private Button btnDeactivate;
    private Button btnClose;
}
