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

namespace ProfessionalDocAnalyzer
{
    public partial class ucKwDicCon : UserControl
    {
        public ucKwDicCon()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        // Declare delegate for when filter has completed
        public delegate void ProcessHandler();

        [Category("Action")]
        [Description("Fires when the filter has completed")]
        public event ProcessHandler FilterCompleted;

        private int _FilterCount = 0;
        private List<string> _lstFilter = new List<string>();
        public List<string> FilterUIDs
        {
            get { return _lstFilter; }
        }

        public int FilterCount
        {
            get { return _FilterCount; }
        }

        private int _Count = 0;
        public int Count
        {
            get { return _Count; }
        }

        private AnalysisResultsType.SearchType _SearchType;
        private AnalysisResultsType.Selection _Selection;

        private DataTable _dtKwDicCon; //Keywords or Concepts or Dictionary Terms
        private DataTable _dtARwithFindings; // Reformed
        private DataTable _dtARwithFindings_Raw; // e.g. Analysis Result segments' UIDs with keywords or concepts or dictionary terms - Orginal DataTable

        public DataTable CreateTable_KwdicCon() // Create a common Table for Keywords, Dictionary Terms, and Concepts
        {
            DataTable table = new DataTable("ItemsFound");

            table.Columns.Add(KwDicCon_Fields.Select, typeof(bool));
            table.Columns.Add(KwDicCon_Fields.Item, typeof(string));
            table.Columns.Add(KwDicCon_Fields.Count, typeof(int));
            table.Columns.Add(KwDicCon_Fields.UIDs, typeof(string));
            
            return table;
        }

        public bool LoadData(DataTable dtARwithFindings, AnalysisResultsType.Selection Selection, AnalysisResultsType.SearchType SearchType)
        {
            _dtARwithFindings_Raw = dtARwithFindings;
            _dtKwDicCon = CreateTable_KwdicCon();
            _Selection = Selection;
            _SearchType = SearchType;
            switch (_SearchType)
            {
                case AnalysisResultsType.SearchType.Concepts:
                    lblHeaderCaption.Text = "Concepts Found";
                    break;


                case AnalysisResultsType.SearchType.Dictionary:
                    lblHeaderCaption.Text = "Dictionary Terms Found";
                    break;

                case AnalysisResultsType.SearchType.Keywords:
                    lblHeaderCaption.Text = "Keywords Found";
                    if (_Selection == AnalysisResultsType.Selection.Logic_Segments)
                    {
                        Map_Keywords_AnalysisResults_2_dtKwDicCon(dtARwithFindings);
                    }

                    break;

                case AnalysisResultsType.SearchType.FAR_DFAR:
                    lblHeaderCaption.Text = "FAR/DFARs Found";
                    break;

            }

            this.dvgKeywords.DataSource = null;

           // _dtKwDicCon = dtKwDicCon;
           // _dtARwithFindings = dtARwithFindings;

            if (_dtKwDicCon == null)
                return false;

            this.dvgKeywords.DataSource = _dtKwDicCon;

            LoadCloud();

            return true;
        }

        private void LoadCloud()
        {
            tagCloudControl1.ClearAllItems();

            foreach (DataRow row in _dtKwDicCon.Rows)
            {
                tagCloudControl1.AddItem(row[KwDicCon_Fields.Item].ToString(), Convert.ToDouble(row[KwDicCon_Fields.Count].ToString()));
            }
        }


        // --- Data Mapping ---

        private bool Map_Concepts_AnalysisResults_2_dtKwDicCon(DataTable dtARwithFindings)
        {
            if (dtARwithFindings == null)
                return false;

            if (dtARwithFindings.Rows.Count == 0)
                return false;

            _dtARwithFindings = CreateTable_KwdicCon();

            DataRow newRow;

            string keyword = string.Empty;
            int keywordCount = 0;

            dtARwithFindings.DefaultView.Sort = "Keyword";

            foreach (DataRow row in dtARwithFindings.Rows)
            {
                if (row["Keyword"].ToString() != keyword)
                {
                    if (keyword != string.Empty)
                    {
                        newRow = _dtKwDicCon.NewRow();
                        newRow[KwDicCon_Fields.Select] = true;
                        newRow[KwDicCon_Fields.Item] = keyword;
                        newRow[KwDicCon_Fields.Count] = keywordCount;

                        _dtKwDicCon.Rows.Add(newRow);
                    }

                    keyword = row["Keyword"].ToString();
                    keywordCount = Convert.ToInt32(row["Count"].ToString());
                }
                else
                {
                    keywordCount = keywordCount + Convert.ToInt32(row["Count"].ToString());
                }

            }

            newRow = _dtKwDicCon.NewRow();
            newRow[KwDicCon_Fields.Select] = true;
            newRow[KwDicCon_Fields.Item] = keyword;
            newRow[KwDicCon_Fields.Count] = keywordCount;

            return true;
        }

        private bool Map_Concepts_DeepAnalysisResults_2_dtKwDicCon(DataTable dtARwithFindings)
        {
            _dtARwithFindings = CreateTable_KwdicCon();

            return true;
        }

        private bool Map_Dictionary_AnalysisResults_2_dtKwDicCon(DataTable dtARwithFindings)
        {
            _dtARwithFindings = CreateTable_KwdicCon();

            return true;
        }

        private bool Map_Dictionary_DeepAnalysisResults_2_dtKwDicCon(DataTable dtARwithFindings)
        {
            _dtARwithFindings = CreateTable_KwdicCon();

            return true;
        }

        private bool Map_Keywords_AnalysisResults_2_dtKwDicCon(DataTable dtARwithFindings)
        {
            if (dtARwithFindings == null)
                return false;

            if (dtARwithFindings.Rows.Count == 0)
                return false;

            _dtARwithFindings = CreateTable_KwdicCon();

            DataRow newRow;

            string keyword = string.Empty;
            string lastKeyword = ">>NOT<<";
            int keywordCount = 0;
            string segmentUID = string.Empty;

          //  dtARwithFindings.DefaultView.Sort = "Keyword";
            DataView dv = dtARwithFindings.DefaultView;
            dv.Sort = "Keyword asc";
            DataTable dtARwithFindings_Sorted = dv.ToTable();

            foreach (DataRow row in dtARwithFindings_Sorted.Rows)
            {
                keyword = row["Keyword"].ToString();
                segmentUID = row["SegmentUID"].ToString();

                if (lastKeyword != keyword)
                {
                    //if (keyword != string.Empty)
                    //{

                    keywordCount = Convert.ToInt32(row["Count"].ToString());

                        newRow = _dtKwDicCon.NewRow();
                        newRow[KwDicCon_Fields.Select] = true;
                        newRow[KwDicCon_Fields.Item] = keyword;
                        newRow[KwDicCon_Fields.Count] = keywordCount;
                        newRow[KwDicCon_Fields.UIDs] = segmentUID;

                        _dtKwDicCon.Rows.Add(newRow);
                    //}
                    //else
                    //{

                    //}

                    //keyword = row["Keyword"].ToString();
                    //keywordCount = Convert.ToInt32(row["Count"].ToString());
                }
                else
                {
                    keywordCount = keywordCount + Convert.ToInt32(row["Count"].ToString());
                    foreach (DataRow rowX in _dtKwDicCon.Rows)
                    {
                        if (rowX[KwDicCon_Fields.Item].ToString() == keyword)
                        {
                            rowX[KwDicCon_Fields.Count] = keywordCount;
                            rowX[KwDicCon_Fields.UIDs] = string.Concat(rowX[KwDicCon_Fields.UIDs].ToString(), "|", segmentUID);
                            break;
                        }
                    }
                }

                lastKeyword = keyword;
            }

  //          newRow = _dtKwDicCon.NewRow();
 

      //      return true;

            return true;
        }

        private bool Map_Keywords_DeepAnalysisResults_2_dtKwDicCon(DataTable dtARwithFindings)
        {
            _dtARwithFindings = CreateTable_KwdicCon();

            return true;
        }


        private void butFilter_Click(object sender, EventArgs e)
        {
            _lstFilter.Clear();
            _FilterCount = 0;
  

            Cursor.Current = Cursors.WaitCursor; // Waiting 
            _FilterCount = 0;

            switch (_SearchType)
            {
                case AnalysisResultsType.SearchType.Concepts:

                    break;

                case AnalysisResultsType.SearchType.Dictionary:
                    break;

                case AnalysisResultsType.SearchType.FAR_DFAR:
                    break;

                case AnalysisResultsType.SearchType.Keywords:
                    if (_Selection == AnalysisResultsType.Selection.Logic_Sentences)
                    {
                        Keywords_DeepAnalysisResults_Filter();
                        if (FilterCompleted != null)
                            FilterCompleted();
                    }

                    break;

            }



            Cursor.Current = Cursors.Default; // Done 

            if (FilterCompleted != null)
                FilterCompleted(); // Throw event for completed, then read FilterUIDs and FilterCount for form

        }

        private int Concepts_AnalysisResults_Filter()
        {
            _lstFilter.Clear();

            string segmentSumUIDs = string.Empty;
            string segUID = string.Empty;
            string[] foundSegUIDs;

            foreach (DataGridViewRow row in dvgKeywords.Rows)
            {
                DataGridViewCheckBoxCell cell = row.Cells["Select"] as DataGridViewCheckBoxCell;

                //Compare to the true value because Value isn't boolean
                
                if (cell.Value.ToString() == "True")
                {
                    segmentSumUIDs = row.Cells["SegmentSumUIDs"].Value.ToString();
                    if (segmentSumUIDs.IndexOf(',') == -1)
                    {
                        _lstFilter.Add(segUID);
                    }
                    else
                    {
                        foundSegUIDs = segmentSumUIDs.Split(',');
                        foreach (string uid in foundSegUIDs)
                        {
                           _lstFilter.Add(uid);
                        }
                    }
                }
            }

            _Count = _lstFilter.Count;

            return _Count;

        }

        private int Keywords_DeepAnalysisResults_Filter()
        {
            string keyword = string.Empty;
            string uid = string.Empty;

            foreach (DataGridViewRow row in dvgKeywords.Rows)
            {
                DataGridViewCheckBoxCell cell = row.Cells["Select"] as DataGridViewCheckBoxCell;

                //Compare to the true value because Value isn't boolean
                //   if (cell.Value == cell.TrueValue)
                if (cell.Value.ToString() == "True")
                {
                    keyword = row.Cells["Keyword"].Value.ToString();
                    foreach (DataRow row2 in _dtARwithFindings.Rows)
                    {
                        if (keyword == row2["Keyword"].ToString())
                        {
                            uid = row2["SentenceUID"].ToString();
                            if (!_lstFilter.Contains(uid)) //if (!lstFilter.Exists(i => i == uid))
                            {
                                _lstFilter.Add(uid);
                                _FilterCount++;
                            }
                        }
                    }
                }

            }

            _Count = _lstFilter.Count;

            return _Count;
        }

        private void AdjustColumns()
        {

            Application.DoEvents();

            try
            {
                if (dvgKeywords.Columns.Contains(KwDicCon_Fields.Select))
                {
                    dvgKeywords.Columns[KwDicCon_Fields.Select].Width = 50;
                    dvgKeywords.Columns[KwDicCon_Fields.Select].HeaderText = string.Empty;
                }
                if (dvgKeywords.Columns.Contains(KwDicCon_Fields.UIDs))
                {
                    dvgKeywords.Columns[KwDicCon_Fields.UIDs].Visible = false;
                }
                if (dvgKeywords.Columns.Contains(KwDicCon_Fields.Count))
                {
                    dvgKeywords.Columns[KwDicCon_Fields.Count].Width = 100;

                    int count = 0;

                    foreach (DataGridViewRow row in dvgKeywords.Rows)
                    {
                        if (row.Cells[KwDicCon_Fields.Count].Value.ToString() != string.Empty)
                        {
                            count = Convert.ToInt32(row.Cells[KwDicCon_Fields.Count].Value);
                            if (count == 0)
                                row.Cells[KwDicCon_Fields.Count].Style.BackColor = Color.LightGray;
                            else if (count < 10)
                                row.Cells[KwDicCon_Fields.Count].Style.BackColor = Color.GreenYellow;
                            else if (count > 9 && count < 21)
                                row.Cells[KwDicCon_Fields.Count].Style.BackColor = Color.LightGreen;
                            else if (count > 20 && count < 41)
                            {
                                row.Cells[KwDicCon_Fields.Count].Style.ForeColor = Color.White;
                                row.Cells[KwDicCon_Fields.Count].Style.BackColor = Color.Green;
                            }
                            else if (count >= 41)
                            {
                                row.Cells[KwDicCon_Fields.Count].Style.ForeColor = Color.White;
                                row.Cells[KwDicCon_Fields.Count].Style.BackColor = Color.DarkGreen;
                            }

                        }
                    }
                }


            }
            catch
            {

            }

        }

        private void ucKwDicCon_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                AdjustColumns();
            }
        }

        private void butFilter_Click_1(object sender, EventArgs e)
        {
            Filter_GetSelected();
        }

        private int Filter_GetSelected()
        {
            _lstFilter.Clear();
            _FilterCount = 0;
            string keyword = string.Empty;
            string uid = string.Empty;
            string[] uids;

            Cursor.Current = Cursors.WaitCursor; // Waiting 
            _FilterCount = 0;

            foreach (DataGridViewRow row in dvgKeywords.Rows)
            {
                DataGridViewCheckBoxCell cell = row.Cells[KwDicCon_Fields.Select] as DataGridViewCheckBoxCell;

                //Compare to the true value because Value isn't boolean
                //   if (cell.Value == cell.TrueValue)
                if (cell.Value.ToString() == "True")
                {
                    keyword = row.Cells[KwDicCon_Fields.Item].Value.ToString();
                    uid = row.Cells[KwDicCon_Fields.UIDs].Value.ToString();
                    if (uid.Trim().Length > 0)
                    {
                        if (uid.IndexOf('|') == -1)
                        {
                            _lstFilter.Add(uid);
                        }
                        else
                        {
                            uids = uid.Split('|');
                            foreach (string xUID in uids)
                            {
                                _lstFilter.Add(xUID);
                            }
                        }

                    }
                }
            }

            _lstFilter = _lstFilter.Distinct().ToList();

            _FilterCount = _lstFilter.Count;

            Cursor.Current = Cursors.Default; // Done 

            if (FilterCompleted != null)
                FilterCompleted(); // Throw event for completed, then read FilterUIDs and FilterCount for form

            return _FilterCount;

        }

  
    }
}
