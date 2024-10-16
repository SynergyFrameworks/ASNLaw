using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Threading.Tasks;
using Atebion.Common;
using Atebion.Tasks;
using Atebion.DiffSxS;
using AcroParser2;


namespace ProfessionalDocAnalyzer
{
    class AnalyzerManager
    {
        string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get {return _ErrorMessage; }
        }

        private bool _isUseDefaultParseAnalysis = true;
        public bool isUseDefaultParseAnalysis
        {
            set { _isUseDefaultParseAnalysis = value; }
            get { return _isUseDefaultParseAnalysis; }
        }

        private string _ParsedSecPath = string.Empty;
        public string ParsedSecPath
        {
            get 
            {
                SetParsePathsProperties();
                return _ParsedSecPath; 
            }
        }

        private string _ParsedSecXMLPath = string.Empty;
        public string ParsedSecXMLPath
        {
            get 
            {
                SetParsePathsProperties();
                return _ParsedSecXMLPath; 
            }
        }

        private string _ParsedSecNotesPath = string.Empty;
        public string ParsedSecNotesPath
        {
            get
            {
                SetParsePathsProperties();
                return _ParsedSecNotesPath;
            }
        }

        private string _ParsedSecIndexPath = string.Empty;
        public string ParsedSecIndexPath
        {
            get
            {
                SetParsePathsProperties();
                return _ParsedSecIndexPath;
            }
        }

        private string _ParsedSecExportPath = string.Empty;
        public string ParsedSecExportPath
        {
            get
            {
                SetParsePathsProperties();
                return _ParsedSecExportPath;
            }
        }



        private string _AnalysisName = string.Empty; // Use when _isUseDefaultParseAnalysis = False
        public string AnalysisName
        {
            set {
                    _AnalysisName = value;
                    AppFolders.AnalysisName = _AnalysisName;
                }
            get { return _AnalysisName; }
        }

        private string _AnalysisPath = string.Empty;
        public string AnalysisPath
        {
            get
            {
                _AnalysisPath = AppFolders.AnalysisCurrent;
                
                return _AnalysisPath; }
        }

        private string[] _DocumentFilesNames;
        public string[] DocumentFilesNames
        {
            set { _DocumentFilesNames = value; }
            get { return _DocumentFilesNames; }
        }

        private string _ParseTypeFile = "ParseType.par";
        private string _ParseErrorFile = "ParseError.err";

        // Docs diff folders
        private string _modFolder = string.Empty;
        public string ModFolder
        {
            get { return _modFolder; }
        }

        private string _modWholeFolder = string.Empty;
        public string ModWholeFolder
        {
            get { return _modWholeFolder; }
        }

        private string _modPartFolder = string.Empty;
        public string ModPartFolder
        {
            get { return _modPartFolder; }
        }


        public string GetNextSet_AnalysisName(string Task, bool toSet)
        {
            string newAnalysisPath = string.Empty;
            string taskNameNo = string.Empty;
            int i = 1;

            do
            {
                taskNameNo = string.Concat(Task, "_", i.ToString());
                newAnalysisPath = Path.Combine(AppFolders.AnalysisPath, taskNameNo);

                i++;
            } while (Directory.Exists(newAnalysisPath));


            if (toSet)
                AppFolders.AnalysisName = taskNameNo; // Will generate folders for the new Analysis

            return taskNameNo;
        }

        public bool AcroSeekerAnalyze(string AnalysisName, string ModelPath, string DictionaryName1, string DictionariesPath, string IgnoreDictionaryName, string IgnoreDictionaryPath)
        {
            _ErrorMessage = string.Empty;

            if (_DocumentFilesNames.Length == 0)
            {
                _ErrorMessage = "No documents have been selected.";
                return false;
            }

            AppFolders.AnalysisName = AnalysisName;
            string AnalysisFolder = AppFolders.AnalysisCurrent;
            string DocsFolder = Path.Combine(AnalysisFolder, "Docs");
            string projectFolder = AppFolders.ProjectCurrent;
            string projectDocsFolder = Path.Combine(projectFolder, "Docs");

            AcroParser2.Analyzer acroAnalyzer = new Analyzer();

            // Set Model Path
            acroAnalyzer.ModelPath = ModelPath;

            // Set Dictionary
            acroAnalyzer.DictionariesPath = DictionariesPath;
            acroAnalyzer.DictionaryName1 = DictionaryName1;
            //if (DictionaryName1 != string.Empty)
            //{
            //    string dicFile = string.Concat(DictionaryName1, ".xml");
            //    string dicPathFile = Path.Combine(DictionariesPath, dicFile);
            //    if (!File.Exists(dicPathFile))
            //    {
            //        _ErrorMessage = string.Concat("Unable to find Dictionary file: ", dicPathFile);
            //    }
            //    else
            //    {
            //        GenericDataManger gDataMgr = new GenericDataManger();
            //        DataSet ds = gDataMgr.LoadDatasetFromXml(dicPathFile);

            //        acroAnalyzer.tdDictionary1 = ds.Tables[0];
            //    }
            //}

            // Set Ignore Dictionary
            acroAnalyzer.IgnoreDictionaryPath = IgnoreDictionaryPath;
            acroAnalyzer.IgnoreDictionaryName = IgnoreDictionaryName;

            string resultDocFolder = string.Empty;
            string sentencePath = string.Empty;
            string resultsPath = string.Empty;
            string currentDocPath = string.Empty;
            string resultsFile = "Results.html";
            string resultsFilePath = string.Empty;
            string resultsMSWordTableFile = "Acronyms.docx";
            string resultsMSWordTablePathFile = string.Empty;

            string[] files;

            string orgPathDoc = string.Empty;
            string orgNewPathDoc = string.Empty;
            string orgFileName = string.Empty;

            foreach (string documentFileName in _DocumentFilesNames)
            {
                currentDocPath = Path.Combine(projectDocsFolder, documentFileName, "Current");
                orgPathDoc = this.GetCurrentDocument(currentDocPath);

                files = Directory.GetFiles(currentDocPath, "*.txt");
                if (files.Length == 0)
                {
                    if (_ErrorMessage == string.Empty)
                        _ErrorMessage = string.Concat("Unable to find selected file in folder: ", currentDocPath);
                    else
                        _ErrorMessage = string.Concat(_ErrorMessage, Environment.NewLine, Environment.NewLine, "Unable to find selected file in folder: ", currentDocPath);
                }
                else
                {
                    resultDocFolder = Path.Combine(DocsFolder, documentFileName);
                    if (!Directory.Exists(resultDocFolder))
                        Directory.CreateDirectory(resultDocFolder);

                    sentencePath = Path.Combine(resultDocFolder, "Sentences");
                    if (!Directory.Exists(sentencePath))
                        Directory.CreateDirectory(sentencePath);

                    resultsPath = Path.Combine(resultDocFolder, "Results");
                    if (!Directory.Exists(resultsPath))
                        Directory.CreateDirectory(resultsPath);

                    // Copy the orginial file to analysis doc folder
                    orgFileName = Files.GetFileName(orgPathDoc);
                    orgNewPathDoc = Path.Combine(resultDocFolder, orgFileName);

                    if (!File.Exists(orgNewPathDoc))
                        File.Copy(orgPathDoc, orgNewPathDoc);

                    resultsFilePath = Path.Combine(resultsPath, resultsFile);

                    resultsMSWordTablePathFile = Path.Combine(resultsPath, resultsMSWordTableFile);

                    acroAnalyzer.SentencePath = sentencePath;

                    if (!acroAnalyzer.Analyze(files[0]))
                    {
                        if (_ErrorMessage == string.Empty)
                            _ErrorMessage = acroAnalyzer.ErrorMessage;
                        else
                            _ErrorMessage = string.Concat(_ErrorMessage, Environment.NewLine, Environment.NewLine, acroAnalyzer.ErrorMessage);
                    }

                    if (!acroAnalyzer.GenerateRpt(resultsFilePath, documentFileName))
                    {
                        if (_ErrorMessage == string.Empty)
                            _ErrorMessage = acroAnalyzer.ErrorMessage;
                        else
                            _ErrorMessage = string.Concat(_ErrorMessage, Environment.NewLine, Environment.NewLine, acroAnalyzer.ErrorMessage);
                    }


                    if (!acroAnalyzer.GenerateWordTable(resultsMSWordTablePathFile, documentFileName, true))
                    {
                        if (_ErrorMessage == string.Empty)
                            _ErrorMessage = acroAnalyzer.ErrorMessage;
                        else
                            _ErrorMessage = string.Concat(_ErrorMessage, Environment.NewLine, Environment.NewLine, acroAnalyzer.ErrorMessage);
                    }

                    string logFilePath = Path.Combine(resultsPath, "AS_Analysis.log");
                    Files.WriteStringToFile(acroAnalyzer.AnalysisLog, logFilePath);

                    string xmlFilePath = Path.Combine(resultsPath, "AS_Analysis.xml");
                    acroAnalyzer.dsAcronyms.WriteXml(xmlFilePath);

                    string xmlFilePath_Found = Path.Combine(resultsPath, "AS_Analysis_Found.xml");
                    acroAnalyzer.dtAcronymsFound.WriteXml(xmlFilePath_Found);

                    string xmlFilePath_NotFound = Path.Combine(resultsPath, "AS_Analysis_NotFound.xml");
                    acroAnalyzer.dtAcronymsNotDefinedFound.WriteXml(xmlFilePath_NotFound);

                }

            }


            if (_ErrorMessage.Length > 0)
            {
                return false;
            }

            return true;
        }

        public bool CompareDocsDiff(string OldFile, string NewFile, string AnalysisName)
        {
            DocsDiff docDiff = new DocsDiff();

            AppFolders.AnalysisName = AnalysisName;

            string AnalysisFolder = AppFolders.AnalysisCurrent;
            string DocsFolder = Path.Combine(AnalysisFolder, "Docs"); //AppFolders.AnalysisCurrentDocs;

            // Old
            string oldFileName = string.Concat(OldFile, ".txt");
            string oldPath = Path.Combine(AppFolders.DocPath, OldFile, "Current");
            string[] files = Directory.GetFiles(oldPath, "*.txt");
            if (files.Length == 0)
            {
                _ErrorMessage = string.Concat("Unable to find selected file in folder: ", oldPath);
                return false;
            }
            string oldPathFile = files[0];
            string oldOrgPathFile = GetCurrentDocument(oldPath);
            

            // New
            string newFileName = string.Concat(NewFile, ".txt");
            string newPath = Path.Combine(AppFolders.DocPath, NewFile, "Current");
            files = Directory.GetFiles(newPath, "*.txt");
            if (files.Length == 0)
            {
                _ErrorMessage = string.Concat("Unable to find selected file in folder: ", newPath);
                return false;
            }
            string newPathFile = files[0];
            string newOrgPathFile = GetCurrentDocument(newPath);



            string oldFileFolder = Path.Combine(DocsFolder, OldFile);
            if (!Directory.Exists(oldFileFolder))
            {
                Directory.CreateDirectory(oldFileFolder);
            }

            string newFileFolder = Path.Combine(DocsFolder, NewFile);
            if (!Directory.Exists(newFileFolder))
            {
                Directory.CreateDirectory(newFileFolder);
            }

          //  string desOldFileName = string.Concat(OldFile, ".txt");
            string desOldFile = Path.Combine(oldFileFolder, oldFileName);
            File.Copy(oldPathFile, desOldFile);
            string oldOrgFile = Files.GetFileName(oldOrgPathFile);
            string oldOrgDesPathFile = Path.Combine(oldFileFolder, oldOrgFile);
            //File.Copy(oldOrgPathFile, oldOrgDesPathFile);
            File.Copy(oldOrgPathFile, oldOrgDesPathFile, true);

            //  string desNewFileName = string.Concat(newFileName, ".txt");
            string desNewFile = Path.Combine(newFileFolder, newFileName);
            File.Copy(newPathFile, desNewFile);
            string newOrgFile = Files.GetFileName(newOrgPathFile);
            string newOrgDesPathFile = Path.Combine(newFileFolder, newOrgFile);
            File.Copy(newOrgPathFile, newOrgDesPathFile,true);

            // Mods
            _modFolder = AppFolders.AnalysisCurrentDiffMods;
            //_modFolder = Path.Combine(AnalysisFolder, "Mods");
            //if (!Directory.Exists(_modFolder))
            //{
            //    Directory.CreateDirectory(_modFolder);
            //}

            // Mod Part
            _modPartFolder = AppFolders.AnalysisCurrentDiffModsPart;
            //_modPartFolder = Path.Combine(_modFolder, "Part");
            //if (!Directory.Exists(_modPartFolder))
            //{
            //    Directory.CreateDirectory(_modPartFolder);
            //}

            // Mod Whole
            _modWholeFolder = AppFolders.AnalysisCurrentDiffModsWhole;
            //_modWholeFolder = Path.Combine(_modFolder, "Whole");
            //if (!Directory.Exists(_modWholeFolder))
            //{
            //    Directory.CreateDirectory(_modWholeFolder);
            //}


            return docDiff.CompareDocs3(oldPathFile, newPathFile, oldOrgPathFile, newOrgPathFile, AnalysisFolder, _modWholeFolder, _modPartFolder); 
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

        private bool Default_Refresh() // Added 05.06.2020
        {
  
            Files.DeleteAllFileInDir(AppFolders.DocParsedSec);
            Files.DeleteAllFileInDir(AppFolders.DocParsedSecXML);
            Files.DeleteAllFileInDir(AppFolders.DocParsedSecHTML);
            Files.DeleteAllFileInDir(AppFolders.DocParsedSecIndex);
            Files.DeleteAllFileInDir(AppFolders.DocParsedSecIndex2);
            Files.DeleteAllFileInDir(AppFolders.DocParsedSecKeywords);
            
            return true;
        }

        public bool ParseDocuments2(bool isLegal, bool isToBeNumericalHierarchyConcatenations, bool isDefualt) // If isDefualt = False Then use Analysis folders
        {
            _ErrorMessage = string.Empty;

            if (_DocumentFilesNames.Length == 0)
            {
                _ErrorMessage = "Document Files not defined.";

                return false;
            }

            foreach (string docFileName in _DocumentFilesNames)
            {
                AppFolders.DocName = docFileName;

                string[] txtFiles = Directory.GetFiles(AppFolders.CurrentDocPath, "*.txt");
                if (txtFiles.Length == 0) // Document Not Found
                {
                    string msg = string.Concat("Unable to locate the converted document text file for Document: ", docFileName);

                    if (_ErrorMessage.Length == 0)
                    {
                        _ErrorMessage = msg;
                    }
                    else
                    {
                        _ErrorMessage = string.Concat(Environment.NewLine, msg);
                    }
                }
                else
                {
                    string docFile = txtFiles[0];

                    DocumentAnalysis da = new DocumentAnalysis();

                    _ParsedSecPath = AppFolders.DocParsedSec;
                    _ParsedSecXMLPath = AppFolders.DocParsedSecXML;

                    if (isDefualt) // Added 05.06.2020
                    {
                        Default_Refresh();
                    }

                    Atebion.Common.Files.CopyFiles(AppFolders.CurrentDocPath, AppFolders.AnalysisCurrentDocsDocName); // Added 09.27.2019


                    if (isLegal) // Leagl Documents
                    {
                        string[] legalParsedSec = Directory.GetFiles(AppFolders.DocParsedSec_Hold_Logic);
                        string[] legalParsedSecXML = Directory.GetFiles(AppFolders.DocParsedSec_Hold_Logic_XML);
                        if (legalParsedSec.Length == 0 || legalParsedSecXML.Length == 0) // no previous legal parse segments were found
                        {
                            int countSegments = da.Parse_Legal(docFile, @AppFolders.DocParsedSec_Hold_Logic, @AppFolders.DocParsedSec_Hold_Logic_XML);

                            if (countSegments == -1) // Parsing failed
                            {
                                if (_ErrorMessage.Length == 0)
                                {
                                    _ErrorMessage = da.ErrorMessage;
                                }
                                else
                                {
                                    _ErrorMessage = string.Concat(Environment.NewLine, da.ErrorMessage);
                                }

                                string errFile = Path.Combine(AppFolders.DocInformation, _ParseErrorFile);
                                Atebion.Common.Files.WriteStringToFile(_ErrorMessage, errFile);
                            }
                            else // Copy result to holding folders and save information to Log file.
                            {
                                if (isDefualt)
                                {
                                    // Copy Parse Segments and XML to Holding Folders
                                    Atebion.Common.Files.CopyFiles(AppFolders.DocParsedSec_Hold_Logic, AppFolders.DocParsedSec);
                                    Atebion.Common.Files.CopyFiles(AppFolders.DocParsedSec_Hold_Logic_XML, AppFolders.DocParsedSecXML);
                                }
                                else
                                {
                                    Atebion.Common.Files.CopyFiles(AppFolders.DocParsedSec_Hold_Logic, AppFolders.AnalysisParseSeg);
                                    Atebion.Common.Files.CopyFiles(AppFolders.DocParsedSec_Hold_Logic_XML, AppFolders.AnalysisParseSegXML);
                                }

                                // Set Parse Type to record type as Legal
                                string parseTypePathFile = Path.Combine(AppFolders.DocInformation, _ParseTypeFile);
                                Atebion.Common.Files.WriteStringToFile("Legal", parseTypePathFile);
                            }
                        }
                        else // use previous legal parse segments
                        {
                            if (isDefualt)
                            {
                                // Copy Parse Segments and XML to Holding Folders
                                Atebion.Common.Files.CopyFiles(AppFolders.DocParsedSec_Hold_Logic, AppFolders.DocParsedSec);
                                Atebion.Common.Files.CopyFiles(AppFolders.DocParsedSec_Hold_Logic_XML, AppFolders.DocParsedSecXML);
                            }
                            else
                            {
                                Atebion.Common.Files.CopyFiles(AppFolders.DocParsedSec_Hold_Logic, AppFolders.AnalysisParseSeg);
                                Atebion.Common.Files.CopyFiles(AppFolders.DocParsedSec_Hold_Logic_XML, AppFolders.AnalysisParseSegXML);
                            }
                        }
                        if (isToBeNumericalHierarchyConcatenations)
                        {
                            GenerateNumericalHierarchyConcatenation();
                        }
                    }
                    else // Paragraph Documents
                    {
                        string[] paragraphParsedSec = Directory.GetFiles(AppFolders.DocParsedSec_Hold_Paragraph);
                        string[] paragraphParsedSecXML = Directory.GetFiles(AppFolders.DocParsedSec_Hold_Paragraph_XML);

                        if (paragraphParsedSec.Length == 0 || paragraphParsedSecXML.Length == 0)
                        {
                            int countSegments = da.Parse_Paragraph(docFile, @AppFolders.DocParsedSec_Hold_Paragraph, @AppFolders.DocParsedSec_Hold_Paragraph_XML);

                            if (countSegments == -1)
                            {
                                if (_ErrorMessage.Length == 0)
                                {
                                    _ErrorMessage = da.ErrorMessage;
                                }
                                else
                                {
                                    _ErrorMessage = string.Concat(Environment.NewLine, da.ErrorMessage);
                                }

                                string errFile = Path.Combine(AppFolders.DocInformation, _ParseErrorFile);
                                Atebion.Common.Files.WriteStringToFile(_ErrorMessage, errFile);
                            }
                            else
                            {
                                if (isDefualt)
                                {
                                    // Copy Parse Segments and XML to Holding Folders
                                    Atebion.Common.Files.CopyFiles(AppFolders.DocParsedSec_Hold_Paragraph, AppFolders.DocParsedSec);
                                    Atebion.Common.Files.CopyFiles(AppFolders.DocParsedSec_Hold_Paragraph_XML, AppFolders.DocParsedSecXML);
                                }
                                else
                                {
                                    Atebion.Common.Files.CopyFiles(AppFolders.DocParsedSec_Hold_Paragraph, AppFolders.AnalysisParseSeg);
                                    Atebion.Common.Files.CopyFiles(AppFolders.DocParsedSec_Hold_Paragraph_XML, AppFolders.AnalysisParseSegXML);
                                }

                                // Set Parse Type to record type as Paragraph
                                string parseTypePathFile = Path.Combine(AppFolders.DocInformation, _ParseTypeFile);
                                Atebion.Common.Files.WriteStringToFile("Paragraph", parseTypePathFile);
                            }
                        }
                        else // To save time for an user, use the previous parsed segments in the Hold folders
                        {
                            if (isDefualt)
                            {
                                // Copy Parse Segments and XML to Holding Folders
                                Atebion.Common.Files.CopyFiles(AppFolders.DocParsedSec_Hold_Paragraph, AppFolders.DocParsedSec);
                                Atebion.Common.Files.CopyFiles(AppFolders.DocParsedSec_Hold_Paragraph_XML, AppFolders.DocParsedSecXML);
                            }
                            else
                            {
                                Atebion.Common.Files.CopyFiles(AppFolders.DocParsedSec_Hold_Paragraph, AppFolders.AnalysisParseSeg);
                                Atebion.Common.Files.CopyFiles(AppFolders.DocParsedSec_Hold_Paragraph_XML, AppFolders.AnalysisParseSegXML);
                            }
                        }
                    }

                }
            }

            return true;
        }

        public bool ParseDocuments(bool isLegal, bool isToBeNumericalHierarchyConcatenations)
        {
            _ErrorMessage = string.Empty;

            if (_DocumentFilesNames.Length == 0)
            {
                _ErrorMessage = "Document Files not defined.";

                return false;
            }

            foreach (string docFileName in _DocumentFilesNames)
            {
                AppFolders.DocName = docFileName;

                string[] txtFiles = Directory.GetFiles(AppFolders.CurrentDocPath, "*.txt");
                if (txtFiles.Length == 0) // Document Not Found
                {
                    string msg = string.Concat("Unable to locate the converted document text file for Document: ", docFileName);

                    if (_ErrorMessage.Length == 0)
                    {
                        _ErrorMessage = msg;
                    }
                    else
                    {
                        _ErrorMessage = string.Concat(Environment.NewLine, msg);
                    }
                }
                else
                {
                    string docFile = txtFiles[0];

                    DocumentAnalysis da = new DocumentAnalysis();

                    if (_isUseDefaultParseAnalysis) // Results held under the orginal DA path structure. Use for Quick CM and Requirement Matrix
                    {
                        _ParsedSecPath = AppFolders.DocParsedSec;
                        _ParsedSecXMLPath = AppFolders.DocParsedSecXML;

                        if (isLegal) // Leagl Documents
                        {
                            string[] legalParsedSec = Directory.GetFiles(AppFolders.DocParsedSec_Hold_Logic);
                            if (legalParsedSec.Length == 0) // no previous legal parse segments were found
                            {
                                int countSegments = da.Parse_Legal(docFile, @AppFolders.DocParsedSec, @AppFolders.DocParsedSecXML);

                                if (countSegments == -1) // Parsing failed
                                {
                                    if (_ErrorMessage.Length == 0)
                                    {
                                        _ErrorMessage = da.ErrorMessage;
                                    }
                                    else
                                    {
                                        _ErrorMessage = string.Concat(Environment.NewLine, da.ErrorMessage);
                                    }

                                    string errFile = Path.Combine(AppFolders.DocInformation, _ParseErrorFile);
                                    Atebion.Common.Files.WriteStringToFile(_ErrorMessage, errFile);
                                }
                                else // Copy result to holding folders and save information to Log file.
                                {

                                    // Copy Parse Segments and XML to Holding Folders
                                    Atebion.Common.Files.CopyFiles(AppFolders.DocParsedSec, AppFolders.DocParsedSec_Hold_Logic);
                                    Atebion.Common.Files.CopyFiles(AppFolders.DocParsedSecXML, AppFolders.DocParsedSecXML_Hold_Legal);

                                    // Set Parse Type to record type as Legal
                                    string parseTypePathFile = Path.Combine(AppFolders.DocInformation, _ParseTypeFile);
                                    Atebion.Common.Files.WriteStringToFile("Legal", parseTypePathFile);
                                }
                            }
                            else // use previous legal parse segments
                            {
                                Atebion.Common.Files.CopyFiles(AppFolders.DocParsedSec_Hold_Logic, AppFolders.DocParsedSec);
                                Atebion.Common.Files.CopyFiles(AppFolders.DocParsedSecXML_Hold_Legal, AppFolders.DocParsedSecXML);
                            }
                            if (isToBeNumericalHierarchyConcatenations)
                            {
                                GenerateNumericalHierarchyConcatenation();
                            }
                            else
                            {
                                // ToDo GenerateNumericalHierarchy
                            }
                        }
                        else // Paragraph Documents
                        {
                            string[] paragraphParsedSec = Directory.GetFiles(AppFolders.DocParsedSec_Hold_Paragraph);
                            if (paragraphParsedSec.Length == 0)
                            {
                                int countSegments = da.Parse_Paragraph(docFile, @AppFolders.DocParsedSec, @AppFolders.DocParsedSecXML);

                                if (countSegments == -1)
                                {
                                    if (_ErrorMessage.Length == 0)
                                    {
                                        _ErrorMessage = da.ErrorMessage;
                                    }
                                    else
                                    {
                                        _ErrorMessage = string.Concat(Environment.NewLine, da.ErrorMessage);
                                    }

                                    string errFile = Path.Combine(AppFolders.DocInformation, _ParseErrorFile);
                                    Atebion.Common.Files.WriteStringToFile(_ErrorMessage, errFile);
                                }
                                else
                                {
                                    // Copy Parse Segments and XML to Holding Folders
                                    Atebion.Common.Files.CopyFiles(AppFolders.DocParsedSec, AppFolders.DocParsedSec_Hold_Paragraph);
                                    Atebion.Common.Files.CopyFiles(AppFolders.DocParsedSecXML, AppFolders.DocParsedSecXML_Hold_Paragraph);

                                    // Set Parse Type to record type as Paragraph
                                    string parseTypePathFile = Path.Combine(AppFolders.DocInformation, _ParseTypeFile);
                                    Atebion.Common.Files.WriteStringToFile("Paragraph", parseTypePathFile);
                                }
                            }
                            else // To save time for an user, use the previous parsed segments in the Hold folders
                            {
                                Atebion.Common.Files.CopyFiles(AppFolders.DocParsedSec_Hold_Paragraph, AppFolders.DocParsedSec);
                                Atebion.Common.Files.CopyFiles(AppFolders.DocParsedSecXML_Hold_Paragraph, AppFolders.DocParsedSecXML);
                            }
                        }
                    }
                    else // Use for Analysis paths
                    {
                        _ParsedSecPath = AppFolders.AnalysisParseSeg;
                        _ParsedSecXMLPath = AppFolders.AnalysisParseSegXML;

                        if (isLegal) // Leagl Documents
                        {
                            string[] legalParsedSec = Directory.GetFiles(AppFolders.DocParsedSec_Hold_Logic);
                            if (legalParsedSec.Length == 0) // no previous legal parse segments were found
                            {
                                int countSegments = da.Parse_Legal(docFile, _ParsedSecPath, _ParsedSecXMLPath);

                                if (countSegments == -1)
                                {
                                    if (_ErrorMessage.Length == 0)
                                    {
                                        _ErrorMessage = da.ErrorMessage;
                                    }
                                    else
                                    {
                                        _ErrorMessage = string.Concat(Environment.NewLine, da.ErrorMessage);
                                    }

                                    string errFile = Path.Combine(AppFolders.AnalysisInfor, _ParseErrorFile);
                                    Atebion.Common.Files.WriteStringToFile(_ErrorMessage, errFile);
                                }
                            }
                            else
                            {

                                // Copy Parse Segments and XML to Holding Folders
                                Atebion.Common.Files.CopyFiles(AppFolders.DocParsedSec_Hold_Logic, AppFolders.AnalysisParseSeg);
                                Atebion.Common.Files.CopyFiles(AppFolders.DocParsedSecXML_Hold_Legal, AppFolders.AnalysisXML);

                                // Set Parse Type to record type as Legal
                                string parseTypePathFile = Path.Combine(AppFolders.DocInformation, _ParseTypeFile);
                                string projectLogText = string.Concat("Project=", _AnalysisName);
                                string userLogText = string.Concat("User=", AppFolders.UserName);
                                string[] logContent = new string[] { "Legal", projectLogText, userLogText };
                                Atebion.Common.Files.WriteStringToFile(logContent, parseTypePathFile);
                            }

                            if (isToBeNumericalHierarchyConcatenations)
                            {
                                GenerateNumericalHierarchyConcatenation();
                            }
  

                            
                        }

                        else // Paragraph Documents
                        {
                            string[] paragraphParsedSec = Directory.GetFiles(AppFolders.DocParsedSec_Hold_Paragraph);
                            if (paragraphParsedSec.Length == 0)
                            {
                                int countSegments = da.Parse_Paragraph(docFile, @AppFolders.AnalysisParseSeg, @AppFolders.AnalysisXML);

                                if (countSegments == -1)
                                {
                                    if (_ErrorMessage.Length == 0)
                                    {
                                        _ErrorMessage = da.ErrorMessage;
                                    }
                                    else
                                    {
                                        _ErrorMessage = string.Concat(Environment.NewLine, da.ErrorMessage);
                                    }

                                    string errFile = Path.Combine(AppFolders.AnalysisInfor, _ParseErrorFile);
                                    Atebion.Common.Files.WriteStringToFile(_ErrorMessage, errFile);
                                }
                                else
                                {
                                    // Copy Parse Segments and XML to Holding Folders
                                    Atebion.Common.Files.CopyFiles(AppFolders.AnalysisParseSeg, AppFolders.DocParsedSec_Hold_Paragraph);
                                    Atebion.Common.Files.CopyFiles(AppFolders.AnalysisXML, AppFolders.DocParsedSecXML_Hold_Paragraph);


                                    // Set Parse Type to record type as Paragraph
                                    string parseTypePathFile = Path.Combine(AppFolders.DocInformation, _ParseTypeFile);
                                    string projectLogText = string.Concat("Project=", _AnalysisName);
                                    string userLogText = string.Concat("User=", AppFolders.UserName);
                                    string[] logContent = new string[] { "Paragraph", projectLogText, userLogText };
                                    Atebion.Common.Files.WriteStringToFile(logContent, parseTypePathFile);
                                }
                            }
                            else // To save time for an user, use the previous parsed segments in the Hold folders
                            {
                                Atebion.Common.Files.CopyFiles(AppFolders.DocParsedSec_Hold_Paragraph, AppFolders.DocParsedSec);
                                Atebion.Common.Files.CopyFiles(AppFolders.DocParsedSecXML_Hold_Paragraph, AppFolders.DocParsedSecXML);
                            }
                        }
                    }

                }
            }

                return true;
        }

        public bool MapPages(bool isUseDefaultParseAnalysis) 
        {
            _ErrorMessage = string.Empty;

            // Document Page Mapping to Parse Segments -- Document content is split into pages right after an user has selected a doucment file to import into PDA
            if (AppFolders.DocParsePage != string.Empty)
            {

                string[] pagefiles = Directory.GetFiles(AppFolders.DocParsePage, "*.txt");
                if (pagefiles.Length > 0)
                {
                    if (isUseDefaultParseAnalysis)
                    {
                        if (!InsertPageSource2ParseResultsTable(_ParsedSecXMLPath))
                        {
                            return false;
                        }
                        else
                        {
                            if (!MapParseSeg4Pages(AppFolders.DocParsePage, _ParsedSecPath, _ParsedSecXMLPath))
                            {
                                return false;
                            }

                        }
                    }
                    else
                    {
                        if (!InsertPageSource2ParseResultsTable(AppFolders.AnalysisParseSegXML))
                        {
                            return false;
                        }
                        else
                        {
                            if (!MapParseSeg4Pages(AppFolders.DocParsePage, AppFolders.AnalysisParseSeg, AppFolders.AnalysisParseSegXML))
                            {
                                return false;
                            }

                        }

                    }
                }
            }

            return true;
        }

        private bool InsertPageSource2ParseResultsTable(string XMLFolder)
        {
            _ErrorMessage = string.Empty;

            string xmlParseSegFile = ParseResultsFields.XMLFile;
            string xmlParseSegPathFile = Path.Combine(XMLFolder, xmlParseSegFile);


            if (!File.Exists(xmlParseSegPathFile))
            {
                _ErrorMessage = string.Concat("Parse Results XML file was not found: ", xmlParseSegPathFile);
                return false;
            }

            DataSet dsParseSeg = Atebion.Common.Files.LoadDatasetFromXml(xmlParseSegPathFile);

            if (dsParseSeg == null)
            {
                _ErrorMessage = string.Concat("Unable to read Parse Results XML file: ", xmlParseSegPathFile, "   Error: ", Atebion.Common.Files.ErrorMessage);
                return false;
            }

            // Check for PageSource column
            if (!dsParseSeg.Tables[0].Columns.Contains(ParseResultsFields.PageSource))
            {
                dsParseSeg.Tables[0].Columns.Add(ParseResultsFields.PageSource, typeof(System.String));
            }

            return true;
        }

        private bool MapParseSeg4Pages(string ParsePageFolder, string ParsedSegFolder, string XMLFolder)
        {
            _ErrorMessage = string.Empty;

            string[] pagefiles = Directory.GetFiles(ParsePageFolder, "*.txt"); // Page sources are only for Docx && pdfs
            if (pagefiles.Length == 0)
            {
                return true;
            }


            DataTable dtMappingTable = createTable_PageMapping();
            DataTable dtPageScope = createTable_PageScope();

            string xmlParseSegFile = ParseResultsFields.XMLFile;
            string xmlParseSegPathFile = Path.Combine(XMLFolder, xmlParseSegFile);

            if (!File.Exists(xmlParseSegPathFile))
            {
                _ErrorMessage = string.Concat("Parse Results XML file was not found: ", xmlParseSegPathFile);
                return false;
            }

            DataSet dsParseSeg = Atebion.Common.Files.LoadDatasetFromXml(xmlParseSegPathFile);
            if (dsParseSeg == null)
            {
                _ErrorMessage = string.Concat("Unable to read Parse Results XML file: ", xmlParseSegPathFile, "   Error: ", Atebion.Common.Files.ErrorMessage);
                return false;
            }

            int uid = -1;
            int currentPageNo = -1;

            bool firstPgIsZero = false;

            Atebion.RTFBox.RichTextBox rtfBox = new Atebion.RTFBox.RichTextBox();

            string file = string.Empty;
            string pathFile = string.Empty;
            string parseText = string.Empty;

            string[] textLines;
            string pageText = string.Empty;

            string page0File = "0.txt";
            string pagePathFile = Path.Combine(ParsedSegFolder, page0File);
            if (File.Exists(pagePathFile))
            {
                firstPgIsZero = true;

            }
            else
            {
                firstPgIsZero = false;

            }

            int startLine = 0;
            int endLine = 0;

            //Loop Thru Page Files and line ranges (scope)
            bool firstLoop = true;
            int i = 0;
            foreach (string pageFile in pagefiles)
            {
                textLines = Files.ReadFile2Array(pageFile);

                DataRow rowPageScope = dtPageScope.NewRow();
                rowPageScope[PageLineFields.UID] = i;

                if (!firstLoop)
                {
                    startLine = endLine + 1;
                }

                endLine = endLine + textLines.Length;

                rowPageScope[PageLineFields.PageNo] = i + 1;
                rowPageScope[PageLineFields.LineStart] = startLine;
                rowPageScope[PageLineFields.LineEnd] = endLine;

                dtPageScope.Rows.Add(rowPageScope);
                dtPageScope.AcceptChanges();

                i++;
                firstLoop = false;
            }


            int parsedFilesQty = dsParseSeg.Tables[0].Rows.Count;

            if (!dsParseSeg.Tables[0].Columns.Contains(ParseResultsFields.PageSource))
            {
                dsParseSeg.Tables[0].Columns.Add(ParseResultsFields.PageSource, typeof(int));
            }

            // Loop through parsed segments
            i = 0;
            int notFoundPageQty = 0;
            foreach (DataRow row in dsParseSeg.Tables[0].Rows)
            {
                uid = Convert.ToInt32(row[ParseResultsFields.UID].ToString());
                if (row[ParseResultsFields.LineStart].ToString().Length > 0) // If there is no LineStart, then it may have been split manually
                {
                    startLine = Convert.ToInt32(row[ParseResultsFields.LineStart].ToString());

                    currentPageNo = FindPageSource(dtPageScope, startLine, firstPgIsZero);
                }

                if (notFoundPageQty == -1)
                    notFoundPageQty++;

                DataRow rowMapping = dtMappingTable.NewRow();
                rowMapping[PageMappingFields.UID] = i;
                rowMapping[PageMappingFields.Seg_UID] = uid;
                rowMapping[PageMappingFields.PageNo] = currentPageNo;

                dtMappingTable.Rows.Add(rowMapping);
                dtMappingTable.AcceptChanges();

                row[ParseResultsFields.PageSource] = currentPageNo;
                dsParseSeg.AcceptChanges();

                i++;
            }

            GenericDataManger gdManager = new GenericDataManger();

            // Save Update Parse Segment with Page Numbers
            gdManager.SaveDataXML(dsParseSeg, xmlParseSegPathFile);

            // Save Parse Segment to Page Mapping 
            DataSet dsMapping = new DataSet();
            dsMapping.Tables.Add(dtMappingTable);
            string xmlMappingFile = Path.Combine(XMLFolder, PageMappingFields.XMLFile);
            gdManager.SaveDataXML(dsMapping, xmlMappingFile);

            // Save Page Scope
            DataSet dsPageScope = new DataSet();
            dsPageScope.Tables.Add(dtPageScope);
            string xmlPageScopeFile = Path.Combine(XMLFolder, PageLineFields.XMLFile);
            gdManager.SaveDataXML(dsPageScope, xmlPageScopeFile);


            if (notFoundPageQty == 0)
            {
                return true;
            }
            else
            {

                _ErrorMessage = string.Concat(notFoundPageQty.ToString(), " parsed segments/paragraphs were not able to identify their source pages.");

                return false;
            }

        }

        private int FindPageSource(DataTable dtPageScope, int lineNo, bool firstPgIsZero)
        {
            int startLine = 0;
            int endLine = 0;
            int i = 1;

            if (firstPgIsZero)
                i = 0;


            foreach (DataRow row in dtPageScope.Rows)
            {
                startLine = Convert.ToInt32(row[PageLineFields.LineStart].ToString());
                endLine = Convert.ToInt32(row[PageLineFields.LineEnd].ToString());

                if (lineNo >= startLine && lineNo <= endLine)
                {
                    if (firstPgIsZero)
                    {
                        i = i + 1;
                    }

                    return i;
                }

                i++;
            }

            return -1;
        }

        private DataTable createTable_PageMapping()
        {
            DataTable table = new DataTable(PageMappingFields.TableName);

            table.Columns.Add(PageMappingFields.UID, typeof(int));
            table.Columns.Add(PageMappingFields.Seg_UID, typeof(string));
            table.Columns.Add(PageMappingFields.PageNo, typeof(string));


            return table;

        }

        private DataTable createTable_PageScope()
        {
            DataTable table = new DataTable(PageLineFields.TableName);

            table.Columns.Add(PageLineFields.UID, typeof(int));
            table.Columns.Add(PageLineFields.PageNo, typeof(int));
            table.Columns.Add(PageLineFields.LineStart, typeof(int));
            table.Columns.Add(PageLineFields.LineEnd, typeof(int));


            return table;

        }

        private void SetParsePathsProperties()
        {
            if (_isUseDefaultParseAnalysis) // Results held under the orginal DA path structure. Use for Quick CM and Requirement Matrix
            {
                _ParsedSecPath = AppFolders.DocParsedSec;
                _ParsedSecXMLPath = AppFolders.DocParsedSecXML;
                _ParsedSecNotesPath = AppFolders.DocParsedSecNotes;
                _ParsedSecIndexPath = AppFolders.DocParsedSecIndex2;
                _ParsedSecExportPath = AppFolders.DocParsedSecExport;
            }
            else
            {
                _ParsedSecPath = AppFolders.AnalysisParseSeg;
                _ParsedSecXMLPath = AppFolders.AnalysisParseSegXML;
                _ParsedSecNotesPath = AppFolders.AnalysisParseSegNotes;
                _ParsedSecIndexPath = AppFolders.AnalysisParseSegIndex2;
                _ParsedSecExportPath = AppFolders.AnalysisParseSegExport;
            }
            
        }

        public bool GenerateNumericalHierarchyConcatenation()
        {
            _ErrorMessage = string.Empty;

            if (_ParsedSecXMLPath.Length == 0)
            {
                _ErrorMessage = "Analysis Results' XML folder is not known.";
                return false;
            }

  
            string parseResultsFile = "ParseResults.xml";
            string parseResultsPathFile = Path.Combine(_ParsedSecXMLPath, parseResultsFile);
            if (!File.Exists(parseResultsPathFile))
            {
                _ErrorMessage = string.Concat("Unable to find Analysis Results file: ", parseResultsPathFile);
                return false;
            }

            DataSet dsParseResults = Files.LoadDatasetFromXml(parseResultsPathFile);

            if (dsParseResults == null)
            {
                _ErrorMessage = string.Concat("Unable to open the Analysis Results file: ", parseResultsPathFile);
                return false;
            }

            DataTable dtParseResults = dsParseResults.Tables[0];

            
            int Highestlevel = 0;

            DataTable dtNumericalHierarchyXML = GenerateNumericalHierarchyXML(out Highestlevel);

            dtParseResults = SetNumericalHierarchyAnalysisResults(dtParseResults, dtNumericalHierarchyXML);

            DataView dv = new DataView(dsParseResults.Tables[0]);
            dv.Sort = "SortOrder";
            dtParseResults = dv.ToTable();

            string[] Parents = new string[Highestlevel];

   //         string lastParentNo = string.Empty;
    //        string lastNo = string.Empty;
            string currentNo = string.Empty;
            string newNo = string.Empty;

        //    int lastParentlevel = 0;
            int currentLevel = 0;
       //     int lastLevel = 0;

            string sLastlevel = string.Empty;
            string sCurrentLevel = string.Empty;

            foreach (DataRow row in dtParseResults.Rows)
            {
                sCurrentLevel = row[ParseResultsFields.NumberLevel].ToString();
                currentNo = row[ParseResultsFields.Number].ToString();

                if (!DataFunctions.IsNumeric(sCurrentLevel))
                {
                    _ErrorMessage = string.Concat(currentNo, "  ", row[ParseResultsFields.Caption].ToString(), " - No Numerical Hierarchy was found.");
                }
                else
                {
                    if (sCurrentLevel != "-1")
                    {

                        row[ParseResultsFields.OriginalNumber] = currentNo;
                        currentLevel = Convert.ToInt32(sCurrentLevel);

                        if (currentLevel > 1)
                        {
                            newNo = string.Concat(Parents[currentLevel - 2], " ", currentNo); // Parents[currentLevel - 2] Zero Base, get previous Level
                            row[ParseResultsFields.OriginalNumber] = currentNo;
                            row[ParseResultsFields.Number] = newNo;
                            Parents[currentLevel - 1] = newNo; // Zero Base, therefore Level 1 - 0 in array
                        }
                        else if (currentLevel == 1)
                        {
                            Parents[currentLevel - 1] = currentNo; // Zero Base, therefore Level 1 - 0 in array
                        }
                    }
                }

                //lastLevel = currentLevel;

            }

            if (_ErrorMessage.Length == 0)
            {
                DataSet dsNewParseResults = new DataSet();
                dsNewParseResults.Tables.Add(dtParseResults);

                GenericDataManger gdManager = new GenericDataManger();

                gdManager.SaveDataXML(dsNewParseResults, parseResultsPathFile);
                return true;
            }

            return false;
            
        }

        public DataTable SetNumericalHierarchyAnalysisResults(DataTable dtAnalysisResults, DataTable dtNumericalHierarchyXML)
        {
            _ErrorMessage = string.Empty;

            dtAnalysisResults = AddNewCols(dtAnalysisResults); // Add columns to support Generating Numerical Hierarchy Concatenation'

            string parameter = string.Empty;

            foreach (DataRow row in dtAnalysisResults.Rows)
            {
                parameter = row[ParseResultsFields.Parameter].ToString();

                row[ParseResultsFields.NumberLevel] = GetLevel(dtNumericalHierarchyXML, parameter);
            }


            return dtAnalysisResults;
        }


        private int GetLevel(DataTable dtNumericalHierarchyXML, string Parameter)
        {
            dtNumericalHierarchyXML.CaseSensitive = true;

            DataRow[] rows = dtNumericalHierarchyXML.Select(ParseResultsFields.Parameter + " = '" + Parameter + "'");
            if (rows.Length == 0)
                return 1;

            string sLevel = "1";
            string s = string.Empty;
            foreach (DataRow xRow in rows)
            {
                if (Parameter == xRow[ParseResultsFields.Parameter].ToString())
                {
                    sLevel = xRow[ParseResultsFields.NumberLevel].ToString();
                    break;
                }
                
            }

            if (!DataFunctions.IsNumeric(sLevel))
                return 1;

            int level = Convert.ToInt32(sLevel);

            return level;

        }

        private bool FindValueInDataTable(DataTable dt, string InField, string sValue)
        {
            DataRow[] foundValue = dt.Select(InField + " = '" + sValue + "'");
            if (foundValue.Length != 0)
            {
                return true;
            }

            return false;

        }



        private DataTable AddNewCols(DataTable dtParseResults)
        {
            if (!dtParseResults.Columns.Contains(ParseResultsFields.NumberLevel))
            {
                dtParseResults.Columns.Add(ParseResultsFields.NumberLevel, typeof(int));
            }

            if (!dtParseResults.Columns.Contains(ParseResultsFields.OriginalNumber))
            {
                dtParseResults.Columns.Add(ParseResultsFields.OriginalNumber, typeof(string));
            }


            return dtParseResults;
        }

        private DataTable createHierarchyParameterTable()
        {
            DataTable table = new DataTable("HierarchyParameter");

            table.Columns.Add(ParseResultsFields.Parameter, typeof(string));
            table.Columns.Add(ParseResultsFields.Parent, typeof(string));
            table.Columns.Add(ParseResultsFields.NumberLevel, typeof(int));
            
            return table;

        }

        public DataTable GenerateNumericalHierarchyXML(out int Highestlevel)
        {
            _ErrorMessage = string.Empty;

            Highestlevel = 0;

            if (_ParsedSecXMLPath.Length == 0)
            {
                _ErrorMessage = "Analysis Results' XML folder is not known.";
                return null;
            }

            string parametersFoundFile = "ParametersFound.xml";
            string parametersFoundpathFile = Path.Combine(_ParsedSecXMLPath, parametersFoundFile);
            if (!File.Exists(parametersFoundpathFile))
            {
                _ErrorMessage = string.Concat("Unable to find Parse Parameters Found file: ", parametersFoundpathFile);
                return null;
            }


            DataSet dsParameterFound = Files.LoadDatasetFromXml(parametersFoundpathFile);
            if (dsParameterFound == null)
            {
                _ErrorMessage = string.Concat("Unable to open the Parameters Found file: ", dsParameterFound);
                return null;
            }

            DataTable dtHierarchyParameter = createHierarchyParameterTable();

            string parameter = string.Empty;
            string lastParameter = string.Empty;
            string lastParent= string.Empty;
            string parent = string.Empty;
            int lastLevel = -1;
            int level = -1;
            DataRow newRow;
            string found = string.Empty;
            string Lastfound = string.Empty;

            foreach (DataRow parRow in dsParameterFound.Tables[0].Rows)
            {
                parameter = parRow[ParseResultsFields.Parameter].ToString();
                parent = parRow[ParseResultsFields.Parent].ToString();
                parent = parent.Replace("Parent: ", "");
                found = parRow["Found"].ToString();

                if (parameter != lastParameter)
                {
                    if (parameter == "Header")
                    {
                        parent = string.Empty;
                        level = -1;

                        newRow = dtHierarchyParameter.NewRow();
                        newRow[ParseResultsFields.Parameter] = parameter;
                        newRow[ParseResultsFields.Parent] = parent;
                        newRow[ParseResultsFields.NumberLevel] = level;
                        dtHierarchyParameter.Rows.Add(newRow);

                        lastParameter = parameter;
                        lastParent = parent;
                        lastLevel = level;
                        Lastfound = found;

 
                    }
                    else
                    {
                        if (!FindValueInDataTable(dtHierarchyParameter, ParseResultsFields.Parameter, parameter))
                        {
                            //if (parent == "Root" && (found.IndexOf('(') > -1 || found.IndexOf(')') > -1))
                            //{
                            //    level = 1;
                            //    parent = "Root";
                            //}
                            if (parent == "Root")
                            {      
                                level = 1;
                            }
                            else
                            {
                                if (lastParent == "Root" && (Lastfound.IndexOf('(') > -1 || Lastfound.IndexOf(')') > -1))
                                {
                                    level = 1;
                                    parent = "Root";
                                }
                                else
                                {
                                    if (DataFunctions.IsNumeric(found))
                                    {
                                        level = 1;
                                        parent = "Root";
                                    }
                                    else if (parent != string.Empty)
                                    {
                                        if (found.Length == 2)
                                        {
                                            if (DataFunctions.IsNumeric(found.Substring(0, 1)) && found.Substring(1, 1) == ".") // e.g. 2.
                                            {
                                                level = 1;
                                                parent = "Root";
                                            }
                                            else if (IsFoundUpperCase(found.Substring(0, 1)) == true && found.Substring(1, 1) == ".") // e.g. C.
                                            {
                                                level = 1;
                                                parent = "Root";
                                            }
                                            else
                                            {
                                                level = GetLevel(dtHierarchyParameter, parent) + 1;
                                            }
                                        }
                                        else if (found.Length > 2)
                                        {
                                            if (IsFoundUpperCase(found.Substring(0,1)) == true && found.Substring(1,1) == "-" && DataFunctions.IsNumeric(found.Substring(2,1))) // e.g. M-1
                                            {
                                                level = 1;
                                                parent = "Root";
                                            }
                                            else
                                            {
                                                level = GetLevel(dtHierarchyParameter, parent) + 1;
                                            }
                                        }
                                        else
                                        {
                                            level = GetLevel(dtHierarchyParameter, parent) + 1;
                                        }
                                    }
                                    else if (lastParameter != string.Empty)
                                    {
                                        //if (DataFunctions.IsNumeric(found))
                                        //{
                                        //    level = 1;
                                        //    parent = "Root";
                                        //}
                                        //else
                                        //{

                                            level = GetLevel(dtHierarchyParameter, lastParameter) + 1;
                                            
                                            
                                       // }
                                    }
                                    else
                                    {
                                        level++;
                                    }
                                }
                            }

                            if (level == 0)
                            {
                                string c = "ASSDE";
                            }

                            newRow = dtHierarchyParameter.NewRow();
                            newRow[ParseResultsFields.Parameter] = parameter;
                            newRow[ParseResultsFields.Parent] = parent;
                            newRow[ParseResultsFields.NumberLevel] = level;
                            dtHierarchyParameter.Rows.Add(newRow);

                            lastParameter = parameter;
                            lastParent = parent;
                            lastLevel = level;
                            Lastfound = found;

                            if (level > Highestlevel)
                            {
                                Highestlevel = level;
                            }
                        }

 
                    }

 
 
                }
            }

            //_ParsedSecXMLPath

            string hierarchyParameterFile = "HierarchyParameter.xml";
            string hierarchyParameterPathFile = Path.Combine(_ParsedSecXMLPath, hierarchyParameterFile);

            DataSet dsHierarchyParameter = new DataSet();
            dsHierarchyParameter.Tables.Add(dtHierarchyParameter);

            try
            {
                if (File.Exists(hierarchyParameterPathFile))
                {
                    File.Delete(hierarchyParameterPathFile);
                }

                GenericDataManger gdManager = new GenericDataManger();

                gdManager.SaveDataXML(dsHierarchyParameter, hierarchyParameterPathFile);

                return dtHierarchyParameter;

            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.Message;
            }

            return null;

        }

        private bool IsFoundUpperCase(string Found)
        {
            if (Found.Trim().Length == 0)
                return false;

            char[] chars = Found.ToCharArray();

            return char.IsUpper(chars[0]);

        }




        

    }
}
