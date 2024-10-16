using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using Atebion.Common;

using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;

using Lucene.Net.Util;

namespace Atebion.ConceptAnalyzer
{
    public class Indexer
    {
        private Analyzer analyzer = new StandardAnalyzer();
        private Lucene.Net.Store.Directory luceneIndexDirectory;
        private IndexWriter writer;

        RichTextBox _rtfCtrl = new RichTextBox();
        private string ReadRTFFile(string RTFFile)
        {
            _rtfCtrl.LoadFile(RTFFile);
            return _rtfCtrl.Text;
        }

        public void CreateIndex(string indexPath, string searchDir)
        {           

            if (System.IO.Directory.Exists(indexPath))
            {
                System.IO.Directory.Delete(indexPath, true);
            }

            // Initialise Lucene

            luceneIndexDirectory = Lucene.Net.Store.FSDirectory.GetDirectory(indexPath, true);
            writer = new IndexWriter(luceneIndexDirectory, analyzer, true);

            string[] files = Files.GetFilesFromDir(searchDir, "*.*");

            string currentFile = string.Empty;
            string currentText = string.Empty;
            for (int i = 0; i < files.Count(); i++)
            {
                currentFile = string.Concat(searchDir, @"\", files[i]);
                currentText = ReadRTFFile(currentFile);

                Document doc = new Document();

                doc.Add(new Field("SearchFile", files[i], Field.Store.YES, Field.Index.UN_TOKENIZED));
                doc.Add(new Field("SearchText", currentText, Field.Store.YES, Field.Index.TOKENIZED));

                writer.AddDocument(doc);

            }

            writer.Optimize();
            writer.Flush();
            writer.Close();
            luceneIndexDirectory.Close();
        }
    }
}
