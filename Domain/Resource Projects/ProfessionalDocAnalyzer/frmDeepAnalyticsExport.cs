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
using Atebion.DeepAnalytics;
using Atebion.Excel.Output;
using Atebion.Common;

namespace ProfessionalDocAnalyzer
{
    public partial class frmDeepAnalyticsExport : MetroFramework.Forms.MetroForm
    {
        public frmDeepAnalyticsExport()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        public frmDeepAnalyticsExport(DataTable dt, int FoundQty, int TotalQty, ucDeepAnalyticsFilterDisplay.Modes FilterMode, bool PageExists)
        {
            StackTrace st = new StackTrace(false);

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
            _TotalQty = TotalQty;
            _FilterMode = FilterMode;
            _PageExists = PageExists;

            //_SearchCriteria = SearchCriteria;
            LoadData();
        }



        #region Private Var.s

        private DataTable _dt;

        private RichTextBox _rtfCrtl;

        //  string _DocType = string.Empty;
        private string _ProposalPath = string.Empty;
        private string _UID = string.Empty;
        private string _exportPathFile = string.Empty;

        private Analysis _DeepAnalysis = new Analysis();

        // Search Results
        private int _FoundQty = 0;
        private int _TotalQty = 0;
        private ucDeepAnalyticsFilterDisplay.Modes _FilterMode;
        private bool _PageExists = false;
        //   private string _SearchCriteria = string.Empty;

        private bool isMSWordInstallled = false;

        private string _exportFileName = string.Empty;

        private string _headerCaptions = string.Empty;
        private string _ColumnWidth = string.Empty;

        private DataSet _ds;

        private Modes _ExportMode = Modes.Excel;

        private enum Modes
        {
            Excel = 0,
            Word = 1,
            HTML = 2,
            SharePoint = 3,
            RFP365 = 4
        }

        private const string NONE = "-- None --"; // Added for Excel templates
        private int _ExcelTemplatesQty = 0;  // Added for Excel templates

        private int itemsSelectedToExport()
        {
            int returnValue = 0;

            if (chkbNumber.Checked)
                returnValue++;

            if (chkbCaption.Checked)
                returnValue++;

            if (chkbNoCaption.Checked)
                returnValue++;

            if (chkbSectionText.Checked)
                returnValue++;

            if (chkbKeywords.Checked)
                returnValue++;

            if (chkbNotes.Checked)
                returnValue++;

            return returnValue;
        }

        private bool FindDuplicatesNumberCaption()
        {
            // Use View because user may have split section(s), so the sections won't be in the correct order in the DataTable
            DataView view;
            view = _dt.DefaultView;
            view.Sort = "SortOrder ASC";

            List<string> lstNoCaption = new List<string>();

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

        // ---> Removed, conversion during analysis process
        //private bool CovertSegments2HTML()
        //{
        //    // Convert Parsed RTF files into HTML files 
        //    AtebionRTFf2HTMLf.Convert convert = new AtebionRTFf2HTMLf.Convert();

        //    int qtyConverted = convert.ConvertFiles(AppFolders.DocParsedSec, AppFolders.DocParsedSecHTML);

        //    if (convert.ErrorMessage != string.Empty)
        //    {
        //        this.lblMessage.Text = convert.ErrorMessage;
        //        return false;
        //    }

        //    return false;
        //}

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
                    _exportPathFile = string.Concat(_DeepAnalysis.ExportPath, @"\", _exportFileName, ".xlsx");

                    if (cboExcelTemplate.Text == NONE)
                    {
                        ExportToExcel();
                    }
                    else // Use Template
                    {
                        Export2ExcelTemplate();
                    }

                    break;
                case Modes.Word:

                    _exportPathFile = string.Concat(_DeepAnalysis.ExportPath, @"\", _exportFileName, ".docx");

                    ExportToWord();

                    break;
                case Modes.HTML:
                    _exportPathFile = string.Concat(_DeepAnalysis.ExportPath, @"\", _exportFileName, ".html");

                    ExportToHTML();

                    break;
                case Modes.SharePoint:
                    _exportPathFile = string.Concat(_DeepAnalysis.ExportPath, @"\", _exportFileName, ".xlsx");
                    //if (FindDuplicatesNumberCaption())
                    //{
                    //    MessageBox.Show("Unable to export for SharePoint, Duplicates found for Number and Caption.", "Duplicates Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //    return false;
                    //}

                    ExportToExcel();

                    break;
                case Modes.RFP365:
                    _exportPathFile = string.Concat(_DeepAnalysis.ExportPath, @"\", _exportFileName, ".xlsx");
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
                if (_ExportMode == Modes.HTML)
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


        private void ModeAdjustments()
        {
            EnableElementsChkboxes();

            lblMessage.Text = string.Empty;

            chkbOpenWMSWord.Visible = false;

            switch (_ExportMode)
            {
                case Modes.Excel:
                    lblMessage.Text = "Exports analysis results into an Excel file.";
                    if (chkbSectionText.Checked)
                        chkbSecGrayBakgrd.Visible = true;

                    SetExcelTemplateMessage();
                    if (cboExcelTemplate.Text != NONE)
                    {
                        DisableElementsChkboxes();
                    }

                    picExcelTemplate.Visible = true;
                    cboExcelTemplate.Visible = true;
                    lblExcelTemplate.Visible = true;

                    break;
                case Modes.Word:
                    lblMessage.Text = "Exports analysis results into a MS Word file.";
                    EnableElementsChkboxes();

                    chkbNumber.Enabled = true;
                    chkbCaption.Enabled = true;
                    chkbNoCaption.Enabled = true;

                    chkb4SharePoint.Checked = false;
                    chkb4SharePoint.Visible = false;
                    chkbSecGrayBakgrd.Visible = false; // Show after MS Word shading issue has been fixed

                    picExcelTemplate.Visible = false;
                    cboExcelTemplate.Visible = false;
                    lblExcelTemplate.Visible = false;

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
                        + Environment.NewLine + Environment.NewLine +
                        "If duplicates are found, you will need to modify the segments Number and/or Capture from the Analysis Results panel, by clicking on the Edit button.";

                    break;
                case Modes.RFP365:
                    lblMessage.Text = "Exports analysis results into an Excel file formated so you can upload the exported file to RFP365.";
                    EnableElementsChkboxes();

                    chkbCaption.Checked = true;
                    chkbNumber.Checked = true;
                    chkbNoCaption.Checked = false;
                    chkbSectionText.Checked = true;
                    chkbKeywords.Checked = true;

                    break;

            }
        }


        private void rdXLSX_Click(object sender, EventArgs e)
        {
            if (rdXLSX.Checked)
            {
                _ExportMode = Modes.Excel;

                ModeAdjustments();
            }
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

                this.lblMessage.Text = "In the Excel export file for SharePoint, the first column should be a unique value. As such, we are using the UID for the first column.";
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

        private void rbRTF_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRTF.Checked)
            {
                //     chkbNoCaption.Checked = false;
                chkbNumber.Enabled = true;
                chkbCaption.Enabled = true;
                chkbNoCaption.Enabled = true;

                chkb4SharePoint.Checked = false;
                chkb4SharePoint.Visible = false;
            }
        }

        private void rbHTML_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHTML.Checked)
            {
                _ExportMode = Modes.HTML;

                ModeAdjustments();

            }
        }

        private void lblLinkRFP365_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void EnableElementsChkboxes()
        {

            chkbCaption.Enabled = true;
            chkbNumber.Enabled = true;
            chkbNoCaption.Enabled = true;
            chkbSectionText.Enabled = true;
            chkbKeywords.Enabled = true;
            chkbNotes.Enabled = true;
            chkbPage.Enabled = true;
            chkbSecGrayBakgrd.Enabled = true;

            panTemplate.Visible = false;
        }



        private void rbSharePoint_Click(object sender, EventArgs e)
        {
            if (rbSharePoint.Checked == true)
            {
                _ExportMode = Modes.SharePoint;

                ModeAdjustments();
            }
        }



        #endregion

        #region Private Functions
        private void LoadData()
        {
            //   _ProposalDetailMgr = new ProposalDetailMgr(_ProposalPath, _DocType);

            //_Dir_CurrentParsedSec = _ProposalDetailMgr.Dir_CurrentParsedSec;
            //_Dir_CurrentParsedSecNotes = _ProposalDetailMgr.Dir_CurrentParsedSecNotes;
            //_Dir_CurrentParsedSecXML = _ProposalDetailMgr.Dir_CurrentParsedSecXML;
            //_Dir_Export = _ProposalDetailMgr.Dir_Export;

            if (_PageExists)
            {
                chkbPage.Visible = true;
            }
            else
            {
                chkbPage.Visible = false;
            }

            _DeepAnalysis.CurrentDocPath = AppFolders.CurrentDocPath; // Set Paths

            ucDeepAnalyticsFilterDisplay1.Count = _FoundQty;
            ucDeepAnalyticsFilterDisplay1.Total = _TotalQty;
            ucDeepAnalyticsFilterDisplay1.CurrentMode = _FilterMode;
            ucDeepAnalyticsFilterDisplay1.UpdateStatusDisplay();

            if (_dt == null)
            {
                GenericDataManger gDataMgr = new GenericDataManger();

                string ParseResultsFile = string.Concat(_DeepAnalysis.XMLPath, @"\Sentences.xml");
                _ds = gDataMgr.LoadDatasetFromXml(ParseResultsFile);
                _dt = _ds.Tables[0];
            }


            isMSWordInstallled = MSWordAutomation.IsMSWordInstalled();

            this.txtExportFileName.Text = GetNewRptFileName();

            _ExcelTemplatesQty = LoadExcelTemplates();

            if (_ExcelTemplatesQty == 0)
            {
                // workgroup -- Copy templates from Local to current workgroup
                if (!AppFolders.IsLocal) // Not Local
                {
                    // Removed 10.26.2017 //string localTemplatePath = Path.Combine(AppFolders.AppDataPath_Local, "Tools", "ExcelTemp", "DAR");
                    // Removed 10.26.2017 //string[] files_xml = Directory.GetFiles(localTemplatePath);
                    string[] files_xml = Directory.GetFiles(AppFolders.AppDataPathToolsExcelTempDAR); // Added 10.26.2017
                    string fileName = string.Empty;
                    string workgroupTemplatePathFile = string.Empty;
                    if (files_xml.Length > 0)
                    {
                        foreach (string file_xml in files_xml)
                        {
                            fileName = Files.GetFileName(file_xml);
                            workgroupTemplatePathFile = Path.Combine(AppFolders.AppDataPathToolsExcelTempDAR, fileName);

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
            string[] files = Directory.GetFiles(AppFolders.AppDataPathToolsExcelTempDAR, "*.xlsx");

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

            _exportPathFile = string.Concat(_DeepAnalysis.ExportPath, @"\", _exportFileName, ".xlsx");

            if (File.Exists(_exportPathFile))
            {
                MessageBox.Show("Please enter another Export File Name.", "Export File Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblMessage.Text = string.Concat("Export File Already Exists", Environment.NewLine, Environment.NewLine, "Please enter another Export File Name.");

                return;
            }


            // Gather data
            DataTable dtEmpty;

            if (chkbNoCaption.Checked) // Added 10.14.2013
                dtEmpty = CreateNewDT_NoCaption(false); // Added 10.14.2013
            else
                dtEmpty = CreateNewDT(false);

            DataTable dtExport = PopulateDataTable2(_dt, dtEmpty, false);

            // Populate Metadata
            excelOutPut.Metadata_Date = string.Concat(DateTime.Now.ToLongDateString(), "  ", DateTime.Now.ToLongTimeString());
            excelOutPut.Metadata_DocName = AppFolders.DocName;
            excelOutPut.Metadata_ProjectName = AppFolders.ProjectName;
            excelOutPut.Metadata_YourName = AppFolders.UserName;

            if (!excelOutPut.ExportDAResults2Template(dtExport, TemplateName, AppFolders.AppDataPathToolsExcelTempDAR, _DeepAnalysis.ExportPath, _exportFileName))
            {
                MessageBox.Show(excelOutPut.ErrorMessage, "Unable to Generate an Excel Export via a Template", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                lblMessage.Text = string.Concat(excelOutPut.ErrorMessage, Environment.NewLine, Environment.NewLine, "Unable to Generate an Excel Export via a Template");

                return;
            }

        }

        private string GetNewRptFileName()
        {
            string BASE_NAME = string.Concat(AppFolders.DocName, "_DA_");
            string filename = string.Empty;
            string pathFile = string.Empty;

            for (int i = 1; i < 32000; i++)
            {
                filename = string.Concat(BASE_NAME, i.ToString(), ".html");
                pathFile = Path.Combine(_DeepAnalysis.ExportPath, filename);
                if (!File.Exists(pathFile))
                {
                    filename = string.Concat(BASE_NAME, i.ToString(), ".docx");
                    pathFile = Path.Combine(_DeepAnalysis.ExportPath, filename);
                    if (!File.Exists(pathFile))
                    {
                        filename = string.Concat(BASE_NAME, i.ToString(), ".xlsx");
                        pathFile = Path.Combine(_DeepAnalysis.ExportPath, filename);
                        if (!File.Exists(pathFile))
                        {
                            return string.Concat(BASE_NAME, i.ToString());
                        }
                    }
                }
            }

            return string.Concat(BASE_NAME, "5000"); ;
        }


        private void ExportToExcel()
        {


            //    ExportToExcel excel = new ExportToExcel();

            DataTable dtEmpty;

            if (chkbNoCaption.Checked) // Added 10.14.2013
                dtEmpty = CreateNewDT_NoCaption(false); // Added 10.14.2013
            else
                dtEmpty = CreateNewDT(false);


            DataTable dtExport = PopulateDataTable2(_dt, dtEmpty, false);

            _exportPathFile = string.Concat(_DeepAnalysis.ExportPath, @"\", _exportFileName, ".xlsx");

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

            //    Atebion.Export.Excel.ExportToExcel excport2Excel = new Atebion.Export.Excel.ExportToExcel();

            Atebion.Excel.Output.ExcelOutput export2Excel = new ExcelOutput();


            export2Excel.ExportDAResults(dtExport, _headerCaptions, _exportPathFile, _exportPathFile, _DeepAnalysis.HTMLPath, chkbSecGrayBakgrd.Checked, chkbUID.Checked);

            //            excport2Excel.CreateXLS3(dtExport, _headerCaptions, _exportPathFile, _exportPathFile, _DeepAnalysis.HTMLPath, chkbSecGrayBakgrd.Checked, chkbUID.Checked);

        }

        private void ExportToWord()
        {
            WordOXML word = new WordOXML();


            _headerCaptions = string.Empty; // Added 10.19.2013

            DataTable dtEmpty;

            if (chkbNoCaption.Checked) // Added 10.19.2013
                dtEmpty = CreateNewDT_NoCaption(true); // Added 10.19.2013
            else
                dtEmpty = CreateNewDT(true);

            DataTable dtExport = PopulateDataTable2(_dt, dtEmpty, true);

            _exportPathFile = string.Concat(_DeepAnalysis.ExportPath, @"\", _exportFileName, ".docx");

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

            //   word.writeDocx3(dtExport, _headerCaptions, _exportPathFile, AppFolders.DocName, AppFolders.ProjectName, _DeepAnalysis.HTMLPath, AppFolders.DocParsedSecHTML, chkbSecGrayBakgrd.Checked); // Added enbedded HTML parsed segements
            word.writeDocx3(dtExport, _headerCaptions, _exportPathFile, AppFolders.DocName, AppFolders.ProjectName, _DeepAnalysis.HTMLPath, AppFolders.DocParsedSecHTML, false); // Added enbedded HTML parsed segements

        }

        private void ExportToHTML()
        {

            HTMLGen htmlGen = new HTMLGen();

            DataTable dtEmpty;

            if (chkbNoCaption.Checked) // Added 10.19.2013
                dtEmpty = CreateNewDT_NoCaption(true); // Added 10.19.2013
            else
                dtEmpty = CreateNewDT(true);

            DataTable dtExport = PopulateDataTable2(_dt, dtEmpty, true);

            _exportPathFile = string.Concat(_DeepAnalysis.ExportPath, @"\", _exportFileName, ".html");

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

            htmlGen.WriteHTML2(dtExport, _headerCaptions, _ColumnWidth, _exportPathFile, AppFolders.DocName, AppFolders.ProjectName, "Project", "Document"); // Changed 08.12.2013

        }

        private DataTable CreateNewDT_NoCaption(bool useUID4Key)
        {
            DataTable table = new DataTable();

            if (this.chkbUID.Checked)
            {
                table.Columns.Add("UID", typeof(string));
                _headerCaptions = "UID";
                _ColumnWidth = "2";
            }

            bool forSharePoint = false;
            if (chkb4SharePoint.Checked)
            {
                forSharePoint = true;
            }

            //if (!forSharePoint)
            //{
            if (this.chkbUID.Checked)
            {
                table.Columns.Add("Lines", typeof(string));
                _headerCaptions = "Lines";
                _ColumnWidth = "1";
            }
            //}

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

            if (chkbSectionText.Checked || chkbSentence.Checked)
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

            if (forSharePoint)
            {
                if (chkbUID.Checked)
                {
                    table.Columns.Add("Lines", typeof(string));
                    _headerCaptions += "|Lines";
                    _ColumnWidth += "|1";
                }
            }


            if (useUID4Key)
            {
                table.Columns.Add("xUID", typeof(string)); // Example: Sentence = 5_2, Segment = 5
            }
            else
            {
                table.Columns.Add("isSegment", typeof(int)); // 0=Sentence, 1=Segment
            }


            return table;
        }

        private DataTable CreateNewDT(bool useUID4Key)
        {
            DataTable table = new DataTable();

            if (this.chkbUID.Checked)
            {
                table.Columns.Add("UID", typeof(string));
                _headerCaptions = "UID";
                _ColumnWidth = "2";
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

            if (chkbSectionText.Checked || chkbSentence.Checked)
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


            if (useUID4Key)
            {
                table.Columns.Add("xUID", typeof(string)); // Example: Sentence = 5_2, Segment = 5
            }
            else
            {
                table.Columns.Add("isSegment", typeof(int)); // 0=Sentence, 1=Segment
            }



            return table;


        }

        private DataTable PopulateDataTable(DataTable sourceDT, DataTable exportDT, bool useUID4Key)
        {
            // Use View because user may have split section(s), so the sections won't be in the correct order in the DataTable
            DataView view;
            view = sourceDT.DefaultView;
            view.Sort = "SortOrder ASC";

            bool foundPageCol = sourceDT.Columns.Contains("Page"); // Added 8.17.2018

            bool newSegment = false;
            string segUID = string.Empty; // Segment UID
            string UID = string.Empty; // Sentence UID 

            bool includeRow = false;
            int loopAmount = 3; // 1st loop insert segment, 2nd loop inser1 sentence //Added 4.29.2015
            foreach (DataRowView rowSource in view) // Loop thru view rows. 
            {
                UID = rowSource["UID"].ToString();

                if (UID.IndexOf('_') > -1)
                {
                    segUID = string.Empty;
                }
                else
                {
                    segUID = UID;
                    UID = string.Empty;
                }

                // string[] partsUID = UID.Split('_');

                // // Check if this is a new segment
                // //if (segUID != partsUID[0])
                // if (chkbSectionText.Checked)
                // {
                //     segUID = partsUID[0];
                //     newSegment = true;
                // }
                // else
                // {
                //     newSegment = false;
                // }

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
                    string noteFile = string.Empty;

                    if (segUID.Length > 0)
                    {
                        noteFile = string.Concat(AppFolders.DocParsedSecNotes, @"\", segUID, ".rtf");
                    }
                    else
                    {
                        noteFile = string.Concat(_DeepAnalysis.NotesPath, @"\", UID, ".rtf");
                    }

                    if (File.Exists(noteFile))
                        includeRow = true;
                    else
                        includeRow = false;
                }
                if (rbOptionSecWOnlyKeywordsAndNotes.Checked) // Included Sections with only with both Keywords and Notes
                {
                    if (rowSource["Keywords"].ToString().Trim() != string.Empty)
                    {
                        string noteFile = string.Empty;

                        if (segUID.Length > 0)
                        {
                            noteFile = string.Concat(AppFolders.DocParsedSecNotes, @"\", segUID, ".rtf");
                        }
                        else
                        {
                            noteFile = string.Concat(_DeepAnalysis.NotesPath, @"\", UID, ".rtf");
                        }

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
                    // int loopAmount = 1; // default
                    bool insertSegment = false; // default
                    //// Determine if Segment should be entered
                    if (chkbSectionText.Checked)
                    {
                        //if (newSegment)
                        //{
                        //   loopAmount = 2; // 1st loop insert segment, 2nd loop inser sentence
                        insertSegment = true;
                        //}
                        //else
                        //{
                        //    loopAmount = 1;
                        //}
                    }
                    //else
                    //{
                    //    loopAmount = 1;
                    //}




                    bool insertSentence = false;
                    if (chkbSentence.Checked)
                    {
                        insertSentence = true;
                    }

                    for (int i = 1; i < loopAmount; i++)
                    {

                        DataRow rowExport = exportDT.NewRow();

                        // Set Segment/Sentence flags for shaded rows
                        if (useUID4Key)
                        {
                            if (segUID.Length > 0) // Segment
                            {
                                rowExport["xUID"] = segUID;
                            }
                            else // Sentence
                            {
                                rowExport["xUID"] = UID;
                            }
                        }
                        else
                        {
                            if (segUID.Length > 0) // Segment
                            {
                                rowExport["isSegment"] = 1;
                            }
                            else // Sentence
                            {
                                rowExport["isSegment"] = 0;
                            }
                        }

                        //

                        if (this.chkbUID.Checked)
                        {
                            if (insertSegment && i == 1) // If this is a new segment then we will insert the 
                            {
                                rowExport["UID"] = segUID;
                            }
                            else
                            {
                                rowExport["UID"] = UID;
                            }
                        }

                        if (chkbNumber.Checked)
                            rowExport["Number"] = rowSource["Number"];

                        if (chkbCaption.Checked)
                            rowExport["Caption"] = rowSource["Caption"];

                        if (chkbKeywords.Checked)
                            rowExport["Keywords"] = rowSource["Keywords"];

                        if (chkbNoCaption.Checked) // Added 10.14.2013
                            rowExport["NoCaption"] = string.Concat(rowSource["Number"], " ", rowSource["Caption"]);

                        // Added 8.17.2018
                        if (_PageExists && foundPageCol)
                        {
                            if (chkbPage.Checked)
                            {
                                rowExport["Page"] = rowSource["Page"];
                            }
                        }


                        // >> Segment
                        //if (insertSegment && i == 1) // If this is a new segment then we will insert the 
                        if (insertSegment && segUID.Length > 0)
                        {
                            // segUID = rowSource["UID"].ToString();


                            //if (rdXLSX.Checked || rbSharePoint.Checked)
                            //    {
                            //         string rtfFile = string.Concat(AppFolders.DocParsedSec, @"\", segUID, ".rtf");
                            //        rtfExport.LoadFile(rtfFile);
                            //        rowExport["Text"] = rtfExport.Text;
                            //    }
                            //    else
                            //    {
                            rowExport["Text"] = getSegTextHTML(segUID).Trim(); // Content between "<body>" and "</body>" tags
                            //  }
                        }
                        // >> Sentence
                        //      else if (insertSentence && i == 2)
                        else if (insertSentence && UID.Length > 0)
                        {

                            //if (rdXLSX.Checked || rbSharePoint.Checked)
                            //{
                            //    string rtfFile = string.Concat(_DeepAnalysis.ParseSentencesPath, @"\", UID, ".rtf");
                            //    rtfExport.LoadFile(rtfFile);
                            //    rowExport["Text"] = rtfExport.Text;
                            //}
                            //else
                            //{
                            rowExport["Text"] = getSegTextHTML(UID); // Content between "<body>" and "</body>" tags
                            //}
                        }


                        if (chkbNotes.Checked)
                        {
                            string noteFile = string.Empty;

                            //  if (insertSegment && i == 1) // >> Segment
                            if (insertSegment && segUID.Length > 0) // >> Segment 
                            {
                                noteFile = string.Concat(AppFolders.DocParsedSecNotes, @"\", segUID, ".rtf");
                            }
                            else
                            {
                                noteFile = string.Concat(_DeepAnalysis.NotesPath, @"\", UID, ".rtf");
                            }


                            if (File.Exists(noteFile))
                            {
                                rtfExport.LoadFile(noteFile);
                                rowExport["Notes"] = rtfExport.Text;
                            }
                        }

                        exportDT.Rows.Add(rowExport);

                        // -> Added 4.29.2015
                        //if (i == 1 && newSegment)
                        //    exportDT.Rows.Add(rowExport);
                        //else if (i == 2 && insertSentence)
                        //    exportDT.Rows.Add(rowExport);

                        //if (i == 1 && newSegment)
                        //    exportDT.Rows.Add(rowExport);
                        //else if (i == 2 && insertSentence)
                        //    exportDT.Rows.Add(rowExport);


                        //if (loopAmount == 2)
                        //    loopAmount = 1;
                        //else
                        //    loopAmount++;

                        //<-

                    }
                }

            }

            return exportDT;
        }

        private struct exportData
        {
            public string UID;
            public bool isSegment;
            public string Number;
            public string Caption;
            public string NumberCaption;
            public string Keywords;
            public string Text;
            public string NoteFile;
            public string Page;
        }

        private exportData _exportData;

        private void GetRowData(DataRowView rowSource, bool isSegment)
        {
            if (isSegment)
            {
                string number = string.Empty;
                string[] partsUID = rowSource["UID"].ToString().Split('_');
                _exportData.UID = partsUID[0];
                _exportData.NoteFile = GetText(string.Concat(AppFolders.DocParsedSecNotes, @"\", _exportData.UID, ".rtf"), "rtf");
                _exportData.Keywords = GetSegKeywords(_exportData.UID, out number);
                _exportData.Number = number;

                // Added 8.17.2018
                if (_PageExists)
                {
                    _exportData.Page = rowSource["Page"].ToString();
                }

                // Get Segment Text
                //if (rdXLSX.Checked || rbSharePoint.Checked) // Plain Text
                //{
                //    string rtfFile = string.Concat(AppFolders.DocParsedSec, @"\", _exportData.UID, ".rtf");
                //    rtfExport.LoadFile(rtfFile);
                //    _exportData.Text = rtfExport.Text;
                //}
                //else // HTML Text
                //{
                _exportData.Text = getSegTextHTML(_exportData.UID).Trim(); // Content between "<body>" and "</body>" tags
                //}
            }
            else
            {
                _exportData.UID = rowSource["UID"].ToString();
                _exportData.NoteFile = GetText(string.Concat(_DeepAnalysis.NotesPath, @"\", _exportData.UID, ".rtf"), "rtf");
                _exportData.Keywords = rowSource["Keywords"].ToString();

                // Added 8.17.2018
                if (_PageExists)
                {
                    _exportData.Page = rowSource["Page"].ToString();
                }


                //if (rdXLSX.Checked || rbSharePoint.Checked)
                //{
                //    string rtfFile = string.Concat(_DeepAnalysis.ParseSentencesPath, @"\", _exportData.UID, ".rtf");
                //    rtfExport.LoadFile(rtfFile);
                //    _exportData.Text = rtfExport.Text;
                //}
                //else
                //{
                _exportData.Text = getSentanceTextHTML(_exportData.UID).Trim(); // Content between "<body>" and "</body>" tags
                //}
            }

            _exportData.isSegment = isSegment;
            if (isSegment)
            {
                _exportData.NumberCaption = string.Concat(_exportData.Number, " ", rowSource["Caption"].ToString());
            }
            else
            {
                _exportData.Number = rowSource["Number"].ToString();
                _exportData.NumberCaption = string.Concat(rowSource["Number"].ToString(), " ", rowSource["Caption"].ToString());
            }
            _exportData.Caption = rowSource["Caption"].ToString();


        }

        private DataSet _segDS;
        private DataTable _segDT;
        private string GetSegKeywords(string UID, out string Number)
        {
            string keywords = string.Empty;

            if (_segDT == null)
            {
                GenericDataManger gDataMgr = new GenericDataManger();
                string ParseResultsFile = string.Concat(AppFolders.DocParsedSecXML, @"\ParseResults.xml");
                _segDS = gDataMgr.LoadDatasetFromXml(ParseResultsFile);
                _segDT = _segDS.Tables[0];
            }

            foreach (DataRow row in _segDT.Rows)
            {
                if (row["UID"].ToString() == UID)
                {
                    keywords = row["Keywords"].ToString();
                    Number = row["Number"].ToString();
                    return keywords;
                }
            }

            Number = string.Empty;
            return keywords;
        }

        private DataTable LoadRow(DataTable exportDT, bool useUID4Key)
        {
            // Check Filters
            if (rbOptionSecWOnlyKeywords.Checked) // Only with Keywords
            {
                if (_exportData.Keywords.Trim() == string.Empty)
                    return exportDT;
            }

            if (rbOptionSecWOnlyNotes.Checked) // Only with Notes
            {
                if (_exportData.NoteFile == string.Empty)
                    return exportDT;
            }

            if (rbOptionSecWOnlyKeywordsAndNotes.Checked) // Only with both Keywords and Notes
            {
                if (_exportData.Keywords.Trim() == string.Empty)
                    return exportDT;

                if (!File.Exists(_exportData.NoteFile))
                    return exportDT;
            }

            // Load Data
            DataRow rowExport = exportDT.NewRow();

            if (chkbUID.Checked)
                rowExport["UID"] = _exportData.UID;

            if (chkbNumber.Checked)
                rowExport["Number"] = _exportData.Number;

            if (chkbCaption.Checked)
                rowExport["Caption"] = _exportData.Caption;

            if (chkbKeywords.Checked)
                rowExport["Keywords"] = _exportData.Keywords;

            if (chkbNoCaption.Checked)
                rowExport["NoCaption"] = string.Concat(_exportData.Number, " ", _exportData.Caption);

            if (chkbSectionText.Checked || chkbSentence.Checked)
                rowExport["Text"] = _exportData.Text;

            if (chkbNotes.Checked)
                rowExport["Notes"] = _exportData.NoteFile;

            // added 8.17.2018
            if (chkbPage.Checked && _exportData.Page != null)
                rowExport["Page"] = _exportData.Page;

            // Set Segment/Sentence flags for shaded rows
            if (useUID4Key)
            {
                rowExport["xUID"] = _exportData.UID;
            }
            else
            {
                if (_exportData.isSegment) // Segment
                {
                    rowExport["isSegment"] = 1;
                }
                else // Sentence
                {
                    rowExport["isSegment"] = 0;
                }
            }


            exportDT.Rows.Add(rowExport);

            return exportDT;

        }

        private DataTable PopulateDataTable2(DataTable sourceDT, DataTable exportDT, bool useUID4Key)
        {
            // Use View because user may have split section(s), so the sections won't be in the correct order in the DataTable
            DataView view;
            view = sourceDT.DefaultView;
            view.Sort = "SortOrder ASC";

            string segPreviousUID = string.Empty;
            string segUID = string.Empty; // Segment UID
            string UID = string.Empty; // Sentence UID 
            string tempUID = string.Empty;


            foreach (DataRowView rowSource in view) // Loop thru view rows. 
            {
                tempUID = rowSource["UID"].ToString();

                string[] partsUID = tempUID.Split('_');
                segUID = partsUID[0];

                if (chkbSectionText.Checked)
                {
                    if (segUID != segPreviousUID) // If New Segment UID, then get Segment Keywords
                    {
                        GetRowData(rowSource, true);
                        exportDT = LoadRow(exportDT, useUID4Key);
                        segPreviousUID = segUID;

                        if (chkbSentence.Checked) // Get Sentence assocated with the Segment
                        {
                            GetRowData(rowSource, false);
                            exportDT = LoadRow(exportDT, useUID4Key);
                        }
                    }
                    else
                    {
                        if (chkbSentence.Checked)
                        {
                            GetRowData(rowSource, false);
                            exportDT = LoadRow(exportDT, useUID4Key);
                        }
                    }
                }
                else
                {
                    if (chkbSentence.Checked)
                    {
                        GetRowData(rowSource, false);
                        exportDT = LoadRow(exportDT, useUID4Key);
                    }
                }

            }

            return exportDT;
        }

        private string GetText(string desFile, string ext)
        {
            if (_rtfCrtl == null)
                _rtfCrtl = new RichTextBox();

            if (!File.Exists(desFile))
                return string.Empty;

            if (ext.ToLower() == "rtf")
                _rtfCrtl.LoadFile(desFile);
            else
                _rtfCrtl.LoadFile(desFile, RichTextBoxStreamType.PlainText);

            string txt = _rtfCrtl.Text;

            _rtfCrtl.Clear();

            return txt;

        }

        private string getSegTextHTML(string UID)
        {
            string returnHTML = string.Empty;

            //  returnHTML = Files.ReadFile(string.Concat(_DeepAnalysis.HTMLPath, @"\", UID, ".html")); // 03.11.2017
            returnHTML = Files.ReadFile(string.Concat(AppFolders.DocParsedSecHTML, @"\", UID, ".html"));

            if (returnHTML.Length > 0) // Added 10.26.2014 
            {
                int StartBody = returnHTML.IndexOf("<body>") + 6;
                int EndBody = returnHTML.IndexOf("</body>") - StartBody;

                returnHTML = returnHTML.Substring(StartBody, EndBody);
            }

            return returnHTML;

        }

        private string getSentanceTextHTML(string UID)
        {
            string returnHTML = string.Empty;

            returnHTML = Files.ReadFile(string.Concat(_DeepAnalysis.HTMLPath, @"\", UID, ".html"));

            if (returnHTML.Length > 0) // Added 10.26.2014 
            {
                int StartBody = returnHTML.IndexOf("<body>") + 6;
                int EndBody = returnHTML.IndexOf("</body>") - StartBody;

                returnHTML = returnHTML.Substring(StartBody, EndBody);
            }

            return returnHTML;

        }

        #endregion

        private void chkbSectionText_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbSectionText.Checked)
            {
                if (rdXLSX.Checked)
                    chkbSecGrayBakgrd.Visible = true;
                else
                    chkbSecGrayBakgrd.Visible = false; // Show after MS Word shading issue has been fixed
            }
            else
                chkbSecGrayBakgrd.Visible = false;
        }

        private void chkbNoCaption_Click(object sender, EventArgs e)
        {
            if (chkbNoCaption.Checked)
            {
                chkbNumber.Checked = false;
                chkbCaption.Checked = false;
            }
        }

        private void chkbCaption_Click(object sender, EventArgs e)
        {
            if (chkbCaption.Checked)
            {
                chkbNoCaption.Checked = false;
            }
        }

        private void chkbNumber_Click(object sender, EventArgs e)
        {
            if (chkbNumber.Checked)
            {
                chkbNoCaption.Checked = false;
            }
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void cboExcelTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetExcelTemplateMessage();
        }

        private void DisableElementsChkboxes()
        {

            chkbCaption.Enabled = false;
            chkbNumber.Enabled = false;
            chkbNoCaption.Enabled = false;
            chkbSectionText.Enabled = false;
            chkbKeywords.Enabled = false;
            chkbNotes.Enabled = false;
            chkbPage.Enabled = false;

            chkbSectionText.Enabled = false;
            chkbSecGrayBakgrd.Enabled = false;

            panTemplate.Visible = true;
        }

        private void SetExcelTemplateMessage()
        {
            string selectedTemplate = cboExcelTemplate.Text;


            if (selectedTemplate == NONE)
            {
                DisableElementsChkboxes();


                string msg = string.Concat(
                    "Exporting Deep Analysis Results without a selected Excel Template (e.g. ", NONE, " ) will generate an Excel file.",
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
                    string pathFile = Path.Combine(AppFolders.AppDataPathToolsExcelTempDAR, fileName_xml);

                    if (File.Exists(pathFile))
                    {
                        GenericDataManger gDataMgr = new GenericDataManger();
                        DataSet ds = gDataMgr.LoadDatasetFromXml(pathFile);
                        lblMessage.Text = string.Concat(ds.Tables[0].Rows[0][ExcelTemplateFields.Description].ToString(),
                            Environment.NewLine, Environment.NewLine, "Created by: ", ds.Tables[0].Rows[0][ExcelTemplateFields.CreatedBy].ToString(),
                            Environment.NewLine, "Created: ", ds.Tables[0].Rows[0][ExcelTemplateFields.CreatedDate].ToString(),
                            Environment.NewLine, Environment.NewLine, "Modified: ", ds.Tables[0].Rows[0][ExcelTemplateFields.ModifiedDate].ToString(),
                            Environment.NewLine, "Modified by: ", ds.Tables[0].Rows[0][ExcelTemplateFields.ModifiedBy].ToString());


                        if (ds.Tables[0].Rows[0][ExcelTemplateFields.LocNumber].ToString().Length > 0)
                        {
                            this.chkbNumber.Checked = true;
                        }
                        else
                        {
                            this.chkbNumber.Checked = false;
                        }
                        if (ds.Tables[0].Rows[0][ExcelTemplateFields.LocCaption].ToString().Length > 0)
                        {
                            this.chkbCaption.Checked = true;
                        }
                        else
                        {
                            this.chkbCaption.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0][ExcelTemplateFields.LocNoCaption].ToString().Length > 0)
                        {
                            this.chkbNoCaption.Checked = true;
                        }
                        else
                        {
                            this.chkbNoCaption.Checked = false;
                        }
                        if (ds.Tables[0].Rows[0][ExcelTemplateFields.LocSegText].ToString().Length > 0)
                        {
                            this.chkbSectionText.Checked = true;
                        }
                        else
                        {
                            this.chkbSectionText.Checked = false;
                        }

                        if (ds.Tables[0].Columns.Contains(ExcelTemplateFields.LocPage)) // Added 8.20.2018
                        {
                            // Added 8.17.2018
                            if (ds.Tables[0].Rows[0][ExcelTemplateFields.LocPage].ToString().Length > 0)
                            {
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
                            this.chkbNotes.Checked = true;
                        }
                        else if (ds.Tables[0].Rows[0][ExcelTemplateFields.NotesEmbedded].ToString() != NONE)
                        {
                            this.chkbNotes.Checked = true;
                        }
                        else
                        {
                            this.chkbNotes.Checked = false;
                        }

                        if (ds.Tables[0].Rows[0][ExcelTemplateFields.LocSentText].ToString().Length > 0)
                        {
                            chkbSentence.Checked = true;
                        }
                        else
                        {
                            chkbSentence.Checked = false;
                        }

                        if ((bool)ds.Tables[0].Rows[0][ExcelTemplateFields.SegTextUseBkColor] == true)
                        {
                            chkbSecGrayBakgrd.Checked = true;
                        }
                        else
                        {
                            chkbSecGrayBakgrd.Checked = false;
                        }
                    }

                }
            }
        }

        private void lblMessage_TextChanged_1(object sender, EventArgs e)
        {
            txtMessage.Text = lblMessage.Text;
        }

        private void txtMessage_TextChanged_1(object sender, EventArgs e)
        {
            txtMessage.Text = lblMessage.Text;
        }

        private void butDownloadTemplate_Click(object sender, EventArgs e)
        {
            if (!Directories.IsInternetAvailable())
            {
                MessageBox.Show("Internet Connection could not be found.", "Unable to open Download", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            frmDownLoad frm = new frmDownLoad();
            frm.LoadData("DAR", AppFolders.AppDataPathToolsExcelTempDAR, ContentTypes.Excel_Templates);

            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LoadExcelTemplates();
            }
        }

 
    }
}
