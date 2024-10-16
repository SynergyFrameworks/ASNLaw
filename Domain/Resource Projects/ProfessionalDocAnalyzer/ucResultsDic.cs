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
    public partial class ucResultsDic : UserControl
    {
        public ucResultsDic()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        private Atebion.ConceptAnalyzer.Analysis _CAMgr;
        private string _ProjectName = string.Empty;
        private string _AnalysisName = string.Empty;
        private string _DocumentName = string.Empty;
        private string _DictionaryName = string.Empty;

        private string _AnalysisResultsPath = string.Empty;
        private string _NotesPath = string.Empty;

        private DataSet _dsDicAnalysisResults;

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
                dvgParsedResults.Columns["Parameter"].Visible = false;
                dvgParsedResults.Columns["Parent"].Visible = false;
                dvgParsedResults.Columns["LineStart"].Visible = false;
                dvgParsedResults.Columns["LineEnd"].Visible = false;
                dvgParsedResults.Columns["SectionLength"].Visible = false;
                dvgParsedResults.Columns["ColumnStart"].Visible = false;
                dvgParsedResults.Columns["ColumnEnd"].Visible = false;
                dvgParsedResults.Columns["IndexStart"].Visible = false;
                dvgParsedResults.Columns["IndexEnd"].Visible = false;
                dvgParsedResults.Columns["SortOrder"].Visible = false;
                dvgParsedResults.Columns["FileName"].Visible = false;

                if (dvgParsedResults.Columns.Contains("Keywords"))
                    dvgParsedResults.Columns["Keywords"].Visible = false;

                if (dvgParsedResults.Columns.Contains("Concepts"))
                    dvgParsedResults.Columns["Concepts"].Visible = false;

                dvgParsedResults.Columns["Dictionary"].Visible = false;

                if (dvgParsedResults.Columns.Contains("DicCat"))
                    dvgParsedResults.Columns["DicCat"].Visible = false;

                dvgParsedResults.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dvgParsedResults.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;

                //dvgParsedResults.Columns["Number"].Width = 35;
                //dvgParsedResults.Columns["Page"].Width = 25;
                //dvgParsedResults.Columns["DicItems"].Width = 40;
                //dvgParsedResults.Columns["Weight"].Width = 30;
                //dvgParsedResults.Columns["Caption"].Width = 50;

                dvgParsedResults.Columns["Weight"].Width = 50;
                dvgParsedResults.Columns["Caption"].Width = 100;
                dvgParsedResults.Columns["DicItems"].Width = 70;

                if (_PageNumbersExists) // Added 8.14.2018
                {
                    dvgParsedResults.Columns["Page"].Visible = true;
                    dvgParsedResults.Columns["Page"].Width = 40;
                    dvgParsedResults.Columns["Page"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                else
                {
                    if (dvgParsedResults.Columns.Contains("Page"))
                        dvgParsedResults.Columns["Page"].Visible = false;
                }

                dvgParsedResults.Columns["Number"].Width = 50;


                dvgParsedResults.Columns["DicItems"].HeaderText = "Dic. Items [Qty]";
                dvgParsedResults.Columns["DicDefs"].HeaderText = "(Category) Item - Definition";

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

                Double dbWeight = 0;

                foreach (DataGridViewRow row in dvgParsedResults.Rows)
                {
                    //if (row.Cells["DicItems"].Value == null)
                    //{
                    //    row.DefaultCellStyle.BackColor = Color.LightGray;
                    //}
                    if (row.Cells["DicItems"].Value.ToString() == string.Empty)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                    }
                    else
                    {
                        if (row.Cells["Weight"].Value.ToString() != string.Empty)
                        {
                            dbWeight = Convert.ToDouble(row.Cells["Weight"].Value);

                            if (dbWeight == 0)
                                row.Cells["Weight"].Style.BackColor = Color.LightGray;
                            else if (dbWeight > 0 && dbWeight < 1)
                                row.Cells["Weight"].Style.BackColor = Color.GreenYellow;
                            else if (dbWeight > .99 && dbWeight < 3)
                                row.Cells["Weight"].Style.BackColor = Color.LightGreen;
                            else if (dbWeight > 2.99 && dbWeight < 5)
                            {
                                row.Cells["Weight"].Style.BackColor = Color.Green;
                                row.Cells["Weight"].Style.ForeColor = Color.White;
                            }
                            else if (dbWeight >= 5)
                            {
                                row.Cells["Weight"].Style.BackColor = Color.DarkGreen;
                                row.Cells["Weight"].Style.ForeColor = Color.White;
                            }

                        }
                    }
                }


                ucResultsDicItemsFilter1.adjColumns();
            }
            catch
            { }

        }


        public void Export()
        {
            frmExport frm = new frmExport();

            if (frm.Load_ExportDicDoc(_CAMgr, _dsDicAnalysisResults.Tables[0], _ProjectName, _AnalysisName, _DocumentName))
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
            _CAMgr.GetNext_ExportTemps_DicDoc_ReportName(_ProjectName, _AnalysisName, _DocumentName, out _ReportPath);
         

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
            string subject = string.Concat("Dictionary Analysis for Document: ", _DocumentName);
            frm.LoadData(_ReportPath, subject);
            frm.ShowDialog(this);

        }


        public bool LoadData(Atebion.ConceptAnalyzer.Analysis CAMgr, string ProjectName, string AnalysisName, string DocumentName, string DictionaryName)
        {
            if (!_Refresh)
            {
                if (_ProjectName == ProjectName && _AnalysisName == AnalysisName && _DocumentName == DocumentName)
                {
                    return true; // No need to reload
                }
            }

            _Notepad = Image.FromFile(Path.Combine(Application.StartupPath, "Notepad16x16.jpg"));
            //_Blank = Image.FromFile(Path.Combine(Application.StartupPath, "Blank16x16.jpg"));
            //_BlankGrey = Image.FromFile(Path.Combine(Application.StartupPath, "BlankGrey16x16.jpg"));

            _CAMgr = CAMgr;
            _ProjectName = ProjectName;
            _AnalysisName = AnalysisName;
            _DocumentName = DocumentName;
            _DictionaryName = DictionaryName;

            _dsDicAnalysisResults = _CAMgr.Get_Document_Dic_AnalysisResults(_ProjectName, _AnalysisName, _DocumentName, out _AnalysisResultsPath, out _NotesPath);

            if (_dsDicAnalysisResults == null)
            {
                MessageBox.Show(_CAMgr.ErrorMessage, "Unable to Display Dictionary Analysis Results", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            lblHeader.Text = string.Concat("Dictionary - ", _DictionaryName, "            Parsed Segments/Paragraphs [", _dsDicAnalysisResults.Tables[0].Rows.Count.ToString(), "]");

            this.dvgParsedResults.DataSource = _dsDicAnalysisResults.Tables[0];

            Check4PageNos(_dsDicAnalysisResults.Tables[0]); // Added 8.14.2018 -- If _PageNumbersExists == false Then hide Page column

            ucResultsDicItemsFilter1.LoadData(CAMgr, ProjectName, AnalysisName, DocumentName);

            ucResultsNotes1.Prefix = string.Empty;
            ucResultsNotes1.LoadData(_NotesPath);

            // ToDo load Notes & Dictionary item found filter

            return true;

        }

        private void Check4PageNos(DataTable dt) // Added 8.14.2018
        {
            _PageNumbersExists = false;

            if (dt == null)
                return;

            if (dt.Rows.Count == 0)
                return;

            if (dt.Columns.Contains("Page"))
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Page"] == null)
                        return;

                    if (row["Page"].ToString().Trim() == string.Empty)
                        return;

                    _PageNumbersExists = true;
                }
            }
            else
            {
                _PageNumbersExists = false;
            }


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
            string fileName = Path.Combine(_AnalysisResultsPath, row.Cells["FileName"].Value.ToString()); // ToDo: Fix Error -- Null --Combine: When clicking on the Cancel button 

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

        private void ucResultsDicItemsFilter1_FilterCompleted()
        {
            _FoundQty = ucResultsDicItemsFilter1.FilterCount;

            if (_FoundQty == 0)
                return;

            List<string> UIDResults = ucResultsDicItemsFilter1.FilterResults;

            string filter = string.Empty;
            int i = 0;
            string uid = string.Empty;
            foreach (string s in UIDResults)
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

            _dv = new DataView(_dsDicAnalysisResults.Tables[0]);

            _dv.RowFilter = filter;
            _dv.Sort = "SortOrder ASC";

            this.dvgParsedResults.DataSource = _dv;
            this.dvgParsedResults.Refresh();

            _FoundQty = _dv.Count; // Reset to get count of rows, not search items found -- Fixed Added 07/02/2017

            // Filter Status Display -- Keyword Filter
            //ucDeepAnalyticsFilterDisplay1.Count = _FoundQty;
            //ucDeepAnalyticsFilterDisplay1.CurrentMode = ucDeepAnalyticsFilterDisplay.Modes.Keywords;
            //ucDeepAnalyticsFilterDisplay1.UpdateStatusDisplay();

            ucResultsDicItemsFilter1.RefreshButton_Show();
        }

        private void ucResultsDicItemsFilter1_ShowAll()
        {
            ShowAll();
        }

        public void ShowAll()
        {
            Cursor.Current = Cursors.WaitCursor; // Waiting 

            _Refresh = true;

            LoadData(_CAMgr, _ProjectName, _AnalysisName, _DocumentName, _DictionaryName);
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

            _dv = new DataView(_dsDicAnalysisResults.Tables[0]);

            _dv.RowFilter = filter;
            _dv.Sort = "SortOrder ASC";

            this.dvgParsedResults.DataSource = _dv;
            this.dvgParsedResults.Refresh();

            _FoundQty = _dv.Count;

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
                frm.Load_ExportDicDoc(_CAMgr, dt1, _ProjectName, _AnalysisName, _DocumentName);
            }
            catch (Exception Ex)
            {
                DataView dv2 = (DataView)dvgParsedResults.DataSource;
                frm.Load_ExportDicDoc(_CAMgr, dv2.ToTable(), _ProjectName, _AnalysisName, _DocumentName);
            }

            Cursor.Current = Cursors.Default;

            frm.ShowDialog();

            //ExportManager exportMgr = new ExportManager();
            //string reportName = exportMgr.GetNewRptFileName(AppFolders.AnalysisParseSegExport);

            //if (!_CAMgr.ExportDicDoc(_dsDicAnalysisResults.Tables[0], reportName, AppFolders.AppDataPathToolsExcelTempDicDoc, reportName, AppFolders.ProjectName, AppFolders.AnalysisName, _DocumentName, true))
            //{
            //    string errMsg = string.Concat("Error: ", _CAMgr.ErrorMessage);
            //    MessageBox.Show(errMsg, "Unable to Generate Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    Cursor.Current = Cursors.Default;
            //    return;
            //}

            //  

        }


    }
}
