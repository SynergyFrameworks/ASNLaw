namespace ProfessionalDocAnalyzer
{
    partial class frmExported
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExported));
            this.picExcelTemplate = new System.Windows.Forms.PictureBox();
            this.lstbExportedFiles = new System.Windows.Forms.ListBox();
            this.butRemoveFile = new MetroFramework.Controls.MetroButton();
            this.butEditFile = new MetroFramework.Controls.MetroButton();
            this.butCancel = new MetroFramework.Controls.MetroButton();
            this.butEmailFile = new MetroFramework.Controls.MetroButton();
            this.lblMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picExcelTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // picExcelTemplate
            // 
            this.picExcelTemplate.Cursor = System.Windows.Forms.Cursors.Default;
            this.picExcelTemplate.Image = ((System.Drawing.Image)(resources.GetObject("picExcelTemplate.Image")));
            this.picExcelTemplate.Location = new System.Drawing.Point(12, 19);
            this.picExcelTemplate.Name = "picExcelTemplate";
            this.picExcelTemplate.Size = new System.Drawing.Size(38, 38);
            this.picExcelTemplate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picExcelTemplate.TabIndex = 73;
            this.picExcelTemplate.TabStop = false;
            // 
            // lstbExportedFiles
            // 
            this.lstbExportedFiles.BackColor = System.Drawing.Color.Black;
            this.lstbExportedFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstbExportedFiles.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstbExportedFiles.ForeColor = System.Drawing.Color.White;
            this.lstbExportedFiles.FormattingEnabled = true;
            this.lstbExportedFiles.ItemHeight = 21;
            this.lstbExportedFiles.Location = new System.Drawing.Point(23, 79);
            this.lstbExportedFiles.Name = "lstbExportedFiles";
            this.lstbExportedFiles.Size = new System.Drawing.Size(476, 231);
            this.lstbExportedFiles.TabIndex = 74;
            this.lstbExportedFiles.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // butRemoveFile
            // 
            this.butRemoveFile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.butRemoveFile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butRemoveFile.BackgroundImage")));
            this.butRemoveFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butRemoveFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butRemoveFile.Location = new System.Drawing.Point(68, 357);
            this.butRemoveFile.Name = "butRemoveFile";
            this.butRemoveFile.Size = new System.Drawing.Size(28, 28);
            this.butRemoveFile.TabIndex = 166;
            this.butRemoveFile.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.butRemoveFile.UseSelectable = true;
            this.butRemoveFile.Click += new System.EventHandler(this.butRemoveFile_Click);
            // 
            // butEditFile
            // 
            this.butEditFile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butEditFile.BackgroundImage")));
            this.butEditFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butEditFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butEditFile.ForeColor = System.Drawing.Color.White;
            this.butEditFile.Location = new System.Drawing.Point(25, 358);
            this.butEditFile.Name = "butEditFile";
            this.butEditFile.Size = new System.Drawing.Size(28, 28);
            this.butEditFile.TabIndex = 165;
            this.butEditFile.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butEditFile.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.butEditFile.UseSelectable = true;
            this.butEditFile.Click += new System.EventHandler(this.butEditFile_Click);
            // 
            // butCancel
            // 
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.Location = new System.Drawing.Point(424, 357);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 167;
            this.butCancel.Text = "Close";
            this.butCancel.UseSelectable = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butEmailFile
            // 
            this.butEmailFile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.butEmailFile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butEmailFile.BackgroundImage")));
            this.butEmailFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butEmailFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butEmailFile.Location = new System.Drawing.Point(111, 357);
            this.butEmailFile.Name = "butEmailFile";
            this.butEmailFile.Size = new System.Drawing.Size(28, 28);
            this.butEmailFile.TabIndex = 168;
            this.butEmailFile.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.butEmailFile.UseSelectable = true;
            this.butEmailFile.Click += new System.EventHandler(this.butEmailFile_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.White;
            this.lblMessage.Location = new System.Drawing.Point(158, 343);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(250, 42);
            this.lblMessage.TabIndex = 169;
            // 
            // frmExported
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(522, 397);
            this.ControlBox = false;
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.butEmailFile);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butRemoveFile);
            this.Controls.Add(this.butEditFile);
            this.Controls.Add(this.lstbExportedFiles);
            this.Controls.Add(this.picExcelTemplate);
            this.Name = "frmExported";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "    Excel Exports";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            ((System.ComponentModel.ISupportInitialize)(this.picExcelTemplate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox picExcelTemplate;
        private System.Windows.Forms.ListBox lstbExportedFiles;
        private MetroFramework.Controls.MetroButton butRemoveFile;
        private MetroFramework.Controls.MetroButton butEditFile;
        private MetroFramework.Controls.MetroButton butCancel;
        private MetroFramework.Controls.MetroButton butEmailFile;
        private System.Windows.Forms.Label lblMessage;
    }
}