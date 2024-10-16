namespace MatrixBuilder
{
    partial class frmWorkGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWorkGroup));
            this.lblDefinition = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.butConnect = new MetroFramework.Controls.MetroButton();
            this.butCreate = new MetroFramework.Controls.MetroButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panBottom = new System.Windows.Forms.Panel();
            this.butCancel = new MetroFramework.Controls.MetroButton();
            this.butOk = new MetroFramework.Controls.MetroButton();
            this.panDir = new System.Windows.Forms.Panel();
            this.butBrowse = new MetroFramework.Controls.MetroButton();
            this.txtbWorkgroupPath = new System.Windows.Forms.TextBox();
            this.lblWorkgroupPath = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panConnect = new System.Windows.Forms.Panel();
            this.lblConnectMessage = new System.Windows.Forms.Label();
            this.panCreate = new System.Windows.Forms.Panel();
            this.txtbWorkgroupDescription = new System.Windows.Forms.TextBox();
            this.lblWorkgroupDescription = new System.Windows.Forms.Label();
            this.txtbWorkgroupName = new System.Windows.Forms.TextBox();
            this.lblWorkGroupName = new System.Windows.Forms.Label();
            this.lblCreate = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panBottom.SuspendLayout();
            this.panDir.SuspendLayout();
            this.panConnect.SuspendLayout();
            this.panCreate.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDefinition
            // 
            this.lblDefinition.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDefinition.ForeColor = System.Drawing.Color.White;
            this.lblDefinition.Location = new System.Drawing.Point(219, 15);
            this.lblDefinition.Name = "lblDefinition";
            this.lblDefinition.Size = new System.Drawing.Size(514, 45);
            this.lblDefinition.TabIndex = 184;
            this.lblDefinition.Text = "Workgroups (teams) - Team members can share Analysis Results, Deep Analysis, Matr" +
    "ices (Proposal Outline && Compliance Matrix).";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(9, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(48, 48);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 183;
            this.pictureBox2.TabStop = false;
            // 
            // butConnect
            // 
            this.butConnect.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.butConnect.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.butConnect.Location = new System.Drawing.Point(78, 127);
            this.butConnect.Name = "butConnect";
            this.butConnect.Size = new System.Drawing.Size(255, 23);
            this.butConnect.TabIndex = 192;
            this.butConnect.Text = "Connect to an existing Workgroup";
            this.butConnect.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.butConnect.UseSelectable = true;
            this.butConnect.Click += new System.EventHandler(this.butConnect_Click);
            // 
            // butCreate
            // 
            this.butCreate.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.butCreate.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.butCreate.Location = new System.Drawing.Point(382, 127);
            this.butCreate.Name = "butCreate";
            this.butCreate.Size = new System.Drawing.Size(207, 23);
            this.butCreate.TabIndex = 191;
            this.butCreate.Text = "Create a new Workgroup";
            this.butCreate.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.butCreate.UseSelectable = true;
            this.butCreate.Click += new System.EventHandler(this.butCreate_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(23, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 25);
            this.label4.TabIndex = 193;
            this.label4.Text = "I want to ...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(344, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 25);
            this.label1.TabIndex = 194;
            this.label1.Text = "or";
            // 
            // panBottom
            // 
            this.panBottom.Controls.Add(this.butCancel);
            this.panBottom.Controls.Add(this.butOk);
            this.panBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panBottom.Location = new System.Drawing.Point(20, 423);
            this.panBottom.Name = "panBottom";
            this.panBottom.Size = new System.Drawing.Size(742, 31);
            this.panBottom.TabIndex = 195;
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(664, 3);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 1;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseSelectable = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butOk
            // 
            this.butOk.Location = new System.Drawing.Point(571, 3);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(75, 23);
            this.butOk.TabIndex = 0;
            this.butOk.Text = "Save";
            this.butOk.UseSelectable = true;
            this.butOk.Click += new System.EventHandler(this.butOk_Click);
            // 
            // panDir
            // 
            this.panDir.Controls.Add(this.butBrowse);
            this.panDir.Controls.Add(this.txtbWorkgroupPath);
            this.panDir.Controls.Add(this.lblWorkgroupPath);
            this.panDir.Location = new System.Drawing.Point(17, 159);
            this.panDir.Name = "panDir";
            this.panDir.Size = new System.Drawing.Size(745, 66);
            this.panDir.TabIndex = 196;
            this.panDir.Visible = false;
            // 
            // butBrowse
            // 
            this.butBrowse.Location = new System.Drawing.Point(661, 34);
            this.butBrowse.Name = "butBrowse";
            this.butBrowse.Size = new System.Drawing.Size(75, 23);
            this.butBrowse.TabIndex = 199;
            this.butBrowse.Text = "Browse";
            this.butBrowse.UseSelectable = true;
            this.butBrowse.Click += new System.EventHandler(this.butBrowse_Click);
            // 
            // txtbWorkgroupPath
            // 
            this.txtbWorkgroupPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbWorkgroupPath.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbWorkgroupPath.Location = new System.Drawing.Point(8, 32);
            this.txtbWorkgroupPath.MaxLength = 50;
            this.txtbWorkgroupPath.Name = "txtbWorkgroupPath";
            this.txtbWorkgroupPath.Size = new System.Drawing.Size(638, 25);
            this.txtbWorkgroupPath.TabIndex = 197;
            this.txtbWorkgroupPath.TextChanged += new System.EventHandler(this.txtbWorkgroupPath_TextChanged);
            // 
            // lblWorkgroupPath
            // 
            this.lblWorkgroupPath.AutoSize = true;
            this.lblWorkgroupPath.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWorkgroupPath.ForeColor = System.Drawing.Color.White;
            this.lblWorkgroupPath.Location = new System.Drawing.Point(5, 9);
            this.lblWorkgroupPath.Name = "lblWorkgroupPath";
            this.lblWorkgroupPath.Size = new System.Drawing.Size(129, 20);
            this.lblWorkgroupPath.TabIndex = 198;
            this.lblWorkgroupPath.Text = "Workgroup Folder";
            // 
            // panConnect
            // 
            this.panConnect.Controls.Add(this.lblConnectMessage);
            this.panConnect.Location = new System.Drawing.Point(20, 231);
            this.panConnect.Name = "panConnect";
            this.panConnect.Size = new System.Drawing.Size(742, 186);
            this.panConnect.TabIndex = 197;
            this.panConnect.Visible = false;
            // 
            // lblConnectMessage
            // 
            this.lblConnectMessage.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnectMessage.ForeColor = System.Drawing.Color.White;
            this.lblConnectMessage.Location = new System.Drawing.Point(6, 14);
            this.lblConnectMessage.Name = "lblConnectMessage";
            this.lblConnectMessage.Size = new System.Drawing.Size(730, 159);
            this.lblConnectMessage.TabIndex = 185;
            this.lblConnectMessage.Text = "Workgroups (teams) - Team members can share Analysis Results, Deep Analysis, Matr" +
    "ices (Proposal Outline & Compliance Matrix).";
            // 
            // panCreate
            // 
            this.panCreate.Controls.Add(this.txtbWorkgroupDescription);
            this.panCreate.Controls.Add(this.lblWorkgroupDescription);
            this.panCreate.Controls.Add(this.txtbWorkgroupName);
            this.panCreate.Controls.Add(this.lblWorkGroupName);
            this.panCreate.Controls.Add(this.lblCreate);
            this.panCreate.Location = new System.Drawing.Point(17, 227);
            this.panCreate.Name = "panCreate";
            this.panCreate.Size = new System.Drawing.Size(742, 186);
            this.panCreate.TabIndex = 198;
            this.panCreate.Visible = false;
            // 
            // txtbWorkgroupDescription
            // 
            this.txtbWorkgroupDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbWorkgroupDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbWorkgroupDescription.Location = new System.Drawing.Point(13, 93);
            this.txtbWorkgroupDescription.MaxLength = 100;
            this.txtbWorkgroupDescription.Multiline = true;
            this.txtbWorkgroupDescription.Name = "txtbWorkgroupDescription";
            this.txtbWorkgroupDescription.Size = new System.Drawing.Size(554, 78);
            this.txtbWorkgroupDescription.TabIndex = 188;
            this.txtbWorkgroupDescription.Visible = false;
            // 
            // lblWorkgroupDescription
            // 
            this.lblWorkgroupDescription.AutoSize = true;
            this.lblWorkgroupDescription.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWorkgroupDescription.ForeColor = System.Drawing.Color.White;
            this.lblWorkgroupDescription.Location = new System.Drawing.Point(9, 70);
            this.lblWorkgroupDescription.Name = "lblWorkgroupDescription";
            this.lblWorkgroupDescription.Size = new System.Drawing.Size(85, 20);
            this.lblWorkgroupDescription.TabIndex = 189;
            this.lblWorkgroupDescription.Text = "Description";
            this.lblWorkgroupDescription.Visible = false;
            // 
            // txtbWorkgroupName
            // 
            this.txtbWorkgroupName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbWorkgroupName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbWorkgroupName.Location = new System.Drawing.Point(13, 36);
            this.txtbWorkgroupName.MaxLength = 50;
            this.txtbWorkgroupName.Name = "txtbWorkgroupName";
            this.txtbWorkgroupName.Size = new System.Drawing.Size(292, 25);
            this.txtbWorkgroupName.TabIndex = 186;
            this.txtbWorkgroupName.Visible = false;
            // 
            // lblWorkGroupName
            // 
            this.lblWorkGroupName.AutoSize = true;
            this.lblWorkGroupName.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWorkGroupName.ForeColor = System.Drawing.Color.White;
            this.lblWorkGroupName.Location = new System.Drawing.Point(9, 13);
            this.lblWorkGroupName.Name = "lblWorkGroupName";
            this.lblWorkGroupName.Size = new System.Drawing.Size(127, 20);
            this.lblWorkGroupName.TabIndex = 187;
            this.lblWorkGroupName.Text = "Workgroup Name";
            this.lblWorkGroupName.Visible = false;
            // 
            // lblCreate
            // 
            this.lblCreate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreate.ForeColor = System.Drawing.Color.White;
            this.lblCreate.Location = new System.Drawing.Point(12, 11);
            this.lblCreate.Name = "lblCreate";
            this.lblCreate.Size = new System.Drawing.Size(730, 166);
            this.lblCreate.TabIndex = 185;
            this.lblCreate.Text = "Enter or Select a folder to create a new Workgroup.";
            // 
            // frmWorkGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(782, 474);
            this.ControlBox = false;
            this.Controls.Add(this.panDir);
            this.Controls.Add(this.panCreate);
            this.Controls.Add(this.panConnect);
            this.Controls.Add(this.panBottom);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.butConnect);
            this.Controls.Add(this.butCreate);
            this.Controls.Add(this.lblDefinition);
            this.Controls.Add(this.pictureBox2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmWorkGroup";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "      Workgroup";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmWorkGroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panBottom.ResumeLayout(false);
            this.panDir.ResumeLayout(false);
            this.panDir.PerformLayout();
            this.panConnect.ResumeLayout(false);
            this.panCreate.ResumeLayout(false);
            this.panCreate.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDefinition;
        private System.Windows.Forms.PictureBox pictureBox2;
        private MetroFramework.Controls.MetroButton butConnect;
        private MetroFramework.Controls.MetroButton butCreate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panBottom;
        private MetroFramework.Controls.MetroButton butCancel;
        private MetroFramework.Controls.MetroButton butOk;
        private System.Windows.Forms.Panel panDir;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private MetroFramework.Controls.MetroButton butBrowse;
        private System.Windows.Forms.TextBox txtbWorkgroupPath;
        private System.Windows.Forms.Label lblWorkgroupPath;
        private System.Windows.Forms.Panel panConnect;
        private System.Windows.Forms.Label lblConnectMessage;
        private System.Windows.Forms.Panel panCreate;
        private System.Windows.Forms.Label lblCreate;
        private System.Windows.Forms.TextBox txtbWorkgroupDescription;
        private System.Windows.Forms.Label lblWorkgroupDescription;
        private System.Windows.Forms.TextBox txtbWorkgroupName;
        private System.Windows.Forms.Label lblWorkGroupName;
    }
}