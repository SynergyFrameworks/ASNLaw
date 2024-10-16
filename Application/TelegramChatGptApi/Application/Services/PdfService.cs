using Aspose.Pdf.Facades;
using System.Collections.Generic;

namespace TelegramChatGptApi.Application.Services
{
    public class PdfService
    {
        public void ModifyPdf(string inputPdfPath, string outputPdfPath, Dictionary<string, string> parameters)
        {
            // Create a Form object to work with AcroForms (interactive form fields) in the PDF
            using (var pdfForm = new Form(inputPdfPath, outputPdfPath))
            {
                // Fill in form fields using the parameters dictionary
                foreach (var parameter in parameters)
                {
                    pdfForm.FillField(parameter.Key, parameter.Value);
                }

                // Save the modified PDF
                pdfForm.Save();
            }
        }
    }
}
