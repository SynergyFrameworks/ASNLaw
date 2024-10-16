using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

using Atebion.Common;

namespace ProfessionalDocAnalyzer
{
    public partial class ucKeywordGroups : UserControl
    {
        public ucKeywordGroups()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        #region private vars

        private string _KeywordGrpPath = string.Empty;
        private KeywordsMgr _KeywordsMgr;
        #endregion

        #region Properties

        private int _KeywordGrpCount = 0;

        public int Count
        {
            get
            {
                _KeywordGrpCount = lstbKeywordGroups.Items.Count;
                return _KeywordGrpCount;

            }
        }

        private string _KeywordFile = string.Empty;

        public string KeywordFile
        {
            get { return _KeywordFile; }
        }



        #endregion


        #region private functions

        private void ErrorRemoveDocTypeGroup(string result)
        {
            MessageBox.Show("Error: " + result, "Unable to Create Backup -- Cannot Remove", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }



        #endregion

        #region public methods

        public void LoadData()
        {
            lstbKeywords.Items.Clear();
            lblHeader.Visible = true;
            picHeader.Visible = true;


            if (_KeywordGrpPath == string.Empty)
            {
                _KeywordGrpPath = AppFolders.KeywordGrpPath;
                if (_KeywordGrpPath == string.Empty)
                {
                    return;
                }
            }

            if (_KeywordsMgr == null)
                _KeywordsMgr = new KeywordsMgr(_KeywordGrpPath, false);


            lstbKeywordGroups.Items.Clear();

            _KeywordGrpPath = AppFolders.KeywordGrpPath;

            string[] files = Directory.GetFiles(_KeywordGrpPath);

            int i = 0;
            string fileNameNoExt = string.Empty;

            foreach (string fileName in files)
            {
                fileNameNoExt = Files.GetFileNameWOExt(fileName);
                if (fileNameNoExt != string.Empty)
                {
                    lstbKeywordGroups.Items.Add(fileNameNoExt);
                    i++;
                }
            }

            _KeywordGrpCount = i;

        }

        public bool New()
        {
            bool returnValue = false;

            frmKeywords frm = new frmKeywords(AppFolders.KeywordGrpPath, false);
            // Show Dialog as a modal dialog and determine if DialogResult = OK.
            if (frm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                LoadData();
                returnValue = true;
            }

            frm.Dispose();

            return returnValue;
        }

        public void Download()
        {
            frmDownLoad frm = new frmDownLoad();
            frm.LoadData("xml", _KeywordGrpPath, ContentTypes.KeywordGroups);

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                LoadData();
            }
        }

        public bool Edit()
        {
            bool returnValue = false;

            if (KeywordFile != string.Empty)
            {
                frmKeywords frm = new frmKeywords(AppFolders.KeywordGrpPath, KeywordFile);
                // Show Dialog as a modal dialog and determine if DialogResult = OK.
                frm.ShowDialog(this);

                frm.Dispose();

                LoadData();

                returnValue = true;
            }

            return returnValue;

        }

        public bool Delete()
        {
            bool returnValue = false; // Added 09.07.2013
            if (lstbKeywordGroups.Items.Count == 0)
                return returnValue;

            if (this.lstbKeywordGroups.SelectedIndex < 0)
                this.lstbKeywordGroups.SelectedIndex = 0;

            string msg = string.Concat("Are you sure you want to remove Keyword  Group: ", this.lstbKeywordGroups.SelectedItem.ToString());
            if (MessageBox.Show(msg, "Confirm Removal", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string result = Directories.DirExistsOrCreate(string.Concat(_KeywordGrpPath, @"\Removed"));
                if (result != string.Empty)
                {
                    ErrorRemoveDocTypeGroup(result);
                    return returnValue;
                }
                result = Directories.DirExistsOrCreate(string.Concat(_KeywordGrpPath, @"\Removed\Keywords"));
                if (result != string.Empty)
                {
                    ErrorRemoveDocTypeGroup(result);
                    return returnValue;
                }

                string fromPathFile = string.Concat(_KeywordGrpPath, @"\", this.lstbKeywordGroups.SelectedItem.ToString(), ".xml");
                string toPathFile = string.Empty;
                string prefix = "~";

                bool FoundFile = true;
                do
                {
                    toPathFile = string.Concat(_KeywordGrpPath, @"\Removed\Keywords", @"\", prefix, this.lstbKeywordGroups.SelectedItem.ToString(), ".xml");
                    if (File.Exists(toPathFile))
                    {
                        prefix = prefix + "~";
                    }
                    else
                        FoundFile = false;

                } while (FoundFile == true);

                File.Move(fromPathFile, toPathFile);

                lstbKeywords.Items.Clear(); // Added 09.07.2013
                LoadData();

                return true; // Added 09.07.2013
            }

            return returnValue; // Added 09.07.2013

        }

        #endregion

        private void lstbKeywordGroups_Click(object sender, EventArgs e)
        {

        }

        private void lstbKeywordGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if an Doc Type exists
            if (this.lstbKeywordGroups.Items.Count == 0)
            {
                return;
            }


            if (this.lstbKeywordGroups.SelectedItem == null)
            {
                this.lstbKeywordGroups.SelectedIndex = 0;
            }

            _KeywordFile = lstbKeywordGroups.SelectedItem.ToString() + ".xml";


            // Load Keywords into listbox
            this.lstbKeywords.Items.Clear();
            _KeywordsMgr.Path = _KeywordGrpPath;
            _KeywordsMgr.DataFile = _KeywordFile;
            _KeywordsMgr.LoadData();
            DataSet ds = _KeywordsMgr.Data;

            if (ds != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    this.lstbKeywords.Items.Add(dr["Keyword"].ToString());

                }

            }
            

            string selFile = string.Concat(AppFolders.KeywordGrpPath, @"\", lstbKeywordGroups.SelectedItem.ToString(), ".xml");


            FileInformation selectedFileInfo = new FileInformation(selFile);

            lblMessage.Text = string.Concat(
    "Created: ", selectedFileInfo.CreationDate, " ", "\r\n",
    "Modified: ", selectedFileInfo.LastWriteTime, " ", "\r\n",
    "Qty: ", this.lstbKeywords.Items.Count.ToString()
        );
        }

        private void picHeader_Click(object sender, EventArgs e)
        {
            if (AppFolders.KeywordGrpPath == string.Empty)
                return;

            System.Diagnostics.Process.Start("explorer.exe", AppFolders.KeywordGrpPath);
        }

        private void lstbKeywords_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblMessage_TextChanged(object sender, EventArgs e)
        {
            txtbMessage.Text = lblMessage.Text;
        }

        private void txtbMessage_TextChanged(object sender, EventArgs e)
        {
            txtbMessage.Text = lblMessage.Text;
        }

        private void picHeader_Click_1(object sender, EventArgs e)
        {
            if (AppFolders.KeywordGrpPath.Length > 0)
                System.Diagnostics.Process.Start("explorer.exe", AppFolders.KeywordGrpPath);
        }

  
    }
}
