using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using System.Diagnostics;
using System.IO;

using hOOt;

using Atebion.Common;
using RaptorDB;

namespace ProfessionalDocAnalyzer
{
    public partial class frmHootProcessing : Form
    {
        public frmHootProcessing()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        private string _IndexPath = string.Empty;
        private string _DocsPath = string.Empty;

        public bool CreateIndex(string IndexPath, string DocsPath)
        {
            _IndexPath = IndexPath;
            _DocsPath = DocsPath;

            return true;
        }

        private Hoot _hoot;


        private void RunIndexer()
        {
            Cursor.Current = Cursors.WaitCursor; // Waiting

            if (_hoot == null)
                _hoot = new Hoot(_IndexPath, "index", true);

            string[] files = Directory.GetFiles(_DocsPath, "*", SearchOption.TopDirectoryOnly);


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
