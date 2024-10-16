namespace ProfessionalDocAnalyzer
{
    partial class ucExported
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
            this.panHeader = new System.Windows.Forms.Panel();
            this.lblHeaderCaption = new System.Windows.Forms.Label();
            this.panFooter = new System.Windows.Forms.Panel();
            this.butDelete = new MetroFramework.Controls.MetroButton();
            this.txtbMessage = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.chkbOpenWMSWord = new System.Windows.Forms.CheckBox();
            this.butEmail = new MetroFramework.Controls.MetroButton();
            this.butOpen = new MetroFramework.Controls.MetroButton();
            this.panLeftPadding = new System.Windows.Forms.Panel();
            this.lstbExportedFiles = new System.Windows.Forms.ListBox();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.panHeader.SuspendLayout();
            this.panFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panHeader
            // 
            this.panHeader.BackColor = System.Drawing.Color.White;
            this.panHeader.Controls.Add(this.lblHeaderCaption);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(601, 32);
            this.panHeader.TabIndex = 0;
            // 
            // lblHeaderCaption
            // 
            this.lblHeaderCaption.AutoSize = true;
            this.lblHeaderCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblHeaderCaption.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderCaption.ForeColor = System.Drawing.Color.Black;
            this.lblHeaderCaption.Location = new System.Drawing.Point(14, 4);
            this.lblHeaderCaption.Name = "lblHeaderCaption";
            this.lblHeaderCaption.Size = new System.Drawing.Size(129, 25);
            this.lblHeaderCaption.TabIndex = 2;
            this.lblHeaderCaption.Text = "Exported Files";
            this.lblHeaderCaption.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // panFooter
            // 
            this.panFooter.BackColor = System.Drawing.Color.White;
            this.panFooter.Controls.Add(this.butDelete);
            this.panFooter.Controls.Add(this.txtbMessage);
            this.panFooter.Controls.Add(this.lblMessage);
            this.panFooter.Controls.Add(this.chkbOpenWMSWord);
            this.panFooter.Controls.Add(this.butEmail);
            this.panFooter.Controls.Add(this.butOpen);
            this.panFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panFooter.Location = new System.Drawing.Point(0, 259);
            this.panFooter.Name = "panFooter";
            this.panFooter.Size = new System.Drawing.Size(601, 77);
            this.panFooter.TabIndex = 1;
            // 
            // butDelete
            // 
            this.butDelete.Location = new System.Drawing.Point(203, 8);
            this.butDelete.Name = "butDelete";
            this.butDelete.Size = new System.Drawing.Size(75, 23);
            this.butDelete.TabIndex = 96;
            this.butDelete.Text = "Delete";
            this.butDelete.UseSelectable = true;
            this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
            // 
            // txtbMessage
            // 
            this.txtbMessage.BackColor = System.Drawing.Color.White;
            this.txtbMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbMessage.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbMessage.ForeColor = System.Drawing.Color.Black;
            this.txtbMessage.Location = new System.Drawing.Point(19, 37);
            this.txtbMessage.Multiline = true;
            this.txtbMessage.Name = "txtbMessage";
            this.txtbMessage.Size = new System.Drawing.Size(340, 38);
            this.txtbMessage.TabIndex = 95;
            this.txtbMessage.TextChanged += new System.EventHandler(this.txtbMessage_TextChanged);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(339, 5);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 94;
            this.lblMessage.Visible = false;
            this.lblMessage.TextChanged += new System.EventHandler(this.lblMessage_TextChanged);
            // 
            // chkbOpenWMSWord
            // 
            this.chkbOpenWMSWord.AutoSize = true;
            this.chkbOpenWMSWord.BackColor = System.Drawing.Color.Transparent;
            this.chkbOpenWMSWord.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbOpenWMSWord.ForeColor = System.Drawing.Color.Black;
            this.chkbOpenWMSWord.Location = new System.Drawing.Point(434, -24);
            this.chkbOpenWMSWord.Name = "chkbOpenWMSWord";
            this.chkbOpenWMSWord.Size = new System.Drawing.Size(124, 19);
            this.chkbOpenWMSWord.TabIndex = 93;
            this.chkbOpenWMSWord.Text = "Open w/ MS Word";
            this.chkbOpenWMSWord.UseVisualStyleBackColor = false;
            this.chkbOpenWMSWord.Visible = false;
            // 
            // butEmail
            // 
            this.butEmail.Location = new System.Drawing.Point(111, 8);
            this.butEmail.Name = "butEmail";
            this.butEmail.Size = new System.Drawing.Size(75, 23);
            this.butEmail.TabIndex = 92;
            this.butEmail.Text = "Email";
            this.butEmail.UseSelectable = true;
            this.butEmail.Visible = false;
            this.butEmail.Click += new System.EventHandler(this.butEmail_Click);
            // 
            // butOpen
            // 
            this.butOpen.Location = new System.Drawing.Point(19, 8);
            this.butOpen.Name = "butOpen";
            this.butOpen.Size = new System.Drawing.Size(75, 23);
            this.butOpen.TabIndex = 91;
            this.butOpen.Text = "Open";
            this.butOpen.UseSelectable = true;
            this.butOpen.Visible = false;
            this.butOpen.Click += new System.EventHandler(this.butOpen_Click);
            // 
            // panLeftPadding
            // 
            this.panLeftPadding.BackColor = System.Drawing.Color.White;
            this.panLeftPadding.Dock = System.Windows.Forms.DockStyle.Left;
            this.panLeftPadding.Location = new System.Drawing.Point(0, 32);
            this.panLeftPadding.Name = "panLeftPadding";
            this.panLeftPadding.Size = new System.Drawing.Size(19, 227);
            this.panLeftPadding.TabIndex = 2;
            // 
            // lstbExportedFiles
            // 
            this.lstbExportedFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstbExportedFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstbExportedFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstbExportedFiles.FormattingEnabled = true;
            this.lstbExportedFiles.ItemHeight = 16;
            this.lstbExportedFiles.Location = new System.Drawing.Point(19, 32);
            this.lstbExportedFiles.Name = "lstbExportedFiles";
            this.lstbExportedFiles.Size = new System.Drawing.Size(582, 227);
            this.lstbExportedFiles.TabIndex = 3;
            this.lstbExportedFiles.SelectedIndexChanged += new System.EventHandler(this.lstbExportedFiles_SelectedIndexChanged);
            this.lstbExportedFiles.DoubleClick += new System.EventHandler(this.lstbExportedFiles_DoubleClick);
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // ucExported
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lstbExportedFiles);
            this.Controls.Add(this.panLeftPadding);
            this.Controls.Add(this.panFooter);
            this.Controls.Add(this.panHeader);
            this.Name = "ucExported";
            this.Size = new System.Drawing.Size(601, 336);
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            this.panFooter.ResumeLayout(false);
            this.panFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.Panel panFooter;
        private System.Windows.Forms.Panel panLeftPadding;
        private System.Windows.Forms.ListBox lstbExportedFiles;
        private System.Windows.Forms.Label lblHeaderCaption;
        private MetroFramework.Controls.MetroButton butEmail;
        private MetroFramework.Controls.MetroButton butOpen;
        private System.Windows.Forms.CheckBox chkbOpenWMSWord;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtbMessage;
        private MetroFramework.Controls.MetroButton butDelete;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
    }
}
