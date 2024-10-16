using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Windows;
using System.Drawing;
using System.Text.RegularExpressions;

using Atebion.Common;
using Atebion.RTFBox;

namespace Atebion.ConceptAnalyzer
{
    class ParagraphParseEng
    {
        private string _ErrorMsg = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMsg; }
        }

        private string _ParseSec = string.Empty;
        public string ParseSec
        {
            get { return _ParseSec; }
            set { _ParseSec = value; }
        }

        private string _ParseSecXML = string.Empty;
        public string ParseSecXML
        {
            get { return _ParseSecXML; }
            set { _ParseSecXML = value; }
        }

        private string _ParseSecKeywords = string.Empty;
        public string ParseSecKeywords
        {
            get { return _ParseSecKeywords; }
            set { _ParseSecKeywords = value; }
        }


        private DataSet _dsParseResults = new DataSet();
        private DataSet _dsKeywordsFound = new DataSet();
        private RichTextBox _rtfCrtl = new RichTextBox();

        public int Parse3(string pathFile, string ParsedSegFolder, string XMLFolder) // Parse by paragraph for Analysis Results area
        {
            _ErrorMsg = string.Empty;

            _ParseSec = ParsedSegFolder;
            _ParseSecXML = XMLFolder;

            int Counter = 0;
            int lineCounter = 0;

            if (!PrepocessValidation(pathFile)) // Validation (e.g. file & folders)
                return 0;

            string txt = Files.ReadFile(pathFile);
            if (txt == string.Empty)
            {
                return 0;
            }

            string[] lines = txt.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries); // Split document text into paragraphs

            StringBuilder sb = new StringBuilder();
            DataTable dtParseResults = createTable();
            string paragraphFile = string.Empty;
            int lineStartNo = 0;

            System.Windows.Forms.RichTextBox rtfCtrl = new System.Windows.Forms.RichTextBox();

            // -> Added to fix RTFbox bug where the 1st file has no text when saved. -- A real Hack-Job!
            string file1 = string.Concat(Counter.ToString(), ".rtf");

            paragraphFile = Path.Combine(_ParseSec, file1);
            if (File.Exists(paragraphFile))
                File.Delete(paragraphFile);

            rtfCtrl.Text = string.Empty;
            rtfCtrl.SaveFile(paragraphFile);

            if (File.Exists(paragraphFile))
                File.Delete(paragraphFile);
            // <-

            string modLine = string.Empty;
            foreach (string line in lines)
            {
                modLine = RemoveSpecialCharacters(line.Trim());
                if (modLine != string.Empty)
                {
                    sb.AppendLine(string.Concat(line, Environment.NewLine));
                }
                else
                {
                    if (sb.ToString().Trim() != string.Empty)
                    {
                        string file = string.Concat(Counter.ToString(), ".rtf");

                        paragraphFile = Path.Combine(_ParseSec, file);
                        if (File.Exists(paragraphFile))
                            File.Delete(paragraphFile);

                        rtfCtrl.Text = sb.ToString();
                        rtfCtrl.SaveFile(paragraphFile);

                        DataRow row = dtParseResults.NewRow();
                        row[ParseResultsFields.UID] = Counter;
                        row[ParseResultsFields.SortOrder] = Counter;
                        row[ParseResultsFields.Number] = Counter.ToString();
                        row[ParseResultsFields.Caption] = GetCaption(sb.ToString(), 50, true);
                        row[ParseResultsFields.FileName] = string.Concat(Counter.ToString(), ".rtf");
                        //    row[ParseResultsFields.SectionLength] = sb.ToString().Length;
                        row[ParseResultsFields.LineStart] = lineStartNo;
                        row[ParseResultsFields.LineEnd] = lineCounter - 1;
                        row[ParseResultsFields.Keywords] = string.Empty; // Insert in during FindKeywords

                        dtParseResults.Rows.Add(row);

                        sb = new StringBuilder();


                        Counter++;
                        lineStartNo = lineCounter++;
                    }

                    lineCounter++;
                }
            }


            if (sb.ToString().Trim() != string.Empty)
            {
                string file = string.Concat(Counter.ToString(), ".rtf");

                paragraphFile = Path.Combine(_ParseSec, file);
                if (File.Exists(paragraphFile))
                    File.Delete(paragraphFile);

                rtfCtrl.Text = sb.ToString();
                rtfCtrl.SaveFile(paragraphFile);

                DataRow row = dtParseResults.NewRow();
                row[ParseResultsFields.UID] = Counter;
                row[ParseResultsFields.SortOrder] = Counter;
                row[ParseResultsFields.Number] = Counter.ToString();
                row[ParseResultsFields.Caption] = GetCaption(sb.ToString(), 50, true);
                row[ParseResultsFields.FileName] = string.Concat(Counter.ToString(), ".rtf");
                //    row[ParseResultsFields.SectionLength] = sb.ToString().Length;
                row[ParseResultsFields.LineStart] = lineStartNo;
                row[ParseResultsFields.LineEnd] = lineCounter - 1;
                row[ParseResultsFields.Keywords] = string.Empty; // Insert in during FindKeywords

                dtParseResults.Rows.Add(row);

                sb = new StringBuilder();


                Counter++;
                lineStartNo = lineCounter++;
            }



            _dsParseResults.Tables.Add(dtParseResults);

            string parseResultsFile = Path.Combine(_ParseSecXML, "ParseResults.xml");
            if (File.Exists(parseResultsFile))
                File.Delete(parseResultsFile);

            SaveDataXML(_dsParseResults, parseResultsFile);

            return Counter;
        }

 

        public string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

   


        //public int findKeywords2(DataSet dsKeywords, Color SelectColor)
        //{
        //    string word = string.Empty;
        //    int count = 0;
        //    int FoundKwUID = 0;
        //    string keywords = string.Empty;
        //    string file = string.Empty;
        //    string parseResultsFile = Path.Combine(_ParseSecXML, "ParseResults.xml");
        //    string keywordFile = Path.Combine(_ParseSecXML, "KeywordsFound2.xml");

        //    _ErrorMsg = string.Empty;

        //    if (_dsParseResults == null)
        //    {
        //        if (File.Exists(parseResultsFile))
        //        {
        //            _dsParseResults = LoadDatasetFromXml(parseResultsFile);
        //        }
        //        else
        //        {
        //            _ErrorMsg = "Unable to find ParseResults.xml file!";
        //            return -1;
        //        }
        //    }

        //    DataTable dtFoundKeywords = createTableKeywordsFound();

        //    foreach (DataRow dr in _dsParseResults.Tables[0].Rows) // Loop Sentences
        //    {
        //        keywords = string.Empty;
        //        string uid = dr[ParseResultsFields.UID].ToString();
        //        file = string.Concat(AppFolders.DocParsedSec, @"\", uid, ".rtf");

        //        foreach (DataRow drKeywords in dsKeywords.Tables[0].Rows) // Loop Selected Keywords
        //        {
        //            word = drKeywords[KeywordsFoundFields.Keyword].ToString();

        //            int qtyFound = HighlightText(file, word, Color.YellowGreen, false); // Find Keyword and highlight it in Dark Green
        //            //int currentKwQty = (int)drKeywords[KeywordsFoundFields.Count];
        //            //drKeywords[KeywordsFoundFields.Count] = currentKwQty + qtyFound; // Set the Qty found for the Current Keyword
        //            count = count + qtyFound;

        //            if (qtyFound > 0)
        //            {
        //                count = count + qtyFound;
        //                // Save Found Keyword results to Dataset
        //                DataRow row = dtFoundKeywords.NewRow();
        //                row[KeywordsFoundFields.UID] = FoundKwUID;
        //                row[KeywordsFoundFields.Keyword] = word;
        //                row[KeywordsFoundFields.Count] = qtyFound;
        //                row[KeywordsFoundFields.SegmentUID] = uid;

        //                dtFoundKeywords.Rows.Add(row);

        //                //row[NP_Keywords.UID] = _FoundKwUID;
        //                //row[NP_Keywords.KeyWord] = word;
        //                //row[NP_Keywords.SentenceUID] = uid;
        //                //_dsFoundKeywords.Tables[0].Rows.Add(row);

        //                //_FoundKwUID++;

        //                if (keywords == string.Empty)
        //                {
        //                    keywords = string.Concat(word, " [", qtyFound.ToString(), "]");
        //                }
        //                else
        //                {
        //                    keywords = string.Concat(keywords, ", ", word, " [", qtyFound.ToString(), "]");
        //                }

        //                FoundKwUID++;

        //            }

        //            dr[ParseResultsFields.Keywords] = keywords;

        //        }

        //    }

        //    GenericDataManger dataMgr = new GenericDataManger();
        //    dataMgr.SaveDataXML(_dsParseResults, parseResultsFile);

        //    if (File.Exists(keywordFile))
        //    {
        //        File.Delete(keywordFile);
        //    }
        //    DataSet dsKeywordsFound = new DataSet();
        //    dsKeywordsFound.Tables.Add(dtFoundKeywords);
        //    dataMgr.SaveDataXML(dsKeywordsFound, keywordFile);

        //    return count;

        //}

        private int HighlightText(string file, string word, Color color, bool HighlightText)
        {
            int count = 0;

            _rtfCrtl.Clear();

            _rtfCrtl.LoadFile(file, System.Windows.Forms.RichTextBoxStreamType.RichText);

            int s_start = _rtfCrtl.SelectionStart, startIndex = 0, index;

            while ((index = _rtfCrtl.Text.IndexOf(word, startIndex)) != -1)
            {
                _rtfCrtl.Select(index, word.Length);

                if (HighlightText) // Highlight Text
                    _rtfCrtl.SelectionColor = color;
                else // Highlight Background
                    _rtfCrtl.SelectionBackColor = color;

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

        //public int DeleteKeywordsSec(string uid)
        //{
        //    int returnResult = 0;
        //    string keywordFile = Path.Combine(_ParseSecXML, "KeywordsFound2.xml");

        //    if (!File.Exists(keywordFile))
        //    {
        //        _ErrorMsg = string.Concat("Unable to find file ", keywordFile);
        //        return returnResult;
        //    }

        //    // FoundKeywords
        //    GenericDataManger gDataMgr = new GenericDataManger();
        //    DataSet dsFoundKeywords = gDataMgr.LoadDatasetFromXml(keywordFile);
        //    if (dsFoundKeywords.Tables[0].Rows.Count == 0)
        //    {
        //        return returnResult;
        //    }

        //    for (int i = dsFoundKeywords.Tables[0].Rows.Count - 1; i >= 0; i--)
        //    {
        //        DataRow dr1 = dsFoundKeywords.Tables[0].Rows[i];
        //        if (dr1[KeywordsFoundFields.SegmentUID].ToString() == uid.ToString())
        //        {
        //            dr1.Delete();
        //            returnResult++;
        //        }
        //    }

        //    gDataMgr.SaveDataXML(dsFoundKeywords, keywordFile);
        //    return returnResult;
        //}

        //public int FindKeywordsSec(string uid)
        //{
        //    string word = string.Empty;
        //    int count = 0;
        //    int FoundKwUID = 0;
        //    string keywords = string.Empty;
        //    string file = string.Empty;

        //    _ParseSecXML = AppFolders.DocParsedSecXML;
        //    string parseResultsFile = Path.Combine(_ParseSecXML, "ParseResults.xml");
        //    string keywordFile = Path.Combine(_ParseSecXML, "KeywordsFound2.xml");
        //    string KeywordsSelectedFile = Path.Combine(_ParseSecXML, "KeywordsSelected.xml");

        //    _ErrorMsg = string.Empty;

        //    if (!File.Exists(KeywordsSelectedFile))
        //    {
        //        _ErrorMsg = string.Concat("Unable to find file ", KeywordsSelectedFile);
        //        return 0;
        //    }

        //    if (!File.Exists(keywordFile))
        //    {
        //        _ErrorMsg = string.Concat("Unable to find file ", keywordFile);
        //        return 0;
        //    }

        //    // Get Keywords Selected
        //    GenericDataManger gDataMgr = new GenericDataManger();
        //    DataSet dsKeywords = gDataMgr.LoadDatasetFromXml(KeywordsSelectedFile);
        //    if (dsKeywords.Tables[0].Rows.Count == 0)
        //    {
        //        return 0;
        //    }

        //    // FoundKeywords
        //    DataSet dsFoundKeywords = gDataMgr.LoadDatasetFromXml(keywordFile);
        //    if (dsFoundKeywords.Tables[0].Rows.Count == 0)
        //    {
        //        return 0;
        //    }


        //    //if (_dsParseResults == null)
        //    //{
        //    if (File.Exists(parseResultsFile))
        //    {
        //        _dsParseResults = LoadDatasetFromXml(parseResultsFile);
        //    }
        //    else
        //    {
        //        _ErrorMsg = "Unable to find ParseResults.xml file!";
        //        return -1;
        //    }
        //    //}

        //    // Remove Previous Found Keywords
        //    for (int i = dsFoundKeywords.Tables[0].Rows.Count - 1; i >= 0; i--)
        //    {
        //        DataRow dr1 = dsFoundKeywords.Tables[0].Rows[i];
        //        if (dr1[KeywordsFoundFields.SegmentUID].ToString() == uid.ToString())
        //            dr1.Delete();
        //    }

        //    gDataMgr.SaveDataXML(dsFoundKeywords, keywordFile);

        //    keywords = string.Empty;

        //    foreach (DataRow dr in _dsParseResults.Tables[0].Rows) // Loop Sentences
        //    {

        //        if (dr[ParseResultsFields.UID].ToString() == uid)
        //        {
        //            file = string.Concat(AppFolders.DocParsedSec, @"\", uid, ".rtf");

        //            foreach (DataRow drKeywords in dsKeywords.Tables[0].Rows) // Loop Selected Keywords
        //            {
        //                word = drKeywords[KeywordsFoundFields.Keyword].ToString();

        //                int qtyFound = HighlightText(file, word, Color.YellowGreen, false); // Find Keyword and highlight it in Dark Green
        //                //int currentKwQty = (int)drKeywords[KeywordsFoundFields.Count];
        //                //drKeywords[KeywordsFoundFields.Count] = currentKwQty + qtyFound; // Set the Qty found for the Current Keyword
        //                count = count + qtyFound;

        //                if (qtyFound > 0)
        //                {
        //                    count = count + qtyFound;
        //                    // Save Found Keyword results to Dataset
        //                    DataRow row = dsFoundKeywords.Tables[0].NewRow();
        //                    row[KeywordsFoundFields.UID] = FoundKwUID;
        //                    row[KeywordsFoundFields.Keyword] = word;
        //                    row[KeywordsFoundFields.Count] = qtyFound;
        //                    row[KeywordsFoundFields.SegmentUID] = uid;

        //                    dsFoundKeywords.Tables[0].Rows.Add(row);

        //                    //row[NP_Keywords.UID] = _FoundKwUID;
        //                    //row[NP_Keywords.KeyWord] = word;
        //                    //row[NP_Keywords.SentenceUID] = uid;
        //                    //_dsFoundKeywords.Tables[0].Rows.Add(row);

        //                    //_FoundKwUID++;

        //                    if (keywords == string.Empty)
        //                    {
        //                        keywords = string.Concat(word, " [", qtyFound.ToString(), "]");
        //                    }
        //                    else
        //                    {
        //                        keywords = string.Concat(keywords, ", ", word, " [", qtyFound.ToString(), "]");
        //                    }

        //                    FoundKwUID++;

        //                }

        //                dr[ParseResultsFields.Keywords] = keywords;

        //            }
        //        }

        //    }

        //    gDataMgr.SaveDataXML(_dsParseResults, parseResultsFile);
        //    gDataMgr.SaveDataXML(dsFoundKeywords, keywordFile);

        //    gDataMgr = null;

        //    return count;
        //}


        public DataSet LoadDatasetFromXml(string fileName)
        {
            DataSet ds = new DataSet();
            FileStream fs = null;

            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                using (StreamReader reader = new StreamReader(fs))
                {
                    ds.ReadXml(reader);
                }
            }
            catch (Exception e)
            {
                _ErrorMsg = string.Concat("Loading Data From XML file: ", fileName, " -- Error: ", e.ToString());
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }

            return ds;
        }

        private void SaveDataXML(DataSet ds, string pathFile)
        {
            try
            {
                ds.WriteXml(pathFile, XmlWriteMode.WriteSchema);
            }
            catch (Exception e)
            {
                _ErrorMsg = string.Concat("Saving Data an XML file: ", pathFile, " -- Error: ", e.ToString());
            }

        }

        private bool PrepocessValidation(string pathFile)
        {
            _ErrorMsg = string.Empty;

            if (!File.Exists(pathFile))
            {
                _ErrorMsg = "Document not found!";
                return false;
            }

            if (!Directory.Exists(_ParseSec))
            {
                _ErrorMsg = "Parse Segment Folder not found!";
                return false;
            }

            if (!Directory.Exists(_ParseSecXML))
            {
                _ErrorMsg = "Parse Segment XML Folder not found!";
                return false;
            }

            return true;
        }
        //private string ReadFile(string pathFile)
        //{
        //    _ErrorMsg = string.Empty;

        //    string txt = string.Empty;
        //    try
        //    {
        //        TikaOnDotNet.TextExtraction.TextExtractor textX = new TikaOnDotNet.TextExtraction.TextExtractor();

        //        var results = textX.Extract(pathFile);
        //        txt = results.Text.Trim();

        //    }
        //    catch (Exception ex)
        //    {
        //        _ErrorMsg = ex.Message;
        //        txt = string.Empty;
        //    }

        //    return txt;
        //}

        //private DataTable createTableKeywordsFound()
        //{
        //    DataTable table = new DataTable("KeywordsFound");

        //    table.Columns.Add(KeywordsFoundFields.UID, typeof(string));
        //    table.Columns.Add(KeywordsFoundFields.Keyword, typeof(string));
        //    table.Columns.Add(KeywordsFoundFields.Count, typeof(int));
        //    table.Columns.Add(KeywordsFoundFields.SegmentUID, typeof(int));

        //    return table;
        //}

        private DataTable createTable()
        {
            DataTable table = new DataTable("ParseResults");

            table.Columns.Add(ParseResultsFields.UID, typeof(string));
            table.Columns.Add(ParseResultsFields.Parameter, typeof(string));
            table.Columns.Add(ParseResultsFields.Parent, typeof(string));
            table.Columns.Add(ParseResultsFields.LineStart, typeof(int));
            table.Columns.Add(ParseResultsFields.LineEnd, typeof(int));
            table.Columns.Add(ParseResultsFields.SectionLength, typeof(int));
            table.Columns.Add(ParseResultsFields.ColumnStart, typeof(int));
            table.Columns.Add(ParseResultsFields.ColumnEnd, typeof(int));
            table.Columns.Add(ParseResultsFields.IndexStart, typeof(int));
            table.Columns.Add(ParseResultsFields.IndexEnd, typeof(int));
            table.Columns.Add(ParseResultsFields.Number, typeof(string));
            table.Columns.Add(ParseResultsFields.Caption, typeof(string));
            table.Columns.Add(ParseResultsFields.SortOrder, typeof(int));
            table.Columns.Add(ParseResultsFields.FileName, typeof(string));
            table.Columns.Add(ParseResultsFields.Keywords, typeof(string));

            return table;

        }

        private DataTable createTableDocsExplorer()
        {
            DataTable table = new DataTable("ParseResults");

            table.Columns.Add(ParseResultsFields.UID, typeof(string));
            table.Columns.Add(ParseResultsFields.LineStart, typeof(int));
            table.Columns.Add(ParseResultsFields.LineEnd, typeof(int));
            table.Columns.Add(ParseResultsFields.Number, typeof(string));
            table.Columns.Add(ParseResultsFields.Caption, typeof(string));
            table.Columns.Add(ParseResultsFields.SortOrder, typeof(int));
            table.Columns.Add(ParseResultsFields.FileName, typeof(string));
            table.Columns.Add(ParseResultsFields.Keywords, typeof(string));
            //table.Columns.Add(ParseResultsFields.CommonConceptsWords, typeof(string));
            //table.Columns.Add(ParseResultsFields.CommonPhrases, typeof(string));
            return table;

        }

        public string GetCaption(string strInput, int intMax_Length, bool booEnd_With_Dots)
        {
            string functionReturnValue = null;
            const string strDOTS = " ...";
            int i = 0;
            int intUBound = 0;
          //  int x = 0;
            int intAdjMax_Length = 0;
            string[] strString = null;
            string strNew_String = null;

            strNew_String = string.Empty;
            //>> Set Default 

            // ERROR: Not supported in C#: OnErrorStatement


            bool booAdjEnd_With_Dots = false;
            //Added 09.21.2013
            booAdjEnd_With_Dots = false;

            //>>> Added 07.07.2013
            //>> Split on Carriage Returns
            if (strInput.Contains(System.Environment.NewLine))
            {
                string[] strNewSegment = null;
                strNewSegment = strInput.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                strNew_String = strNewSegment[0];

                if (strNew_String.Length <= intMax_Length)
                {
                    functionReturnValue = strNew_String;
                    return functionReturnValue;
                }
                else
                {
                    strInput = strNew_String;
                    booAdjEnd_With_Dots = true;
                }
            }



            if (booEnd_With_Dots)
            {
                // If intMax_Length > Len(strDOTS) Then
                //Change 09.21.2013
                if (intMax_Length < strInput.Length)
                {
                    intAdjMax_Length = intMax_Length - strDOTS.Length;
                }
                else
                {
                    intAdjMax_Length = intMax_Length;
                }
            }
            else
            {
                intAdjMax_Length = intMax_Length;
            }
            strString = strInput.Split(' '); // VB Code: Strings.Split(Strings.Trim(strInput), " ");
            intUBound = strString.Length;
            strNew_String = string.Empty;

            for (i = 0; i < intUBound; i++)
            {
                if ((strNew_String + " " + strString[i]).Length <= intAdjMax_Length)
                {
                    strNew_String = strNew_String + " " + strString[i];
                }
                else
                {
                    break;
                }
            }

            if (booAdjEnd_With_Dots)
            {
                strNew_String = strNew_String + strDOTS;
            }

            //>> Cleanup Title
            strNew_String = strNew_String.Replace("'", " "); // ReplaceChars(strNew_String, "'", " ");
            //>> Remove single quotes
            strNew_String = strNew_String.Replace((char)34, ' '); // ReplaceChars(strNew_String, Strings.Chr(34), " ");
            //>> Remove double quotes
            Valid_ASCII(strNew_String);
            strNew_String = strNew_String.Trim();


            return strNew_String;

        }

        public string Valid_ASCII(string strString)
        {
            int x = 0;


            for (x = 1; x < strString.Length; x++)
            {
                if ((int)Convert.ToChar(strString.Substring(x, 1)) > 126 || (int)Convert.ToChar(strString.Substring(x, 1)) < 32)  // VB code: if (String.Asc(Strings.Mid(strString, x, 1)) > 126 | Strings.Asc(Strings.Mid(strString, x, 1)) < 32)              
                {
                    strString.Replace(strString.Substring(x, 1), ""); // VB code: strString = ReplaceChars(strString, Strings.Mid(strString, x, 1), " ");                                    
                }
            }

            return strString;
        }





    }
}
