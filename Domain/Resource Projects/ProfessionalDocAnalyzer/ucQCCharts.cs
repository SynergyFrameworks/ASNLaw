using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

//using System.Web.UI.DataVisualization.Charting;
using System.Windows.Forms.DataVisualization.Charting;

namespace ProfessionalDocAnalyzer
{
    public partial class ucQCCharts : UserControl
    {
        public ucQCCharts()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        // --- Readability ---

        private double _Readability_VeryEasy = 0;
        public double Readability_VeryEasy
        {
            get { return _Readability_VeryEasy; }
            set{_Readability_VeryEasy = value;}
        }

        private double _Readability_Easy = 0;
        public double Readability_Easy
        {
            get { return _Readability_Easy; }
            set { _Readability_Easy = value; }
        }

        private double _Readability_FairlyEasy = 0;
        public double Readability_FairlyEasy
        {
            get { return _Readability_FairlyEasy; }
            set { _Readability_FairlyEasy = value; }
        }

        private double _Readability_Standard = 0;
        public double Readability_Standard
        {
            get { return _Readability_Standard; }
            set { _Readability_Standard = value; }
        }

        private double _Readability_FairlyDifficult = 0;
        public double Readability_FairlyDifficult
        {
            get { return _Readability_FairlyDifficult; }
            set { _Readability_FairlyDifficult = value; }
        }

        private double _Readability_Difficult = 0;
        public double Readability_Difficult
        {
            get { return _Readability_Difficult; }
            set { _Readability_Difficult = value; }
        }

        private double _Readability_VeryConfusing = 0;
        public double Readability_VeryConfusing
        {
            get { return _Readability_VeryConfusing; }
            set { _Readability_VeryConfusing = value; }
        }


        private int _Sentences_Total = 0;
        public int Sentences_Total
        {
            get { return _Sentences_Total; }
            set { _Sentences_Total = value; }
        }


        // --- Sentences ---

        private int _Sentences_Long = 0;
        public int Sentences_Long
        {
            get { return _Sentences_Long; }
            set { _Sentences_Long = value; }
        }

        private string _Sentences_Long_Color = "Pink";
        public string Sentences_Long_Color
        {
            get { return _Sentences_Long_Color; }
            set { _Sentences_Long_Color = value; }
        }


        // --- Complex ---

        private int _Words_Complex = 0;
        public int Words_Complex
        {
            get { return _Words_Complex; }
            set { _Words_Complex = value; }
        }

        private int _Words_Total = 0;
        public int Words_Total
        {
            get { return _Words_Total; }
            set { _Words_Total = value; }
        }

        private string _Words_Complex_Color = "Pink";
        public string Words_Complex_Color
        {
            get { return _Words_Complex_Color; }
            set { _Words_Complex_Color = value; }
        }

        // --- Passive Voice ---

        private int _Passive_Voice = 0;
        public int Passive_Voice
        {
            get { return _Passive_Voice; }
            set { _Passive_Voice = value; }
        }

        private string _Passive_Voice_Color = "Pink";
        public string Passive_Voice_Color
        {
            get { return _Passive_Voice_Color; }
            set { _Passive_Voice_Color = value; }
        }

        // Adverbs
        private int _Adverbs_Total = 0;
        public int Adverbs_Total
        {
            get { return _Adverbs_Total; }
            set { _Adverbs_Total = value; }
        }

        private string _Adverbs_Color = "Pink";
        public string Adverbs_Color
        {
            get { return _Adverbs_Color; }
            set { _Adverbs_Color = value; }
        }

        // Dictionary Terms
        private int _DictionaryTerms_Total;
        public int DictionaryTerms_Total
        {
            get { return _DictionaryTerms_Total; }
            set { _DictionaryTerms_Total = value; }
        }

        private string _DictionaryTerms_Color = "Pink";
        public string DictionaryTerms_Color
        {
            get { return _DictionaryTerms_Color; }
            set { _DictionaryTerms_Color = value; }
        }

        public void LoadData(string ChartPicsPath)
        {

            // --- Readability ---
            chartReadability_Bar.Series.Clear();
            chartReadability_Bar.Series.Add("s1");
            chartReadability_Bar.Series["s1"].ChartType = SeriesChartType.Doughnut;
            chartReadability_Bar.Series["s1"].IsValueShownAsLabel = true;

            chartReadability_Bar.Series["s1"].Points.AddXY("Very Easy", _Readability_VeryEasy);
            chartReadability_Bar.Series["s1"].Points.AddXY("Easy", _Readability_Easy);
            chartReadability_Bar.Series["s1"].Points.AddXY("Fairly Easy", _Readability_FairlyEasy);
            chartReadability_Bar.Series["s1"].Points.AddXY("Standard", _Readability_Standard);
            chartReadability_Bar.Series["s1"].Points.AddXY("Fairly Difficult", _Readability_FairlyDifficult);
            chartReadability_Bar.Series["s1"].Points.AddXY("Difficult", _Readability_Difficult);
            chartReadability_Bar.Series["s1"].Points.AddXY("Very Confusing", _Readability_VeryConfusing);

            chartReadability_Bar.Series["s1"].Points[0].Color = Color.DarkGreen;     // Very Easy
            chartReadability_Bar.Series["s1"].Points[1].Color = Color.Green;         // Easy
            chartReadability_Bar.Series["s1"].Points[2].Color = Color.LightGreen;    // Fairly Easy
            chartReadability_Bar.Series["s1"].Points[3].Color = Color.GreenYellow;   // Standard
            chartReadability_Bar.Series["s1"].Points[4].Color = Color.Yellow;        // Fairly Difficult
            chartReadability_Bar.Series["s1"].Points[5].Color = Color.Salmon;        // Difficult
            chartReadability_Bar.Series["s1"].Points[6].Color = Color.Red;           // Very Confusing

            if (chartReadability_Bar.Titles.Count == 0)
                chartReadability_Bar.Titles.Add("Readability");

            // ---> End Readability <---


            // --- Long Sentences ---
            chartSentences_Pie.Series.Clear();
            chartSentences_Pie.Series.Add("s1");
            chartSentences_Pie.Series["s1"].ChartType = SeriesChartType.Doughnut;
            chartSentences_Pie.Series["s1"].IsValueShownAsLabel = true;

            chartSentences_Pie.Series["s1"].Points.AddXY("Long", _Sentences_Long);
            chartSentences_Pie.Series["s1"].Points.AddXY("Not Long", (_Sentences_Total -_Sentences_Long));

            chartSentences_Pie.Series["s1"].Points[0].Color = Color.FromName(_Sentences_Long_Color);     // Long
            chartSentences_Pie.Series["s1"].Points[1].Color = Color.WhiteSmoke;         // Not Long

            if (chartSentences_Pie.Titles.Count == 0)
                chartSentences_Pie.Titles.Add("Sentences");

            // ---> End Long Sentences <---


            // --- Complex Words --
            chartWords_Pie.Series.Clear();
            chartWords_Pie.Series.Add("s1");
            chartWords_Pie.Series["s1"].ChartType = SeriesChartType.Doughnut;
            chartWords_Pie.Series["s1"].IsValueShownAsLabel = true;

            chartWords_Pie.Series["s1"].Points.AddXY("Complex", _Words_Complex);
            chartWords_Pie.Series["s1"].Points.AddXY("Not Complex", (_Words_Total - _Words_Complex));

            chartWords_Pie.Series["s1"].Points[0].Color = Color.FromName(_Words_Complex_Color);     // Complex Word
            chartWords_Pie.Series["s1"].Points[1].Color = Color.WhiteSmoke;         // Not Complex Words

            if (chartWords_Pie.Titles.Count == 0)
                chartWords_Pie.Titles.Add("Complex Words");

            // ---> End Complex Words <--


            // Passive Voice
            chartPassiveVoice.Series.Clear();
            chartPassiveVoice.Series.Add("s1");
            chartPassiveVoice.Series["s1"].ChartType = SeriesChartType.Doughnut;
            chartPassiveVoice.Series["s1"].IsValueShownAsLabel = true;

            chartPassiveVoice.Series["s1"].Points.AddXY("Passive Voice", _Passive_Voice);
            chartPassiveVoice.Series["s1"].Points.AddXY("Not Passive Voice", (_Words_Total - _Passive_Voice));

            chartPassiveVoice.Series["s1"].Points[0].Color = Color.FromName(_Passive_Voice_Color);     // Passive Voice
            chartPassiveVoice.Series["s1"].Points[1].Color = Color.WhiteSmoke;         // Not Passive Voice

            if (chartPassiveVoice.Titles.Count == 0)
                chartPassiveVoice.Titles.Add("Passive Voice");

            // ---> End Passive Voice <--

            // Adverbs
            chartAdverbs.Series.Clear();
            chartAdverbs.Series.Add("s1");
            chartAdverbs.Series["s1"].ChartType = SeriesChartType.Doughnut;
            chartAdverbs.Series["s1"].IsValueShownAsLabel = true;

            chartAdverbs.Series["s1"].Points.AddXY("Adverbs", _Adverbs_Total);
            chartAdverbs.Series["s1"].Points.AddXY("Not Adverbs", (_Words_Total - _Adverbs_Total));

            chartAdverbs.Series["s1"].Points[0].Color = Color.FromName(_Adverbs_Color);     // Adverbs
            chartAdverbs.Series["s1"].Points[1].Color = Color.WhiteSmoke;         // Not Adverbs

            if (chartAdverbs.Titles.Count == 0)
                chartAdverbs.Titles.Add("Adverbs");

            // ---> End Adverbs <--

            // Dictionary Terms
            chartDictionaryTerms.Series.Clear();
            chartDictionaryTerms.Series.Add("s1");
            chartDictionaryTerms.Series["s1"].ChartType = SeriesChartType.Doughnut;
            chartDictionaryTerms.Series["s1"].IsValueShownAsLabel = true;

            chartDictionaryTerms.Series["s1"].Points.AddXY("Dictionary Terms", _DictionaryTerms_Total);
            chartDictionaryTerms.Series["s1"].Points.AddXY("Not Dictionary Terms", (_Words_Total - _DictionaryTerms_Total));

            chartDictionaryTerms.Series["s1"].Points[0].Color = Color.FromName(_DictionaryTerms_Color);     // Dictionary Terms
            chartDictionaryTerms.Series["s1"].Points[1].Color = Color.WhiteSmoke;         // Not Dictionary Terms

            if (chartDictionaryTerms.Titles.Count == 0)
                chartDictionaryTerms.Titles.Add("Dictionary Terms");

            if (!Directory.Exists(ChartPicsPath))
                return;
            

            // Save Charts into Images
            //     Readability
            string fileName = "R.png";
            string picPathFile = Path.Combine(ChartPicsPath, fileName);
            if (!File.Exists(picPathFile))
            {
                chartReadability_Bar.SaveImage(picPathFile, ChartImageFormat.Png);
            }

            //     Long Sentences
            fileName = "LS.png";
            picPathFile = Path.Combine(ChartPicsPath, fileName);
            if (!File.Exists(picPathFile))
            {
                chartSentences_Pie.SaveImage(picPathFile, ChartImageFormat.Png);
            }

            //     Complex Words
            fileName = "CW.png";
            picPathFile = Path.Combine(ChartPicsPath, fileName);
            if (!File.Exists(picPathFile))
            {
                chartWords_Pie.SaveImage(picPathFile, ChartImageFormat.Png);
            }

            //     Passive Voice
            fileName = "PV.png";
            picPathFile = Path.Combine(ChartPicsPath, fileName);
            if (!File.Exists(picPathFile))
            {
                chartPassiveVoice.SaveImage(picPathFile, ChartImageFormat.Png);
            }

            //     Adverbs
            fileName = "A.png";
            picPathFile = Path.Combine(ChartPicsPath, fileName);
            if (!File.Exists(picPathFile))
            {
                chartAdverbs.SaveImage(picPathFile, ChartImageFormat.Png);
            }


        }

        private double[] GetPointArray_Readability()
        {
            List<double> lstLevels = new List<double>();

            lstLevels.Add(_Readability_VeryEasy);
            lstLevels.Add(_Readability_Easy);
            lstLevels.Add(_Readability_FairlyEasy);
            lstLevels.Add(_Readability_Standard);
            lstLevels.Add(_Readability_FairlyDifficult);
            lstLevels.Add(_Readability_Difficult);
            lstLevels.Add(_Readability_VeryConfusing);


            return lstLevels.ToArray();
        }
    }
}
