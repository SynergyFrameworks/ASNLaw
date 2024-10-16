using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Domain.Common;

namespace Domain.ConceptAnalyzer
{
    class Projects
    {
        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        public string[] GetProjects()
        {
            string[] projects;

            _ErrorMessage = string.Empty;

            string projectPaths = AppFolders_CA.Project;
            if (projectPaths.Length == 0)
            {
                _ErrorMessage = AppFolders_CA.ErrorMessage;
                return null;
            }

            projects = Directory.GetDirectories(projectPaths);

            List<string> projectNames = new List<string>();

            string projectName = string.Empty;
            foreach (string project in projects)
            {
                projectName = Directories.GetLastFolder(project);
                projectNames.Add(projectName);
            }


            return projectNames.ToArray();
        }

        public bool ProjectExists(string ProjectName)
        {
            string[] projects = GetProjects();

            string xProjectName = ProjectName.ToUpper();

            string xproject = string.Empty;
            foreach (string project in projects)
            {
                xproject = project.ToUpper();

                if (xProjectName == xproject) 
                {
                    return true;
                }
            }

            return false;

        }

        public string ProjectFileDetails(string ProjectName, string FileName, bool isDAProject, out string FileNameWExt, out bool Parsed, out string ext, out string DocPath)
        {
            _ErrorMessage = string.Empty;

            FileNameWExt = string.Empty;
            Parsed = false;
            ext = string.Empty;
            DocPath = string.Empty;

            string fileDetails = string.Empty;

            AppFolders_CA.ProjectName = ProjectName;

            string s = AppFolders_CA.Project;
            string projectFolder = AppFolders_CA.ProjectCurrent;
            string ProjectDocsFolder = AppFolders_CA.DocPath;

            string[] docs = Directory.GetDirectories(ProjectDocsFolder);

            if (docs == null)
            {
                return string.Empty; ;
            }
            
            string file = string.Empty;

            foreach (string doc in docs)
            {
                file = Directories.GetLastFolder(doc);

                if (FileName == file)
                {
                    AppFolders_CA.DocName = FileName;

                    string pattern = string.Concat(FileName, ".*");
                    DocPath = AppFolders_CA.AnalysisCurrentDocsDocName;
                    string[] files = Directory.GetFiles(DocPath, pattern);
                    {
                        int fileCount = files.Length;
                        if (fileCount > 0)
                        {
                            if (fileCount == 1)
                            {
                                string extX = string.Empty;
                                string fileX = files[0];
                                FileNameWExt = Files.GetFileName(fileX, out extX);
                                ext = extX;
                            }
                            else
                            {
                                string extX = string.Empty;
                                string fileX = string.Empty;
                                foreach (string f in files)
                                {
                                    fileX = Files.GetFileName(f, out extX);
                                    if (extX.ToUpper() != "TXT")
                                    {
                                        FileNameWExt = fileX;
                                        ext = extX;
                                        break;
                                    }
                                }

                            }
                        }

                    }


                    // Get File Detail Information
                    string infoPath = AppFolders_CA.AnalysisInfor;
                    if (infoPath.Length > 0)
                    {
                        string infoFile = "Info.txt";

                        string infoPathFile = Path.Combine(infoPath, infoFile);
                        if (File.Exists(infoPathFile))
                        {
                            fileDetails = Files.ReadFile(infoPathFile);
                        }

                    }

                    // Check for  Parsed Segments //ParseSeg
                    string parseSegPath = AppFolders_CA.DocParsedSec;
                    if (parseSegPath.Length > 0)
                    {
                        string[] parseFiles = Directory.GetFiles(parseSegPath);
                        if (parseFiles.Length > 0)
                            Parsed = true;
                    }

                    break;

                }

               
            }

            return fileDetails;

        }

        public string[] GetProjectFiles(string ProjectName, bool isDAProject)
        {
            _ErrorMessage = string.Empty;

            AppFolders_CA.ProjectName = ProjectName;

            string s = AppFolders_CA.Project;
            string projectFolder = AppFolders_CA.ProjectCurrent;
            string projectDocsFolder = AppFolders_CA.DocPath;

            string[] docs = Directory.GetDirectories(projectDocsFolder);

            if (docs == null)
            {
                return null;
            }

            List<string> files = new List<string>();
            string file = string.Empty;

            foreach (string doc in docs)
            {
                file = Directories.GetLastFolder(doc);
                files.Add(file);
            }


            return files.ToArray();
        }

        public bool ProjectNew(string ProjectName, string Description, bool isDAProject)
        {
            _ErrorMessage = string.Empty;

            if (ProjectExists(ProjectName))
            {
                _ErrorMessage = string.Concat("Project '", ProjectName, "' already exists.");
                return false;
            }

            string s = AppFolders_CA.Project;

            if (s.Length == 0)
            {
                _ErrorMessage = AppFolders_CA.ErrorMessage;
                return false;
            }

            AppFolders_CA.ProjectName = ProjectName;

            string projFolder = AppFolders_CA.ProjectCurrent;
            if (projFolder.Length == 0)
            {
                _ErrorMessage = AppFolders_CA.ErrorMessage;
                return false;
            }

            string descriptionPathFile = Path.Combine(projFolder, "Description.txt");

            Files.WriteStringToFile(Description, descriptionPathFile);

            s = AppFolders_CA.DocPath;

            return true;
        }

        public string GetProjectDescription(string ProjectName)
        {
            _ErrorMessage = string.Empty;

            string s = AppFolders_CA.Project;

            if (s.Length == 0)
            {
                _ErrorMessage = AppFolders_CA.ErrorMessage;
                return string.Empty;
            }

            AppFolders_CA.ProjectName = ProjectName;

            string projFolder = AppFolders_CA.ProjectCurrent;
            if (projFolder.Length == 0)
            {
                _ErrorMessage = AppFolders_CA.ErrorMessage;
                return string.Empty;
            }

            string descriptionPathFile = Path.Combine(projFolder, "Description.txt");

            string description = Files.ReadFile(descriptionPathFile);

            if (description.Length == 0)
            {
                _ErrorMessage = Files.ErrorMessage;
            }

            return description;
        }

    }
}
