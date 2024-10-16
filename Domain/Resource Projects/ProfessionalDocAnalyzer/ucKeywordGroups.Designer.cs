namespace ProfessionalDocAnalyzer
{
    partial class ucKeywordGroups
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucKeywordGroups));
            this.panHeader = new System.Windows.Forms.Panel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.picHeader = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lstbKeywordGroups = new System.Windows.Forms.ListBox();
            this.lstbKeywords = new System.Windows.Forms.ListBox();
            this.txtbMessage = new System.Windows.Forms.TextBox();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.panHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panHeader
            // 
            this.panHeader.BackColor = System.Drawing.Color.White;
            this.panHeader.Controls.Add(this.lblMessage);
            this.panHeader.Controls.Add(this.lblHeader);
            this.panHeader.Controls.Add(this.picHeader);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.ForeColor = System.Drawing.Color.White;
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(737, 50);
            this.panHeader.TabIndex = 5;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(320, 11);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 16;
            this.lblMessage.Visible = false;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.Black;
            this.lblHeader.Location = new System.Drawing.Point(53, 11);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(164, 30);
            this.lblHeader.TabIndex = 15;
            this.lblHeader.Text = "Keyword Groups";
            // 
            // picHeader
            // 
            this.picHeader.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picHeader.Image = ((System.Drawing.Image)(resources.GetObject("picHeader.Image")));
            this.picHeader.Location = new System.Drawing.Point(9, 6);
            this.picHeader.Name = "picHeader";
            this.picHeader.Size = new System.Drawing.Size(41, 38);
            this.picHeader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHeader.TabIndex = 14;
            this.picHeader.TabStop = false;
            this.picHeader.Click += new System.EventHandler(this.picHeader_Click_1);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 50);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lstbKeywordGroups);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lstbKeywords);
            this.splitContainer1.Panel2.Controls.Add(this.txtbMessage);
            this.splitContainer1.Size = new System.Drawing.Size(737, 452);
            this.splitContainer1.SplitterDistance = 243;
            this.splitContainer1.TabIndex = 7;
            // 
            // lstbKeywordGroups
            // 
            this.lstbKeywordGroups.BackColor = System.Drawing.Color.White;
            this.lstbKeywordGroups.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstbKeywordGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstbKeywordGroups.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstbKeywordGroups.ForeColor = System.Drawing.Color.Black;
            this.lstbKeywordGroups.FormattingEnabled = true;
            this.lstbKeywordGroups.ItemHeight = 21;
            this.lstbKeywordGroups.Location = new System.Drawing.Point(0, 0);
            this.lstbKeywordGroups.Name = "lstbKeywordGroups";
            this.lstbKeywordGroups.Size = new System.Drawing.Size(243, 452);
            this.lstbKeywordGroups.TabIndex = 1;
            this.lstbKeywordGroups.SelectedIndexChanged += new System.EventHandler(this.lstbKeywordGroups_SelectedIndexChanged);
            // 
            // lstbKeywords
            // 
            this.lstbKeywords.BackColor = System.Drawing.Color.White;
            this.lstbKeywords.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstbKeywords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstbKeywords.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstbKeywords.ForeColor = System.Drawing.Color.Black;
            this.lstbKeywords.FormattingEnabled = true;
            this.lstbKeywords.ItemHeight = 21;
            this.lstbKeywords.Location = new System.Drawing.Point(0, 0);
            this.lstbKeywords.Name = "lstbKeywords";
            this.lstbKeywords.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lstbKeywords.Size = new System.Drawing.Size(490, 391);
            this.lstbKeywords.Sorted = true;
            this.lstbKeywords.TabIndex = 45;
            this.lstbKeywords.SelectedIndexChanged += new System.EventHandler(this.lstbKeywords_SelectedIndexChanged);
            // 
            // txtbMessage
            // 
            this.txtbMessage.BackColor = System.Drawing.Color.White;
            this.txtbMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtbMessage.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbMessage.ForeColor = System.Drawing.Color.Black;
            this.txtbMessage.Location = new System.Drawing.Point(0, 391);
            this.txtbMessage.Multiline = true;
            this.txtbMessage.Name = "txtbMessage";
            this.txtbMessage.Size = new System.Drawing.Size(490, 61);
            this.txtbMessage.TabIndex = 43;
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // ucKeywordGroups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panHeader);
            this.Name = "ucKeywordGroups";
            this.Size = new System.Drawing.Size(737, 502);
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.PictureBox picHeader;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lstbKeywordGroups;
        private System.Windows.Forms.ListBox lstbKeywords;
        private System.Windows.Forms.TextBox txtbMessage;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
    }
}
