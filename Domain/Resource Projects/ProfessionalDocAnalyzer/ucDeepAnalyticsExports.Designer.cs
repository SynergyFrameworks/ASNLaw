namespace ProfessionalDocAnalyzer
{
    partial class ucDeepAnalyticsExports
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDeepAnalyticsExports));
            this.panHeader = new System.Windows.Forms.Panel();
            this.lblHeaderCaption = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panFooter = new System.Windows.Forms.Panel();
            this.lblEmailMsg = new System.Windows.Forms.Label();
            this.butEmail = new System.Windows.Forms.PictureBox();
            this.chkbOpenWMSWord = new System.Windows.Forms.CheckBox();
            this.butDelete = new System.Windows.Forms.PictureBox();
            this.txtbMessage = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.butOpen = new System.Windows.Forms.PictureBox();
            this.lstbExportedFiles = new System.Windows.Forms.ListBox();
            this.panHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.butEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.butDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.butOpen)).BeginInit();
            this.SuspendLayout();
            // 
            // panHeader
            // 
            this.panHeader.BackColor = System.Drawing.Color.Black;
            this.panHeader.Controls.Add(this.pictureBox1);
            this.panHeader.Controls.Add(this.lblHeaderCaption);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(737, 40);
            this.panHeader.TabIndex = 29;
            // 
            // lblHeaderCaption
            // 
            this.lblHeaderCaption.AutoSize = true;
            this.lblHeaderCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblHeaderCaption.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderCaption.ForeColor = System.Drawing.Color.White;
            this.lblHeaderCaption.Location = new System.Drawing.Point(43, 8);
            this.lblHeaderCaption.Name = "lblHeaderCaption";
            this.lblHeaderCaption.Size = new System.Drawing.Size(87, 25);
            this.lblHeaderCaption.TabIndex = 1;
            this.lblHeaderCaption.Text = "Exported";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(9, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(38, 38);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 38;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // panFooter
            // 
            this.panFooter.BackColor = System.Drawing.Color.Black;
            this.panFooter.Controls.Add(this.lblEmailMsg);
            this.panFooter.Controls.Add(this.butEmail);
            this.panFooter.Controls.Add(this.chkbOpenWMSWord);
            this.panFooter.Controls.Add(this.butDelete);
            this.panFooter.Controls.Add(this.txtbMessage);
            this.panFooter.Controls.Add(this.lblMessage);
            this.panFooter.Controls.Add(this.butOpen);
            this.panFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panFooter.Location = new System.Drawing.Point(0, 334);
            this.panFooter.Name = "panFooter";
            this.panFooter.Size = new System.Drawing.Size(737, 50);
            this.panFooter.TabIndex = 30;
            // 
            // lblEmailMsg
            // 
            this.lblEmailMsg.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmailMsg.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblEmailMsg.Location = new System.Drawing.Point(294, 7);
            this.lblEmailMsg.Name = "lblEmailMsg";
            this.lblEmailMsg.Size = new System.Drawing.Size(166, 43);
            this.lblEmailMsg.TabIndex = 85;
            this.lblEmailMsg.Text = "You must Send or Close email before resuming.";
            this.lblEmailMsg.Visible = false;
            // 
            // butEmail
            // 
            this.butEmail.Image = ((System.Drawing.Image)(resources.GetObject("butEmail.Image")));
            this.butEmail.Location = new System.Drawing.Point(105, 6);
            this.butEmail.Name = "butEmail";
            this.butEmail.Size = new System.Drawing.Size(38, 38);
            this.butEmail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.butEmail.TabIndex = 84;
            this.butEmail.TabStop = false;
            this.butEmail.Visible = false;
            this.butEmail.Click += new System.EventHandler(this.butEmail_Click);
            // 
            // chkbOpenWMSWord
            // 
            this.chkbOpenWMSWord.AutoSize = true;
            this.chkbOpenWMSWord.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbOpenWMSWord.ForeColor = System.Drawing.Color.White;
            this.chkbOpenWMSWord.Location = new System.Drawing.Point(164, 19);
            this.chkbOpenWMSWord.Name = "chkbOpenWMSWord";
            this.chkbOpenWMSWord.Size = new System.Drawing.Size(124, 19);
            this.chkbOpenWMSWord.TabIndex = 44;
            this.chkbOpenWMSWord.Text = "Open w/ MS Word";
            this.chkbOpenWMSWord.UseVisualStyleBackColor = true;
            this.chkbOpenWMSWord.Visible = false;
            // 
            // butDelete
            // 
            this.butDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
            this.butDelete.Location = new System.Drawing.Point(56, 7);
            this.butDelete.Name = "butDelete";
            this.butDelete.Size = new System.Drawing.Size(38, 38);
            this.butDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.butDelete.TabIndex = 43;
            this.butDelete.TabStop = false;
            this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
            // 
            // txtbMessage
            // 
            this.txtbMessage.BackColor = System.Drawing.Color.Black;
            this.txtbMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbMessage.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtbMessage.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbMessage.ForeColor = System.Drawing.Color.White;
            this.txtbMessage.Location = new System.Drawing.Point(485, 0);
            this.txtbMessage.Multiline = true;
            this.txtbMessage.Name = "txtbMessage";
            this.txtbMessage.Size = new System.Drawing.Size(252, 50);
            this.txtbMessage.TabIndex = 42;
            this.txtbMessage.TextChanged += new System.EventHandler(this.txtbMessage_TextChanged);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(153, 19);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 41;
            this.lblMessage.Visible = false;
            this.lblMessage.TextChanged += new System.EventHandler(this.lblMessage_TextChanged);
            // 
            // butOpen
            // 
            this.butOpen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butOpen.Image = ((System.Drawing.Image)(resources.GetObject("butOpen.Image")));
            this.butOpen.Location = new System.Drawing.Point(9, 7);
            this.butOpen.Name = "butOpen";
            this.butOpen.Size = new System.Drawing.Size(38, 38);
            this.butOpen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.butOpen.TabIndex = 40;
            this.butOpen.TabStop = false;
            this.butOpen.Click += new System.EventHandler(this.butEmail_Click);
            // 
            // lstbExportedFiles
            // 
            this.lstbExportedFiles.BackColor = System.Drawing.Color.Black;
            this.lstbExportedFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstbExportedFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstbExportedFiles.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstbExportedFiles.ForeColor = System.Drawing.Color.White;
            this.lstbExportedFiles.FormattingEnabled = true;
            this.lstbExportedFiles.ItemHeight = 20;
            this.lstbExportedFiles.Location = new System.Drawing.Point(0, 40);
            this.lstbExportedFiles.Name = "lstbExportedFiles";
            this.lstbExportedFiles.Size = new System.Drawing.Size(737, 294);
            this.lstbExportedFiles.TabIndex = 31;
            this.lstbExportedFiles.SelectedIndexChanged += new System.EventHandler(this.lstbExportedFiles_SelectedIndexChanged);
            // 
            // ucDeepAnalyticsExports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lstbExportedFiles);
            this.Controls.Add(this.panFooter);
            this.Controls.Add(this.panHeader);
            this.Name = "ucDeepAnalyticsExports";
            this.Size = new System.Drawing.Size(737, 384);
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panFooter.ResumeLayout(false);
            this.panFooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.butEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.butDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.butOpen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblHeaderCaption;
        private System.Windows.Forms.Panel panFooter;
        private System.Windows.Forms.Label lblEmailMsg;
        private System.Windows.Forms.PictureBox butEmail;
        private System.Windows.Forms.CheckBox chkbOpenWMSWord;
        private System.Windows.Forms.PictureBox butDelete;
        private System.Windows.Forms.TextBox txtbMessage;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.PictureBox butOpen;
        private System.Windows.Forms.ListBox lstbExportedFiles;
    }
}
