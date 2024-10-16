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
    public partial class ucResultsMultiConcepts : UserControl
    {
        public ucResultsMultiConcepts()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        private Atebion.ConceptAnalyzer.Analysis _CAMgr;
        private Atebion.ConceptAnalyzer.AnalysisConcepts _AnalysisConceptsMgr;

        DataSet _dsXFilter;
        DataSet _dsConceptsDocsSumResults;

        private string _ProjectName = string.Empty;
        private string _AnalysisName = string.Empty;
        private string _DocumentName = string.Empty;
        private string _AnalysisFolder = string.Empty;

        private string _AnalysisResultsPath = string.Empty;
        private string _NotesPath = string.Empty;
        private string _ReportPath = string.Empty;


        private string _Siarad = string.Empty;

        private Image _Notepad;

        private int _FoundQty = 0;
        private DataView _dv;
        private DataTable _dtSorted;


        private string[] _docs;

        private bool _isLoaded = false;


        public void Export()
        {
            frmExport frm = new frmExport();
            try
            {
                DataTable dt1 = new DataTable();
                dt1 = (DataTable)dvgConceptResults.DataSource;
                //frm.Load_ExportDicDoc(_CAMgr, dt1, _ProjectName, _AnalysisName, _DocumentName);
                if (frm.Load_ExportConceptsDocs(_CAMgr, dt1, _docs, _ProjectName, _AnalysisName))
                {
                    frm.ShowDialog(this);
                }
                else
                {
                    string error = frm.ErrorMessage;
                    MessageBox.Show(error, "Unable to Open Export Window", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception Ex)
            {
                DataView dv2 = (DataView)dvgConceptResults.DataSource;
                if (frm.Load_ExportConceptsDocs(_CAMgr, dv2.ToTable(), _docs, _ProjectName, _AnalysisName))
                {
                    frm.ShowDialog(this);
                }
                else
                {
                    string error = frm.ErrorMessage;
                    MessageBox.Show(error, "Unable to Open Export Window", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                //frm.Load_ExportDicDoc(_CAMgr, dv2.ToTable(), _ProjectName, _AnalysisName, _DocumentName);
            }
        }

        public int GetReportsCount()
        {
            _CAMgr.GetNext_ExportTemps_ConceptsDocs_ReportName(_ProjectName, _AnalysisName, out _ReportPath);
            

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
            string subject = string.Concat("Concept Analysis for Multi-Documents, Analysis: ", _AnalysisName);
            frm.LoadData(_ReportPath, subject);
            frm.ShowDialog(this);

        }


        public bool LoadData(Atebion.ConceptAnalyzer.Analysis CAMgr, string ProjectName, string AnalysisName)
        {
            //if (!_Refresh)
            //{
            //    if (_ProjectName == ProjectName && _AnalysisName == AnalysisName && _DocumentName == DictionaryName)
            //    {
            //        return true; // No need to reload
            //    }
            //}

            _AnalysisConceptsMgr = new Atebion.ConceptAnalyzer.AnalysisConcepts();

            _Notepad = Image.FromFile(Path.Combine(Application.StartupPath, "Notepad16x16.jpg"));

            _CAMgr = CAMgr;
            _ProjectName = ProjectName;
            _AnalysisName = AnalysisName;
            _DocumentName = string.Empty;
            

            _docs = _CAMgr.GetAnalsysFiles(ProjectName, AnalysisName, false);

            string xRefPathFile = string.Empty;

            _AnalysisFolder = string.Empty;

            DataRow rowAnalysisParameters = _CAMgr.GetAnalysisParameters(ProjectName, AnalysisName, false, out _AnalysisFolder);

            ucResultsNotes1.Prefix = "C";
            ucResultsNotes1.LoadData(_AnalysisFolder);

            _NotesPath = _AnalysisFolder;

            List<string> conceptsFound = new List<string>();
            string xRefPath = string.Empty;


            _dsConceptsDocsSumResults = _AnalysisConceptsMgr.Get_Documents_Concept_Summary(ProjectName, AnalysisName, _docs, conceptsFound, out xRefPath, out _dsXFilter);

            if (_dsConceptsDocsSumResults == null) // Added 06.12.2018
            {
                MessageBox.Show("Unable to find Concept Summary Data.", "Data Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (_dsConceptsDocsSumResults.Tables[0].Rows.Count == 0) // Added 06.12.2018
            {
                MessageBox.Show("Unable to find Concept Summary Data.", "Data Rows Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            _isLoaded = true;

            _dv = _dsConceptsDocsSumResults.Tables[0].DefaultView; // ToDo Check for rows
            _dv.Sort = DocsConceptsAnalysisFieldConst.Concept;
            _dtSorted = _dv.ToTable();

            dvgConceptResults.DataSource = _dtSorted;

            DataView dv2 = _dsXFilter.Tables[0].DefaultView;
            dv2.Sort = DocsConceptsAnalysisFieldConst.Concept;
            DataTable dtSorted2 = dv2.ToTable();

            DataSet dsConceptsFiltering = new DataSet();
            dsConceptsFiltering.Tables.Add(dtSorted2);

            ucResultsConceptItemsFilter1.LoadData(dsConceptsFiltering, _CAMgr, _ProjectName, _AnalysisName);
  
            return true;
        }

        public void adjColumns()
        {
            try
            {
                dvgConceptResults.Columns[DocsConceptsAnalysisFieldConst.UID].Visible = false;


                dvgConceptResults.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dvgConceptResults.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;

          //      dvgConceptResults.Columns[DocsConceptsAnalysisFieldConst.CountTotal].Width = 35;
                dvgConceptResults.Columns[DocsConceptsAnalysisFieldConst.CountTotal].Visible = false;


           //     dvgConceptResults.Columns[DocsConceptsAnalysisFieldConst.CountTotal].HeaderText = "Total";


                dvgConceptResults.AllowUserToAddRows = false; // Remove blank last row

                int i = 0;
                string colName = string.Empty;
                string headerNewName = string.Empty;
                string adjDoc = string.Empty;

                foreach (string doc in _docs)
                {
                    adjDoc = Directories.GetLastFolder(doc);
                    colName = string.Concat("Count_", i.ToString());
                    headerNewName = string.Concat(adjDoc, "  [Count]");
                    if (dvgConceptResults.Columns.Contains(colName))
                    {
                        dvgConceptResults.Columns[colName].HeaderText = headerNewName;

                        i++;
                    }
                }

                if (!dvgConceptResults.Columns.Contains("Notes")) //Added 05.02.2014       
                {
                    DataGridViewImageColumn imageCol = new DataGridViewImageColumn();

                    imageCol.HeaderText = "";
                    imageCol.Name = "Notes";

                    dvgConceptResults.Columns.Add(imageCol);

                }

                dvgConceptResults.Columns["Notes"].Visible = true;
                dvgConceptResults.Columns["Notes"].Width = 20;


                dvgConceptResults.AllowUserToAddRows = false; // Remove blank last row


                int count;
                foreach (DataGridViewRow row in dvgConceptResults.Rows)
                {

                    for (int x = 0; x < _docs.Length; x++)
                    {
                        colName = string.Concat(DocsConceptsAnalysisFieldConst.Count_, x.ToString());

                        if (dvgConceptResults.Columns.Contains(colName))
                        {

                            if (row.Cells[colName].Value.ToString() == string.Empty)
                            {
                                row.Cells[colName].Style.BackColor = Color.LightGray;
                            }
                            else
                            {
                                if (row.Cells[colName].Value.ToString() != string.Empty)
                                {
                                    count = Convert.ToInt32(row.Cells[colName].Value);

                                    if (count == 0)
                                        row.Cells[colName].Style.BackColor = Color.LightGray;
                                    else if (count < 10)
                                        row.Cells[colName].Style.BackColor = Color.GreenYellow;
                                    else if (count > 9 && count < 21)
                                        row.Cells[colName].Style.BackColor = Color.LightGreen;
                                    else if (count > 20 && count < 41)
                                    {
                                        row.Cells[colName].Style.ForeColor = Color.White;
                                        row.Cells[colName].Style.BackColor = Color.Green;
                                    }
                                    else if (count >= 41)
                                    {
                                        row.Cells[colName].Style.ForeColor = Color.White;
                                        row.Cells[colName].Style.BackColor = Color.DarkGreen;
                                    }

                                }
                            }
                        }
                    }
                }

                ucResultsConceptItemsFilter1.adjColumns();

            }
            catch
            { }

        }

        private void dvgConceptResults_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (!_isLoaded)
                return;

            if (dvgConceptResults.Columns[e.ColumnIndex].Name == "Notes")
            {
                // Your code would go here - below is just the code I used to test 

                if (dvgConceptResults.Rows[e.RowIndex].Cells["UID"].Value == null)
                    return;

                string sUID = dvgConceptResults.Rows[e.RowIndex].Cells["UID"].Value.ToString();
                string file = string.Concat("C_", sUID, ".rtf");
                string noteFile = Path.Combine(_NotesPath, file);
                if (File.Exists(noteFile))
                {
                    e.Value = _Notepad;
                }
                else
                {
                    dvgConceptResults.Columns["Notes"].DefaultCellStyle.NullValue = null;

                }
            } 
        }

        public void adjParseResultsColumns()
        {
            try
            {
                dvgParsedResults.Columns["UID"].Visible = false;
                dvgParsedResults.Columns["SortOrder"].Visible = false;
                dvgParsedResults.Columns["FileName"].Visible = false;
 

                dvgParsedResults.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dvgParsedResults.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;

                dvgParsedResults.Columns["Number"].Width = 45;
                dvgParsedResults.Columns["Page"].Width = 35;
                //dvgParsedResults.Columns["DicItems"].Width = 40;
                //dvgParsedResults.Columns["Weight"].Width = 30;
                //dvgParsedResults.Columns["Caption"].Width = 50;


                //dvgParsedResults.Columns["DicItems"].HeaderText = "Dic. Items [Qty]";
                //dvgParsedResults.Columns["DicDefs"].HeaderText = "(Category) Item - Definition";

                dvgParsedResults.AllowUserToAddRows = false; // Remove blank last row


               // ucResultsDicItemsFilter1.adjColumns();
            }
            catch
            { }

        }

        private void dvgConceptResults_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            splitContainer4.Visible = false;

            if (e.RowIndex == -1)
                return;

            string resultsUID = dvgConceptResults.Rows[e.RowIndex].Cells["UID"].Value.ToString();
            this.ucResultsNotes1.UID = resultsUID;

            // --> Check the selected cell
            if (dvgConceptResults.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                return;

            string value = dvgConceptResults.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            if (value == "0")
                return;

            if (!DataFunctions.IsNumeric(value))
                return;
            // <--


            string colName = dvgConceptResults.Columns[e.ColumnIndex].Name;
            if (colName.IndexOf("Count_") == 0)
            {
                string[] columSplit = colName.Split('_');
                int docNo = Convert.ToInt32(columSplit[1]); // ToDo Fix: could be a possible error if the file name has "_" in its file name
                //MessageBox.Show(_docs[docNo]); // Test

                string concept = dvgConceptResults.Rows[e.RowIndex].Cells[DocsConceptsAnalysisFieldConst.Concept].Value.ToString();

                // Get Analysis Document Folders               
                AppFolders.ProjectName = _ProjectName;
                AppFolders.AnalysisName = _AnalysisName;
                AppFolders.DocName = _docs[docNo]; // ToDo Check value?

                string AnalysisDocPath = AppFolders.DocNamePath;
                string docXMLPath = AppFolders.AnalysisParseSegXML;
                _AnalysisResultsPath = AppFolders.AnalysisParseSeg;


                if (!Directory.Exists(docXMLPath))
                {
                    string msg = string.Concat("Unable to find the file Document XML folder: ", docXMLPath);
                    MessageBox.Show(msg, "Document XML Folder Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                string dicAnalysisSumXMLFile = Path.Combine(docXMLPath, "ConceptsDocSum.xml");
                string parseResultsXMLFile = Path.Combine(docXMLPath, "ConceptResults.xml");

                DataSet dsParseResults = Files.LoadDatasetFromXml(parseResultsXMLFile);
                DataSet dsDicAnalysisSumXMLFile = Files.LoadDatasetFromXml(dicAnalysisSumXMLFile);
                if (dsDicAnalysisSumXMLFile.Tables.Count == 0)
                {
                    return;
                }
                string selectStatment = string.Concat(DocsConceptsAnalysisFieldConst.Concept, " = '", concept, "'");
                DataRow[] foundConcepts = dsDicAnalysisSumXMLFile.Tables[0].Select(selectStatment);
                
                if (foundConcepts.Length == 0)
                {
                    //string msg = string.Concat("Unable to find the Dictionary Item: ", dicItem);
                    //MessageBox.Show(msg, "Dictionary Item Not Found in Document Summary XML", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                string segmentSumUIDs = foundConcepts[0]["SegmentSumUIDs"].ToString();

                string filter = string.Concat("UID IN (", segmentSumUIDs, ")");

                DataView dv = new DataView(dsParseResults.Tables[0]);

                dv.RowFilter = filter;
                dv.Sort = "SortOrder ASC";

                this.dvgParsedResults.DataSource = dv;
                this.dvgParsedResults.Refresh();

                lblParseResults.Text = string.Concat(Directories.GetLastFolder(_docs[docNo]), "         Concept:  ", concept, "         Rows Qty:  ", dv.Count.ToString());

                //    int FoundQty = dv.Count; // Reset to get count of rows, not search items found -- Fixed Added 07/02/2017

                splitContainer4.Visible = true;

                this.Refresh();

                adjParseResultsColumns();

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
            //string resultsUID = row.Cells["UID"].Value.ToString();

            //this.ucResultsNotes1.UID = resultsUID;


            Cursor.Current = Cursors.Default; // Back to normal

        }

        public void ShowAll()
        {
            Cursor.Current = Cursors.WaitCursor; // Waiting 


            LoadData(_CAMgr, _ProjectName, _AnalysisName);

            adjColumns();

            

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

            filter = string.Concat("Concept IN (", filter, ")");

            _dv = new DataView(_dsConceptsDocsSumResults.Tables[0]);

            _dv.RowFilter = filter;
            _dv.Sort = "Concept ASC";

            this.dvgConceptResults.DataSource = _dv;
            this.dvgConceptResults.Refresh();

            _FoundQty = distinct.Count; // Reset to get count of rows, not search items found 

            // Filter Status Display -- Keyword Filter
            //ucDeepAnalyticsFilterDisplay1.Count = _FoundQty;
            //ucDeepAnalyticsFilterDisplay1.CurrentMode = ucDeepAnalyticsFilterDisplay.Modes.Keywords;
            //ucDeepAnalyticsFilterDisplay1.UpdateStatusDisplay();

            ucResultsConceptItemsFilter1.RefreshButton_Show();

            adjColumns();
        }

        private void ucResultsConceptItemsFilter1_ShowAll()
        {
            ShowAll();
        }

        private void dvgConceptResults_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            adjColumns();
        }

        private void butGenerateReport_Click(object sender, EventArgs e)
        {
            Export();
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
