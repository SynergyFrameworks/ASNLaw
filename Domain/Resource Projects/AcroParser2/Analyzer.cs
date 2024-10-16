using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.IO;
using System.Windows.Forms;


using System.Diagnostics; // Use for Timer

using Atebion.Common;

//using Microsoft.VisualBasic.CompilerServices;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Xml.Schema;


namespace AcroParser2
{

    public class RegMatches
    {
        public int matchesCount;
        public List<RegMatchFound> matchFound;
        public string sentence;
        public int sentenceNo;
    }
    public class MainSentences
    {
        public string sentence;
        public int sentenceNo;
    }


    public class RegMatchFound
    {
        public int matchIndex;
        public int matchLength;
        public string matchFoundAcronym;
        public bool foundFlag;

        public bool FoundFlag
        {
            get { return foundFlag; }
            set { foundFlag = value; }
        }
    }
    public class Analyzer
    {
        public Analyzer()
        {


        }


        // Output Properties
        public string[] Sentences
        {
            get { return _Sentences; }
        }

        //private DataTable _dtAcronymsUseB4Defined; // New - Added since the previous previous Parser Engine #1 
        //public DataTable dtAcronymsB4Defined
        //{
        //    get { return _dtAcronymsUseB4Defined; }
        //}

        private DataTable _dtAcronymsDocDefined; // ???
        public DataTable dtAcronymsDocDefined
        {
            get { return _dtAcronymsDocDefined; }
        }

        private DataTable _dtAcronymsDicDefined;
        public DataTable dtAcronymsDicDefined
        {
            get { return _dtAcronymsDicDefined; }
        }

        private DataTable _dtAcronymsFound;
        public DataTable dtAcronymsFound
        {
            get { return _dtAcronymsFound; }
        }

        private DataTable _dtAcronymsFoundButNotDefindFirstTime;
        public DataTable dtAcronymsFoundButNotDefindFirstTime
        {
            get { return _dtAcronymsFoundButNotDefindFirstTime; }
        }

        private DataTable _dtAcronymsNotDefined;
        public DataTable dtAcronymsNotDefinedFound
        {
            get { return _dtAcronymsNotDefined; }
        }

        private DataTable _dtAcronymsMultDefined;
        public DataTable dtAcronymsMultDefined
        {
            get { return _dtAcronymsMultDefined; }
        }

        private DataTable _dtAcronymsDiffDefined;
        public DataTable dtAcronymsDiffDefined
        {
            get { return _dtAcronymsDiffDefined; }
        }

        private DataSet _dsAcronyms;
        /// <summary>
        /// DataSet contains all the Acronyms results tables
        /// </summary>
        public DataSet dsAcronyms
        {
            get { return _dsAcronyms; }
        }

        private string _AnalysisLog = string.Empty;
        public string AnalysisLog
        {
            get { return _AnalysisLog; }
        }

        private StringBuilder _sbErrors = new StringBuilder();
        public string Errors
        {
            get { return _sbErrors.ToString(); }
        }

        private string _txtFile = string.Empty; // User Selected File

        // Parsing Vars
        private string[] _Sentences;
        private string _ModelPath = string.Empty; // a sub folder "Model" under the application path
        private string _SentencePath = string.Empty;
        private string _DictionaryName1 = string.Empty; // user selected dictionary 1 Name
        private string _DictionaryName2 = string.Empty; // user selected dictionary 1 Name
        DataTable _tdDictionary1; // user selected dictionary 1
        DataTable _tdDictionary2; // user selected dictionary 2
        DataTable _tdIgnoreDictionary; // User selected

        // Input Properties
        public string ModelPath
        {
            get { return _ModelPath; }
            set { _ModelPath = value; }
        }

        public string SentencePath
        {
            get { return _SentencePath; }
            set { _SentencePath = value; }
        }

        private string _IgnoreDictionaryName = string.Empty;
        public string IgnoreDictionaryName
        {
            get { return _IgnoreDictionaryName; }
            set { _IgnoreDictionaryName = value; }
        }

        private string _IgnoreDictionaryPath = string.Empty;
        public string IgnoreDictionaryPath
        {
            get { return _IgnoreDictionaryPath; }
            set { _IgnoreDictionaryPath = value; }
        }

        public string DictionaryName1
        {
            get { return _DictionaryName1; }
            set { _DictionaryName1 = value; }
        }

        public string DictionaryName2
        {
            get { return _DictionaryName2; }
            set { _DictionaryName2 = value; }
        }

        public DataTable tdDictionary1
        {
            get { return _tdDictionary1; }
            set { _tdDictionary1 = value; }
        }

        public DataTable tdDictionary2
        {
            get { return _tdDictionary2; }
            set { _tdDictionary2 = value; }
        }

        private string _DictionariesPath = string.Empty;
        public string DictionariesPath
        {
            get { return _DictionariesPath; }
            set { _DictionariesPath = value; }
        }

        private bool _UseDicDeepSearch = false;
        public bool UseDicDeepSearch
        {
            get { return _UseDicDeepSearch; }
            set { _UseDicDeepSearch = value; }
        }


        private OpenNLP.Tools.SentenceDetect.MaximumEntropySentenceDetector mSentenceDetector;
        private RichTextBox _rtfCrtl = new Atebion.Windows.Forms.RichTextBox();


        private int _UIDCounter = 0;
        private int _UIDDocDefined = 0;
        private int _UIDDicDefined = 0;
        private int _UIDDNotDefined = 0;
        private int _UIDMultiDefined = 0;
        private int _UIDDiffDefined = 0;

        private bool _isTestMode = false;

        StringBuilder _sbLog = new StringBuilder();

        private string _ErrorMsg = string.Empty;

        public string ErrorMessage
        {
            get { return _ErrorMsg; }

        }

        //public bool Analyze(string txtFile, string ModelFolder, string SentencePath, string DefinitionsPath, string DictionaryName1, string DictionaryName2 )
        //{
        //    _txtFile = txtFile;
        //    _ModelPath = ModelFolder;
        //    _SentencePath = SentencePath;

        //    _tdDictionary1 = null;
        //    _tdDictionary2 = null;
        //    _DictionaryName1 = string.Empty;
        //    _DictionaryName2 = string.Empty;



        //    return Analyze();
        //}
        public List<RegMatches> FindDefinedAcronymsRegX(List<MainSentences> _Sentences)
        {
            List<RegMatches> listRegMatches = new List<RegMatches>();
            Parallel.ForEach(_Sentences, (sentenceObj) =>
            {
                string Sentence_Cleaned = DataFunctions.RegExFixInvalidCharacters(sentenceObj.sentence);
                Sentence_Cleaned = Sentence_Cleaned.Replace("\n", "").Replace("\r", ""); // 10.09.2020
                // Remove periods
                Sentence_Cleaned = Sentence_Cleaned.Replace(".", "");
                if (Sentence_Cleaned.Trim().Length > 0)
                {
                    MatchCollection matches;
                    const string ACRONYM_RegExp1 = @"\b((?<Acronym>\w)\w*\W+)+(?<=(?<-Acronym>.(?=.*?(?<Reverse>\k<Acronym>)))+)(?(Acronym)(?!))\((?<-Reverse>\k<Reverse>)+\)(?(Reverse)(?!))";
                    Regex regex = new Regex(@ACRONYM_RegExp1, RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);
                    matches = regex.Matches(Sentence_Cleaned);

                    RegMatches RegMatches = new RegMatches();
                    RegMatches.matchesCount = matches.Count;
                    RegMatchFound regMatchFound = null;
                    List<RegMatchFound> regMatchFoundList = null;
                    regMatchFoundList = new List<RegMatchFound>();
                    string foundAcronym = string.Empty;
                    foreach (Match matchObj in matches)
                    {
                        foundAcronym = matchObj.Value.Trim();
                        if (!IsNonAcronym(foundAcronym))
                        {
                            regMatchFound = new RegMatchFound();
                            regMatchFound.matchFoundAcronym = matchObj.Value;
                            regMatchFound.matchIndex = matchObj.Index;
                            regMatchFound.matchLength = matchObj.Length;
                            regMatchFound.FoundFlag = true;
                            regMatchFoundList.Add(regMatchFound);
                        }
                    }
                    RegMatches.matchFound = regMatchFoundList;
                    RegMatches.sentence = sentenceObj.sentence;
                    RegMatches.sentenceNo = sentenceObj.sentenceNo
                    ;
                    listRegMatches.Add(RegMatches);
                }
            });
            return listRegMatches;
        }
        public bool Analyze(string txtFile)
        {
            _ErrorMsg = string.Empty;

            _txtFile = txtFile;

            if (!ValidateParameters())
                return false;

            _sbLog.Clear();

            Stopwatch timer_Total = new Stopwatch();
            timer_Total.Start();

            // Dictionaries ...
            if (_DictionariesPath.Length > 0)
            {
                DefinitionsLibs defLib = new DefinitionsLibs(_DictionariesPath);
                if (_DictionaryName1.Length > 0)
                {
                    DataSet dsDefLib1 = defLib.GetDataset(DictionaryName1);
                    _tdDictionary1 = dsDefLib1.Tables[0];
                }
                if (_DictionaryName2.Length > 0)
                {
                    DataSet dsDefLib12 = defLib.GetDataset(DictionaryName2);
                    _tdDictionary2 = dsDefLib12.Tables[0];
                }
            }

            // Create table to hold the analysis results
            _dtAcronymsFound = CreateTableFoundAcronyms();

            _sbLog.AppendLine("AcroSeeker Analysis Log");
            _sbLog.AppendLine("");
            // Split the selected Document Text into Sentences -- Process (1) see Arch. Diagram
            _sbLog.AppendLine("Split the selected Document Text into Sentences");
            Stopwatch timer_Process = new Stopwatch();
            timer_Process.Start();
            int i = ParseDoc2Sentences();
            timer_Process.Stop();
            _sbLog.AppendLine(string.Concat("Seconds: ", timer_Process.Elapsed.TotalSeconds.ToString()));
            _sbLog.AppendLine(string.Concat("Qty Sentences: ", i.ToString()));
            if (i == 0) // An error occured or no Sentences were found
            {
                _ErrorMsg = "No Sentences were found.";
                return false;
            }
            else if (i == 1) // An error may have occured or no Sentences were found
            {
                if (_Sentences[0].Trim() == string.Empty)
                {
                    _ErrorMsg = "Error: No Sentences were found.";
                    return false;
                }
            }

            int sentenceNo = 1;
            int qtyFound_3 = 0;
            int qtyFound_4 = 0;
            int qtyFound_3and4 = 0;
            int qtyFound_5 = 0;
            int qtyFound_6_0 = 0;
            int qtyFound_6_1 = 0;
            int qtyFound_7 = 0;
            int qtyFound_8 = 0;
            int qtyFound_10 = 0;


            _sbLog.AppendLine("");
            _sbLog.AppendLine("Scan Sentences for Acronyms");
            timer_Process.Restart();
            bool found = false;

            //Create sentences repository object & populate it with Sentence ,   
            List<MainSentences> mainSentencesList = new List<MainSentences>();
            int sentenceCounter = 1;
            foreach (var sentenceObj in _Sentences)
            {
                MainSentences mainSentenceObj = new MainSentences();
                mainSentenceObj.sentence = sentenceObj;
                mainSentenceObj.sentenceNo = sentenceCounter;
                mainSentencesList.Add(mainSentenceObj);
                sentenceCounter++;
            }

            /*
             * In this version "found" variable is use less
             */

            //Process RegX in a thread based / multitasking 
            var regXResults = FindDefinedAcronymsRegX(mainSentencesList);

            foreach (var regXResultObj in regXResults) // Process (2) -- Each Sentence
            {
                if (regXResultObj != null && regXResultObj.matchFound.Count > 0)
                {
                    // Find Acronyms with Def.s - Process (3) see Arch. Diagram
                    qtyFound_3 = FindDefinedAcronyms(regXResultObj, regXResultObj.sentenceNo) + qtyFound_3;
                }
            }

            foreach (var regXResultObj in regXResults) // Process (2) -- Each Sentence
            {

                if (regXResultObj != null)
                {
                    if (regXResultObj.matchFound.Count == 0)
                    {
                        qtyFound_4 = FindIrregularAcronyms(regXResultObj.sentence, regXResultObj.sentenceNo, out found) + qtyFound_4;
                    }
                }
            }

            qtyFound_3and4 = CompileAcronymsDocDefined();

            timer_Process.Stop();
            _sbLog.AppendLine(string.Concat("Seconds: ", timer_Process.Elapsed.TotalSeconds.ToString()));
            //   sbLog.AppendLine(string.Concat("Qty Acronyms: ", qtyFound_3and4.ToString())); // Was giving the wrong qty -- ToDo Fix
            _sbLog.AppendLine("");


            // After scanning the document for Acronyms and their Definitions, then Find ...

            _sbLog.AppendLine("Identify Acronyms to Ignore");
            timer_Process.Restart();
            qtyFound_5 = RemoveIgnoreUndefinedAcronyms();
            _sbLog.AppendLine(string.Concat("Seconds: ", timer_Process.Elapsed.TotalSeconds.ToString()));
            _sbLog.AppendLine(string.Concat("Qty Acronyms: ", qtyFound_5.ToString()));
            _sbLog.AppendLine("");

            _sbLog.AppendLine("Find Definitions in Selected Dictionaries");
            timer_Process.Restart();
            qtyFound_6_0 = FindLibAcronyms(); // Find Definitions in Selected Dictionaries
            UpdateFoundAcronymsPerDics();
            timer_Process.Stop();
            _sbLog.AppendLine(string.Concat("Seconds: ", timer_Process.Elapsed.TotalSeconds.ToString()));
            _sbLog.AppendLine(string.Concat("Qty Dictionary Definitions: ", qtyFound_6_0.ToString()));
            _sbLog.AppendLine("");

            _sbLog.AppendLine("Find Acronyms with No Definitions");
            timer_Process.Restart();
            qtyFound_6_1 = FindAcronymsNotDefined(); // Find Acronyms with No Definitions
            timer_Process.Stop();
            _sbLog.AppendLine(string.Concat("Seconds: ", timer_Process.Elapsed.TotalSeconds.ToString()));
            _sbLog.AppendLine(string.Concat("Qty Not Defined: ", qtyFound_6_1.ToString()));
            _sbLog.AppendLine("");

            _sbLog.AppendLine("Find Acronyms with Multi-Definitions");
            timer_Process.Restart();
            qtyFound_7 = FindAcronymsMultiDefined(); // Find Acronyms with Multi-Definitions
            _sbLog.AppendLine(string.Concat("Seconds: ", timer_Process.Elapsed.TotalSeconds.ToString()));
            _sbLog.AppendLine(string.Concat("Qty Multi-Definitions: ", qtyFound_7.ToString()));
            _sbLog.AppendLine("");

            _sbLog.AppendLine("Find Acronyms with Diff-Definitions");
            timer_Process.Restart();
            qtyFound_8 = FindAcronymsDiffDefined(); // Find Acronyms with Diff-Definitions
            _sbLog.AppendLine(string.Concat("Seconds: ", timer_Process.Elapsed.TotalSeconds.ToString()));
            _sbLog.AppendLine(string.Concat("Qty Diff-Definitions: ", qtyFound_8.ToString()));
            _sbLog.AppendLine("");

            if (_UseDicDeepSearch)
            {
                _sbLog.AppendLine("Find Acronyms via selected Dictionaries - Deep Search"); // ToDo --> Set for only Professional Edition
                timer_Process.Restart();
                qtyFound_10 = FindLibAcronyms_DeepSearch();
                _sbLog.AppendLine(string.Concat("Seconds: ", timer_Process.Elapsed.TotalSeconds.ToString()));
                _sbLog.AppendLine(string.Concat("Qty Acronyms via Dictionaries: ", qtyFound_10.ToString()));
                _sbLog.AppendLine("");
            }

            AddTables2_dsAcronyms();

            timer_Total.Stop();

            _sbLog.AppendLine("Completed Analysis");
            _sbLog.AppendLine(string.Concat("Seconds: ", timer_Total.Elapsed.TotalSeconds.ToString()));

            _AnalysisLog = _sbLog.ToString();

            return true;  // not completed -- ToDo
        }

        /// <summary>
        /// check input Paremeters
        /// </summary>
        /// <returns></returns>
        private bool ValidateParameters()
        {
            // check parameters
            if (_txtFile.Length == 0)
            {
                _ErrorMsg = "Document to analyze is not defined.";
                return false;
            }
            if (_ModelPath.Length == 0)
            {
                _ErrorMsg = "Model folder is not defined.";
                return false;
            }
            if (_SentencePath.Length == 0)
            {
                _ErrorMsg = "Data Results folder is not defined.";
                return false;
            }

            if (!File.Exists(_txtFile))
            {
                _ErrorMsg = string.Concat("Unable to locate (converted) Source File: ", _txtFile);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Create a new Batch Run data row
        /// </summary>
        /// <param name="BatchRunName"></param>
        /// <param name="ResultPath"></param>
        /// <param name="CatalogPathFile"></param>
        /// <param name="modResultPath"></param>
        /// <returns></returns>
        public bool BatchRunNew(string BatchRunName, string ResultPath, string CatalogPathFile, bool CreateSubFolder, out string modResultPath)
        {
            _ErrorMsg = string.Empty;
            modResultPath = string.Empty;

            GenericDataManger gDataMgr = new GenericDataManger();

            // Validate input
            if (CatalogPathFile == string.Empty)
            {
                _ErrorMsg = "Catalog path and file name has not been defined.";
                return false;
            }

            if (ResultPath == string.Empty)
            {
                _ErrorMsg = "Result Path has not been defined.";
                return false;
            }

            if (BatchRunName == string.Empty)
            {
                _ErrorMsg = "Batch Run Name has not been defined.";
                return false;
            }

            string adjResultPath = string.Empty;
            if (!Directory.Exists(ResultPath))
            {
                try
                {
                    Directory.CreateDirectory(ResultPath);
                }
                catch (Exception exNewDir)
                {
                    _ErrorMsg = string.Concat("Unable to Find or Create Results Folder: ", ResultPath, "  -  Error: ", exNewDir.Message);
                    return false;
                }

                if (CreateSubFolder)
                {
                    ResultPath = Path.Combine(ResultPath, BatchRunName);
                }
                if (!Directory.Exists(ResultPath))
                {
                    try
                    {
                        Directory.CreateDirectory(ResultPath);
                    }
                    catch (Exception exNewDir)
                    {
                        _ErrorMsg = string.Concat("Unable to Find or Create Results Folder: ", ResultPath, "  -  Error: ", exNewDir.Message);
                        return false;
                    }
                }

                adjResultPath = ResultPath;
            }
            else
            {
                if (CreateSubFolder)
                {
                    ResultPath = Path.Combine(ResultPath, BatchRunName);
                }
                if (!Directory.Exists(ResultPath))
                {
                    try
                    {
                        Directory.CreateDirectory(ResultPath);
                    }
                    catch (Exception exNewDir)
                    {
                        _ErrorMsg = string.Concat("Unable to Find or Create Results Folder: ", ResultPath, "  -  Error: ", exNewDir.Message);
                        return false;
                    }
                }
                else
                {
                    string[] files = Directory.GetFiles(ResultPath);
                    if (files.Length > 0)
                    {
                        _ErrorMsg = string.Concat("Please select another folder, this folder already contains files. -- Folder: ", ResultPath);
                        return false;

                    }
                }

            }

            if (adjResultPath == string.Empty)
            {
                adjResultPath = ResultPath;
            }

            DataSet ds;

            if (!File.Exists(CatalogPathFile)) // 1st time using Mult-Docs
            {
                DataTable dt = CreateTableRunsCatalog();

                ds = new DataSet();
                ds.Tables.Add(dt);

            }
            else
            {


                ds = gDataMgr.LoadDatasetFromXml(CatalogPathFile);
                var rows = ds.Tables[0].Rows;

                string xBatchRunName = string.Empty;
                string xResultPath = string.Empty;

                foreach (DataRow row in rows)
                {
                    xBatchRunName = row[CatalogFieldConst.BatchRunName].ToString();
                    if (xBatchRunName.ToUpper() == BatchRunName.ToUpper())
                    {
                        _ErrorMsg = string.Concat("Batch Run Name '", BatchRunName, "' already exists. Please enter another name.");
                        return false;
                    }

                    xResultPath = row[CatalogFieldConst.ResultPath].ToString();
                    if (xResultPath.ToUpper() == adjResultPath.ToUpper())
                    {
                        _ErrorMsg = string.Concat("Result Folder '", ResultPath, "' has already been used. Please select another folder.");
                        return false;
                    }
                }

            }

            int uid = ds.Tables[0].Rows.Count + 1;

            DataRow newRow = ds.Tables[0].NewRow();

            newRow[CatalogFieldConst.UID] = uid;
            newRow[CatalogFieldConst.BatchRunName] = BatchRunName;
            newRow[CatalogFieldConst.ResultPath] = adjResultPath;
            newRow[CatalogFieldConst.RanDateTime] = DateTime.Now.ToString("F");

            ds.Tables[0].Rows.Add(newRow);

            gDataMgr.SaveDataXML(ds, CatalogPathFile);

            modResultPath = adjResultPath;

            return true;

        }

        private DataTable CreateTableRunsCatalog()
        {
            DataTable table = new DataTable("RunsCatalog");

            table.Columns.Add(CatalogFieldConst.UID, typeof(int));
            table.Columns.Add(CatalogFieldConst.BatchRunName, typeof(string));
            table.Columns.Add(CatalogFieldConst.ResultPath, typeof(string));
            table.Columns.Add(CatalogFieldConst.RanDateTime, typeof(string));

            return table;
        }

        /// <summary>
        /// Create a DataTable to hold all Acronyms Found
        /// </summary>
        /// <returns></returns>
        private DataTable CreateTableFoundAcronyms()
        {
            DataTable table = new DataTable("FoundAcronyms");

            table.Columns.Add(AcronymsFoundFieldConst.UID, typeof(int));
            table.Columns.Add(AcronymsFoundFieldConst.Acronym, typeof(string));
            table.Columns.Add(AcronymsFoundFieldConst.Definition, typeof(string));
            table.Columns.Add(AcronymsFoundFieldConst.DefinitionSource, typeof(string));
            table.Columns.Add(AcronymsFoundFieldConst.Dictionary, typeof(string)); // Name of Dictionary where the Definition came from. Otherwise, Empty String.
            table.Columns.Add(AcronymsFoundFieldConst.xSentence, typeof(string));
            table.Columns.Add(AcronymsFoundFieldConst.SentenceNo, typeof(int));
            table.Columns.Add(AcronymsFoundFieldConst.FoundByAlgorithm, typeof(string));
            table.Columns.Add(AcronymsFoundFieldConst.Index, typeof(int));
            table.Columns.Add(AcronymsFoundFieldConst.Length, typeof(int));

            return table;
        }

        private void CreateTableAcronymsDocDefined()
        {
            _dtAcronymsDocDefined = new DataTable("DocDefined");
            _dtAcronymsDocDefined.Columns.Add(AcronymsFoundFieldConst.UID, typeof(int));
            _dtAcronymsDocDefined.Columns.Add(AcronymsFoundFieldConst.Acronym, typeof(string));
            _dtAcronymsDocDefined.Columns.Add(AcronymsFoundFieldConst.Definition, typeof(string));
            _dtAcronymsDocDefined.Columns.Add(AcronymsFoundFieldConst.SentenceNo, typeof(int));

            _dtAcronymsDocDefined.Columns.Add(AcronymsFoundFieldConst.xSentence, typeof(string)); // Added 08.06.2017
        }

        private void CreateTableAcronymsDicDefined()
        {
            _dtAcronymsDicDefined = new DataTable("DicDefined");
            _dtAcronymsDicDefined.Columns.Add(AcronymsFoundFieldConst.UID, typeof(int));
            _dtAcronymsDicDefined.Columns.Add(AcronymsFoundFieldConst.Acronym, typeof(string));
            _dtAcronymsDicDefined.Columns.Add(AcronymsFoundFieldConst.Definition, typeof(string));
            _dtAcronymsDicDefined.Columns.Add(AcronymsFoundFieldConst.Dictionary, typeof(string)); // Name of Dictionary where the Definition came from. Otherwise, Empty String.
            _dtAcronymsDicDefined.Columns.Add(AcronymsFoundFieldConst.SentenceNos, typeof(string)); // Example: "1|3|20|30"           
            _dtAcronymsDicDefined.Columns.Add(AcronymsFoundFieldConst.xSentence, typeof(string));
        }


        private void CreateTableAcronymsNotDefinedFound()
        {
            _dtAcronymsNotDefined = new DataTable("NotDefined");
            _dtAcronymsNotDefined.Columns.Add(AcronymsFoundFieldConst.UID, typeof(int));
            _dtAcronymsNotDefined.Columns.Add(AcronymsFoundFieldConst.Acronym, typeof(string));
            _dtAcronymsNotDefined.Columns.Add(AcronymsFoundFieldConst.SentenceNos, typeof(string)); // Example: "1|3|20|30"
            _dtAcronymsNotDefined.Columns.Add(AcronymsFoundFieldConst.xSentence, typeof(string));
        }

        private void CreateTableAcronymsMultDefined()
        {
            _dtAcronymsMultDefined = new DataTable("MultDefined");
            _dtAcronymsMultDefined.Columns.Add(AcronymsFoundFieldConst.UID, typeof(int));
            _dtAcronymsMultDefined.Columns.Add(AcronymsFoundFieldConst.Acronym, typeof(string));
            _dtAcronymsMultDefined.Columns.Add(AcronymsFoundFieldConst.Definition, typeof(string));
            _dtAcronymsMultDefined.Columns.Add(AcronymsFoundFieldConst.SentenceNo, typeof(string));
            _dtAcronymsMultDefined.Columns.Add(AcronymsFoundFieldConst.xSentence, typeof(string));
        }

        private void CreateTableAcronymsDiffDefined()
        {
            _dtAcronymsDiffDefined = new DataTable("DiffDefined");
            _dtAcronymsDiffDefined.Columns.Add(AcronymsFoundFieldConst.UID, typeof(int));
            _dtAcronymsDiffDefined.Columns.Add(AcronymsFoundFieldConst.Acronym, typeof(string));
            _dtAcronymsDiffDefined.Columns.Add(AcronymsFoundFieldConst.Definition, typeof(string));
            _dtAcronymsDiffDefined.Columns.Add(AcronymsFoundFieldConst.SentenceNo, typeof(string));
            _dtAcronymsDiffDefined.Columns.Add(AcronymsFoundFieldConst.xSentence, typeof(string));
        }

        private DataTable CreateTableAcronyms4Word()
        {
            DataTable table = new DataTable("DefinedAcronyms");

            table.Columns.Add(AcronymsFoundFieldConst.Acronym, typeof(string));
            table.Columns.Add(AcronymsFoundFieldConst.Definition, typeof(string));

            return table;
        }

        private void AddTables2_dsAcronyms()
        {
            if (_dsAcronyms == null)
            {
                _dsAcronyms = new DataSet();
            }

            if (_dtAcronymsFound != null)
            {
                if (!_dsAcronyms.Tables.Contains(_dtAcronymsFound.TableName))
                    _dsAcronyms.Tables.Add(_dtAcronymsFound);
            }

            if (_dtAcronymsDocDefined != null)
                if (!_dsAcronyms.Tables.Contains(_dtAcronymsDocDefined.TableName))
                    _dsAcronyms.Tables.Add(_dtAcronymsDocDefined);

            if (_dtAcronymsDicDefined != null)
                if (!_dsAcronyms.Tables.Contains(_dtAcronymsDicDefined.TableName))
                    _dsAcronyms.Tables.Add(_dtAcronymsDicDefined);

            if (_dtAcronymsMultDefined != null)
                if (!_dsAcronyms.Tables.Contains(_dtAcronymsMultDefined.TableName))
                    _dsAcronyms.Tables.Add(_dtAcronymsMultDefined);

            if (_dtAcronymsDiffDefined != null)
                if (!_dsAcronyms.Tables.Contains(_dtAcronymsDiffDefined.TableName))
                    _dsAcronyms.Tables.Add(_dtAcronymsDiffDefined);

            if (_dtAcronymsNotDefined != null)
                if (!_dsAcronyms.Tables.Contains(_dtAcronymsNotDefined.TableName))
                    _dsAcronyms.Tables.Add(_dtAcronymsNotDefined);
        }


        /// <summary>
        /// Analysis Workflow Process (1) -- Parse doc. into sentences 
        /// </summary>
        /// <returns></returns>
        public int ParseDoc2Sentences()
        {
            _ErrorMsg = string.Empty;

            //  _isTestMode = true;

            //if (_ModelPath == null || _ModelPath == string.Empty)
            //{
            _Sentences = Files.ReadFile2Array(_txtFile); // Split by lines
                                                         //}
                                                         //else
                                                         //{
                                                         //string fileTxt = Files.ReadFile(_txtFile);
                                                         //if (fileTxt.Length == 0)
                                                         //{
                                                         //    _ErrorMsg = Files.ErrorMessage;
                                                         //    return 0;
                                                         //}
                                                         //_Sentences = SplitSentences(fileTxt);
                                                         //if (_Sentences == null)
                                                         //{
                                                         //    return 0;
                                                         //}

            SentencesCheckandFix(); // Fix zero lenght sentences -- Added 09.11.2020 -- Added due to a document from SURESH B - MACE <SURESH.B@mahindra.com>


            //}

            RemoveUpperCaseCaptions(0); // Removes Uppercase Captions.

            if (_isTestMode)
                SaveSentences();

            if (_Sentences == null)
                return 0;

            return _Sentences.Length;
        }

        private string[] SplitSentences(string txt)
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

            return mSentenceDetector.SentenceDetect(txt);
        }

        private bool SentencesCheckandFix() // Added 09.14.2020
        {
            int count = _Sentences.Length;
            if (count == 0)
                return false;


            for (int i = 0; i < count; i++)
            {
                if (_Sentences[i].IndexOf("\n") > -1)
                {
                    _Sentences = Files.ReadFile2Array(_txtFile); // Split by lines

                    return true;
                }
            }


            return true;
        }

        public void RemoveUpperCaseCaptions(int f)
        {
            if (_Sentences.Length == 0)
                return;

            if (_Sentences.Length == (f + 1))
                return;

            int i = 0;

            bool runAgain = false;

            List<string> lstSentences = new List<string>();

            lstSentences = _Sentences.ToList();

            bool foundCaption = false;
            string new_s = string.Empty;
            foreach (string s in lstSentences)
            {
                if (f <= i)
                {
                    string[] lines = s.Split('\n');
                    foreach (string ls in lines)
                    {
                        if (ls.Trim() != string.Empty)
                        {
                            if (IsAllUpper(ls))
                            {
                                foundCaption = true;
                                new_s = s.Replace(ls, string.Empty);
                            }
                        }
                    }
                    if (foundCaption)
                    {
                        lstSentences.RemoveAt(i);
                        lstSentences.Insert(i, new_s);
                        _Sentences = null;
                        _Sentences = lstSentences.ToArray();
                        runAgain = true;
                        break;
                    }
                }

                i++;
            }

            if (runAgain)
            {
                //lstSentences.Clear();
                //RemoveUpperCaseCaptions(i);
            }



        }

        private int SaveSentences()
        {
            Files.DeleteAllFileInDir(_SentencePath); // Remove Sentence files from the previous run.

            string pathFile;
            int i = 0;
            foreach (string txt in _Sentences)
            {
                pathFile = string.Concat(_SentencePath, i.ToString(), ".rtf");
                _rtfCrtl.Text = txt;
                _rtfCrtl.SaveFile(pathFile, RichTextBoxStreamType.RichText);

                i++;
            }

            return i;
        }


        /// <summary>
        /// Find Acronyms with Def.s - Process (3) See arch. diagram
        /// </summary>
        /// <param name="Sentence">Sentence text</param>
        /// <param name="SentenceNo">Sentence Number. 1 = 1st. Sentence</param>
        /// <returns>Returns Qty of Acronyms found in the Sentence</returns>
        private int FindDefinedAcronyms(RegMatches Sentence_Cleaned, int SentenceNo)
        {

            int results = 0;

            //found = false;

            //if (Sentence_Cleaned.Trim().Length == 0)
            //{
            //    return results;
            //}

            //string Sentence_Cleaned = DataFunctions.RegExFixInvalidCharacters(Sentence); // Added 05.18.2020

            //// Remove periods
            //Sentence_Cleaned = Sentence_Cleaned.Replace(".", "");

            MatchCollection matches;

            int loopFound = 0;
            string foundAcronym = string.Empty;
            int rightPar = 0;
            int leftPar = 0;
            string acronym = string.Empty;
            int acronymLength = 0;
            string acronymDef = string.Empty;
            int acronymDefLength = 0;

            bool acronymPlural = false;

            int index = -1;
            int length = -1;

            //const string ACRONYM_RegExp1 = @"\b((?<Acronym>\w)\w*\W+)+(?<=(?<-Acronym>.(?=.*?(?<Reverse>\k<Acronym>)))+)(?(Acronym)(?!))\((?<-Reverse>\k<Reverse>)+\)(?(Reverse)(?!))";

            //Regex regex = new Regex(@ACRONYM_RegExp1, RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase| RegexOptions.Compiled);

            //matches = regex.Matches(Sentence_Cleaned);

            loopFound = Sentence_Cleaned.matchFound.Count;
            if (loopFound > 0)
            {

                foreach (RegMatchFound match in Sentence_Cleaned.matchFound)
                {
                    foundAcronym = match.matchFoundAcronym.Trim();

                    if (!IsNonAcronym(foundAcronym))
                    {

                        //Test code
                        if (acronym == "A1KE")
                        {
                            bool foundX = true;
                        }
                        //

                        acronymPlural = DataFunctions.isAcronymPlural(foundAcronym); // checks if the Acronym is Plural, e.g. CPRs

                        if (acronymPlural) // if Acronym is Plural
                        {
                            foundAcronym = foundAcronym.Substring(0, foundAcronym.Length - 1); // remove the last char
                        }

                        index = match.matchIndex;
                        length = match.matchLength;

                        leftPar = foundAcronym.IndexOf('(');
                        rightPar = foundAcronym.IndexOf(')');

                        acronymDefLength = (rightPar - 1) - (leftPar);
                        acronymLength = (leftPar - 1);

                        if (acronymDefLength > 1) // Fixed zero length bug in old AcroParser -- 10/20/2015
                        {
                            acronym = foundAcronym.Substring(leftPar + 1, acronymDefLength);
                            acronymDef = foundAcronym.Substring(0, acronymLength);

                            if (acronymPlural)
                                foundAcronym = string.Concat(foundAcronym, "s");

                            CreateNewRow(acronym, acronymDef, AcronymsFoundFieldConst.DefinitionSource_Document, Sentence_Cleaned.sentence, SentenceNo, "3.1", index, length, string.Empty);
                            //found = true;
                            match.FoundFlag = true;
                        }
                    }

                }
            }

            return loopFound;
        }

        public string FindAcronymsListingInTable(string foundAcronym, string Sentence, int SentenceNo)
        {
            string acronym = string.Empty;
            string acronymDef = string.Empty;
            string adjSentence = string.Empty;


            // Test Code
            if (foundAcronym == "DART")
            {
                bool foundit = true;
            }
            //

            adjSentence = Sentence.Trim();

            if (foundAcronym.Length == 0)
                return string.Empty;


            string[] words = adjSentence.Split(' ');
            int wordsLength = words.Length;
            if (wordsLength > 1)
            {
                return string.Empty;
            }

            if (words[0] != foundAcronym)
                return string.Empty;

            if (SentenceNo > _Sentences.Length) // Check for out of raange
            {
                return string.Empty;
            }

            // When MS word tables are converted to plain text, each cell in on another line in the text file
            // Therefore, a table with Acronym in column 1 and its Definition is in column 2, then the Definition is on the line below the Acronym 
            string nextSentence = _Sentences[SentenceNo]; // SentenceNo is 1-Based, while the Array is zero base
            words = nextSentence.Split(' ');

            char[] charsAcronym = foundAcronym.ToCharArray();
            char[] charWord = words[0].ToCharArray();

            if (charWord[0] == charsAcronym[0])
            {
                acronymDef = nextSentence;
                //for (int i = 1; i < wordsLength; i++)
                //{
                //    acronymDef = string.Concat(acronymDef, " ", words[i]);
                //}
            }


            if (acronymDef.Length > 1)
            {
                return acronymDef;
            }



            return string.Empty;
        }



        /// <summary>
        /// Added Found Acronym into Table
        /// </summary>
        /// <param name="acronym"></param>
        /// <param name="acronymDef"></param>
        /// <param name="DefinitionSource"></param>
        /// <param name="Sentence"></param>
        /// <param name="SentenceNo"></param>
        /// <param name="FoundByAlgorithm"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        private void CreateNewRow(string acronym, string acronymDef, string DefinitionSource, string Sentence, int SentenceNo, string FoundByAlgorithm, int index, int length, string Dictionary)
        {
            _UIDCounter++;

            // Test
            bool found90 = false;
            if (acronym == "90")
            {
                found90 = true;
            }

            //Test code
            if (acronym == "CPs")
            {
                found90 = true;
            }
            //

            DataRow row = _dtAcronymsFound.NewRow();
            row[AcronymsFoundFieldConst.UID] = _UIDCounter;
            row[AcronymsFoundFieldConst.Acronym] = acronym;
            row[AcronymsFoundFieldConst.Definition] = acronymDef;

            if (acronymDef.Length > 0)
                row[AcronymsFoundFieldConst.DefinitionSource] = DefinitionSource;
            else
                row[AcronymsFoundFieldConst.DefinitionSource] = string.Empty;

            row[AcronymsFoundFieldConst.Dictionary] = Dictionary;
            row[AcronymsFoundFieldConst.xSentence] = Sentence;
            row[AcronymsFoundFieldConst.SentenceNo] = SentenceNo;
            row[AcronymsFoundFieldConst.FoundByAlgorithm] = FoundByAlgorithm; // 3.1 = Process (3), 1st RegExp
            row[AcronymsFoundFieldConst.Index] = index;
            row[AcronymsFoundFieldConst.Length] = length;

            _dtAcronymsFound.Rows.Add(row);

            _dtAcronymsFound.AcceptChanges();
        }

        private void CreateNewDocRow(string acronym, string acronymDef, int SentenceNo, string sentence)
        {
            _UIDDocDefined++;

            DataRow row = _dtAcronymsDocDefined.NewRow();
            row[AcronymsFoundFieldConst.UID] = _UIDDicDefined;
            row[AcronymsFoundFieldConst.Acronym] = acronym;
            row[AcronymsFoundFieldConst.Definition] = acronymDef;
            row[AcronymsFoundFieldConst.SentenceNo] = SentenceNo;
            row[AcronymsFoundFieldConst.xSentence] = sentence;

            _dtAcronymsDocDefined.Rows.Add(row);
        }



        /// <summary>
        /// Added Found Acronym Dic Def into Table
        /// </summary>
        private void CreateNewDicRow(string acronym, string acronymDef, string SentenceNos, string DictionaryName)
        {
            _UIDDicDefined++;

            DataRow row = _dtAcronymsDicDefined.NewRow();
            row[AcronymsFoundFieldConst.UID] = _UIDDicDefined;
            row[AcronymsFoundFieldConst.Acronym] = acronym;
            row[AcronymsFoundFieldConst.Definition] = acronymDef;
            row[AcronymsFoundFieldConst.Dictionary] = DictionaryName;
            row[AcronymsFoundFieldConst.SentenceNos] = SentenceNos;

            _dtAcronymsDicDefined.Rows.Add(row);

            _dtAcronymsDicDefined.AcceptChanges();

        }

        /// <summary>
        /// Added Found Acronym with Def into Table
        /// </summary>
        private void CreateNotDefindedRow(string acronym, string SentenceNos, string sentence)
        {
            _UIDDNotDefined++;

            DataRow row = _dtAcronymsNotDefined.NewRow();
            row[AcronymsFoundFieldConst.UID] = _UIDDNotDefined;
            row[AcronymsFoundFieldConst.Acronym] = acronym;
            row[AcronymsFoundFieldConst.SentenceNos] = SentenceNos;
            row[AcronymsFoundFieldConst.xSentence] = sentence;

            _dtAcronymsNotDefined.Rows.Add(row);

            _dtAcronymsNotDefined.AcceptChanges();

        }

        private void CreateMultitDefindedRow(string acronym, string definition, string SentenceNo, string Sentence)
        {
            _UIDMultiDefined++;

            DataRow row = _dtAcronymsMultDefined.NewRow();
            row[AcronymsFoundFieldConst.UID] = _UIDMultiDefined;
            row[AcronymsFoundFieldConst.Acronym] = acronym;
            row[AcronymsFoundFieldConst.Definition] = definition;
            row[AcronymsFoundFieldConst.SentenceNo] = SentenceNo;
            row[AcronymsFoundFieldConst.xSentence] = Sentence;

            _dtAcronymsMultDefined.Rows.Add(row);

        }


        private void CreateDiffDefindedRow(string acronym, string definition, string Sentence)
        {
            _UIDDiffDefined++;

            DataRow row = _dtAcronymsDiffDefined.NewRow();
            row[AcronymsFoundFieldConst.UID] = _UIDMultiDefined;
            row[AcronymsFoundFieldConst.Acronym] = acronym;
            row[AcronymsFoundFieldConst.Definition] = definition;
            //  row[AcronymsFoundFieldConst.SentenceNo] = SentenceNo;
            row[AcronymsFoundFieldConst.xSentence] = Sentence;

            _dtAcronymsDiffDefined.Rows.Add(row);

        }



        /// <summary>
        /// Find Acronyms without Def.s - Process (4) See arch. diagram
        /// </summary>
        /// <param name="Sentence">Sentence text</param>
        /// <param name="SentenceNo">Sentence Number. 1 = 1st. Sentence</param>
        /// <returns>Returns Qty of Acronyms found in the Sentence</returns>
        public int FindIrregularAcronyms(string Sentence, int SentenceNo, out bool found)
        {

            int results = 0;

            found = false;

            if (Sentence.Trim().Length == 0)
            {
                return results;
            }

            int loopFound = 0;

            try
            {

                // -- Rule: If all words on a line are uppercase, ignore
                string noneWhitespaceSentence = Sentence.Trim().Replace(" ", "");

                if (IsAllUpper(noneWhitespaceSentence))
                {
                    return results;

                }

                // -- Rule: If all words are uppercase and enclosed by quotes, ignore
                var extractTextWithInDoubleQuotes = from Match match in Regex.Matches(Sentence, "\"([^\"]*)\"")
                                                    select match.ToString();

                if (extractTextWithInDoubleQuotes.FirstOrDefault() != null)
                {
                    string noneWhitespaceAndExtractedTextWithInDoubleQuotes = extractTextWithInDoubleQuotes.FirstOrDefault().Trim().Replace(" ", "");
                    if (IsAllUpper(extractTextWithInDoubleQuotes.FirstOrDefault()))
                    {
                        return results;
                    }
                }

                // -- Rule: If all words are uppercase and contain a number, with or without a hyphen, ignore
                // -- Rule: Igrone if a line start with number example -- Header  1.1  MANAGEMENT.

                string noneWhitespaceSentenceCheckForDigit = Sentence.Trim().Replace(" ", "");

                if (IsAllUpper(noneWhitespaceSentenceCheckForDigit))
                {
                    //return results;
                    string k = "";

                    for (int i = 0; i < noneWhitespaceSentenceCheckForDigit.Length; i++)
                    {
                        if (char.IsDigit(Sentence[i]))
                        {
                            return results;
                        }
                    }
                }


                // --Rule: If the word Section is uppercase and the next value is an uppercase character (e.g. J) or roman numeral ( e.g. II) or number (e.g. 2 or TWO), ignore  

                string[] listOfWords = Sentence.Split(' ');

                if (listOfWords.Length > 0)
                {
                    for (int i = 0; i < listOfWords.Length; i++)
                    {
                        string[] restrictionList = new string[] { "I", "II", "III", "IV", "IV", "V", "VI", "VII", "VIII", "IX", "X", "J", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN" };
                        foreach (string restriction in restrictionList)
                        {
                            if (listOfWords[i].Trim() == restriction)
                            {
                                if (i > 0)
                                {
                                    if (IsAllUpper(listOfWords[i - 1]))
                                    {
                                        return results;
                                    }
                                }
                            }
                        }
                    }
                }

                // Implement
                // a.Example: FIGURES LIST OF FIGURES Figure 3.1: MAISR Sustainment Enterprise 9
                // b.Logical Solution:  Check if the possible acronym/ word is in a cluster of other upper -case words of two or more.
                // c.FIGURES is Not an acronym
                // d.Note: MAISR is an acronym




                //  string Sentence = CleanSentence(inputSentence);

                if (Sentence.Length < 1) // Added 09.11.2020
                    return results;

                Sentence = DataFunctions.RegExFixInvalidCharacters(Sentence); // Added 05.18.2020

                if (Sentence.Length < 1) // Added 09.11.2020
                    return results;

                string[] words = Sentence.Split(' ');
                if (words.Length == 0)
                {
                    return results;
                }

                MatchCollection matches;

                string foundAcronym = string.Empty;

                string acronym = string.Empty;
                string acronymDef = string.Empty;

                bool acronymPlural = false;

                int index = -1;
                int length = -1;

                const string ACRONYM_RegExp2 = @"\b[A-Z0-9&'s]{2,}\b"; // @"\b[A-Z]{2,9}\b"; // Remove max lenght of 9 -- Now can find acronyms with '&', Numbers and "'s" as well

                Regex regex = new Regex(@ACRONYM_RegExp2);

                matches = regex.Matches(Sentence);


                string adjFoundAcronym = string.Empty;
                string foundByAlgorithm = string.Empty;

                bool sameAcronym = false;
                loopFound = matches.Count;
                if (loopFound > 0)
                {

                    foreach (Match match in matches) // Loop through the found Acronyms
                    {
                        foundAcronym = match.Value.Trim();


                        //  acronymDef = string.Empty; // Reset

                        if (!IsNonAcronym(foundAcronym)) // If so-called foundAcronym is Not a real acronym
                        {
                            index = match.Index;
                            length = match.Length;

                            // Test code
                            if (foundAcronym == "DART")
                            {
                                Debug.WriteLine("Found: DART - 5.1");
                            }

                            acronymDef = FindAcronymsListingInTable(foundAcronym, Sentence, SentenceNo);
                            if (acronymDef.Length > 0) // If Acronym was found in a list
                            {
                                foundByAlgorithm = "5.1";
                                CreateNewRow(foundAcronym, acronymDef, AcronymsFoundFieldConst.DefinitionSource_Document, Sentence.Trim(), SentenceNo, foundByAlgorithm, index, length, string.Empty);
                            }

                            if (acronymDef.Length == 0)
                            {
                                adjFoundAcronym = string.Concat("(", foundAcronym, ")");
                                if (Sentence.IndexOf(adjFoundAcronym) > -1 || acronymDef.Length == 0) // If Acronym is within parentheses e.g. (Acronym), check if this Acronym has been found by the previous Process (3), if n then seach for Acronym's Definition - Process (6) see arch. diagram 
                                {


                                    sameAcronym = isSameAcronym(foundAcronym, SentenceNo);

                                    acronymPlural = DataFunctions.isAcronymPlural(foundAcronym); // checks if the Acronym is Plural, e.g. CPRs

                                    if (acronymPlural) // if Acronym is Plural
                                    {
                                        foundAcronym = foundAcronym.Substring(0, foundAcronym.Length - 1); // remove the last char
                                    }

                                    if (!sameAcronym)
                                    {
                                        // --- Find Def.s ---


                                        if (acronymDef.Length == 0)
                                        {
                                            acronymDef = FindDef_ApostropheS(Sentence, foundAcronym); // Look for Acronym Def with Apostrophe S (e.g. CBP's)
                                            if (acronymDef.Length > 0) // If not an Apostrophe Def.
                                            {
                                                foundByAlgorithm = "4.1";
                                            }
                                        }

                                        if (acronymDef.Length == 0)
                                        {
                                            acronymDef = FindDef_Hyphen(Sentence, foundAcronym);
                                            if (acronymDef.Length > 0) // If not an Hyphen Def.
                                            {
                                                foundByAlgorithm = "4.2";
                                            }
                                        }

                                        if (acronymDef.Length == 0)
                                        {
                                            acronymDef = FindDef_Concealed(Sentence, foundAcronym);
                                            if (acronymDef.Length > 0) // No Def. Found
                                            {
                                                foundByAlgorithm = "4.3";
                                            }
                                        }


                                        if (acronymDef.Length == 0 && acronymPlural)
                                        {
                                            acronymDef = FindDef_Plural(Sentence, foundAcronym);
                                            if (acronymDef.Length > 0)
                                                foundByAlgorithm = "4.4";
                                        }


                                        if (acronymDef.Length == 0 && acronymPlural)
                                        {
                                            acronymDef = FindDef_AcronymEndingWithSmallS(Sentence, foundAcronym);
                                            if (acronymDef.Length > 0) // No Def. Found
                                            {
                                                foundByAlgorithm = "4.10";
                                            }
                                        }

                                        if (acronymDef.Length == 0) // No Def. Found
                                        {
                                            acronymDef = FindDef_AndSymbol(Sentence, foundAcronym);
                                            if (acronymDef.Length > 0)
                                                foundByAlgorithm = "4.11";
                                        }


                                        if (acronymDef.Length == 0) // No Def. Found
                                        {
                                            acronymDef = FindDef_Concat(Sentence, foundAcronym);
                                            if (acronymDef.Length > 0)
                                                foundByAlgorithm = "4.5";
                                        }

                                        if (acronymDef.Length == 0) // No Def. Found
                                        {
                                            acronymDef = FindDef_ExtraWord(Sentence, foundAcronym);
                                            if (acronymDef.Length > 0)
                                                foundByAlgorithm = "4.6";

                                        }


                                        if (acronymDef.Length == 0) // No Def. Found
                                        {
                                            acronymDef = FindDef_ConcatAcronym(Sentence, foundAcronym);
                                            if (acronymDef.Length > 0)
                                                foundByAlgorithm = "4.7";
                                        }

                                        if (acronymDef.Length == 0) // No Def. Found
                                        {
                                            acronymDef = FindDef_IdentificationAcronym(Sentence, foundAcronym);
                                            if (acronymDef.Length > 0)
                                                foundByAlgorithm = "4.8";
                                        }

                                        if (acronymDef.Length == 0) // No Def. Found
                                        {
                                            acronymDef = FindDef_AvailabilityAcronym(Sentence, foundAcronym);
                                            if (acronymDef.Length > 0)
                                                foundByAlgorithm = "4.12";
                                        }


                                        if (acronymDef.Length == 0) // No Def. Found
                                        {
                                            acronymDef = FindDef_AcronymWithPrefixAcronym(Sentence, foundAcronym);
                                            if (acronymDef.Length > 0)
                                                foundByAlgorithm = "4.14";
                                        }

                                        if (acronymDef.Length == 0) // No Def. Found -- This Acronym Identification Check is to always be last
                                        {
                                            acronymDef = FindDef_NotPatternAcronym(Sentence, foundAcronym);
                                            if (acronymDef.Length > 0)
                                                foundByAlgorithm = "4.9";
                                        }

                                        if (acronymDef.Length == 0)
                                        {
                                            foundByAlgorithm = "4.0";
                                        }


                                        if (acronymPlural)
                                            foundAcronym = string.Concat(foundAcronym, "s");

                                        if (foundByAlgorithm == string.Empty)
                                            foundByAlgorithm = "4.0";

                                        //Here before insert foundAcronym check if this acronym got uppercase word before it or after it.

                                        if (CheckLeftAndRightWordsUpperCase(foundAcronym, Sentence.Trim()))
                                        {
                                            CreateNewRow(foundAcronym, acronymDef, AcronymsFoundFieldConst.DefinitionSource_Document, Sentence.Trim(), SentenceNo, foundByAlgorithm, index, length, string.Empty);

                                        }

                                        // No pattern for Def
                                        if (foundByAlgorithm.Length == 0)
                                        {
                                            foundByAlgorithm = "4.0.X";
                                            CreateNewRow(foundAcronym, string.Empty, AcronymsFoundFieldConst.DefinitionSource_Document, Sentence.Trim(), SentenceNo, foundByAlgorithm, index, length, string.Empty);
                                        }


                                        // ToDo as a Post process - Search in selected dictionaries ...

                                    }

                                }
                                else // No parentheses - Added 10.06.2020
                                {


                                    if (!IsAllUpper(Sentence)) // Added to prevent headers from being treated as acronyms
                                    {
                                        foundByAlgorithm = "4.0.1";
                                        CreateNewRow(foundAcronym, string.Empty, AcronymsFoundFieldConst.DefinitionSource_Document, Sentence.Trim(), SentenceNo, foundByAlgorithm, index, length, string.Empty);
                                    }
                                }


                            }
                        }
                    }
                }

                if (foundByAlgorithm.Length > 0)
                    found = true;
                else
                    found = false;

            }
            catch (Exception ex)
            {
                //_ErrorMsg = string.Concat(Environment.NewLine, "Error in FindIrregularAcronyms at Sentence No ", SentenceNo.ToString(), "  ", ex.Message);

                _sbLog.AppendLine(string.Concat("! Error found in FindIrregularAcronyms at Sentence No ", SentenceNo.ToString()));
                _sbLog.AppendLine(string.Concat("! Error Message: ", ex.Message));
                _sbLog.AppendLine(string.Concat("! Error in Sentence: ", Sentence));
                _sbLog.AppendLine("Notice: Please share the above Error information with Atebion LLC so they can address this issue.");

                found = false;

            }


            return loopFound;
        }
        public bool CheckLeftAndRightWordsUpperCase(string foundAcronym, string Sentence)
        {
            string[] words = Sentence.Split(' ');
            string wordToCheck = string.Empty;
            if (words.Count() >= 2)
            {
                bool checkLeftAndRightWordsUpperCase = true;
                bool isAllUpperWithNoneChars = true;
                for (int i = 0; i <= words.Count(); i++)
                {
                    if (foundAcronym == words[i])
                    {

                        //check left of the Acronym if it is  a uppercase word
                        //check righ of the Acronym if it is  a uppercase word
                        if (i == 0)
                        {
                            //check only the right side of the  Acronym
                            wordToCheck = words[i + 1];
                            isAllUpperWithNoneChars = IsAllUpperWithNoneChars(wordToCheck);
                            if (isAllUpperWithNoneChars)
                            {
                                checkLeftAndRightWordsUpperCase = false;
                                return checkLeftAndRightWordsUpperCase;
                            }
                            else
                            {
                                return true;
                            }
                            //call verify uppercase
                        }
                        else if (i == words.Count() - 1)
                        {
                            //check only the left side of the  Acronym
                            wordToCheck = words[i - 1];
                            isAllUpperWithNoneChars = IsAllUpperWithNoneChars(wordToCheck);
                            if (isAllUpperWithNoneChars)
                            {
                                checkLeftAndRightWordsUpperCase = false;
                                return checkLeftAndRightWordsUpperCase;
                            }
                            else
                            {
                                return true;
                            }
                        }
                        else
                        {
                            //check only the right side of the  Acronym
                            wordToCheck = words[i + 1];
                            isAllUpperWithNoneChars = IsAllUpperWithNoneChars(wordToCheck);
                            if (isAllUpperWithNoneChars)
                            {
                                checkLeftAndRightWordsUpperCase = false;
                                return checkLeftAndRightWordsUpperCase;
                            }
                            //check only the left side of the  Acronym
                            wordToCheck = words[i - 1];
                            isAllUpperWithNoneChars = IsAllUpperWithNoneChars(wordToCheck);
                            if (isAllUpperWithNoneChars)
                            {
                                checkLeftAndRightWordsUpperCase = false;
                                return checkLeftAndRightWordsUpperCase;
                            }

                            return checkLeftAndRightWordsUpperCase;
                        }

                    }

                }

            }

            return true;
        }

        public string RemoveNoneChars(string word)
        {
            Regex reg = new Regex("[^a-zA-Z']");
            return reg.Replace(word, string.Empty);
        }



        /// <summary>
        /// Returns True is non-Acronym is found
        /// </summary>
        /// <param name="Acronym"></param>
        /// <returns></returns>
        private bool IsNonAcronym(string Acronym)
        {
            if (Acronym.Trim().Length == 0)
                return true;

            // Do not use acronyms that start with a number
            char[] x = Acronym.ToCharArray();
            if (DataFunctions.IsNumeric(x[0].ToString()))
                return true;

            // Known list of non-Acronyms
            string[] nonAcronyms = new string[] { "I", "II", "III", "IV" };
            foreach (string acronym in nonAcronyms)
            {
                if (Acronym == acronym)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Process (5)
        /// </summary>
        /// <returns></returns>
        private int RemoveIgnoreUndefinedAcronyms()
        {
            if (_tdIgnoreDictionary == null)
            {
                return 0;
            }

            int counter = 0;

            List<DataRow> rows = new List<DataRow>();
            string filter = string.Empty;
            string acronym = string.Empty;

            FindAcronymsNotDefined();

            string ignoreFile = string.Concat(_IgnoreDictionaryName, ".xml");
            string ignoreDictionary = Path.Combine(_IgnoreDictionaryPath, ignoreFile);
            if (!File.Exists(ignoreDictionary)) ;
            return -1;

            Atebion.Common.GenericDataManger gDataMgr = new GenericDataManger();

            DataSet dsignoreDictionary = gDataMgr.LoadDatasetFromXml(ignoreDictionary);
            _tdIgnoreDictionary = dsignoreDictionary.Tables[0];

            foreach (DataRow row in _dtAcronymsNotDefined.Rows)
            {
                acronym = row[AcronymsFoundFieldConst.Acronym].ToString();
                acronym = Regex.Replace(acronym, @"[\r\n\x00\x1a\\'""]", @"\$0");
                filter = string.Concat(AcronymsFoundFieldConst.Acronym, " = '", acronym, "'");

                DataRow[] ignoreRow = _tdIgnoreDictionary.Select(filter);
                if (ignoreRow.Length > 0)
                {
                    DataRow[] rows2Remove = _dtAcronymsFound.Select(filter);
                    int count = rows2Remove.Length;
                    for (int i = 0; i < count; i++)
                    {
                        rows2Remove[i].Delete();
                        counter++;
                    }

                    _dtAcronymsFound.AcceptChanges();
                }
            }

            return counter;
        }

        /// <summary>
        /// Searches through selected dictionaries for definitions of Acronyms NOT defined in the document. (6) Process
        /// </summary>
        /// <returns>Qty Found</returns>
        private int FindLibAcronyms()
        {
            string lastAcronym = string.Empty;
            string currentAcronym = string.Empty;
            string def = string.Empty;
            string sentenceNos = string.Empty;
            bool foundDef = false;
            int qtyDefsFound = 0;

            bool isLastRow = false;

            bool useDictionary1 = false;
            bool useDictionary2 = false;

            int rowCount = -1;
            int i = 1; // default 1st row

            CreateTableAcronymsDicDefined();

            if (tdDictionary1 != null)
            {
                useDictionary1 = true;
            }

            if (tdDictionary2 != null)
            {
                useDictionary2 = true;
            }

            if (useDictionary1 == false && useDictionary2 == false)
            {
                return 0; // No Dictionaries selected
            }

            if (_dtAcronymsFound.Rows.Count > 0)
            {
                DataView dv = _dtAcronymsFound.DefaultView;
                dv.Sort = string.Concat(AcronymsFoundFieldConst.Acronym, " desc");
                DataTable dt = dv.ToTable();
                rowCount = dt.Rows.Count;
                foreach (DataRow row in dt.Rows)
                {

                    currentAcronym = row[AcronymsFoundFieldConst.Acronym].ToString();
                    def = row[AcronymsFoundFieldConst.Definition].ToString();
                    if (def.Length > 0)
                    {
                        foundDef = true;
                    }

                    if (lastAcronym != currentAcronym || isLastRow)
                    {
                        if (currentAcronym != string.Empty)
                        {
                            if (!foundDef)
                            {
                                if (useDictionary1) // Search in Dictionary #1
                                {
                                    def = FindLibAcronym(tdDictionary1, currentAcronym);
                                }
                                if (def.Length > 0)
                                {
                                    CreateNewDicRow(currentAcronym, def, sentenceNos, _DictionaryName1);

                                    // Test code
                                    //if (lastAcronym == "CPRs")
                                    //{
                                    //    string a = "found";
                                    //}
                                    //<-

                                    if (isLastRow)
                                    {
                                        row[AcronymsFoundFieldConst.Definition] = def;
                                        row[AcronymsFoundFieldConst.DefinitionSource] = AcronymsFoundFieldConst.DefinitionSource_Dictionary;
                                        row[AcronymsFoundFieldConst.Dictionary] = _DictionaryName1;
                                    }
                                    else
                                    {
                                        dt.Rows[i - 1][AcronymsFoundFieldConst.Definition] = def;
                                        dt.Rows[i - 1][AcronymsFoundFieldConst.DefinitionSource] = AcronymsFoundFieldConst.DefinitionSource_Dictionary;
                                        dt.Rows[i - 1][AcronymsFoundFieldConst.Dictionary] = _DictionaryName1;
                                    }

                                    dt.AcceptChanges();
                                    qtyDefsFound++;
                                }
                                else if (useDictionary2) // Search in Dictionary #1
                                {
                                    def = FindLibAcronym(tdDictionary2, currentAcronym);

                                    if (def.Length > 0)
                                    {
                                        CreateNewDicRow(currentAcronym, def, sentenceNos, _DictionaryName2);

                                        if (isLastRow)
                                        {
                                            row[AcronymsFoundFieldConst.Definition] = def;
                                            row[AcronymsFoundFieldConst.DefinitionSource] = AcronymsFoundFieldConst.DefinitionSource_Dictionary;
                                            row[AcronymsFoundFieldConst.Dictionary] = _DictionaryName2;
                                        }
                                        else
                                        {
                                            dt.Rows[i - 1][AcronymsFoundFieldConst.Definition] = def;
                                            dt.Rows[i - 1][AcronymsFoundFieldConst.DefinitionSource] = AcronymsFoundFieldConst.DefinitionSource_Dictionary;
                                            dt.Rows[i - 1][AcronymsFoundFieldConst.Dictionary] = _DictionaryName2;
                                        }

                                        dt.AcceptChanges();
                                        qtyDefsFound++;
                                    }
                                }
                            }

                            // Reset
                            sentenceNos = string.Empty;
                            foundDef = false;
                        }


                    }

                    lastAcronym = currentAcronym;

                    if (sentenceNos.Length == 0)
                    {
                        sentenceNos = row[AcronymsFoundFieldConst.SentenceNo].ToString();
                    }
                    else
                    {
                        sentenceNos = string.Concat(sentenceNos, "|", row[AcronymsFoundFieldConst.SentenceNo].ToString());
                    }

                    lastAcronym = currentAcronym;

                    i++;

                    if (i == rowCount)
                    {
                        isLastRow = true;
                    }
                }
            }

            _dtAcronymsFound.AcceptChanges();

            return qtyDefsFound;
        }

        private bool WasAcronymFound(string Acronym) // 9.24.2017
        {
            bool exists = false;

            DataRow[] foundAcronym = _dtAcronymsFound.Select(AcronymsFoundFieldConst.Acronym + " = '" + Acronym + "'");

            if (foundAcronym.Length > 0)
            {
                exists = true;
            }

            return exists;
        }

        /// <summary>
        /// Searches through selected dictionaries for Acronyms -- Deep Search
        /// </summary>
        /// <returns></returns>
        private int FindLibAcronyms_DeepSearch() // 9.25.2017
        {
            string lastAcronym = string.Empty;
            string currentAcronym = string.Empty;
            string def = string.Empty;
            int index = -1;
            int length = 0;
            string sentenceNos = string.Empty;
            bool foundDicAcronym = false;
            int qtyFound = 0;

            MatchCollection matches;

            bool useDictionary1 = false;
            bool useDictionary2 = false;

            int rowCount = 1;
            //    int i = 1; // default 1st row

            if (tdDictionary1 != null)
            {
                if (tdDictionary1.Rows.Count > 0)
                    useDictionary1 = true;
            }

            if (tdDictionary2 != null)
            {
                if (tdDictionary2.Rows.Count > 0)
                    useDictionary2 = true;
            }

            if (useDictionary1 == false && useDictionary2 == false)
            {
                return 0; // No Dictionaries selected
            }

            string Acronym_RegExp = string.Empty;


            if (_Sentences.Length > 0)
            {
                foreach (string sentence in _Sentences)
                {
                    foundDicAcronym = false; // Reset to defualt
                    if (sentence.Length > 0)
                    {
                        if (useDictionary1)
                        {
                            foreach (DataRow dic1Row in _tdDictionary1.Rows)
                            {
                                try
                                {
                                    currentAcronym = dic1Row[AcronymsFoundFieldConst.Acronym].ToString();

                                    if (!WasAcronymFound(currentAcronym))
                                    {
                                        Acronym_RegExp = @"\b" + currentAcronym + @"\b";

                                        //Acronym_RegExp = currentAcronym;

                                        Regex regex = new Regex(Acronym_RegExp, RegexOptions.Multiline);

                                        matches = regex.Matches(sentence);

                                        if (matches.Count > 0)
                                        {
                                            def = dic1Row[AcronymsFoundFieldConst.Definition].ToString();
                                            index = matches[0].Index;
                                            length = matches[0].Length;

                                            CreateNewRow(currentAcronym, def, AcronymsFoundFieldConst.DefinitionSource_Dictionary, sentence, rowCount, "10.1", index, length, _DictionaryName1);
                                            CreateNewDicRow(currentAcronym, def, rowCount.ToString(), _DictionaryName1);
                                            CreateNotDefindedRow(currentAcronym, rowCount.ToString(), sentence);

                                            qtyFound++;
                                            foundDicAcronym = true;
                                        }
                                    }
                                }
                                catch (Exception ex1)
                                {
                                    string error = string.Concat("FindLibAcronyms_DeepSearch - Dictionary: ", _DictionaryName1, " - Acronym: ", currentAcronym, "  -- Error: ", ex1.Message);
                                    _sbErrors.AppendLine(error);
                                }

                            }
                        }

                        if (useDictionary2 && !foundDicAcronym)
                        {
                            foreach (DataRow dic2Row in _tdDictionary2.Rows)
                            {
                                try
                                {
                                    currentAcronym = dic2Row[AcronymsFoundFieldConst.Acronym].ToString();

                                    if (!WasAcronymFound(currentAcronym))
                                    {
                                        Acronym_RegExp = @"\b" + currentAcronym + @"\b";

                                        Regex regex = new Regex(Acronym_RegExp, RegexOptions.Multiline);

                                        matches = regex.Matches(sentence);

                                        if (matches.Count > 0)
                                        {
                                            def = dic2Row[AcronymsFoundFieldConst.Definition].ToString();
                                            index = matches[0].Index;
                                            length = matches[0].Length;

                                            CreateNewRow(currentAcronym, def, AcronymsFoundFieldConst.DefinitionSource_Dictionary, sentence, rowCount, "10.2", index, length, _DictionaryName2);
                                            CreateNewDicRow(currentAcronym, def, rowCount.ToString(), _DictionaryName1);
                                            CreateNotDefindedRow(currentAcronym, rowCount.ToString(), sentence);

                                            qtyFound++;
                                        }
                                    }
                                }
                                catch (Exception ex2)
                                {
                                    string error = string.Concat("FindLibAcronyms_DeepSearch - Dictionary: ", _DictionaryName2, " - Acronym: ", currentAcronym, "  -- Error: ", ex2.Message);
                                    _sbErrors.AppendLine(error);
                                }
                            }
                        }
                    }

                    rowCount++;
                }
            }

            return qtyFound;
        }

        /// <summary>
        /// Update the Found Acronyms table with the found in Dictionaries
        /// </summary>
        /// <returns></returns>
        private int UpdateFoundAcronymsPerDics() // Added 09.20.2017
        {
            string acronym = string.Empty;
            string def = string.Empty;
            string dicName = string.Empty;

            int counter = 0;

            if (_dtAcronymsFound.Rows.Count == 0)
            {
                return 0;
            }

            if (_dtAcronymsDocDefined.Rows.Count == 0)
            {
                return 0;
            }

            foreach (DataRow row in _dtAcronymsDicDefined.Rows)
            {
                acronym = row[AcronymsFoundFieldConst.Acronym].ToString();
                def = row[AcronymsFoundFieldConst.Definition].ToString();
                dicName = row[AcronymsFoundFieldConst.Dictionary].ToString();
                foreach (DataRow foundRow in _dtAcronymsFound.Rows)
                {
                    if (foundRow[AcronymsFoundFieldConst.Acronym].ToString() == acronym)
                    {
                        foundRow[AcronymsFoundFieldConst.Definition] = def;
                        foundRow[AcronymsFoundFieldConst.DefinitionSource] = AcronymsFoundFieldConst.DefinitionSource_Dictionary;
                        foundRow[AcronymsFoundFieldConst.Dictionary] = dicName;

                        _dtAcronymsFound.AcceptChanges();
                        counter++;
                    }
                }

            }

            return counter;

        }


        /// <summary>
        /// Searches for Acronym in a selected Lib. DataTable
        /// </summary>
        /// <param name="dtLib">Lib. DataTable</param>
        /// <param name="FoundAcronym">Acronym  found in document</param>
        /// <returns>Acronym's Definition in the Library/Dictionary</returns>
        private string FindLibAcronym(DataTable dtLib, string FoundAcronym)
        {
            if (dtLib == null)
                return string.Empty;

            if (FoundAcronym.Length == 0)
                return string.Empty;

            string acronym = string.Empty;
            string query = string.Empty;
            string def = string.Empty;
            string filter = string.Empty;
            DataRow[] rowsFound;

            string adjFoundAcronym = FoundAcronym.ToUpper();

            adjFoundAcronym = Regex.Replace(adjFoundAcronym, @"[\r\n\x00\x1a\\'""]", @"\$0");
            query = string.Concat(DefLibFieldConst.Acronym, " = ", "'", adjFoundAcronym, "'");

            rowsFound = dtLib.Select(query);
            if (rowsFound.Length > 0)
            {
                def = rowsFound[0][DefLibFieldConst.Definition].ToString();

            }

            return def;
        }

        /// <summary>
        /// Compiles Acronyms found in Document
        /// </summary>
        /// <returns></returns>
        private int CompileAcronymsDocDefined()
        {

            CreateTableAcronymsDocDefined();
            _UIDDocDefined = 0;

            int counter = 0;

            string acronym = string.Empty;
            string acronymDef = string.Empty;
            int sentenceNo = -1;
            string sentence = string.Empty;

            string filter = string.Concat(AcronymsFoundFieldConst.DefinitionSource, " = '", AcronymsFoundFieldConst.DefinitionSource_Document, "'");
            DataRow[] rows = _dtAcronymsFound.Select(filter);
            foreach (DataRow row in rows)
            {
                acronym = row[AcronymsFoundFieldConst.Acronym].ToString();
                acronymDef = row[AcronymsFoundFieldConst.Definition].ToString();
                sentenceNo = (int)row[AcronymsFoundFieldConst.SentenceNo];
                sentence = row[AcronymsFoundFieldConst.xSentence].ToString();
                CreateNewDocRow(acronym, acronymDef, sentenceNo, sentence);
                counter++;
            }

            return counter;
        }

        /// <summary>
        /// Finds Acronyms NOT Defined (5.1) (6.1)
        /// </summary>
        /// <returns></returns>
        private int FindAcronymsNotDefined()
        {
            string acronym = string.Empty;
            string sentenceNos = string.Empty;
            int count = 0;

            _UIDDNotDefined = 0;
            CreateTableAcronymsNotDefinedFound();

            //      List<DataRow> rows = _dtAcronymsFound.AsEnumerable().Select(c => (DataRow)c[AcronymsFoundFieldConst.Acronym]).Distinct().ToList();

            string[] TobeDistinct = { AcronymsFoundFieldConst.Acronym };
            DataTable dtUniqRecords = new DataTable();
            dtUniqRecords = _dtAcronymsFound.DefaultView.ToTable(true, TobeDistinct);

            string sentence = string.Empty;
            string foundDef = string.Empty;
            foreach (DataRow row in dtUniqRecords.Rows)
            {
                acronym = row[AcronymsFoundFieldConst.Acronym].ToString();
                //  sentenceNo = (int)row[AcronymsFoundFieldConst.SentenceNo];
                if (!isAcronymsDefined(acronym, out foundDef))
                {
                    sentenceNos = GetSentenceNos4Acronym(acronym);

                    // sentence = row[AcronymsFoundFieldConst.xSentence].ToString();

                    sentence = string.Empty;

                    if (sentenceNos.Length > 0) // Added 09.10.2017
                    {
                        if (sentenceNos.IndexOf('|') > -1)
                        {
                            string[] sNos = sentenceNos.Split('|');
                            sentence = _Sentences[Convert.ToInt32(sNos[0]) - 1]; // Subtract 1 b/c of zero base
                        }
                        else
                        {
                            sentence = _Sentences[Convert.ToInt32(sentenceNos) - 1]; // Subtract 1 b/c of zero base
                        }
                    }


                    CreateNotDefindedRow(acronym, sentenceNos, sentence);

                    count++;
                }
            }

            return count;

        }



        /// <summary>
        /// Catalogue Multi-Definitions (7)
        /// </summary>
        /// <returns>Qty Found</returns>
        private int FindAcronymsMultiDefined()
        {
            string acronym = string.Empty;
            string sentenceNos = string.Empty;
            int count = 0;


            List<string> Defs = new List<string>();

            _UIDMultiDefined = 0;
            CreateTableAcronymsMultDefined();

            //List<DataRow> rows = _dtAcronymsFound.AsEnumerable().Select(c => (DataRow)c[AcronymsFoundFieldConst.Acronym]).Distinct().ToList();
            string[] TobeDistinct = { AcronymsFoundFieldConst.Acronym };
            DataTable dtUniqRecords = new DataTable();
            dtUniqRecords = _dtAcronymsFound.DefaultView.ToTable(true, TobeDistinct);

            foreach (DataRow row in dtUniqRecords.Rows)
            {
                acronym = row[AcronymsFoundFieldConst.Acronym].ToString();

                Defs = GetMultiDefindedAcronym(acronym, out sentenceNos);
                if (Defs.Count > 1)
                {
                    string[] SentenceNos = sentenceNos.Split('|'); // Added 9.8.2017
                    int i = 0;

                    foreach (string def in Defs)
                    {
                        string[] parseDefSentence = def.Split('|');
                        CreateMultitDefindedRow(acronym, parseDefSentence[0], SentenceNos[i], parseDefSentence[1]);
                        count++;
                        i++;
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// Catalogue Diff-Definitions (8)
        /// </summary>
        /// <returns>>Qty Found</returns>
        private int FindAcronymsDiffDefined()
        {
            string acronym = string.Empty;
            string sentenceNos = string.Empty;
            int count = 0;

            List<string> DefsSentences = new List<string>();
            List<string> Defs = new List<string>();
            List<string> Sentences = new List<string>();

            string[] sDefsSentences;


            _UIDDiffDefined = 0;
            CreateTableAcronymsDiffDefined(); //_dtAcronymsDiffDefined

            //     List<DataRow> rows = _dtAcronymsFound.AsEnumerable().Select(c => (DataRow)c[AcronymsFoundFieldConst.Acronym]).Distinct().ToList();
            string[] TobeDistinct = { AcronymsFoundFieldConst.Acronym };
            DataTable dtUniqRecords = new DataTable();
            dtUniqRecords = _dtAcronymsFound.DefaultView.ToTable(true, TobeDistinct);

            foreach (DataRow row in dtUniqRecords.Rows)
            {
                acronym = row[AcronymsFoundFieldConst.Acronym].ToString();

                DefsSentences.Clear();
                Defs.Clear();
                Sentences.Clear();

                DefsSentences = GetMultiDefindedAcronym(acronym, out sentenceNos);
                if (DefsSentences.Count > 1)
                {
                    foreach (string def in DefsSentences)
                    {
                        if (def.IndexOf('|') > -1)
                        {
                            sDefsSentences = def.Split('|');
                            Defs.Add(sDefsSentences[0]);
                            Sentences.Add(sDefsSentences[1]);
                        }
                    }

                    string[] SentenceNos = sentenceNos.Split('|');

                    List<string> distincts = Defs.Distinct().ToList();

                    foreach (string distinctDiff in distincts)
                    {
                        int i = 0;
                        foreach (string def in Defs)
                        {
                            if (distinctDiff != def)
                            {
                                CreateDiffDefindedRow(acronym, def, Sentences[i]);
                                count++;
                            }
                            i++;
                        }
                    }
                }
            }

            return count;
        }



        /// <summary>
        /// Get Definitions for a given Acronym
        /// </summary>
        /// <param name="FoundAcronym">Given Acronym</param>
        /// <param name="sentenceNos">Sentences of the Definitions</param>
        /// <returns></returns>
        private List<string> GetMultiDefindedAcronym(string FoundAcronym, out string sentenceNos)
        {
            sentenceNos = string.Empty;

            List<string> Multi = new List<string>();
            string diff = string.Empty;
            string sentence = string.Empty;


            string filter = string.Concat(AcronymsFoundFieldConst.Acronym, " = '", Regex.Replace(FoundAcronym, @"[\r\n\x00\x1a\\'""]", @"\$0"), "'");

            DataRow[] rows = _dtAcronymsFound.Select(filter);

            if (rows.Length == 0)
            {
                return Multi;
            }

            foreach (DataRow row in rows)
            {
                if (row[AcronymsFoundFieldConst.DefinitionSource].ToString() == AcronymsFoundFieldConst.DefinitionSource_Document)
                {
                    diff = row[AcronymsFoundFieldConst.Definition].ToString();
                    if (diff.Length > 0) // Should always contain a Def.
                    {
                        sentence = row[AcronymsFoundFieldConst.xSentence].ToString(); // Added 9.8.2017
                        Multi.Add(string.Concat(diff, "|", sentence));
                        if (sentenceNos == string.Empty)
                        {
                            sentenceNos = row[AcronymsFoundFieldConst.SentenceNo].ToString();
                        }
                        else
                        {
                            sentenceNos = string.Concat(sentenceNos, "|", row[AcronymsFoundFieldConst.SentenceNo].ToString());
                        }
                    }
                }
            }

            return Multi;
        }

        private string GetSentenceNos4Acronym(string FoundAcronym)
        {
            string sentenceNos = string.Empty;

            string filter = string.Concat(AcronymsFoundFieldConst.Acronym, " = '", Regex.Replace(FoundAcronym, @"[\r\n\x00\x1a\\'""]", @"\$0"), "'");

            DataRow[] rows = _dtAcronymsFound.Select(filter);

            if (rows.Length == 0)
            {
                return string.Empty;
            }

            foreach (DataRow row in rows)
            {
                if (sentenceNos == string.Empty)
                {
                    sentenceNos = row[AcronymsFoundFieldConst.SentenceNo].ToString();
                }
                else
                {
                    sentenceNos = string.Concat(sentenceNos, "|", row[AcronymsFoundFieldConst.SentenceNo].ToString());
                }
            }

            return sentenceNos;
        }

        /// <summary>
        /// Check if the Acronym is Defined in the Document
        /// </summary>
        /// <param name="FoundAcronym"></param>
        /// <returns></returns>
        private bool isAcronymsDefined(string FoundAcronym, out string foundDef)
        {
            if (FoundAcronym.Length == 0)
            {
                foundDef = string.Empty;
                return false;
            }


            string acronym = string.Empty;
            string query = string.Empty;
            string def = string.Empty;
            string source = string.Empty;
            string filter = string.Empty;
            DataRow[] rowsFound;

            string adjFoundAcronym = FoundAcronym.ToUpper();

            adjFoundAcronym = Regex.Replace(adjFoundAcronym, @"[\r\n\x00\x1a\\'""]", @"\$0");

            query = string.Concat(AcronymsFoundFieldConst.Acronym, " = ", "'", adjFoundAcronym, "'");

            rowsFound = _dtAcronymsFound.Select(query);
            if (rowsFound.Length > 0)
            {
                foreach (DataRow row in rowsFound)
                {
                    def = row[AcronymsFoundFieldConst.Definition].ToString();
                    source = row[AcronymsFoundFieldConst.DefinitionSource].ToString(); // Added 9.21.2017

                    if (def.Length > 0 && source == AcronymsFoundFieldConst.DefinitionSource_Document) // Added source 9.21.2017
                                                                                                       // if (def.Length > 0) 
                    {
                        foundDef = def;
                        return true;
                    }
                }

            }

            foundDef = string.Empty;
            return false;
        }



        /// <summary>
        /// Searches in the primary table to see if the Acronym is the same as found in Process 3.1 (see Arch. diagram).
        /// </summary>
        /// <param name="foundAcronym">Identified Acronym</param>
        /// <param name="SentenceNo">Current Sentence</param>
        /// <returns>Returns if the Acronym as was found in Process 3.1</returns>
        private bool isSameAcronym(string foundAcronym, int SentenceNo)
        {
            bool sameAcronym = false;
            int index = -1;

            if (foundAcronym.Length > 1) // Fixed zero length bug in old AcroParser -- 10/20/2015
            {

                //  string findStr = string.Concat(AcronymsFoundFieldConst.Acronym, " = '", foundAcronym, "'", " AND ", AcronymsFoundFieldConst.SentenceNo, " = ", SentenceNo.ToString());
                string findStr = string.Concat(AcronymsFoundFieldConst.Acronym, " = '", Regex.Replace(foundAcronym, @"[\r\n\x00\x1a\\'""]", @"\$0"), "'", " AND ", AcronymsFoundFieldConst.SentenceNo, " = ", SentenceNo.ToString());
                DataRow[] rows = _dtAcronymsFound.Select(findStr);

                if (rows.Length == 0)
                {
                    return sameAcronym;
                }
                else
                {
                    // Is this the same Acronym as was found before?
                    foreach (DataRow row in rows)
                    {

                        if (foundAcronym == row[AcronymsFoundFieldConst.Index].ToString())
                        {
                            if (row[AcronymsFoundFieldConst.FoundByAlgorithm].ToString() == "3.1") // RegExp in Function that finds Acronyms with Def.
                            {
                                int xIndex = (int)row[AcronymsFoundFieldConst.Index];
                                int xLength = (int)row[AcronymsFoundFieldConst.Length];

                                if (foundAcronym.Length < xLength)
                                {
                                    int x = xLength - (row[AcronymsFoundFieldConst.Definition].ToString().Length + 2); // Plus 2 for " ("
                                    if (x == index) // If the start of Acronym is the same, then  
                                    {
                                        sameAcronym = true;
                                        return sameAcronym;
                                    }
                                }
                            }
                        }
                    }

                }

            }

            return sameAcronym;
        }

        /// <summary>
        /// Finds Def.s with 's (Passed Tests ~180 mil-sec)
        /// </summary>
        /// <param name="Sentence"></param>
        /// <param name="FoundAcronym"></param>
        /// <returns></returns>
        private string FindDef_ApostropheS(string Sentence, string FoundAcronym)
        {

            if (FoundAcronym.Contains("AFPC"))
            {
                string p = "";
            }
            try
            {
                string apostropheS = string.Empty;
                if (FoundAcronym.Contains("'s"))
                {
                    apostropheS = "'s";
                }
                else if (FoundAcronym.Contains("’s"))
                {
                    apostropheS = "’s";
                }

                if (apostropheS.Length == 0)
                {
                    return string.Empty;
                }

                //Stopwatch stopwatch = new Stopwatch();
                //// Begin timing.
                //stopwatch.Start();

                string apostropheWord1stChar = string.Empty;
                int i = 0;
                string[] words = Sentence.Split(' ');
                foreach (string word in words)
                {
                    if (word.IndexOf(apostropheS) > -1)
                    {
                        apostropheWord1stChar = word.Substring(0, 1);
                        if (FoundAcronym.Contains(string.Concat(apostropheWord1stChar, apostropheS)))
                        {
                            int max = words.Length - 2;
                            int start = max - (FoundAcronym.Length - 2);
                            if (start < 0)
                            {
                                return string.Empty;
                            }
                            StringBuilder sb = new StringBuilder();
                            string def = string.Empty;

                            for (int x = start; x < max; x++)
                            {
                                if (sb.Length == 0)
                                    sb.Append(string.Concat(words[x]));
                                else
                                    sb.Append(" " + string.Concat(words[x]));
                            }
                            string result = sb.ToString();
                            string xAcronym = Get1stCharFromWords(result) + apostropheS;
                            if (FoundAcronym.ToUpper() != xAcronym.ToUpper()) // If the 1st char.s of result Not Equal FoundAcronym, then the Def. might contain "and" or "&" or "of". Therefore, adjust result accordingly!
                            {
                                if (result.IndexOf("and ") > -1) // check for "and", if found concatenate another word
                                {
                                    if (start - 1 > -1)
                                    {
                                        result = string.Concat(words[start - 1], " ", result); // e.g. Customs and Border Protection’s = CBP's
                                    }
                                }
                                else if (result.IndexOf("& ") > -1) // check for "&", if found concatenate another word
                                {
                                    if ((start - 1) > -1)
                                    {
                                        result = string.Concat(words[start - 1], " ", result); // e.g. Customs & Border Protection’s = CBP's
                                    }
                                }
                                else if (result.IndexOf("of ") > -1) // check for "of", if found concatenate another word
                                {
                                    if ((start - 1) > -1)
                                    {
                                        result = string.Concat(words[start - 1], " ", result); // e.g. National Institute of Justice's  = NIJ's
                                    }
                                }

                            }
                            //stopwatch.Stop();
                            //_TimeElapsed = stopwatch.Elapsed.ToString();
                            return result;
                        }

                    }

                    i++;
                }
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;

                // Get stack trace for the exception with source file information
                var st = new StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();

                _sbLog.AppendLine(" ");
                _sbLog.AppendLine("Error ");
                _sbLog.AppendLine("Function: FindDef_ApostropheS");
                _sbLog.AppendLine(string.Concat("         FoundAcronym: ", FoundAcronym));
                _sbLog.AppendLine(string.Concat("         Sentence: ", Sentence));
                _sbLog.AppendLine(string.Concat("         Line: ", line.ToString()));
                _sbLog.AppendLine(string.Concat("         Message: ", ex.Message));

            }

            //stopwatch.Stop();

            //_TimeElapsed = string.Empty;
            return string.Empty;
        }

        private string FindDef_Plural(string Sentence, string FoundAcronym)
        {
            try
            {
                string adjFoundAcronym = string.Concat("(", FoundAcronym, "s)");

                string lastWord1stChar = string.Empty;
                string lastCharFoundAcronym = string.Empty;
                int i = 0;

                //   int locAcroonym = Sentence.IndexOf(adjFoundAcronym);

                string adjWord = string.Empty;
                string[] words = Sentence.Split(' ');
                foreach (string word in words)
                {

                    adjWord = CleanWord(word);

                    if (adjWord == adjFoundAcronym)
                    {
                        if (i == 0) // If the Acronym is the 1st and Last word, then there is not Def.
                        {
                            return string.Empty;
                        }

                        lastWord1stChar = words[i - 1].Substring(0, 1); // Get the 1st Char of the previous word
                        lastCharFoundAcronym = FoundAcronym.Substring(FoundAcronym.Length - 1, 1);
                        if (lastWord1stChar.ToUpper() == lastCharFoundAcronym.ToUpper())
                        {

                            int max = i;
                            int start = max - (FoundAcronym.Length);
                            if (start < 0)
                            {
                                return string.Empty;
                            }
                            StringBuilder sb = new StringBuilder();
                            string def = string.Empty;

                            for (int x = start; x < max; x++)
                            {
                                if (sb.Length == 0)
                                    sb.Append(string.Concat(words[x]));
                                else
                                    sb.Append(" " + string.Concat(words[x]));
                            }
                            string result = sb.ToString();
                            //
                            string xAcronym = Get1stCharFromWords(result);
                            if (FoundAcronym.ToUpper() != xAcronym.ToUpper()) // If the 1st char.s of result Not Equal FoundAcronym, then the Def. might contain "and" or "&" or "of". Therefore, adjust result accordingly!
                            {
                                if (result.IndexOf("and ") > -1) // check for "and", if found concatenate another word
                                {
                                    if (start - 1 > -1)
                                    {
                                        result = string.Concat(words[start - 1], " ", result); // e.g. Customs and Border Protection’s = CBP's
                                    }
                                }
                                else if (result.IndexOf("& ") > -1) // check for "&", if found concatenate another word
                                {
                                    if ((start - 1) > -1)
                                    {
                                        result = string.Concat(words[start - 1], " ", result); // e.g. Customs & Border Protection’s = CBP's
                                    }
                                }
                                else if (result.IndexOf("of ") > -1) // check for "of", if found concatenate another word
                                {
                                    if ((start - 1) > -1)
                                    {
                                        result = string.Concat(words[start - 1], " ", result); // e.g. National Institute of Justice's  = NIJ's
                                    }
                                }

                            }
                            //stopwatch.Stop();
                            //_TimeElapsed = stopwatch.Elapsed.ToString();
                            return result;
                        }

                    }

                    i++;
                }
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;

                // Get stack trace for the exception with source file information
                var st = new StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();

                _sbLog.AppendLine(" ");
                _sbLog.AppendLine("Error ");
                _sbLog.AppendLine("Function: FindDef_Plural");
                _sbLog.AppendLine(string.Concat("         FoundAcronym: ", FoundAcronym));
                _sbLog.AppendLine(string.Concat("         Sentence: ", Sentence));
                _sbLog.AppendLine(string.Concat("         Line: ", line.ToString()));
                _sbLog.AppendLine(string.Concat("         Message: ", ex.Message));

            }

            //stopwatch.Stop();

            //_TimeElapsed = string.Empty;
            return string.Empty;


        }

        private string[] GetTheLast2Words(string subSentence)
        {
            List<string> last2Words = new List<string>();

            string[] words = subSentence.Split(' ');

            int qtyWords = words.Length;
            if (qtyWords == 0 || words.Length == 1)
            {
                return null;
            }

            int x = qtyWords - 2;
            last2Words.Add(words[x]);

            x = qtyWords - 1;
            last2Words.Add(words[x]);

            return last2Words.ToArray();
        }

        /// <summary>
        /// Finds Def.s defined as two words concatenation, e.g. Vertical Replenishment (VERTREP) and Connected Replenishment (CONREP)
        /// </summary>
        /// <param name="Sentence"></param>
        /// <param name="FoundAcronym"></param>
        /// <returns></returns>
        private string FindDef_Concat(string Sentence, string FoundAcronym)
        {

            //Stopwatch stopwatch = new Stopwatch();
            //// Begin timing.
            //stopwatch.Start();

            try
            {

                string adjFoundAcronym = string.Concat("(", FoundAcronym, ")");

                int acronymLocIndex = Sentence.IndexOf(adjFoundAcronym);

                if (acronymLocIndex == -1 || acronymLocIndex == 0)
                    return string.Empty;

                string subSentence = Sentence.Substring(0, acronymLocIndex - 1);

                string[] twoWords = GetTheLast2Words(subSentence);

                if (twoWords == null)
                    return string.Empty;

                if (twoWords[0].Trim() == string.Empty)
                    return string.Empty;

                if (twoWords[1].Trim() == string.Empty)
                    return string.Empty;

                char[] word1Char = twoWords[0].ToCharArray();
                char c1 = word1Char[0];
                //if (!char.IsUpper(c1))
                //    return string.Empty;

                if (FoundAcronym.IndexOf(c1.ToString().ToUpper()) == -1)
                    return string.Empty;

                char[] word2Char = twoWords[1].ToCharArray();
                char c2 = word2Char[0];

                if (FoundAcronym.IndexOf(c2.ToString().ToUpper()) == -1)
                    return string.Empty;


                //if (!char.IsUpper(c2))
                //    return string.Empty;

                string subWord1 = string.Empty;
                string lastWord1 = string.Empty;
                string word1 = twoWords[0];
                foreach (char c in word1Char)
                {
                    if (subWord1 == string.Empty)
                    {
                        subWord1 = c.ToString();
                    }
                    else
                    {
                        lastWord1 = subWord1;
                        subWord1 = string.Concat(subWord1, c.ToString());
                        if (FoundAcronym.ToUpper().IndexOf(subWord1.ToUpper()) == -1)
                        {
                            subWord1 = lastWord1;
                            break;
                        }
                    }
                }


                string subWord2 = string.Empty;
                string lastWord2 = string.Empty;
                string word2 = twoWords[1];
                foreach (char c in word2Char)
                {
                    if (subWord2 == string.Empty)
                    {
                        subWord2 = c.ToString();
                    }
                    else
                    {
                        lastWord2 = subWord2;
                        subWord2 = string.Concat(subWord2, c.ToString());
                        if (FoundAcronym.ToUpper().IndexOf(subWord2.ToUpper()) == -1)
                        {
                            subWord2 = lastWord2;
                            break;
                        }
                    }
                }

                // Test code - CONREP 
                //if (FoundAcronym == "CONREP")
                //{
                //    string test1 = "found";
                //}


                string concatAcronym = string.Concat(subWord1.ToUpper(), subWord2.ToUpper());
                if (FoundAcronym.ToUpper() != concatAcronym)
                    return string.Empty;


                string def = string.Concat(twoWords[0], " ", twoWords[1]);

                return def;

            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;

                // Get stack trace for the exception with source file information
                var st = new StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();

                _sbLog.AppendLine(" ");
                _sbLog.AppendLine("Error ");
                _sbLog.AppendLine("Function: FindDef_Concat");
                _sbLog.AppendLine(string.Concat("         FoundAcronym: ", FoundAcronym));
                _sbLog.AppendLine(string.Concat("         Sentence: ", Sentence));
                _sbLog.AppendLine(string.Concat("         Line: ", line.ToString()));
                _sbLog.AppendLine(string.Concat("         Message: ", ex.Message));

            }

            return string.Empty;


            //stopwatch.Stop();

            //_TimeElapsed = string.Empty;

        }

        private string WordRemoveEndPeriod(string word) // Added 10.24.2017
        {
            if (word == string.Empty)
                return word;

            if (word.IndexOf('.') == -1)
                return word;

            if (word.Substring(word.Length - 1, 1) == ".")
            {
                return word.Substring(0, word.Length - 1);
            }

            return word;
        }

        private string WordRemoveEndColon(string word) // Added 11.01.2017
        {
            if (word == string.Empty)
                return word;

            if (word.IndexOf(':') == -1)
                return word;

            if (word.Substring(word.Length - 1, 1) == ":")
            {
                return word.Substring(0, word.Length - 1);
            }

            return word;
        }

        private string WordRemoveEndComma(string word) // Added 10.24.2017
        {
            if (word == string.Empty)
                return word;

            if (word.IndexOf(',') == -1)
                return word;

            if (word.Substring(word.Length - 1, 1) == ",")
            {
                return word.Substring(0, word.Length - 1);
            }

            return word;
        }

        private string WordRemoveEndSemicolon(string word) // Added 10.25.2017
        {
            if (word == string.Empty)
                return word;

            if (word.IndexOf(';') == -1)
                return word;

            if (word.Substring(word.Length - 1, 1) == ";")
            {
                return word.Substring(0, word.Length - 1);
            }

            return word;
        }

        private string RemoveQuotes(string word) // Added 10.25.2017
        {
            if (word == string.Empty)
                return word;

            if (word.IndexOf('"') == -1)
                return word;

            string adjWord = word.Replace("\"", "");

            adjWord = adjWord.Replace("'", "");

            adjWord = adjWord.Replace("'", "");

            return adjWord;
        }

        private string RemoveLeftQuotes(string word) // Added 10.25.2017
        {
            if (word == string.Empty)
                return word;

            if (word.IndexOf('“') == -1)
                return word;

            string adjWord = word.Replace("“", "");

            return adjWord;
        }

        private string RemoveRightQuotes(string word) // Added 10.25.2017
        {
            if (word == string.Empty)
                return word;

            if (word.IndexOf('”') == -1)
                return word;

            string adjWord = word.Replace("”", "");

            return adjWord;
        }



        private string CleanSentence(string Sentence)
        {
            string adjSentence = RemoveQuotes(Sentence);
            adjSentence = RemoveLeftQuotes(adjSentence);
            adjSentence = RemoveRightQuotes(adjSentence);
            adjSentence = adjSentence.Replace("'", ""); // Added 05.26.2020
            adjSentence = adjSentence.Replace("\n", "").Replace("\r", ""); // 10.09.2020

            return adjSentence;
        }

        private string CleanWord(string word) // Added 10.25.2017
        {
            string adjWord = word;

            if (word == string.Empty)
                return word;

            adjWord = WordRemoveEndPeriod(adjWord);
            adjWord = WordRemoveEndColon(adjWord);
            adjWord = WordRemoveEndComma(adjWord);
            adjWord = WordRemoveEndSemicolon(adjWord);
            adjWord = RemoveQuotes(adjWord);
            adjWord = RemoveLeftQuotes(adjWord);
            adjWord = RemoveRightQuotes(adjWord);

            return adjWord;

        }

        // Example type: AF Reserve Command (AFRC)
        private string FindDef_AcronymWithPrefixAcronym(string Sentence, string FoundAcronym)
        {
            // Test Code
            bool foundFoundAcronym = false;
            if (FoundAcronym == "AFRC")
                foundFoundAcronym = true;

            int acronymLength = FoundAcronym.Length;
            if (acronymLength < 3)
                return string.Empty;

            char[] charsAcronym = FoundAcronym.ToCharArray();

            string adjFoundAcronym = string.Concat("(", FoundAcronym, ")");

            int index = Sentence.IndexOf(adjFoundAcronym);
            if (index == -1)
                return string.Empty;

            string subSentence = Sentence.Substring(0, index);

            string adjWord = string.Empty;
            int i = 0;
            int y = 0;

            string result = string.Empty;
            string[] words = subSentence.Split(' ');
            int wordCount = words.Length;
            char[] charsWord;

            int lastWord = -1;
            int lastCharAcronym = -1;
            bool is1stLoop = false;
            bool notFound1stChar = false;

            string testAcronym = string.Empty;

            try
            {
                for (i = (wordCount - 1); i > -1; i--)
                {
                    if (notFound1stChar)
                        break;

                    adjWord = CleanWord(words[i]);
                    if (adjWord.Trim().Length > 1)
                    {
                        charsWord = adjWord.ToCharArray();
                        is1stLoop = true;
                        for (y = (acronymLength - 1); y > -1; y--)
                        {

                            if (charsWord[0] == charsAcronym[y])
                            {
                                is1stLoop = false;
                                result = string.Concat(adjWord, result);
                                testAcronym = string.Concat(testAcronym, charsAcronym[y]);
                            }
                            else
                            {
                                if (is1stLoop)
                                {
                                    lastWord = i;
                                    lastCharAcronym = y;
                                    notFound1stChar = true;
                                }
                                else
                                {
                                    lastWord = i - 1;
                                    lastCharAcronym = y;
                                }

                                break;
                            }
                        }
                    }
                }

                if (testAcronym == FoundAcronym)
                    return result;

                if (result == string.Empty)
                    return string.Empty;

                adjWord = CleanWord(words[lastWord]);
                charsWord = adjWord.ToCharArray();
                if (charsWord[0] == charsAcronym[0])
                {

                    for (int x = 0; x < (i + 1); x++)
                    {
                        if (charsWord[x] == charsAcronym[x])
                            result = string.Concat(adjWord, result);
                        else
                            break;
                    }

                }

                if (testAcronym == FoundAcronym)
                    return result;
                else
                    return string.Empty;

            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;

                // Get stack trace for the exception with source file information
                var st = new StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();

                _sbLog.AppendLine(" ");
                _sbLog.AppendLine("Error ");
                _sbLog.AppendLine("Function: FindDef_AcronymWithPrefixAcronym");
                _sbLog.AppendLine(string.Concat("         FoundAcronym: ", FoundAcronym));
                _sbLog.AppendLine(string.Concat("         Sentence: ", Sentence));
                _sbLog.AppendLine(string.Concat("         Line: ", line.ToString()));
                _sbLog.AppendLine(string.Concat("         Message: ", ex.Message));

            }

            return string.Empty;
        }

        private string FindDef_AcronymEndingWithSmallS(string Sentence, string FoundAcronym)
        {

            //Stopwatch stopwatch = new Stopwatch();
            //// Begin timing.
            //stopwatch.Start();

            try
            {

                int acronymLength = FoundAcronym.Length + 1;

                if (acronymLength == 0)
                    return string.Empty;

                //string small_s = FoundAcronym.Substring(acronymLength - 1, 1);

                //if (small_s != "s")
                //    return string.Empty;

                string adjFoundAcronym = string.Concat("(", FoundAcronym, "s", ")");

                string adjWord = string.Empty;
                int i = 0;

                string result = string.Empty;
                string[] words = Sentence.Split(' ');
                foreach (string word in words)
                {
                    adjWord = CleanWord(word); // Added 10.25.2017  

                    if (adjWord == adjFoundAcronym)
                    {
                        if (i == 0) // If the Acronym is the 1st and Last word, then there is not Def.
                        {
                            return string.Empty;
                        }

                        for (int x = (i - (acronymLength - 1)); x < (i - 1); x++)
                        {
                            if (x < 0)
                            {
                                return string.Empty;
                            }
                            result = string.Concat(result, " ", words[x]);
                            //if (words[x].Trim() == "and")
                            //    andFound = true;
                        }
                        result = result.Trim();
                        string xAcronym = Get1stCharFromWords(result);

                        if (xAcronym.ToUpper() == FoundAcronym.ToUpper())
                        {
                            return result;
                        }

                    }

                    i++;
                }
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;

                // Get stack trace for the exception with source file information
                var st = new StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();

                _sbLog.AppendLine(" ");
                _sbLog.AppendLine("Error ");
                _sbLog.AppendLine("Function: FindDef_AcronymEndingWithSmallS");
                _sbLog.AppendLine(string.Concat("         FoundAcronym: ", FoundAcronym));
                _sbLog.AppendLine(string.Concat("         Sentence: ", Sentence));
                _sbLog.AppendLine(string.Concat("         Line: ", line.ToString()));
                _sbLog.AppendLine(string.Concat("         Message: ", ex.Message));

            }


            return string.Empty;
        }

        private string FindDef_AvailabilityAcronym(string Sentence, string FoundAcronym)
        {
            try
            {
                string adjFoundAcronym = string.Concat("(", FoundAcronym, ")");

                int acronymLength = FoundAcronym.Length;

                if (acronymLength < 3)
                {
                    return string.Empty;
                }

                string last2Char = FoundAcronym.Substring((acronymLength - 2), 2);

                if (last2Char.ToUpper() != "AV")
                {
                    return string.Empty;
                }

                string adjWord = string.Empty;
                string previousWord = string.Empty;
                int i = 0;

                string result = string.Empty;

                string[] words = Sentence.Split(' ');
                foreach (string word in words)
                {
                    adjWord = CleanWord(word);

                    if (adjWord.ToUpper() == adjFoundAcronym)
                    {

                        if (i == 0)
                        {
                            return string.Empty;
                        }


                        int x = acronymLength - 1;

                        if (i >= x)
                        {
                            int start_i = i - x;

                            if (start_i < 0)
                            {
                                return string.Empty;
                            }

                            for (int y = start_i; y < i; y++)
                            {
                                result = string.Concat(result, " ", words[y]);
                            }
                            result = result.Trim();
                            string xAcronym = Get1stCharFromWords(result);
                            xAcronym = string.Concat(xAcronym, "V");

                            if (FoundAcronym.ToUpper() == xAcronym.ToUpper())
                            {
                                return result;
                            }
                            else
                            {
                                return string.Empty;
                            }

                        }
                    }


                    i++;

                }
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;

                // Get stack trace for the exception with source file information
                var st = new StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();

                _sbLog.AppendLine(" ");
                _sbLog.AppendLine("Error ");
                _sbLog.AppendLine("Function: FindDef_AvailabilityAcronym");
                _sbLog.AppendLine(string.Concat("         FoundAcronym: ", FoundAcronym));
                _sbLog.AppendLine(string.Concat("         Sentence: ", Sentence));
                _sbLog.AppendLine(string.Concat("         Line: ", line.ToString()));
                _sbLog.AppendLine(string.Concat("         Message: ", ex.Message));

            }

            return string.Empty;
        }

        private string FindDef_IdentificationAcronym(string Sentence, string FoundAcronym)
        {
            try
            {
                string adjFoundAcronym = string.Concat("(", FoundAcronym, ")");

                int acronymLength = FoundAcronym.Length;

                if (acronymLength < 3)
                {
                    return string.Empty;
                }

                string last2Char = FoundAcronym.Substring((acronymLength - 2), 2);

                if (last2Char.ToUpper() != "ID")
                {
                    return string.Empty;
                }

                string adjWord = string.Empty;
                string previousWord = string.Empty;
                int i = 0;

                string result = string.Empty;

                string[] words = Sentence.Split(' ');
                foreach (string word in words)
                {
                    adjWord = CleanWord(word);

                    if (adjWord.ToUpper() == adjFoundAcronym)
                    {

                        if (i == 0)
                        {
                            return string.Empty;
                        }


                        int x = acronymLength - 1;

                        if (i >= x)
                        {
                            int start_i = i - x;

                            if (start_i < 0)
                            {
                                return string.Empty;
                            }

                            for (int y = start_i; y < i; y++)
                            {
                                result = string.Concat(result, " ", words[y]);
                            }
                            result = result.Trim();
                            string xAcronym = Get1stCharFromWords(result);
                            xAcronym = string.Concat(xAcronym, "D");

                            if (FoundAcronym.ToUpper() == xAcronym.ToUpper())
                            {
                                return result;
                            }
                            else
                            {
                                return string.Empty;
                            }

                        }
                    }


                    i++;

                }
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;

                // Get stack trace for the exception with source file information
                var st = new StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();

                _sbLog.AppendLine(" ");
                _sbLog.AppendLine("Error ");
                _sbLog.AppendLine("Function: FindDef_IdentificationAcronym");
                _sbLog.AppendLine(string.Concat("         FoundAcronym: ", FoundAcronym));
                _sbLog.AppendLine(string.Concat("         Sentence: ", Sentence));
                _sbLog.AppendLine(string.Concat("         Line: ", line.ToString()));
                _sbLog.AppendLine(string.Concat("         Message: ", ex.Message));

            }

            return string.Empty;
        }

        private string FindDef_NotPatternAcronym(string Sentence, string FoundAcronym)
        {

            if (FoundAcronym.ToUpper() != "AV" && FoundAcronym.ToUpper() != "PCP" && FoundAcronym.ToUpper() != "CPR" && FoundAcronym.ToUpper() != "COTS" && FoundAcronym.ToUpper() != "USSTRATCOM" && FoundAcronym.ToUpper() != "INCO")
            {
                return string.Empty;
            }


            string adjFoundAcronym = string.Concat("(", FoundAcronym, ")");

            int acronymLength = FoundAcronym.Length;

            string adjWord = string.Empty;
            string previousWord = string.Empty;
            int i = 0;

            string[] words = Sentence.Split(' ');
            foreach (string word in words)
            {
                adjWord = CleanWord(word);

                if (adjWord.ToUpper() == adjFoundAcronym)
                {
                    if (FoundAcronym.ToUpper() == "AV") // Antivirus
                    {
                        if (i != 0)
                        {
                            previousWord = words[i - 1];
                            if (previousWord.ToUpper() == "Antivirus".ToUpper())
                            {
                                return previousWord;
                            }
                        }
                    }

                    if (FoundAcronym.ToUpper() == "PCP") // Phencyclidine
                    {
                        if (i != 0)
                        {
                            previousWord = words[i - 1];
                            if (previousWord.ToUpper() == "Phencyclidine".ToUpper())
                            {
                                return previousWord;
                            }
                        }
                    }

                    if (FoundAcronym.ToUpper() == "CPR") // Cardiopulmonary Resuscitation
                    {
                        if (i >= 2)
                        {
                            previousWord = string.Concat(words[i - 2], " ", words[i - 1]);
                            if (previousWord.ToUpper() == "cardiopulmonary resuscitation".ToUpper())
                            {
                                return previousWord;
                            }
                        }
                    }

                    if (FoundAcronym.ToUpper() == "COTS") // Cardiopulmonary Resuscitation
                    {
                        if (i >= 3)
                        {
                            previousWord = string.Concat(words[i - 3], " ", words[i - 2], " ", words[i - 1]);
                            if (previousWord.ToUpper() == "commercially available off-the-shelf".ToUpper())
                            {
                                return previousWord;
                            }
                        }
                    }

                    if (FoundAcronym.ToUpper() == "USSTRATCOM") // U.S. Strategic Command
                    {
                        if (i >= 3)
                        {
                            previousWord = string.Concat(words[i - 3], " ", words[i - 2], " ", words[i - 1]);
                            if (previousWord.ToUpper() == "U.S. Strategic Command".ToUpper())
                            {
                                return previousWord;
                            }
                        }
                    }

                    if (FoundAcronym.ToUpper() == "INCO") // Installation and Checkout
                    {
                        if (i >= 3)
                        {
                            previousWord = string.Concat(words[i - 3], " ", words[i - 2], " ", words[i - 1]);
                            if (previousWord.ToUpper() == "Installation and Checkout".ToUpper())
                            {
                                return previousWord;
                            }
                            else if (previousWord.ToUpper() == "Installation and Checkouts".ToUpper())
                            {
                                return previousWord;
                            }
                            else if (previousWord.ToUpper() == "Installation & Checkout".ToUpper())
                            {
                                return previousWord;
                            }
                            else if (previousWord.ToUpper() == "Installation & Checkouts".ToUpper())
                            {
                                return previousWord;
                            }
                        }
                    }

                }

                i++;
            }

            return string.Empty;

        }

        private string FindDef_ConcatAcronym(string Sentence, string FoundAcronym)
        {

            //Stopwatch stopwatch = new Stopwatch();
            //// Begin timing.
            //stopwatch.Start();
            try
            {

                string adjFoundAcronym = string.Concat("(", FoundAcronym, ")");

                int acronymLength = FoundAcronym.Length;

                string adjWord = string.Empty;
                int i = 0;

                string[] words = Sentence.Split(' ');
                foreach (string word in words)
                {
                    adjWord = CleanWord(word); // Added 10.25.2017  

                    if (adjWord == adjFoundAcronym)
                    {
                        if (i == 0) // If the Acronym is the 1st and Last word, then there is not Def.
                        {
                            return string.Empty;
                        }

                        if (!Regex.IsMatch(words[i - 1], "[A-Z]")) // Is word prior to Acronym uppercase? If not, then exit
                        {
                            return string.Empty;
                        }

                        string otherAcronym = words[i - 1];
                        if (otherAcronym.Length > FoundAcronym.Length)
                        {
                            return string.Empty; // Other otherAcronym is not a subset of FoundAcronym
                        }

                        int delta = FoundAcronym.Length - otherAcronym.Length;
                        string result = string.Empty;

                        bool andFound = false;

                        for (int x = (i - (delta + 1)); x < (i - 1); x++)
                        {
                            if (x < 0)
                            {
                                return string.Empty;
                            }

                            result = string.Concat(result, " ", words[x]);
                            if (words[x].Trim() == "and")
                                andFound = true;
                        }
                        result = result.Trim();
                        string xAcronym = Get1stCharFromWords(result);
                        xAcronym = string.Concat(xAcronym, otherAcronym);
                        if (xAcronym.ToUpper() == FoundAcronym)
                        {
                            result = string.Concat(result, " ", otherAcronym);

                            return result;
                        }
                        else if (andFound)
                        {
                            result = string.Empty;
                            string resultWithAnd = string.Empty;
                            for (int x = (i - (delta + 2)); x < (i - 1); x++)
                            {
                                if (words[x].Trim() != "and")
                                    result = string.Concat(result, " ", words[x]);
                                else
                                    resultWithAnd = string.Concat(resultWithAnd, " ", words[x]);
                            }

                            result = result.Trim();
                            xAcronym = Get1stCharFromWords(result);
                            xAcronym = string.Concat(xAcronym, otherAcronym);
                            if (xAcronym.ToUpper() == FoundAcronym)
                            {
                                result = string.Concat(result, " ", otherAcronym);

                                return result;
                            }

                        }

                    }


                    i++;
                }
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;

                // Get stack trace for the exception with source file information
                var st = new StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();

                _sbLog.AppendLine(" ");
                _sbLog.AppendLine("Error ");
                _sbLog.AppendLine("Function: FindDef_ConcatAcronym");
                _sbLog.AppendLine(string.Concat("         FoundAcronym: ", FoundAcronym));
                _sbLog.AppendLine(string.Concat("         Sentence: ", Sentence));
                _sbLog.AppendLine(string.Concat("         Line: ", line.ToString()));
                _sbLog.AppendLine(string.Concat("         Message: ", ex.Message));

            }

            return string.Empty;
        }

        private string FindDef_ExtraWord(string Sentence, string FoundAcronym)
        {

            //Stopwatch stopwatch = new Stopwatch();
            //// Begin timing.
            //stopwatch.Start();
            try
            {

                string adjFoundAcronym = string.Concat("(", FoundAcronym, ")");

                int acronymLength = FoundAcronym.Length;

                string adjWord = string.Empty;
                int i = 0;
                string[] words = Sentence.Split(' ');
                foreach (string word in words)
                {
                    adjWord = CleanWord(word); // Added 10.25.2017  

                    if (adjWord == adjFoundAcronym)
                    {
                        if (i == 0) // If the Acronym is the 1st and Last word, then there is not Def.
                        {
                            return string.Empty;
                        }

                        int x = i - (acronymLength + 1);
                        if (x < 0)
                            return string.Empty; // Not found

                        string result = string.Empty;
                        for (int y = x; y < i - 1; y++)
                        {
                            result = string.Concat(result, " ", words[y]);
                        }
                        result = result.Trim();
                        string xAcronym = Get1stCharFromWords(result);

                        if (xAcronym.ToUpper() == FoundAcronym.ToUpper())
                        {
                            return result;
                        }
                    }

                    i++;
                }
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;

                // Get stack trace for the exception with source file information
                var st = new StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();

                _sbLog.AppendLine(" ");
                _sbLog.AppendLine("Error ");
                _sbLog.AppendLine("Function: FindDef_ExtraWord");
                _sbLog.AppendLine(string.Concat("         FoundAcronym: ", FoundAcronym));
                _sbLog.AppendLine(string.Concat("         Sentence: ", Sentence));
                _sbLog.AppendLine(string.Concat("         Line: ", line.ToString()));
                _sbLog.AppendLine(string.Concat("         Message: ", ex.Message));

            }

            //stopwatch.Stop();

            //_TimeElapsed = string.Empty;
            return string.Empty;
        }

        /// <summary>
        /// Finds Def.s containing either "and" or "&" or "of" (Passed Tests ~164 mil-sec)
        /// </summary>
        /// <param name="Sentence"></param>
        /// <param name="FoundAcronym"></param>
        /// <returns></returns>
        private string FindDef_Concealed(string Sentence, string FoundAcronym)
        {

            //Stopwatch stopwatch = new Stopwatch();
            //// Begin timing.
            //stopwatch.Start();

            try
            {

                string adjFoundAcronym = string.Concat("(", FoundAcronym, ")");

                string lastWord1stChar = string.Empty;
                string lastCharFoundAcronym = string.Empty;
                string adjWord = string.Empty;
                int i = 0;
                string[] words = Sentence.Split(' ');
                foreach (string word in words)
                {
                    adjWord = CleanWord(word); // Added 10.25.2017  

                    if (adjWord == adjFoundAcronym)
                    {
                        if (i == 0) // If the Acronym is the 1st and Last word, then there is not Def.
                        {
                            return string.Empty;
                        }

                        lastWord1stChar = words[i - 1].Substring(0, 1); // Get the 1st Char of the previous word
                        lastCharFoundAcronym = FoundAcronym.Substring(FoundAcronym.Length - 1, 1);
                        if (lastWord1stChar == lastCharFoundAcronym)
                        {

                            int max = i;

                            int start = max - (FoundAcronym.Length);

                            if (start < (FoundAcronym.Length + 1))
                            {
                                return string.Empty;
                            }

                            StringBuilder sb = new StringBuilder();
                            string def = string.Empty;

                            for (int x = start; x < max; x++)
                            {
                                if (sb.Length == 0)
                                    sb.Append(string.Concat(words[x]));
                                else
                                    sb.Append(" " + string.Concat(words[x]));
                            }
                            string result = sb.ToString();
                            string xAcronym = Get1stCharFromWords(result);
                            if (FoundAcronym.ToUpper() != xAcronym.ToUpper()) // If the 1st char.s of result Not Equal FoundAcronym, then the Def. might contain "and" or "&" or "of". Therefore, adjust result accordingly!
                            {
                                if (result.IndexOf("and ") > -1) // check for "and", if found concatenate another word
                                {
                                    if (start - 1 > -1)
                                    {
                                        result = string.Concat(words[start - 1], " ", result); // e.g. Customs and Border Protection’s = CBP's
                                    }
                                }
                                else if (result.IndexOf("& ") > -1) // check for "&", if found concatenate another word
                                {
                                    if ((start - 1) > -1)
                                    {
                                        result = string.Concat(words[start - 1], " ", result); // e.g. Customs & Border Protection’s = CBP's
                                    }
                                }
                                else if (result.IndexOf("of ") > -1) // check for "of", if found concatenate another word
                                {
                                    if ((start - 1) > -1)
                                    {
                                        result = string.Concat(words[start - 1], " ", result); // e.g. National Institute of Justice's  = NIJ's
                                    }
                                }

                            }
                            //stopwatch.Stop();
                            //_TimeElapsed = stopwatch.Elapsed.ToString();
                            return result;
                        }

                    }

                    i++;
                }
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;

                // Get stack trace for the exception with source file information
                var st = new StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();

                _sbLog.AppendLine(" ");
                _sbLog.AppendLine("Error ");
                _sbLog.AppendLine("Function: FindDef_Concealed");
                _sbLog.AppendLine(string.Concat("         FoundAcronym: ", FoundAcronym));
                _sbLog.AppendLine(string.Concat("         Sentence: ", Sentence));
                _sbLog.AppendLine(string.Concat("         Line: ", line.ToString()));
                _sbLog.AppendLine(string.Concat("         Message: ", ex.Message));

            }

            //stopwatch.Stop();

            //_TimeElapsed = string.Empty;
            return string.Empty;
        }

        private string FindDef_AndSymbol(string Sentence, string FoundAcronym)
        {
            try
            {
                if (Sentence.Trim().Length == 0)
                    return string.Empty;

                if (FoundAcronym.IndexOf('&') == -1)
                    return string.Empty;

                int acronymLength = FoundAcronym.Length;

                string adjFoundAcronym = string.Concat("(", FoundAcronym, ")");

                string[] words = Sentence.Split(' ');
                string adjWord = string.Empty;

                int i = 0;

                string result = string.Empty;
                string result_withoutAnd = string.Empty;

                if (words.Length > 0)
                {

                    foreach (string word in words)
                    {
                        adjWord = CleanWord(word);

                        if (adjWord.ToUpper() == adjFoundAcronym.ToUpper())
                        {
                            if (i == 0)
                            {
                                return string.Empty;
                            }

                            int x = acronymLength;

                            if (i >= x)
                            {
                                int start_i = i - x;
                                if (start_i < 0)
                                {
                                    return string.Empty;
                                }

                                for (int y = start_i; y < i; y++)
                                {
                                    result = string.Concat(result, " ", words[y]);

                                    if (words[y].Trim() == "and")
                                    {
                                        result_withoutAnd = string.Concat(result_withoutAnd, " ", "&");
                                    }
                                    else
                                    {
                                        result_withoutAnd = string.Concat(result_withoutAnd, " ", words[y]);
                                    }
                                }
                                result = result.Trim();
                                result_withoutAnd = result_withoutAnd.Trim();
                                string xAcronym = Get1stCharFromWords(result_withoutAnd);

                                if (FoundAcronym.ToUpper() == xAcronym.ToUpper())
                                {
                                    return result;
                                }
                                else
                                {
                                    return string.Empty;
                                }

                            }


                        }


                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;

                // Get stack trace for the exception with source file information
                var st = new StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();

                _sbLog.AppendLine(" ");
                _sbLog.AppendLine("Error ");
                _sbLog.AppendLine("Function: FindDef_AndSymbol");
                _sbLog.AppendLine(string.Concat("         FoundAcronym: ", FoundAcronym));
                _sbLog.AppendLine(string.Concat("         Sentence: ", Sentence));
                _sbLog.AppendLine(string.Concat("         Line: ", line.ToString()));
                _sbLog.AppendLine(string.Concat("         Message: ", ex.Message));

            }



            return string.Empty;
        }


        /// <summary>
        /// Finds Def.s that are Hyphenated (Passed Tests ~106 mil-sec)-- e.g. Commercial-off-the-shelf = COTS and  Commercial off-the-shelf = COTS
        /// </summary>
        /// <param name="Sentence">Sentence Text</param>
        /// <param name="FoundAcronym">Identified Acronym</param>
        /// <returns>Acronym's Definition or Empty String is Not Found</returns>
        private string FindDef_Hyphen(string Sentence, string FoundAcronym)
        {
            try
            {
                if (Sentence.Trim().Length == 0)
                    return string.Empty;

                if (Sentence.IndexOf('-') == -1)
                    return string.Empty;

                //Stopwatch stopwatch = new Stopwatch();
                //// Begin timing.
                //stopwatch.Start();


                string[] words = Sentence.Split(' ');
                string firstChars;
                int i = 0;

                if (words.Length > 0)
                {

                    foreach (string word in words)
                    {
                        if (word.IndexOf('-') > -1)
                        {
                            string wordHyphen = GetWordsHyphen(word);

                            firstChars = Get1stCharFromWords(wordHyphen);
                            if (firstChars.ToUpper() == FoundAcronym.ToUpper()) // Example: Commercial-off-the-shelf = COTS
                            {
                                // Stop timing.
                                //stopwatch.Stop();
                                //_TimeElapsed = stopwatch.Elapsed.ToString();
                                return word;
                            }
                            else
                            {
                                if (i > 0)
                                {
                                    string priorWord = words[i - 1];
                                    if (priorWord.Length > 0) // Added 10.22.2017
                                    {
                                        string firstChar = priorWord.Substring(0, 1);
                                        firstChars = string.Concat(firstChar, firstChars);
                                        if (firstChars.ToUpper() == FoundAcronym.ToUpper()) // Example: Commercial off-the-shelf = COTS
                                        {
                                            string adjustedDef = string.Concat(priorWord, " ", word);
                                            //stopwatch.Stop();
                                            //_TimeElapsed = stopwatch.Elapsed.ToString();
                                            return adjustedDef;
                                        }
                                    }
                                }

                            }
                        }
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;

                // Get stack trace for the exception with source file information
                var st = new StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();

                _sbLog.AppendLine(" ");
                _sbLog.AppendLine("Error ");
                _sbLog.AppendLine("Function: FindDef_Hyphen");
                _sbLog.AppendLine(string.Concat("         FoundAcronym: ", FoundAcronym));
                _sbLog.AppendLine(string.Concat("         Sentence: ", Sentence));
                _sbLog.AppendLine(string.Concat("         Line: ", line.ToString()));
                _sbLog.AppendLine(string.Concat("         Message: ", ex.Message));

            }

            //stopwatch.Stop();
            //_TimeElapsed = string.Empty;
            return string.Empty;
        }

        private string GetWordsHyphen(string HyphenText)
        {
            string result = string.Empty;

            if (HyphenText.IndexOf('-') == -1)
            {
                return result;
            }

            StringBuilder sb = new StringBuilder();

            string[] words = HyphenText.Split('-');
            foreach (string word in words)
            {
                if (sb.Length > 0)
                    sb.Append(string.Concat(" ", word));
                else
                    sb.Append(string.Concat(word));
            }

            return sb.ToString();
        }

        private string Get1stCharFromWords(string txt)
        {
            string result = string.Empty;
            string[] words = txt.Split(' ');

            foreach (string word in words)
            {
                if (word.Length > 0)
                    result += word[0];

            }

            return result;
        }

        private bool IsAllUpper(string input)
        {
            input = RemoveNoneChars(input);
            input = input.Trim();
            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsLetter(input[i]) && !Char.IsUpper(input[i]))
                    return false;
            }
            return true;
        }
        private bool IsAllUpperWithNoneChars(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (!Char.IsUpper(input[i]))
                    return false;
            }

            return true;
        }

        #region Reports

        public bool GenerateWordTable(string pathFileRpt, string OrgFileName, bool ShowAcronymsWithoutDef)
        {
            bool result = false;

            //     DataTable dt4Word = CreateTableAcronyms4Word();

            //   List<DataRow> rows = _dtAcronymsFound.AsEnumerable().Select(c => (DataRow)c[AcronymsFoundFieldConst.Acronym]).Distinct().ToList();

            DataView dv = getAcronymsFound();

            if (dv == null)
            {
                _ErrorMsg = "No Acronyms were Found";
                return false;
            }

            //string acronym = string.Empty;
            //string foundDef = string.Empty;
            //foreach (DataRow row in dv.ToTable().Rows)
            //{
            //    acronym = row[AcronymsFoundFieldConst.Acronym].ToString();
            //    isAcronymsDefined(acronym, out foundDef);

            //if (foundDef.Length == 0)
            //{
            //    if (ShowAcronymsWithoutDef)
            //    {
            //        DataRow newRow = dt4Word.NewRow();
            //        newRow[AcronymsFoundFieldConst.Acronym] = acronym;
            //        newRow[AcronymsFoundFieldConst.Definition] = foundDef;

            //        dt4Word.Rows.Add(newRow);
            //    }
            //}
            //else
            //{
            //DataRow newRow = dt4Word.NewRow();
            //newRow[AcronymsFoundFieldConst.Acronym] = acronym;
            //newRow[AcronymsFoundFieldConst.Definition] = foundDef;

            //dt4Word.Rows.Add(newRow);
            //}

            //}

            //if (dt4Word.Rows.Count == 0)
            //    return result;

            //DataView dv = new DataView(dt4Word);
            //string sort = string.Concat(AcronymsFoundFieldConst.Acronym, " Asc");
            //dv.Sort = sort;
            //DataTable dt4WordDistinctValues = dv.ToTable(true, AcronymsFoundFieldConst.Acronym, AcronymsFoundFieldConst.Definition);

            GenWordRpt genWordTbl = new GenWordRpt();

            string headerCaptions = "Acronym|Definition";
            //    result = genWordTbl.GenerateRpt(dt4WordDistinctValues, headerCaptions, pathFileRpt, OrgFileName);
            result = genWordTbl.GenerateRpt(dv.ToTable(), headerCaptions, pathFileRpt, OrgFileName);

            return result;
        }

        private DataTable AcronymsUseBeforeDefinition(DataTable dt, string[] _Sentences)
        {
            DataTable dt3 = dt.AsEnumerable().CopyToDataTable(); ;
            dt3.Rows.Clear();

            var ListData = dt.AsEnumerable().Distinct().ToList();
            var arDistinct = new ArrayList();

            string acronym = string.Empty;
            int SentencesNo;
            foreach (DataRow row in dt.Rows)
            {
                SentencesNo = Convert.ToInt32(row[AcronymsFoundFieldConst.SentenceNo]);
                acronym = row[AcronymsFoundFieldConst.Acronym].ToString();
                for (int i = 0; i < SentencesNo; i++)
                {
                    if (_Sentences[i].Contains(acronym))
                    {
                        //double check 
                        string[] words = _Sentences[i].Split(' ');
                        foreach (string word in words)
                        {
                            if (word == acronym)
                            {
                                // copy 
                                DataRow newRow = dt3.NewRow();
                                newRow[AcronymsFoundFieldConst.Acronym] = acronym;
                                newRow[AcronymsFoundFieldConst.xSentence] = _Sentences[i];

                                dt3.Rows.Add(newRow);
                            }
                        }

                    }

                }
            }



            foreach (var item in ListData)
            {
                if (!arDistinct.Contains(item[0].ToString()))
                {
                    arDistinct.Add(item[0].ToString());
                }
            }
            foreach (var item in arDistinct)
            {
                var lstItem = ListData.Where(x => x.ItemArray[0].ToString() == item.ToString()).ToList();
                if (lstItem.Count > 1)
                {
                    var sortedListItem = lstItem.OrderByDescending(x => x.ItemArray[2]);
                    if (sortedListItem != null)
                    {
                        if (sortedListItem.LastOrDefault().ItemArray[1].ToString() == string.Empty)
                        {
                            dt3.ImportRow(sortedListItem.LastOrDefault());
                        }
                    }

                }
            }
            //DataTable dt2 = ListData.CopyToDataTable();

            return dt3;
        }

        public bool GenerateRpt(string pathFileRpt, string OrgFileName)
        {
            bool result = false;

            GenHTMLRpt htmlRpt = new GenHTMLRpt();
            // --> Start
            htmlRpt.StartRpt_1(pathFileRpt);

            //--> Header
            htmlRpt.GenRtpHeader_2(OrgFileName);

            //--> Summary Area
            htmlRpt.GenSummaryArea_3(); // Start Summary Area
            htmlRpt.GenSumQtyOfSentences_4(_Sentences.Length.ToString()); // Qty of Sentences

            // --> Summary Qty Acronyms 
            DataView dvSumAcronymsFound = getAcronymsFound();
            DataView dvSumAcronymsFoundForReport = getAcronymsFoundForReport();
            DataView dvAcronymsUniqueFound = getAcronymsUniqueFound(); // change 10.10.2020
            if (dvAcronymsUniqueFound != null)
            {
                int acronymsUniqueFound = dvAcronymsUniqueFound.Count;
                htmlRpt.GenSumAcronymsFound_5(acronymsUniqueFound.ToString());
            }
            else
            {
                htmlRpt.GenSumAcronymsFound_5("0");
            }

            // --> Summary Qty Defined Acronyms 
            DataView dvDefinedAcronymsFound = getDefinedAcronymsFound();
            if (dvDefinedAcronymsFound != null)
                htmlRpt.GenSumAcronymsDefined_6(dvDefinedAcronymsFound.Count.ToString());
            else
                htmlRpt.GenSumAcronymsDefined_6("0");

            // --> Summary Qty Not Defined Acronyms
            getNotDefinedAcronymsFound(); // set _NotDefinedDocAcronyms value // Added 9.12.2107
            htmlRpt.GenSumAcronymsNotDefined_7(_NotDefinedDocAcronyms.ToString());

            //
            getAcronymsDefsViaDic(); // set _AcronymsDefsViaDic value // Added 9.23.2017
            htmlRpt.GenSumAcronymsDefViaDic_7a(_AcronymsDefsViaDic.ToString(), _NotDefinedDocAcronyms);

            //--> Summary Qty Mult-Defined Acronyms
            getMultiDefinedAcronymFound(); // set _DuplicatesDefinedSame value // Added 9.12.2107
            htmlRpt.GenSumAcronymsMultiDefined_8(_DuplicatesDefinedSame.ToString());

            // --> Summary Qty Defined Diff Acronyms
            getMultiDiffDefinedAcronymFound(); // set _DuplicatesDefinedDiff value // Added 9.12.2107
            htmlRpt.GenSumAcronymsDefinedDiff_9(_DuplicatesDefinedDiff.ToString());

            //// --> Summary Qty Defined Diff Acronyms
            //getMultiDiffDefinedAcronymFound(); // set _DuplicatesDefinedDiff value // Added 9.12.2107
            //htmlRpt.GenSumAcronymsDefinedDiff_11(_DuplicatesDefinedDiff.ToString());

            //// --> Summary Qty Acronyms Use Before Definition
            var definedAllAcronymsFound = getDefinedAllAcronymsFound();
            var acroUsedBeforeDefinition = new DataTable();
            if (definedAllAcronymsFound != null)
            {

                acroUsedBeforeDefinition = AcronymsUseBeforeDefinition(definedAllAcronymsFound.ToTable(), _Sentences);//(dvSumAcronymsFoundForReport.ToTable());
                var acroUsedBeforeDefinitionDistnict = htmlRpt.RemoveDuplicatesRecordsGeneric(acroUsedBeforeDefinition);
                htmlRpt.GenHeadingForAcronymsUsedBeforeDefinition(acroUsedBeforeDefinitionDistnict.Rows.Count.ToString());
            }
            else
            {
                htmlRpt.GenHeadingForAcronymsUsedBeforeDefinition("0");
            }


            // --> End Summary Area
            htmlRpt.GenEndSumArea_10();

            //// --> Summary Qty Acronyms Use Before Definition




            //--> Details
            // --> Details Acronyms Found



            htmlRpt.GenAcronymsFoundDetails_10(dvSumAcronymsFound);

            // --> Details Acronyms Defined Found
            DataView dvDefinedAllAcronymsFound = getDefinedAllAcronymsFound();
            if (dvDefinedAllAcronymsFound != null)
                htmlRpt.GenAcronymsDefinedDetails_11(dvDefinedAllAcronymsFound);


            //  --> Details Acronyms Not Defined Found
            DataView dvNotDefinedAcronymsFound = getNotDefinedAcronymsFound();
            if (dvNotDefinedAcronymsFound != null)
                htmlRpt.GenAcronymsNotDefinedDetails2_12(dvNotDefinedAcronymsFound); // Changed in Beta 2
            //   htmlRpt.GenAcronymsNotDefinedDetails_12(dvNotDefinedAcronymsFound);

            // --> Details Acronyms Def.s via Dictionaries -- Added 9.23.2017
            DataView dvAcronymsDefsViaDic = getAcronymsDefsViaDic();
            if (dvAcronymsDefsViaDic != null)
                htmlRpt.GenAcronymsDefViaDic(dvAcronymsDefsViaDic);

            //  --> Details Multi Acronyms Defined Found
            DataView dvMultiDefinedAcronymFound = getMultiDefinedAcronymFound();
            if (dvMultiDefinedAcronymFound != null)
                htmlRpt.GenAcronymsMultiDetails_13(dvMultiDefinedAcronymFound);

            //  --> Details Multi Diff Acronyms Defined Found
            DataView dvMultiDiffDefinedAcronymFound = getMultiDiffDefinedAcronymFound();
            if (dvMultiDiffDefinedAcronymFound != null)
                htmlRpt.GenAcronymsDefinedDiffDetails_14(dvMultiDiffDefinedAcronymFound);
            if (acroUsedBeforeDefinition != null)
            {
                htmlRpt.GenAcronymsUsedBeforeDefinition(acroUsedBeforeDefinition.AsDataView());
            }


            // --> Add Footer and complete HTML Report
            htmlRpt.GenFooterEnd_15();

            return result;

        }


        private DataView getAcronymsUniqueFound() // Added 10.10.2020
        {

            DataTable dt = _dtAcronymsFound.DefaultView.ToTable(true, "Acronym");

            dt.DefaultView.Sort = AcronymsFoundFieldConst.Acronym;

            DataView dv = dt.DefaultView;

            return dv;
        }


        private DataView getAcronymsFound()
        {
            if (_dtAcronymsFound.Rows.Count == 0)
                return null;

            DataTable dt = _dtAcronymsFound.DefaultView.ToTable(true, AcronymsFoundFieldConst.Acronym, AcronymsFoundFieldConst.Definition);

            //  _dtAcronymsFound.DefaultView.Sort = AcronymsFoundFieldConst.Acronym; // Changed 9.6.2017
            dt.DefaultView.Sort = AcronymsFoundFieldConst.Acronym;
            //  DataView dv = _dtAcronymsFound.DefaultView; // Changed 9.6.2017
            DataView dv = dt.DefaultView;

            return dv;

        }
        private DataView getAcronymsFoundForReport()
        {
            if (_dtAcronymsFound.Rows.Count == 0)
                return null;

            DataTable dt = _dtAcronymsFound.DefaultView.ToTable(true, AcronymsFoundFieldConst.Acronym, AcronymsFoundFieldConst.Definition, AcronymsFoundFieldConst.SentenceNo, AcronymsFoundFieldConst.xSentence);

            //  _dtAcronymsFound.DefaultView.Sort = AcronymsFoundFieldConst.Acronym; // Changed 9.6.2017
            dt.DefaultView.Sort = AcronymsFoundFieldConst.Acronym;
            //  DataView dv = _dtAcronymsFound.DefaultView; // Changed 9.6.2017
            DataView dv = dt.DefaultView;

            return dv;

        }

        private DataView getDefinedAcronymsFound()
        {

            if (_dtAcronymsDocDefined.Rows.Count == 0)
                return null;

            DataTable dt = _dtAcronymsDocDefined.DefaultView.ToTable(true, AcronymsFoundFieldConst.Acronym, AcronymsFoundFieldConst.Definition);

            //_dtAcronymsDocDefined.DefaultView.Sort = AcronymsFoundFieldConst.Acronym; // 9.7.2017
            dt.DefaultView.Sort = AcronymsFoundFieldConst.Acronym;

            // DataView dv = _dtAcronymsDocDefined.DefaultView; // 9.7.2017
            DataView dv = dt.DefaultView;


            return dv;
        }

        private DataView getDefinedAllAcronymsFound()
        {
            if (_dtAcronymsDocDefined.Rows.Count == 0)
                return null;

            _dtAcronymsDocDefined.DefaultView.Sort = AcronymsFoundFieldConst.Acronym;
            DataView dv = _dtAcronymsDocDefined.DefaultView;

            return dv;
        }

        private int _NotDefinedDocAcronyms = 0;
        private DataView getNotDefinedAcronymsFound()
        {
            _NotDefinedDocAcronyms = _dtAcronymsNotDefined.Rows.Count;
            if (_NotDefinedDocAcronyms == 0)
                return null;

            _dtAcronymsNotDefined.DefaultView.Sort = AcronymsFoundFieldConst.Acronym;
            DataView dv = _dtAcronymsNotDefined.DefaultView;

            _NotDefinedDocAcronyms = dv.ToTable().Rows.Count; // 9.12.2017

            return dv;
        }

        // Added 9.23.2017
        private int _AcronymsDefsViaDic = 0;
        private DataView getAcronymsDefsViaDic()
        {
            _AcronymsDefsViaDic = _dtAcronymsDicDefined.Rows.Count;
            if (_AcronymsDefsViaDic == 0)
                return null;

            _dtAcronymsDicDefined.DefaultView.Sort = AcronymsFoundFieldConst.Acronym;
            DataView dv = _dtAcronymsDicDefined.DefaultView;

            //    _AcronymsDefsViaDic = dv.ToTable().Rows.Count; // 9.12.2017

            return dv;
        }


        private int _DuplicatesDefinedSame = 0;
        private DataView getMultiDefinedAcronymFound()
        {
            _DuplicatesDefinedSame = _dtAcronymsMultDefined.Rows.Count;
            if (_DuplicatesDefinedSame == 0)
                return null;

            _dtAcronymsMultDefined.DefaultView.Sort = AcronymsFoundFieldConst.Acronym;
            DataView dv = _dtAcronymsMultDefined.DefaultView;

            return dv;
        }

        private int _DuplicatesDefinedDiff = 0;
        private DataView getMultiDiffDefinedAcronymFound()
        {
            _DuplicatesDefinedDiff = _dtAcronymsDiffDefined.Rows.Count;
            if (_DuplicatesDefinedDiff == 0)
                return null;

            _dtAcronymsDiffDefined.DefaultView.Sort = AcronymsFoundFieldConst.Acronym;
            DataView dv = _dtAcronymsDiffDefined.DefaultView;

            return dv;
        }


        #endregion
    }
}
