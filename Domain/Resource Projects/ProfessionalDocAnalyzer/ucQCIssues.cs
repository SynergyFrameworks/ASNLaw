using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

using Atebion.Common;
using Atebion.QC;

namespace ProfessionalDocAnalyzer
{
    public partial class ucQCIssues : UserControl
    {
        public ucQCIssues()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        private DataSet _dsQCIssues;
        private DataView _dv;
        private string _QCIssuesXMLFile;

        public DataSet dsQCIssues
        {
            get { return _dsQCIssues; }
        }


        public bool LoadData(string QCIssuesXMLFile)
        {
            if (!File.Exists(QCIssuesXMLFile))
                return false;

            _QCIssuesXMLFile = QCIssuesXMLFile;

            GenericDataManger GDataMgr = new GenericDataManger();
            _dsQCIssues = GDataMgr.LoadDatasetFromXml(QCIssuesXMLFile);

            if (_dsQCIssues == null)
                return false;

            return true;
        }

        public bool LoadSelectedIssues(int ParseSeg_UID)
        {
            string filter = string.Concat(IssueFields.ParseSeg_UID, " IN (", ParseSeg_UID, ")");

            _dv = new DataView(_dsQCIssues.Tables[0]);

            _dv.RowFilter = filter;
            _dv.Sort = "UID ASC";

            this.dvgIssues.DataSource = _dv;

   //         Application.DoEvents();
            

            return true;
        }

        public void AdjustColumns()
        {
            try
            {

                dvgIssues.Columns[Atebion.QC.IssueFields.UID].Visible = false;
                dvgIssues.Columns[Atebion.QC.IssueFields.ParseSeg_UID].Visible = false;
                dvgIssues.Columns[Atebion.QC.IssueFields.Flag].Visible = false;
                dvgIssues.Columns[Atebion.QC.IssueFields.IssueQty].Visible = false;
                dvgIssues.Columns[Atebion.QC.IssueFields.IssueCat].Visible = false;
                dvgIssues.Columns[Atebion.QC.IssueFields.IssueColor].Visible = false;
                dvgIssues.Columns[Atebion.QC.IssueFields.Weight].Visible = false;

                dvgIssues.Font = new Font("Segoe UI", 10);

                dvgIssues.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
              //  dvgIssues.Columns[IssueFields.Issue].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                if (this.Width > 11)
                {
                    dvgIssues.Columns[IssueFields.Issue].Width = this.Width - 10;
                }
                dvgIssues.Columns[IssueFields.Issue].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                dvgIssues.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;

                //  dvgIssues.Columns[IssueFields.Issue].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dvgIssues.AllowUserToAddRows = false;  // Remove last blank row

                dvgIssues.ColumnHeadersVisible = false;

                string color = string.Empty;
                foreach (DataGridViewRow row in dvgIssues.Rows)
                {
                    // Issure Qty Back Color
                    if (row.Cells[IssueFields.IssueColor].Value != null)
                    {
                        color = row.Cells[IssueFields.IssueColor].Value.ToString();
                        row.Cells[IssueFields.Issue].Style.BackColor = Color.FromName(color);
                        if (color == "Green" || color == "DarkGreen" || color == "Red")
                        {
                            row.Cells[IssueFields.Issue].Style.ForeColor = Color.White;
                        }

                    }
                }

                //if (dvgIssues.Columns[IssueFields.Issue].Width > this.Width)
                //{
                //    if (this.Width > 11)
                //    {
                //        dvgIssues.Columns[IssueFields.Issue].Width = this.Width - 10;
                //        dvgIssues.Columns[IssueFields.Issue].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                //    }
                //}

                //  Application.DoEvents();

                //   dvgIssues.Columns[Atebion.QC.IssueFields.IssueQty].Width = 10;
            }
            catch
            {

            }
        }

        private void dvgIssues_Paint(object sender, PaintEventArgs e)
        {
            AdjustColumns();
        }

        private void dvgIssues_SelectionChanged(object sender, EventArgs e)
        {
            dvgIssues.ClearSelection();
        }


    }
}
