using System;
using System.IO;
using Xceed.Words.NET;

namespace Word.Docx
{
    public class DocxManager : IWordManager
    {
        public IDocument OpenDocument(string path)
        {
            var fi = new FileInfo(path);

            if (fi.Exists && fi.Length > 0)
            {
                // Open an existing document and wrap it in DocxDocument
                return new DocxDocument(DocX.Load(path));
            }
            else
            {
                // Create a new document, save it, and wrap it in DocxDocument
                var doc = DocX.Create(path);
                doc.Save();
                return new DocxDocument(doc);
            }
        }
    }
}
