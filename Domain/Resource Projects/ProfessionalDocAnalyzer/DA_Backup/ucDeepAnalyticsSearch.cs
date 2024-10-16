using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.Diagnostics;
using System.IO;
using Atebion.DeepAnalytics;
using Atebion.Common;

using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;

using Lucene.Net.Util;

namespace ProfessionalDocAnalyzer
{
    public partial class ucDeepAnalyticsSearch : UserControl
    {
        public ucDeepAnalyticsSearch()
        {
            InitializeComponent();
        }

        // Query Ref.: https://lucene.apache.org/core/2_9_4/queryparsersyntax.html

        // Lucene vars
        private Analyzer analyzer = new StandardAnalyzer();
        private Lucene.Net.Store.Directory luceneIndexDirectory;
        private IndexWriter writer;
        private string indexPath = string.Empty;

        // Declare delegate for when search has completed
        public delegate void ProcessHandler();

        [Category("Action")]
        [Description("Fires when the Search has completed")]
        public event ProcessHandler SearchCompleted;

        private const string _newLine = "\r\n";
      //  private Hoot hoot;

        private Analysis _DeepAnalysis = new Analysis();

        private string[] _Found;
        private int _FoundQty = 0;
        private string _SearchCriteria = string.Empty;

        public string[] FoundResults
        {
            get { return _Found; }
        }

        public int FoundQty
        {
            get { return _FoundQty; }
        }

        public string SearchCriteria
        {
            get { return _SearchCriteria; }
        }




        public void LoadData()
        {
            lblInformation.Text = string.Concat
                (
                "Wildcards:",
                     _newLine,
                    "  ? = Matches any single character.",
                    _newLine,
                    "  * = Matches any one or more characters. For example, bet* matches any text that includes 'bet', such as 'better'.",
                    _newLine,
                    _newLine,
                    "The AND operator matches content where both terms exist anywhere in the text (e.g. Should AND Require, finds both)",
                    _newLine,
                     _newLine,
                    "The OR operator is the default conjunction operator.",
                    _newLine,
                    "This means that if there is no Boolean operator between two terms, the OR operator is used. (e.g. Should OR Require, finds either)",
                    _newLine,
                     _newLine,
                    "The NOT operator excludes content that contain the term after NOT. (e.g. Should Not Require)",
                    _newLine,
                    _newLine,
                    "Grouping example: (Should OR Require) AND Must",
                    _newLine,
                     _newLine,
                    @"A Phrase is a group of words surrounded by double quotes such as ""Should Apply"".",
                    _newLine,
                    "    Phrase searching is supported within Deep Analysis, but not supported in the previous Analysis Results."

 
                );

            _DeepAnalysis.CurrentDocPath = AppFolders.CurrentDocPath;

           // ChkRunIndexer();
        }

        //private void ChkRunIndexer()
        //{
        //    DateTime dtIndex = Files.GetLatestFileDatetime(AppFolders.DocParsedSecIndex);
        //    DateTime dtParsedFiles = Files.GetLatestFileDatetime(AppFolders.DocParsedSec);

        //    if (hoot != null)
        //    {

        //    //    hoot.Shutdown();
        //    //    hoot.FreeMemory();
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

        //        hoot = new Hoot(_DeepAnalysis.IndexPath, "index", true);

        //        RunIndexer();

        //        //if (dtIndex == null)
        //        //{
        //        //    RunIndexer();
        //        //}
        //        //else
        //        //{
        //        //    int result = DateTime.Compare(dtIndex, dtParsedFiles);
        //        //    if (result <= 0) // "is earlier than"
        //        //        RunIndexer();
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        lblInformation.Text = string.Concat("Search Loading Error: ", ex.Message);
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
        //        hoot = new Hoot(_DeepAnalysis.IndexPath, "index", true);

        //    string[] files = Directory.GetFiles(_DeepAnalysis.ParseSentencesPath, "*", SearchOption.TopDirectoryOnly);


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

        //    Cursor.Current = Cursors.Default; // Back to normal
        //}


        private void ucSearch_Resize(object sender, EventArgs e)
        {
            // 7/2/2017
            //if (this.Height > 127)
            //{
            //    txtbInformation.Height = this.Height - 127;
            //    txtbInformation.Visible = true;
            //}
            //else
            //{
            //    txtbInformation.Visible = false;
            //    this.listBox1.Visible = false;
            //}
        }

        private void lblInformation_TextChanged(object sender, EventArgs e)
        {
            txtbInformation.Text = lblInformation.Text;
        }

        private void txtbInformation_TextChanged(object sender, EventArgs e)
        {
            txtbInformation.Text = lblInformation.Text;
        }

        private void picHeader_Click(object sender, EventArgs e)
        {
            if (this.Height > 127)
            {
                if (txtbInformation.Visible == true)
                {
                    txtbInformation.Visible = false;
                    txtbInformation.Dock = DockStyle.None;
                    this.listBox1.Dock = DockStyle.Bottom;
                    this.listBox1.Height = this.Height - 127;
                    this.listBox1.Visible = true;
                }
                else
                {
                    listBox1.Visible = false;
                    listBox1.Dock = DockStyle.None;
                    this.txtbInformation.Dock = DockStyle.Bottom;
                    this.txtbInformation.Height = this.Height - 127;
                    this.txtbInformation.Visible = true;
                }

            }
            else
            {
                txtbInformation.Visible = false;
                this.listBox1.Visible = false;
            }
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            try
            {
                  //ChkRunIndexer(); // Added 07.12.2014 -- Re-Index every time

                //if (hoot == null)
                //{
 
                //    hoot = new Hoot(_DeepAnalysis.IndexPath, "index", true);
                //    //MessageBox.Show("hOOt not loaded");
                //    //return;
                //}

                listBox1.Items.Clear();
                DateTime dt = DateTime.Now;

                SearchText3(this.txtbSearch.Text.Trim());

                //listBox1.BeginUpdate();

                //string fileName = string.Empty;
                //foreach (var d in hoot.FindDocumentFileNames(txtbSearch.Text.Trim()))
                //{
                //    fileName = Files.GetFileNameWOExt(d);
                //    listBox1.Items.Add(fileName);
                //}

                _Found = new string[listBox1.Items.Count];
                listBox1.Items.CopyTo(_Found, 0);
                _FoundQty = listBox1.Items.Count;

                listBox1.EndUpdate();
                lblInformation.Text = string.Concat("Found: ", _FoundQty.ToString(), _newLine, _newLine, "Seconds: ", DateTime.Now.Subtract(dt).TotalSeconds.ToString());


                _SearchCriteria = txtbSearch.Text;

                // 7.5.2015
                //hoot.Save();
                //hoot.Shutdown();
                //hoot.FreeMemory();
                //

                if (SearchCompleted != null)
                    SearchCompleted();
            }
            catch (Exception ex)
            {
                lblInformation.Text = string.Concat("Error: ", ex.Message);
            }
        }

        private void SearchText3(string strQuery)
        {
            if (luceneIndexDirectory == null)
            {

                indexPath = _DeepAnalysis.Index2Path;
                luceneIndexDirectory = FSDirectory.GetDirectory(indexPath, false);
            }


            IndexSearcher searcher = new IndexSearcher(luceneIndexDirectory);
            QueryParser parser = new QueryParser("SearchText", analyzer);

            Query query = parser.Parse(strQuery);
            Hits hitsFound = searcher.Search(query);
     //       List<SearchFileData> results = new List<SearchFileData>();
     //       SearchFileData DataFile = null;

            string fileName = string.Empty;
            listBox1.Items.Clear();

            for (int i = 0; i < hitsFound.Length(); i++)
            {
                //DataFile = new SearchFileData();
                Document doc = hitsFound.Doc(i);
                //DataFile.SearchFile = doc.Get("SearchFile");
                //DataFile.SearchText = doc.Get("SearchText");
                //float score = hitsFound.Score(i);
                //DataFile.Score = score;

                //results.Add(DataFile);

                fileName = doc.Get("SearchFile");
                fileName = Files.GetFileNameWOExt(fileName);
                listBox1.Items.Add(fileName);
            }

        }

        private void lblHeader_Click(object sender, EventArgs e)
        {
           // System.Diagnostics.Process.Start("explorer.exe", AppFolders.);
        }

        private void picHeader_Click_1(object sender, EventArgs e)
        {
          //  System.Diagnostics.Process.Start("explorer.exe", AppFolders.DocParsedSecIndex);

            if (listBox1.Visible == false)
                listBox1.Visible = true;
            else
                listBox1.Visible = false;
        }

        private void ucDeepAnalyticsSearch_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                txtbSearch.Focus();
            }
        }
    }
}
