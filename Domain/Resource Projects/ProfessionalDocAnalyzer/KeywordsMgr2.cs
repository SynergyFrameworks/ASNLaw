using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Atebion.Common;


namespace ProfessionalDocAnalyzer
{
    class KeywordsMgr2
    {

        private string _ErrorMsg = string.Empty;

        public string ErrorMessage
        {
            get { return _ErrorMsg; }
        }

        private string _ParseSecXML = string.Empty;

        private DataSet _dsKeywordLib = new DataSet();
        private DataSet _dsParseResults;
        private RichTextBox _rtfCrtl = new RichTextBox();

       // private bool _ColorColumnExists = false;

        GenericDataManger _gDataMgr = new GenericDataManger();

        public DataSet GetEmptyKeywordsLib()
        {
            DataTable tbKeywordLib = createTableKeywordsLib();
            _dsKeywordLib.Tables.Add(tbKeywordLib);

            return _dsKeywordLib;
        }

        public DataSet GetKeywordsLib(string file, string DefaultColor)
        {
            _ErrorMsg = string.Empty;

            if (!File.Exists(file))
            {
                _ErrorMsg = string.Concat("File not found: ", file);
                return null;
            }


            DataSet ds = _gDataMgr.LoadDatasetFromXml(file);
            if (ds == null)
            {
                _ErrorMsg = _gDataMgr.ErrorMessage;
                return null;
            }
            if (ds.Tables[0].Rows.Count == 0)
            {
                _ErrorMsg = "No Keywords Found";
                return null;
            }

            ////if (ColorColumnFound(ds))
            ////{
            ////    _dsKeywordLib = ds;
            ////    return _dsKeywordLib;
            ////}

            string fileName = Files.GetFileNameWOExt(file);

            bool useDefaultColor = true;
            if (ds.Tables[0].Columns.Contains(KeywordsFoundFields.ColorHighlight))
            {
                useDefaultColor = false;
            }

            DataTable tbKeywordLib = createTableKeywordsLib();
            int counter = 0;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                DataRow newRow = tbKeywordLib.NewRow();
                newRow[KeywordsFoundFields.UID] = counter;
                newRow[KeywordsFoundFields.Keyword] = row[KeywordsFoundFields.Keyword].ToString();
                
                if (useDefaultColor)
                {
                    newRow[KeywordsFoundFields.ColorHighlight] = DefaultColor;
                }
                else
                {
                    newRow[KeywordsFoundFields.ColorHighlight] = row[KeywordsFoundFields.ColorHighlight].ToString();
                }
        
                newRow[KeywordsFoundFields.KeywordLib] = fileName;

                tbKeywordLib.Rows.Add(newRow);

                counter++;
            }

            _dsKeywordLib.Tables.Clear(); // Remove old table
            _dsKeywordLib.Tables.Add(tbKeywordLib);

            return _dsKeywordLib;
        }


        private bool ColorColumnFound(DataSet ds)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            return dr.Table.Columns.Contains(KeywordsFoundFields.ColorHighlight);

        }



        public int FindKeywordsSec(string uid)
        {
            string word = string.Empty;
            int count = 0;
            int FoundKwUID = 0;
            string keywords = string.Empty;
            string file = string.Empty;

            _ParseSecXML = AppFolders.DocParsedSecXML;
            string parseResultsFile = Path.Combine(_ParseSecXML, "ParseResults.xml");
            string keywordFile = Path.Combine(_ParseSecXML, "KeywordsFound2.xml");
            string KeywordsSelectedFile = Path.Combine(_ParseSecXML, "KeywordsSelected.xml");

            _ErrorMsg = string.Empty;

            if (!File.Exists(KeywordsSelectedFile))
            {
                _ErrorMsg = string.Concat("Unable to find file ", KeywordsSelectedFile);
                return 0;
            }

            if (!File.Exists(keywordFile))
            {
                _ErrorMsg = string.Concat("Unable to find file ", keywordFile);
                return 0;
            }

            // Get Keywords Selected
            GenericDataManger gDataMgr = new GenericDataManger();
            DataSet dsKeywords = gDataMgr.LoadDatasetFromXml(KeywordsSelectedFile);
            if (dsKeywords.Tables[0].Rows.Count == 0)
            {
                return 0;
            }

            // FoundKeywords
            DataSet dsFoundKeywords = gDataMgr.LoadDatasetFromXml(keywordFile);
            if (dsFoundKeywords.Tables[0].Rows.Count == 0)
            {
                return 0;
            }


            //if (_dsParseResults == null)
            //{
            if (File.Exists(parseResultsFile))
            {
                _dsParseResults = LoadDatasetFromXml(parseResultsFile);
            }
            else
            {
                _ErrorMsg = "Unable to find ParseResults.xml file!";
                return -1;
            }
            //}

            // Remove Previous Found Keywords
            for (int i = dsFoundKeywords.Tables[0].Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr1 = dsFoundKeywords.Tables[0].Rows[i];
                if (dr1[KeywordsFoundFields.SegmentUID] == uid)
                    dr1.Delete();
            }

            gDataMgr.SaveDataXML(dsFoundKeywords, keywordFile);

            keywords = string.Empty;
            string color = "YellowGreen";

            // Is Find Keayword by Whole Word?
            bool isWholeKeywords = false;
            string xIsWholeKeywords = "No";
            string isWholeKeywordsPathFile = Path.Combine(AppFolders.DocParsedSecKeywords, "isWholeKeywords.txt");
            if (File.Exists(isWholeKeywordsPathFile))
            {
                xIsWholeKeywords = Atebion.Common.Files.ReadFile(isWholeKeywordsPathFile);
                if (xIsWholeKeywords.IndexOf("Yes") != -1)
                {
                    isWholeKeywords = true;
                }
            }

            foreach (DataRow dr in _dsParseResults.Tables[0].Rows) // Loop Sentences
            {

                if (dr[ParseResultsFields.UID].ToString() == uid)
                {
                    file = string.Concat(AppFolders.DocParsedSec, @"\", uid, ".rtf");

                    foreach (DataRow drKeywords in dsKeywords.Tables[0].Rows) // Loop Selected Keywords
                    {
                        word = drKeywords[KeywordsFoundFields.Keyword].ToString();
                        color = drKeywords[KeywordsFoundFields.ColorHighlight].ToString();

                        int qtyFound = HighlightText2(file, word, isWholeKeywords, color, false); // Find Keyword and highlight it in Dark Green
                        //int currentKwQty = (int)drKeywords[KeywordsFoundFields.Count];
                        //drKeywords[KeywordsFoundFields.Count] = currentKwQty + qtyFound; // Set the Qty found for the Current Keyword
                        count = count + qtyFound;

                        if (qtyFound > 0)
                        {
                            count = count + qtyFound;
                            // Save Found Keyword results to Dataset
                            DataRow row = dsFoundKeywords.Tables[0].NewRow();
                            row[KeywordsFoundFields.UID] = FoundKwUID;
                            row[KeywordsFoundFields.Keyword] = word;
                            row[KeywordsFoundFields.Count] = qtyFound;
                            row[KeywordsFoundFields.SegmentUID] = uid;

                            dsFoundKeywords.Tables[0].Rows.Add(row);

                            //row[NP_Keywords.UID] = _FoundKwUID;
                            //row[NP_Keywords.KeyWord] = word;
                            //row[NP_Keywords.SentenceUID] = uid;
                            //_dsFoundKeywords.Tables[0].Rows.Add(row);

                            //_FoundKwUID++;

                            if (keywords == string.Empty)
                            {
                                keywords = string.Concat(word, " [", qtyFound.ToString(), "]");
                            }
                            else
                            {
                                keywords = string.Concat(keywords, ", ", word, " [", qtyFound.ToString(), "]");
                            }

                            FoundKwUID++;

                        }

                        dr[ParseResultsFields.Keywords] = keywords;

                    }
                }

            }

            gDataMgr.SaveDataXML(_dsParseResults, parseResultsFile);
            gDataMgr.SaveDataXML(dsFoundKeywords, keywordFile);

            gDataMgr = null;

            return count;
        }

        public int FindKeywordsInDoc(string DocFile, string SavePath, DataSet dsKeywords, bool WholeWord)
        {
            int count = 0;
            string word = string.Empty;
            string color = "YellowGreen";

            _rtfCrtl.Clear();
            _rtfCrtl.LoadFile(DocFile, RichTextBoxStreamType.PlainText);

            foreach (DataRow drKeywords in dsKeywords.Tables[0].Rows) // Loop Selected Keywords
            {
                word = drKeywords[KeywordsFoundFields.Keyword].ToString();
                color = drKeywords[KeywordsFoundFields.ColorHighlight].ToString();

                int qtyFound = HighlightTextInDoc(word, WholeWord, color, false);
                count = count + qtyFound;
            }

            string fileName = Atebion.Common.Files.GetFileNameWOExt(DocFile);
            string file = string.Concat(fileName, ".rtf");

            string pathFile = Path.Combine(SavePath, file);

            _rtfCrtl.SaveFile(pathFile, System.Windows.Forms.RichTextBoxStreamType.RichText);

            return count;

        }


        public int FindKeywords(DataSet dsKeywords, Color SelectColor, bool WholeWord, bool isUseDefaultParseAnalysis)
        {
            string word = string.Empty;
            int count = 0;
            int FoundKwUID = 0;
            string keywords = string.Empty;
            string file = string.Empty;

            string parseResultsFile = string.Empty;
            if (isUseDefaultParseAnalysis) // Defualt
                parseResultsFile = Path.Combine(AppFolders.DocParsedSecXML, "ParseResults.xml");
            else // Analysis
                parseResultsFile = Path.Combine(AppFolders.AnalysisParseSegXML, "ParseResults.xml");
                //parseResultsFile = Path.Combine(AppFolders.AnalysisParseSegKeywords, "ParseResults.xml");

            string keywordFile = string.Empty;
            if (isUseDefaultParseAnalysis) // Defualt
                keywordFile = Path.Combine(AppFolders.DocParsedSecXML, "KeywordsFound2.xml");
            else // Analysis
                keywordFile = Path.Combine(AppFolders.AnalysisParseSegXML, "KeywordsFound2.xml");
               // keywordFile = Path.Combine(AppFolders.AnalysisParseSegKeywords, "KeywordsFound2.xml");

            _ErrorMsg = string.Empty;

            if (_dsParseResults == null)
            {
                if (File.Exists(parseResultsFile))
                {
                    _dsParseResults = LoadDatasetFromXml(parseResultsFile);
                }
                else
                {
                    _ErrorMsg = "Unable to find ParseResults.xml file!";
                    return -1;
                }
            }

            DataTable dtFoundKeywords = createTableKeywordsFound();
            string color = "YellowGreen";
            string library = string.Empty;

            foreach (DataRow dr in _dsParseResults.Tables[0].Rows) // Loop Sentences
            {
                keywords = string.Empty;
                string uid = dr[ParseResultsFields.UID].ToString();
                if (isUseDefaultParseAnalysis)
                {
                    file = string.Concat(AppFolders.DocParsedSec, @"\", uid, ".rtf");
                }
                else
                {
                    file = string.Concat(AppFolders.AnalysisParseSeg, @"\", uid, ".rtf");
                }

                foreach (DataRow drKeywords in dsKeywords.Tables[0].Rows) // Loop Selected Keywords
                {
                    word = drKeywords[KeywordsFoundFields.Keyword].ToString();
                    color = drKeywords[KeywordsFoundFields.ColorHighlight].ToString();
                    library = drKeywords[KeywordsFoundFields.KeywordLib].ToString();

                   // int qtyFound = HighlightText(file, word, color, false); // Find Keyword and highlight it in Dark Green
                    int qtyFound = HighlightText2(file, word, WholeWord, color, false);
                    count = count + qtyFound;

                    if (qtyFound > 0)
                    {
                        count = count + qtyFound;
                        // Save Found Keyword results to Dataset
                        DataRow row = dtFoundKeywords.NewRow();
                        row[KeywordsFoundFields.UID] = FoundKwUID;
                        row[KeywordsFoundFields.Keyword] = word;
                        row[KeywordsFoundFields.Count] = qtyFound;
                        row[KeywordsFoundFields.SegmentUID] = uid;
                        row[KeywordsFoundFields.KeywordLib] = library;

                        dtFoundKeywords.Rows.Add(row);

                        //row[NP_Keywords.UID] = _FoundKwUID;
                        //row[NP_Keywords.KeyWord] = word;
                        //row[NP_Keywords.SentenceUID] = uid;
                        //_dsFoundKeywords.Tables[0].Rows.Add(row);

                        //_FoundKwUID++;

                        if (keywords == string.Empty)
                        {
                            keywords = string.Concat(word, " [", qtyFound.ToString(), "]");
                        }
                        else
                        {
                            keywords = string.Concat(keywords, ", ", word, " [", qtyFound.ToString(), "]");
                        }

                        FoundKwUID++;

                    }

                    dr[ParseResultsFields.Keywords] = keywords;

                }

            }

            GenericDataManger dataMgr = new GenericDataManger();
            dataMgr.SaveDataXML(_dsParseResults, parseResultsFile);

            if (File.Exists(keywordFile))
            {
                File.Delete(keywordFile);
            }
            DataSet dsKeywordsFound = new DataSet();
            dsKeywordsFound.Tables.Add(dtFoundKeywords);
            dataMgr.SaveDataXML(dsKeywordsFound, keywordFile);

            return count;

        }

        public int HighlightTextInDoc(string word, bool wholeWord, string color, bool HighlightText)
        {
            int count = 0;

            if (color == string.Empty)
                color = "YellowGreen";

           

            string txt = _rtfCrtl.Text;

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

            //Added 05.07.2019 in the PDA & Added in the New Edition on 02.03.2020
            if (word == "?")
            {
                count = 0;
                for (int i = 0; i < txt.Length; i++)
                {
                    int x = txt.IndexOf("?", i);

                    if (x > -1)
                    {
                        i = x + 1;

                        _rtfCrtl.Select(x, 1);

                        if (HighlightText) // Highlight Text
                            _rtfCrtl.SelectionColor = Color.FromName(color);
                        else // Highlight Background
                            _rtfCrtl.SelectionBackColor = Color.FromName(color);

                        count++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {

                Regex regex = new Regex(adjWord, RegexOptions.IgnoreCase);

                MatchCollection matches = regex.Matches(txt);
                int index = -1;
                int startIndex = 0;

                count = matches.Count;

                if (count > 0)
                {

                    foreach (Match match in matches)
                    {
                        index = match.Index;
                        //if (wholeWord)
                        //{
                        //    _rtfCrtl.Select(index + 1, word.Length);
                        //}
                        //else
                        //{
                        _rtfCrtl.Select(index, word.Length);
                        //}

                        if (HighlightText) // Highlight Text
                            _rtfCrtl.SelectionColor = Color.FromName(color);
                        else // Highlight Background
                            _rtfCrtl.SelectionBackColor = Color.FromName(color);

                        startIndex = index + word.Length;
                    }


                }
            }


            return count;
        }

        public int HighlightText2(string file, string word, bool wholeWord, string color, bool HighlightText)
        {
            int count = 0;

            if (color == string.Empty)
                color = "YellowGreen";

            _rtfCrtl.Clear();

            if (!File.Exists(file))
            {
                return count;
            }

            _rtfCrtl.LoadFile(file, System.Windows.Forms.RichTextBoxStreamType.RichText);

            string txt = _rtfCrtl.Text;

            string adjWord;
            if (wholeWord)
            {
                 // adjWord = "\\W?(" + word + ")\\W?";
                //adjWord = "\\w(" + word + ")\\w";
                //adjWord = @"\b(" + word + @")\b";
                 adjWord = "\\b" + word + "\\b";
                //adjWord = string.Concat(@"(?<TM>\w*", word, @"\w*)");
            }
            else
            {
                adjWord = word;
            }

            //Added 05.07.2019 in PDA, but added in the New Addition 02.03.2020
            if (word == "?")
            {
                count = 0;
                for (int i = 0; i < txt.Length; i++)
                {
                    int x = txt.IndexOf("?", i);

                    if (x > -1)
                    {
                        i = x + 1;

                        _rtfCrtl.Select(x, 1);

                        if (HighlightText) // Highlight Text
                            _rtfCrtl.SelectionColor = Color.FromName(color);
                        else // Highlight Background
                            _rtfCrtl.SelectionBackColor = Color.FromName(color);

                        count++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {

                Regex regex = new Regex(adjWord, RegexOptions.IgnoreCase);

                MatchCollection matches = regex.Matches(txt);
                int index = -1;
                int startIndex = 0;

                count = matches.Count;

                if (count > 0)
                {

                    foreach (Match match in matches)
                    {
                        index = match.Index;
                        //if (wholeWord)
                        //{
                        //    _rtfCrtl.Select(index + 1, word.Length);
                        //}
                        //else
                        //{
                        _rtfCrtl.Select(index, word.Length);
                        //}

                        if (HighlightText) // Highlight Text
                            _rtfCrtl.SelectionColor = Color.FromName(color);
                        else // Highlight Background
                            _rtfCrtl.SelectionBackColor = Color.FromName(color);

                        startIndex = index + word.Length;
                    }

                    _rtfCrtl.SaveFile(file, System.Windows.Forms.RichTextBoxStreamType.RichText);
                }
            }


            return count;
        }

        //private int HighlightText(string file, string word, string color, bool HighlightText)
        //{
        //    int count = 0;

        //    if (color == string.Empty)
        //        color = "YellowGreen";

        //    _rtfCrtl.Clear();

        //    if (!File.Exists(file)) // Added 08.16.2016 -- Fixed bug in ver.1.7.16.04 -- The last row in datagridview is sometimes blank
        //    {
        //        return count;
        //    }

        //    _rtfCrtl.LoadFile(file, System.Windows.Forms.RichTextBoxStreamType.RichText);

        //    int s_start = _rtfCrtl.SelectionStart, startIndex = 0, index;

        //    while ((index = _rtfCrtl.Text.IndexOf(word, startIndex, StringComparison.CurrentCultureIgnoreCase)) != -1) // 01.04.2018 Added StringComparison.CurrentCultureIgnoreCase)) != -1)
        //    {
        //        _rtfCrtl.Select(index, word.Length);

        //        if (HighlightText) // Highlight Text
        //            _rtfCrtl.SelectionColor = Color.FromName(color);
        //        else // Highlight Background
        //            _rtfCrtl.SelectionBackColor = Color.FromName(color);

        //        startIndex = index + word.Length;
        //        count++;
        //    }

        //    if (count > 0)
        //        _rtfCrtl.SaveFile(file, System.Windows.Forms.RichTextBoxStreamType.RichText);

        //    //_rtfCrtl.SelectionStart = s_start;
        //    //_rtfCrtl.SelectionLength = 0;
        //    //_rtfCrtl.SelectionColor = Color.Black;



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

        public int DeleteKeywordsSec(string uid)
        {
            int returnResult = 0;
            string keywordFile = Path.Combine(_ParseSecXML, "KeywordsFound2.xml");

            if (!File.Exists(keywordFile))
            {
                _ErrorMsg = string.Concat("Unable to find file ", keywordFile);
                return returnResult;
            }

            // FoundKeywords
            GenericDataManger gDataMgr = new GenericDataManger();
            DataSet dsFoundKeywords = gDataMgr.LoadDatasetFromXml(keywordFile);
            if (dsFoundKeywords.Tables[0].Rows.Count == 0)
            {
                return returnResult;
            }

            for (int i = dsFoundKeywords.Tables[0].Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr1 = dsFoundKeywords.Tables[0].Rows[i];
                if (dr1[KeywordsFoundFields.SegmentUID] == uid)
                {
                    dr1.Delete();
                    returnResult++;
                }
            }

            gDataMgr.SaveDataXML(dsFoundKeywords, keywordFile);
            return returnResult;
        }


        private DataTable createTableKeywordsLib()
        {
            DataTable table = new DataTable("Keywords");

            table.Columns.Add(KeywordsFoundFields.UID, typeof(string));
            table.Columns.Add(KeywordsFoundFields.Keyword, typeof(string));
            table.Columns.Add(KeywordsFoundFields.ColorHighlight, typeof(string));
            table.Columns.Add(KeywordsFoundFields.Count, typeof(int)); // Used for Deep Analysis only 
            table.Columns.Add(KeywordsFoundFields.KeywordLib, typeof(string));

            return table;
        }

        private DataTable createTableKeywordsFound()
        {
            DataTable table = new DataTable("KeywordsFound");

            table.Columns.Add(KeywordsFoundFields.UID, typeof(string));
            table.Columns.Add(KeywordsFoundFields.Keyword, typeof(string));
            table.Columns.Add(KeywordsFoundFields.Count, typeof(int));
            table.Columns.Add(KeywordsFoundFields.SegmentUID, typeof(int));
            table.Columns.Add(KeywordsFoundFields.KeywordLib, typeof(string));

            return table;
        }
        

    }
}
