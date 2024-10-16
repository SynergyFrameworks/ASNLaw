using Aspose.OCR;
using LawUI.Abstracts;
using System.Text;

namespace LawUI.Domain
{
    public class OcrTextExtractor : ITextExtractor
    {
        public string ExtractText(string filePath)
        {
            AsposeOcr ocr = new AsposeOcr();
            OcrInput input = new OcrInput(InputType.PDF);
            input.Add(filePath);
            var results = ocr.Recognize(input);

            StringBuilder extractedText = new StringBuilder();
            foreach (var result in results)
            {
                extractedText.Append(result.RecognitionText);
            }

            return extractedText.ToString();
        }
    }
}
