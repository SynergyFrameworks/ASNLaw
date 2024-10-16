namespace ProfessionalDocAnalyzer
{
    partial class ucAnalysisResults
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucAnalysisResults));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panHeader = new System.Windows.Forms.Panel();
            this.picDic = new System.Windows.Forms.PictureBox();
            this.lblParseResultsCaption = new System.Windows.Forms.Label();
            this.txtbMessage = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dvgParsedResults = new System.Windows.Forms.DataGridView();
            this.panParseResultsHeader = new System.Windows.Forms.Panel();
            this.butRefresh = new MetroFramework.Controls.MetroButton();
            this.butDelete = new MetroFramework.Controls.MetroButton();
            this.butExport = new MetroFramework.Controls.MetroButton();
            this.butEdit = new MetroFramework.Controls.MetroButton();
            this.butCombine = new MetroFramework.Controls.MetroButton();
            this.butSplit = new MetroFramework.Controls.MetroButton();
            this.chkDocView = new System.Windows.Forms.CheckBox();
            this.panRightHeader = new System.Windows.Forms.Panel();
            this.butExported = new System.Windows.Forms.Button();
            this.butKwDicCon = new System.Windows.Forms.Button();
            this.butSearch = new System.Windows.Forms.Button();
            this.butNotes = new System.Windows.Forms.Button();
            this.richTextBox1 = new Atebion.RTFBox.RichTextBox();
            this.panDA = new System.Windows.Forms.Panel();
            this.panDARight = new System.Windows.Forms.Panel();
            this.lblDANotice = new System.Windows.Forms.Label();
            this.butDeepAnalysis = new MetroFramework.Controls.MetroButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.butSpeacker = new System.Windows.Forms.PictureBox();
            this.lblParsedSec = new System.Windows.Forms.Label();
            this.ucSearch_hoot1 = new ProfessionalDocAnalyzer.ucSearch_hoot();
            this.ucExported1 = new ProfessionalDocAnalyzer.ucExported();
            this.ucKwDicCon1 = new ProfessionalDocAnalyzer.ucKwDicCon();
            this.ucSearch1 = new ProfessionalDocAnalyzer.ucSearch();
            this.ucNotes1 = new ProfessionalDocAnalyzer.ucNotes();
            this.ucDocTypeName1 = new ProfessionalDocAnalyzer.ucDocTypeName();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.panHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgParsedResults)).BeginInit();
            this.panParseResultsHeader.SuspendLayout();
            this.panRightHeader.SuspendLayout();
            this.panDA.SuspendLayout();
            this.panDARight.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.butSpeacker)).BeginInit();
            this.SuspendLayout();
            // 
            // panHeader
            // 
            this.panHeader.BackColor = System.Drawing.Color.Black;
            this.panHeader.Controls.Add(this.ucDocTypeName1);
            this.panHeader.Controls.Add(this.picDic);
            this.panHeader.Controls.Add(this.lblParseResultsCaption);
            this.panHeader.Controls.Add(this.txtbMessage);
            this.panHeader.Controls.Add(this.lblMessage);
            this.panHeader.Controls.Add(this.lblHeader);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.ForeColor = System.Drawing.Color.White;
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(954, 35);
            this.panHeader.TabIndex = 5;
            // 
            // picDic
            // 
            this.picDic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picDic.Image = ((System.Drawing.Image)(resources.GetObject("picDic.Image")));
            this.picDic.Location = new System.Drawing.Point(3, 2);
            this.picDic.Name = "picDic";
            this.picDic.Size = new System.Drawing.Size(32, 32);
            this.picDic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDic.TabIndex = 20;
            this.picDic.TabStop = false;
            // 
            // lblParseResultsCaption
            // 
            this.lblParseResultsCaption.AutoSize = true;
            this.lblParseResultsCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblParseResultsCaption.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParseResultsCaption.ForeColor = System.Drawing.Color.White;
            this.lblParseResultsCaption.Location = new System.Drawing.Point(157, 8);
            this.lblParseResultsCaption.Name = "lblParseResultsCaption";
            this.lblParseResultsCaption.Size = new System.Drawing.Size(118, 17);
            this.lblParseResultsCaption.TabIndex = 19;
            this.lblParseResultsCaption.Text = "- Parsed Segments";
            // 
            // txtbMessage
            // 
            this.txtbMessage.BackColor = System.Drawing.Color.Black;
            this.txtbMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbMessage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbMessage.ForeColor = System.Drawing.Color.White;
            this.txtbMessage.Location = new System.Drawing.Point(317, 10);
            this.txtbMessage.Margin = new System.Windows.Forms.Padding(30, 3, 30, 3);
            this.txtbMessage.Multiline = true;
            this.txtbMessage.Name = "txtbMessage";
            this.txtbMessage.Size = new System.Drawing.Size(591, 18);
            this.txtbMessage.TabIndex = 18;
            this.txtbMessage.TextChanged += new System.EventHandler(this.txtbMessage_TextChanged);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(222, 6);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 17;
            this.lblMessage.Visible = false;
            this.lblMessage.TextChanged += new System.EventHandler(this.lblMessage_TextChanged);
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(37, 6);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(121, 21);
            this.lblHeader.TabIndex = 15;
            this.lblHeader.Text = "Analysis Results";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 35);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(30, 3, 30, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.richTextBox1);
            this.splitContainer1.Panel2.Controls.Add(this.panDA);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(954, 547);
            this.splitContainer1.SplitterDistance = 343;
            this.splitContainer1.TabIndex = 6;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dvgParsedResults);
            this.splitContainer2.Panel1.Controls.Add(this.panParseResultsHeader);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ucSearch_hoot1);
            this.splitContainer2.Panel2.Controls.Add(this.ucExported1);
            this.splitContainer2.Panel2.Controls.Add(this.ucKwDicCon1);
            this.splitContainer2.Panel2.Controls.Add(this.ucSearch1);
            this.splitContainer2.Panel2.Controls.Add(this.ucNotes1);
            this.splitContainer2.Panel2.Controls.Add(this.panRightHeader);
            this.splitContainer2.Size = new System.Drawing.Size(954, 343);
            this.splitContainer2.SplitterDistance = 571;
            this.splitContainer2.TabIndex = 0;
            // 
            // dvgParsedResults
            // 
            this.dvgParsedResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dvgParsedResults.BackgroundColor = System.Drawing.Color.White;
            this.dvgParsedResults.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvgParsedResults.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dvgParsedResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgParsedResults.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dvgParsedResults.DefaultCellStyle = dataGridViewCellStyle2;
            this.dvgParsedResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvgParsedResults.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dvgParsedResults.Location = new System.Drawing.Point(0, 39);
            this.dvgParsedResults.MultiSelect = false;
            this.dvgParsedResults.Name = "dvgParsedResults";
            this.dvgParsedResults.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvgParsedResults.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dvgParsedResults.RowHeadersWidth = 5;
            this.dvgParsedResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dvgParsedResults.Size = new System.Drawing.Size(571, 304);
            this.dvgParsedResults.TabIndex = 14;
            this.dvgParsedResults.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dvgParsedResults_CellFormatting);
            this.dvgParsedResults.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dvgParsedResults_CellPainting);
            this.dvgParsedResults.SelectionChanged += new System.EventHandler(this.dvgParsedResults_SelectionChanged);
            this.dvgParsedResults.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dvgParsedResults_KeyUp);
            // 
            // panParseResultsHeader
            // 
            this.panParseResultsHeader.BackColor = System.Drawing.Color.PowderBlue;
            this.panParseResultsHeader.Controls.Add(this.butRefresh);
            this.panParseResultsHeader.Controls.Add(this.butDelete);
            this.panParseResultsHeader.Controls.Add(this.butExport);
            this.panParseResultsHeader.Controls.Add(this.butEdit);
            this.panParseResultsHeader.Controls.Add(this.butCombine);
            this.panParseResultsHeader.Controls.Add(this.butSplit);
            this.panParseResultsHeader.Controls.Add(this.chkDocView);
            this.panParseResultsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panParseResultsHeader.Location = new System.Drawing.Point(0, 0);
            this.panParseResultsHeader.Name = "panParseResultsHeader";
            this.panParseResultsHeader.Size = new System.Drawing.Size(571, 39);
            this.panParseResultsHeader.TabIndex = 1;
            // 
            // butRefresh
            // 
            this.butRefresh.Location = new System.Drawing.Point(533, 10);
            this.butRefresh.Name = "butRefresh";
            this.butRefresh.Size = new System.Drawing.Size(75, 23);
            this.butRefresh.TabIndex = 95;
            this.butRefresh.Text = "Refresh";
            this.butRefresh.UseSelectable = true;
            this.butRefresh.Visible = false;
            this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
            // 
            // butDelete
            // 
            this.butDelete.Location = new System.Drawing.Point(452, 10);
            this.butDelete.Name = "butDelete";
            this.butDelete.Size = new System.Drawing.Size(75, 23);
            this.butDelete.TabIndex = 94;
            this.butDelete.Text = "Delete";
            this.butDelete.UseSelectable = true;
            this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
            // 
            // butExport
            // 
            this.butExport.Location = new System.Drawing.Point(371, 10);
            this.butExport.Name = "butExport";
            this.butExport.Size = new System.Drawing.Size(75, 23);
            this.butExport.TabIndex = 93;
            this.butExport.Text = "Export";
            this.butExport.UseSelectable = true;
            this.butExport.Click += new System.EventHandler(this.butExport_Click);
            // 
            // butEdit
            // 
            this.butEdit.Location = new System.Drawing.Point(290, 10);
            this.butEdit.Name = "butEdit";
            this.butEdit.Size = new System.Drawing.Size(75, 23);
            this.butEdit.TabIndex = 92;
            this.butEdit.Text = "Edit";
            this.butEdit.UseSelectable = true;
            this.butEdit.Click += new System.EventHandler(this.butEdit_Click);
            // 
            // butCombine
            // 
            this.butCombine.Location = new System.Drawing.Point(209, 10);
            this.butCombine.Name = "butCombine";
            this.butCombine.Size = new System.Drawing.Size(75, 23);
            this.butCombine.TabIndex = 91;
            this.butCombine.Text = "Combine";
            this.butCombine.UseSelectable = true;
            this.butCombine.Click += new System.EventHandler(this.butCombine_Click);
            // 
            // butSplit
            // 
            this.butSplit.Location = new System.Drawing.Point(128, 10);
            this.butSplit.Name = "butSplit";
            this.butSplit.Size = new System.Drawing.Size(75, 23);
            this.butSplit.TabIndex = 90;
            this.butSplit.Text = "Split";
            this.butSplit.UseSelectable = true;
            this.butSplit.Click += new System.EventHandler(this.butSplit_Click);
            // 
            // chkDocView
            // 
            this.chkDocView.AutoSize = true;
            this.chkDocView.BackColor = System.Drawing.Color.Transparent;
            this.chkDocView.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDocView.ForeColor = System.Drawing.Color.Black;
            this.chkDocView.Location = new System.Drawing.Point(8, 14);
            this.chkDocView.Name = "chkDocView";
            this.chkDocView.Size = new System.Drawing.Size(119, 19);
            this.chkDocView.TabIndex = 28;
            this.chkDocView.Text = "Show Document";
            this.chkDocView.UseVisualStyleBackColor = false;
            this.chkDocView.CheckedChanged += new System.EventHandler(this.chkDocView_CheckedChanged);
            // 
            // panRightHeader
            // 
            this.panRightHeader.BackColor = System.Drawing.Color.PowderBlue;
            this.panRightHeader.Controls.Add(this.butExported);
            this.panRightHeader.Controls.Add(this.butKwDicCon);
            this.panRightHeader.Controls.Add(this.butSearch);
            this.panRightHeader.Controls.Add(this.butNotes);
            this.panRightHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panRightHeader.Location = new System.Drawing.Point(0, 0);
            this.panRightHeader.Name = "panRightHeader";
            this.panRightHeader.Size = new System.Drawing.Size(379, 39);
            this.panRightHeader.TabIndex = 0;
            // 
            // butExported
            // 
            this.butExported.BackColor = System.Drawing.Color.WhiteSmoke;
            this.butExported.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.butExported.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butExported.Location = new System.Drawing.Point(258, 10);
            this.butExported.Name = "butExported";
            this.butExported.Size = new System.Drawing.Size(75, 23);
            this.butExported.TabIndex = 3;
            this.butExported.Text = "Exported";
            this.butExported.UseVisualStyleBackColor = false;
            this.butExported.Click += new System.EventHandler(this.butExported_Click);
            this.butExported.MouseEnter += new System.EventHandler(this.butExported_MouseEnter);
            this.butExported.MouseLeave += new System.EventHandler(this.butExported_MouseLeave);
            // 
            // butKwDicCon
            // 
            this.butKwDicCon.BackColor = System.Drawing.Color.WhiteSmoke;
            this.butKwDicCon.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.butKwDicCon.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butKwDicCon.Location = new System.Drawing.Point(177, 10);
            this.butKwDicCon.Name = "butKwDicCon";
            this.butKwDicCon.Size = new System.Drawing.Size(75, 23);
            this.butKwDicCon.TabIndex = 2;
            this.butKwDicCon.Text = "Keywords";
            this.butKwDicCon.UseVisualStyleBackColor = false;
            this.butKwDicCon.Click += new System.EventHandler(this.butKwDicCon_Click);
            this.butKwDicCon.MouseEnter += new System.EventHandler(this.butKwDicCon_MouseEnter);
            this.butKwDicCon.MouseLeave += new System.EventHandler(this.butKwDicCon_MouseLeave);
            // 
            // butSearch
            // 
            this.butSearch.BackColor = System.Drawing.Color.WhiteSmoke;
            this.butSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.butSearch.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butSearch.Location = new System.Drawing.Point(96, 10);
            this.butSearch.Name = "butSearch";
            this.butSearch.Size = new System.Drawing.Size(75, 23);
            this.butSearch.TabIndex = 1;
            this.butSearch.Text = "Search";
            this.butSearch.UseVisualStyleBackColor = false;
            this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
            this.butSearch.MouseEnter += new System.EventHandler(this.butSearch_MouseEnter);
            this.butSearch.MouseLeave += new System.EventHandler(this.butSearch_MouseLeave);
            // 
            // butNotes
            // 
            this.butNotes.BackColor = System.Drawing.Color.Teal;
            this.butNotes.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.butNotes.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butNotes.ForeColor = System.Drawing.Color.White;
            this.butNotes.Location = new System.Drawing.Point(15, 10);
            this.butNotes.Name = "butNotes";
            this.butNotes.Size = new System.Drawing.Size(75, 23);
            this.butNotes.TabIndex = 0;
            this.butNotes.Text = "Notes";
            this.butNotes.UseVisualStyleBackColor = false;
            this.butNotes.Click += new System.EventHandler(this.butNotes_Click);
            this.butNotes.MouseEnter += new System.EventHandler(this.butNotes_MouseEnter);
            this.butNotes.MouseLeave += new System.EventHandler(this.butNotes_MouseLeave);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 32);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(954, 131);
            this.richTextBox1.TabIndex = 17;
            this.richTextBox1.Text = "";
            // 
            // panDA
            // 
            this.panDA.BackColor = System.Drawing.Color.White;
            this.panDA.Controls.Add(this.panDARight);
            this.panDA.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panDA.Location = new System.Drawing.Point(0, 163);
            this.panDA.Name = "panDA";
            this.panDA.Size = new System.Drawing.Size(954, 37);
            this.panDA.TabIndex = 16;
            this.panDA.Visible = false;
            // 
            // panDARight
            // 
            this.panDARight.Controls.Add(this.lblDANotice);
            this.panDARight.Controls.Add(this.butDeepAnalysis);
            this.panDARight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panDARight.Location = new System.Drawing.Point(177, 0);
            this.panDARight.Name = "panDARight";
            this.panDARight.Size = new System.Drawing.Size(777, 37);
            this.panDARight.TabIndex = 0;
            // 
            // lblDANotice
            // 
            this.lblDANotice.AutoSize = true;
            this.lblDANotice.BackColor = System.Drawing.Color.Transparent;
            this.lblDANotice.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDANotice.ForeColor = System.Drawing.Color.Red;
            this.lblDANotice.Location = new System.Drawing.Point(30, 10);
            this.lblDANotice.Name = "lblDANotice";
            this.lblDANotice.Size = new System.Drawing.Size(565, 17);
            this.lblDANotice.TabIndex = 92;
            this.lblDANotice.Text = "Segments are newer than sentences in the Deep Analysis. Suggest, rerunning the De" +
    "ep Analysis.";
            this.lblDANotice.Visible = false;
            // 
            // butDeepAnalysis
            // 
            this.butDeepAnalysis.BackColor = System.Drawing.Color.DarkGreen;
            this.butDeepAnalysis.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.butDeepAnalysis.ForeColor = System.Drawing.Color.White;
            this.butDeepAnalysis.Location = new System.Drawing.Point(606, 6);
            this.butDeepAnalysis.Name = "butDeepAnalysis";
            this.butDeepAnalysis.Size = new System.Drawing.Size(151, 23);
            this.butDeepAnalysis.TabIndex = 91;
            this.butDeepAnalysis.Text = "Run Deep Analysis";
            this.butDeepAnalysis.UseSelectable = true;
            this.butDeepAnalysis.Click += new System.EventHandler(this.butDeepAnalysis_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.PowderBlue;
            this.panel1.Controls.Add(this.butSpeacker);
            this.panel1.Controls.Add(this.lblParsedSec);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(954, 32);
            this.panel1.TabIndex = 2;
            // 
            // butSpeacker
            // 
            this.butSpeacker.BackColor = System.Drawing.Color.Black;
            this.butSpeacker.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butSpeacker.Image = ((System.Drawing.Image)(resources.GetObject("butSpeacker.Image")));
            this.butSpeacker.Location = new System.Drawing.Point(155, 2);
            this.butSpeacker.Name = "butSpeacker";
            this.butSpeacker.Size = new System.Drawing.Size(28, 28);
            this.butSpeacker.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.butSpeacker.TabIndex = 20;
            this.butSpeacker.TabStop = false;
            this.butSpeacker.Visible = false;
            // 
            // lblParsedSec
            // 
            this.lblParsedSec.AutoSize = true;
            this.lblParsedSec.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParsedSec.ForeColor = System.Drawing.Color.Black;
            this.lblParsedSec.Location = new System.Drawing.Point(15, 5);
            this.lblParsedSec.Name = "lblParsedSec";
            this.lblParsedSec.Size = new System.Drawing.Size(134, 21);
            this.lblParsedSec.TabIndex = 19;
            this.lblParsedSec.Text = "Selected Segment";
            // 
            // ucSearch_hoot1
            // 
            this.ucSearch_hoot1.BackColor = System.Drawing.Color.White;
            this.ucSearch_hoot1.Location = new System.Drawing.Point(320, 71);
            this.ucSearch_hoot1.Name = "ucSearch_hoot1";
            this.ucSearch_hoot1.Size = new System.Drawing.Size(753, 451);
            this.ucSearch_hoot1.TabIndex = 5;
            this.ucSearch_hoot1.Visible = false;
            this.ucSearch_hoot1.SearchCompleted += new ProfessionalDocAnalyzer.ucSearch_hoot.ProcessHandler(this.ucSearch_hoot1_SearchCompleted);
            // 
            // ucExported1
            // 
            this.ucExported1.BackColor = System.Drawing.Color.White;
            this.ucExported1.Location = new System.Drawing.Point(284, 113);
            this.ucExported1.Name = "ucExported1";
            this.ucExported1.Size = new System.Drawing.Size(843, 336);
            this.ucExported1.TabIndex = 4;
            this.ucExported1.Visible = false;
            // 
            // ucKwDicCon1
            // 
            this.ucKwDicCon1.Location = new System.Drawing.Point(193, 148);
            this.ucKwDicCon1.Name = "ucKwDicCon1";
            this.ucKwDicCon1.Size = new System.Drawing.Size(754, 438);
            this.ucKwDicCon1.TabIndex = 3;
            this.ucKwDicCon1.Visible = false;
            this.ucKwDicCon1.FilterCompleted += new ProfessionalDocAnalyzer.ucKwDicCon.ProcessHandler(this.ucKwDicCon1_FilterCompleted);
            // 
            // ucSearch1
            // 
            this.ucSearch1.BackColor = System.Drawing.Color.White;
            this.ucSearch1.Location = new System.Drawing.Point(96, 193);
            this.ucSearch1.Name = "ucSearch1";
            this.ucSearch1.Size = new System.Drawing.Size(581, 329);
            this.ucSearch1.TabIndex = 2;
            this.ucSearch1.Visible = false;
            this.ucSearch1.SearchCompleted += new ProfessionalDocAnalyzer.ucSearch.ProcessHandler(this.ucSearch1_SearchCompleted);
            // 
            // ucNotes1
            // 
            this.ucNotes1.Location = new System.Drawing.Point(15, 223);
            this.ucNotes1.Name = "ucNotes1";
            this.ucNotes1.Size = new System.Drawing.Size(1029, 491);
            this.ucNotes1.TabIndex = 1;
            this.ucNotes1.UID = "";
            this.ucNotes1.Visible = false;
            // 
            // ucDocTypeName1
            // 
            this.ucDocTypeName1.BackColor = System.Drawing.Color.Black;
            this.ucDocTypeName1.Location = new System.Drawing.Point(317, 4);
            this.ucDocTypeName1.Name = "ucDocTypeName1";
            this.ucDocTypeName1.Size = new System.Drawing.Size(626, 27);
            this.ucDocTypeName1.TabIndex = 7;
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // ucAnalysisResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panHeader);
            this.Name = "ucAnalysisResults";
            this.Size = new System.Drawing.Size(954, 582);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ucAnalysisResults_Paint);
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDic)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dvgParsedResults)).EndInit();
            this.panParseResultsHeader.ResumeLayout(false);
            this.panParseResultsHeader.PerformLayout();
            this.panRightHeader.ResumeLayout(false);
            this.panDA.ResumeLayout(false);
            this.panDARight.ResumeLayout(false);
            this.panDARight.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.butSpeacker)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.TextBox txtbMessage;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panParseResultsHeader;
        private System.Windows.Forms.CheckBox chkDocView;
        private MetroFramework.Controls.MetroButton butCombine;
        private MetroFramework.Controls.MetroButton butSplit;
        private System.Windows.Forms.Label lblParseResultsCaption;
        private MetroFramework.Controls.MetroButton butRefresh;
        private MetroFramework.Controls.MetroButton butDelete;
        private MetroFramework.Controls.MetroButton butExport;
        private MetroFramework.Controls.MetroButton butEdit;
        private System.Windows.Forms.DataGridView dvgParsedResults;
        private System.Windows.Forms.Panel panRightHeader;
        private System.Windows.Forms.Button butExported;
        private System.Windows.Forms.Button butKwDicCon;
        private System.Windows.Forms.Button butSearch;
        private System.Windows.Forms.Button butNotes;
        private System.Windows.Forms.Panel panel1;
        private ucExported ucExported1;
        private ucKwDicCon ucKwDicCon1;
        private ucSearch ucSearch1;
        private ucNotes ucNotes1;
        private System.Windows.Forms.PictureBox butSpeacker;
        private System.Windows.Forms.Label lblParsedSec;
        private System.Windows.Forms.PictureBox picDic;
        private ucDocTypeName ucDocTypeName1;
        private System.Windows.Forms.Panel panDA;
        private System.Windows.Forms.Panel panDARight;
        private MetroFramework.Controls.MetroButton butDeepAnalysis;
        private Atebion.RTFBox.RichTextBox richTextBox1;
        private ucSearch_hoot ucSearch_hoot1;
        private System.Windows.Forms.Label lblDANotice;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
    }
}
