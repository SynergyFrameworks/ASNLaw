using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Atebion.Common;
using AcroParser;

namespace ProfessionalDocAnalyzer
{
    public partial class frmIgnoreLst : MetroFramework.Forms.MetroForm
    {
        public frmIgnoreLst()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
            this.CancelButton = this.butCancel;
            //    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            //this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            LoadData();
        }


        public frmIgnoreLst(string DefName)
        {
            InitializeComponent();

            this.CancelButton = this.butCancel;
            //    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            //this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            _DefName = DefName; // string.Concat(AppFolders.AppDataIgnoreLst, @"\", DefName, ".xml");

            LoadData();
        }

        private subPanels _subPanel = subPanels.BatchLoad;
        private enum subPanels
        {
            BatchLoad = 0,
            Maintain = 1,
        }

        private string _path = string.Empty;
        private string _file = string.Empty;
        private string _fileName = string.Empty;

        private bool isNew = false;

        private AcroParser.AcroIgnoreList _AcroIgnoreListMgr;
        private DataSet _ds;

        private string _DefName = string.Empty;
        private DataSet _dsDefLib;
        private string _SelectedFile = string.Empty;
        private int UID = 0;

        //   private AcroParser.DefinitionsLibs defLib;

        private int AcroCount = 0;
        private int DupsCount = 0;


        private void LoadData()
        {
            _AcroIgnoreListMgr = new AcroIgnoreList(AppFolders.AppDataPathToolsAcroSeekerIgnoreLib);


            if (_DefName != string.Empty)
            {
                _dsDefLib = _AcroIgnoreListMgr.GetDataset(_DefName);
                this.txtbFileName.Text = _DefName;
            }
            else
            {
                _dsDefLib = _AcroIgnoreListMgr.CreateEmptyDS();
            }

            lstAcronyms.Items.Clear();
            foreach (DataRow row in _dsDefLib.Tables[0].Rows)
            {
                lstAcronyms.Items.Add(row[1].ToString());
            }

            this.lblMessage.Text = _defualtMessage;

            ModeAdjust();

        }


        private void butAddBatchAcronyms_Click(object sender, EventArgs e)
        {
            string[] Acronyms = txtBatchAcronyms.Text.Trim().Split(',');

            if (Acronyms.Length == 0)
            {
                string msg = "Please enter comma-separated Acronyms for batch load.";
                MessageBox.Show(msg, "No Acronyms to Load", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (string newAcronym in Acronyms)
            {

                if (newAcronym.Trim() != string.Empty) // Validation
                {
                    if (lstAcronyms.FindStringExact(newAcronym.Trim()) == -1) // Validation
                    {
                        lstAcronyms.Items.Add(newAcronym.Trim());
                    }
                }

            }
        }

        private void butNew_Click(object sender, EventArgs e)
        {

            string newAcronym = this.txtAcron.Text.Trim();

            if (!ValidateAcronym(newAcronym, false))
                return;

            lstAcronyms.Items.Add(newAcronym);
        }



        private bool ValidateAcronym(string newAcronym, bool isReplace)
        {
            // Validation
            if (newAcronym == string.Empty)
            {
                MessageBox.Show("Please enter a Acronym.", "Unable to Enter a Blank Acronym", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblMessage.Text = "Please enter a Acronym. Blank Acronyms are not supported.";
                return false;
            }
            if (!isReplace)
            {
                if (lstAcronyms.FindStringExact(newAcronym) > -1)
                {
                    MessageBox.Show("Please enter another Acronym.", "Acronym must be Unique", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    lblMessage.Text = "Acronym must be unique. Please enter another Acronym.";
                    return false;
                }
            }

            return true;
        }

        private void butReplace_Click(object sender, EventArgs e)
        {
            string newAcronym = this.txtAcron.Text.Trim();

            int selectedIndex = lstAcronyms.SelectedIndex;

            if (!ValidateAcronym(newAcronym, true))
                return;


            if (selectedIndex > -1)
                lstAcronyms.Items[selectedIndex] = newAcronym;
        }

        private void butDelete_Click(object sender, EventArgs e)
        {
            int selectedIndex = lstAcronyms.SelectedIndex;

            if (selectedIndex > -1)
                lstAcronyms.Items.RemoveAt(selectedIndex);
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            if (lstAcronyms.Items.Count == 0) // Validate
            {
                MessageBox.Show("Please enter Acronyms before saving.", "No Acronyms", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtbFileName.Text.Trim() == string.Empty) // Validate
            {
                MessageBox.Show("Please enter Acronym Group Name before saving.", "No Acronym Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (isNew) // Validate -- Acronym Exists?
            {
                string fileName = txtbFileName.Text.Trim();


                string newPathFile = string.Concat(_path, @"\", fileName, ".xml");

                if (File.Exists(newPathFile))
                {
                    string msg = "Acronym group already exists! – Saving these changes will overwrite an existing Acronym Group.";
                    if (MessageBox.Show(msg, "Confirm Overwrite Existing Acronym Group", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }

            }


            _ds = _AcroIgnoreListMgr.CreateEmptyDS();
            int i = 0;
            foreach (string Acronym in lstAcronyms.Items)
            {
                DataRow row = _ds.Tables[0].NewRow();
                row[0] = i;
                row[1] = Acronym;
                _ds.Tables[0].Rows.Add(row);
                i++;
            }

            string pathFile = string.Concat(AppFolders.AppDataPathToolsAcroSeekerIgnoreLib, @"\", txtbFileName.Text.Trim(), ".xml");

            if (!_AcroIgnoreListMgr.SaveDataset(_ds, pathFile))
            {
                string errMsg = _AcroIgnoreListMgr.ErrorMessage;
                MessageBox.Show(errMsg, "Unable to Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void txtbMessage_TextChanged(object sender, EventArgs e)
        {
            txtbMessage.Text = lblMessage.Text;
        }

        private void lblMessage_TextChanged(object sender, EventArgs e)
        {
            txtbMessage.Text = lblMessage.Text;
        }

        private string _defualtMessage = string.Concat("Batch Load – Delimited Acronyms Syntax: ", Environment.NewLine,
                                        "Acronym1, Acronym2, Acronym3, Acronym4, ..."
                                        );

        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_subPanel == subPanels.BatchLoad)
            {
                this.lblMessage.Text = _defualtMessage;
            }
            else
            {
                this.lblMessage.Text = string.Concat("Add New: Enter New Ignore Acronym and click the Add button.", Environment.NewLine,
                                                      "Replace: Select an Ignore Acronym and either enter a new Acronym.", Environment.NewLine,
                                                      "Delete: Select an Ignore Acronym and click the Delete button.");
            }
        }

        private void butBatchLoad_MouseEnter(object sender, EventArgs e)
        {
            if (butBatchLoad.BackColor == Color.WhiteSmoke)
            {
                butBatchLoad.BackColor = Color.LightGreen;
            }
        }

        private void butBatchLoad_MouseLeave(object sender, EventArgs e)
        {
            if (_subPanel != subPanels.BatchLoad)
            {
                butBatchLoad.BackColor = Color.WhiteSmoke;
            }
        }

        private void butMaintain_MouseEnter(object sender, EventArgs e)
        {
            if (butMaintain.BackColor == Color.WhiteSmoke)
            {
                butMaintain.BackColor = Color.LightGreen;
            }
        }

        private void butBatchLoad_Click(object sender, EventArgs e)
        {
            _subPanel = subPanels.BatchLoad;
            ModeAdjust();
        }

        private void butMaintain_Click(object sender, EventArgs e)
        {
            _subPanel = subPanels.Maintain;
            ModeAdjust();
        }

        public void AdjButtons()
        {
            if (_subPanel == subPanels.BatchLoad)
            {
                butBatchLoad.BackColor = Color.LightGreen;
                butMaintain.BackColor = Color.WhiteSmoke;
            }
            else
            {
                butBatchLoad.BackColor = Color.WhiteSmoke;
                butMaintain.BackColor = Color.LightGreen;
            }
        }


        private void ModeAdjust()
        {
            panBatchLoad.Visible = false;
            panMaintain.Visible = false;

            AdjButtons();

            if (_subPanel == subPanels.BatchLoad)
            {
                panBatchLoad.Visible = true;
                panBatchLoad.Dock = DockStyle.Fill;
            }
            else
            {
                panMaintain.Visible = true;
                panMaintain.Dock = DockStyle.Fill;
            }

        }


    }
}
