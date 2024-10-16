namespace ProfessionalDocAnalyzer
{
    partial class frmWGConnect
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
            this.butExport = new MetroFramework.Controls.MetroButton();
            this.butCancel = new MetroFramework.Controls.MetroButton();
            this.butBrowse = new MetroFramework.Controls.MetroButton();
            this.txtbWorkgroupPath = new System.Windows.Forms.TextBox();
            this.lblWorkgroupPath = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.lblConnectMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // butExport
            // 
            this.butExport.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.butExport.Location = new System.Drawing.Point(768, 278);
            this.butExport.Margin = new System.Windows.Forms.Padding(4);
            this.butExport.Name = "butExport";
            this.butExport.Size = new System.Drawing.Size(100, 28);
            this.butExport.TabIndex = 72;
            this.butExport.Text = "Connect";
            this.butExport.UseSelectable = true;
            this.butExport.Click += new System.EventHandler(this.butExport_Click);
            // 
            // butCancel
            // 
            this.butCancel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.butCancel.Location = new System.Drawing.Point(885, 278);
            this.butCancel.Margin = new System.Windows.Forms.Padding(4);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(100, 28);
            this.butCancel.TabIndex = 71;
            this.butCancel.Text = "Close";
            this.butCancel.UseSelectable = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butBrowse
            // 
            this.butBrowse.Location = new System.Drawing.Point(885, 123);
            this.butBrowse.Margin = new System.Windows.Forms.Padding(4);
            this.butBrowse.Name = "butBrowse";
            this.butBrowse.Size = new System.Drawing.Size(100, 28);
            this.butBrowse.TabIndex = 202;
            this.butBrowse.Text = "Browse";
            this.butBrowse.UseSelectable = true;
            this.butBrowse.Click += new System.EventHandler(this.butBrowse_Click);
            // 
            // txtbWorkgroupPath
            // 
            this.txtbWorkgroupPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbWorkgroupPath.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbWorkgroupPath.Location = new System.Drawing.Point(15, 121);
            this.txtbWorkgroupPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtbWorkgroupPath.MaxLength = 50;
            this.txtbWorkgroupPath.Name = "txtbWorkgroupPath";
            this.txtbWorkgroupPath.Size = new System.Drawing.Size(850, 29);
            this.txtbWorkgroupPath.TabIndex = 200;
            // 
            // lblWorkgroupPath
            // 
            this.lblWorkgroupPath.AutoSize = true;
            this.lblWorkgroupPath.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWorkgroupPath.ForeColor = System.Drawing.Color.Black;
            this.lblWorkgroupPath.Location = new System.Drawing.Point(11, 92);
            this.lblWorkgroupPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWorkgroupPath.Name = "lblWorkgroupPath";
            this.lblWorkgroupPath.Size = new System.Drawing.Size(165, 25);
            this.lblWorkgroupPath.TabIndex = 201;
            this.lblWorkgroupPath.Text = "Workgroup Folder";
            // 
            // lblConnectMessage
            // 
            this.lblConnectMessage.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnectMessage.ForeColor = System.Drawing.Color.Black;
            this.lblConnectMessage.Location = new System.Drawing.Point(11, 159);
            this.lblConnectMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConnectMessage.Name = "lblConnectMessage";
            this.lblConnectMessage.Size = new System.Drawing.Size(853, 110);
            this.lblConnectMessage.TabIndex = 203;
            this.lblConnectMessage.Text = "Workgroups (teams) - Team members can share Analysis Results, Deep Analysis, Matr" +
    "ices (Proposal Outline & Compliance Matrix).";
            // 
            // frmWGConnect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(1001, 315);
            this.ControlBox = false;
            this.Controls.Add(this.lblConnectMessage);
            this.Controls.Add(this.butBrowse);
            this.Controls.Add(this.txtbWorkgroupPath);
            this.Controls.Add(this.lblWorkgroupPath);
            this.Controls.Add(this.butExport);
            this.Controls.Add(this.butCancel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmWGConnect";
            this.Padding = new System.Windows.Forms.Padding(27, 74, 27, 25);
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Connect to a Workgroup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton butExport;
        private MetroFramework.Controls.MetroButton butCancel;
        private MetroFramework.Controls.MetroButton butBrowse;
        private System.Windows.Forms.TextBox txtbWorkgroupPath;
        private System.Windows.Forms.Label lblWorkgroupPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label lblConnectMessage;
    }
}