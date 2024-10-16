using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ParamClipBrd
{
    public partial class frmMain : Form
    {
        public frmMain(string Parameters)
        {
            InitializeComponent();

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.TopMost = true;
            this.Opacity = 1;

            _Parameters = Parameters;

            ShowNotice();

            LoadData();

        }

        public frmMain()
        {
            InitializeComponent();

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.TopMost = true;
            this.Opacity = 1;

            ShowNotice();

        }

        private string _Parameters = string.Empty;

        private void ShowNotice()
        {
            txtbNote1.Text = "Storyboard Data Fields in MS Word are often shown as a misspelled word. Matrix Builder cannot populate Data Fields denoted with a squiggly red line.";

            txtbNote2.Text = string.Concat("To enable Atebion products to populate a Data Field: ",
                Environment.NewLine, "1.  Select the Field Name",
                Environment.NewLine, "2.  Right Mouse-Click",
                Environment.NewLine, "       A Popup Menu should appear",
                Environment.NewLine, "3.  Click on the “ignore” menu item");

            txtbNote3.Text = "Your Data Field should Not have a squiggly red line. However, MS Word will sometimes insert hidden characters if you make adjustments, avoid backspacing.";

        }

        private void LoadData()
        {

            if (_Parameters == null)
                return;

            if (_Parameters == string.Empty)
                return;

            if (_Parameters.IndexOf('|') < 0) // Delimiter
            {
                this.lstParameters.Items.Add(_Parameters);
                return;
            }

            string[] param = _Parameters.Split('|');

            foreach (string val in param)
            {
                if (val != string.Empty)
                {
                    this.lstParameters.Items.Add(val);
                }
            }
        }


        private void butSave2Clipbrd_Click(object sender, EventArgs e)
        {
            if (txtMergeText.Text.Length == 0) // Added 09.15.2019
            {
                MessageBox.Show("Please select a field from the list below.", "No Field Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Clipboard.SetText(this.txtMergeText.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void chkbTransparent_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkbTransparent.Checked)
            {
                this.Opacity = 0.5;
            }
            else
            {
                this.Opacity = 1;
            }
        }

        private void lstParameters_Click(object sender, EventArgs e)
        {
            if (lstParameters.Items.Count == 0)
                return;

            this.txtMergeText.Text = string.Concat("{{", lstParameters.SelectedItem.ToString(), "}}");

            if (chkListClick.Checked)
                Clipboard.SetText(this.txtMergeText.Text);
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            //if (lstParameters.Items.Count == 0)
            //    Application.Exit();
            //else
            //{
                panNotice.Visible = false;
                this.Width = 294;
            //}
        }

        private void chkListClick_CheckedChanged(object sender, EventArgs e)
        {
            if (chkListClick.Checked)
                butSave2Clipbrd.Visible = false;
            else
                butSave2Clipbrd.Visible = true;
        }

  

   
    }
}
