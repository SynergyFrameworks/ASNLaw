using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
//using Atebion.Excel21;
//using Atebion.Excel.OpenXML;
using Atebion.Word.OpenXML;
using Atebion.RTF.Generation;
using Atebion.HTML.Generation;
//using Atebion.Export.Excel;
using Atebion.Excel.Output;
using Atebion.Common;



namespace ProfessionalDocAnalyzer
{
    public partial class frmExportSec : MetroFramework.Forms.MetroForm
    {
        //public void LoadData(DataTable dt, bool PageExists)
        //{
        //    _dt = dt;

        //    StackTrace st = new StackTrace(false);

        //    InitializeComponent();

        //    this.AcceptButton = this.butExport;
        //    this.CancelButton = this.butCancel;
        //    //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        //    this.MaximizeBox = false;
        //    this.MinimizeBox = false;
        //    this.ShowInTaskbar = false;
        //    this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

        //    _PageExists = PageExists;

        //    if (_PageExists)
        //    {
        //        chkbPage.Visible = true;
        //    }
        //    else
        //    {
        //        chkbPage.Visible = false;
        //    }

        //    //_DocType = DocType;
        //    //_ProposalPath = ProposalPath;

        //  //  LoadData();
        //}

        private string _ProjectName = string.Empty;
        private string _DocName = string.Empty;
        private string _DocParsedSec = string.Empty;
        private string _DocParsedSecExport = string.Empty;
        private string _DocParsedSecHTML = string.Empty;
        private string _DocParsedSecXML = string.Empty;
        private string _DocParsedSecNotes = string.Empty;




        public void LoadData(DataTable dt, int FoundQty, string SearchCriteria, bool PageExists, string ProjectName, string DocName, string DocParsedSec, string DocParsedSecExport, string DocParsedSecHTML, string DocParsedSecXML, string DocParsedSecNotes)
        {
            InitializeComponent();

            this.AcceptButton = this.butExport;
            this.CancelButton = this.butCancel;
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            _dt = dt;
            _FoundQty = FoundQty;
            _SearchCriteria = SearchCriteria;
            _QualityCheck = false;
            _PageExists = PageExists;

            _ProjectName = ProjectName;
            _DocName = DocName;
            _DocParsedSec = DocParsedSec;
            _DocParsedSecExport = DocParsedSecExport;
            _DocParsedSecHTML = DocParsedSecHTML;
            _DocParsedSecXML = DocParsedSecXML;
            _DocParsedSecNotes = DocParsedSecNotes;

            if (_PageExists)
            {
                chkbPage.Visible = true;
            }
            else
            {
                chkbPage.Visible = false;
            }

            PopulateData();
        }

        //public void LoadData(DataTable dt, int FoundQty, string SearchCriteria, bool QualityCheck, bool PageExists)
        //{
        //    InitializeComponent();

        //    this.AcceptButton = this.butExport;
        //    this.CancelButton = this.butCancel;
        //    //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        //    this.MaximizeBox = false;
        //    this.MinimizeBox = false;
        //    this.ShowInTaskbar = false;
        //    this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

        //    _dt = dt;
        //    _FoundQty = FoundQty;
        //    _SearchCriteria = SearchCriteria;
        //    _QualityCheck = QualityCheck;
        //    _PageExists = PageExists;

        //    if (_PageExists)
        //    {
        //        chkbPage.Visible = true;
        //    }
        //    else
        //    {
        //        chkbPage.Visible = false;
        //    }

        //    PopulateData();
        //}

        #region Private Var.s

        private DataTable _dt;

      //  string _DocType = string.Empty;
        private string _ProposalPath = string.Empty;
        private string _UID = string.Empty;
        private string _exportPathFile = string.Empty;

        private bool _QualityCheck = false;

        // Search Results
        private int _FoundQty = 0;
        private string _SearchCriteria = string.Empty;

        private bool isMSWordInstallled = false;

        private string _exportFileName = string.Empty;

    //    ProposalDetailMgr _ProposalDetailMgr;

        //string _Dir_CurrentParsedSecXML = string.Empty;
        //string _Dir_CurrentParsedSec = string.Empty;
        //string _Dir_CurrentParsedSecNotes = string.Empty;
        //string _Dir_Export = string.Empty;
        string _headerCaptions = string.Empty;
        string _ColumnWidth = string.Empty;

        private DataSet _ds;

        private bool _PageExists = false; // 8.15.2018

   //     private bool isRTF = false; // 09.08.2013

        private Modes _ExportMode = Modes.Excel;

        private int _ExcelTemplatesQty = 0;

        private enum Modes
        {
            Excel = 0,
            Word = 1,
            HTML = 2,
            SharePoint = 3,
            RFP365 = 4 
        }

        private bool _NoCaption_Use = false; // Added for Excel templates
        private const string NONE = "-- None --"; // Added for Excel templates

        #endregion
        #region Private Functions

        private void PopulateData()
        {
         //   _ProposalDetailMgr = new ProposalDetailMgr(_ProposalPath, _DocType);

            //_Dir_CurrentParsedSec = _ProposalDetailMgr.Dir_CurrentParsedSec;
            //_Dir_CurrentParsedSecNotes = _ProposalDetailMgr.Dir_CurrentParsedSecNotes;
            //_Dir_CurrentParsedSecXML = _ProposalDetailMgr.Dir_CurrentParsedSecXML;
            //_Dir_Export = _ProposalDetailMgr.Dir_Export;

         //   lblLinkRFP365.Links.Add(0, 6, "http://www.rfp365.com/"); // Removed was causing an errror

            if (Directories.IsInternetAvailable())
            {
                butDownloadTemplate.Visible = true;
            }
            else
            {
                butDownloadTemplate.Visible = false;
            }

            if (_dt == null)
            {
                string ParseResultsFile = Path.Combine(_DocParsedSecXML, "ParseResults.xml");
                if (!File.Exists(ParseResultsFile))
                {
                    string msg = string.Concat("Unable able to find Analysis Results file: ", ParseResultsFile);
                    lblMessage.Text = msg;
                }
                else
                {
                    //   _ds = XMLDataSet.LoadDatasetFromXml(ParseResultsFile);
                    GenericDataManger gDataMgr = new GenericDataManger();
                    _ds = gDataMgr.LoadDatasetFromXml(ParseResultsFile);
                    _dt = _ds.Tables[0];
                }
            }
            else
            {
                string msg = string.Format("The Parse Results for this Export are only those {0} segments found by Search, using Search Criteria: '{1}'.", _FoundQty.ToString(), _SearchCriteria);

                lblMessage.Text = msg;

            }

            isMSWordInstallled = MSWordAutomation.IsMSWordInstalled();

            this.txtExportFileName.Text = GetNewRptFileName();

            _ExcelTemplatesQty = LoadExcelTemplates();

            if (_ExcelTemplatesQty == 0)
            {
                // workgroup -- Copy templates from Local to current workgroup
                if (!AppFolders.IsLocal) // Not Local
                {
                    // Removed 10.26.2017 //string localTemplatePath = Path.Combine(AppFolders.AppDataPath_Local, "Tools", "ExcelTemp", "AR");
                    // Removed 10.26.2017 //string[] files_xml = Directory.GetFiles(localTemplatePath);
                    string[] files_xml = Directory.GetFiles(AppFolders.AppDataPathToolsExcelTempAR); // Added 10.26.2017
                    string fileName = string.Empty;
                    string workgroupTemplatePathFile = string.Empty;
                    if (files_xml.Length > 0)
                    {
                        foreach (string file_xml in files_xml)
                        {
                            fileName = Files.GetFileName(file_xml);
                            workgroupTemplatePathFile = Path.Combine(AppFolders.AppDataPathToolsExcelTempAR, fileName);

                            if (!File.Exists(workgroupTemplatePathFile))
                                File.Copy(file_xml, workgroupTemplatePathFile);
                        }

                        _ExcelTemplatesQty = LoadExcelTemplates();
                        if (_ExcelTemplatesQty == 0)
                        {
                            string msg = string.Concat("Currently you don’t have any Excel Export Templates.", Environment.NewLine, Environment.NewLine,
                                "However, you can still Export to an Excel file without Templates.", Environment.NewLine, Environment.NewLine,
                                "You can Create, Download, and Edit Excel Export Templates from the Tools/Settings area. Click the Tools button at the bottom left and then select Settings under the Tools area.");

                            lblMessage.Text = msg;
                        }

                    }
                }
                else // Local
                {
                    string msg = string.Concat("Currently you don’t have any Excel Export Templates.", Environment.NewLine, Environment.NewLine,
                        "However, you can still Export to an Excel file without Templates.", Environment.NewLine, Environment.NewLine,
                        "You can Create, Download, and Edit Excel Export Templates from the Tools/Settings area. Click the Tools button at the bottom left and then select Settings under the Tools area.");

                    lblMessage.Text = msg;
                    
                }
            }


            if (_ExcelTemplatesQty == 0)
            {
                lblExcelTemplate.Visible = false;
                cboExcelTemplate.Visible = false;
                picExcelTemplate.Visible = false;

        //        lblMessage.Text = "You can create and download Excel Templates from the Tools/Setting area. Click the Tools button at the bottom right, select Settings ...";    

            }
            else
            {
                lblExcelTemplate.Visible = true;
                cboExcelTemplate.Visible = true;
                picExcelTemplate.Visible = true;
            }

        }

        private int LoadExcelTemplates()
        {

            int qtyTemplates = 0;
            string[] files = Directory.GetFiles(AppFolders.AppDataPathToolsExcelTempAR, "*.xlsx");

            qtyTemplates = files.Length;

            cboExcelTemplate.Items.Clear();

            cboExcelTemplate.Items.Add(NONE);

            string templateName = string.Empty;
            for (int i = 0; i < qtyTemplates; i++)
            {
                templateName = Files.GetFileNameWOExt(files[i]);
                cboExcelTemplate.Items.Add(templateName);
            }

            cboExcelTemplate.SelectedIndex = 0;

            return qtyTemplates;
        }

        private string GetNewRptFileName()
        {
            string BASE_NAME = string.Concat(_DocName, "_"); 
            string filename = string.Empty;
            string pathFile = string.Empty;

            for (int i = 1; i < 32000; i++)
            {
                filename = string.Concat(BASE_NAME, i.ToString(), ".html");
                pathFile = Path.Combine(_DocParsedSecExport, filename);
                if (!File.Exists(pathFile))
                {
                    filename = string.Concat(BASE_NAME, i.ToString(), ".docx");
                    pathFile = Path.Combine(_DocParsedSecExport, filename);
                    if (!File.Exists(pathFile))
                    {
                        filename = string.Concat(BASE_NAME, i.ToString(), ".xlsx");
                        pathFile = Path.Combine(_DocParsedSecExport, filename);
                        if (!File.Exists(pathFile))
                        {
                            return string.Concat(BASE_NAME, i.ToString());
                        }
                   }  
                }
            }

            return string.Concat(BASE_NAME, "5000");;
        }

        private string GetExt()
        {
 
            if (this.rbHTML.Checked)
            {
                return ".html";
            }
            else if (this.rbWordDOCX.Checked)
            {
                return ".docx";
            }

            return ".xlsx";

        }

        private void Export2ExcelTemplate()
        {
            ExcelOutput excelOutPut = new ExcelOutput();

            string TemplateName = this.cboExcelTemplate.Text;

            if (TemplateName.Length == 0) // Should Never occur, otherwise Bad code
            {
                lblMessage.Text = "Please select an Excel Template.";
                return;
            }
            if (TemplateName == NONE) // Should Never occur, otherwise Bad code
            {
                lblMessage.Text = "Please select an Excel Template.";
                return;
            }

            _exportPathFile = string.Concat(_DocParsedSecExport, @"\", _exportFileName, ".xlsx");

            if (File.Exists(_exportPathFile))
            {
                MessageBox.Show("Please enter another Export File Name.", "Export File Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblMessage.Text = string.Concat("Export File Already Exists", Environment.NewLine, Environment.NewLine, "Please enter another Export File Name.");
              
                return;
            }


            // Gather data
            DataTable dtEmpty;

            if (_NoCaption_Use)
            {
                dtEmpty = CreateNewDT_NoCaption();
            }
            else
            {
                dtEmpty = CreateNewDT();
            }

            DataTable dtExport = PopulateDataTable(_dt, dtEmpty);

            

            // Populate Metadata
            excelOutPut.Metadata_Date = string.Concat(DateTime.Now.ToLongDateString(), "  ", DateTime.Now.ToLongTimeString());
            excelOutPut.Metadata_DocName = _DocName;
            excelOutPut.Metadata_ProjectName = _ProjectName;
            excelOutPut.Metadata_YourName = AppFolders.UserName;

            if (!excelOutPut.ExportAResults2Template(dtEmpty, TemplateName, AppFolders.AppDataPathToolsExcelTempAR, _DocParsedSecExport, _exportFileName))
            {
                MessageBox.Show(excelOutPut.ErrorMessage, "Unable to Generate an Excel Export via a Template", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                lblMessage.Text = string.Concat(excelOutPut.ErrorMessage, Environment.NewLine, Environment.NewLine, "Unable to Generate an Excel Export via a Template");

                return;
            }

        }

        private void ExportToWord()
        {
            WordOXML word = new WordOXML();


            _headerCaptions = string.Empty; // Added 10.19.2013

            DataTable dtEmpty;

            if (chkbNoCaption.Checked) // Added 10.19.2013
                dtEmpty = CreateNewDT_NoCaption(); // Added 10.19.2013
            else
                dtEmpty = CreateNewDT();

            DataTable dtExport = PopulateDataTable(_dt, dtEmpty);

            _exportPathFile = string.Concat(_DocParsedSecExport, @"\", _exportFileName, ".docx");

            if (File.Exists(_exportPathFile))
            {
                string exFileMsg = "This file name already exist. Do you want to replace the existing file?";
                if (MessageBox.Show(exFileMsg, "File Already Exist", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    lblMessage.Text = "Parse results were not exported to a MS Word file. Please enter another file name.";
                    return;
                }

                if (Files.FileIsLocked(_exportPathFile))
                {
                    exFileMsg = "Unable to replace the existing exported file because the file is opened by another application.";
                    MessageBox.Show(exFileMsg, "Exported File is Open", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblMessage.Text = exFileMsg;
                    return;
                }
            }



            if (_QualityCheck) // added 05.30.2016
            {
                if (this.chkQualityCheck.Checked)
                {
                    word.writeDocx4(dtExport, _headerCaptions, _exportPathFile, _DocName, _ProjectName, _DocParsedSecHTML, _ColumnWidth); // Added enbedded HTML parsed segements
                }
                else
                {
                    word.writeDocx2(dtExport, _headerCaptions, _exportPathFile, _DocName, _ProjectName, _DocParsedSecHTML, _ColumnWidth); // Added enbedded HTML parsed segements
                }
            }
            else
            {
                word.writeDocx2(dtExport, _headerCaptions, _exportPathFile, _DocName, _ProjectName, _DocParsedSecHTML, _ColumnWidth); // Added enbedded HTML parsed segements
            }

            

 

        }


        private void ExportToExcel()
        {


        //    ExportToExcel excel = new ExportToExcel();

            DataTable dtEmpty;

            if (chkbNoCaption.Checked) // Added 10.14.2013
                dtEmpty = CreateNewDT_NoCaption(); // Added 10.14.2013
            else
                dtEmpty = CreateNewDT();


            DataTable dtExport = PopulateDataTable(_dt, dtEmpty);

            _exportPathFile = string.Concat(_DocParsedSecExport, @"\", _exportFileName, ".xlsx");

            if (File.Exists(_exportPathFile))
            {
                string exFileMsg = "This file name already exist. Do you want to replace the existing file?";
                if (MessageBox.Show(exFileMsg, "File Already Exist", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    lblMessage.Text = "Parse results were not exported to an Excel file. Please enter another file name.";
                    return;
                }

                if (Files.FileIsLocked(_exportPathFile))
                {
                    exFileMsg = "Unable to replace the existing exported file because the file is opened by another application.";
                    MessageBox.Show(exFileMsg, "Exported File is Open", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblMessage.Text = exFileMsg;
                    return;
                }
            }

 
            // Atebion.Export.Excel.ExportToExcel excport2Excel = new Atebion.Export.Excel.ExportToExcel();
            //     excport2Excel.CreateXLS2(dtExport, _headerCaptions, _exportPathFile, _exportPathFile);

            Atebion.Excel.Output.ExcelOutput excelOutput = new ExcelOutput();
            excelOutput.ExportAResults(dtExport, _headerCaptions, _exportPathFile, _exportPathFile);
 
        }

        private void ExportToRTF()
        {
            RTFGen rtfGen = new RTFGen();

            DataTable dtEmpty;

            if (chkbNoCaption.Checked) // Added 10.19.2013
                dtEmpty = CreateNewDT_NoCaption(); // Added 10.19.2013
            else
                dtEmpty = CreateNewDT();

            DataTable dtExport = PopulateDataTable(_dt, dtEmpty);

            _exportPathFile = string.Concat(_DocParsedSecExport, @"\", _exportFileName, ".rtf");

            if (File.Exists(_exportPathFile))
            {
                string exFileMsg = "This file name already exist. Do you want to replace the existing file?";
                if (MessageBox.Show(exFileMsg, "File Already Exist", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    lblMessage.Text = "Parse results were not exported to a RTF file. Please enter another file name.";
                    return;
                }

                if (Files.FileIsLocked(_exportPathFile))
                {
                    exFileMsg = "Unable to replace the existing exported file because the file is opened by another application.";
                    MessageBox.Show(exFileMsg, "Exported File is Open", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblMessage.Text = exFileMsg;
                    return;
                }
            }

          //  rtfGen.writeRTF(dtExport, _headerCaptions, _ColumnWidth, _exportPathFile, _exportPathFile, _ProjectName, "Project", "Document");
            rtfGen.writeRTF(dtExport, _headerCaptions, _ColumnWidth, _exportPathFile, _DocName, _ProjectName, "Project", "Document Name"); // Change 09.21.2013

        }

        private void ExportToHTML()
        {
 


            // Get Keywords -- Added 06.06.2014
            //string[] inKeywords = new string[0]; // empty string array
            //if (File.Exists(AppFolders.DocParsedSecKeywords + @"\Keywords.txt"))
            //{
            //    inKeywords = File.ReadAllLines(AppFolders.DocParsedSecKeywords + @"\Keywords.txt", Encoding.UTF8);
            //}
            
            
            HTMLGen htmlGen = new HTMLGen();

            DataTable dtEmpty;

            if (chkbNoCaption.Checked) // Added 10.19.2013
                dtEmpty = CreateNewDT_NoCaption(); // Added 10.19.2013
            else
                dtEmpty = CreateNewDT();

            DataTable dtExport = PopulateDataTable(_dt, dtEmpty);

            _exportPathFile = string.Concat(_DocParsedSecExport, @"\", _exportFileName, ".html");

            if (File.Exists(_exportPathFile))
            {
                string exFileMsg = "This file name already exist. Do you want to replace the existing file?";
                if (MessageBox.Show(exFileMsg, "File Already Exist", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    lblMessage.Text = "Parse results were not exported to a HTML file. Please enter another file name.";
                    return;
                }

                if (Files.FileIsLocked(_exportPathFile))
                {
                    exFileMsg = "Unable to replace the existing exported file because the file is opened by another application.";
                    MessageBox.Show(exFileMsg, "Exported File is Open", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblMessage.Text = exFileMsg;
                    return;
                }
            }

            htmlGen.WriteHTML2(dtExport, _headerCaptions, _ColumnWidth, _exportPathFile, _DocName, _ProjectName, "Project", "Document"); // Changed 08.12.2013

        }

   
        private DataTable CreateNewDT_NoCaption() // Added 10.14.2013
        {
            DataTable table = new DataTable();

            table.Columns.Add("UID", typeof(string)); // Added 08.20.2013
            _headerCaptions = "UID";

            bool forSharePoint = false;
            if (chkb4SharePoint.Checked)
            {
                forSharePoint = true;
            }

            if (_QualityCheck) // added 05/18/2016
            {
                if (this.chkQualityCheck.Checked)
                {
                    table.Columns.Add("Quality", typeof(string));
                    _headerCaptions = "Quality Check";
                    _ColumnWidth = "1";
                }
            }

            if (!forSharePoint)
            {
                if (this.chkbLineNos.Checked) 
                {
                    table.Columns.Add("Lines", typeof(string));
                    if (_headerCaptions == string.Empty)
                        _headerCaptions = "Lines";
                    else
                        _headerCaptions += "|Lines";

                    _ColumnWidth = "1";
                }
            }

            table.Columns.Add("NoCaption", typeof(string));
            if (_headerCaptions == string.Empty)
            {
                _headerCaptions = "No and Caption";
                _ColumnWidth = "5";
            }
            else
            {
                _headerCaptions += "|No and Caption";
                _ColumnWidth += "|5";
            }

            if (chkbKeywords.Checked)
            {
                table.Columns.Add("Keywords", typeof(string));
                if (_headerCaptions == string.Empty)
                {
                    _headerCaptions = "Keywords";
                    _ColumnWidth = "4";
                }
                else
                {
                    _headerCaptions += "|Keywords";
                    _ColumnWidth += "|4";
                }
            }
            if (chkbSectionText.Checked)
            {
                table.Columns.Add("Text", typeof(string));
                if (_headerCaptions == string.Empty)
                {
                    _headerCaptions = "Text";
                    _ColumnWidth = "9";
                }
                else
                {
                    _headerCaptions += "|Text";

                    if (chkbNotes.Checked)
                        _ColumnWidth += "|3";
                    else
                        _ColumnWidth += "|6";
                }
            }
            if (chkbNotes.Checked)
            {
                table.Columns.Add("Notes", typeof(string));
                if (_headerCaptions == string.Empty)
                {
                    _headerCaptions = "Notes";
                    _ColumnWidth = "9";
                }
                else
                {
                    _headerCaptions += "|Notes";
                    if (chkbSectionText.Checked)
                        _ColumnWidth += "|3";
                    else
                        _ColumnWidth += "|6";
                }
            }

            
            if (_PageExists)
            {
                if (chkbPage.Checked)
                {
                    table.Columns.Add("Page", typeof(string)); // Added 05.14.2019
                    _headerCaptions += "|Page";
                    _ColumnWidth += "|3";
                }

            }

            if (forSharePoint)
            {
                if (this.chkbLineNos.Checked)
                {
                    table.Columns.Add("Lines", typeof(string));
                    _headerCaptions += "|Lines";
                    _ColumnWidth += "|1";
                }
            }


            return table;
        }
   

        private DataTable CreateNewDT()
        {
            DataTable table = new DataTable();

            table.Columns.Add("UID", typeof(string)); // Added 08.20.2013
            _headerCaptions = "UID";

            if (_QualityCheck) // added 05.18.2016
            {
                if (this.chkQualityCheck.Checked)
                {
                    table.Columns.Add("Quality", typeof(string));
                    _headerCaptions = "Quality Check";
                    _ColumnWidth = "1";
                }
            }

            if (this.chkbLineNos.Checked) // Added 09.19.2013
            {
                table.Columns.Add("Lines", typeof(string));
                if (_headerCaptions == string.Empty)
                {
                    _headerCaptions = "Lines";
                    _ColumnWidth = "1";
                }
                else
                {
                    _headerCaptions += "|Lines";
                    _ColumnWidth += "|1";
                }
            }

            if (chkbNumber.Checked)
            {
                table.Columns.Add("Number", typeof(string));
                if (_headerCaptions == string.Empty)
                {             
                    _headerCaptions = "Number";
                    _ColumnWidth = "2";
                }
                else
                {
                    _headerCaptions += "|Number";
                    _ColumnWidth += "|2";
                }
            }
            
            if (chkbCaption.Checked)
            {
                table.Columns.Add("Caption", typeof(string));
                if (_headerCaptions == string.Empty)
                {
                    _headerCaptions = "Caption";
                    _ColumnWidth = "4";
                }
                else
                {
                    _headerCaptions += "|Caption";
                    _ColumnWidth += "|4";
                }

            }
            if (chkbKeywords.Checked)
            {
                table.Columns.Add("Keywords", typeof(string));
                if (_headerCaptions == string.Empty)
                {
                    _headerCaptions = "Keywords";
                    _ColumnWidth = "4";
                }
                else
                {
                    _headerCaptions += "|Keywords";
                    _ColumnWidth += "|4";
                }
            }
            if (chkbSectionText.Checked)
            {
                table.Columns.Add("Text", typeof(string));
                if (_headerCaptions == string.Empty)
                {
                    _headerCaptions = "Text";
                    _ColumnWidth = "9";
                }
                else
                {
                    _headerCaptions += "|Text";

                    if (chkbNotes.Checked)
                        _ColumnWidth += "|3";
                    else
                        _ColumnWidth += "|6";
                }
            }
            if (chkbNotes.Checked)
            {
                table.Columns.Add("Notes", typeof(string));
                if (_headerCaptions == string.Empty)
                {
                    _headerCaptions = "Notes";
                    _ColumnWidth = "9";
                }
                else
                {
                    _headerCaptions += "|Notes";
                    if (chkbSectionText.Checked)
                        _ColumnWidth += "|3";
                    else
                        _ColumnWidth += "|6";
                }
            }

            // Added 8.15.2018
            if (_PageExists)
            {
                if (chkbPage.Checked)
                {
                    table.Columns.Add("Page", typeof(string));
                    _headerCaptions += "|Page";
                    _ColumnWidth += "|3";
                }

            }


            return table;


        }

        private DataTable PopulateDataTable(DataTable sourceDT, DataTable exportDT)
        {
            // Use View because user may have split section(s), so the sections won't be in the correct order in the DataTable
            DataView view;
            view = sourceDT.DefaultView;
            view.Sort = "SortOrder ASC";

            bool foundPageCol = sourceDT.Columns.Contains("Page"); // Added 8.15.2018

            bool includeRow = false;
            foreach (DataRowView rowSource in view) // Loop thru view rows. 
            {
                string UID = rowSource["UID"].ToString();


                // ---- Check Filter Options ----
                if (rbOptionNone.Checked) // Include all rows
                    includeRow = true;
                if (rbOptionSecWOnlyKeywords.Checked) // Included Sections with only with Keywords
                {
                    if (rowSource["Keywords"].ToString().Trim() != string.Empty)
                        includeRow = true;
                    else
                        includeRow = false;
                }
                if (rbOptionSecWOnlyNotes.Checked) // Included Sections with only with Notes
                {
                        
                        string noteFile = string.Concat(_DocParsedSecNotes, @"\", UID, "_1.rtf");
                        if (File.Exists(noteFile))
                            includeRow = true;
                        else
                            includeRow = false;
                }
                if (rbOptionSecWOnlyKeywordsAndNotes.Checked) // Included Sections with only with both Keywords and Notes
                {
                    if (rowSource["Keywords"].ToString().Trim() != string.Empty)
                    {
                        //string UID = rowSource["UID"].ToString();
                        string noteFile = string.Concat(_DocParsedSecNotes, @"\", UID, "_1.rtf");
                        if (File.Exists(noteFile))
                            includeRow = true;
                        else
                            includeRow = false;
                    }
                    else
                        includeRow = false;
                }

                if (includeRow)
                {
                    DataRow rowExport = exportDT.NewRow();


                    rowExport["UID"] = UID; // Added 08.20.2014

                    if (_QualityCheck) // Added 05.18.2016
                    {
                        if (chkQualityCheck.Checked)
                        {
                            rowExport["Quality"] = rowSource["Quality"];
                        }
                    }

                    if (this.chkbLineNos.Checked) // Added 09.19.2013
                        rowExport["Lines"] = string.Concat(rowSource["LineStart"].ToString(), "\\" , rowSource["LineEnd"].ToString()) ;

                    if (chkbNumber.Checked)
                        rowExport["Number"] = rowSource["Number"];

                    if (chkbCaption.Checked)
                        rowExport["Caption"] = rowSource["Caption"];

                    if (chkbKeywords.Checked)
                        rowExport["Keywords"] = rowSource["Keywords"];

                    if (chkbNoCaption.Checked) // Added 10.14.2013
                        rowExport["NoCaption"] = string.Concat(rowSource["Number"], " ", rowSource["Caption"]);

                    if (rdXLSX.Checked && cboExcelTemplate.Text != NONE)
                    {
                        string html_text = getTextHTML(UID); // Content between "<body>" and "</body>" tags
                        rowExport["Text"] = html_text.Trim(); // Content between "<body>" and "</body>" tags
                    }
                    else if (chkbSectionText.Checked)
                    {
 
                        //if (this.rbHTML.Checked) // Added 08.12.2014
                        //{
                            string html_text = getTextHTML(UID); // Content between "<body>" and "</body>" tags
                            rowExport["Text"] = html_text.Trim(); 
                        //}
                        //else
                        //{
                        //    string rtfFile = rowSource["FileName"].ToString();
                        //    rtfExport.LoadFile(string.Concat(_DocParsedSec, @"\", rtfFile));
                        //    rowExport["Text"] = rtfExport.Text;
                        //}
                        
                    }

                    if (chkbNotes.Checked)
                    {
                      //  string UID = rowSource["UID"].ToString();
                        string noteFile = string.Concat(_DocParsedSecNotes, @"\", UID, "_1.rtf");
                        if (File.Exists(noteFile))
                        {
                            rtfExport.LoadFile(noteFile);
                            rowExport["Notes"] = rtfExport.Text.Trim();
                        }
                    }

                    // Added 8.15.2018
                    if (_PageExists && foundPageCol)
                    {
                        if (chkbPage.Checked)
                        {
                            rowExport["Page"] = rowSource["Page"];
                        }
                    }

                    exportDT.Rows.Add(rowExport);
                }
                   
            }

            return exportDT;
        }


        private string getTextHTML(string UID)
        {
            string returnHTML = string.Empty;

            returnHTML = Files.ReadFile(string.Concat(_DocParsedSecHTML, @"\", UID, ".html"));

            if (returnHTML.Length > 0) // Added 10.26.2014 
            {
                int StartBody = returnHTML.IndexOf("<body>") + 6;
                int EndBody = returnHTML.IndexOf("</body>") - StartBody;

                returnHTML = returnHTML.Substring(StartBody, EndBody);
            }

            return returnHTML;

        }



        #endregion

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }


        private int itemsSelectedToExport()
        {
            int returnValue = 0;

            if (chkbNumber.Checked)
                returnValue++;
        
            if (chkbCaption.Checked)
                returnValue++;

            if (chkbNoCaption.Checked) // Added 07.21.2014 -- Version 1.7.12.3
                returnValue++;

            if (chkbSectionText.Checked)
                returnValue++;

            if (chkbKeywords.Checked)
                returnValue++;

            if (chkbNotes.Checked)
                returnValue++;

            return returnValue;
        }

        private bool FindDuplicatesNumberCaption() // Added 10.13.2013
        {
            // Use View because user may have split section(s), so the sections won't be in the correct order in the DataTable
            DataView view;
            view = _dt.DefaultView;
            view.Sort = "SortOrder ASC";

            List <string> lstNoCaption = new List<string>();

            foreach (DataRowView rowSource in view) // Loop thru view rows. 
            {
                string numberCaption = string.Concat(rowSource["Number"].ToString().Trim(), " ", rowSource["Caption"].ToString().Trim());
                lstNoCaption.Add(numberCaption);
            }

            List<string> lstDupsNoCaption = Atebion.Common.DataFunctions.FindDuplicates(lstNoCaption);

            if (lstDupsNoCaption.Count > 0)
            {
                string messageNotice = "Unable to export for SharePoint; Duplicates found for Number and Caption as follows:" + Environment.NewLine + Environment.NewLine;
                foreach (string numberCaption in lstDupsNoCaption)
                {
                    messageNotice = string.Concat(messageNotice, numberCaption, Environment.NewLine);
                }

                lblMessage.Text = messageNotice;

                if (lstDupsNoCaption.Count > 7)
                {
                    txtMessage.ScrollBars = ScrollBars.Vertical;
                }
                return true;
            }
            

            return false;
        }

        private bool CovertSegments2HTML()
        {
            if (!Directory.Exists(_DocParsedSecHTML))
            {
                Directory.CreateDirectory(_DocParsedSecHTML);
                if (!Directory.Exists(_DocParsedSecHTML))
                {
                    return false;
                }
            }

            Files.DeleteAllFileInDir(_DocParsedSecHTML); // Added 01.23.2020

            // Convert Parsed RTF files into HTML files 
            AtebionRTFf2HTMLf.Convert convert = new AtebionRTFf2HTMLf.Convert();

            int qtyConverted = convert.ConvertFiles(_DocParsedSec, _DocParsedSecHTML);

            if (convert.ErrorMessage != string.Empty)
            {
                this.lblMessage.Text = convert.ErrorMessage;
                return false;
            }

            return false;
        }

        private bool ExportSegments()
        {
            _exportFileName = txtExportFileName.Text.Trim();

            _headerCaptions = string.Empty;

            _ColumnWidth = string.Empty;


            if (_exportFileName == string.Empty)
            {
                MessageBox.Show("An Export File Name is required before analysis results can be exported.", "Export Name Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (itemsSelectedToExport() == 0)
            {
                string noSelectedItem = "You have Not selected any Result Items (e.g. Number, Text) to export.";
                MessageBox.Show(noSelectedItem, "No Items Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            Cursor.Current = Cursors.WaitCursor; // Waiting 



            switch (_ExportMode)
            {
                case Modes.Excel:

                    CovertSegments2HTML();

                    _exportPathFile = string.Concat(_DocParsedSecExport, @"\", _exportFileName, ".xlsx");

                    if (cboExcelTemplate.Text == NONE)
                    {
                        ExportToExcel();
                    }
                    else
                    {
                        Export2ExcelTemplate();
                    }

                    break;
                case Modes.Word:
                    // Convert Parsed RTF files into HTML files 
  
                    _exportPathFile = string.Concat(_DocParsedSecExport, @"\", _exportFileName, ".docx");
                    CovertSegments2HTML();
                    //if (!CovertSegments2HTML())
                    //{
                    //    break;
                    //}

                    ExportToWord();

                    break;
                case Modes.HTML:
                    _exportPathFile = string.Concat(_DocParsedSecExport, @"\", _exportFileName, ".html");
                    CovertSegments2HTML(); // Added 10.26.2014 -- Was missing and causing an exception to occur in1.7.13.6
                    // Convert Parsed RTF files into HTML files 
                    //if (!CovertSegments2HTML())
                    //{
                    //    break;
                    //}

                    ExportToHTML();

                    break;
                case Modes.SharePoint:
                    _exportPathFile = string.Concat(_DocParsedSecExport, @"\", _exportFileName, ".xlsx");
                    if (FindDuplicatesNumberCaption())
                    {
                        MessageBox.Show("Unable to export for SharePoint, Duplicates found for Number and Caption.", "Duplicates Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }

                    ExportToExcel();

                    break;
                case Modes.RFP365:
                    _exportPathFile = string.Concat(_DocParsedSecExport, @"\", _exportFileName, ".xlsx");
                    ExportToExcel();
                    break;

            }

            Cursor.Current = Cursors.Default; // Back to normal

            if (_exportPathFile == string.Empty) // Nothing was selected
            {
                string noSelectedItem = "You have Not selected a File format type (e.g. Excel, HTML).";
                MessageBox.Show(noSelectedItem, "No File Format Type Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return false;
            }
            else
            {
                lblMessage.Text = string.Concat("Exported File: ", _exportPathFile);
            }

            if (File.Exists(_exportPathFile))
            {
                if (_ExportMode == Modes.HTML) // 10.26.2014 Changed From: "Modes.Word" To: "Modes.HTML"
                {
                    if (chkbOpenWMSWord.Checked)
                        MSWordAutomation.OpenMSWord(_exportPathFile);
                    else
                        Process.Start(_exportPathFile);
                }
                else
                    Process.Start(_exportPathFile);

                return true;
            }
            else
            {
                string fileNotFound = "Error: The export file was not generated due to an error.";
                MessageBox.Show(fileNotFound, "Export File Not Generated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblMessage.Text = fileNotFound;
                return false;
            }
                
        }


        private void butExport_Click(object sender, EventArgs e)
        {
            ExportSegments();
        }

        private void frmExportSec_Load(object sender, EventArgs e)
        {
           // PopulateData();
        }


        private void DisableElementsChkboxes()
        {
            chkbLineNos.Enabled = false;
            chkbCaption.Enabled = false;
            chkbNumber.Enabled = false;
            chkbNoCaption.Enabled = false;
            chkbSectionText.Enabled = false;
            chkbKeywords.Enabled = false;
            chkbNotes.Enabled = false;
            chkbPage.Enabled = false;

            panTemplate.Visible = true;
        }

        private void ModeAdjustments()
        {
            EnableElementsChkboxes();

       //     lblMessage.Text = string.Empty;
            
            chkbOpenWMSWord.Visible = false;
            butDownloadTemplate.Visible = false;

            switch (_ExportMode)
            {
                case Modes.Excel:
                  //  lblMessage.Text = "Exports analysis results into an Excel file.";

                    SetExcelTemplateMessage();
                    if (cboExcelTemplate.Text != NONE)
                    {
                        DisableElementsChkboxes();
                    }

                    butDownloadTemplate.Visible = true;
                    break;
                case Modes.Word:
                    lblMessage.Text = "Exports analysis results into a MS Word file.";
                    EnableElementsChkboxes();

                    chkbNumber.Enabled = true;
                    chkbCaption.Enabled = true;
                    chkbNoCaption.Enabled = true;

                    chkb4SharePoint.Checked = false; 
                    chkb4SharePoint.Visible = false;
                    break;
                case Modes.HTML:
                    lblMessage.Text = "Exports analysis results into a HTML file.";
                    EnableElementsChkboxes();

                    chkbNumber.Enabled = true;
                    chkbCaption.Enabled = true;
                    chkbNoCaption.Enabled = true;

                    chkb4SharePoint.Checked = false;
                    chkb4SharePoint.Visible = false;

                    if (isMSWordInstallled)
                    {                    
                        chkbOpenWMSWord.Visible = true;
                    }
                    break;
                case Modes.SharePoint:
                    chkbNumber.Checked = false;
                    chkbCaption.Checked = false;
                    chkbNumber.Enabled = false;
                    chkbCaption.Enabled = false;

                    chkbNoCaption.Enabled = false;
                    chkbNoCaption.Checked = true;

                    this.lblMessage.Text = "In the Excel export file for SharePoint, the first column should be a unique value. As such, we are concatenating the Number and the Caption into the first column."
                        + Environment.NewLine + Environment.NewLine +
                        "In addition, the Professional Document Analyzer will check for Number/Caption duplicates. If duplicates are found, you will be given a list of duplicates after you click the Export button."
                        +Environment.NewLine + Environment.NewLine +
                        "If duplicates are found, you will need to modify the segments Number and/or Capture from the Analysis Results panel, by clicking on the Edit button.";

                    break;
                case Modes.RFP365:
                    lblMessage.Text = "Exports analysis results into an Excel file formated so you can upload the exported file to RFP365.";
                    EnableElementsChkboxes();

                    chkbLineNos.Checked = false;
                    this.chkbCaption.Checked = true;
                    chkbNumber.Checked = true;
                    chkbNoCaption.Checked = false;
                    chkbSectionText.Checked = true;
                    chkbKeywords.Checked = true;

                    chkbLineNos.Enabled = false;
                    chkbCaption.Enabled = false;
                    chkbNumber.Enabled = false;
                    chkbNoCaption.Enabled = false;
                    chkbSectionText.Enabled = false;
                    chkbKeywords.Enabled = false;
                    break;

            }
        }


        private void rdXLSX_Click(object sender, EventArgs e)
        {
            if (rdXLSX.Checked)
            {
                _ExportMode = Modes.Excel;

                ModeAdjustments();

                //chkbOpenWMSWord.Visible = false;
                //chkb4SharePoint.Visible = true;
            }
        }

        private void rbWordDOCX_Click(object sender, EventArgs e)
        {
            //if (rbWordDOCX.Checked)
            //{
            //    chkbOpenWMSWord.Visible = false;
            //    chkb4SharePoint.Visible = false;
            //}
        }

        private void rbRTF_Click(object sender, EventArgs e)
        {
            //if (isMSWordInstallled)
            //{
            //    if (rbRTF.Checked)
            //        chkbOpenWMSWord.Visible = true;
            //}

            //if (rbRTF.Checked)
            //    chkb4SharePoint.Visible = false;
        }

        private void rbHTML_Click(object sender, EventArgs e)
        {
            
                _ExportMode = Modes.HTML;
        
        }

        private void txtMessage_TextChanged(object sender, EventArgs e)
        {
            txtMessage.Text = lblMessage.Text;
        }

        private void lblMessage_TextChanged(object sender, EventArgs e)
        {
            txtMessage.Text = lblMessage.Text;
        }

        private void chkbNoCaption_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbNoCaption.Checked)
            {
                chkbNumber.Checked = false;
                chkbCaption.Checked = false;
            }
        }

        private void chkb4SharePoint_CheckedChanged(object sender, EventArgs e)
        {
            txtMessage.ScrollBars = ScrollBars.None;

            if (chkb4SharePoint.Checked == true)
            {
                chkbNumber.Checked = false;
                chkbCaption.Checked = false;
                chkbNumber.Enabled = false;
                chkbCaption.Enabled = false;

                chkbNoCaption.Enabled = false;
                chkbNoCaption.Checked = true;

                this.lblMessage.Text = "In the Excel export file for SharePoint, the first column should be a unique value. As such, we are concatenating the Number and the Caption into the first column."
                    + Environment.NewLine + Environment.NewLine +
                    "In addition, the Professional Document Analyzer will check for Number/Caption duplicates. If duplicates are found, you will be given a list of duplicates after you click the Export button."
                    +Environment.NewLine + Environment.NewLine +
                    "If duplicates are found, you will need to modify the segments Number and/or Capture from the Analysis Results panel, by clicking on the Edit button.";
            }
            else
            {
                chkbNumber.Enabled = true;
                chkbCaption.Enabled = true;
                chkbNoCaption.Enabled = true;

                this.lblMessage.Text = string.Empty;
            }
        }

        private void chkbNumber_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbNumber.Checked)
            {
                chkbNoCaption.Checked = false;
            }
        }

        private void chkbCaption_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbCaption.Checked)
            {
                chkbNoCaption.Checked = false;
            }

        }

        private void rbWordDOCX_CheckedChanged(object sender, EventArgs e)
        {
            if (rbWordDOCX.Checked) // Added 10.19.2013
            {
                _ExportMode = Modes.Word;

                ModeAdjustments();

 
                
            }
        }


        private void rbHTML_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHTML.Checked)
            {
                _ExportMode = Modes.HTML;

                ModeAdjustments();

                EnableElementsChkboxes();

            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblLinkRFP365_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void EnableElementsChkboxes()
        {
            chkbLineNos.Enabled = true;
            chkbCaption.Enabled = true;
            chkbNumber.Enabled = true;
            chkbNoCaption.Enabled = true;
            chkbSectionText.Enabled = true;
            chkbKeywords.Enabled = true;
            chkbNotes.Enabled = true;
            chkbPage.Enabled = true;

            panTemplate.Visible = false;
        }

        private void rbRFP365_Click(object sender, EventArgs e)
        {

            if (rbRFP365.Checked == true)
            {
                _ExportMode = Modes.RFP365;

                ModeAdjustments();

                EnableElementsChkboxes();
            }
        }

        private void rbSharePoint_Click(object sender, EventArgs e)
        {
            if (rbSharePoint.Checked == true)
            {
                _ExportMode = Modes.SharePoint;

                ModeAdjustments();

                EnableElementsChkboxes();
            }
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void rdXLSX_CheckedChanged(object sender, EventArgs e)
        {
            if (_ExcelTemplatesQty > 0)
            {
                if (rdXLSX.Checked)
                {
                    lblExcelTemplate.Visible = true;
                    cboExcelTemplate.Visible = true;
                    picExcelTemplate.Visible = true;

                }
                else
                {
                    lblExcelTemplate.Visible = false;
                    cboExcelTemplate.Visible = false;
                    picExcelTemplate.Visible = false;
 
                }
            }
        }

        private void SetExcelTemplateMessage()
        {
            string selectedTemplate = cboExcelTemplate.Text;


            if (selectedTemplate == NONE)
            {
                DisableElementsChkboxes();
                

                string msg = string.Concat(
                    "Exporting Analysis Results without a selected Excel Template (e.g. ", NONE, " ) will generate an Excel file.",
                    Environment.NewLine, Environment.NewLine,
                    "Export Elements are defined in the Template Settings. You can Create, Download, and Edit Excel Export Templates from the Tools/Settings area. Click the Tools button at the bottom left and then select the Settings under the Tools area.",
                    Environment.NewLine, Environment.NewLine,             
                    "You can Create, Download, and Edit Excel Export Templates from the Tools/Settings area. Click the Tools button at the bottom left and then select the Settings under the Tools area.");
        
                lblMessage.Text = msg;

                txtMessage.Update();
                EnableElementsChkboxes();
            }
            else
            {
                
                DisableElementsChkboxes();

                lblMessage.Text = string.Empty;

                string TemplateName = cboExcelTemplate.Text;

                if (TemplateName != string.Empty)
                {
                    string fileName_xml = string.Concat(TemplateName, ".xml");
                    string pathFile = Path.Combine(AppFolders.AppDataPathToolsExcelTempAR, fileName_xml);

                    if (File.Exists(pathFile))
                    {
                        GenericDataManger gDataMgr = new GenericDataManger();
                        DataSet ds = gDataMgr.LoadDatasetFromXml(pathFile);
                        lblMessage.Text = string.Concat(ds.Tables[0].Rows[0][ExcelTemplateFields.Description].ToString(),
                            Environment.NewLine, Environment.NewLine, "Created by: ", ds.Tables[0].Rows[0][ExcelTemplateFields.CreatedBy].ToString(),
                            Environment.NewLine, "Created: ", ds.Tables[0].Rows[0][ExcelTemplateFields.CreatedDate].ToString(),
                            Environment.NewLine, Environment.NewLine, "Modified: ", ds.Tables[0].Rows[0][ExcelTemplateFields.ModifiedDate].ToString(),
                            Environment.NewLine, "Modified by: ", ds.Tables[0].Rows[0][ExcelTemplateFields.ModifiedBy].ToString());

                        //StringBuilder sb = new StringBuilder();

                        if (ds.Tables[0].Rows[0][ExcelTemplateFields.LocLineNo].ToString().Length > 0)
                        {
                            //sb.AppendLine(chkbLineNos.Text);
                            //sb.AppendLine(Environment.NewLine);
                            this.chkbLineNos.Checked = true;
                        }
                        else
                        {
                            this.chkbLineNos.Checked = false;
                        }
                        if (ds.Tables[0].Rows[0][ExcelTemplateFields.LocNumber].ToString().Length > 0)
                        {
                            //sb.AppendLine(chkbNumber.Text);
                            //sb.AppendLine(Environment.NewLine);
                            this.chkbNumber.Checked = true;
                        }
                        else
                        {
                            this.chkbNumber.Checked = false;
                        }
                        if (ds.Tables[0].Rows[0][ExcelTemplateFields.LocCaption].ToString().Length > 0)
                        {
                            //sb.AppendLine(chkbCaption.Text);
                            //sb.AppendLine(Environment.NewLine);
                            this.chkbCaption.Checked = true;
                        }
                        else
                        {
                            this.chkbCaption.Checked = false;
                        }
                        
                        if (ds.Tables[0].Rows[0][ExcelTemplateFields.LocNoCaption].ToString().Length > 0)
                        {
                            _NoCaption_Use = true;
                            //sb.AppendLine(chkbNoCaption.Text);
                            //sb.AppendLine(Environment.NewLine);
                            this.chkbNoCaption.Checked = true;
                        }
                        else
                        {
                            this.chkbNoCaption.Checked = false;
                            _NoCaption_Use = false;
                        }
                        if (ds.Tables[0].Rows[0][ExcelTemplateFields.LocSegText].ToString().Length > 0)
                        {
                            //sb.AppendLine(chkbSectionText.Text);
                            //sb.AppendLine(Environment.NewLine);
                            this.chkbSectionText.Checked = true;
                        }
                        else
                        {
                            this.chkbSectionText.Checked = false;
                        }
                        if (ds.Tables[0].Columns.Contains(ExcelTemplateFields.LocPage)) // Added 9.20.2018
                        {
                            if (ds.Tables[0].Rows[0][ExcelTemplateFields.LocPage].ToString().Length > 0) // Added 8.17.2018
                            {
                                //sb.AppendLine(chkbSectionText.Text);
                                //sb.AppendLine(Environment.NewLine);
                                this.chkbPage.Checked = true;
                            }
                            else
                            {
                                this.chkbPage.Checked = false;
                            }
                        }
                        else
                        {
                            this.chkbPage.Checked = false;
                        }
                        if (ds.Tables[0].Rows[0][ExcelTemplateFields.LocNotes].ToString().Length > 0)
                        {
                            //sb.AppendLine(chkbNotes.Text);
                            //sb.AppendLine(Environment.NewLine);
                            this.chkbNotes.Checked = true;
                        }
                        else if (ds.Tables[0].Rows[0][ExcelTemplateFields.NotesEmbedded].ToString() != NONE)
                        {
                            //sb.AppendLine(string.Concat(chkbNotes.Text, " are embedded"));
                            //sb.AppendLine(Environment.NewLine);

                            this.chkbNotes.Checked = true;
                        }
                        else
                        {
                            this.chkbNotes.Checked = false;
                        }

                    }

                }
            }
        }

        private void cboExcelTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetExcelTemplateMessage();
        }

   

        private void cboExcelTemplate_VisibleChanged(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
        }

        private void butDownloadTemplate_Click(object sender, EventArgs e)
        {
            if (!Directories.IsInternetAvailable())
            {
                MessageBox.Show("Internet Connection could not be found.", "Unable to open Download", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            frmDownLoad frm = new frmDownLoad();
            frm.LoadData("AR", AppFolders.AppDataPathToolsExcelTempAR, ContentTypes.Excel_Templates);

            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PopulateData();
            }
        }

 

 


    }
}
