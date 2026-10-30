namespace GSTS.Forms;

partial class ChangePasswordForm
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
        lblOld = new Label();
        txtOldPassword = new TextBox();
        lblNew = new Label();
        txtNewPassword = new TextBox();
        lblConfirm = new Label();
        txtConfirmPassword = new TextBox();
        lblHint = new Label();
        btnSave = new Button();
        btnCancel = new Button();
        SuspendLayout();
        //
        // lblOld
        //
        lblOld.AutoSize = true;
        lblOld.Location = new Point(25, 28);
        lblOld.Name = "lblOld";
        lblOld.Size = new Size(103, 15);
        lblOld.TabIndex = 0;
        lblOld.Text = "Current password:";
        //
        // txtOldPassword
        //
        txtOldPassword.Location = new Point(160, 25);
        txtOldPassword.Name = "txtOldPassword";
        txtOldPassword.Size = new Size(220, 23);
        txtOldPassword.TabIndex = 1;
        txtOldPassword.UseSystemPasswordChar = true;
        //
        // lblNew
        //
        lblNew.AutoSize = true;
        lblNew.Location = new Point(25, 68);
        lblNew.Name = "lblNew";
        lblNew.Size = new Size(87, 15);
        lblNew.TabIndex = 2;
        lblNew.Text = "New password:";
        //
        // txtNewPassword
        //
        txtNewPassword.Location = new Point(160, 65);
        txtNewPassword.Name = "txtNewPassword";
        txtNewPassword.Size = new Size(220, 23);
        txtNewPassword.TabIndex = 3;
        txtNewPassword.UseSystemPasswordChar = true;
        //
        // lblConfirm
        //
        lblConfirm.AutoSize = true;
        lblConfirm.Location = new Point(25, 108);
        lblConfirm.Name = "lblConfirm";
        lblConfirm.Size = new Size(129, 15);
        lblConfirm.TabIndex = 4;
        lblConfirm.Text = "Confirm new password:";
        //
        // txtConfirmPassword
        //
        txtConfirmPassword.Location = new Point(160, 105);
        txtConfirmPassword.Name = "txtConfirmPassword";
        txtConfirmPassword.Size = new Size(220, 23);
        txtConfirmPassword.TabIndex = 5;
        txtConfirmPassword.UseSystemPasswordChar = true;
        //
        // lblHint
        //
        lblHint.AutoSize = true;
        lblHint.ForeColor = Color.DimGray;
        lblHint.Location = new Point(160, 132);
        lblHint.Name = "lblHint";
        lblHint.Size = new Size(140, 15);
        lblHint.TabIndex = 6;
        lblHint.Text = "At least 8 characters";
        //
        // btnSave
        //
        btnSave.Location = new Point(160, 162);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(105, 32);
        btnSave.TabIndex = 7;
        btnSave.Text = "Save";
        btnSave.UseVisualStyleBackColor = true;
        btnSave.Click += btnSave_Click;
        //
        // btnCancel
        //
        btnCancel.Location = new Point(275, 162);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(105, 32);
        btnCancel.TabIndex = 8;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = true;
        btnCancel.Click += btnCancel_Click;
        //
        // ChangePasswordForm
        //
        AcceptButton = btnSave;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancel;
        ClientSize = new Size(410, 215);
        Controls.Add(lblOld);
        Controls.Add(txtOldPassword);
        Controls.Add(lblNew);
        Controls.Add(txtNewPassword);
        Controls.Add(lblConfirm);
        Controls.Add(txtConfirmPassword);
        Controls.Add(lblHint);
        Controls.Add(btnSave);
        Controls.Add(btnCancel);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "ChangePasswordForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "GSTS – Change Password";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblOld;
    private TextBox txtOldPassword;
    private Label lblNew;
    private TextBox txtNewPassword;
    private Label lblConfirm;
    private TextBox txtConfirmPassword;
    private Label lblHint;
    private Button btnSave;
    private Button btnCancel;
}
