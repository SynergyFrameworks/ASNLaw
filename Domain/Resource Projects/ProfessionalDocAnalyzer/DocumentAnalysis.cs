using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Data;
using System.Text.RegularExpressions;

using Atebion.Common;
using Atebion.Import;
//using OpenTextSummarizer;

namespace ProfessionalDocAnalyzer
{
    public class DocumentAnalysis
    {
        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        public int Parse_Legal(string TxtPathFile, string ParsedSegFolder, string XMLFolder)
        {
            int count = -1;

            _ErrorMessage = string.Empty;
            
            Atebion.RTFBox.RichTextBox rtfBox = new Atebion.RTFBox.RichTextBox();
            Atebion.RTFBox.RichTextBox rtfBoxCopy = new Atebion.RTFBox.RichTextBox();

            rtfBox.Text = Atebion.Common.Files.ReadFile(TxtPathFile);
            if (rtfBox.Text.Length == 0)
            {
                _ErrorMessage = string.Concat("Parse Legal - Unable to read file: ", TxtPathFile);
                return count;
            }

            AtebionParse.Parse parser = new AtebionParse.Parse(); // Legal Parser Engine

            // Find Parameters
            bool result = parser.Analyze_Doc_Parameters(rtfBox, true);
            DataSet dsParametersFound = parser.ParametersFound;

            // Write Found Parameters to a file:
            string ParametersFoundFile = Path.Combine(XMLFolder, "ParametersFound.xml");
            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(ParametersFoundFile);
            dsParametersFound.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
            xmlSW.Close();

            // Parse Document
            string capTerm = string.Empty;
            string sMsg = string.Empty;
            string sUniqueCode = parser.GetUniqueCode(); // UniqueCode Needed?
            bool bPercent = false;

            bool parseReturn = parser.Parse4(sUniqueCode, rtfBox, rtfBoxCopy, ParsedSegFolder, ref sMsg, bPercent);

            // Validate Parsed Results
            int warningsFound = parser.Validate_Results();
            DataSet dsValidationResults = parser.ValidationResults;

            if (warningsFound > 0)
            {
                string ParsedResultsValidatedFile = Path.Combine(XMLFolder, "ParsedResultsValidated.xml");
                xmlSW = new System.IO.StreamWriter(ParsedResultsValidatedFile);
                dsValidationResults.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                xmlSW.Close();
            }

            DataSet dsParseResults = parser.ParseResults;
            count = dsParseResults.Tables[0].Rows.Count;
            string ParseResultsFile = Path.Combine(XMLFolder, "ParseResults.xml");

            // Save Parse Results Dataset as an XML file
            GenericDataManger gDataMgr = new GenericDataManger();
            gDataMgr.SaveDataXML(dsParseResults, ParseResultsFile);

            if (parser != null) // Added to free memory. 
            {
                parser = null;
                gDataMgr = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();

            }

            return count;
        }


        public int Parse_Paragraph(string TxtPathFile, string ParsedSegFolder, string XMLFolder)
        {
            int count = -1;

            _ErrorMessage = string.Empty;

            ParagraphParseEng parser = new ParagraphParseEng();

            count = parser.Parse4(TxtPathFile, ParsedSegFolder, XMLFolder);

            return count;
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

        /// <summary>
        /// Use to find text in a document per page -- use for splits
        /// </summary>
        /// <param name="StartPageSearch">1st page to start search</param>
        /// <param name="EndPageSearch">Last page to search</param>
        /// <param name="TextualContent">Text to seach for in page(s)</param>
        /// <returns>Returns Page Number</returns>
        public int FindTextPageSource(string ParsePageFolder, int StartPageSearch, int EndPageSearch, string TextualContent) // Added 9.26.2018
        {
            string[] pagefiles = Directory.GetFiles(ParsePageFolder, "*.txt"); // Page sources are only for Docx && pdfs
            if (pagefiles.Length == 0)
            {
                return -1;
            }

            if (StartPageSearch == EndPageSearch)
                return StartPageSearch;


            string[] sourceLines = TextualContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            string pagePathFile = string.Empty;
            string file = string.Empty;
            string pageText = string.Empty;

            for (int i = StartPageSearch; i < (EndPageSearch + 1); i++)
            {
                file = string.Concat(i.ToString(), ".txt");
                pagePathFile = Path.Combine(ParsePageFolder, file);

                if (!File.Exists(pagePathFile))
                {
                    return -1; // Not Found !!!
                }

                pageText = Atebion.Common.Files.ReadFile(pagePathFile);

                if (pageText.IndexOf(TextualContent) > -1)
                {
                    return i; // return page number
                }

                string[] sentences = Regex.Split(pageText, @"(?<=[\.!\?])\s+");
             //   string[] lines = pageText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (string sentence in sentences)
                {
                    if (sentence.Trim().Length > 0)
                    {
                        if (pageText.IndexOf(sentence) > -1)
                        {
                            return i; // return page number
                        }

                        //foreach (string sourceLine in sourceLines)
                        //{
                        //    if (sourceLine.Trim().Length > 0)
                        //    {
                        //        if (sentence.IndexOf(sourceLine) > -1)
                        //        {
                        //            return i; // return page number
                        //        }
                        //    }
                        //} //loop through lines of the source text (input text)
                    }
                } // loop through lines of the current page
            }

            return -1; // Not found!
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

        public bool InsertPageSource2ParseResultsTable(string XMLFolder)
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
 



    }

    
}
