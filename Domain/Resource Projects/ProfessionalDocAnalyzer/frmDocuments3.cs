using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using Atebion.Common;
using Atebion.CovertDoctoRTF;
using Atebion.Import;

namespace ProfessionalDocAnalyzer
{
    public partial class frmDocuments3 : MetroFramework.Forms.MetroForm
    {
        public frmDocuments3()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        private bool _ErrorOccurred = false;
        public bool ErrorOccurred
        {
            get { return _ErrorOccurred; }
        }

        public string DocumentName
        {
            get { return txtbDocName.Text; }
        }


        private FileInformation _selectedFileInfo;

        private bool _WordInstalled = false;
        private csCovertDoctoRTF _ConvertViaWord = new csCovertDoctoRTF();

        private bool _isDocReplacement = false;

        public void LoadData(string DocName)
        {
            lblMessage.Text = string.Concat("Select a document to import into the Professional Document Analyzer.", Environment.NewLine, Environment.NewLine,

            "File Types Supported:  MS Word (doc & docx), Excel (xlsx), PowerPoint (pptx), textual Portable Document Format (pdf), Rich Text Format (rtf), and Plain Text (txt).", Environment.NewLine, Environment.NewLine,

            "Content to Page Numbers are supported for MS Word (docx) and Portable Document Format (pdf) file types."
            );

            txtbDocName.Text = DocName;

            if (DocName.Length > 0)
            {
                txtbDocName.Enabled = false;
                _isDocReplacement = true;
            }

            _WordInstalled = _ConvertViaWord.IsMSWordInstalled();

            //if (_WordInstalled)
            //    chkbConvertUsingWord.Checked = true;
            //else
                chkbConvertUsingWord.Checked = false;

        }

        private void butLoadFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = string.Empty;

            openFileDialog1.Filter = "Rich Text Format|*.rtf|MS Word (*.doc)|*.doc|MS Word  (*.docx)|*.docx|Excel (*.xlsx)|*.xlsx|PowerPoint (*.pptx)|*.pptx|Text (*.txt)|*.txt|Portable Document Format|*.pdf|All Files|*.*";

            openFileDialog1.FilterIndex = 8; // Show All -- Added 12.11.2018

            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK)
            {
                string selectedFile = openFileDialog1.FileName;

                // Check file type
                string ext;
                string fileName = Files.GetFileName(selectedFile, out ext);
                string fileNameWOExt = Files.GetFileNameWOExt(selectedFile).Trim();
                ext = ext.ToLower();
                bool supportedFileFormat = false;
                if (ext == ".rtf" || ext == ".doc" || ext == ".docx" || ext == ".xlsx" || ext == ".rtf" || ext == ".pptx" || ext == ".pdf" || ext == ".txt")
                {
                    supportedFileFormat = true;
                }

                if (!supportedFileFormat)
                {
                    string msgXExt = string.Concat("The Professional Document Analyzer does not support the ", ext, " file type");
                    MessageBox.Show(msgXExt, "File Type Not Supported", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }

                mtxtbFile.Text = selectedFile;

                if (txtbDocName.Text.Length == 0)
                {
                    //txtbDocName.Text = fileNameWOExt;
                    if (fileNameWOExt.Length > 41)
                        txtbDocName.Text = fileNameWOExt.Substring(0, 40);
                    else
                        txtbDocName.Text = fileNameWOExt;

                    txtbDocName.Text.Replace('.', '_'); // Added 05.06.2020
                }

                _selectedFileInfo = new FileInformation(selectedFile);

                double kb = Convert.ToDouble(_selectedFileInfo.FileSize) / 1000;

                lblMessage.Text = string.Concat(
                    "File: ", _selectedFileInfo.FileName, Environment.NewLine,
                    "Created: ", _selectedFileInfo.CreationDate, " ", _selectedFileInfo.CreationTime, Environment.NewLine,
                    "Size: ", _selectedFileInfo.FileSize, " KB"
                    );

                if (_WordInstalled)
                {
                    if (ext.ToLower() == ".pdf" || ext.ToLower() == ".docx")
                    {
                        chkbConvertUsingWord.Visible = true;
                    }
                    else
                    {
                        chkbConvertUsingWord.Visible = false;
                    }
                }

            }
        }


        public bool ConvertFile2PlainText(string ext, string SourcePathFile, string ConvertedPathFile)
        {
            if (!File.Exists(SourcePathFile))
            {
                string msg = string.Concat("Unable to find the selected file: ", SourcePathFile);
                MessageBox.Show(msg, "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            string txt = string.Empty;

            if (ext == ".pdf" || ext == ".docx")
            {
                if (chkbConvertUsingWord.Visible && chkbConvertUsingWord.Checked)
                {
                    if (convertDocViaMSWord(ext, SourcePathFile, ConvertedPathFile))
                    {
                        return true;
                    }
                    else
                    {
                        txt = ReadFile(SourcePathFile);
                    }
                }
            }
            else
            {
                txt = ReadFile(SourcePathFile);
            }

            if (txt == string.Empty)
            {
                if (ext == ".doc" || ext == ".docx") // If the failure occurred reading a MS Word file, then try converting the  file via MS word. -- This rarely occurs
                {
                    if (!convertDocViaMSWord(ext, SourcePathFile, ConvertedPathFile))
                    {
                        return false;
                    }
                    else
                    {
                        // Determine if this should be used
                        // Files.ReplaceTextInFile(_fileName, cleanedFile, textOld, textNew); // Remove numbered bullets, the number should remain.
                        return true;
                    }
                }
                else
                {
                    string msg = string.Concat("Unable to Convert File ", SourcePathFile, " to Plain Text");
                    return false;
                }
            }

            if (File.Exists(ConvertedPathFile))
            {
                File.Delete(ConvertedPathFile);
            }

            Files.WriteStringToFile(txt, ConvertedPathFile);

            // Determine if this should be used
            // Files.ReplaceTextInFile(_fileName, cleanedFile, textOld, textNew); // Remove numbered bullets, the number should remain.

            return true;
        }

        private string ReadFile(string pathFile)
        {

            string txt = string.Empty;
            try
            {
                TikaOnDotNet.TextExtraction.TextExtractor textX = new TikaOnDotNet.TextExtraction.TextExtractor();

                var results = textX.Extract(pathFile);
                txt = results.Text.Trim();

            }
            catch 
            {
                // --------  Do NOT show this error, because will try to convert by MS Word  --------
                //string msg = string.Concat("An error occurred while extracting text from file ", pathFile, " --  Error: ", ex.Message);
                //MessageBox.Show(msg, "Unable to Convert File into Text", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt = string.Empty;
            }


            return txt;
        }

        private bool convertDocViaMSWord(string ext, string TempPathFile, string TempConvertedPathFile) // Added 10.26.2017
        {

            if (ext == ".doc" || ext == ".docx" || ext == ".pdf")
            {
                
                if (!_ConvertViaWord.IsMSWordInstalled())
                {
                    string msg = "Scion Analytics' converter was unable to convert your Document to Plain Text.";
                    MessageBox.Show(msg, "Unable to Convert File into Text", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    lblMessage.Text = msg;

                    return false;
                }

                if (!_ConvertViaWord.CovertDoc(csCovertDoctoRTF.wdFormats.wdFormatText, TempPathFile, TempConvertedPathFile))
                {
                    string msg = _ConvertViaWord.Error_Message;

                    MessageBox.Show(msg, "Unable to Convert File into Text", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = msg;
                    return false;
                }

                return true;
            }
            return false;
        }


        private bool Validate()
        {
            string msg = string.Empty;

            string newDoc = txtbDocName.Text.Trim();
            if (newDoc == string.Empty)
            {
                msg = "Please enter a document name. A document name is used to identify your new document and becomes the folder name which holds its analysis results.";
                MessageBox.Show(msg, "Document Name is Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblMessage.Text = msg;
                return false;
            }

            if (!File.Exists(mtxtbFile.Text.Trim()))
            {
                msg = string.Concat("Unable to find file: ", mtxtbFile.Text.Trim());
                MessageBox.Show(msg, "Document Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                lblMessage.Text = msg;
                return false;
            }

            string newDocPath = Path.Combine(AppFolders.DocPath, newDoc);
            if (Directory.Exists(newDocPath))
            {
                msg = string.Concat("A Document Name ", newDoc, " already exists. Are you sure you want to replace the document and all of its analysis results?");
                if (MessageBox.Show(msg, "Confirm Document and Analysis Results Replacement", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    return false;
                }

                string newDocArchivePath = string.Empty;
                string currentDocPath = Path.Combine(newDocPath, "Current");
                if (Directory.Exists(currentDocPath))
                {
                    for (int i = 1; i < 37000; i++ )
                    {
                        newDocArchivePath = Path.Combine(newDocPath, i.ToString());
                        if (!Directory.Exists(newDocArchivePath))
                        {
                            try
                            {
                                Directory.Move(currentDocPath, newDocArchivePath);
                                msg = string.Concat("Your previous Analysis Results have been archive in this folder: ", newDocArchivePath);
                                MessageBox.Show(msg, "Previous Analysis Results has been Archived", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return true;
                            }
                            catch
                            {
                                msg = "Unable to archive Analysis Results. A file most likely is being used or is open.";
                                MessageBox.Show(msg, "Unable to Replace Document at this time.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                lblMessage.Text = msg;
                                return false;
                            }
                        }
                    }
                }

            }

            return true;
        }

        // Added 09.20.2018
        private void CleanFile2(string PathFile)
        {
            if (!File.Exists(PathFile))
                return;

            //string txt = Atebion.Common.Files.ReadFile(PathFile);
            //txt.Replace("\t", "");
            //Atebion.Common.Files.WriteStringToFile(txt, PathFile);

            string cleanLine = string.Empty;
            List<string> cleanLines = new List<string>();

            string[] lines = Atebion.Common.Files.ReadFile2Array(PathFile);
            foreach (string line in lines)
            {
                cleanLine = line.Trim();
                cleanLines.Add(cleanLine);
            }

            Atebion.Common.Files.WriteStringToFile(cleanLines.ToArray(), PathFile, true);


        }

        private void butSave_Click(object sender, EventArgs e)
        {
            txtbDocName.Text = txtbDocName.Text.Replace('.', '_');
            txtbDocName.Text = DataFunctions.RegExFixInvalidCharacters(txtbDocName.Text);
            txtbDocName.Text.Trim();
            txtbDocName.Refresh();

            if (!Validate())
                return;

            string msg = string.Empty;

            string selectedFile = mtxtbFile.Text.Trim();

            string ext;
            string fileName = Files.GetFileName(selectedFile, out ext);
            string fileNameWOExt = Files.GetFileNameWOExt(selectedFile);
            ext = ext.ToLower();

            AppFolders.DocName = DataFunctions.RegExFixInvalidCharacters(this.txtbDocName.Text.Trim()); // 03.26.2020 Added RegExFixInvalidCharacters and Trim()
            string currentDocPath = AppFolders.CurrentDocPath;
            string newPathDoc = Path.Combine(currentDocPath, fileName);
            try
            {
                File.Copy(selectedFile, newPathDoc);
            }
            catch (Exception ex)
            {
                msg = string.Concat("Unable to copy your selected document file.  Error: ", ex.Message);
                MessageBox.Show(msg, "Correct the Issue (e.g. close the document) and Try Again", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblMessage.Text = msg;
                return;
            }

            Atebion.Import.Manager ImportMgr;

            string notice = "Please wait while the Professional Document Analyzer converts your selected document to Plain Text";
            if (ext.ToUpper() == ".PDF" || ext.ToUpper() == ".DOCX")
            {
                lblNotice.Text = string.Concat(notice, " and Identify content to page numbers.");
                
            }
            else
            {
                lblNotice.Text = string.Concat(notice, ".");
            }

            lblMessage.Visible = false;
            panAnalyzeNotice.Visible = true;
            Application.DoEvents();
            panAnalyzeNotice.Select();

            Cursor.Current = Cursors.WaitCursor; // Wait

            butSave.Visible = false;

            // ------------------ Convert File -------------------------

            // Convert the selected file.          
            string convertedFile = string.Concat(fileNameWOExt, ".txt");
            string convertedPathFile = Path.Combine(currentDocPath, convertedFile);

            
            if (!ConvertFile2PlainText(ext, newPathDoc, convertedPathFile))
            {
                if (!File.Exists(convertedPathFile))
                {
                    if (ext.ToUpper() == ".PDF")
                    {
                        //Attempt to extract text from pdf doc using msword engine
                        if (!convertDocViaMSWord(ext, newPathDoc, convertedPathFile))
                        {
                            string msgError = "Your selected PDF document could not be imported. Most likely the PDF file is an image type PDF.  Suggest converting the PDF file to Plain Text via an Optical Character Recognition (OCR) software tool.";
                            MessageBox.Show(msgError, "Document was Not Imported", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            Cursor.Current = Cursors.Default; // Default
                            _ErrorOccurred = true;
                            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                            //this.Close();
                            return;
                        }
                    }
                    else
                    {
                        string msgError = "Your selected PDF document could not be imported. Suggest converting the document file to Plain Text via MS Word and then select the converted text file.";
                        MessageBox.Show(msgError, "Document was Not Imported", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Cursor.Current = Cursors.Default; // Default
                        _ErrorOccurred = true;
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                        this.Close();

                        return;

                    }
                }
            }

 

            // ------------------ Find Pages -------------------------
            int pagesQty = 0;
            string pagesPath = AppFolders.DocParsePage;
            if (pagesPath.Length > 0)
            {             
                if (ext.ToUpper() == ".PDF")
                {
                    ImportMgr = new Manager();
                    pagesQty = ImportMgr.PDF2Pages(newPathDoc, pagesPath);
                    if (pagesQty == -1)
                    {
                        MessageBox.Show("Error: " + ImportMgr.ErrorMessage, "Unable to Find Page Numbers", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    Application.DoEvents();
                    ImportMgr = null;
                }
                else if (ext.ToUpper() == ".DOCX")
                {
                    ImportMgr = new Manager();
                    pagesQty = ImportMgr.DOCX2Pages(newPathDoc, pagesPath);
                    if (pagesQty == -1)
                    {
                        MessageBox.Show("Error: " + ImportMgr.ErrorMessage, "Unable to Find Page Numbers", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    Application.DoEvents();
                    ImportMgr = null;

                }
            }

            // --------------- Information file -- Generate -----------------
           
            StringBuilder sb = new StringBuilder();

            //   sb.AppendLine(string.Concat("File: ", fileInfo.FileName));

            double kb = Convert.ToDouble(_selectedFileInfo.FileSize) / 1000;
            if (pagesQty > 0)
                sb.AppendLine(string.Concat("Pages: ", pagesQty.ToString()));
            sb.AppendLine(string.Concat("Size: ", kb.ToString(), " KB"));
            sb.AppendLine(string.Concat("Created: ", _selectedFileInfo.CreationDate));

            sb.AppendLine(string.Concat("Added by: ", AppFolders.UserName));
            DateTime now = DateTime.Now;
            sb.AppendLine(string.Concat("Added Date: ", now.ToLongDateString()));

            sb.AppendLine(string.Concat("Source: ", selectedFile));
        
            string infoPathFile = Path.Combine(AppFolders.DocInformation, "Info.txt");

            Files.WriteStringToFile(sb.ToString(), infoPathFile);

            // ------------------ Clean File - Remove Tabs - Added 09.20.2018 -------------------------
            CleanFile2(convertedPathFile);


            Cursor.Current = Cursors.Default; // Default

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();

        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void txtbDocName_TextChanged(object sender, EventArgs e)
        {
   
        }





    }
}
