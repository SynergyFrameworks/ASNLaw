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
using WorkgroupMgr;
using MetroFramework.Forms;

namespace MatrixBuilder
{
    public partial class ucProjects : UserControl
    {
        public ucProjects()
        {
            InitializeComponent();
        }

        private string _ProjectRootPath = string.Empty;
        private string _ProjectCurrent = string.Empty;
        private string _SelectedDoc = string.Empty;

        private bool _NotSelect = false;

        // Declare delegate for when a project has been selected
        public delegate void ProcessHandler();

        [Category("Action")]
        [Description("Fires when a Project is selected")]
        public event ProcessHandler ProjectSelected;

        public string Project
        {
            get { return _ProjectCurrent; }
            set
            {
                _ProjectCurrent = value;

                lstbProjects.SelectedIndex = lstbProjects.FindString(_ProjectCurrent);
            }

        }

        private WorkgroupMgr.Projects _ProjMgr;

        public void LoadData(string ProjectRootPath)
        {
            _ProjectRootPath = ProjectRootPath;
            lblDefinition.Text = "Projects and Documents were defined in the Document Analyzer. Select a Project for creating a Matrix using its associated Documents. Documents should have been parsed (analyzed) by the Document Analyzer prior to creating a Matrix.";

            _ProjMgr = new Projects(_ProjectRootPath);

           // this.lstbProjects.Items.Clear();
            this.lstbProjects.DataSource = null; 
            this.dvgDocs.DataSource = null;
            this.richerTextBox1.Text = string.Empty;

            _NotSelect = true;
            lstbProjects.DataSource = _ProjMgr.GetProjectNames();

            _NotSelect = false;

            if (lstbProjects.Items.Count > 0)
            {
                lstbProjects.ClearSelected();
               // lstbProjects.SelectedIndex = 0;
            }


        }

        public bool ProjectSelectionUpdated()
        {
            if (_NotSelect)
                return false;

            _ProjectCurrent = lstbProjects.Text;

            AppFolders.ProjectCurrent = _ProjectCurrent;

            DataTable dt = _ProjMgr.GetDocuments(_ProjectCurrent);
            if (dt == null)
                return false;

            dvgDocs.DataSource = dt;

            dvgDocs.AllowUserToAddRows = false; // Remove blank last row

            foreach (DataGridViewRow row in dvgDocs.Rows)
            {
                if (row.Cells[ProjectDocsFields.AnalysisResults].Value.ToString().Equals("Yes"))
                {
                    row.Cells[ProjectDocsFields.AnalysisResults].Style.ForeColor = Color.LightGreen;
                }
                else
                {
                    row.Cells[ProjectDocsFields.AnalysisResults].Style.ForeColor = Color.Red;
                }

                if (row.Cells[ProjectDocsFields.DeepAnalysisResults].Value.ToString().Equals("Yes"))
                {
                    row.Cells[ProjectDocsFields.DeepAnalysisResults].Style.ForeColor = Color.LightGreen;
                }
                else
                {
                    row.Cells[ProjectDocsFields.DeepAnalysisResults].Style.ForeColor = Color.Red;
                }
            }

            dvgDocs.Columns[ProjectDocsFields.AnalysisResults].HeaderText = "Analysis Results?";
            dvgDocs.Columns[ProjectDocsFields.DeepAnalysisResults].HeaderText = "Deep Analysis?";


            if (ProjectSelected != null)
                ProjectSelected();

            return true;
        }

        private void lstbProjects_SelectedIndexChanged(object sender, EventArgs e)
        {

            ProjectSelectionUpdated();

            //if (_NotSelect)
            //    return;

            //_ProjectCurrent = lstbProjects.Text;

            //AppFolders.ProjectCurrent = _ProjectCurrent;

            //DataTable dt = _ProjMgr.GetDocuments(_ProjectCurrent);
            //if (dt == null)
            //    return;

            //dvgDocs.DataSource = dt;

            //dvgDocs.AllowUserToAddRows = false; // Remove blank last row

            //foreach (DataGridViewRow row in dvgDocs.Rows)
            //{
            //    if (row.Cells[ProjectDocsFields.AnalysisResults].Value.ToString().Equals("Yes"))
            //    {
            //        row.Cells[ProjectDocsFields.AnalysisResults].Style.ForeColor = Color.LightGreen;
            //    }
            //    else
            //    {
            //        row.Cells[ProjectDocsFields.AnalysisResults].Style.ForeColor = Color.Red;
            //    }

            //    if (row.Cells[ProjectDocsFields.DeepAnalysisResults].Value.ToString().Equals("Yes"))
            //    {
            //        row.Cells[ProjectDocsFields.DeepAnalysisResults].Style.ForeColor = Color.LightGreen;
            //    }
            //    else
            //    {
            //        row.Cells[ProjectDocsFields.DeepAnalysisResults].Style.ForeColor = Color.Red;
            //    }
            //}

            //dvgDocs.Columns[ProjectDocsFields.AnalysisResults].HeaderText = "Analysis Results?";
            //dvgDocs.Columns[ProjectDocsFields.DeepAnalysisResults].HeaderText = "Deep Analysis?";


            //if (ProjectSelected != null)
            //    ProjectSelected();
        }

        private void dvgDocs_SelectionChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor; // Waiting 

            DataGridViewRow row = dvgDocs.CurrentRow;

            if (row == null) // Check, sometimes data has not been loaded yet
            {
                Cursor.Current = Cursors.Default; // Back to normal
                this.richerTextBox1.Text = string.Empty;
                return;
            }

            _SelectedDoc = row.Cells[ProjectDocsFields.Name].Value.ToString();

            string pathFile = _ProjMgr.GetDocFile(_ProjectCurrent, _SelectedDoc);
            if (pathFile == null)
            {
                if (_ProjMgr.ErrorMessage.Length > 0)
                {
               //     MessageBox.Show(_ProjMgr.ErrorMessage, "Unable to Open Document", MessageBoxButtons.OK, MessageBoxIcon.Error); // ToDo - Fix giving False results
                    this.richerTextBox1.Text = string.Empty;
                    return;
                }
            }

            this.richerTextBox1.LoadFile(pathFile);
            

            Cursor.Current = Cursors.Default;
        }

        private void ucProjects_VisibleChanged(object sender, EventArgs e)
        {
            // ToDo - Fix: Font colors are not set on 1st open
            //if (this.Visible)
            //    this.dvgDocs.Refresh();
        }
    }
}
