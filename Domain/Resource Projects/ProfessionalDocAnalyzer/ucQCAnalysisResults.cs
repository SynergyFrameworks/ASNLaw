using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using Atebion.Common;
using Atebion.QC;

namespace ProfessionalDocAnalyzer
{
    public partial class ucQCAnalysisResults : UserControl
    {
        public ucQCAnalysisResults()
        {
            InitializeComponent();
        }

        private bool _isLoaded = false;

        private string _ParseSegPath = string.Empty;
        private string _XMLPath = string.Empty;
        private string _ChartPicsPath = string.Empty;
     //   private string _LsQCPath = string.Empty;
        private string _SourceDoc = string.Empty;

        private string _QCIssuesFile = "QCIssues.xml";
        private string _QCParseResultsFile = "QCParseResults.xml";
        private string _QCReadabilitySumFile = "QCReadabilitySum.xml";
        private string _QCStatsFile = "QCStats.xml";

        private string _QCIssuesPathFile = string.Empty;
        private string _QCParseResultsPathFile = string.Empty;
        private string _QCReadabilitySumPathFile = string.Empty;
        private string _QCStatsPathFile = string.Empty;
        private string _PathNotes = string.Empty;
        private string _NotesPostFix = string.Empty;

        private DataSet _dsParseResults;


        // colors
        private string _Color_LongSentence = "Pink";
        private string _Color_Adverbs = "Pink";
        private string _Color_ComplexWords = "Pink";
        private string _Color_PassiveVoice = "Pink";
        private string _Color_DictionaryTerms = "Pink";

        // Summary Values
        private string _ReadabilityAvg = "0";
        private string _ReadabilityLevel = "";
        private string _TotalWords = "0";
        private string _TotalSentence = "0";
        private string _TotalAdverbs = "0";
        private string _TotalComplexWords = "0";
        private string _TotalDictionaryTerms = "0";
        private string _TotalLongSentence = "0";
        private string _TotalPassiveVoice = "0";
        private string _TotalDicTerms = "0";

        private string _UID = "-1";
        private string _FileName = string.Empty;

        // Search
        private int _FoundQty = 0;
        private string _SearchCriteria = string.Empty;
        private DataView _dv;

        private subpanels _CurrentSubpanel = subpanels.Issues;
        private enum subpanels
        {
            Issues = 0,
            Search = 1,
            Reports = 2,
            Charts = 3,
            Notes = 4

        }

        private string _ProjectName = string.Empty;

        private Image _Notepad;

        public bool LoadData(string ParseSegPath, string XMLPath, string SourceDoc)
        {
            _ParseSegPath = ParseSegPath;
            _XMLPath = XMLPath;
           // _LsQCPath = LsQCPath;
            _SourceDoc = SourceDoc;

            _PathNotes = Path.Combine(_ParseSegPath, "Notes");

            if (File.Exists(string.Concat(Application.StartupPath, @"\Notepad16x16.jpg")))
                _Notepad = Image.FromFile(string.Concat(Application.StartupPath, @"\Notepad16x16.jpg"));

            if (!CheckFoldersFile())
                return false;

            ucDocTypeName1.LoadData(_SourceDoc);

            if (!LoadAnalysisResults())
                return false;

            string[] dirs = ParseSegPath.Split(Path.DirectorySeparatorChar);
            if (dirs.Length > 6)
            {
                _ProjectName = dirs[dirs.Length - 6];
            }

            _QCIssuesPathFile = Path.Combine( _XMLPath, _QCIssuesFile);
            if (File.Exists(_QCIssuesPathFile))
            {
                ucQCIssues1.LoadData(_QCIssuesPathFile);
                ucQCIssues1.Visible = true;
                ucQCIssues1.Dock = DockStyle.Fill;
            }
            else
            {
                ucQCIssues1.Visible = false;
            }

            if (LoadStats())
            {
                panAdverbs.Visible = true;
                panComplexWords.Visible = true;
                panLongSentences.Visible = true;
                panPassiveVoice.Visible = true;
                panDicTerms.Visible = true;
            }
            else
            {
                panAdverbs.Visible = false;
                panComplexWords.Visible = false;
                panLongSentences.Visible = false;
                panPassiveVoice.Visible = false;
                panDicTerms.Visible = false;
            }

            LoadShowSelections();

            LoadReadabilityChart();

            _isLoaded = true;

            return true;
        }

        private void LoadShowSelections()
        {
            cboShow.Items.Clear();
            cboShow.Items.Add("All");
            cboShow.Items.Add("Less Than A");
            cboShow.Items.Add("Less Than B");
            cboShow.Items.Add("Less Than C");
            cboShow.Items.Add("Less Than D");
        //    cboShow.Items.Add("Less than F");
            //cboShow.Items.Add(QAFilter.Harder_than_Very_Easy);
            //cboShow.Items.Add(QAFilter.Harder_than_Easy);
            //cboShow.Items.Add(QAFilter.Harder_than_Fairly_Easy);
            //cboShow.Items.Add(QAFilter.Harder_than_Standard);
            //cboShow.Items.Add(QAFilter.Harder_than_Fairly_Difficult);
            //cboShow.Items.Add(QAFilter.Harder_than_Difficult);

            cboShow.SelectedIndex = 0;

        }

        private bool LoadReadabilityChart()
        {
            if (!File.Exists(_QCReadabilitySumPathFile))
            {
                butCharts.Visible = false;
            }

            butCharts.Visible = true;

            GenericDataManger eDataMgr = new GenericDataManger();
            DataSet ds = eDataMgr.LoadDatasetFromXml(_QCReadabilitySumPathFile);
            if (ds == null)
                return false;

            if (ds.Tables.Count == 0)
                return false;

            if (ds.Tables[0].Rows.Count == 0)
                return false;


            DataRow row = ds.Tables[0].Rows[0];

            ucQCCharts1.Readability_VeryEasy = Convert.ToDouble(row[ReadabilitySumFields.VeryEasy].ToString());
            ucQCCharts1.Readability_Easy = Convert.ToDouble(row[ReadabilitySumFields.Easy].ToString());
            ucQCCharts1.Readability_FairlyEasy = Convert.ToDouble(row[ReadabilitySumFields.FairlyEasy].ToString());
            ucQCCharts1.Readability_Standard = Convert.ToDouble(row[ReadabilitySumFields.Standard].ToString());
            ucQCCharts1.Readability_Difficult = Convert.ToDouble(row[ReadabilitySumFields.Difficult].ToString());
            ucQCCharts1.Readability_FairlyDifficult = Convert.ToDouble(row[ReadabilitySumFields.FairlyDifficult].ToString());
            ucQCCharts1.Readability_VeryConfusing = Convert.ToDouble(row[ReadabilitySumFields.FairlyDifficult].ToString());

            ucQCCharts1.LoadData(_ChartPicsPath);

            return true;
        }

        private bool LoadStats()
        {
            string statsPathFile = Path.Combine(_XMLPath, _QCStatsFile);
            if (!File.Exists(statsPathFile))
            {
                return false;
            }

            GenericDataManger eDataMgr = new GenericDataManger();
            DataSet dsQCStats = eDataMgr.LoadDatasetFromXml(statsPathFile);
            if (dsQCStats == null)
                return false;
            
            Atebion.QC.Analysis qcAnalysis = new Analysis(AppFolders.AppDataPathToolsQC);

            if (dsQCStats.Tables.Count == 0)
                return false;

            if (dsQCStats.Tables[0].Rows.Count == 0)
                return false;


            DataRow row = dsQCStats.Tables[0].Rows[0];
            _TotalWords = row[QCStatsFields.TotalWords].ToString();
             _TotalSentence = row[QCStatsFields.TotalSentence].ToString();
             _TotalAdverbs = row[QCStatsFields.TotalAdverbs].ToString();
             _TotalComplexWords = row[QCStatsFields.TotalComplexWords].ToString();
             _TotalDictionaryTerms = row[QCStatsFields.TotalDictionaryTerms].ToString();
             _TotalLongSentence = row[QCStatsFields.TotalLongSentence].ToString();
             _TotalPassiveVoice = row[QCStatsFields.TotalPassiveVoice].ToString();
             _TotalDicTerms = row[QCStatsFields.TotalDictionaryTerms].ToString();

            lblWords.Text = string.Concat("Words  ", _TotalWords);

            lblSentences.Text = string.Concat("Sentences  ", _TotalSentence);

            lblAdverbsValue.Text = _TotalAdverbs;
            panAdverbs.BackColor = Color.FromName(qcAnalysis.Color_Adverbs);
            _Color_Adverbs = qcAnalysis.Color_Adverbs;

            lblComplexWordsValue.Text = _TotalComplexWords;
            panComplexWords.BackColor = Color.FromName(qcAnalysis.Color_ComplexWords);
            _Color_ComplexWords = qcAnalysis.Color_ComplexWords;

            
            lblLongSentencesValue.Text = _TotalLongSentence;
            panLongSentences.BackColor = Color.FromName(qcAnalysis.Color_LongSentence);
            _Color_LongSentence = qcAnalysis.Color_LongSentence;

            lblPassiveVoiceValue.Text = _TotalPassiveVoice;
            panPassiveVoice.BackColor = Color.FromName(qcAnalysis.Color_PassiveVoice);
            _Color_PassiveVoice = qcAnalysis.Color_PassiveVoice;

            lblDicTermsValue.Text = _TotalDicTerms; 
            panDicTerms.BackColor = Color.FromName(qcAnalysis.Color_DictionaryTerms);
            _Color_DictionaryTerms = qcAnalysis.Color_DictionaryTerms;

            // Charts
                    // Sentences
            ucQCCharts1.Sentences_Long = Convert.ToInt32(_TotalLongSentence);
            ucQCCharts1.Sentences_Total = Convert.ToInt32(_TotalSentence);
            ucQCCharts1.Sentences_Long_Color = qcAnalysis.Color_LongSentence;
                   // Complex Words
            ucQCCharts1.Words_Complex = Convert.ToInt32(_TotalComplexWords);
            ucQCCharts1.Words_Total = Convert.ToInt32(_TotalWords);
            ucQCCharts1.Words_Complex_Color = qcAnalysis.Color_ComplexWords;
                   // Passive Voice
            ucQCCharts1.Passive_Voice = Convert.ToInt32(_TotalPassiveVoice);
            ucQCCharts1.Passive_Voice_Color = qcAnalysis.Color_PassiveVoice;
                  // Adverbs
            ucQCCharts1.Adverbs_Total = Convert.ToInt32(_TotalAdverbs);
            ucQCCharts1.Adverbs_Color = qcAnalysis.Color_Adverbs;
                // Dictionary Terms
            ucQCCharts1.DictionaryTerms_Total = Convert.ToInt32(_TotalDicTerms);
            ucQCCharts1.DictionaryTerms_Color = qcAnalysis.Color_DictionaryTerms;

            return true;
        }

        private void HideSubpanels()
        {
            ucExported1.Visible = false;
            ucNotes1.Visible = false;
            ucQCCharts1.Visible = false;
            ucQCIssues1.Visible = false;
            ucSearch1.Visible = false;
            ucSearch_hoot1.Visible = false;

            butCharts.ForeColor = Color.Black;
            butCharts.BackColor = Color.WhiteSmoke;

            butExported.ForeColor = Color.Black;
            butExported.BackColor = Color.WhiteSmoke;

            butIssue.ForeColor = Color.Black;
            butIssue.BackColor = Color.WhiteSmoke;

            butNotes.ForeColor = Color.Black;
            butNotes.BackColor = Color.WhiteSmoke;

            butSearch.ForeColor = Color.Black;
            butSearch.BackColor = Color.WhiteSmoke;

            

        }

        private void AdjustSubpanels()
        {
            HideSubpanels();

            switch (_CurrentSubpanel)
            {
                case subpanels.Issues:
                    butIssue.ForeColor = Color.White;
                    butIssue.BackColor = Color.Teal;
                    ucQCIssues1.Visible = true;
                    ucQCIssues1.Dock = DockStyle.Fill;

                    break;

                case subpanels.Charts:
                    butCharts.ForeColor = Color.White;
                    butCharts.BackColor = Color.Teal;
                    ucQCCharts1.Visible = true;
                    ucQCCharts1.Dock = DockStyle.Fill;

                    break;

                case subpanels.Notes:
                    butNotes.ForeColor = Color.White;
                    butNotes.BackColor = Color.Teal;
                    string notesPath = Path.Combine(_ParseSegPath, "Notes");
                    if (Directory.Exists(notesPath))
                    {
                        ucNotes1.LoadData(notesPath, string.Empty);
                        ucNotes1.UID = _UID;
                        ucNotes1.Visible = true;
                        ucNotes1.Dock = DockStyle.Fill;
                    }

                    break;

                case subpanels.Reports:
                    butExported.ForeColor = Color.White;
                    butExported.BackColor = Color.Teal;
                    string exportPath = Path.Combine(_ParseSegPath, "Export");
                    ucExported1.LoadData(exportPath);
                    ucExported1.Visible = true;
                    ucExported1.Dock = DockStyle.Fill;

                    break;

                case subpanels.Search:
                    butSearch.ForeColor = Color.White;
                    butSearch.BackColor = Color.Teal;

                    string indexPath = string.Empty;

                    if (!AppFolders.UseHootSearchEngine)
                    {
                        indexPath = Path.Combine(_ParseSegPath, "Index2");
                        if (ucSearch1.LoadData(indexPath, _ParseSegPath))
                        {
                            ucSearch1.Visible = true;
                            ucSearch1.Dock = DockStyle.Fill;
                        }
                    }
                    else
                    {
                        indexPath = Path.Combine(_ParseSegPath, "Index");
                        if (ucSearch_hoot1.LoadData(indexPath, _ParseSegPath))
                        {
                            ucSearch_hoot1.Visible = true;
                            ucSearch_hoot1.Dock = DockStyle.Fill;
                        }
                    }

                    break;

            }
        }


        public void AdjustColumns()
        {
            Atebion.QC.Analysis analysis = new Atebion.QC.Analysis(AppFolders.AppDataPathToolsQC);

            if (!dvgParsedResults.Columns.Contains("Notes")) //Added 05.02.2014       
            {
                // DataGridViewColumn dgvcNotes = new System.Windows.Forms.DataGridViewImageCell; // new DataGridViewColumn();

                DataGridViewImageColumn imageCol = new DataGridViewImageColumn();

                imageCol.HeaderText = "";
                imageCol.Name = "Notes";
                //   imageCol.Width = 20;

                dvgParsedResults.Columns.Add(imageCol);

            }

            dvgParsedResults.Columns["Notes"].Visible = true;

            // Hide columns
            dvgParsedResults.Columns[Atebion.QC.QCParseResultsFields.UID].Visible = false;
            dvgParsedResults.Columns[Atebion.QC.QCParseResultsFields.Weight].Visible = false;
            dvgParsedResults.Columns[Atebion.QC.QCParseResultsFields.Words].Visible = false;
            dvgParsedResults.Columns[Atebion.QC.QCParseResultsFields.Sentences].Visible = false;
          //  dvgParsedResults.Columns[Atebion.QC.QCParseResultsFields.DictionaryTerms].Visible = false;

            dvgParsedResults.Columns[QCParseResultsFields.LongSentences].HeaderText = "Long Sentences";
            dvgParsedResults.Columns[QCParseResultsFields.DictionaryTerms].HeaderText = "Dictionary Terms";
            dvgParsedResults.Columns[QCParseResultsFields.ComplexWords].HeaderText = "Complex Words";
            dvgParsedResults.Columns[QCParseResultsFields.PassiveVoice].HeaderText = "Passive Voice";
            dvgParsedResults.Columns[Atebion.QC.QCParseResultsFields.DictionaryTerms].HeaderText = "Dictionary Terms";

            foreach (DataGridViewColumn col in dvgParsedResults.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            dvgParsedResults.ColumnHeadersDefaultCellStyle.BackColor = Color.PowderBlue;

            dvgParsedResults.Columns[Atebion.QC.QCParseResultsFields.Rank].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dvgParsedResults.Columns[Atebion.QC.QCParseResultsFields.Weight].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (dvgParsedResults.Columns.Contains(Atebion.QC.QCParseResultsFields.Page))
            {
                dvgParsedResults.Columns[Atebion.QC.QCParseResultsFields.Page].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            dvgParsedResults.Columns[Atebion.QC.QCParseResultsFields.Readability].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dvgParsedResults.Columns[Atebion.QC.QCParseResultsFields.LongSentences].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dvgParsedResults.Columns[Atebion.QC.QCParseResultsFields.ComplexWords].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dvgParsedResults.Columns[Atebion.QC.QCParseResultsFields.PassiveVoice].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dvgParsedResults.Columns[Atebion.QC.QCParseResultsFields.Adverbs].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dvgParsedResults.Columns[Atebion.QC.QCParseResultsFields.DictionaryTerms].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

          //  dvgParsedResults.Columns[Atebion.QC.QCParseResultsFields.Weight].Width = 30;
            dvgParsedResults.Columns[Atebion.QC.QCParseResultsFields.Rank].Width = 25;
            dvgParsedResults.Columns[Atebion.QC.QCParseResultsFields.Number].Width = 30;
            dvgParsedResults.Columns[Atebion.QC.QCParseResultsFields.Caption].Width = 50;
            if (dvgParsedResults.Columns.Contains(Atebion.QC.QCParseResultsFields.Page))
            {
                dvgParsedResults.Columns[Atebion.QC.QCParseResultsFields.Page].Width = 25;
            }

            dvgParsedResults.Columns[Atebion.QC.QCParseResultsFields.Readability].Width = 30;
            dvgParsedResults.Columns[Atebion.QC.QCParseResultsFields.LongSentences].Width = 30;
            dvgParsedResults.Columns[Atebion.QC.QCParseResultsFields.ComplexWords].Width = 30;
            dvgParsedResults.Columns[Atebion.QC.QCParseResultsFields.PassiveVoice].Width = 30;
            dvgParsedResults.Columns[Atebion.QC.QCParseResultsFields.Adverbs].Width = 30;
            dvgParsedResults.Columns[Atebion.QC.QCParseResultsFields.DictionaryTerms].Width = 30;
            dvgParsedResults.Columns["Notes"].Width = 20;

  

            dvgParsedResults.AllowUserToAddRows = false;  // Remove last blank row

            setRankFont();
            setQualityBackColor("Flesch Reading Ease");
           // setWeightColors();

            foreach (DataGridViewRow row in dvgParsedResults.Rows)
            {
                // Adverb Back Color
                if (dvgParsedResults.Columns.Contains(Atebion.QC.QCParseResultsFields.Adverbs))
                {
                    if (row.Cells[Atebion.QC.QCParseResultsFields.Adverbs].Value != null && row.Cells[Atebion.QC.QCParseResultsFields.Adverbs].Value.ToString() != string.Empty)
                    {
                        row.Cells[Atebion.QC.QCParseResultsFields.Adverbs].Style.BackColor = Color.FromName(analysis.Color_Adverbs);
                    }
                }

                // Complex Words Back Color
                if (dvgParsedResults.Columns.Contains(Atebion.QC.QCParseResultsFields.ComplexWords))
                {
                    if (row.Cells[Atebion.QC.QCParseResultsFields.ComplexWords].Value != null && row.Cells[Atebion.QC.QCParseResultsFields.ComplexWords].Value.ToString() != string.Empty)
                    {
                        row.Cells[Atebion.QC.QCParseResultsFields.ComplexWords].Style.BackColor = Color.FromName(analysis.Color_ComplexWords);
                    }
                }

                // Dictionary Terms Back Color
                if (dvgParsedResults.Columns.Contains(Atebion.QC.QCParseResultsFields.DictionaryTerms))
                {
                    if (row.Cells[Atebion.QC.QCParseResultsFields.DictionaryTerms].Value != null && row.Cells[Atebion.QC.QCParseResultsFields.DictionaryTerms].Value.ToString() != string.Empty)
                    {
                        row.Cells[Atebion.QC.QCParseResultsFields.DictionaryTerms].Style.BackColor = Color.FromName(analysis.Color_DictionaryTerms);
                    }
                }

                // Long Sentences Back Color
                if (dvgParsedResults.Columns.Contains(Atebion.QC.QCParseResultsFields.LongSentences))
                {
                    if (row.Cells[Atebion.QC.QCParseResultsFields.LongSentences].Value != null && row.Cells[Atebion.QC.QCParseResultsFields.LongSentences].Value.ToString() != string.Empty)
                    {
                        row.Cells[Atebion.QC.QCParseResultsFields.LongSentences].Style.BackColor = Color.FromName(analysis.Color_LongSentence);
                    }
                }

                // Passive Voice Back Color
                if (dvgParsedResults.Columns.Contains(Atebion.QC.QCParseResultsFields.PassiveVoice))
                {
                    if (row.Cells[Atebion.QC.QCParseResultsFields.PassiveVoice].Value != null && row.Cells[Atebion.QC.QCParseResultsFields.PassiveVoice].Value.ToString() != string.Empty)
                    {
                        row.Cells[Atebion.QC.QCParseResultsFields.PassiveVoice].Style.BackColor = Color.FromName(analysis.Color_PassiveVoice);
                    }
                }


            }

        }

        private void setRankFont()
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Font = new Font(dvgParsedResults.Font.FontFamily, 10, FontStyle.Bold);
            foreach (DataGridViewRow row in dvgParsedResults.Rows)
            {
                if (row.Cells[Atebion.QC.QCParseResultsFields.Rank].Value != null)
                {
                    row.Cells[Atebion.QC.QCParseResultsFields.Rank].Style.ApplyStyle(style);  
                }
            }

        }

        private void setWeightColors()
        {
            Double dblWeight;
            foreach (DataGridViewRow row in dvgParsedResults.Rows)
            {
                if (row.Cells[Atebion.QC.QCParseResultsFields.Weight].Value != null && row.Cells[Atebion.QC.QCParseResultsFields.Weight].Value.ToString() != string.Empty)
                {
                    dblWeight = Convert.ToDouble(row.Cells[Atebion.QC.QCParseResultsFields.Weight].Value);
                    if (dblWeight < 2.00)
                    {
                        row.Cells[Atebion.QC.QCParseResultsFields.Weight].Style.BackColor = Color.DarkGreen;
                        row.Cells[Atebion.QC.QCParseResultsFields.Weight].Style.ForeColor = Color.White;
                    }
                    else if (dblWeight > 1.99 && dblWeight < 3.00)
                    {
                        row.Cells[Atebion.QC.QCParseResultsFields.Weight].Style.BackColor = Color.Blue;
                        row.Cells[Atebion.QC.QCParseResultsFields.Weight].Style.ForeColor = Color.White;
                    }
                    else if (dblWeight > 2.99 && dblWeight < 4.00)
                    {
                        row.Cells[Atebion.QC.QCParseResultsFields.Weight].Style.BackColor = Color.Yellow;
                        row.Cells[Atebion.QC.QCParseResultsFields.Weight].Style.ForeColor = Color.Black;
                    }
                    else if (dblWeight > 3.99 && dblWeight < 5.00)
                    {
                        row.Cells[Atebion.QC.QCParseResultsFields.Weight].Style.BackColor = Color.Orange;
                        row.Cells[Atebion.QC.QCParseResultsFields.Weight].Style.ForeColor = Color.Black;
                    }
                    else if (dblWeight > 4.99)
                    {
                        row.Cells[Atebion.QC.QCParseResultsFields.Weight].Style.BackColor = Color.Red;
                        row.Cells[Atebion.QC.QCParseResultsFields.Weight].Style.ForeColor = Color.White;
                    }

                }

            }
            
        }

        private void setQualityBackColor(string QualityType) // Added 05.07.2014
        {
           // string SentenceColor = ucQualityAnalyzer1.LongSentenceColor;
            Double dblQuality;

            foreach (DataGridViewRow row in dvgParsedResults.Rows)
            {
                //if (dvgParsedResults.Columns.Contains(_LongQty)) // Check due to older versions
                //{
                //    if (row.Cells[_LongQty].Value.ToString().Length > 0)
                //    {
                //        row.Cells[_LongQty].Style.BackColor = Color.FromName(SentenceColor);
                //    }
                //}

                if (row.Cells[Atebion.QC.QCParseResultsFields.Readability].Value != null)
                {
                    dblQuality = Convert.ToDouble(row.Cells[Atebion.QC.QCParseResultsFields.Readability].Value);
                    if (QualityType == "Flesch Reading Ease")
                    {
                        if (dblQuality < 30)
                            row.Cells[Atebion.QC.QCParseResultsFields.Readability].Style.BackColor = Color.Red;
                        else if (dblQuality < 40 && dblQuality >= 29.99)
                            row.Cells[Atebion.QC.QCParseResultsFields.Readability].Style.BackColor = Color.Salmon;
                        else if (dblQuality < 50 && dblQuality >= 39.99)
                            row.Cells[Atebion.QC.QCParseResultsFields.Readability].Style.BackColor = Color.Salmon; //Gold
                        else if (dblQuality < 60 && dblQuality >= 49.99)
                            row.Cells[Atebion.QC.QCParseResultsFields.Readability].Style.BackColor = Color.Yellow;
                        else if (dblQuality < 70 && dblQuality >= 59.99)
                            row.Cells[Atebion.QC.QCParseResultsFields.Readability].Style.BackColor = Color.GreenYellow;
                        else if (dblQuality < 80 && dblQuality >= 69.99)
                            row.Cells[Atebion.QC.QCParseResultsFields.Readability].Style.BackColor = Color.LightGreen;
                        else if (dblQuality < 90 && dblQuality >= 79.99)
                        {
                            row.Cells[Atebion.QC.QCParseResultsFields.Readability].Style.BackColor = Color.Green;
                            row.Cells[Atebion.QC.QCParseResultsFields.Readability].Style.ForeColor = Color.White;
                        }
                        else if (dblQuality >= 89.99)
                        {
                            row.Cells[Atebion.QC.QCParseResultsFields.Readability].Style.BackColor = Color.DarkGreen;
                            row.Cells[Atebion.QC.QCParseResultsFields.Readability].Style.ForeColor = Color.White;
                        }

                    }
                    else // Flesch-Kincaid Grade Level
                    {
                        if (dblQuality > 12)
                            row.Cells[Atebion.QC.QCParseResultsFields.Readability].Style.BackColor = Color.Red;
                        else if (dblQuality < 13 && dblQuality > 6)
                            row.Cells[Atebion.QC.QCParseResultsFields.Readability].Style.BackColor = Color.Yellow;
                        else
                            row.Cells[Atebion.QC.QCParseResultsFields.Readability].Style.BackColor = Color.Green;
                    }
                }
            }


        }

        private bool LoadAnalysisResults()
        {
            dvgParsedResults.Columns.Clear();

            GenericDataManger gDataMgr = new GenericDataManger();
            _dsParseResults = gDataMgr.LoadDatasetFromXml(_QCParseResultsPathFile);

            dvgParsedResults.DataSource = _dsParseResults.Tables[0];

            if (_dsParseResults == null)
                return false;

            GetAvgRankReadability();

            return true;
        }

        private bool GetAvgRankReadability()
        {
            List<double> weightList = new List<double>();
            double weight = 0;

            List<double> readabilityList = new List<double>();
            double readability = 0;

            foreach (DataRow row in _dsParseResults.Tables[0].Rows)
            {
                if (row[QCParseResultsFields.Weight].ToString() != string.Empty)
                {
                    weight = Convert.ToDouble(row[QCParseResultsFields.Weight].ToString());
                    weightList.Add(weight);
                }

                if (row[QCParseResultsFields.Readability].ToString() != string.Empty)
                {
                    readability = Convert.ToDouble(row[QCParseResultsFields.Readability].ToString());
                    readabilityList.Add(readability);
                }
            }

            double readabilityAvg = readabilityList.Average();
            readabilityAvg = Math.Round(readabilityAvg, 2);
            double weightAvg = weightList.Average();

            Atebion.QC.Analysis qcAnalysis = new Analysis();
            string rank = qcAnalysis.GetWeightRank(weightAvg);
            lblRankAvg.Text = rank;

            string gradeLevel = string.Empty;
            string color = string.Empty;
            _ReadabilityLevel = string.Empty;
            _ReadabilityLevel = qcAnalysis.GetReadablilityLevel(readabilityAvg, out gradeLevel, out color);

            _ReadabilityAvg = readabilityAvg.ToString();

            panReadabilityAvg.BackColor = Color.FromName(color);
            if (color == "Green" || color == "DarkGreen")
            {
                lblReadabilityAvg.ForeColor = Color.White;
            }
            else
            {
                lblReadabilityAvg.ForeColor = Color.Black;
            }

            lblReadabilityAvgLevel.Text = _ReadabilityLevel;
            
            //if (readabilityAvg < 30)
            //    this.panReadabilityAvg.BackColor = Color.Red;
            //else if (readabilityAvg < 40 && readabilityAvg >= 29.99)
            //    this.panReadabilityAvg.BackColor = Color.Salmon;
            //else if (readabilityAvg < 50 && readabilityAvg >= 39.99)
            //    this.panReadabilityAvg.BackColor = Color.Salmon; //Gold
            //else if (readabilityAvg < 60 && readabilityAvg >= 49.99)
            //    this.panReadabilityAvg.BackColor = Color.Yellow;
            //else if (readabilityAvg < 70 && readabilityAvg >= 59.99)
            //    this.panReadabilityAvg.BackColor = Color.GreenYellow;
            //else if (readabilityAvg < 80 && readabilityAvg >= 69.99)
            //    this.panReadabilityAvg.BackColor = Color.LightGreen;
            //else if (readabilityAvg < 90 && readabilityAvg >= 79.99)
            //{
            //    this.panReadabilityAvg.BackColor = Color.Green;
            //    this.lblReadabilityAvg.ForeColor = Color.White;
            //}
            //else if (readabilityAvg >= 89.99)
            //{
            //    this.panReadabilityAvg.BackColor = Color.DarkGreen;
            //    lblReadabilityAvg.ForeColor = Color.White;
            //}

            lblReadabilityAvgValue.Text = readabilityAvg.ToString();

            return true;

        }

        private bool CheckFoldersFile()
        {

            string msg = string.Empty;

            // Check Directories
            if (!Directory.Exists(_ParseSegPath))
            {
                msg = string.Concat("Unable to find QC Parse Segment folder: ", _ParseSegPath);
                MessageBox.Show(msg, "Folder NOT Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (!Directory.Exists(_XMLPath))
            {
                msg = string.Concat("Unable to find QC XML folder: ", _XMLPath);
                MessageBox.Show(msg, "Folder NOT Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;

            }

            _ChartPicsPath = Path.Combine(_ParseSegPath, "ChartPics");
            if (!Directory.Exists(_ChartPicsPath))
            {
                try
                {
                    Directory.CreateDirectory(_ChartPicsPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Unable to Create ChartPics Folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            //if (!Directory.Exists(_LsQCPath))
            //{
            //    msg = string.Concat("Unable to find QC Long Sentences folder: ", _LsQCPath);
            //    MessageBox.Show(msg, "Folder NOT Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return false;
            //}

            // Check for QC Files
            _QCIssuesPathFile = Path.Combine(_XMLPath, _QCIssuesFile);
            if (!File.Exists(_QCIssuesPathFile))
            {
                msg = string.Concat("Unable to find QC Issues Found file: ", _QCIssuesPathFile);
                MessageBox.Show(msg, "File NOT Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            _QCParseResultsPathFile = Path.Combine(_XMLPath, _QCParseResultsFile);
            if (!File.Exists(_QCParseResultsPathFile))
            {
                msg = string.Concat("Unable to find QC Analysis Results file: ", _QCParseResultsPathFile);
                MessageBox.Show(msg, "File NOT Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            _QCReadabilitySumPathFile = Path.Combine(_XMLPath, _QCReadabilitySumFile);
            if (!File.Exists(_QCReadabilitySumPathFile))
            {
                msg = string.Concat("Unable to find QC Readablitiy Summary file: ", _QCReadabilitySumPathFile);
                MessageBox.Show(msg, "File NOT Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;

            }

            _QCStatsPathFile = Path.Combine(_XMLPath, _QCStatsFile);
            if (!File.Exists(_QCStatsPathFile))
            {
                msg = string.Concat("Unable to find QC Stats file: ", _QCStatsPathFile);
                MessageBox.Show(msg, "File NOT Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            return true;
        }

        private void ucQCIssues1_Load(object sender, EventArgs e)
        {

        }

        private void dvgParsedResults_SelectionChanged(object sender, EventArgs e)
        {
            ShowParsedDataPerCurrentRow();
        }

        private void ShowParsedDataPerCurrentRow()
        {
            Cursor.Current = Cursors.WaitCursor; // Waiting 

            //  butSpeacker.Visible = false;

            DataGridViewRow row = dvgParsedResults.CurrentRow;

            if (row == null) // Check, sometimes data has not been loaded yet
            {
                Cursor.Current = Cursors.Default; // Back to normal
                return;
            }

            _UID = row.Cells["UID"].Value.ToString();
            if (_UID == string.Empty)
            {
                richTextBox1.Text = string.Empty;
                Cursor.Current = Cursors.Default; // Back to normal
                return; // Most likely last row, which is empty
            }

            //string file = row.Cells["FileName"].Value.ToString();
            //if (file == string.Empty)
            //{
                string file = string.Concat(_UID, ".rtf");
            //}

                _FileName = Path.Combine(_ParseSegPath, file); // ToDo: Fix Error -- Null --Combine: When clicking on the Cancel button                 

            if (File.Exists(_FileName))
            {

                if (Files.FileIsLocked(_FileName)) // Added 11.02.2013
                {
                    string msg = "The selected document is currently opened by another application. Please close this document file and try again.";
                    MessageBox.Show(msg, "Unable to Open this Document", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    richTextBox1.Text = msg;

                    return;

                }

                richTextBox1.LoadFile(_FileName);

                if (ucNotes1.Visible)
                    ucNotes1.UID = _UID;



                // Check to see if Siarad exists, show button if it does 
                //string appPath = System.Windows.Forms.Application.StartupPath;
                //_Siarad = string.Concat(appPath, @"\Siarad.exe");
                //if (File.Exists(_Siarad))
                //{
                //    butSpeacker.Visible = true;
                //}
                //else
                //{
                //    butSpeacker.Visible = false;
                //}
            }
            else
            {
                richTextBox1.Text = string.Empty;
                richTextBox1.Text = string.Concat("Error: Cannot find file: ", _FileName);

            }

            // Make Text Larger -- Easier to Read
            richTextBox1.SelectAll();
            richTextBox1.SelectionFont = new Font("Segoe UI", 12);
            richTextBox1.DeselectAll();


            // Check if there is a Warning for the selected section
            //  string resultsUID = row.Cells["UID"].Value.ToString();

            this.ucNotes1.UID = _UID.ToString();

            this.ucQCIssues1.LoadSelectedIssues(Convert.ToInt32(_UID));
          //  this.ucQCIssues1.AdjustColumns();

            //if (this.chkDocView.Checked)
            //{
            //    if (_frmDocumentView == null)
            //    {
            //        this.chkDocView.Checked = false;
            //        return;
            //    }

            //    if (!CheckOpened("Document View"))
            //    {
            //        this.chkDocView.Checked = false;
            //        return;
            //    }

            //    int line = 0;
            //    //if (row.Cells["LineStart"].Value != null)
            //    if (row.Cells["LineStart"].Value.ToString() != string.Empty) // Changed on 12.19.2018
            //    {
            //        line = Convert.ToInt32(row.Cells["LineStart"].Value);
            //    }
            //    line++; // parser engine is zero based, while document viewer is one base. Therefore add one.


            //    _frmDocumentView.ShowSegment(line, richTextBox1.Text);
            //}

            Cursor.Current = Cursors.Default; // Back to normal

        }

        private void dvgParsedResults_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dvgParsedResults.Columns[e.ColumnIndex].Name == "Notes")
            {

                if (dvgParsedResults.Rows[e.RowIndex].Cells["UID"].Value == null)
                    return;

                string sUID = dvgParsedResults.Rows[e.RowIndex].Cells["UID"].Value.ToString();


                string noteFile = string.Concat(sUID, _NotesPostFix, ".rtf");
                string notePathFile = Path.Combine(_PathNotes, noteFile);
                if (File.Exists(notePathFile))
                {
                    e.Value = _Notepad;
                }
                else
                {
                    dvgParsedResults.Columns["Notes"].DefaultCellStyle.NullValue = null;
                }
            } 
        
        }

        private void butCharts_Click(object sender, EventArgs e)
        {
            _CurrentSubpanel = subpanels.Charts;
            AdjustSubpanels();
        }

        private void butIssue_Click(object sender, EventArgs e)
        {
            _CurrentSubpanel = subpanels.Issues;
            AdjustSubpanels();
        }

        private void butGenerateReport_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor; // Waiting 

            ExportManager eMgr = new ExportManager();

       //     string projName = "Project Testc 1"; // ToDo Test Real Project
      //      string exportFile = "Test1"; // ToDo auto name

            if (dvgParsedResults.DataSource == null)
                return;

            DataTable dtSource;

            try
            {
                dtSource = (DataTable)dvgParsedResults.DataSource;
            }
            catch
            {
                try
                {
                    DataView dvSource = (DataView)dvgParsedResults.DataSource;
                    dtSource = dvSource.ToTable();
                }
                catch
                {
                    return;
                }
                     
            }

            eMgr.ExportQCAnalysisResults(_ProjectName, _SourceDoc, dtSource, ucQCIssues1.dsQCIssues, _ParseSegPath, _ChartPicsPath, lblRankAvg.Text, _Color_LongSentence, _Color_ComplexWords, _Color_PassiveVoice, _Color_Adverbs, _Color_DictionaryTerms, _ReadabilityAvg, _TotalLongSentence, _TotalComplexWords, _TotalPassiveVoice, _TotalAdverbs, _TotalDictionaryTerms);


            Cursor.Current = Cursors.Default; // Back to normal

            

        }

        private void butNotes_Click(object sender, EventArgs e)
        {
            _CurrentSubpanel = subpanels.Notes;
            AdjustSubpanels();
        }

        private void butExported_Click(object sender, EventArgs e)
        {
            _CurrentSubpanel = subpanels.Reports;
            AdjustSubpanels();
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            _CurrentSubpanel = subpanels.Search;
            AdjustSubpanels();
        }

        private void SearchFilter(string[] UIDResults, string SearchCriteria)
        {
            _SearchCriteria = SearchCriteria;

            string filter = string.Empty;
            int i = 0;
            foreach (string s in UIDResults)
            {
                if (i == 0)
                    filter = s;
                else
                    filter = string.Concat(filter, ", ", s);

                i++;
            }

            filter = string.Concat("UID IN (", filter, ")");

            _dv = new DataView(_dsParseResults.Tables[0]);

            _dv.RowFilter = filter;
            _dv.Sort = "UID ASC";

            this.dvgParsedResults.DataSource = _dv;

            butRefresh.Visible = true;

            lblMessage.Text = string.Format("Search found {0} Segments for Search Criteria: '{1}'", _FoundQty.ToString(), _SearchCriteria);

            Application.DoEvents();

            AdjustColumns();

            this.Refresh();
        }

        private void ucSearch1_SearchCompleted()
        {
            _FoundQty = ucSearch1.FoundQty;
            if (_FoundQty == 0)
                return;

            string[] UIDResults = ucSearch1.FoundResults;

            string searchCriteria = ucSearch1.SearchCriteria;

            SearchFilter(UIDResults, searchCriteria);

            //string filter = string.Empty;
            //int i = 0;
            //foreach (string s in UIDResults)
            //{
            //    if (i == 0)
            //        filter = s;
            //    else
            //        filter = string.Concat(filter, ", ", s);

            //    i++;
            //}

            //filter = string.Concat("UID IN (", filter, ")");

            //_dv = new DataView(_dsParseResults.Tables[0]);

            //_dv.RowFilter = filter;
            //_dv.Sort = "UID ASC";

            //this.dvgParsedResults.DataSource = _dv;

            ////butSplit.Visible = false;
            ////butCombine.Visible = false;
            ////butEdit.Visible = false;
            ////butExport.Visible = false;
            ////butDelete.Visible = false;

            //butRefresh.Visible = true;

            //lblMessage.Text = string.Format("Search found {0} Segments for Search Criteria: '{1}'", _FoundQty.ToString(), _SearchCriteria);

            //Application.DoEvents();

            //AdjustColumns();

            //this.Refresh();
        }

        private void ShowAllResults()
        {
            LoadAnalysisResults();
            butRefresh.Visible = false;
            lblMessage.Text = string.Empty;

            lblMessage.Text = string.Concat("Quantity of Segments: ", dvgParsedResults.RowCount.ToString());

            AdjustColumns(); 

            this.Refresh();
        }

        private void butRefresh_Click(object sender, EventArgs e)
        {
            ShowAllResults();

            if (!AppFolders.UseHootSearchEngine)
                ucSearch1.ShowInstructions();
            else
                ucSearch_hoot1.ShowInstructions();
        }

        private void butReadabilityInfo_Click(object sender, EventArgs e)
        {
            frmReadabilityRef frm = new frmReadabilityRef();
            frm.LoadData();
            frm.ShowDialog(this);
        }

        private void cboShow_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isLoaded)
                return;

            string filter = string.Empty;

            string selection = cboShow.Text;

            switch (selection)
            {
                case "All":
                    ShowAllResults();

                    if (!AppFolders.UseHootSearchEngine)
                        ucSearch1.ShowInstructions();
                    else
                        ucSearch_hoot1.ShowInstructions();

                    return;

                case "Less Than A":
                    filter = "'B', 'C', 'D', 'F'";
                    break;

                case "Less Than B":
                    filter = "'C', 'D', 'F'";
                    break;

                case "Less Than C":
                    filter = "'D', 'F'";
                    break;

                case "Less Than D":
                    filter = "'F'";
                    break;

            }


            filter = string.Concat("Rank IN (", filter, ")");

            _dv = new DataView(_dsParseResults.Tables[0]);

            _dv.RowFilter = filter;
            _dv.Sort = "UID ASC";

            this.dvgParsedResults.DataSource = _dv;

            if (selection == "All")
                butRefresh.Visible = false;
            else
            {
                butRefresh.Visible = true;

                butRefresh.Visible = false;
                AdjustColumns();

                this.Refresh();
            }

        }

        private void ucSearch_hoot1_SearchCompleted()
        {
            _FoundQty = ucSearch_hoot1.FoundQty;
            if (_FoundQty == 0)
                return;

            string[] UIDResults = ucSearch_hoot1.FoundResults;

            string searchCriteria = ucSearch_hoot1.SearchCriteria;

            SearchFilter(UIDResults, searchCriteria);
        }


    }
}
