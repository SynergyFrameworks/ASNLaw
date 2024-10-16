using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using Atebion.Common;

namespace ProfessionalDocAnalyzer
{
    public class QueryFinder
    {
        private OpenNLP.Tools.SentenceDetect.MaximumEntropySentenceDetector mSentenceDetector;
        private string _ErrorMsg = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMsg; }
        }

        private string _ModelPath = string.Empty; // ToDo set path
        public string ModelPath
        {
            get { return _ModelPath; }
            set { _ModelPath = value; }
        }

        private string _ParameterFile = string.Empty;
        public string ParameterFile
        {
            get { return _ParameterFile; }
            set { _ParameterFile = value; }
        }

        private string _ParameterEmbeddedFile = string.Empty;
        public string ParameterEmbeddedFile
        {
            get { return _ParameterEmbeddedFile; }
            set { _ParameterEmbeddedFile = value; }
        }

        private RichTextBox _rtfCrtl = new RichTextBox();

        private DataTable createTable()
        {
            DataTable table = new DataTable("QueriesFound");

            table.Columns.Add(QueriesFoundFields.UID, typeof(string));
            table.Columns.Add(QueriesFoundFields.SegmentUID, typeof(string));
            table.Columns.Add(QueriesFoundFields.Query, typeof(string));
            table.Columns.Add(QueriesFoundFields.Perameter, typeof(string));

            return table;

        }

        private int HighlightText(string file, string word, string color, bool HighlightText)
        {
            int count = 0;

            if (color == string.Empty)
                color = "YellowGreen";

            _rtfCrtl.Clear();

            if (!File.Exists(file)) // Added 08.16.2016 -- Fixed bug in ver.1.7.16.04 -- The last row in datagridview is sometimes blank
            {
                return count;
            }

            _rtfCrtl.LoadFile(file, System.Windows.Forms.RichTextBoxStreamType.RichText);

            int s_start = _rtfCrtl.SelectionStart, startIndex = 0, index;

            while ((index = _rtfCrtl.Text.IndexOf(word, startIndex)) != -1)
            {
                _rtfCrtl.Select(index, word.Length);

                //if (HighlightText) // Highlight Text
                //    _rtfCrtl.SelectionColor = Color.FromName(color);
                //else // Highlight Background
                    _rtfCrtl.SelectionBackColor = Color.FromName(color);

                startIndex = index + word.Length;
                count++;
            }

            if (count > 0)
                _rtfCrtl.SaveFile(file, System.Windows.Forms.RichTextBoxStreamType.RichText);

            //_rtfCrtl.SelectionStart = s_start;
            //_rtfCrtl.SelectionLength = 0;
            //_rtfCrtl.SelectionColor = Color.Black;



            return count;
        }

        public int FindQueriesInSegments(string HightlightColor, bool FindOnlyQuestions)
        {
            _ErrorMsg = string.Empty;

            string DocParsedSecPath = AppFolders.DocParsedSec;
           if (DocParsedSecPath == string.Empty)
            {
                _ErrorMsg = "Parsed Segments path was not found.";
                return -1;
            }

            string DocParsedSecXMLPath = AppFolders.DocParsedSecXML;
            if (DocParsedSecXMLPath == string.Empty)
            {
                _ErrorMsg = "Parsed Segments XML path was not found.";
                return -1;
            }

            string ParseResultsXMLFile = Path.Combine(DocParsedSecXMLPath, "ParseResults.xml");
            if (!File.Exists(ParseResultsXMLFile))
            {
                _ErrorMsg = "Analysis Results XML file not found.";
                return -1;
            }

            
            DataSet dsParsedSec = Files.LoadDatasetFromXml(ParseResultsXMLFile);
            string uid = string.Empty;
            string segFile = string.Empty; // Parsed Segment file
            string segPathFile = string.Empty; // Parsed Segment path/file

            DataTable dt = createTable();
            DataRow rowQuery;
            int i = 0;

            List<string> lstFoundQueries;


            foreach (DataRow row in dsParsedSec.Tables[0].Rows)
            {
                uid = row[ParseResultsFields.UID].ToString();
                segFile = string.Concat(uid, ".rtf");
                segPathFile = Path.Combine(DocParsedSecPath, segFile);

                if (File.Exists(segPathFile))
                {
                    _rtfCrtl.Clear();

                    _rtfCrtl.LoadFile(segPathFile, System.Windows.Forms.RichTextBoxStreamType.RichText);

                    lstFoundQueries = Analyze(_rtfCrtl.Text, FindOnlyQuestions);
                    if (lstFoundQueries != null)
                    {
                        if (lstFoundQueries.Count > 0)
                        {
                            foreach (string query in lstFoundQueries)
                            {
                                rowQuery = dt.NewRow();
                                rowQuery[QueriesFoundFields.UID] = i.ToString();
                                rowQuery[QueriesFoundFields.SegmentUID] = uid;
                                rowQuery[QueriesFoundFields.Query] = query;

                                dt.Rows.Add(rowQuery);

                                HighlightText(segPathFile, query, HightlightColor, true);

                                i++;

                            }
                        }
                    }
                }

            } // loop through Analysis Results

            string queriesFile = Path.Combine(DocParsedSecXMLPath, "Queries.xml");
            if (File.Exists(queriesFile))
                File.Delete(queriesFile);

            if (dt.Rows.Count > 0)
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);

                GenericDataManger dMgr = new GenericDataManger();
                dMgr.SaveDataXML(ds, queriesFile);
            }

            return i;
        }


        public List<string> Analyze(string txt, bool FindOnlyQuestions)
        {
            // -> Test 
            //ModelPath = @"I:\Tom\Atebion\DocAnalyzerLite_License_NewUX - MultiCombine - 15 Release\WizardFramework\WizardFramework\WizardFramework\bin\Release\Model";
            //ParameterFile = @"I:\Tom\Atebion\ConceptQuestionsFind\ConceptQuestFinder\ConceptQuestFinder\bin\Debug\Parameters\QueryStart.txt";
            //ParameterEmbeddedFile = @"I:\Tom\Atebion\ConceptQuestionsFind\ConceptQuestFinder\ConceptQuestFinder\bin\Debug\Parameters\QueryEmbedded.txt";

            _ErrorMsg = string.Empty;

            // Check for Model Dir
            if (!Directory.Exists(ModelPath))
            {
                _ErrorMsg = string.Concat("Model Folder Not Found: ", ModelPath);
                return null;
            }

            if (!validateParamerFiles())
            {
                return null;
            }

            // split text into sentences 
            string[] sentences = SplitSentences(txt);


            // Identifiy questions
            return FindQuestions(sentences, _ParameterFile, _ParameterEmbeddedFile, FindOnlyQuestions);
        }

        private bool validateParamerFiles()
        {
            if (!File.Exists(ParameterFile))
            {
                _ErrorMsg = string.Concat("Parameter File Not Found: ", ParameterFile);
                return false;
            }

            if (!File.Exists(ParameterEmbeddedFile))
            {
                _ErrorMsg = string.Concat("Parameter Embedded File Not Found: ", ParameterEmbeddedFile);
                return false;
            }

            return true;
        }

        private string[] SplitSentences(string paragraph)
        {
            _ErrorMsg = string.Empty;

            if (mSentenceDetector == null)
            {
                string fileEnglish = Path.Combine(_ModelPath, "EnglishSD.nbin");
                if (File.Exists(fileEnglish))
                    mSentenceDetector = new OpenNLP.Tools.SentenceDetect.EnglishMaximumEntropySentenceDetector(fileEnglish);
                else
                {
                    _ErrorMsg = string.Concat("Unable to find file: ", fileEnglish);
                    return null;
                }
            }

            return mSentenceDetector.SentenceDetect(paragraph);
        }

        private List<string> FindQuestions(string[] sentences, string parametersFile, string parameterEmbeddedFile, bool FindOnlyQuestions)
        {
            bool isQuery = false;
            string[] parameters = GetParameters(parametersFile);
            if (parameters == null)
            {
                return null;
            }

            string[] parametersEmbedded = GetParameters(parameterEmbeddedFile);

            List<string> foundQuestions = new List<string>();

            string parameterUsed = string.Empty;

            foreach (string sentence in sentences)
            {
                string modSentence = sentence.TrimStart('\t');
                modSentence = sentence.TrimStart();

                isQuery = false;
                if (modSentence.IndexOf("?") > -1)
                {
                    modSentence = cleanupSentence(modSentence, "?", true);
                    isQuery = true;
                }
                else if (!FindOnlyQuestions)
                {
                    isQuery = IsSentenceAQuery(parameters, modSentence, out parameterUsed);

                    if (isQuery)
                    {
                        modSentence = cleanupSentence(modSentence, parameterUsed, false);
                    }
                    else
                    {
                        if (parametersEmbedded != null)
                        {
                            isQuery = IsSentenceAEmbeddedQuery(parametersEmbedded, modSentence, out parameterUsed);
                            if (isQuery)
                                modSentence = cleanupSentence(modSentence, parameterUsed, true);
                        }
                    }
                }

                if (isQuery)
                {
                    foundQuestions.Add(modSentence);
                }
            }

            return foundQuestions;

        }

        private string cleanupSentence(string Sentence, string Parameter, bool isEnbedded)
        {
            string[] lines = Regex.Split(Sentence, "\r\n|\r|\n");

            if (lines.Length > 0)
            {
                foreach (string line in lines)
                {
                    if (isEnbedded)
                    {
                        if (line.IndexOf(Parameter) > -1)
                        {
                            return line.Trim();
                        }
                    }
                    else
                    {
                        if (line.IndexOf(Parameter) == 0)
                        {
                            return line.Trim();
                        }
                    }
                }
            }

            return Sentence;
             
        }


        private bool IsSentenceAQuery(string[] Parameters, string Sentence, out string outParameter)
        {
            foreach (string parameter in Parameters)
            {
                if (Sentence.IndexOf(parameter) == 0)
                {
                    outParameter = parameter;
                    return true;
                }
            }

            outParameter = string.Empty;
            return false;

        }

        private bool IsSentenceAEmbeddedQuery(string[] Parameters, string Sentence, out string parameterUsed)
        {
            string modSentence = Sentence.ToLower();
            foreach (string parameter in Parameters)
            {
                if (modSentence.IndexOf(parameter) > 0)
                {
                    parameterUsed = parameter;
                    return true;
                }
            }

            parameterUsed = string.Empty;
            return false;

        }


        private string[] GetParameters(string parametersFile)
        {
            if (!File.Exists(parametersFile))
            {
                _ErrorMsg = string.Concat("Parameter File Not Found: ", parametersFile);
                return null;
            }

            return ReadFile2Array(parametersFile);

        }

        private string[] ReadFile2Array(string pathFile)
        {
            List<string> fileContent = new List<string>();

            TextReader tr = new StreamReader(pathFile);

            string currentLine = string.Empty;

            while ((currentLine = tr.ReadLine()) != null)
            {
                fileContent.Add(currentLine);
            }

            tr.Close();

            string[] lines = fileContent.ToArray();

            return lines;
        }

        public bool SplitSegQueries(string OrgSegUID, string NewSegUID, string xmlSegFile, string xmlQueries, string SegPath)
        {
            bool returnValue = false;

            _ErrorMsg = string.Empty;

            // Chedk if files exists
            if (!File.Exists(xmlSegFile))
            {
                _ErrorMsg = string.Concat("Analysis Results file Not found: ", xmlSegFile);
                return returnValue;
            }
            if (!File.Exists(xmlQueries))
            {
                _ErrorMsg = string.Concat("Found Queries/Questions file Not found: ", xmlQueries);
                return returnValue;
            }

            GenericDataManger dataMgr = new GenericDataManger();

            // Get Data
            //DataSet dsSeg = dataMgr.LoadDatasetFromXml(xmlSegFile);
            //if (dsSeg == null)
            //{
            //    _ErrorMsg = dataMgr.ErrorMessage;
            //    return returnValue;
            //}

            DataSet dsQueries = dataMgr.LoadDatasetFromXml(xmlQueries);
            if (dsQueries == null)
            {
                _ErrorMsg = dataMgr.ErrorMessage;
                return returnValue;
            }

            //// Get Parent RTF File
            //RichTextBox rtfOrgSeg = new RichTextBox();
            //string orgSegFile = string.Concat(OrgSegUID, ".rtf");
            //orgSegFile = Path.Combine(SegPath, orgSegFile);
            //if (!File.Exists(orgSegFile))
            //{
            //    _ErrorMsg = string.Concat("Parent Segment Not found: ", orgSegFile);
            //    return returnValue;
            //}
            //rtfOrgSeg.LoadFile(orgSegFile);
            //string orgSegText = rtfOrgSeg.Text;

            // Get Child RTF File
            RichTextBox rtfNewSeg = new RichTextBox();
            string NewSegFile = string.Concat(NewSegUID, ".rtf");
            NewSegFile = Path.Combine(SegPath, NewSegFile);
            if (!File.Exists(NewSegFile))
            {
                _ErrorMsg = string.Concat("New Child Segment Not found: ", NewSegFile);
                return returnValue;
            }
            rtfNewSeg.LoadFile(NewSegFile);
            string newSegText = rtfNewSeg.Text;

            string expression = string.Concat(QueriesFoundFields.SegmentUID, " = '", OrgSegUID, "'");
            DataRow[] foundRows = dsQueries.Tables[0].Select(expression);
            int i = 0;
            string query = string.Empty;

            foreach (DataRow row in foundRows)
            {
                query = row[QueriesFoundFields.Query].ToString();

                if (newSegText.IndexOf(query) > -1)
                {
                    row[QueriesFoundFields.SegmentUID] = NewSegUID;
                    dsQueries.Tables[0].AcceptChanges();
                    i++;
                }
            }

            if (i > 0)
            {
                dsQueries.Tables[0].AcceptChanges();
                dataMgr.SaveDataXML(dsQueries, xmlQueries);
                returnValue = true;
            }

            return returnValue;
        }

        public bool CombineSegQueries(string ParentUID, string[] CombineSegUIDs, string xmlSegFile, string xmlQueries)
        {
            bool returnValue = false;

            _ErrorMsg = string.Empty;

            // Chedk if files exists
            if (!File.Exists(xmlSegFile))
            {
                _ErrorMsg = string.Concat("Analysis Results file Not found: ", xmlSegFile);
                return returnValue;
            }
            if (!File.Exists(xmlQueries))
            {
                _ErrorMsg = string.Concat("Found Queries/Questions file Not found: ", xmlQueries);
                return returnValue;
            }

            GenericDataManger dataMgr = new GenericDataManger();

            // Get Data
            DataSet dsSeg = dataMgr.LoadDatasetFromXml(xmlSegFile);
            if (dsSeg == null)
            {
                _ErrorMsg = dataMgr.ErrorMessage;
                return returnValue;
            }

            DataSet dsQueries = dataMgr.LoadDatasetFromXml(xmlQueries);
            if (dsQueries == null)
            {
                _ErrorMsg = dataMgr.ErrorMessage;
                return returnValue;
            }

            string expression;
            DataRow[] foundRows;
            int i = 0;
            foreach (string segUId in CombineSegUIDs)
            {
                expression = string.Concat(QueriesFoundFields.SegmentUID, " = '", segUId, "'");

                foundRows = dsQueries.Tables[0].Select(expression);

                foreach (DataRow row in foundRows)
                {
                    row[QueriesFoundFields.SegmentUID] = ParentUID;
                    dsQueries.Tables[0].AcceptChanges();
                    i++;
                }
            }

            if (i > 0)
            {
                dataMgr.SaveDataXML(dsQueries, xmlQueries);
                returnValue = true;
            }
               
            return returnValue;
        }

    }
}
