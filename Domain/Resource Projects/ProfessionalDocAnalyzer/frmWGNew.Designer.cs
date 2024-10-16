namespace ProfessionalDocAnalyzer
{
    partial class frmWGNew
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtbWorkgroupDescription = new System.Windows.Forms.TextBox();
            this.lblWorkgroupDescription = new System.Windows.Forms.Label();
            this.txtbWorkgroupName = new System.Windows.Forms.TextBox();
            this.lblWorkGroupName = new System.Windows.Forms.Label();
            this.butBrowse = new MetroFramework.Controls.MetroButton();
            this.txtbWorkgroupPath = new System.Windows.Forms.TextBox();
            this.lblWorkgroupPath = new System.Windows.Forms.Label();
            this.butCancel = new MetroFramework.Controls.MetroButton();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.lblConnectMessage = new System.Windows.Forms.Label();
            this.butSave = new MetroFramework.Controls.MetroButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.cboWorkgroups = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtbWorkgroupDescription
            // 
            this.txtbWorkgroupDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbWorkgroupDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbWorkgroupDescription.Location = new System.Drawing.Point(13, 227);
            this.txtbWorkgroupDescription.MaxLength = 100;
            this.txtbWorkgroupDescription.Multiline = true;
            this.txtbWorkgroupDescription.Name = "txtbWorkgroupDescription";
            this.txtbWorkgroupDescription.Size = new System.Drawing.Size(638, 61);
            this.txtbWorkgroupDescription.TabIndex = 3;
            // 
            // lblWorkgroupDescription
            // 
            this.lblWorkgroupDescription.AutoSize = true;
            this.lblWorkgroupDescription.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWorkgroupDescription.ForeColor = System.Drawing.Color.Black;
            this.lblWorkgroupDescription.Location = new System.Drawing.Point(9, 204);
            this.lblWorkgroupDescription.Name = "lblWorkgroupDescription";
            this.lblWorkgroupDescription.Size = new System.Drawing.Size(85, 20);
            this.lblWorkgroupDescription.TabIndex = 193;
            this.lblWorkgroupDescription.Text = "Description";
            // 
            // txtbWorkgroupName
            // 
            this.txtbWorkgroupName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbWorkgroupName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbWorkgroupName.Location = new System.Drawing.Point(13, 170);
            this.txtbWorkgroupName.MaxLength = 50;
            this.txtbWorkgroupName.Name = "txtbWorkgroupName";
            this.txtbWorkgroupName.Size = new System.Drawing.Size(292, 25);
            this.txtbWorkgroupName.TabIndex = 2;
            // 
            // lblWorkGroupName
            // 
            this.lblWorkGroupName.AutoSize = true;
            this.lblWorkGroupName.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWorkGroupName.ForeColor = System.Drawing.Color.Black;
            this.lblWorkGroupName.Location = new System.Drawing.Point(9, 147);
            this.lblWorkGroupName.Name = "lblWorkGroupName";
            this.lblWorkGroupName.Size = new System.Drawing.Size(127, 20);
            this.lblWorkGroupName.TabIndex = 191;
            this.lblWorkGroupName.Text = "Workgroup Name";
            // 
            // butBrowse
            // 
            this.butBrowse.Location = new System.Drawing.Point(666, 102);
            this.butBrowse.Name = "butBrowse";
            this.butBrowse.Size = new System.Drawing.Size(75, 23);
            this.butBrowse.TabIndex = 1;
            this.butBrowse.Text = "Browse";
            this.butBrowse.UseSelectable = true;
            this.butBrowse.Click += new System.EventHandler(this.butBrowse_Click);
            // 
            // txtbWorkgroupPath
            // 
            this.txtbWorkgroupPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbWorkgroupPath.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbWorkgroupPath.Location = new System.Drawing.Point(13, 100);
            this.txtbWorkgroupPath.MaxLength = 2700;
            this.txtbWorkgroupPath.Name = "txtbWorkgroupPath";
            this.txtbWorkgroupPath.Size = new System.Drawing.Size(638, 25);
            this.txtbWorkgroupPath.TabIndex = 0;
            // 
            // lblWorkgroupPath
            // 
            this.lblWorkgroupPath.AutoSize = true;
            this.lblWorkgroupPath.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWorkgroupPath.ForeColor = System.Drawing.Color.Black;
            this.lblWorkgroupPath.Location = new System.Drawing.Point(10, 77);
            this.lblWorkgroupPath.Name = "lblWorkgroupPath";
            this.lblWorkgroupPath.Size = new System.Drawing.Size(129, 20);
            this.lblWorkgroupPath.TabIndex = 204;
            this.lblWorkgroupPath.Text = "Workgroup Folder";
            // 
            // butCancel
            // 
            this.butCancel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.butCancel.Location = new System.Drawing.Point(666, 422);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 5;
            this.butCancel.Text = "Close";
            this.butCancel.UseSelectable = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // lblConnectMessage
            // 
            this.lblConnectMessage.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnectMessage.ForeColor = System.Drawing.Color.Black;
            this.lblConnectMessage.Location = new System.Drawing.Point(308, 20);
            this.lblConnectMessage.Name = "lblConnectMessage";
            this.lblConnectMessage.Size = new System.Drawing.Size(433, 66);
            this.lblConnectMessage.TabIndex = 208;
            this.lblConnectMessage.Text = "Workgroups (teams) - Team members can share/collaborate with Tasks, Templates, Li" +
    "braries, Analysis Results, Matrices/Reports and other data.";
            // 
            // butSave
            // 
            this.butSave.Location = new System.Drawing.Point(576, 422);
            this.butSave.Name = "butSave";
            this.butSave.Size = new System.Drawing.Size(75, 23);
            this.butSave.TabIndex = 4;
            this.butSave.Text = "Save";
            this.butSave.UseSelectable = true;
            this.butSave.Click += new System.EventHandler(this.butSave_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(10, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(255, 20);
            this.label1.TabIndex = 209;
            this.label1.Text = "Import Tasks, Templates and Libraries";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.PowderBlue;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cboWorkgroups);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(14, 311);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(637, 95);
            this.panel1.TabIndex = 210;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(10, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(612, 24);
            this.label2.TabIndex = 211;
            this.label2.Text = "Import Tasks, Templates, and Libraries from a selected Workgroup or Use the Defau" +
    "lt settings.";
            // 
            // cboWorkgroups
            // 
            this.cboWorkgroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWorkgroups.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboWorkgroups.FormattingEnabled = true;
            this.cboWorkgroups.Location = new System.Drawing.Point(13, 32);
            this.cboWorkgroups.Name = "cboWorkgroups";
            this.cboWorkgroups.Size = new System.Drawing.Size(292, 25);
            this.cboWorkgroups.TabIndex = 210;
            // 
            // frmWGNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(752, 456);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.butSave);
            this.Controls.Add(this.lblConnectMessage);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butBrowse);
            this.Controls.Add(this.txtbWorkgroupPath);
            this.Controls.Add(this.lblWorkgroupPath);
            this.Controls.Add(this.txtbWorkgroupDescription);
            this.Controls.Add(this.lblWorkgroupDescription);
            this.Controls.Add(this.txtbWorkgroupName);
            this.Controls.Add(this.lblWorkGroupName);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmWGNew";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Create a new Workgroup";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtbWorkgroupDescription;
        private System.Windows.Forms.Label lblWorkgroupDescription;
        private System.Windows.Forms.TextBox txtbWorkgroupName;
        private System.Windows.Forms.Label lblWorkGroupName;
        private MetroFramework.Controls.MetroButton butBrowse;
        private System.Windows.Forms.TextBox txtbWorkgroupPath;
        private System.Windows.Forms.Label lblWorkgroupPath;
        private MetroFramework.Controls.MetroButton butCancel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label lblConnectMessage;
        private MetroFramework.Controls.MetroButton butSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cboWorkgroups;
        private System.Windows.Forms.Label label2;
    }
}