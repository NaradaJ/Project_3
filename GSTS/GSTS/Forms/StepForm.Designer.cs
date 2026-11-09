namespace GSTS.Forms;

partial class StepForm
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
        lblDescription = new Label();
        txtDescription = new TextBox();
        lblStage = new Label();
        numStage = new NumericUpDown();
        chkCompulsory = new CheckBox();
        lblHint = new Label();
        btnOk = new Button();
        btnCancel = new Button();
        ((System.ComponentModel.ISupportInitialize)numStage).BeginInit();
        SuspendLayout();
        //
        // lblTitle
        //
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblTitle.Location = new Point(15, 12);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(90, 25);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Add Step";
        //
        // lblDescription
        //
        lblDescription.AutoSize = true;
        lblDescription.Location = new Point(18, 55);
        lblDescription.Name = "lblDescription";
        lblDescription.Size = new Size(140, 15);
        lblDescription.TabIndex = 1;
        lblDescription.Text = "Step or document:";
        //
        // txtDescription
        //
        txtDescription.Location = new Point(150, 52);
        txtDescription.MaxLength = 200;
        txtDescription.Name = "txtDescription";
        txtDescription.Size = new Size(290, 23);
        txtDescription.TabIndex = 2;
        //
        // lblStage
        //
        lblStage.AutoSize = true;
        lblStage.Location = new Point(18, 92);
        lblStage.Name = "lblStage";
        lblStage.Size = new Size(39, 15);
        lblStage.TabIndex = 3;
        lblStage.Text = "Stage:";
        //
        // numStage
        //
        numStage.Location = new Point(150, 89);
        numStage.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
        numStage.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        numStage.Name = "numStage";
        numStage.Size = new Size(70, 23);
        numStage.TabIndex = 4;
        numStage.Value = new decimal(new int[] { 1, 0, 0, 0 });
        //
        // chkCompulsory
        //
        chkCompulsory.AutoSize = true;
        chkCompulsory.Location = new Point(150, 125);
        chkCompulsory.Name = "chkCompulsory";
        chkCompulsory.Size = new Size(200, 19);
        chkCompulsory.TabIndex = 5;
        chkCompulsory.Text = "Compulsory (shown with *)";
        chkCompulsory.UseVisualStyleBackColor = true;
        //
        // lblHint
        //
        lblHint.ForeColor = Color.DimGray;
        lblHint.Location = new Point(18, 155);
        lblHint.Name = "lblHint";
        lblHint.Size = new Size(422, 34);
        lblHint.TabIndex = 6;
        lblHint.Text = "A ticket moves to the next stage only when every compulsory step of its current stage is complete. Optional steps can be skipped.";
        //
        // btnOk
        //
        btnOk.Location = new Point(210, 200);
        btnOk.Name = "btnOk";
        btnOk.Size = new Size(110, 32);
        btnOk.TabIndex = 7;
        btnOk.Text = "Save Step";
        btnOk.UseVisualStyleBackColor = true;
        btnOk.Click += btnOk_Click;
        //
        // btnCancel
        //
        btnCancel.Location = new Point(330, 200);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(110, 32);
        btnCancel.TabIndex = 8;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = true;
        btnCancel.Click += btnCancel_Click;
        //
        // StepForm
        //
        AcceptButton = btnOk;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancel;
        ClientSize = new Size(458, 250);
        Controls.Add(lblTitle);
        Controls.Add(lblDescription);
        Controls.Add(txtDescription);
        Controls.Add(lblStage);
        Controls.Add(numStage);
        Controls.Add(chkCompulsory);
        Controls.Add(lblHint);
        Controls.Add(btnOk);
        Controls.Add(btnCancel);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "StepForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "GSTS – Add Step";
        ((System.ComponentModel.ISupportInitialize)numStage).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblTitle;
    private Label lblDescription;
    private TextBox txtDescription;
    private Label lblStage;
    private NumericUpDown numStage;
    private CheckBox chkCompulsory;
    private Label lblHint;
    private Button btnOk;
    private Button btnCancel;
}
