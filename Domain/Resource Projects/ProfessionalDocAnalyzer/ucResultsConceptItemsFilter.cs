using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace ProfessionalDocAnalyzer
{
    public partial class ucResultsConceptItemsFilter : UserControl
    {
        public ucResultsConceptItemsFilter()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }


        // Declare delegate for when filter has completed
        public delegate void ProcessHandler();

        [Category("Action")]
        [Description("Fires when the filter has completed")]
        public event ProcessHandler FilterCompleted;

        [Category("Action")]
        [Description("Fires when the Refesh buttion event occurs")]
        public event ProcessHandler ShowAll;

        private int _FilterCount = 0;
        private List<string> _lstFilter = new List<string>();
        public List<string> FilterResults
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

        public void RefreshButton_Show()
        {
            butRefreshFind.Visible = true;
            this.Refresh();
        }

        public void RefreshButton_Hide()
        {
            butRefreshFind.Visible = false;
            this.Refresh();
        }
        
        private bool _isMultiDocsResults = false;

        public void adjColumns()
        {
            try
            {
                dvgConceptItems.Columns["UID"].Visible = false;
                dvgConceptItems.Columns["SegmentSumUIDs"].Visible = false;


                dvgConceptItems.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dvgConceptItems.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;


                dvgConceptItems.Columns["Select"].Width = 50;
                dvgConceptItems.Columns["Count"].Width = 50;

                dvgConceptItems.AllowUserToAddRows = false; // Remove blank last row

                //dvgDicItems.Columns["DicItem"].HeaderText = "Item";

                //setKeywordBackColor();

                int count;
                string colName = "Count";

                foreach (DataGridViewRow row in dvgConceptItems.Rows)
                {
                    if (row.Cells[colName].Value.ToString() != string.Empty)
                    {
                        count = Convert.ToInt32(row.Cells[colName].Value);


                        if (count < 10)
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
            catch
            { }

        }

        public bool LoadData(Atebion.ConceptAnalyzer.Analysis CAMgr, string ProjectName, string AnalysisName, string DocumentName)
        {
            _isMultiDocsResults = false;

            DataSet ds = CAMgr.Get_Document_Concept_Summary(ProjectName, AnalysisName, DocumentName);

            if (ds == null)
            {
                MessageBox.Show(CAMgr.ErrorMessage, "Unable to Load Found Summary", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            dvgConceptItems.DataSource = ds.Tables[0];

            return true;
        }

        public bool LoadData(DataSet dsConceptDocs, Atebion.ConceptAnalyzer.Analysis CAMgr, string ProjectName, string AnalysisName)
        {
            _isMultiDocsResults = true;

            dvgConceptItems.DataSource = dsConceptDocs.Tables[0];

            return true;
        }

        private void butFilter_Click(object sender, EventArgs e)
        {
            if (_isMultiDocsResults)
            {
                FilterConceptDocs();
            }
            else
            {
                FilterSingleDoc();
            }
        }


        private void FilterSingleDoc()
        {
            List<string> SegUIDs = new List<string>();
            _FilterCount = 0;
            string segmentSumUIDs = string.Empty;
            string segUID = string.Empty;


            string[] foundSegUIDs;

            Cursor.Current = Cursors.WaitCursor; // Waiting 
            _FilterCount = 0;

            foreach (DataGridViewRow row in dvgConceptItems.Rows)
            {
                DataGridViewCheckBoxCell cell = row.Cells["Select"] as DataGridViewCheckBoxCell;

                //Compare to the true value because Value isn't boolean
                //   if (cell.Value == cell.TrueValue)
                if (cell.Value.ToString() == "True")
                {
                    segmentSumUIDs = row.Cells["SegmentSumUIDs"].Value.ToString();
                    if (segmentSumUIDs.IndexOf(',') == -1)
                    {
                        segUID = segmentSumUIDs.Trim();
                        //if (!SegUIDs.Exists(z => z.EndsWith(segUID)))
                        //{
                        SegUIDs.Add(segUID);
                        //}

                    }
                    else
                    {
                        foundSegUIDs = segmentSumUIDs.Split(',');
                        foreach (string uid in foundSegUIDs)
                        {
                            //if (!SegUIDs.Exists(z => z.EndsWith(uid)))
                            //{
                            SegUIDs.Add(uid);
                            //}
                        }
                    }
                }
            }

            _FilterCount = SegUIDs.Count;

            lblFound.Text = string.Concat("Found: ", _FilterCount.ToString());
            lblFound.Visible = true;
            butRefreshFind.Visible = true;
            this.Refresh();


            _lstFilter = SegUIDs;

            Cursor.Current = Cursors.Default; // Done 

            if (FilterCompleted != null)
                FilterCompleted(); // Throw event for completed, then read FilterUIDs and FilterCount for form
        }


        private void FilterConceptDocs()
        {
            List<string> dicItems = new List<string>();
            _FilterCount = 0;
            string dicItem = string.Empty;
            //string segUID = string.Empty;


            //string[] foundSegUIDs;

            Cursor.Current = Cursors.WaitCursor; // Waiting 
            _FilterCount = 0;

            foreach (DataGridViewRow row in dvgConceptItems.Rows)
            {
                DataGridViewCheckBoxCell cell = row.Cells["Select"] as DataGridViewCheckBoxCell;

                //Compare to the true value because Value isn't boolean
                //   if (cell.Value == cell.TrueValue)
                if (cell.Value.ToString() == "True")
                {
                    dicItem = row.Cells["Concept"].Value.ToString();
                    dicItems.Add(dicItem);
                }
            }

            _FilterCount = dicItems.Count;

            lblFound.Text = string.Concat("Found: ", _FilterCount.ToString());
            lblFound.Visible = true;
            butRefreshFind.Visible = true;
            this.Refresh();


            _lstFilter = dicItems;

            Cursor.Current = Cursors.Default; // Done 

            if (FilterCompleted != null)
                FilterCompleted(); // Throw event for completed, then read FilterUIDs and FilterCount for form
        }

        private void butRefreshFind_Click(object sender, EventArgs e)
        {
            lblFound.Visible = false;
            butRefreshFind.Visible = false;
            this.Refresh();

            if (ShowAll != null)
                ShowAll(); // Throw event for Refresh
        }



    }
}
