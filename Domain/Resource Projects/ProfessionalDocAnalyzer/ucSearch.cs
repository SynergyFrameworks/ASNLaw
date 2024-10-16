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
    public partial class ucSearch : UserControl
    {
        public ucSearch()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        // Declare delegate for when search has completed
        public delegate void ProcessHandler();

        [Category("Action")]
        [Description("Fires when the Search has completed")]
        public event ProcessHandler SearchCompleted;

        private string[] _Found;
        private int _FoundQty = 0;
        private string _SearchCriteria = string.Empty;
        private const string _newLine = "\r\n";

        // Query Ref.: https://lucene.apache.org/core/2_9_4/queryparsersyntax.html

        // Lucene vars
        private Analyzer _analyzer = new StandardAnalyzer();
        private Lucene.Net.Store.Directory _LuceneIndexDirectory;

        // private IndexWriter writer;
        private string _indexPath = string.Empty;
        private string _filesPath = string.Empty;

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

        public bool LoadData(string IndexPath, string FilesPath)
        {
            _indexPath = IndexPath;

            if (!System.IO.Directory.Exists(_indexPath))
            {
                string msg = string.Concat("Unable to create the search engine's index folder: ", _indexPath);
                try
                {
                    System.IO.Directory.CreateDirectory(_indexPath);
                    if (!System.IO.Directory.Exists(_indexPath))
                    {
                        MessageBox.Show(msg, "Unable to Search", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                }
                catch
                {
                    MessageBox.Show(msg, "Unable to Search", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }

            _filesPath = FilesPath;

            ShowInstructions();

            return true;

        }

        public void ShowInstructions()
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
                    @"A Phrase is a group of words surrounded by double quotes such as ""Should Apply""."

                );
        }

        public void ChkRunIndexer()
        {
            Indexer indexer = new Indexer();

            indexer.CreateIndex(_indexPath, _filesPath);

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

                DateTime dt = DateTime.Now;

                SearchText3(this.txtbSearch.Text.Trim());

                _FoundQty = _Found.Length;

                lblInformation.Text = string.Concat("Found: ", _FoundQty.ToString(), _newLine, _newLine, "Seconds: ", DateTime.Now.Subtract(dt).TotalSeconds.ToString());
                
             //   butRefreshFind.Visible = true;
                this.Refresh();

                _SearchCriteria = txtbSearch.Text;

                if (SearchCompleted != null)
                    SearchCompleted();
            }
            catch (Exception ex)
            {
                lblInformation.Text = string.Concat("Error: ", ex.Message);

                if (lblInformation.Text.IndexOf("Syntax error:") != -1)
                {
                    lblInformation.Text = "Not Found";
                }
            }
        }

        private void SearchText3(string strQuery)
        {
 
            _LuceneIndexDirectory = FSDirectory.GetDirectory(_indexPath, false);
            

            List<string> found = new List<string>();

            IndexSearcher searcher = new IndexSearcher(_LuceneIndexDirectory);
            QueryParser parser = new QueryParser("SearchText", _analyzer);

            Query query = parser.Parse(strQuery);
            Hits hitsFound = searcher.Search(query);

            Application.DoEvents();
            //       List<SearchFileData> results = new List<SearchFileData>();
            //       SearchFileData DataFile = null;

            string fileName = string.Empty;


            for (int i = 0; i < hitsFound.Length(); i++)
            {
                Document doc = hitsFound.Doc(i);

                fileName = doc.Get("SearchFile");
                fileName = Files.GetFileNameWOExt(fileName);
                found.Add(fileName);
            }

            Application.DoEvents();
            _Found = found.ToArray();

            // Clean up
            hitsFound = null;
            query = null;
            parser = null;
            searcher.Close();
            searcher = null;
            _LuceneIndexDirectory.Close();
            _LuceneIndexDirectory = null;



        }

        private void txtbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                butSearch_Click(this, new EventArgs());
            }
        }

    }
}
