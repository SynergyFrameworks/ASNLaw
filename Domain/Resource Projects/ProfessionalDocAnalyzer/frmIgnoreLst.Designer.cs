namespace ProfessionalDocAnalyzer
{
    partial class frmIgnoreLst
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
            this.lblHeader = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panFooter = new System.Windows.Forms.Panel();
            this.butCancel = new MetroFramework.Controls.MetroButton();
            this.butOK = new MetroFramework.Controls.MetroButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.lstAcronyms = new System.Windows.Forms.ListBox();
            this.txtbFileName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panBatchLoad = new System.Windows.Forms.Panel();
            this.panMaintain = new System.Windows.Forms.Panel();
            this.butDelete = new MetroFramework.Controls.MetroButton();
            this.butReplace = new MetroFramework.Controls.MetroButton();
            this.butNew = new MetroFramework.Controls.MetroButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAcron = new System.Windows.Forms.TextBox();
            this.panRightHeader = new System.Windows.Forms.Panel();
            this.butMaintain = new System.Windows.Forms.Button();
            this.butBatchLoad = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtbMessage = new System.Windows.Forms.TextBox();
            this.panDelimitTitle = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panDelintBottom = new System.Windows.Forms.Panel();
            this.butImportFile = new MetroFramework.Controls.MetroButton();
            this.butAddBatchAcronyms = new MetroFramework.Controls.MetroButton();
            this.txtBatchAcronyms = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panBatchLoad.SuspendLayout();
            this.panMaintain.SuspendLayout();
            this.panRightHeader.SuspendLayout();
            this.panDelimitTitle.SuspendLayout();
            this.panDelintBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.Black;
            this.lblHeader.Location = new System.Drawing.Point(40, 15);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(208, 30);
            this.lblHeader.TabIndex = 109;
            this.lblHeader.Text = "Ignore Acronyms List";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ProfessionalDocAnalyzer.Properties.Resources.appbar_cancel;
            this.pictureBox1.Location = new System.Drawing.Point(9, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(38, 38);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 110;
            this.pictureBox1.TabStop = false;
            // 
            // panFooter
            // 
            this.panFooter.Controls.Add(this.butCancel);
            this.panFooter.Controls.Add(this.butOK);
            this.panFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panFooter.Location = new System.Drawing.Point(10, 374);
            this.panFooter.Name = "panFooter";
            this.panFooter.Size = new System.Drawing.Size(737, 39);
            this.panFooter.TabIndex = 111;
            // 
            // butCancel
            // 
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.Location = new System.Drawing.Point(659, 6);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 83;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseSelectable = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(578, 6);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(75, 23);
            this.butOK.TabIndex = 84;
            this.butOK.Text = "Save";
            this.butOK.UseSelectable = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(10, 60);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.lstAcronyms);
            this.splitContainer1.Panel1.Controls.Add(this.txtbFileName);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel2.Controls.Add(this.panBatchLoad);
            this.splitContainer1.Panel2.Controls.Add(this.panMaintain);
            this.splitContainer1.Panel2.Controls.Add(this.panRightHeader);
            this.splitContainer1.Size = new System.Drawing.Size(737, 314);
            this.splitContainer1.SplitterDistance = 346;
            this.splitContainer1.TabIndex = 112;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(18, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 17);
            this.label2.TabIndex = 83;
            this.label2.Text = "Ignore Acronyms";
            // 
            // lstAcronyms
            // 
            this.lstAcronyms.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstAcronyms.ForeColor = System.Drawing.Color.Black;
            this.lstAcronyms.FormattingEnabled = true;
            this.lstAcronyms.ItemHeight = 17;
            this.lstAcronyms.Location = new System.Drawing.Point(17, 85);
            this.lstAcronyms.Name = "lstAcronyms";
            this.lstAcronyms.Size = new System.Drawing.Size(292, 225);
            this.lstAcronyms.Sorted = true;
            this.lstAcronyms.TabIndex = 82;
            // 
            // txtbFileName
            // 
            this.txtbFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbFileName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbFileName.Location = new System.Drawing.Point(17, 26);
            this.txtbFileName.MaxLength = 50;
            this.txtbFileName.Name = "txtbFileName";
            this.txtbFileName.Size = new System.Drawing.Size(292, 25);
            this.txtbFileName.TabIndex = 80;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(18, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(169, 17);
            this.label4.TabIndex = 81;
            this.label4.Text = "Ignore Acronyms List Name";
            // 
            // panBatchLoad
            // 
            this.panBatchLoad.Controls.Add(this.txtBatchAcronyms);
            this.panBatchLoad.Controls.Add(this.panDelintBottom);
            this.panBatchLoad.Controls.Add(this.panDelimitTitle);
            this.panBatchLoad.Location = new System.Drawing.Point(197, 106);
            this.panBatchLoad.Name = "panBatchLoad";
            this.panBatchLoad.Size = new System.Drawing.Size(270, 154);
            this.panBatchLoad.TabIndex = 4;
            // 
            // panMaintain
            // 
            this.panMaintain.BackColor = System.Drawing.Color.Black;
            this.panMaintain.Controls.Add(this.butDelete);
            this.panMaintain.Controls.Add(this.butReplace);
            this.panMaintain.Controls.Add(this.butNew);
            this.panMaintain.Controls.Add(this.label1);
            this.panMaintain.Controls.Add(this.txtAcron);
            this.panMaintain.Location = new System.Drawing.Point(34, 123);
            this.panMaintain.Name = "panMaintain";
            this.panMaintain.Size = new System.Drawing.Size(200, 100);
            this.panMaintain.TabIndex = 3;
            this.panMaintain.Visible = false;
            // 
            // butDelete
            // 
            this.butDelete.Location = new System.Drawing.Point(14, 100);
            this.butDelete.Name = "butDelete";
            this.butDelete.Size = new System.Drawing.Size(75, 23);
            this.butDelete.TabIndex = 81;
            this.butDelete.Text = "Delete";
            this.butDelete.UseSelectable = true;
            // 
            // butReplace
            // 
            this.butReplace.Location = new System.Drawing.Point(14, 68);
            this.butReplace.Name = "butReplace";
            this.butReplace.Size = new System.Drawing.Size(75, 23);
            this.butReplace.TabIndex = 80;
            this.butReplace.Text = "Replace";
            this.butReplace.UseSelectable = true;
            // 
            // butNew
            // 
            this.butNew.Location = new System.Drawing.Point(14, 36);
            this.butNew.Name = "butNew";
            this.butNew.Size = new System.Drawing.Size(75, 23);
            this.butNew.TabIndex = 79;
            this.butNew.Text = "Add";
            this.butNew.UseSelectable = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(104, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 78;
            this.label1.Text = "Acronym";
            // 
            // txtAcron
            // 
            this.txtAcron.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAcron.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAcron.Location = new System.Drawing.Point(107, 35);
            this.txtAcron.Name = "txtAcron";
            this.txtAcron.Size = new System.Drawing.Size(148, 25);
            this.txtAcron.TabIndex = 77;
            // 
            // panRightHeader
            // 
            this.panRightHeader.Controls.Add(this.butMaintain);
            this.panRightHeader.Controls.Add(this.butBatchLoad);
            this.panRightHeader.Controls.Add(this.lblMessage);
            this.panRightHeader.Controls.Add(this.txtbMessage);
            this.panRightHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panRightHeader.Location = new System.Drawing.Point(0, 0);
            this.panRightHeader.Name = "panRightHeader";
            this.panRightHeader.Size = new System.Drawing.Size(387, 100);
            this.panRightHeader.TabIndex = 1;
            // 
            // butMaintain
            // 
            this.butMaintain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.butMaintain.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.butMaintain.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butMaintain.Location = new System.Drawing.Point(95, 71);
            this.butMaintain.Name = "butMaintain";
            this.butMaintain.Size = new System.Drawing.Size(75, 23);
            this.butMaintain.TabIndex = 79;
            this.butMaintain.Text = "Maintain";
            this.butMaintain.UseVisualStyleBackColor = false;
            this.butMaintain.Click += new System.EventHandler(this.butMaintain_Click);
            this.butMaintain.MouseEnter += new System.EventHandler(this.butMaintain_MouseEnter);
            // 
            // butBatchLoad
            // 
            this.butBatchLoad.BackColor = System.Drawing.Color.LightGreen;
            this.butBatchLoad.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.butBatchLoad.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butBatchLoad.Location = new System.Drawing.Point(14, 71);
            this.butBatchLoad.Name = "butBatchLoad";
            this.butBatchLoad.Size = new System.Drawing.Size(75, 23);
            this.butBatchLoad.TabIndex = 78;
            this.butBatchLoad.Text = "Batch Load";
            this.butBatchLoad.UseVisualStyleBackColor = false;
            this.butBatchLoad.Click += new System.EventHandler(this.butBatchLoad_Click);
            this.butBatchLoad.MouseEnter += new System.EventHandler(this.butBatchLoad_MouseEnter);
            this.butBatchLoad.MouseLeave += new System.EventHandler(this.butBatchLoad_MouseLeave);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(275, 44);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 77;
            this.lblMessage.Visible = false;
            // 
            // txtbMessage
            // 
            this.txtbMessage.BackColor = System.Drawing.Color.White;
            this.txtbMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbMessage.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbMessage.ForeColor = System.Drawing.Color.Blue;
            this.txtbMessage.Location = new System.Drawing.Point(14, 6);
            this.txtbMessage.Multiline = true;
            this.txtbMessage.Name = "txtbMessage";
            this.txtbMessage.Size = new System.Drawing.Size(348, 59);
            this.txtbMessage.TabIndex = 76;
            // 
            // panDelimitTitle
            // 
            this.panDelimitTitle.BackColor = System.Drawing.Color.Black;
            this.panDelimitTitle.Controls.Add(this.label5);
            this.panDelimitTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panDelimitTitle.Location = new System.Drawing.Point(0, 0);
            this.panDelimitTitle.Name = "panDelimitTitle";
            this.panDelimitTitle.Size = new System.Drawing.Size(270, 20);
            this.panDelimitTitle.TabIndex = 82;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(3, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 17);
            this.label5.TabIndex = 80;
            this.label5.Text = "Delimited Acronyms";
            // 
            // panDelintBottom
            // 
            this.panDelintBottom.BackColor = System.Drawing.Color.Black;
            this.panDelintBottom.Controls.Add(this.butImportFile);
            this.panDelintBottom.Controls.Add(this.butAddBatchAcronyms);
            this.panDelintBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panDelintBottom.Location = new System.Drawing.Point(0, 120);
            this.panDelintBottom.Name = "panDelintBottom";
            this.panDelintBottom.Size = new System.Drawing.Size(270, 34);
            this.panDelintBottom.TabIndex = 84;
            // 
            // butImportFile
            // 
            this.butImportFile.Location = new System.Drawing.Point(130, 6);
            this.butImportFile.Name = "butImportFile";
            this.butImportFile.Size = new System.Drawing.Size(75, 23);
            this.butImportFile.TabIndex = 83;
            this.butImportFile.Text = "Import File";
            this.butImportFile.UseSelectable = true;
            this.butImportFile.Visible = false;
            // 
            // butAddBatchAcronyms
            // 
            this.butAddBatchAcronyms.Location = new System.Drawing.Point(10, 5);
            this.butAddBatchAcronyms.Name = "butAddBatchAcronyms";
            this.butAddBatchAcronyms.Size = new System.Drawing.Size(75, 23);
            this.butAddBatchAcronyms.TabIndex = 82;
            this.butAddBatchAcronyms.Text = "Add";
            this.butAddBatchAcronyms.UseSelectable = true;
            this.butAddBatchAcronyms.Click += new System.EventHandler(this.butAddBatchAcronyms_Click);
            // 
            // txtBatchAcronyms
            // 
            this.txtBatchAcronyms.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBatchAcronyms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBatchAcronyms.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBatchAcronyms.Location = new System.Drawing.Point(0, 20);
            this.txtBatchAcronyms.Multiline = true;
            this.txtBatchAcronyms.Name = "txtBatchAcronyms";
            this.txtBatchAcronyms.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBatchAcronyms.Size = new System.Drawing.Size(270, 100);
            this.txtBatchAcronyms.TabIndex = 85;
            // 
            // frmIgnoreLst
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(757, 418);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panFooter);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblHeader);
            this.Name = "frmIgnoreLst";
            this.Padding = new System.Windows.Forms.Padding(10, 60, 10, 5);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panFooter.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panBatchLoad.ResumeLayout(false);
            this.panBatchLoad.PerformLayout();
            this.panMaintain.ResumeLayout(false);
            this.panMaintain.PerformLayout();
            this.panRightHeader.ResumeLayout(false);
            this.panRightHeader.PerformLayout();
            this.panDelimitTitle.ResumeLayout(false);
            this.panDelimitTitle.PerformLayout();
            this.panDelintBottom.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel panFooter;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MetroFramework.Controls.MetroButton butCancel;
        private MetroFramework.Controls.MetroButton butOK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstAcronyms;
        private System.Windows.Forms.TextBox txtbFileName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panRightHeader;
        private System.Windows.Forms.Button butMaintain;
        private System.Windows.Forms.Button butBatchLoad;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtbMessage;
        private System.Windows.Forms.Panel panMaintain;
        private MetroFramework.Controls.MetroButton butDelete;
        private MetroFramework.Controls.MetroButton butReplace;
        private MetroFramework.Controls.MetroButton butNew;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAcron;
        private System.Windows.Forms.Panel panBatchLoad;
        private System.Windows.Forms.Panel panDelimitTitle;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panDelintBottom;
        private MetroFramework.Controls.MetroButton butImportFile;
        private MetroFramework.Controls.MetroButton butAddBatchAcronyms;
        private System.Windows.Forms.TextBox txtBatchAcronyms;
    }
}