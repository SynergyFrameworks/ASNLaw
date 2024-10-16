using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace ProfessionalDocAnalyzer
{
    public partial class frmLastestReleaseInfo : MetroFramework.Forms.MetroForm
    {
        public frmLastestReleaseInfo(string ReleaseMsg, string ReleaseLog, string LatestRelease, bool ShowHide)
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();

            _ReleaseMsg = ReleaseMsg;
            _ReleaseLog = ReleaseLog;
            _LatestRelease = LatestRelease;
            _ShowHide = ShowHide;

            LoadData();
        }

        private string _ReleaseMsg = string.Empty;
        private string _ReleaseLog = string.Empty;
        private string _LatestRelease = string.Empty;
        private bool _ShowHide = true;

        private void LoadData()
        {
            lblMessage.Text = _ReleaseMsg;
            LblLog.Text = _ReleaseLog;

            this.chkbHide.Visible = _ShowHide;

            this.butCancel.Select(); // Added 11.20.2015
        }


        private void LblLog_TextChanged(object sender, EventArgs e)
        {
            txtLog.Text = LblLog.Text;
        }

        private void txtMessage_TextChanged(object sender, EventArgs e)
        {
            txtMessage.Text = lblMessage.Text; ;
        }

        private void lblMessage_TextChanged(object sender, EventArgs e)
        {
            txtMessage.Text = lblMessage.Text;
        }
        
        private void butDownLatest_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"http://www.atebionllc.com/downloadProfNEUpdate.html"); 
        }

        private void txtLog_TextChanged(object sender, EventArgs e)
        {
            txtLog.Text = LblLog.Text;
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            if (chkbHide.Visible)
            { 
                string hideThis = "No";
                if (chkbHide.Checked)
                {
                    hideThis = "Yes";
                }

                CurrentSettings.SetAppInIFile(CurrentSettings.configSecUserSettings, CurrentSettings.configKeyHideLastestRelease, hideThis);
                CurrentSettings.SetAppInIFile(CurrentSettings.configSecUserSettings, CurrentSettings.configKeyLatestRelease, _LatestRelease);
            }

            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
