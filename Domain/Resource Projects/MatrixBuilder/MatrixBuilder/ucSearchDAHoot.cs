using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using WorkgroupMgr;
using hOOt;

namespace MatrixBuilder
{
    public partial class ucSearchDAHoot : UserControl
    {
        public ucSearchDAHoot()
        {
            InitializeComponent();
        }

        // Declare delegate for when search has completed
        public delegate void ProcessHandler();

        [Category("Action")]
        [Description("Fires when the Search has completed")]
        public event ProcessHandler SearchCompleted;

        private Hoot _hoot;

        private string[] _Found;
        public string[] FoundResults
        {
            get { return _Found; }
        }

        private int _FoundQty;
        public int FoundQty
        {
            get { return _FoundQty; }
        }

        private string _SearchCriteria = string.Empty;
        public string SearchCriteria
        {
            get { return _SearchCriteria; }
        }

        private string _DocParsedSecIndexPath = string.Empty;
        public string IndexPath
        {
            set
            {
                _DocParsedSecIndexPath = value;

            }
            get { return _DocParsedSecIndexPath; }
        }

        private string _DocParsedSecPath = string.Empty;
        public string DocParsedSecPath
        {
            set { _DocParsedSecPath = value; }
            get { return _DocParsedSecPath; }
        }

        public void LoadData()
        {
            ChkRunIndexer();

        }

        public void Clear()
        {
            txtbSearch.Text = string.Empty;
            lblFound.Text = string.Empty;
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            if (_hoot == null)
            {
                _hoot = new Hoot(_DocParsedSecIndexPath, "index", true);

            }

            List<string> lstFound = new List<string>();

            string fileName = string.Empty;
            _SearchCriteria = txtbSearch.Text;
            foreach (var d in _hoot.FindDocumentFileNames(_SearchCriteria))
            {
                fileName = Files.GetFileNameWOExt(d);
              //  if (DataFunctions.IsNumeric(fileName))
                    lstFound.Add(fileName);

            }

            lblFound.Text = string.Concat("Found: ", lstFound.Count.ToString());

            _FoundQty = lstFound.Count;
            if (_FoundQty == 0)
                return;

            _Found = lstFound.ToArray();

            if (SearchCompleted != null)
                SearchCompleted();


        }

        public void ChkRunIndexer()
        {
            if (_DocParsedSecIndexPath == string.Empty)
                return;

            if (_DocParsedSecPath == string.Empty)
                return;

            DateTime dtIndex = Files.GetLatestFileDatetime(_DocParsedSecIndexPath);
            DateTime dtParsedFiles = Files.GetLatestFileDatetime(_DocParsedSecPath);


            try
            {
                _hoot = new Hoot(_DocParsedSecIndexPath, "index", true);

                if (dtIndex == null)
                {
                    RunIndexer();
                }
                else
                {
                    int result = DateTime.Compare(dtIndex, dtParsedFiles);
                    if (result <= 0) // "is earlier than"
                        RunIndexer();
                }
            }
            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message, "An Error has Occurred while checking the Search Indexer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //  lblFound.Text = string.Concat("Error: ", ex.Message);

            }
        }

        private void RunIndexer()
        {
            Cursor.Current = Cursors.WaitCursor; // Waiting

            if (_hoot == null)
                _hoot = new Hoot(_DocParsedSecIndexPath, "index", true);

            string[] files = Directory.GetFiles(_DocParsedSecPath, "*", SearchOption.TopDirectoryOnly);


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
                    if (_hoot.IsIndexed(fn) == false)
                    {
                        TextReader tf = new EPocalipse.IFilter.FilterReader(fn);
                        string s = "";
                        if (tf != null)
                            s = tf.ReadToEnd();

                        _hoot.Index(new Document(new FileInfo(fn), s), true);
                    }
                }
                catch { }
                i++;
                if (i > 1000)
                {
                    i = 0;
                    _hoot.Save();
                }
            }
            _hoot.Save();
            _hoot.OptimizeIndex();

            Cursor.Current = Cursors.Default; // Back to normal
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
                    if (_hoot.IsIndexed(fn) == false)
                    {
                        TextReader tf = new EPocalipse.IFilter.FilterReader(fn);
                        string s = "";
                        if (tf != null)
                            s = tf.ReadToEnd();

                        _hoot.Index(new Document(new FileInfo(fn), s), true);
                    }
                }
                catch { }
                i++;
                if (i > 1000)
                {
                    i = 0;
                    _hoot.Save();
                }
            }
            _hoot.Save();
            _hoot.OptimizeIndex();
        }

  
    }
}
