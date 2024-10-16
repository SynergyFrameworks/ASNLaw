using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Atebion.Common;

namespace ProfessionalDocAnalyzer
{
    public partial class frmWGNew : MetroFramework.Forms.MetroForm
    {
        public frmWGNew()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
            
        }

        private Atebion.WorkGroups.Manager _workgroupMgr;
        private DataTable _dtWorkgroups;

        private string _Workgroup_Selected = string.Empty;

        private const string _USE_DEFAULT_SETTINGS = "-- Use Default Settings --";

        public string NewWorkgroupName
        {
            get { return txtbWorkgroupName.Text.Trim(); }
        }


        public bool LoadData(Atebion.WorkGroups.Manager WorkgroupMgr)
        {
            _workgroupMgr = WorkgroupMgr;
            lblConnectMessage.Text = "Select a Folder to create the Workgroup by clicking the Browse button.";

           

            _dtWorkgroups = _workgroupMgr.GetWorkGroupList(); // Added 7/12/2017

            int qty = LoadWorkgroups(_dtWorkgroups); // was using dt
            if (qty > 0)
            {
                cboWorkgroups.SelectedIndex = 0;

                return true;
            }

            return false;
               
        }

        private int LoadWorkgroups(DataTable dt)
        {
            int counter = 0;

            cboWorkgroups.Items.Clear();

            cboWorkgroups.Items.Add(_USE_DEFAULT_SETTINGS);

            string workgroupName;
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    workgroupName = row[Atebion.WorkGroups.WorkgroupCatalogueFields.WorkgroupName].ToString();
                    cboWorkgroups.Items.Add(workgroupName);
                    counter++;
                }
            }
            catch
            {
                MessageBox.Show("Unable to show Workgroups at this time.", "Please try Later", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return counter;
        }

        public string Workgroup_Selected
        {
            get
            {
                if (Directories.GetLastFolder(_Workgroup_Selected) == "Workgroup")
                {
                    if (_Workgroup_Selected.Length > 10)
                        _Workgroup_Selected = _Workgroup_Selected.Substring(0, _Workgroup_Selected.Length - 10);
                }
                return _Workgroup_Selected;
            }
        }

        private void butBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.txtbWorkgroupPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }


        private bool Validate()
        {
            _workgroupMgr = new Atebion.WorkGroups.Manager(AppFolders.AppDataPath, AppFolders.UserName, AppFolders.AppDataPathUser);
            _dtWorkgroups = _workgroupMgr.GetWorkGroupList();

            DataRow[] filteredRows =
            _dtWorkgroups.Select(string.Format("{0} LIKE '%{1}%'", "WorkGroupName", this.txtbWorkgroupName.Text.Trim()));

            if (filteredRows.Count() > 0)
            {
                MessageBox.Show("This Workgroup Name is being used. Please enter a different Workgroup Name.", "Workgroup Name Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                lblConnectMessage.Text = "Please enter a Workgroup Name.";
                txtbWorkgroupName.Focus();
                return false;
            }

            //MessageBox.Show(filteredRows.Count().ToString());

            if (this.txtbWorkgroupName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter a Workgroup Name.", "Workgroup Name Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                lblConnectMessage.Text = "Please enter a Workgroup Name.";
                txtbWorkgroupName.Focus();
                return false;
            }


            if (this.txtbWorkgroupPath.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please select a Workgroup folder.", "Workgroup Folder Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                lblConnectMessage.Text = "Please select a Workgroup folder.";
                butBrowse.Focus();
                return false;
            }

            if (!Directory.Exists(txtbWorkgroupPath.Text.Trim()))
            {
                MessageBox.Show("Please select a Workgroup folder.", "Workgroup Folder Does Not Exists", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                lblConnectMessage.Text = "Please select a Workgroup folder.";
                butBrowse.Focus();
                return false;
            }

            if (this.txtbWorkgroupDescription.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter a Workgroup Description.", "Workgroup Description Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                lblConnectMessage.Text = "Please enter a Workgroup Description.";
                txtbWorkgroupDescription.Focus();
                return false;
            }

            return true;
        }



        private bool CheckCreateFolder()
        {
            if (txtbWorkgroupPath.Text == string.Empty)
            {
                MessageBox.Show(string.Concat("Enter or Select a folder to create a new Workgroup."));
                lblConnectMessage.Text = "Enter or Select a folder to create a new Workgroup.";

                return false;
            }
            else
            {
                if (!Directory.Exists(txtbWorkgroupPath.Text.Trim()))
                {
                    MessageBox.Show(string.Concat("Unable to find or connect to folder: ", txtbWorkgroupPath.Text.Trim()));
                    lblConnectMessage.Text = string.Concat("Unable to find or connect to folder: ", txtbWorkgroupPath.Text.Trim());

                    return false;
                }
                else
                {
                    if (_workgroupMgr.WorkgroupExists(txtbWorkgroupPath.Text.Trim()))
                    {
                        MessageBox.Show(string.Concat("There is already a Workgroup at this path location.", Environment.NewLine, Environment.NewLine, "Please select another folder."));
                        lblConnectMessage.Text = string.Concat("There is already a Workgroup at this path location.", Environment.NewLine, Environment.NewLine, "Please select another folder.");

                        return false;
                    }
                    else
                    {

                        return true;
                    }
                }
            }
        }

        private void butSave_Click_1(object sender, EventArgs e)
        {
            lblConnectMessage.ForeColor = Color.White;
            lblConnectMessage.Text = string.Empty;

            string rootPath = txtbWorkgroupPath.Text.Trim();

            if (!CheckCreateFolder())
                return;

            if (!Validate())
                return;

            //string ImportSettingFrom = string.Empty;
            //if (this.cboWorkgroups.Text != _USE_DEFAULT_SETTINGS)
            //    ImportSettingFrom = cboWorkgroups.Text;

            int selectedIndex = cboWorkgroups.SelectedIndex;

            if (selectedIndex == -1)
                return;

            Cursor.Current = Cursors.WaitCursor; // Waiting
            lblConnectMessage.Text = "Please Wait: Creating a new workgroup, and importing templates and libraries.";
            lblConnectMessage.Refresh();

            string workgroupRootPath = string.Empty;
            if (selectedIndex > 0)
            {
                DataRow row = _dtWorkgroups.Rows[selectedIndex - 1];
                workgroupRootPath = row[Atebion.WorkGroups.WorkgroupCatalogueFields.WorkgroupRootPath].ToString();
            }

            _workgroupMgr.ApplicationPath = Application.StartupPath;

            if (!_workgroupMgr.WorkgroupCreate(rootPath, txtbWorkgroupName.Text, txtbWorkgroupDescription.Text, workgroupRootPath))
            {
                MessageBox.Show(_workgroupMgr.ErrorMessage, "Workgroup Creation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblConnectMessage.Text = _workgroupMgr.ErrorMessage;
                lblConnectMessage.ForeColor = Color.Red;
                Cursor.Current = Cursors.Default; // Back to normal
                return;
            }

            Cursor.Current = Cursors.Default; // Back to normal

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
          
        }
  
    }
}
