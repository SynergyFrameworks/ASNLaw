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

using Atebion_Dictionary;
using Atebion.Common;
using Atebion.ConceptAnalyzer;
using Atebion.CovertDoctoRTF;


namespace ProfessionalDocAnalyzer
{
    public partial class ucResultsConcepts : UserControl
    {
        public ucResultsConcepts()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        private Atebion.ConceptAnalyzer.Analysis _CAMgr;
        private string _ProjectName = string.Empty;
        private string _AnalysisName = string.Empty;
        private string _DocumentName = string.Empty;


        private string _ConceptParseSegPath = string.Empty;
        private string _NotesPath = string.Empty;

        private DataSet _dsConceptsAnalysisResults;

        private string _Siarad = string.Empty;

        private Image _Notepad;

        private int _FoundQty = 0;
        private DataView _dv;

        private bool _Refresh = false;

        private string _ReportPath = string.Empty;

        private bool _PageNumbersExists = false;

        public void adjColumns()
        {
            try
            {
                dvgParsedResults.Columns["UID"].Visible = false;
                dvgParsedResults.Columns["SortOrder"].Visible = false;
                dvgParsedResults.Columns["FileName"].Visible = false;
                

                dvgParsedResults.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dvgParsedResults.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;

                dvgParsedResults.Columns["Number"].Width = 50;

                if (_PageNumbersExists) // Added 8.14.2018
                {
                    dvgParsedResults.Columns["Page"].Visible = true;
                    dvgParsedResults.Columns["Page"].Width = 40;
                    dvgParsedResults.Columns["Page"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                else
                {
                    dvgParsedResults.Columns["Page"].Visible = false;
                }
                
                //dvgParsedResults.Columns["Caption"].Width = 50;

   

                if (!dvgParsedResults.Columns.Contains("Notes")) //Added 05.02.2014       
                {
                    DataGridViewImageColumn imageCol = new DataGridViewImageColumn();

                    imageCol.HeaderText = "";
                    imageCol.Name = "Notes";

                    dvgParsedResults.Columns.Add(imageCol);

                }

                dvgParsedResults.Columns["Notes"].Visible = true;
                dvgParsedResults.Columns["Notes"].Width = 20;


                dvgParsedResults.AllowUserToAddRows = false; // Remove blank last row

                ucResultsConceptItemsFilter1.adjColumns();

            }
            catch
            { }

        }

        public bool LoadData(Atebion.ConceptAnalyzer.Analysis CAMgr, string ProjectName, string AnalysisName, string DocumentName)
        {
            //if (!_Refresh)
            //{
            //    if (_ProjectName == ProjectName && _ProjectName == ProjectName)
            //    {
            //        return true; // No need to reload
            //    }
            //}

            _Notepad = Image.FromFile(Path.Combine(Application.StartupPath, "Notepad16x16.jpg"));
            //_Blank = Image.FromFile(Path.Combine(Application.StartupPath, "Blank16x16.jpg"));
            //_BlankGrey = Image.FromFile(Path.Combine(Application.StartupPath, "BlankGrey16x16.jpg"));

            _CAMgr = CAMgr;
            _ProjectName = ProjectName;
            _DocumentName = DocumentName;
            _AnalysisName = AnalysisName;


            _dsConceptsAnalysisResults = _CAMgr.Get_Document_Concept_AnalysisResults(_ProjectName, AnalysisName, _DocumentName, out _ConceptParseSegPath, out _NotesPath);

            if (_dsConceptsAnalysisResults == null)
            {
                MessageBox.Show(_CAMgr.ErrorMessage, "Unable to Display Concepts Analysis Results", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

           // lblHeader.Text = string.Concat("Concepts            Parsed Segments/Paragraphs [", _dsConceptsAnalysisResults.Tables[0].Rows.Count.ToString(), "]");

            _dv = _dsConceptsAnalysisResults.Tables[0].DefaultView;

            this.dvgParsedResults.DataSource = _dv;

            Check4PageNos(_dsConceptsAnalysisResults.Tables[0]);

            ucResultsConceptItemsFilter1.LoadData(_CAMgr, ProjectName, AnalysisName, DocumentName);

            ucResultsNotes1.Prefix = string.Empty;
            ucResultsNotes1.LoadData(_NotesPath);

            // ToDo load Notes & Dictionary item found filter

            butGenerateReport.Visible = true;

            return true;

        }

        private void Check4PageNos(DataTable dt) // Added 8.14.2018
        {
            _PageNumbersExists = false;

            if (dt == null)
                return;

            if (dt.Rows.Count == 0)
                return;

            foreach (DataRow row in dt.Rows)
            {
                if (row["Page"] == null)
                    return;

                if (row["Page"].ToString().Trim() == string.Empty)
                    return;

                _PageNumbersExists = true;
            }



        }

        public void Export()
        {

            frmExport frm = new frmExport();

            if (frm.Load_ExportConceptsDoc(_CAMgr, _dv.ToTable(), _ProjectName, _AnalysisName, _DocumentName))
            {
                frm.ShowDialog(this);
            }
            else
            {
                string error = frm.ErrorMessage;
                MessageBox.Show(error, "Unable to Open Export Window", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }

        public int GetReportsCount()
        {

            _CAMgr.GetNext_ExportTemps_ConceptsDoc_ReportName(_ProjectName, _DocumentName, out _ReportPath);

            if (_ReportPath == string.Empty)
            {
                MessageBox.Show("Unable to find the Exported Reports folder.", "Exported Reports Folder not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            string[] files = Directory.GetFiles(_ReportPath, "*.xlsx");

            return files.Length;
        }

        public void ShowExportedReports()
        {

            frmExported frm = new frmExported();
            string subject = string.Concat("Concept Analysis for Document: ", _DocumentName);
            frm.LoadData(_ReportPath, subject);
            frm.ShowDialog(this);
            
        }

        private void dvgParsedResults_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dvgParsedResults.Columns[e.ColumnIndex].Name == "Notes")
            {
                // Your code would go here - below is just the code I used to test 

                if (dvgParsedResults.Rows[e.RowIndex].Cells["UID"].Value == null)
                    return;

                string sUID = dvgParsedResults.Rows[e.RowIndex].Cells["UID"].Value.ToString();
                string file = string.Concat(sUID, ".rtf");
                string noteFile = Path.Combine(_NotesPath, file);
                if (File.Exists(noteFile))
                {
                    e.Value = _Notepad;
                }
                else
                {
                    dvgParsedResults.Columns["Notes"].DefaultCellStyle.NullValue = null;

                }
            } 
        }

        public void ShowAll()
        {
            Cursor.Current = Cursors.WaitCursor; // Waiting 

            _Refresh = true;

            LoadData(_CAMgr, _ProjectName, _AnalysisName, _DocumentName);
            adjColumns();

            _Refresh = false;

            Cursor.Current = Cursors.Default; // Back to normal
        }

        public void SearchShow(string[] FoundFiles)
        {
            string filter = string.Empty;
            int i = 0;
            string uid = string.Empty;
            foreach (string s in FoundFiles)
            {
                //uid = string.Concat("'", s, "'");
                uid = s;
                if (i == 0)
                    filter = uid;
                else
                    filter = string.Concat(filter, ", ", uid);

                i++;
            }

            filter = string.Concat("UID IN (", filter, ")");

            _dv = new DataView(_dsConceptsAnalysisResults.Tables[0]);

            _dv.RowFilter = filter;
            _dv.Sort = "SortOrder ASC";

            this.dvgParsedResults.DataSource = _dv;
            this.dvgParsedResults.Refresh();

            _FoundQty = _dv.Count;

        }

        private void dvgParsedResults_SelectionChanged(object sender, EventArgs e)
        {
            ShowParsedDataPerCurrentRow();
        }

        private void ShowParsedDataPerCurrentRow()
        {
            Cursor.Current = Cursors.WaitCursor; // Waiting 

            butSpeacker.Visible = false;

            DataGridViewRow row = dvgParsedResults.CurrentRow;

            if (row == null) // Check, sometimes data has not been loaded yet
            {
                Cursor.Current = Cursors.Default; // Back to normal
                return;
            }

            //  string fileName = path + row.Cells["FileName"].Value.ToString();
            string fileName = Path.Combine(_ConceptParseSegPath, row.Cells["FileName"].Value.ToString()); // ToDo: Fix Error -- Null --Combine: When clicking on the Cancel button 

            //  if (row.Cells[0].Value.ToString() == string.Empty)
            if (row.Cells["UID"].ToString() == string.Empty) // 10.06.2013
            {
                richTextBox1.Text = string.Empty;
                Cursor.Current = Cursors.Default; // Back to normal
                return; // Most likely last row, which is empty
            }

            if (File.Exists(fileName)) // added 05.25.2013
            {

                if (Files.FileIsLocked(fileName)) // Added 11.02.2013
                {
                    string msg = "The selected document is currently opened by another application. Please close this document file and try again.";
                    MessageBox.Show(msg, "Unable to Open this Document", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    richTextBox1.Text = msg;

                    return;

                }

                richTextBox1.LoadFile(fileName);

                // Make Text Larger -- Easier to Read
                richTextBox1.SelectAll();
                richTextBox1.SelectionFont = new Font("Segoe UI", 12);
                richTextBox1.DeselectAll();

                //_FileName = fileName;

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
                richTextBox1.Text = string.Concat("Error: Cannot find file: ", fileName);

            }

            // Check if there is a Warning for the selected section
            string resultsUID = row.Cells["UID"].Value.ToString();

            this.ucResultsNotes1.UID = resultsUID;


            Cursor.Current = Cursors.Default; // Back to normal

        }

        private void ucResultsConceptItemsFilter1_FilterCompleted()
        {
            _FoundQty = ucResultsConceptItemsFilter1.FilterCount;

            if (_FoundQty == 0)
                return;

            List<string> UIDResults = ucResultsConceptItemsFilter1.FilterResults;

            List<string> distinct = UIDResults.Distinct().ToList();

            distinct.Sort();

            string filter = string.Empty;
            int i = 0;
            string uid = string.Empty;
            foreach (string s in distinct)
            {
                //uid = string.Concat("'", s, "'");
                uid = s.Trim();
                if (i == 0)
                    filter = string.Concat("'", uid, "'");
                else
                    filter = string.Concat(filter, ", '", uid, "'");

                i++;
            }

            filter = string.Concat("UID IN (", filter, ")");

            _dv = new DataView(_dsConceptsAnalysisResults.Tables[0]);

            _dv.RowFilter = filter;
            _dv.Sort = "SortOrder ASC";

            this.dvgParsedResults.DataSource = _dv;
            this.dvgParsedResults.Refresh();

            _FoundQty = distinct.Count; // Reset to get count of rows, not search items found 

            // Filter Status Display -- Keyword Filter
            //ucDeepAnalyticsFilterDisplay1.Count = _FoundQty;
            //ucDeepAnalyticsFilterDisplay1.CurrentMode = ucDeepAnalyticsFilterDisplay.Modes.Keywords;
            //ucDeepAnalyticsFilterDisplay1.UpdateStatusDisplay();

            ucResultsConceptItemsFilter1.RefreshButton_Show();
        }

        private void ucResultsConceptItemsFilter1_ShowAll()
        {
            ShowAll();
        }

        private void dvgParsedResults_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            adjColumns();
        }

        private void butGenerateReport_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            frmExport frm = new frmExport();
            try
            {
                DataTable dt1 = new DataTable();
                dt1 = (DataTable)dvgParsedResults.DataSource;
                frm.Load_ExportConceptsDoc(_CAMgr, dt1, _ProjectName, _AnalysisName, _DocumentName);
            }
            catch (Exception Ex)
            {
                DataView dv2 = (DataView)dvgParsedResults.DataSource;
                frm.Load_ExportConceptsDoc(_CAMgr, dv2.ToTable(), _ProjectName, _AnalysisName, _DocumentName);
            }
            Cursor.Current = Cursors.Default;
            frm.ShowDialog();
        }

        private void butGenDic_Click(object sender, EventArgs e)
        {

            frmConcepts2Dic frm = new frmConcepts2Dic();
            frm.LoadData(_CAMgr, _ProjectName, _AnalysisName, _DocumentName);

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                string msg = string.Concat("The Concepts identified in this document ", _DocumentName, " was saved to a Dictionary.");
            }
        }

    }
}
