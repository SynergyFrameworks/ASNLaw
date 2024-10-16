using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
//using OfficeOpenXml;
using System.Xml;
using System.Diagnostics;

using Atebion.Common;





namespace Atebion.ConceptAnalyzer
{
    public class Analysis
    {
        /// <summary>
        /// Use for generating RAM models and templates ONLY!
        /// </summary>
        /// <param name="AppDataPath"></param>
        public Analysis(string AppDataPath)
        {
            AppFolders_CA.AppDataPath = AppDataPath;
            string s = AppFolders_CA.AppDataPathTools;
            s = AppFolders_CA.AppDataPathToolsRAMDefs;
            s = AppFolders_CA.AppDataPathToolsExcelTemp;
        }

        public Analysis(string AnalysisPath, string AppDataPath, string ProjectName)
        {
           // _AnalysisRootPath = AnalysisRootPath;

          //  _ConceptAnalyzerRootPath = ConceptAnalyzerRootPath;

            AppFolders_CA.AnalysisPath = AnalysisPath;

            AppFolders_CA.AppDataPath = AppDataPath;

            AppFolders_CA.ProjectName = ProjectName;

            Validation_Start();
        }

        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        private string _AnalysisLogFile = string.Empty;
        public string AnalysisLogFile
        {
            get { return _AnalysisLogFile; }
        }

        private string _AnalysisErrorLogFile = string.Empty;
        public string AnalysisErrorLogFile
        {
            get { return _AnalysisErrorLogFile; }
        }

    //    private string _ConceptAnalyzerRootPath = string.Empty;
        private string _ParseTypeFile = "ParseType.par";

        private string _AnalysisRootPath = string.Empty;

        private const string _XML_FILE = "AnalysisRuns.xml";

        private DataSet _ds;

        private GenericDataManger _DataMgr = new GenericDataManger();
        private Projects _ProjectMgr = new Projects();
        private Documents _DocMgr;
        private AnalysisDictionaries _DicAnalysis;
        private DiscreteContentMgr _DisContentMgr = new DiscreteContentMgr();
        private AnalysisConcepts _AnalysisConceptsMgr = new AnalysisConcepts();


        private bool Validation_Start()
        {
            _ErrorMessage = string.Empty;

            string s = AppFolders_CA.AnalysisCurrent;

            if (s == string.Empty)
                return false;

            s = AppFolders_CA.ProjectCurrent;

            if (s == string.Empty)
                return false;

            return true;
        }

        /// <summary>
        /// Validate core CA folders during initialization
        /// </summary>
        /// <returns></returns>
        //private bool Validation_Start()
        //{
        //    _ErrorMessage = string.Empty;

        //    if (!Directory.Exists(_ConceptAnalyzerRootPath))
        //    {
        //        _ErrorMessage = "Concept Analyzer Root Path is not found.";
        //        return false;
        //    }

        //    AppFolders_CA.AppDataPath = _ConceptAnalyzerRootPath;

        //    string s = string.Empty;

        //    //string s = AppFolders_CA.AppDataPath_Tools_CA_Use_Cases;
        //    //if (AppFolders_CA.ErrorMessage != string.Empty)
        //    //{
        //    //    _ErrorMessage = AppFolders_CA.ErrorMessage;
        //    //    return false;
        //    //}

        //    s = AppFolders_CA.Project;
        //    if (AppFolders_CA.ErrorMessage != string.Empty)
        //    {
        //        _ErrorMessage = AppFolders_CA.ErrorMessage;
        //        return false;
        //    }

        //    s = AppFolders_CA.AppDataPathToolsExcelTemp;
        //    if (AppFolders_CA.ErrorMessage != string.Empty)
        //    {
        //        _ErrorMessage = AppFolders_CA.ErrorMessage;
        //        return false;
        //    }

        //    s = AppFolders_CA.AppDataPathToolsExcelTempDicDoc;
        //    if (AppFolders_CA.ErrorMessage != string.Empty)
        //    {
        //        _ErrorMessage = AppFolders_CA.ErrorMessage;
        //        return false;
        //    }

        //    s = AppFolders_CA.AppDataPathToolsExcelTempConceptsDocs;
        //    if (AppFolders_CA.ErrorMessage != string.Empty)
        //    {
        //        _ErrorMessage = AppFolders_CA.ErrorMessage;
        //        return false;
        //    }

        //    s = AppFolders_CA.AppDataPathToolsExcelTempDicDoc;
        //    if (AppFolders_CA.ErrorMessage != string.Empty)
        //    {
        //        _ErrorMessage = AppFolders_CA.ErrorMessage;
        //        return false;
        //    }

        //    s = AppFolders_CA.AppDataPathToolsExcelTempDicDocs;
        //    if (AppFolders_CA.ErrorMessage != string.Empty)
        //    {
        //        _ErrorMessage = AppFolders_CA.ErrorMessage;
        //        return false;
        //    }
            


        //    return true;
        //}

        private bool CheckCAProjectFolders(string projectName)
        {
            AppFolders_CA.ProjectName = projectName;
            string s = AppFolders_CA.ProjectCurrent;
            if (s.Length == 0)
            {
                _ErrorMessage = AppFolders_CA.ErrorMessage;
                return false;
            }

            s = AppFolders_CA.DocPath;
            if (s.Length == 0)
            {
                _ErrorMessage = AppFolders_CA.ErrorMessage;
                return false;
            }


            return true;
        }


        public enum ParseType
        {
            Legal = 0,
            Paragraph = 1
        }

        //private void DocLogFile_Write(StringBuilder docLog, string docName)
        //{
        //    string logFile = string.Concat(docName, ".log");
        //    string logPathFile = Path.Combine(AppFolders_CA.AnalysisCurrent, logFile);

        //    Files.WriteStringToFile(docLog.ToString(), logPathFile, true);
        //}

        //public bool AnalysisNameExits(string ProjectName, string AnalysisName)
        //{
        //    // Set Folders
        //    AppFolders_CA.ProjectName = ProjectName;
        //    string s = AppFolders_CA.Project;
        //    string projectFolder = AppFolders_CA.ProjectCurrent;
        //    string projectDocsFolder = AppFolders_CA.DocPath;

        //   return AppFolders_CA.AnalysisNameExists(AnalysisName);
        //}

        //public string GetNextAnalysisName(string ProjectName)
        //{
        //    const string ANALYSIS_NAME_PREFIX = "Analysis_";
        //    string newAnalysisName = string.Empty;

        //    // Set Folders
        //    AppFolders_CA.ProjectName = ProjectName;
        //    string s = AppFolders_CA.Project;
        //    string projectFolder = AppFolders_CA.ProjectCurrent;
        //    string projectDocsFolder = AppFolders_CA.DocPath;

        //    for (int i = 0; i < 32000; i++)
        //    {
        //        newAnalysisName = string.Concat(ANALYSIS_NAME_PREFIX, i.ToString());

        //        if (!AppFolders_CA.AnalysisNameExists(newAnalysisName))
        //        {
        //            return newAnalysisName;
        //        }
        //    }

        //    return newAnalysisName;
        //}

        public bool Analyze4DictionaryDocsCompare(string ProjectName, string AnalysisName, string[] Docs, string Dictionary, string DictionariesPath, out string SumXRefPathFile, out DataSet dsDicFilter)
        {
            _DicAnalysis.Get_Documents_Dic_Summary(ProjectName, AnalysisName, Docs, Dictionary, DictionariesPath, out SumXRefPathFile, out dsDicFilter);

            if (_DicAnalysis.ErrorMessage.Length > 0)
            {
                _ErrorMessage = _DicAnalysis.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool Analyze4DictionaryTerms(string ProjectName, string AnalysisName, string Dictionary, string ProjectsPath, string DictionariesPath, string DocXMLPath, string DocParseSegPath, string doc, bool FindWholeWords, bool FindSynonyms, bool DoCompareDocs)
        {
            _ErrorMessage = string.Empty;

            if (Dictionary.Length == 0)
            {
                _ErrorMessage = "The Dictionary has not been selected.";
                return false;
            }

            if (doc == null)
            {
                _ErrorMessage = "No document has been selected.";
                return false;
            }

            if (_DicAnalysis == null)
                _DicAnalysis = new AnalysisDictionaries();

            int NotFoundDicItemsQty = 0;
            int foundDicitemsQty = 0;

            // Find Dictionary Items in Document
            foundDicitemsQty = _DicAnalysis.Dictionary_Analysis(AnalysisName, doc, Dictionary, DictionariesPath, DocXMLPath, DocParseSegPath, FindWholeWords, FindSynonyms, out NotFoundDicItemsQty);

            if (foundDicitemsQty == -1)
            {
                _ErrorMessage = _DicAnalysis.ErrorMessage;
                return false;
            }

            // Generate Summary
            _DicAnalysis.Get_Document_Dic_Summary(ProjectName, AnalysisName, doc);
            
            return true;
        }

        public bool Analyze4Concepts(string ProjectName, string AnalysisName, string ProjectsPath, string[] docs, bool DoCompareDocs)
        {
            _ErrorMessage = string.Empty;

            string TxtFilePath = string.Empty;
            string docPath = string.Empty;

            if (docs == null)
            {
                _ErrorMessage = "No documents have been selected.";
                return false;
            }

            AppFolders_CA.ProjectName = ProjectName;

            if (AppFolders_CA.ProjectName.Trim().Length == 0)
            {
                _ErrorMessage = "Project has Not been defined/selected.";
                return false;
            }

            if (_DocMgr == null)
            {
                _DocMgr = new Documents();
                if (_DocMgr == null)
                {
                    _ErrorMessage = "Unable to open the Documents Manager.";
                    return false;
                }
            }

            if (_AnalysisConceptsMgr == null)
            {
                _AnalysisConceptsMgr = new AnalysisConcepts();
            }


       //     string txtPathFile = string.Empty; // Converted document into a plain text file

            string DocInfoPath = string.Empty;
            string DocIndex2Path = string.Empty;
            string DocParsePagesPath = string.Empty;
            string DocParseSegPath = string.Empty;
            string DocXMLPath = string.Empty;
            string DocConceptParseSegPath = string.Empty;
            string AnalysisDocConceptParseSegPath = string.Empty;

            string AnalysisDocPath = string.Empty;
            string AnalysisDocInfoPath = string.Empty;
            string AnalysisReportsPath = string.Empty;
            string AnalysisDocNotes = string.Empty;
            string AnalysisDocAnalysisResultsPath = string.Empty;
            string AnalysisDocXMLPath = string.Empty;
            string AnalysisDocHTMLPath = string.Empty;

            bool previousParse = false;

            List<string> conceptsFound = new List<string>();


            StringBuilder sbErrors = new StringBuilder();

            string[] txtFiles;

            foreach (string doc in docs)
            {
                List<string> docConceptsFound = new List<string>();

                docPath = Path.Combine(ProjectsPath, "Docs", doc, "Current");
                if (!Directory.Exists(docPath))
                {
                    sbErrors.AppendLine(string.Concat("Concept Analysis Error - Document Path Not Found: ", docPath));
                    continue;
                }

                txtFiles = Directory.GetFiles(docPath, "*.txt");
                if (txtFiles.Length == 0)
                {
                    sbErrors.AppendLine(string.Concat("Concept Analysis Error - Text Document was not found in path: ", docPath));

                    continue;
                }

                TxtFilePath = txtFiles[0];

               _DocMgr.GetPathTxtFileName(ProjectName, AnalysisName, doc, TxtFilePath, out DocInfoPath, out DocIndex2Path, out DocParsePagesPath, out DocParseSegPath, out DocXMLPath, out DocConceptParseSegPath);

                if (!_DocMgr.Summary_Generate(TxtFilePath, DocInfoPath, true, true, out docConceptsFound))
                {
                     sbErrors.AppendLine(string.Concat("Concept Analysis - Document: ", doc, " - Summary/Concept Error: ", _DocMgr.ErrorMessage));
                     continue;
                }
                else
                {
                    if (!_AnalysisConceptsMgr.FindConceptsInSegments(docConceptsFound, AppFolders_CA.AnalysisParseSegXML, DocParseSegPath, DocConceptParseSegPath))
                    {
                        sbErrors.AppendLine(_AnalysisConceptsMgr.ErrorMessage);
                        continue;
                    }

                    DataFunctions.CombineLists(ref conceptsFound, docConceptsFound);
                }

            }
            //Cleanup operation
            string cleanupDir = ProjectsPath + "\\Analysis\\" + AnalysisName + "\\Docs\\";
            string[] dirs = Directory.GetDirectories(cleanupDir);

            bool dist = false;
            foreach (string dir in dirs)
            {
                foreach (string doc in docs)
                {
                    if (dir.Split('\\').LastOrDefault() == doc)
                    {
                        dist = true;
                    }
                }
                if (dist == false)
                {
                    // delete
                    //Console.WriteLine("path"+ cleanupDir + "\\" + dir.Split('\\').LastOrDefault());
                    Directory.Delete(cleanupDir + "\\" + dir.Split('\\').LastOrDefault(), true);
                }

                dist = false;
            }

            if (DoCompareDocs)
            {
                foreach (string doc in docs)
                {
                    // Setup Analysis Document Folders
                    AppFolders_CA.DocName = doc;
                    AnalysisDocPath = AppFolders_CA.AnalysisCurrentDocsDocName;
                    if (AnalysisDocPath.Length == 0)
                    {
                        sbErrors.AppendLine(string.Concat("Concept Documents Compare Analysis - Document: ", doc, " - Folder Error: ", AppFolders_CA.ErrorMessage));
                        continue; // Go to the next Doc
                    }

                    AnalysisDocXMLPath = AppFolders_CA.AnalysisParseSegXML;
                    if (AnalysisDocXMLPath.Length == 0)
                    {
                        sbErrors.AppendLine(string.Concat("Concept Documents Compare Analysis - Document: ", doc, " - Folder Error: ", AppFolders_CA.ErrorMessage));
                        continue; // Go to the next Doc
                    }

                    AnalysisDocConceptParseSegPath = AppFolders_CA.AnalysisParseSeg;
                    if (AnalysisDocConceptParseSegPath.Length == 0)
                    {
                        sbErrors.AppendLine(string.Concat("Concept Documents Compare Analysis - Document: ", doc, " - Folder Error: ", AppFolders_CA.ErrorMessage));
                        continue; // Go to the next Doc
                    }

                    DocParsePagesPath = AppFolders_CA.DocParsePage;
                    if (DocParsePagesPath.Length == 0)
                    {
                        sbErrors.AppendLine(string.Concat("Concept Documents Compare Analysis - Document: ", doc, " - Folder Error: ", AppFolders_CA.ErrorMessage));
                        continue; // Go to the next Doc
                    }


                    string notUsed = _DocMgr.GetPathTxtFileName(ProjectName, AnalysisName, doc, TxtFilePath, out DocInfoPath, out DocIndex2Path, out DocParsePagesPath, out DocParseSegPath, out DocXMLPath, out DocConceptParseSegPath);

                 //   string notUsed = _DocMgr.GetPathTxtFileName(ProjectName, doc, out DocInfoPath, out DocIndex2Path, out DocParsePagesPath, out DocParseSegPath, out DocXMLPath, out DocConceptParseSegPath);


                    _AnalysisConceptsMgr.FindConceptsInSegments(conceptsFound, AnalysisDocXMLPath, DocParseSegPath, AnalysisDocConceptParseSegPath);
                    

                }

                DataSet dsXFilter = new DataSet();
                string xRefPath = string.Empty;

                _AnalysisConceptsMgr.Get_Documents_Concept_Summary(ProjectName, AnalysisName, docs, conceptsFound, out xRefPath, out dsXFilter); 

            }

            if (sbErrors.Length == 0)
            {
                return true;
            }
            else
            {
                _ErrorMessage = sbErrors.ToString();
                return false;
            }
        }

        public bool Generate_Default_RAM_Models()
        {
            _ErrorMessage = string.Empty;

            ResponsAssigMatrix RAM_Mgr = new ResponsAssigMatrix();

            if (!RAM_Mgr.Generate_Default_RAM_Models())
            {
                _ErrorMessage = RAM_Mgr.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool GenerateSampleRAMExcelTemplate()
        {
            _ErrorMessage = string.Empty;

            ResponsAssigMatrix RAM_Mgr = new ResponsAssigMatrix();

            if (!RAM_Mgr.GenerateSampleExcelTemplate())
            {
                _ErrorMessage = RAM_Mgr.ErrorMessage;
                return false;
            }

            return true;

        }

        //public bool AnalyzeDocs(string ProjectName, string AnalysisName, string[] Docs, ParseType parseType, string Dictionary, string DictionariesPath, bool FindWholeWords, bool FindSynonyms, bool GenSummaries, bool FindConcepts, bool FindEmails, bool FindDates, bool FindURLs, string UseCaseName, string UseCaseDescription)
        //{
        //    _ErrorMessage = string.Empty;


        //    if (AppFolders_CA.ProjectName.Trim().Length == 0)
        //    {
        //        _ErrorMessage = "Project has Not been defined/selected.";
        //        return false;
        //    }

        //    if (_DocMgr == null)
        //    {
        //        _DocMgr = new Documents();
        //        if (_DocMgr == null)
        //        {
        //            _ErrorMessage = "Unable to open the Documents Manager.";
        //            return false;
        //        }
        //    }

        //    StringBuilder sbLog = new StringBuilder();
        //    StringBuilder sbErrors = new StringBuilder();

        //    sbLog.AppendLine(string.Concat("Analysis: ", AnalysisName));
        //    sbLog.AppendLine(string.Concat("ProjectName: ", ProjectName));
        //    sbLog.AppendLine(string.Concat("Documents Qty: ", Docs.Length));
        // //   sbLog.AppendLine(string.Concat("Analysis Run By: ", "Tom Lipscomb")); // ToDo change
        //    DateTime now = DateTime.Now;
        //    sbLog.AppendLine(string.Concat("Date: ", now.ToLongDateString()));

        //    sbLog.AppendLine(string.Empty);
        //    sbLog.AppendLine("Start - Analysis ------------------------------------");

        //    // measure total time to complete analysis
        //    Stopwatch stopwtotal = new Stopwatch();
        //    stopwtotal.Start();

        //    // Set Folders
        //    AppFolders_CA.ProjectName = ProjectName;
        //    string s = AppFolders_CA.Project;
        //    string projectFolder = AppFolders_CA.ProjectCurrent;
        //    string projectDocsFolder = AppFolders_CA.DocPath;





        //    AppFolders_CA.AnalysisName = AnalysisName;
        //    string AnalysisPath = AppFolders_CA.AnalysisCurrent;
            
            
        //   // string AnalysisInfoPath = AppFolders_CA.AnalysisInfor;

        //    if (AnalysisPath.Length == 0)
        //    {
        //        _ErrorMessage = string.Concat("Error: Unable to create Analysis Name folder: ", AnalysisName);            

        //        stopwtotal.Stop();
        //        return false;
        //    }

        //    // Create Analysis Parameter XML
        //    DataTable dtAnalysis = CreateEmptyDataTable_Analysis();
        //    DataRow rowAnalysis = dtAnalysis.NewRow();
        //    rowAnalysis[AnalysisUCaseFieldConst.UID] = 0;
        //    rowAnalysis[AnalysisUCaseFieldConst.CreatedBy] = AppFolders_CA.UserName;
        //    now = DateTime.Now;
        //    rowAnalysis[AnalysisUCaseFieldConst.DateCreated] = now.ToLongDateString();
        //    rowAnalysis[AnalysisUCaseFieldConst.DicFindSynonyms] = FindSynonyms;
        //    rowAnalysis[AnalysisUCaseFieldConst.DicFindWholewords] = FindWholeWords;
        //    rowAnalysis[AnalysisUCaseFieldConst.DictionaryName] = Dictionary;
        //    rowAnalysis[AnalysisUCaseFieldConst.FindConcepts] = FindConcepts;
        //    rowAnalysis[AnalysisUCaseFieldConst.FindDates] = FindDates;
        //    rowAnalysis[AnalysisUCaseFieldConst.FindEmails] = FindEmails;
        //    rowAnalysis[AnalysisUCaseFieldConst.FindURLs] = FindURLs;
        //    rowAnalysis[AnalysisUCaseFieldConst.GenerateSummaries] = GenSummaries;
        //    rowAnalysis[AnalysisUCaseFieldConst.Name] = AnalysisName;
        //    rowAnalysis[AnalysisUCaseFieldConst.ParseType] = parseType.ToString();

        //    dtAnalysis.Rows.Add(rowAnalysis);

        //    DataSet dsAnalysis = new DataSet();
        //    dsAnalysis.Tables.Add(dtAnalysis);
        //    string analysisPatXMLFile = Path.Combine(AnalysisPath, AnalysisUCaseFieldConst.XMLAnalysisFile);

        //    GenericDataManger gdManager = new GenericDataManger();
        //    gdManager.SaveDataXML(dsAnalysis, analysisPatXMLFile);

        //    //bool UseCaseExist = true;
        //    //if (UseCaseName.Length > 0)
        //    //{
        //    //    UseCase usecase = new UseCase(AppFolders_CA.AppDataPath_Tools_CA_Use_Cases);
        //    //    UseCaseRuns usecaseRuns = new UseCaseRuns(AppFolders_CA.AppDataPath_Tools_CA_Use_Cases);

        //    //    if (!usecase.UseCaseExists(UseCaseName))
        //    //    {
        //    //        if (!usecase.CreateNewUseCase(UseCaseName, UseCaseDescription, parseType.ToString(), Dictionary, DictionariesPath, FindWholeWords, FindSynonyms, GenSummaries, FindConcepts, FindEmails, FindDates, FindURLs))
        //    //        {
        //    //            sbLog.AppendLine(string.Concat("Create Use Case Error: ", usecase.ErrorMessage));
        //    //            sbErrors.AppendLine(string.Concat("Create Use Case - Name: ", UseCaseName, " - Error: ", usecase.ErrorMessage));
        //    //            UseCaseExist = false;
        //    //        }
        //    //    }
        //    //    if (UseCaseExist)
        //    //    {
        //    //        sbLog.AppendLine("");
        //    //        if (UseCaseDescription.Length > 0)
        //    //            sbLog.AppendLine(string.Concat("Created Use Case: ", UseCaseName, " - ", UseCaseDescription));
        //    //        else
        //    //            sbLog.AppendLine(string.Concat("Created Use Case: ", UseCaseName));


        //    //        if (!usecaseRuns.AddNewUseCaseRun(UseCaseName, ProjectName, AnalysisName))
        //    //        {
        //    //            sbLog.AppendLine(string.Concat("Create Use Case Run Error: ", usecaseRuns.ErrorMessage));
        //    //            sbErrors.AppendLine(string.Concat("Create Use Case Run - Name: ", UseCaseName, " - Error: ", usecaseRuns.ErrorMessage));
        //    //        }
        //    //        else
        //    //        {
        //    //            sbLog.AppendLine(string.Concat("Created Use Case Run: ", UseCaseName));
        //    //        }
        //    //        sbLog.AppendLine("");
        //    //    }
        //    //}



        //    string txtPathFile = string.Empty; // Converted document into a plain text file

        //    string DocInfoPath = string.Empty;
        //    string DocIndex2Path = string.Empty;
        //    string DocParsePagesPath = string.Empty;
        //    string DocParseSegPath = string.Empty;
        //    string DocXMLPath = string.Empty;
        //    string DocConceptParseSegPath = string.Empty;
        //    string AnalysisDocConceptParseSegPath = string.Empty;

        //    string AnalysisDocPath = string.Empty;
        //    string AnalysisDocInfoPath = string.Empty;
        //    string AnalysisReportsPath = string.Empty;
        //    string AnalysisDocNotes = string.Empty;
        //    string AnalysisDocAnalysisResultsPath = string.Empty;
        //    string AnalysisDocXMLPath = string.Empty;
        //    string AnalysisDocHTMLPath = string.Empty;

        //    bool previousParse = false;

        //    List<string> conceptsFound = new List<string>();

        //    // Loop through each Document
        //    foreach (string doc in Docs)
        //    {

        //        // measure total time to complete analysis
        //        Stopwatch stopwDoc = new Stopwatch();
        //        stopwDoc.Start();

        //        sbLog.AppendLine(string.Empty);
        //        sbLog.AppendLine(string.Empty);
        //        sbLog.AppendLine(string.Concat("Start - Document Analysis - Document: ",  doc, " ------------------------------------"));

        //        txtPathFile = _DocMgr.GetPathTxtFileName(ProjectName, doc, out DocInfoPath, out DocIndex2Path, out DocParsePagesPath, out DocParseSegPath, out DocXMLPath, out DocConceptParseSegPath);
        //        if (txtPathFile.Length == 0)
        //        {
        //            sbLog.AppendLine(string.Concat("Error: ", _DocMgr.ErrorMessage));
        //            sbErrors.AppendLine(string.Concat("Document Analysis - Document: ", doc, " - File Error: ", _DocMgr.ErrorMessage));

        //            continue; // Go to the next Doc
        //        }
 
        //        // Setup Analysis Document Folders
        //        AppFolders_CA.AnalysisCurrentDocsDocName = doc;
        //        AnalysisDocPath = AppFolders_CA.AnalysisCurrentDocsDocName;
        //        if (AnalysisDocPath.Length == 0)
        //        {
        //            sbLog.AppendLine(string.Concat("Error: ", AppFolders_CA.ErrorMessage));
        //            sbErrors.AppendLine(string.Concat("Document Analysis - Document: ", doc, " - Folder Error: ", AppFolders_CA.ErrorMessage));
        //            continue; // Go to the next Doc
        //        }

        //        AnalysisDocInfoPath = AppFolders_CA.AnalysisInfor;
        //        if (AnalysisDocInfoPath.Length == 0)
        //        {
        //            sbLog.AppendLine(string.Concat("Error: ", AppFolders_CA.ErrorMessage));
        //            sbErrors.AppendLine(string.Concat("Document Analysis - Document: ", doc, " - Folder Error: ", AppFolders_CA.ErrorMessage));
        //            continue; // Go to the next Doc
        //        }

        //        AnalysisDocAnalysisResultsPath = AppFolders_CA.AnalysisParseSeg;
        //        if (AnalysisDocAnalysisResultsPath.Length == 0)
        //        {
        //            sbLog.AppendLine(string.Concat("Error: ", AppFolders_CA.ErrorMessage));
        //            sbErrors.AppendLine(string.Concat("Document Analysis - Document: ", doc, " - Folder Error: ", AppFolders_CA.ErrorMessage));
        //            continue; // Go to the next Doc
        //        }

        //        AnalysisReportsPath = AppFolders_CA.AnalysisCurrent_ParseResults_Doc_Reports;
        //        if (AnalysisReportsPath.Length == 0)
        //        {
        //            sbLog.AppendLine(string.Concat("Error: ", AppFolders_CA.ErrorMessage));
        //            sbErrors.AppendLine(string.Concat("Document Analysis - Document: ", doc, " - Folder Error: ", AppFolders_CA.ErrorMessage));
        //            continue; // Go to the next Doc
        //        }

        //        AnalysisDocHTMLPath = AppFolders_CA.AnalysisCurrent_ParseResults_Doc_HTML;
        //        if (AnalysisDocHTMLPath.Length == 0)
        //        {
        //            sbLog.AppendLine(string.Concat("Error: ", AppFolders_CA.ErrorMessage));
        //            sbErrors.AppendLine(string.Concat("Document Analysis - Document: ", doc, " - Folder Error: ", AppFolders_CA.ErrorMessage));
        //            continue; // Go to the next Doc
        //        }

        //        AnalysisDocNotes = AppFolders_CA.AnalysisNotes;
        //        if (AnalysisDocNotes.Length == 0)
        //        {
        //            sbLog.AppendLine(string.Concat("Error: ", AppFolders_CA.ErrorMessage));
        //            sbErrors.AppendLine(string.Concat("Document Analysis - Document: ", doc, " - Folder Error: ", AppFolders_CA.ErrorMessage));
        //            continue; // Go to the next Doc
        //        }

        //        AnalysisDocXMLPath = AppFolders_CA.AnalysisXML;
        //        if (AnalysisDocXMLPath.Length == 0)
        //        {
        //            sbLog.AppendLine(string.Concat("Error: ", AppFolders_CA.ErrorMessage));
        //            sbErrors.AppendLine(string.Concat("Document Analysis - Document: ", doc, " - Folder Error: ", AppFolders_CA.ErrorMessage));
        //            continue; // Go to the next Doc
        //        }

        //        // End -- Setup Analysis Document Folders
                  

        //        // Parse
        //        sbLog.AppendLine(string.Empty);
        //        string parseTypePathFile = Path.Combine(DocInfoPath, _ParseTypeFile);
        //        string previousParseType = string.Empty;
        //        if (File.Exists(parseTypePathFile))
        //        {
        //            previousParseType = Files.ReadFile(parseTypePathFile);
        //        }
        //        int segCount = 0;
        //        if (parseType == ParseType.Legal)
        //        {
        //            sbLog.AppendLine("Parse Type: Legal");
        //            if (previousParseType.IndexOf("Legal") == -1)
        //            {
        //                previousParse = false;

        //                Files.DeleteAllFileInDir(DocParseSegPath);

        //                string xmlParseResults = Path.Combine(DocXMLPath, ParseResultsFields.XMLFile);
        //                if (File.Exists(xmlParseResults))
        //                    File.Delete(xmlParseResults);

        //                // measure total time to parse document
        //                Stopwatch stopwParse = new Stopwatch();
        //                stopwParse.Start();

        //                segCount = _DocMgr.Parse_Legal(txtPathFile, DocParseSegPath, DocXMLPath);

        //                Files.WriteStringToFile("Legal", parseTypePathFile);
        //                sbLog.AppendLine(string.Concat(doc, " - Segment Qty: ", segCount.ToString()));

        //                stopwParse.Stop();
        //                double time = stopwParse.Elapsed.TotalSeconds;
        //                string timeX = FormatTimeProcess(time);
        //                sbLog.AppendLine(string.Concat("Time: ", timeX));

        //            }
        //            else
        //            {
        //                sbLog.AppendLine("Parsing was completed in a previous analysis");
        //                previousParse = true;
        //            }

                    
        //        }
        //        else
        //        {
        //            sbLog.AppendLine("Parse Type: Paragraphs");

        //            if (previousParseType.IndexOf("Paragraphs") == -1)
        //            {
        //                previousParse = false; 

        //                Files.DeleteAllFileInDir(DocParseSegPath);

        //                string xmlParseResults = Path.Combine(DocXMLPath, ParseResultsFields.XMLFile);
        //                if (File.Exists(xmlParseResults))
        //                    File.Delete(xmlParseResults);

        //                // measure total time to parse document
        //                Stopwatch stopwParse = new Stopwatch();
        //                stopwParse.Start();

        //                segCount = _DocMgr.Parse_Paragraph(txtPathFile, DocParseSegPath, DocXMLPath);

        //                Files.WriteStringToFile("Paragraphs", parseTypePathFile);
        //                sbLog.AppendLine(string.Concat(doc, " - Paragraphs Qty: ", segCount.ToString()));

        //                stopwParse.Stop();
        //                double time = stopwParse.Elapsed.TotalSeconds;
        //                string timeX = FormatTimeProcess(time);
        //                sbLog.AppendLine(string.Concat("Time: ", timeX));
        //            }
        //            else
        //            {
        //                sbLog.AppendLine("Parsing was completed in a previous analysis");
        //                previousParse = true;
        //            }
        //        }

        //        if (!_DocMgr.ParseResults_CheckFixNewCols(DocXMLPath))
        //        {
        //            sbLog.AppendLine(string.Concat("Unable to continue analysis for Document: ", doc, "    Error: ", _DocMgr.ErrorMessage));
        //            sbErrors.AppendLine(string.Concat("Document Analysis - Document: ", doc, " - Analysis Results Data Error: ", _DocMgr.ErrorMessage));
        //            continue; // Go to the next Doc
        //        }
        //        // End Parse
    

        //        // Map Parse Results with Pages
        //        if (!previousParse)
        //        {
        //            sbLog.AppendLine(string.Empty);
        //            sbLog.AppendLine("Map parse segments/paragraphs to pages");
        //            // measure total time to parse document
        //            Stopwatch stopwPageMapping = new Stopwatch();
        //            stopwPageMapping.Start();

        //            if (!_DocMgr.MapParseSeg4Pages(DocParsePagesPath, DocParseSegPath, DocXMLPath))
        //            {
        //                sbLog.AppendLine(string.Concat("Mapping Parsed Segments/Paragraphs for document ", doc, "    Warning: ", _DocMgr.ErrorMessage));
        //                sbErrors.AppendLine(string.Concat("Document Analysis - Document: ", doc, " - Mapping Parsed Segments/Paragraphs Warning: ", _DocMgr.ErrorMessage));
        //                stopwPageMapping.Stop();
        //            }
        //            else
        //            {
        //                stopwPageMapping.Stop();
        //                double time = stopwPageMapping.Elapsed.TotalSeconds;
        //                string timeX = FormatTimeProcess(time);
        //                sbLog.AppendLine(string.Concat("Time: ", timeX));
        //            }
        //        }
        //        else
        //        {
        //            sbLog.AppendLine(string.Empty);
        //            sbLog.AppendLine("Map parse segments/paragraphs to pages, completed in a previous analysis");
        //        }



        //        // Generate the Lucene Search Index
        //        if (!previousParse)
        //        {
        //            sbLog.AppendLine(string.Empty);
        //            sbLog.AppendLine("Generate Lucene Search Index");
        //            // measure total time to parse document
        //            Stopwatch stopwIndexer = new Stopwatch();
        //            stopwIndexer.Start();

        //            Indexer indexer = new Indexer();
        //            indexer.CreateIndex(DocIndex2Path, DocParseSegPath);

        //            double timeIndexer = stopwIndexer.Elapsed.TotalSeconds;
        //            string timeIndexerX = FormatTimeProcess(timeIndexer);
        //            sbLog.AppendLine(string.Concat("Time: ", timeIndexerX));
        //        }
        //        else
        //        {
        //            sbLog.AppendLine(string.Empty);
        //            sbLog.AppendLine("Generate Lucene Search Index, completed in a previous analysis");

        //        }


        //        // Find dictionatry items in parsed segments/paragraphs
        //        if (Dictionary.Length > 0)
        //        {
        //            sbLog.AppendLine(string.Empty);
        //            sbLog.AppendLine(string.Concat("Find Dictionary Items - Dictionary: ", Dictionary));
        //            // measure total time to parse document
        //            Stopwatch stopwDic = new Stopwatch();
        //            stopwDic.Start();

        //            if (_DicAnalysis == null)
        //            _DicAnalysis = new AnalysisDictionaries();

        //            // Find Dictionary Items in Document
        //            int NotFoundDicItemsQty;
        //            int foundDicitemsQty = _DicAnalysis.Dictionary_Analysis(AnalysisName, doc, Dictionary, DictionariesPath, DocXMLPath, DocParseSegPath, FindWholeWords, FindSynonyms, out NotFoundDicItemsQty);

        //            // Generate Summary
        //            _DicAnalysis.Get_Document_Dic_Summary(ProjectName, AnalysisName, doc);


        //            sbLog.AppendLine(string.Concat("Found Dictionary Items: ", foundDicitemsQty.ToString()));
        //            sbLog.AppendLine(string.Concat("Dictionary Items Not Found: ", NotFoundDicItemsQty.ToString()));

        //            double time = stopwDic.Elapsed.TotalSeconds;
        //            string timeX = FormatTimeProcess(time);
        //            sbLog.AppendLine(string.Concat("Time: ", timeX));
        //        }

        //        // Find Discrete Contents - Emails, Dates & URLs
        //        if (!previousParse)
        //        {
        //            if (FindEmails || FindDates || FindURLs)
        //            {
        //                sbLog.AppendLine(string.Empty);
        //                sbLog.AppendLine(string.Concat("Find Discrete Content Items - Find Emails: ", FindEmails.ToString(), " - Find Dates: ", FindDates.ToString(), " - Find URLs: ", FindURLs.ToString()));
        //                // measure total time to parse document
        //                Stopwatch stopwDiscreteContent = new Stopwatch();
        //                stopwDiscreteContent.Start();

        //                int emailsFound = 0;
        //                int datesFound = 0;
        //                int urlsFound = 0;

        //                int DiscreteContentQty = _DisContentMgr.FindDiscreteContent(AnalysisName, doc, DocXMLPath, DocParseSegPath, FindEmails, FindDates, FindURLs, out emailsFound, out datesFound, out urlsFound);

        //                sbLog.AppendLine(string.Concat("Emails Found: ", emailsFound.ToString()));
        //                sbLog.AppendLine(string.Concat("Dates Found: ", datesFound.ToString()));
        //                sbLog.AppendLine(string.Concat("URLs Found: ", urlsFound.ToString()));
        //                sbLog.AppendLine(string.Concat("Total Found: ", DiscreteContentQty.ToString()));



        //                double time = stopwDiscreteContent.Elapsed.TotalSeconds;
        //                string timeX = FormatTimeProcess(time);
        //                sbLog.AppendLine(string.Concat("Time: ", timeX));
        //            }
        //        }
        //        else
        //        {
        //            sbLog.AppendLine(string.Empty);
        //            sbLog.AppendLine("Find Discrete Content Items, completed in a previous analysis");
        //        }

        //        // Generate Summary and Find Concepts
        //        //if (GenSummaries || FindConcepts)
        //        //{
        //            // measure total time to geneate summary and/or identify concepts
        //            Stopwatch stopwSummary = new Stopwatch();
        //            stopwSummary.Start();

        //            sbLog.AppendLine(string.Empty);
        //            if (GenSummaries && FindConcepts)
        //            {
        //                sbLog.AppendLine("Generate Summary and Identify Concepts");
        //            }
        //            else if (GenSummaries)
        //            {
        //                sbLog.AppendLine("Generate Summary");
        //            }
        //            else if (GenSummaries && FindConcepts)
        //            {
        //                sbLog.AppendLine("Identify Concepts");
        //            }

        //            List<string> docConceptsFound = new List<string>();

        //            if (!_DocMgr.Summary_Generate(txtPathFile, DocInfoPath, GenSummaries, FindConcepts, out docConceptsFound))
        //            {
        //                sbLog.AppendLine(string.Concat("Error: ", _DocMgr.ErrorMessage));
        //                sbErrors.AppendLine(string.Concat("Document Analysis - Document: ", doc, " - Summary/Concept Error: ", _DocMgr.ErrorMessage));
        //                stopwSummary.Stop();
        //                //continue; // Go to the next Doc
        //            }
        //            else
        //            {
        //                stopwSummary.Stop();
        //                double time = stopwSummary.Elapsed.TotalSeconds;
        //                string timeX = string.Empty;
        //                if (time >= 60)
        //                {
        //                    time = time / 60;
        //                    timeX = string.Concat("Time: ", time.ToString(), " min.s");
        //                }
        //                else
        //                {
        //                    timeX = string.Concat("Time: ", time.ToString(), " sec.s");
        //                }

        //                sbLog.AppendLine(string.Concat("Time: ", timeX));

        //                _AnalysisConceptsMgr.FindConceptsInSegments(docConceptsFound, DocXMLPath, DocParseSegPath, DocConceptParseSegPath);

        //                DataFunctions.CombineLists(ref conceptsFound, docConceptsFound);
        //            }

        //        //}
        //        // End Summary

        //        sbLog.AppendLine(string.Empty);
        //        sbLog.AppendLine(string.Empty);
        //        stopwDoc.Start();
        //        double timeDoc = stopwDoc.Elapsed.TotalSeconds;
        //        string timeDocX = FormatTimeProcess(timeDoc);
        //        sbLog.AppendLine(string.Concat("Total Document Analysis Time: ", timeDoc));
        //        sbLog.AppendLine(string.Concat("End - Document Analysis - Document: ", doc, " ------------------------------------"));

        //    } // Loop Documents


        //    if (Docs.Length > 1 && Dictionary.Length > 0)
        //    {
        //        sbLog.AppendLine(string.Empty);
        //        sbLog.AppendLine("Summarize Dictionary Results between Documents");
        //        // measure total time to parse document
        //        Stopwatch stopwSumDicDocs = new Stopwatch();
        //        stopwSumDicDocs.Start();

        //        string SumXRefPathFile = string.Empty;
        //        DataSet dsDicFilter;

        //        _DicAnalysis.Get_Documents_Dic_Summary(ProjectName, AnalysisName, Docs, Dictionary, DictionariesPath, out SumXRefPathFile, out dsDicFilter);

        //        double time = stopwSumDicDocs.Elapsed.TotalSeconds;
        //        string timeX = FormatTimeProcess(time);
        //        sbLog.AppendLine(string.Concat("Time: ", timeX));
        //    }


        //    //if (conceptsFound.Count > 0)
        //    //{
        //        sbLog.AppendLine(string.Empty);
        //        sbLog.AppendLine("Find Common Concepts between Documents");
        //        // measure total time to parse document
        //        Stopwatch stopConceptsDocs = new Stopwatch();
        //        stopConceptsDocs.Start();
 
              

        //            foreach (string doc in Docs)
        //            {

        //                // Setup Analysis Document Folders
        //                AppFolders_CA.AnalysisCurrentDocsDocName = doc;
        //                AnalysisDocPath = AppFolders_CA.AnalysisCurrentDocsDocName;
        //                if (AnalysisDocPath.Length == 0)
        //                {
        //                    sbLog.AppendLine(string.Concat("Error: ", AppFolders_CA.ErrorMessage));
        //                    sbErrors.AppendLine(string.Concat("Document Analysis - Document: ", doc, " - Folder Error: ", AppFolders_CA.ErrorMessage));
        //                    continue; // Go to the next Doc
        //                }

        //                AnalysisDocXMLPath = AppFolders_CA.AnalysisXML;
        //                if (AnalysisDocXMLPath.Length == 0)
        //                {
        //                    sbLog.AppendLine(string.Concat("Error: ", AppFolders_CA.ErrorMessage));
        //                    sbErrors.AppendLine(string.Concat("Document Analysis - Document: ", doc, " - Folder Error: ", AppFolders_CA.ErrorMessage));
        //                    continue; // Go to the next Doc
        //                }

        //                AnalysisDocConceptParseSegPath = AppFolders_CA.AnalysisParseSeg;
        //                if (AnalysisDocConceptParseSegPath.Length == 0)
        //                {
        //                    sbLog.AppendLine(string.Concat("Error: ", AppFolders_CA.ErrorMessage));
        //                    sbErrors.AppendLine(string.Concat("Document Analysis - Document: ", doc, " - Folder Error: ", AppFolders_CA.ErrorMessage));
        //                    continue; // Go to the next Doc
        //                }

        //                DocParsePagesPath = AppFolders_CA.DocParsePage;
        //                if (DocParsePagesPath.Length == 0)
        //                {
        //                    sbLog.AppendLine(string.Concat("Error: ", AppFolders_CA.ErrorMessage));
        //                    sbErrors.AppendLine(string.Concat("Document Analysis - Document: ", doc, " - Folder Error: ", AppFolders_CA.ErrorMessage));
        //                    continue; // Go to the next Doc
        //                }

                       

        //               // txtPathFile = _DocMgr.GetPathTxtFileName(ProjectName, doc, out DocInfoPath, out DocIndex2Path, out DocParsePagesPath, out DocParseSegPath, out DocXMLPath, out DocConceptParseSegPath);
        //                //if (txtPathFile.Length == 0)
        //                //{
        //                //    sbLog.AppendLine(string.Concat("Error: ", _DocMgr.ErrorMessage));
        //                //    sbErrors.AppendLine(string.Concat("Concept Analysis - Document: ", doc, " - File Error: ", _DocMgr.ErrorMessage));
        //                //}
        //                //else
        //                //{
        //                //  //  Files.CopyFiles(DocParseSegPath, DocConceptParseSegPath);

        //                //DataSet dsXFilter = new DataSet();
        //                //string xRefPath = string.Empty;

        //                //_AnalysisConceptsMgr.Get_Documents_Concept_Summary(ProjectName, AnalysisName, Docs, conceptsFound, out xRefPath, out dsXFilter); 

        //                string notUsed = _DocMgr.GetPathTxtFileName(ProjectName, doc, out DocInfoPath, out DocIndex2Path, out DocParsePagesPath, out DocParseSegPath, out DocXMLPath, out DocConceptParseSegPath);


        //                _AnalysisConceptsMgr.FindConceptsInSegments(conceptsFound, AnalysisDocXMLPath, DocParseSegPath, AnalysisDocConceptParseSegPath);
        //                //}

        //            }

        //            DataSet dsXFilter = new DataSet();
        //            string xRefPath = string.Empty;

        //            _AnalysisConceptsMgr.Get_Documents_Concept_Summary(ProjectName, AnalysisName, Docs, conceptsFound, out xRefPath, out dsXFilter); 

        //            stopConceptsDocs.Stop();
        //            double totalTime = stopwtotal.Elapsed.TotalSeconds;
        //            string totalTimeX = FormatTimeProcess(totalTime);
        //            sbLog.AppendLine(string.Concat("Total Common Concepts Found: ", txtPathFile.Length.ToString()));
        //            sbLog.AppendLine(string.Concat("Total Analysis Time: ", totalTimeX));
                
        //   // }


        //    sbLog.AppendLine(string.Empty);
        //    sbLog.AppendLine(string.Empty);
        //    stopwtotal.Stop();
        //    double totalCATime = stopwtotal.Elapsed.TotalSeconds;
        //    string totalCATimeX = FormatTimeProcess(totalCATime);
        //    sbLog.AppendLine(string.Concat("Total Analysis Time: ", totalCATimeX));
        //    sbLog.AppendLine("End - Analysis ------------------------------------");

        //    _AnalysisLogFile = Path.Combine(AnalysisPath, "Analysis.log");
        //    Files.WriteStringToFile(sbLog.ToString(), _AnalysisLogFile, true);

        //    _AnalysisErrorLogFile = string.Empty;
        //    if (sbErrors.ToString().Length > 0)
        //    {
        //        _AnalysisErrorLogFile = Path.Combine(AnalysisPath, "Analysis.err");
        //        Files.WriteStringToFile(sbErrors.ToString(), _AnalysisErrorLogFile, true);
        //    }

        //    return true;
        //}

        private string FormatTimeProcess(double time)
        {
            string timeX = string.Empty;
            if (time >= 60)
            {
                time = time / 60;
                timeX = string.Concat(time.ToString(), " min.s");
            }
            else
            {
                timeX = string.Concat(time.ToString(), " sec.s");
            }

            return timeX;
        }

        public string[] Get_ExportTemps_ConceptsDoc(out string tempsPath, string AppCARptTemps)
        {
            tempsPath = AppFolders_CA.AppDataPathToolsExcelTempDicDoc;

            string[] xmlFiles = Directory.GetFiles(tempsPath, "*.xml");

            if (xmlFiles.Length == 0) // ToDo get default from the installation location
            {
                string fileXML = "ConceptsDoc.xml";
                string pathFileXML = Path.Combine(AppCARptTemps, fileXML);
                string fileXLSX = "ConceptsDoc.xlsx";
                string pathFileXLSX = Path.Combine(AppCARptTemps, fileXLSX);

                string pathFileXML_Destination = Path.Combine(tempsPath, fileXML);
                string pathFileXLSX_Destination = Path.Combine(tempsPath, fileXLSX);

                if (File.Exists(pathFileXML) && File.Exists(pathFileXLSX))
                {
                    File.Copy(pathFileXML, pathFileXML_Destination);
                    File.Copy(pathFileXLSX, pathFileXLSX_Destination);
                }
            }

            return xmlFiles;
        }

        private bool CovertSegments2HTML(string RTFFilesPath, string HTMLFilesPath)
        {
            _ErrorMessage = string.Empty;

            // Convert Parsed RTF files into HTML files 
            AtebionRTFf2HTMLf.Convert convert = new AtebionRTFf2HTMLf.Convert();

            int qtyConverted = convert.ConvertFiles(RTFFilesPath, HTMLFilesPath);

            if (convert.ErrorMessage != string.Empty)
            {
                _ErrorMessage = convert.ErrorMessage;
                return false;
            }

            return false;
        }

        private string getTextHTML(string HTMLPath, string RTFFilesPath, string UID)
        {

            string[] htmlFiles = Directory.GetFiles(HTMLPath);
            if (htmlFiles.Length == 0)
            {
                if (!CovertSegments2HTML(RTFFilesPath, HTMLPath))
                {
                    return string.Empty;
                }

            }

            string returnHTML = string.Empty;

            string file = string.Concat(UID, ".html");

            returnHTML = Files.ReadFile(Path.Combine(HTMLPath, file));

            if (returnHTML.Length > 0) 
            {
                int StartBody = returnHTML.IndexOf("<body>") + 6;
                int EndBody = returnHTML.IndexOf("</body>") - StartBody;

                returnHTML = returnHTML.Substring(StartBody, EndBody);
            }

            return returnHTML;

        }

        private string getNotesText(string path, string uid)
        {
            if (!Directory.Exists(path))
                return string.Empty;

            string file = string.Concat(uid.Trim(), ".rtf");
            string pathFile = Path.Combine(path, file);

            if (File.Exists(pathFile))
            {
                
                return Files.ReadRTFFile(pathFile);
            }

            return string.Empty;

        }

        public bool ReportConceptsDocExists(string ReportName, string ProjectName, string DocumentName)
        {
            _ErrorMessage = string.Empty;

            AppFolders_CA.ProjectName = ProjectName;
            AppFolders_CA.DocName = DocumentName;

            string s = AppFolders_CA.AnalysisCurrentDocsDocName;
            string ExportPath = AppFolders_CA.AnalysisReports;
            

            string file = string.Concat(ReportName, ".xml");
            string pathFile = Path.Combine(ExportPath, file);

            return File.Exists(pathFile);

        }

        private DataTable CreateReportTable_ConceptsDoc()
        {
            DataTable table = new DataTable(ConceptsResultsFields.TableName);

            table.Columns.Add(ConceptsResultsFields.UID, typeof(string));
            table.Columns.Add(ConceptsResultsFields.SortOrder, typeof(int));
            table.Columns.Add(ConceptsResultsFields.Text, typeof(string));
            table.Columns.Add(ConceptsResultsFields.Number, typeof(string));
            table.Columns.Add(ConceptsResultsFields.Caption, typeof(string));
            table.Columns.Add(ConceptsResultsFields.PageSource, typeof(string));
            table.Columns.Add(ConceptsResultsFields.ConceptsWords, typeof(string));
            table.Columns.Add(ConceptsResultsFields.Notes, typeof(string));
           
            return table;
        }


        private DataTable MapData2Rpt_ConceptsDoc(DataTable dt, string TemplateName, string TempatePath, string HTMLPath, string RTFPath)
        {
  
            DataTable dtReport = CreateReportTable_ConceptsDoc();


            string docPath = Directory.GetParent(RTFPath).FullName;
            string docNotesPath = Path.Combine(docPath, "Notes");
           

            int i = 0;
            string htmlText = string.Empty;
            string uid = string.Empty;
            foreach (DataRow row in dt.Rows)
            {
                uid = row[ReportConceptsDocFields.UID].ToString();
                htmlText = getTextHTML(HTMLPath, RTFPath, uid); // Content HTML between "<body>" and "</body>" tags

                DataRow reportRow = dtReport.NewRow();
                reportRow[ConceptsResultsFields.UID] = Convert.ToInt32(uid);
                reportRow[ConceptsResultsFields.SortOrder] = i;
                reportRow[ConceptsResultsFields.Text] = htmlText.Trim();
                reportRow[ConceptsResultsFields.Number] = row[ReportConceptsDocFields.Number].ToString();
                reportRow[ConceptsResultsFields.Caption] = row[ReportConceptsDocFields.Caption].ToString();
                reportRow[ConceptsResultsFields.PageSource] = row["Page"].ToString();
                reportRow[ConceptsResultsFields.ConceptsWords] = row[ReportConceptsDocFields.Concepts].ToString();
                reportRow[ConceptsResultsFields.Notes] = getNotesText(docNotesPath, uid); // Test

                dtReport.Rows.Add(reportRow);
                dtReport.AcceptChanges();

                i++;
            }

            return dtReport;

        }

 

        public bool ExportConceptsDoc(DataTable dt, string TemplateName, string TempatePath, string resultFileName, string ProjectName, string DocumentName)
        {
            _ErrorMessage = string.Empty;

            ExcelOutput excelOutput = new ExcelOutput();

            AppFolders_CA.ProjectName = ProjectName;
            AppFolders_CA.DocName = DocumentName;

            string s = AppFolders_CA.AnalysisCurrentDocsDocName;
            string ExportPath = AppFolders_CA.AnalysisReports;
            string HTMLPath = AppFolders_CA.AnalysisHTML;
            string RTFPath = AppFolders_CA.AnalysisParseSeg;

            DataTable dtReport = MapData2Rpt_ConceptsDoc(dt, TemplateName, TempatePath, HTMLPath, RTFPath);

            excelOutput.Metadata_DocName = DocumentName;
            excelOutput.Metadata_ProjectName = ProjectName;

            if (excelOutput.ExportConceptsDoc(dtReport, TemplateName, TempatePath, ExportPath, resultFileName))
            {
                return true;
            }
            else
            {
                _ErrorMessage = excelOutput.ErrorMessage;

                return false;
            }
        }

        public string GetNext_ExportTemps_ConceptsDoc_ReportName(string ProjectName, string DocumentName, out string ReportPath)
        {
            string nextReportName = string.Empty;

            AppFolders_CA.ProjectName = ProjectName;
            AppFolders_CA.DocName = DocumentName;

            string s = AppFolders_CA.AnalysisCurrentDocsDocName;
            ReportPath = AppFolders_CA.AnalysisReports; 

            string fileName = string.Empty;
            string pathFile = string.Empty;
            for (int i = 0; i < 32000; i++)
            {
                fileName = string.Concat("RptConceptsDoc_", i.ToString(), ".xlsx");
                pathFile = Path.Combine(ReportPath, fileName);

                if (!File.Exists(pathFile))
                {
                    nextReportName = Files.GetFileNameWOExt(pathFile);
                    return nextReportName;
                }
            }

            return string.Empty;
        }


        private DataTable ConceptsDocs_AddNotes(DataTable dt, string selectedAnalysisPath)
        {
            if (!dt.Columns.Contains(ConceptsResultsFields.Notes))
            {
                dt.Columns.Add(ConceptsResultsFields.Notes, typeof(string));          
            }

            if (dt.Rows.Count == 0)
            {
                return dt;
            }

            string uid = string.Empty;
            string file = string.Empty;
            string pathFile = string.Empty;
            string txt = string.Empty;
            foreach (DataRow row in dt.Rows)
            {
                uid = row[ConceptsResultsFields.UID].ToString();
                file = string.Concat("C_", uid, ".rtf");
                pathFile = Path.Combine(selectedAnalysisPath, file);
                if (File.Exists(pathFile))
                {
                    txt = Files.ReadRTFFile(pathFile);
                    row[ConceptsResultsFields.Notes] = txt;
                }
                else
                {
                    row[ConceptsResultsFields.Notes] = string.Empty;
                }


                dt.AcceptChanges();

            }

            return dt;

        }

        private DataTable DicDocs_AddNotes(DataTable dt, string selectedAnalysisPath)
        {
            if (!dt.Columns.Contains(ConceptsResultsFields.Notes))
            {
                dt.Columns.Add(ConceptsResultsFields.Notes, typeof(string));
            }

            if (dt.Rows.Count == 0)
            {
                return dt;
            }

            string uid = string.Empty;
            string file = string.Empty;
            string pathFile = string.Empty;
            string txt = string.Empty;
            foreach (DataRow row in dt.Rows)
            {
                uid = row[ConceptsResultsFields.UID].ToString();
                file = string.Concat("D_", uid, ".rtf");
                pathFile = Path.Combine(selectedAnalysisPath, file);
                if (File.Exists(pathFile))
                {
                    txt = Files.ReadRTFFile(pathFile);
                    row[ConceptsResultsFields.Notes] = txt;
                }
                else
                {
                    row[ConceptsResultsFields.Notes] = string.Empty;
                }


                dt.AcceptChanges();

            }

            return dt;

        }

        public bool ExportConceptsDocs(DataTable dt, string[] Docs, string TemplateName, string TempatePath, string resultFileName, string ProjectName, string AnalysisName, bool UseColor)
        {
            _ErrorMessage = string.Empty;

            ExcelOutput excelOutput = new ExcelOutput();

            AppFolders_CA.ProjectName = ProjectName;
            AppFolders_CA.AnalysisName = AnalysisName;

            string s = AppFolders_CA.ProjectCurrent;
            s = AppFolders_CA.AnalysisCurrent;
            string selectedAnalysisPath = AppFolders_CA.AnalysisCurrent;
          //  s = AppFolders_CA.AnalysisReports;
            
            string ExportPath = AppFolders_CA.AnalysisReports;

            DataTable dtWithNotes = new DataTable();
            DataView dv = dt.DefaultView;
            dtWithNotes = dv.ToTable();
            dtWithNotes = ConceptsDocs_AddNotes(dtWithNotes, selectedAnalysisPath);

            excelOutput.Metadata_AnalysisName = AnalysisName;
            excelOutput.Metadata_ProjectName = ProjectName;

            if (excelOutput.ExportConceptsDocs(dtWithNotes, Docs, TemplateName, TempatePath, ExportPath, resultFileName, UseColor))
            {
                return true;
            }
            else
            {
                _ErrorMessage = excelOutput.ErrorMessage;

                return false;
            }
        }

        public string[] Get_ExportTemps_ConceptsDocs(out string tempsPath, string AppCARptTemps)
        {
            tempsPath = AppFolders_CA.AppDataPathToolsExcelTempConceptsDocs;

            string[] xmlFiles = Directory.GetFiles(tempsPath, "*.xml");

            if (xmlFiles.Length == 0) // ToDo get default from the installation location
            {
                string fileXML = "ConceptsDocs.xml";
                string pathFileXML = Path.Combine(AppCARptTemps, fileXML);
                string fileXLSX = "ConceptsDocs.xlsx";
                string pathFileXLSX = Path.Combine(AppCARptTemps, fileXLSX);

                string pathFileXML_Destination = Path.Combine(tempsPath, fileXML);
                string pathFileXLSX_Destination = Path.Combine(tempsPath, fileXLSX);

                if (File.Exists(pathFileXML) && File.Exists(pathFileXLSX))
                {
                    File.Copy(pathFileXML, pathFileXML_Destination);
                    File.Copy(pathFileXLSX, pathFileXLSX_Destination);
                }
            }

            return xmlFiles;
        }

        public bool ReportConceptsDocsExists(string ReportName, string ProjectName, string AnalysisName)
        {
            _ErrorMessage = string.Empty;

            AppFolders_CA.ProjectName = ProjectName;
            AppFolders_CA.AnalysisName = AnalysisName;

            string s = AppFolders_CA.ProjectCurrent;
            s = AppFolders_CA.AnalysisCurrent;
            s = AppFolders_CA.AnalysisCurrent;
            s = AppFolders_CA.AnalysisReports;

            string ExportPath = AppFolders_CA.AnalysisReports;

            string file = string.Concat(ReportName, ".xml");
            string pathFile = Path.Combine(ExportPath, file);

            return File.Exists(pathFile);

        }

        public string GetNext_ExportTemps_ConceptsDocs_ReportName(string ProjectName, string AnalysisName, out string ReportPath)
        {
            AppFolders_CA.ProjectName = ProjectName;
            AppFolders_CA.AnalysisName = AnalysisName;


            string s = AppFolders_CA.AnalysisCurrentDocsDocName;
            s = AppFolders_CA.AnalysisCurrent;
            s = AppFolders_CA.AnalysisReports;

            ReportPath = AppFolders_CA.AnalysisReports;

            string fileName = string.Empty;
            string pathFile = string.Empty;
            string nextReportName = string.Empty;
            for (int i = 0; i < 32000; i++)
            {
                fileName = string.Concat("RptConceptsDocs_", i.ToString(), ".xlsx");
                pathFile = Path.Combine(ReportPath, fileName);

                if (!File.Exists(pathFile))
                {
                    nextReportName = Files.GetFileNameWOExt(pathFile);
                    return nextReportName;
                }
            }

            return string.Empty;
        }

        private DataTable CreateReportTable_DicDoc()
        {
            DataTable table = new DataTable(ReportDicDocFields.TableName);

            table.Columns.Add(ReportDicDocFields.UID, typeof(string));
          //  table.Columns.Add(ReportDicDocFields.SortOrder, typeof(int));
            table.Columns.Add(ReportDicDocFields.Text, typeof(string));
            table.Columns.Add(ReportDicDocFields.Number, typeof(string));
            table.Columns.Add(ReportDicDocFields.Caption, typeof(string));
            table.Columns.Add(ReportDicDocFields.PageNo, typeof(string));
            table.Columns.Add(ReportDicDocFields.DicItems, typeof(string));
            table.Columns.Add(ReportDicDocFields.DicDefs, typeof(string));
            table.Columns.Add(ReportDicDocFields.WeightDoc, typeof(string));
            table.Columns.Add(ReportDicDocFields.Notes, typeof(string));
           

            return table;
        }

        private DataTable MapData2Rpt_DicDoc(DataTable dt, string TemplateName, string TempatePath, string HTMLPath, string RTFPath, string NotesPath)
        {

            DataTable dtReport = CreateReportTable_DicDoc();

            int i = 0;
            string htmlText = string.Empty;
            string uid = string.Empty;
            foreach (DataRow row in dt.Rows)
            {  
                uid = row[ParseResultsFields.UID].ToString();
                htmlText = getTextHTML(HTMLPath, RTFPath, uid); // Content HTML between "<body>" and "</body>" tags

                if (htmlText.Length == 0)
                {
                    htmlText = getTextHTML(HTMLPath, RTFPath, uid); // Content HTML between "<body>" and "</body>" tags
                }

                DataRow reportRow = dtReport.NewRow();
                reportRow[ReportDicDocFields.UID] = Convert.ToInt32(uid);
              //  reportRow[ReportDicDocFields.SortOrder] = i;
                reportRow[ReportDicDocFields.Text] = htmlText.TrimEnd('\r', '\n'); // Remove carriage return;
                reportRow[ReportDicDocFields.Number] = row[ParseResultsFields.Number].ToString();
                reportRow[ReportDicDocFields.Caption] = row[ParseResultsFields.Caption].ToString();

                if (dt.Columns.Contains(ParseResultsFields.PageSource))
                    reportRow[ReportDicDocFields.PageNo] = row[ParseResultsFields.PageSource].ToString();

                reportRow[ReportDicDocFields.DicItems] = row[ParseResultsFields.DictionaryItems].ToString().TrimEnd('\r', '\n'); // Remove carriage return
                reportRow[ReportDicDocFields.DicDefs] = row[ParseResultsFields.DictionaryDefinitions].ToString().TrimEnd('\r', '\n'); // Remove carriage return
                reportRow[ReportDicDocFields.WeightDoc] = row[ParseResultsFields.Weight].ToString();
                reportRow[ReportDicDocFields.Notes] = getNotesText(NotesPath, uid);

                dtReport.Rows.Add(reportRow);
                dtReport.AcceptChanges();

                i++;
            }

            return dtReport;

        }

        public bool ExportDicDocRAM(DataTable dt, DataTable dtDicAnalysis, string TemplateName, string TempatePath, string RAMDictionaryPath, string resultFileName, string ProjectName, string AnalysisName, string DocumentName, bool UseColor)
        {
            _ErrorMessage = string.Empty;

            ExcelOutput excelOutput = new ExcelOutput();

            AppFolders_CA.ProjectName = ProjectName;
            AppFolders_CA.AnalysisName = AnalysisName;
            AppFolders_CA.DocName = DocumentName;

            string s = AppFolders_CA.AnalysisCurrentDocsDocName;
            s = AppFolders_CA.AnalysisCurrent;
            s = AppFolders_CA.AnalysisCurrentDocsDocName;
            //    s = AppFolders_CA.AnalysisCurrentDocsDocName_Reports;
            string analysisResultsPath = AppFolders_CA.AnalysisParseSeg;
            string htmlPath = AppFolders_CA.AnalysisHTML;
            string ExportPath = AppFolders_CA.AnalysisReports; //
            string notesPath = AppFolders_CA.AnalysisNotes;
            string infoPath = AppFolders_CA.AnalysisInfor;

            DataTable dtReport = MapData2Rpt_DicDoc(dt, TemplateName, TempatePath, htmlPath, analysisResultsPath, notesPath);

            excelOutput.Metadata_DocName = DocumentName;
            excelOutput.Metadata_ProjectName = ProjectName;
            excelOutput.Metadata_AnalysisName = AnalysisName;

            string templateFile = string.Concat(TemplateName, ".xml");
            string pathFileTemplateXML = Path.Combine(TempatePath, templateFile);
            if (!File.Exists(pathFileTemplateXML))
            {
                _ErrorMessage = string.Concat("Unable to find RAM Template configuration file: ", pathFileTemplateXML);
                return false;
            }

            GenericDataManger gDMgr = new GenericDataManger();

            DataSet dsRAMDef = gDMgr.LoadDatasetFromXml(pathFileTemplateXML);

            if (dsRAMDef == null)
            {
                _ErrorMessage = string.Concat("Unable to open RAM Template configuration file: ", pathFileTemplateXML);
                return false;
            }

            string modelName = string.Empty;
            List<string> RAMInfo = new List<string>();

            ResponsAssigMatrix RAM_Mgr = new ResponsAssigMatrix();
            string RMDictionaryName = RAM_Mgr.Get_DictionaryName_FromRAMTemplate(dsRAMDef, out modelName);
            if (RMDictionaryName.Length == 0)
            {
                _ErrorMessage = string.Concat("Unable to find Dictionary Name in the RAM Template configuration file: ", pathFileTemplateXML);
                return false;
            }

           // if (excelOutput.ExportDicDoc(dtReport, TemplateName, TempatePath, ExportPath, resultFileName, UseColor))
            if (excelOutput.ExportDicDocRAM(dtReport, dtDicAnalysis, dsRAMDef, RAMDictionaryPath, RMDictionaryName, TemplateName, TempatePath, ExportPath, resultFileName, UseColor))
            {
                RAMInfo.Add(string.Concat("TemplateName=", TemplateName));
                RAMInfo.Add(string.Concat("DictionaryName=", RMDictionaryName));
                RAMInfo.Add(string.Concat("ModelName=", modelName));
                RAMInfo.Add(string.Concat("GeneratedBy=", AppFolders_CA.UserName));
                RAMInfo.Add(string.Concat("Date=", DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")));

                string infoFile = "RAMInfor.txt";
                string infoPathFile = Path.Combine(infoPath, infoFile);

                Files.WriteStringToFile(RAMInfo.ToArray(), infoPathFile);

                return true;
            }
            else
            {
                _ErrorMessage = excelOutput.ErrorMessage;

                return false;
            }

        }


        public bool ExportDicDoc(DataTable dt, string TemplateName, string TempatePath, string resultFileName, string ProjectName, string AnalysisName, string DocumentName, bool UseColor)
        {
            _ErrorMessage = string.Empty;

            ExcelOutput excelOutput = new ExcelOutput();

            AppFolders_CA.ProjectName = ProjectName;
            AppFolders_CA.AnalysisName = AnalysisName;
            AppFolders_CA.DocName = DocumentName;

            string s = AppFolders_CA.AnalysisCurrentDocsDocName;
            s = AppFolders_CA.AnalysisCurrent;
            s = AppFolders_CA.AnalysisCurrentDocsDocName;
        //    s = AppFolders_CA.AnalysisCurrentDocsDocName_Reports;
            string analysisResultsPath = AppFolders_CA.AnalysisParseSeg;
            string htmlPath = AppFolders_CA.AnalysisHTML;
            string ExportPath = AppFolders_CA.AnalysisReports; //
            string notesPath = AppFolders_CA.AnalysisNotes;


            DataTable dtReport = MapData2Rpt_DicDoc(dt, TemplateName, TempatePath, htmlPath, analysisResultsPath, notesPath);

            excelOutput.Metadata_DocName = DocumentName;
            excelOutput.Metadata_ProjectName = ProjectName;
            excelOutput.Metadata_AnalysisName = AnalysisName;

            if (excelOutput.ExportDicDoc(dtReport, TemplateName, TempatePath, ExportPath, resultFileName, UseColor))
            {
                return true;
            }
            else
            {
                _ErrorMessage = excelOutput.ErrorMessage;

                return false;
            }
        }

        public string[] Get_ExportTemps_DicDoc(out string tempsPath, string AppCARptTemps)
        {
            tempsPath = AppFolders_CA.AppDataPathToolsExcelTempDicDoc;

            string[] xmlFiles = Directory.GetFiles(tempsPath, "*.xml");

            if (xmlFiles.Length == 0) // ToDo get default from the installation location
            {
                string fileXML = "DicDoc.xml";
                string pathFileXML = Path.Combine(AppCARptTemps, fileXML);
                string fileXLSX = "DicDoc.xlsx";
                string pathFileXLSX = Path.Combine(AppCARptTemps, fileXLSX);

                string pathFileXML_Destination = Path.Combine(tempsPath, fileXML);
                string pathFileXLSX_Destination = Path.Combine(tempsPath, fileXLSX);

                if (File.Exists(pathFileXML) && File.Exists(pathFileXLSX))
                {
                    File.Copy(pathFileXML, pathFileXML_Destination);
                    File.Copy(pathFileXLSX, pathFileXLSX_Destination);
                }
            }

            return xmlFiles;
        }

        public string GetNext_ExportTemps_DicDoc_ReportName(string ProjectName, string AnalysisName, string DocumentName, out string ReportPath)
        {
            AppFolders_CA.ProjectName = ProjectName;
            AppFolders_CA.AnalysisName = AnalysisName;
            AppFolders_CA.DocName = DocumentName;


            ReportPath = AppFolders_CA.AnalysisCurrentCompareDocsReport;
           

            string fileName = string.Empty;
            string pathFile = string.Empty;
            string nextReportName = string.Empty;
            for (int i = 0; i < 32000; i++)
            {
                fileName = string.Concat("RptDicDoc_", i.ToString(), ".xlsx");
                pathFile = Path.Combine(ReportPath, fileName);

                if (!File.Exists(pathFile))
                {
                    nextReportName = Files.GetFileNameWOExt(pathFile);
                    return nextReportName;
                }
                    
            }

            return string.Empty;
        }

        public bool ReportDicsDocExists(string ReportName, string ProjectName, string AnalysisName, string DocumentName)
        {
            _ErrorMessage = string.Empty;

            AppFolders_CA.ProjectName = ProjectName;
            AppFolders_CA.AnalysisName = AnalysisName;
            AppFolders_CA.DocName = DocumentName;

            string s = AppFolders_CA.AnalysisCurrentDocsDocName;
            s = AppFolders_CA.AnalysisCurrent;
            s = AppFolders_CA.AnalysisCurrentDocsDocName;
          //  s = AppFolders_CA.AnalysisCurrentDocsDocName_Reports;
            string ExportPath = AppFolders_CA.AnalysisCurrentCompareDocsReport;

            string file = string.Concat(ReportName, ".xml");
            string pathFile = Path.Combine(ExportPath, file);

            return File.Exists(pathFile);

        }


        public bool ExportDicDocs(DataTable dt, DataTable dtTotals, string[] Docs, string TemplateName, string TempatePath, string resultFileName, string ProjectName, string AnalysisName, bool UseColor)
        {
            _ErrorMessage = string.Empty;

            ExcelOutput excelOutput = new ExcelOutput();
            AppFolders_CA.ProjectName = ProjectName;
            AppFolders_CA.AnalysisName = AnalysisName;

            string s = AppFolders_CA.ProjectCurrent;
            s = AppFolders_CA.AnalysisCurrent;
            string selectedAnalysisPath = AppFolders_CA.AnalysisCurrent;
            s = AppFolders_CA.AnalysisReports;

            string ExportPath = AppFolders_CA.AnalysisCurrentCompareDocsReport;

            DataTable dtWithNotes = new DataTable();
            DataView dv = dt.DefaultView;
            dtWithNotes = dv.ToTable();
            dtWithNotes = DicDocs_AddNotes(dtWithNotes, selectedAnalysisPath);

            excelOutput.Metadata_AnalysisName = AnalysisName;
            excelOutput.Metadata_ProjectName = ProjectName;

            if (excelOutput.ExportDicDocs(dtWithNotes, dtTotals, Docs, TemplateName, TempatePath, ExportPath, resultFileName, UseColor))
            {
                return true;
            }
            else
            {
                _ErrorMessage = excelOutput.ErrorMessage;

                return false;
            }
        }

        public string[] Get_ExportTemps_DicDocs(out string tempsPath, string AppCARptTemps)
        {
            tempsPath = AppFolders_CA.AppDataPathToolsExcelTempDicDocs;

            string[] xmlFiles = Directory.GetFiles(tempsPath, "*.xml");

            if (xmlFiles.Length == 0) // ToDo get default from the installation location
            {
                string fileXML = "DicDocs.xml";
                string pathFileXML = Path.Combine(AppCARptTemps, fileXML);
                string fileXLSX = "DicDocs.xlsx";
                string pathFileXLSX = Path.Combine(AppCARptTemps, fileXLSX);

                string pathFileXML_Destination = Path.Combine(tempsPath, fileXML);
                string pathFileXLSX_Destination = Path.Combine(tempsPath, fileXLSX);

                if (File.Exists(pathFileXML) && File.Exists(pathFileXLSX))
                {
                    File.Copy(pathFileXML, pathFileXML_Destination);
                    File.Copy(pathFileXLSX, pathFileXLSX_Destination);
                }
            }
            return xmlFiles;
        }

        public bool ReportDicsDocsExists(string ReportName, string ProjectName, string AnalysisName)
        {
            _ErrorMessage = string.Empty;

            AppFolders_CA.ProjectName = ProjectName;
            AppFolders_CA.AnalysisName = AnalysisName;

            string s = AppFolders_CA.ProjectCurrent;
            s = AppFolders_CA.AnalysisCurrent;
            s = AppFolders_CA.AnalysisCurrent;
            s = AppFolders_CA.AnalysisReports;

            string ExportPath = AppFolders_CA.AnalysisCurrentCompareDocsReport;

            string file = string.Concat(ReportName, ".xml");
            string pathFile = Path.Combine(ExportPath, file);

            return File.Exists(pathFile);

        }

        public string GetNext_ExportTemps_DicDocs_ReportName(string ProjectName, string AnalysisName, out string ReportPath)
        {
            AppFolders_CA.ProjectName = ProjectName;
            AppFolders_CA.AnalysisName = AnalysisName;
            


            string s = AppFolders_CA.AnalysisCurrentDocsDocName;
            s = AppFolders_CA.AnalysisCurrent;
            s = AppFolders_CA.AnalysisReports;
            ReportPath = AppFolders_CA.AnalysisCurrentCompareDocsReport;


            string fileName = string.Empty;
            string pathFile = string.Empty;
            string nextReportName = string.Empty;
            for (int i = 0; i < 32000; i++)
            {
                fileName = string.Concat("RptDicDocs_", i.ToString(), ".xlsx");
                pathFile = Path.Combine(ReportPath, fileName);

                if (!File.Exists(pathFile))
                {
                    nextReportName = Files.GetFileNameWOExt(pathFile);
                    return nextReportName;
                }
            }

            return string.Empty;
        }



        public string Get_Document_Dic_ItemsNotFound(string ProjectName, string AnalysisName, string DocumentName)
        {
            _ErrorMessage = string.Empty;

            if (_DicAnalysis == null)
                _DicAnalysis = new AnalysisDictionaries();

            string notFound = _DicAnalysis.Get_Document_Dic_ItemsNotFound(ProjectName, AnalysisName, DocumentName);

            _ErrorMessage = _DicAnalysis.ErrorMessage;

            return notFound;         
        }

        public DataSet Get_Document_Dic_AnalysisResults(string ProjectName, string AnalysisName, string DocumentName, out string AnalysisResultPath, out string AnalysisResultsNotesPath)
        {
            _ErrorMessage = string.Empty;

            if (_DicAnalysis == null)
                _DicAnalysis = new AnalysisDictionaries();

            DataSet ds = _DicAnalysis.Get_Document_Dic_AnalysisResults(ProjectName, AnalysisName, DocumentName, out AnalysisResultPath, out AnalysisResultsNotesPath);

            _ErrorMessage = _DicAnalysis.ErrorMessage;

            return ds;

        }

        public DataSet Get_Document_Concept_AnalysisResults(string ProjectName, string AnalysisName, string DocumentName, out string ConceptParseSegPath, out string ProjectResultsNotesPath)
        {
            _ErrorMessage = string.Empty;

            DataSet ds = _AnalysisConceptsMgr.Get_Document_Concept_AnalysisResults(ProjectName, AnalysisName, DocumentName, out ConceptParseSegPath, out ProjectResultsNotesPath);

            _ErrorMessage = _AnalysisConceptsMgr.ErrorMessage;

            return ds;

        }

        public DataSet Get_Document_Emails(string ProjectName, string AnalysisName, string DocumentName, string TxtFilePath, out string DocParseSegPath)
        {
            _ErrorMessage = string.Empty;

            string DocInfoPath = string.Empty;
            string DocIndex2Path = string.Empty;
            string DocParsePagesPath = string.Empty;
           // string DocParseSegPath = string.Empty;
            string DocXMLPath = string.Empty;
            string DocConceptParseSegPath = string.Empty;

            

            if (_DocMgr == null)
                _DocMgr = new Documents();

            string txtPathFile = _DocMgr.GetPathTxtFileName(ProjectName, AnalysisName, DocumentName, TxtFilePath, out DocInfoPath, out DocIndex2Path, out DocParsePagesPath, out DocParseSegPath, out DocXMLPath, out DocConceptParseSegPath);

            _ErrorMessage = _DocMgr.ErrorMessage;

            if (DocXMLPath == string.Empty)
                return null;

            string pathFile = Path.Combine(DocXMLPath, DiscreteContentFields.XMLFile_Emails);
            if (!File.Exists(pathFile))
            {
                _ErrorMessage = string.Concat("Unable to find Emails data file: ", pathFile);
                return null;
            }

            DataSet ds = Files.LoadDatasetFromXml(pathFile);

            return ds;

        }

        public DataSet Get_Document_Dates(string ProjectName, string AnalysisName, string DocumentName, string TxtFilePath, out string DocParseSegPath)
        {
            _ErrorMessage = string.Empty;

            string DocInfoPath = string.Empty;
            string DocIndex2Path = string.Empty;
            string DocParsePagesPath = string.Empty;
          //  string DocParseSegPath = string.Empty;
            string DocXMLPath = string.Empty;
            string DocConceptParseSegPath = string.Empty;

            if (_DocMgr == null)
                _DocMgr = new Documents();

            string txtPathFile = _DocMgr.GetPathTxtFileName(ProjectName, AnalysisName, DocumentName, TxtFilePath, out DocInfoPath, out DocIndex2Path, out DocParsePagesPath, out DocParseSegPath, out DocXMLPath, out DocConceptParseSegPath);

            _ErrorMessage = _DocMgr.ErrorMessage;

            if (DocXMLPath == string.Empty)
                return null;

            string pathFile = Path.Combine(DocXMLPath, DiscreteContentFields.XMLFile_Dates);
            if (!File.Exists(pathFile))
            {
                _ErrorMessage = string.Concat("Unable to find Dates data file: ", pathFile);
                return null;
            }

            DataSet ds = Files.LoadDatasetFromXml(pathFile);

            return ds;

        }

        public DataSet Get_Document_URLs(string ProjectName, string AnalysisName, string DocumentName, string TxtFilePath, out string DocParseSegPath)
        {
            _ErrorMessage = string.Empty;

            string DocInfoPath = string.Empty;
            string DocIndex2Path = string.Empty;
            string DocParsePagesPath = string.Empty;
          //  string DocParseSegPath = string.Empty;
            string DocXMLPath = string.Empty;
            string DocConceptParseSegPath = string.Empty;

            if (_DocMgr == null)
                _DocMgr = new Documents();

            string txtPathFile = _DocMgr.GetPathTxtFileName(ProjectName, AnalysisName, DocumentName, TxtFilePath, out DocInfoPath, out DocIndex2Path, out DocParsePagesPath, out DocParseSegPath, out DocXMLPath, out DocConceptParseSegPath);

            _ErrorMessage = _DocMgr.ErrorMessage;

            if (DocXMLPath == string.Empty)
                return null;

            string pathFile = Path.Combine(DocXMLPath, DiscreteContentFields.XMLFile_URLs);
            if (!File.Exists(pathFile))
            {
                _ErrorMessage = string.Concat("Unable to find Dates data file: ", pathFile);
                return null;
            }

            DataSet ds = Files.LoadDatasetFromXml(pathFile);

            return ds;

        }



        public string ProjectFileDetails(string ProjectName, string FileName, bool isDAProject, out string FileNameWExt, out bool Parsed, out string ext, out string DocPath)
        {
            _ErrorMessage = string.Empty;

            string fileDetails = _ProjectMgr.ProjectFileDetails(ProjectName, FileName, isDAProject, out FileNameWExt, out Parsed, out ext, out DocPath);

            return fileDetails;

        }
        public string[] GetProjectFiles(string ProjectName, bool isDAProject)
        {
            _ErrorMessage = string.Empty;

            string[] docs = _ProjectMgr.GetProjectFiles(ProjectName, isDAProject);

            if (docs == null)
            {
                _ErrorMessage = _ProjectMgr.ErrorMessage;
            }

            return docs;

        }

        public string[] GetAnalsysFiles(string ProjectName, string AnalysisName, bool isDAProject)
        {
            _ErrorMessage = string.Empty;

            AppFolders_CA.ProjectName = ProjectName;
            string projectFolder = AppFolders_CA.ProjectCurrent;
            string s = AppFolders_CA.AnalysisCurrent;
            AppFolders_CA.AnalysisName = AnalysisName;
            string AnalysisFolder = AppFolders_CA.AnalysisCurrent;

            if (AnalysisFolder == string.Empty)
            {
                _ErrorMessage = AppFolders_CA.ErrorMessage;
                return null;
            }


            string[] docs = Directory.GetDirectories(AppFolders_CA.AnalysisCurrentDocs);

            if (docs == null)
            {
                _ErrorMessage = string.Concat("Unable to find Documents for ", AnalysisName);
            }

            List<string> dirX = new List<string>(docs);
            int i = 0;
            string lastFolder = string.Empty;
            foreach (string dir in dirX)
            {
                lastFolder = new DirectoryInfo(dir).Name;
                if (lastFolder == "Reports")
                {
                    dirX.RemoveAt(i);
                    break;
                }

                i++;
            }

            List<string> docsX = new List<string>();
            foreach (string dir in dirX)
            {
                lastFolder = new DirectoryInfo(dir).Name;
                docsX.Add(lastFolder);
            }

            return docsX.ToArray();
        }

        public string GetAnalysisLog(string ProjectName, string AnalysisName, bool isDAProject)
        {
            _ErrorMessage = string.Empty;

            AppFolders_CA.ProjectName = ProjectName;
            string projectFolder = AppFolders_CA.ProjectCurrent;
            string s = AppFolders_CA.AnalysisCurrent;
            AppFolders_CA.AnalysisName = AnalysisName;
            string AnalysisFolder = AppFolders_CA.AnalysisCurrent;
            

            if (AnalysisFolder == string.Empty)
            {
                _ErrorMessage = AppFolders_CA.ErrorMessage;
                return string.Empty;
            }

            string analysisPathLogFile = Path.Combine(AnalysisFolder, "Analysis.log");
            if (!File.Exists(analysisPathLogFile))
            {
                _ErrorMessage = string.Concat("Analysis Log file was not found.  File: ", analysisPathLogFile);
                return string.Empty;
            }

            string logText = Files.ReadFile(analysisPathLogFile);

            return logText;
        }

        public DataRow GetAnalysisParameters(string ProjectName, string AnalysisName, bool isDAProject, out string AnalysisFolder)
        {
            _ErrorMessage = string.Empty;

            AppFolders_CA.ProjectName = ProjectName;
            string projectFolder = AppFolders_CA.ProjectCurrent;
            string s = AppFolders_CA.AnalysisCurrent;
            AppFolders_CA.AnalysisName = AnalysisName;
            AnalysisFolder = AppFolders_CA.AnalysisCurrent;
            

            if (AnalysisFolder == string.Empty)
            {
                _ErrorMessage = AppFolders_CA.ErrorMessage;
                return null;
            }

            string analysisPatXMLFile = Path.Combine(AnalysisFolder, AnalysisUCaseFieldConst.XMLAnalysisFile);
            if (!File.Exists(analysisPatXMLFile))
            {
                _ErrorMessage = string.Concat("Unable to locate Analysis Parameter file: ", analysisPatXMLFile);
                return null;
            }

            DataSet ds = Files.LoadDatasetFromXml(analysisPatXMLFile);
            if (ds == null)
            {
                _ErrorMessage = string.Concat("Unable to read the Analysis Parameter file: ", analysisPatXMLFile);
                return null;
            }

            if (ds.Tables[0].Rows.Count == 0)
            {
                _ErrorMessage = string.Concat("The Analysis Parameter file contains no data. - File: ", analysisPatXMLFile);
                return null;
            }


            return ds.Tables[0].Rows[0];

        }

        public string GetAnalysisError(string ProjectName, string AnalysisName, bool isDAProject)
        {
            _ErrorMessage = string.Empty;

            AppFolders_CA.ProjectName = ProjectName;
            string projectFolder = AppFolders_CA.ProjectCurrent;
            string s = AppFolders_CA.AnalysisCurrent;
            AppFolders_CA.AnalysisName = AnalysisName;
            string AnalysisFolder = AppFolders_CA.AnalysisCurrent;
            

            if (AnalysisFolder == string.Empty)
            {
                _ErrorMessage = AppFolders_CA.ErrorMessage;
                return string.Empty;
            }

            string analysisPathErrFile = Path.Combine(AnalysisFolder, "Analysis.err");
            if (!File.Exists(analysisPathErrFile))
            {
                return string.Empty;
            }

            string errText = Files.ReadFile(analysisPathErrFile);

            return errText;
        }

        public string[] GetProjects(bool isDAProject)
        {
            string[] projects;

            _ErrorMessage = string.Empty;

            projects = _ProjectMgr.GetProjects();

            if (projects == null)
            {
                _ErrorMessage = _ProjectMgr.ErrorMessage;
            }

            return projects;

        }

        public string GetProject_Description(string ProjectName)
        {
            string description = _ProjectMgr.GetProjectDescription(ProjectName);

            if (description.Length > 0)
            {
                _ErrorMessage = _ProjectMgr.ErrorMessage;
            }

            return description;

        }

        public string GetSummaryPathFile(string ProjectName, string DocumentName)
        {
            _ErrorMessage = string.Empty;

            if (_DocMgr == null)
            _DocMgr = new Documents();

            string pathFile = _DocMgr.GetSummaryPathFile(ProjectName, DocumentName);

            _ErrorMessage = _DocMgr.ErrorMessage;

            return pathFile;

        }

        public string GetSearchIndexPath(string ProjectName, string DocumentName)
        {
            _ErrorMessage = string.Empty;

            if (_DocMgr == null)
                _DocMgr = new Documents();

            string pathFile = _DocMgr.GetSearchIndexPath(ProjectName, DocumentName);

            _ErrorMessage = _DocMgr.ErrorMessage;

            return pathFile;

        }

        public DataSet Get_Documents_Dic_Summary(string ProjectName, string AnalysisName, string[] Docs, string Dictionary, string DictionariesPath, out string SumXRefPathFile, out DataSet dsDicFilter)
        {
            _ErrorMessage = string.Empty;

            if (_DicAnalysis == null)
                _DicAnalysis = new AnalysisDictionaries();


            DataSet ds =  _DicAnalysis.Get_Documents_Dic_Summary(ProjectName, AnalysisName, Docs, Dictionary, DictionariesPath, out SumXRefPathFile, out dsDicFilter);


            return ds;

        }

        public DataSet Get_Document_Concept_Summary(string ProjectName, string AnalysisName, string DocumentName)
        {
            _ErrorMessage = string.Empty;

            if (_AnalysisConceptsMgr == null)
                _AnalysisConceptsMgr = new AnalysisConcepts();


            DataSet ds = _AnalysisConceptsMgr.Get_Document_Concept_Summary(ProjectName, AnalysisName, DocumentName);

            _ErrorMessage = _AnalysisConceptsMgr.ErrorMessage;

            return ds;

        }

        public DataSet Get_Document_Dic_Summary(string ProjectName, string AnalysisName, string DocumentName)
        {
            _ErrorMessage = string.Empty;

            if (_DicAnalysis == null)
                _DicAnalysis = new AnalysisDictionaries();
 

            DataSet ds = _DicAnalysis.Get_Document_Dic_Summary(ProjectName, AnalysisName, DocumentName);

            _ErrorMessage = _DicAnalysis.ErrorMessage;

            return ds;

        }

        public bool Project_Exists(string ProjectName)
        {
            return _ProjectMgr.ProjectExists(ProjectName);
        }

        public bool Project_New(string ProjectName, string Description, bool isDAProject)
        {
            if (!_ProjectMgr.ProjectNew(ProjectName, Description, isDAProject))
            {
                _ErrorMessage = _ProjectMgr.ErrorMessage;

                return false;
            }

            return true;

        }
 

        /// <summary>
        /// Connect to a CA Project
        /// </summary>
        /// <param name="projectName">Project Name</param>
        /// <param name="isDAProject">Is new a Document Analyzer Project?</param>
        /// <returns>True if no error occured</returns>
        public bool Project_Connect(string projectName, bool isDAProject)
        {
            if (!CheckCAProjectFolders(projectName))
                return false;

            _DocMgr = new Documents();
            return true;
        }

        public bool Document_Add(string DocumentName, string DocumentPathFile, out string CopiedPathFile, string UserName)
        {
            CopiedPathFile = string.Empty;

            _ErrorMessage = string.Empty;

            if (AppFolders_CA.ProjectName.Trim().Length == 0)
            {
                _ErrorMessage = "Project has Not been defined/selected.";
                return false;
            }

            if (_DocMgr == null)
            {
                _DocMgr = new Documents();
                if (_DocMgr == null)
                {
                    _ErrorMessage = "Unable to open the Documents Manager.";
                    return false;
                }
            }

            if (!_DocMgr.Document_Add(AppFolders_CA.ProjectName, DocumentPathFile, out CopiedPathFile, UserName))
            {
                _ErrorMessage = _DocMgr.ErrorMessage;
                return false;
            }

            return true;
        }

        public string Document_GetPathFile(string ProjectName, string FileName, bool isDAProject)
        {
            _ErrorMessage = string.Empty;

            if (AppFolders_CA.ProjectName.Trim().Length == 0)
            {
                _ErrorMessage = "Project has Not been defined/selected.";
                return string.Empty;
            }

            if (_DocMgr == null)
            {
                _DocMgr = new Documents();
                if (_DocMgr == null)
                {
                    _ErrorMessage = "Unable to open the Documents Manager.";
                    return string.Empty;
                }
            }

            return _DocMgr.GetPathFile(ProjectName, FileName, isDAProject);
        
        }



        public DataTable CreateEmptyDataTable_Analysis()
        {

            DataTable table = new DataTable(AnalysisUCaseFieldConst.TableNameAnalysis);

            table.Columns.Add(AnalysisUCaseFieldConst.UID, typeof(int));
       //     table.Columns.Add(AnalysisUCaseFieldConst.ProjectUID, typeof(int));
            table.Columns.Add(AnalysisUCaseFieldConst.Name, typeof(string));
            table.Columns.Add(AnalysisUCaseFieldConst.Use_PDAProjects, typeof(bool));

            table.Columns.Add(AnalysisUCaseFieldConst.DictionaryName, typeof(string));

            table.Columns.Add(AnalysisUCaseFieldConst.ParseType, typeof(string)); // 0 = Legal, 1 = Paragraph
            table.Columns.Add(AnalysisUCaseFieldConst.Use_Dictionary, typeof(bool));
            table.Columns.Add(AnalysisUCaseFieldConst.DicFindWholewords, typeof(bool));
            table.Columns.Add(AnalysisUCaseFieldConst.DicFindSynonyms, typeof(bool));
            table.Columns.Add(AnalysisUCaseFieldConst.GenerateSummaries, typeof(bool));
            table.Columns.Add(AnalysisUCaseFieldConst.FindConcepts, typeof(bool));
            table.Columns.Add(AnalysisUCaseFieldConst.FindEmails, typeof(bool));
            table.Columns.Add(AnalysisUCaseFieldConst.FindDates, typeof(bool));
            table.Columns.Add(AnalysisUCaseFieldConst.FindURLs, typeof(bool));
            table.Columns.Add(AnalysisUCaseFieldConst.CreatedBy, typeof(string));
            table.Columns.Add(AnalysisUCaseFieldConst.DateCreated, typeof(DateTime));

            return table;

        }

        public DataSet GetDataset()
        {
            _ErrorMessage = string.Empty;

            string pathFile = Path.Combine(_AnalysisRootPath, _XML_FILE);


            if (!File.Exists(pathFile))
            {
                _ErrorMessage = string.Concat("Unable to find ", pathFile);
                return null;
            }

            _ds = Files.LoadDatasetFromXml(pathFile);

            return _ds;
        }

        public bool SaveDataset(DataSet ds)
        {
            _ds = ds;

            return SaveDataset();
        }

        public bool SaveDataset()
        {
            _ErrorMessage = string.Empty;


            string pathFile = Path.Combine(_AnalysisRootPath, _XML_FILE);

            try
            {
                _DataMgr.SaveDataXML(_ds, pathFile);
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.Message;
            }

            if (_ErrorMessage == string.Empty)
                return true;
            else
                return false;
        }


    }
}
