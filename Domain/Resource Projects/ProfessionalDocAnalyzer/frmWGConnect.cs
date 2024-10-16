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
using Atebion.WorkGroups;

namespace ProfessionalDocAnalyzer
{
    public partial class frmWGConnect : MetroFramework.Forms.MetroForm
    {
        public frmWGConnect(Atebion.WorkGroups.Manager WorkgroupMgr)
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
            _workgroupMgr = WorkgroupMgr;
            lblConnectMessage.Text = "Select a Workgroup Folder to join the Workgroup by clicking the Browse button.";
        }

        Atebion.WorkGroups.Manager _workgroupMgr;

        private string _Workgroup_Selected = string.Empty;

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
        
        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void butBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.txtbWorkgroupPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private bool CheckConnectionFolder()
        {
            if (txtbWorkgroupPath.Text == string.Empty) 
            {
                MessageBox.Show("Enter or Select a folder to become a member of a specified Workgroup", "Was unable to find a workgroup to connect to", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblConnectMessage.Text = "Enter or Select a folder to become a member of a specified Workgroup.";
                return false;

            }

            if (!Directory.Exists(txtbWorkgroupPath.Text.Trim()))
            {
                MessageBox.Show("No workgroup folder found", "Was unable to find a workgroup to connect to", MessageBoxButtons.OK, MessageBoxIcon.Error);

                lblConnectMessage.Text = string.Concat("Unable to find or connect to folder: ", txtbWorkgroupPath.Text.Trim());
                return false;
            }


            if (!Directory.Exists(txtbWorkgroupPath.Text.Trim() + @"\Workgroup"))
            {
                MessageBox.Show("No workgroup folder found", "Was unable to find a workgroup to connect to", MessageBoxButtons.OK, MessageBoxIcon.Error);

                lblConnectMessage.Text = string.Concat("Unable to find or connect to folder: ", txtbWorkgroupPath.Text.Trim());
                return false;
            }

            if (!_workgroupMgr.WorkgroupExists(txtbWorkgroupPath.Text.Trim()))
            {
                lblConnectMessage.Text = string.Concat("Was unable to find a workgroup to connect to.", Environment.NewLine, Environment.NewLine, "Please select the correct folder.");
                return false;
            }

            _Workgroup_Selected = txtbWorkgroupPath.Text.Trim();

            // lblConnectMessage.Text = "Click the Save button to become a member of this Workgroup.";
            return true;
        }

        private void butExport_Click(object sender, EventArgs e)
        {
            bool resultGood = false;
            lblConnectMessage.ForeColor = Color.White;
            lblConnectMessage.Text = string.Empty;

            if (!CheckConnectionFolder())
                return;

            string rootPath = txtbWorkgroupPath.Text.Trim();

         
            string lastFolder = Directories.GetLastFolder(rootPath);
            if (lastFolder.ToLower() == "workgroup")
            {
                rootPath = Directory.GetParent(rootPath).FullName; ;
            }

            resultGood = _workgroupMgr.WorkgroupConnection_Add(rootPath, false);

            if (!resultGood)
            {
                MessageBox.Show(_workgroupMgr.ErrorMessage, "Unable to Connect to Workgroup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblConnectMessage.Text = _workgroupMgr.ErrorMessage;
                lblConnectMessage.ForeColor = Color.Red;
                return;
            }
            else
            {
              AppFolders.ConnectWorkgroupPath = rootPath;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
         
            
        }
    }
}
