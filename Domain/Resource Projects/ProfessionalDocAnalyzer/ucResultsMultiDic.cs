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
    public partial class ucResultsMultiDic : UserControl
    {
        public ucResultsMultiDic()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        private Atebion.ConceptAnalyzer.Analysis _CAMgr;
        private string _ProjectName = string.Empty;
        private string _AnalysisName = string.Empty;
        private string _DocumentName = string.Empty;
        private string _DictionaryName = string.Empty;
        private string _AnalysisFolder = string.Empty;

        private string _AnalysisResultsPath = string.Empty;
        private string _NotesPath = string.Empty;
        private string _ReportPath = string.Empty;

        private DataSet _dsDicAnalysisResults;
        private DataSet _dsDicDocsSumResults;
        private DataSet _dsDicFiltering;

        private DataTable _dtSorted;
        private DataTable _dtSums;

        private bool _HasLoaded = false;

        private string _Siarad = string.Empty;

        private Image _Notepad;

        private int _FoundQty = 0;
        private DataView _dv;

        private bool _Refresh = false;

        private string[] _docs;


        public int GetReportsCount()
        {
            _CAMgr.GetNext_ExportTemps_DicDocs_ReportName(_ProjectName, _AnalysisName, out _ReportPath);


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
            string subject = string.Concat("Dictionary Analysis for Multi-Documents, Analysis: ", _AnalysisName);
            frm.LoadData(_ReportPath, subject);
            frm.ShowDialog(this);

        }

        public void Export()
        {
            frmExport frm = new frmExport();

            if (frm.Load_ExportDicDocs(_CAMgr, _dtSorted, _dtSums, _docs, _ProjectName, _AnalysisName))
            {
                frm.ShowDialog(this);
            }
            else
            {
                string error = frm.ErrorMessage;
                MessageBox.Show(error, "Unable to Open Export Window", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        public bool LoadData(Atebion.ConceptAnalyzer.Analysis CAMgr, string ProjectName, string AnalysisName, string DictionaryName)
        {
            _HasLoaded = false;

            if (!_Refresh)
            {
                if (_ProjectName == ProjectName && _AnalysisName == AnalysisName && _DocumentName == DictionaryName)
                {
                    return true; // No need to reload
                }
            }

            _Notepad = Image.FromFile(Path.Combine(Application.StartupPath, "Notepad16x16.jpg"));

            _CAMgr = CAMgr;
            _ProjectName = ProjectName;
            _AnalysisName = AnalysisName;
            _DocumentName = string.Empty;
            _DictionaryName = DictionaryName;

            lblHeader.Text = string.Concat("Dictionary - ", DictionaryName);

            _docs = _CAMgr.GetAnalsysFiles(ProjectName, AnalysisName, false);

            string xRefPathFile = string.Empty;

            _AnalysisFolder = string.Empty;

            DataRow rowAnalysisParameters = _CAMgr.GetAnalysisParameters(ProjectName, AnalysisName, false, out _AnalysisFolder);

            ucResultsNotes1.Prefix = "D";
            ucResultsNotes1.LoadData(_AnalysisFolder);

            _NotesPath = _AnalysisFolder;

          //  _DictionaryName = rowAnalysisParameters[AnalysisUCaseFieldConst.DictionaryName].ToString();

            _dsDicDocsSumResults = _CAMgr.Get_Documents_Dic_Summary(ProjectName, AnalysisName, _docs, _DictionaryName, AppFolders.DictionariesPath, out xRefPathFile, out _dsDicFiltering);

            DataView dv = _dsDicDocsSumResults.Tables[0].DefaultView;
            dv.Sort = string.Concat(DictionariesAnalysisFieldConst.Category, ",", DictionariesAnalysisFieldConst.DicItem);
            _dtSorted = dv.ToTable();

            dvgDicResults.DataSource = _dtSorted;

            DataView dv2 = _dsDicFiltering.Tables[0].DefaultView;
            dv2.Sort = string.Concat(DictionariesAnalysisFieldConst.Category, ",", DictionariesAnalysisFieldConst.DicItem);
            DataTable dtSorted2 = dv2.ToTable();

            DataSet dsDicdsDicFiltering = new DataSet();
            dsDicdsDicFiltering.Tables.Add(dtSorted2);

            ucResultsDicItemsFilter1.LoadData(dsDicdsDicFiltering, _CAMgr, _ProjectName, _AnalysisName);

            LoadDicTotalResults();

            


            return true;
        }

        private void LoadDicTotalResults()
        {
            if (dvgDicResults.Rows.Count == 0)
            {
                dvgDicTotalResults.DataSource = null;
                return;
            }

           
            List<double> weightTotal = new List<double>();
            for (int x = 0; x < _docs.Length; x++)
            {
                weightTotal.Add(0);
            }

            List<int> countTotal = new List<int>();
            for (int x = 0; x < _docs.Length; x++)
            {
                countTotal.Add(0);
            }

            string colName = string.Empty;
            double dbWeight = 0;
            int count = 0;

            foreach (DataGridViewRow row in dvgDicResults.Rows)
            {

                for (int x = 0; x < _docs.Length; x++)
                {
                    colName = string.Concat("WeightTotal_", x.ToString());

                    if (dvgDicResults.Columns.Contains(colName)) // Added 07.11.2020 - check if column exists
                    {

                        if (row.Cells[colName].Value != null)
                        {
                            if (row.Cells[colName].Value.ToString() != string.Empty)
                            {
                                dbWeight = Convert.ToDouble(row.Cells[colName].Value);

                                weightTotal[x] = weightTotal[x] + dbWeight;
                            }
                        }

                        colName = string.Concat("Count_", x.ToString());

                        if (row.Cells[colName].Value != null)
                        {
                            if (row.Cells[colName].Value.ToString() != string.Empty)
                            {
                                count = Convert.ToInt32(row.Cells[colName].Value);

                                countTotal[x] = countTotal[x] + count;
                            }
                        }
                    }
                }
            }

            _dtSums = new DataTable("Totals");

            _dtSums.Columns.Add("Totals", typeof(string));
            for (int x = 0; x < _docs.Length; x++)
            {
                colName = string.Concat("WeightTotal_", x.ToString());
                _dtSums.Columns.Add(colName, typeof(string));

                colName = string.Concat("Count_", x.ToString());
                _dtSums.Columns.Add(colName, typeof(string));

            }
            _dtSums.Columns.Add("Nothing", typeof(string));

            DataRow rowX = _dtSums.NewRow();
            rowX["Totals"] = "Totals";
            for (int x = 0; x < _docs.Length; x++)
            {
                colName = string.Concat("WeightTotal_", x.ToString());
                rowX[colName] = weightTotal[x];

                colName = string.Concat("Count_", x.ToString());
                rowX[colName] = countTotal[x];

            }

            _dtSums.Rows.Add(rowX);

            dvgDicTotalResults.DataSource = _dtSums;

            dvgDicTotalResults.AllowUserToAddRows = false; // Remove blank last row

            dvgDicTotalResults.Columns["Totals"].HeaderText = string.Empty;
            for (int x = 0; x < _docs.Length; x++)
            {
                colName = string.Concat("WeightTotal_", x.ToString());
                dvgDicTotalResults.Columns[colName].HeaderText = "Total Weight";

                colName = string.Concat("Count_", x.ToString());
                dvgDicTotalResults.Columns[colName].HeaderText = "Total Count";
            }


            dvgDicTotalResults.Columns["Nothing"].HeaderText = string.Empty;

          //  dvgDicTotalResults.RowHeadersVisible = false;

            

        }

        public void adjColumns()
        {
            try
            {
                dvgDicResults.Columns["UID"].Visible = false;
                dvgDicResults.Columns["DictionaryName"].Visible = false;
                dvgDicResults.Columns["HighlightColor"].Visible = false;
                dvgDicResults.Columns["Definition"].Visible = false;

                dvgDicResults.Columns["DicItem"].HeaderText = "Item";
                

                dvgDicResults.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dvgDicResults.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;

  

                //dvgDicResults.Columns["Weight"].Width = 50;
                //dvgDicResults.Columns["Caption"].Width = 100;
                //dvgDicResults.Columns["DicItems"].Width = 70;
                //dvgDicResults.Columns["Page"].Width = 30;
                //dvgDicResults.Columns["Number"].Width = 50;

                dvgDicResults.ColumnHeadersHeight = 80;

                int i = 0;
                string colName = string.Empty;
                string headerNewName = string.Empty;
                string adjDoc = string.Empty;

                foreach (string doc in _docs)
                {
                    adjDoc = Directories.GetLastFolder(doc);
                    colName = string.Concat("Count_", i.ToString());
                    headerNewName = string.Concat(adjDoc, "  [Count]");
                    if (dvgDicResults.Columns.Contains(colName))
                    {
                        dvgDicResults.Columns[colName].HeaderText = headerNewName;
                    }

                    colName = string.Concat("WeightTotal_", i.ToString());
                    headerNewName = string.Concat(adjDoc, "  [Weight]");
                    if (dvgDicResults.Columns.Contains(colName))
                    {
                        dvgDicResults.Columns[colName].HeaderText = headerNewName;
                    }

                    colName = string.Concat("Synonym_", i.ToString());
                    if (dvgDicResults.Columns.Contains(colName))
                    {
                        dvgDicResults.Columns[colName].Visible = false;
                    }

                    i++;
                }

                
                    
                 

 

                if (!dvgDicResults.Columns.Contains("Notes")) //Added 05.02.2014       
                {
                    DataGridViewImageColumn imageCol = new DataGridViewImageColumn();

                    imageCol.HeaderText = "";
                    imageCol.Name = "Notes";

                    dvgDicResults.Columns.Add(imageCol);

                }

                dvgDicResults.Columns["Notes"].Visible = true;
                dvgDicResults.Columns["Notes"].Width = 20;


                dvgDicResults.AllowUserToAddRows = false; // Remove blank last row

              //  Double dbWeight = 0;

                //foreach (DataGridViewRow row in dvgDicResults.Rows)
                //{
                //    if (row.Cells["HighlightColor"].Value != null)
                //    {
                //        row.Cells["DicItem"].Style.BackColor = Color.FromName(row.Cells["HighlightColor"].Value.ToString());
                //    }
                //}

                //double dbWeight;
                //foreach (DataGridViewRow row in dvgDicResults.Rows)
                //{

                //    for (int x = 0; x < _docs.Length; x++)
                //    {
                //        colName = string.Concat("WeightTotal_", x.ToString());

                //        if (row.Cells[colName].Value.ToString() == string.Empty)
                //        {
                //            row.Cells[colName].Style.BackColor = Color.LightGray;
                //        }
                //        else
                //        {
                //            if (row.Cells[colName].Value.ToString() != string.Empty)
                //            {
                //                dbWeight = Convert.ToDouble(row.Cells[colName].Value);


                //                if (dbWeight < 1)
                //                    row.Cells[colName].Style.BackColor = Color.GreenYellow;
                //                else if (dbWeight > .99 && dbWeight < 3)
                //                    row.Cells[colName].Style.BackColor = Color.LightGreen;
                //                else if (dbWeight > 2.99 && dbWeight < 5)
                //                {
                //                    row.Cells[colName].Style.ForeColor = Color.White;
                //                    row.Cells[colName].Style.BackColor = Color.Green;
                //                }
                //                else if (dbWeight >= 5)
                //                {
                //                    row.Cells[colName].Style.ForeColor = Color.White;
                //                    row.Cells[colName].Style.BackColor = Color.DarkGreen;
                //                }

                //            }
                //        }
                //    }
                //}


                ucResultsDicItemsFilter1.adjColumns();
            }
            catch
            { }

        }

        private void AdjWeightColors()
        {
            string colName = string.Empty;

            foreach (DataGridViewRow row in dvgDicResults.Rows)
            {
                if (row.Cells["HighlightColor"].Value != null)
                {
                    row.Cells["DicItem"].Style.BackColor = Color.FromName(row.Cells["HighlightColor"].Value.ToString());
                }
            }

            double dbWeight;
            foreach (DataGridViewRow row in dvgDicResults.Rows)
            {

                for (int x = 0; x < _docs.Length; x++)
                {
                    colName = string.Concat("WeightTotal_", x.ToString());

                    if (dvgDicResults.Columns.Contains(colName))
                    {
                        if (row.Cells[colName].Value == null || row.Cells[colName].Value.ToString() == string.Empty)
                        {
                            row.Cells[colName].Style.BackColor = Color.LightGray;
                        }
                        else
                        {
                            dbWeight = Convert.ToDouble(row.Cells[colName].Value);


                            if (dbWeight < 1)
                                row.Cells[colName].Style.BackColor = Color.GreenYellow;
                            else if (dbWeight > .99 && dbWeight < 3)
                                row.Cells[colName].Style.BackColor = Color.LightGreen;
                            else if (dbWeight > 2.99 && dbWeight < 5)
                            {
                                row.Cells[colName].Style.ForeColor = Color.White;
                                row.Cells[colName].Style.BackColor = Color.Green;
                            }
                            else if (dbWeight >= 5)
                            {
                                row.Cells[colName].Style.ForeColor = Color.White;
                                row.Cells[colName].Style.BackColor = Color.DarkGreen;
                            }
                        }
                    }
                }
            }
        }

        private void ucResultsDicItemsFilter1_FilterCompleted()
        {
            _FoundQty = ucResultsDicItemsFilter1.FilterCount;

            if (_FoundQty == 0)
                return;

            List<string> dicFilteredResults = ucResultsDicItemsFilter1.FilterResults;

            string filter = string.Empty;
            int i = 0;
            string dicItem = string.Empty;
            foreach (string s in dicFilteredResults)
            {
                //uid = string.Concat("'", s, "'");
                dicItem = s;
                if (i == 0)
                    filter = string.Concat("'", dicItem, "'");
                else
                    filter = string.Concat(filter, ", '", dicItem, "'");

                i++;
            }

            filter = string.Concat("DicItem IN (", filter, ")");

            _dv = new DataView(_dsDicDocsSumResults.Tables[0]);

            _dv.RowFilter = filter;
            _dv.Sort = "Category, DicItem ASC";

            this.dvgDicResults.DataSource = _dv;
            this.dvgDicResults.Refresh();

            _FoundQty = _dv.Count; // Reset to get count of rows, not search items found -- Fixed Added 07/02/2017

            // Filter Status Display -- Keyword Filter
            //ucDeepAnalyticsFilterDisplay1.Count = _FoundQty;
            //ucDeepAnalyticsFilterDisplay1.CurrentMode = ucDeepAnalyticsFilterDisplay.Modes.Keywords;
            //ucDeepAnalyticsFilterDisplay1.UpdateStatusDisplay();

            ucResultsDicItemsFilter1.RefreshButton_Show();

            adjColumns();

            LoadDicTotalResults();

            AdjWeightColors();
        }

        private void dvgDicResults_Paint(object sender, PaintEventArgs e)
        {
            AdjWeightColors();

            AdjTotals();

            _HasLoaded = true;

        }

        private void AdjTotals()
        {
            int totalsColCount = dvgDicTotalResults.ColumnCount;
            int resultsColCount = dvgDicResults.ColumnCount;

            try
            {
                if (totalsColCount == 0)
                    return;

                int total1stColWidth = 0;

                for (int i = 1; i < resultsColCount; i++)
                {
                    if (i < 7)
                    {
                        if (dvgDicResults.Columns[i].Visible)
                        {
                            total1stColWidth = total1stColWidth + dvgDicResults.Columns[i].Width;
                        }
                        if (i == 6)
                        {
                            dvgDicTotalResults.Columns[0].Width = total1stColWidth + 4;
                        }
                    }

                }

                string colName = string.Empty;

                var dictionary = new Dictionary<string, double>(_docs.Length);

                var row = dvgDicTotalResults.Rows[0];

                double weightValue = 0;

                for (int x = 0; x < _docs.Length; x++)
                {
                    colName = string.Concat("WeightTotal_", x.ToString());

                    if (dvgDicResults.Columns.Contains(colName)) // Added 07.11.2020 - check if column exists
                    {

                        dvgDicTotalResults.Columns[colName].Width = dvgDicResults.Columns[colName].Width;

                        if (dvgDicTotalResults.Rows[0].Cells[colName].Value == null || dvgDicTotalResults.Rows[0].Cells[colName].Value.ToString() == string.Empty)
                        {
                            weightValue = 0;
                        }
                        else
                        {
                            weightValue = Convert.ToDouble(dvgDicTotalResults.Rows[0].Cells[colName].Value.ToString());
                        }

                        dictionary.Add(colName, weightValue);


                        colName = string.Concat("Count_", x.ToString());

                        dvgDicTotalResults.Columns[colName].Width = dvgDicResults.Columns[colName].Width;
                        
                    }
                }

                // -- Add Back ground color --

                var items = from pair in dictionary
                            orderby pair.Value descending
                            select pair; // Sort from higher value

                int y = 0;
                Color backgoundColor;
                Color forgroundColor;
                foreach (KeyValuePair<string, double> pair in items)
                {
                    colName = pair.Key;

                    if (y == 0)
                    {
                        backgoundColor = Color.DarkGreen;
                        forgroundColor = Color.White;
                    }
                    else if (y == 1)
                    {
                        backgoundColor = Color.LightGreen;
                        forgroundColor = Color.Black;
                    }
                    else if (y == 2)
                    {
                        backgoundColor = Color.GreenYellow;
                        forgroundColor = Color.Black;
                    }
                    else
                    {
                        break;
                    }

                    dvgDicTotalResults.Rows[0].Cells[colName].Style.ForeColor = forgroundColor;
                    dvgDicTotalResults.Rows[0].Cells[colName].Style.BackColor = backgoundColor;

                    y++;

                }


            }
            catch
            {
                return;
            }
        }


        private void ucResultsDicItemsFilter1_ShowAll()
        {
            DataView dv = _dsDicDocsSumResults.Tables[0].DefaultView;
            dv.Sort = string.Concat(DictionariesAnalysisFieldConst.Category, ",", DictionariesAnalysisFieldConst.DicItem);
            DataTable dtSorted = dv.ToTable();

            dvgDicResults.DataSource = dtSorted;

            adjColumns();

            LoadDicTotalResults();

            AdjWeightColors();
        }

        private void dvgDicResults_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dvgDicResults.Columns[e.ColumnIndex].Name == "Notes")
            {
                // Your code would go here - below is just the code I used to test 

                if (dvgDicResults.Rows[e.RowIndex].Cells["UID"].Value == null)
                    return;

                string sUID = dvgDicResults.Rows[e.RowIndex].Cells["UID"].Value.ToString();
                string file = string.Concat("D_", sUID, ".rtf");
                string noteFile = Path.Combine(_NotesPath, file);
                if (File.Exists(noteFile))
                {
                    e.Value = _Notepad;
                }
                else
                {
                    dvgDicResults.Columns["Notes"].DefaultCellStyle.NullValue = null;

                }
            } 
        }

        private void dvgDicTotalResults_SelectionChanged(object sender, EventArgs e)
        {
            dvgDicTotalResults.ClearSelection(); 
        }

        private void dvgDicTotalResults_Paint(object sender, PaintEventArgs e)
        {
            //int newWidth = dvgDicResults.Columns["Category"].Width + dvgDicResults.Columns["DicItem"].Width + dvgDicResults.Columns["Weight"].Width;

            ////if (newWidth > 20)
            ////    newWidth = newWidth - 20;

            //dvgDicTotalResults.Columns["Totals"].Width = newWidth;

            //string colName = string.Empty;

            //for (int x = 0; x < _docs.Length; x++)
            //{
            //    colName = string.Concat("WeightTotal_", x.ToString());
            //    dvgDicTotalResults.Columns[colName].Width = dvgDicTotalResults.Columns[colName].Width; 

            //    colName = string.Concat("Count_", x.ToString());
            //    dvgDicTotalResults.Columns[colName].Width = dvgDicTotalResults.Columns[colName].Width; 
            //}

            //dvgDicTotalResults.Columns["Nothing"].Width = 20;
            
        }

        private void dvgDicResults_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            splitContainer4.Visible = false;

            if (e.RowIndex == -1)
                return;

            string resultsUID = dvgDicResults.Rows[e.RowIndex].Cells["UID"].Value.ToString();
            this.ucResultsNotes1.UID = resultsUID;

            string colName = dvgDicResults.Columns[e.ColumnIndex].Name;
            if (colName.IndexOf("WeightTotal_") == 0 || colName.IndexOf("Count_") == 0)
            {
                string[] columSplit = colName.Split('_');
                int docNo = Convert.ToInt32(columSplit[1]);
                //MessageBox.Show(_docs[docNo]); // Test

                string dicItem = dvgDicResults.Rows[e.RowIndex].Cells["DicItem"].Value.ToString();

                // Get Analysis Document Folders               
                //AppFolders_CA.CACurrentProject = _ProjectName;
                //AppFolders_CA.CAAnalysisName = _AnalysisName;
                //AppFolders_CA.CACurrentDocument = _docs[docNo];
                AppFolders.DocName = _docs[docNo];

                string AnalysisDocPath = AppFolders.AnalysisCurrentDocsDocName; //AppFolders_CA.AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc;
                string docXMLPath = AppFolders.AnalysisXML; //AppFolders_CA.AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_XML;
                _AnalysisResultsPath = AppFolders.AnalysisParseSeg;  //AppFolders_CA.AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_AnalysisResults;

                if (!Directory.Exists(docXMLPath))
                {
                    string msg = string.Concat("Unable to find the file Document XML folder: ", docXMLPath);
                    MessageBox.Show(msg, "Document XML Folder Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                string dicAnalysisSumXMLFile = Path.Combine(docXMLPath, "DicAnalysisSum.xml");
                string parseResultsXMLFile = Path.Combine(docXMLPath, "ParseResults.xml");

                DataSet dsParseResults = Files.LoadDatasetFromXml(parseResultsXMLFile);
                DataSet dsDicAnalysisSumXMLFile = Files.LoadDatasetFromXml(dicAnalysisSumXMLFile);

                string selectStatment = string.Concat("DicItem = '", dicItem, "'");
                DataRow[] foundDic = dsDicAnalysisSumXMLFile.Tables[0].Select(selectStatment);
                if (foundDic.Length == 0)
                {
                    //string msg = string.Concat("Unable to find the Dictionary Item: ", dicItem);
                    //MessageBox.Show(msg, "Dictionary Item Not Found in Document Summary XML", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                string segmentSumUIDs = foundDic[0]["SegmentSumUIDs"].ToString();

                string filter = string.Concat("UID IN (", segmentSumUIDs, ")");

                DataView dv = new DataView(dsParseResults.Tables[0]);

                dv.RowFilter = filter;
                dv.Sort = "SortOrder ASC";

                this.dvgParsedResults.DataSource = dv;
                this.dvgParsedResults.Refresh();

                lblParseResults.Text = string.Concat(Directories.GetLastFolder(_docs[docNo]), "         Dic. Item:  ", dicItem, "         Rows Qty:  ", dv.Count.ToString());

            //    int FoundQty = dv.Count; // Reset to get count of rows, not search items found -- Fixed Added 07/02/2017

                splitContainer4.Visible = true;

                this.Refresh();

                adjParseResultsColumns();

                
                
            }
        }

        public void adjParseResultsColumns()
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
                dvgParsedResults.Columns["Keywords"].Visible = false;
                dvgParsedResults.Columns["Concepts"].Visible = false;
                dvgParsedResults.Columns["Dictionary"].Visible = false;
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
                dvgParsedResults.Columns["Page"].Width = 30;
                dvgParsedResults.Columns["Number"].Width = 50;


                dvgParsedResults.Columns["DicItems"].HeaderText = "Dic. Items [Qty]";
                dvgParsedResults.Columns["DicDefs"].HeaderText = "(Category) Item - Definition";

                //if (!dvgParsedResults.Columns.Contains("Notes")) //Added 05.02.2014       
                //{
                //    DataGridViewImageColumn imageCol = new DataGridViewImageColumn();

                //    imageCol.HeaderText = "";
                //    imageCol.Name = "Notes";

                //    dvgParsedResults.Columns.Add(imageCol);

                //}

                //dvgParsedResults.Columns["Notes"].Visible = true;
                //dvgParsedResults.Columns["Notes"].Width = 20;


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


                            if (dbWeight < 1)
                                row.Cells["Weight"].Style.BackColor = Color.GreenYellow;
                            else if (dbWeight > .99 && dbWeight < 3)
                                row.Cells["Weight"].Style.BackColor = Color.LightGreen;
                            else if (dbWeight > 2.99 && dbWeight < 5)
                                row.Cells["Weight"].Style.BackColor = Color.Green;
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

        private void dvgDicResults_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            adjColumns();
        }

        private void butGenerateReport_Click(object sender, EventArgs e)
        {
            Export();
        }

        private void ucResultsMultiDic_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void dvgDicResults_Validated(object sender, EventArgs e)
        {
            
        }

        private void dvgDicResults_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (_HasLoaded)
                AdjTotals();
        }

  
    }
}
