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
    public partial class frmRefResContent : MetroFramework.Forms.MetroForm
    {
        public frmRefResContent(WorkgroupMgr.RefResMgr RefResMgr, string RefResName)
        {
            InitializeComponent();

            _RefResMgr = RefResMgr;
            _RefResName = RefResName;
            _isNew = true;
        }

        public frmRefResContent(WorkgroupMgr.RefResMgr RefResMgr, string RefResName, string ContentName, string ContentText)
        {
            InitializeComponent();

            _RefResMgr = RefResMgr;
            _RefResName = RefResName;
            _ContentName = ContentName;
            _ContentText = ContentText;
            _isNew = false;

            LoadData();
        }

        private WorkgroupMgr.RefResMgr _RefResMgr;
        private string _RefResName = string.Empty;
        private string _ContentName = string.Empty;
        private string _ContentText = string.Empty;
        private bool _isNew = true;

        private void LoadData()
        {
            if (!_isNew)
            {
                txtbContentName.Text = _ContentName;
                txtbContent.Text = _ContentText;
            }
        }

        private bool Validate()
        {
            if (txtbContentName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter a Reference Resource Content Name.", "Content Name Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtbContentName.Focus();
                return false;
            }

            if (txtbContent.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter Reference Resource Content.", "Content Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtbContent.Focus();
                return false;
            }

            string msg = string.Concat("There is already an existing Reference Resource Content with the same Name.", Environment.NewLine, Environment.NewLine,
                            "Please enter another Content Name.");


            if (_isNew) // check if this Content Name is already used
            {
                string txt = _RefResMgr.GetRefResContentText(_RefResName, txtbContentName.Text.Trim());
                if (txt.Length > 0)
                {

                    MessageBox.Show(msg, "Content Name Already Exits", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtbContentName.Focus();
                    return false;
                }

            }
            else
            {
                if (txtbContentName.Text.Trim() != _ContentName)
                {
                    string txt = _RefResMgr.GetRefResContentText(_RefResName, txtbContentName.Text.Trim());
                    if (txt.Length > 0)
                    { 
                        MessageBox.Show(msg, "Content Name Already Exits", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtbContentName.Focus();
                        return false;
                    }
                }
            }

            return true;
            
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            if (!Validate())
                return;

            if (!_RefResMgr.CreateRefResContent(_RefResName, txtbContentName.Text.Trim(), txtbContent.Text.Trim()))
            {
                if (_RefResMgr.ErrorMessage.Length > 0)
                {
                    MessageBox.Show(_RefResMgr.ErrorMessage, "Content Not Saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblDefinition.Text = _RefResMgr.ErrorMessage;
                    lblDefinition.ForeColor = Color.Tomato;
                    return;
                }

            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }


    }
}
