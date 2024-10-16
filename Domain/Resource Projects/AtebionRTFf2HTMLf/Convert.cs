using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using Itenso.Rtf;
using Itenso.Rtf.Support;
using Itenso.Rtf.Converter.Text;
using Itenso.Rtf.Converter.Xml;
using Itenso.Rtf.Converter.Html;


namespace AtebionRTFf2HTMLf
{
    public class Convert
    {
        public Convert()
        {

        }

        public Convert(string RTFFolder, string HTMLFolder)
        {

        }

        private string _ErrorMessage = string.Empty;

        private string _RTFFolder = string.Empty;
        private string _HTMLFolder = string.Empty;

        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }
      
        public string RTFFolder
        {
            get { return _RTFFolder; }
            set { _RTFFolder = value; }
        }

        public string HTMLFolder
        {
            get { return _HTMLFolder; }
            set { _HTMLFolder = value; }
        }

        public int ConvertFiles(string rtfFolder, string htmlFolder)
        {
            _RTFFolder = rtfFolder;
            _HTMLFolder = htmlFolder;

            int i = ConvertFiles();

            return i;
        }

        /// <summary>
        /// Converts RTF file to Plain Text file
        /// </summary>
        /// <param name="Input_RTFFile">RTF Path/File</param>
        /// <param name="Output_TxtFile">Plain Text Path/File</param>
        /// <returns>True = successful. False = failed, see .ErrorMessage for details</returns>
        public bool ConvertRTF2Txt(string Input_RTFFile, string Output_TxtFile) // Added 10/17/2015
        {
            _ErrorMessage = string.Empty;

            if (!File.Exists(Input_RTFFile))
            {
                _ErrorMessage = "RTF file not found.";
                return false;
            }

            string rftText = Files.ReadFile(Input_RTFFile);

            try
            {
                IRtfGroup rtfStructure = RtfParserTool.Parse(rftText);
                RtfTextConverter textConverter = new RtfTextConverter();
                RtfInterpreterTool.Interpret(rtfStructure, textConverter);
                string txt = textConverter.PlainText;

                File.WriteAllText(Output_TxtFile, txt);

                return true;
            }
            catch (Exception exception)
            {
                _ErrorMessage = exception.Message;
                return false;
            }
        }

        public int ConvertFiles()
        {
            _ErrorMessage = string.Empty;

            int i = 0;

            Files.ClearFolder(_HTMLFolder);

            RichTextBox ctlRTFbox = new RichTextBox();

            string[] files = Directory.GetFiles(_RTFFolder, "*.RTF");

            string filename = string.Empty;
            string rtfText = string.Empty;
            string htmlText = string.Empty;
            string htmlFile = string.Empty;

            foreach (string file in files)
            {

                filename = Files.GetFileNameWOExt(file);

                ctlRTFbox.LoadFile(file);

                rtfText = ctlRTFbox.Rtf;

               // rtfText = Files.ReadFile(file);

               // rtfText = File.ReadAllText(file, Encoding.Unicode);

                try
                {
                    IRtfDocument rtfDocument = RtfInterpreterTool.BuildDoc(rtfText);
                    RtfHtmlConverter htmlConverter = new RtfHtmlConverter(rtfDocument);
                    htmlText = htmlConverter.Convert();


                    htmlFile = string.Concat(_HTMLFolder, @"\", filename, ".html");
                    Files.WriteStringToFile(htmlText, htmlFile, true);

                    i++; // counter

                }
                catch (Exception ex)
                {
                    _ErrorMessage += string.Concat("File Name: ", filename, " Error: ", ex.Message, Environment.NewLine);
                }
            }

            CleanupHTML(); // Added 12.10.2016

            return i;
        }

        private void CleanupHTML() // Added 12.10.2016
        {
            
            string[] files = Directory.GetFiles(_HTMLFolder, "*.html");
            string html = string.Empty;
            foreach (string file in files)
            {
                html = Files.ReadFile(file);
                html = html.Replace("<p>&nbsp;</p>", "");
                Files.WriteStringToFile(html, file, true);
            }
        }





    }
}
