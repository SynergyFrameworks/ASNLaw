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

namespace ProfessionalDocAnalyzer
{
    public partial class ucDeepAnalyticsResults : UserControl
    {
        public ucDeepAnalyticsResults()
        {
            StackTrace st = new StackTrace(false);

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

        public bool LoadData()
        {
            _DeepAnalysis.CurrentDocPath = AppFolders.CurrentDocPath; // Set Paths

            LoadData2();

            return true;

        }

        public bool LoadData(string CurrentDocPath)
        {
            _DeepAnalysis.CurrentDocPath = CurrentDocPath;

            string errMsg = _DeepAnalysis.ErrorMessage;
            if (errMsg.Length > 0)
            {
                MessageBox.Show(errMsg, "Unable to Load Deep Analysis Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }


            LoadData2();

            return true;

        }



        private void LoadData2()
        {
           // _DeepAnalysis.CurrentDocPath = AppFolders.CurrentDocPath; // Set Paths

            //_pathXML = AppFolders.DocParsedSecXML;
            //_pathParseSec = AppFolders.DocParsedSec;

            _Notepad = Image.FromFile(string.Concat(Application.StartupPath, @"\Notepad16x16.jpg"));
         //   _Blank = Image.FromFile(string.Concat(Application.StartupPath, @"\Blank16x16.jpg"));


   

            //// Keywords Found
            ucDeepAnalyticsKeywords1.LoadData(_DeepAnalysis);
            // lblKeywordsQty.Text = ucDeepAnalyticsKeywords1.Count.ToString();
            //       clKeywords.DescriptionText = ucDeepAnalyticsKeywords1.Count.ToString();

            //// Search
            //  ucDeepAnalyticsSearch1.LoadData();

            // Notes
            ucDeepAnalyticsNotes1.LoadData(_DeepAnalysis);
            ucDeepAnalyticsNotes1.Path = _DeepAnalysis.NotesPath;

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

        private void ResetButtons()
        {
            butSearch.BackColor = Color.WhiteSmoke;
            butSearch.ForeColor = Color.Black;

            butNotes.BackColor = Color.WhiteSmoke;
            butNotes.ForeColor = Color.Black;

            butKwDicCon.BackColor = Color.WhiteSmoke;
            butKwDicCon.ForeColor = Color.Black;

            butExported.BackColor = Color.WhiteSmoke;
            butExported.ForeColor = Color.Black;

            this.Refresh();

        }

        private void ModeAdjustments()
        {
            Cursor.Current = Cursors.WaitCursor; // Waiting 

            ucSearch_hoot1.Visible = false;
            ucSearch1.Visible = false;

            ucDeepAnalyticsNotes1.Visible = false;
            ucDeepAnalyticsKeywords1.Visible = false;
            ucExported1.Visible = false;

            ResetButtons();

            switch (_CurrentMode)
            {
                case Modes.KeywordsFound:
                    ucDeepAnalyticsKeywords1.Top = 0;
                    ucDeepAnalyticsKeywords1.Dock = DockStyle.Fill;
                    ucDeepAnalyticsKeywords1.Visible = true;
                    ucDeepAnalyticsKeywords1.AdjustColumns();

                    butKwDicCon.BackColor = Color.Teal;
                    butKwDicCon.ForeColor = Color.White;

                    break;


                case Modes.Exports:
                    ucExported1.LoadData(_DeepAnalysis.ExportPath);
                    ucExported1.Dock = DockStyle.Fill;
                    ucExported1.Visible = true;

                    butExported.BackColor = Color.Teal;
                    butExported.ForeColor = Color.White;

                    break;


                case Modes.Notes:
                    ucDeepAnalyticsNotes1.Dock = DockStyle.Fill;
                    ucDeepAnalyticsNotes1.Visible = true;

                    butNotes.BackColor = Color.Teal;
                    butNotes.ForeColor = Color.White;

                    break;

                case Modes.Search:
                    string s = _DeepAnalysis.CurrentDocPath;
                    string indexPath = Path.Combine(s, "Deep Analytics", "Current", "Index2");

                    string pathFileError = Path.Combine(indexPath, "Error.txt");

                    if (!AppFolders.UseHootSearchEngine)
                    {
                        ucSearch1.LoadData(indexPath, _DeepAnalysis.ParseSentencesPath);
                        ucSearch1.Dock = DockStyle.Fill;
                        ucSearch1.Visible = true;
                    }
                    else
                    {
                        ucSearch_hoot1.LoadData(indexPath, _DeepAnalysis.ParseSentencesPath);
                        ucSearch_hoot1.Dock = DockStyle.Fill;
                        ucSearch_hoot1.Visible = true;
                    }

                    butSearch.BackColor = Color.Teal;
                    butSearch.ForeColor = Color.White;

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

            GenericDataManger gDataMgr = new GenericDataManger();

            _dsParseResults = gDataMgr.LoadDatasetFromXml(_ParsedFile);

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
                    e.Value = dvgParsedResults.Columns["Notes"].DefaultCellStyle.NullValue = null;
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

            string docParseSec = Path.Combine(_DeepAnalysis.CurrentDocPath, "ParseSec");

           // _FileName = string.Concat(string.Concat(AppFolders.DocParsedSec, @"\", segmentUID, ".rtf")); // ?ToDo: Fix Error -- Null --Combine: When clicking on the Cancel button 
            _FileName = string.Concat(string.Concat(docParseSec, @"\", segmentUID, ".rtf")); // ?ToDo: Fix Error -- Null --Combine: When clicking on the Cancel button 


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
                // Make Text Larger -- Easier to Read
                richTextBox1.SelectAll();
                richTextBox1.SelectionFont = new Font("Segoe UI", 10);
                richTextBox1.DeselectAll();
                richTextBox1.BackColor = Color.LightGray;

                richTextBox2.LoadFile(sentenceFileName); // Load sentence RichTextBox
                // Make Text Larger -- Easier to Read
                richTextBox2.SelectAll();
                richTextBox2.SelectionFont = new Font("Segoe UI", 12);
                richTextBox2.DeselectAll();
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

            this.ucDeepAnalyticsNotes1.UID = resultsUID;

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

        public void AdjustColumns()
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



            if (!chkDocView.Checked)
            {
                if (_frmDocumentView != null)
                {
                    _frmDocumentView.Close();
                    _frmDocumentView = null;
                }
            }
            else
            {

                _frmDocumentView = new frmDocumentView();
 
                string pathFile = string.Empty;
                string[] txtFiles = Directory.GetFiles(AppFolders.DocParsedSecKeywords, "Keywords.txt", SearchOption.TopDirectoryOnly);
                if (txtFiles.Length > 0)
                {
                    pathFile = txtFiles[0];
                }
                else // No files found
                {
                    string msg = string.Concat("Unable to find file: ", pathFile);
                    MessageBox.Show(msg, "Unable to Display Document", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    chkDocView.Checked = false;

                    return;
                }

                _frmDocumentView = new frmDocumentView();
                _frmDocumentView.LoadData(pathFile, AppFolders.DocParsedSecKeywords, AppFolders.DocName);
                

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

        private void ucDeepAnalyticsKeywords1_FilterCompleted()
        {
            _FoundQty = ucDeepAnalyticsKeywords1.FilterCount;

            if (_FoundQty == 0)
                return;

            List<string> UIDResults = ucDeepAnalyticsKeywords1.FilterUIDs;

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

        private void SearchFilter(string[] UIDResults)
        {
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


        //private void ucDeepAnalyticsSearch1_SearchCompleted()
        //{
        //    _FoundQty = ucDeepAnalyticsSearch1.FoundQty;
        //    if (_FoundQty == 0)
        //        return;

        //    string[] UIDResults = ucDeepAnalyticsSearch1.FoundResults;

        //    _SearchCriteria = ucDeepAnalyticsSearch1.SearchCriteria;

        //    string filter = string.Empty;
        //    int i = 0;
        //    foreach (string s in UIDResults)
        //    {
        //        if (i == 0)
        //            filter = string.Concat("'", s, "'");
        //        else
        //            filter = string.Concat(filter, ", ", "'", s, "'");

        //        i++;
        //    }

        //    filter = string.Concat("UID IN (", filter, ")");

        //    _dv = new DataView(_dsParseResults.Tables[0]);

        //    _dv.RowFilter = filter;
        //    _dv.Sort = "SortOrder ASC";

        //    this.dvgParsedResults.DataSource = _dv;


        //    butRefresh.Visible = true;


        //    //     this.clSearch.DescriptionText = _FoundQty.ToString();

        //    ucDeepAnalyticsFilterDisplay1.Count = _FoundQty;
        //    ucDeepAnalyticsFilterDisplay1.Total = _TotalCount;
        //    ucDeepAnalyticsFilterDisplay1.CurrentMode = ucDeepAnalyticsFilterDisplay.Modes.Search;
        //    ucDeepAnalyticsFilterDisplay1.UpdateStatusDisplay();

        //    //    lblMessage.Text = string.Format("Search found {0} Segments for Search Criteria: '{1}'", _FoundQty.ToString(), _SearchCriteria);

        //    Application.DoEvents();

        //    AdjustColumns();
        //}

        private void SearchCompleted()
        {

        }

        private void ucSearch_hoot1_SearchCompleted()
        {

            _FoundQty = ucSearch_hoot1.FoundQty;
            if (_FoundQty == 0)
                return;

            string[] UIDResults = ucSearch_hoot1.FoundResults;

            _SearchCriteria = ucSearch_hoot1.SearchCriteria;

            SearchFilter(UIDResults);
        }

        private void ucSearch1_SearchCompleted()
        {
            _FoundQty = ucSearch1.FoundQty;
            if (_FoundQty == 0)
                return;

            string[] UIDResults = ucSearch1.FoundResults;

            _SearchCriteria = ucSearch1.SearchCriteria;

            SearchFilter(UIDResults);
        }

        private void butSearch_MouseLeave(object sender, EventArgs e)
        {
            if (_CurrentMode != Modes.Search)
            {
                butSearch.BackColor = Color.WhiteSmoke;
                butSearch.ForeColor = Color.Black;
            }
        }

        private void butNotes_MouseLeave(object sender, EventArgs e)
        {
            if (_CurrentMode != Modes.Notes)
            {
                butNotes.BackColor = Color.WhiteSmoke;
                butNotes.ForeColor = Color.Black;
            }
        }

        private void butKwDicCon_MouseLeave(object sender, EventArgs e)
        {
            if (_CurrentMode != Modes.KeywordsFound)
            {
                butKwDicCon.BackColor = Color.WhiteSmoke;
                butKwDicCon.ForeColor = Color.Black;
            }
        }

        private void butExported_MouseLeave(object sender, EventArgs e)
        {
            if (_CurrentMode != Modes.Exports)
            {
                butExported.BackColor = Color.WhiteSmoke;
                butExported.ForeColor = Color.Black;
            }
        }

        private void mouseEnter(Button button)
        {
 
            button.BackColor = Color.Teal;
            button.ForeColor = Color.White;

            button.Refresh();
        }

        private void butNotes_MouseEnter(object sender, EventArgs e)
        {
            mouseEnter(butNotes);
        }

        private void butSearch_MouseEnter(object sender, EventArgs e)
        {
            mouseEnter(butSearch);
        }

        private void butKwDicCon_MouseEnter(object sender, EventArgs e)
        {
            mouseEnter(butKwDicCon);
        }

        private void butExported_MouseEnter(object sender, EventArgs e)
        {
            mouseEnter(butExported);
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

            ucExported1.LoadData(_DeepAnalysis.ExportPath);

        }
 

        //private void ucDAHootSearch1_SearchCompleted()
        //{
        //    _FoundQty = ucDAHootSearch1.FoundQty;
        //    if (_FoundQty == 0)
        //        return;

        //    string[] UIDResults = ucDAHootSearch1.FoundResults;

        //    _SearchCriteria = ucDeepAnalyticsSearch1.SearchCriteria;

        //    string filter = string.Empty;
        //    int i = 0;
        //    foreach (string s in UIDResults)
        //    {
        //        if (i == 0)
        //            filter = string.Concat("'", s, "'");
        //        else
        //            filter = string.Concat(filter, ", ", "'", s, "'");

        //        i++;
        //    }

        //    filter = string.Concat("UID IN (", filter, ")");

        //    _dv = new DataView(_dsParseResults.Tables[0]);

        //    _dv.RowFilter = filter;
        //    _dv.Sort = "SortOrder ASC";

        //    this.dvgParsedResults.DataSource = _dv;


        //    butRefresh.Visible = true;


        //    //     this.clSearch.DescriptionText = _FoundQty.ToString();

        //    ucDeepAnalyticsFilterDisplay1.Count = _FoundQty;
        //    ucDeepAnalyticsFilterDisplay1.Total = _TotalCount;
        //    ucDeepAnalyticsFilterDisplay1.CurrentMode = ucDeepAnalyticsFilterDisplay.Modes.Search;
        //    ucDeepAnalyticsFilterDisplay1.UpdateStatusDisplay();

        //    //    lblMessage.Text = string.Format("Search found {0} Segments for Search Criteria: '{1}'", _FoundQty.ToString(), _SearchCriteria);

        //    Application.DoEvents();

        //    AdjustColumns();
        //}

 
 
    }
}
