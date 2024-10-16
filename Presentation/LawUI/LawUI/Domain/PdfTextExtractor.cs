using Aspose.Pdf;
using Aspose.Pdf.Text;
using LawUI.Abstracts;

namespace LawUI.Domain
{
public class PdfTextExtractor : ITextExtractor
    {
        public string ExtractText(string filePath)
        {
            Document pdfDocument = new Document(filePath);
            TextAbsorber textAbsorber = new TextAbsorber();
            pdfDocument.Pages.Accept(textAbsorber);
            return textAbsorber.Text;
        }
    }

}
