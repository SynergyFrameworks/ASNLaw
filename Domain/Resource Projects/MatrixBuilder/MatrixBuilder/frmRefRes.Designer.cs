namespace MatrixBuilder
{
    partial class frmRefRes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRefRes));
            this.lblDefinition = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtbRefResName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtbDescription = new System.Windows.Forms.TextBox();
            this.panMain = new System.Windows.Forms.Panel();
            this.butBrowse = new MetroFramework.Controls.MetroButton();
            this.txtbRefResPath = new System.Windows.Forms.TextBox();
            this.lblRefResPath = new System.Windows.Forms.Label();
            this.lblRefResTypeNotice = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.butInternal = new MetroFramework.Controls.MetroButton();
            this.butShared = new MetroFramework.Controls.MetroButton();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.butCancel = new MetroFramework.Controls.MetroButton();
            this.butOk = new MetroFramework.Controls.MetroButton();
            this.panBottom = new System.Windows.Forms.Panel();
            this.panHeader = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDefinition
            // 
            this.lblDefinition.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDefinition.ForeColor = System.Drawing.Color.White;
            this.lblDefinition.Location = new System.Drawing.Point(284, 12);
            this.lblDefinition.Name = "lblDefinition";
            this.lblDefinition.Size = new System.Drawing.Size(514, 45);
            this.lblDefinition.TabIndex = 182;
            this.lblDefinition.Text = "Reference Resources - A collection of reusable excerpts content that can be alloc" +
    "ated into a Matrix, such as Win Themes && Discriminators.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(376, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 20);
            this.label2.TabIndex = 185;
            this.label2.Text = "Description";
            // 
            // txtbRefResName
            // 
            this.txtbRefResName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbRefResName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbRefResName.Location = new System.Drawing.Point(379, 40);
            this.txtbRefResName.MaxLength = 50;
            this.txtbRefResName.Name = "txtbRefResName";
            this.txtbRefResName.Size = new System.Drawing.Size(292, 25);
            this.txtbRefResName.TabIndex = 86;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(376, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(183, 20);
            this.label4.TabIndex = 87;
            this.label4.Text = "Reference Resource Name";
            // 
            // txtbDescription
            // 
            this.txtbDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbDescription.Location = new System.Drawing.Point(379, 117);
            this.txtbDescription.MaxLength = 100;
            this.txtbDescription.Multiline = true;
            this.txtbDescription.Name = "txtbDescription";
            this.txtbDescription.Size = new System.Drawing.Size(292, 112);
            this.txtbDescription.TabIndex = 184;
            // 
            // panMain
            // 
            this.panMain.Controls.Add(this.butBrowse);
            this.panMain.Controls.Add(this.txtbRefResPath);
            this.panMain.Controls.Add(this.lblRefResPath);
            this.panMain.Controls.Add(this.lblRefResTypeNotice);
            this.panMain.Controls.Add(this.label3);
            this.panMain.Controls.Add(this.label1);
            this.panMain.Controls.Add(this.butInternal);
            this.panMain.Controls.Add(this.butShared);
            this.panMain.Controls.Add(this.pictureBox3);
            this.panMain.Controls.Add(this.pictureBox1);
            this.panMain.Controls.Add(this.txtbDescription);
            this.panMain.Controls.Add(this.label2);
            this.panMain.Controls.Add(this.txtbRefResName);
            this.panMain.Controls.Add(this.label4);
            this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMain.Location = new System.Drawing.Point(20, 72);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(776, 334);
            this.panMain.TabIndex = 181;
            // 
            // butBrowse
            // 
            this.butBrowse.Location = new System.Drawing.Point(677, 282);
            this.butBrowse.Name = "butBrowse";
            this.butBrowse.Size = new System.Drawing.Size(75, 23);
            this.butBrowse.TabIndex = 196;
            this.butBrowse.Text = "Browse";
            this.butBrowse.UseSelectable = true;
            this.butBrowse.Visible = false;
            this.butBrowse.Click += new System.EventHandler(this.butBrowse_Click);
            // 
            // txtbRefResPath
            // 
            this.txtbRefResPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbRefResPath.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbRefResPath.Location = new System.Drawing.Point(135, 282);
            this.txtbRefResPath.MaxLength = 50;
            this.txtbRefResPath.Name = "txtbRefResPath";
            this.txtbRefResPath.Size = new System.Drawing.Size(536, 25);
            this.txtbRefResPath.TabIndex = 194;
            this.txtbRefResPath.Visible = false;
            // 
            // lblRefResPath
            // 
            this.lblRefResPath.AutoSize = true;
            this.lblRefResPath.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRefResPath.ForeColor = System.Drawing.Color.White;
            this.lblRefResPath.Location = new System.Drawing.Point(132, 259);
            this.lblRefResPath.Name = "lblRefResPath";
            this.lblRefResPath.Size = new System.Drawing.Size(243, 20);
            this.lblRefResPath.TabIndex = 195;
            this.lblRefResPath.Text = "Reference Resource Folder (shared)";
            this.lblRefResPath.Visible = false;
            // 
            // lblRefResTypeNotice
            // 
            this.lblRefResTypeNotice.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRefResTypeNotice.ForeColor = System.Drawing.Color.White;
            this.lblRefResTypeNotice.Location = new System.Drawing.Point(132, 63);
            this.lblRefResTypeNotice.Name = "lblRefResTypeNotice";
            this.lblRefResTypeNotice.Size = new System.Drawing.Size(221, 183);
            this.lblRefResTypeNotice.TabIndex = 193;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.LightGreen;
            this.label3.Location = new System.Drawing.Point(16, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 20);
            this.label3.TabIndex = 192;
            this.label3.Text = "Ref. Resource";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.label1.Location = new System.Drawing.Point(16, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.TabIndex = 191;
            this.label1.Text = "Workgroup";
            // 
            // butInternal
            // 
            this.butInternal.Highlight = true;
            this.butInternal.Location = new System.Drawing.Point(19, 149);
            this.butInternal.Name = "butInternal";
            this.butInternal.Size = new System.Drawing.Size(80, 23);
            this.butInternal.TabIndex = 190;
            this.butInternal.Text = "Internal";
            this.butInternal.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.butInternal.UseSelectable = true;
            this.butInternal.Click += new System.EventHandler(this.butInternal_Click);
            // 
            // butShared
            // 
            this.butShared.Location = new System.Drawing.Point(19, 285);
            this.butShared.Name = "butShared";
            this.butShared.Size = new System.Drawing.Size(80, 23);
            this.butShared.TabIndex = 189;
            this.butShared.Text = "Shared";
            this.butShared.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.butShared.UseSelectable = true;
            this.butShared.Click += new System.EventHandler(this.butShared_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(19, 195);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(80, 84);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 187;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(19, 63);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(80, 80);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 186;
            this.pictureBox1.TabStop = false;
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(677, 10);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 1;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseSelectable = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butOk
            // 
            this.butOk.Location = new System.Drawing.Point(585, 10);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(75, 23);
            this.butOk.TabIndex = 0;
            this.butOk.Text = "Save";
            this.butOk.UseSelectable = true;
            this.butOk.Click += new System.EventHandler(this.butOk_Click);
            // 
            // panBottom
            // 
            this.panBottom.Controls.Add(this.butCancel);
            this.panBottom.Controls.Add(this.butOk);
            this.panBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panBottom.Location = new System.Drawing.Point(20, 406);
            this.panBottom.Name = "panBottom";
            this.panBottom.Size = new System.Drawing.Size(776, 47);
            this.panBottom.TabIndex = 180;
            // 
            // panHeader
            // 
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.Location = new System.Drawing.Point(20, 60);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(776, 12);
            this.panHeader.TabIndex = 179;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(7, 10);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(48, 48);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 178;
            this.pictureBox2.TabStop = false;
            // 
            // frmRefRes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(816, 473);
            this.ControlBox = false;
            this.Controls.Add(this.lblDefinition);
            this.Controls.Add(this.panMain);
            this.Controls.Add(this.panBottom);
            this.Controls.Add(this.panHeader);
            this.Controls.Add(this.pictureBox2);
            this.Name = "frmRefRes";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "     Reference Resource";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.TopMost = true;
            this.panMain.ResumeLayout(false);
            this.panMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblDefinition;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtbRefResName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtbDescription;
        private System.Windows.Forms.Panel panMain;
        private MetroFramework.Controls.MetroButton butCancel;
        private MetroFramework.Controls.MetroButton butOk;
        private System.Windows.Forms.Panel panBottom;
        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.PictureBox pictureBox2;
        private MetroFramework.Controls.MetroButton butBrowse;
        private System.Windows.Forms.TextBox txtbRefResPath;
        private System.Windows.Forms.Label lblRefResPath;
        private System.Windows.Forms.Label lblRefResTypeNotice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroButton butInternal;
        private MetroFramework.Controls.MetroButton butShared;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}