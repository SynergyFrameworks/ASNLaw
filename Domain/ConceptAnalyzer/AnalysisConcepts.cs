using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
//using OfficeOpenXml;
using System.Xml;
using System.Drawing;
using System.Text.RegularExpressions;

using Domain.Common;
//using Domain.Dictionary;


namespace Domain.ConceptAnalyzer
{
    public class AnalysisConcepts
    {
        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        //private RichTextBox _rtfCrtl = new RichTextBox();


        public DataTable Create_EmptyConceptSum(List<string> Concepts)
        {
            DataTable table = new DataTable(ConceptsDocSum.TableName);
            table.Columns.Add(ConceptsDocSum.UID, typeof(int));
            table.Columns.Add(ConceptsDocSum.Select, typeof(bool));
            table.Columns.Add(ConceptsDocSum.Concept, typeof(string));
            table.Columns.Add(ConceptsDocSum.SegmentSumUIDs, typeof(string));
            table.Columns.Add(ConceptsDocSum.Count, typeof(int));

            int i = 0;
            Concepts.Sort();
            foreach (string concept in Concepts)
            {
                DataRow row = table.NewRow();

                row[ConceptsDocSum.UID] = i;
                row[ConceptsDocSum.Select] = true;
                row[ConceptsDocSum.Concept] = concept;
                row[ConceptsDocSum.SegmentSumUIDs] = string.Empty;
                row[ConceptsDocSum.Count] = 0;

                table.Rows.Add(row);

                table.AcceptChanges();

                i++;

            }

            return table;
        }

        private DataTable createTable()
        {
            DataTable table = new DataTable(ConceptsResultsFields.TableName);

            table.Columns.Add(ConceptsResultsFields.UID, typeof(string));
            table.Columns.Add(ConceptsResultsFields.SortOrder, typeof(int));
            table.Columns.Add(ConceptsResultsFields.FileName, typeof(string));
            table.Columns.Add(ConceptsResultsFields.Number, typeof(string));
            table.Columns.Add(ConceptsResultsFields.Caption, typeof(string));
            table.Columns.Add(ConceptsResultsFields.PageSource, typeof(string));
            table.Columns.Add(ConceptsResultsFields.ConceptsWords, typeof(string));
           
            return table;

        }

        public DataSet CreateEmpty_DocsConceptsAnalysisSummary_DataTable(string[] docs)
        {
            _ErrorMessage = string.Empty;

            if (docs.Length == 0)
            {
                _ErrorMessage = "No Documents have been defined.";
                return null;
            }
            DataSet dsDocsDicAnalysis = new System.Data.DataSet();


            DataTable table = new DataTable(DocsConceptsAnalysisFieldConst.TableName);

            table.Columns.Add(DocsConceptsAnalysisFieldConst.UID, typeof(int));

            table.Columns.Add(DocsConceptsAnalysisFieldConst.Concept, typeof(string));
            table.Columns.Add(DocsConceptsAnalysisFieldConst.CountTotal, typeof(int));
  
            string countField = string.Empty;
            

            int x = 0;
            foreach (string doc in docs)
            {

                // countField = DefineField(DocsDictionariesAnalysisFieldConst.Count_, doc);
                countField = string.Concat(DocsConceptsAnalysisFieldConst.Count_, x.ToString());
                table.Columns.Add(countField, typeof(int));

                x++;
            }

            dsDocsDicAnalysis.Tables.Add(table);

            return dsDocsDicAnalysis;

        }

        public DataSet CreateEmpty_ConceptAnalysisSummary_DataTable()
        {

            DataSet dsConceptAnalysis = new System.Data.DataSet();


            DataTable table = new DataTable(DocsConceptsAnalysisFieldConst.FilterTableName);

            table.Columns.Add(DocsConceptsAnalysisFieldConst.UID, typeof(int));
            table.Columns.Add(DocsConceptsAnalysisFieldConst.Select, typeof(bool));
            table.Columns.Add(DocsConceptsAnalysisFieldConst.SegmentSumUIDs, typeof(string));
            table.Columns.Add(DocsConceptsAnalysisFieldConst.Concept, typeof(string));
            table.Columns.Add(DocsConceptsAnalysisFieldConst.Count, typeof(int));


            dsConceptAnalysis.Tables.Add(table);

            return dsConceptAnalysis;

        }

        private bool InsertDocData_DocsConceptsAnalysisSum(DataSet dsDocsConceptsAnalysisSum, string concept, int count, string x)
        {
            string searchExpression = string.Concat(DocsConceptsAnalysisFieldConst.Concept, " = '", concept, "'");
            DataRow[] row = dsDocsConceptsAnalysisSum.Tables[0].Select(searchExpression);

            if (searchExpression.Length == 0)
            {
                return false;
            }

            string fieldName_Count = string.Concat(DocsConceptsAnalysisFieldConst.Count_, x);
            row[0][fieldName_Count] = count;
            

            dsDocsConceptsAnalysisSum.Tables[0].AcceptChanges();

            return true;

        }

        public DataSet Get_Documents_Concept_Summary(string ProjectName, string AnalysisName, string[] Docs, List<string> Concepts, out string SumXRefPathFile, out DataSet dsConceptDocsSum4Filtering)
        {
            _ErrorMessage = string.Empty;
            SumXRefPathFile = string.Empty;

            dsConceptDocsSum4Filtering = null;

            if (Docs.Length == 0)
            {
                _ErrorMessage = "Documents have not been defined.";
                return null;
            }

            AppFolders_CA.ProjectName = ProjectName;
            string s = AppFolders_CA.Project;
            string projectFolder = AppFolders_CA.ProjectCurrent;

            AppFolders_CA.AnalysisName = AnalysisName;

            string analysisFolder = AppFolders_CA.AnalysisCurrent;


            DataSet dsDocsConceptsAnalysisSum;

            string totalConceptsSumPathFile = Path.Combine(analysisFolder, DocsConceptsAnalysisFieldConst.xFilterFile);

            string docsConceptsAnalysisSumPathFile = Path.Combine(analysisFolder, DocsConceptsAnalysisFieldConst.XMLFile);
            if (File.Exists(docsConceptsAnalysisSumPathFile))
            {
                dsDocsConceptsAnalysisSum = Files.LoadDatasetFromXml(docsConceptsAnalysisSumPathFile);
                dsConceptDocsSum4Filtering = Files.LoadDatasetFromXml(totalConceptsSumPathFile);

                _ErrorMessage = Files.ErrorMessage;
                return dsDocsConceptsAnalysisSum;
            }

            dsDocsConceptsAnalysisSum = CreateEmpty_DocsConceptsAnalysisSummary_DataTable(Docs);
            dsConceptDocsSum4Filtering = CreateEmpty_ConceptAnalysisSummary_DataTable();


            DataRow newRow;
            int i = 0;

            // Populate with Concepts
            foreach (string concept in Concepts)
            {
                newRow = dsDocsConceptsAnalysisSum.Tables[0].NewRow();
                newRow[DocsConceptsAnalysisFieldConst.UID] = i;
                newRow[DocsConceptsAnalysisFieldConst.Concept] = concept;

                dsDocsConceptsAnalysisSum.Tables[0].Rows.Add(newRow);
                dsDocsConceptsAnalysisSum.AcceptChanges();


                newRow = dsConceptDocsSum4Filtering.Tables[0].NewRow();
                newRow[DocsConceptsAnalysisFieldConst.UID] = i;
                newRow[DocsConceptsAnalysisFieldConst.Select] = true;
                newRow[DocsConceptsAnalysisFieldConst.Concept] = concept;
                newRow[DocsConceptsAnalysisFieldConst.Count] = 0;

                dsConceptDocsSum4Filtering.Tables[0].Rows.Add(newRow);
                dsConceptDocsSum4Filtering.AcceptChanges();

                i++;
            }

            string analysisDocFolder = string.Empty;
            string analysisDocXMLFolder = string.Empty;
            string pathFile_ConceptsProjectSummary = string.Empty;

            string projectDocFolder = string.Empty;
            string projectDocXMLFolder = string.Empty;

            DataSet dsConceptAnalysis;

            string conceptX = string.Empty;
            int countX = 0;

            StringBuilder sb = new StringBuilder();
            int x = 0;
            foreach (string doc in Docs)
            {
                AppFolders_CA.DocName = doc;
                analysisDocFolder = AppFolders_CA.AnalysisCurrentDocsDocName;
                analysisDocXMLFolder = AppFolders_CA.AnalysisParseSegXML;

                projectDocFolder = AppFolders_CA.AnalysisCurrentDocsDocName;
                projectDocXMLFolder = AppFolders_CA.AnalysisXML;

                if (projectDocXMLFolder == string.Empty)
                {
                    sb.AppendLine(string.Concat("Unable to find the Project Document XML folder: ", projectDocXMLFolder));
                }
                else
                {
                    pathFile_ConceptsProjectSummary = Path.Combine(analysisDocXMLFolder, ConceptsDocSum.xmlFile);
                    //if (!File.Exists(pathFile_ConceptsProjectSummary))
                    //{
                        
                    //    dsConceptAnalysis = Get_Document_Concept_AnalysisResults(ProjectName, doc, );
                    //    // sb.AppendLine(string.Concat("Unable to find Dictionary Found Items Summary file: ", pathFile_DicAnalysisSummary));
                    //}
                    //else
                    //{
                        dsConceptAnalysis = Files.LoadDatasetFromXml(pathFile_ConceptsProjectSummary);
                    //}

                    if (dsConceptAnalysis == null)
                    {
                        sb.AppendLine(string.Concat("Unable to load Document Concepts Found Summary file: ", pathFile_ConceptsProjectSummary));
                    }
                    else
                    {
                        if (dsConceptAnalysis.Tables.Count == 0)
                        {
                           _ErrorMessage = string.Concat("No Data Table Found -- Unable to populate Documents Concepts Summary for Document: ", doc, " with Concept: ", conceptX);
                           return null;
                        }

                        if (dsConceptAnalysis.Tables[0].Rows.Count == 0)
                        {
                            _ErrorMessage = string.Concat("No Data Rows were Found -- Unable to populate Documents Concepts Summary for Document: ", doc, " with Concept: ", conceptX);
                            return null;
                        }
                        
                        foreach (DataRow rowConceptAnalysis in dsConceptAnalysis.Tables[0].Rows)
                        {
                            conceptX = rowConceptAnalysis[DocsConceptsAnalysisFieldConst.Concept].ToString();
                            countX  = Convert.ToInt32(rowConceptAnalysis[DocsConceptsAnalysisFieldConst.Count].ToString());
                           
                            // Insert Summary Document Data into Documents Summary
                            if (!InsertDocData_DocsConceptsAnalysisSum(dsDocsConceptsAnalysisSum, conceptX, countX, x.ToString())) 
                            {
                                sb.AppendLine(string.Concat("Unable to populate Documents Concepts Summary for Document: ", doc, " with Concept: ", conceptX));
                            }                   

                        } // Loop though each Document Summary Item

                    }

                }

                x++;
            } // Loop though each Doocument


            // Summary Filtering DataSet -- Insert Total count for each Concept
            int docCount = Docs.Length;
            int qty = 0;
            int uid = -1;
            string countField = string.Empty;
            foreach (DataRow row in dsDocsConceptsAnalysisSum.Tables[0].Rows)
            {
                qty = 0;
                uid = Convert.ToInt32(row[DocsConceptsAnalysisFieldConst.UID].ToString());
                for (int z = 0; z < docCount; z++)
                {
                    countField = string.Concat("Count_", z.ToString());

                    if (row[countField].ToString() != string.Empty)
                        qty = Convert.ToInt32(row[countField].ToString()) + qty;

                }

                dsConceptDocsSum4Filtering.Tables[0].Rows[uid][DocsConceptsAnalysisFieldConst.Count] = qty;
                dsConceptDocsSum4Filtering.AcceptChanges();

                row[DocsConceptsAnalysisFieldConst.CountTotal] = qty;

                dsDocsConceptsAnalysisSum.Tables[0].AcceptChanges();

            }

            GenericDataManger gdManager = new GenericDataManger();

            string pathFileConceptsAnalysisSum = Path.Combine(analysisFolder, DocsConceptsAnalysisFieldConst.XMLFile);
            gdManager.SaveDataXML(dsDocsConceptsAnalysisSum, pathFileConceptsAnalysisSum);

            string pathFileConceptDocsSum4Filtering = Path.Combine(analysisFolder, DocsConceptsAnalysisFieldConst.xFilterFile);
            gdManager.SaveDataXML(dsConceptDocsSum4Filtering, pathFileConceptDocsSum4Filtering);


            int y = 0;
            StringBuilder sbXRef = new StringBuilder();
            string value = string.Empty;
            foreach (string doc in Docs)
            {
                value = string.Concat(y.ToString(), "=", doc);
                sbXRef.AppendLine(value);
                y++;
            }

            SumXRefPathFile = Path.Combine(analysisFolder, DocsConceptsAnalysisFieldConst.xRefFile);
            Files.WriteStringToFile(sbXRef.ToString(), SumXRefPathFile);

            return dsDocsConceptsAnalysisSum;

  
        }

 

        public DataSet Get_Document_Concept_Summary(string ProjectName, string AnalysisName, string DocumentName)
        {
            _ErrorMessage = string.Empty;

            // Set Folders
            AppFolders_CA.ProjectName = ProjectName;
            string s = AppFolders_CA.Project;
            string projectFolder = AppFolders_CA.ProjectCurrent;

            AppFolders_CA.AnalysisName = AnalysisName;
            s = AppFolders_CA.AnalysisCurrent;

            string projectDocsFolder = AppFolders_CA.DocPath;

            AppFolders_CA.DocName = DocumentName;
            
            string projectDocFolder = AppFolders_CA.AnalysisCurrentDocsDocName;

            string projectDocXMLFolder = AppFolders_CA.AnalysisParseSegXML;

            if (projectDocXMLFolder.Length == 0)
            {
                _ErrorMessage = string.Concat("Unable to find document ", DocumentName,"'s XML folder.");
                return null;
            }

            string DocXMLFile = Path.Combine(projectDocXMLFolder, ConceptsDocSum.xmlFile);

            if (!File.Exists(DocXMLFile))
            {
                _ErrorMessage = string.Concat("Unable to find document ", DocumentName, "s Concept Summary XML file: ", DocXMLFile);
                return null;
            }

            return Files.LoadDatasetFromXml(DocXMLFile);


        }

        public DataSet Get_Document_Concept_AnalysisResults(string ProjectName, string AnalysisName, string DocumentName, out string ConceptParseSegPath, out string ProjectResultsNotesPath)
        {
            _ErrorMessage = string.Empty;

            // Set Folders
            AppFolders_CA.ProjectName = ProjectName;
            string s = AppFolders_CA.Project;
            string projectFolder = AppFolders_CA.ProjectCurrent;

            AppFolders_CA.AnalysisName = AnalysisName;
            s = AppFolders_CA.AnalysisCurrent;

            s = AppFolders_CA.DocPath;

            AppFolders_CA.DocName = DocumentName;

            string projectDocFolder = AppFolders_CA.AnalysisCurrentDocsDocName;

            ProjectResultsNotesPath = AppFolders_CA.AnalysisNotes;

            ConceptParseSegPath = AppFolders_CA.AnalysisParseSeg;

          //  /.AnalysisNotes

            string ProjectDocXMLPath = AppFolders_CA.AnalysisParseSegXML;
            if (ProjectDocXMLPath == string.Empty)
            {
                _ErrorMessage = string.Concat("Unable to find the Project Document XML folder: ", ProjectDocXMLPath);
                return null;
            }

            string pathConceptResultsFile = Path.Combine(ProjectDocXMLPath, ConceptsResultsFields.XMLFile);
            if (!File.Exists(pathConceptResultsFile))
            {
                _ErrorMessage = string.Concat("Unble to find the Concepts Analysis Results file: ", pathConceptResultsFile);
                return null;
            }

            DataSet ds = Files.LoadDatasetFromXml(pathConceptResultsFile);

            return ds;

        }

        private void ConceptSummary_Update(ref DataTable table, string concept, int count, string parseSegUID)
        {
            string uid = string.Empty;

            foreach (DataRow row in table.Rows)
            {
                if (row[ConceptsDocSum.Concept].ToString().ToLower() == concept.ToLower())
                {
                    row[ConceptsDocSum.Count] = Convert.ToInt32(row[ConceptsDocSum.Count].ToString()) + count;
                    uid = row[ConceptsDocSum.SegmentSumUIDs].ToString();
                    if (uid.Length == 0)
                    {
                        row[ConceptsDocSum.SegmentSumUIDs] = parseSegUID;
                    }
                    else
                    {
                        row[ConceptsDocSum.SegmentSumUIDs] = string.Concat(uid, ", ", parseSegUID);
                    }

                    table.AcceptChanges();

                    return;
                }

            }
        }

        public bool FindConceptsInSegments(List<string> Concepts, string docXMLPath, string parseSegPath, string ConceptParseSegPath)
        {
            _ErrorMessage = string.Empty;

            // ?
            //int filesCount = CopyParseSegFiles(parseSegPath, ConceptParseSegPath);

            //if (filesCount == 0)
            //{
            //    _ErrorMessage = string.Concat("No parse segment files were found in folder: ", parseSegPath);
            //    return false;
            //}

            string analysisResultsPathFile = Path.Combine(docXMLPath, ParseResultsFields.XMLFile);

            if (!File.Exists(analysisResultsPathFile))
            {
                _ErrorMessage = string.Concat("Unable to find parse segment file: ", analysisResultsPathFile);
                return false;
            }

            DataSet dsAnalysisResults = Files.LoadDatasetFromXml(analysisResultsPathFile);

            DataTable dtConceptAnalsys = createTable();

            DataTable dtConceptsSummary = Create_EmptyConceptSum(Concepts);
            
            string uid_parseSeg = string.Empty;
            string fileParseSeg = string.Empty;
            string filePathParseSeg = string.Empty;
            string txtParseSeg = string.Empty;

            string ConceptsInSeg = string.Empty;

            int conceptsFound = -1;

            if (dsAnalysisResults.Tables.Count == 0)
            {
                _ErrorMessage = "No Analysis Results were generated due to a previous error.";
                return false;
            }

            bool pageColExists = dsAnalysisResults.Tables[0].Columns.Contains(ParseResultsFields.PageSource);

            //Remove Keywords column if it exists
            if (dsAnalysisResults.Tables[0].Columns.Contains(ParseResultsFields.Keywords))
            {
                dsAnalysisResults.Tables[0].Columns.Remove(ParseResultsFields.Keywords);
            }

            // Add ConceptWords column if it does not exist
            if (!dsAnalysisResults.Tables[0].Columns.Contains(ParseResultsFields.ConceptsWords))
            {
                dsAnalysisResults.Tables[0].Columns.Add(ParseResultsFields.ConceptsWords, typeof(string));
            }

            foreach (DataRow rowParseSeg in dsAnalysisResults.Tables[0].Rows)
            {
                uid_parseSeg = rowParseSeg[ParseResultsFields.UID].ToString();

                fileParseSeg = string.Concat(uid_parseSeg, ".rtf");
                filePathParseSeg = Path.Combine(ConceptParseSegPath, fileParseSeg);

                if (!File.Exists(filePathParseSeg))
                {
                    _ErrorMessage = string.Concat("Unable to find parse file: ", filePathParseSeg);
                    return false;
                }

                ConceptsInSeg = string.Empty;

                DataRow newRowCA = dtConceptAnalsys.NewRow();
                newRowCA[ConceptsResultsFields.UID] = uid_parseSeg;
                newRowCA[ConceptsResultsFields.SortOrder] = rowParseSeg[ParseResultsFields.SortOrder];

                if (pageColExists)
                    newRowCA[ConceptsResultsFields.PageSource] = rowParseSeg[ParseResultsFields.PageSource];

                newRowCA[ConceptsResultsFields.FileName] = rowParseSeg[ParseResultsFields.FileName];
                newRowCA[ConceptsResultsFields.Number] = rowParseSeg[ParseResultsFields.Number];
                newRowCA[ConceptsResultsFields.Caption] = rowParseSeg[ParseResultsFields.Caption];
                


                foreach (string concept in Concepts)
                {
                    conceptsFound = HighlightText2(filePathParseSeg, concept, true, "Yellow", false);

                    if (conceptsFound > 0)
                    {

                        ConceptSummary_Update(ref dtConceptsSummary, concept, conceptsFound, uid_parseSeg);

                        if (ConceptsInSeg == string.Empty)

                        {
                            ConceptsInSeg = string.Concat(concept, " [", conceptsFound, "]");
                        }
                        else
                        {

                            ConceptsInSeg = string.Concat(ConceptsInSeg, ", ", concept, " [", conceptsFound, "]");
                        }


                    }

                }


                newRowCA[ConceptsResultsFields.ConceptsWords] = ConceptsInSeg;

                dtConceptAnalsys.Rows.Add(newRowCA);

                dtConceptAnalsys.AcceptChanges();

                rowParseSeg[ParseResultsFields.ConceptsWords] = ConceptsInSeg;

                dsAnalysisResults.Tables[0].AcceptChanges();

            }

            GenericDataManger gdManager = new GenericDataManger();

            gdManager.SaveDataXML(dsAnalysisResults, analysisResultsPathFile);

            DataSet dsConcepts = new DataSet();
            dsConcepts.Tables.Add(dtConceptAnalsys);
            string conceptsPathFile = Path.Combine(docXMLPath, ConceptsResultsFields.XMLFile);
            gdManager.SaveDataXML(dsConcepts, conceptsPathFile);


            DataSet dsConceptsSummary = new DataSet();
            dsConceptsSummary.Tables.Add(dtConceptsSummary);
            string conceptsSummaryPathFile = Path.Combine(docXMLPath, ConceptsDocSum.xmlFile);
            gdManager.SaveDataXML(dsConceptsSummary, conceptsSummaryPathFile);


            return true;
        }

        private int CopyParseSegFiles(string parseSegPath, string ConceptParseSegPath)
        {
            string[] files = Directory.GetFiles(parseSegPath);

            string newFile = string.Empty;
            string newPathFile = string.Empty;

            foreach (string file in files)
            {

                newFile = Files.GetFileName(file);
                newPathFile = Path.Combine(ConceptParseSegPath, newFile);

                File.Copy(file, newPathFile, true);

            }

            return files.Length;

        }

        private int HighlightText2(string file, string word, bool wholeWord, string color, bool HighlightText)
        {
            int count = 0;

            if (color == string.Empty)
                color = "YellowGreen";

          //  _rtfCrtl.Clear();

            if (!File.Exists(file))
            {
                return count;
            }

            //_rtfCrtl.LoadFile(file, System.Windows.Forms.RichTextBoxStreamType.RichText);

          //  string txt = _rtfCrtl.Text;

            string adjWord;
            if (wholeWord)
            {
                //  adjWord = "\\W?(" + word + ")\\W?";
                adjWord = @"\b(" + word + @")\b";
            }
            else
            {
                adjWord = word;
            }

            try
            {
                Regex regex = new Regex(adjWord, RegexOptions.IgnoreCase);

               // MatchCollection matches = regex.Matches(txt);
                int index = -1;
                int startIndex = 0;

               // count = matches.Count;

                if (count > 0)
                {
                    // Test code
                    //if (count > 1)
                    //{
                    //    bool what = true;
                    //}

                    ////foreach (Match match in matches)
                    ////{
                    ////    index = match.Index;
                    ////    //if (wholeWord)
                    ////    //{
                    ////    //    _rtfCrtl.Select(index + 1, word.Length);
                    ////    //}
                    ////    //else
                    ////    //{
                    //////    _rtfCrtl.Select(index, word.Length);
                    ////    //}

                    ////    ////if (HighlightText) // Highlight Text
                    ////    ////  //  _rtfCrtl.SelectionColor = Color.FromName(color);
                    ////    ////else // Highlight Background
                    ////    ////  //  _rtfCrtl.SelectionBackColor = Color.FromName(color);

                    ////    ////startIndex = index + word.Length;
                    ////}

                    //for (int i = 0; i < count; i++)
                    //{
                    //    index = match.Captures[i].Index;
                    //    _rtfCrtl.Select(index, word.Length);

                    //    if (HighlightText) // Highlight Text
                    //        _rtfCrtl.SelectionColor = Color.FromName(color);
                    //    else // Highlight Background
                    //        _rtfCrtl.SelectionBackColor = Color.FromName(color);

                    //    startIndex = index + word.Length;

                    //}

                  //  _rtfCrtl.SaveFile(file, System.Windows.Forms.RichTextBoxStreamType.RichText);
                }
            }
            catch
            {
                return 0;
            }





            return count;
        }

    }


}
