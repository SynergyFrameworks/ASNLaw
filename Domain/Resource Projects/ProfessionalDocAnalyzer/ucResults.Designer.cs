namespace ProfessionalDocAnalyzer
{
    partial class ucResults
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucResults));
            this.panHeader = new System.Windows.Forms.Panel();
            this.butFind = new MetroFramework.Controls.MetroButton();
            this.txtbFind = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.picHeader = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvProjects = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ucDeepAnalyticsResults1 = new ProfessionalDocAnalyzer.ucDeepAnalyticsResults();
            this.ucResults_RTF_Preview1 = new ProfessionalDocAnalyzer.ucResults_RTF_Preview();
            this.ucResults_HTMLPreview1 = new ProfessionalDocAnalyzer.ucResults_HTMLPreview();
            this.ucDiffSxS1 = new ProfessionalDocAnalyzer.ucDiffSxS();
            this.ucAcroSeekerResults1 = new ProfessionalDocAnalyzer.ucAcroSeekerResults();
            this.ucQCAnalysisResults1 = new ProfessionalDocAnalyzer.ucQCAnalysisResults();
            this.ucResultsMultiDic1 = new ProfessionalDocAnalyzer.ucResultsMultiDic();
            this.ucResultsConcepts1 = new ProfessionalDocAnalyzer.ucResultsConcepts();
            this.ucResultsMultiConcepts1 = new ProfessionalDocAnalyzer.ucResultsMultiConcepts();
            this.ucResults_Home1 = new ProfessionalDocAnalyzer.ucResults_Home();
            this.ucResults_TxtDocView1 = new ProfessionalDocAnalyzer.ucResults_TxtDocView();
            this.ucResults_WordPreview1 = new ProfessionalDocAnalyzer.ucResults_WordPreview();
            this.ucResultsDic1 = new ProfessionalDocAnalyzer.ucResultsDic();
            this.ucAnalysisResults1 = new ProfessionalDocAnalyzer.ucAnalysisResults();
            this.ucResults_ExcelPreview1 = new ProfessionalDocAnalyzer.ucResults_ExcelPreview();
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
            this.panHeader.Controls.Add(this.butFind);
            this.panHeader.Controls.Add(this.txtbFind);
            this.panHeader.Controls.Add(this.label1);
            this.panHeader.Controls.Add(this.lblMessage);
            this.panHeader.Controls.Add(this.lblHeader);
            this.panHeader.Controls.Add(this.picHeader);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(939, 50);
            this.panHeader.TabIndex = 9;
            // 
            // butFind
            // 
            this.butFind.Location = new System.Drawing.Point(517, 14);
            this.butFind.Name = "butFind";
            this.butFind.Size = new System.Drawing.Size(75, 23);
            this.butFind.TabIndex = 92;
            this.butFind.Text = "Find";
            this.butFind.UseSelectable = true;
            this.butFind.Click += new System.EventHandler(this.butFind_Click);
            // 
            // txtbFind
            // 
            this.txtbFind.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbFind.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbFind.Location = new System.Drawing.Point(312, 13);
            this.txtbFind.Name = "txtbFind";
            this.txtbFind.Size = new System.Drawing.Size(196, 25);
            this.txtbFind.TabIndex = 91;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(144, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 17);
            this.label1.TabIndex = 90;
            this.label1.Text = "In the Results tree find item";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.White;
            this.lblMessage.Location = new System.Drawing.Point(230, 16);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 25);
            this.lblMessage.TabIndex = 16;
            this.lblMessage.Visible = false;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.Black;
            this.lblHeader.Location = new System.Drawing.Point(44, 11);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(78, 30);
            this.lblHeader.TabIndex = 15;
            this.lblHeader.Text = "Results";
            // 
            // picHeader
            // 
            this.picHeader.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picHeader.Image = ((System.Drawing.Image)(resources.GetObject("picHeader.Image")));
            this.picHeader.Location = new System.Drawing.Point(9, 6);
            this.picHeader.Name = "picHeader";
            this.picHeader.Size = new System.Drawing.Size(38, 38);
            this.picHeader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHeader.TabIndex = 14;
            this.picHeader.TabStop = false;
            this.picHeader.Click += new System.EventHandler(this.picHeader_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 50);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvProjects);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ucDeepAnalyticsResults1);
            this.splitContainer1.Panel2.Controls.Add(this.ucResults_RTF_Preview1);
            this.splitContainer1.Panel2.Controls.Add(this.ucResults_HTMLPreview1);
            this.splitContainer1.Panel2.Controls.Add(this.ucDiffSxS1);
            this.splitContainer1.Panel2.Controls.Add(this.ucAcroSeekerResults1);
            this.splitContainer1.Panel2.Controls.Add(this.ucQCAnalysisResults1);
            this.splitContainer1.Panel2.Controls.Add(this.ucResultsMultiDic1);
            this.splitContainer1.Panel2.Controls.Add(this.ucResultsConcepts1);
            this.splitContainer1.Panel2.Controls.Add(this.ucResultsMultiConcepts1);
            this.splitContainer1.Panel2.Controls.Add(this.ucResults_Home1);
            this.splitContainer1.Panel2.Controls.Add(this.ucResults_TxtDocView1);
            this.splitContainer1.Panel2.Controls.Add(this.ucResults_WordPreview1);
            this.splitContainer1.Panel2.Controls.Add(this.ucResultsDic1);
            this.splitContainer1.Panel2.Controls.Add(this.ucAnalysisResults1);
            this.splitContainer1.Panel2.Controls.Add(this.ucResults_ExcelPreview1);
            this.splitContainer1.Size = new System.Drawing.Size(939, 451);
            this.splitContainer1.SplitterDistance = 258;
            this.splitContainer1.TabIndex = 10;
            // 
            // tvProjects
            // 
            this.tvProjects.BackColor = System.Drawing.Color.Black;
            this.tvProjects.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvProjects.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvProjects.ForeColor = System.Drawing.Color.White;
            this.tvProjects.FullRowSelect = true;
            this.tvProjects.HideSelection = false;
            this.tvProjects.HotTracking = true;
            this.tvProjects.ImageIndex = 0;
            this.tvProjects.ImageList = this.imageList1;
            this.tvProjects.Location = new System.Drawing.Point(0, 0);
            this.tvProjects.Name = "tvProjects";
            this.tvProjects.SelectedImageIndex = 0;
            this.tvProjects.ShowLines = false;
            this.tvProjects.ShowNodeToolTips = true;
            this.tvProjects.Size = new System.Drawing.Size(369, 419);
            this.tvProjects.StateImageList = this.imageList1;
            this.tvProjects.TabIndex = 161;
            this.tvProjects.Visible = false;
            this.tvProjects.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvProjects_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "UseCase");
            this.imageList1.Images.SetKeyName(1, "Project");
            this.imageList1.Images.SetKeyName(2, "Analysis");
            this.imageList1.Images.SetKeyName(3, "Document");
            this.imageList1.Images.SetKeyName(4, "Documents");
            this.imageList1.Images.SetKeyName(5, "Excel");
            this.imageList1.Images.SetKeyName(6, "Word");
            this.imageList1.Images.SetKeyName(7, "Report");
            this.imageList1.Images.SetKeyName(8, "AnalysisResults");
            this.imageList1.Images.SetKeyName(9, "appbar.page.xml.png");
            // 
            // ucDeepAnalyticsResults1
            // 
            this.ucDeepAnalyticsResults1.Location = new System.Drawing.Point(80, 18);
            this.ucDeepAnalyticsResults1.Name = "ucDeepAnalyticsResults1";
            this.ucDeepAnalyticsResults1.Size = new System.Drawing.Size(1270, 511);
            this.ucDeepAnalyticsResults1.TabIndex = 14;
            this.ucDeepAnalyticsResults1.Visible = false;
            // 
            // ucResults_RTF_Preview1
            // 
            this.ucResults_RTF_Preview1.BackColor = System.Drawing.Color.White;
            this.ucResults_RTF_Preview1.Location = new System.Drawing.Point(80, 20);
            this.ucResults_RTF_Preview1.Name = "ucResults_RTF_Preview1";
            this.ucResults_RTF_Preview1.Size = new System.Drawing.Size(724, 520);
            this.ucResults_RTF_Preview1.TabIndex = 13;
            this.ucResults_RTF_Preview1.Visible = false;
            // 
            // ucResults_HTMLPreview1
            // 
            this.ucResults_HTMLPreview1.BackColor = System.Drawing.Color.White;
            this.ucResults_HTMLPreview1.Location = new System.Drawing.Point(26, 20);
            this.ucResults_HTMLPreview1.Name = "ucResults_HTMLPreview1";
            this.ucResults_HTMLPreview1.Size = new System.Drawing.Size(724, 520);
            this.ucResults_HTMLPreview1.TabIndex = 12;
            this.ucResults_HTMLPreview1.Visible = false;
            // 
            // ucDiffSxS1
            // 
            this.ucDiffSxS1.Location = new System.Drawing.Point(60, 36);
            this.ucDiffSxS1.Name = "ucDiffSxS1";
            this.ucDiffSxS1.Size = new System.Drawing.Size(1277, 424);
            this.ucDiffSxS1.TabIndex = 11;
            this.ucDiffSxS1.Visible = false;
            // 
            // ucAcroSeekerResults1
            // 
            this.ucAcroSeekerResults1.BackColor = System.Drawing.Color.White;
            this.ucAcroSeekerResults1.Location = new System.Drawing.Point(81, 38);
            this.ucAcroSeekerResults1.Name = "ucAcroSeekerResults1";
            this.ucAcroSeekerResults1.Size = new System.Drawing.Size(889, 508);
            this.ucAcroSeekerResults1.TabIndex = 10;
            this.ucAcroSeekerResults1.Visible = false;
            // 
            // ucQCAnalysisResults1
            // 
            this.ucQCAnalysisResults1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucQCAnalysisResults1.Location = new System.Drawing.Point(119, 20);
            this.ucQCAnalysisResults1.Name = "ucQCAnalysisResults1";
            this.ucQCAnalysisResults1.Size = new System.Drawing.Size(1338, 501);
            this.ucQCAnalysisResults1.TabIndex = 9;
            this.ucQCAnalysisResults1.Visible = false;
            // 
            // ucResultsMultiDic1
            // 
            this.ucResultsMultiDic1.BackColor = System.Drawing.Color.Black;
            this.ucResultsMultiDic1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucResultsMultiDic1.Location = new System.Drawing.Point(57, 56);
            this.ucResultsMultiDic1.Name = "ucResultsMultiDic1";
            this.ucResultsMultiDic1.Size = new System.Drawing.Size(1043, 589);
            this.ucResultsMultiDic1.TabIndex = 8;
            this.ucResultsMultiDic1.Visible = false;
            // 
            // ucResultsConcepts1
            // 
            this.ucResultsConcepts1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucResultsConcepts1.Location = new System.Drawing.Point(237, 6);
            this.ucResultsConcepts1.Name = "ucResultsConcepts1";
            this.ucResultsConcepts1.Size = new System.Drawing.Size(1043, 626);
            this.ucResultsConcepts1.TabIndex = 7;
            this.ucResultsConcepts1.Visible = false;
            // 
            // ucResultsMultiConcepts1
            // 
            this.ucResultsMultiConcepts1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucResultsMultiConcepts1.Location = new System.Drawing.Point(198, 38);
            this.ucResultsMultiConcepts1.Name = "ucResultsMultiConcepts1";
            this.ucResultsMultiConcepts1.Size = new System.Drawing.Size(1043, 589);
            this.ucResultsMultiConcepts1.TabIndex = 6;
            this.ucResultsMultiConcepts1.Visible = false;
            // 
            // ucResults_Home1
            // 
            this.ucResults_Home1.BackColor = System.Drawing.Color.White;
            this.ucResults_Home1.Location = new System.Drawing.Point(198, 38);
            this.ucResults_Home1.Name = "ucResults_Home1";
            this.ucResults_Home1.Size = new System.Drawing.Size(696, 378);
            this.ucResults_Home1.TabIndex = 5;
            this.ucResults_Home1.Visible = false;
            // 
            // ucResults_TxtDocView1
            // 
            this.ucResults_TxtDocView1.BackColor = System.Drawing.Color.White;
            this.ucResults_TxtDocView1.Location = new System.Drawing.Point(152, 65);
            this.ucResults_TxtDocView1.Name = "ucResults_TxtDocView1";
            this.ucResults_TxtDocView1.Size = new System.Drawing.Size(881, 542);
            this.ucResults_TxtDocView1.TabIndex = 4;
            this.ucResults_TxtDocView1.Visible = false;
            // 
            // ucResults_WordPreview1
            // 
            this.ucResults_WordPreview1.BackColor = System.Drawing.Color.White;
            this.ucResults_WordPreview1.Location = new System.Drawing.Point(119, 99);
            this.ucResults_WordPreview1.Name = "ucResults_WordPreview1";
            this.ucResults_WordPreview1.Size = new System.Drawing.Size(800, 562);
            this.ucResults_WordPreview1.TabIndex = 3;
            this.ucResults_WordPreview1.Visible = false;
            // 
            // ucResultsDic1
            // 
            this.ucResultsDic1.BackColor = System.Drawing.Color.Black;
            this.ucResultsDic1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucResultsDic1.Location = new System.Drawing.Point(81, 136);
            this.ucResultsDic1.Name = "ucResultsDic1";
            this.ucResultsDic1.Size = new System.Drawing.Size(1043, 626);
            this.ucResultsDic1.TabIndex = 2;
            this.ucResultsDic1.Visible = false;
            // 
            // ucAnalysisResults1
            // 
            this.ucAnalysisResults1.Location = new System.Drawing.Point(44, 215);
            this.ucAnalysisResults1.Name = "ucAnalysisResults1";
            this.ucAnalysisResults1.Size = new System.Drawing.Size(1027, 582);
            this.ucAnalysisResults1.TabIndex = 1;
            this.ucAnalysisResults1.Visible = false;
            this.ucAnalysisResults1.RunDeepAnalysisResults += new ProfessionalDocAnalyzer.ucAnalysisResults.ProcessHandler(this.ucAnalysisResults1_RunDeepAnalysisResults);
            // 
            // ucResults_ExcelPreview1
            // 
            this.ucResults_ExcelPreview1.BackColor = System.Drawing.Color.White;
            this.ucResults_ExcelPreview1.Location = new System.Drawing.Point(12, 315);
            this.ucResults_ExcelPreview1.Name = "ucResults_ExcelPreview1";
            this.ucResults_ExcelPreview1.Size = new System.Drawing.Size(725, 514);
            this.ucResults_ExcelPreview1.TabIndex = 0;
            this.ucResults_ExcelPreview1.Visible = false;
            // 
            // ucResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panHeader);
            this.Name = "ucResults";
            this.Size = new System.Drawing.Size(939, 501);
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
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
        private System.Windows.Forms.TreeView tvProjects;
        private System.Windows.Forms.ImageList imageList1;
        private ucResults_ExcelPreview ucResults_ExcelPreview1;
        private ucAnalysisResults ucAnalysisResults1;
        private ucResultsDic ucResultsDic1;
        private ucResults_WordPreview ucResults_WordPreview1;
        private ucResults_TxtDocView ucResults_TxtDocView1;
        private ucResults_Home ucResults_Home1;
        private ucResultsMultiConcepts ucResultsMultiConcepts1;
        private ucResultsConcepts ucResultsConcepts1;
        private ucResultsMultiDic ucResultsMultiDic1;
        private ucQCAnalysisResults ucQCAnalysisResults1;
        private ucAcroSeekerResults ucAcroSeekerResults1;
        private ucDiffSxS ucDiffSxS1;
        private ucResults_HTMLPreview ucResults_HTMLPreview1;
        private ucResults_RTF_Preview ucResults_RTF_Preview1;
        private ucDeepAnalyticsResults ucDeepAnalyticsResults1;
        private MetroFramework.Controls.MetroButton butFind;
        private System.Windows.Forms.TextBox txtbFind;
        private System.Windows.Forms.Label label1;
    }
}
