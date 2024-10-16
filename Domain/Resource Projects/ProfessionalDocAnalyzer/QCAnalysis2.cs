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
using Atebion.QC;

namespace ProfessionalDocAnalyzer
{
    public class QCAnalysis2
    {
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

        private string _ParseResultsFile = "ParseResults.xml";
        private string _QCIssuesFile = "QCIssues.xml";
        private string _QCParseResultsFile = "QCParseResults.xml";
        private string _QCNoticeFile = "QCNotices.txt";
        private string _QCStatsFile = "QCStats.xml";
        private string _QCReadabilitySumFile = "QCReadabilitySum.xml";
        private DataSet _dsParseResults;
        private DataSet _dsQCParseResults;
        private DataSet _dsQCIssues;
        private DataSet _dsQCStats;
        private DataSet _dsQCReadablity;
        private DataSet _dsDictionary;

        private double _WeightTotal_Segment = 0;

        private OpenNLP.Tools.SentenceDetect.MaximumEntropySentenceDetector mSentenceDetector;

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

        private int _LongSentence_Def = 21;
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

        private double _WeightItem_LongSentence = .50;
        public double WeightItem_LongSentence
        {
            get { return _WeightItem_LongSentence; }
            set { _WeightItem_LongSentence = value; }
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
            get { return _TotalFound_Adverbs; }
        }

        private double _WeightItem_Adverbs = .25;
        public double WeightItem_Adverbs
        {
            get { return _WeightItem_Adverbs; }
            set { _WeightItem_Adverbs = value; }
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
            set { _Find_PassiveVoice = value; }
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

        private string _Color_DictionaryTerms = "Blue";
        public string Color_DictionaryTerms
        {
            get { return _Color_DictionaryTerms; }
            set { _Color_DictionaryTerms = value; }
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



            string docParsedSecPathFile = Path.Combine(_DocParsedSecXML_Path, _ParseResultsFile);
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

            const string EASY_WORDS = "Use short, easy words. The more syllables your words have, the harder it is to read. Don’t use 3 or 4 syllable words if a 2-syllable word works just as well.";
            const string PASSIVE_VOICE = "Passive Voice typically creates unclear, less direct, and wordy sentences. Whereas, active voice is clearer and more concise.";
            const string ADVERB = "Only use an adverb if it’s necessary, where you can’t convey the same meaning without it.";


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

                    row[QCParseResultsFields.Readability] = rfeResult; // enter Readablitiy into QC Analysis Results table

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


                            if (complexWordsQty > 1)
                            {
                                complexWordsText = FormatArray2CommaString(complexWords);
                                complexWordsText = string.Concat("Complex Words  Qty: ", complexWordsQty.ToString(), Environment.NewLine, Environment.NewLine, complexWordsText, Environment.NewLine, Environment.NewLine, EASY_WORDS, Environment.NewLine, Environment.NewLine);
                            }
                            else
                            {
                                complexWordsText = string.Concat("Complex Word  Qty: 1", Environment.NewLine, Environment.NewLine, complexWords[0], Environment.NewLine, Environment.NewLine, EASY_WORDS, Environment.NewLine, Environment.NewLine);
                            }

                            rowQC[IssueFields.Issue] = complexWordsText;

                            _dsQCIssues.Tables[0].Rows.Add(rowQC);

                            _TotalFound_ComplexWords = _TotalFound_ComplexWords + complexWordsQty;

                            _WeightTotal_Segment = _WeightTotal_Segment + (complexWordsQty * _WeightItem_ComplexWords);

                            row[QCParseResultsFields.ComplexWords] = complexWordsQty;

                            qcUID++;
                        }
                        //else
                        //{
                        //    row[QCParseResultsFields.ComplexWords] = 0;
                        //}
                    }


                    // Long Sentences
                    if (_Find_LongSentence)
                    {
                        longSentencesQty = FindlongSentences(uid, qcUID, _LongSentence_Def, _Color_LongSentence);
                        if (longSentencesQty > 0)
                        {

                            _TotalFound_LongSentence = _TotalFound_LongSentence + longSentencesQty;

                            _WeightTotal_Segment = _WeightTotal_Segment + (longSentencesQty * _WeightItem_LongSentence);

                            row[QCParseResultsFields.LongSentences] = longSentencesQty;

                            qcUID++;

                        }
                    }


                    // HighLight complex Words
                    //foreach (string complexword in complexWords)
                    //{
                    //    HighlightText2(complexword, true, _Color_ComplexWords, false);
                    //}
                    if (_Find_ComplexWords)
                    {
                        if (complexWordsQty > 0)
                        {
                            foreach (string cword in complexWords)
                            {
                                _RTFcontrol.Find(cword, RichTextBoxFinds.WholeWord);

                                _RTFcontrol.SelectionBackColor = Color.FromName(_Color_ComplexWords);
                            }
                        }
                    }

                    // Passive Voice
                    if (_Find_PassiveVoice)
                    {
                        passivePhrases = FindPassivePhrases(text2Review);
                        if (passivePhrases.Length > 0)
                        {
                            passiveText = FormatArray2CommaString(passivePhrases);
                            passiveText = string.Concat("Passive Voice  Qty: ", passivePhrases.Length.ToString(), Environment.NewLine, Environment.NewLine, passiveText, Environment.NewLine, Environment.NewLine, PASSIVE_VOICE, Environment.NewLine, Environment.NewLine);


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
                                _RTFcontrol.Find(ptext, RichTextBoxFinds.WholeWord);

                                _RTFcontrol.SelectionBackColor = Color.FromName(_Color_PassiveVoice);
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
                            adverbText = string.Concat("Adverb  Qty: ", adverbs.Length.ToString(), Environment.NewLine, Environment.NewLine, adverbText, Environment.NewLine, Environment.NewLine, ADVERB, Environment.NewLine, Environment.NewLine);


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
                                _RTFcontrol.Find(adverb, RichTextBoxFinds.WholeWord);

                                _RTFcontrol.SelectionBackColor = Color.FromName(_Color_Adverbs);
                            }

                        }
                    }

                    if (_DictionaryPathFile.Length != 0)
                    {
                        dicQtyTermsFound = 0;
                        dicTerms = string.Empty;

                        dicTerms = FindDictionaryTerms(out dicQtyTermsFound);
                        if (dicQtyTermsFound > 0)
                        {
                            dicTerms = string.Concat("Dictionary Terms  Qty: ", dicQtyTermsFound.ToString(), Environment.NewLine, Environment.NewLine, dicTerms);

                            rowQC = _dsQCIssues.Tables[0].NewRow();
                            rowQC[IssueFields.UID] = qcUID;
                            rowQC[IssueFields.ParseSeg_UID] = uid;
                            rowQC[IssueFields.IssueQty] = dicQtyTermsFound.ToString();
                            rowQC[IssueFields.IssueCat] = IssueCategory.Dictionary;
                            rowQC[IssueFields.IssueColor] = _Color_DictionaryTerms;
                            rowQC[IssueFields.Issue] = dicTerms;

                            _dsQCIssues.Tables[0].Rows.Add(rowQC);

                            _TotalFound_DictionaryTerms = _TotalFound_DictionaryTerms + _DictionaryPathFile.Length;

                            row[QCParseResultsFields.DictionaryTerms] = _DictionaryPathFile.Length;

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

                row[IssueFields.Weight] = _WeightTotal_Segment;

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

        private int FindlongSentences(string UID, int qcUID, int LongDef, string color)
        {
            int QtyLong = 0;
            // bool ignoreLength = false;


            StringBuilder sb = new StringBuilder();


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
                        if (sentence.IndexOf(':') == -1) // ignore length if sentence contains a ':'
                        {
                            
                            _RTFcontrol.Find(sentence.Trim());
                            _RTFcontrol.SelectionBackColor = Color.FromName(color);

                            file = string.Concat(UID, ".rtf");
                            pathFile = Path.Combine(_DocParsedSec_Path, file);

                            //     HighlightText2(pathFile, sentence.Trim(), true, color, false);

                            sb.AppendLine(sentence + Environment.NewLine + Environment.NewLine);
                            QtyLong++;
                        }
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

                string issueMsg = string.Concat("Shorten your sentences. Compound and convoluted sentences decrease readability. Keep your sentences short.", Environment.NewLine, Environment.NewLine, "Try breaking your long sentences up into short sentences.");

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
            if (count == 0)
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

            string[] sentences = SplitSentences(inputText);

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

        private string FindDictionaryTerms(out int totalTermsFound)
        {
            totalTermsFound = 0;


            if (_DictionaryPathFile.Length == 0)
                return string.Empty;

            if (_dsDictionary == null)
            {
                if (!File.Exists(_DictionaryPathFile))
                    return string.Empty;

                GenericDataManger gDataMgr = new GenericDataManger();
                _dsDictionary = gDataMgr.LoadDatasetFromXml(_DictionaryPathFile);

                if (_dsDictionary == null)
                {
                    return string.Empty;
                }
            }

            string term = string.Empty;
            string def = string.Empty;
            string dicColor = string.Empty;
            int termFound = 0;
            StringBuilder sb = new StringBuilder();

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

                // ToDo  termFound = HighlightText2(term, true, dicColor, true);

                if (termFound > 0)
                {
                    def = dicColor = rowDicItem[Atebion_Dictionary.DictionaryFieldConst.Definition].ToString();
                    sb.AppendLine(string.Concat(term, "  ", termFound.ToString()));
                    if (def.Length > 0)
                        sb.AppendLine(def);

                    sb.AppendLine(" ");

                    totalTermsFound = totalTermsFound + termFound;
                }
            }

            return sb.ToString();
        }

        private int HighlightText2(string file, string word, bool wholeWord, string color, bool HighlightText)
        {
            int count = 0;

            if (color == string.Empty)
                color = "YellowGreen";


            _RTFcontrol.Clear();

            if (!File.Exists(file))
            {
                return count;
            }

            _RTFcontrol.LoadFile(file, System.Windows.Forms.RichTextBoxStreamType.RichText);

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

                _RTFcontrol.SaveFile(file, System.Windows.Forms.RichTextBoxStreamType.RichText);
            }


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
            pieces = inputText.Split(' ');
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
            table.Columns.Add(IssueFields.Flag, typeof(bool));
            table.Columns.Add(IssueFields.IssueQty, typeof(string));
            table.Columns.Add(IssueFields.IssueCat, typeof(string));
            table.Columns.Add(IssueFields.Issue, typeof(string));
            table.Columns.Add(IssueFields.IssueColor, typeof(string));
            table.Columns.Add(IssueFields.Weight, typeof(string));

            return table;

        }

    }
}
