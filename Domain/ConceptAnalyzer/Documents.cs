using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Data;
using System.Text.RegularExpressions;

using Domain.Common;
//using Domain.Import;
using OpenTextSummarizer;



namespace Domain.ConceptAnalyzer
{
    class Documents
    {
        public Documents()
        {
            
        }

  //      private Domain.Import.Manager _ImportMgr = new Manager();

        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        public bool Document_Add(string ProjectName, string DocumentPathFile, out string CopiedPathFile, string UserName)
        {
            CopiedPathFile = string.Empty;

            _ErrorMessage = string.Empty;

            if (!File.Exists(DocumentPathFile))
            {
                _ErrorMessage = string.Concat("Unable to find file: ", DocumentPathFile);
                return false;
            }

            AppFolders_CA.ProjectName = ProjectName;
          //  string s = AppFolders_CA.ProjectCurrent;
            string documentName = Files.GetFileNameWOExt(DocumentPathFile);
            string s = AppFolders_CA.DocPath;
            AppFolders_CA.DocName = documentName;
            string docFolder = AppFolders_CA.AnalysisCurrentDocsDocName;

            string ext;
            string file = Files.GetFileName(DocumentPathFile, out ext);
            CopiedPathFile = Path.Combine(docFolder, file);
            
            // Copy selected file to the Project Document folder
            File.Copy(DocumentPathFile, CopiedPathFile, true);


            // --------------- Convert selected file into a Text file -----------------
            //AppFolders_CA.AnalysisCurrentDocsDocName = documentName;

            string convertedFile = string.Concat(documentName, ".txt");
            string convertedPathFile = Path.Combine(docFolder, convertedFile);
            //if (!_ImportMgr.ConvertFile2PlainText(ext, copiedPathFile, convertedPathFile))
            //{
            //    _ErrorMessage = _ImportMgr.ErrorMessage;

            //    return false;
            //}

            // --------------- Parse into Pages - PDF & DOCX only -----------------
            //string pagesPath = AppFolders_CA.DocParsePage;
            //int pagesQty = -1;
            //if (ext.ToUpper() == "PDF")
            //{
            //    pagesQty = _ImportMgr.PDF2Pages(CopiedPathFile, pagesPath);
            //    if (pagesQty == -1)
            //    {
            //        _ErrorMessage = _ImportMgr.ErrorMessage;
            //    }
            //}
            //else if (ext.ToUpper() == "DOCX")
            //{
            //    pagesQty = _ImportMgr.DOCX2Pages(CopiedPathFile, pagesPath);
            //    if (pagesQty == -1)
            //    {
            //        _ErrorMessage = _ImportMgr.ErrorMessage;
            //    }
            //}

            // --------------- Information file -- Generate -----------------
            Domain.Common.FileInformation fileInfo = new FileInformation(DocumentPathFile);
            StringBuilder sb = new StringBuilder();
            
         //   sb.AppendLine(string.Concat("File: ", fileInfo.FileName));

            //double kb = Convert.ToDouble(fileInfo.FileSize) / 1000;
            //if (pagesQty > 0)
            //    sb.AppendLine(string.Concat("Pages: ", pagesQty.ToString()));
            //sb.AppendLine(string.Concat("Size: ", kb.ToString(), " KB"));
            sb.AppendLine(string.Concat("Created: ", fileInfo.CreationDate));

            sb.AppendLine(string.Concat("Added by: ", UserName));
            DateTime now = DateTime.Now;
            sb.AppendLine(string.Concat("Added Date: ", now.ToLongDateString()));

            sb.AppendLine(string.Concat("Source: ", DocumentPathFile));

            string infoPath = AppFolders_CA.AnalysisInfor;

            string infoPathFile = Path.Combine(infoPath, "Info.txt");

            Files.WriteStringToFile(sb.ToString(), infoPathFile);

            return true;
        }

        /// <summary>
        /// Gets the converted path & file name, unless the selected document was already a Plain Text file
        /// </summary>
        /// <param name="ProjectName"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public string GetPathTxtFileName(string ProjectName, string AnalysisName, string FileName, string TxtFilePath, out string DocInfoPath, out string DocIndex2Path, out string DocParsePagesPath, out string DocParseSegPath, out string DocXMLPath, out string DocConceptParseSeg)
        {
            _ErrorMessage = string.Empty;

            DocInfoPath = string.Empty;
            DocIndex2Path = string.Empty;
            DocParsePagesPath = string.Empty;
            DocParseSegPath = string.Empty;
            DocXMLPath = string.Empty;
            DocConceptParseSeg = string.Empty;

            AppFolders_CA.ProjectName = ProjectName;
            string s = AppFolders_CA.ProjectCurrent;
            AppFolders_CA.AnalysisName = AnalysisName;

            AppFolders_CA.DocName = FileName;

            string projectCurrentDocFolder = TxtFilePath; //AppFolders_CA.AnalysisCurrentDocsDocName;

            //string file = string.Concat(FileName, ".txt");
            //string txtPathFile = Path.Combine(projectCurrentDocFolder, file);

            if (!File.Exists(TxtFilePath))
            {
                _ErrorMessage = string.Concat("Unable to find file: ", TxtFilePath);
                return string.Empty;
            }

            DocInfoPath = AppFolders_CA.AnalysisInfor;
            DocIndex2Path = AppFolders_CA.AnalysisIndex2;
            DocParsePagesPath = AppFolders_CA.DocParsePage;
            DocParseSegPath = AppFolders_CA.DocParsedSec;
            DocXMLPath = AppFolders_CA.AnalysisXML;
            DocConceptParseSeg = AppFolders_CA.AnalysisParseSeg;

            return TxtFilePath;
        }

        public string ProjectFileDetails(string ProjectName, string FileName, bool isDAProject, out string FileNameWExt, out bool Parsed, out string ext)
        {
            _ErrorMessage = string.Empty;

            FileNameWExt = string.Empty;
            Parsed = false;
            ext = string.Empty;

            string fileDetails = string.Empty;

            AppFolders_CA.ProjectName = ProjectName;

            string s = AppFolders_CA.Project;
            string projectFolder = AppFolders_CA.ProjectCurrent;
            string projectDocsFolder = AppFolders_CA.DocPath;

            string[] docs = Directory.GetDirectories(projectDocsFolder);

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
                    string docPath = AppFolders_CA.AnalysisCurrentDocsDocName;
                    string[] files = Directory.GetFiles(docPath, pattern);
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

        public string GetPathFile(string ProjectName, string FileName, bool isDAProject)
        {
            _ErrorMessage = string.Empty;

            AppFolders_CA.ProjectName = ProjectName;

            string s = AppFolders_CA.Project;
            string projectFolder = AppFolders_CA.ProjectCurrent;
            string projectDocsFolder = AppFolders_CA.DocPath;

            string[] docs = Directory.GetDirectories(projectDocsFolder);

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

                    string pattern = string.Concat(FileName, ".txt");
                    string docPath = AppFolders_CA.AnalysisCurrentDocsDocName;
                    string[] files = Directory.GetFiles(docPath, pattern);

                    if (files.Length > 0)
                    {
                        return files[0];
                    }
                    else
                    {
                        return string.Empty;
                    }

                }

            }

            return string.Empty;
        }

        public string GetSearchIndexPath(string ProjectName, string DocumentName)
        {
            _ErrorMessage = string.Empty;

            // Set Folders
            AppFolders_CA.ProjectName = ProjectName;
            AppFolders_CA.DocName = DocumentName;

            string s = AppFolders_CA.Project;
            string projectFolder = AppFolders_CA.ProjectCurrent;
            s = AppFolders_CA.DocPath;

            s = AppFolders_CA.AnalysisCurrentDocsDocName;

            string currentDocIndex = AppFolders_CA.AnalysisInfor;
            if (currentDocIndex == string.Empty)
            {
                _ErrorMessage = AppFolders_CA.ErrorMessage;
                return string.Empty;
            }

            return currentDocIndex;

        }

        public string GetSummaryPathFile(string ProjectName, string DocumentName)
        {
            _ErrorMessage = string.Empty;

            // Set Folders
            AppFolders_CA.ProjectName = ProjectName;
            AppFolders_CA.DocName = DocumentName;

            string s = AppFolders_CA.Project;
            string projectFolder = AppFolders_CA.ProjectCurrent;
            s = AppFolders_CA.DocPath;

            s = AppFolders_CA.AnalysisCurrentDocsDocName;

            string currentDocInfoPath = AppFolders_CA.AnalysisInfor;
            if (currentDocInfoPath == string.Empty)
            {
                _ErrorMessage = AppFolders_CA.ErrorMessage;
                return string.Empty;
            }

            string sumPathFile = Path.Combine(currentDocInfoPath, "SumText.txt");
            if (!File.Exists(sumPathFile))
            {
                _ErrorMessage = string.Concat("Unable to find Summary File: ", sumPathFile);
                return string.Empty;
            }

            return sumPathFile;

        }

        ////////public bool Summary_Generate(string DocPathFile, string SavePath, bool GenerateSum, bool FindConcepts, out List<string> conceptsFound)
        ////////{
        ////////    conceptsFound = null;

        ////////    string txt = Files.ReadFile(DocPathFile);
        ////////    if (txt.Length == 0)
        ////////    {
        ////////        _ErrorMessage = string.Concat("Unable to read document: ", DocPathFile);
        ////////        return false;
        ////////    }
        ////////    return SummarizeText(txt, GenerateSum, FindConcepts, SavePath, out conceptsFound);
        ////////}


        /// <summary>
        /// Analyze Document Text for Summary Information -- Output: ...\Information\SumCommonWords.txt & ...\Information\SumText.txt
        /// </summary>
        /// <param name="strText">Document Text</param>
        ////////public bool SummarizeText(string strText, bool GenerateSum, bool FindConcepts, string SavePath, out List<string> conceptsFound)
        ////////{
        ////////    _ErrorMessage = string.Empty;

        ////////    conceptsFound = null;

        ////////    try 
        ////////    {
        ////////        //int sentCount = 1;
        ////////        //int.TryParse(strSentenceCount, out sentCount);
        ////////        SummarizerArguments sumargs = new SummarizerArguments
        ////////        {
        ////////            DictionaryLanguage = "en",
        ////////            //  DisplayLines = sentCount,
        ////////            DisplayLines = 0,
        ////////            DisplayPercent = 10,
        ////////            InputFile = "",
        ////////            InputString = strText
        ////////        };
        ////////        SummarizedDocument doc = Summarizer.Summarize(sumargs);

        ////////        // Common Words
        ////////        if (FindConcepts)
        ////////        {
        ////////           // conceptsFound = doc.Concepts;
        ////////            List<string> conceptsFoundDirty = doc.Concepts;
        ////////            conceptsFound = CleanCommonConcepts(conceptsFoundDirty); // Added 03.31.2020

        ////////            var concepts = doc.Concepts.Select(p => p + ", ");
        ////////            string commonWords = string.Concat(concepts.ToArray());
        ////////            commonWords = commonWords.Substring(0, commonWords.Length - 2); // remove last ','
        ////////            int index = commonWords.LastIndexOf(',');
        ////////            commonWords = commonWords.Remove(index, 1).Insert(index, " &"); // replace last ',' with '&'
        ////////            string commonWordsFile = Path.Combine(SavePath, "SumCommonWords.txt");
        ////////            Files.WriteStringToFile(commonWords, commonWordsFile, true); // Write Common words to a file
        ////////        }


        ////////        // Summary Text
        ////////        if (GenerateSum)
        ////////        {
        ////////            int item = 1;
        ////////            string lines = string.Empty;
        ////////            string sumText =  Path.Combine(SavePath, "SumText.txt");
        ////////            foreach (string s in doc.Sentences)
        ////////            {
        ////////                lines = lines + string.Concat("[", item.ToString(), "] ", s, "\r\n\r\n");
        ////////                item++;
        ////////            }
        ////////            Files.WriteStringToFile(lines, sumText, true);
        ////////        }

        ////////        // Find Important Words
        ////////        //// tab-character
        ////////        //char tab = '\u0009';
        ////////        //string adjWord = string.Empty;

        ////////        //StringBuilder sbWords = new StringBuilder();
        ////////        //List<string> Words = new List<string>();
        ////////        //bool isDigit = false;
        ////////        //List<string> distinct = doc.ImportantWords.Distinct().ToList();
        ////////        //distinct = distinct.OrderBy(q => q).ToList();
        ////////        //foreach (string word in distinct)
        ////////        //{
        ////////        //    adjWord = word.Replace(tab.ToString(), "");
        ////////        //    adjWord = adjWord.Replace("(s)", "");
        ////////        //    adjWord = adjWord.Replace("(s", "");
        ////////        //    adjWord = adjWord.Replace("'s", "");
        ////////        //    adjWord = adjWord.Replace("'", "");
                  

        ////////        //    isDigit = char.IsDigit(adjWord[0]);

        ////////        //    if (!isDigit)
        ////////        //    {
        ////////        //        if (!isCommonWord(adjWord))
        ////////        //        {
        ////////        //            if (! Domain.Common.DataFunctions.ListItemExists(Words, adjWord))
        ////////        //            {
        ////////        //                if (adjWord.Trim().Length > 3)
        ////////        //                {
        ////////        //                    Words.Add(adjWord);
        ////////        //                    sbWords.AppendLine(adjWord);
        ////////        //                }
        ////////        //            }
        ////////        //        }
        ////////        //    }
        ////////        //}

        ////////        //string importantWordsPathFile = Path.Combine(SavePath, "ImportantWords.txt");
        ////////        //Files.WriteStringToFile(sbWords.ToString(), importantWordsPathFile, true);

                
        ////////    }
        ////////    catch (Exception ex) // Added 09.16.2016
        ////////    {
        ////////        string sumText = Path.Combine(SavePath, "SumError.txt");
        ////////        Files.WriteStringToFile("Error: " + ex.Message, sumText, true);

        ////////        _ErrorMessage = ex.Message;

        ////////        return false;
        ////////    }

        ////////    return true;

        ////////}


        private List<string> CleanCommonConcepts(List<string> conceptsD) // Added 03.31.2020
        {
            if (conceptsD == null ||conceptsD.Count == 0)
                return conceptsD;

            List<string> conceptClean = new List<string>();

            foreach (string concept in conceptsD)
            {
                if (concept.Length > 3)
                    conceptClean.Add(concept);
            }

            return conceptClean;
        }


        private bool isCommonWord(string word)
        {
            if (word.ToLower() == "etc." || word.ToLower() == "i.e." || word.ToLower() == "follow" || word.ToLower() == "after" || word.ToLower() == "within" || word.ToLower() == "used" || word.ToLower() == "and/or" || word.ToLower() == "takes")
                return true;

            return false;
        }

        //TODO;
        //////////public int Parse_Legal(string TxtPathFile, string ParsedSegFolder, string XMLFolder)
        //////////{
        //////////    int count = -1;

        //////////    _ErrorMessage = string.Empty;

        //////////    Domain.RTFBox.RichTextBox  rtfBox = new RTFBox.RichTextBox();
        //////////    Domain.RTFBox.RichTextBox  rtfBoxCopy = new RTFBox.RichTextBox();

        //////////    rtfBox.Text = Files.ReadFile(TxtPathFile);
        //////////    if (rtfBox.Text.Length == 0)
        //////////    {
        //////////        _ErrorMessage = string.Concat("Parse Legal - Unable to read file: ", TxtPathFile);
        //////////        return count;
        //////////    }

        //////////    DomainParse.Parse parser = new DomainParse.Parse(); // Legal Parser Engine

        //////////    // Find Parameters
        //////////    bool result = parser.Analyze_Doc_Parameters(rtfBox, true);
        //////////    DataSet dsParametersFound = parser.ParametersFound;

        //////////    // Write Found Parameters to a file:
        //////////    string ParametersFoundFile = Path.Combine(XMLFolder, "ParametersFound.xml");
        //////////    System.IO.StreamWriter  xmlSW = new System.IO.StreamWriter(ParametersFoundFile);
        //////////    dsParametersFound.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
        //////////    xmlSW.Close();

        //////////    // Parse Document
        //////////    string capTerm = string.Empty;
        //////////    string sMsg = string.Empty;
        //////////    string sUniqueCode = parser.GetUniqueCode(); // UniqueCode Needed?
        //////////    bool bPercent = false;

        //////////    bool parseReturn = parser.Parse4(sUniqueCode, rtfBox, rtfBoxCopy, ParsedSegFolder, ref sMsg, bPercent);

        //////////    // Validate Parsed Results
        //////////    int warningsFound = parser.Validate_Results();
        //////////    DataSet dsValidationResults = parser.ValidationResults;

        //////////    if (warningsFound > 0)
        //////////    {
        //////////        string ParsedResultsValidatedFile = Path.Combine(XMLFolder, "ParsedResultsValidated.xml");
        //////////        xmlSW = new System.IO.StreamWriter(ParsedResultsValidatedFile);
        //////////        dsValidationResults.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
        //////////        xmlSW.Close();
        //////////    }

        //////////    DataSet dsParseResults = parser.ParseResults;
        //////////    count = dsParseResults.Tables[0].Rows.Count;
        //////////    string ParseResultsFile = Path.Combine(XMLFolder, ParseResultsFields.XMLFile);

        //////////    // Save Parse Results Dataset as an XML file
        //////////    GenericDataManger gDataMgr = new GenericDataManger();
        //////////    gDataMgr.SaveDataXML(dsParseResults, ParseResultsFile);

        //////////    if (parser != null) // Added to free memory. 
        //////////    {
        //////////        parser = null;
        //////////        gDataMgr = null;

        //////////        GC.Collect();
        //////////        GC.WaitForPendingFinalizers();

        //////////    }

        //////////    return count;
        //////////}


        public int Parse_Paragraph(string TxtPathFile, string ParsedSegFolder, string XMLFolder)
        {
            int count = -1;

            _ErrorMessage = string.Empty;

            ParagraphParseEng parser = new ParagraphParseEng();

            count = parser.Parse3(TxtPathFile, ParsedSegFolder, XMLFolder);

            return count;
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

        public bool ParseResults_CheckFixNewCols(string XMLFolder)
        {
            _ErrorMessage = string.Empty;

            string xmlParseSegFile = ParseResultsFields.XMLFile;
            string xmlParseSegPathFile = Path.Combine(XMLFolder, xmlParseSegFile);

 

            if (!File.Exists(xmlParseSegPathFile))
            {
                _ErrorMessage = string.Concat("Parse Results XML file was not found: ", xmlParseSegPathFile);
                return false;
            }

            DataSet dsParseSeg = Files.LoadDatasetFromXml(xmlParseSegPathFile);

            if (dsParseSeg == null)
            {
                _ErrorMessage = string.Concat("Unable to read Parse Results XML file: ", xmlParseSegPathFile, "   Error: ", Files.ErrorMessage);
                return false;
            }

            // Check for PageSource column
            if (!dsParseSeg.Tables[0].Columns.Contains(ParseResultsFields.PageSource))
            {
                dsParseSeg.Tables[0].Columns.Add(ParseResultsFields.PageSource, typeof(System.String));
            }

            // Check for Concepts column
            if (!dsParseSeg.Tables[0].Columns.Contains(ParseResultsFields.ConceptsWords))
            {
                dsParseSeg.Tables[0].Columns.Add(ParseResultsFields.ConceptsWords, typeof(System.String));
            }

            // Check for Dictionary column
            if (!dsParseSeg.Tables[0].Columns.Contains(ParseResultsFields.Dictionary))
            {
                dsParseSeg.Tables[0].Columns.Add(ParseResultsFields.Dictionary, typeof(System.String));
            }

            // Check for Dictionary Category column
            if (!dsParseSeg.Tables[0].Columns.Contains(ParseResultsFields.DictionaryCategory))
            {
                dsParseSeg.Tables[0].Columns.Add(ParseResultsFields.DictionaryCategory, typeof(System.String));
            }

            // Check for Dictionary Items column
            if (!dsParseSeg.Tables[0].Columns.Contains(ParseResultsFields.DictionaryItems))
            {
                dsParseSeg.Tables[0].Columns.Add(ParseResultsFields.DictionaryItems, typeof(System.String));
            }

            // Check for Dictionary Definitions column
            if (!dsParseSeg.Tables[0].Columns.Contains(ParseResultsFields.DictionaryDefinitions))
            {
                dsParseSeg.Tables[0].Columns.Add(ParseResultsFields.DictionaryDefinitions, typeof(System.String));
            }

            // Check for Weighted Value Unique Total column
            if (!dsParseSeg.Tables[0].Columns.Contains(ParseResultsFields.Weight))
            {
                dsParseSeg.Tables[0].Columns.Add(ParseResultsFields.Weight, typeof(System.Double));
            }

            GenericDataManger gdManager = new GenericDataManger();

            gdManager.SaveDataXML(dsParseSeg, xmlParseSegPathFile);

            return true;

        }

        public bool MapParseSeg2Pages(string ParsePageFolder, string ParsedSegFolder, string XMLFolder)
        {
             _ErrorMessage = string.Empty;

             string[] pagefiles = Directory.GetFiles(ParsePageFolder, "*.txt"); // Page sources are only for Docx && pdfs
             if (pagefiles.Length == 0)
             {
                 return true;
             }


            DataTable dtMappingTable =  createTable_PageMapping();

            string xmlParseSegFile = ParseResultsFields.XMLFile;
            string xmlParseSegPathFile = Path.Combine(XMLFolder, xmlParseSegFile);

            if (!File.Exists(xmlParseSegPathFile))
            {
                _ErrorMessage = string.Concat("Parse Results XML file was not found: ", xmlParseSegPathFile);
                return false;
            }

            DataSet dsParseSeg = Files.LoadDatasetFromXml(xmlParseSegPathFile);
            if (dsParseSeg == null)
            {
                _ErrorMessage = string.Concat("Unable to read Parse Results XML file: ", xmlParseSegPathFile, "   Error: ", Files.ErrorMessage);
                return false;
            }

            int uid = -1;
            int currentPageNo = -1;
            int previousPageNo = -1;
            int lastPageNo = -1;
            bool firstPgIsZero = false;

            ////Domain.RTFBox.RichTextBox rtfBox = new RTFBox.RichTextBox();

            string file = string.Empty;
            string pathFile = string.Empty;
            string parseText = string.Empty;

            string pageText = string.Empty;

            string pageFile = "0.txt";
            string pagePathFile = Path.Combine(ParsedSegFolder, pageFile);
            if (File.Exists(pagePathFile))
            {
                firstPgIsZero = true;
                currentPageNo = 0;
            }
            else
            {
                firstPgIsZero = false;
                currentPageNo = 1;
            }

            string[] pageFiles = Directory.GetFiles(ParsePageFolder, "*.txt", SearchOption.TopDirectoryOnly);
            lastPageNo = pageFiles.Length;
            if (firstPgIsZero)
                lastPageNo = lastPageNo - 1;

            int i = 0;
        //    int lineQty = 0;
            bool foundPage = false;

            int parsedFilesQty = dsParseSeg.Tables[0].Rows.Count;

            int page1stLoop = currentPageNo;

            foreach (DataRow row in dsParseSeg.Tables[0].Rows)
            {
                uid = Convert.ToInt32(row[ParseResultsFields.UID].ToString()) ;

                file = string.Concat(uid, ".rtf");
                pathFile = Path.Combine(ParsedSegFolder, file);

                if (File.Exists(pathFile))
                {
                    ////////rtfBox.LoadFile(pathFile);
                    ////////parseText = rtfBox.Text;

                    //string[] parsedSentences = Regex.Split(parseText, @"(?<=[\.!\?])\s+");
                    string[] parsedSentences = parseText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                    if (parsedSentences.Length > 0)
                    {
                        page1stLoop = currentPageNo;
                        foundPage = false;

                        do
                        {
                            file = string.Concat(currentPageNo, ".txt");
                            pathFile = Path.Combine(ParsePageFolder, file);
                            if (File.Exists(pathFile))
                            {
                                pageText = Files.ReadFile(pathFile);

                                for (int x = 0; x < parsedSentences.Length; x++)
                                {
                                    if (parsedSentences[x].Trim().Length > 0)
                                    {
                                        if (pageText.Contains(parsedSentences[x]))
                                        {
                                            if (firstPgIsZero)
                                            {
                                                row[ParseResultsFields.PageSource] = currentPageNo + 1;
                                                dsParseSeg.Tables[0].AcceptChanges();
                                                i++;
                                            }
                                            else
                                            {
                                                row[ParseResultsFields.PageSource] = currentPageNo;
                                                dsParseSeg.Tables[0].AcceptChanges();
                                                i++;
                                            }
                                            foundPage = true;
                                            break;
                                        }
                                    }
                                }

                                if (!foundPage)
                                {
                                    previousPageNo = currentPageNo;
                                    currentPageNo++;

                                    if (currentPageNo > lastPageNo)
                                    {
                                        currentPageNo = page1stLoop; // reset an try for the next parse segment

                                        parsedSentences = Regex.Split(parseText, @"(?<=[\.!\?])\s+");

                                        for (int x = 0; x < parsedSentences.Length; x++)
                                        {
                                            if (parsedSentences[x].Trim().Length > 0)
                                            {
                                                if (pageText.Contains(parsedSentences[x]))
                                                {
                                                    if (firstPgIsZero)
                                                    {
                                                        row[ParseResultsFields.PageSource] = currentPageNo + 1;
                                                        dsParseSeg.Tables[0].AcceptChanges();
                                                        i++;
                                                    }
                                                    else
                                                    {
                                                        row[ParseResultsFields.PageSource] = currentPageNo;
                                                        dsParseSeg.Tables[0].AcceptChanges();
                                                        i++;
                                                    }
                                                    foundPage = true;
                                                    break;
                                                }
                                            }
                                        }

                                        if (!foundPage)
                                        {
                                            if (currentPageNo > lastPageNo)
                                            {
                                                currentPageNo = page1stLoop; // reset an try for the next parse segment

                                                row[ParseResultsFields.PageSource] = -1;
                                                dsParseSeg.Tables[0].AcceptChanges();
                                                foundPage = true;
                                                break;
                                            }
                                        }
                                    }
                                }
 
                            }

                            //if (currentPageNo > lastPageNo)
                            //{
                            //    break;
                            //}

                        } while (!foundPage);
                    }
                }

                GenericDataManger gdManager = new GenericDataManger();

                gdManager.SaveDataXML(dsParseSeg, xmlParseSegPathFile);
                

            }

            if (i == parsedFilesQty)
            {
                return true;
            }
            else
            {
                if (i < parsedFilesQty)
                {
                    int NotFoundQty = parsedFilesQty - i;
                    _ErrorMessage = string.Concat(NotFoundQty.ToString(), " parsed segments/paragraphs were not able to identify their source pages.");
                }

                return false;
            }

        }

        public bool MapParseSeg3Pages(string ParsePageFolder, string ParsedSegFolder, string XMLFolder)
        {
            _ErrorMessage = string.Empty;

            string[] pagefiles = Directory.GetFiles(ParsePageFolder, "*.txt"); // Page sources are only for Docx && pdfs
            if (pagefiles.Length == 0)
            {
                return true;
            }


            DataTable dtMappingTable = createTable_PageMapping();

            string xmlParseSegFile = ParseResultsFields.XMLFile;
            string xmlParseSegPathFile = Path.Combine(XMLFolder, xmlParseSegFile);

            if (!File.Exists(xmlParseSegPathFile))
            {
                _ErrorMessage = string.Concat("Parse Results XML file was not found: ", xmlParseSegPathFile);
                return false;
            }

            DataSet dsParseSeg = Files.LoadDatasetFromXml(xmlParseSegPathFile);
            if (dsParseSeg == null)
            {
                _ErrorMessage = string.Concat("Unable to read Parse Results XML file: ", xmlParseSegPathFile, "   Error: ", Files.ErrorMessage);
                return false;
            }

            int uid = -1;
            int currentPageNo = -1;
            //int foundPageNo = 0;
            //int previousPageNo = -1;
            int lastPageNo = -1;
            bool firstPgIsZero = false;

            ////////Domain.RTFBox.RichTextBox rtfBox = new RTFBox.RichTextBox();

            string file = string.Empty;
            string pathFile = string.Empty;
            string parseText = string.Empty;

            string[] textLines;
            string pageText = string.Empty;

            string pageFile = "0.txt";
            string pagePathFile = Path.Combine(ParsedSegFolder, pageFile);
            if (File.Exists(pagePathFile))
            {
                firstPgIsZero = true;
                currentPageNo = 0;
            }
            else
            {
                firstPgIsZero = false;
                currentPageNo = 1;
            }

            string[] pageFiles = Directory.GetFiles(ParsePageFolder, "*.txt", SearchOption.TopDirectoryOnly);
            lastPageNo = pageFiles.Length;
            if (firstPgIsZero)
                lastPageNo = lastPageNo - 1;

            int i = 0;
            //    int lineQty = 0;
            bool foundPage = false;

            int parsedFilesQty = dsParseSeg.Tables[0].Rows.Count;

            // Loop through parsed segments
            foreach (DataRow row in dsParseSeg.Tables[0].Rows)
            {
                uid = Convert.ToInt32(row[ParseResultsFields.UID].ToString());

                file = string.Concat(uid, ".rtf");
                pathFile = Path.Combine(ParsedSegFolder, file);

                if (File.Exists(pathFile))
                {
                    //////////rtfBox.LoadFile(pathFile);
                    //////////parseText = rtfBox.Text;

                    //string[] parsedSentences = Regex.Split(parseText, @"(?<=[\.!\?])\s+");
                    //string[] parsedSentences = parseText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                    string[] segLines = parseText.Split(System.Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                    foundPage = false;
                    // Loop though segment lines
                    foreach (string segLine in segLines)
                    {
                        // Loop through pages
                        for (int y = currentPageNo; y < lastPageNo; y++)
                        {
                            pageText = Files.ReadFile(pageFiles[y]);
                            if (pageText.Contains(segLine))
                            {
                                if (firstPgIsZero)
                                {
                                    row[ParseResultsFields.PageSource] = y + 1;
                                }
                                else
                                {
                                    row[ParseResultsFields.PageSource] = y;
                                }

                                dsParseSeg.Tables[0].AcceptChanges();
                                i++;
                                foundPage = true;
                                currentPageNo = y;
                                break;
                            }

                            if (foundPage)
                            {
                                break;
                            }
                            else
                            {
                              //  currentPageNo = foundPageNo; // Reset current page to the last found item
                                for (int z = currentPageNo; z < lastPageNo; z++)
                                {
                                    
                                    textLines = Files.ReadFile2Array(pageFiles[z]);
                                    if (textLines.Contains(segLine))
                                    {
                                        if (firstPgIsZero)
                                        {
                                            row[ParseResultsFields.PageSource] = z + 1;  
                                        }
                                        else
                                        {
                                            row[ParseResultsFields.PageSource] = z;
                                        }

                                        dsParseSeg.Tables[0].AcceptChanges();
                                        i++;
                                        foundPage = true;
                                        currentPageNo = z;
                                        break;
                                    }

                                }

                            }

                        }     
                    }

                }
            }

            GenericDataManger gdManager = new GenericDataManger();

            gdManager.SaveDataXML(dsParseSeg, xmlParseSegPathFile);

            if (i == parsedFilesQty)
            {
                return true;
            }
            else
            {
                if (i < parsedFilesQty)
                {
                    int NotFoundQty = parsedFilesQty - i;
                    _ErrorMessage = string.Concat(NotFoundQty.ToString(), " parsed segments/paragraphs were not able to identify their source pages.");
                }

                return false;
            }

        }


        public bool MapParseSeg4Pages(string ParsePageFolder, string ParsedSegFolder, string XMLFolder)
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

            DataSet dsParseSeg = Files.LoadDatasetFromXml(xmlParseSegPathFile);
            if (dsParseSeg == null)
            {
                _ErrorMessage = string.Concat("Unable to read Parse Results XML file: ", xmlParseSegPathFile, "   Error: ", Files.ErrorMessage);
                return false;
            }

            int uid = -1;
            int currentPageNo = -1;
 
            bool firstPgIsZero = false;

           //////// Domain.RTFBox.RichTextBox rtfBox = new RTFBox.RichTextBox();

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

            // Loop through parsed segments
            i = 0;
            int notFoundPageQty = 0;
            foreach (DataRow row in dsParseSeg.Tables[0].Rows)
            {
                uid = Convert.ToInt32(row[ParseResultsFields.UID].ToString());
                startLine = Convert.ToInt32(row[ParseResultsFields.LineStart].ToString());
                
                currentPageNo = FindPageSource(dtPageScope, startLine, firstPgIsZero);

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
 



    }
}
