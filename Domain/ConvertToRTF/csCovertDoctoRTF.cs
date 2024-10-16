using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.IO;
using Microsoft.Win32;


namespace Domain.CovertToRTF
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class csCovertDoctoRTF
	{
		public csCovertDoctoRTF()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private string msErrorMsg = "";
		public string Error_Message
		{
			get 
			{ 
				return msErrorMsg;
			}
		}

        public bool IsMSWordInstalled()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\Winword.exe");
            if (key != null)
                key.Close();

            return key != null;
        }
		
		public enum wdFormats
		{
			
			wdFormatDocument = 0,
			wdFormatDOSText = 4,
			wdFormatDOSTextLineBreaks = 5,
			wdFormatEncodedText = 7,
			wdFormatHTML = 8,
			wdFormatRTF = 6,
			wdFormatTemplate = 1,
			wdFormatText = 2,
			wdFormatTextLineBreaks = 3,
			wdFormatUnicodeText = 7

		};

        #region Public Functions
        public bool CovertDoc(wdFormats wdFormat, string sFromFile, string sToFile)
        {
            // Bypass Warning popup
            try
            {
                // Provide the required 'version' parameter
                WordPdfImportWarningRemover wordpdf = new WordPdfImportWarningRemover("your_version_here");
            }
            catch (Exception e)
            {
                msErrorMsg = e.Message;
            }

            bool returnValue = false; // default
            try
            {
                Object[] objTrue = new Object[1];
                objTrue[0] = true;

                Type typeWord = Type.GetTypeFromProgID("Word.Application");

                // object WordApp = Activator.CreateInstance(typeWord); // Good!
                dynamic WordApp = Activator.CreateInstance(typeWord); // New 07.23.2014 -- Need to Test

                WordApp.Visible = false; // New 07.23.2014 -- Need to Test
                WordApp.ScreenUpdating = false; // New 07.23.2014 -- Need to Test

                object[] Docpath = { sFromFile };
                object[] ToFile_Format = { sToFile, wdFormat };
                object WordDocs = typeWord.InvokeMember("Documents", System.Reflection.BindingFlags.GetProperty, null, WordApp, null);
                object doc = WordDocs.GetType().InvokeMember("Open", System.Reflection.BindingFlags.InvokeMethod, null, WordDocs, Docpath);
                doc.GetType().InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod, null, doc, ToFile_Format);

                typeWord.InvokeMember("Quit", BindingFlags.InvokeMethod, null, WordApp, objTrue);

                returnValue = true;
            }
            catch (Exception e)
            {
                msErrorMsg = e.Message;
                returnValue = false;
            }
            finally
            {
                // Cleanup
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            return returnValue;
        }


        public bool CovertDoc3(wdFormats wdFormat, string sFromFile, string sToFile)
        {

            bool returnValue = false; //default
            try
            {
                Object[] objTrue = new Object[1];
                objTrue[0] = true;

                Type typeWord = Type.GetTypeFromProgID("Word.Application");
                // object WordApp = Activator.CreateInstance(typeWord); // Good!
                dynamic WordApp = Activator.CreateInstance(typeWord); // New 07.23.2014 -- Need to Test

                WordApp.Visible = false; // New 07.23.2014 -- Need to Test
                WordApp.ScreenUpdating = false; // New 07.23.2014 -- Need to Test

                object[] Docpath = { sFromFile };
                object[] ToFile_Format = { sToFile, wdFormat };
                object WordDocs = typeWord.InvokeMember("Documents", System.Reflection.BindingFlags.GetProperty, null, WordApp, null);
                object doc = WordDocs.GetType().InvokeMember("Open", System.Reflection.BindingFlags.InvokeMethod, null, WordDocs, Docpath);
                //doc.GetType().InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod, null, doc, ToFile_Format);

                doc.GetType().InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod, null, doc, new object[] { sToFile, ToFile_Format }); // Changed 10.13.2014

                typeWord.InvokeMember("Quit", BindingFlags.InvokeMethod, null, WordApp, objTrue);

                returnValue = true;
            }
            catch (Exception e)
            {
                msErrorMsg = e.Message;
                returnValue = false;
            }
            finally
            {
                // cleanup
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            return returnValue;
        }

        public bool CovertDoc2(wdFormats wdFormat, string sFromFile, string sToFile )
        {
            			bool returnValue = false; //default
                        try
                        {
                            Type wordType = Type.GetTypeFromProgID("Word.Application");

                            dynamic Word = Activator.CreateInstance(wordType);


                            object oMissing = System.Reflection.Missing.Value;


                            DirectoryInfo dirInfo = new DirectoryInfo(@"\\server\folder");
                            FileInfo wordFile = new FileInfo(sFromFile);

                            Word.Visible = false;
                            Word.ScreenUpdating = false;

                            Object filename = (Object)wordFile.FullName;

                            var doc = Word.Documents.Open(ref sFromFile);
                            doc.Activate();

                            object outputFileName = wordFile.FullName.Replace(".doc", ".rtf");

                            //*in the WdSaveFormat enum, 17 is the value for pdf format*/ 
                            object fileFormat = wdFormat;


                            doc.SaveAs(ref outputFileName, ref fileFormat);

                            //in the   WdSaveOptions enum, 0 is for Do not save pending changes.*/
                            object saveChanges = 0;

                            doc.Close(ref saveChanges);
                            doc = null;

                            Word.Quit();

                            returnValue = true;
                        }
                        catch (Exception e)
                        {
                            msErrorMsg = e.Message;
                            returnValue = false;
                        }
                        finally
                        {
                            // cleanup
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                        }
                        return returnValue;
        }



		#endregion 
	}
}
