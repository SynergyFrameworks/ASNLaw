using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Atebion.Common;

namespace ProfessionalDocAnalyzer
{
    public partial class frmCombinemultiSec : MetroFramework.Forms.MetroForm
    {
        public frmCombinemultiSec(DataView dv, int SelectedIndex, string ParseResultsFile, string XMLPath, string DocParsedSecPath, AnalysisResultsType.SearchType SearchType)
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();

            this.zzzzRangeBar1.RangeChanged += new Zzzz.ZzzzRangeBar.RangeChangedEventHandler(this.OnRangeChanged);
            this.zzzzRangeBar1.RangeChanging += new Zzzz.ZzzzRangeBar.RangeChangedEventHandler(this.OnRangeChanging);

            _dv = dv;
            _StartIndex = SelectedIndex;
            _ParseResultsFile = ParseResultsFile;
            _XMLPath = XMLPath;
            _DocParsedSecPath = DocParsedSecPath;
            _SearchType = SearchType;

            this.CancelButton = this.butCancel;
            //    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            //this.MaximizeBox = false;
            this.MinimizeBox = false;
            //this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;




        }


        // Input Parameters
        private DataView _dv;
        private int _StartIndex = 0;
        private string _ParseResultsFile = string.Empty;
        private string _XMLPath = string.Empty;
        private string _DocParsedSecPath = string.Empty;
        private AnalysisResultsType.SearchType _SearchType;
        //


        private int _EndIndex = 1;
        private string _StartUID;
        private int _StartLineStart;
        private int _EndLineEnd;
        private bool _ShowTestData = false;
        private string _pathParseSec = string.Empty;
        private string _pathXML = string.Empty;

        private bool _isMultiColorKeywords = true;

        private string _FileName = string.Empty;

        private List<string> _UIDs = new List<string>();
        Dictionary<string, int> _dicKeywords = new Dictionary<string, int>();
        string[] _keywordsSel;

        private void OnRangeChanged(object sender, System.EventArgs e)
        {
            _StartIndex = this.zzzzRangeBar1.RangeMinimum;
            _EndIndex = this.zzzzRangeBar1.RangeMaximum;

            SelectGridRange(_StartIndex, _EndIndex);

            CombineSegements(_StartIndex, _EndIndex);
        }

        private void OnRangeChanging(object sender, System.EventArgs e)
        {
            _StartIndex = this.zzzzRangeBar1.RangeMinimum;
            _EndIndex = this.zzzzRangeBar1.RangeMaximum;

            SelectGridRange(_StartIndex, _EndIndex);

            UpdateStatus(_StartIndex, _EndIndex);

        }

        private void UpdateStatus(int StartIndex, int EndIndex)
        {

            DataGridViewRow rowStart = dvgParsedResults.Rows[StartIndex];
            DataGridViewRow rowEnd = dvgParsedResults.Rows[EndIndex];

            string NoStart = rowStart.Cells["Number"].Value.ToString();

            string NoEnd;
            if (rowEnd.Cells["Number"].Value != null)
            {
                NoEnd = rowEnd.Cells["Number"].Value.ToString();
            }
            else
            {
                rowEnd = dvgParsedResults.Rows[EndIndex - 1];
                NoEnd = rowEnd.Cells["Number"].Value.ToString();
            }

            lblSelStatus.Text = string.Concat("Selection  From: ", NoStart, "  To: ", NoEnd);
        }

        private void Load_dicKeywords()
        {
            clsKeywordsHack kwHack = new clsKeywordsHack();

            if (AppFolders.DocParsedSecKeywords.Trim() != string.Empty)
            {
                _keywordsSel = kwHack.GetKeywords();
            }
            else
            {
                //string path = 
                _keywordsSel = kwHack.GetKeywords(_DocParsedSecPath.Replace("ParseSeg", "Keywords"));
            }


            foreach (string keyword in _keywordsSel)
            {
                _dicKeywords.Add(keyword, 0);
            }

        }

        private string getQty(string keywordWithQty, out string keyword)
        {
            string returnValue = string.Empty;
            keyword = string.Empty;

            int leftBracket = keywordWithQty.IndexOf('[');
            int rightBracket = keywordWithQty.IndexOf(']');
            if (leftBracket > -1 && rightBracket > -1)
            {
                returnValue = keywordWithQty.Substring((leftBracket + 1), (rightBracket - leftBracket) - 1);

                keyword = keywordWithQty.Substring(0, (leftBracket - 1));

                keyword = keyword.Trim();
            }

            return returnValue;
        }

        private void Update_dicKeywords(string keywords)
        {
            string[] ykeyword;
            string keyword;
            string qtyFound;
            string xxKeyword = string.Empty;

            if (keywords.Length > 0)
            {
                int x = keywords.LastIndexOf(',');
                if (x > -1)
                {
                    string[] keywordsParsed = keywords.Split(',');
                    // Loop through Keywords found for the selected segment
                    foreach (string xKeyword in keywordsParsed)
                    {
                        qtyFound = getQty(xKeyword, out keyword); // Added 12/27/2016

                        // xxKeyword = xKeyword.Trim(); comment out 12/27/2016
                        //ykeyword = xxKeyword.Split(' '); comment out 12/27/2016
                        //keyword = ykeyword[0].Trim(); comment out 12/27/2016

                        //      qtyFound = ykeyword[ykeyword.Length -1].Trim(); comment out 12/27/2016
                        //if (ykeyword.Length == 3)
                        //{
                        //    qtyFound = ykeyword[2].Trim();
                        //}
                        //else
                        //{
                        //    qtyFound = ykeyword[1].Trim();
                        //}

                        if (qtyFound != string.Empty)
                        {
                            //qtyFound = qtyFound.Replace('[', ' '); comment out 12/27/2016
                            //qtyFound = qtyFound.Replace(']', ' '); comment out 12/27/2016

                            qtyFound = qtyFound.Trim();
                            if (Convert.ToInt32(qtyFound) > 0)
                            {
                                if (_dicKeywords.ContainsKey(keyword))  // Added 4.4.2014 -- fixed Error Missing Key -- Fixed in 1.7.14.2
                                {
                                    _dicKeywords[keyword] = _dicKeywords[keyword] + Convert.ToInt32(qtyFound);
                                }
                                else
                                {
                                    MessageBox.Show("This Error may be the result of using Analysis Results from a previous version of the Document Analyzer. Suggest rerunning the Analyzer to fix this error, in the former step.", "Error: Unable to Locate Keywords", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
                else
                {
                    xxKeyword = keywords.Trim();
                    ykeyword = xxKeyword.Split(' ');
                    keyword = ykeyword[0].Trim();
                    qtyFound = ykeyword[ykeyword.Length - 1].Trim();
                    //if (ykeyword.Length == 3)
                    //{
                    //    qtyFound = ykeyword[2].Trim();
                    //}
                    //else
                    //{
                    //    qtyFound = ykeyword[1].Trim();
                    //}

                    if (qtyFound != string.Empty)
                    {
                        qtyFound = qtyFound.Replace('[', ' ');
                        qtyFound = qtyFound.Replace(']', ' ');

                        qtyFound = qtyFound.Trim();
                        if (Convert.ToInt32(qtyFound) > 0)
                        {
                            if (_dicKeywords.ContainsKey(keyword)) // Added 1/30/2017
                                _dicKeywords[keyword] = _dicKeywords[keyword] + Convert.ToInt32(qtyFound);
                            //else // Added 1/30/2017
                            //    MessageBox.Show("Unable to find Keyword (" + keyword + ") in keywords collection.", "Error: Unable to Find Keyword", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }

                }
            }


        }

        private void Clear_dicKeywords()
        {

            foreach (string keyword in _keywordsSel)
            {
                _dicKeywords[keyword] = 0;
            }

        }

        private string ConcatString_dicKeywords()
        {
            string keywordString = string.Empty;

            foreach (KeyValuePair<string, int> pair in _dicKeywords)
            {

                if (keywordString == string.Empty)
                {
                    if (pair.Value.ToString() != "0")
                        keywordString = string.Concat(pair.Key, " [", pair.Value.ToString(), "]");
                }
                else
                {
                    if (pair.Value.ToString() != "0")
                        keywordString = string.Concat(keywordString, ", ", pair.Key, " [", pair.Value.ToString(), "]");
                }

            }

            return keywordString;
        }






        private void LoadData()
        {
            if (File.Exists(Path.Combine(AppFolders.DocParsedSecXML, "KeywordsFound2.xml.xml")))
            {
                _isMultiColorKeywords = true;
            }
            else
            {
                _isMultiColorKeywords = false;
            }

            LinkLabel.Link link = new LinkLabel.Link();
            link.LinkData = @"http://www.atebionllc.com/documentation/da/#!/combine";
            linkLabelHelp.Links.Add(link);

            if (_SearchType == AnalysisResultsType.SearchType.Keywords)
                Load_dicKeywords(); // Load Selected Keywords into the Dictionary

            //if (AppFolders.DocParsedSecXML != string.Empty)
            //{
            //    _pathXML = AppFolders.DocParsedSecXML;
            //}
            //else
            //{
                _pathXML = _XMLPath;
            //}

            //if (AppFolders.DocParsedSec != string.Empty)
            //{
            //    _pathParseSec = AppFolders.DocParsedSec;
            //}
            //else
            //{
                _pathParseSec = _DocParsedSecPath;
            //}



            // Load DataGridView
            this.dvgParsedResults.DataSource = _dv;

            // Adjust columns
            AdjColumns();

            //// Set Range Scale Min/Max per DataGridView
            zzzzRangeBar1.TotalMinimum = 0;
            zzzzRangeBar1.TotalMaximum = dvgParsedResults.RowCount - 1;
            zzzzRangeBar1.DivisionNum = this.dvgParsedResults.Rows.Count;
            //zzzzRangeBar1.Refresh();

            // Select 1st (parent) Segement per _SelectedUID -- In DataGridView
            GoToRowParsedResults(_StartIndex);
            _EndIndex = _StartIndex; // +1;
                                     //   SelectGridRange(_StartIndex, _EndIndex);
                                     //GoToRowParsedResults(_EndIndex);

            // Set Select Range
            //       zzzzRangeBar1.RangeMinimum = _StartIndex;
            //       zzzzRangeBar1.RangeMaximum = _EndIndex;
            //         zzzzRangeBar1.SelectRange(_StartIndex, _EndIndex + 1);


            // Update Status and show Combined Segments
            SelectGridRange(_StartIndex, _EndIndex);
            CombineSegements(_StartIndex, _EndIndex);
            UpdateStatus(_StartIndex, _EndIndex);
            //  zzzzRangeBar1.Focus();

        }

        private void GoToRowParsedResults(int index)
        {
            dvgParsedResults.CurrentCell = dvgParsedResults[10, index];
            dvgParsedResults.Rows[index].Selected = true;
            ShowParsedDataPerCurrentRow();
        }

        private void SelectGridRange(int StartRow, int EndRow)
        {
            dvgParsedResults.ClearSelection();

            for (int i = StartRow; i < EndRow + 1; i++)
            {
                dvgParsedResults.Rows[i].Selected = true;
            }
        }

        private void CombineSegements(int StartRow, int EndRow)
        {
            if (_SearchType == AnalysisResultsType.SearchType.Keywords)
                Clear_dicKeywords(); // Reset values to 0

            _UIDs.Clear(); // Remove all UID values

            this.richTextBox2.Text = string.Empty;

            Cursor.Current = Cursors.WaitCursor; // Waiting
            for (int i = StartRow; i < EndRow + 1; i++)
            //  for (int i = EndRow; i > StartRow -1; i--)
            {
                this.richTextBox1.Text = string.Empty;

                DataGridViewRow row = dvgParsedResults.Rows[i];

                if (row == null) // Check, sometimes data has not been loaded yet
                {
                    Cursor.Current = Cursors.Default; // Back to normal
                    return;
                }

                //  string fileName = path + row.Cells["FileName"].Value.ToString();
                string fileName = string.Concat(_pathParseSec, @"\", row.Cells["FileName"].Value.ToString()); // ToDo: Fix Error -- Null --Combine: When clicking on the Cancel button 


                if (row.Cells["UID"].Value.ToString() == string.Empty)
                {
                    richTextBox1.Text = string.Empty;
                    Cursor.Current = Cursors.Default; // Back to normal
                    return; // Most likely last row, which is empty
                }

                if (row.Cells["UID"].Value.ToString().Length > 0)
                {
                    if (_SearchType == AnalysisResultsType.SearchType.Keywords)
                        Update_dicKeywords(row.Cells["Keywords"].Value.ToString()); // build Keyword count
                }

                _UIDs.Add(row.Cells["UID"].Value.ToString()); // collect UIDs which are being combined

                if (File.Exists(fileName))
                {

                    if (Files.FileIsLocked(fileName))
                    {
                        string msg = "The selected document is currently opened by another application. Please close this document file and try again.";
                        MessageBox.Show(msg, "Unable to Open this Document", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        richTextBox1.Text = msg;

                        return;

                    }

                    richTextBox1.LoadFile(fileName);

                    _FileName = fileName;

                }
                else
                {
                    richTextBox1.Text = string.Empty;
                    richTextBox1.Text = string.Concat("Error: Cannot find file: ", fileName);

                }

                if (i == StartRow)
                {
                    _StartUID = row.Cells["UID"].Value.ToString();
                    txtNumber.Text = row.Cells["Number"].Value.ToString();
                    txtCaption.Text = row.Cells["Caption"].Value.ToString();

                    if (_SearchType == AnalysisResultsType.SearchType.Keywords)
                    {
                        if (_isMultiColorKeywords)
                            _StartLineStart = (int)row.Cells["LineStart"].Value;
                    }
                }
                else if (i == EndRow)
                {
                    if (_SearchType == AnalysisResultsType.SearchType.Keywords)
                    {
                        if (_isMultiColorKeywords)
                            _EndLineEnd = (int)row.Cells["LineEnd"].Value;
                    }
                }

                richTextBox1.AppendText(Environment.NewLine + Environment.NewLine);
                richTextBox1.SelectAll();
                richTextBox1.Copy();

                Application.DoEvents(); //12.29.2017

                richTextBox2.Paste();

                Application.DoEvents(); //12.29.2017

            }

            Cursor.Current = Cursors.Default; // Back to normal

        }

        private void ShowParsedDataPerCurrentRow()
        {
            Cursor.Current = Cursors.WaitCursor; // Waiting 


            DataGridViewRow row = dvgParsedResults.CurrentRow;

            if (row == null) // Check, sometimes data has not been loaded yet
            {
                Cursor.Current = Cursors.Default; // Back to normal
                return;
            }

            //  string fileName = path + row.Cells["FileName"].Value.ToString();
            string fileName = string.Concat(_pathParseSec, @"\", row.Cells["FileName"].Value.ToString()); // ToDo: Fix Error -- Null --Combine: When clicking on the Cancel button 

            //  if (row.Cells[0].Value.ToString() == string.Empty)
            if (row.Cells["UID"].Value.ToString() == string.Empty) // 10.06.2013
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

                _FileName = fileName;

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
                richTextBox1.Text = string.Concat("Error: Cannot find file: ", fileName);

            }

            // Check if there is a Warning for the selected section
            string resultsUID = row.Cells["UID"].Value.ToString();


            Cursor.Current = Cursors.Default; // Back to normal

        }

        private void AdjColumns()
        {
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

            }

            dvgParsedResults.Columns[5].Visible = false;
            dvgParsedResults.Columns[6].Visible = false;
            dvgParsedResults.Columns[7].Visible = false;
        }

        private void dvgParsedResults_Click(object sender, EventArgs e)
        {
            SelectEvent();

            //int currentIndex = dvgParsedResults.CurrentRow.Index;

            //if (currentIndex < _StartIndex)
            //{
            //    _StartIndex = currentIndex;
            //}
            //else if (currentIndex > _StartIndex)
            //{
            //    _EndIndex = currentIndex;
            //}

            //// Set Select Range
            //zzzzRangeBar1.SelectRange(_StartIndex, _EndIndex);

            //// Update Status and show Combined Segments
            //SelectGridRange(_StartIndex, _EndIndex);
            //CombineSegements(_StartIndex, _EndIndex);
            //UpdateStatus(_StartIndex, _EndIndex);


        }

        private void SelectEvent()
        {
            Application.DoEvents();

            int currentIndex = dvgParsedResults.CurrentRow.Index;

            Application.DoEvents();

            if (currentIndex < _StartIndex)
            {
                _StartIndex = currentIndex;
            }
            else if (currentIndex > _StartIndex)
            {
                _EndIndex = currentIndex;
            }

            // Set Select Range
            zzzzRangeBar1.SelectRange(_StartIndex, _EndIndex);

            Application.DoEvents();

            // Update Status and show Combined Segments
            SelectGridRange(_StartIndex, _EndIndex);
            Application.DoEvents();
            CombineSegements(_StartIndex, _EndIndex);
            UpdateStatus(_StartIndex, _EndIndex);
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            SaveCombineSegment();

            AdjustQueriesFound();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private bool SaveCombineSegment()
        {
            if (_isMultiColorKeywords)
            {
                return SaveLegalCombineSegment();
            }
            else
            {
                return SaveParagraphCombineSegment();

            }

        }

        private bool SaveParagraphCombineSegment()
        {
            Cursor.Current = Cursors.WaitCursor; // Waiting

            bool returnResults = false;
            string tempDocParsedSecXML = "";
            string tempDocParsedSec = "";
            //if (AppFolders.DocParsedSecXML != string.Empty)
            //{
            //    tempDocParsedSecXML = AppFolders.DocParsedSecXML;
            //}
            //else
            //{
                tempDocParsedSecXML = _XMLPath;
            //}
            //if (AppFolders.DocParsedSec != string.Empty)
            //{
            //    tempDocParsedSec = AppFolders.DocParsedSec;
            //}
            //else
            //{
                tempDocParsedSec = _DocParsedSecPath;
            //}

            //       string keywords = ConcatString_dicKeywords();

            string parentUID = _UIDs[0];
            string lastChild = _UIDs[_UIDs.Count - 1];

            DataTable dt = _dv.ToTable();

            //Get Data from last child
            DataRow[] rowsLast = dt.Select(string.Concat("UID = '", lastChild, "'"));
            //string indexEnd = rowsLast[0]["IndexEnd"].ToString();
            //string columnEnd = rowsLast[0]["ColumnEnd"].ToString();

            //if (indexEnd == string.Empty)
            //    indexEnd = "0";

            //if (columnEnd == string.Empty)
            //    columnEnd = "0";

            // Update parent segment to include keywords in child segments
            DataRow[] rows = dt.Select(string.Concat("UID = '", parentUID, "'"));
            //       rows[0]["Keywords"] = keywords;
            rows[0]["Number"] = txtNumber.Text.Trim();
            rows[0]["Caption"] = txtCaption.Text.Trim();
            //rows[0]["IndexEnd"] = Convert.ToInt32(indexEnd);
            //rows[0]["ColumnEnd"] = Convert.ToInt32(columnEnd);
            rows[0]["SectionLength"] = this.richTextBox2.TextLength;
            dt.AcceptChanges();

            int count = _UIDs.Count;
            for (int i = 1; i < count; i++)
            {
                rows = dt.Select(string.Concat("UID = '", _UIDs[i].ToString(), "'"));
                rows[0].Delete();
                dt.AcceptChanges();
            }



            // Save Changes to the ParseResults.xml file
            string ParseResultsFile = string.Concat(tempDocParsedSecXML, @"\", "ParseResults.xml");
            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(ParseResultsFile);

            DataSet ds = new DataSet();

            ds.Tables.Add(dt);

            ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
            xmlSW.Close();

            // Save segment file
            string segmentFile = string.Concat(tempDocParsedSec, @"\", parentUID, ".rtf");
           // MessageBox.Show(segmentFile);
            this.richTextBox2.SaveFile(segmentFile);

            // re-Set Keywords
            //ParagraphParseEng paragraphParseEng = new ParagraphParseEng();
            //paragraphParseEng.FindKeywordsSec(parentUID);
            KeywordsMgr2 keywordMgr = new KeywordsMgr2();
            keywordMgr.FindKeywordsSec(parentUID);


            // Remove Keywords from child segments
            for (int i = Convert.ToInt32(parentUID) + 1; i < _UIDs.Count; i++)
            {
                keywordMgr.DeleteKeywordsSec(i.ToString());

            }


            Cursor.Current = Cursors.Default;

            returnResults = true;

            return returnResults;
        }

        private bool SaveLegalCombineSegment()
        {
            Cursor.Current = Cursors.WaitCursor; // Waiting

            bool returnResults = false;

            string tempDocParsedSecXML = "";
            string tempDocParsedSec = "";
            if (AppFolders.DocParsedSecXML != string.Empty)
            {
                tempDocParsedSecXML = AppFolders.DocParsedSecXML;
            }
            else
            {
                tempDocParsedSecXML = _XMLPath;
            }
            if (AppFolders.DocParsedSec != string.Empty)
            {
                tempDocParsedSec = AppFolders.DocParsedSec;
            }
            else
            {
                tempDocParsedSec = _DocParsedSecPath;
            }

            string keywords = ConcatString_dicKeywords();

            string parentUID = _UIDs[0];
            string lastChild = _UIDs[_UIDs.Count - 1];

            DataTable dt = _dv.ToTable();

            //Get Data from last child
            DataRow[] rowsLast = dt.Select(string.Concat("UID = '", lastChild, "'"));
            string indexEnd = rowsLast[0]["IndexEnd"].ToString();
            string columnEnd = rowsLast[0]["ColumnEnd"].ToString();

            if (indexEnd == string.Empty)
                indexEnd = "0";

            if (columnEnd == string.Empty)
                columnEnd = "0";

            // Update parent segment to include keywords in child segments
            DataRow[] rows = dt.Select(string.Concat("UID = '", parentUID, "'"));
            rows[0]["Keywords"] = keywords;
            rows[0]["Number"] = txtNumber.Text.Trim();
            rows[0]["Caption"] = txtCaption.Text.Trim();
            rows[0]["IndexEnd"] = Convert.ToInt32(indexEnd);
            rows[0]["ColumnEnd"] = Convert.ToInt32(columnEnd);
            rows[0]["SectionLength"] = this.richTextBox2.TextLength;
            dt.AcceptChanges();

            int count = _UIDs.Count;
            for (int i = 1; i < count; i++)
            {
                rows = dt.Select(string.Concat("UID = '", _UIDs[i].ToString(), "'"));
                rows[0].Delete();
                dt.AcceptChanges();
            }


            // Remove Child segments from the ParseResults.xml file
            //int count = _UIDs.Count;
            //foreach (DataRow row in dt.Rows)
            //{
            //    if (row["UID"].ToString() == parentUID)
            //    {
            //        row.BeginEdit();
            //        row["Keywords"] = keywords;
            //        row["IndexEnd"] = Convert.ToInt32(indexEnd);
            //        row["ColumnEnd"] = Convert.ToInt32(columnEnd);
            //        row["SectionLength"] = this.richTextBox2.TextLength;

            //    }
            //    else
            //    {
            //        for (int i = 1; i < count; i++)
            //        {
            //            if (row["UID"].ToString() == _UIDs[i].ToString())
            //            {
            //                row.BeginEdit();
            //                row.Delete();
            //                //  dt.AcceptChanges();
            //            }
            //        }
            //    }
            //}

            //dt.AcceptChanges();

            // Save Changes to the ParseResults.xml file
            string ParseResultsFile = string.Concat(tempDocParsedSecXML, @"\", "ParseResults.xml");
            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(ParseResultsFile);

            DataSet ds = new DataSet();

            ds.Tables.Add(dt);

            ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
            xmlSW.Close();


            // Save segment file
            string segmentFile = string.Concat(tempDocParsedSec, @"\", parentUID, ".rtf");
            this.richTextBox2.SaveFile(segmentFile);

            Cursor.Current = Cursors.Default;

            returnResults = true;

            return returnResults;
        }

        private void AdjustQueriesFound() // Added 12.11.2016
        {
            // Parse Sgements XML file
            string parseSegFile = "ParseResults.xml";
            parseSegFile = Path.Combine(AppFolders.DocParsedSecXML, parseSegFile);

            // Query XML file          
            string queryFile = "Queries.xml";
            queryFile = Path.Combine(AppFolders.DocParsedSecXML, queryFile);

            QueryFinder queriesFinder = new QueryFinder();
            queriesFinder.CombineSegQueries(_UIDs[0], _UIDs.ToArray(), parseSegFile, queryFile);

            queriesFinder = null;
        }

        private void frmCombinemultiSec_Load(object sender, EventArgs e)
        {
            LoadData();

            SelectEvent();

            zzzzRangeBar1.RangeMinimum = _StartIndex;
            //Do Not uncomment -- Error ->> zzzzRangeBar1.RangeMaximum = _EndIndex;
        }

        private void frmCombinemultiSec_Activated(object sender, EventArgs e)
        {

        }

        private void linkLabelHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://www.atebionllc.com/documentation/da/#!/combine");
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }




    }
}
