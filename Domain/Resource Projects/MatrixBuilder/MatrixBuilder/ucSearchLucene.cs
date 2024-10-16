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

using WorkgroupMgr;

using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;

using Lucene.Net.Util;

namespace MatrixBuilder
{
    public partial class ucSearchLucene : UserControl
    {
        public ucSearchLucene()
        {
            InitializeComponent();
        }

        // Query Ref.: https://lucene.apache.org/core/2_9_4/queryparsersyntax.html

        // Lucene vars
        private Analyzer analyzer = new StandardAnalyzer();
        private Lucene.Net.Store.Directory luceneIndexDirectory;
        private IndexWriter writer;
        private string _IndexPath = string.Empty;

        private List<string> filesFound;

        // Declare delegate for when search has completed
        public delegate void ProcessHandler();

        [Category("Action")]
        [Description("Fires when the Search has completed")]
        public event ProcessHandler SearchCompleted;

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

        public string IndexPath
        {
            get { return _IndexPath; }
            set { _IndexPath = value; }
        }


        public void Clear()
        {
            this.txtbSearch.Text = string.Empty;
            lblFound.Text = string.Empty;
        }


        private void SearchText3(string strQuery)
        {
            if (_IndexPath == string.Empty)
            {
                MessageBox.Show("The Search Engine Index file has not been identified.", "Search Engine Index File Unknown", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (luceneIndexDirectory == null)
            { 
                luceneIndexDirectory = FSDirectory.GetDirectory(_IndexPath, false);
            }


            IndexSearcher searcher = new IndexSearcher(luceneIndexDirectory);
            QueryParser parser = new QueryParser("SearchText", analyzer);

            Query query = parser.Parse(strQuery);
            Hits hitsFound = searcher.Search(query);
  
            string fileName = string.Empty;

            if (filesFound == null)
            {
                filesFound = new List<string>();
            }
            else
            {
                filesFound.Clear();
            }

            for (int i = 0; i < hitsFound.Length(); i++)
            {             
                Document doc = hitsFound.Doc(i);

                fileName = doc.Get("SearchFile");
                fileName = Files.GetFileNameWOExt(fileName);
                filesFound.Add(fileName);
            }

        }


        private void butSearch_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;

            SearchText3(this.txtbSearch.Text.Trim());

            _Found = filesFound.ToArray();

            _FoundQty = filesFound.Count;

            lblFound.Text = string.Concat("Found: ", _FoundQty.ToString(), Environment.NewLine, Environment.NewLine, "Seconds: ", DateTime.Now.Subtract(dt).TotalSeconds.ToString());

            _SearchCriteria = txtbSearch.Text;

            if (SearchCompleted != null)
                SearchCompleted();

        }
    }
}
