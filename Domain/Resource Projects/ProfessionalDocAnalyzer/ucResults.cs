using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

using Atebion.Common;

namespace ProfessionalDocAnalyzer
{
    public partial class ucResults : UserControl
    {
        public ucResults()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        // Declare delegate for when a project has been selected
        public delegate void ProcessHandler();

        [Category("Action")]
        [Description("Fires when a Project is selected")]
        public event ProcessHandler ProjectSelected;

        [Category("Action")]
        [Description("Fires when a Project is unselected")]
        public event ProcessHandler ProjectUnselected;

        [Category("Action")]
        [Description("Fires when an Analysis is selected")]
        public event ProcessHandler AnalysisSelected;

        [Category("Action")]
        [Description("Fires when an Analysis is unselected")]
        public event ProcessHandler AnalysisUnselected;

        [Category("Action")]
        [Description("Fires when the Deep Analysis is clicked")]
        public event ProcessHandler RunDeepAnalysisResults;


        private ResultsComponents _CurrentMode = new ResultsComponents();
        private enum ResultsComponents
        {
            Project = 1,
            Analysis = 2,
            Documents = 3,
            Document = 4,
            AnalysisResults = 60,
            AnalysisResults_KWSeg = 5,
            AnalysisResults_DicDoc = 6,
            AnalysisResults_ConceptsDoc = 7,
            AnalysisResults_CompareDicDocs = 8,
            AnalysisResults_CompareConceptsDocs = 9,
            AnalysisResults_ReadabilityDoc = 10,
            AnalysisResults_DiffSxS = 11,
            DeepAnalysisResults_KW = 12,
            Report = 70,
            Report_KWSeg_Excel = 20,
            Report_KWSeg_Word = 21,
            Report_KWSeg_HTML = 22,
            Report_DicDoc_Excel = 24,
            Report_ConceptsDoc_Excel = 26,
            Report_CompareDicDocs_Excel = 28,
            Report_CompareConceptsDocs_Excel = 30,
            Report_ReadabilityDoc_HTML = 32,
            Report_DiffSxS_HTML = 34,
            Report_DeepAnalysisResults_KW_Excel = 36,
            Report_DeepAnalysisResults_KW_Word = 37,
            Report_RAM_Excel = 39,



            Other = 50
        }

        private string _CurrentPath = string.Empty;

        public bool Delete(bool isProject)
        {
            string subject = string.Empty;
            if (isProject)
                subject = "Project";
            else
                subject = "Analysis";

            string msg = string.Empty;

            msg = string.Concat("Are you sure you want to Delete the selected ", subject, "?");

            if (MessageBox.Show(msg, string.Concat("Confirm the Removal of the Selected ", subject), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return false;

            if (!Directory.Exists(_CurrentPath))
            {
                msg = string.Concat("Unable to find the ", subject, " data folder.");
                MessageBox.Show(msg, string.Concat("Unable to Remove ", subject), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            string obsoletePath = Directories.SetFolder2Obsolete(_CurrentPath);
            if (obsoletePath == string.Empty)
            {
                MessageBox.Show(Directories.ErrorMessage, string.Concat("Unable to Remove ", subject), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            LoadProjectTree();

            msg = string.Concat("The selected ", subject, " folder was archived as ", obsoletePath);
            MessageBox.Show(msg, string.Concat(subject, " Folder was Removed"), MessageBoxButtons.OK, MessageBoxIcon.Information);

            return true;
        }

        public bool LoadData()
        {
            DoubleBuffered = true;

            HideAllPanels();

            ucResults_Home1.Dock = DockStyle.Fill;
            ucResults_Home1.Visible = true;

            _CurrentMode = ResultsComponents.Other;

            LoadProjectTree();

            return true;
        }

        public bool LoadProjectTree()
        {
            // Analysis vars
            string analysisName = string.Empty;
            //  string previousAnalysisName = "~~~~~";
            string analysisUID = string.Empty;
            int analysisLevel = 0;
            string analysisKey = string.Empty;
            string analysisFolder = string.Empty;
            string[] analyses;

            // Project vars        
            string projName = string.Empty;
            string projKey = string.Empty;
            string previousProjName = "~~~~~";
            string projUID = string.Empty;
            int projLevel = -1;

            // Documents var
            string[] docs;
            string docName = string.Empty;
            int i;
            string docKey = string.Empty;
            string documentsPath = string.Empty;
            string parseSecFolder = string.Empty;
            string[] parseSegFiles;
            string parseSegFile = string.Empty;
            string analysisResultKey = string.Empty;
            int docLevel = 2;

            // Analysis Docs vars
            string analysisDocsName = string.Empty;
            string analysisDocsFolder = string.Empty;

            // Compare Documents
            string compareReportsFolder = string.Empty;
            bool compareDocsRptFound = false;
            

            // Deep Analysis 
            string deepAnalysisFolder = string.Empty;
            string deepAnalysisParseSentencesFolder = string.Empty;
            string deepAnalysisExportFolder = string.Empty;

            // Reports
            string[] reports;
            string reportName = string.Empty;
            string docReportsFolder = string.Empty;
            string reportExt = string.Empty;
            string reportKey = string.Empty;
            int reportCounter = 0;

            int fileTypeIconNo = 7;



            tvProjects.Nodes.Clear();

            

            string projectsFolder = AppFolders.Project;

            if (projectsFolder.Length == 0)
            {
                string msg = string.Concat("Unable to find the Projects folder: ", projectsFolder);
                MessageBox.Show(msg, "Projects Folder Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return false;
            }

            string[] projFolders = Directory.GetDirectories(projectsFolder);

            if (projFolders.Length == 0)
            {
                string msg = string.Concat("You have no active or open projects ");
                MessageBox.Show(msg, "No Projects Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            

            foreach (string project in projFolders)
            {
                projLevel++;

                // Add I Project
                projName = Directories.GetLastFolder(project);
                if (projName.IndexOf('~') == -1)
                {
                    projKey = string.Concat("proj_", projLevel.ToString(), "|", "path_", project);
                    tvProjects.Nodes.Add(projKey, projName, Convert.ToInt32(ResultsComponents.Project), Convert.ToInt32(ResultsComponents.Project));

                    previousProjName = projName;
                    docKey = string.Concat("documents", "|", "proj_", projLevel.ToString());

                    // Add Documents folder
                    tvProjects.Nodes[projLevel].Nodes.Add(docKey, "Documents", Convert.ToInt32(ResultsComponents.Document), Convert.ToInt32(ResultsComponents.Document));

                    // Add Documents under a Project
                    documentsPath = Path.Combine(project, "Docs");
                    if (Directory.Exists(documentsPath)) // Added 08.26.2020
                    {
                        docs = Directory.GetDirectories(documentsPath);
                        i = 0;
                        docLevel = 1;
                        foreach (string doc in docs)
                        {
                            if (doc.IndexOf('~') == -1)
                            {
                                docKey = string.Concat("doc_", i.ToString(), "|", "proj_", projLevel.ToString(), "|", "path_", doc);
                                docName = Directories.GetLastFolder(doc);
                                // Add Documents
                                tvProjects.Nodes[projLevel].Nodes[0].Nodes.Add(docKey, docName, Convert.ToInt32(ResultsComponents.Documents), Convert.ToInt32(ResultsComponents.Documents));

                                // Add Analysis Results
                                parseSecFolder = Path.Combine(documentsPath, docName, "Current", "ParseSec");
                                if (Directory.Exists(parseSecFolder))
                                {
                                    parseSegFiles = Directory.GetFiles(parseSecFolder, "*.rtf");
                                    if (parseSegFiles.Length > 0)
                                    {
                                        analysisResultKey = string.Concat("AnalysisResults_", i.ToString(), "|", "doc_", i.ToString(), "|", "proj_", projLevel.ToString(), "|", "path_", parseSecFolder);
                                        tvProjects.Nodes[projLevel].Nodes[0].Nodes[i].Nodes.Add(analysisResultKey, "Analysis Results", 8, 8);
                                    }
                                }

                                // Add Default Export Reports
                                reportCounter = 0;
                                docReportsFolder = Path.Combine(documentsPath, docName, "Current", "ParseSec", "Export");
                                if (Directory.Exists(docReportsFolder))
                                {
                                    reports = Directory.GetFiles(docReportsFolder);
                                    foreach (string rpt in reports)
                                    {
                                        reportName = Files.GetFileName(rpt, out reportExt);
                                        if (reportName.IndexOf('~') == -1)
                                        {
                                            // reportName = string.Concat(reportName, ".", reportExt);
                                            reportKey = string.Concat("rpt_", reportCounter.ToString(), "|", "doc_", i.ToString(), "|", "proj_", projLevel.ToString(), "|", "pathFile_", rpt);
                                            fileTypeIconNo = GetFileTypeIconNo(reportExt);

                                            tvProjects.Nodes[projLevel].Nodes[0].Nodes[i].Nodes.Add(reportKey, reportName, fileTypeIconNo, fileTypeIconNo);

                                            reportCounter++;
                                        }
                                    }

                                }

                                // Deep Analysis
                                deepAnalysisFolder = Path.Combine(documentsPath, docName, "Current", "Deep Analytics", "Current");
                                if (Directory.Exists(deepAnalysisFolder))
                                {
                                    deepAnalysisParseSentencesFolder = Path.Combine(deepAnalysisFolder, "Parse Sentences");
                                    string[] files = Directory.GetFiles(deepAnalysisParseSentencesFolder, "*.rtf");
                                    if (files.Length > 0)
                                    {
                                        analysisResultKey = string.Concat("DeepAnalysisResults_", i.ToString(), "|", "doc_", i.ToString(), "|", "proj_", projLevel.ToString(), "|", "path_", deepAnalysisFolder);
                                        tvProjects.Nodes[projLevel].Nodes[0].Nodes[i].Nodes.Add(analysisResultKey, "Deep Analysis Results", 8, 8);

                                        deepAnalysisExportFolder = Path.Combine(deepAnalysisFolder, "Export");
                                        if (Directory.Exists(deepAnalysisExportFolder))
                                        {
                                            int x = 0;
                                            reports = Directory.GetFiles(deepAnalysisExportFolder);
                                            foreach (string rpt in reports)
                                            {
                                                reportName = Files.GetFileName(rpt, out reportExt);
                                                if (reportName.IndexOf('~') == -1)
                                                {
                                                    reportKey = string.Concat("rpt_", x.ToString(), "|", "doc_", i.ToString(), "|", "proj_", projLevel.ToString(), "|", "pathFile_", rpt);
                                                    tvProjects.Nodes[projLevel].Nodes[0].Nodes[i].Nodes.Add(reportKey, reportName, fileTypeIconNo, fileTypeIconNo);

                                                    x++;
                                                }
                                            }

                                        }

                                    }

                                }

                                    i++;

                                //}   
                            }
                        }
                    }

                    // Add Analysis under a Project
                    analysisFolder = Path.Combine(project, "Analysis");
                    if (Directory.Exists(analysisFolder))
                    {
                        docLevel = 2;
                        // Replaced with getAnalysisSortedFolders() --  analyses = Directory.GetDirectories(analysisFolder);
                        //   analyses = getSortedFolders(analysisFolder); // sort the folders''
                        // GenericDataManger gDataMgr = new GenericDataManger();
                        analyses = Directories.GetSortedFolders(analysisFolder); // sort the folders
                        string selectedAnalysisPath = string.Empty;
                        analysisLevel = 0;
                        foreach (string analysis in analyses)
                        {
                            if (analysis.IndexOf('~') == -1)
                            {

                                analysisName = Directories.GetLastFolder(analysis);

                                selectedAnalysisPath = Path.Combine(analysisFolder, analysis);

                                // Add Analysis
                                analysisLevel++;
                                analysisKey = string.Concat("analysis_" + analysisLevel.ToString(), "|", "proj_", projLevel.ToString(), "|", "path_", selectedAnalysisPath);
                                tvProjects.Nodes[projLevel].Nodes.Add(analysisKey, analysisName, Convert.ToInt32(ResultsComponents.Analysis), Convert.ToInt32(ResultsComponents.Analysis));

                                // Check for Compare Documents Report
                                compareDocsRptFound = false;
                                compareReportsFolder = Path.Combine(project, "Analysis", analysisName, "CompareReports");
                                if (Directory.Exists(compareReportsFolder))
                                {
                                    PopulateTree_CompareDocuments(project, projName, analysisName, analysisFolder, projLevel, analysisLevel);

                                } // End -- // Compare Documents Report
                                else // Find Documents
                                {
                                    // Add Documents under a Project
                                    i = 0;
                                    analysisDocsFolder = Path.Combine(analysisFolder, "Docs");
                                    if (Directory.Exists(analysisDocsFolder))
                                    {
                                        docs = Directory.GetDirectories(analysisDocsFolder);
                                        foreach (string doc in docs)
                                        {
                                            docKey = string.Concat("doc_", i.ToString(), "|", "analysis_" + analysisLevel.ToString(), "|", "proj_", projLevel.ToString(), "|", "path_", doc);
                                            docName = Directories.GetLastFolder(doc);

                                            // Add Documents
                                            tvProjects.Nodes[projLevel].Nodes[analysisLevel].Nodes.Add(docKey, docName, Convert.ToInt32(ResultsComponents.Documents), Convert.ToInt32(ResultsComponents.Documents));

                                            // Add Reports for doc
                                            reportCounter = 0;
                                            docReportsFolder = Path.Combine(analysisDocsFolder, docName, "Report");
                                            if (Directory.Exists(docReportsFolder))
                                            {
                                                reports = Directory.GetFiles(docReportsFolder);
                                                foreach (string rpt in reports)
                                                {
                                                    reportName = Files.GetFileName(rpt, out reportExt);
                                                    reportName = string.Concat(reportName, ".", reportExt);
                                                    reportKey = string.Concat("rpt_", reportCounter.ToString(), "|", "doc_", i.ToString(), "|", "analysis_" + analysisLevel.ToString(), "|", "proj_", projLevel.ToString(), "|", "pathFile_", rpt);
                                                    fileTypeIconNo = GetFileTypeIconNo(reportExt);

                                                    tvProjects.Nodes[projLevel].Nodes[analysisLevel].Nodes[docLevel].Nodes.Add(reportKey, reportName, fileTypeIconNo, fileTypeIconNo);

                                                    reportCounter++;
                                                }

                                            }

                                            i++;
                                        }
                                    }
                                }

                            }
                        }
                    }

                }

            }

         //   tvProjects.ExpandAll();
            TreeExpand2Level(1);

            tvProjects.Visible = true;
            tvProjects.Dock = DockStyle.Fill;

            return true;

        }

        // Moved to Atebion.Common.Dirctories
        //private string[] getSortedFolders(string dirPath)
        //{
        //    List<string> data = new List<string>();

        //    string[] strDir = Directory.GetDirectories(dirPath);

        //     NumericComparer nc = new NumericComparer();
        //     Array.Sort(strDir, nc);
        //     foreach (var item in strDir)
        //    {
        //        data.Add(Path.GetFileName(item));
        //    }

        //     return data.ToArray();
        //}

        private void TreeExpand2Level(int level)
        {
            int count = tvProjects.Nodes.Count;
            for (int i = 0; i < count; i++)
            {
                if (tvProjects.Nodes[i].Level <= level)
                {
                    tvProjects.Nodes[i].Expand();

                }

            }
            
        }

        private bool Check4RAM(string AnalysisNameFolder)
        {
            string RAMInforFile = "RAMInfor.txt";

            string docsFolder = Path.Combine(AnalysisNameFolder, "Docs");
            if (Directory.Exists(docsFolder))
            {
                string[] docs = Directory.GetDirectories(docsFolder);

                if (docs.Length == 0 || docs.Length > 1)
                    return false;

                string docFolder = docs[0];
                string docInforFolder = Path.Combine(docFolder, "Infor");
                if (!Directory.Exists(docInforFolder))
                    return false;

                string docInforPathFile = Path.Combine(docInforFolder, RAMInforFile);
                if (File.Exists(docInforPathFile))
                    return true;

            }

            return false;
            
        }

        private void PopulateTree_CompareDocuments(string project, string projName, string analysisName, string analysisFolder, int projLevel, int analysisLevel)
        {
            // Documents var
            string[] docs;
            string docName = string.Empty;
            int i = 0;
            string docKey = string.Empty;
            string documentsPath = string.Empty;
            string parseSecFolder = string.Empty;
            string[] parseSegFiles;
            string parseSegFile = string.Empty;
            string analysisResultKey = string.Empty;
            int docLevel = 0;

            string inforFolder = string.Empty;
            

            // Analysis Docs vars
            string analysisDocsName = string.Empty;
            string analysisDocsFolder = string.Empty;

            // Compare Documents
            string compareReportsFolder = string.Empty;
            bool compareDocsRptFound = false;

            // Reports
            string[] reports;
            string reportName = string.Empty;
            string docReportsFolder = string.Empty;
            string reportExt = string.Empty;
            string reportKey = string.Empty;
 //           int reportCounter = 0;

            int fileTypeIconNo = 7;

            // Check for Compare Documents Report
            compareDocsRptFound = false;
            compareReportsFolder = Path.Combine(project, "Analysis", analysisName, "CompareReports");
            if (Directory.Exists(compareReportsFolder))
            {
                reports = Directory.GetFiles(compareReportsFolder);
                if (reports.Length > 0)
                    compareDocsRptFound = true;
            }

            // Diff
            bool diffModsFolderFound = false;
            string modsFolder = Path.Combine(project, "Analysis", analysisName, "Mods");
            if (Directory.Exists(modsFolder))
            {
                diffModsFolderFound = true;
            }


  
            docLevel = 0;
            parseSecFolder = Path.Combine(project, "Analysis", analysisName);

            bool RAMRptExists = Check4RAM(parseSecFolder);
            //if (RAMRptExists) // Testing
            //{
            //     System.Media.SystemSounds.Beep.Play();
            //}

            if (Directory.Exists(parseSecFolder))
            {
                // Check for Dictionary Compare
                parseSegFile = Path.Combine(parseSecFolder, "DocsDicSumAnalysis.xml");
                analysisResultKey = string.Concat("AnalysisResults_0", "|", "doc_", docLevel.ToString(), "|", "analysis_", analysisLevel, "|", "proj_", projLevel.ToString(), "|", "path_", parseSecFolder);

                if (File.Exists(parseSegFile))
                {
                    analysisResultKey = string.Concat(analysisResultKey, "|", "compareAnalysis_DocsDic");
                    tvProjects.Nodes[projLevel].Nodes[analysisLevel].Nodes.Add(analysisResultKey, "Analysis Results", 8, 8);
                    docLevel++;
                }
                else
                {
                    // Check for Dictionary Compare
                    parseSegFile = Path.Combine(parseSecFolder, "DocsConceptsSumAnalysis.xml");
                    analysisResultKey = string.Concat(analysisResultKey, "|", "compareAnalysis_DocsConcepts");

                    if (File.Exists(parseSegFile))
                    {

                        tvProjects.Nodes[projLevel].Nodes[analysisLevel].Nodes.Add(analysisResultKey, "Analysis Results", 8, 8);
                        docLevel++;
                    }
                }
            }

            int reportCounter = 0;
                
        // Compare Documents Reports found
        if (compareDocsRptFound)
        {
            
            reports = Directory.GetFiles(compareReportsFolder); // Hack to do it again b/c VS give compile error
            foreach (string rpt in reports)
            {
                //reportName = Files.GetFileNameWOExt(rpt);
                reportName = Files.GetFileName(rpt, out reportExt);
                // reportName = string.Concat(reportName, ".", reportExt);
                reportKey = string.Concat("rpt_", reportCounter.ToString(), "|", "pathFile_", rpt, "|", analysisResultKey);
                fileTypeIconNo = GetFileTypeIconNo(reportExt);

                analysisDocsFolder = Path.Combine(analysisFolder, "Docs");
                if (Directory.Exists(analysisDocsFolder))
                {
                    docs = Directory.GetDirectories(analysisDocsFolder);
                    for (int y = 0; y < docs.Count(); y++)
                    {
                        if (y == 0)
                        {
                            docName = docs[y];
                        }
                        else if (y + 1 == docs.Count())
                        {
                            docName = string.Concat(docName, " & ", docs[y]);
                        }
                        else
                        {
                            docName = string.Concat(docName, ", ", docs[y]);
                        }
                    }

                    reportName = string.Concat(reportName, "  Docs: ", docName);
                }

                // Add Compare Documents Report
                tvProjects.Nodes[projLevel].Nodes[analysisLevel].Nodes.Add(reportKey, reportName, fileTypeIconNo, fileTypeIconNo);
                reportCounter++;
            }
        }

      //  int reportCounterX = 0;
        analysisDocsFolder = Path.Combine(project, "Analysis", analysisName, "Docs");
        if (Directory.Exists(analysisDocsFolder))
        {
            if (diffModsFolderFound)
            {
                string diffPath = Path.Combine(project, "Analysis", analysisName);
               // docKey = string.Concat("AnalysisResults_", docLevel.ToString(), "|", "path_", diffPath, "|", analysisResultKey);
                docKey = string.Concat("AnalysisResults_", i, "|", "path_", diffPath);
              //  tvProjects.Nodes[docLevel].Nodes[analysisLevel].Nodes.Add(docKey, "Analysis Results", 8, 8);
                tvProjects.Nodes[projLevel].Nodes[analysisLevel].Nodes.Add(docKey, "Analysis Results", 8, 8);
                i++;

                string diffHTML_Rpt = Path.Combine(diffPath, "DiffReport.html");
                if (File.Exists(diffHTML_Rpt))
                {
                    reportKey = string.Concat("rpt_0", "|", "pathFile_", diffHTML_Rpt, "|", docKey);
                    fileTypeIconNo = GetFileTypeIconNo(".html");
                    reportName = "Diff SxS Report";
                    tvProjects.Nodes[projLevel].Nodes[analysisLevel].Nodes.Add(reportKey, reportName, fileTypeIconNo, fileTypeIconNo);
                }
            }

            string acroseekerResultsFolder = string.Empty;
            string acronymsFile = "Acronyms.docx";
            string acronymsPathFile = string.Empty;

            docs = Directory.GetDirectories(analysisDocsFolder);
            foreach (string doc in docs)
            {
                docKey = string.Concat("doc_", docLevel.ToString(), "|", "path_", doc, "|", analysisResultKey);
                docName = Directories.GetLastFolder(doc);

                // Document
                tvProjects.Nodes[projLevel].Nodes[analysisLevel].Nodes.Add(docKey, docName, Convert.ToInt32(ResultsComponents.Documents), Convert.ToInt32(ResultsComponents.Documents));
               


                parseSecFolder = Path.Combine(analysisDocsFolder, docName, "ParseSeg");

                if (Directory.Exists(parseSecFolder))
                {
                    parseSegFiles = Directory.GetFiles(parseSecFolder, "*.rtf");

                    if (parseSegFiles.Length > 0)
                    {
                        analysisResultKey = string.Concat("AnalysisResults_0", "|", "path_", parseSecFolder, "|", docKey);
                        //tvProjects.Nodes[projLevel].Nodes[analysisLevel].Nodes[reportCounter].Nodes.Add(analysisResultKey, "Analysis Results", 8, 8);
                        tvProjects.Nodes[projLevel].Nodes[analysisLevel].Nodes[docKey].Nodes.Add(analysisResultKey, "Analysis Results", 8, 8);

                    }
                }

                acroseekerResultsFolder = Path.Combine(analysisDocsFolder, docName, "Results");
                if (Directory.Exists(acroseekerResultsFolder))
                {
                    acronymsPathFile = Path.Combine(acroseekerResultsFolder, acronymsFile);
                    if (File.Exists(acronymsPathFile))
                    {
                        analysisResultKey = string.Concat("AnalysisResults_0", "|", "path_", acroseekerResultsFolder, "|", docKey);
                        tvProjects.Nodes[projLevel].Nodes[analysisLevel].Nodes[docKey].Nodes.Add(analysisResultKey, "Analysis Results", 8, 8);
                    }
                }
            


                //if (RAMRptExists) // ???
                //{
                // Reports
                    docReportsFolder = Path.Combine(doc, "Reports");
                    if (Directory.Exists(docReportsFolder))
                    {
                        reports = Directory.GetFiles(docReportsFolder);
                        if (reports.Length > 0)
                        {

                            foreach (string rpt in reports)
                            {
                                reportName = Files.GetFileName(rpt, out reportExt);
                                reportKey = string.Concat("rpt_", reportCounter.ToString(), "|", "pathFile_", rpt, "|", docKey);
                                fileTypeIconNo = GetFileTypeIconNo(reportExt);

                                tvProjects.Nodes[projLevel].Nodes[analysisLevel].Nodes[docLevel].Nodes.Add(reportKey, reportName, fileTypeIconNo, fileTypeIconNo);

                                reportCounter++;
                            }

                        }

                        
                        docReportsFolder = Path.Combine(parseSecFolder, "Export");
                        if (Directory.Exists(docReportsFolder))
                        {
                            reports = Directory.GetFiles(docReportsFolder);
                            if (reports.Length > 0)
                            {

                                foreach (string rpt in reports)
                                {
                                    reportName = Files.GetFileName(rpt, out reportExt);
                                    reportKey = string.Concat("rpt_", reportCounter.ToString(), "|", "pathFile_", rpt, "|", docKey);
                                    fileTypeIconNo = GetFileTypeIconNo(reportExt);

                                    tvProjects.Nodes[projLevel].Nodes[analysisLevel].Nodes[docLevel].Nodes.Add(reportKey, reportName, fileTypeIconNo, fileTypeIconNo);
                                    reportCounter++;
                                }

                            }

                        }

                    //}

                        //reportCounter++;

                }
                else
                {

                    parseSecFolder = Path.Combine(analysisDocsFolder, docName, "ParseSeg");

                    if (Directory.Exists(parseSecFolder))
                    {
                        parseSegFiles = Directory.GetFiles(parseSecFolder, "*.rtf");

                        if (parseSegFiles.Length > 0)
                        {
                            analysisResultKey = string.Concat("AnalysisResults_0", "|", "path_", parseSecFolder, "|", docKey);
                            tvProjects.Nodes[projLevel].Nodes[analysisLevel].Nodes[docLevel].Nodes.Add(analysisResultKey, "Analysis Results", 8, 8);

                            docLevel++;

                        }

                        int reportCounterY = 0;
                        docReportsFolder = Path.Combine(parseSecFolder, "Export");
                        if (Directory.Exists(docReportsFolder))
                        {
                            reports = Directory.GetFiles(docReportsFolder);
                            if (reports.Length > 0)
                            {

                                foreach (string rpt in reports)
                                {
                                    reportName = Files.GetFileName(rpt, out reportExt);
                                    reportKey = string.Concat("rpt_", reportCounterY.ToString(), "|", "pathFile_", rpt, "|", docKey);
                                    fileTypeIconNo = GetFileTypeIconNo(reportExt);

                                    tvProjects.Nodes[projLevel].Nodes[analysisLevel].Nodes[docLevel].Nodes.Add(reportKey, reportName, fileTypeIconNo, fileTypeIconNo);
                                    reportCounterY++;
                                }

                            }

                        }


                        

                    }
                }


            }

        }

           // } // End -- // Compare Documents Report

        }

        private int GetFileTypeIconNo(string ext)
        {
            switch (ext.ToUpper())
            {
                case ".XLSX":
                    return 5;
                case ".DOCX":
                    return 6;
                case ".HTML":
                    return 9;
            }

            return 7;
        }

        private void HideNav()
        {
            tvProjects.Visible = false;
            tvProjects.Dock = DockStyle.None;
        }

        private void HideAllPanels()
        {
            ucResults_ExcelPreview1.Visible = false;
            ucAnalysisResults1.Visible = false;
            ucResultsDic1.Visible = false;
            ucResults_WordPreview1.Visible = false;
            ucResults_TxtDocView1.Visible = false;
            ucResults_Home1.Visible = false;
            ucResultsMultiConcepts1.Visible = false;
            ucResultsMultiDic1.Visible = false;
            ucResultsConcepts1.Visible = false;
            ucQCAnalysisResults1.Visible = false;
            ucAcroSeekerResults1.Visible = false;
            ucDiffSxS1.Visible = false;
            ucResults_HTMLPreview1.Visible = false;
            ucResults_RTF_Preview1.Visible = false;
            ucDeepAnalyticsResults1.Visible = false;

        }

        private bool isAnalysisResults_DefaultParseSeg(string path)
        {
            if (path.Trim().Length == 0)
                return false;

            string parentPath = Directory.GetParent(path).FullName;
            string parentFolderName = new DirectoryInfo(parentPath).Name;
            if (parentFolderName == "Current")
            {
                parentPath = Directory.GetParent(parentPath).FullName;
                string currentDoc = new DirectoryInfo(parentPath).Name;

                parentPath = Directory.GetParent(parentPath).FullName; //Docs folder
                parentPath = Directory.GetParent(parentPath).FullName; // Project Name folder
                string projectName = new DirectoryInfo(parentPath).Name;

                // Set Folders
                AppFolders.ProjectName = projectName;
                AppFolders.AnalysisName = string.Empty;
                AppFolders.DocName = currentDoc;

                // Get Parse Type
                string pathFileParseType = string.Empty;
                AnalysisResultsType.Selection selection = AnalysisResultsType.Selection.Logic_Segments;
                string informationPath = Path.Combine(Directory.GetParent(path).FullName, "Information");
                if (Directory.Exists(informationPath))
                {
                    pathFileParseType = Path.Combine(informationPath, "ParseType.par");
                    if (File.Exists(pathFileParseType)) // Added if statement 02.25.2020
                    {
                        string parseTypeText = File.ReadAllText(pathFileParseType);
                        if (parseTypeText.IndexOf("Legal") != -1)
                        {
                            selection = AnalysisResultsType.Selection.Logic_Segments;
                        }
                        else
                        {
                            selection = AnalysisResultsType.Selection.Paragraph_Segments;
                        }
                    }
                    else
                    {
                        selection = AnalysisResultsType.Selection.Logic_Segments; // Default
                    }
                }

                // Set Search Type
                AnalysisResultsType.SearchType searchType = AnalysisResultsType.SearchType.Keywords;

                // Search Path
                string searchIndexPath = string.Empty;
                if (AppFolders.UseHootSearchEngine)
                {
                    searchIndexPath = AppFolders.DocParsedSecIndex;
                }
                else
                {
                    searchIndexPath = AppFolders.DocParsedSecIndex2;
                }

                ucAnalysisResults1.LoadData(selection, searchType, AppFolders.DocParsedSec, AppFolders.DocParsedSecXML, AppFolders.DocParsedSecExport, AppFolders.DocParsedSecNotes, searchIndexPath, AppFolders.DocParsedSecKeywords, false);
                ucAnalysisResults1.Dock = DockStyle.Fill;
                ucAnalysisResults1.Visible = true;
                ucAnalysisResults1.Adjust4QualityandNotes();


                return true;
            }
            else
            {
                string parseSegFolder = Path.Combine(parentPath, "ParseSeg");
                if (!Directory.Exists(parseSegFolder))
                    return false;


                string parseSegXMLFolder = Path.Combine(parseSegFolder, "XML");
                if (!Directory.Exists(parseSegXMLFolder))
                    return false;

                string parseResultsXMLFile = Path.Combine(parseSegXMLFolder, "ParseResults.xml");
                if (!File.Exists(parseResultsXMLFile))
                    return false;

                string parseSegExportFolder = Path.Combine(parseSegFolder, "Export");
                if (!Directory.Exists(parseSegXMLFolder))
                    return false;

                string parseSegNotesFolder = Path.Combine(parseSegFolder, "Notes");
                if (!Directory.Exists(parseSegNotesFolder))
                    return false;

                AnalysisResultsType.SearchType searchType = AnalysisResultsType.SearchType.Keywords;
                string keywordsFolder = Path.Combine(parentPath, "Keywords");
                if (Directory.Exists(keywordsFolder))
                {
                    string keywordsFile = Path.Combine(keywordsFolder, "Keywords.txt");
                    if (!File.Exists(keywordsFile))
                    {
                        searchType = AnalysisResultsType.SearchType.None;
                    }
                }
                else // ToDo add Others
                {
                    searchType = AnalysisResultsType.SearchType.None;
                }

                string searchIndexPath = Path.Combine(parseSegFolder, "Index2");
                if (!Directory.Exists(searchIndexPath))
                {
                    searchIndexPath = Path.Combine(parseSegFolder, "Index");
                    if (!Directory.Exists(searchIndexPath))
                    {
                        searchIndexPath = string.Empty;
                    }
                }

                AnalysisResultsType.Selection selection = AnalysisResultsType.Selection.Logic_Segments; // ToDo add flag file


                ucAnalysisResults1.LoadData(selection, searchType, parseSegFolder, parseSegXMLFolder, parseSegExportFolder, parseSegNotesFolder, searchIndexPath, keywordsFolder, true);
                ucAnalysisResults1.Dock = DockStyle.Fill;
                ucAnalysisResults1.Visible = true;
                ucAnalysisResults1.Adjust4QualityandNotes();


                return true;

            }

            return false;
        }

        private bool isAnalysisResults_QCReadability(string path)
        {
            if (path.Trim().Length == 0)
                return false;

            string parentPath = Directory.GetParent(path).FullName;
            string docName = new DirectoryInfo(parentPath).Name;
            string docXMLPath = Path.Combine(path, "XML");
            string document = string.Empty;
            if (Directory.Exists(docXMLPath))
            {
                string pathFileXML = Path.Combine(docXMLPath, "QCParseResults.xml");
                if (!File.Exists(pathFileXML))
                {
                    return false;
                }

                document = GetCurrentDocument(parentPath);

                ucQCAnalysisResults1.LoadData(path, docXMLPath, document);

                ucQCAnalysisResults1.Visible = true;
                ucQCAnalysisResults1.Dock = DockStyle.Fill;
                ucQCAnalysisResults1.AdjustColumns();

                return true;
            }

            return false;
        }

        private string GetCurrentDocument(string path)
        {
            if (path == string.Empty)
                return string.Empty;

            string txtFile = string.Empty;
            string otherTextFile = string.Empty;

            string[] files = Directory.GetFiles(path);

            if (files.Length == 0)
                return string.Empty;


            string ext = string.Empty;
            foreach (string file in files)
            {
                Files.GetFileName(file, out ext);
                if (ext.ToLower() == ".txt")
                {
                    txtFile = file;
                }
                else
                {
                    otherTextFile = file;
                }
            }

            if (otherTextFile != string.Empty)
            {
                return otherTextFile;
            }

            return txtFile;

        }

        private bool isAcroSeekerResults(string path)
        {
            if (path.Trim().Length == 0)
                return false;

            string pathFile = Path.Combine(path, "Acronyms.docx");
            if (File.Exists(pathFile))
            {
                string parentPath = Directory.GetParent(path).FullName;
                parentPath = Directory.GetParent(parentPath).FullName;
                parentPath = Directory.GetParent(parentPath).FullName;
                ucAcroSeekerResults1.LoadData(parentPath);
                ucAcroSeekerResults1.Visible = true;
                ucAcroSeekerResults1.Dock = DockStyle.Fill;

                return true;
            }

            return false;

        }

        private bool isDiffResults(string path)
        {
            if (path.Trim().Length == 0)
                return false;

            string modsFolder = Path.Combine(path, "Mods");
            if (Directory.Exists(modsFolder))
            {
                string notesFolder = Path.Combine(path, "Notes");
                string notesHTMLFolder = Path.Combine(notesFolder, "HTML");
                string modsPartFolder = Path.Combine(modsFolder, "Part");
                string modsWholeFolder = Path.Combine(modsFolder, "Whole");

                ucDiffSxS1.LoadData(path, notesFolder, notesHTMLFolder, modsPartFolder, modsFolder, modsWholeFolder);
                ucDiffSxS1.Visible = true;
                ucDiffSxS1.Dock = DockStyle.Fill;
                return true;
            }

            return false;
        }

        private bool isAnalysisResults_DicDoc(string path)
        {
            if (path.Trim().Length == 0)
                return false;

            string parentPath = Directory.GetParent(path).FullName;
            string docName = new DirectoryInfo(parentPath).Name;
            string docXMLPath = Path.Combine(parentPath, "XML");
            if (Directory.Exists(docXMLPath))
            {
                string pathFileXML = Path.Combine(docXMLPath, "DicAnalysis.xml");
                if (File.Exists(pathFileXML))
                {
                    //MessageBox.Show("i am here");
                    GenericDataManger gDataMgr = new GenericDataManger();
                    DataSet ds = gDataMgr.LoadDatasetFromXml(pathFileXML);
                    if (ds == null)
                        return false; // ToDo add error message

                    if (ds.Tables.Count == 0)
                        return false; // ToDo add error message
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = ds.Tables[0].Rows[0]; // get 1st row
                        string dictionaryName = row["DictionaryName"].ToString();

                        string docsPath = Directory.GetParent(parentPath).FullName;
                        string analysisPath = Directory.GetParent(docsPath).FullName;
                        string analysisName = new DirectoryInfo(analysisPath).Name;
                        string analysisFolder = Directory.GetParent(analysisPath).FullName;
                        string projectPath = Directory.GetParent(analysisFolder).FullName;
                        string projectName = new DirectoryInfo(projectPath).Name;

                        AppFolders.ProjectName = projectName;
                        AppFolders.AnalysisName = analysisName;

                        Atebion.ConceptAnalyzer.Analysis CAMgr = new Atebion.ConceptAnalyzer.Analysis(AppFolders.AnalysisPath, AppFolders.AppDataPath, AppFolders.ProjectName);

                        ucResultsDic1.LoadData(CAMgr, projectName, analysisName, docName, dictionaryName);
                        ucResultsDic1.Visible = true;
                        ucResultsDic1.Dock = DockStyle.Fill;
                        ucResultsDic1.adjColumns();
                    }
                    else
                    {
                        MessageBox.Show("No result found for the dictionary used");
                    }
                    return true;
                }


            }


            return false;
        }

        private bool isAnalysisResults_CompareDoc_Concepts(string path)
        {
            if (path.Trim().Length == 0)
                return false;

            if (!Directory.Exists(path))
                return false;

            string xmlPath = Path.Combine(path, "XML");

            if (!Directory.Exists(xmlPath))
                return false;

            string pathFileXML = Path.Combine(xmlPath, "ConceptResults.xml");
            if (!File.Exists(pathFileXML))
                return false;


            string parentPath = Directory.GetParent(path).FullName;
            string docName = new DirectoryInfo(parentPath).Name;
            parentPath = Directory.GetParent(parentPath).FullName;
            parentPath = Directory.GetParent(parentPath).FullName; // Analysis folder
            string analysisName = new DirectoryInfo(parentPath).Name; // Analysis Name
            parentPath = Directory.GetParent(parentPath).FullName; // Analysis folder
            parentPath = Directory.GetParent(parentPath).FullName; // Project folder
            string projectName = new DirectoryInfo(parentPath).Name; // Project Name

            AppFolders.ProjectName = projectName;
            AppFolders.AnalysisName = analysisName;

            Atebion.ConceptAnalyzer.Analysis CAMgr = new Atebion.ConceptAnalyzer.Analysis(AppFolders.AnalysisPath, AppFolders.AppDataPath, AppFolders.ProjectName);

            ucResultsConcepts1.LoadData(CAMgr, projectName, analysisName, docName);
            ucResultsConcepts1.Visible = true;
            ucResultsConcepts1.Dock = DockStyle.Fill;
            ucResultsConcepts1.adjColumns();

            return true;
        }

        private bool isAnalysisResults_CompareDocs_Concepts(string path)
        {
            if (path.Trim().Length == 0)
                return false;

            if (!Directory.Exists(path))
                return false;

            string pathFileXML = Path.Combine(path, "DocsConceptsSumAnalysis.xml");
            if (!File.Exists(pathFileXML))
                return false;

            string analysisName = new DirectoryInfo(path).Name;

            string parentPath = Directory.GetParent(path).FullName; // Analysis folder
            parentPath = Directory.GetParent(parentPath).FullName; // Project Name folder
            string projectName = new DirectoryInfo(parentPath).Name; // project Name

            AppFolders.ProjectName = projectName;
            AppFolders.AnalysisName = analysisName;

            Atebion.ConceptAnalyzer.Analysis CAMgr = new Atebion.ConceptAnalyzer.Analysis(AppFolders.AnalysisPath, AppFolders.AppDataPath, AppFolders.ProjectName);

            ucResultsMultiConcepts1.LoadData(CAMgr, projectName, analysisName);
            ucResultsMultiConcepts1.Visible = true;
            ucResultsMultiConcepts1.Dock = DockStyle.Fill;
            ucResultsMultiConcepts1.adjColumns();

            return true;
        }

        private bool isAnalysisResults_CompareDocs_Dictionary(string path)
        {
            if (path.Trim().Length == 0)
                return false;

            if (!Directory.Exists(path))
                return false;

            string pathFileXML = Path.Combine(path, "DocsDicSumAnalysis.xml");
            if (!File.Exists(pathFileXML))
                return false;

            // Get Dictionary Name
            GenericDataManger gDataMgr = new GenericDataManger();
            DataSet ds = gDataMgr.LoadDatasetFromXml(pathFileXML);
            if (ds == null)
                return false; // ToDo add error message

            if (ds.Tables.Count == 0)
                return false; // ToDo add error message

            DataRow row = ds.Tables[0].Rows[0]; // get 1st row
            string dictionaryName = row["DictionaryName"].ToString();
            // End - Get Dictionary Name


            string analysisName = new DirectoryInfo(path).Name;

            string parentPath = Directory.GetParent(path).FullName; // Analysis folder
            parentPath = Directory.GetParent(parentPath).FullName; // Project Name folder
            string projectName = new DirectoryInfo(parentPath).Name; // project Name

            AppFolders.ProjectName = projectName;
            AppFolders.AnalysisName = analysisName;

            Atebion.ConceptAnalyzer.Analysis CAMgr = new Atebion.ConceptAnalyzer.Analysis(AppFolders.AnalysisPath, AppFolders.AppDataPath, AppFolders.ProjectName);

            ucResultsMultiDic1.LoadData(CAMgr, projectName, analysisName, dictionaryName);
            ucResultsMultiDic1.Visible = true;
            ucResultsMultiDic1.Dock = DockStyle.Fill;
            ucResultsMultiDic1.adjColumns();

            return true;
        }

        private bool GetDeepAnalysisResults(string path)
        {
            if (path.Length == 0)
                return false;

            HideAllPanels();

            string prevPath = Directory.GetParent(path).FullName;
            prevPath = Directory.GetParent(prevPath).FullName;
            string docNamePath = Directory.GetParent(prevPath).FullName;
            string docName = Directories.GetLastFolder(docNamePath);            
            string prevPath2 = Directory.GetParent(docNamePath).FullName;
            prevPath2 = Directory.GetParent(prevPath2).FullName;

            string ProjectName = Directories.GetLastFolder(prevPath2);
            string ProjectPath = Directory.GetParent(prevPath2).FullName;
            

            AppFolders.ProjectName = ProjectName;
            AppFolders.DocName = docName;


            ucDeepAnalyticsResults1.LoadData(prevPath);
            ucDeepAnalyticsResults1.Visible = true;
            ucDeepAnalyticsResults1.Dock = DockStyle.Fill;
            ucDeepAnalyticsResults1.AdjustColumns();
            return true;

        }

        private bool GetAnalysisResults(string path)
        {
            if (path.Length > 0)
            {

                if (isAnalysisResults_DicDoc(path))
                {
                    return true;
                }
                else if (isAnalysisResults_CompareDoc_Concepts(path)) // concepts in a single document
                {
                    return true;
                }
                else if (isAnalysisResults_CompareDocs_Concepts(path)) // Multi-Doc compare concepts
                {
                    return true;
                }
                else if (isAnalysisResults_CompareDocs_Dictionary(path))
                {
                    return true;
                }
                else if (isAnalysisResults_QCReadability(path))
                {
                    return true;
                }
                else if (isAcroSeekerResults(path))
                {
                    return true;
                }
                else if (isDiffResults(path))
                {
                    return true;
                }
                // Check and Get Parse Seg. Analysis Results panel
                else if (isAnalysisResults_DefaultParseSeg(path))
                {
                    return true;
                }

            }

            return false;

        }

        private string GetAttributePath(string nodeKey)
        {
            string attributeValue = string.Empty;
            int x = -1;

            string[] keyAttributes = nodeKey.Split('|');
            if (keyAttributes.Length > 0)
            {
                foreach (string attribute in keyAttributes)
                {
                    x = attribute.IndexOf("path_");
                    if (x == 0)
                    {
                        // attributeElements = attribute.Split('_');
                        attributeValue = attribute.Substring(5);
                        return attributeValue;
                    }
                }
            }

            return string.Empty;
        }

        private bool GetShowDoc(string path)
        {
            //string parentPath = Directory.GetParent(path).FullName;
            //string parentName = new DirectoryInfo(parentPath).Name;

            string docCurrentPath = Path.Combine(path, "Current");

            if (Directory.Exists(docCurrentPath))
            {
                string parseSecPath = Path.Combine(docCurrentPath, "ParseSec");
                if (Directory.Exists(parseSecPath))
                {
                    string KeywordsPath = Path.Combine(parseSecPath, "Keywords");
                    if (Directory.Exists(KeywordsPath))
                    {
                        string[] files = Directory.GetFiles(KeywordsPath, "*.rtf");
                        if (files.Length > 0)
                        {
                            string fileName = Files.GetFileName(files[0]);

                            ucResults_TxtDocView1.LoadData(files[0], KeywordsPath, fileName);
                            ucResults_TxtDocView1.Dock = DockStyle.Fill;
                            ucResults_TxtDocView1.Visible = true;

                            return true;
                        }
                    }
                }

            }

            return false;
        }

        private void tvProjects_AfterSelect(object sender, TreeViewEventArgs e)
        {
            HideAllPanels();

            string nodeKey = tvProjects.SelectedNode.Name.ToString();
            string nodeText = tvProjects.SelectedNode.Text.ToString();

            string nodeTag = string.Empty;
            if (tvProjects.SelectedNode.Tag != null)
                nodeTag = tvProjects.SelectedNode.Tag.ToString();

            string NodeParent = string.Empty;

            if (tvProjects.SelectedNode.Parent != null)
                NodeParent = tvProjects.SelectedNode.Parent.Text.ToString();

            string[] keyAttributes;
            string pathFile = string.Empty;
            string ext = string.Empty;
            int x = -1;
           // string[] attributeElements;
            string attributeValue = string.Empty;
            string subject = string.Empty;
            string path = string.Empty;

            _CurrentPath = path = GetAttributePath(nodeKey);

            if ((nodeKey.IndexOf("proj_") == 0))
            {
                _CurrentMode = ResultsComponents.Project;

                if (ProjectSelected != null)
                    ProjectSelected();
            }
            else if (nodeKey.IndexOf("analysis_") == 0)
            {
                _CurrentMode = ResultsComponents.Analysis;

                if (AnalysisSelected != null)
                    AnalysisSelected();

            }
            else if (nodeKey.IndexOf("doc_") == 0)
            {
                if (AnalysisUnselected != null)
                    AnalysisUnselected();

                _CurrentMode = ResultsComponents.Document;

                HideAllPanels();

                path = GetAttributePath(nodeKey);
                if (Directory.Exists(path))
                {
                    if (!GetShowDoc(path))
                    {
                        string document = GetCurrentDocument(path);
                        if (document.Length == 0)
                        {
                            string lastFolder = Directories.GetLastFolder(path);
                            if (lastFolder != "Current")
                            {
                                if (Directory.Exists(Path.Combine(path, "Current")))
                                {
                                    path = Path.Combine(path, "Current");
                                    document = GetCurrentDocument(path);
                                }
                            }
                        }

                        if (document.Length > 0)
                        {
                            subject = Files.GetFileName(document);
                            ext = Path.GetExtension(document);
                            switch (ext.ToUpper())
                            {
                                case ".XLSX":
                                    //  subject = string.Concat("Report for ", nodeText);
                                    ucResults_ExcelPreview1.LoadData(document, subject);
                                    ucResults_ExcelPreview1.Dock = DockStyle.Fill;
                                    ucResults_ExcelPreview1.Visible = true;
                                    return;
                                case ".DOCX":
                                    ucResults_WordPreview1.LoadData(document, subject);
                                    ucResults_WordPreview1.Dock = DockStyle.Fill;
                                    ucResults_WordPreview1.Visible = true;

                                    return;
                                case ".HTML":
                                    ucResults_HTMLPreview1.LoadData(document, subject);
                                    ucResults_HTMLPreview1.Dock = DockStyle.Fill;
                                    ucResults_HTMLPreview1.Visible = true;
                                    return;
                                case ".RTF":
                                    ucResults_RTF_Preview1.LoadData(document, subject);
                                    ucResults_RTF_Preview1.Dock = DockStyle.Fill;
                                    ucResults_RTF_Preview1.Visible = true;
                                    return;
                                case ".PDF":
                                    ucResults_WordPreview1.LoadData(document, subject);
                                    ucResults_WordPreview1.Dock = DockStyle.Fill;
                                    ucResults_WordPreview1.Visible = true;
                                    //ucResults_HTMLPreview1.LoadData(document, subject);
                                    //ucResults_HTMLPreview1.Dock = DockStyle.Fill;
                                    //ucResults_HTMLPreview1.Visible = true;
                                    return;

                            }

                            //string docName = Files.GetFileName(document);
                            //ucResults_WordPreview1.LoadData(document, docName);
                            //ucResults_WordPreview1.Dock = DockStyle.Fill;
                            //ucResults_WordPreview1.Visible = true;
                        }
                    }
                }
            }
            else if (nodeKey.IndexOf("AnalysisResults_") == 0)
            {
                path = GetAttributePath(nodeKey);

                GetAnalysisResults(path);

                if (AnalysisUnselected != null)
                    AnalysisUnselected();

                _CurrentMode = ResultsComponents.AnalysisResults;
            }

                // Deep analysis
            else if (nodeKey.IndexOf("DeepAnalysisResults_") == 0)
            {
                path = GetAttributePath(nodeKey);

                GetDeepAnalysisResults(path);

                _CurrentMode = ResultsComponents.DeepAnalysisResults_KW;

            }

            // Report
            else if (nodeKey.IndexOf("rpt_") == 0)
            {
                if (AnalysisUnselected != null)
                    AnalysisUnselected();

                _CurrentMode = ResultsComponents.Report;

                keyAttributes = nodeKey.Split('|');
                if (keyAttributes.Length > 0)
                {
                    foreach (string attribute in keyAttributes)
                    {
                        x = attribute.IndexOf("pathFile_");
                        if (x == 0)
                        {
                            // attributeElements = attribute.Split('_');
                            attributeValue = attribute.Substring(9);
                            if (File.Exists(attributeValue))
                            {
                                ext = Path.GetExtension(attributeValue);
                                switch (ext.ToUpper())
                                {
                                    case ".XLSX":
                                        subject = string.Concat("Report for ", nodeText);
                                        ucResults_ExcelPreview1.LoadData(attributeValue, subject);
                                        ucResults_ExcelPreview1.Dock = DockStyle.Fill;
                                        ucResults_ExcelPreview1.Visible = true;
                                        return;
                                    case ".DOCX":
                                        ucResults_WordPreview1.LoadData(attributeValue, subject);
                                        ucResults_WordPreview1.Dock = DockStyle.Fill;
                                        ucResults_WordPreview1.Visible = true;

                                        return;
                                    case ".HTML":
                                        ucResults_HTMLPreview1.LoadData(attributeValue, subject);
                                        ucResults_HTMLPreview1.Dock = DockStyle.Fill;
                                        ucResults_HTMLPreview1.Visible = true;
                                        return;
                                    case ".RTF":
                                        ucResults_RTF_Preview1.LoadData(attributeValue, subject);
                                        ucResults_RTF_Preview1.Dock = DockStyle.Fill;
                                        ucResults_RTF_Preview1.Visible = true;
                                        return;
                                    case ".PDF":
                                        ucResults_WordPreview1.LoadData(attributeValue, subject);
                                        ucResults_WordPreview1.Dock = DockStyle.Fill;
                                        ucResults_WordPreview1.Visible = true;
                                        //ucResults_HTMLPreview1.LoadData(document, subject);
                                        //ucResults_HTMLPreview1.Dock = DockStyle.Fill;
                                        //ucResults_HTMLPreview1.Visible = true;
                                        return;

                                }
                            }

                        }
                    }
                }

            }






        }

        private void picHeader_Click(object sender, EventArgs e)
        {
            
            if (_CurrentPath.Length > 0)
                System.Diagnostics.Process.Start("explorer.exe", _CurrentPath);
        }

        private void ucAnalysisResults1_RunDeepAnalysisResults()
        {
            if (RunDeepAnalysisResults != null)
                RunDeepAnalysisResults();
        }

        private List<TreeNode> CurrentNodeMatches = new List<TreeNode>();

        private int LastNodeIndex = 0;

        private string LastSearchText;


        private void butFind_Click(object sender, EventArgs e)
        {
            string searchText = this.txtbFind.Text;
            if (String.IsNullOrEmpty(searchText))
            {
                return;
            };


            if (LastSearchText != searchText)
            {
                //It's a new Search
                CurrentNodeMatches.Clear();
                LastSearchText = searchText;
                LastNodeIndex = 0;
                SearchNodes(searchText, tvProjects.Nodes[0]);
            }

            if (LastNodeIndex >= 0 && CurrentNodeMatches.Count > 0 && LastNodeIndex < CurrentNodeMatches.Count)
            {
                TreeNode selectedNode = CurrentNodeMatches[LastNodeIndex];
                LastNodeIndex++;
                tvProjects.SelectedNode = selectedNode;
                tvProjects.SelectedNode.Expand();
                tvProjects.Select();

            }
            

        }

        private void SearchNodes(string SearchText, TreeNode StartNode)
        {
            TreeNode node = null;
            while (StartNode != null)
            {
                if (StartNode.Text.ToLower().Contains(SearchText.ToLower()))
                {
                    CurrentNodeMatches.Add(StartNode);
                };
                if (StartNode.Nodes.Count != 0)
                {
                    SearchNodes(SearchText, StartNode.Nodes[0]);//Recursive Search 
                };
                StartNode = StartNode.NextNode;
            };

        }


    }
}
