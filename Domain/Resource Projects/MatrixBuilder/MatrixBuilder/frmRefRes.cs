using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using WorkgroupMgr;

namespace MatrixBuilder
{
    public partial class frmRefRes : MetroFramework.Forms.MetroForm
    {
        public frmRefRes(WorkgroupMgr.RefResMgr refResMgr)
        {
            InitializeComponent();

            _RefResMgr = refResMgr;

            LoadData();
        }

        private string INTERNAL_NOTICE = "Internal - This Reference Resource will be associated with the current/selected Workgroup and should not be shared with other Workgroups. Typically, Reference Resources associated with a Workgroup are at a Team Level.";
        private string SHARED_NOTICE = "Shared - This Reference Resource can easily be shared and modified by other Workgroups. Typically, Reference Resource that are shared are shared a the company level or company division level.";

        private WorkgroupMgr.RefResMgr _RefResMgr;


        private Modes _currentMode = Modes.Internal;
        private enum Modes
        {
            Internal = 0,
            Shared = 1
        }

        private void LoadData()
        {
            AdjMode();
        
        }

        private void AdjMode()
        {
            if (_currentMode == Modes.Internal)
            {
                lblRefResTypeNotice.Text = INTERNAL_NOTICE;
                butShared.Highlight = false;
                butInternal.Highlight = true;

                txtbRefResPath.Visible = false;
                butBrowse.Visible = false;
            }
            else if (_currentMode == Modes.Shared)
            {
                lblRefResTypeNotice.Text = SHARED_NOTICE;
                butInternal.Highlight = false;
                butShared.Highlight = true;

                txtbRefResPath.Visible = true;
                butBrowse.Visible = true;
            }

            butShared.Refresh();
            butInternal.Refresh();
        }

        private void butInternal_Click(object sender, EventArgs e)
        {
            _currentMode = Modes.Internal;
            AdjMode();
        }

        private void butShared_Click(object sender, EventArgs e)
        {
            _currentMode = Modes.Shared;
            AdjMode();
        }

        private bool Validate()
        {
            if (txtbRefResName.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter a Reference Resource Name.", "Reference Resource Name Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtbRefResName.Focus();
                return false;
            }
            
            if (txtbDescription.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter the Reference Resource Description.", "Reference Resource Description Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtbDescription.Focus();
                return false;
            }

            if (_currentMode == Modes.Shared)
            {
                if (txtbRefResPath.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Please enter the Shared Reference Resource Path.", "Shared Reference Resource Path Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtbRefResPath.Focus();
                    return false;
                }
            }

            if (_currentMode != Modes.Internal) // Added 14.04.2017
            {
                if (!Directory.Exists(txtbRefResPath.Text.Trim()))
                {
                    MessageBox.Show("Unable to locate Shared Reference Resource Path or you do not have access to this path (folder). Contact your company’s System Administrator for access rights.", "Unable to Find Shared Reference Resource Path", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtbDescription.Focus();
                    return false;
                }
            }
            

            return true;
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            if (!Validate())
            {
                return;
            }

            string refResName = txtbRefResName.Text.Trim();
            string description = txtbDescription.Text.Trim();
            string locationType = string.Empty;
            string path = string.Empty;
            if (_currentMode == Modes.Internal)
            {
                locationType = WorkgroupMgr.RefResFields.LocationType_Internal;
            }
            else
            {
                locationType = WorkgroupMgr.RefResFields.LocationType_Shared;
                path = txtbRefResPath.Text.Trim();
            }


            if (!_RefResMgr.CreateRefRes(refResName, description, locationType, path))
            {
                MessageBox.Show(_RefResMgr.ErrorMessage, "Reference Resource Not Saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblDefinition.Text = _RefResMgr.ErrorMessage;
                lblDefinition.ForeColor = Color.Tomato;
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
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
                txtbRefResPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

    }
}
