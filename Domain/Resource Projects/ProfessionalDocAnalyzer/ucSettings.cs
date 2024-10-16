using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ProfessionalDocAnalyzer
{
    public partial class ucSettings : UserControl
    {
        public ucSettings()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        // Declare delegate for when a project has been selected
        public delegate void ProcessHandler();

        [Category("Action")]
        [Description("Fires when a Menu Item is selected")]
        public event ProcessHandler MenuItemSelected;

        private string _SelectedMenuItem = string.Empty;
        public string SelectedMenuItem
        {
            get { return _SelectedMenuItem; }
        }

        string _LastWorkGroup = string.Empty;

        public void LoadData()
        {
            HidePanels();

            ucSettings_Home1.Visible = true;
            ucSettings_Home1.Dock = DockStyle.Fill;
            LoadMenu();
        }

        public bool New()
        {
            switch (SelectedMenuItem)
            {
                case SettingsMenu.Acronym_Dictionaries:
                    return ucAcroDictionaries1.New();

                case SettingsMenu.KeywordGroups:
                    return ucKeywordGroups1.New();

                case SettingsMenu.Tasks:
                    return ucTasksSettings1.New();

                case SettingsMenu.Dictionaries:
                    return ucDictionaries1.New();

                case SettingsMenu.RAM_Models:
                    return ucRAMModels1.New();

                case SettingsMenu.Templates:
                    return ucExcelTemplates1.New();


            }

            return false;
        }
        public bool Export()
        {

            return ucDictionaries1.Export();
        }

        public bool Edit()
        {
            switch (SelectedMenuItem)
            {
                case SettingsMenu.Acronym_Dictionaries:
                    return ucAcroDictionaries1.Edit();

                case SettingsMenu.KeywordGroups:
                    return ucKeywordGroups1.Edit();

                case SettingsMenu.Tasks:
                    return ucTasksSettings1.Edit();

                case SettingsMenu.Dictionaries:
                    return ucDictionaries1.Edit();

                case SettingsMenu.RAM_Models:
                    return ucRAMModels1.Edit();

                case SettingsMenu.Templates:
                    return ucExcelTemplates1.Edit();

                case SettingsMenu.QCReadability:
                    return ucQCSettings1.Edit();
                   

            }

            return false;
        }

        public bool Delete()
        {
            switch (SelectedMenuItem)
            {
                case SettingsMenu.Acronym_Dictionaries:
                    return ucAcroDictionaries1.Delete();

                case SettingsMenu.Tasks:
                    return ucTasksSettings1.Delete();

                case  SettingsMenu.KeywordGroups:
                    return ucKeywordGroups1.Delete();

                case SettingsMenu.Dictionaries:
                    return ucDictionaries1.Delete();

                case SettingsMenu.RAM_Models:
                    return ucRAMModels1.Delete();

                case SettingsMenu.Templates:
                    return ucExcelTemplates1.Delete();

            }

            return false;
        }

        public void Import()
        {
            switch (SelectedMenuItem)
            {
                case SettingsMenu.Dictionaries:
                    ucDictionaries1.Import();

                    break;
                    


            }

        }

        public void Download()
        {
            switch (SelectedMenuItem)
            {
                case SettingsMenu.Acronym_Dictionaries:
                    ucAcroDictionaries1.Download();
                    break;

                case SettingsMenu.Dictionaries:
                    ucDictionaries1.Download();
                    break;

                case SettingsMenu.Templates:
                    ucExcelTemplates1.Download();
                    break;

                case SettingsMenu.KeywordGroups:
                    ucKeywordGroups1.Download();
                    break;

                case SettingsMenu.Tasks:
                    ucTasksSettings1.Download();
                    break;

            }


        }


        private void LoadMenu()
        {
            lstbSettings.Items.Clear();

            lstbSettings.Items.Add(SettingsMenu.KeywordGroups);
            lstbSettings.Items.Add(SettingsMenu.Dictionaries);
            lstbSettings.Items.Add(SettingsMenu.RAM_Models);
            lstbSettings.Items.Add(SettingsMenu.Acronym_Dictionaries);
          //  lstbSettings.Items.Add(SettingsMenu.FARs_DFARs);
            lstbSettings.Items.Add(SettingsMenu.Templates);
            lstbSettings.Items.Add(SettingsMenu.Tasks);
          //  lstbSettings.Items.Add(SettingsMenu.InstructionsAndQuestions);
          //  lstbSettings.Items.Add(SettingsMenu.QCReadability);   

        }


        private void HidePanels()
        {
            ucSettings_Home1.Visible = false;
            ucAcroDictionaries1.Visible = false;
            ucDictionaries1.Visible = false;
            ucKeywordGroups1.Visible = false;
        //    ucQuestionsInstructions1.Visible = false;
            ucTasksSettings1.Visible = false;
            ucRAMModels1.Visible = false;
            ucExcelTemplates1.Visible = false;
            ucQCSettings1.Visible = false;

        }

        private void ModeAdjustments()
        {
            HidePanels();

            switch (_SelectedMenuItem)
            {
                case SettingsMenu.Acronym_Dictionaries:
                    ucAcroDictionaries1.LoadData();
                    ucAcroDictionaries1.Visible = true;
                    ucAcroDictionaries1.Dock = DockStyle.Fill;

                    break;

                case SettingsMenu.Dictionaries:
                    ucDictionaries1.LoadData(AppFolders.DictionariesPath);
                    ucDictionaries1.Visible = true;
                    ucDictionaries1.Dock = DockStyle.Fill;

                    break;

                case SettingsMenu.KeywordGroups:
                    ucKeywordGroups1.LoadData();
                    ucKeywordGroups1.Visible = true;
                    ucKeywordGroups1.Dock = DockStyle.Fill;

                    break;

                //case SettingsMenu.InstructionsAndQuestions:
                //    ucQuestionsInstructions1.LoadData();
                //    ucQuestionsInstructions1.Visible = true;
                //    ucQuestionsInstructions1.Dock = DockStyle.Fill;

                //    break;

                case SettingsMenu.Tasks:
                    ucTasksSettings1.LoadData();
                    ucTasksSettings1.Visible = true;
                    ucTasksSettings1.Dock = DockStyle.Fill;

                    break;

                case SettingsMenu.RAM_Models:
                    ucRAMModels1.LoadData();
                    ucRAMModels1.Visible = true;
                    ucRAMModels1.Dock = DockStyle.Fill;

                    break;

                case SettingsMenu.Templates:
                    ucExcelTemplates1.LoadData();
                    ucExcelTemplates1.Visible = true;
                    ucExcelTemplates1.Dock = DockStyle.Fill;

                    break;

                case SettingsMenu.QCReadability:
                    ucQCSettings1.LoadData();
                    ucQCSettings1.Visible = true;
                    ucQCSettings1.Dock = DockStyle.Fill;

                    break;

            }
        }

        private void lstbSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            _SelectedMenuItem = lstbSettings.Text;
            ModeAdjustments();

            if (MenuItemSelected != null)
                MenuItemSelected();

        }

        private void ucSettings_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (_LastWorkGroup.Length > 0)
                {
                    if (_LastWorkGroup != AppFolders.WorkgroupName)
                    {
                        LoadData();
                    }
                }
            }
            else
            {
                _LastWorkGroup = AppFolders.WorkgroupName;
            }
        }

        private void butDefaultReset_Click(object sender, EventArgs e)
        {
            string msg = "The Reset the Default Settings will replace your all existing settings for your current Workgroup. But will not replace any new Templates or Libraries, unless the Default contains the same name.";
            if (MessageBox.Show(msg, "Confirm Default Settings Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            Cursor.Current = Cursors.WaitCursor; // Waiting

            HidePanels();

            Atebion.WorkGroups.Manager workgroupMgr;
            workgroupMgr = new Atebion.WorkGroups.Manager(AppFolders.AppDataPath, AppFolders.UserName, AppFolders.AppDataPathUser);
            workgroupMgr.ApplicationPath = Application.StartupPath;

            workgroupMgr.ImportDefaultSettingFiles();
            LoadData();

            Cursor.Current = Cursors.Default; // Done
        }





    }
}
