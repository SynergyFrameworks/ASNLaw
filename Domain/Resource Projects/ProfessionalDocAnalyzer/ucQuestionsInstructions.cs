using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ProfessionalDocAnalyzer
{
    public partial class ucQuestionsInstructions : UserControl
    {
        public ucQuestionsInstructions()
        {
            InitializeComponent();
        }

        private string _customPathFile = Path.Combine(AppFolders.AppDataPath, "QueryStart.txt");
        private string _defaultPathFile = Path.Combine(Application.StartupPath, "QueryStart.txt");

        private string _customEnbeddedPathFile = Path.Combine(AppFolders.AppDataPath, "QueryEmbedded.txt");
        private string _defaultEnbeddedPathFile = Path.Combine(Application.StartupPath, "QueryEmbedded.txt");

        // --- Code Commented-Out - Was causing an IO error -- This panel is currently not being used
    //    IniFile _inifile;

        public void LoadData()
        {
            //_inifile = new IniFile();

            //_inifile.Load(AppFolders.InIFile);
            //string iniLatestRelease = _inifile.GetKeyValue(CurrentSettings.configSecUserSettings, CurrentSettings.configKeyUseDefaultInstQuestParameters);

            //if (iniLatestRelease.ToLower() != "no")
            //{
            //    rdoInstuctQuestDefault.Checked = true;
            //}
            //else
            //{
            //    rdoInstuctQuestCustom.Checked = true;
            //}

        }


        private void butInstructQuestSentStarting_Click(object sender, EventArgs e)
        {
            //if (File.Exists(_customPathFile))
            //{
            //    System.Diagnostics.Process.Start("notepad.exe", _customPathFile);

            //}
            //else if (File.Exists(_defaultPathFile))
            //{
            //    File.Copy(_defaultPathFile, _customPathFile);
            //    System.Diagnostics.Process.Start("notepad.exe", _customPathFile);
            //}
            //else
            //{
            //    MessageBox.Show("Unable to find Parameter Definitions for Sentences Starting with …", "Please Contact Atebion LLC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
        }

        private void butInstructQuestSentEmbedded_Click(object sender, EventArgs e)
        {

            //if (File.Exists(_customEnbeddedPathFile))
            //{
            //    System.Diagnostics.Process.Start("notepad.exe", _customEnbeddedPathFile);

            //}
            //else if (File.Exists(_defaultEnbeddedPathFile))
            //{
            //    File.Copy(_defaultEnbeddedPathFile, _customEnbeddedPathFile);
            //    System.Diagnostics.Process.Start("notepad.exe", _customEnbeddedPathFile);
            //}
            //else
            //{
            //    MessageBox.Show("Unable to find Parameter Definitions for Sentences with Embedded Phrases.", "Please Contact Atebion LLC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
        }

        private void butDefaultInstructQuestSentStarting_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Are you sure you want to reset to Default?", "Confirm Reset to Default", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    if (File.Exists(_customPathFile))
            //    {
            //        File.Delete(_customPathFile);
            //    }

            //    if (File.Exists(_defaultPathFile))
            //    {
            //        File.Copy(_defaultPathFile, _customPathFile);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Unable to find Parameter Definitions for Sentences Starting with …", "Please Contact Atebion LLC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    }
            //}
        }

        private void butDefaultInstructQuestSentEmbedded_Click(object sender, EventArgs e)
        {

            //if (MessageBox.Show("Are you sure you want to reset to Default?", "Confirm Reset to Default", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    if (File.Exists(_defaultEnbeddedPathFile))
            //    {
            //        if (File.Exists(_customEnbeddedPathFile))
            //        {
            //            File.Delete(_customEnbeddedPathFile);
            //        }

            //        File.Copy(_defaultEnbeddedPathFile, _customEnbeddedPathFile);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Unable to find Parameter Definitions for Sentences with Embedded Phrases.", "Please Contact Atebion LLC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    }
            //}
        }

        private void rdoInstuctQuestDefault_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoInstuctQuestDefault.Checked)
            //{
            //    CurrentSettings.SetAppInIFile(CurrentSettings.configSecUserSettings, CurrentSettings.configKeyUseDefaultInstQuestParameters, "yes");
            //}
        }

        private void rdoInstuctQuestCustom_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoInstuctQuestCustom.Checked)
            //{
            //    CurrentSettings.SetAppInIFile(CurrentSettings.configSecUserSettings, CurrentSettings.configKeyUseDefaultInstQuestParameters, "no");
            //}
        }
    }
}
