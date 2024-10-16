namespace ProfessionalDocAnalyzer
{
    partial class ucDeepAnalyticsResults
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDeepAnalyticsResults));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panHeader = new System.Windows.Forms.Panel();
            this.lblParseResultsCaption = new System.Windows.Forms.Label();
            this.ucDeepAnalyticsFilterDisplay1 = new ProfessionalDocAnalyzer.ucDeepAnalyticsFilterDisplay();
            this.lblQuality = new System.Windows.Forms.TextBox();
            this.txtbMessage = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.picHeader = new System.Windows.Forms.PictureBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dvgParsedResults = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.butRefresh = new MetroFramework.Controls.MetroButton();
            this.butExport = new MetroFramework.Controls.MetroButton();
            this.butDelete = new MetroFramework.Controls.MetroButton();
            this.chkDocView = new System.Windows.Forms.CheckBox();
            this.butExcel = new System.Windows.Forms.PictureBox();
            this.ucExported1 = new ProfessionalDocAnalyzer.ucExported();
            this.ucSearch_hoot1 = new ProfessionalDocAnalyzer.ucSearch_hoot();
            this.ucSearch1 = new ProfessionalDocAnalyzer.ucSearch();
            this.ucDeepAnalyticsNotes1 = new ProfessionalDocAnalyzer.ucDeepAnalyticsNotes();
            this.ucDeepAnalyticsKeywords1 = new ProfessionalDocAnalyzer.ucDeepAnalyticsKeywords();
            this.panRightTopHeader = new System.Windows.Forms.Panel();
            this.butExported = new System.Windows.Forms.Button();
            this.butKwDicCon = new System.Windows.Forms.Button();
            this.butSearch = new System.Windows.Forms.Button();
            this.butNotes = new System.Windows.Forms.Button();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.butSpeacker = new System.Windows.Forms.PictureBox();
            this.lblParsedSec = new System.Windows.Forms.Label();
            this.richTextBox1 = new Atebion.RTFBox.RichTextBox();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.panHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgParsedResults)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.butExcel)).BeginInit();
            this.panRightTopHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.butSpeacker)).BeginInit();
            this.SuspendLayout();
            // 
            // panHeader
            // 
            this.panHeader.BackColor = System.Drawing.Color.Black;
            this.panHeader.Controls.Add(this.lblParseResultsCaption);
            this.panHeader.Controls.Add(this.ucDeepAnalyticsFilterDisplay1);
            this.panHeader.Controls.Add(this.lblQuality);
            this.panHeader.Controls.Add(this.txtbMessage);
            this.panHeader.Controls.Add(this.lblMessage);
            this.panHeader.Controls.Add(this.picHeader);
            this.panHeader.Controls.Add(this.lblHeader);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(1270, 35);
            this.panHeader.TabIndex = 20;
            // 
            // lblParseResultsCaption
            // 
            this.lblParseResultsCaption.AutoSize = true;
            this.lblParseResultsCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblParseResultsCaption.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParseResultsCaption.ForeColor = System.Drawing.Color.White;
            this.lblParseResultsCaption.Location = new System.Drawing.Point(219, 11);
            this.lblParseResultsCaption.Name = "lblParseResultsCaption";
            this.lblParseResultsCaption.Size = new System.Drawing.Size(119, 17);
            this.lblParseResultsCaption.TabIndex = 21;
            this.lblParseResultsCaption.Text = "- Parsed Sentences";
            // 
            // ucDeepAnalyticsFilterDisplay1
            // 
            this.ucDeepAnalyticsFilterDisplay1.BackColor = System.Drawing.Color.Black;
            this.ucDeepAnalyticsFilterDisplay1.Count = 0;
            this.ucDeepAnalyticsFilterDisplay1.CurrentMode = ProfessionalDocAnalyzer.ucDeepAnalyticsFilterDisplay.Modes.All;
            this.ucDeepAnalyticsFilterDisplay1.Location = new System.Drawing.Point(364, 3);
            this.ucDeepAnalyticsFilterDisplay1.Name = "ucDeepAnalyticsFilterDisplay1";
            this.ucDeepAnalyticsFilterDisplay1.Size = new System.Drawing.Size(350, 36);
            this.ucDeepAnalyticsFilterDisplay1.TabIndex = 20;
            this.ucDeepAnalyticsFilterDisplay1.Total = 0;
            // 
            // lblQuality
            // 
            this.lblQuality.BackColor = System.Drawing.Color.Black;
            this.lblQuality.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblQuality.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuality.ForeColor = System.Drawing.Color.White;
            this.lblQuality.Location = new System.Drawing.Point(720, 5);
            this.lblQuality.Multiline = true;
            this.lblQuality.Name = "lblQuality";
            this.lblQuality.Size = new System.Drawing.Size(288, 18);
            this.lblQuality.TabIndex = 19;
            // 
            // txtbMessage
            // 
            this.txtbMessage.BackColor = System.Drawing.Color.Black;
            this.txtbMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbMessage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbMessage.ForeColor = System.Drawing.Color.White;
            this.txtbMessage.Location = new System.Drawing.Point(518, 18);
            this.txtbMessage.Multiline = true;
            this.txtbMessage.Name = "txtbMessage";
            this.txtbMessage.Size = new System.Drawing.Size(295, 18);
            this.txtbMessage.TabIndex = 18;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(222, 6);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 17;
            this.lblMessage.Visible = false;
            // 
            // picHeader
            // 
            this.picHeader.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picHeader.Image = ((System.Drawing.Image)(resources.GetObject("picHeader.Image")));
            this.picHeader.Location = new System.Drawing.Point(9, 3);
            this.picHeader.Name = "picHeader";
            this.picHeader.Size = new System.Drawing.Size(32, 32);
            this.picHeader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHeader.TabIndex = 16;
            this.picHeader.TabStop = false;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(56, 8);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(161, 21);
            this.lblHeader.TabIndex = 15;
            this.lblHeader.Text = "Deep Analysis Results";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 35);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(1270, 476);
            this.splitContainer1.SplitterDistance = 234;
            this.splitContainer1.TabIndex = 21;
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
            this.splitContainer2.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ucExported1);
            this.splitContainer2.Panel2.Controls.Add(this.ucSearch_hoot1);
            this.splitContainer2.Panel2.Controls.Add(this.ucSearch1);
            this.splitContainer2.Panel2.Controls.Add(this.ucDeepAnalyticsNotes1);
            this.splitContainer2.Panel2.Controls.Add(this.ucDeepAnalyticsKeywords1);
            this.splitContainer2.Panel2.Controls.Add(this.panRightTopHeader);
            this.splitContainer2.Size = new System.Drawing.Size(1270, 234);
            this.splitContainer2.SplitterDistance = 622;
            this.splitContainer2.TabIndex = 0;
            // 
            // dvgParsedResults
            // 
            this.dvgParsedResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dvgParsedResults.BackgroundColor = System.Drawing.Color.Black;
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
            this.dvgParsedResults.Size = new System.Drawing.Size(622, 195);
            this.dvgParsedResults.TabIndex = 15;
            this.dvgParsedResults.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dvgParsedResults_CellFormatting);
            this.dvgParsedResults.SelectionChanged += new System.EventHandler(this.dvgParsedResults_SelectionChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.PowderBlue;
            this.panel2.Controls.Add(this.butRefresh);
            this.panel2.Controls.Add(this.butExport);
            this.panel2.Controls.Add(this.butDelete);
            this.panel2.Controls.Add(this.chkDocView);
            this.panel2.Controls.Add(this.butExcel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(622, 39);
            this.panel2.TabIndex = 2;
            // 
            // butRefresh
            // 
            this.butRefresh.Location = new System.Drawing.Point(235, 8);
            this.butRefresh.Name = "butRefresh";
            this.butRefresh.Size = new System.Drawing.Size(75, 23);
            this.butRefresh.TabIndex = 97;
            this.butRefresh.Text = "Refresh";
            this.butRefresh.UseSelectable = true;
            this.butRefresh.Visible = false;
            this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
            // 
            // butExport
            // 
            this.butExport.Location = new System.Drawing.Point(142, 8);
            this.butExport.Name = "butExport";
            this.butExport.Size = new System.Drawing.Size(75, 23);
            this.butExport.TabIndex = 96;
            this.butExport.Text = "Export";
            this.butExport.UseSelectable = true;
            this.butExport.Click += new System.EventHandler(this.butExport_Click);
            // 
            // butDelete
            // 
            this.butDelete.Location = new System.Drawing.Point(331, 8);
            this.butDelete.Name = "butDelete";
            this.butDelete.Size = new System.Drawing.Size(75, 23);
            this.butDelete.TabIndex = 95;
            this.butDelete.Text = "Delete";
            this.butDelete.UseSelectable = true;
            this.butDelete.Click += new System.EventHandler(this.picDelete_Click);
            // 
            // chkDocView
            // 
            this.chkDocView.AutoSize = true;
            this.chkDocView.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDocView.ForeColor = System.Drawing.Color.Black;
            this.chkDocView.Location = new System.Drawing.Point(9, 10);
            this.chkDocView.Name = "chkDocView";
            this.chkDocView.Size = new System.Drawing.Size(114, 19);
            this.chkDocView.TabIndex = 29;
            this.chkDocView.Text = "Show Document";
            this.chkDocView.UseVisualStyleBackColor = true;
            this.chkDocView.Visible = false;
            this.chkDocView.CheckedChanged += new System.EventHandler(this.chkDocView_CheckedChanged);
            // 
            // butExcel
            // 
            this.butExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butExcel.Image = ((System.Drawing.Image)(resources.GetObject("butExcel.Image")));
            this.butExcel.Location = new System.Drawing.Point(364, 6);
            this.butExcel.Name = "butExcel";
            this.butExcel.Size = new System.Drawing.Size(32, 32);
            this.butExcel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.butExcel.TabIndex = 26;
            this.butExcel.TabStop = false;
            this.butExcel.Visible = false;
            // 
            // ucExported1
            // 
            this.ucExported1.BackColor = System.Drawing.Color.White;
            this.ucExported1.Location = new System.Drawing.Point(24, 146);
            this.ucExported1.Name = "ucExported1";
            this.ucExported1.Size = new System.Drawing.Size(601, 336);
            this.ucExported1.TabIndex = 11;
            this.ucExported1.Visible = false;
            // 
            // ucSearch_hoot1
            // 
            this.ucSearch_hoot1.BackColor = System.Drawing.Color.White;
            this.ucSearch_hoot1.Location = new System.Drawing.Point(59, 62);
            this.ucSearch_hoot1.Name = "ucSearch_hoot1";
            this.ucSearch_hoot1.Size = new System.Drawing.Size(753, 451);
            this.ucSearch_hoot1.TabIndex = 10;
            this.ucSearch_hoot1.Visible = false;
            this.ucSearch_hoot1.SearchCompleted += new ProfessionalDocAnalyzer.ucSearch_hoot.ProcessHandler(this.ucSearch_hoot1_SearchCompleted);
            // 
            // ucSearch1
            // 
            this.ucSearch1.BackColor = System.Drawing.Color.White;
            this.ucSearch1.Location = new System.Drawing.Point(94, 98);
            this.ucSearch1.Name = "ucSearch1";
            this.ucSearch1.Size = new System.Drawing.Size(483, 329);
            this.ucSearch1.TabIndex = 9;
            this.ucSearch1.Visible = false;
            this.ucSearch1.SearchCompleted += new ProfessionalDocAnalyzer.ucSearch.ProcessHandler(this.ucSearch1_SearchCompleted);
            // 
            // ucDeepAnalyticsNotes1
            // 
            this.ucDeepAnalyticsNotes1.Location = new System.Drawing.Point(366, 62);
            this.ucDeepAnalyticsNotes1.Name = "ucDeepAnalyticsNotes1";
            this.ucDeepAnalyticsNotes1.Path = "";
            this.ucDeepAnalyticsNotes1.Size = new System.Drawing.Size(1029, 491);
            this.ucDeepAnalyticsNotes1.TabIndex = 6;
            this.ucDeepAnalyticsNotes1.UID = "";
            this.ucDeepAnalyticsNotes1.Visible = false;
            // 
            // ucDeepAnalyticsKeywords1
            // 
            this.ucDeepAnalyticsKeywords1.Location = new System.Drawing.Point(151, 163);
            this.ucDeepAnalyticsKeywords1.Name = "ucDeepAnalyticsKeywords1";
            this.ucDeepAnalyticsKeywords1.Size = new System.Drawing.Size(793, 390);
            this.ucDeepAnalyticsKeywords1.TabIndex = 3;
            this.ucDeepAnalyticsKeywords1.Visible = false;
            this.ucDeepAnalyticsKeywords1.FilterCompleted += new ProfessionalDocAnalyzer.ucDeepAnalyticsKeywords.ProcessHandler(this.ucDeepAnalyticsKeywords1_FilterCompleted);
            // 
            // panRightTopHeader
            // 
            this.panRightTopHeader.BackColor = System.Drawing.Color.PowderBlue;
            this.panRightTopHeader.Controls.Add(this.butExported);
            this.panRightTopHeader.Controls.Add(this.butKwDicCon);
            this.panRightTopHeader.Controls.Add(this.butSearch);
            this.panRightTopHeader.Controls.Add(this.butNotes);
            this.panRightTopHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panRightTopHeader.Location = new System.Drawing.Point(0, 0);
            this.panRightTopHeader.Name = "panRightTopHeader";
            this.panRightTopHeader.Size = new System.Drawing.Size(644, 39);
            this.panRightTopHeader.TabIndex = 8;
            // 
            // butExported
            // 
            this.butExported.BackColor = System.Drawing.Color.WhiteSmoke;
            this.butExported.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.butExported.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butExported.Location = new System.Drawing.Point(258, 8);
            this.butExported.Name = "butExported";
            this.butExported.Size = new System.Drawing.Size(75, 23);
            this.butExported.TabIndex = 7;
            this.butExported.Text = "Exported";
            this.butExported.UseVisualStyleBackColor = false;
            this.butExported.Click += new System.EventHandler(this.clExported_Click);
            this.butExported.MouseEnter += new System.EventHandler(this.butExported_MouseEnter);
            this.butExported.MouseLeave += new System.EventHandler(this.butExported_MouseLeave);
            // 
            // butKwDicCon
            // 
            this.butKwDicCon.BackColor = System.Drawing.Color.WhiteSmoke;
            this.butKwDicCon.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.butKwDicCon.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butKwDicCon.Location = new System.Drawing.Point(177, 8);
            this.butKwDicCon.Name = "butKwDicCon";
            this.butKwDicCon.Size = new System.Drawing.Size(75, 23);
            this.butKwDicCon.TabIndex = 6;
            this.butKwDicCon.Text = "Keywords";
            this.butKwDicCon.UseVisualStyleBackColor = false;
            this.butKwDicCon.Click += new System.EventHandler(this.clKeywords_Click);
            this.butKwDicCon.MouseEnter += new System.EventHandler(this.butKwDicCon_MouseEnter);
            this.butKwDicCon.MouseLeave += new System.EventHandler(this.butKwDicCon_MouseLeave);
            // 
            // butSearch
            // 
            this.butSearch.BackColor = System.Drawing.Color.WhiteSmoke;
            this.butSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.butSearch.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butSearch.Location = new System.Drawing.Point(96, 8);
            this.butSearch.Name = "butSearch";
            this.butSearch.Size = new System.Drawing.Size(75, 23);
            this.butSearch.TabIndex = 5;
            this.butSearch.Text = "Search";
            this.butSearch.UseVisualStyleBackColor = false;
            this.butSearch.Click += new System.EventHandler(this.clSearch_Click);
            this.butSearch.MouseEnter += new System.EventHandler(this.butSearch_MouseEnter);
            this.butSearch.MouseLeave += new System.EventHandler(this.butSearch_MouseLeave);
            // 
            // butNotes
            // 
            this.butNotes.BackColor = System.Drawing.Color.Teal;
            this.butNotes.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.butNotes.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butNotes.ForeColor = System.Drawing.Color.White;
            this.butNotes.Location = new System.Drawing.Point(15, 8);
            this.butNotes.Name = "butNotes";
            this.butNotes.Size = new System.Drawing.Size(75, 23);
            this.butNotes.TabIndex = 4;
            this.butNotes.Text = "Notes";
            this.butNotes.UseVisualStyleBackColor = false;
            this.butNotes.Click += new System.EventHandler(this.clNotes_Click);
            this.butNotes.MouseEnter += new System.EventHandler(this.butNotes_MouseEnter);
            this.butNotes.MouseLeave += new System.EventHandler(this.butNotes_MouseLeave);
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.richTextBox2);
            this.splitContainer3.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.richTextBox1);
            this.splitContainer3.Size = new System.Drawing.Size(1270, 238);
            this.splitContainer3.SplitterDistance = 112;
            this.splitContainer3.TabIndex = 0;
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox2.Location = new System.Drawing.Point(0, 32);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox2.Size = new System.Drawing.Size(1270, 80);
            this.richTextBox2.TabIndex = 25;
            this.richTextBox2.Text = "";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.PowderBlue;
            this.panel1.Controls.Add(this.butSpeacker);
            this.panel1.Controls.Add(this.lblParsedSec);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1270, 32);
            this.panel1.TabIndex = 2;
            // 
            // butSpeacker
            // 
            this.butSpeacker.BackColor = System.Drawing.Color.Black;
            this.butSpeacker.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butSpeacker.Image = ((System.Drawing.Image)(resources.GetObject("butSpeacker.Image")));
            this.butSpeacker.Location = new System.Drawing.Point(235, 3);
            this.butSpeacker.Name = "butSpeacker";
            this.butSpeacker.Size = new System.Drawing.Size(28, 28);
            this.butSpeacker.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.butSpeacker.TabIndex = 21;
            this.butSpeacker.TabStop = false;
            this.butSpeacker.Visible = false;
            this.butSpeacker.Click += new System.EventHandler(this.butSpeacker_Click);
            // 
            // lblParsedSec
            // 
            this.lblParsedSec.AutoSize = true;
            this.lblParsedSec.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParsedSec.ForeColor = System.Drawing.Color.Black;
            this.lblParsedSec.Location = new System.Drawing.Point(7, 5);
            this.lblParsedSec.Name = "lblParsedSec";
            this.lblParsedSec.Size = new System.Drawing.Size(203, 21);
            this.lblParsedSec.TabIndex = 2;
            this.lblParsedSec.Text = "Selected Sentence/Segment";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.AliceBlue;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1270, 122);
            this.richTextBox1.TabIndex = 27;
            this.richTextBox1.Text = "";
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // ucDeepAnalyticsResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panHeader);
            this.Name = "ucDeepAnalyticsResults";
            this.Size = new System.Drawing.Size(1270, 511);
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dvgParsedResults)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.butExcel)).EndInit();
            this.panRightTopHeader.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.butSpeacker)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.TextBox lblQuality;
        private System.Windows.Forms.TextBox txtbMessage;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.PictureBox picHeader;
        private System.Windows.Forms.Label lblHeader;
        private ucDeepAnalyticsFilterDisplay ucDeepAnalyticsFilterDisplay1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dvgParsedResults;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chkDocView;
        private System.Windows.Forms.PictureBox butExcel;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblParsedSec;
        private Atebion.RTFBox.RichTextBox richTextBox1;
        private ucDeepAnalyticsKeywords ucDeepAnalyticsKeywords1;
        private ucDeepAnalyticsNotes ucDeepAnalyticsNotes1;
        private MetroFramework.Controls.MetroButton butDelete;
        private MetroFramework.Controls.MetroButton butExport;
        private MetroFramework.Controls.MetroButton butRefresh;
        private System.Windows.Forms.Label lblParseResultsCaption;
        private System.Windows.Forms.PictureBox butSpeacker;
        private System.Windows.Forms.Panel panRightTopHeader;
        private System.Windows.Forms.Button butExported;
        private System.Windows.Forms.Button butKwDicCon;
        private System.Windows.Forms.Button butSearch;
        private System.Windows.Forms.Button butNotes;
        private ucSearch ucSearch1;
        private ucSearch_hoot ucSearch_hoot1;
        private ucExported ucExported1;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
    }
}
