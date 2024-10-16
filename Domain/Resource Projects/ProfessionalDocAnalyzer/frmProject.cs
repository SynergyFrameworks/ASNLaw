using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using Atebion.Common;

namespace ProfessionalDocAnalyzer
{
    public partial class frmProject : MetroFramework.Forms.MetroForm
    {
        public frmProject(string projectName) // Edit
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();


            //this.AcceptButton = this.butOK;
            //this.CancelButton = this.butCancel;
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            //this.MaximizeBox = false;
            //this.MinimizeBox = false;
            //this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            _projectName = projectName;

            _ProjectMgr = new ProjectManager(_projectName);

            LoadData();

            txtbProjectName.Enabled = false;

            this.Text = "Edit Project";
            this.lblHeaderCaption.Text = this.Text;

            isNew = false;
        }

        public frmProject(bool CreateDefault) // New
        {
            InitializeComponent();

            this.AcceptButton = this.butOK;
            this.CancelButton = this.butCancel;
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            //this.MaximizeBox = false;
            //this.MinimizeBox = false;
            //this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            if (CreateDefault)
                _ProjectMgr = new ProjectManager(true);
            else
                _ProjectMgr = new ProjectManager(false);

          //  this.Text = "New Project";
            this.lblHeaderCaption.Text = "New Project";

            isNew = true;
            
        }

        #region Private Var.s
        private string _projectName = string.Empty;
        private bool isNew = false;

        private ProjectManager _ProjectMgr;

        #endregion

        #region Private Functions
            
        private void LoadData()
        {
            txtbProjectName.Text = _projectName;
            _ProjectMgr.ProjectName = _projectName; // Tell the manager which project you are editing
            txtbDescription.Text = _ProjectMgr.Description;
        }

        private bool ValidateBeforeSaving()
        {
            lblMessage.Text = string.Empty;

            string projectName = txtbProjectName.Text.Trim();
            if (projectName == string.Empty)
            {
                string msg = "Please enter a Project Name";
                MessageBox.Show(msg, "A Project Name is Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblMessage.Text = msg;
                return false;
            }

            return true;
        }

        #endregion

        private void lblMessage_TextChanged(object sender, EventArgs e)
        {
            txtbMessage.Text = lblMessage.Text;
        }

        private void txtbMessage_TextChanged(object sender, EventArgs e)
        {
            txtbMessage.Text = lblMessage.Text;
        }

        private void txtbProjectName_TextChanged(object sender, EventArgs e)
        {
            txtbProjectName.Text = Files.CleanFileName(txtbProjectName.Text);
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            // Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor;

            if (isNew)
            {
                if (!ValidateBeforeSaving())
                {
                    // Set cursor as default arrow
                    Cursor.Current = Cursors.Default;

                    return;
                }

                    

                _ProjectMgr.ProjectName = this.txtbProjectName.Text.Trim();

                if (!_ProjectMgr.CreateProject(txtbDescription.Text))
                {
                    // Set cursor as default arrow
                    Cursor.Current = Cursors.Default;

                    string msg = _ProjectMgr.ErrorMessage;
                    MessageBox.Show(msg, "An Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblMessage.Text = msg;
  
                    return;
                }

                AppFolders.ProjectName = this.txtbProjectName.Text.Trim();
                
            }
            else
            {
                _ProjectMgr.ProjectName = this.txtbProjectName.Text.Trim(); // Tell the Manager which Project has been selected

                _ProjectMgr.Description = txtbDescription.Text;
                string msg = _ProjectMgr.ErrorMessage;
                if (msg != string.Empty)
                {
                    // Set cursor as default arrow
                    Cursor.Current = Cursors.Default;

                    MessageBox.Show(msg, "An Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblMessage.Text = msg;


                    return;
                }

                AppFolders.ProjectName = this.txtbProjectName.Text.Trim();

            }

            // Set cursor as default arrow
            Cursor.Current = Cursors.Default;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void frmProject_Load(object sender, EventArgs e)
        {

        }


    }
}
