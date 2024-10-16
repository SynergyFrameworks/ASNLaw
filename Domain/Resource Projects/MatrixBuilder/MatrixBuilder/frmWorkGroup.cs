using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WorkgroupMgr;

namespace MatrixBuilder
{
    public partial class frmWorkGroup : MetroFramework.Forms.MetroForm
    {
        public frmWorkGroup(string PathLocalData, string UserName, string UserInfoPathFile)
        {
            InitializeComponent();

            _PathLocalData = PathLocalData;
            _UserName = UserName;
            _UserInfoPathFile = UserInfoPathFile;

            LoadData();

        }

        private string _PathLocalData = string.Empty;
        private string _UserName = string.Empty;
        private string _UserInfoPathFile = string.Empty;

        private WorkgroupMgr.Workgroups _workgroupMgr;

        private Modes _curentMode = Modes.None;
        private enum Modes
        {
            None = 0,
            Connect = 1,
            Create = 2
        }

        private void LoadData()
        {
            _workgroupMgr = new Workgroups(_PathLocalData, _UserName, _UserInfoPathFile);
            

        }


        private void frmWorkGroup_Load(object sender, EventArgs e)
        {

        }

        private void butConnect_Click(object sender, EventArgs e)
        {
            _curentMode = Modes.Connect;

            panDir.Visible = true;

            butCreate.Highlight = false;
            panCreate.Visible = false;
            butConnect.Highlight = true;

            CheckConnectionFolder();

            panConnect.Visible = true;
           

            this.Refresh();

        }

        private void txtbWorkgroupPath_TextChanged(object sender, EventArgs e)
        {
            if (butCreate.Highlight == true)
            {
                CheckCreateFolder();
            }
            else
            {
                CheckConnectionFolder();
            }
        }

        private bool CheckConnectionFolder()
        {
            if (txtbWorkgroupPath.Text == string.Empty)
            {
                lblConnectMessage.Text = "Enter or Select a folder to become a member of a specified Workgroup.";
                return false;

            }

            if (!Directory.Exists(txtbWorkgroupPath.Text.Trim()))
            {
                lblConnectMessage.Text = string.Concat("Unable to find or connect to folder: ", txtbWorkgroupPath.Text.Trim());
                return false;             
            }

            if (!_workgroupMgr.WorkgroupExists(txtbWorkgroupPath.Text.Trim()))
            {
                lblConnectMessage.Text = string.Concat("Was unable to find a workgroup to connect to.", Environment.NewLine, Environment.NewLine, "Please select the correct folder.");
                return false; 
            }

            lblConnectMessage.Text = "Click the Save button to become a member of this Workgroup.";
            return true;
        }


        private void butCreate_Click(object sender, EventArgs e)
        {
            _curentMode = Modes.Create;

            panDir.Visible = true;

            butCreate.Highlight = true;
            panCreate.Visible = true;
            butConnect.Highlight = false;

            CheckCreateFolder();

            panConnect.Visible = false;
           

            this.Refresh();
        }

        private bool CheckCreateFolder()
        {
            if (txtbWorkgroupPath.Text == string.Empty)
            {
                lblCreate.Text = "Enter or Select a folder to create a new Workgroup.";
                CreateShowHide(false);
                return false;
            }
            else
            {
                if (!Directory.Exists(txtbWorkgroupPath.Text.Trim()))
                {
                    lblCreate.Text = string.Concat("Unable to find or connect to folder: ", txtbWorkgroupPath.Text.Trim());
                    CreateShowHide(false);
                    return false;
                }
                else
                {
                    if (_workgroupMgr.WorkgroupExists(txtbWorkgroupPath.Text.Trim()))
                    {
                        lblConnectMessage.Text = string.Concat("There is already a Workgroup at this path location.", Environment.NewLine, Environment.NewLine, "Please select another folder.");
                        CreateShowHide(false);
                        return false;
                    }
                    else
                    {
                        CreateShowHide(true);
                        return true;
                    }
                }
            }
        }

        private void CreateShowHide(bool showTextboxes)
        {
            if (showTextboxes)
            {
                lblCreate.Visible = false;
                lblWorkgroupDescription.Visible = true;
                lblWorkGroupName.Visible = true;
                txtbWorkgroupDescription.Visible = true;
                txtbWorkgroupName.Visible = true;
            }
            else
            {
                lblCreate.Visible = true;
                lblWorkgroupDescription.Visible = false;
                lblWorkGroupName.Visible = false;
                txtbWorkgroupDescription.Visible = false;
                txtbWorkgroupName.Visible = false;
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

        private void butOk_Click(object sender, EventArgs e)
        {
            bool resultGood = false;
            lblConnectMessage.ForeColor = Color.White;
            lblConnectMessage.Text = string.Empty;

            string rootPath = txtbWorkgroupPath.Text.Trim();

            if (_curentMode == Modes.Connect)
            {
                if (!CheckConnectionFolder())
                {
                    return;
                } 

                string lastFolder = Files.GetLastFolder(rootPath);
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

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                if (!CheckCreateFolder())
                {
                    return;
                }

                if (!_workgroupMgr.WorkgroupCreate(rootPath, txtbWorkgroupName.Text, txtbWorkgroupDescription.Text))
                {
                    MessageBox.Show(_workgroupMgr.ErrorMessage, "Workgroup Creation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblCreate.Text = _workgroupMgr.ErrorMessage;
                    lblCreate.ForeColor = Color.Red;
                    return;
                }

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();

            }
            //_workgroupMgr
        }
    }
}
