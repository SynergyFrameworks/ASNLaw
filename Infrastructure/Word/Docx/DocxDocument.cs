using System.Collections.Generic;
using Xceed.Words.NET;
using Xceed.Document.NET;
using Word.Docx;

namespace Word
{
    public class DocxDocument : IDocument
    {
        private readonly DocX _doc;

        public DocxDocument(DocX doc)
        {
            _doc = doc;
        }

        public void Save()
        {
            _doc.Save();
        }

        public void SaveAs(string path)
        {
            _doc.SaveAs(path);
        }

        public ITable InsertTable(int columns, int rows)
        {
            var table = _doc.InsertTable(rows, columns);
            return new DocxTable(table);
        }

        public IParagraph InsertParagraph(string text)
        {
            var paragraph = _doc.InsertParagraph(text);
            return new DocxParagraph(paragraph);
        }

        public IParagraph InsertParagraph(string text, IFormatting formatting)
        {
            var docxFormatting = ((DocxFormatting)formatting).ToXceedFormatting();
            var paragraph = _doc.InsertParagraph(text, false, docxFormatting);
            return new DocxParagraph(paragraph);
        }

        public IList<IProperty> GetProperties()
        {
            // Implement property retrieval logic if needed
            return new List<IProperty>();
        }

        public IList<ITable> GetTables()
        {
            var tables = new List<ITable>();
            foreach (var table in _doc.Tables)
            {
                tables.Add(new DocxTable(table));
            }
            return tables;
        }

        public void SetPropertyValue(string propertyName, string propertyValue)
        {
            // Implement property setting logic if needed
        }
    }
}
