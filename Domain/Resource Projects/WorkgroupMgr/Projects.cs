using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace WorkgroupMgr
{
    public class Projects
    {
        public Projects(string ProjectsRootFolder)
        {
            _ProjectsRootFolder = ProjectsRootFolder;
        }

        private string _ProjectsRootFolder = string.Empty;

        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        private const string DEEP_ANALYSIS_FILE_XML = "Sentences.xml";

        public string[] GetProjectNames()
        {
            _ErrorMessage = string.Empty;

            List<string> lstProjects = new List<string>();

            try
            {
                string projectName = string.Empty;
                string[] dirs = Directory.GetDirectories(_ProjectsRootFolder);
                foreach (string dir in dirs)
                {
                    if (dir.IndexOf('~') == -1) // ~ are Deleted dirs
                    {
                        projectName = Files.GetLastFolder(dir);
                        lstProjects.Add(projectName);
                    }
                }

            }
            catch (Exception ex)
            {
                _ErrorMessage = string.Concat("Unable to get projects due to an Error.", Environment.NewLine, Environment.NewLine, ex.Message);
                return null;
            }

            return lstProjects.ConvertAll(x => x.ToString()).ToArray(); 
        }

        public string GetProjectDocARPath_ParsedSections(string project, string docName)
        {
            _ErrorMessage = string.Empty;

            string path = Path.Combine(_ProjectsRootFolder, project, "Docs", docName, "Current", "ParseSec");

            if (!Directory.Exists(path))
            {
                _ErrorMessage = string.Concat("Unable to find Project ", project, "'s document '", docName, "' Analysis Results folder at ", path);
                return string.Empty;
            }

            return path;
        }

        public string GetProjectDocDARPath_ParseSentences(string project, string docName)
        {
            _ErrorMessage = string.Empty;

            string path = Path.Combine(_ProjectsRootFolder, project, "Docs", docName, "Current", "Deep Analytics", "Current", "Parse Sentences");

            if (!Directory.Exists(path))
            {
                _ErrorMessage = string.Concat("Unable to find Project ", project, "'s document '", docName, "' Deep Analysis folder at ", path);
                return string.Empty;
            }

            return path;
        }

        public string GetProjectDocDARPath_XML(string project, string docName)
        {
            _ErrorMessage = string.Empty;

            string path = Path.Combine(_ProjectsRootFolder, project, "Docs", docName, "Current", "Deep Analytics", "Current", "XML");

            if (!Directory.Exists(path))
            {
                _ErrorMessage = string.Concat("Unable to find Project ", project, "'s document '", docName, "' Deep Analysis XML folder at ", path);
                return string.Empty;
            }

            return path;
        }

        public string GetProjectDocDARPath_Index(string project, string docName)
        {
            _ErrorMessage = string.Empty;

            string path = Path.Combine(_ProjectsRootFolder, project, "Docs", docName, "Current", "Deep Analytics", "Current", "Index2");

            if (!Directory.Exists(path))
            {
                _ErrorMessage = string.Concat("Unable to find Project ", project, "'s document '", docName, "' Deep Analysis Search Index folder at ", path);
                return string.Empty;
            }

            return path;
        }

        // Added 07.31.2019
        public string GetProjectDocDARPath_HootIndex(string project, string docName)
        {
            _ErrorMessage = string.Empty;

            string path = Path.Combine(_ProjectsRootFolder, project, "Docs", docName, "Current", "Deep Analytics", "Current", "Index");

            if (!Directory.Exists(path))
            {
                _ErrorMessage = string.Concat("Unable to find Project ", project, "'s document '", docName, "' Deep Analysis Search Index folder at ", path);
                return string.Empty;
            }

            return path;
        }

        public string GetProjectDocDARPath_Notes(string project, string docName)
        {
            _ErrorMessage = string.Empty;

            string path = Path.Combine(_ProjectsRootFolder, project, "Docs", docName, "Current", "Deep Analytics", "Current", "Notes");

            if (!Directory.Exists(path))
            {
                _ErrorMessage = string.Concat("Unable to find Project ", project, "'s document '", docName, "' Deep Analysis Notes folder at ", path);
                return string.Empty;
            }

            return path;
        }

        public bool DeepAnalysis_Exists(string project, string docName)
        {
            string pathXML = GetProjectDocDARPath_XML(project, docName);

            if (pathXML == string.Empty)
            {
                return false;
            }

            string pathFile = Path.Combine(pathXML, DEEP_ANALYSIS_FILE_XML);
            if (!File.Exists(pathFile))
            {   
                return false;
            }

            return true;
        }

        public bool AnalysisResults_Exists(string project, string docName)
        {
            string path = GetProjectDocARPath_ParsedSections(project, docName);
            if (path == string.Empty)
            {
                return false;
            }

            string[] files = Directory.GetFiles(path, "*.rtf");
            if (files.Length == 0)
            {
                return false;
            }

            return true;
        }

        public DataTable GetDeepAnalysisResults(string project, string docName)
        {
            _ErrorMessage = string.Empty;

            string pathXML = GetProjectDocDARPath_XML(project, docName);
            if (pathXML == string.Empty)
            {
                return null;
            }

            string pathFile = Path.Combine(pathXML, DEEP_ANALYSIS_FILE_XML);
            if (!File.Exists(pathFile))
            {
                _ErrorMessage = string.Concat("Unable to find Deep Analysis Data file: ", pathFile);
                return null;
            }

            DataSet ds = DataFunctions.LoadDatasetFromXml(pathFile);
            if (ds == null)
            {
                _ErrorMessage = DataFunctions._ErrorMessage;
                return null;
            }

            return ds.Tables[0];
            
        }

        public DataTable GetAnalysisResults(string project, string docName)
        {
            _ErrorMessage = string.Empty;

            string path = Path.Combine(_ProjectsRootFolder, project, "Docs", docName, "Current", "ParseSec", "XML");

            if (!Directory.Exists(path))
            {
                _ErrorMessage = string.Concat("Unable to find Project ", project, "'s document '", docName, "' Analysis Results XML (data) folder at ", path);
                return null;
            }

            string pathFile = Path.Combine(path, "ParseResults.xml");
            if (!File.Exists(pathFile))
            {
                _ErrorMessage = string.Concat("Unable to find Project ", project, "'s document '", docName, "' Analysis Results XML (data) file: ", pathFile);
                return null;
            }

            DataSet ds = DataFunctions.LoadDatasetFromXml(pathFile);
            if (ds == null)
            {
                _ErrorMessage = string.Concat("Unable to load Project ", project, "'s document '", docName, "' Analysis Results XML (data) file: ", pathFile, Environment.NewLine, Environment.NewLine, DataFunctions._ErrorMessage);
                return null;
            }

            return ds.Tables[0];

        }

        public string GetDocFile(string project, string docName)
        {
            _ErrorMessage = string.Empty;

            string projectDocPath = Path.Combine(_ProjectsRootFolder, project, "Docs", docName, "Current");

            if (!Directory.Exists(projectDocPath))
            {
                _ErrorMessage = string.Concat("Unable to find Document Analyzer's Document folder: ", projectDocPath);

                return null;
            }
          
        //    string[] files = Directory.GetFiles(projectDocPath, "*.txt,*.rtf", SearchOption.TopDirectoryOnly);

            string[] extensions = { "rtf", "txt"};

            string[] files = Directory.GetFiles(projectDocPath, "*.*")
                .Where(f => extensions.Contains(f.Split('.').Last().ToLower())).ToArray();

            if (files.Length == 0)
            {
                _ErrorMessage = string.Concat("Unable to find Document Analyzer's Document: ", docName);
                return null;
            }

            return files[0];

            //string file = string.Concat(docName, ".txt");
            //string docPathFile = Path.Combine(projectDocPath, file);
            //if (File.Exists(docPathFile))
            //{
            //    return docPathFile;
            //}


            //file = string.Concat(docName, ".rtf");
            //docPathFile = Path.Combine(projectDocPath, file);
            //if (File.Exists(docPathFile))
            //{
            //    return docPathFile;
            //}

            

        }
        public string[] GetDocuemntsNames(string project)
        {
            _ErrorMessage = string.Empty;

            string projectDocsPath = Path.Combine(_ProjectsRootFolder, project, "Docs");
            if (!Directory.Exists(projectDocsPath))
            {
                _ErrorMessage = string.Concat("Unable to find Document Analyzer Documents for Project: ", project);

                return null;
            }

            List<string> docNames = new List<string>();
            string docName = string.Empty;

            string[] dirs = Directory.GetDirectories(projectDocsPath);
            foreach (string dir in dirs)
            {
                if (dir.IndexOf('~') == -1) // ~ are Deleted dirs
                {

                    docName = Files.GetLastFolder(dir);
                    docNames.Add(docName);
                }
            }

            return docNames.ToArray();
        }

        public DataTable GetDocuments(string project)
        {
            _ErrorMessage = string.Empty;

            DataTable dt = CreateDocsTable();

            string projectDocsPath = Path.Combine(_ProjectsRootFolder, project, "Docs");
            if (!Directory.Exists(projectDocsPath))
            {
                _ErrorMessage = string.Concat("Unable to find Document Analyzer Documents for Project: ", project);

                return null;
            }


            string parsedSecPath = string.Empty;
            string parsedDeepPath = string.Empty;

            string docName = string.Empty;
            string AR = "No";
            string DAR = "No";

            string[] parsedSec; // Parsed Segments files
            string[] parsedSent; // Parsed Sentences files


            string[] dirs = Directory.GetDirectories(projectDocsPath);
            foreach (string dir in dirs)
            {
                if (dir.IndexOf('~') == -1) // ~ are Deleted dirs
                {
                    parsedSecPath = Path.Combine(dir, "Current", "ParseSec");
                    if (Directory.Exists(parsedSecPath))
                    {
                        parsedSec = Directory.GetFiles(parsedSecPath, "*.rtf", SearchOption.TopDirectoryOnly);
                        if (parsedSec.Length > 0)
                        {
                            AR = "Yes";
                            parsedDeepPath = Path.Combine(dir, "Current", "Deep Analytics", "Current", "Parse Sentences");
                            if (Directory.Exists(parsedDeepPath))
                            {
                                parsedSent = Directory.GetFiles(parsedDeepPath, "*.rtf", SearchOption.TopDirectoryOnly);
                                if (parsedSent.Length > 0)
                                {
                                    DAR = "Yes";
                                }
                                else
                                {
                                    DAR = "No";
                                }
                            }
                            else
                            {
                                DAR = "No";
                            }
                        }
                        else
                        {
                            AR = "No";
                            DAR = "No";
                        }

                    }
                    else
                    {
                        AR = "No";
                        DAR = "No";
                    }

                    docName = Files.GetLastFolder(dir);

                    DataRow row = dt.NewRow();
                    row[ProjectDocsFields.Name] = docName;
                    row[ProjectDocsFields.AnalysisResults] = AR;
                    row[ProjectDocsFields.DeepAnalysisResults] = DAR;

                    dt.Rows.Add(row);

                }
            }

            return dt;

        }

        private DataTable CreateDocsTable()
        {
            DataTable dt = new DataTable(ProjectDocsFields.TableName);

            dt.Columns.Add(ProjectDocsFields.Name, typeof(string));
            dt.Columns.Add(ProjectDocsFields.AnalysisResults, typeof(string));
            dt.Columns.Add(ProjectDocsFields.DeepAnalysisResults, typeof(string));
       
            return dt;
        }

    }
}
