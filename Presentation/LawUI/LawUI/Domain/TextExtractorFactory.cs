using LawUI.Abstracts;
using Aspose.Pdf;


namespace LawUI.Domain
{
    public static class TextExtractorFactory
    {
        public static ITextExtractor GetTextExtractor(string filePath)
        {
            if (filePath.EndsWith(".pdf"))
            {
                if (PdfNeedsOcr(filePath))
                {
                    return new OcrTextExtractor();
                }
                else
                {
                    return new PdfTextExtractor();
                }
            }
            else if (filePath.EndsWith(".docx"))
            {
                return new WordTextExtractor();
            }
            else
            {
                throw new NotSupportedException("File type not supported");
            }
        }

        private static bool PdfNeedsOcr(string filePath)
        {
            Document pdfDocument = new Document(filePath);
            foreach (var page in pdfDocument.Pages)
            {
                foreach (var image in page.Resources.Images)
                {
                    return true; // If any image is found, OCR is needed
                }
            }
            return false; // No images found, OCR is not needed
        }
    }

}
