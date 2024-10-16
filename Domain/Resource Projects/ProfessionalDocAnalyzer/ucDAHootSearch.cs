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

using hOOt;
using RaptorDB;

namespace ProfessionalDocAnalyzer
{
    public partial class ucDAHootSearch : UserControl
    {
        public ucDAHootSearch()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        // Declare delegate for when search has completed
        public delegate void ProcessHandler();

        [Category("Action")]
        [Description("Fires when the Search has completed")]
        public event ProcessHandler SearchCompleted;

        private const string _newLine = "\r\n";
        private Hoot hoot;

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
                    "Prefix (+) character for AND operations :",
                    _newLine,
                    "  +microsoft +testing Search for 'microsoft' and 'testing' and all words must exist",
                    _newLine,
                    _newLine,
                    "Prefix (-) for NOT operations: ",
                    _newLine,
                    "  microsoft -testing",
                    _newLine,
                    "  Search for 'microsoft' excluding 'testing'",
                    _newLine,
                    _newLine,
                    "Default is the OR operation:",
                    _newLine,
                    "  microsft testing",
                    _newLine,
                    "  Search for 'microsoft' or 'testing' and any word can exist "

                );

            _DeepAnalysis.CurrentDocPath = AppFolders.CurrentDocPath;

            ChkRunIndexer();
        }

        private void ChkRunIndexer()
        {

            DateTime dtIndex = Files.GetLatestFileDatetime(_DeepAnalysis.IndexPath);
            DateTime dtParsedFiles = Files.GetLatestFileDatetime(_DeepAnalysis.ParseSentencesPath);

            if (hoot != null)
            {

                //    hoot.Shutdown();
                //    hoot.FreeMemory();
                hoot = null;
            }

            // Delete all Index files -- Added 07.12.2014
            //System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(AppFolders.DocParsedSecIndex);

            //foreach (FileInfo file in downloadedMessageInfo.GetFiles())
            //{
            //    file.Delete();
            //}
            //


            try
            {

                hoot = new Hoot(_DeepAnalysis.IndexPath, "index", true);

                RunIndexer();

                //if (dtIndex == null)
                //{
                //    RunIndexer();
                //}
                //else
                //{
                //    int result = DateTime.Compare(dtIndex, dtParsedFiles);
                //    if (result <= 0) // "is earlier than"
                //        RunIndexer();
                //}
            }
            catch (Exception ex)
            {
                lblInformation.Text = string.Concat("Search Loading Error: ", ex.Message);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] files = e.Argument as string[];
            BackgroundWorker wrk = sender as BackgroundWorker;
            int i = 0;
            foreach (string fn in files)
            {
                if (wrk.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                backgroundWorker1.ReportProgress(1, fn);
                try
                {
                    if (hoot.IsIndexed(fn) == false)
                    {
                        TextReader tf = new EPocalipse.IFilter.FilterReader(fn);
                        string s = "";
                        if (tf != null)
                            s = tf.ReadToEnd();

                        hoot.Index(new Document(new FileInfo(fn), s), true);
                    }
                }
                catch { }
                i++;
                if (i > 1000)
                {
                    i = 0;
                    hoot.Save();
                }
            }
            hoot.Save();
            hoot.OptimizeIndex();
        }

        private void RunIndexer()
        {
            Cursor.Current = Cursors.WaitCursor; // Waiting

            if (hoot == null)
                hoot = new Hoot(_DeepAnalysis.IndexPath, "index", true);

            string[] files = Directory.GetFiles(_DeepAnalysis.ParseSentencesPath, "*", SearchOption.TopDirectoryOnly);


            //  backgroundWorker1.RunWorkerAsync(files);

            //string[] files = e.Argument as string[];
            //BackgroundWorker wrk = sender as BackgroundWorker;
            int i = 0;
            foreach (string fn in files)
            {
                //if (wrk.CancellationPending)
                //{
                //    e.Cancel = true;
                //    break;
                //}
                backgroundWorker1.ReportProgress(1, fn);
                try
                {
                    if (hoot.IsIndexed(fn) == false)
                    {
                        TextReader tf = new EPocalipse.IFilter.FilterReader(fn);
                        string s = "";
                        if (tf != null)
                            s = tf.ReadToEnd();

                        hoot.Index(new Document(new FileInfo(fn), s), true);
                    }
                }
                catch { }
                i++;
                if (i > 1000)
                {
                    i = 0;
                    hoot.Save();
                }
            }
            hoot.Save();
            hoot.OptimizeIndex();

            Cursor.Current = Cursors.Default; // Back to normal
        }

        private void lblInformation_TextChanged(object sender, EventArgs e)
        {
            txtbInformation.Text = lblInformation.Text;
        }

        private void txtbInformation_TextChanged(object sender, EventArgs e)
        {
            txtbInformation.Text = lblInformation.Text;
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //  ChkRunIndexer(); // Added 07.12.2014 -- Re-Index every time

                if (hoot == null)
                {
                    hoot = new Hoot(_DeepAnalysis.IndexPath, "index", true);
                    //MessageBox.Show("hOOt not loaded");
                    //return;
                }

                listBox1.Items.Clear();
                DateTime dt = DateTime.Now;
                listBox1.BeginUpdate();

                string fileName = string.Empty;
                foreach (var d in hoot.FindDocumentFileNames(txtbSearch.Text))
                {
                    fileName = Files.GetFileNameWOExt(d);
                    // if (DataFunctions.IsNumeric(fileName))
                    listBox1.Items.Add(fileName);
                }

                _Found = new string[listBox1.Items.Count];
                listBox1.Items.CopyTo(_Found, 0);
                _FoundQty = listBox1.Items.Count;

                listBox1.EndUpdate();
                lblInformation.Text = string.Concat("Found: ", _FoundQty.ToString(), _newLine, _newLine, "Seconds: ", DateTime.Now.Subtract(dt).TotalSeconds.ToString());


                _SearchCriteria = txtbSearch.Text;

                if (SearchCompleted != null)
                    SearchCompleted();
            }
            catch (Exception ex)
            {
                lblInformation.Text = string.Concat("Error: ", ex.Message);
            }
        }

    }
}
