using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


using Domain.Common;

using PdfSharp.Pdf;
using PdfSharp.Pdf.Content;
using PdfSharp.Pdf.Content.Objects;
using PdfSharp.Pdf.IO;

using TikaOnDotNet.TextExtraction;
using Domain.CovertToRTF;

namespace PdfImport
{
    public class Manager
    {

        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }


        #region Conversion

        public bool ConvertFile2PlainText(string ext, string SourcePathFile, string ConvertedPathFile)
        {
            if (!File.Exists(SourcePathFile))
            {
                _ErrorMessage = string.Concat("Unable to find the selected file: ", SourcePathFile);
                return false;
            }

            if (ext == "txt")
            {
                if (File.Exists(ConvertedPathFile))
                    File.Delete(ConvertedPathFile);

                File.Copy(SourcePathFile, ConvertedPathFile);
                return true;
            }

            string txt = ReadFile(SourcePathFile);

            if (txt == string.Empty)
            {
                if (ext == "doc" || ext == "docx") // If the failure occurred reading a MS Word file, then try converting the  file via MS word. -- This rarely occurs
                {
                    if(!convertDocViaMSWord(ext, SourcePathFile, ConvertedPathFile))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    _ErrorMessage = string.Concat("Unable to Convert File ", SourcePathFile, " to Plain Text");                   
                    return false;
                }
            }

            if (File.Exists(ConvertedPathFile))
            {
                File.Delete(ConvertedPathFile);
            }

            Files.WriteStringToFile(txt, ConvertedPathFile);

  
            return true;
        }


        private string ReadFile(string pathFile)
        {
            _ErrorMessage = string.Empty;

            string txt = string.Empty;
            try
            {
                TikaOnDotNet.TextExtraction.TextExtractor textX = new TikaOnDotNet.TextExtraction.TextExtractor();

                var results = textX.Extract(pathFile);
                txt = results.Text.Trim();

            }
            catch (Exception ex)
            {
 
                _ErrorMessage = string.Concat("An error occurred while extracting text from file ", pathFile, " --  Error: ", ex.Message);
                txt = string.Empty;
            }
                         

            return txt;
        }

        private bool convertDocViaMSWord(string ext, string TempPathFile, string TempConvertedPathFile) // Added 10.26.2017
        {
            _ErrorMessage = string.Empty;

            if (ext == "doc" || ext == "docx")
            {
                csCovertDoctoRTF convert = new csCovertDoctoRTF();
                if (!convert.IsMSWordInstalled())
                {
                    _ErrorMessage = "Atebion's converter was unable to convert your Document to Plain Text.";

                    return false;
                }

                if (!convert.CovertDoc(csCovertDoctoRTF.wdFormats.wdFormatText, TempPathFile, TempConvertedPathFile))
                {
                    _ErrorMessage = convert.Error_Message;

                    return false;
                }

                return true;
            }
            return false;            
        }

#endregion

        #region Parse into Pages

        #region PDF into Pages
        public int PDF2Pages(string SourcePathFile, string ParsePagesPath)
        {
            _ErrorMessage = string.Empty;

            if (!File.Exists(SourcePathFile))
            {
                _ErrorMessage = string.Concat("Unable to find file: ", SourcePathFile);

                return -1;
            }

            string outputText = string.Empty;

            PdfDocument reader = PdfReader.Open(SourcePathFile, PdfDocumentOpenMode.ReadOnly);


            // Iterate pages
            int count = reader.PageCount;
            string pageFile = string.Empty;
            string pagePathFile = string.Empty;

            for (int i = 0; i < count; i++)
            {
                PdfPage page = reader.Pages[i];

              //  sb.AppendLine(string.Concat("---- Page: ", (i + 1).ToString(), " ----"));
                
                PdfDictionary.PdfStream stream = page.Contents.Elements.GetDictionary(0).Stream;

                outputText = new PDFTextExtractor().ExtractTextFromPDFBytes(stream.Value);

                pageFile = string.Concat(i.ToString(), ".txt");
                pagePathFile = Path.Combine(ParsePagesPath, pageFile);

                if (!Files.WriteStringToFile(outputText, pagePathFile, true))
                {
                    string exX;
                    _ErrorMessage = string.Concat("Unable to parse file into pages. File: ", Files.GetFileName(SourcePathFile, out exX), " -- Error: ", Files.ErrorMessage);
                    Files.DeleteAllFileInDir(ParsePagesPath);
                    return -1;
                }
            }

            return count;
        }

        #endregion

        #region MS Word Doc into Pages

        public int DOCX2Pages(string SourcePathFile, string ParsePagesPath)
        {
            //---------------------------Late Binding------------------------------------------

            try
            {

                Type wordType = Type.GetTypeFromProgID("Word.Application");

                dynamic Word = Activator.CreateInstance(wordType);

                Word.Visible = false;
                Word.ScreenUpdating = false;

                object Miss = System.Reflection.Missing.Value;

                object pathFile = SourcePathFile;
                var doc = Word.Documents.Open(ref pathFile);
                doc.Activate();

                object wdStatisticPages = 2;
                // int PagesCount = doc.ComputeStatistics(wdStatisticPages, ref Miss);
                int PagesCount = doc.ComputeStatistics(wdStatisticPages);

                //Get pages
                object What = 1; // Microsoft.Office.Interop.Word.WdGoToItem.wdGoToPage;
                object Which = 1; // Microsoft.Office.Interop.Word.WdGoToDirection.wdGoToAbsolute;
                object Start;
                object End;
                object CurrentPageNumber;
                object NextPageNumber;

                List<string> Pages = new List<string>();

                for (int Index = 1; Index < PagesCount + 1; Index++)
                {
                    CurrentPageNumber = (Convert.ToInt32(Index.ToString()));
                    NextPageNumber = (Convert.ToInt32((Index + 1).ToString()));

                    // Get start position of current page
                    Start = Word.Selection.GoTo(ref What, ref Which, ref CurrentPageNumber).Start;

                    // Get end position of current page                                
                    End = Word.Selection.GoTo(ref What, ref Which, ref NextPageNumber).End;

                    // Get text
                    if (Convert.ToInt32(Start.ToString()) != Convert.ToInt32(End.ToString()))
                    {
                        // Pages.Add(string.Concat("----- Page: ", CurrentPageNumber.ToString(), Environment.NewLine));
                        Pages.Add(doc.Range(ref Start, ref End).Text);
                        //  Pages.Add(string.Concat(Environment.NewLine, Environment.NewLine));
                    }
                    else
                    {
                        //   Pages.Add(string.Concat("----- Page: ", CurrentPageNumber.ToString(), Environment.NewLine));
                        Pages.Add(doc.Range(ref Start).Text);
                        //  Pages.Add(string.Concat(Environment.NewLine, Environment.NewLine));
                    }
                }

                string pageFile = string.Empty;
                string pagePathFile = string.Empty;
                int pageNo = 1;
                foreach (string s in Pages)
                {
                    pageFile = string.Concat(pageNo.ToString(), ".txt");
                    pagePathFile = Path.Combine(ParsePagesPath, pageFile);

                    Files.WriteStringToFile(s, pagePathFile, true);
                    pageNo++;
                }

                //in the   WdSaveOptions enum, 0 is for Do not save pending changes.*/
                object saveChanges = 0;

                doc.Close(ref saveChanges);

                Word.Quit();

                pageNo = pageNo - 1;


                return pageNo;
            }
            catch
            {
                return 0;
            }

        }


        #endregion


        #endregion
    }
}
