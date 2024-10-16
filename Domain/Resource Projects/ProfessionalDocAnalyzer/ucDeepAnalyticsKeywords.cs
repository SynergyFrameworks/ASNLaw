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
    public partial class ucDeepAnalyticsKeywords : UserControl
    {
        public ucDeepAnalyticsKeywords()
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
        private List<string> lstFilter = new List<string>();
        public List<string> FilterUIDs
        {
            get { return lstFilter; }
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


        private Analysis _DeepAnalysis = new Analysis();
        private DataSet _dsKeywordsFound;

        private bool _isLoaded = false;

        public void LoadData()
        {
            _DeepAnalysis.CurrentDocPath = AppFolders.CurrentDocPath;
        }

        public void LoadData(Analysis DeepAnalysis)
        {
            this.dvgKeywords.DataSource = null;
            _DeepAnalysis = DeepAnalysis;

            _isLoaded = true;

            string keywordSumFile = _DeepAnalysis.FoundSumKeywordsFile;
            if (File.Exists(keywordSumFile))
            {
                GenericDataManger dataMgr = new GenericDataManger();
                DataSet ds = dataMgr.LoadDatasetFromXml(keywordSumFile);
                this.dvgKeywords.DataSource = ds.Tables[0];

                _dsKeywordsFound = dataMgr.LoadDatasetFromXml(_DeepAnalysis.FoundKeywordsFile);

                // Get Total Keyword Count
                _Count = 0; // Reset to default
                foreach (DataRow row in _dsKeywordsFound.Tables[0].Rows)
                {
                    if (row["Count"].ToString() != string.Empty)
                        _Count += (int)row["Count"];


                }



                // Removed 01.08.2016 AdjustColumns();

            }

        }

        private void butFilter_Click(object sender, EventArgs e)
        {
            lstFilter.Clear();
            _FilterCount = 0;
            string keyword = string.Empty;
            string uid = string.Empty;

            Cursor.Current = Cursors.WaitCursor; // Waiting 
            _FilterCount = 0;

            foreach (DataGridViewRow row in dvgKeywords.Rows)
            {
                DataGridViewCheckBoxCell cell = row.Cells["Select"] as DataGridViewCheckBoxCell;

                //Compare to the true value because Value isn't boolean
                //   if (cell.Value == cell.TrueValue)
                if (cell.Value.ToString() == "True")
                {
                    keyword = row.Cells["Keyword"].Value.ToString();
                    foreach (DataRow row2 in _dsKeywordsFound.Tables[0].Rows)
                    {
                        if (keyword == row2["Keyword"].ToString())
                        {
                            uid = row2["SentenceUID"].ToString();
                            if (!lstFilter.Contains(uid)) //if (!lstFilter.Exists(i => i == uid))
                            {
                                lstFilter.Add(uid);
                                _FilterCount++;
                            }
                        }
                    }
                }

            }

            Cursor.Current = Cursors.Default; // Done 

            if (FilterCompleted != null)
                FilterCompleted(); // Throw event for completed, then read FilterUIDs and FilterCount for form

        }

        private void dvgKeywords_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!_isLoaded)
                return;

            if (e.RowIndex == -1)
                return;

            if (e.ColumnIndex == 0)
            {
                if (dvgKeywords.CurrentCell.Value == null)
                    dvgKeywords.CurrentCell.Value = true;
                else if (dvgKeywords.CurrentCell.Value.ToString() == "0")
                    dvgKeywords.CurrentCell.Value = true;
                else if (dvgKeywords.CurrentCell.Value.ToString() == "1")
                    dvgKeywords.CurrentCell.Value = false;
            }
        }

        private void dvgKeywords_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (dvgKeywords.CurrentCell is DataGridViewCheckBoxCell)
                {
                    dvgKeywords.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
            catch { }
        }

 
        public void AdjustColumns()
        {
            if (!_isLoaded)
                return;

            // dvgParsedResults.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);

            Application.DoEvents();

            try
            {
                if (dvgKeywords.Columns.Contains("KeywordLib"))
                    dvgKeywords.Columns["KeywordLib"].Visible = false;

                if (dvgKeywords.Columns.Contains("Color"))
                {
                    setKeywordBackColor();
                    dvgKeywords.Columns["Color"].Visible = false;
                }


                //Count
                if (dvgKeywords.Columns.Contains("Count"))
                    dvgKeywords.Columns["Count"].Width = 50;

                if (dvgKeywords.Columns.Contains("Select"))
                    dvgKeywords.Columns["Select"].Width = 50;

            }
            catch
            {

            }


        }

        private void setKeywordBackColor()
        {
            if (!_isLoaded)
                return;

            string colorHighlight = string.Empty;

            foreach (DataGridViewRow row in dvgKeywords.Rows)
            {
                if (row.Cells[KeywordsFoundFields.ColorHighlight].Value != null)
                {
                    colorHighlight = row.Cells[KeywordsFoundFields.ColorHighlight].Value.ToString();
                    row.Cells[KeywordsFoundFields.Select].Style.BackColor = Color.FromName(colorHighlight);
                }

            }
        }

        private void ucDeepAnalyticsKeywords_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                AdjustColumns(); // Added 01.08.2016
            }
        }

    }
}
