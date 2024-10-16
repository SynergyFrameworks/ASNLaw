using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.Configuration;
using System.Linq;
using System.IO;

//using hOOt;
using System.Threading;
using System.Diagnostics;

using System.Text.RegularExpressions;

using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;

using Lucene.Net.Util;

using Atebion.Common;



namespace Atebion.DeepAnalytics
{
    public class Analysis
    {

        ~Analysis()
        {
            //if (hoot != null)
            //{

            //    hoot.FreeMemory();
            //    hoot.Shutdown();
            //    hoot = null;
            //}

            System.GC.Collect();
        }

        // Declare delegate for when analysis has completed
        public delegate void ProcessHandler();

        [Category("Action")]
        [Description("Fires when Building the Data Structure.")]
        public event ProcessHandler BuildingStructure;

        [Category("Action")]
        [Description("Fires when the Parsing Sentences")]
        public event ProcessHandler Parsing;

        [Category("Action")]
        [Description("Fires when Finding Keywords in Sentences")]
        public event ProcessHandler FindingKeyword;

        [Category("Action")]
        [Description("Fires when Building Hoot Search Engine Index")]
        public event ProcessHandler BuildingSearchIndex;


        public string CurrentDocPath
        {
            get { return AppFolders.CurrentDocPath; }
            set { AppFolders.CurrentDocPath = value; }
        }

        public string XMLPath
        {
            get { return AppFolders.XML; }
        }

        public string ParseSentencesPath
        {
            get { return AppFolders.ParseSentences; }
        }

        public string NotesPath
        {
            get { return AppFolders.Notes; }
        }

        public string Index2Path
        {
            get { return AppFolders.Index2; }
        }

        public string IndexPath
        {
            get { return AppFolders.Index; }
        }

        public string GetHootIndexPath
        {
            get { return AppFolders.HootIndexPath; }
        }

        public string ExportPath
        {
            get { return AppFolders.Export; }
        }

        public string HTMLPath
        {
            get { return AppFolders.HTML; }
        }

        private string _foundKwFile = string.Empty;

        public string FoundKeywordsFile
        {
            get
            {
                _foundKwFile = string.Concat(AppFolders.XML, @"\FoundKeywords.xml");
                return _foundKwFile;
            }
        }

        string _foundKwSumFile = string.Empty;
        public string FoundSumKeywordsFile
        {
            get
            {
                _foundKwSumFile = string.Concat(AppFolders.XML, @"\FoundSumKeywords.xml");
                return _foundKwSumFile;
            }
        }

        public string SentencesFile
        {
            get
            {
                _SentencesXMLFile = string.Concat(AppFolders.XML, @"\Sentences.xml");
                return _SentencesXMLFile;
            }
        }


        private string _ModelPath; // a sub folder "Model" under the application path
        private string _SentencesXMLFile; // File that holds parsed Sentences for each parse segment.
        private DataSet _dsSentences; // Hold parsed Sentences Information
        private DataSet _dsSelectedKeywords; // Holds the selected keywords from the Document Analysis 
        private DataSet _dsFoundKeywords;

        private int _FoundKwUID = 0;

        private string _ErrorMessage = string.Empty;

        private int _sortOrder = -1;

        private OpenNLP.Tools.SentenceDetect.MaximumEntropySentenceDetector mSentenceDetector;
        //private OpenNLP.Tools.Tokenize.EnglishMaximumEntropyTokenizer mTokenizer;
        //private OpenNLP.Tools.PosTagger.EnglishMaximumEntropyPosTagger mPosTagger;
        //private OpenNLP.Tools.Chunker.EnglishTreebankChunker mChunker;
        //private OpenNLP.Tools.Parser.EnglishTreebankParser mParser;
        //private OpenNLP.Tools.NameFind.EnglishNameFinder mNameFinder;
        //private OpenNLP.Tools.Lang.English.TreebankLinker mCoreferenceFinder;

        private const string SENTENCES = "Sentences";

        private RichTextBox _rtfCrtl;

        GenericDataManger _gDataMgr;

        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        public int Analyze(string currentDocPath, string SourceFolder, string ModelFolder, DataSet SelectedKeywords)
        {
            _dsSelectedKeywords = SelectedKeywords; // Holds Selected Keywords from the Document Analysis process
            _dsFoundKeywords = CreateDataset_FoundKeywords();

            Stopwatch watchEntireProcess = new Stopwatch();
            Stopwatch watchStructure = new Stopwatch();
            Stopwatch watchParse = new Stopwatch();
            Stopwatch watchFindKeywords = new Stopwatch();
            Stopwatch watchSearchIndex = new Stopwatch();

            watchEntireProcess.Start();

            if (BuildingStructure != null) // Throw Status Event
                BuildingStructure();

            // **** START Structure ****
            watchStructure.Start();

            // Create & Validate folders
            if (!AppFolders.ValidatePaths(currentDocPath))
            {
                _ErrorMessage = AppFolders.ErrorMessage;
                return -1;
            }

            _ModelPath = string.Concat(ModelFolder, @"\");

            // Archive any previous parse results
            string nrMsg = string.Empty;
            GetNextRevisionDir(true, out nrMsg);
            if (nrMsg != string.Empty)
            {
                _ErrorMessage = nrMsg;
                return -2;

            }


            _SentencesXMLFile = SentencesFile;
            _rtfCrtl = new RichTextBox();
            _gDataMgr = new GenericDataManger();

  

            watchStructure.Stop();
            // END Structure

            // **** START Parse ***
            if (Parsing != null) // Throw Status Event
                Parsing();

            watchParse.Start();

             // Read Parsed Segment 
             string parsedSegmentsXMLFile = string.Concat(SourceFolder, @"\XML\ParseResults.xml");
             if (!File.Exists(parsedSegmentsXMLFile))
             {
                 _ErrorMessage = string.Concat("Unable to locate file ", parsedSegmentsXMLFile);
                 return -3;
             }
             DataSet dsParsedSegments = _gDataMgr.LoadDatasetFromXml(parsedSegmentsXMLFile);
             DataView view;
             view = dsParsedSegments.Tables[0].DefaultView;
             view.Sort = "SortOrder ASC";

             string sFile = string.Empty;
             string ext = string.Empty;
             string UID = string.Empty;
             string Number = string.Empty;
             string Caption = string.Empty;
             int LineStart = 0;
             int Page = 0;

             bool containsPageNoCol = dsParsedSegments.Tables[0].Columns.Contains("Page");

             // Create Sentence Database
             _dsSentences = CreateDataset_Sentences(containsPageNoCol);

             foreach (DataRowView rowView in view) // Loop through parsed segments dataset
             {

                 sFile = rowView["FileName"].ToString();
                 UID = rowView["UID"].ToString();
                 Number = rowView["Number"].ToString();
                 Caption = rowView["Caption"].ToString();

                 if (rowView["LineStart"].ToString().Length == 0) // Added if statement 12.18.2018
                 {
                     LineStart++;
                 }
                 else
                 {
                     LineStart = Convert.ToInt32(rowView["LineStart"].ToString());
                 }

                 if (containsPageNoCol)
                 {
                     if (rowView["Page"].ToString() != string.Empty) // Added 01.15.2019
                     {
                         if (DataFunctions.IsNumeric2(rowView["Page"].ToString()))
                         {
                            Page = Convert.ToInt32(rowView["Page"].ToString());
                         }
                     }
                 }


                 if (ext == string.Empty)
                 {
                     string[] fileParts = sFile.Split('.');
                     ext = fileParts[1];
                 }

                 sFile = string.Concat(SourceFolder, @"\", sFile);

                 // Get text from each document file from the Source Folder

                string sText = GetText(sFile, ext);

                // Split Source Text into to sentences
                string[] sSentences = SplitSentences(sText);

                // Save each sentence as a file into the Desination folder and save parse info into an XML file in the Desination XML folder
                SaveSentencesInfo(sSentences, UID, Number, Caption, containsPageNoCol, Page, LineStart);

              }

             // Save Parse Sentences result to an XML file
             _gDataMgr.SaveDataXML(_dsSentences, _SentencesXMLFile);

             watchParse.Stop();
             // END Parse

            // **** START Find Keywords ****
             if (FindingKeyword != null)
                 FindingKeyword(); // Throw Status Event

             watchFindKeywords.Start();

             // Is Find Keayword by Whole Word?
             bool isWholeKeywords = false;
             string xIsWholeKeywords = "No";
             string isWholeKeywordsPathFile = Path.Combine(AppFolders.DocParsedSecKeywords, "isWholeKeywords.txt");
             if (File.Exists(isWholeKeywordsPathFile))
             {
                 xIsWholeKeywords = Files.ReadFile(isWholeKeywordsPathFile);
                 if (xIsWholeKeywords.IndexOf("Yes") != -1)
                 {
                     isWholeKeywords = true;
                 }
             }


            // Find Keywords
             int keywordsCount = findKeywords(isWholeKeywords);

             // Save Found Keywords result to an XML file
           // string foundKwFile = string.Concat(AppFolders.XML, @"\FoundKeywords.xml");
            _gDataMgr.SaveDataXML(_dsFoundKeywords, FoundKeywordsFile);

            // Save Keyword Summary to an XML file
         //   string foundKwSumFile = string.Concat(AppFolders.XML, @"\FoundSumKeywords.xml");
            _gDataMgr.SaveDataXML(_dsSelectedKeywords, FoundSumKeywordsFile);
            

            watchFindKeywords.Stop();
            // END Find Keywords

            // *** START Building Search Index ****
            if (BuildingSearchIndex != null)
                BuildingSearchIndex();

            watchSearchIndex.Start();
            // Index Parse Sentences
            CreateIndex(); // Lucene search engine (indexer)
           // Hoot-> ChkRunIndexer();

            watchSearchIndex.Stop();
            // END Building Search Index

            watchEntireProcess.Stop();

            // Save/Write Stats
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Process,Time (Milliseconds)");
            sb.AppendLine(string.Concat("Build Structure,", watchStructure.Elapsed.Milliseconds.ToString()));
            sb.AppendLine(string.Concat("Parse Sentences,", watchParse.Elapsed.Milliseconds.ToString()));
            sb.AppendLine(string.Concat("Find Keywords,", watchFindKeywords.Elapsed.Milliseconds.ToString()));
            //sb.AppendLine(string.Concat("Build Search Index,", watchStructure.Elapsed.Milliseconds.ToString()));
            var totalTime = watchStructure.Elapsed.Milliseconds + watchParse.Elapsed.Milliseconds + watchFindKeywords.Elapsed.Milliseconds + watchStructure.Elapsed.Milliseconds;
            sb.AppendLine(string.Concat("Total,", totalTime.ToString()));

            sb.AppendLine(" , ");
            sb.AppendLine("Item,Count");
            sb.AppendLine(string.Concat("Sentences,", _dsSentences.Tables[0].Rows.Count.ToString()));
            sb.AppendLine(string.Concat("Keywords,", keywordsCount.ToString()));

            string statFile = string.Concat(AppFolders.XML, @"\Stats.csv");
            Files.WriteStringToFile(sb.ToString(), statFile, true);

            return 0;
        }

        private string GetText(string desFile, string ext)
        {

            if (ext.ToLower() == "rtf")
                _rtfCrtl.LoadFile(desFile);
            else
                _rtfCrtl.LoadFile(desFile, RichTextBoxStreamType.PlainText);

            string txt = _rtfCrtl.Text;

            _rtfCrtl.Clear();

            return txt;

        }

  
      private bool ContainsColumn(string columnName, DataTable table)
      {
        DataColumnCollection columns = table.Columns;

        if (columns.Contains(columnName))
        {
            return true;
        }

        return false;
      }

      private int findKeywords(bool isWholeKeywords)
        {
            string word = string.Empty;
            int count = 0;
            string keywords = string.Empty;
            string color = string.Empty;
            string file = string.Empty;

            bool isColorColExists = false;
            isColorColExists = ContainsColumn("Color", _dsSelectedKeywords.Tables[0]);
            
            foreach (DataRow dr in _dsSentences.Tables[0].Rows) // Loop Sentences
            {
                keywords = string.Empty;
                string sentenceUID = dr[NP_Sentences.UID].ToString();
                file = string.Concat(AppFolders.ParseSentences, @"\", sentenceUID, ".rtf");

                foreach (DataRow drKeywords in _dsSelectedKeywords.Tables[0].Rows) // Loop Selected Keywords
                {
                    word = drKeywords["Keyword"].ToString();
                    if (isColorColExists)
                    {
                        color = drKeywords["Color"].ToString();
                    }
                    else
                    {
                        color = "LightGreen";
                    }
                    
                    int qtyFound = HighlightText2(file, word, isWholeKeywords, color, false); // Find Keyword and highlight it in Dark Green
                    int currentKwQty = 0;
                    if (drKeywords["Count"].ToString() != string.Empty)
                    {
                        currentKwQty = (int)drKeywords["Count"];
                    }
                  //  int currentKwQty = (int)drKeywords["Count"];
                    drKeywords["Count"] = currentKwQty + qtyFound; // Set the Qty found for the Current Keyword
                    count = count + qtyFound;

                    if (qtyFound > 0)
                    {
                        // Save Found Keyword results to Dataset
                        DataRow row = _dsFoundKeywords.Tables[0].NewRow();
                        row[NP_Keywords.UID] = _FoundKwUID;
                        row[NP_Keywords.KeyWord] = word;
                        row[NP_Keywords.SentenceUID] = sentenceUID;
                        _dsFoundKeywords.Tables[0].Rows.Add(row);

                        _FoundKwUID++;

                        if (keywords == string.Empty)
                        {
                            keywords = string.Concat(word, " [", qtyFound.ToString(), "]");
                        }
                        else
                        {
                            keywords = string.Concat(keywords, ", ", word, " [", qtyFound.ToString(), "]");
                        }
                    }

                    dr[NP_Sentences.Keywords] = keywords; 
                 
                }
                   
            }

            _gDataMgr.SaveDataXML(_dsSentences, _SentencesXMLFile);

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
              //  adjWord = "\\W?(" + word + ")\\W?";
              adjWord = @"\b(" + word + @")\b";
          }
          else
          {
              adjWord = word;
          }

            //Added 05.08.2019
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

                      _rtfCrtl.SaveFile(file, System.Windows.Forms.RichTextBoxStreamType.RichText);

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

        private int HighlightText(string file, string word, string sColor, bool HighlightText)
        {
            int count = 0;

            Color color = Color.FromName(sColor);

            _rtfCrtl.LoadFile(file, RichTextBoxStreamType.RichText);

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
                _rtfCrtl.SaveFile(file);

            //_rtfCrtl.SelectionStart = s_start;
            //_rtfCrtl.SelectionLength = 0;
            //_rtfCrtl.SelectionColor = Color.Black;

            return count;
        }


        private int SaveSentencesInfo(string[] Sentences, string ParentUID, string Number, string Caption, bool ContainsPageNo, int PageNo, int LineStart)
        {
            int total = Sentences.Length;
            int count = 0;
            string newUID;
            string newFile;

            foreach (string sentence in Sentences)
            {
                DataRow row = _dsSentences.Tables[0].NewRow();
                newUID = string.Concat(ParentUID, "_", count.ToString());
                row[NP_Sentences.UID] = newUID;
              //  row[NP_Sentences.Number] = Number;
                row[NP_Sentences.Number] = string.Concat(Number, "-S", Convert.ToString(count + 1));
                row[NP_Sentences.Caption] = Caption;
                row[NP_Sentences.Sentence] = sentence;
                row[NP_Sentences.SortOrder] = _sortOrder;
                row[NP_Sentences.LineStart] = LineStart;

                if (ContainsPageNo)
                {
                    row[NP_Sentences.Page] = PageNo;
                }

                _dsSentences.Tables[0].Rows.Add(row); // Add new row

                newFile = string.Concat(AppFolders.ParseSentences, @"\", newUID, ".rtf");
                _rtfCrtl.Text = sentence;
                _rtfCrtl.Font = new Font(_rtfCrtl.Font.Name, 10); // Added 02.07.2015
             //   _rtfCrtl.BackColor = Color.AntiqueWhite; // Added 02.07.2015
                _rtfCrtl.SaveFile(newFile, RichTextBoxStreamType.RichText); // write sentence to a text file, syntax: [parent UID] "_" [line count] ".txt"
              //  System.IO.File.WriteAllText(newFile, sentence); // write sentence to a text file, syntax: [parent UID] "_" [line count] ".txt"


                _sortOrder++;
                count++;

            }

            return count;

        }

        public string GetNextRevisionDir(bool RenameDir, out string message)
        {
            message = string.Empty;

            if (AppFolders.ParseSentences == string.Empty) 
                return message;

            try
            {
                string[] files = System.IO.Directory.GetFiles(AppFolders.ParseSentences);

                if (files.Length == 0)
                {
                    return string.Empty;
                }
            }
            catch (Exception ex1) 
            {
                message = string.Concat("Error reading folder.", " -- Error: ", ex1.Message);
                return string.Empty;
            }

            for (int i = 1; i < 32000; i++)
            {

                string xPath = string.Concat(AppFolders.DeepAnalyticsPath, @"\", i.ToString());
                if (!System.IO.Directory.Exists(xPath))
                {
                    if (!RenameDir)
                        return xPath;

                    try
                    {
                        // Rename Current dir to Revision dir
                        System.IO.Directory.Move(AppFolders.CurrentDocPath, xPath);
                        if (!System.IO.Directory.Exists(xPath))
                        {
                            _ErrorMessage = "Unable to replace the existing Document until all associated files have been closed.";
                            return _ErrorMessage;
                        }

                        message = string.Concat("The existing Document file and all associated files (i.e. parsed segments & notes) have been saved as Revision ", i.ToString(), " and can be found in folder: ", xPath);


                        // Create Current dir again
                        string newCurrentDocPath = AppFolders.CurrentDocPath;
                        if (newCurrentDocPath == string.Empty)
                            return _ErrorMessage;
                        else
                            return xPath;
                    }
                    catch (Exception ex)
                    {
                        return string.Concat(ex.Message, " -- ", "Unable to replace the existing Document until all associated files have been closed.");
                    }
                }
            }

            _ErrorMessage = "Exceeded Max. RFP file Revisions of 32,000. ";
            return string.Empty;
        }

        //private string GetFileName(string pathFile, out string ext)
        //{
        //    FileInfo objFile = new FileInfo(pathFile);
        //    ext = objFile.Extension;
        //    ext = ext.Replace(".", string.Empty);
        //    return objFile.Name;
        //}

        #region Lucene

        private Analyzer analyzer = new StandardAnalyzer();
        private Lucene.Net.Store.Directory luceneIndexDirectory;
        private IndexWriter writer;
        private string indexPath = string.Empty;

        RichTextBox _rtfCtrl = new RichTextBox();
        private string ReadRTFFile(string RTFFile)
        {
            _rtfCtrl.LoadFile(RTFFile);
            return _rtfCtrl.Text;
        }

        private void CreateIndex()
        {
            string indexPath = AppFolders.Index2;
            string searchDir = AppFolders.ParseSentences;

            try // Added 07.27.2019
            {

                if (System.IO.Directory.Exists(indexPath))
                {
                    System.IO.Directory.Delete(indexPath, true);
                }

                // Initialise Lucene
                luceneIndexDirectory = FSDirectory.GetDirectory(indexPath, true);
                writer = new IndexWriter(luceneIndexDirectory, analyzer, true);

                string[] files = Files.GetFilesFromDir(searchDir, "*.*");

                string currentFile = string.Empty;
                string currentText = string.Empty;
                for (int i = 0; i < files.Count(); i++)
                {
                    currentFile = string.Concat(searchDir, @"\", files[i]);
                    currentText = ReadRTFFile(currentFile);

                    Document doc = new Document();

                    doc.Add(new Field("SearchFile", files[i], Field.Store.YES, Field.Index.UN_TOKENIZED));
                    doc.Add(new Field("SearchText", currentText, Field.Store.YES, Field.Index.TOKENIZED));

                    writer.AddDocument(doc);

                }

                writer.Optimize();
                writer.Flush();
                writer.Close();
                luceneIndexDirectory.Close();
            }
            catch (Exception ex)
            {
                string pathFile =  Path.Combine(indexPath, "Error.txt");
                Files.WriteStringToFile(ex.Message, pathFile);

                //HootSearchEng hootSearchEng = new HootSearchEng();
                //hootSearchEng.ChkRunIndexer(AppFolders.Index, AppFolders.ParseSentences);
            }
        }

        #endregion

        #region Hoot

        //Hoot hoot;

        //BackgroundWorker backgroundWorker1 = new System.ComponentModel.BackgroundWorker();

        //private void ChkRunIndexer()
        //{
        //    //DateTime dtIndex = Files.GetLatestFileDatetime(AppFolders.DocParsedSecIndex);
        //    //DateTime dtParsedFiles = Files.GetLatestFileDatetime(AppFolders.DocParsedSec);

        //    if (hoot != null)
        //    {
        //        hoot.FreeMemory();
        //        hoot.Shutdown();
        //        hoot = null;
        //    }

        //    // Delete all Index files -- Added 07.12.2014
        //    //System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(AppFolders.DocParsedSecIndex);

        //    //foreach (FileInfo file in downloadedMessageInfo.GetFiles())
        //    //{
        //    //    file.Delete();
        //    //}
        //    //


        //    try
        //    {
        //        hoot = new Hoot(AppFolders.Index, "index", true);

 
        //            RunIndexer();
  
                
        //    }
        //    catch (Exception ex)
        //    {
        //        _ErrorMessage = string.Concat("Search Loading Error: ", ex.Message);
        //    }
        //}

        //private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    string[] files = e.Argument as string[];
        //    BackgroundWorker wrk = sender as BackgroundWorker;
        //    int i = 0;
        //    foreach (string fn in files)
        //    {
        //        if (wrk.CancellationPending)
        //        {
        //            e.Cancel = true;
        //            break;
        //        }
        //        backgroundWorker1.ReportProgress(1, fn);
        //        try
        //        {
        //            if (hoot.IsIndexed(fn) == false)
        //            {
        //                TextReader tf = new EPocalipse.IFilter.FilterReader(fn);
        //                string s = "";
        //                if (tf != null)
        //                    s = tf.ReadToEnd();

        //                hoot.Index(new Document(new FileInfo(fn), s), true);
        //            }
        //        }
        //        catch { }
        //        i++;
        //        if (i > 1000)
        //        {
        //            i = 0;
        //            hoot.Save();
        //        }
        //    }
        //    hoot.Save();
        //    hoot.OptimizeIndex();
        //}

        //private void RunIndexer()
        //{
        //    Cursor.Current = Cursors.WaitCursor; // Waiting

        //    if (hoot == null)
        //        hoot = new Hoot(AppFolders.Index, "index", true);

        //    string[] files = Directory.GetFiles(AppFolders.ParseSentences, "*", SearchOption.TopDirectoryOnly);


        //    //  backgroundWorker1.RunWorkerAsync(files);

        //    //string[] files = e.Argument as string[];
        //    //BackgroundWorker wrk = sender as BackgroundWorker;
        //    int i = 0;
        //    foreach (string fn in files)
        //    {
        //        //if (wrk.CancellationPending)
        //        //{
        //        //    e.Cancel = true;
        //        //    break;
        //        //}
        //        backgroundWorker1.ReportProgress(1, fn);
        //        try
        //        {
        //            if (hoot.IsIndexed(fn) == false)
        //            {
        //                TextReader tf = new EPocalipse.IFilter.FilterReader(fn);
        //                string s = "";
        //                if (tf != null)
        //                    s = tf.ReadToEnd();

        //                hoot.Index(new Document(new FileInfo(fn), s), true);
        //            }
        //        }
        //        catch { }
        //        i++;
        //        if (i > 1000)
        //        {
        //            i = 0;
        //            hoot.Save();
        //        }
        //    }
        //    hoot.Save();
        //    hoot.OptimizeIndex();

        //    hoot.FreeMemory();
        //    hoot.Shutdown(); // 2.14.2015
        //    if (hoot != null) // 7.7.2015
        //    {
        //        ((IDisposable)hoot).Dispose(); // 7.7.2015
        //    }
        //   // hoot = null; // 2.14.2015

        //    Cursor.Current = Cursors.Default; // Back to normal
        //}
        #endregion



        #region NLP methods

        private string[] SplitSentences(string paragraph)
        {

            string[] sentences = Regex.Split(paragraph, @"(?<=[\.!\?])\s+");

            //if (mSentenceDetector == null)
            //{
            //    mSentenceDetector = new OpenNLP.Tools.SentenceDetect.EnglishMaximumEntropySentenceDetector(_ModelPath + "EnglishSD.nbin");
            //}

            //string[] sentences = mSentenceDetector.SentenceDetect(paragraph);

            // ToDo Added 07.14.2017 - Adjustments to Sentences where for example, "This is the 1. reason we are making this change." -- Result is two Sentences, should be one.
            //int length = sentences.Length;
            //if (length == 0)
            //{
            //    return sentences;
            //}

            //List<string> sentences_Adj = new List<string>();
            //string sentence = string.Empty;
            //string sentence_Next = string.Empty;
            //for (int i = 0; i < length; i++)
            //{
            //    sentence = sentences[i];
            //    if ((i + 1) < length) // Not last sentence
            //    {
            //        sentence_Next = sentences[i + 1];
            //        if (!String.IsNullOrEmpty(sentence_Next) && Char.IsLetter(sentence_Next[0]))
            //        {
            //            if (!Char.IsUpper(sentence_Next[0]))
            //            {

            //            }
            //            else
            //            {

            //            }

            //        }


            //    }
            //}



            return sentences;//mSentenceDetector.SentenceDetect(paragraph);
        }


        //private DataSet CreateDataset_SumFoundKeywords()
        //{
        //    // Create a new DataTable.
        //    DataTable table = new DataTable("SumFoundKeywords");

        //    // Declare variables for DataColumn and DataRow objects.
        //    DataColumn column = null;

        //    // Create new DataColumn, set DataType, ColumnName 
        //    // and add to DataTable.    
        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.String");
        //    column.ColumnName = NP_SumKeywords.KeyWord;
        //    column.ReadOnly = false;
        //    column.Unique = false;

        //    // Add the Column to the DataColumnCollection.
        //    table.Columns.Add(column);

        //}

        private DataSet CreateDataset_FoundKeywords()
        {
            // Create a new DataTable.
            DataTable table = new DataTable("FoundKeywords");

            // Declare variables for DataColumn and DataRow objects.
            DataColumn column = null;

            // Create new DataColumn, set DataType, ColumnName 
            // and add to DataTable.    
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = NP_Keywords.UID;
            column.ReadOnly = false;
            column.Unique = false;

            // Add the Column to the DataColumnCollection.
            table.Columns.Add(column);

            // Create new DataColumn, set DataType, ColumnName 
            // and add to DataTable.    
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = NP_Keywords.KeyWord;
            column.ReadOnly = false;
            column.Unique = false;

            // Add the Column to the DataColumnCollection.
            table.Columns.Add(column);

            // Create new DataColumn, set DataType, ColumnName 
            // and add to DataTable.    
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = NP_Keywords.Count;
            column.ReadOnly = false;
            column.Unique = false;

            // Add the Column to the DataColumnCollection.
            table.Columns.Add(column);

            // Create new DataColumn, set DataType, ColumnName 
            // and add to DataTable.    
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = NP_Keywords.SentenceUID;
            column.ReadOnly = false;
            column.Unique = false;

            // Add the Column to the DataColumnCollection.
            table.Columns.Add(column);

            // Make the UID column the primary key column.
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = table.Columns[NP_Keywords.UID];
            table.PrimaryKey = PrimaryKeyColumns;

            // Instantiate the DataSet variable.
            DataSet dataSet = null;
            dataSet = new DataSet();

            // Add the new DataTable to the DataSet.
            dataSet.Tables.Add(table);

            //Return dataset
            return dataSet;

        }

        private DataSet CreateDataset_Sentences(bool IncludePage) 
        {
            // Create a new DataTable.
            DataTable table = new DataTable("Sentences");

            // Declare variables for DataColumn and DataRow objects.
            DataColumn column = null;

            // Create new DataColumn, set DataType, ColumnName 
            // and add to DataTable.    
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = NP_Sentences.UID;
            column.ReadOnly = false;
            column.Unique = false;

            // Add the Column to the DataColumnCollection.
            table.Columns.Add(column);

            // Create new DataColumn, set DataType, ColumnName 
            // and add to DataTable.    
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = NP_Sentences.Number;
            column.ReadOnly = false;
            column.Unique = false;

            // Add the Column to the DataColumnCollection.
            table.Columns.Add(column);

            // Create new DataColumn, set DataType, ColumnName 
            // and add to DataTable.    
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = NP_Sentences.Caption;
            column.ReadOnly = false;
            column.Unique = false;

            // Add the Column to the DataColumnCollection.
            table.Columns.Add(column);

            // Create new DataColumn, set DataType, ColumnName 
            // and add to DataTable.    
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = NP_Sentences.Sentence;
            column.ReadOnly = false;
            column.Unique = false;

            // Add the Column to the DataColumnCollection.
            table.Columns.Add(column);

            // Create new DataColumn, set DataType, ColumnName 
            // and add to DataTable.    
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = NP_Sentences.Keywords;
            column.ReadOnly = false;
            column.Unique = false;

            // Add the Column to the DataColumnCollection.
            table.Columns.Add(column);

            if (IncludePage) // Added 7.31.2018
            {
                // Create new DataColumn, set DataType, ColumnName 
                // and add to DataTable.    
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Int32");
                column.ColumnName = NP_Sentences.Page;
                column.ReadOnly = false;
                column.Unique = false;

                // Add the Column to the DataColumnCollection.
                table.Columns.Add(column);
            }

            // Added 7.31.2018
            // Create new DataColumn, set DataType, ColumnName 
            // and add to DataTable.    
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = NP_Sentences.LineStart;
            column.ReadOnly = false;
            column.Unique = false;

            // Add the Column to the DataColumnCollection.
            table.Columns.Add(column);


            // Create new DataColumn, set DataType, ColumnName 
            // and add to DataTable.    
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = NP_Sentences.SortOrder;
            column.ReadOnly = false;
            column.Unique = false;

            // Add the Column to the DataColumnCollection.
            table.Columns.Add(column);


            // Make the UID column the primary key column.
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = table.Columns[NP_Sentences.UID];
            table.PrimaryKey = PrimaryKeyColumns;

            // Instantiate the DataSet variable.
            DataSet dataSet = null;
            dataSet = new DataSet();

            // Add the new DataTable to the DataSet.
            dataSet.Tables.Add(table);

            //Return dataset
            return dataSet;

        }

        public class NP_Sentences
        {
            public const string UID = "UID";
            public const string Number = "Number";
            public const string Caption = "Caption";
            public const string Sentence = "Sentence";
            public const string SortOrder = "SortOrder";
            public const string Keywords = "Keywords";
            public const string Page = "Page";
            public const string LineStart = "LineStart";

        }

        public class NP_Keywords
        {
            public const string UID = "UID";
            public const string KeyWord = "Keyword";
            public const string SentenceUID = "SentenceUID";
            public const string Count = "Count";

        }

        //public class NP_SumKeywords
        //{
        //    public const string UID = "UID";
        //    public const string KeyWord = "Keyword";
        //    public const string Total = "Total";
        //}



        #endregion


        
    }
}
