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

using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;

using Lucene.Net.Util;

using Atebion.Common;

namespace ConceptAnalyzer
{
    public partial class ucResultsSearch : UserControl
    {
        public ucResultsSearch()
        {
            InitializeComponent();

            
           
        }

        // Query Ref.: https://lucene.apache.org/core/2_9_4/queryparsersyntax.html

        // Lucene vars
        private Analyzer analyzer = new StandardAnalyzer();
        private Lucene.Net.Store.Directory luceneIndexDirectory;
       // private IndexWriter writer;
        private string _indexPath = string.Empty;

        // Declare delegate for when search has completed
        public delegate void ProcessHandler();

        [Category("Action")]
        [Description("Fires when the Search has completed")]
        public event ProcessHandler SearchCompleted;

        [Category("Action")]
        [Description("Fires when the Refresh has been clicked")]
        public event ProcessHandler RefreshResults;

        private const string _newLine = "\r\n";

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

        public void LoadData(string IndexPath)
        {
            _indexPath = IndexPath;

            luceneIndexDirectory = FSDirectory.GetDirectory(_indexPath, false);
        }

        private void picInfo_Search_Click(object sender, EventArgs e)
        {
            
//            this.htmlToolTip1.Show(@"<p>  ? = Matches any single character. </p>
//                <p></p>
//                <p>  * = Matches any one or more characters. For example, bet* matches any text that includes 'bet', such as 'better'. </p>
//                <p></p>
//                <p></p>                    
//                <p>The AND operator matches content where both terms exist anywhere in the text (e.g. Should AND Require, finds both) </p>
//                <p></p>
//                <p></p>                    
//                <p> The OR operator is the default conjunction operator. </p>
//                <p></p>
//                 <p>This means that if there is no Boolean operator between two terms, the OR operator is used. (e.g. Should OR Require, finds either) </p>
//                <p></p>
//                <p></p> 
//                <p> The NOT operator excludes content that contain the term after NOT. (e.g. Should Not Require) </p>
//                <p></p>
//                <p></p> 
//                 <p>Grouping example: (Should OR Require) AND Must </p>
//                <p></p>
//                <p></p> 
//                <p> @ Phrase is a group of words surrounded by double quotes such as Should Apply
//                <p></p> 
//                <p>Phrase searching is supported within Deep Analysis, but not supported in the previous Analysis Results. </p>", this.Parent, 500 );

            Application.DoEvents();
        }


        private void SearchText3(string strQuery)
        {
            //if (luceneIndexDirectory == null)
            //{
                luceneIndexDirectory = FSDirectory.GetDirectory(_indexPath, false);
            //}

            List<string> found = new List<string>();

            IndexSearcher searcher = new IndexSearcher(luceneIndexDirectory);
            QueryParser parser = new QueryParser("SearchText", analyzer);

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

        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            try
            {
                
                DateTime dt = DateTime.Now;

                SearchText3(this.txtbSearch.Text.Trim());

                _FoundQty = _Found.Length;

                lblInformation.Text = string.Concat("Found: ", _FoundQty.ToString(), _newLine, _newLine, "Seconds: ", DateTime.Now.Subtract(dt).TotalSeconds.ToString());
                lblInformation.Visible = true;
                butRefreshFind.Visible = true;
                this.Refresh();

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

                if (lblInformation.Text.IndexOf("Syntax error:") !=-1)
                {
                    lblInformation.Text = "Not Found";
                }
            }
        }

        private void butRefreshFind_Click(object sender, EventArgs e)
        {
            lblInformation.Text = string.Empty;
            lblInformation.Visible = false;
            butRefreshFind.Visible = false;


            if (RefreshResults != null)
                RefreshResults();
        }

    }
}
