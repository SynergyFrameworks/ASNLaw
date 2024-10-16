namespace ProfessionalDocAnalyzer
{
    partial class ucProjectsDocs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucProjectsDocs));
            this.panHeader = new System.Windows.Forms.Panel();
            this.lblAnalysisName = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lstbProjects = new System.Windows.Forms.ListBox();
            this.panLeftFooter = new System.Windows.Forms.Panel();
            this.txtbProjectDescription = new System.Windows.Forms.TextBox();
            this.lblProjectDescription = new System.Windows.Forms.Label();
            this.panLeftHeader = new System.Windows.Forms.Panel();
            this.picProjects = new System.Windows.Forms.PictureBox();
            this.lblInstructions_Projects = new System.Windows.Forms.Label();
            this.butProject_Remove = new MetroFramework.Controls.MetroButton();
            this.butProject_New = new MetroFramework.Controls.MetroButton();
            this.lblProjects = new System.Windows.Forms.Label();
            this.lstbDocsMulti = new System.Windows.Forms.CheckedListBox();
            this.lstbDocs = new System.Windows.Forms.ListBox();
            this.panRightFooter = new System.Windows.Forms.Panel();
            this.panRightHeader = new System.Windows.Forms.Panel();
            this.panParseType = new System.Windows.Forms.Panel();
            this.lblParseType = new System.Windows.Forms.Label();
            this.rdbParagraph = new System.Windows.Forms.RadioButton();
            this.rdbLegal = new System.Windows.Forms.RadioButton();
            this.picDocuments = new System.Windows.Forms.PictureBox();
            this.lblInstructions_Documents = new System.Windows.Forms.Label();
            this.butDoc_Replace = new MetroFramework.Controls.MetroButton();
            this.butDoc_Edit = new MetroFramework.Controls.MetroButton();
            this.butDoc_Remove = new MetroFramework.Controls.MetroButton();
            this.butDoc_New = new MetroFramework.Controls.MetroButton();
            this.lblDocuments = new System.Windows.Forms.Label();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.panDiff = new System.Windows.Forms.Panel();
            this.lblDiff = new System.Windows.Forms.Label();
            this.cboDiff = new System.Windows.Forms.ComboBox();
            this.ucDocDetails1 = new ProfessionalDocAnalyzer.ucDocDetails();
            this.panHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panLeftFooter.SuspendLayout();
            this.panLeftHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProjects)).BeginInit();
            this.panRightFooter.SuspendLayout();
            this.panRightHeader.SuspendLayout();
            this.panParseType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDocuments)).BeginInit();
            this.panDiff.SuspendLayout();
            this.SuspendLayout();
            // 
            // panHeader
            // 
            this.panHeader.Controls.Add(this.lblAnalysisName);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(797, 39);
            this.panHeader.TabIndex = 0;
            // 
            // lblAnalysisName
            // 
            this.lblAnalysisName.AutoSize = true;
            this.lblAnalysisName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnalysisName.Location = new System.Drawing.Point(10, 10);
            this.lblAnalysisName.Name = "lblAnalysisName";
            this.lblAnalysisName.Size = new System.Drawing.Size(113, 21);
            this.lblAnalysisName.TabIndex = 1;
            this.lblAnalysisName.Text = "Analysis Name";
            this.lblAnalysisName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 39);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Black;
            this.splitContainer1.Panel1.Controls.Add(this.lstbProjects);
            this.splitContainer1.Panel1.Controls.Add(this.panLeftFooter);
            this.splitContainer1.Panel1.Controls.Add(this.panLeftHeader);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lstbDocsMulti);
            this.splitContainer1.Panel2.Controls.Add(this.lstbDocs);
            this.splitContainer1.Panel2.Controls.Add(this.panRightFooter);
            this.splitContainer1.Panel2.Controls.Add(this.panRightHeader);
            this.splitContainer1.Size = new System.Drawing.Size(797, 429);
            this.splitContainer1.SplitterDistance = 194;
            this.splitContainer1.TabIndex = 6;
            // 
            // lstbProjects
            // 
            this.lstbProjects.BackColor = System.Drawing.Color.Black;
            this.lstbProjects.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstbProjects.Dock = System.Windows.Forms.DockStyle.Left;
            this.lstbProjects.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstbProjects.ForeColor = System.Drawing.Color.White;
            this.lstbProjects.FormattingEnabled = true;
            this.lstbProjects.ItemHeight = 21;
            this.lstbProjects.Location = new System.Drawing.Point(0, 108);
            this.lstbProjects.Name = "lstbProjects";
            this.lstbProjects.Size = new System.Drawing.Size(233, 221);
            this.lstbProjects.TabIndex = 2;
            this.lstbProjects.SelectedIndexChanged += new System.EventHandler(this.lstbProjects_SelectedIndexChanged);
            // 
            // panLeftFooter
            // 
            this.panLeftFooter.Controls.Add(this.txtbProjectDescription);
            this.panLeftFooter.Controls.Add(this.lblProjectDescription);
            this.panLeftFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panLeftFooter.Location = new System.Drawing.Point(0, 329);
            this.panLeftFooter.Name = "panLeftFooter";
            this.panLeftFooter.Size = new System.Drawing.Size(194, 100);
            this.panLeftFooter.TabIndex = 1;
            // 
            // txtbProjectDescription
            // 
            this.txtbProjectDescription.BackColor = System.Drawing.Color.Black;
            this.txtbProjectDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbProjectDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbProjectDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbProjectDescription.ForeColor = System.Drawing.Color.White;
            this.txtbProjectDescription.Location = new System.Drawing.Point(0, 0);
            this.txtbProjectDescription.Multiline = true;
            this.txtbProjectDescription.Name = "txtbProjectDescription";
            this.txtbProjectDescription.Size = new System.Drawing.Size(194, 100);
            this.txtbProjectDescription.TabIndex = 1;
            this.txtbProjectDescription.TextChanged += new System.EventHandler(this.txtbProjectDescription_TextChanged_1);
            // 
            // lblProjectDescription
            // 
            this.lblProjectDescription.AutoSize = true;
            this.lblProjectDescription.Location = new System.Drawing.Point(148, 24);
            this.lblProjectDescription.Name = "lblProjectDescription";
            this.lblProjectDescription.Size = new System.Drawing.Size(35, 13);
            this.lblProjectDescription.TabIndex = 0;
            this.lblProjectDescription.Text = "label1";
            this.lblProjectDescription.Visible = false;
            this.lblProjectDescription.TextChanged += new System.EventHandler(this.lblProjectDescription_TextChanged_1);
            // 
            // panLeftHeader
            // 
            this.panLeftHeader.BackColor = System.Drawing.Color.Black;
            this.panLeftHeader.Controls.Add(this.picProjects);
            this.panLeftHeader.Controls.Add(this.lblInstructions_Projects);
            this.panLeftHeader.Controls.Add(this.butProject_Remove);
            this.panLeftHeader.Controls.Add(this.butProject_New);
            this.panLeftHeader.Controls.Add(this.lblProjects);
            this.panLeftHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panLeftHeader.Location = new System.Drawing.Point(0, 0);
            this.panLeftHeader.Name = "panLeftHeader";
            this.panLeftHeader.Size = new System.Drawing.Size(194, 108);
            this.panLeftHeader.TabIndex = 0;
            // 
            // picProjects
            // 
            this.picProjects.Image = ((System.Drawing.Image)(resources.GetObject("picProjects.Image")));
            this.picProjects.Location = new System.Drawing.Point(11, 4);
            this.picProjects.Name = "picProjects";
            this.picProjects.Size = new System.Drawing.Size(45, 45);
            this.picProjects.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picProjects.TabIndex = 9;
            this.picProjects.TabStop = false;
            this.picProjects.Click += new System.EventHandler(this.picProjects_Click);
            // 
            // lblInstructions_Projects
            // 
            this.lblInstructions_Projects.AutoSize = true;
            this.lblInstructions_Projects.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstructions_Projects.ForeColor = System.Drawing.Color.White;
            this.lblInstructions_Projects.Location = new System.Drawing.Point(11, 45);
            this.lblInstructions_Projects.Name = "lblInstructions_Projects";
            this.lblInstructions_Projects.Size = new System.Drawing.Size(52, 21);
            this.lblInstructions_Projects.TabIndex = 8;
            this.lblInstructions_Projects.Text = "dsfsdf";
            // 
            // butProject_Remove
            // 
            this.butProject_Remove.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.butProject_Remove.Location = new System.Drawing.Point(101, 75);
            this.butProject_Remove.Name = "butProject_Remove";
            this.butProject_Remove.Size = new System.Drawing.Size(85, 27);
            this.butProject_Remove.TabIndex = 7;
            this.butProject_Remove.Text = "Remove";
            this.metroToolTip1.SetToolTip(this.butProject_Remove, "Remove the selected project");
            this.butProject_Remove.UseSelectable = true;
            this.butProject_Remove.Click += new System.EventHandler(this.butProject_Remove_Click);
            // 
            // butProject_New
            // 
            this.butProject_New.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.butProject_New.Location = new System.Drawing.Point(10, 75);
            this.butProject_New.Name = "butProject_New";
            this.butProject_New.Size = new System.Drawing.Size(85, 27);
            this.butProject_New.TabIndex = 6;
            this.butProject_New.Text = "New";
            this.metroToolTip1.SetToolTip(this.butProject_New, "Create a new project");
            this.butProject_New.UseSelectable = true;
            this.butProject_New.Click += new System.EventHandler(this.butProject_New_Click);
            // 
            // lblProjects
            // 
            this.lblProjects.AutoSize = true;
            this.lblProjects.BackColor = System.Drawing.Color.Transparent;
            this.lblProjects.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProjects.ForeColor = System.Drawing.Color.White;
            this.lblProjects.Location = new System.Drawing.Point(52, 10);
            this.lblProjects.Name = "lblProjects";
            this.lblProjects.Size = new System.Drawing.Size(86, 30);
            this.lblProjects.TabIndex = 0;
            this.lblProjects.Text = "Projects";
            this.lblProjects.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblProjects.Click += new System.EventHandler(this.lblProjects_Click);
            // 
            // lstbDocsMulti
            // 
            this.lstbDocsMulti.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstbDocsMulti.CheckOnClick = true;
            this.lstbDocsMulti.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstbDocsMulti.FormattingEnabled = true;
            this.lstbDocsMulti.Location = new System.Drawing.Point(12, 238);
            this.lstbDocsMulti.Name = "lstbDocsMulti";
            this.lstbDocsMulti.Size = new System.Drawing.Size(385, 24);
            this.lstbDocsMulti.Sorted = true;
            this.lstbDocsMulti.TabIndex = 5;
            this.lstbDocsMulti.Visible = false;
            this.lstbDocsMulti.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstbDocsMulti_ItemCheck);
            this.lstbDocsMulti.SelectedIndexChanged += new System.EventHandler(this.lstbDocsMulti_SelectedIndexChanged);
            this.lstbDocsMulti.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lstbDocsMulti_MouseUp);
            // 
            // lstbDocs
            // 
            this.lstbDocs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstbDocs.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstbDocs.FormattingEnabled = true;
            this.lstbDocs.ItemHeight = 21;
            this.lstbDocs.Location = new System.Drawing.Point(0, 108);
            this.lstbDocs.Name = "lstbDocs";
            this.lstbDocs.Size = new System.Drawing.Size(427, 105);
            this.lstbDocs.Sorted = true;
            this.lstbDocs.TabIndex = 4;
            this.lstbDocs.Visible = false;
            this.lstbDocs.SelectedIndexChanged += new System.EventHandler(this.lstbDocs_SelectedIndexChanged);
            // 
            // panRightFooter
            // 
            this.panRightFooter.Controls.Add(this.ucDocDetails1);
            this.panRightFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panRightFooter.Location = new System.Drawing.Point(0, 284);
            this.panRightFooter.Name = "panRightFooter";
            this.panRightFooter.Size = new System.Drawing.Size(599, 145);
            this.panRightFooter.TabIndex = 3;
            // 
            // panRightHeader
            // 
            this.panRightHeader.Controls.Add(this.panDiff);
            this.panRightHeader.Controls.Add(this.panParseType);
            this.panRightHeader.Controls.Add(this.picDocuments);
            this.panRightHeader.Controls.Add(this.lblInstructions_Documents);
            this.panRightHeader.Controls.Add(this.butDoc_Replace);
            this.panRightHeader.Controls.Add(this.butDoc_Edit);
            this.panRightHeader.Controls.Add(this.butDoc_Remove);
            this.panRightHeader.Controls.Add(this.butDoc_New);
            this.panRightHeader.Controls.Add(this.lblDocuments);
            this.panRightHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panRightHeader.Location = new System.Drawing.Point(0, 0);
            this.panRightHeader.Name = "panRightHeader";
            this.panRightHeader.Size = new System.Drawing.Size(599, 108);
            this.panRightHeader.TabIndex = 1;
            // 
            // panParseType
            // 
            this.panParseType.Controls.Add(this.lblParseType);
            this.panParseType.Controls.Add(this.rdbParagraph);
            this.panParseType.Controls.Add(this.rdbLegal);
            this.panParseType.Location = new System.Drawing.Point(178, 11);
            this.panParseType.Name = "panParseType";
            this.panParseType.Size = new System.Drawing.Size(301, 38);
            this.panParseType.TabIndex = 17;
            this.panParseType.Visible = false;
            // 
            // lblParseType
            // 
            this.lblParseType.AutoSize = true;
            this.lblParseType.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParseType.Location = new System.Drawing.Point(6, 11);
            this.lblParseType.Name = "lblParseType";
            this.lblParseType.Size = new System.Drawing.Size(74, 17);
            this.lblParseType.TabIndex = 19;
            this.lblParseType.Text = "Parse Type";
            this.lblParseType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rdbParagraph
            // 
            this.rdbParagraph.AutoSize = true;
            this.rdbParagraph.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbParagraph.Location = new System.Drawing.Point(162, 9);
            this.rdbParagraph.Name = "rdbParagraph";
            this.rdbParagraph.Size = new System.Drawing.Size(87, 21);
            this.rdbParagraph.TabIndex = 18;
            this.rdbParagraph.Text = "Paragraph";
            this.metroToolTip1.SetToolTip(this.rdbParagraph, "Parses by paragraphs");
            this.rdbParagraph.UseVisualStyleBackColor = true;
            // 
            // rdbLegal
            // 
            this.rdbLegal.AutoSize = true;
            this.rdbLegal.Checked = true;
            this.rdbLegal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbLegal.Location = new System.Drawing.Point(90, 9);
            this.rdbLegal.Name = "rdbLegal";
            this.rdbLegal.Size = new System.Drawing.Size(57, 21);
            this.rdbLegal.TabIndex = 17;
            this.rdbLegal.TabStop = true;
            this.rdbLegal.Text = "Legal";
            this.metroToolTip1.SetToolTip(this.rdbLegal, "Parses by numbering");
            this.rdbLegal.UseVisualStyleBackColor = true;
            // 
            // picDocuments
            // 
            this.picDocuments.Image = global::ProfessionalDocAnalyzer.Properties.Resources.appbar_page_multiple;
            this.picDocuments.Location = new System.Drawing.Point(12, 8);
            this.picDocuments.Name = "picDocuments";
            this.picDocuments.Size = new System.Drawing.Size(45, 45);
            this.picDocuments.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDocuments.TabIndex = 15;
            this.picDocuments.TabStop = false;
            // 
            // lblInstructions_Documents
            // 
            this.lblInstructions_Documents.AutoSize = true;
            this.lblInstructions_Documents.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstructions_Documents.ForeColor = System.Drawing.Color.Blue;
            this.lblInstructions_Documents.Location = new System.Drawing.Point(8, 50);
            this.lblInstructions_Documents.Name = "lblInstructions_Documents";
            this.lblInstructions_Documents.Size = new System.Drawing.Size(52, 21);
            this.lblInstructions_Documents.TabIndex = 14;
            this.lblInstructions_Documents.Text = "dsfsdf";
            // 
            // butDoc_Replace
            // 
            this.butDoc_Replace.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.butDoc_Replace.Location = new System.Drawing.Point(285, 75);
            this.butDoc_Replace.Name = "butDoc_Replace";
            this.butDoc_Replace.Size = new System.Drawing.Size(85, 27);
            this.butDoc_Replace.TabIndex = 13;
            this.butDoc_Replace.Text = "Replace";
            this.metroToolTip1.SetToolTip(this.butDoc_Replace, "Replace document with a newer version");
            this.butDoc_Replace.UseSelectable = true;
            this.butDoc_Replace.Click += new System.EventHandler(this.butDoc_Replace_Click);
            // 
            // butDoc_Edit
            // 
            this.butDoc_Edit.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.butDoc_Edit.Location = new System.Drawing.Point(103, 75);
            this.butDoc_Edit.Name = "butDoc_Edit";
            this.butDoc_Edit.Size = new System.Drawing.Size(85, 27);
            this.butDoc_Edit.TabIndex = 12;
            this.butDoc_Edit.Text = "Open";
            this.metroToolTip1.SetToolTip(this.butDoc_Edit, "Clean up doc before analyzing");
            this.butDoc_Edit.UseSelectable = true;
            this.butDoc_Edit.Click += new System.EventHandler(this.butDoc_Edit_Click);
            // 
            // butDoc_Remove
            // 
            this.butDoc_Remove.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.butDoc_Remove.Location = new System.Drawing.Point(194, 75);
            this.butDoc_Remove.Name = "butDoc_Remove";
            this.butDoc_Remove.Size = new System.Drawing.Size(85, 27);
            this.butDoc_Remove.TabIndex = 11;
            this.butDoc_Remove.Text = "Remove";
            this.metroToolTip1.SetToolTip(this.butDoc_Remove, "Remove document");
            this.butDoc_Remove.UseSelectable = true;
            this.butDoc_Remove.Click += new System.EventHandler(this.butDoc_Remove_Click);
            // 
            // butDoc_New
            // 
            this.butDoc_New.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.butDoc_New.Location = new System.Drawing.Point(12, 75);
            this.butDoc_New.Name = "butDoc_New";
            this.butDoc_New.Size = new System.Drawing.Size(85, 27);
            this.butDoc_New.TabIndex = 10;
            this.butDoc_New.Text = "New";
            this.metroToolTip1.SetToolTip(this.butDoc_New, "Import document into prof. Doc. Analyzer");
            this.butDoc_New.UseSelectable = true;
            this.butDoc_New.Click += new System.EventHandler(this.butDoc_New_Click);
            // 
            // lblDocuments
            // 
            this.lblDocuments.AutoSize = true;
            this.lblDocuments.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocuments.Location = new System.Drawing.Point(53, 15);
            this.lblDocuments.Name = "lblDocuments";
            this.lblDocuments.Size = new System.Drawing.Size(119, 30);
            this.lblDocuments.TabIndex = 1;
            this.lblDocuments.Text = "Documents";
            this.lblDocuments.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // panDiff
            // 
            this.panDiff.Controls.Add(this.cboDiff);
            this.panDiff.Controls.Add(this.lblDiff);
            this.panDiff.Location = new System.Drawing.Point(168, 15);
            this.panDiff.Name = "panDiff";
            this.panDiff.Size = new System.Drawing.Size(418, 38);
            this.panDiff.TabIndex = 18;
            this.panDiff.Visible = false;
            // 
            // lblDiff
            // 
            this.lblDiff.AutoSize = true;
            this.lblDiff.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiff.Location = new System.Drawing.Point(7, 8);
            this.lblDiff.Name = "lblDiff";
            this.lblDiff.Size = new System.Drawing.Size(62, 17);
            this.lblDiff.TabIndex = 20;
            this.lblDiff.Text = "Old Doc.";
            this.lblDiff.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboDiff
            // 
            this.cboDiff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDiff.FormattingEnabled = true;
            this.cboDiff.Location = new System.Drawing.Point(75, 8);
            this.cboDiff.Name = "cboDiff";
            this.cboDiff.Size = new System.Drawing.Size(300, 21);
            this.cboDiff.TabIndex = 21;
            // 
            // ucDocDetails1
            // 
            this.ucDocDetails1.AutoSize = true;
            this.ucDocDetails1.BackColor = System.Drawing.Color.Black;
            this.ucDocDetails1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDocDetails1.Location = new System.Drawing.Point(0, 0);
            this.ucDocDetails1.Name = "ucDocDetails1";
            this.ucDocDetails1.Size = new System.Drawing.Size(599, 145);
            this.ucDocDetails1.TabIndex = 3;
            // 
            // ucProjectsDocs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panHeader);
            this.Name = "ucProjectsDocs";
            this.Size = new System.Drawing.Size(797, 468);
            this.Load += new System.EventHandler(this.ucProjectsDocs_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ucProjectsDocs_Paint);
            this.Enter += new System.EventHandler(this.ucProjectsDocs_Enter);
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panLeftFooter.ResumeLayout(false);
            this.panLeftFooter.PerformLayout();
            this.panLeftHeader.ResumeLayout(false);
            this.panLeftHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProjects)).EndInit();
            this.panRightFooter.ResumeLayout(false);
            this.panRightFooter.PerformLayout();
            this.panRightHeader.ResumeLayout(false);
            this.panRightHeader.PerformLayout();
            this.panParseType.ResumeLayout(false);
            this.panParseType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDocuments)).EndInit();
            this.panDiff.ResumeLayout(false);
            this.panDiff.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.Label lblAnalysisName;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lstbProjects;
        private System.Windows.Forms.Panel panLeftFooter;
        private System.Windows.Forms.TextBox txtbProjectDescription;
        private System.Windows.Forms.Label lblProjectDescription;
        private System.Windows.Forms.Panel panLeftHeader;
        private System.Windows.Forms.Label lblInstructions_Projects;
        private MetroFramework.Controls.MetroButton butProject_Remove;
        private MetroFramework.Controls.MetroButton butProject_New;
        private System.Windows.Forms.Label lblProjects;
        private System.Windows.Forms.ListBox lstbDocs;
        private System.Windows.Forms.Panel panRightFooter;
        private ucDocDetails ucDocDetails1;
        private System.Windows.Forms.Panel panRightHeader;
        private System.Windows.Forms.Label lblInstructions_Documents;
        private MetroFramework.Controls.MetroButton butDoc_Replace;
        private MetroFramework.Controls.MetroButton butDoc_Edit;
        private MetroFramework.Controls.MetroButton butDoc_Remove;
        private MetroFramework.Controls.MetroButton butDoc_New;
        private System.Windows.Forms.Label lblDocuments;
        private System.Windows.Forms.PictureBox picProjects;
        private System.Windows.Forms.PictureBox picDocuments;
        private System.Windows.Forms.CheckedListBox lstbDocsMulti;
        private System.Windows.Forms.Panel panParseType;
        private System.Windows.Forms.Label lblParseType;
        private System.Windows.Forms.RadioButton rdbParagraph;
        private System.Windows.Forms.RadioButton rdbLegal;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
        private System.Windows.Forms.Panel panDiff;
        private System.Windows.Forms.Label lblDiff;
        private System.Windows.Forms.ComboBox cboDiff;

    }
}
