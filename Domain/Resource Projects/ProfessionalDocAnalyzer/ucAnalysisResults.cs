using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

using Atebion.Common;

namespace ProfessionalDocAnalyzer
{
    public partial class ucAnalysisResults : UserControl
    {
        public ucAnalysisResults()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        // Declare delegate for when analysis has completed
        public delegate void ProcessHandler();

        [Category("Action")]
        [Description("Fires when the Analysis Results were Not Found.")]
        public event ProcessHandler AnalysisResultsNotFound;

        [Category("Action")]
        [Description("Fires when the Deep Analysis is clicked")]
        public event ProcessHandler RunDeepAnalysisResults;

        private Image _Notepad;

        private int _FoundQty = 0;
        private DataView _dv;

        //private DataTable _dtAnalysisResults;
        private string _pathXML = string.Empty;
        private string _pathParseSec = string.Empty;
        private string _PathExport = string.Empty;
        private string _PathNotes = string.Empty;
        private string _PathIndex = string.Empty;
        private string _PathKeywords = string.Empty;
        private string _docFileName = string.Empty;
        private string _NotesPostFix = "_1";
        private bool _isAnalysis = false;

        private string _ParsedFile = string.Empty;
        private DataSet _dsParseResults;

        private string _SearchCriteria = string.Empty;

        private bool _ShowTestData = false;

        private const string _LongQty = "Long";


        private AnalysisResultsType.Selection _Selection;
        private AnalysisResultsType.SearchType _SearchType;

        private DataTable _dtARwithFindings;

        private string _UID = "-1";
        private string _FileName = string.Empty;
        private frmDocumentView _frmDocumentView;

        private bool _SegmentsChanged = false;

        private bool _SearchLoadedOK = false;

        private subPanels _subPanel = subPanels.Notes;
        private enum subPanels
        {
            Notes = 0,
            KwDicCon = 1,
            Search = 2,
            Exported

        }


        public bool LoadData(AnalysisResultsType.Selection Selection, AnalysisResultsType.SearchType SearchType, string PathParseSec, string PathXML, string PathExport, string PathNotes, string PathIndex, string PathKeywords, bool isAnalysis)
        {
            _Selection = Selection;
            _SearchType = SearchType;
            _pathXML = PathXML;
            _pathParseSec = PathParseSec;
            _PathExport = PathExport;
            _PathNotes = PathNotes;
            _PathIndex = PathIndex;
            _PathKeywords = PathKeywords;
            _isAnalysis = isAnalysis;

            _Notepad = Image.FromFile(Path.Combine(Application.StartupPath, "Notepad16x16.jpg"));

            string parentPath = Directory.GetParent(PathParseSec).FullName;
            string docFileName = GetCurrentDocument(parentPath);
            _docFileName = docFileName;

            this.ucDocTypeName1.LoadData(docFileName);

            // Show or Hide Deep Analysis panel button
            string pathFileDoDeepAnalysis = Path.Combine(_pathXML, "EditAnalysisResults.txt");
            if (File.Exists(pathFileDoDeepAnalysis))
            {
                panDA.Visible = true;
            }
            else
            {
                panDA.Visible = false;
            }

            // --- Exports
            ucExported1.LoadData(_PathExport);

            // --- Load Keywords or Dictionary Terms or Concepts or FARs/DFARs
            _dtARwithFindings = GetARwithFindings(_Selection, _SearchType, _pathXML);
            if (_dtARwithFindings != null)
            {
                if (ucKwDicCon1.LoadData(_dtARwithFindings, _Selection, _SearchType))
                {
                    butKwDicCon.Visible = true;
                }
                else
                {
                    butKwDicCon.Visible = false;
                }
            }
            else
            {
                butKwDicCon.Visible = false;
            }

            // --- Notes
            //if (Selection == AnalysisResultsType.Selection.Logic_Segments || Selection == AnalysisResultsType.Selection.Paragraph_Segments)
            //    _NotesPostFix = "_1";
            //else
            //    _NotesPostFix = string.Empty;

            ucNotes1.LoadData(_PathNotes, _NotesPostFix);


            // -- Search
            if (!AppFolders.UseHootSearchEngine)
                _SearchLoadedOK = ucSearch1.LoadData(_PathIndex, _pathParseSec);
            else
                _SearchLoadedOK = ucSearch_hoot1.LoadData(_PathIndex, _pathParseSec);


            // -- Parse Results
            LoadParseResults();

            // Set Notes as the active sub-panel
            ResetSubpanels();

            _subPanel = subPanels.Notes;
            butNotes.BackColor = Color.Teal;
            butNotes.ForeColor = Color.White;

            ucNotes1.Visible = true;
            ucNotes1.Dock = DockStyle.Fill;

            if (!_isAnalysis) // Is Default
            {
                butDeepAnalysis.Visible = ShouldRunInDepthAnalysis();
            }
            else
            {
                butDeepAnalysis.Visible = false;
                lblDANotice.Visible = false;
            }

            //GetCurrentDocument


            return true;
        }


        private bool ShouldRunInDepthAnalysis() // Added 01.04.2015
        {
            lblDANotice.Visible = false;

            Atebion.DeepAnalytics.Analysis ida = new Atebion.DeepAnalytics.Analysis();
            ida.CurrentDocPath = AppFolders.CurrentDocPath;


            // Check if there have been any Parse Sentences
            int ParseSentencesFilesCount = Files.GetFilesCount(ida.ParseSentencesPath, "*.*");
            if (ParseSentencesFilesCount == 0)
            {
                return true;
            }

            // Get Parsed Segments file dates and Parse Sentences Dates 
            DateTime dtParsedSec = Files.GetLatestFileDatetime(AppFolders.DocParsedSec);
            DateTime dtParseSentences = Files.GetLatestFileDatetime(ida.ParseSentencesPath);

            //FileInfo fiParsedSec = Files.GetNewestFile(new DirectoryInfo(AppFolders.DocParsedSec));
            //FileInfo fiParseSentences = Files.GetNewestFile(new DirectoryInfo(ida.ParseSentencesPath));

            int result = DateTime.Compare(dtParsedSec, dtParseSentences);
            if (result < 0) // Segments are earlier than Sentences
            {
                return false;
            }
            else
            {
                lblDANotice.Visible = true;
                return true;
            }
        }


        private void LoadParseResults()
        {

            _ParsedFile = Path.Combine(_pathXML, "ParseResults.xml");

            if (!File.Exists(_ParsedFile))
            {
                MessageBox.Show("Please run Analysis on the selected document.", "No Analysis Results Found", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (AnalysisResultsNotFound != null)
                    AnalysisResultsNotFound();

                return;
            }

            dvgParsedResults.Columns.Clear(); 

            _dsParseResults = Files.LoadDatasetFromXml(_ParsedFile);

            DataView view;
            view = _dsParseResults.Tables[0].DefaultView;
            view.Sort = "SortOrder ASC";
            dvgParsedResults.DataSource = view;

            if (!_ShowTestData)
            {
                dvgParsedResults.Columns[0].Visible = false;
                dvgParsedResults.Columns[1].Visible = false;
                dvgParsedResults.Columns[2].Visible = false;
                dvgParsedResults.Columns[3].Visible = false;
                dvgParsedResults.Columns[4].Visible = false;
                dvgParsedResults.Columns[8].Visible = false;
                dvgParsedResults.Columns[9].Visible = false;
                dvgParsedResults.Columns[12].Visible = false;
                dvgParsedResults.Columns["FileName"].Visible = false;
              //  butExportParseData.Visible = false;
            }
            else
            {
                dvgParsedResults.Columns[0].Visible = true;
                dvgParsedResults.Columns[1].Visible = true;
                dvgParsedResults.Columns[2].Visible = true;
                dvgParsedResults.Columns[3].Visible = true;
                dvgParsedResults.Columns[4].Visible = true;
                dvgParsedResults.Columns[8].Visible = true;
                dvgParsedResults.Columns[9].Visible = true;
                dvgParsedResults.Columns[12].Visible = true;
                dvgParsedResults.Columns["FileName"].Visible = true;
                // butExportParseData.Visible = true;
            }

            dvgParsedResults.Columns[5].Visible = false;
            dvgParsedResults.Columns[6].Visible = false;
            dvgParsedResults.Columns[7].Visible = false;



            if (!dvgParsedResults.Columns.Contains("Notes"))   
            {
                DataGridViewImageColumn imageCol = new DataGridViewImageColumn();

                imageCol.HeaderText = "";
                imageCol.Name = "Notes";
                

                dvgParsedResults.Columns.Add(imageCol);

            }

            dvgParsedResults.Columns["Notes"].Visible = true;


            dvgParsedResults.AllowUserToAddRows = false; // Remove blank last row


            DataGridViewCellStyle dgvColumnHeaderStyle = new DataGridViewCellStyle();
            dgvColumnHeaderStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dvgParsedResults.ColumnHeadersDefaultCellStyle = dgvColumnHeaderStyle;

            if (dvgParsedResults.Columns.Contains("Quality"))
            {
                dvgParsedResults.Columns["Quality"].DefaultCellStyle = dgvColumnHeaderStyle;
            }
            if (dvgParsedResults.Columns.Contains(_LongQty))
            {
                dvgParsedResults.Columns[_LongQty].DefaultCellStyle = dgvColumnHeaderStyle;
            }

            lblMessage.Text = string.Concat("Quantity of Segments: ", dvgParsedResults.RowCount.ToString()); 

             //Adjust4QualityandNotes(); 
        }



        private DataTable GetARwithFindings(AnalysisResultsType.Selection Selection, AnalysisResultsType.SearchType SearchType, string PathXML)
        {
            DataTable dtARwithFindings = null;
            string xmlFile = string.Empty;
            string xmlPathFile = string.Empty;

            DataSet dsARwithFindings;
            string msg = string.Empty;

            switch (Selection)
            {
                case AnalysisResultsType.Selection.Logic_Segments:
                    if (SearchType == AnalysisResultsType.SearchType.Keywords)
                    {
                        xmlFile = "KeywordsFound2.xml";
                        xmlPathFile = Path.Combine(PathXML, xmlFile);

                       
                        if (!File.Exists(xmlPathFile))
                        {
                            msg = string.Concat("Unable to find Analysis Results Keywords detail file: ", xmlPathFile);
                            MessageBox.Show(msg, "Keywords Detail Cannot be Displayed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return null;
                        }

                        dsARwithFindings = Atebion.Common.Files.LoadDatasetFromXml(xmlPathFile);
                        if (dsARwithFindings == null)
                        {
                            msg = string.Concat("Unable to read Analysis Results Keywords detail file: ", xmlPathFile);
                            MessageBox.Show(msg, "Keywords Detail Cannot be Displayed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return null;       
                        }

                        dtARwithFindings = dsARwithFindings.Tables[0];

                    }

                    break;

            }

            return dtARwithFindings;
        }

        private void dvgParsedResults_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dvgParsedResults.Columns[e.ColumnIndex].Name == "Notes")
            {

                if (dvgParsedResults.Rows[e.RowIndex].Cells["UID"].Value == null)
                    return;

                string sUID = dvgParsedResults.Rows[e.RowIndex].Cells["UID"].Value.ToString();


                string noteFile = string.Concat(sUID, _NotesPostFix, ".rtf");
                string notePathFile = Path.Combine(_PathNotes, noteFile);
                if (File.Exists(notePathFile))
                {
                    e.Value = _Notepad;
                }
                else
                {
                    dvgParsedResults.Columns["Notes"].DefaultCellStyle.NullValue = null;
                }
            } 
        }

        private void ShowParsedDataPerCurrentRow()
        {
            Cursor.Current = Cursors.WaitCursor; // Waiting 

          //  butSpeacker.Visible = false;

            DataGridViewRow row = dvgParsedResults.CurrentRow;

            if (row == null) // Check, sometimes data has not been loaded yet
            {
                Cursor.Current = Cursors.Default; // Back to normal
                return;
            }

            _UID = row.Cells["UID"].Value.ToString();
            if (_UID == string.Empty)
            {
                richTextBox1.Text = string.Empty;
                Cursor.Current = Cursors.Default; // Back to normal
                return; // Most likely last row, which is empty
            }

            string file = row.Cells["FileName"].Value.ToString();
            if (file == string.Empty)
            {
                file = string.Concat(_UID, ".rtf");
            }

            _FileName = Path.Combine(_pathParseSec, file); // ToDo: Fix Error -- Null --Combine: When clicking on the Cancel button                 

            if (File.Exists(_FileName)) 
            {

                if (Files.FileIsLocked(_FileName)) // Added 11.02.2013
                {
                    string msg = "The selected document is currently opened by another application. Please close this document file and try again.";
                    MessageBox.Show(msg, "Unable to Open this Document", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    richTextBox1.Text = msg;

                    return;

                }

                richTextBox1.LoadFile(_FileName);

                // Make Text Larger -- Easier to Read
                richTextBox1.SelectAll();
                richTextBox1.SelectionFont = new Font("Segoe UI", 12);
                richTextBox1.DeselectAll();

                // Check to see if Siarad exists, show button if it does 
                //string appPath = System.Windows.Forms.Application.StartupPath;
                //_Siarad = string.Concat(appPath, @"\Siarad.exe");
                //if (File.Exists(_Siarad))
                //{
                //    butSpeacker.Visible = true;
                //}
                //else
                //{
                //    butSpeacker.Visible = false;
                //}
            }
            else
            {
                richTextBox1.Text = string.Empty;
                richTextBox1.Text = string.Concat("Error: Cannot find file: ", _FileName);

            }

            // Check if there is a Warning for the selected section
          //  string resultsUID = row.Cells["UID"].Value.ToString();

            this.ucNotes1.UID = _UID;

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
                //if (row.Cells["LineStart"].Value != null)
                if (row.Cells["LineStart"].Value.ToString() != string.Empty) // Changed on 12.19.2018
                {
                    line = Convert.ToInt32(row.Cells["LineStart"].Value);
                }
                line++; // parser engine is zero based, while document viewer is one base. Therefore add one.


                _frmDocumentView.ShowSegment(line, richTextBox1.Text);
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

        private void dvgParsedResults_SelectionChanged(object sender, EventArgs e)
        {
            ShowParsedDataPerCurrentRow();
        }

        private void dvgParsedResults_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                RefreshParseResults();

                if (lblMessage.Text.IndexOf("Quantity of Segments") > -1)
                    lblMessage.Text = string.Concat("Quantity of Segments: ", dvgParsedResults.RowCount.ToString());
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


            }
            finally
            {
                this.dvgParsedResults.Refresh();
            }


           // string pathFile = string.Concat(AppFolders.DocParsedSecXML, @"\ParseResults.xml");

            GenericDataManger gdManager = new GenericDataManger();

            gdManager.SaveDataXML(_dsParseResults, _ParsedFile);
        
        }

        private void RemoveSec(int index)
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

        private void butDelete_Click(object sender, EventArgs e)
        {
            if (dvgParsedResults.CurrentRow == null)
            {
                return;
            }


            string msg = "Are you sure you want to permanently delete the selected segment?";
            if (MessageBox.Show(msg, "Please Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;   

            RemoveSec(dvgParsedResults.CurrentRow.Index);

            if (lblMessage.Text.IndexOf("Quantity of Segments") > -1)
                lblMessage.Text = string.Concat("Quantity of Segments: ", dvgParsedResults.RowCount.ToString());

            _SegmentsChanged = true;

            Adjust4QualityandNotes(); // Added 05.23.2014
        }

        public void Adjust4QualityandNotes()
        {

            try
            {
                dvgParsedResults.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);

                Application.DoEvents();



                if (dvgParsedResults.Columns.Contains("Number"))
                {
                    dvgParsedResults.Columns["Keywords"].Width = 60;
                }

                if (dvgParsedResults.Columns.Contains(_LongQty))
                {
                    dvgParsedResults.Columns[_LongQty].Width = 35;
                }

                if (dvgParsedResults.Columns.Contains("Keywords"))
                    dvgParsedResults.Columns["Keywords"].Width = 80;

                if (dvgParsedResults.Columns.Contains(ParseResultsFields.NumberLevel))
                    dvgParsedResults.Columns[ParseResultsFields.NumberLevel].Visible = false;

                if (dvgParsedResults.Columns.Contains(ParseResultsFields.OriginalNumber))
                    dvgParsedResults.Columns[ParseResultsFields.OriginalNumber].Visible = false;


                if (dvgParsedResults.Columns.Contains(ParseResultsFields.PageSource))
                {
                    dvgParsedResults.Columns[ParseResultsFields.PageSource].Width = 30;
                    dvgParsedResults.Columns[ParseResultsFields.PageSource].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                if (dvgParsedResults.Columns.Contains("Notes"))
                {
                    dvgParsedResults.Columns["Notes"].Width = 20;
                }

                dvgParsedResults.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;

            }
            catch { }


            Application.DoEvents();
        }

        private void CheckDocumentView()
        {
            if (_isAnalysis)
            {
                if (chkDocView.Checked)
                {
                    string docName = Files.GetFileNameWOExt(_docFileName);

                    string file = string.Concat(docName, ".txt");
                    string currentFilePath = new DirectoryInfo(_PathKeywords).Parent.FullName;
                    string pathFile = Path.Combine(currentFilePath, file);
                    string keywordsPathFile = string.Concat(pathFile);                     
                    
                    if (File.Exists(pathFile))
                    {
                        _frmDocumentView = new frmDocumentView();
                        _frmDocumentView.LoadData(keywordsPathFile, AppFolders.DocParsedSecKeywords, AppFolders.DocName);

                    }
                    else
                    {
                        string msg = string.Concat("Unable to find file: ", keywordsPathFile);
                        MessageBox.Show(msg, "Unable to Display Document", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                        _frmDocumentView = new frmDocumentView();
                        _frmDocumentView.LoadData(keywordsPathFile, AppFolders.DocParsedSecKeywords, AppFolders.DocName);
                    

                    _frmDocumentView.Show();

                    // Show current segment
                    DataGridViewRow row = dvgParsedResults.CurrentRow;
                    if (row == null)
                        return;

                    int line = 0;
                    if (row.Cells["LineStart"].Value.ToString() != string.Empty) // Changed on 12.19.2018
                    {
                        line = Convert.ToInt32(row.Cells["LineStart"].Value);
                    }
                    line++; // parser engine is zero based, while document viewer is one base. Therefore add one.

                    _frmDocumentView.ShowSegment(line, richTextBox1.Text);

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
            else // Default
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
                    if (row == null)
                        return;

                    int line = 0;
                    if (row.Cells["LineStart"].Value.ToString() != string.Empty) // Changed on 12.19.2018
                    {
                        line = Convert.ToInt32(row.Cells["LineStart"].Value);
                    }
                    line++; // parser engine is zero based, while document viewer is one base. Therefore add one.

                    _frmDocumentView.ShowSegment(line, richTextBox1.Text);

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
        }

        private void chkDocView_CheckedChanged(object sender, EventArgs e)
        {
            CheckDocumentView();
        }

        private void butSplit_Click(object sender, EventArgs e)
        {
            if (dvgParsedResults.CurrentRow == null)
                return;

            int curentIndex = dvgParsedResults.CurrentRow.Index;

            DataGridViewRow row = dvgParsedResults.CurrentRow;

            if (row == null)
                return;

            frmSplitSec frm = new frmSplitSec(_FileName, _ParsedFile, _pathXML, _pathParseSec, _SearchType, row.Cells["Number"].Value.ToString(), row.Cells["Caption"].Value.ToString(), row.Cells["UID"].Value.ToString());

            // Show Dialog as a modal dialog and determine if DialogResult = OK.
            try 
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    _SegmentsChanged = true;

                    LoadParseResults();

                    GoToRowParsedResults(curentIndex);

                    //ucQueriesFound1.LoadData(); 
                    //if (queryIndex != -1)
                    //{
                    //    ucQueriesFound1.CurrentIndex = queryIndex;
                    //}

                }
            }
            catch { }

            lblMessage.Text = string.Concat("Quantity of Segments: ", dvgParsedResults.RowCount.ToString()); // Added 10.03.2013

            Adjust4QualityandNotes();

            if (!AppFolders.UseHootSearchEngine)
                ucSearch1.ChkRunIndexer(); // Re-index because an error occurs after combining w/ the Seach Engine if not re-Indexed
            else
                ucSearch_hoot1.RunIndexer();

            frm = null;

        }

        private void GoToRowParsedResults(int index)
        {
            if (index < 0) 
                index = 0;

            try 
            {
                int columnIndex = FirstCompletelyVisibleColumnIndex();
                //  dvgParsedResults.CurrentCell = dvgParsedResults[10, index];
                dvgParsedResults.CurrentCell = dvgParsedResults[columnIndex, index];
                dvgParsedResults.Rows[index].Selected = true;
                ShowParsedDataPerCurrentRow();
            }
            catch
            {
                return;
            }

            
        }

        private int FirstCompletelyVisibleColumnIndex() 
        {
            // Number of visible columns.
            int count = -1;
            foreach (DataGridViewColumn column in dvgParsedResults.Columns)
            {
                if (column.Visible)
                {
                    count++;
                    return column.Index;
                }

            }

            return count;
        }

        private void butCombine_Click(object sender, EventArgs e)
        {
            if (dvgParsedResults.CurrentRow == null) 
            {
                return;
            }

            int curentIndex = dvgParsedResults.CurrentRow.Index;

            if (curentIndex < 1)  // Changed 10/23/2015
            {
                string msgX = "To combine segments, select the bottom to combine with the top.";
                MessageBox.Show(msgX, "No Top Section to Combine With", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        //    int queryIndex = ucQueriesFound1.CurrentIndex; // Added 12/12/2016

            DataView dv = (DataView)dvgParsedResults.DataSource;

            frmCombinemultiSec frm = new frmCombinemultiSec(dv, curentIndex, _ParsedFile, _pathXML, _pathParseSec, _SearchType);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                _SegmentsChanged = true;

                dvgParsedResults.DataSource = null;

                LoadParseResults();

                GoToRowParsedResults(curentIndex);

                //ucQueriesFound1.LoadData(); // Added 12.12.2016
                //if (queryIndex != -1)
                //{
                //    ucQueriesFound1.CurrentIndex = queryIndex;
                //}
            }

            lblMessage.Text = string.Concat("Quantity of Segments: ", dvgParsedResults.RowCount.ToString());

            Adjust4QualityandNotes();

            if (!AppFolders.UseHootSearchEngine)
                ucSearch1.ChkRunIndexer(); // Re-index because an error occurs after combining w/ the Seach Engine if not re-Indexed
            else
                //ucSearch_hoot1.RunIndexer();

            frm = null;
        }

        private void ResetSubpanels()
        {
            butSearch.BackColor = Color.WhiteSmoke;
            butSearch.ForeColor = Color.Black;
            ucSearch1.Visible = false;
            ucSearch_hoot1.Visible = false;

            butNotes.BackColor = Color.WhiteSmoke;
            butNotes.ForeColor = Color.Black;
            ucNotes1.Visible = false;

            butKwDicCon.BackColor = Color.WhiteSmoke;
            butKwDicCon.ForeColor = Color.Black;
            ucKwDicCon1.Visible = false;

            butExported.BackColor = Color.WhiteSmoke;
            butExported.ForeColor = Color.Black;
            ucExported1.Visible = false;

            this.Refresh();
            
        }

        private void butSearch_MouseEnter(object sender, EventArgs e)
        {
            mouseEnter(butSearch);
        }

        private void butSearch_MouseLeave(object sender, EventArgs e)
        {
            if (_subPanel != subPanels.Search)
            {
                butSearch.BackColor = Color.WhiteSmoke;
                butSearch.ForeColor = Color.Black;
            }
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            ResetSubpanels();

            _subPanel = subPanels.Search;
            butSearch.BackColor = Color.Teal;
            butSearch.ForeColor = Color.White;

            if (_SearchLoadedOK)
            {
                if (!AppFolders.UseHootSearchEngine)
                {
                    ucSearch1.Visible = true;
                    ucSearch1.Dock = DockStyle.Fill;
                }
                else
                {
                    ucSearch_hoot1.Visible = true;
                    ucSearch_hoot1.Dock = DockStyle.Fill;
                }

            }

            this.Refresh();


        }

        private void mouseEnter(Button button)
        {
            //if (button.BackColor == Color.WhiteSmoke)
            //{
                button.BackColor = Color.Teal;
                button.ForeColor = Color.White;
          //  }

            button.Refresh();
        }

        private void mouseLeave(Button button)
        {
            //if (button.BackColor == Color.Teal)
            //{
                button.BackColor = Color.WhiteSmoke;
                button.ForeColor = Color.Black;
           // }

            button.Refresh();
        }

        private void butNotes_MouseEnter(object sender, EventArgs e)
        {
            mouseEnter(butNotes);
        }

        private void butNotes_MouseLeave(object sender, EventArgs e)
        {
            if (_subPanel != subPanels.Notes)
            {
                mouseLeave(butNotes);
            }
        }

        private void butNotes_Click(object sender, EventArgs e)
        {
            ResetSubpanels();

            _subPanel = subPanels.Notes;
            butNotes.BackColor = Color.Teal;
            butNotes.ForeColor = Color.White;

            ucNotes1.Visible = true;
            ucNotes1.Dock = DockStyle.Fill;

            this.Refresh();
        }

        private void butKwDicCon_MouseEnter(object sender, EventArgs e)
        {
            mouseEnter(butKwDicCon);
        }

        private void butKwDicCon_MouseLeave(object sender, EventArgs e)
        {
            if (_subPanel != subPanels.KwDicCon)
            {
                mouseLeave(butKwDicCon);
            }
        }

        private void butKwDicCon_Click(object sender, EventArgs e)
        {
            ResetSubpanels();

            _subPanel = subPanels.KwDicCon;
            butKwDicCon.BackColor = Color.Teal;
            butKwDicCon.ForeColor = Color.White;

            ucKwDicCon1.Visible = true;
            ucKwDicCon1.Dock = DockStyle.Fill;

            this.Refresh();
        }

        private void butExported_MouseEnter(object sender, EventArgs e)
        {
            mouseEnter(butExported);
        }

        private void butExported_MouseLeave(object sender, EventArgs e)
        {
            if (_subPanel != subPanels.Exported)
            {
                mouseLeave(butExported);
            }
        }

        private void butExported_Click(object sender, EventArgs e)
        {
            ResetSubpanels();

            _subPanel = subPanels.Exported;
            butExported.BackColor = Color.Teal;
            butExported.ForeColor = Color.White;

            ucExported1.Visible = true;
            ucExported1.Dock = DockStyle.Fill;

            this.Refresh();
        }

        private void butEdit_Click(object sender, EventArgs e)
        {
            if (dvgParsedResults.CurrentRow == null)
                return;

            int curentIndex = dvgParsedResults.CurrentRow.Index;

            DataGridViewRow row = dvgParsedResults.CurrentRow;
            if (row == null)
                return;

            string number = row.Cells["Number"].Value.ToString();
            string caption = row.Cells["Caption"].Value.ToString();

            frmEditSec frm = new frmEditSec(_UID, number, caption, _FileName, _ParsedFile);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    _SegmentsChanged = true;

                    LoadParseResults();

                    GoToRowParsedResults(curentIndex);
                }
                catch{}
            }

            this.Refresh();
        }

        private void ucAnalysisResults_Paint(object sender, PaintEventArgs e)
        {
            Adjust4QualityandNotes();
        }


        private void SearchFilter(string[] UIDResults, string SearchCriteria)
        {
            //_FoundQty = ucSearch1.FoundQty;
            //if (_FoundQty == 0)
            //    return;

            //string[] UIDResults = ucSearch1.FoundResults;

            //_SearchCriteria = ucSearch1.SearchCriteria;

            _SearchCriteria = SearchCriteria;

            string filter = string.Empty;
            int i = 0;
            foreach (string s in UIDResults)
            {
                if (i == 0)
                    filter = s;
                else
                    filter = string.Concat(filter, ", ", s);

                i++;
            }

            filter = string.Concat("UID IN (", filter, ")");

            _dv = new DataView(_dsParseResults.Tables[0]);

            _dv.RowFilter = filter;
            _dv.Sort = "SortOrder ASC";

            this.dvgParsedResults.DataSource = _dv;

            //butSplit.Visible = false;
            //butCombine.Visible = false;
            //butEdit.Visible = false;
            //butExport.Visible = false;
            //butDelete.Visible = false;

            butRefresh.Visible = true;

            lblMessage.Text = string.Format("Search found {0} Segments for Search Criteria: '{1}'", _FoundQty.ToString(), _SearchCriteria);

            Application.DoEvents();

            Adjust4QualityandNotes();

            this.Refresh();

        }

        private void ucSearch1_SearchCompleted()
        {
            _FoundQty = ucSearch1.FoundQty;
            if (_FoundQty == 0)
                return;

            string[] UIDResults = ucSearch1.FoundResults;

           // _SearchCriteria = ucSearch1.SearchCriteria;

            string searchCriteria = ucSearch1.SearchCriteria;

            SearchFilter(UIDResults, searchCriteria);

            //string filter = string.Empty;
            //int i = 0;
            //foreach (string s in UIDResults)
            //{
            //    if (i == 0)
            //        filter = s;
            //    else
            //        filter = string.Concat(filter, ", ", s);

            //    i++;
            //}

            //filter = string.Concat("UID IN (", filter, ")");

            //_dv = new DataView(_dsParseResults.Tables[0]);

            //_dv.RowFilter = filter;
            //_dv.Sort = "SortOrder ASC"; 

            //this.dvgParsedResults.DataSource = _dv;

            ////butSplit.Visible = false;
            ////butCombine.Visible = false;
            ////butEdit.Visible = false;
            ////butExport.Visible = false;
            ////butDelete.Visible = false;
            
            //butRefresh.Visible = true;

            //lblMessage.Text = string.Format("Search found {0} Segments for Search Criteria: '{1}'", _FoundQty.ToString(), _SearchCriteria);

            //Application.DoEvents();

            //Adjust4QualityandNotes();

            //this.Refresh();
        }

        private void ucKwDicCon1_FilterCompleted()
        {
            _FoundQty = ucKwDicCon1.FilterCount;

            if (_FoundQty == 0)
                return;

            List<string> UIDResults = ucKwDicCon1.FilterUIDs;

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

            //// Filter Status Display -- Keyword Filter
            //ucKwDicCon1.Count = _FoundQty;
            //ucKwDicCon1.CurrentMode = ucKwDicCon1.Modes.Keywords;
            //ucKwDicCon1.UpdateStatusDisplay();

            switch (_SearchType)
            {
                case AnalysisResultsType.SearchType.Concepts:
                    lblMessage.Text = string.Concat("Filter per Selected Concepts - Quantity of Segments: ", dvgParsedResults.RowCount.ToString()); 
                    break;


                case AnalysisResultsType.SearchType.Dictionary:
                    lblMessage.Text = string.Concat("Filter per Selected Dictionary Terms - Quantity of Segments: ", dvgParsedResults.RowCount.ToString()); 
                    break;

                case AnalysisResultsType.SearchType.Keywords:
                    lblMessage.Text = string.Concat("Filter per Selected Keywords - Quantity of Segments: ", dvgParsedResults.RowCount.ToString()); 


                    break;

                case AnalysisResultsType.SearchType.FAR_DFAR:
                    lblMessage.Text = string.Concat("Filter per Selected FARs/DFARs - Quantity of Segments: ", dvgParsedResults.RowCount.ToString()); 
                    break;

            }

            butRefresh.Visible = true;

            this.Refresh();
        }

        private void ShowAllResults()
        {
            LoadParseResults();
            //butSplit.Visible = true;
            //butCombineMulti.Visible = true;
            ////   butCombine.Visible = true;
            //butEditSec.Visible = true;
            //butRemoveUsers.Visible = true;
            butRefresh.Visible = false;
            lblMessage.Text = string.Empty;


            lblMessage.Text = string.Concat("Quantity of Segments: ", dvgParsedResults.RowCount.ToString()); 

            Adjust4QualityandNotes(); // Added 05.23.2014

            this.Refresh();
        }

        private string GetCurrentDocument(string path)
        {
            if (path == string.Empty)
                return string.Empty;

            string txtFile = string.Empty;
            string otherTextFile = string.Empty;

            string[] files = Directory.GetFiles(path);

            if (files.Length == 0)
                return string.Empty;


            string ext = string.Empty;
            foreach (string file in files)
            {
                Files.GetFileName(file, out ext);
                if (ext.ToLower() == ".txt")
                {
                    txtFile = file;
                }
                else
                {
                    otherTextFile = file;
                }
            }

            if (otherTextFile != string.Empty)
            {
                return otherTextFile;
            }

            return txtFile;

        }

        private void butRefresh_Click(object sender, EventArgs e)
        {
            ShowAllResults();

            if (!AppFolders.UseHootSearchEngine)
                ucSearch1.ShowInstructions();
            else
                ucSearch_hoot1.ShowInstructions();

        }

        private void lblMessage_TextChanged(object sender, EventArgs e)
        {
            txtbMessage.Text = lblMessage.Text;
        }

        private void txtbMessage_TextChanged(object sender, EventArgs e)
        {
            txtbMessage.Text = lblMessage.Text;
        }

        private void dvgParsedResults_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
 
        }

        private void butDeepAnalysis_Click(object sender, EventArgs e)
        {
            if (RunDeepAnalysisResults != null)
                RunDeepAnalysisResults();
        }

        private void butExport_Click(object sender, EventArgs e)
        {
            frmExportSec frm;

            if (dvgParsedResults.RowCount == 0)
                return;

            bool pageColExists = dvgParsedResults.Columns.Contains(ParseResultsFields.PageSource); // Added 8.15.2018
            // Open Edit Dialog
            if (butRefresh.Visible)
            {
   

                //frm = new frmExportSec();
                //frm.LoadData(_dv.ToTable(), _FoundQty, _SearchCriteria, false, pageColExists);
            }
            else
            {
                _FoundQty = 0;
                _SearchCriteria = string.Empty;

                //_dv = (DataView)(dvgParsedResults.DataSource);

                //frm = new frmExportSec();
                //frm.LoadData(_dv.ToTable(), pageColExists);
            }

            _dv = (DataView)(dvgParsedResults.DataSource);

            frm = new frmExportSec();
            string pathHTML = Path.Combine(_pathParseSec, "HTML");
            string pathXML = Path.Combine(_pathParseSec, "XML");
            string ext = string.Empty;
            string docName = Files.GetFileName(_docFileName, out ext);
            frm.LoadData(_dv.ToTable(), _FoundQty, _SearchCriteria, pageColExists, AppFolders.ProjectName, docName, _pathParseSec, _PathExport, pathHTML, pathXML, _PathNotes);   

        //    frm.LoadData(_dv.ToTable(), pageColExists);


            // Show Dialog as a modal dialog and determine if DialogResult = OK.
            frm.ShowDialog(this);

            // Call Section Export 
            ucExported1.LoadData(_PathExport);
            int count = ucExported1.Count;
           // lblExportQty.Text = count.ToString();
            
        }

        private void ucSearch_hoot1_SearchCompleted()
        {
            _FoundQty = ucSearch_hoot1.FoundQty;
            if (_FoundQty == 0)
                return;

            string[] UIDResults = ucSearch_hoot1.FoundResults;

            string searchCriteria = ucSearch_hoot1.SearchCriteria;

            SearchFilter(UIDResults, searchCriteria);
        }

  
    }
}
