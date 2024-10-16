namespace MatrixBuilder
{
    partial class frmSBTemplate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSBTemplate));
            this.lblDefinition = new System.Windows.Forms.Label();
            this.panHeader = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panBottom = new System.Windows.Forms.Panel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.panFooterRight = new System.Windows.Forms.Panel();
            this.butCancel = new MetroFramework.Controls.MetroButton();
            this.butOk = new MetroFramework.Controls.MetroButton();
            this.splitContMain = new System.Windows.Forms.SplitContainer();
            this.reoGridControl1 = new unvell.ReoGrid.ReoGridControl();
            this.panLeftHeader = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblMatrixCaption = new System.Windows.Forms.Label();
            this.butEditSBTemp = new MetroFramework.Controls.MetroButton();
            this.rbNo = new MetroFramework.Controls.MetroRadioButton();
            this.rbYes = new MetroFramework.Controls.MetroRadioButton();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.lblMatrixDescription = new System.Windows.Forms.Label();
            this.txtbDescription = new System.Windows.Forms.TextBox();
            this.lblTemplateName = new System.Windows.Forms.Label();
            this.txtbTemplateName = new System.Windows.Forms.TextBox();
            this.lblWordTempFile = new System.Windows.Forms.Label();
            this.butLoadFile = new MetroFramework.Controls.MetroButton();
            this.txtbTemplate = new System.Windows.Forms.TextBox();
            this.lblMatrixTemplate = new System.Windows.Forms.Label();
            this.cboMatrixTemplate = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lblSBDetails = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panBottom.SuspendLayout();
            this.panFooterRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContMain)).BeginInit();
            this.splitContMain.Panel1.SuspendLayout();
            this.splitContMain.Panel2.SuspendLayout();
            this.splitContMain.SuspendLayout();
            this.panLeftHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDefinition
            // 
            this.lblDefinition.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDefinition.ForeColor = System.Drawing.Color.White;
            this.lblDefinition.Location = new System.Drawing.Point(291, 15);
            this.lblDefinition.Name = "lblDefinition";
            this.lblDefinition.Size = new System.Drawing.Size(774, 37);
            this.lblDefinition.TabIndex = 188;
            this.lblDefinition.Text = "Storyboards are MS Word documents with fields associated with a particular Matrix" +
    " Template. A Matrix Template may have one or more Storyboard templates.";
            // 
            // panHeader
            // 
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.Location = new System.Drawing.Point(10, 60);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(1068, 12);
            this.panHeader.TabIndex = 187;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(6, 9);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(48, 48);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 186;
            this.pictureBox2.TabStop = false;
            // 
            // panBottom
            // 
            this.panBottom.Controls.Add(this.lblMessage);
            this.panBottom.Controls.Add(this.panFooterRight);
            this.panBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panBottom.Location = new System.Drawing.Point(10, 446);
            this.panBottom.Name = "panBottom";
            this.panBottom.Size = new System.Drawing.Size(1068, 47);
            this.panBottom.TabIndex = 189;
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.White;
            this.lblMessage.Location = new System.Drawing.Point(12, 4);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(850, 43);
            this.lblMessage.TabIndex = 207;
            // 
            // panFooterRight
            // 
            this.panFooterRight.Controls.Add(this.butCancel);
            this.panFooterRight.Controls.Add(this.butOk);
            this.panFooterRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panFooterRight.Location = new System.Drawing.Point(868, 0);
            this.panFooterRight.Name = "panFooterRight";
            this.panFooterRight.Size = new System.Drawing.Size(200, 47);
            this.panFooterRight.TabIndex = 2;
            // 
            // butCancel
            // 
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.Location = new System.Drawing.Point(109, 12);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 10;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseSelectable = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butOk
            // 
            this.butOk.Location = new System.Drawing.Point(17, 12);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(75, 23);
            this.butOk.TabIndex = 9;
            this.butOk.Text = "Save";
            this.butOk.UseSelectable = true;
            this.butOk.Click += new System.EventHandler(this.butOk_Click);
            // 
            // splitContMain
            // 
            this.splitContMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContMain.Location = new System.Drawing.Point(10, 72);
            this.splitContMain.Name = "splitContMain";
            // 
            // splitContMain.Panel1
            // 
            this.splitContMain.Panel1.Controls.Add(this.reoGridControl1);
            this.splitContMain.Panel1.Controls.Add(this.panLeftHeader);
            // 
            // splitContMain.Panel2
            // 
            this.splitContMain.Panel2.Controls.Add(this.butEditSBTemp);
            this.splitContMain.Panel2.Controls.Add(this.rbNo);
            this.splitContMain.Panel2.Controls.Add(this.rbYes);
            this.splitContMain.Panel2.Controls.Add(this.lblQuestion);
            this.splitContMain.Panel2.Controls.Add(this.lblMatrixDescription);
            this.splitContMain.Panel2.Controls.Add(this.txtbDescription);
            this.splitContMain.Panel2.Controls.Add(this.lblTemplateName);
            this.splitContMain.Panel2.Controls.Add(this.txtbTemplateName);
            this.splitContMain.Panel2.Controls.Add(this.lblWordTempFile);
            this.splitContMain.Panel2.Controls.Add(this.butLoadFile);
            this.splitContMain.Panel2.Controls.Add(this.txtbTemplate);
            this.splitContMain.Panel2.Controls.Add(this.lblMatrixTemplate);
            this.splitContMain.Panel2.Controls.Add(this.cboMatrixTemplate);
            this.splitContMain.Panel2.Controls.Add(this.panel1);
            this.splitContMain.Size = new System.Drawing.Size(1068, 374);
            this.splitContMain.SplitterDistance = 506;
            this.splitContMain.TabIndex = 190;
            // 
            // reoGridControl1
            // 
            this.reoGridControl1.BackColor = System.Drawing.Color.White;
            this.reoGridControl1.ColumnHeaderContextMenuStrip = null;
            this.reoGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reoGridControl1.LeadHeaderContextMenuStrip = null;
            this.reoGridControl1.Location = new System.Drawing.Point(0, 35);
            this.reoGridControl1.Name = "reoGridControl1";
            this.reoGridControl1.RowHeaderContextMenuStrip = null;
            this.reoGridControl1.Script = null;
            this.reoGridControl1.SheetTabContextMenuStrip = null;
            this.reoGridControl1.SheetTabNewButtonVisible = true;
            this.reoGridControl1.SheetTabVisible = true;
            this.reoGridControl1.SheetTabWidth = 60;
            this.reoGridControl1.ShowScrollEndSpacing = true;
            this.reoGridControl1.Size = new System.Drawing.Size(506, 339);
            this.reoGridControl1.TabIndex = 3;
            this.reoGridControl1.Text = "reoGridControl1";
            this.reoGridControl1.Visible = false;
            // 
            // panLeftHeader
            // 
            this.panLeftHeader.Controls.Add(this.pictureBox1);
            this.panLeftHeader.Controls.Add(this.lblMatrixCaption);
            this.panLeftHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panLeftHeader.Location = new System.Drawing.Point(0, 0);
            this.panLeftHeader.Name = "panLeftHeader";
            this.panLeftHeader.Size = new System.Drawing.Size(506, 35);
            this.panLeftHeader.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(6, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(28, 28);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 187;
            this.pictureBox1.TabStop = false;
            // 
            // lblMatrixCaption
            // 
            this.lblMatrixCaption.AutoSize = true;
            this.lblMatrixCaption.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatrixCaption.ForeColor = System.Drawing.Color.White;
            this.lblMatrixCaption.Location = new System.Drawing.Point(38, 6);
            this.lblMatrixCaption.Name = "lblMatrixCaption";
            this.lblMatrixCaption.Size = new System.Drawing.Size(288, 21);
            this.lblMatrixCaption.TabIndex = 186;
            this.lblMatrixCaption.Text = "Storyboard\'s associated Matrix Template";
            // 
            // butEditSBTemp
            // 
            this.butEditSBTemp.Location = new System.Drawing.Point(365, 209);
            this.butEditSBTemp.Name = "butEditSBTemp";
            this.butEditSBTemp.Size = new System.Drawing.Size(154, 23);
            this.butEditSBTemp.TabIndex = 7;
            this.butEditSBTemp.Text = "Edit Storyboard Template";
            this.butEditSBTemp.UseSelectable = true;
            this.butEditSBTemp.Visible = false;
            this.butEditSBTemp.Click += new System.EventHandler(this.butEditSBTemp_Click);
            // 
            // rbNo
            // 
            this.rbNo.AutoSize = true;
            this.rbNo.Checked = true;
            this.rbNo.Location = new System.Drawing.Point(480, 173);
            this.rbNo.Name = "rbNo";
            this.rbNo.Size = new System.Drawing.Size(39, 15);
            this.rbNo.TabIndex = 6;
            this.rbNo.TabStop = true;
            this.rbNo.Text = "No";
            this.rbNo.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbNo.UseSelectable = true;
            this.rbNo.Visible = false;
            // 
            // rbYes
            // 
            this.rbYes.AutoSize = true;
            this.rbYes.Location = new System.Drawing.Point(431, 173);
            this.rbYes.Name = "rbYes";
            this.rbYes.Size = new System.Drawing.Size(40, 15);
            this.rbYes.TabIndex = 5;
            this.rbYes.Text = "Yes";
            this.rbYes.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbYes.UseSelectable = true;
            this.rbYes.Visible = false;
            // 
            // lblQuestion
            // 
            this.lblQuestion.AutoSize = true;
            this.lblQuestion.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion.ForeColor = System.Drawing.Color.White;
            this.lblQuestion.Location = new System.Drawing.Point(14, 171);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(411, 17);
            this.lblQuestion.TabIndex = 206;
            this.lblQuestion.Text = "Does your MS Word Storyboard Template have Matrix Builder fields?";
            this.lblQuestion.Visible = false;
            // 
            // lblMatrixDescription
            // 
            this.lblMatrixDescription.AutoSize = true;
            this.lblMatrixDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatrixDescription.ForeColor = System.Drawing.Color.White;
            this.lblMatrixDescription.Location = new System.Drawing.Point(15, 247);
            this.lblMatrixDescription.Name = "lblMatrixDescription";
            this.lblMatrixDescription.Size = new System.Drawing.Size(74, 17);
            this.lblMatrixDescription.TabIndex = 205;
            this.lblMatrixDescription.Text = "Description";
            // 
            // txtbDescription
            // 
            this.txtbDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbDescription.Location = new System.Drawing.Point(16, 268);
            this.txtbDescription.MaxLength = 250;
            this.txtbDescription.Multiline = true;
            this.txtbDescription.Name = "txtbDescription";
            this.txtbDescription.Size = new System.Drawing.Size(503, 80);
            this.txtbDescription.TabIndex = 8;
            // 
            // lblTemplateName
            // 
            this.lblTemplateName.AutoSize = true;
            this.lblTemplateName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemplateName.ForeColor = System.Drawing.Color.White;
            this.lblTemplateName.Location = new System.Drawing.Point(14, 43);
            this.lblTemplateName.Name = "lblTemplateName";
            this.lblTemplateName.Size = new System.Drawing.Size(170, 17);
            this.lblTemplateName.TabIndex = 203;
            this.lblTemplateName.Text = "Storyboard Template Name";
            // 
            // txtbTemplateName
            // 
            this.txtbTemplateName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbTemplateName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbTemplateName.Location = new System.Drawing.Point(16, 63);
            this.txtbTemplateName.MaxLength = 50;
            this.txtbTemplateName.Name = "txtbTemplateName";
            this.txtbTemplateName.Size = new System.Drawing.Size(205, 25);
            this.txtbTemplateName.TabIndex = 0;
            // 
            // lblWordTempFile
            // 
            this.lblWordTempFile.AutoSize = true;
            this.lblWordTempFile.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWordTempFile.ForeColor = System.Drawing.Color.White;
            this.lblWordTempFile.Location = new System.Drawing.Point(13, 112);
            this.lblWordTempFile.Name = "lblWordTempFile";
            this.lblWordTempFile.Size = new System.Drawing.Size(191, 17);
            this.lblWordTempFile.TabIndex = 201;
            this.lblWordTempFile.Text = "MS Word Template File (*.docx)";
            // 
            // butLoadFile
            // 
            this.butLoadFile.Location = new System.Drawing.Point(444, 133);
            this.butLoadFile.Name = "butLoadFile";
            this.butLoadFile.Size = new System.Drawing.Size(75, 23);
            this.butLoadFile.TabIndex = 3;
            this.butLoadFile.Text = "Select File";
            this.butLoadFile.UseSelectable = true;
            this.butLoadFile.Click += new System.EventHandler(this.butLoadFile_Click);
            // 
            // txtbTemplate
            // 
            this.txtbTemplate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbTemplate.Enabled = false;
            this.txtbTemplate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbTemplate.Location = new System.Drawing.Point(16, 131);
            this.txtbTemplate.MaxLength = 50;
            this.txtbTemplate.Name = "txtbTemplate";
            this.txtbTemplate.Size = new System.Drawing.Size(421, 25);
            this.txtbTemplate.TabIndex = 2;
            // 
            // lblMatrixTemplate
            // 
            this.lblMatrixTemplate.AutoSize = true;
            this.lblMatrixTemplate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatrixTemplate.ForeColor = System.Drawing.Color.White;
            this.lblMatrixTemplate.Location = new System.Drawing.Point(277, 41);
            this.lblMatrixTemplate.Name = "lblMatrixTemplate";
            this.lblMatrixTemplate.Size = new System.Drawing.Size(151, 17);
            this.lblMatrixTemplate.TabIndex = 191;
            this.lblMatrixTemplate.Text = "Select a Matrix Template";
            // 
            // cboMatrixTemplate
            // 
            this.cboMatrixTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMatrixTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboMatrixTemplate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMatrixTemplate.FormattingEnabled = true;
            this.cboMatrixTemplate.ItemHeight = 17;
            this.cboMatrixTemplate.Location = new System.Drawing.Point(280, 63);
            this.cboMatrixTemplate.Name = "cboMatrixTemplate";
            this.cboMatrixTemplate.Size = new System.Drawing.Size(239, 25);
            this.cboMatrixTemplate.TabIndex = 190;
            this.cboMatrixTemplate.SelectedIndexChanged += new System.EventHandler(this.cboMatrixTemplate_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Controls.Add(this.lblSBDetails);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(558, 35);
            this.panel1.TabIndex = 3;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(8, 3);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(28, 28);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 187;
            this.pictureBox3.TabStop = false;
            // 
            // lblSBDetails
            // 
            this.lblSBDetails.AutoSize = true;
            this.lblSBDetails.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSBDetails.ForeColor = System.Drawing.Color.White;
            this.lblSBDetails.Location = new System.Drawing.Point(38, 6);
            this.lblSBDetails.Name = "lblSBDetails";
            this.lblSBDetails.Size = new System.Drawing.Size(138, 21);
            this.lblSBDetails.TabIndex = 186;
            this.lblSBDetails.Text = "Storyboard Details";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // frmSBTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.CancelButton = this.butCancel;
            this.ClientSize = new System.Drawing.Size(1088, 503);
            this.ControlBox = false;
            this.Controls.Add(this.splitContMain);
            this.Controls.Add(this.panBottom);
            this.Controls.Add(this.lblDefinition);
            this.Controls.Add(this.panHeader);
            this.Controls.Add(this.pictureBox2);
            this.Name = "frmSBTemplate";
            this.Padding = new System.Windows.Forms.Padding(10, 60, 10, 10);
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "    Storyboard Template";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panBottom.ResumeLayout(false);
            this.panFooterRight.ResumeLayout(false);
            this.splitContMain.Panel1.ResumeLayout(false);
            this.splitContMain.Panel2.ResumeLayout(false);
            this.splitContMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContMain)).EndInit();
            this.splitContMain.ResumeLayout(false);
            this.panLeftHeader.ResumeLayout(false);
            this.panLeftHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblDefinition;
        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panBottom;
        private System.Windows.Forms.Panel panFooterRight;
        private MetroFramework.Controls.MetroButton butCancel;
        private MetroFramework.Controls.MetroButton butOk;
        private System.Windows.Forms.SplitContainer splitContMain;
        private System.Windows.Forms.Panel panLeftHeader;
        private System.Windows.Forms.Label lblMatrixCaption;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lblSBDetails;
        private unvell.ReoGrid.ReoGridControl reoGridControl1;
        private System.Windows.Forms.Label lblMatrixTemplate;
        private System.Windows.Forms.ComboBox cboMatrixTemplate;
        private System.Windows.Forms.Label lblMatrixDescription;
        private System.Windows.Forms.TextBox txtbDescription;
        private System.Windows.Forms.Label lblTemplateName;
        private System.Windows.Forms.TextBox txtbTemplateName;
        private System.Windows.Forms.Label lblWordTempFile;
        private MetroFramework.Controls.MetroButton butLoadFile;
        private System.Windows.Forms.TextBox txtbTemplate;
        private System.Windows.Forms.Label lblQuestion;
        private MetroFramework.Controls.MetroButton butEditSBTemp;
        private MetroFramework.Controls.MetroRadioButton rbNo;
        private MetroFramework.Controls.MetroRadioButton rbYes;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}