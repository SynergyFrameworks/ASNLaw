namespace MatrixBuilder
{
    partial class frmDownloadMatrix
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDownloadMatrix));
            this.lblNotice = new System.Windows.Forms.Label();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.butCancel = new MetroFramework.Controls.MetroButton();
            this.butOK = new MetroFramework.Controls.MetroButton();
            this.lblSaved1 = new System.Windows.Forms.Label();
            this.chklstDownload = new System.Windows.Forms.CheckedListBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblHeader = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNotice
            // 
            this.lblNotice.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotice.ForeColor = System.Drawing.Color.LightCyan;
            this.lblNotice.Location = new System.Drawing.Point(331, 19);
            this.lblNotice.Name = "lblNotice";
            this.lblNotice.Size = new System.Drawing.Size(445, 41);
            this.lblNotice.TabIndex = 124;
            this.lblNotice.Text = "Check Templates to Download";
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // butCancel
            // 
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.Location = new System.Drawing.Point(701, 385);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 121;
            this.butCancel.Text = "Cancel";
            this.metroToolTip1.SetToolTip(this.butCancel, "Cancel Changes");
            this.butCancel.UseSelectable = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butOK
            // 
            this.butOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.butOK.Location = new System.Drawing.Point(611, 385);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(75, 23);
            this.butOK.TabIndex = 122;
            this.butOK.Text = "Save";
            this.metroToolTip1.SetToolTip(this.butOK, "Download checked Template ");
            this.butOK.UseSelectable = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // lblSaved1
            // 
            this.lblSaved1.AutoSize = true;
            this.lblSaved1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaved1.ForeColor = System.Drawing.Color.Lime;
            this.lblSaved1.Location = new System.Drawing.Point(36, 385);
            this.lblSaved1.Name = "lblSaved1";
            this.lblSaved1.Size = new System.Drawing.Size(43, 17);
            this.lblSaved1.TabIndex = 123;
            this.lblSaved1.Text = "Saved";
            this.lblSaved1.Visible = false;
            // 
            // chklstDownload
            // 
            this.chklstDownload.BackColor = System.Drawing.Color.Black;
            this.chklstDownload.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chklstDownload.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chklstDownload.ForeColor = System.Drawing.Color.White;
            this.chklstDownload.FormattingEnabled = true;
            this.chklstDownload.Location = new System.Drawing.Point(33, 63);
            this.chklstDownload.Name = "chklstDownload";
            this.chklstDownload.Size = new System.Drawing.Size(603, 288);
            this.chklstDownload.TabIndex = 120;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(13, 8);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(38, 38);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 119;
            this.pictureBox2.TabStop = false;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(55, 9);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(271, 30);
            this.lblHeader.TabIndex = 118;
            this.lblHeader.Text = "Matrix Templates Download";
            // 
            // frmDownloadMatrix
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(791, 417);
            this.ControlBox = false;
            this.Controls.Add(this.lblNotice);
            this.Controls.Add(this.lblSaved1);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.chklstDownload);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.lblHeader);
            this.Name = "frmDownloadMatrix";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNotice;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
        private MetroFramework.Controls.MetroButton butCancel;
        private MetroFramework.Controls.MetroButton butOK;
        private System.Windows.Forms.Label lblSaved1;
        private System.Windows.Forms.CheckedListBox chklstDownload;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblHeader;
    }
}