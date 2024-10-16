using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Atebion.DeepAnalytics;
using Atebion.Common;
//using Atebion.Export.Excel;

namespace ProfessionalDocAnalyzer
{
    public partial class ucDeepAnalyticsResults : UserControl
    {
        public ucDeepAnalyticsResults()
        {
            InitializeComponent();
        }

        // Declare delegate for when analysis has completed
        public delegate void ProcessHandler();

        [Category("Action")]
        [Description("Fires when the Analysis Results were Not Found.")]
        public event ProcessHandler AnalysisResultsNotFound;

            private string _pathXML = string.Empty;
            private string _pathParseSec = string.Empty;
            private string _ParsedFile = string.Empty;
            private string _FileName = string.Empty;
            private string _Siarad = string.Empty;

            private bool _ShowTestData = false;
            private int _FoundQty = 0;
            private int _TotalCount = 0;
            private DataView _dv;
            private string _SearchCriteria = string.Empty;

            private Analysis _DeepAnalysis = new Analysis();
            private DataSet _dsParseResults;

            private Image _Notepad;
            private Image _Blank;

            private Modes _CurrentMode;
            private enum Modes
            {
                None = 0,
                KeywordsFound = 1,
                ParametersFound = 2, // Not used
                Errors = 3, // Not used
                SimilarDocs = 4, // Not Used
                Notes = 5,
                Exports = 6,
                Diff = 7, // Not used
                Sum = 8,
                Search = 9,
                Quality = 10
            }

            TabProp selectedTabProp;
            TabProp unselectedTabProp;
            public struct TabProp
            {
                public Color backcolor;
                public Color forecolor;
            }

         private frmDocumentView _frmDocumentView;

        public void Clear()
        {
            richTextBox1.Clear();
            dvgParsedResults.DataSource = null;
        }

        public void LoadData()
        {
            _DeepAnalysis.CurrentDocPath = AppFolders.CurrentDocPath; // Set Paths
 
            //_pathXML = AppFolders.DocParsedSecXML;
            //_pathParseSec = AppFolders.DocParsedSec;

            _Notepad = Image.FromFile(string.Concat(Application.StartupPath, @"\Notepad16x16.jpg"));
            _Blank = Image.FromFile(string.Concat(Application.StartupPath, @"\Blank16x16.jpg"));


            //// Exports
            ucDeepAnalyticsExports2.LoadData();
            lblExportQty.Text = ucDeepAnalyticsExports2.Count.ToString();
        //    clExported.DescriptionText = ucDeepAnalyticsExports2.Count.ToString();


            //// Keywords Found
            ucDeepAnalyticsKeywords2.LoadData();
           // lblKeywordsQty.Text = ucDeepAnalyticsKeywords2.Count.ToString();
     //       clKeywords.DescriptionText = ucDeepAnalyticsKeywords2.Count.ToString();

            //// Search
          //  ucDeepAnalyticsSearch2.LoadData();

            // Notes
            ucDeepAnalyticsNotes2.Path = _DeepAnalysis.NotesPath;

            //// Parse Results
            LoadParseResults();

            GetStartTabs();

         //removed 01.08.2016   AdjustColumns();
        }

        private void GetStartTabs() // Called only once 
        {

            selectedTabProp.backcolor = Color.White;
            selectedTabProp.forecolor = Color.Black;

            unselectedTabProp.backcolor = Color.FromArgb(64, 64, 64);
            unselectedTabProp.forecolor = Color.White;

            _CurrentMode = Modes.Notes;
            ModeAdjustments();
        }

        private void ModeAdjustments()
        {
            Cursor.Current = Cursors.WaitCursor; // Waiting 

            clNotes.Highlight = false;

            clKeywords.Highlight = false;

            clExported.Highlight = false;

            clSearch.Highlight = false;

            ucDeepAnalyticsSearch2.Visible = false;
            ucDeepAnalyticsNotes2.Visible = false;
            ucDeepAnalyticsKeywords2.Visible = false;
            ucDeepAnalyticsExports2.Visible = false;
            ucDAHootSearch1.Visible = false;


            switch (_CurrentMode)
            {
                case Modes.KeywordsFound:
                    clKeywords.Highlight = true;

                    ucDeepAnalyticsKeywords2.Top = 0;
                    ucDeepAnalyticsKeywords2.Dock = DockStyle.Fill;
                    ucDeepAnalyticsKeywords2.Visible = true;

                    break;


                case Modes.Exports:
                    clExported.Highlight = true;

                    ucDeepAnalyticsExports2.LoadData();
                    ucDeepAnalyticsExports2.Dock = DockStyle.Fill;
                    ucDeepAnalyticsExports2.Visible = true;

                    break;


                case Modes.Notes:
                    clNotes.Highlight = true;

                    ucDeepAnalyticsNotes2.Dock = DockStyle.Fill;
                    ucDeepAnalyticsNotes2.Visible = true;

                    break;

                case Modes.Search:
                    clSearch.Highlight = true;

                    string s = _DeepAnalysis.CurrentDocPath;
                    string indexPath = Path.Combine(s, "Deep Analytics", "Current", "Index2");

                    string pathFileError = Path.Combine(indexPath, "Error.txt");

                    if (File.Exists(pathFileError))
                    {
                        ucDAHootSearch1.LoadData();
                        ucDAHootSearch1.Dock = DockStyle.Fill;
                        ucDAHootSearch1.Visible = true;
                    }
                    else
                    {
                        ucDeepAnalyticsSearch2.LoadData();
                        ucDeepAnalyticsSearch2.Dock = DockStyle.Fill;
                        ucDeepAnalyticsSearch2.Visible = true;
                    }



                    break;
            }

            this.Refresh();

            Cursor.Current = Cursors.Default;

        }

        private void LoadParseResults()
        {
          //  this.clSearch.DescriptionText = "in results";

            _ParsedFile = _DeepAnalysis.SentencesFile;

            if (!File.Exists(_ParsedFile))
            {
                MessageBox.Show("Please run the In_Depth Analysis on the selected document.", "No Analysis Results Found", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (AnalysisResultsNotFound != null)
                    AnalysisResultsNotFound();

                return;
            }

            dvgParsedResults.Columns.Clear(); 

            _dsParseResults = XMLDataSet.LoadDatasetFromXml(_ParsedFile);

            DataView view;
            view = _dsParseResults.Tables[0].DefaultView;
            view.Sort = "SortOrder ASC";
            //dvgParsedResults.DataSource = _dsParseResults.Tables[0];
            dvgParsedResults.DataSource = view;

            if (!_ShowTestData)
            {
                dvgParsedResults.Columns["UID"].Visible = false; // Col. index 0
                dvgParsedResults.Columns["SortOrder"].Visible = false; // Col. index 5
                dvgParsedResults.Columns["Sentence"].Visible = false; 
                
            }
            else
            {
                dvgParsedResults.Columns["UID"].Visible = true; // Col. index 0
                dvgParsedResults.Columns["SortOrder"].Visible = true; // Col. index 5
                dvgParsedResults.Columns["Sentence"].Visible = true;
            }


            if (!dvgParsedResults.Columns.Contains("Notes"))      
            {
                
                DataGridViewImageColumn imageCol = new DataGridViewImageColumn();

                imageCol.HeaderText = "";
                imageCol.Name = "Notes";
            //    imageCol.Width = 20;

                dvgParsedResults.Columns.Add(imageCol);

            }

            dvgParsedResults.Columns["Notes"].Visible = true;


            dvgParsedResults.AllowUserToAddRows = false; // Remove blank last row



            //if (dvgParsedResults.Columns.Contains("Quality")) //Added 05.07.2014    
            //{
            //    dvgParsedResults.Columns["Quality"].Width = 35;

            //    string pathFile = string.Concat(AppFolders.DocParsedSecXML, @"\", "Quality.log"); 

            //    if (File.Exists(pathFile))
            //    {
            //        string content = Files.ReadFile(pathFile);

            //        string[] QualityContent = content.Split('|');

            //        setQualityBackColor(QualityContent[0]);
            //    }

            //}

            //    dvgParsedResults.AutoResizeColumns();

            //<<


      //      lblMessage.Text = string.Concat("Quantity of Segments: ", dvgParsedResults.RowCount.ToString()); // Added 10.03.2013

           // clKeywords.DescriptionText = dvgParsedResults.RowCount.ToString();

            // Filter Status Display -- Show All
            _TotalCount = dvgParsedResults.RowCount;
            ucDeepAnalyticsFilterDisplay1.Count = dvgParsedResults.RowCount;
            ucDeepAnalyticsFilterDisplay1.Total = dvgParsedResults.RowCount;
            ucDeepAnalyticsFilterDisplay1.CurrentMode = ucDeepAnalyticsFilterDisplay.Modes.All;
            ucDeepAnalyticsFilterDisplay1.UpdateStatusDisplay();
            
        }

        private void clNotes_Click(object sender, EventArgs e)
        {
            _CurrentMode = Modes.Notes;
            ModeAdjustments();
        }

        private void clSearch_Click(object sender, EventArgs e)
        {
            _CurrentMode = Modes.Search;
            ModeAdjustments();
        }

        private void clKeywords_Click(object sender, EventArgs e)
        {
            _CurrentMode = Modes.KeywordsFound;
            ModeAdjustments();
        }

        private void clExported_Click(object sender, EventArgs e)
        {
            _CurrentMode = Modes.Exports;
            ModeAdjustments();
        }


        private void dvgParsedResults_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dvgParsedResults.Columns[e.ColumnIndex].Name == "Notes")
            {
                if (dvgParsedResults.Rows[e.RowIndex].Cells["UID"].Value == null)
                    return;

                string sUID = dvgParsedResults.Rows[e.RowIndex].Cells["UID"].Value.ToString();
                string noteFile = string.Concat(_DeepAnalysis.NotesPath, @"\", sUID, ".rtf");
                if (File.Exists(noteFile))
                {
                    e.Value = _Notepad;
                }
                else
                {
                    e.Value = _Blank;
                }
            } 
        }

        private void dvgParsedResults_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                RefreshParseResults();
            }
        }

        private void RefreshParseResults()
        {
            DataView view = (DataView)this.dvgParsedResults.DataSource;

            try
            {
                view.Sort = "SortOrder ASC";
                this.dvgParsedResults.DataSource = view;
                //  this.dvgParsedResults.SelectedIndex = _currentIndex - 1;
            }
            catch (Exception e1)
            {
                string msg1 = "Error: " + e1.Message;
                MessageBox.Show(msg1, "Deletion Did Occur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ucDeepAnalyticsFilterDisplay1.UpdateStatusError(e1.Message);

            }
            finally
            {
                this.dvgParsedResults.Refresh();
            }

            GenericDataManger genericDataManger = new GenericDataManger();

            genericDataManger.SaveDataXML(_dsParseResults, _ParsedFile);
        }

        private void dvgParsedResults_SelectionChanged(object sender, EventArgs e)
        {
            ShowParsedDataPerCurrentRow();
        }

        private void ShowParsedDataPerCurrentRow()
        {
            
            butSpeacker.Visible = false;

            if (dvgParsedResults.Rows.Count == 0)
                return;

            Cursor.Current = Cursors.WaitCursor; // Waiting 

            DataGridViewRow row = dvgParsedResults.CurrentRow;

            if (row == null) // Check, sometimes data has not been loaded yet
            {
                Cursor.Current = Cursors.Default; // Back to normal
                return;
            }

            string uid = row.Cells["UID"].Value.ToString();
            
            if (uid == string.Empty) 
            {
                richTextBox1.Text = string.Empty;
                richTextBox2.Text = string.Empty;
                Cursor.Current = Cursors.Default; // Back to normal
                return; // Most likely last row, which is empty
            }

            string[] uidSplit = uid.Split('_');
            string segmentUID = uidSplit[0].ToString();

            //  string fileName = path + row.Cells["FileName"].Value.ToString();
            string sentenceFileName = string.Concat(_DeepAnalysis.ParseSentencesPath, @"\", uid, ".rtf"); // ?ToDo: Fix Error -- Null --Combine: When clicking on the Cancel button 

            _FileName = string.Concat(string.Concat(AppFolders.DocParsedSec, @"\", segmentUID, ".rtf")); // ?ToDo: Fix Error -- Null --Combine: When clicking on the Cancel button 

            // Check Segment File
            if (!File.Exists(_FileName))
            {
                string msg = string.Concat("Unable to find the current Segment file: ", _FileName);
                MessageBox.Show(msg, "Unable to Open Segment File", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                ucDeepAnalyticsFilterDisplay1.UpdateStatusError(msg);

                richTextBox1.Text = msg;
                return;
            }

            if (Files.FileIsLocked(_FileName))
            {
                string msg = string.Concat("The selected Segment File (", _FileName, ") is currently opened by another application. Please close this document file and try again.");
                MessageBox.Show(msg, "Unable to Open Segment File", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                richTextBox1.Text = msg;
                return;
            }
    

            // Check Sentence File
            if (File.Exists(sentenceFileName)) 
            {

                if (Files.FileIsLocked(sentenceFileName)) 
                {
                    string msg = string.Concat("The selected Sentence File (", sentenceFileName, ") is currently opened by another application. Please close this document file and try again.");
                    MessageBox.Show(msg, "Unable to Open this File", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    richTextBox1.Text = msg;

                    return;

                }

                richTextBox1.LoadFile(_FileName); // Load segment RichTextBox
              //  richTextBox1.Font = new Font("Segoe UI", 8);

                richTextBox2.LoadFile(sentenceFileName); // Load sentence RichTextBox
           //     richTextBox2.Font = new Font("Segoe UI", 14);

                string sentence = richTextBox2.Text.Trim();

                HighlightText(sentence, Color.Yellow, false);



        //        _FileName = fileName;

                // Check to see if Siarad exists, show button if it does 
                string appPath = System.Windows.Forms.Application.StartupPath;
                _Siarad = string.Concat(appPath, @"\Siarad.exe");
                if (File.Exists(_Siarad))
                {
                    butSpeacker.Visible = true;
                }
                else
                {
                    butSpeacker.Visible = false;
                }
            }
            else
            {
                richTextBox1.Text = string.Empty;
                richTextBox1.Text = string.Concat("Error: Cannot find file: ", sentenceFileName);

                ucDeepAnalyticsFilterDisplay1.UpdateStatusError(string.Concat("Cannot find file: ", sentenceFileName));

            }

            // Check if there is a Warning for the selected section
            string resultsUID = row.Cells["UID"].Value.ToString();

            this.ucDeepAnalyticsNotes2.UID = resultsUID;

            if (this.chkDocView.Checked)
            {
                if (_frmDocumentView == null)
                {
                    this.chkDocView.Checked = false;
                    return;
                }

                if (!CheckOpened("Document View"))
                {
                    this.chkDocView.Checked = false;
                    return;
                }

                int line = 0;
                if (row.Cells["LineStart"].Value.ToString() != string.Empty) // Changed on 12.19.2018
                {
                    line = Convert.ToInt32(row.Cells["LineStart"].Value);
                }
                line++; // parser engine is zero based, while document viewer is one base. Therefore add one.

                _frmDocumentView.ShowSegment(line, richTextBox2.Text.Trim());
            }

            Cursor.Current = Cursors.Default; // Back to normal

        }

        private bool CheckOpened(string name)
        {
            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                if (frm.Text == name)
                {
                    return true;
                }
            }
            return false;
        }

        private int HighlightText(string word, Color color, bool HighlightText)
        {
            int count = 0;

            if (word == string.Empty) // Added 05/26/2016 to fix System.OutOfMemoryException -- found in 1.7.15.20
                return count;

            int s_start = richTextBox1.SelectionStart, startIndex = 0, index;

            while ((index = richTextBox1.Text.IndexOf(word, startIndex)) != -1)
            {
                richTextBox1.Select(index, word.Length);

                if (HighlightText) // Highlight Text
                    richTextBox1.SelectionColor = color;
                else // Highlight Background
                    richTextBox1.SelectionBackColor = color;

                startIndex = index + word.Length;
                count++;
            }

            return count;
        }

        private void picDelete_Click(object sender, EventArgs e)
        {
            string msg = "Are you sure you want to permanently delete the selected sentence?";
            if (MessageBox.Show(msg, "Please Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            RemoveSentence(dvgParsedResults.CurrentRow.Index);

            AdjustColumns(); 
        }

        private void RemoveSentence(int index)
        {
            dvgParsedResults.Rows.RemoveAt(index);

            DataView view = (DataView)this.dvgParsedResults.DataSource;

            DataGridViewRow row = dvgParsedResults.CurrentRow;

            string sortOrder = row.Cells["sortOrder"].ToString();

            foreach (DataRowView row1 in view)
            {
                if (row1[0].ToString() == sortOrder)
                {
                    row1.Delete();
                    break;
                }
            }

            RefreshParseResults();

        }

        private void AdjustColumns()
        {

           // dvgParsedResults.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);

            Application.DoEvents();

            if (dvgParsedResults.Columns.Contains("LineStart"))
            {
                dvgParsedResults.Columns["LineStart"].Visible = false;
                chkDocView.Visible = true;
            }
            else
            {
                chkDocView.Visible = false;
                chkDocView.Checked = false;
            }

            if (dvgParsedResults.Columns.Contains("Keywords"))
                dvgParsedResults.Columns["Keywords"].Width = 200;

            if (dvgParsedResults.Columns.Contains("Number"))
                dvgParsedResults.Columns["Number"].Width = 75;

            if (dvgParsedResults.Columns.Contains("Page"))
            {
                dvgParsedResults.Columns["Page"].Width = 40;
                dvgParsedResults.Columns["Page"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }


            if (dvgParsedResults.Columns.Contains("Notes"))
                dvgParsedResults.Columns["Notes"].Width = 20;

        }

        private void butRefresh_Click(object sender, EventArgs e)
        {
            LoadParseResults();
            AdjustColumns();

            // Filter Status Display -- Show All
            ucDeepAnalyticsFilterDisplay1.Count = dvgParsedResults.RowCount;
            ucDeepAnalyticsFilterDisplay1.Total = dvgParsedResults.RowCount;
            ucDeepAnalyticsFilterDisplay1.CurrentMode = ucDeepAnalyticsFilterDisplay.Modes.All;
            ucDeepAnalyticsFilterDisplay1.UpdateStatusDisplay();

            butRefresh.Visible = false;

            
        }

        private void butExcel_Click(object sender, EventArgs e)
        {
            //ExportToExcel excel = new ExportToExcel();


            //string exportPathFile = string.Concat(AppFolders.DocParsedSecTemp, @"\DataResults.xlsx");

            //if (File.Exists(exportPathFile))
            //{
            //    File.Delete(exportPathFile);
            //}

            //Atebion.Export.Excel.ExportToExcel excport2Excel = new Atebion.Export.Excel.ExportToExcel();

            //excport2Excel.CreateXLS(_dsParseResults.Tables[0], AppFolders.ProjectName + " Parse Data", exportPathFile, exportPathFile);

            //if (!File.Exists(exportPathFile))
            //{
            //    Application.DoEvents();
            //}
            //Process.Start(exportPathFile);
        }

        private void butSpeacker_Click(object sender, EventArgs e)
        {
            if (_FileName == string.Empty)
                return;

            Process proc = new Process();
            proc.StartInfo.FileName = _Siarad;
            proc.StartInfo.Arguments = @"""" + _FileName + @"""";
            proc.Start();
        }

        private void dvgParsedResults_Paint(object sender, PaintEventArgs e)
        {
           // AdjustColumns();
        }



        private void butExport_Click(object sender, EventArgs e)
        {
            bool pageColExists = dvgParsedResults.Columns.Contains(ParseResultsFields.PageSource); // Added 8.15.2018

            _dv = (DataView)dvgParsedResults.DataSource;
            frmDeepAnalyticsExport frm = new frmDeepAnalyticsExport(_dv.ToTable(), 
                ucDeepAnalyticsFilterDisplay1.Count,
                ucDeepAnalyticsFilterDisplay1.Total,
                ucDeepAnalyticsFilterDisplay1.CurrentMode,
                pageColExists);
  

            // Show Dialog as a modal dialog and determine if DialogResult = OK.
            frm.ShowDialog(this);

            // Call Section Export 
            ucDeepAnalyticsExports2.LoadData();
            int count = ucDeepAnalyticsExports2.Count;
            lblExportQty.Text = count.ToString();
          //  clExported.DescriptionText = count.ToString();
        }

        private void lblParseSentences_Click(object sender, EventArgs e)
        {
            //if (butExcel.Visible)
            //    butExcel.Visible = false;
            //else
            //    butExcel.Visible = true;
        }

        private void dvgParsedResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ucDeepAnalyticsResults_Paint(object sender, PaintEventArgs e)
        {
            AdjustColumns(); // Added 01.08.2016
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDeepAnalyticsResults));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panHeader = new System.Windows.Forms.Panel();
            this.ucDeepAnalyticsFilterDisplay1 = new WizardFramework.ucDeepAnalyticsFilterDisplay();
            this.lblQuality = new System.Windows.Forms.TextBox();
            this.txtbMessage = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.picHeader = new System.Windows.Forms.PictureBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.butExcel = new System.Windows.Forms.PictureBox();
            this.butRefresh = new System.Windows.Forms.PictureBox();
            this.picDelete = new System.Windows.Forms.PictureBox();
            this.butExport = new System.Windows.Forms.PictureBox();
            this.butSpeacker = new System.Windows.Forms.PictureBox();
            this.clNotes = new MetroFramework.Controls.MetroButton();
            this.clSearch = new MetroFramework.Controls.MetroButton();
            this.clKeywords = new MetroFramework.Controls.MetroButton();
            this.clExported = new MetroFramework.Controls.MetroButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dvgParsedResults = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkDocView = new System.Windows.Forms.CheckBox();
            this.lblParseSentences = new System.Windows.Forms.Label();
            this.ucDeepAnalyticsKeywords2 = new WizardFramework.ucDeepAnalyticsKeywords();
            this.ucDeepAnalyticsExports2 = new WizardFramework.ucDeepAnalyticsExports();
            this.ucDeepAnalyticsSearch2 = new WizardFramework.ucDeepAnalyticsSearch();
            this.ucDeepAnalyticsNotes2 = new WizardFramework.ucDeepAnalyticsNotes();
            this.panRightOptions = new System.Windows.Forms.Panel();
            this.lblExportQty = new System.Windows.Forms.Label();
            this.lblExport = new System.Windows.Forms.Label();
            this.lblKeywords = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
            this.lblNotes = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox1 = new Atebion.RTFBox.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblParsedSec = new System.Windows.Forms.Label();
            this.ucDAHootSearch1 = new WizardFramework.ucDAHootSearch();
            this.panHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.butExcel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.butRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.butExport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.butSpeacker)).BeginInit();
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
            this.panRightOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panHeader
            // 
            this.panHeader.BackColor = System.Drawing.Color.Black;
            this.panHeader.Controls.Add(this.ucDeepAnalyticsFilterDisplay1);
            this.panHeader.Controls.Add(this.lblQuality);
            this.panHeader.Controls.Add(this.txtbMessage);
            this.panHeader.Controls.Add(this.lblMessage);
            this.panHeader.Controls.Add(this.picHeader);
            this.panHeader.Controls.Add(this.lblHeader);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(1270, 50);
            this.panHeader.TabIndex = 19;
            // 
            // ucDeepAnalyticsFilterDisplay1
            // 
            this.ucDeepAnalyticsFilterDisplay1.BackColor = System.Drawing.Color.Black;
            this.ucDeepAnalyticsFilterDisplay1.Count = 0;
            this.ucDeepAnalyticsFilterDisplay1.CurrentMode = WizardFramework.ucDeepAnalyticsFilterDisplay.Modes.All;
            this.ucDeepAnalyticsFilterDisplay1.Location = new System.Drawing.Point(314, 8);
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
            this.lblQuality.Location = new System.Drawing.Point(570, 18);
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
            this.picHeader.Location = new System.Drawing.Point(9, 6);
            this.picHeader.Name = "picHeader";
            this.picHeader.Size = new System.Drawing.Size(41, 38);
            this.picHeader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHeader.TabIndex = 16;
            this.picHeader.TabStop = false;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(56, 11);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(214, 30);
            this.lblHeader.TabIndex = 15;
            this.lblHeader.Text = "Deep Analysis Results";
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
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
            this.metroToolTip1.SetToolTip(this.butExcel, "Export the Parse Results Data");
            this.butExcel.Visible = false;
            this.butExcel.Click += new System.EventHandler(this.butExcel_Click);
            // 
            // butRefresh
            // 
            this.butRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butRefresh.Image = ((System.Drawing.Image)(resources.GetObject("butRefresh.Image")));
            this.butRefresh.Location = new System.Drawing.Point(277, 1);
            this.butRefresh.Name = "butRefresh";
            this.butRefresh.Size = new System.Drawing.Size(38, 38);
            this.butRefresh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.butRefresh.TabIndex = 25;
            this.butRefresh.TabStop = false;
            this.metroToolTip1.SetToolTip(this.butRefresh, "Refresh Parse Results. Show all results");
            this.butRefresh.Visible = false;
            this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
            // 
            // picDelete
            // 
            this.picDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picDelete.Image = ((System.Drawing.Image)(resources.GetObject("picDelete.Image")));
            this.picDelete.Location = new System.Drawing.Point(189, 1);
            this.picDelete.Name = "picDelete";
            this.picDelete.Size = new System.Drawing.Size(38, 38);
            this.picDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDelete.TabIndex = 18;
            this.picDelete.TabStop = false;
            this.metroToolTip1.SetToolTip(this.picDelete, "Remove the selected segment");
            this.picDelete.Click += new System.EventHandler(this.picDelete_Click);
            // 
            // butExport
            // 
            this.butExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butExport.Image = ((System.Drawing.Image)(resources.GetObject("butExport.Image")));
            this.butExport.Location = new System.Drawing.Point(233, 1);
            this.butExport.Name = "butExport";
            this.butExport.Size = new System.Drawing.Size(38, 38);
            this.butExport.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.butExport.TabIndex = 17;
            this.butExport.TabStop = false;
            this.metroToolTip1.SetToolTip(this.butExport, "Export the Parse Results");
            this.butExport.Click += new System.EventHandler(this.butExport_Click);
            // 
            // butSpeacker
            // 
            this.butSpeacker.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butSpeacker.Image = ((System.Drawing.Image)(resources.GetObject("butSpeacker.Image")));
            this.butSpeacker.Location = new System.Drawing.Point(251, 4);
            this.butSpeacker.Name = "butSpeacker";
            this.butSpeacker.Size = new System.Drawing.Size(32, 32);
            this.butSpeacker.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.butSpeacker.TabIndex = 18;
            this.butSpeacker.TabStop = false;
            this.metroToolTip1.SetToolTip(this.butSpeacker, "Read the selected segment to me");
            this.butSpeacker.Visible = false;
            this.butSpeacker.Click += new System.EventHandler(this.butSpeacker_Click);
            // 
            // clNotes
            // 
            this.clNotes.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("clNotes.BackgroundImage")));
            this.clNotes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.clNotes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.clNotes.ForeColor = System.Drawing.Color.White;
            this.clNotes.Highlight = true;
            this.clNotes.Location = new System.Drawing.Point(7, 6);
            this.clNotes.Name = "clNotes";
            this.clNotes.Size = new System.Drawing.Size(38, 38);
            this.clNotes.TabIndex = 25;
            this.clNotes.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.clNotes.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroToolTip1.SetToolTip(this.clNotes, "Write Notes for the\r\n selected Segment/Paragraph");
            this.clNotes.UseSelectable = true;
            this.clNotes.Click += new System.EventHandler(this.clNotes_Click);
            // 
            // clSearch
            // 
            this.clSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("clSearch.BackgroundImage")));
            this.clSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.clSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.clSearch.ForeColor = System.Drawing.Color.White;
            this.clSearch.Location = new System.Drawing.Point(7, 50);
            this.clSearch.Name = "clSearch";
            this.clSearch.Size = new System.Drawing.Size(38, 38);
            this.clSearch.TabIndex = 27;
            this.clSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.clSearch.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroToolTip1.SetToolTip(this.clSearch, "Search for Words or Phrases \r\nin Segments/Paragraphs");
            this.clSearch.UseSelectable = true;
            this.clSearch.Click += new System.EventHandler(this.clSearch_Click);
            // 
            // clKeywords
            // 
            this.clKeywords.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("clKeywords.BackgroundImage")));
            this.clKeywords.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.clKeywords.Cursor = System.Windows.Forms.Cursors.Hand;
            this.clKeywords.ForeColor = System.Drawing.Color.White;
            this.clKeywords.Location = new System.Drawing.Point(7, 94);
            this.clKeywords.Name = "clKeywords";
            this.clKeywords.Size = new System.Drawing.Size(38, 38);
            this.clKeywords.TabIndex = 30;
            this.clKeywords.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.clKeywords.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroToolTip1.SetToolTip(this.clKeywords, "Keyword Cloud");
            this.clKeywords.UseSelectable = true;
            this.clKeywords.Click += new System.EventHandler(this.clKeywords_Click);
            // 
            // clExported
            // 
            this.clExported.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("clExported.BackgroundImage")));
            this.clExported.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.clExported.Cursor = System.Windows.Forms.Cursors.Hand;
            this.clExported.ForeColor = System.Drawing.Color.White;
            this.clExported.Location = new System.Drawing.Point(7, 138);
            this.clExported.Name = "clExported";
            this.clExported.Size = new System.Drawing.Size(38, 38);
            this.clExported.TabIndex = 33;
            this.clExported.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.clExported.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroToolTip1.SetToolTip(this.clExported, "Listing of Exported \r\nAnalysis Results");
            this.clExported.UseSelectable = true;
            this.clExported.Click += new System.EventHandler(this.clExported_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 50);
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
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.splitContainer1.Size = new System.Drawing.Size(1270, 461);
            this.splitContainer1.SplitterDistance = 280;
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
            this.splitContainer2.Panel2.Controls.Add(this.ucDAHootSearch1);
            this.splitContainer2.Panel2.Controls.Add(this.ucDeepAnalyticsKeywords2);
            this.splitContainer2.Panel2.Controls.Add(this.ucDeepAnalyticsExports2);
            this.splitContainer2.Panel2.Controls.Add(this.ucDeepAnalyticsSearch2);
            this.splitContainer2.Panel2.Controls.Add(this.ucDeepAnalyticsNotes2);
            this.splitContainer2.Panel2.Controls.Add(this.panRightOptions);
            this.splitContainer2.Panel2.Padding = new System.Windows.Forms.Padding(0, 0, 25, 0);
            this.splitContainer2.Size = new System.Drawing.Size(1270, 280);
            this.splitContainer2.SplitterDistance = 595;
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
            this.dvgParsedResults.Location = new System.Drawing.Point(0, 42);
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
            this.dvgParsedResults.Size = new System.Drawing.Size(595, 238);
            this.dvgParsedResults.TabIndex = 14;
            this.dvgParsedResults.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dvgParsedResults_CellContentClick);
            this.dvgParsedResults.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dvgParsedResults_CellFormatting);
            this.dvgParsedResults.SelectionChanged += new System.EventHandler(this.dvgParsedResults_SelectionChanged);
            this.dvgParsedResults.Paint += new System.Windows.Forms.PaintEventHandler(this.dvgParsedResults_Paint);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Controls.Add(this.chkDocView);
            this.panel2.Controls.Add(this.butExcel);
            this.panel2.Controls.Add(this.butRefresh);
            this.panel2.Controls.Add(this.picDelete);
            this.panel2.Controls.Add(this.butExport);
            this.panel2.Controls.Add(this.lblParseSentences);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(595, 42);
            this.panel2.TabIndex = 1;
            // 
            // chkDocView
            // 
            this.chkDocView.AutoSize = true;
            this.chkDocView.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDocView.ForeColor = System.Drawing.Color.White;
            this.chkDocView.Location = new System.Drawing.Point(331, 12);
            this.chkDocView.Name = "chkDocView";
            this.chkDocView.Size = new System.Drawing.Size(114, 19);
            this.chkDocView.TabIndex = 29;
            this.chkDocView.Text = "Show Document";
            this.chkDocView.UseVisualStyleBackColor = true;
            this.chkDocView.Visible = false;
            this.chkDocView.CheckedChanged += new System.EventHandler(this.chkDocView_CheckedChanged);
            // 
            // lblParseSentences
            // 
            this.lblParseSentences.AutoSize = true;
            this.lblParseSentences.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblParseSentences.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParseSentences.ForeColor = System.Drawing.Color.White;
            this.lblParseSentences.Location = new System.Drawing.Point(3, 12);
            this.lblParseSentences.Name = "lblParseSentences";
            this.lblParseSentences.Size = new System.Drawing.Size(158, 25);
            this.lblParseSentences.TabIndex = 2;
            this.lblParseSentences.Text = "Parsed Sentences";
            this.lblParseSentences.Click += new System.EventHandler(this.lblParseSentences_Click);
            // 
            // ucDeepAnalyticsKeywords2
            // 
            this.ucDeepAnalyticsKeywords2.Location = new System.Drawing.Point(151, 163);
            this.ucDeepAnalyticsKeywords2.Name = "ucDeepAnalyticsKeywords2";
            this.ucDeepAnalyticsKeywords2.Size = new System.Drawing.Size(793, 390);
            this.ucDeepAnalyticsKeywords2.TabIndex = 5;
            this.ucDeepAnalyticsKeywords2.Visible = false;
            this.ucDeepAnalyticsKeywords2.FilterCompleted += new WizardFramework.ucDeepAnalyticsKeywords.ProcessHandler(this.ucDeepAnalyticsKeywords2_FilterCompleted);
            // 
            // ucDeepAnalyticsExports2
            // 
            this.ucDeepAnalyticsExports2.Location = new System.Drawing.Point(191, 118);
            this.ucDeepAnalyticsExports2.Name = "ucDeepAnalyticsExports2";
            this.ucDeepAnalyticsExports2.Size = new System.Drawing.Size(737, 384);
            this.ucDeepAnalyticsExports2.TabIndex = 4;
            this.ucDeepAnalyticsExports2.Visible = false;
            // 
            // ucDeepAnalyticsSearch2
            // 
            this.ucDeepAnalyticsSearch2.BackColor = System.Drawing.Color.Black;
            this.ucDeepAnalyticsSearch2.Location = new System.Drawing.Point(287, 94);
            this.ucDeepAnalyticsSearch2.Name = "ucDeepAnalyticsSearch2";
            this.ucDeepAnalyticsSearch2.Size = new System.Drawing.Size(756, 408);
            this.ucDeepAnalyticsSearch2.TabIndex = 3;
            this.ucDeepAnalyticsSearch2.Visible = false;
            this.ucDeepAnalyticsSearch2.SearchCompleted += new WizardFramework.ucDeepAnalyticsSearch.ProcessHandler(this.ucDeepAnalyticsSearch2_SearchCompleted);
            // 
            // ucDeepAnalyticsNotes2
            // 
            this.ucDeepAnalyticsNotes2.Location = new System.Drawing.Point(433, 58);
            this.ucDeepAnalyticsNotes2.Name = "ucDeepAnalyticsNotes2";
            this.ucDeepAnalyticsNotes2.Path = "";
            this.ucDeepAnalyticsNotes2.Size = new System.Drawing.Size(1029, 491);
            this.ucDeepAnalyticsNotes2.TabIndex = 2;
            this.ucDeepAnalyticsNotes2.UID = "";
            this.ucDeepAnalyticsNotes2.Visible = false;
            // 
            // panRightOptions
            // 
            this.panRightOptions.AutoScroll = true;
            this.panRightOptions.BackColor = System.Drawing.Color.Black;
            this.panRightOptions.Controls.Add(this.lblExportQty);
            this.panRightOptions.Controls.Add(this.lblExport);
            this.panRightOptions.Controls.Add(this.clExported);
            this.panRightOptions.Controls.Add(this.lblKeywords);
            this.panRightOptions.Controls.Add(this.clKeywords);
            this.panRightOptions.Controls.Add(this.lblSearch);
            this.panRightOptions.Controls.Add(this.clSearch);
            this.panRightOptions.Controls.Add(this.lblNotes);
            this.panRightOptions.Controls.Add(this.clNotes);
            this.panRightOptions.Dock = System.Windows.Forms.DockStyle.Left;
            this.panRightOptions.Location = new System.Drawing.Point(0, 0);
            this.panRightOptions.Name = "panRightOptions";
            this.panRightOptions.Size = new System.Drawing.Size(145, 280);
            this.panRightOptions.TabIndex = 1;
            // 
            // lblExportQty
            // 
            this.lblExportQty.AutoSize = true;
            this.lblExportQty.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExportQty.ForeColor = System.Drawing.Color.White;
            this.lblExportQty.Location = new System.Drawing.Point(56, 163);
            this.lblExportQty.Name = "lblExportQty";
            this.lblExportQty.Size = new System.Drawing.Size(13, 15);
            this.lblExportQty.TabIndex = 35;
            this.lblExportQty.Text = "0";
            // 
            // lblExport
            // 
            this.lblExport.AutoSize = true;
            this.lblExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblExport.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExport.ForeColor = System.Drawing.Color.White;
            this.lblExport.Location = new System.Drawing.Point(51, 141);
            this.lblExport.Name = "lblExport";
            this.lblExport.Size = new System.Drawing.Size(69, 20);
            this.lblExport.TabIndex = 34;
            this.lblExport.Text = "Exported";
            this.lblExport.Click += new System.EventHandler(this.lblExport_Click);
            // 
            // lblKeywords
            // 
            this.lblKeywords.AutoSize = true;
            this.lblKeywords.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblKeywords.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKeywords.ForeColor = System.Drawing.Color.White;
            this.lblKeywords.Location = new System.Drawing.Point(51, 103);
            this.lblKeywords.Name = "lblKeywords";
            this.lblKeywords.Size = new System.Drawing.Size(73, 20);
            this.lblKeywords.TabIndex = 31;
            this.lblKeywords.Text = "Keywords";
            this.lblKeywords.Click += new System.EventHandler(this.lblKeywords_Click);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.ForeColor = System.Drawing.Color.White;
            this.lblSearch.Location = new System.Drawing.Point(51, 58);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(53, 20);
            this.lblSearch.TabIndex = 28;
            this.lblSearch.Text = "Search";
            this.lblSearch.Click += new System.EventHandler(this.lblSearch_Click);
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblNotes.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotes.ForeColor = System.Drawing.Color.White;
            this.lblNotes.Location = new System.Drawing.Point(51, 16);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(48, 20);
            this.lblNotes.TabIndex = 26;
            this.lblNotes.Text = "Notes";
            this.lblNotes.Click += new System.EventHandler(this.lblNotes_Click);
            // 
            // splitContainer3
            // 
            this.splitContainer3.BackColor = System.Drawing.Color.Black;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 40);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.richTextBox2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.richTextBox1);
            this.splitContainer3.Size = new System.Drawing.Size(1255, 137);
            this.splitContainer3.SplitterDistance = 60;
            this.splitContainer3.TabIndex = 2;
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox2.Location = new System.Drawing.Point(0, 0);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox2.Size = new System.Drawing.Size(1255, 60);
            this.richTextBox2.TabIndex = 24;
            this.richTextBox2.Text = "";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.AliceBlue;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1255, 73);
            this.richTextBox1.TabIndex = 26;
            this.richTextBox1.Text = "";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.butSpeacker);
            this.panel1.Controls.Add(this.lblParsedSec);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1255, 40);
            this.panel1.TabIndex = 1;
            // 
            // lblParsedSec
            // 
            this.lblParsedSec.AutoSize = true;
            this.lblParsedSec.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParsedSec.ForeColor = System.Drawing.Color.White;
            this.lblParsedSec.Location = new System.Drawing.Point(3, 7);
            this.lblParsedSec.Name = "lblParsedSec";
            this.lblParsedSec.Size = new System.Drawing.Size(246, 25);
            this.lblParsedSec.TabIndex = 2;
            this.lblParsedSec.Text = "Selected Sentence/Segment";
            // 
            // ucDAHootSearch1
            // 
            this.ucDAHootSearch1.Location = new System.Drawing.Point(495, 16);
            this.ucDAHootSearch1.Name = "ucDAHootSearch1";
            this.ucDAHootSearch1.Size = new System.Drawing.Size(753, 451);
            this.ucDAHootSearch1.TabIndex = 6;
            this.ucDAHootSearch1.Visible = false;
            this.ucDAHootSearch1.SearchCompleted += new WizardFramework.ucDAHootSearch.ProcessHandler(this.ucDAHootSearch1_SearchCompleted);
            // 
            // ucDeepAnalyticsResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panHeader);
            this.Name = "ucDeepAnalyticsResults";
            this.Size = new System.Drawing.Size(1270, 511);
            this.VisibleChanged += new System.EventHandler(this.ucDeepAnalyticsResults_VisibleChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ucDeepAnalyticsResults_Paint);
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.butExcel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.butRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.butExport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.butSpeacker)).EndInit();
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
            this.panRightOptions.ResumeLayout(false);
            this.panRightOptions.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        private void lblNotes_Click(object sender, EventArgs e)
        {
            _CurrentMode = Modes.Notes;
            ModeAdjustments();
        }

        private void lblSearch_Click(object sender, EventArgs e)
        {
            _CurrentMode = Modes.Search;
            ModeAdjustments();
        }

        private void lblKeywords_Click(object sender, EventArgs e)
        {
            _CurrentMode = Modes.KeywordsFound;
            ModeAdjustments();
        }

        private void lblExport_Click(object sender, EventArgs e)
        {
            _CurrentMode = Modes.Exports;
            ModeAdjustments();
        }

        private void chkDocView_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDocView.Checked)
            {

                string file = string.Concat(AppFolders.DocName, ".txt");
                string pathFile = Path.Combine(AppFolders.CurrentDocPath, file);
                string fileRTF = string.Concat(AppFolders.DocName, ".rtf");
                string keywordsPathFile = Path.Combine(AppFolders.DocParsedSecKeywords, fileRTF);
                if (File.Exists(keywordsPathFile))
                {
                    _frmDocumentView = new frmDocumentView();
                    _frmDocumentView.LoadData(keywordsPathFile, AppFolders.DocParsedSecKeywords, AppFolders.DocName);

                }
                else
                {
                    if (!File.Exists(pathFile))
                    {
                         // Added 12.18.2018
                        string[] txtFiles = Directory.GetFiles(AppFolders.CurrentDocPath, "*.txt");
                        if (txtFiles.Length > 0)
                        {
                            pathFile = txtFiles[0];
                        }
                        else // End Added
                        {
                            string msg = string.Concat("Unable to find file: ", pathFile);
                            MessageBox.Show(msg, "Unable to Display Document", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }

                    _frmDocumentView = new frmDocumentView();
                    _frmDocumentView.LoadData(pathFile, AppFolders.DocParsedSecKeywords, AppFolders.DocName);
                }

                _frmDocumentView.Show();

                // Show current segment
                DataGridViewRow row = dvgParsedResults.CurrentRow;
                if (row == null) // Added 05.07.2019
                    return;

                if (row.Cells["LineStart"].Value == null) // Added 05.07.2019
                    return;

                int line = 0;
                if (row.Cells["LineStart"].Value.ToString() != string.Empty) // Changed on 12.19.2018
                {
                    line = Convert.ToInt32(row.Cells["LineStart"].Value);
                }
                line++; // parser engine is zero based, while document viewer is one base. Therefore add one.

                _frmDocumentView.ShowSegment(line, richTextBox2.Text.Trim());

            }
            else
            {
                if (_frmDocumentView != null)
                {
                    _frmDocumentView.Close();
                    _frmDocumentView = null;
                }
            }

        
        }

        private void ucDeepAnalyticsResults_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == false) // Added 07/21/2016
            {
                if (_frmDocumentView != null) // Added 7.27.2018
                {
                    _frmDocumentView.Close();
                    _frmDocumentView = null;
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private void ucDeepAnalyticsKeywords2_FilterCompleted()
        {
            _FoundQty = ucDeepAnalyticsKeywords2.FilterCount;

            if (_FoundQty == 0)
                return;

            List<string> UIDResults = ucDeepAnalyticsKeywords2.FilterUIDs;

            string filter = string.Empty;
            int i = 0;
            string uid = string.Empty;
            foreach (string s in UIDResults)
            {
                uid = string.Concat("'", s, "'");
                if (i == 0)
                    filter = uid;
                else
                    filter = string.Concat(filter, ", ", uid);

                i++;
            }

            filter = string.Concat("UID IN (", filter, ")");

            _dv = new DataView(_dsParseResults.Tables[0]);

            _dv.RowFilter = filter;
            _dv.Sort = "SortOrder ASC";

            this.dvgParsedResults.DataSource = _dv;
            this.dvgParsedResults.Refresh();

            _FoundQty = _dv.Count; // Reset to get count of rows, not search items found -- Fixed Added 07/02/2017

            // Filter Status Display -- Keyword Filter
            ucDeepAnalyticsFilterDisplay1.Count = _FoundQty;
            ucDeepAnalyticsFilterDisplay1.CurrentMode = ucDeepAnalyticsFilterDisplay.Modes.Keywords;
            ucDeepAnalyticsFilterDisplay1.UpdateStatusDisplay();

            butRefresh.Visible = true;
        }

        private void ucDeepAnalyticsSearch2_SearchCompleted()
        {
            _FoundQty = ucDeepAnalyticsSearch2.FoundQty;
            if (_FoundQty == 0)
                return;

            string[] UIDResults = ucDeepAnalyticsSearch2.FoundResults;

            _SearchCriteria = ucDeepAnalyticsSearch2.SearchCriteria;

            string filter = string.Empty;
            int i = 0;
            foreach (string s in UIDResults)
            {
                if (i == 0)
                    filter = string.Concat("'", s, "'");
                else
                    filter = string.Concat(filter, ", ", "'", s, "'");

                i++;
            }

            filter = string.Concat("UID IN (", filter, ")");

            _dv = new DataView(_dsParseResults.Tables[0]);

            _dv.RowFilter = filter;
            _dv.Sort = "SortOrder ASC";

            this.dvgParsedResults.DataSource = _dv;


            butRefresh.Visible = true;


            //     this.clSearch.DescriptionText = _FoundQty.ToString();

            ucDeepAnalyticsFilterDisplay1.Count = _FoundQty;
            ucDeepAnalyticsFilterDisplay1.Total = _TotalCount;
            ucDeepAnalyticsFilterDisplay1.CurrentMode = ucDeepAnalyticsFilterDisplay.Modes.Search;
            ucDeepAnalyticsFilterDisplay1.UpdateStatusDisplay();

            //    lblMessage.Text = string.Format("Search found {0} Segments for Search Criteria: '{1}'", _FoundQty.ToString(), _SearchCriteria);

            Application.DoEvents();

            AdjustColumns(); 
        }

        private void SearchCompleted()
        {

        }

        private void ucDeepAnalyticsKeywords1_FilterCompleted()
        {

        }

        private void ucDAHootSearch1_SearchCompleted()
        {
            _FoundQty = ucDAHootSearch1.FoundQty;
            if (_FoundQty == 0)
                return;

            string[] UIDResults = ucDAHootSearch1.FoundResults;

            _SearchCriteria = ucDeepAnalyticsSearch2.SearchCriteria;

            string filter = string.Empty;
            int i = 0;
            foreach (string s in UIDResults)
            {
                if (i == 0)
                    filter = string.Concat("'", s, "'");
                else
                    filter = string.Concat(filter, ", ", "'", s, "'");

                i++;
            }

            filter = string.Concat("UID IN (", filter, ")");

            _dv = new DataView(_dsParseResults.Tables[0]);

            _dv.RowFilter = filter;
            _dv.Sort = "SortOrder ASC";

            this.dvgParsedResults.DataSource = _dv;


            butRefresh.Visible = true;


            //     this.clSearch.DescriptionText = _FoundQty.ToString();

            ucDeepAnalyticsFilterDisplay1.Count = _FoundQty;
            ucDeepAnalyticsFilterDisplay1.Total = _TotalCount;
            ucDeepAnalyticsFilterDisplay1.CurrentMode = ucDeepAnalyticsFilterDisplay.Modes.Search;
            ucDeepAnalyticsFilterDisplay1.UpdateStatusDisplay();

            //    lblMessage.Text = string.Format("Search found {0} Segments for Search Criteria: '{1}'", _FoundQty.ToString(), _SearchCriteria);

            Application.DoEvents();

            AdjustColumns(); 
        }

 
 

    }
}
