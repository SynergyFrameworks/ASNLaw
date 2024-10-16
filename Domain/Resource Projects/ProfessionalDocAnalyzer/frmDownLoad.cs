using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using Atebion.Common;

namespace ProfessionalDocAnalyzer
{
    public partial class frmDownLoad : MetroFramework.Forms.MetroForm
    {
        public frmDownLoad()
        {
            StackTrace st = new StackTrace(false);
            InitializeComponent();
        }

        private string _FileType = string.Empty;
        private string _baseURL = string.Empty;
        private string _downloadToPath = string.Empty;
        private string _ContentType = string.Empty;

        private string _fileExt = string.Empty;


        public void LoadData(string FileType, string DownloadToPath, string ContentType)
        {
            _FileType = FileType;
            _downloadToPath = DownloadToPath;
            _ContentType = ContentType;

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            string content = string.Empty;
            WebClient webClient;

            chklstDownload.Items.Clear();

            string fileName = string.Empty;

            switch (ContentType)
            {
                case ContentTypes.Tasks_WorkFlow:
                    _baseURL = @"https://www.atebionllc.com/Templates/Tasks/";

                    lblHeader.Text = "Tasks";
                    lblNotice.Text = "Check Tasks to Download";

                    try
                    {

                        webClient = new WebClient();
                        content = webClient.DownloadString(_baseURL);
                        foreach (Match m in Regex.Matches(content, "<a href=\\\"[^\\.]+\\.tsk\">"))
                        {
                            fileName = Regex.Match(m.Value, "\\w+\\.tsk").Value;
                            fileName = Files.GetFileNameWOExt(fileName);
                            chklstDownload.Items.Add(fileName);
                        }

                        _fileExt = ".tsk";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Unable to Download Tasks", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;

                case ContentTypes.Dictionaries:
            
                    _baseURL = @"https://www.atebionllc.com/CA/Dictionaries/";

                    lblHeader.Text = "Dictionaries";
                    lblNotice.Text = "Dictionaries to Download";

                    try
                    {
                        webClient = new WebClient();
                        content = webClient.DownloadString(_baseURL);
                        foreach (Match m in Regex.Matches(content, "<a href=\\\"[^\\.]+\\.dicx\">"))
                        {
                            fileName = Regex.Match(m.Value, "\\w+\\.dicx").Value;
                            fileName = Files.GetFileNameWOExt(fileName);
                            chklstDownload.Items.Add(fileName);
                        }

                        _fileExt = ".dicx";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Unable to Download Dictionaries", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                    break;

                case ContentTypes.KeywordGroups:

                    _baseURL = @"https://www.atebionllc.com/Keywords/";

                    lblHeader.Text = "Keyword Groups";
                    lblNotice.Text = "Keyword Groups to Download";

                    try
                    {

                        webClient = new WebClient();
                        content = webClient.DownloadString(_baseURL);
                        foreach (Match m in Regex.Matches(content, "<a href=\\\"[^\\.]+\\.xml\">"))
                        {
                            fileName = Regex.Match(m.Value, "\\w+\\.xml").Value;
                            fileName = Files.GetFileNameWOExt(fileName);
                            chklstDownload.Items.Add(fileName);
                        }

                        _fileExt = ".xml";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Unable to Download Keyword Groups", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;

                case ContentTypes.Acronym_Dictionaries:

                    _baseURL = @"https://www.atebionllc.com/AcroSeeker/Dictionaries/";

                    lblHeader.Text = "Acronym Dictionaries";
                    lblNotice.Text = "Acronym Dictionaries to Download";

                    try
                    {

                        webClient = new WebClient();
                        content = webClient.DownloadString(_baseURL);
                        foreach (Match m in Regex.Matches(content, "<a href=\\\"[^\\.]+\\.xml\">"))
                        {
                            fileName = Regex.Match(m.Value, "\\w+\\.xml").Value;
                            fileName = Files.GetFileNameWOExt(fileName);
                            chklstDownload.Items.Add(fileName);
                        }

                        _fileExt = ".xml";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Unable to Download Acronym Dictionaries", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;

                case ContentTypes.Acronym_Ignore_Dictionaries:
                    _baseURL = @"https://www.atebionllc.com/AcroSeeker/Ignore/";

                    lblHeader.Text = "Acronym Ignore Dictionaries";
                    lblNotice.Text = "Acronym Ignore Dictionaries to Download";

                    try
                    {
                        webClient = new WebClient();
                        content = webClient.DownloadString(_baseURL);
                        foreach (Match m in Regex.Matches(content, "<a href=\\\"[^\\.]+\\.xml\">"))
                        {
                            fileName = Regex.Match(m.Value, "\\w+\\.xml").Value;
                            fileName = Files.GetFileNameWOExt(fileName);
                            chklstDownload.Items.Add(fileName);
                        }

                        _fileExt = ".xml";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Unable to Download Acronym Ignore Dictionaries", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;

                case ContentTypes.Excel_Templates:

                    if (_FileType == "AR")
                    {
                            _baseURL = @"https://www.atebionllc.com/Templates/AR/";

                            lblHeader.Text = "Excel Analysis Results Templates";
                            lblNotice.Text = "Check Excel Templates to Download";

                            _downloadToPath = AppFolders.AppDataPathToolsExcelTempAR;
                    }
                    else if (_FileType == "DAR")
                    {
                        _baseURL = @"https://www.atebionllc.com/Templates/DAR/";

                        lblHeader.Text = "Excel Deep Analysis Results Templates";
                        lblNotice.Text = "Check Excel Templates to Download";

                        _downloadToPath = AppFolders.AppDataPathToolsExcelTempDAR;
                    }
                    else if (_FileType == "ConceptsDoc")
                    {
                        _baseURL = @"https://www.atebionllc.com/Templates/ConceptsDoc/";

                        lblHeader.Text = "Excel Concept Document Templates";
                        lblNotice.Text = "Check Excel Templates to Download";

                        _downloadToPath = AppFolders.AppDataPathToolsExcelTempConceptsDoc;
                    }
                    else if (_FileType == "ConceptsDocs")
                    {
                        _baseURL = @"https://www.atebionllc.com/Templates/ConceptsDocs/";

                        lblHeader.Text = "Excel Concept Compare Documents Templates";
                        lblNotice.Text = "Check Excel Templates to Download";

                        _downloadToPath = AppFolders.AppDataPathToolsExcelTempConceptsDocs;
                    }
                    else if (_FileType == "DicDoc")
                    {
                        _baseURL = @"https://www.atebionllc.com/Templates/DicDoc/";

                        lblHeader.Text = "Excel Dictionary Analysis Document Templates";
                        lblNotice.Text = "Check Excel Templates to Download";

                        _downloadToPath = AppFolders.AppDataPathToolsExcelTempDicDoc;
                    }
                    else if (_FileType == "DicDocs")
                    {
                        _baseURL = @"https://www.atebionllc.com/Templates/DicDocs/";

                        lblHeader.Text = "Excel Dictionary Compare Documents Templates";
                        lblNotice.Text = "Check Excel Templates to Download";

                        _downloadToPath = AppFolders.AppDataPathToolsExcelTempDicDocs;
                    }
                    else if (_FileType == "DicRAM")
                    {
                        _baseURL = @"https://www.atebionllc.com/Templates/DicRAM/";

                        lblHeader.Text = "Excel Dictionary RAM Templates";
                        lblNotice.Text = "Check Excel Templates to Download";

                        _downloadToPath = AppFolders.AppDataPathToolsExcelTempDicRAM;
                    }

                    try
                    {
                        var webClient2 = new WebClient();

                        string content2 = webClient2.DownloadString(_baseURL);

                        //WebClient r = new WebClient();
                        //content = r.DownloadString(_baseURL);
                        foreach (Match m in Regex.Matches(content2, "<a href=\\\"[^\\.]+\\.xml\">"))
                        {
                            fileName = Regex.Match(m.Value, "\\w+\\.xml").Value;
                            fileName = Files.GetFileNameWOExt(fileName);
                            chklstDownload.Items.Add(fileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Unable to Download Templates", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                break;


            }

        }

        //private void butOK_Click(object sender, EventArgs e)
        //{
        //    switch (_ContentType)
        //    {
        //        case ContentTypes.Tasks_WorkFlow:
        //            Download_Tasks();

        //            break;

        //        case ContentTypes.Dictionaries:
        //            Download_Dictionaries();

        //            break;

        //        case ContentTypes.Acronym_Dictionaries:
        //            Download_Acroynm_Dictionaries();

        //            break;

        //        case ContentTypes.Acronym_Ignore_Dictionaries:
        //            Download_Acroynm_Dictionaries(); // Can use the same code as Acronym Dictionaries

        //            break;

        //        case ContentTypes.Excel_Templates:
        //            Download_Excel_Templates();

        //            break;
        //    }
        //}

        private void Download_Tasks()
        {
            Cursor.Current = Cursors.WaitCursor;
            int counter = 0;
            int alreadyExisted = 0;
            string storeLocation;
            string fullFileName = string.Empty;
            WebClient r = new WebClient();
            foreach (var fileName in chklstDownload.CheckedItems)
            {
                fullFileName = string.Concat(fileName.ToString(), ".tsk");
                storeLocation = Path.Combine(_downloadToPath, fullFileName);

                if (!File.Exists(storeLocation))
                {

                    r.DownloadFile(_baseURL + fullFileName, storeLocation);
                    counter++;
                }
                else
                {
                    alreadyExisted++;
                }
            }

            if (alreadyExisted == 0)
                lblSaved1.Text = string.Concat("Downloaded  ", counter.ToString());
            else
                lblSaved1.Text = string.Concat("Existing Tasks Not Replaced  ", alreadyExisted.ToString(), "  Downloaded  ", counter.ToString());

            Cursor.Current = Cursors.Default;
            lblSaved1.Visible = true;
            this.Refresh();
            System.Threading.Thread.Sleep(2000);
            lblSaved1.Visible = false;
            this.Refresh();

            this.DialogResult = DialogResult.OK;

            this.Close();

        }


        private void Download_Excel_Templates()
        {
            Cursor.Current = Cursors.WaitCursor;
            int counter = 0;
            int alreadyExisted = 0;
            string storeLocation_xml;
            string fullFileName_xml = string.Empty;

            string storeLocation_xlsx;
            string fullFileName_xlsx = string.Empty;


            WebClient r = new WebClient();
            foreach (var fileNameX in chklstDownload.CheckedItems)
            {
                fullFileName_xml = string.Concat(fileNameX.ToString(), ".xml");
                storeLocation_xml = Path.Combine(_downloadToPath, fullFileName_xml);

                //if (_FileType == "AR" || _FileType == "DAR")
                //{
                    fullFileName_xlsx = string.Concat(fileNameX.ToString(), ".xlsx");
                    storeLocation_xlsx = Path.Combine(_downloadToPath, fullFileName_xlsx);

                    if (!File.Exists(storeLocation_xlsx))
                    {

                        r.DownloadFile(_baseURL + fullFileName_xlsx, storeLocation_xlsx);
                        counter++;
                    }
                    else
                    {
                        alreadyExisted++;
                    }
                //}

                if (!File.Exists(storeLocation_xml))
                {

                    r.DownloadFile(_baseURL + fullFileName_xml, storeLocation_xml);
                    counter++;
                }
                else
                {
                    alreadyExisted++;
                }

            }

            if (counter > 1)
                counter = counter / 2;

            if (alreadyExisted == 0)
                lblSaved1.Text = string.Concat("Downloaded  ", counter.ToString());
            else
                lblSaved1.Text = string.Concat("Existing File(s) Not Replaced  ", alreadyExisted.ToString(), "  Downloaded  ", counter.ToString());

            Cursor.Current = Cursors.Default;
            lblSaved1.Visible = true;
            this.Refresh();
            System.Threading.Thread.Sleep(2000);
            lblSaved1.Visible = false;

            this.Refresh();

            this.DialogResult = DialogResult.OK;

            this.Close();

        }

 

        private void Download_Acroynm_Dictionaries()
        {
            Cursor.Current = Cursors.WaitCursor;
            int counter = 0;
            int alreadyExisted = 0;
            string storeLocation;
            string fullFileName = string.Empty;
            WebClient r = new WebClient();
            foreach (var fileName in chklstDownload.CheckedItems)
            {
                fullFileName = string.Concat(fileName.ToString(), ".xml");
                storeLocation = Path.Combine(_downloadToPath, fullFileName);

                if (!File.Exists(storeLocation))
                {

                    r.DownloadFile(_baseURL + fullFileName, storeLocation);
                    counter++;
                }
                else
                {
                    alreadyExisted++;
                }
            }

            if (alreadyExisted == 0)
                lblSaved1.Text = string.Concat("Downloaded  ", counter.ToString());
            else
                lblSaved1.Text = string.Concat("Existing Lib.s Not Replaced  ", alreadyExisted.ToString(), "  Downloaded  ", counter.ToString());

            Cursor.Current = Cursors.Default;
            lblSaved1.Visible = true;
            this.Refresh();
            System.Threading.Thread.Sleep(2000);
            lblSaved1.Visible = false;
            this.Refresh();

            this.DialogResult = DialogResult.OK;

            this.Close();

        }

        private void Download_Dictionaries()
        {

            Cursor.Current = Cursors.WaitCursor;
            int counter = 0;
            int alreadyExisted = 0;
            string storeLocation_txt;
            string fullFileName_txt = string.Empty;

            string storeLocation_dicx;
            string fullFileName_dicx = string.Empty;

            WebClient r = new WebClient();
            foreach (var fileName in chklstDownload.CheckedItems)
            {
                fullFileName_txt = string.Concat(fileName.ToString(), ".txt");
                storeLocation_txt = Path.Combine(_downloadToPath, fullFileName_txt);

                if (_FileType == "dicx")
                {
                    fullFileName_dicx = string.Concat(fileName.ToString(), ".dicx");
                    storeLocation_dicx = Path.Combine(_downloadToPath, fullFileName_dicx);

                    if (!File.Exists(storeLocation_dicx))
                    {

                        r.DownloadFile(_baseURL + fullFileName_dicx, storeLocation_dicx);
                        counter++;
                    }
                    else
                    {
                        alreadyExisted++;
                    }
                }

                if (!File.Exists(storeLocation_txt))
                {

                    r.DownloadFile(_baseURL + fullFileName_txt, storeLocation_txt);
                    counter++;
                }
                else
                {
                    alreadyExisted++;
                }

            }

            if (counter > 1)
                counter = counter / 2;

            if (alreadyExisted == 0)
                lblSaved1.Text = string.Concat("Downloaded  ", counter.ToString());
            else
                lblSaved1.Text = string.Concat("Existing File(s) Not Replaced  ", alreadyExisted.ToString(), "  Downloaded  ", counter.ToString());

            Cursor.Current = Cursors.Default;
            lblSaved1.Visible = true;
            this.Refresh();
            System.Threading.Thread.Sleep(2000);
            lblSaved1.Visible = false;
            this.Refresh();

            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }

        private void butOK_Click_1(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            int counter = 0;
            int alreadyExisted = 0;
            string storeLocation_xml;
            string fullFileName_xml = string.Empty;

            string storeLocation_xlsx;
            string fullFileName_xlsx = string.Empty;

            string storeLocation_txt = string.Empty;
            string fullFileName_txt = string.Empty;

            string storeLocation_dicx = string.Empty;
            string fullFileName_dicx = string.Empty;


            WebClient r = new WebClient();
            foreach (var fileName in chklstDownload.CheckedItems)
            {
                if (_ContentType ==  ContentTypes.Excel_Templates)
                {
                    fullFileName_xml = string.Concat(fileName.ToString(), ".xml");
                    storeLocation_xml = Path.Combine(_downloadToPath, fullFileName_xml);

  
                    fullFileName_xlsx = string.Concat(fileName.ToString(), ".xlsx");
                    storeLocation_xlsx = Path.Combine(_downloadToPath, fullFileName_xlsx);

                    if (!File.Exists(storeLocation_xlsx))
                    {

                        r.DownloadFile(_baseURL + fullFileName_xlsx, storeLocation_xlsx);
                        counter++;
                    }
                    else
                    {
                        alreadyExisted++;
                    }
                

                    if (!File.Exists(storeLocation_xml))
                    {

                        r.DownloadFile(_baseURL + fullFileName_xml, storeLocation_xml);
                        counter++;
                    }
                    else
                    {
                        alreadyExisted++;
                    }
                }
                else if (_ContentType == ContentTypes.Dictionaries)
                {
                    fullFileName_txt = string.Concat(fileName.ToString(), ".txt");
                    storeLocation_txt = Path.Combine(_downloadToPath, fullFileName_txt);


                    fullFileName_dicx = string.Concat(fileName.ToString(), ".dicx");
                    storeLocation_dicx = Path.Combine(_downloadToPath, fullFileName_dicx);

                    if (!File.Exists(storeLocation_txt))
                    {

                        r.DownloadFile(_baseURL + fullFileName_txt, storeLocation_txt);
                        counter++;
                    }
                    else
                    {
                        alreadyExisted++;
                    }


                    if (!File.Exists(storeLocation_dicx))
                    {

                        r.DownloadFile(_baseURL + fullFileName_dicx, storeLocation_dicx);
                        counter++;
                    }
                    else
                    {
                        alreadyExisted++;
                    }
                }
                else if (_ContentType == ContentTypes.KeywordGroups || _ContentType == ContentTypes.Acronym_Dictionaries || _ContentType == ContentTypes.Acronym_Ignore_Dictionaries)
                {
                    fullFileName_xml = string.Concat(fileName.ToString(), ".xml");
                    storeLocation_xml = Path.Combine(_downloadToPath, fullFileName_xml);

                    if (!File.Exists(storeLocation_xml))
                    {

                        r.DownloadFile(_baseURL + fullFileName_xml, storeLocation_xml);
                        counter++;
                    }
                    else
                    {
                        alreadyExisted++;
                    }

                }
                else if (_ContentType == ContentTypes.Tasks_WorkFlow)
                {
                    fullFileName_xml = string.Concat(fileName.ToString(), ".tsk");
                    storeLocation_xml = Path.Combine(_downloadToPath, fullFileName_xml);

                    if (!File.Exists(storeLocation_xml))
                    {

                        r.DownloadFile(_baseURL + fullFileName_xml, storeLocation_xml);
                        counter++;
                    }
                    else
                    {
                        alreadyExisted++;
                    }

                }

            }
            
   

            if (counter > 1)
                counter = counter / 2;

            if (alreadyExisted == 0)
                lblSaved1.Text = string.Concat("Downloaded  ", counter.ToString());
            else
                lblSaved1.Text = string.Concat("Existing File(s) Not Replaced  ", alreadyExisted.ToString(), "  Downloaded  ", counter.ToString());

            Cursor.Current = Cursors.Default;
            lblSaved1.Visible = true;
            this.Refresh();
            System.Threading.Thread.Sleep(2000);
            lblSaved1.Visible = false;
            this.Refresh();

            this.DialogResult = DialogResult.OK;

            this.Close();

        }

    }
}
