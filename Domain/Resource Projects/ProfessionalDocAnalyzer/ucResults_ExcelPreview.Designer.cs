namespace ProfessionalDocAnalyzer
{
    partial class ucResults_ExcelPreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucResults_ExcelPreview));
            this.panHeader = new System.Windows.Forms.Panel();
            this.panHeaderRight = new System.Windows.Forms.Panel();
            this.butPrint = new MetroFramework.Controls.MetroButton();
            this.butDelete = new MetroFramework.Controls.MetroButton();
            this.butEmail = new MetroFramework.Controls.MetroButton();
            this.butOpen = new MetroFramework.Controls.MetroButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.panTemplateTypesRtPadding = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.reoGridControl1 = new unvell.ReoGrid.ReoGridControl();
            this.panHeader.SuspendLayout();
            this.panHeaderRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panHeader
            // 
            this.panHeader.BackColor = System.Drawing.Color.Black;
            this.panHeader.Controls.Add(this.panHeaderRight);
            this.panHeader.Controls.Add(this.pictureBox2);
            this.panHeader.Controls.Add(this.lblHeader);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(725, 41);
            this.panHeader.TabIndex = 0;
            // 
            // panHeaderRight
            // 
            this.panHeaderRight.Controls.Add(this.butPrint);
            this.panHeaderRight.Controls.Add(this.butDelete);
            this.panHeaderRight.Controls.Add(this.butEmail);
            this.panHeaderRight.Controls.Add(this.butOpen);
            this.panHeaderRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panHeaderRight.Location = new System.Drawing.Point(362, 0);
            this.panHeaderRight.Name = "panHeaderRight";
            this.panHeaderRight.Size = new System.Drawing.Size(363, 41);
            this.panHeaderRight.TabIndex = 135;
            // 
            // butPrint
            // 
            this.butPrint.Location = new System.Drawing.Point(185, 9);
            this.butPrint.Name = "butPrint";
            this.butPrint.Size = new System.Drawing.Size(75, 23);
            this.butPrint.TabIndex = 138;
            this.butPrint.Text = "Print";
            this.butPrint.UseSelectable = true;
            this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
            // 
            // butDelete
            // 
            this.butDelete.Location = new System.Drawing.Point(278, 9);
            this.butDelete.Name = "butDelete";
            this.butDelete.Size = new System.Drawing.Size(75, 23);
            this.butDelete.TabIndex = 137;
            this.butDelete.Text = "Delete";
            this.butDelete.UseSelectable = true;
            this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
            // 
            // butEmail
            // 
            this.butEmail.Location = new System.Drawing.Point(102, 9);
            this.butEmail.Name = "butEmail";
            this.butEmail.Size = new System.Drawing.Size(75, 23);
            this.butEmail.TabIndex = 136;
            this.butEmail.Text = "Email";
            this.butEmail.UseSelectable = true;
            this.butEmail.Click += new System.EventHandler(this.butEmail_Click);
            // 
            // butOpen
            // 
            this.butOpen.Location = new System.Drawing.Point(18, 9);
            this.butOpen.Name = "butOpen";
            this.butOpen.Size = new System.Drawing.Size(75, 23);
            this.butOpen.TabIndex = 135;
            this.butOpen.Text = "Open";
            this.butOpen.UseSelectable = true;
            this.butOpen.Click += new System.EventHandler(this.butOpen_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(9, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(38, 38);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 133;
            this.pictureBox2.TabStop = false;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(50, 8);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(78, 25);
            this.lblHeader.TabIndex = 132;
            this.lblHeader.Text = "Preview";
            // 
            // panTemplateTypesRtPadding
            // 
            this.panTemplateTypesRtPadding.Dock = System.Windows.Forms.DockStyle.Left;
            this.panTemplateTypesRtPadding.Location = new System.Drawing.Point(0, 41);
            this.panTemplateTypesRtPadding.Name = "panTemplateTypesRtPadding";
            this.panTemplateTypesRtPadding.Size = new System.Drawing.Size(10, 473);
            this.panTemplateTypesRtPadding.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(715, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(10, 473);
            this.panel1.TabIndex = 3;
            // 
            // reoGridControl1
            // 
            this.reoGridControl1.BackColor = System.Drawing.Color.White;
            this.reoGridControl1.ColumnHeaderContextMenuStrip = null;
            this.reoGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reoGridControl1.LeadHeaderContextMenuStrip = null;
            this.reoGridControl1.Location = new System.Drawing.Point(10, 41);
            this.reoGridControl1.Name = "reoGridControl1";
            this.reoGridControl1.RowHeaderContextMenuStrip = null;
            this.reoGridControl1.Script = null;
            this.reoGridControl1.SheetTabContextMenuStrip = null;
            this.reoGridControl1.SheetTabNewButtonVisible = true;
            this.reoGridControl1.SheetTabVisible = true;
            this.reoGridControl1.SheetTabWidth = 60;
            this.reoGridControl1.ShowScrollEndSpacing = true;
            this.reoGridControl1.Size = new System.Drawing.Size(705, 473);
            this.reoGridControl1.TabIndex = 4;
            this.reoGridControl1.Text = "reoGridControl1";
            this.reoGridControl1.Visible = false;
            // 
            // ucResults_ExcelPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.reoGridControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panTemplateTypesRtPadding);
            this.Controls.Add(this.panHeader);
            this.Name = "ucResults_ExcelPreview";
            this.Size = new System.Drawing.Size(725, 514);
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            this.panHeaderRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.Panel panTemplateTypesRtPadding;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panHeaderRight;
        private MetroFramework.Controls.MetroButton butDelete;
        private MetroFramework.Controls.MetroButton butEmail;
        private MetroFramework.Controls.MetroButton butOpen;
        private System.Windows.Forms.Panel panel1;
        private unvell.ReoGrid.ReoGridControl reoGridControl1;
        private MetroFramework.Controls.MetroButton butPrint;
    }
}
