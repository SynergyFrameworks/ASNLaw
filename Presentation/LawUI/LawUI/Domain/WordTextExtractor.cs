using Aspose.Words;
using LawUI.Abstracts;

namespace LawUI.Domain
{


    public class WordTextExtractor : ITextExtractor
    {
        public string ExtractText(string filePath)
        {
            Document doc = new Document(filePath);
            return doc.ToString(SaveFormat.Text);
        }
    }

}
