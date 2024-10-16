using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Atebion.Common;

namespace ProfessionalDocAnalyzer
{
    class ProjectManager
    {
        public ProjectManager(string projectName) // Edit
        {
            _projectName = projectName;
        }

        public ProjectManager(bool CreateDefault) // New
        {
            _CreateDefault = CreateDefault;
        }

        public ProjectManager() // Neither New or Edit
        {
            
        }

        

        #region Private Var.s

        

        private string _projectName = string.Empty;
        private string _ProjectCurrentPath = string.Empty;
        private bool _CreateDefault = false;

        private const string DESCRIPTION_FILE_NAME = "ProjDescription.txt";

        #endregion

        #region Private Functions

        public bool LoadData()
        {
            _ErrorMessage = string.Empty;

            AppFolders.ProjectName = _projectName;

            _ProjectCurrentPath = AppFolders.ProjectCurrent;

            if (_projectName == string.Empty)
            {
                _ErrorMessage = "Project Name is Not Defined";
                return false;
            }

            return true;

        }

        public List<string> GetProjectNames()
        {
            if (AppFolders.Project == string.Empty)
                return null;

            List<string> list = new List<string>();
            string lastFolderName = string.Empty;
            string s = string.Empty;

            string[] projectsPaths = Directory.GetDirectories(AppFolders.Project);

            for (int i = 0; i < projectsPaths.Length; i++)
            {
                s = projectsPaths[i];
  
                lastFolderName = Directories.GetLastFolder(projectsPaths[i]);
                if (lastFolderName.IndexOf("~") == -1) // check for projects that have been removed
                    list.Add(lastFolderName);
                
            }

            return list;
        }

        public bool UpdateDescription(string descriptionText)
        {
            _ProjectCurrentPath = AppFolders.ProjectCurrent;
            if (_ProjectCurrentPath == string.Empty)
            {
                _ErrorMessage = AppFolders.ErrorMessage;
                return false;
            }

            string projDescription = Path.Combine(_ProjectCurrentPath, DESCRIPTION_FILE_NAME);

            try
            {
                Files.WriteStringToFile(descriptionText, projDescription);
            }
            catch
            {
                return false;
            }

            return true;
        }

        #endregion


        #region Public Properties
        public string ProjectName
        {
            set
            {
                _projectName = value;

                AppFolders.ProjectName = _projectName;
                AppFolders.DocName = string.Empty; // Reset default

                _ProjectCurrentPath = AppFolders.ProjectCurrent;

            }
            get { return _projectName; }
        }

        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        private string _Description = string.Empty;
        public string Description
        {
            get
            {
                if (_projectName == string.Empty)
                {
                    _ErrorMessage = "Project Name is Not Set";
                    return string.Empty;
                }

                if (_ProjectCurrentPath == string.Empty)
                {
                    _ErrorMessage = "Current Project Path is Unknown";
                    return string.Empty;
                }

                string projDescription = string.Concat(_ProjectCurrentPath, @"\", DESCRIPTION_FILE_NAME);

                if (!File.Exists(projDescription))
                    return string.Empty;

                _Description = Files.ReadFile(projDescription);
                return _Description;
            }
            set
            {
                _ErrorMessage = string.Empty;
                _Description = value;
                UpdateDescription(_Description);
            }
        }

        #endregion

        public bool CreateProject(string descriptionText)
        {
            if (_projectName == string.Empty)
            {
                _ErrorMessage = "Project Name is Not Defined";
                return false;
            }

            if (Directory.Exists(Path.Combine(AppFolders.Project, _projectName)))
            {
                //_ErrorMessage = "Project Name already exists.";
                //return false;
            }

            AppFolders.ProjectName = _projectName;

            _ProjectCurrentPath = AppFolders.ProjectCurrent;
            if (_ProjectCurrentPath == string.Empty)
            {
                _ErrorMessage = AppFolders.ErrorMessage;
                return false;
            }

            string projDescription = Path.Combine(_ProjectCurrentPath, DESCRIPTION_FILE_NAME);

            Files.WriteStringToFile(descriptionText, projDescription);

            return true;

        }

    }
}
