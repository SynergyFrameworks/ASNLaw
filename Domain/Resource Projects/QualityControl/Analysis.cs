using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Drawing;

using Atebion.Common;



namespace Atebion.QC
{
    public class Analysis
    {
        public Analysis()
        {
             
        }

        public Analysis(string QCSettingsPath)
        {

            string pathQCSettingsFile = Path.Combine(QCSettingsPath, _QCSettingsFile);
            
            if (!File.Exists(pathQCSettingsFile))
                return;

            GenericDataManger gDMgr = new GenericDataManger();

            DataSet dsQCSettings = gDMgr.LoadDatasetFromXml(pathQCSettingsFile);

            if (dsQCSettings == null)
            {
                _ErrorMessage = gDMgr.ErrorMessage;
                return;
            }

            LoadSettings(QCSettingsPath);
        }

        private string _QCSettingsPath = string.Empty;
        private string _pathXML = string.Empty;
        private string _pathParseSec = string.Empty;
        private string _ModelPath = string.Empty;
        private string _ParsedFile = string.Empty;
        private string _UID = string.Empty;
        private string _DictionaryPathFile = string.Empty;

        //private const string _Quality = "Quality";
        //private const string _ComplexWordsQty = "ComplexWords";
        //private const string _LongQty = "Long";

        private string _DocParsedSecXML_Path = string.Empty;
        private string _DocParsedSec_Path = string.Empty;
        private string _DocParsedSecQC_Path = string.Empty;

        private RichTextBox _RTFcontrol = new RichTextBox();

        private string _QCSettingsFile = "QCSettings.qcx";
        private string _ParseResultsFile = "ParseResults.xml";
        private string _QCIssuesFile = "QCIssues.xml";
        private string _QCParseResultsFile = "QCParseResults.xml";
        private string _QCNoticeFile = "QCNotices.txt";
        private string _QCStatsFile = "QCStats.xml";
        private string _QCReadabilitySumFile = "QCReadabilitySum.xml";

        private DataSet _dsSettings;
        private DataSet _dsParseResults;
        private DataSet _dsQCParseResults;
        private DataSet _dsQCIssues;
        private DataSet _dsQCStats;
        private DataSet _dsQCReadablity;
        private DataSet _dsDictionary;

        private double _WeightTotal_Segment = 0;

        // NLP
        private OpenNLP.Tools.SentenceDetect.MaximumEntropySentenceDetector mSentenceDetector;
        private string[] _Sentences;
        private string[] _WordsInSentence;
        private string[] _WordsInSegment; // or words in paragraph, depending on the parse type


        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        // Total_Words
        private int _TotalFound_Words = 0;
        public int TotalFound_Words
        {
            get { return _TotalFound_Words; }
        }

        // Complex Words
        private bool _Find_ComplexWords = true;
        public bool Find_ComplexWords
        {
            get { return _Find_ComplexWords; }
            set { _Find_ComplexWords = value; }
        }

        private int _ComplexWords_SyllableCountGreaterThan = 3;
        public int ComplexWords_SyllableCountGreaterThan
        {
            get { return _ComplexWords_SyllableCountGreaterThan; }
            set { _ComplexWords_SyllableCountGreaterThan = value; }

        }

        private string _Color_ComplexWords = "PaleGreen";
        public string Color_ComplexWords
        {
            get { return _Color_ComplexWords; }
            set { _Color_ComplexWords = value; }
        }

        private int _TotalFound_ComplexWords = 0;
        public int TotalFound_ComplexWords
        {
            get { return _TotalFound_ComplexWords; }
        }

        private double _WeightItem_ComplexWords = .25;
        public double WeightItem_ComplexWords
        {
            get { return _WeightItem_ComplexWords; }
            set { _WeightItem_ComplexWords = value; }
        }

        private string _Importance_ComplexWords = "Not Important";
        public string Importance_ComplexWords
        {
            get { return _Importance_ComplexWords; }
            set { _Importance_ComplexWords = value; }
        }

        private double _WeightTotal_ComplexWords = 0;
        public double WeightTotal_ComplexWords
        {
            get { return _WeightTotal_ComplexWords; }
        }

        // Sentences
        private int _TotalFound_Sentences = 0;
        public int TotalFound_Sentences
        {
            get { return _TotalFound_Sentences; }
        }


        // Long Sentences
        private bool _Find_LongSentence = true;
        public bool Find_LongSentence
        {
            get { return _Find_LongSentence; }
            set { _Find_LongSentence = true; }
        }

        private string _Color_LongSentence = "Thistle";
        public string Color_LongSentence
        {
            get { return _Color_LongSentence; }
            set { _Color_LongSentence = value; }

        }

        private int _LongSentence_Def = 30;
        public int LongSentence_Def
        {
            get { return _LongSentence_Def; }
            set { _LongSentence_Def = value; }
        }

        private int _TotalFound_LongSentence = 0;
        public int TotalFound_LongSentence
        {
            get { return _TotalFound_LongSentence; }
        }

        private double _WeightItem_LongSentence = 1.0;
        public double WeightItem_LongSentence
        {
            get { return _WeightItem_LongSentence; }
            set { _WeightItem_LongSentence = value; }
        }

        private string _Importance_LongSentence = "Very Important";
        public string Importance_LongSentence
        {
            get { return _Importance_LongSentence; }
            set { _Importance_LongSentence = value; }
        }

        private double _WeightTotal_LongSentence = 0;
        public double WeightTotal_LongSentence
        {
            get { return _WeightTotal_LongSentence; }
        }



        // Adverbs
        private bool _Find_Adverbs = true;
        public bool Find_Adverbs
        {
            get { return _Find_Adverbs; }
            set { _Find_Adverbs = value; }
        }

        private string _Color_Adverbs = "PowderBlue";
        public string Color_Adverbs
        {
            get { return _Color_Adverbs; }
            set { _Color_Adverbs = value; }
        }

        private int _Adverbs_ShowGreaterThan = 0;
        public int Adverbs_ShowGreaterThan
        {
            get { return _Adverbs_ShowGreaterThan; }
            set { _Adverbs_ShowGreaterThan = value; }
        }

        private int _TotalFound_Adverbs = 0;
        public int TotalFound_Adverbs
        {
            get {return _TotalFound_Adverbs;}
        }

        private double _WeightItem_Adverbs = .25;
        public double WeightItem_Adverbs
        {
            get { return _WeightItem_Adverbs; }
            set {_WeightItem_Adverbs = value;}
        }

        private string _Importance_Adverbs = "Not Important";
        public string Importance_Adverbs
        {
            get { return _Importance_Adverbs; }
            set { _Importance_Adverbs = value; }
        }

        private double _WeightTotal_Adverbs = 0;
        public double WeightTotal_Adverbs
        {
            get { return _WeightTotal_Adverbs; }
        }


        // Passive Voice
        private bool _Find_PassiveVoice = true;
        public bool Find_PassiveVoice
        {
            get { return _Find_PassiveVoice; }
            set {_Find_PassiveVoice = value;}
        }

        private string _Color_PassiveVoice = "MistyRose"; //Color.Aqua;
        public string Color_PassiveVoice
        {
            get { return _Color_PassiveVoice; }
            set { _Color_PassiveVoice = value; }
        }

        private int _TotalFound_PassiveVoice = 0;
        public int TotalFound_PassiveVoice
        {
            get { return _TotalFound_PassiveVoice; } 
        }

        private double _WeightItem_PassiveVoice = .5;
        public double WeightItem_PassiveVoice
        {
            get { return _WeightItem_PassiveVoice; }
            set { _WeightItem_PassiveVoice = value; }
        }

        private string _Importance_PassiveVoice = "Somewhat Important";
        public string Importance_PassiveVoice
        {
            get { return _Importance_PassiveVoice; }
            set { _Importance_PassiveVoice = value; }
        }

        private double _WeightTotal_PassiveVoice = 0;
        public double WeightTotal_PassiveVoice
        {
            get { return _WeightTotal_PassiveVoice; }
        }


        // Dictionary Terms
        private bool _Find_DictionaryTerms = true;
        public bool Find_DictionaryTerms
        {
            get { return _Find_DictionaryTerms; }
            set { _Find_DictionaryTerms = value; }
        }

        private string _Color_DictionaryTerms = "LemonChiffon";
        public string Color_DictionaryTerms
        {
            get { return _Color_DictionaryTerms; }
            set {_Color_DictionaryTerms = value;}
        }

        private bool _Dictionary_UseTermsColors = false;
        public bool Dictionary_UseTermsColors
        {
            get { return _Dictionary_UseTermsColors; }
            set { _Dictionary_UseTermsColors = value; }
        }

        private int _TotalFound_DictionaryTerms = 0;
        public int TotalFound_DictionaryTerms
        {
            get { return _TotalFound_DictionaryTerms; }
        }

        private double _WeightTotal_DictionaryTerms = 0;
        public double WeightTotal_DictionaryTerms
        {
            get { return _WeightTotal_DictionaryTerms; }
        }





        private static class _QtySummary
        {
            public static int LongSentences = 0;
            public static int Sentences = 0;
            public static int Words = 0;
            public static int Segments = 0;

            public static int freVeryEasy = 0;
            public static int freEasy = 0;
            public static int freFairlyEasy = 0;
            public static int freStandard = 0;
            public static int freFairlyDifficult = 0;
            public static int freDifficult = 0;
            public static int freVeryConfusing = 0;

           // public static string LongColor = "pink";

        }

        public double GetImportanceValue(string Importance)
        {
            switch (Importance)
            {
                case "Not Important":
                    return .25;

                case "Somewhat Important":
                    return .50;

                case "Important":
                    return .75;

                case "Very Important":
                    return 1.00;

            }

            return .50;
        }


        public bool SaveSettings(string pathQCSettings)
        {
            bool isNew = false;

            DataRow row;
            if (_dsSettings == null)
            {
                CreateSettingsDataSet();

                row = _dsSettings.Tables[0].NewRow();
                isNew = true;
            }
            else
            {
                if (_dsSettings.Tables[0].Rows.Count == 0)
                {
                    row = _dsSettings.Tables[0].NewRow();
                    isNew = true;
                }
                else
                    row = _dsSettings.Tables[0].Rows[0];
            }


            row[SettingsFields.A_Color] = _Color_Adverbs;
            row[SettingsFields.A_Importance] = _Importance_Adverbs;
            row[SettingsFields.CW_Color] = _Color_ComplexWords;
            row[SettingsFields.CW_Importance] = _Importance_ComplexWords;
            row[SettingsFields.DT_Color] = _Color_DictionaryTerms;
            //if (_DictionaryPathFile.Length > 0)
            //{
            //    string dictionaryName = Files.GetFileNameWOExt(_DictionaryPathFile);
            //    row[SettingsFields.DT_Dictionary] = dictionaryName;
            //}
            row[SettingsFields.LS_Color] = _Color_LongSentence;
            row[SettingsFields.LS_Importance] = _Importance_LongSentence;
            row[SettingsFields.LS_WordsGreaterThan] = _LongSentence_Def;
            row[SettingsFields.PV_Color] = _Color_PassiveVoice;
            row[SettingsFields.PV_Importance] = _Importance_PassiveVoice;

            if (isNew)
            {
                _dsSettings.Tables[0].Rows.Add(row);
            }


            string pathSettingsFile = Path.Combine(pathQCSettings, _QCSettingsFile);
            GenericDataManger gDMgr = new GenericDataManger();
            gDMgr.SaveDataXML(_dsSettings, pathSettingsFile);

            if (File.Exists(pathSettingsFile))
            {
                return true;
            }

            return false;

        }


        public bool LoadSettings(string pathQCSettingsPath)
        {
            string pathSettingsFile = Path.Combine(pathQCSettingsPath, _QCSettingsFile);

            GenericDataManger gDMgr = new GenericDataManger();

            DataSet dsQCSettings = gDMgr.LoadDatasetFromXml(pathSettingsFile);

           

            if (dsQCSettings == null)
            {
                _ErrorMessage = gDMgr.ErrorMessage;
                return false;
            }

            if (dsQCSettings.Tables.Count == 0)
                return false;

            if (dsQCSettings.Tables[0].Rows.Count == 0)
                return false;

            DataRow row = dsQCSettings.Tables[0].Rows[0];
            
            // Adverbs
            _Color_Adverbs = row[SettingsFields.A_Color].ToString();
            _Importance_Adverbs = row[SettingsFields.A_Importance].ToString();
            _WeightItem_Adverbs = GetImportanceValue(_Importance_Adverbs);
            
            
            // Complex Words        
            _Color_ComplexWords = row[SettingsFields.CW_Color].ToString();
            _Importance_ComplexWords = row[SettingsFields.CW_Importance].ToString();
            _WeightItem_ComplexWords = GetImportanceValue(_Importance_ComplexWords);

            // Dictionary Terms
            _Color_DictionaryTerms = row[SettingsFields.DT_Color].ToString();
            //string dictionaryName = row[SettingsFields.DT_Dictionary].ToString();
            //if (dictionaryName.Length > 0)
            //{
            //    string dicFile = string.Concat(dictionaryName, ".dicx");
            //    _DictionaryPathFile = Path.Combine(DictionaryPath, dicFile);
            //    if (!File.Exists(_DictionaryPathFile))
            //    {
            //        _DictionaryPathFile = string.Empty;
            //    }
            //}
            //else
            //{
            //    _DictionaryPathFile = string.Empty;
            //}

            // Long Sentence
            _Color_LongSentence = row[SettingsFields.LS_Color].ToString();
            _Importance_LongSentence = row[SettingsFields.LS_Importance].ToString();
            _WeightItem_LongSentence = GetImportanceValue(_Importance_LongSentence);
            _LongSentence_Def = Convert.ToInt32(row[SettingsFields.LS_WordsGreaterThan].ToString());

            // Passive Voice
            _Color_PassiveVoice = row[SettingsFields.PV_Color].ToString();
            _Importance_PassiveVoice = row[SettingsFields.PV_Importance].ToString();
            _WeightItem_PassiveVoice = GetImportanceValue(_Importance_PassiveVoice);

            

            return true;
        } 


        public bool AnalyzeDocs(string DocParsedSecXML_Path, string DocParsedSec_Path, string DocParsedSecQC_Path, string NLPEnglishModel_Path, string DictionaryPathFile)
        {
            _ErrorMessage = string.Empty;
            string msg = string.Empty;

            _DictionaryPathFile = DictionaryPathFile; // if parameter DictionaryPathFile = string.empty, don't try to find dictionary terms

            _DocParsedSecXML_Path = DocParsedSecXML_Path;
            if (!Directory.Exists(_DocParsedSecXML_Path))
            {
                _ErrorMessage = string.Concat("Unable to find Document Analysis Results Segment Folder: ", _DocParsedSecXML_Path);
                return false;
            }

            _DocParsedSec_Path = DocParsedSec_Path;
            if (!Directory.Exists(_DocParsedSec_Path))
            {
                _ErrorMessage = string.Concat("Unable to find Analysis Results XML Folder: ", _DocParsedSec_Path);
                return false;
            }

            _DocParsedSecQC_Path = DocParsedSecQC_Path;
            if (!Directory.Exists(_DocParsedSecQC_Path))
            {
                _ErrorMessage = string.Concat("Unable to find Analysis Results QC Folder: ", _DocParsedSecQC_Path);
                return false;
            }

            _ModelPath = NLPEnglishModel_Path;
            if (!Directory.Exists(_ModelPath))
            {
                _ErrorMessage = string.Concat("Unable to find NLP English Model Folder: ", _ModelPath);
                return false;
            }


         //   _RTFcontrol = new RichTextBox5(); 
            
            

            string docParsedSecPathFile = Path.Combine(_DocParsedSecXML_Path,_ParseResultsFile);
            if (!File.Exists(docParsedSecPathFile))
            {
                _ErrorMessage = string.Concat("Unable to find Analysis Results XML File: ", _DocParsedSecXML_Path);
                return false;

            }

            GenericDataManger gDataMgr = new GenericDataManger();
            _dsParseResults = gDataMgr.LoadDatasetFromXml(docParsedSecPathFile);
            if (_dsParseResults == null)
            {
                _ErrorMessage = string.Concat("Unable to open the Analysis Results Segments XML File: ", docParsedSecPathFile);
                return false;
            }

            if (!CreateQCParseSegDataSet())
            {
                return false; // Should Never Occur
            }

            if (!CreateQCStatsDataSet())
            {
                return false; // Should Never Occur
            }

            CreateQCReadablitySumDataSet();

           // string docParsedSecXMLPathFile = Path.Combine(_DocParsedSecXML_Path,_dsQCIssuesFile);

            DataTable dtQCIssues = CreateTable_FoundIssues();

            _dsQCIssues = new DataSet();
            _dsQCIssues.Tables.Add(dtQCIssues);


            // Set Defaults
            _TotalFound_Adverbs = 0;
            _TotalFound_ComplexWords = 0;
            _TotalFound_DictionaryTerms = 0;
            _TotalFound_LongSentence = 0;
            _TotalFound_PassiveVoice = 0;


            ReadAnalyze();
            

            return true;
        }

        private bool ReadAnalyze()
        {
            _ErrorMessage = string.Empty;
            int AnalyzeErrors = 0;

            ResetSumValues();


            string msg = string.Empty;
            StringBuilder sbNotices = new StringBuilder();

            int txtLenght = 0;
            string text2Review = string.Empty;
            double rfeResult = 0;
            int complexWordsQty = 0;
            DataRow rowQC;
            int qcUID = 0;
            string complexWordsText = string.Empty;
            int longSentencesQty = 0;

            int wordCount = 0;
            int sentenceCount = 0;

            string[] passivePhrases;
            string passiveText = string.Empty;

            string[] adverbs;
            string adverbText = string.Empty;

            string dicTerms = string.Empty;
            int dicQtyTermsFound = 0;

            double readablityWeightAdj = 0;
            string readablityGradeLevel = string.Empty;
            string readablityColor = string.Empty;
            string readablityLevel = string.Empty;

            const string EASY_WORDS = "Use short, easy words. The more syllables your words have, the harder it is to read. Don’t use 3 or 4 syllable words if a 2-syllable word works just as well.";
            const string PASSIVE_VOICE = "Passive Voice typically creates unclear, less direct, and wordy sentences. Whereas, active voice is clearer and more concise.";
            const string ADVERB = "Only use an adverb if it’s necessary, where you can’t convey the same meaning without it.";

            // NLP
            string fileEnglishSD = Path.Combine(_ModelPath, "EnglishSD.nbin");
            string fileEnglishPOS = Path.Combine(_ModelPath, "EnglishPOS.nbin");
            string fileEnglishTok = Path.Combine(_ModelPath, "EnglishTok.nbin");
            string modelParserPathFile = Path.Combine(_ModelPath, "Parser", "tagdict");

            if (!File.Exists(fileEnglishSD))
            {
                _ErrorMessage = string.Concat("Unable to find NLP English SD file: ", fileEnglishSD);
                return false;
            }

            if (!File.Exists(fileEnglishPOS))
            {
                _ErrorMessage = string.Concat("Unable to find NLP English Pos file: ", fileEnglishPOS);
                return false;
            }

            OpenNLP.Tools.Tokenize.EnglishMaximumEntropyTokenizer tokenizer = new OpenNLP.Tools.Tokenize.EnglishMaximumEntropyTokenizer(fileEnglishTok);

            OpenNLP.Tools.PosTagger.EnglishMaximumEntropyPosTagger tagger = new OpenNLP.Tools.PosTagger.EnglishMaximumEntropyPosTagger(fileEnglishPOS, modelParserPathFile);
            
            // -- End NLP


            foreach (DataRow row in _dsQCParseResults.Tables[0].Rows) // Loop over the rows.
            {
               // _QtySummary.Segments++;

                _WeightTotal_Segment = 0; // Reset to Defualt;

                string uid = row["UID"].ToString();
                string segFile = string.Concat(uid, ".rtf");
                string segPathFile = Path.Combine(_DocParsedSec_Path, segFile);

                if (!File.Exists(segPathFile))
                {
                    msg = string.Concat("Unable to find parse segment file: ", segPathFile);
                    sbNotices.AppendLine(msg);
                    AnalyzeErrors++;
                }
                else
                {
                    _RTFcontrol.LoadFile(segPathFile);
                    txtLenght = _RTFcontrol.Text.Length;
                    text2Review = _RTFcontrol.Text;

                    _Sentences = SplitSentences(text2Review);
                    _WordsInSegment = tokenizer.Tokenize(text2Review);

                    string[] complexWords;
                    string[] complexWordsUnique;

                    if (txtLenght < 50) // Get better scoring with larger text lenght
                    {
                        text2Review = string.Concat(text2Review, Environment.NewLine, text2Review, Environment.NewLine, text2Review, Environment.NewLine, text2Review);
                        rfeResult = FleschReadingEase(text2Review, ReadabilityType.Flesch_Reading_Ease, out complexWords, out wordCount, out sentenceCount); // use concatenation for Readblitiy Score 
                        text2Review = _RTFcontrol.Text;
                        FleschReadingEase(text2Review, ReadabilityType.Flesch_Reading_Ease, out complexWords, out wordCount, out sentenceCount); // use non-concatenation for getting complex words
                    }
                    else
                    {
                        rfeResult = FleschReadingEase(text2Review, ReadabilityType.Flesch_Reading_Ease, out complexWords, out wordCount, out sentenceCount);
                    }

                    sentenceCount = _Sentences.Length; // Trust sentence count more from NLP

                    row[QCParseResultsFields.Readability] = rfeResult; // enter Readablitiy into QC Analysis Results table

                    readablityLevel = GetReadablilityLevel(rfeResult, out readablityGradeLevel, out readablityColor); 

                    rowQC = _dsQCIssues.Tables[0].NewRow();
                    rowQC[IssueFields.UID] = qcUID;
                    rowQC[IssueFields.ParseSeg_UID] = uid;
                    rowQC[IssueFields.IssueQty] = Math.Round(rfeResult).ToString();
                    rowQC[IssueFields.IssueCat] = IssueCategory.Readability;
                    rowQC[IssueFields.IssueColor] = readablityColor;
                    rowQC[IssueFields.Weight] = Math.Round(rfeResult) * .01; ;
                    rowQC[IssueFields.Issue] = string.Concat(Math.Round(rfeResult, 2).ToString(), "  Readability", 
                        Environment.NewLine, Environment.NewLine, readablityLevel, 
                        Environment.NewLine, Environment.NewLine, readablityGradeLevel,
                        Environment.NewLine, Environment.NewLine, "The Flesch–Kincaid Grade Level is a readability test designed to indicate comprehension difficulty when reading a passage of contemporary academic English.");

                    _dsQCIssues.Tables[0].Rows.Add(rowQC);

                    qcUID++;

                    SetFleschReadingEaseSegValue(rfeResult);

                  //  row[_Quality] = rfeResult;

                    // Get Words count
                    if (wordCount == 0)
                    {
                        wordCount = GetWordCount2(text2Review);
                    }
                    _TotalFound_Words = _TotalFound_Words + wordCount;
                    row[QCParseResultsFields.Words] = wordCount;

                    // Sentence Count
                    _TotalFound_Sentences = _TotalFound_Sentences + sentenceCount;
                    row[QCParseResultsFields.Sentences] = sentenceCount;

                    // Long Sentences
                    if (_Find_LongSentence)
                    {
                        // longSentencesQty = FindlongSentences(uid, qcUID, _LongSentence_Def, _Color_LongSentence);
                        longSentencesQty = FindlongSentences3(uid, qcUID, _LongSentence_Def, _Color_LongSentence, tokenizer);
                        if (longSentencesQty > 0)
                        {

                            _TotalFound_LongSentence = _TotalFound_LongSentence + longSentencesQty;

                            _WeightTotal_Segment = _WeightTotal_Segment + (longSentencesQty * _WeightItem_LongSentence);

                            row[QCParseResultsFields.LongSentences] = longSentencesQty;

                            qcUID++;

                        }
                    }

                    // Complex Words
                    if (_Find_ComplexWords)
                    {
                        complexWordsQty = complexWords.Length;
                        if (complexWordsQty > 0)
                        {
                            // Create New Issue - Complex Words
                            //row[_ComplexWordsQty] = complexWordsQty.ToString();
                            complexWordsUnique = complexWords.Distinct().ToArray();

                            rowQC = _dsQCIssues.Tables[0].NewRow();
                            rowQC[IssueFields.UID] = qcUID;
                            rowQC[IssueFields.ParseSeg_UID] = uid;
                            rowQC[IssueFields.IssueQty] = complexWordsQty.ToString();
                            rowQC[IssueFields.IssueCat] = IssueCategory.Long_Word;
                            rowQC[IssueFields.IssueColor] = _Color_ComplexWords;
                            rowQC[IssueFields.Weight] = complexWordsQty * _WeightItem_ComplexWords;


                            //if (complexWordsQty > 1)
                            //{
                                complexWordsText = FormatArray2CommaString(complexWords);
                                complexWordsText = string.Concat(complexWordsQty.ToString(), "  Complex Words ", Environment.NewLine, Environment.NewLine, complexWordsText, Environment.NewLine, Environment.NewLine, EASY_WORDS, Environment.NewLine, Environment.NewLine);
                            //}
                            //else
                            //{
                            //    complexWordsText = string.Concat("Complex Word  ", Environment.NewLine, Environment.NewLine, complexWords[0], Environment.NewLine, Environment.NewLine, EASY_WORDS, Environment.NewLine, Environment.NewLine);
                            //}

                            rowQC[IssueFields.Issue] = complexWordsText;

                            _dsQCIssues.Tables[0].Rows.Add(rowQC);

                            _TotalFound_ComplexWords = _TotalFound_ComplexWords + complexWordsQty;

                           _WeightTotal_Segment = _WeightTotal_Segment + (complexWordsQty * _WeightItem_ComplexWords);

                           row[QCParseResultsFields.ComplexWords] = complexWordsQty;


                           foreach (string cword in complexWords)
                           {
                               //_RTFcontrol.Find(cword, RichTextBoxFinds.WholeWord);

                               //_RTFcontrol.SelectionBackColor = Color.FromName(_Color_ComplexWords);

                               HighlightText2(cword, true, _Color_ComplexWords, false);
                           }

                            qcUID++;
                        }
                        //else
                        //{
                        //    row[QCParseResultsFields.ComplexWords] = 0;
                        //}
                    }


                    // Long Sentences -- Move to above
                    //if (_Find_LongSentence)
                    //{
                    //    longSentencesQty = FindlongSentences(uid, qcUID, _LongSentence_Def, _Color_LongSentence);
                    //    if (longSentencesQty > 0)
                    //    {

                    //        _TotalFound_LongSentence = _TotalFound_LongSentence + longSentencesQty;

                    //        _WeightTotal_Segment = _WeightTotal_Segment + (longSentencesQty * _WeightItem_LongSentence);

                    //        row[QCParseResultsFields.LongSentences] = longSentencesQty;

                    //       qcUID++;

                    //    }
                    //}


                    // HighLight complex Words
                    //foreach (string complexword in complexWords)
                    //{
                    //    HighlightText2(complexword, true, _Color_ComplexWords, false);
                    //}
                    //if (_Find_ComplexWords)
                    //{
                    //    if (complexWordsQty > 0)
                    //    {
                    //        foreach (string cword in complexWords)
                    //        {
                    //            //_RTFcontrol.Find(cword, RichTextBoxFinds.WholeWord);

                    //            //_RTFcontrol.SelectionBackColor = Color.FromName(_Color_ComplexWords);

                    //            HighlightText2(cword, true, _Color_ComplexWords, false);
                    //        }
                    //    }
                    //}

                    // Passive Voice
                    if (_Find_PassiveVoice)
                    {
                        passivePhrases = FindPassivePhrases(text2Review);
                        if (passivePhrases.Length > 0)
                        {
                            passiveText = FormatArray2CommaString(passivePhrases);
                            passiveText = string.Concat(passivePhrases.Length.ToString(), "  Passive Voices ", Environment.NewLine, Environment.NewLine, passiveText, Environment.NewLine, Environment.NewLine, PASSIVE_VOICE, Environment.NewLine, Environment.NewLine);


                            rowQC = _dsQCIssues.Tables[0].NewRow();
                            rowQC[IssueFields.UID] = qcUID;
                            rowQC[IssueFields.ParseSeg_UID] = uid;
                            rowQC[IssueFields.IssueQty] = passivePhrases.Length.ToString();
                            rowQC[IssueFields.IssueCat] = IssueCategory.Passive_Voice;
                            rowQC[IssueFields.IssueColor] = _Color_PassiveVoice;
                            rowQC[IssueFields.Issue] = passiveText;

                            _dsQCIssues.Tables[0].Rows.Add(rowQC);

                            foreach (string ptext in passivePhrases)
                            {
                                //_RTFcontrol.Find(ptext, RichTextBoxFinds.WholeWord);

                                //_RTFcontrol.SelectionBackColor =  Color.FromName(_Color_PassiveVoice);

                                HighlightText2(ptext, true, _Color_PassiveVoice, false);
                            }

                            _TotalFound_PassiveVoice = _TotalFound_PassiveVoice + passivePhrases.Length;

                            _WeightTotal_Segment = _WeightTotal_Segment + (passivePhrases.Length * _WeightItem_PassiveVoice);

                            row[QCParseResultsFields.PassiveVoice] = passivePhrases.Length;

                            qcUID++;
                        }
                    }

                    // Adverbs
                    if (_Find_Adverbs)                    
                    {
                        adverbs = FindAdverbs(text2Review);
                        if (adverbs.Length > _Adverbs_ShowGreaterThan)
                        {
                            adverbText = FormatArray2CommaString(adverbs);
                            adverbText = string.Concat(adverbs.Length.ToString(), "  Adverbs  ", Environment.NewLine, Environment.NewLine, adverbText, Environment.NewLine, Environment.NewLine, ADVERB, Environment.NewLine, Environment.NewLine);


                            rowQC = _dsQCIssues.Tables[0].NewRow();
                            rowQC[IssueFields.UID] = qcUID;
                            rowQC[IssueFields.ParseSeg_UID] = uid;
                            rowQC[IssueFields.IssueQty] = adverbs.Length.ToString();
                            rowQC[IssueFields.IssueCat] = IssueCategory.Adverb;
                            rowQC[IssueFields.IssueColor] = _Color_Adverbs;
                            rowQC[IssueFields.Issue] = adverbText;

                            _dsQCIssues.Tables[0].Rows.Add(rowQC);

                            qcUID++;

                            _TotalFound_Adverbs = _TotalFound_Adverbs + adverbs.Length;

                            _WeightTotal_Segment = _WeightTotal_Segment + (adverbs.Length * _WeightItem_Adverbs);

                            row[QCParseResultsFields.Adverbs] = adverbs.Length;

                            foreach (string adverb in adverbs)
                            {
                                //_RTFcontrol.Find(adverb, RichTextBoxFinds.WholeWord);

                                //_RTFcontrol.SelectionBackColor = Color.FromName(_Color_Adverbs);

                                HighlightText2(adverb, true, _Color_Adverbs, false);
                            }

                        }
                    }

                    if (_DictionaryPathFile.Length != 0)
                    {
                        dicQtyTermsFound = 0;
                        dicTerms = string.Empty;

                        dicQtyTermsFound = FindDictionaryTerms(uid, qcUID, out dicQtyTermsFound);

                        if (dicQtyTermsFound > 0)
                        {
                            row[QCParseResultsFields.DictionaryTerms] = dicQtyTermsFound;
                            _TotalFound_DictionaryTerms = _TotalFound_DictionaryTerms + dicQtyTermsFound;
                            qcUID++;
                        }

                    }

                }

                if (rfeResult > 0 && rfeResult < 100)
                {
                    readablityWeightAdj = (((100 - rfeResult) * .01) * 4);
                }
                else
                {
                    readablityWeightAdj = 0;
                }

                _WeightTotal_Segment = _WeightTotal_Segment + readablityWeightAdj; // Adjust for Readablity
                _WeightTotal_Segment = Math.Round(_WeightTotal_Segment, 2); // Round to the 2nd decimal place

                row[QCParseResultsFields.Weight] = _WeightTotal_Segment;
                row[QCParseResultsFields.Rank] = GetWeightRank(_WeightTotal_Segment);
                

                _RTFcontrol.SaveFile(segPathFile, RichTextBoxStreamType.RichText);
            }

            // Save Data

            GenericDataManger gDataMgr = new GenericDataManger();

            DataRow rowReadabilitySum = _dsQCReadablity.Tables[0].NewRow();
            rowReadabilitySum[ReadabilitySumFields.Difficult] = _QtySummary.freDifficult;
            rowReadabilitySum[ReadabilitySumFields.Easy] = _QtySummary.freEasy;
            rowReadabilitySum[ReadabilitySumFields.FairlyDifficult] = _QtySummary.freFairlyDifficult;
            rowReadabilitySum[ReadabilitySumFields.FairlyEasy] = _QtySummary.freFairlyEasy;
            rowReadabilitySum[ReadabilitySumFields.Standard] = _QtySummary.freStandard;
            rowReadabilitySum[ReadabilitySumFields.VeryConfusing] = _QtySummary.freVeryConfusing;
            rowReadabilitySum[ReadabilitySumFields.VeryEasy] = _QtySummary.freVeryEasy;

            _dsQCReadablity.Tables[0].Rows.Add(rowReadabilitySum);

            string QCReadabilitySumPathFile = Path.Combine(_DocParsedSecXML_Path, _QCReadabilitySumFile);
            gDataMgr.SaveDataXML(_dsQCReadablity, QCReadabilitySumPathFile);
            Application.DoEvents();


            if (AnalyzeErrors > 0)
            {
                string noticesPathFile = string.Concat(_DocParsedSecXML_Path, _QCNoticeFile);
                Files.WriteStringToFile(noticesPathFile, sbNotices.ToString());
            }

            
                // Save QC Analysis Results Data
            string QCParseResultsPathFile = Path.Combine(_DocParsedSecXML_Path, _QCParseResultsFile);
            gDataMgr.SaveDataXML(_dsQCParseResults, QCParseResultsPathFile);
            Application.DoEvents();
                // Save QC Found Issue
            string QCIssuesPathFile = Path.Combine(_DocParsedSecXML_Path, _QCIssuesFile);
            gDataMgr.SaveDataXML(_dsQCIssues, QCIssuesPathFile);
            Application.DoEvents();

                // Save QC Stats
            DataRow rowStats = _dsQCStats.Tables[0].NewRow();
            rowStats[QCStatsFields.TotalAdverbs] = _TotalFound_Adverbs;
            rowStats[QCStatsFields.TotalComplexWords] = _TotalFound_ComplexWords;
            rowStats[QCStatsFields.TotalDictionaryTerms] = _TotalFound_DictionaryTerms;
            rowStats[QCStatsFields.TotalLongSentence] = _TotalFound_LongSentence;
            rowStats[QCStatsFields.TotalPassiveVoice] = _TotalFound_PassiveVoice;
            rowStats[QCStatsFields.TotalSentence] = _TotalFound_Sentences;
            rowStats[QCStatsFields.TotalWords] = _TotalFound_Words;
            _dsQCStats.Tables[0].Rows.Add(rowStats);

            // Save QC Stats
            string QCStatsPathFile = Path.Combine(_DocParsedSecXML_Path, _QCStatsFile);
            gDataMgr.SaveDataXML(_dsQCStats, QCStatsPathFile);
            Application.DoEvents();


            return true;
        }

        public string GetWeightRank(double dblWeight)
        {
            string rank = "F";

            if (dblWeight < 2.00)
            {
                rank = "A";
            }
            else if (dblWeight > 1.99 && dblWeight < 3.00)
            {
                rank = "B";
            }
            else if (dblWeight > 2.99 && dblWeight < 4.00)
            {
                rank = "C";
            }
            else if (dblWeight > 3.99 && dblWeight < 5.00)
            {
                rank = "D";
            }
            else if (dblWeight > 4.99)
            {
                rank = "F";
            }

            return rank;

        }

        public string GetReadablilityLevel(double readability, out string Gradelevel, out string color)
        {
            color = string.Empty;
            Gradelevel = string.Empty;

            if (readability < 30)
            {
                color = "Red";
                Gradelevel = "College Graduate";
                return "Very Confusing";
            }
            else if (readability < 50 && readability >= 30)
            {
                color = "Salmon";
                Gradelevel = "College";
                return "Difficult";
            }
            else if (readability < 60 && readability >= 50)
            {
                color = "Yellow"; //Gold
                Gradelevel = "10th to 12th Grade";
                return "Fairly Difficult";
            }
            else if (readability < 70 && readability >= 60)
            {
                color = "GreenYellow";
                Gradelevel = "8th & 9th Grade";
                return "Standard";
            }
            else if (readability < 80 && readability >= 70)
            {
                color = "LightGreen";
                Gradelevel = "7th Grade";
                return "Fairly Easy";
            }
            else if (readability < 90 && readability >= 79.99)
            {
                color = "Green";
                Gradelevel = "6th Grade";
                return "Easy";
            }
            else if (readability < 90 && readability >= 100)
            {
                color = "DarkGreen";
                Gradelevel = "5th Grade";
                return "Very Easy";
            }
            else
            {
                color = "DarkGreen";
                Gradelevel = "5th Grade";
                return "Very Easy";
            }

            return string.Empty;

        }


        private int FindlongSentences3(string UID, int qcUID, int LongDef, string color, OpenNLP.Tools.Tokenize.EnglishMaximumEntropyTokenizer tokenizer)
        {
            int QtyLong = 0;
            StringBuilder sb = new StringBuilder();

            string[] words;
            int wordCount = 0;

            List<string> lstLSWordCount = new List<string>();

            int found = 0;
            string substr = string.Empty;
            string text = string.Empty;
            // int index = -1;
            string adjsentence = string.Empty;

            foreach (string sentence in _Sentences)   
            {
                if (!sentence.Trim().EndsWith(":"))
                {
                    if (sentence.IndexOf('\n') != -1)
                    {
                       // int segLength = 0;
                        string[] breaks = sentence.Split('\n');
                        foreach (string segment in breaks)
                        {
                            words = tokenizer.Tokenize(segment);
                            wordCount = words.Length;
                            if (wordCount > LongDef)
                            {
                                lstLSWordCount.Add(wordCount.ToString());

                                found = _RTFcontrol.Find(segment.Trim());
                                if (found != 0)
                                {
                                    _RTFcontrol.SelectionBackColor = Color.FromName(color);
                                }
                                else
                                {
                                    found = HighlightText2(segment.Trim(), true, color, false);
                                }

                                if (found > 0)
                                {
                                    sb.AppendLine(segment + Environment.NewLine + Environment.NewLine);
                                    QtyLong++;
                                }

                            }
                        }
                    }
                    else
                    {
                        words = tokenizer.Tokenize(sentence);
                        wordCount = words.Length;
                        if (wordCount > LongDef)
                        {
                            lstLSWordCount.Add(wordCount.ToString());

                            found = _RTFcontrol.Find(sentence.Trim());
                            if (found != 0)
                            {
                                _RTFcontrol.SelectionBackColor = Color.FromName(color);
                            }
                            else
                            {
                                found = HighlightText2(sentence.Trim(), true, color, false);
                            }

                            if (found > 0)
                            {
                                sb.AppendLine(sentence + Environment.NewLine + Environment.NewLine);
                                QtyLong++;
                            }

                        }
                    }
                }
            }
            

            if (QtyLong > 0)
            {
                string file = string.Concat(UID, ".LQC");
                string pathFile = Path.Combine(_DocParsedSecQC_Path, file);
                Files.WriteStringToFile(sb.ToString(), pathFile);

                string wordCountsPerLS = string.Empty;

                int i = 1;
                foreach (string lsLength in lstLSWordCount)
                {
                    if (QtyLong == 1)
                    {
                        wordCountsPerLS = string.Concat("Words: ", lsLength);
                    }
                    else
                    {
                        if (i == 1)
                        {
                            wordCountsPerLS = string.Concat("1.  Words: ", lsLength);
                        }
                        else
                        {
                            wordCountsPerLS = string.Concat(wordCountsPerLS, Environment.NewLine, i.ToString(), ".  Words: ", lsLength);
                        }
                    }

                    i++;
                }

                qcUID++;

                DataRow rowQC = _dsQCIssues.Tables[0].NewRow();
                rowQC[IssueFields.UID] = qcUID;
                rowQC[IssueFields.ParseSeg_UID] = UID;
                rowQC[IssueFields.IssueQty] = QtyLong.ToString();
                rowQC[IssueFields.IssueCat] = IssueCategory.Long_Sentence;
                rowQC[IssueFields.IssueColor] = color;

                string issueMsg = string.Concat(QtyLong.ToString(), "  Long Sentences (> ", LongDef.ToString(), " Words)", Environment.NewLine, Environment.NewLine, wordCountsPerLS, Environment.NewLine, Environment.NewLine, "Shorten your sentences. Compound and convoluted sentences decrease readability. Keep your sentences short.", Environment.NewLine, Environment.NewLine, "Try breaking your long sentences up into short sentences.");

                rowQC[IssueFields.Issue] = issueMsg;

                rowQC[IssueFields.Flag] = false;

                _dsQCIssues.Tables[0].Rows.Add(rowQC);

            }

            _QtySummary.LongSentences = _QtySummary.LongSentences + QtyLong;
            return QtyLong;

        }

        private int FindlongSentences2(string UID, int qcUID, int LongDef, string color, OpenNLP.Tools.Tokenize.EnglishMaximumEntropyTokenizer tokenizer)
        {
            int QtyLong = 0;
            StringBuilder sb = new StringBuilder();

            string[] words;
            int wordCount = 0;

            List<string> lstLSWordCount = new List<string>();

            int found = 0;
            string substr = string.Empty;
            string text = string.Empty;
           // int index = -1;
            string adjsentence = string.Empty;

            string[] newlines = _RTFcontrol.Text.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string newline in newlines)
            {
                string[] sentences = Regex.Split(newline, @"(?<=[\.!\?])\s+", RegexOptions.Singleline);

               // foreach (string sentence in _Sentences)
                foreach (string sentence in sentences)
                {
                    if (!sentence.Trim().EndsWith(":"))
                    {
                        words = tokenizer.Tokenize(sentence);
                        wordCount = words.Length;
                        if (wordCount > LongDef)
                        {
                            

                            lstLSWordCount.Add(wordCount.ToString());

                            found = _RTFcontrol.Find(sentence.Trim());
                            //if (found != 0)
                            //{
                            _RTFcontrol.SelectionBackColor = Color.FromName(color);
                            //}
                            //else
                            //{
                            //    if (sentence.Length > 100)
                            //    {
                            //        substr = sentence.Substring(0, 100);
                            //        text = _RTFcontrol.Text;
                            //        index = text.IndexOf(substr);
                            //        _RTFcontrol.Select(index, 100);
                            //        _RTFcontrol.Select(index + 100, sentence.Length - 100);
                            //        //adjsentence = text.Substring(index, sentence.Trim().Length);
                            //        //_RTFcontrol.Find(adjsentence);
                            //        _RTFcontrol.SelectionBackColor = Color.FromName(color);
                            //    }
                            //}

                            // HighlightText2(sentence, true, color, false);

                            sb.AppendLine(sentence + Environment.NewLine + Environment.NewLine);
                            QtyLong++;

                        }
                    }
                }
            }


            if (QtyLong > 0)
            {
                string file = string.Concat(UID, ".LQC");
                string pathFile = Path.Combine(_DocParsedSecQC_Path, file);
                Files.WriteStringToFile(sb.ToString(), pathFile);

                string wordCountsPerLS = string.Empty;

                int i = 1;
                foreach (string lsLength in lstLSWordCount)
                {
                    if (QtyLong == 1)
                    {
                        wordCountsPerLS = string.Concat("Words: ", lsLength);
                    }
                    else
                    {
                        if (i == 1)
                        {
                            wordCountsPerLS = string.Concat("1.  Words: ", lsLength);
                        }
                        else
                        {
                            wordCountsPerLS = string.Concat(wordCountsPerLS, Environment.NewLine, i.ToString(), ".  Words: ", lsLength);
                        }
                    }

                    i++;
                }

                qcUID++;

                DataRow rowQC = _dsQCIssues.Tables[0].NewRow();
                rowQC[IssueFields.UID] = qcUID;
                rowQC[IssueFields.ParseSeg_UID] = UID;
                rowQC[IssueFields.IssueQty] = QtyLong.ToString();
                rowQC[IssueFields.IssueCat] = IssueCategory.Long_Sentence;
                rowQC[IssueFields.IssueColor] = color;

                string issueMsg = string.Concat(QtyLong.ToString(), "  Long Sentences (> ", LongDef.ToString(), " Words)", Environment.NewLine, Environment.NewLine, wordCountsPerLS, Environment.NewLine, Environment.NewLine, "Shorten your sentences. Compound and convoluted sentences decrease readability. Keep your sentences short.", Environment.NewLine, Environment.NewLine, "Try breaking your long sentences up into short sentences.");

                rowQC[IssueFields.Issue] = issueMsg;

                rowQC[IssueFields.Flag] = false;

                _dsQCIssues.Tables[0].Rows.Add(rowQC);

            }

            _QtySummary.LongSentences = _QtySummary.LongSentences + QtyLong;
            return QtyLong;


        }


        private int FindlongSentences(string UID, int qcUID, int LongDef, string color)
        {
            int QtyLong = 0;
           // bool ignoreLength = false;


            StringBuilder sb = new StringBuilder();

            //file = string.Concat(UID, ".rtf");
            //pathFile = Path.Combine(_DocParsedSec_Path, file);

            string txt = _RTFcontrol.Text;

            //  string[] sentences = Regex.Split(txt, @"(?<=[\.!\?])\s+", RegexOptions.Singleline);

            //  string[] sentences = Regex.Split(txt, @"(?<=[.!?])\s+(?=\p{Lt})", RegexOptions.Singleline);

            string file = string.Empty;
            string pathFile = string.Empty;
            int wordCount = 0;
            //  string[] newlines;

            string[] newlines = txt.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string newline in newlines)
            {
                string[] sentences = Regex.Split(newline, @"(?<=[\.!\?])\s+", RegexOptions.Singleline);
                foreach (string sentence in sentences)
                {
                    wordCount = GetWordCount2(sentence);
                    _QtySummary.Words = _QtySummary.Words + wordCount;


                    if (wordCount > LongDef)
                    {
                        //newlines = sentence.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        //if (newlines.Length == 0 || newlines.Length == 1)
                        //{
                        //if (sentence.IndexOf(':') == -1) // ignore length if sentence contains a ':'
                        //{

                            _RTFcontrol.Find(sentence.Trim());

                            _RTFcontrol.SelectionBackColor = Color.FromName(color);


                       //     HighlightText2(pathFile, sentence.Trim(), true, color, false);

                            sb.AppendLine(sentence + Environment.NewLine + Environment.NewLine);
                            QtyLong++;
                       // }
                        //}
                    }


                    _QtySummary.Sentences++;
                }
            }

            if (QtyLong > 0)
            {
                file = string.Concat(UID, ".LQC");
                pathFile = Path.Combine(_DocParsedSecQC_Path, file);
                Files.WriteStringToFile(sb.ToString(), pathFile);

                qcUID++;

                DataRow rowQC = _dsQCIssues.Tables[0].NewRow();
                rowQC[IssueFields.UID] = qcUID;
                rowQC[IssueFields.ParseSeg_UID] = UID;
                rowQC[IssueFields.IssueQty] = QtyLong.ToString();
                rowQC[IssueFields.IssueCat] = IssueCategory.Long_Sentence;
                rowQC[IssueFields.IssueColor] = color;

                string issueMsg = string.Concat(QtyLong.ToString(), "  Long Sentences", Environment.NewLine, Environment.NewLine, "Shorten your sentences. Compound and convoluted sentences decrease readability. Keep your sentences short.", Environment.NewLine, Environment.NewLine, "Try breaking your long sentences up into short sentences.");

                rowQC[IssueFields.Issue] = issueMsg;

                rowQC[IssueFields.Flag] = false;

                _dsQCIssues.Tables[0].Rows.Add(rowQC);

            }

            _QtySummary.LongSentences = _QtySummary.LongSentences + QtyLong;
            return QtyLong;

        }

        private int GetWordCount2(string text)
        {

            MatchCollection collection = Regex.Matches(text, @"[\S]+");
            return collection.Count;
        }

        private string FormatArray2CommaString(string[] stringArray)
        {
            int count = stringArray.Length;
            if (count  == 0)
                return "None";

            int i = 0;
            string returnValue = string.Empty;

            foreach (string str in stringArray)
            {
                i++;

                //if (i == count)
                //{
                //    returnValue = string.Concat(returnValue, ", ", str);
                //}
                if (i == 1)
                {
                    returnValue = str;
                }
                else
                {
                    returnValue = string.Concat(returnValue, ", ", str);
                }
            }

            return returnValue;
        }

        private string[] SplitSentences(string paragraph)
        {
            _ErrorMessage = string.Empty;

            if (mSentenceDetector == null)
            {
                string fileEnglish = Path.Combine(_ModelPath, "EnglishSD.nbin");
                if (File.Exists(fileEnglish))
                    mSentenceDetector = new OpenNLP.Tools.SentenceDetect.EnglishMaximumEntropySentenceDetector(fileEnglish);
                else
                {
                    _ErrorMessage = string.Concat("Unable to find NLP English file: ", fileEnglish);
                    return null;
                }
            }

            return mSentenceDetector.SentenceDetect(paragraph);
        }

        private string[] FindPassivePhrases(string inputText)
        {

            string word;
            string[] pieces;

            string[] sentences = _Sentences; //SplitSentences(inputText);

            string adjSentence = string.Empty;
            string passiveWords = string.Empty;
            string nextWord = string.Empty;
            string secondNextWord = string.Empty;

            List<string> lstPassiveWords = new List<string>();

            foreach (string sentence in sentences)
            {

                adjSentence = sentence.Replace(".", " ");
                adjSentence = adjSentence.Replace(":", " ");
                adjSentence = adjSentence.Replace(";", ".");
                adjSentence = adjSentence.Replace("?", " ");
                adjSentence = adjSentence.Replace("!", " ");
                adjSentence = adjSentence.Replace(",", "");
                adjSentence = adjSentence.Replace("\'", " ");
                adjSentence = adjSentence.Replace(" ", " ");
                pieces = adjSentence.Split(' ');    

                for (int index = 0; (index <= (pieces.Length - 1)); index++)
                {
                    word = pieces[index];
                    word = word.ToLower().Trim();

                    if (word == "were" || word == "were" || word == "is" || word == "was" || word == "to" || word == "are" || word == "will")
                    {
                        // Get Next word
                        if (index + 1 < pieces.Length)
                        {
                            nextWord = pieces[index + 1];
                        }
                        else
                        {
                            nextWord = string.Empty;
                        }

                        // Get the second next word
                        if (index + 2 < pieces.Length)
                        {
                            secondNextWord = pieces[index + 2];
                        }
                        else
                        {
                            secondNextWord = string.Empty;
                        }

                        if (nextWord.Length > 0)
                        {
                            passiveWords = isPassiveVerb(nextWord, secondNextWord);
                            if (passiveWords.Length > 0)
                            {
                                passiveWords = string.Concat(word, " ", passiveWords);
                                lstPassiveWords.Add(passiveWords);
                            }
                        }
                    }

      
                }

            }

            return lstPassiveWords.ToArray();

        }

        private string[] FindAdverbs(string inputText)
        {
            string fileEnglishSD = Path.Combine(_ModelPath, "EnglishSD.nbin");
            string fileEnglishPOS = Path.Combine(_ModelPath, "EnglishPOS.nbin");
            string fileEnglishTok = Path.Combine(_ModelPath, "EnglishTok.nbin");
            string modelParserPathFile = Path.Combine(_ModelPath, "Parser", "tagdict");

            if (!File.Exists(fileEnglishSD))
            {
                _ErrorMessage = string.Concat("Unable to find NLP English SD file: ", fileEnglishSD);
                return null;
            }

            if (!File.Exists(fileEnglishPOS))
            {
                _ErrorMessage = string.Concat("Unable to find NLP English Pos file: ", fileEnglishPOS);
                return null;
            }

            OpenNLP.Tools.Tokenize.EnglishMaximumEntropyTokenizer tokenizer = new OpenNLP.Tools.Tokenize.EnglishMaximumEntropyTokenizer(fileEnglishTok);
            OpenNLP.Tools.PosTagger.EnglishMaximumEntropyPosTagger tagger = new OpenNLP.Tools.PosTagger.EnglishMaximumEntropyPosTagger(fileEnglishPOS, modelParserPathFile);

            string[] sentences = SplitSentences(inputText);
            string[] tokens;
            string[] tags;
            const string ADVERB = "RB"; // Adverb Tag
            List<string> adverbs = new List<string>();

            foreach (string sentence in sentences)
            {
                tokens = tokenizer.Tokenize(sentence);
                if (tokens.Length > 0)
                {
                    tags = tagger.Tag(tokens);
                    if (tags.Length > 0)
                    {
                        for (int i = 0; i < tags.Length; i++)
                        {
                            if (tags[i] == ADVERB)
                            {
                                adverbs.Add(tokens[i]);
                            }
                        }
                    }
                }
            }

            return adverbs.ToArray();
        }

        private string isPassiveVerb(string word, string word2nd)
        {
            if ((word.EndsWith("ed") || (word.EndsWith("en"))))
            {
                return word;
            }

            string irregularPastTenseVerb = isIrregularPastTenseVerb(word);
            {
                if (irregularPastTenseVerb.Length > 0)
                    return word;
            }

            if (word2nd == string.Empty)
                return string.Empty;

            if ((word2nd.EndsWith("ed") || (word2nd.EndsWith("en"))))
            {
                return string.Concat(word, " ", word2nd);
            }

            irregularPastTenseVerb = isIrregularPastTenseVerb(word2nd);
            {
                if (irregularPastTenseVerb.Length > 0)
                    return string.Concat(word, " ", word2nd);
            }

            return string.Empty;

        }

        private string isIrregularPastTenseVerb(string word)
        {
            if (
                word == "been" ||
                word == "begun" ||
                word == "bent" ||
                word == "bet" ||
                word == "bid" ||
                word == "blown" ||
                word == "brought" ||
                word == "broadcast" ||
                word == "built" ||
                word == "burnt" ||
                word == "bought" ||
                word == "caught" ||
                word == "come" ||
                word == "cost" ||
                word == "cut" ||
                word == "dug" ||
                word == "done" ||
                word == "drawn" ||
                word == "driven" ||
                word == "drunk" ||
                word == "felt" ||
                word == "fought" ||
                word == "found" ||
                word == "flown" ||
                word == "gone" ||
                word == "grown" ||
                word == "hung" ||
                word == "had" ||
                word == "heard" ||
                word == "hit" ||
                word == "held" ||
                word == "hurt" ||
                word == "kept" ||
                word == "known" ||
                word == "laid" ||
                word == "led" ||
                word == "left" ||
                word == "lent" ||
                word == "let" ||
                word == "lain" ||
                word == "lost" ||
                word == "made" ||
                word == "met" ||
                word == "paid" ||
                word == "put" ||
                word == "read" ||
                word == "run" ||
                word == "seen" ||
                word == "sold" ||
                word == "sent" ||
                word == "shown" ||
                word == "shut" ||
                word == "sung" ||
                word == "sat" ||
                word == "slept" ||
                word == "spent" ||
                word == "stood" ||
                word == "swum" ||
                word == "taught" ||
                word == "torn" ||
                word == "told" ||
                word == "thought" ||
                word == "thrown" ||
                word == "understood" ||
                word == "worn" ||
                word == "won" 
                )
            {
                return word;
            }

            return string.Empty;
        }

        private int FindDictionaryTerms(string UID, int qcUID, out int totalTermsFound)
        {
            totalTermsFound = 0;


            if (_DictionaryPathFile.Length == 0)
                return -1;

            if (_dsDictionary == null)
            {
                if (!File.Exists(_DictionaryPathFile))
                    return -1;

                GenericDataManger gDataMgr = new GenericDataManger();
                _dsDictionary = gDataMgr.LoadDatasetFromXml(_DictionaryPathFile);

                if (_dsDictionary == null)
                {
                    return -1;
                }
            }

            string term = string.Empty;
            string def = string.Empty;
            string dicColor = string.Empty;
            int termFound = 0;
          //  StringBuilder sb = new StringBuilder();

            List<string> termFoundText = new List<string>();
          //  List<int> termFoundQty = new List<int>();

            foreach (DataRow rowDicItem in _dsDictionary.Tables[Atebion_Dictionary.DictionaryFieldConst.TableName].Rows)
            {
                termFound = 0; // Reset

                term = rowDicItem[Atebion_Dictionary.DictionaryFieldConst.Item].ToString();

                if (_Dictionary_UseTermsColors)
                {
                    dicColor = rowDicItem[Atebion_Dictionary.DictionaryFieldConst.HighlightColor].ToString();
                }
                else
                {
                    dicColor = _Color_DictionaryTerms;
                }

              termFound = HighlightText2(term, true, dicColor, false);

                if (termFound > 0)
                {
                    def = rowDicItem[Atebion_Dictionary.DictionaryFieldConst.Definition].ToString();

                    def = string.Concat(termFound.ToString(), "  ",term, Environment.NewLine,def);
                    termFoundText.Add(def);
                 

                   
                    totalTermsFound = totalTermsFound + termFound;
                }
            }

            if (totalTermsFound > 0)
            {
                qcUID++;

                DataRow rowQC = _dsQCIssues.Tables[0].NewRow();
                rowQC[IssueFields.UID] = qcUID;
                rowQC[IssueFields.ParseSeg_UID] = UID;
                rowQC[IssueFields.IssueQty] = totalTermsFound;
                rowQC[IssueFields.IssueCat] = IssueCategory.Dictionary;
                rowQC[IssueFields.IssueColor] = _Color_DictionaryTerms;

                int i = 1;
                string termsGrouped = string.Empty;
                //int[] Qty = termFoundQty.ToArray();

                foreach (string termText in termFoundText)
                {

                    if (i == 1)
                    {
                        termsGrouped = termText;
                    }
                    else
                    {
                        termsGrouped = string.Concat(termsGrouped, Environment.NewLine, Environment.NewLine, termText);
                    }

                }

                string issueMsg = string.Concat(totalTermsFound.ToString(), "  Dictionary Terms ", Environment.NewLine, Environment.NewLine, termsGrouped);

                rowQC[IssueFields.Issue] = issueMsg;

                rowQC[IssueFields.Flag] = false;

                _dsQCIssues.Tables[0].Rows.Add(rowQC);

            }


            return totalTermsFound;

        }

        private int HighlightText2(string word, bool wholeWord, string color, bool HighlightText)
        {
            int count = 0;

            word = DataFunctions.RegExFixInvalidCharacters(word);

            if (color == string.Empty)
                color = "YellowGreen";


           // _RTFcontrol.Clear();

            //if (!File.Exists(file))
            //{
            //    return count;
            //}

            //_RTFcontrol.LoadFile(file, System.Windows.Forms.RichTextBoxStreamType.RichText);

            string txt = _RTFcontrol.Text;

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

                MatchCollection matches = regex.Matches(txt);
                int index = -1;
                int startIndex = 0;

                count = matches.Count;

                if (count > 0)
                {
                    // Test code
                    //if (count > 1)
                    //{
                    //    bool what = true;
                    //}

                    foreach (Match match in matches)
                    {
                        index = match.Index;
                        //if (wholeWord)
                        //{
                        //    _rtfCrtl.Select(index + 1, word.Length);
                        //}
                        //else
                        //{
                        _RTFcontrol.Select(index, word.Length);
                        //}

                        if (HighlightText) // Highlight Text
                            _RTFcontrol.SelectionColor = Color.FromName(color);
                        else // Highlight Background
                            _RTFcontrol.SelectionBackColor = Color.FromName(color);

                        startIndex = index + word.Length;
                    }

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

                    //_RTFcontrol.SaveFile(file, System.Windows.Forms.RichTextBoxStreamType.RichText);
                }
            }
            catch { }


            return count;
        }

        private double FleschReadingEase(string inputText, string readabilityType, out string[] complexWords, out int wordCount, out int sentenceCount)
        {
            string ltr;
            wordCount = 0;
            sentenceCount = 0;
            int syllableCount = 0;
            int complexCount = 0;
            int letterCount = 0;
            int i;
            bool inWord = false;
            // NotUsed -- string vowels = "aeiouy";
            string resultsValue = "";
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            inputText = inputText.ToLower();

            for (i = 0; i < inputText.Length; i++)
            {
                ltr = inputText.Substring(i, 1).ToLower();
                if ((ltr == "\'"))
                {
                    //  do nothing if apostrophe
                }
                else if (alphabet.IndexOf(ltr) > -1) //Check for alphabetic input //else if (((ltr >= "a") && (ltr <= "z")))
                {
                    //  check for alphabetic input
                    if (!inWord)
                    {
                        //  new word
                        wordCount++;
                        //  add 1 to count
                    }
                    inWord = true;

                    letterCount++;
                }
                else
                {
                    //  could be space, full stop, comma etc
                    inWord = false;
                    if (((ltr == ".")
                                || ((ltr == "?")
                                || ((ltr == "!")
                                || ((ltr == ":")
                                || (ltr == ";"))))))
                    {
                        sentenceCount++;
                        //  add 1 to count
                    }
                }
            }


            // int input = inputText.Length - 1;
            // char ch;
            //// bool inVowel = false;

            // if (syllableCount == 0)
            //     complexCount = 0;

            // ch =  Convert.ToChar(inputText.Substring(inputText.Length - 1));

            // {
            // if ((ch == 'e')) 
            // {
            //     input--;
            // }

            string word;
            string[] pieces;
            inputText = inputText.Replace(".", " ");
            inputText = inputText.Replace(":", " ");
            inputText = inputText.Replace(";", ".");
            inputText = inputText.Replace("?", " ");
            inputText = inputText.Replace("!", " ");
            inputText = inputText.Replace(",", "");
            inputText = inputText.Replace("\'", " ");
            inputText = inputText.Replace(" ", " ");

           // pieces = inputText.Split(' '); // 09.23.2019
            pieces = _WordsInSegment; // via NLP

            syllableCount = 0;
            complexCount = 0;
            List<string> lstComplexWords = new List<string>();

            for (int index = 0; (index <= (pieces.Length - 1)); index++)
            {
                word = pieces[index];
                word = word.ToLower().Trim();
                string pattern = "[aeiouy]+";
                int count = Regex.Matches(word, pattern).Count;
                if (word.EndsWith("e"))
                {
                    count--;
                }
                if ((word.EndsWith("cial")
                            || (word.EndsWith("tia")
                            || (word.EndsWith("cius")
                            || (word.EndsWith("cious")
                            || (word.EndsWith("giu")
                            || (word.EndsWith("ion") || word.EndsWith("iou"))))))))
                // Or word.EndsWith("sia") Or word.EndsWith("ely") Then
                {
                    count++; // = count + 1;
                }

                syllableCount = (syllableCount + count);
                //If count <>Then
                //    count = 1
                //End If
                //count = 1;
                if (count == 0)
                    count = 1;

                word = word.ToString().TrimEnd(Environment.NewLine.ToCharArray());
                word = word.Replace(Environment.NewLine, " ");
               // if (count >= 3) 09.13.2019
                if (count > _ComplexWords_SyllableCountGreaterThan)
                {
                    if (word.IndexOf(@"\") == -1 && word.IndexOf(@"/") == -1 && word.IndexOf(@"-") == -1 && word.IndexOf(@"(") == -1 && word.IndexOf(@")") == -1)
                    {
                        lstComplexWords.Add(word);
                        complexCount++;
                    }
                }
            }

            complexWords = lstComplexWords.ToArray();

            //resultsValue = (resultsValue + ("Words: "
            //            + (wordCount.ToString() + Environment.NewLine)));
            //resultsValue = (resultsValue + ("Sentences: "
            //            + (sentenceCount.ToString() + Environment.NewLine)));
            //resultsValue = (resultsValue + ("Syllable: "
            //            + (syllableCount.ToString() + Environment.NewLine)));
            //resultsValue = (resultsValue + ("Complex Words: "
            //            + (complexCount.ToString() + Environment.NewLine)));

            ////// Gunning fog index
            ////double fog = 0.4 * ((double)wordCount / sentenceCount + (100.0 * complexCount) / wordCount);
            //resultsValue = (resultsValue + ("Gunning Fog index: "
            //            + (((0.4 * ((wordCount / sentenceCount) + (100 * complexCount) / wordCount))))).ToString() + Environment.NewLine);
            ////resultsValue = (resultsValue + ("Flesch-Kincaid Reading Age: " 
            ////            + (((206.876 
            ////            - ((1.015 
            ////            * (wordCount / sentenceCount)) - (84.6 
            ////            * (syllableCount / wordCount))))).ToString() + Environment.NewLine)));


            ////// Flesch-Kincaid grade level
            ////double fkgl = (0.39 * (wordCount / sentenceCount) + 11.8 * (syllableCount / wordCount) - 15.59;
            //resultsValue = (resultsValue + ("Flesch-Kincaid Grade Level: "
            //             + (Math.Round((0.39 * (wordCount / sentenceCount) + 11.8 * (syllableCount / wordCount) - 15.59), 1)).ToString() + Environment.NewLine));


            ////// Automated readability index
            ////double ari = (4.71 * letterNumberCount) / wordCount + (0.5 * wordCount) / sentenceCount - 21.43;
            //resultsValue = (resultsValue + ("Automated Readability Index: "
            //             + (Math.Round((4.71 * letterCount) / wordCount + (0.5 * wordCount) / sentenceCount - 21.43, 1)).ToString() + Environment.NewLine));

            //// Flesch reading ease score
            //double fres = 206.835 - (1.015 * wordCount) / sentenceCount - (84.6 * syllableCount) / wordCount;
            //      resultsValue = (resultsValue + ("Flesch Reading Ease Score: "
            //       + (Math.Round((206.835 - (1.015 * wordCount) / sentenceCount - (84.6 * syllableCount) / wordCount), 2)).ToString() + Environment.NewLine));

            double returnValue;

            if (sentenceCount == 0)
            {
                sentenceCount = 1;
            }

            if (syllableCount == 0)
            {
                syllableCount = 1;
            }

            returnValue = 0;

            if (readabilityType == ReadabilityType.Flesch_Reading_Ease)
                returnValue = Math.Round((206.835 - (1.015 * wordCount) / sentenceCount - (84.6 * syllableCount) / wordCount), 2);
            else if (readabilityType == ReadabilityType.Flesch_Kincaid_grade_level)
                returnValue = Math.Round((0.39 * (wordCount / sentenceCount) + 11.8 * (syllableCount / wordCount) - 15.59), 1);



            if (returnValue < 0)
                returnValue = returnValue * -1;

            if (returnValue > 100)
                returnValue = 100;

            return returnValue;
        }

        private void ResetSumValues()
        {
            _QtySummary.freDifficult = 0;
            _QtySummary.freEasy = 0;
            _QtySummary.freFairlyDifficult = 0;
            _QtySummary.freFairlyEasy = 0;
            _QtySummary.freStandard = 0;
            _QtySummary.freVeryConfusing = 0;
            _QtySummary.freVeryEasy = 0;
            _QtySummary.LongSentences = 0;
            _QtySummary.Segments = 0;
            _QtySummary.Sentences = 0;
            _QtySummary.Words = 0;
        }

        private void SetFleschReadingEaseSegValue(double dblQuality)
        {
            if (dblQuality < 30)
                _QtySummary.freVeryConfusing++;
            else if (dblQuality >= 29.99 && dblQuality < 50)
                _QtySummary.freDifficult++;
            else if (dblQuality < 60 && dblQuality >= 50)
                _QtySummary.freFairlyDifficult++;
            else if (dblQuality < 70 && dblQuality >= 60)
                _QtySummary.freStandard++;
            else if (dblQuality < 80 && dblQuality >= 70)
                _QtySummary.freFairlyEasy++;
            else if (dblQuality < 90 && dblQuality >= 80)
                _QtySummary.freEasy++;
            else if (dblQuality >= 90)
                _QtySummary.freVeryEasy++;
        }

        private bool CreateQCReadablitySumDataSet()
        {
            DataTable table = new DataTable(ReadabilitySumFields.TableName);
            table.Columns.Add(ReadabilitySumFields.VeryEasy, typeof(double));
            table.Columns.Add(ReadabilitySumFields.Easy, typeof(double));
            table.Columns.Add(ReadabilitySumFields.FairlyEasy, typeof(double));
            table.Columns.Add(ReadabilitySumFields.Standard, typeof(double));
            table.Columns.Add(ReadabilitySumFields.Difficult, typeof(double));
            table.Columns.Add(ReadabilitySumFields.FairlyDifficult, typeof(double));
            table.Columns.Add(ReadabilitySumFields.VeryConfusing, typeof(double));

            _dsQCReadablity = new DataSet();

            _dsQCReadablity.Tables.Add(table);
            
            return true;
        }

        private bool CreateSettingsDataSet()
        {
            DataTable table = new DataTable(SettingsFields.TableName);
            table.Columns.Add(SettingsFields.A_Color, typeof(string));
            table.Columns.Add(SettingsFields.A_Importance, typeof(string));
            table.Columns.Add(SettingsFields.CW_Color, typeof(string));
            table.Columns.Add(SettingsFields.CW_Importance, typeof(string));
            table.Columns.Add(SettingsFields.DT_Color, typeof(string));
         //   table.Columns.Add(SettingsFields.DT_Dictionary, typeof(string));
            table.Columns.Add(SettingsFields.LS_Color, typeof(string));
            table.Columns.Add(SettingsFields.LS_Importance, typeof(string));
            table.Columns.Add(SettingsFields.LS_WordsGreaterThan, typeof(int));
            table.Columns.Add(SettingsFields.PV_Color, typeof(string));
            table.Columns.Add(SettingsFields.PV_Importance, typeof(string));

            _dsSettings = new DataSet();

            _dsSettings.Tables.Add(table);

            return true;
        }

        private bool CreateQCStatsDataSet()
        {
            DataTable table = new DataTable(QCStatsFields.TableName);
            table.Columns.Add(QCStatsFields.TotalWords, typeof(int));
            table.Columns.Add(QCStatsFields.TotalSentence, typeof(int));
            table.Columns.Add(QCStatsFields.TotalLongSentence, typeof(int));
            table.Columns.Add(QCStatsFields.TotalComplexWords, typeof(int));
            table.Columns.Add(QCStatsFields.TotalAdverbs, typeof(int));
            table.Columns.Add(QCStatsFields.TotalPassiveVoice, typeof(int));
            table.Columns.Add(QCStatsFields.TotalDictionaryTerms, typeof(int));

            _dsQCStats = new DataSet();

            _dsQCStats.Tables.Add(table);

            return true;

        }

        private bool CreateQCParseSegDataSet()
        {
            
            bool pageColExists = false;

            DataTable table = new DataTable(QCParseResultsFields.TableName);

            table.Columns.Add(QCParseResultsFields.UID, typeof(int));
            table.Columns.Add(QCParseResultsFields.Rank, typeof(string));
            table.Columns.Add(QCParseResultsFields.Weight, typeof(double));
            table.Columns.Add(QCParseResultsFields.Number, typeof(string));
            table.Columns.Add(QCParseResultsFields.Caption, typeof(string));
            if (DataFunctions.ColumnExists(QCParseResultsFields.Page, _dsParseResults.Tables[0]))
            {
                pageColExists = true;
                table.Columns.Add(QCParseResultsFields.Page, typeof(string));
            }

            table.Columns.Add(QCParseResultsFields.Readability, typeof(double));

            if (_Find_LongSentence)
            {
                table.Columns.Add(QCParseResultsFields.LongSentences, typeof(int));
            }

            if (_Find_ComplexWords)
            {
                table.Columns.Add(QCParseResultsFields.ComplexWords, typeof(int));
            }

            if (_Find_PassiveVoice)
            {
                table.Columns.Add(QCParseResultsFields.PassiveVoice, typeof(int));
            }

            if (_Find_Adverbs)
            {
                table.Columns.Add(QCParseResultsFields.Adverbs, typeof(int));
            }

            if (_Find_DictionaryTerms)
            {
                table.Columns.Add(QCParseResultsFields.DictionaryTerms, typeof(int));
            }

            table.Columns.Add(QCParseResultsFields.Words, typeof(int));
            table.Columns.Add(QCParseResultsFields.Sentences, typeof(int));


            // Populate QC Analysis Results table from the standard Analysis Results table
            DataRow newRow;
            foreach (DataRow row in _dsParseResults.Tables[0].Rows)
            {
                newRow = table.NewRow();

                newRow[QCParseResultsFields.UID] = row[QCParseResultsFields.UID];
                newRow[QCParseResultsFields.Number] = row[QCParseResultsFields.Number];
                newRow[QCParseResultsFields.Caption] = row[QCParseResultsFields.Caption];

                if (pageColExists)
                {
                    newRow[QCParseResultsFields.Page] = row[QCParseResultsFields.Page];
                }

                table.Rows.Add(newRow);

            }

            _dsQCParseResults = new DataSet();

            _dsQCParseResults.Tables.Add(table);

            return true;

        }

        //private bool checkAdjParseSecTable()
        //{
        //    try
        //    {
        //        // Readablity
        //        if (!DataFunctions.ColumnExists(_Quality, _dsParseResults.Tables[0]))
        //        {
        //            System.Data.DataColumn newColumn = new System.Data.DataColumn(_Quality, typeof(System.Double));
        //            newColumn.DefaultValue = 0;
        //            _dsParseResults.Tables[0].Columns.Add(newColumn);
        //        }

        //        // Complex Words
        //        if (!DataFunctions.ColumnExists(_ComplexWordsQty, _dsParseResults.Tables[0]))
        //        {
        //            System.Data.DataColumn newColumn = new System.Data.DataColumn(_ComplexWordsQty, typeof(string));
        //            newColumn.DefaultValue = string.Empty;
        //            _dsParseResults.Tables[0].Columns.Add(newColumn);
        //        }

        //        // Long Sentences Qty
        //        if (!DataFunctions.ColumnExists(_LongQty, _dsParseResults.Tables[0]))
        //        {
        //            System.Data.DataColumn newColumn = new System.Data.DataColumn(_LongQty, typeof(string));
        //            newColumn.DefaultValue = string.Empty;
        //            _dsParseResults.Tables[0].Columns.Add(newColumn);
        //        }


        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        _ErrorMessage = string.Concat("Analysis Results Segments Datatable Error: ", ex.Message);

        //        return false;
        //    }
        //}

        private DataTable CreateTable_FoundIssues()
        {
            DataTable table = new DataTable(IssueFields.TableName);

            table.Columns.Add(IssueFields.UID, typeof(int));
            table.Columns.Add(IssueFields.ParseSeg_UID, typeof(int));
            table.Columns.Add(IssueFields.IssueQty, typeof(string));
       //     table.Columns.Add(IssueFields.Blank, typeof(string));
            table.Columns.Add(IssueFields.Flag, typeof(bool));
            table.Columns.Add(IssueFields.IssueCat, typeof(string));
            table.Columns.Add(IssueFields.Issue, typeof(string));
            table.Columns.Add(IssueFields.IssueColor, typeof(string));
            table.Columns.Add(IssueFields.Weight, typeof(string));

            return table;

        }


    }
}
