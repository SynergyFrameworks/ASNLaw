using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using OfficeOpenXml;
using System.Xml;
using System.Reflection;
using OfficeOpenXml.Style;
using Atebion.Common;

using unvell.ReoGrid;
using unvell.ReoGrid.Events;


namespace ProfessionalDocAnalyzer
{
    public partial class frmExcelTemp : MetroFramework.Forms.MetroForm
    {
        public frmExcelTemp()
        {
            StackTrace st = new StackTrace(false);
            InitializeComponent();
        }

        private TemplateType _TemplateType;
        public enum TemplateType
        {
            AR_Keywords = 0,
            DR_Keywords = 1,
            AR_Dic = 3,
            DR_Dic = 4,
            AR_Concepts = 6,
            DR_Concepts = 7,
            AR_RAM = 9
        }

        public Modes _Mode = Modes.New;

        public enum Modes
        {
            New = 0,
            Edit = 1
        }

        private Panels _currentPanel;
        public enum Panels
        {
            Template = 0,
            Header = 1,
            Data_Mapping = 2,
            RAM_Mapping = 3,
            Validation = 4
        }

        private bool _TemplateUpdated = false; // Is set to true when _Mode = Edit & User Updates the Template

        private string _SelectedFile = string.Empty;
        private string _SheetName = string.Empty;
        private string _PathToolsExcelTemp = string.Empty; // Top Lever Folder for the Excel Templates & Config. files 
 //       private string _PathToolsExcelTempTemp = string.Empty; // A temp folder for copying and testing.

        private string _PathToolsExcelTempConfig = string.Empty; // Folder where the Analysis Results Excel Templates & Config. files are stored

        private string _PathToolsExcelTempConfigBackup = string.Empty; // Backup Folder where the Analysis Results Excel Templates & Config. files are stored
     //   private string _PathToolsExcelTempDARBackup = string.Empty; // Folder where the Deep Analysis Results Excel Templates & Config. files are stored


        //    private string _TempPathFile = string.Empty; // copied Excel file template helded in the Excel Template/Temp folder
        //     private string _OrgTempPathFile = string.Empty; // Original selected Excel Template
        private string _TestPathFileX_xlsx = string.Empty; // copied Excel file template used for Testing the configurations helded in the Excel Template/Temp folder
        private string _TemplateName = string.Empty;
        //    private string _TemplateDSFile = string.Empty;


        private string _OrgPathFile_xml = string.Empty;
        private string _OrgPathFile_xlsx = string.Empty;

        private string _PathFile_xml = string.Empty;
        private string _PathFile_xlsx = string.Empty;

        private string[] _columns = new string[] { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        private DataSet _ds;
        private DataSet _ds_Validation;

        private bool _PageDTColExists = false;


        // ---- RAM ---------
        private string[] _RAM_Excel_Columns = null;

        private string[] _RAM_DicCats = null;
        private int[] _RAM_DicCats_UID = null;
        private string[] _RAM_DicCats_Validation = null;
        private int[] _RAM_DicCats_UID_Validation = null;

        private string[] _RAM_ModelRoles_Name = null;
        private int[] _RAM_ModelRoles_UID = null;
        private string[] _RAM_ModelRoles_Notation = null;
        private string[] _RAM_ModelRoles_Color = null;
        private string[] _RAM_ModelRoles_Decription = null;

        private const string _MESSAGE_PRIMARY = "Mapping responsibilities uses dictionary categories mapped to an Excel column (e.g. 'D') with an associated roles model.";

        private DataSet _dsDictionary;
        private DataSet _dsDictionaryValidation;
        private DataSet _dsRAM_Model;

        // ----- End RAM -----

        public bool LoadData(TemplateType templateType)
        {
            _TemplateType = templateType;

            _Mode = Modes.New;

            switch (templateType)
            {
                case TemplateType.AR_Concepts:
                    lblSubcaption.Text = "Analysis Results w/ Concepts - New";
                    break;

                case TemplateType.AR_Dic:
                    lblSubcaption.Text = "Analysis Results w/ Dictionary Terms - New";
                    break;

                case TemplateType.AR_Keywords:
                    lblSubcaption.Text = "Analysis Results w/ Keywords - New";
                    break;

                case TemplateType.AR_RAM:
                    lblSubcaption.Text = "Responsibility Assignment Matrix (RAM) - New";
                    break;

                case TemplateType.DR_Keywords:
                    lblSubcaption.Text = "Analysis Results w/ Keywords - New";

                    break;

        }


            

            return LoadData(_TemplateType, string.Empty);
        }

        public bool LoadData(TemplateType templateType, string TemplateName)
        {
            _TemplateType = templateType;
            _TemplateName = TemplateName;

            switch (templateType)
            {
                case TemplateType.AR_Concepts:
                    _PathToolsExcelTempConfig = AppFolders.AppDataPathToolsExcelTempConceptsDoc;
                    _PathToolsExcelTempConfigBackup = AppFolders.AppDataPathToolsExcelTempConceptsDocBackup;
                    break;

                case TemplateType.AR_Dic:
                    _PathToolsExcelTempConfig  = AppFolders.AppDataPathToolsExcelTempDicDoc;
                    _PathToolsExcelTempConfigBackup = AppFolders.AppDataPathToolsExcelTempDicDocBackup;
                    break;

                case TemplateType.AR_Keywords:
                    _PathToolsExcelTempConfig = AppFolders.AppDataPathToolsExcelTempAR;
                    _PathToolsExcelTempConfigBackup = AppFolders.AppDataPathToolsExcelTempARBackup;
                    break;

                case TemplateType.AR_RAM:
                    _PathToolsExcelTempConfig = AppFolders.AppDataPathToolsExcelTempDicRAM;
                    _PathToolsExcelTempConfigBackup = AppFolders.AppDataPathToolsExcelTempDicRAMBackup;
                    break;

                case TemplateType.DR_Keywords:
                    _PathToolsExcelTempConfig = AppFolders.AppDataPathToolsExcelTempDAR;
                    _PathToolsExcelTempConfigBackup = AppFolders.AppDataPathToolsExcelTempDARBackup;

                    break;

            }

            LoadFonts();
            LoadFontSizes();
            LoadColNotations();
            LoadStartingDataRow();
            LoadColors();

            if (_TemplateType == TemplateType.AR_RAM)
            {
                LoadRAMDictionaries();
                LoadRAMModels();
            }

            if (_TemplateName.Length > 0)
            {
                _Mode = Modes.Edit;

                if (_TemplateType == TemplateType.AR_RAM)
                    ShowButtons(true, false);
                else
                    ShowButtons(false, false);

                if (_SelectedFile.Length == 0)
                {
                    string file_xlsx = string.Concat(TemplateName, ".xlsx");
                    _SelectedFile = Path.Combine(_PathToolsExcelTempConfig, file_xlsx);
                    _PathFile_xlsx = _SelectedFile;
                }

                reoGridControl1.Load(_SelectedFile);
                panLeftHeader.Visible = true;
                reoGridControl1.Visible = true;
                panExcelScale.Visible = true;

                butTempHeader.Visible = true;
                lblTempHeader.Visible = true;

                switch (templateType)
                {
                    case TemplateType.AR_Concepts:
                        lblSubcaption.Text = "Analysis Results w/ Concepts - Edit";
                        break;

                    case TemplateType.AR_Dic:
                        lblSubcaption.Text = "Analysis Results w/ Dictionary Terms - Edit";
                        break;

                    case TemplateType.AR_Keywords:
                        lblSubcaption.Text = "Analysis Results w/ Keywords - Edit";
                        break;

                    case TemplateType.AR_RAM:
                        lblSubcaption.Text = "Responsibility Assignment Matrix (RAM) - Edit";
                        break;

                    case TemplateType.DR_Keywords:
                        lblSubcaption.Text = "Analysis Results w/ Keywords - Edit";

                        break;

                }

            }





            //else
            //{
            //    _Mode = Modes.New;
            //}

            string file_xml = string.Concat(_TemplateName, ".xml");
            _PathFile_xml = Path.Combine(_PathToolsExcelTempConfig, file_xml);

            LoadNotesEnbedded();

            Load_TemplateData();

            PopulateSheetNames(_SelectedFile);

            _currentPanel = Panels.Template;
            PanelAdjustment();

            return true;
        }

        private void HideButtons()
        {
            butTempHeader.Visible = false;
            lblTempHeader.Visible = false;

            butDataMapping.Visible = false;
            lblDataMapping.Visible = false;

            butRAMMapping.Visible = false;
            lblRAMMapping.Visible = false;

            butSummary.Visible = false;
            lblSummary.Visible = false;

        }

        private void ShowButtons(bool show_RAM_Mapping, bool show_Validation)
        {
            butTempHeader.Visible = true;
            lblTempHeader.Visible = true;

            butDataMapping.Visible = true;
            lblDataMapping.Visible = true;

            if (show_RAM_Mapping)
            {
                butRAMMapping.Visible = true;
                lblRAMMapping.Visible = true;
            }

            if (show_Validation)
            {
                butSummary.Visible = true;
                lblSummary.Visible = true;
            }
        }

        private void HidePanels()
        {
            panDataMapping.Visible = false;
            panMatrix.Visible = false;
            panSummary.Visible = false;
            panPanelHeader.Visible = false;
            panRAMMapping.Visible = false;

            butTemplate.Highlight = false;
            butTempHeader.Highlight = false;
            butDataMapping.Highlight = false;
            butRAMMapping.Highlight = false;
        }

        private void PanelAdjustment()
        {
            Cursor.Current = Cursors.WaitCursor;

            HidePanels();

            switch (_currentPanel)
            {
                case Panels.Template:
                    lblNotice.Text = "Select a Template file and enter configuration parameters.";
                    butTemplate.Highlight = true;
                    panMatrix.Visible = true;
                    panMatrix.Dock = DockStyle.Fill;
                    break;

                case Panels.Header:
                    lblNotice.Text = "Enter Header parameters.";
                    butTempHeader.Highlight = true;
                    panPanelHeader.Visible = true;
                    panPanelHeader.Dock = DockStyle.Fill;
                    break;

                case Panels.Data_Mapping:
                    lblNotice.Text = "Map data elements to the Excel columns. Use Excel Preview for reference.";
                    butDataMapping.Highlight = true;
                    panDataMapping.Visible = true;
                    panDataMapping.Dock = DockStyle.Fill;
                    break;

                case Panels.RAM_Mapping:
                    lblNotice.Text = "Map responsibilities using dictionary categories to Excel columns (e.g. D) with associated roles.";
                    butRAMMapping.Highlight = true;
                    panRAMMapping.Visible = true;
                    panRAMMapping.Dock = DockStyle.Fill;
                    break;

                case Panels.Validation:
                    panSummary.Visible = true;
                    panSummary.Dock = DockStyle.Fill;
                    break;

            }

            this.Refresh();

            Cursor.Current = Cursors.Default;

        }

        private void ShowHide_per_TemplateType()
        {
            // Deep Analysis
            if (_TemplateType == TemplateType.DR_Concepts || _TemplateType == TemplateType.DR_Dic || _TemplateType == TemplateType.DR_Keywords)
            {
                // Exclude 
                
                chkbDAExcludeCaption.Visible = true;
                chkbDAExcludeNoCaption.Visible = true;
                // ToDo  chkbDAExcludeNumber.Visible = true;

            }
            else
            {
                // Exclude 
                chkbDAExcludeCaption.Visible = false;
                chkbDAExcludeNoCaption.Visible = false;
                // ToDo  chkbDAExcludeNumber.Visible = false;

            }

            if (_TemplateType == TemplateType.AR_Dic || _TemplateType == TemplateType.DR_Dic)
            {
                lblLocKeywords.Text = "Dictionary Terms";
                Point xy = new Point(64, 353);
                lblLocKeywords.Location = xy;

                lblLocDicDef.Visible = true;
                cboLocDicDef.Visible = true;

                lblDicWeights.Visible = true;
                cboDicWeights.Visible = true;

            }
            else if (_TemplateType == TemplateType.AR_Keywords || _TemplateType == TemplateType.DR_Keywords)
            {
                lblLocKeywords.Text = "Keywords";
                Point xy = new Point(99, 353);
                lblLocKeywords.Location = xy;

                lblLocDicDef.Visible = false;
                cboLocDicDef.Visible = false;

                lblDicWeights.Visible = false;
                cboDicWeights.Visible = false;


            }
            else if (_TemplateType == TemplateType.AR_Concepts || _TemplateType == TemplateType.DR_Concepts)
            {
                lblLocKeywords.Text = "Concepts";
                Point xy = new Point(99, 353);
                lblLocKeywords.Location = xy;

                lblLocDicDef.Visible = false;
                cboLocDicDef.Visible = false;

                lblLocLineNo.Visible = false;
                cboLocLineNo.Visible = false;

                lblDicWeights.Visible = false;
                cboDicWeights.Visible = false;


            }
            else if (_TemplateType == TemplateType.AR_RAM)
            {
                lblLocKeywords.Visible = false;
                cboLocKeywords.Visible = false;

                lblLocDicDef.Visible = false;
                cboLocDicDef.Visible = false;

                lblLocLineNo.Visible = false;
                cboLocLineNo.Visible = false;

                lblDicWeights.Visible = false;
                cboDicWeights.Visible = false;

                chkWholeNoColor.Visible = false;
                cmbboxClr.Visible = false;

                lblLocNotes.Visible = false;
                cboLocNotes.Visible = false;

                lblNotes_or.Visible = false;

                lblNotes_Embedded.Visible = false;
                cboNotesEmbedded.Visible = false;

            }

            if (_TemplateType == TemplateType.DR_Dic || _TemplateType == TemplateType.DR_Concepts || _TemplateType == TemplateType.DR_Keywords)
            {
                lblLocSentText.Visible = true;
                cboLocSent.Visible = true;

                lblLocLineNo.Visible = false;
                cboLocLineNo.Visible = false;
            }
            else
            {
                lblLocSentText.Visible = false;
                cboLocSent.Visible = false;

                lblLocLineNo.Visible = true;
                cboLocLineNo.Visible = true;
            }




        }

        private void LoadNotesEnbedded()
        {
            if (_TemplateType == TemplateType.AR_Keywords)
            {
                cboNotesEmbedded.Items.Clear();
                cboNotesEmbedded.Items.Add("--None--");
                cboNotesEmbedded.Items.Add("Line No.");
                cboNotesEmbedded.Items.Add("Number");
                cboNotesEmbedded.Items.Add("Caption");
                cboNotesEmbedded.Items.Add("No. + Caption");
                cboNotesEmbedded.Items.Add("Segment/Paragraph Text");
            }
            else if (_TemplateType == TemplateType.DR_Keywords)
            {
                cboNotesEmbedded.Items.Clear();
                cboNotesEmbedded.Items.Add("--None--");
                cboNotesEmbedded.Items.Add("Number");
                cboNotesEmbedded.Items.Add("Caption");
                cboNotesEmbedded.Items.Add("No. + Caption");
                cboNotesEmbedded.Items.Add("Segment/Paragraph & Sentence Text");
            }

            if (cboNotesEmbedded.Items.Count > 0) // Added 01.30.2020 fixed in Beta 1.7
                cboNotesEmbedded.SelectedIndex = 0;

        }

        private bool Load_TemplateData()
        {
            ShowHide_per_TemplateType();

            if (_Mode == Modes.New)
            {
                
                DataTable dt = createTable();

                if (_ds == null)
                    _ds = new DataSet();

                _ds.Tables.Add(dt);

                _PageDTColExists = _ds.Tables[0].Columns.Contains("Page");

                // ToDo adjust display

            }
            else // Edit
            {
                
                string fileName_xml = string.Concat(_TemplateName, ".xml");
                string fileName_xlsx = string.Concat(_TemplateName, ".xlsx");
               
                _OrgPathFile_xml = Path.Combine(_PathToolsExcelTempConfig, fileName_xml);
                _OrgPathFile_xlsx = Path.Combine(_PathToolsExcelTempConfig, fileName_xlsx);
                 

                if (!File.Exists(_OrgPathFile_xml))
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Unable to find the selected Excel Template data file.", "Data File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return false;
                }


                if (!File.Exists(_OrgPathFile_xlsx))
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Unable to find the selected Excel Template file.", "Excel Template File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return false;
                }

                // Test Area Not needed
             //   CopyOrgTemp2TestArea(fileName_xlsx, fileName_xml);

                SubLoadData();

                // ToDo
                //if (BackupsExists())
                //{
                //    butRestoreBackup.Visible = true;
                //}

                txtbTemplateName.Enabled = false;
               // butOpenTemplate.Visible = true;
            }

            Cursor.Current = Cursors.Default;
            return true;
        }

        private DataTable createTable()
        {
            DataTable table = new DataTable("ExcelTemplate");

            table.Columns.Add(ExcelTemplateFields.UID, typeof(string));
            table.Columns.Add(ExcelTemplateFields.OrgTemplateFile, typeof(string));
            table.Columns.Add(ExcelTemplateFields.TemplateName, typeof(string));
            table.Columns.Add(ExcelTemplateFields.SheetName, typeof(string));
            table.Columns.Add(ExcelTemplateFields.ExportTempFor, typeof(string));
            table.Columns.Add(ExcelTemplateFields.Description, typeof(string));
            table.Columns.Add(ExcelTemplateFields.LocProjectName, typeof(string));
            table.Columns.Add(ExcelTemplateFields.LocDocName, typeof(string));
            table.Columns.Add(ExcelTemplateFields.LocDate, typeof(string));
            table.Columns.Add(ExcelTemplateFields.LocYourName, typeof(string));

            table.Columns.Add(Atebion.ConceptAnalyzer.ExcelTemplateFields.DataRowStart, typeof(string)); //
            table.Columns.Add(ExcelTemplateFields.LocLineNo, typeof(string));
            table.Columns.Add(ExcelTemplateFields.LocNumber, typeof(string));

            // Deep Analysis
            if (_TemplateType == TemplateType.DR_Keywords || _TemplateType == TemplateType.DR_Dic || _TemplateType == TemplateType.DR_Concepts)
            {
                table.Columns.Add(ExcelTemplateFields.ExcludeDARNumber, typeof(bool));
                table.Columns.Add(ExcelTemplateFields.ExcludeDARCaption, typeof(bool));
                table.Columns.Add(ExcelTemplateFields.ExcludeDARNoCaption, typeof(bool));
            }

            table.Columns.Add(ExcelTemplateFields.LocCaption, typeof(string));
            table.Columns.Add(ExcelTemplateFields.LocNoCaption, typeof(string));
            table.Columns.Add(ExcelTemplateFields.LocSegText, typeof(string));
            table.Columns.Add(ExcelTemplateFields.SegTextUseBkColor, typeof(bool));
            table.Columns.Add(ExcelTemplateFields.SegTextBkColor, typeof(string));
            if (_TemplateType == TemplateType.AR_Keywords || _TemplateType == TemplateType.DR_Keywords)
                table.Columns.Add(ExcelTemplateFields.LocKeywords, typeof(string));
            else if (_TemplateType == TemplateType.AR_Dic || _TemplateType == TemplateType.DR_Dic)
            {
                table.Columns.Add(ExcelTemplateFields.LocDics, typeof(string));
                table.Columns.Add(ExcelTemplateFields.LocDicDefs, typeof(string));
                table.Columns.Add(ExcelTemplateFields.LocDicWeight, typeof(string));
            }
            else if (_TemplateType == TemplateType.AR_Concepts || _TemplateType == TemplateType.DR_Concepts)
            {
               table.Columns.Add(ExcelTemplateFields.LocConcepts, typeof(string));
            }


            table.Columns.Add(ExcelTemplateFields.LocNotes, typeof(string));
            table.Columns.Add(ExcelTemplateFields.LocPage, typeof(string)); //  Added 8.14.2018
            table.Columns.Add(ExcelTemplateFields.NotesEmbedded, typeof(string));
            table.Columns.Add(ExcelTemplateFields.LocSentText, typeof(string));
            table.Columns.Add(ExcelTemplateFields.FontName, typeof(string));
            table.Columns.Add(ExcelTemplateFields.FontSize, typeof(string));

            //AltColorRows
            table.Columns.Add(ExcelTemplateFields.AltColorRowsUse, typeof(bool));
            table.Columns.Add(ExcelTemplateFields.AltColorRows, typeof(string));

            // Whole Number Color rows
            table.Columns.Add(ExcelTemplateFields.WholeNoColorUse, typeof(bool));
            table.Columns.Add(ExcelTemplateFields.WholeNoColor, typeof(string));

            table.Columns.Add(ExcelTemplateFields.ColorBkgrdOddLinesUse, typeof(bool));
            table.Columns.Add(ExcelTemplateFields.ColorBkgrdOddLines, typeof(string));

            table.Columns.Add(ExcelTemplateFields.CreatedBy, typeof(string));
            table.Columns.Add(ExcelTemplateFields.CreatedDate, typeof(string));
            table.Columns.Add(ExcelTemplateFields.ModifiedDate, typeof(string));
            table.Columns.Add(ExcelTemplateFields.ModifiedBy, typeof(string));

            if (_TemplateType == TemplateType.AR_RAM)
            {
                table.Columns.Add(ExcelTemplateFields.RAM_ModelName, typeof(string));
                table.Columns.Add(ExcelTemplateFields.DictionaryName, typeof(string));
            }


            return table;

        }

        private DataTable CreateRAMTable()
        {
            DataTable table = new DataTable(Atebion.ConceptAnalyzer.ResponsibilityAssMatrixFields.TableName);
            table.Columns.Add(Atebion.ConceptAnalyzer.ResponsibilityAssMatrixFields.UID, typeof(int));
            table.Columns.Add(Atebion.ConceptAnalyzer.ResponsibilityAssMatrixFields.Role_Name, typeof(string)); //Example: Responsible
            table.Columns.Add(Atebion.ConceptAnalyzer.ResponsibilityAssMatrixFields.Role_Notation, typeof(string)); //Example: R
            table.Columns.Add(Atebion.ConceptAnalyzer.ResponsibilityAssMatrixFields.Role_Color, typeof(string)); //Example: Turquoise
            table.Columns.Add(Atebion.ConceptAnalyzer.ResponsibilityAssMatrixFields.Dictionary_Category_UID, typeof(int)); //Example: 1
            table.Columns.Add(Atebion.ConceptAnalyzer.ResponsibilityAssMatrixFields.Role_Column, typeof(string)); //Example: H

            return table;

        }

 

        //private void CopyOrgTemp2TestArea(string fileName_xlsx, string fileName_xml)
        //{
        //    try
        //    {
        //        // Copy the selected Excel Template Settings to the Test (folder) area. 
        //        _TestPathFile_xml = Path.Combine(_PathToolsExcelTempTemp, fileName_xml);
        //        File.Copy(_OrgPathFile_xml, _TestPathFile_xml);
        //    }
        //    catch
        //    {
        //        return;
        //    }

        //    try
        //    {
        //        // Copy the selected Excel Template to the Test (folder) area. 
        //        _TestPathFile_xlsx = Path.Combine(_PathToolsExcelTempTemp, fileName_xlsx);
        //        File.Copy(_OrgPathFile_xlsx, _TestPathFile_xlsx);
        //    }
        //    catch
        //    {
        //        return;
        //    }

        //}

        private void SubLoadData()
        {
            Cursor.Current = Cursors.WaitCursor; // Wait
            GenericDataManger gDMgr = new GenericDataManger();
            _ds = gDMgr.LoadDatasetFromXml(_PathFile_xml);
       
            PopulateFields();

            if (_TemplateType == TemplateType.AR_RAM)
            {
                 
                PopulateRAMFields();
            }

            Cursor.Current = Cursors.Default; // Done
            
        }


        private bool LoadRAMDictionaries()
        {
            txtbRAMInformation.ForeColor = Color.SkyBlue;

            string[] files = Directory.GetFiles(AppFolders.DictionariesPath, "*.dicx");

            if (files.Length == 0)
            {
                txtbRAMInformation.ForeColor = Color.Yellow;
                lblRAMInformation.Text = string.Concat("Unable to find Dictionaries in folder: ", AppFolders.DictionariesPath, Environment.NewLine, Environment.NewLine, "A Dictionary is required for RAM Templates.", Environment.NewLine, Environment.NewLine, "See RAM documentation for details.");

                return false;
            }

            cboRAMDictionary.Items.Clear();

            string dic = string.Empty;
            foreach (string dicFile in files)
            {
                dic = Files.GetFileNameWOExt(dicFile);
                cboRAMDictionary.Items.Add(dic);
            }

            return true;

        }

        private bool LoadRAMModels()
        {
            txtbRAMInformation.ForeColor = Color.SkyBlue;

            string[] files = Directory.GetFiles(AppFolders.AppDataPathToolsRAMDefs, "*.ram");

            if (files.Length == 0)
            {
                txtbRAMInformation.ForeColor = Color.Yellow;
                lblRAMInformation.Text = string.Concat("Unable to find RAM Models in folder: ", AppFolders.AppDataPathToolsRAMDefs, Environment.NewLine, Environment.NewLine, "A RAM Model is required for RAM Templates.", Environment.NewLine, Environment.NewLine, "See RAM documentation for details.");

                return false;
            }

            
            cboRAMModel.Items.Clear();

            string model = string.Empty;
            foreach (string modelFile in files)
            {
                model = Files.GetFileNameWOExt(modelFile);
                cboRAMModel.Items.Add(model);
            }

            return true;

        }

        private string GetExcelColumnsFromDataSet()
        {
            if (_ds.Tables["RAMDef"].Rows.Count == 0)
                return string.Empty;

            List<string> excelColumns = new List<string>();
            string excelColumn = string.Empty;

            foreach (DataRow row in _ds.Tables["RAMDef"].Rows)
            {
                excelColumn = row["LocRole"].ToString();
                if (!excelColumns.Exists(e => e.EndsWith(excelColumn)))
                {
                    excelColumns.Add(excelColumn);
                }
            }

            excelColumns.Sort();
            int counter = 0;
            string result = string.Empty;

            foreach (string col in excelColumns)
            {
                if (counter == 0)
                {
                    result = col;
                }
                else
                {
                    result = string.Concat(result, ", ", col);
                }

                counter++;
            }

            return result;


        }

        private void PopulateRAMFields()
        {
            txtbRAMInformation.Visible = true;

            txtbRAMInformation.ForeColor = Color.SkyBlue;

            if (_TemplateType != TemplateType.AR_RAM)
                return;

            if (!_ds.Tables.Contains("RAMDef"))
            {
                txtbRAMInformation.ForeColor = Color.Yellow;
                lblRAMInformation.Text = "RAM data not found in the Template Data file.";

                return;
            }

            txtRAMCols.Text = GetExcelColumnsFromDataSet();

            string model = _ds.Tables[0].Rows[0]["RAM_ModelName"].ToString();
            int modelIndex = cboRAMModel.FindStringExact(model);
            if (modelIndex != -1)
                cboRAMModel.SelectedIndex = modelIndex;

            string dictionary = _ds.Tables[0].Rows[0]["DictionaryName"].ToString();
            
            int dicIndex = cboRAMDictionary.FindStringExact(dictionary);
            

            if (dicIndex != -1)
                cboRAMDictionary.SelectedIndex = dicIndex;

            if (!Build_RAM_Config())
                return;

            string cat = string.Empty;
            string col = string.Empty;
            int colIndex = -1;
            int xCat = -1;
            string roleNotation = string.Empty;

            foreach (DataRow dataRow in _ds.Tables[1].Rows)
            {
                xCat = Convert.ToInt32(dataRow["DicCatUID"].ToString());
                cat = Get_RAM_Dictionary_Cat(xCat);

                roleNotation = dataRow["RoleNotation"].ToString();
                col  = dataRow["LocRole"].ToString();
                colIndex = Get_RAM_Cat_Index(col);

                if (colIndex != -1) // -1 = Not Found column
                {
                    colIndex = colIndex + 1; // Add 1 b/c of cat column

                    if (cat.Length > 0)
                    {
                        foreach (DataGridViewRow row in dgvRAMConfig.Rows)
                        {
                            //if (row.Cells[0].Value.ToString().Equals(cat))
                            if (row.Cells[0].Value.ToString() == cat)
                            {
                                row.Cells[colIndex].Value = roleNotation;
                                break;
                            }
                        }
                    }
                }
            }

            

            dgvRAMConfig.Visible = true;

        }

        private int Get_RAM_Cat_Index(string columnName)
        {
            int colCount = _RAM_Excel_Columns.Count();

            if (colCount == 0)
                return -1;

            for (int i = 0; i < colCount; i++)
            {
                if (columnName == _RAM_Excel_Columns[i].Trim())
                {
                    return i;
                }

            }

            return -1;
        }

        private int Get_RAM_Dictionary_Cat_UID(string DicCatName)
        {
            int result = -1;

            if (_RAM_DicCats_UID.Length == 0)
                return result;

            if (_RAM_DicCats.Length == 0)
                return result;

            int index = -1;
            int i = 0;
            foreach (string x in _RAM_DicCats)
            {
                if (x == DicCatName)
                {
                    index = i;
                    break;
                }

                i++;
            }
            if(index == -1)
            {
                return result;
            }
            else
            {
                result = _RAM_DicCats_UID[index];
            }

            return result;

        }

        private string Get_RAM_Dictionary_Cat(int DicCatUID)
        {
            string result = string.Empty;

            if (_RAM_DicCats_UID.Length == 0)
                return result;

            if (_RAM_DicCats.Length == 0)
                return result;

            int index = -1;
            int i = 0;
            foreach (int x in _RAM_DicCats_UID)
            {
                if (x == DicCatUID)
                {
                    index = i;
                    break;
                }

                i++;
            }
            if(index == -1)
            {
                return result;
            }
            else
            {
                result = _RAM_DicCats[index];
            }
            

            return result;

        }


        private void PopulateFields()
        {
            DataRow row = _ds.Tables[0].Rows[0];

            _PageDTColExists = _ds.Tables[0].Columns.Contains(ExcelTemplateFields.LocPage);

            if (row == null)
            {
                MessageBox.Show("Data not found in Excel Template data file.", "Excel Template Data File Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
            }

            txtbDescription.Text = row[ExcelTemplateFields.Description].ToString();
            cboLocCaption.SelectedIndex = cboLocCaption.FindString(row[ExcelTemplateFields.LocCaption].ToString());
            txtbLocDate.Text = row[ExcelTemplateFields.LocDate].ToString();
            txtbLocDocName.Text = row[ExcelTemplateFields.LocDocName].ToString();
            cboStartDataRow.SelectedIndex = cboStartDataRow.FindString(row[ExcelTemplateFields.DataRowStart].ToString());
            

            if (_TemplateType == TemplateType.AR_Keywords || _TemplateType == TemplateType.DR_Keywords)
            {
                cboLocKeywords.SelectedIndex = cboLocKeywords.FindString(row[ExcelTemplateFields.LocKeywords].ToString());
            }
            else if (_TemplateType == TemplateType.AR_Dic || _TemplateType == TemplateType.DR_Keywords)
            {
                cboLocKeywords.SelectedIndex = cboLocKeywords.FindString(row[ExcelTemplateFields.LocDics].ToString());
                cboLocDicDef.SelectedIndex = cboLocKeywords.FindString(row[ExcelTemplateFields.LocDicDefs].ToString());
                cboDicWeights.SelectedIndex = cboLocKeywords.FindString(row[ExcelTemplateFields.LocDicWeight].ToString());
            }
            else if (_TemplateType == TemplateType.AR_Concepts || _TemplateType == TemplateType.DR_Concepts)
            {
                cboLocKeywords.SelectedIndex = cboLocKeywords.FindString(row[ExcelTemplateFields.LocConcepts].ToString());
            }

            if (_ds.Tables[0].Columns.Contains(ExcelTemplateFields.LocLineNo))
            {
                cboLocLineNo.SelectedIndex = cboLocLineNo.FindString(row[ExcelTemplateFields.LocLineNo].ToString());
            }


            cboLocNoCaption.SelectedIndex = cboLocNoCaption.FindString(row[ExcelTemplateFields.LocNoCaption].ToString());
            cboLocNotes.SelectedIndex = cboLocNotes.FindString(row[ExcelTemplateFields.LocNotes].ToString());

            // Added 8.14.2018
            if (_PageDTColExists)
                cboLocPage.SelectedIndex = cboLocNotes.FindString(row[ExcelTemplateFields.LocPage].ToString());

            cboLocNumber.SelectedIndex = cboLocNumber.FindString(row[ExcelTemplateFields.LocNumber].ToString());
            txtbLocProjectName.Text = row[ExcelTemplateFields.LocProjectName].ToString();
            cboLocSegTxt.SelectedIndex = cboLocSegTxt.FindString(row[ExcelTemplateFields.LocSegText].ToString());

            if (_ds.Tables[0].Columns.Contains(ExcelTemplateFields.LocSentText))
            {
                cboLocSent.SelectedIndex = cboLocSent.FindString(row[ExcelTemplateFields.LocSentText].ToString());
            }

            txtbLocYourName.Text = row[ExcelTemplateFields.LocYourName].ToString();
            _SheetName = row[ExcelTemplateFields.SheetName].ToString();
            txtbTemplate.Text = row[ExcelTemplateFields.OrgTemplateFile].ToString();
            txtbTemplateName.Text = row[ExcelTemplateFields.TemplateName].ToString();

            // Font
            string FontName = row[ExcelTemplateFields.FontName].ToString();
            cboFonts.SelectedIndex = cboFonts.FindString(FontName);

            string FontSize = row[ExcelTemplateFields.FontSize].ToString();
            cboFontSizes.SelectedIndex = cboFontSizes.FindString(FontSize);


            string ExportTempFor = row[ExcelTemplateFields.ExportTempFor].ToString();
       // ToDo    cboExportTempFor.SelectedIndex = cboExportTempFor.FindString(ExportTempFor);

            string NotesEmbedded = row[ExcelTemplateFields.NotesEmbedded].ToString();
            cboNotesEmbedded.SelectedIndex = cboNotesEmbedded.FindString(NotesEmbedded);

            // Deep Analysis
                                    
            if (_TemplateType == TemplateType.DR_Concepts || _TemplateType == TemplateType.DR_Dic 
                || _TemplateType == TemplateType.DR_Keywords)
            {
                // Exclude 
                if (row[ExcelTemplateFields.ExcludeDARCaption] != null)
                {
                    bool DAExcludeCaption = (bool)row[ExcelTemplateFields.ExcludeDARCaption];
                    chkbDAExcludeCaption.Checked = DAExcludeCaption;
                }
                if (row[ExcelTemplateFields.ExcludeDARNoCaption] != null)
                {
                    bool DAExcludeNoCaption = (bool)row[ExcelTemplateFields.ExcludeDARNoCaption];
                    chkbDAExcludeNoCaption.Checked = DAExcludeNoCaption;
                }

                //if (row[ExcelTemplateFields.ExcludeDARNumber] != null)
                //{
                //    //bool DAExcludeNumber = (bool)row[ExcelTemplateFields.ExcludeDARNumber];
                //    //   ToDo chkbDAExcludeNumber.Checked = DAExcludeNumber;
                //}

            }

            //Segment Back Color
            if (_ds.Tables[0].Columns.Contains(ExcelTemplateFields.SegTextUseBkColor))
            {
                bool SegTextUseBkColor = (bool)row[ExcelTemplateFields.SegTextUseBkColor];
                chkbHighlightSegRow.Checked = SegTextUseBkColor;

                string SegBackColor = row[ExcelTemplateFields.SegTextBkColor].ToString();
                cmbboxClr.SelectedIndex = cmbboxClr.FindString(SegBackColor);
            }

            // Alternating Color Rows
            if (_ds.Tables[0].Columns.Contains(ExcelTemplateFields.AltColorRowsUse))
            {
                bool AlternatingColorRowsUse = (bool)row[ExcelTemplateFields.AltColorRowsUse];
                this.chkbAltColorRows.Checked = AlternatingColorRowsUse;
                string AlternatingColor = row[ExcelTemplateFields.AltColorRows].ToString();
                cboAltColorRows.SelectedIndex = cboAltColorRows.FindString(AlternatingColor);
            }

            // Whole Number Color Rows
            if (_ds.Tables[0].Columns.Contains(ExcelTemplateFields.WholeNoColorUse))
            {
                bool WholeNoColorRowsUse = (bool)row[ExcelTemplateFields.WholeNoColorUse];
                this.chkWholeNoColor.Checked = WholeNoColorRowsUse;
                string WholeNoColor = row[ExcelTemplateFields.WholeNoColor].ToString();
                cboWholeNoColor.SelectedIndex = cboWholeNoColor.FindString(WholeNoColor);
            }

        }

        private void LoadFonts()
        {
            List<string> fonts = new List<string>();

            foreach (FontFamily font in System.Drawing.FontFamily.Families)
            {
                fonts.Add(font.Name);
            }

            cboFonts.DataSource = fonts;
            cboFonts.SelectedIndex = cboFonts.FindString("Arial");

            // ToDo set default for New
        }

        private void LoadFontSizes()
        {
            cboFontSizes.Items.Clear();
            cboFontSizes.Items.Add("8");
            cboFontSizes.Items.Add("9");
            cboFontSizes.Items.Add("10");
            cboFontSizes.Items.Add("12");
            cboFontSizes.Items.Add("14");
            cboFontSizes.Items.Add("18");

            //cboSentFontSizes.Items.Clear();
            //cboSentFontSizes.Items.Add("8");
            //cboSentFontSizes.Items.Add("9");
            //cboSentFontSizes.Items.Add("10");
            //cboSentFontSizes.Items.Add("12");
            //cboSentFontSizes.Items.Add("14");
            //cboSentFontSizes.Items.Add("18");

            if (_Mode == Modes.New) // Select Defaults
            {
                cboFontSizes.SelectedIndex = 2; // Font size 10 pt
                //cboSentFontSizes.SelectedIndex = 2; // Font size 10 pt
            }

        }

        private void LoadColNotations()
        {
            cboLocCaption.DataSource = new string[] { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            cboLocKeywords.DataSource = new string[] { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            cboLocLineNo.DataSource = new string[] { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            cboLocNoCaption.DataSource = new string[] { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            cboLocNotes.DataSource = new string[] { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            cboLocNumber.DataSource = new string[] { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            cboLocSegTxt.DataSource = new string[] { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            cboLocSent.DataSource = new string[] { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            cboLocPage.DataSource = new string[] { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            cboDicWeights.DataSource = new string[] { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            cboLocDicDef.DataSource = new string[] { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            cboLocPage.DataSource = new string[] { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };


        }

        private void LoadStartingDataRow()
        {
            cboStartDataRow.Items.Clear();

            for (int i = 1; i < 51; i++)
            {
                cboStartDataRow.Items.Add(i.ToString());
            }

            cboStartDataRow.SelectedIndex = 3;
        }

        private void LoadColors()
        {
            // Load colors into drop-down list
            this.cmbboxClr.Items.Clear();
            this.cboAltColorRows.Items.Clear();
            Type colorType = typeof(System.Drawing.Color);
            PropertyInfo[] propInfoList = colorType.GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);
            foreach (PropertyInfo c in propInfoList)
            {
                if (c.Name != "Transparent")
                {
                    this.cmbboxClr.Items.Add(c.Name);
                    cboAltColorRows.Items.Add(c.Name);
                    cboWholeNoColor.Items.Add(c.Name);
                }
            }

            if (_Mode == Modes.New)
            {
                // Set Defualt Color
                int index = cmbboxClr.FindString("WhiteSmoke");
                if (index != -1)
                    cmbboxClr.SelectedIndex = index;


                index = cboAltColorRows.FindString("LightBlue");
                if (index != -1)
                    cboAltColorRows.SelectedIndex = index;


                index = cboWholeNoColor.FindString("WhiteSmoke");
                if (index != -1)
                    cboWholeNoColor.SelectedIndex = index;
            }
        }

        private int PopulateSheetNames(string TemplatePathFile)
        {
            int count = 0;
            if (TemplatePathFile == string.Empty)
                return 0;

            FileInfo fileInfo = new FileInfo(TemplatePathFile);
            var excel = new ExcelPackage(fileInfo);

            foreach (var worksheet in excel.Workbook.Worksheets)
            {
                cboSheetName.Items.Add(worksheet.Name);
                count++;
            }

            if (_Mode == Modes.New)
            {
                if (count == 0)
                    cboSheetName.SelectedIndex = 0;
            }
            else
            {
                int index = cboSheetName.FindStringExact(_SheetName);
                if (index != -1)
                    cboSheetName.SelectedIndex = index;

            }

            return count;
        }

        private void butLoadFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.Filter = "Excel files (*.xlsx)|*.xlsx";
            openFileDialog1.Title = "Please select an Excel file for a Template";

            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor; // Waiting 

                _SelectedFile = openFileDialog1.FileName;

                txtbTemplate.Text = _SelectedFile;

                //string fileName = Files.GetFileName(_SelectedFile);
                //_TestPathFile_xlsx = Path.Combine(_PathToolsExcelTempTemp, fileName);

                //try
                //{
                //    if (File.Exists(_TestPathFile_xlsx))
                //        File.Delete(_TestPathFile_xlsx);

                //    File.Copy(_SelectedFile, _TestPathFile_xlsx);
                //}
                //catch
                //{

                //}

                if (txtbTemplateName.Text.Length == 0)
                {
                    txtbTemplateName.Text = Files.GetFileNameWOExt(_SelectedFile);
                }

                try // Added try/catch 08.15.2020
                {
                    reoGridControl1.Load(_SelectedFile);
                }
                catch (Exception ex)
                {
                    string errMsg = ex.Message;

                    int index = errMsg.IndexOf("process cannot access the file");

                    if (index != -1 )
                    {
                        string msg = "Please close the Excel Template and the reselect it.";
                        MessageBox.Show(msg, "Close Excel Template");
                    }
                    else
                    {
                        MessageBox.Show(errMsg, "An Error has Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    return;

                }
                panLeftHeader.Visible = true;
                reoGridControl1.Visible = true;
                panExcelScale.Visible = true;

                butTempHeader.Visible = true;
                lblTempHeader.Visible = true;

                butDataMapping.Visible = true;
                lblDataMapping.Visible = true;

                if (_TemplateType == TemplateType.AR_RAM)
                {
                    butRAMMapping.Visible = true;
                    lblRAMMapping.Visible = true;
                }


             //   butOpenTemplate.Visible = true;

                PopulateSheetNames(_SelectedFile);

                if (_Mode == Modes.Edit)
                    _TemplateUpdated = true;

                Cursor.Current = Cursors.Default;
            }
        }


        private bool CheckSheetName(string ExcelFile, string SheetName)
        {
            //if (_Mode == Modes.New) // ToDo -- check!
            //{
                //if (_TestPathFileX_xlsx.Length == 0)
                //{
                //    _TestPathFileX_xlsx = this.txtbTemplate.Text;
                //}
            //}

            FileInfo testTemplate = new FileInfo(ExcelFile);

            // Create Excel EPPlus Package based on template stream
            using (ExcelPackage package = new ExcelPackage(testTemplate))
            {
                // Grab the sheet with the template.
                ExcelWorksheet sheet = package.Workbook.Worksheets[SheetName];

                if (sheet == null)
                {
                    MessageBox.Show("Please enter the correct Sheet Name.", "Wrong Sheet Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    lblSheetName.ForeColor = Color.Yellow;
                    txtbSheetName.Focus();
                    return false;
                }
                else
                {
                    return true;
                }

            }

        }

               /// <summary>
        /// retruns false if an issue was found
        /// </summary>
        /// <returns></returns>
        private bool ValidateSettingsOK()
        {
            lblTemplate.ForeColor = Color.White;
            lblSheetName.ForeColor = Color.White;
            lblTemplateName.ForeColor = Color.White;
            lblMatrixDescription.ForeColor = Color.White;

            string ExcelTemplateFile = _SelectedFile;

            if (_Mode == Modes.New)
            {
                ExcelTemplateFile = txtbTemplate.Text;

                if (ExcelTemplateFile == string.Empty)
                {
                    lblTemplate.ForeColor = Color.Yellow;
                    _currentPanel = Panels.Template;
                    PanelAdjustment();

                    MessageBox.Show("Please select an Excel Template before proceeding.", "No Excel Template Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtbTemplate.Focus();
                    return false;
                }
            }
 

            if (txtbSheetName.Text.Trim() == string.Empty)
            {
                lblSheetName.ForeColor = Color.Yellow;
                _currentPanel = Panels.Template;
                PanelAdjustment();

                MessageBox.Show("Please enter the Sheet Name for the selected Excel Template.", "Sheet Name Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtbSheetName.Focus();
                return false;
            }

            if (txtbTemplateName.Text.Trim() == string.Empty)
            {
                lblTemplateName.ForeColor = Color.Yellow;
                _currentPanel = Panels.Template;
                PanelAdjustment();

                MessageBox.Show("Please enter the Template Name for the selected Excel Template.", "Template Name Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtbTemplateName.Focus();
                return false;
            }

            string sheetName = txtbSheetName.Text.Trim();

            if (!CheckSheetName(ExcelTemplateFile, sheetName))
            {
                lblSheetName.ForeColor = Color.Yellow;
                _currentPanel = Panels.Template;
                PanelAdjustment();

                txtbSheetName.Focus();
                return false;
            }

            if (txtbDescription.Text.Trim() == string.Empty)
            {
                lblMatrixDescription.ForeColor = Color.Yellow;
                _currentPanel = Panels.Template;
                PanelAdjustment();

                MessageBox.Show("Please enter the Description for the selected Excel Template.", "Description Required", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtbDescription.Focus();
                return false;
            }

            bool mustHaveElement = ValidateMustHaveElements();

            if (!mustHaveElement)
            {
                _currentPanel = Panels.Data_Mapping;
                PanelAdjustment();

                MessageBox.Show("Please select columns for data export.", "Data Columns Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return false;

            }

            bool passed = ValidateAllDataCols();

            if (!passed)
            {
                _currentPanel = Panels.Data_Mapping;
                PanelAdjustment();

                return false;
            }

            if (_TemplateType == TemplateType.AR_RAM)
            {
                passed = ValidateMustHaveRAMElements();
                if (!passed)
                {
                    _currentPanel = Panels.RAM_Mapping;
                    PanelAdjustment();

                    return false;
                }
            }

            return true;
        }

        private bool ValidateMustHaveElements()
        {
            if (cboLocNumber.Text.Length > 0)
                return true;

            if (cboLocCaption.Text.Length > 0)
                return true;

            if (cboLocNoCaption.Text.Length > 0)
                return true;

            if (cboLocSegTxt.Text.Length > 0)
                return true;

            if (cboLocSent.Text.Length > 0)
                return true;

            return false;
        }

        private bool ValidateMustHaveRAMElements()
        {

            if (dgvRAMConfig.Rows.Count == 0)
            {
                MessageBox.Show("Responsibility Assignment Matrix (RAM) has not been configured.", "RAM Configuration Required", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return false;
            }

            int colCount = dgvRAMConfig.Columns.Count;
            if (colCount == 0)
            {
                MessageBox.Show("Responsibility Assignment Matrix (RAM) has not been configured.", "RAM Configuration Required", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return false;
            }

           
            // Search for an assigned role - a min. of role is required
            bool foundRole = false;
            foreach (DataGridViewRow row in dgvRAMConfig.Rows)
            {
                for (int i = 1; i < colCount; i++)
                {
                    if (row.Cells[i].Value != null && row.Cells[i].Value.ToString().Length > 0)
                    {
                        foundRole = true; ;
                    }
                }
            }

            if (!foundRole)
            {
               string msg = string.Concat("Responsibility Assignment Matrix (RAM) has not been configured.", Environment.NewLine, Environment.NewLine, "Map responsibilities using dictionary categories to an Excel column with an associated roles model." );

               MessageBox.Show(msg, "RAM Configuration Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;

        }

        private bool ValidateAllDataCols()
        {
            bool result = true;

            if (!ChkColDulps(cboLocLineNo, lblLocLineNo))
            {
                result = false;
            }
            if (!ChkColDulps(cboLocNumber, lblLocNumber))
            {
                result = false;
            }
            if (!ChkColDulps(cboLocCaption, lblLocCaption))
            {
                result = false;
            }
            if (!ChkColDulps(cboLocNoCaption, lblLocNoCaption))
            {
                result = false;
            }
            if (!ChkColDulps(cboLocSegTxt, lblLocSegText))
            {
                result = false;
            }
            if (!ChkColDulps(cboLocKeywords, lblLocKeywords))
            {
                result = false;
            }

            if (!ChkColDulps(cboLocNotes, lblLocNotes))
            {
                result = false;
            }

            if (!ChkColDulps(cboLocPage, lblLocPage))
            {
                result = false;
            }

            return result;
        }

        private bool ChkColDulps(ComboBox cbo, Label lbl)
        {
            lblNotice.Text = "Empty Cell Locations will NOT be populated in the Excel Template";

            string controlName = cbo.Name;
            string dupl = ValidateCol(controlName, cbo.Text);

            if (dupl != string.Empty)
            {
                lbl.ForeColor = Color.Yellow;
                duplicateValueMessage();
                return false;
            }
            else
            {
                lbl.ForeColor = Color.White;
                return true;
            }
        }

        private string ValidateCol(string sourceControlName, string colValue)
        {
            return ValidateCol(sourceControlName, colValue, string.Empty);
        }

        private string ValidateCol(string sourceControlName, string colValue, string exceptCrtl)
        {

            if (colValue == string.Empty)
                return string.Empty;

            if (sourceControlName != "cboLocCaption")
            {
                if (colValue == cboLocCaption.Text)
                {
                    return "cboLocCaption";
                }
            }

            if (sourceControlName != "cboLocKeywords")
            {
                if (colValue == cboLocKeywords.Text)
                {
                    return "cboLocKeywords";
                }
            }

            if (sourceControlName != "cboLocDicDef")
            {
                if (colValue == cboLocDicDef.Text)
                {
                    return "cboLocDicDef";
                }
            }

            if (sourceControlName != "cboLocLineNo")
            {
                if (colValue == cboLocLineNo.Text)
                {
                    return "cboLocLineNo";
                }
            }

            if (sourceControlName != "cboLocNoCaption")
            {
                if (colValue == cboLocNoCaption.Text)
                {
                    return "cboLocNoCaption";
                }
            }

            if (sourceControlName != "cboLocNotes")
            {
                if (colValue == cboLocNotes.Text)
                {
                    return "cboLocNotes";
                }
            }

            if (sourceControlName != "cboLocNumber")
            {
                if (colValue == cboLocNumber.Text)
                {
                    return "cboLocNumber";
                }
            }

            if (sourceControlName != "cboLocSegTxt")
            {
                if (exceptCrtl != "cboLocSegTxt")
                {
                    if (colValue == cboLocSegTxt.Text)
                    {
                        return "cboLocSegTxt";
                    }
                }
            }

            if (sourceControlName != "cboLocSent")
            {
                if (exceptCrtl != "cboLocSent")
                {
                    if (colValue == cboLocSent.Text)
                    {
                        return "cboLocSent";
                    }
                }
            }


            return string.Empty;
        }

        private void duplicateValueMessage()
        {
            string msg = "Duplicate data row Columns should not be the same, except for Segments/Paragraphs and Sentences.";

            lblNotice.Text = msg;
            // MessageBox.Show(msg, "Column Has Already Been Used", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void butOK_Click(object sender, EventArgs e)
        {
        //////    string selectedDic = cboRAMDictionary.Text;

        //////    if (selectedDic.Length == 0)
        //////        return;

        //////    GetRAMDictionaryForValidation(selectedDic);

        //////_RAM_DicCats_Validation = null;
        //////_RAM_DicCats_UID_Validation = null;

        //////string dictionaryName = _ds.Tables[0].Rows[0]["DictionaryName"].ToString();

        //////    List<string> dicCats = new List<string>();
        //////    List<int> uids = new List<int>();

        //////    foreach (DataRow row in _dsDictionaryValidation.Tables["Category"].Rows)
        //////    {
        //////        uids.Add(Convert.ToInt32(row["UID"].ToString()));
        //////        dicCats.Add(row["Name"].ToString());
        //////    }

        //////    _RAM_DicCats_UID_Validation = uids.ToArray();
        //////    _RAM_DicCats_Validation = dicCats.ToArray();

        //////    string cat = string.Empty;
   
        //////    int xCat = -1;
        //////    bool exitSaving = false;
        //////    foreach (DataRow dataRow in _ds.Tables[1].Rows)
        //////    {
        //////        xCat = Convert.ToInt32(dataRow["DicCatUID"].ToString());
        //////        //cat = Get_RAM_Dictionary_Cat(xCat);

        //////        //Validate here
        //////        string result = string.Empty;

        //////        if (_RAM_DicCats_Validation.Length == 0)
        //////            return;

        //////        if (_RAM_DicCats.Length == 0)
        //////            return;

        //////        int index = -1;
        //////        int i = 0;
        //////        foreach (int x in _RAM_DicCats_UID_Validation)
        //////        {
        //////            if (x == xCat)
        //////            {
        //////                index = i;
        //////                break;
        //////            }

        //////            i++;
        //////        }
        //////        if (index == -1)
        //////        {
        //////            exitSaving = true;
        //////            MessageBox.Show("Issue mapping", "Issue mapping", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //////            //return result;
        //////            break;
        //////        }
        //////    }

        //////    if(exitSaving)
        //////    {
        //////        return;
        //////    }








            Cursor.Current = Cursors.WaitCursor; // Waiting
            if (!ValidateSettingsOK())
            {
                Cursor.Current = Cursors.Default;
                return;
            }

           string file = string.Concat(txtbTemplateName.Text.Trim(), ".xml");
           string pathFile_xml = Path.Combine(_PathToolsExcelTempConfig, file);

            if (_Mode == Modes.New)
            {
                if (File.Exists(pathFile_xml))
                {
                    MessageBox.Show("Please enter another Excel Template Name.", "Excel Template is already Used", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Cursor.Current = Cursors.Default;
                    return;
                }
            }
            else
            {
                Archive();
            }

            Populate_DataSet();

            GenericDataManger gDataMgr = new GenericDataManger();

            gDataMgr.SaveDataXML(_ds, pathFile_xml);

            if (_Mode == Modes.New || _TemplateUpdated)
            {
                file = string.Concat(txtbTemplateName.Text.Trim(), ".xlsx");
                string pathFile_xlsx = Path.Combine(_PathToolsExcelTempConfig, file);
                try
                {
                    File.Copy(txtbTemplate.Text, pathFile_xlsx);
                }
                catch (Exception ex)
                {
                    string msg = string.Concat("Unable to copy your selected Excel Template file to template folder: ", _PathToolsExcelTempConfig, Environment.NewLine, Environment.NewLine, "Error: ", ex.Message);
                    MessageBox.Show(msg, "Excel Template not Saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Not Needed -> CopyWorkingTemplate();

            Cursor.Current = Cursors.Default;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();

        }

        private void Archive()
        {
            string file_xml = string.Concat(txtbTemplateName.Text.Trim(), ".xml");
            string pathFile_xml = string.Empty;
            string pathFileBackup_xml = string.Empty;

            string file_xlsx = string.Concat(txtbTemplateName.Text.Trim(), ".xlsx");
            string pathFile_xlsx = string.Empty;
            string pathFileBackup_xlsx = string.Empty;



            pathFile_xml = Path.Combine(_PathToolsExcelTempConfig, file_xml);
            pathFileBackup_xml = Path.Combine(_PathToolsExcelTempConfigBackup, file_xml);

            pathFile_xlsx = Path.Combine(_PathToolsExcelTempConfig, file_xlsx);
            pathFileBackup_xlsx = Path.Combine(_PathToolsExcelTempConfigBackup, file_xlsx);


            // Delete previous Backup xml files -- Settings files
            if (File.Exists(pathFileBackup_xml))
            {
                File.Delete(pathFileBackup_xml);
            }

            if (File.Exists(pathFileBackup_xlsx))
            {
                File.Delete(pathFileBackup_xlsx);
            }

            // Save file to Backup folder
            if (File.Exists(pathFile_xml)) 
                File.Copy(pathFile_xml, pathFileBackup_xml);

            if (File.Exists(pathFile_xlsx)) 
                File.Copy(pathFile_xlsx, pathFileBackup_xlsx);

        }

        private void Populate_DataSet()
        {
            //  Added 8.16.2018
            if (!_ds.Tables[0].Columns.Contains(ExcelTemplateFields.LocPage))
            {
                _ds.Tables[0].Columns.Add(ExcelTemplateFields.LocPage, typeof(string));
            }

            DataRow row;

            if (_Mode == Modes.New)
            {
                row = _ds.Tables[0].NewRow();
            }
            else
            {
                row = _ds.Tables[0].Rows[0];
            }


            row[ExcelTemplateFields.Description] = txtbDescription.Text.Trim();
            row[ExcelTemplateFields.LocCaption] = this.cboLocCaption.Text;
            row[ExcelTemplateFields.LocDate] = txtbLocDate.Text.Trim().ToUpper();
            row[ExcelTemplateFields.LocDocName] = txtbLocDocName.Text.Trim().ToUpper();
            row[ExcelTemplateFields.DataRowStart] = this.cboStartDataRow.Text;

            // Keywords
            if (_TemplateType == TemplateType.AR_Keywords || _TemplateType == TemplateType.DR_Keywords)
                row[ExcelTemplateFields.LocKeywords] = this.cboLocKeywords.Text;
            else if (_TemplateType == TemplateType.AR_Dic || _TemplateType == TemplateType.DR_Dic) // Dictionary
            {
                row[ExcelTemplateFields.LocDics] = this.cboLocKeywords.Text;
                row[ExcelTemplateFields.LocDicDefs] = cboLocDicDef.Text;
                row[ExcelTemplateFields.LocDicWeight] = cboDicWeights.Text;
            }
            else if (_TemplateType == TemplateType.AR_Concepts || _TemplateType == TemplateType.DR_Concepts) // Concept
            {
                row[ExcelTemplateFields.LocConcepts] = this.cboLocKeywords.Text;
            }

            if (_ds.Tables[0].Columns.Contains(ExcelTemplateFields.LocLineNo))
            {
                row[ExcelTemplateFields.LocLineNo] = this.cboLocLineNo.Text;
            }
            row[ExcelTemplateFields.LocNoCaption] = this.cboLocNoCaption.Text;
            row[ExcelTemplateFields.LocNotes] = this.cboLocNotes.Text;
            row[ExcelTemplateFields.LocPage] = this.cboLocPage.Text; // Added 8.16.2018
            row[ExcelTemplateFields.LocNumber] = this.cboLocNumber.Text;
            row[ExcelTemplateFields.LocProjectName] = txtbLocProjectName.Text.Trim().ToUpper();
            row[ExcelTemplateFields.LocSegText] = this.cboLocSegTxt.Text;
            if (_ds.Tables[0].Columns.Contains(ExcelTemplateFields.LocSentText))
            {
                row[ExcelTemplateFields.LocSentText] = this.cboLocSent.Text;
            }
            row[ExcelTemplateFields.LocYourName] = txtbLocYourName.Text.Trim().ToUpper();
            row[ExcelTemplateFields.SheetName] = txtbSheetName.Text.Trim();
            row[ExcelTemplateFields.OrgTemplateFile] = txtbTemplate.Text.Trim();
            row[ExcelTemplateFields.TemplateName] = txtbTemplateName.Text.Trim();

            if (_TemplateType == TemplateType.AR_RAM)
            {
                row[ExcelTemplateFields.RAM_ModelName] = this.cboRAMModel.Text;
                row[ExcelTemplateFields.DictionaryName] = this.cboRAMDictionary.Text;
            }

            if (_Mode == Modes.New)
            {
                row[ExcelTemplateFields.CreatedBy] = AppFolders.UserName;
                row[ExcelTemplateFields.CreatedDate] = DateTime.Now.ToString();
            }
            else
            {
                row[ExcelTemplateFields.ModifiedBy] = AppFolders.UserName;
                row[ExcelTemplateFields.ModifiedDate] = DateTime.Now.ToString();
            }

            // Font
            string FontName = cboFonts.Text;
            row[ExcelTemplateFields.FontName] = FontName;

            string FontSize = cboFontSizes.Text;
            row[ExcelTemplateFields.FontSize] = FontSize;


            // Alternating color rows for AR
            if (_ds.Tables[0].Columns.Contains(ExcelTemplateFields.AltColorRowsUse))
            {
                row[ExcelTemplateFields.AltColorRowsUse] = this.chkbAltColorRows.Checked;
                row[ExcelTemplateFields.AltColorRows] = this.cboAltColorRows.Text;
            }

            // Whole Number Color
            if (_ds.Tables[0].Columns.Contains(ExcelTemplateFields.WholeNoColorUse))
            {
                row[ExcelTemplateFields.WholeNoColorUse] = this.chkWholeNoColor.Checked;
                row[ExcelTemplateFields.WholeNoColor] = this.cboWholeNoColor.Text;
            }

            //string ExportTempFor = cboExportTempFor.Text;
            //row[ExcelTemplateFields.ExportTempFor] = ExportTempFor;

            string NotesEmbedded = cboNotesEmbedded.Text;
            row[ExcelTemplateFields.NotesEmbedded] = NotesEmbedded;

            _PageDTColExists = _ds.Tables[0].Columns.Contains("Page");

            if (_PageDTColExists)
            {
                string LocPage = cboLocPage.Text;
                row[ExcelTemplateFields.LocPage] = LocPage;
            }

            // Deep Analysis
            if (_TemplateType == TemplateType.DR_Keywords || _TemplateType == TemplateType.DR_Dic || _TemplateType == TemplateType.DR_Concepts)
            {
                row[ExcelTemplateFields.ExcludeDARCaption] = chkbDAExcludeCaption.Checked;

                row[ExcelTemplateFields.ExcludeDARNoCaption] = chkbDAExcludeNoCaption.Checked;

                //          row[ExcelTemplateFields.ExcludeDARNumber] = chkbDAExcludeNumber.Checked;
            }

            if (_ds.Tables[0].Columns.Contains(ExcelTemplateFields.SegTextUseBkColor))
            {
                row[ExcelTemplateFields.SegTextUseBkColor] = chkbHighlightSegRow.Checked;
            }

            if (_ds.Tables[0].Columns.Contains(ExcelTemplateFields.SegTextBkColor))
            {
                row[ExcelTemplateFields.SegTextBkColor] = cmbboxClr.Text;
            }

            if (_Mode == Modes.New)
            {
                _ds.Tables[0].Rows.Add(row);
            }

 

            //if (File.Exists(_TemplateDSFile))
            //{
            //    File.Delete(_TemplateDSFile); // should have already been Archived under the Backup folder
            //}


            if (_TemplateType == TemplateType.AR_RAM)
            {
                Populate_RAM_Table();
            }


            if (_PathFile_xml == string.Empty)
            {
                string fileName = string.Concat(_TemplateName, ".xml");

                _PathFile_xml = Path.Combine(_PathToolsExcelTempConfig, fileName);
            }

            GenericDataManger gDataMgr = new GenericDataManger();
            gDataMgr.SaveDataXML(_ds, _PathFile_xml);

            //_ds.WriteXml(_TestPathFile_xml, XmlWriteMode.WriteSchema);

        }

        private void Populate_DataSetWithOutSaving()
        {
            //  Added 8.16.2018
            if (!_ds.Tables[0].Columns.Contains(ExcelTemplateFields.LocPage))
            {
                _ds.Tables[0].Columns.Add(ExcelTemplateFields.LocPage, typeof(string));
            }

            DataRow row;

            if (_Mode == Modes.New)
            {
                row = _ds.Tables[0].NewRow();
            }
            else
            {
                row = _ds.Tables[0].Rows[0];
            }


            row[ExcelTemplateFields.Description] = txtbDescription.Text.Trim();
            row[ExcelTemplateFields.LocCaption] = this.cboLocCaption.Text;
            row[ExcelTemplateFields.LocDate] = txtbLocDate.Text.Trim().ToUpper();
            row[ExcelTemplateFields.LocDocName] = txtbLocDocName.Text.Trim().ToUpper();
            row[ExcelTemplateFields.DataRowStart] = this.cboStartDataRow.Text;

            // Keywords
            if (_TemplateType == TemplateType.AR_Keywords || _TemplateType == TemplateType.DR_Keywords)
                row[ExcelTemplateFields.LocKeywords] = this.cboLocKeywords.Text;
            else if (_TemplateType == TemplateType.AR_Dic || _TemplateType == TemplateType.DR_Dic) // Dictionary
            {
                row[ExcelTemplateFields.LocDics] = this.cboLocKeywords.Text;
                row[ExcelTemplateFields.LocDicDefs] = cboLocDicDef.Text;
                row[ExcelTemplateFields.LocDicWeight] = cboDicWeights.Text;
            }
            else if (_TemplateType == TemplateType.AR_Concepts || _TemplateType == TemplateType.DR_Concepts) // Concept
            {
                row[ExcelTemplateFields.LocConcepts] = this.cboLocKeywords.Text;
            }

            if (_ds.Tables[0].Columns.Contains(ExcelTemplateFields.LocLineNo))
            {
                row[ExcelTemplateFields.LocLineNo] = this.cboLocLineNo.Text;
            }
            row[ExcelTemplateFields.LocNoCaption] = this.cboLocNoCaption.Text;
            row[ExcelTemplateFields.LocNotes] = this.cboLocNotes.Text;
            row[ExcelTemplateFields.LocPage] = this.cboLocPage.Text; // Added 8.16.2018
            row[ExcelTemplateFields.LocNumber] = this.cboLocNumber.Text;
            row[ExcelTemplateFields.LocProjectName] = txtbLocProjectName.Text.Trim().ToUpper();
            row[ExcelTemplateFields.LocSegText] = this.cboLocSegTxt.Text;
            if (_ds.Tables[0].Columns.Contains(ExcelTemplateFields.LocSentText))
            {
                row[ExcelTemplateFields.LocSentText] = this.cboLocSent.Text;
            }
            row[ExcelTemplateFields.LocYourName] = txtbLocYourName.Text.Trim().ToUpper();
            row[ExcelTemplateFields.SheetName] = txtbSheetName.Text.Trim();
            row[ExcelTemplateFields.OrgTemplateFile] = txtbTemplate.Text.Trim();
            row[ExcelTemplateFields.TemplateName] = txtbTemplateName.Text.Trim();

            if (_TemplateType == TemplateType.AR_RAM)
            {
                row[ExcelTemplateFields.RAM_ModelName] = this.cboRAMModel.Text;
                row[ExcelTemplateFields.DictionaryName] = this.cboRAMDictionary.Text;
            }

            if (_Mode == Modes.New)
            {
                row[ExcelTemplateFields.CreatedBy] = AppFolders.UserName;
                row[ExcelTemplateFields.CreatedDate] = DateTime.Now.ToString();
            }
            else
            {
                row[ExcelTemplateFields.ModifiedBy] = AppFolders.UserName;
                row[ExcelTemplateFields.ModifiedDate] = DateTime.Now.ToString();
            }

            // Font
            string FontName = cboFonts.Text;
            row[ExcelTemplateFields.FontName] = FontName;

            string FontSize = cboFontSizes.Text;
            row[ExcelTemplateFields.FontSize] = FontSize;


            // Alternating color rows for AR
            if (_ds.Tables[0].Columns.Contains(ExcelTemplateFields.AltColorRowsUse))
            {
                row[ExcelTemplateFields.AltColorRowsUse] = this.chkbAltColorRows.Checked;
                row[ExcelTemplateFields.AltColorRows] = this.cboAltColorRows.Text;
            }

            // Whole Number Color
            if (_ds.Tables[0].Columns.Contains(ExcelTemplateFields.WholeNoColorUse))
            {
                row[ExcelTemplateFields.WholeNoColorUse] = this.chkWholeNoColor.Checked;
                row[ExcelTemplateFields.WholeNoColor] = this.cboWholeNoColor.Text;
            }

            //string ExportTempFor = cboExportTempFor.Text;
            //row[ExcelTemplateFields.ExportTempFor] = ExportTempFor;

            string NotesEmbedded = cboNotesEmbedded.Text;
            row[ExcelTemplateFields.NotesEmbedded] = NotesEmbedded;

            _PageDTColExists = _ds.Tables[0].Columns.Contains("Page");

            if (_PageDTColExists)
            {
                string LocPage = cboLocPage.Text;
                row[ExcelTemplateFields.LocPage] = LocPage;
            }

            // Deep Analysis
            if (_TemplateType == TemplateType.DR_Keywords || _TemplateType == TemplateType.DR_Dic || _TemplateType == TemplateType.DR_Concepts)
            {
                row[ExcelTemplateFields.ExcludeDARCaption] = chkbDAExcludeCaption.Checked;

                row[ExcelTemplateFields.ExcludeDARNoCaption] = chkbDAExcludeNoCaption.Checked;

                //          row[ExcelTemplateFields.ExcludeDARNumber] = chkbDAExcludeNumber.Checked;
            }

            if (_ds.Tables[0].Columns.Contains(ExcelTemplateFields.SegTextUseBkColor))
            {
                row[ExcelTemplateFields.SegTextUseBkColor] = chkbHighlightSegRow.Checked;
            }

            if (_ds.Tables[0].Columns.Contains(ExcelTemplateFields.SegTextBkColor))
            {
                row[ExcelTemplateFields.SegTextBkColor] = cmbboxClr.Text;
            }

            if (_Mode == Modes.New)
            {
               // _ds.Tables[0].Rows.Add(row);
            }



            //if (File.Exists(_TemplateDSFile))
            //{
            //    File.Delete(_TemplateDSFile); // should have already been Archived under the Backup folder
            //}


            if (_TemplateType == TemplateType.AR_RAM)
            {
                Populate_RAM_Table();
            }


            if (_PathFile_xml == string.Empty)
            {
                string fileName = string.Concat(_TemplateName, ".xml");

                _PathFile_xml = Path.Combine(_PathToolsExcelTempConfig, fileName);
            }

            GenericDataManger gDataMgr = new GenericDataManger();
            //gDataMgr.SaveDataXML(_ds, _PathFile_xml);

            //_ds.WriteXml(_TestPathFile_xml, XmlWriteMode.WriteSchema);

        }

        private void Populate_RAM_Table()
        {

            int uid = 0;
            int DicCatUID = -1;
            string DicCatName = string.Empty;

            int catCount = _RAM_DicCats.Length;
            int colCount = dgvRAMConfig.ColumnCount;

            string LocRole = string.Empty;
            string RoleNotation = string.Empty;

            string color = string.Empty;
            string description = string.Empty;
            string name = string.Empty;



            if (_ds.Tables.Count == 2) // for Edit mode
            {
                _ds.Tables.Remove("RAMDef");
            }

            DataTable dtRAMDef = CreateRAMTable();

            DataRow dataRow;


            foreach (DataGridViewRow row in dgvRAMConfig.Rows)
            {
                DicCatName = row.Cells[0].Value.ToString();
                DicCatUID = Get_RAM_Dictionary_Cat_UID(DicCatName);
                for (int i = 1; i < colCount; i++)
                {
                    if (i < row.Cells.Count) // Added for when columns are less than dictionary cats
                    {
                        if (row.Cells[i].Value != null)
                        {
                            RoleNotation = row.Cells[i].Value.ToString();
                            if (RoleNotation.Trim().Length > 0) // check if Role has been assigned
                            {
                                LocRole = dgvRAMConfig.Columns[i].Name; // e.g. Excel column "F"
                                Get_RAM_Model_Role_Attributes(RoleNotation, out color, out description, out name);

                                dataRow = dtRAMDef.NewRow();
                                dataRow["UID"] = uid;
                                dataRow["DicCatUID"] = DicCatUID;
                                dataRow["LocRole"] = LocRole;
                                dataRow["RoleNotation"] = RoleNotation;
                                dataRow["RoleName"] = name;
                                dataRow["RoleColor"] = color;
                                //  dataRow["RoleDescription"] = description;

                                dtRAMDef.Rows.Add(dataRow);

                                uid++;
                            }
                        }
                    }
                }

            }

            // Adjust Column wides    
            //for (int i = 1; i < colCount; i++)
            //{
            //    dgvRAMConfig.Columns[i].Width = 35;
            //}

            _ds.Tables.Add(dtRAMDef);

        }

        // Not Needed
        //private void CopyWorkingTemplate()
        //{
        //    //string fileName = string.Concat(txtbTemplateName.Text.Trim(), ".xlsx");
        //    //_OrgTempPathFile = Path.Combine(_TempPathFile, fileName);

        //    //if (_TemplateName == string.Empty)
        //    //{
        //    _TemplateName = txtbTemplateName.Text.Trim();
        //    // }

        //    if (_OrgPathFile_xlsx == string.Empty)
        //    {
        //        string fileName_xml = string.Concat(_TemplateName, ".xml");
        //        string fileName_xlsx = string.Concat(_TemplateName, ".xlsx");


   
               
        //            _OrgPathFile_xml = Path.Combine(_PathToolsExcelTempAR, fileName_xml);
        //            _OrgPathFile_xlsx = Path.Combine(_PathToolsExcelTempAR, fileName_xlsx);
   
        //    }

        //    // Excel Template
        //    if (File.Exists(_OrgPathFile_xlsx))
        //        File.Delete(_OrgPathFile_xlsx);

        //    // Replace Selected Template with Test Template
        //    File.Copy(_TestPathFile_xlsx, _OrgPathFile_xlsx);


        //    // Template Settings File
        //    if (File.Exists(_OrgPathFile_xml))
        //        File.Delete(_OrgPathFile_xml);

        //    File.Copy(_TestPathFile_xml, _OrgPathFile_xml);
        //}

        private void cboLocLineNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateAllDataCols();
        }

        private void cboLocNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateAllDataCols();

        }

        private void cboLocCaption_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateAllDataCols();
        }

        private void cboLocNoCaption_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateAllDataCols();

        }

        private void cboLocSegTxt_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateAllDataCols();

        }

        private void cboLocKeywords_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateAllDataCols();

        }

        private void cboLocDicDef_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateAllDataCols();

        }

        private void cboLocNotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateAllDataCols();

        }

        private void cboLocPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateAllDataCols();

        }

        private void cboLocSent_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateAllDataCols();

        }

        private void chkWholeNoColor_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWholeNoColor.Checked)
            {
                chkbAltColorRows.Checked = false;
                cboWholeNoColor.Visible = true;
            }
            else
            {
                cboWholeNoColor.Visible = false;
            }
        }

        private void chkbAltColorRows_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbAltColorRows.Checked)
            {
                cboAltColorRows.Visible = true;
                chkWholeNoColor.Checked = false;
            }
            else
            {
                cboAltColorRows.Visible = false;
            }
        }


        private void RAM_Config_DGV(int QtyRoleCols, string DictionaryName, string ModelName)
        {
            //dgvDocTypes.Columns.Add(
        }

        private bool ValidateStartRAMMapping(bool ShowErrorMsgs)
        {
            txtbRAMInformation.ForeColor = Color.SkyBlue;
            lblDictionary.ForeColor = Color.White;
            lblRoleCols.ForeColor = Color.White;
            lblRAMModel.ForeColor = Color.White;

            string msg = string.Empty;

            if (cboRAMDictionary.Text.Length == 0)
            {
                if (dgvRAMConfig.Visible)
                {
                    lblDictionary.ForeColor = Color.Yellow;
                    txtbRAMInformation.ForeColor = Color.Yellow;
                    lblRAMInformation.Text = string.Concat("Please select a Dictionary.", Environment.NewLine, Environment.NewLine, _MESSAGE_PRIMARY);
                    if (ShowErrorMsgs)
                    {
                        MessageBox.Show(_MESSAGE_PRIMARY, "Dictionary Selection is Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    this.Refresh();
                    cboRAMDictionary.Focus();


                    return false;
                }
            }

            if (txtRAMCols.Text.Trim().Length == 0)
            {
                if (ShowErrorMsgs)
                {
                    msg = string.Concat("Please enter Excel columns for role assignments.", Environment.NewLine, Environment.NewLine, "Enter column notations as comma-delimited. For example: E, F, G, H");
                    setDelimitedCols_Error(msg, ShowErrorMsgs);
                    this.Refresh();
                }

                return false;
            }

            if (cboRAMModel.Text.Length == 0)
            {
                if (dgvRAMConfig.Visible)
                {
                    lblRAMModel.ForeColor = Color.Yellow;

                    txtbRAMInformation.ForeColor = Color.Yellow;

                    msg = string.Concat("Please select a role mode.", Environment.NewLine, Environment.NewLine, _MESSAGE_PRIMARY);
                    lblRAMInformation.Text = msg;
                    if (ShowErrorMsgs)
                    {
                        MessageBox.Show(msg, "Role Model is Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    this.Refresh();
                    cboRAMModel.Focus();


                    return false;
                }
            }


            return CheckDelimitedColsValue(ShowErrorMsgs);
        }

        private void setDelimitedCols_Error(string msg, bool ShowErrorMsgs)
        {
            lblRoleCols.ForeColor = Color.Yellow;
            txtbRAMInformation.ForeColor = Color.Yellow;
            lblRAMInformation.Text = msg;

            if (ShowErrorMsgs)
                MessageBox.Show(msg, "Invalid Columns Entry", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            txtRAMCols.Focus();
        }

        private bool CheckDelimitedColsValue(bool ShowErrorMsgs)
        {
            _RAM_Excel_Columns = null;

            string msg = string.Empty;

            string delimitedCols = txtRAMCols.Text.Trim();

            if (delimitedCols.IndexOf(',') == -1)
            {
                if (delimitedCols.Trim().Length == 1)
                {
                    try
                    {
                        string adjdelimitedCols = delimitedCols.Replace(",","");
                        adjdelimitedCols = adjdelimitedCols.Replace(" ", "");
                        bool isAlphaBet = Regex.IsMatch(adjdelimitedCols.Trim(), "[a-z]", RegexOptions.IgnoreCase);
                        if (!isAlphaBet)
                        {
                            msg = "Please enter Excel columns as comma-delimited values. For example: E, F, G, H";
                            setDelimitedCols_Error(msg, ShowErrorMsgs);
                            return false;
                        }
                    }
                    catch { return false; }
                }
            }

            string[] delimitedColsArray = delimitedCols.Split(',');

            string xCol = string.Empty;

            foreach (string col in delimitedColsArray)
            {
                xCol = col.Trim();

                if (xCol != string.Empty)
                {

                    if (xCol.Length != 1)
                    {
                        //if (ShowErrorMsgs)
                        //{
                        msg = "Only columns A though Z are supported.";
                        setDelimitedCols_Error(msg, ShowErrorMsgs);
                        // }
                        return false;
                    }

                    bool colIsChar = Char.IsLetter(xCol, 0);
                    if (!colIsChar)
                    {
                        //if (ShowErrorMsgs)
                        //{
                        msg = "Only characters A though Z are supported.";
                        setDelimitedCols_Error(msg, ShowErrorMsgs);
                        //}
                        return false;
                    }
                }

            }

            _RAM_Excel_Columns = delimitedColsArray;

            return true;
        }

        private string[] Get_RAM_Excel_Columns()
        {

            CheckDelimitedColsValue(false); // _RAM_Excel_Columns is set in validation

            return _RAM_Excel_Columns;
        }

        private string[] Get_RAM_Dictionary_Cats()
        {
            _RAM_DicCats_UID = null;
            _RAM_DicCats = null;

            if (_dsDictionary == null)
            {
                string dictionaryName = _ds.Tables[0].Rows[0]["DictionaryName"].ToString();
                GetRAMDictionary(dictionaryName);
                if (_dsDictionary == null)
                    return null;
            }

            List<string> dicCats = new List<string>();
            List<int> uids = new List<int>();

            foreach (DataRow row in _dsDictionary.Tables["Category"].Rows)
            {

                uids.Add(Convert.ToInt32(row["UID"].ToString()));
                dicCats.Add(row["Name"].ToString());
            }

            _RAM_DicCats_UID = uids.ToArray();
            _RAM_DicCats = dicCats.ToArray();

            return _RAM_DicCats;
        }

        private string[] Get_RAM_Model_Roles()
        {
            if (_dsRAM_Model == null)
                return null;

            List<int> uids = new List<int>();
            List<string> roleNames = new List<string>();
            List<string> roleNotations = new List<string>();
            List<string> roleColor = new List<string>();
            List<string> roleDescription = new List<string>();

            uids.Add(-1);
            roleNames.Add("");
            roleNotations.Add("");
            roleColor.Add("White");
            roleDescription.Add("");

            foreach (DataRow row in _dsRAM_Model.Tables[0].Rows)
            {
                uids.Add(Convert.ToInt32(row["UID"].ToString()));
                roleNames.Add(row["RoleName"].ToString());
                roleNotations.Add(row["RoleNotation"].ToString());
                roleColor.Add(row["RoleColor"].ToString());
                roleDescription.Add(row["RoleDescription"].ToString());
            }


            _RAM_ModelRoles_Color = roleColor.ToArray();
            _RAM_ModelRoles_Decription = roleDescription.ToArray();
            _RAM_ModelRoles_Name = roleNames.ToArray();
            _RAM_ModelRoles_Notation = roleNotations.ToArray();

            return _RAM_ModelRoles_Notation;

        }

        private int Get_RAM_Model_Role_Attributes(string Notation, out string color, out string description, out string name)
        {
            int index = 0;

            color = string.Empty;
            description = string.Empty;
            name = string.Empty;

            foreach (string x in _RAM_ModelRoles_Notation)
            {
                if (x == Notation)
                {
                    color = _RAM_ModelRoles_Color[index];
                    description = _RAM_ModelRoles_Decription[index];
                    name = _RAM_ModelRoles_Name[index];

                    break;
                }

                index++; 
            }

            return index;

        }

        private void RAMStartConfigMapping_OnOff()
        {
            butStartRAMMapping.Visible = false; // default
            dgvRAMConfig.Visible = false; // default

            bool result = ValidateStartRAMMapping(false);

            butStartRAMMapping.Visible = result; 
            dgvRAMConfig.Visible = result; 
        }

        private bool Build_RAM_Config()
        {
            txtbRAMInformation.ForeColor = Color.SkyBlue;
            lblDictionary.ForeColor = Color.White;
            lblRoleCols.ForeColor = Color.White;
            lblRAMModel.ForeColor = Color.White;

            if (!ValidateStartRAMMapping(true))
            {
                return false;
            }

            if (_RAM_Excel_Columns.Length == 0)
                return false;

            Get_RAM_Dictionary_Cats();

            // Clear
            dgvRAMConfig.DataSource = null;
            dgvRAMConfig.Rows.Clear();
            dgvRAMConfig.Columns.Clear();

            dgvRAMConfig.Columns.Add(Atebion.ConceptAnalyzer.ResponsibilityAssMatrixFields.Dictionary_Category_Name, "Dictionary Category");
            if (_RAM_DicCats == null)
                return false;
            foreach (string cat in _RAM_DicCats)
            {
                int rowIndex = dgvRAMConfig.Rows.Add();
                DataGridViewRow row = dgvRAMConfig.Rows[rowIndex];
                row.Cells[Atebion.ConceptAnalyzer.ResponsibilityAssMatrixFields.Dictionary_Category_Name].Value = cat;

            }

            Get_RAM_Model_Roles();

            foreach (string excelColumn in _RAM_Excel_Columns)
            {
                DataGridViewComboBoxColumn column = new DataGridViewComboBoxColumn();
                column.Name = excelColumn.Trim();
                column.DataSource = _RAM_ModelRoles_Notation;
                column.HeaderText = excelColumn.Trim();
                column.DataPropertyName = "RoleNotation";

                dgvRAMConfig.Columns.Add(column);
            }


            dgvRAMConfig.AllowUserToAddRows = false;

            lblRAMInformation.Text = string.Concat("Map responsibilities by associating dictionary categories to Excel columns (e.g. ", this.txtRAMCols.Text.Trim(), " ) with role notations from the selected model.");

            return true;

        }

        private void butStartRAMMapping_Click(object sender, EventArgs e)
        {
            if (dgvRAMConfig.Rows.Count > 0)
            {
                string msg = string.Concat("Are you sure you want to replace the existing RAM Mapping per the new parameters.",
                                            Environment.NewLine, Environment.NewLine,
                                            "Any existing role mapping will be cleared/lost.");
                if (MessageBox.Show(msg, "Confirm New Mapping", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
            }

            Build_RAM_Config();

        }

        private void lblRAMInformation_TextChanged(object sender, EventArgs e)
        {
            txtbRAMInformation.Text = lblRAMInformation.Text;
        }

        private void txtbRAMInformation_TextChanged(object sender, EventArgs e)
        {
            txtbRAMInformation.Text = lblRAMInformation.Text; ;

        }

        private bool GetRAMDictionary(string DictionaryName)
        {
            string msg = string.Empty;

            string file = string.Concat(DictionaryName, ".dicx");
            string pathFile = Path.Combine(AppFolders.DictionariesPath, file);

            if (!File.Exists(pathFile))
            {
                lblDictionary.ForeColor = Color.Yellow;
                txtbRAMInformation.ForeColor = Color.Yellow;
                msg = string.Concat("Unable to find dictionary: ", pathFile);
                lblRAMInformation.Text = msg;
                MessageBox.Show(msg, "Dictionary Selected NOT found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
                //cboRAMDictionary.Focus();


                return false;
            }

            GenericDataManger gDataMgr = new GenericDataManger();
            _dsDictionary = gDataMgr.LoadDatasetFromXml(pathFile);

            if (_dsDictionary == null)
            {
                lblDictionary.ForeColor = Color.Yellow;
                txtbRAMInformation.ForeColor = Color.Yellow;
                msg = string.Concat("Error: ", gDataMgr.ErrorMessage);
                lblRAMInformation.Text = msg;
                MessageBox.Show(msg, "Unable to Read the Dictionary", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboRAMDictionary.Focus();

                butStartRAMMapping.Visible = false;

                return false;
            }

            return true;
        }

        private bool GetRAMDictionaryForValidation(string DictionaryName)
        {
            string msg = string.Empty;

            string file = string.Concat(DictionaryName, ".dicx");
            string pathFile = Path.Combine(AppFolders.DictionariesPath, file);

            if (!File.Exists(pathFile))
            {
                lblDictionary.ForeColor = Color.Yellow;
                txtbRAMInformation.ForeColor = Color.Yellow;
                msg = string.Concat("Unable to find dictionary: ", pathFile);
                lblRAMInformation.Text = msg;
                MessageBox.Show(msg, "Dictionary Selected NOT found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboRAMDictionary.Focus();


                return false;
            }

            GenericDataManger gDataMgr = new GenericDataManger();
            _dsDictionaryValidation = gDataMgr.LoadDatasetFromXml(pathFile);

            if (_dsDictionary == null)
            {
                lblDictionary.ForeColor = Color.Yellow;
                txtbRAMInformation.ForeColor = Color.Yellow;
                msg = string.Concat("Error: ", gDataMgr.ErrorMessage);
                lblRAMInformation.Text = msg;
                MessageBox.Show(msg, "Unable to Read the Dictionary", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboRAMDictionary.Focus();

                butStartRAMMapping.Visible = false;

                return false;
            }

            return true;
        }

        private void cboRAMDictionary_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtbRAMInformation.ForeColor = Color.SkyBlue;
            lblDictionary.ForeColor = Color.White;

            string selectedDic = cboRAMDictionary.Text;

            if (selectedDic.Length == 0)
                return;


            string msg = string.Empty;

            //string file = string.Concat(selectedDic, ".dicx");
            //string pathFile = Path.Combine(AppFolders.DictionariesPath, file);

            //if (!File.Exists(pathFile))
            //{
            //    lblDictionary.ForeColor = Color.Yellow;
            //    txtbRAMInformation.ForeColor = Color.Yellow;
            //    msg = string.Concat("Unable to find dictionary: ", pathFile);
            //    lblRAMInformation.Text = msg;
            //    MessageBox.Show(msg, "Dictionary Selected NOT found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    cboRAMDictionary.Focus();

            //    butStartRAMMapping.Visible = false;

            //    return;
            //}

            //GenericDataManger gDataMgr = new GenericDataManger();
            //_dsDictionary = gDataMgr.LoadDatasetFromXml(pathFile);



            GetRAMDictionary(selectedDic);
            //{
            //    lblDictionary.ForeColor = Color.Yellow;
            //    txtbRAMInformation.ForeColor = Color.Yellow;
            //    msg = string.Concat("Error: ", gDataMgr.ErrorMessage);
            //    lblRAMInformation.Text = msg;
            //    MessageBox.Show(msg, "Unable to Read the Selected Dictionary", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    cboRAMDictionary.Focus();

            //    butStartRAMMapping.Visible = false;

            //    return;
            //}

        }

        private void cboRAMModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtbRAMInformation.ForeColor = Color.SkyBlue;
            lblRAMModel.ForeColor = Color.White;

            string selectedRAM = cboRAMModel.Text;

            if (selectedRAM.Length == 0)
                return;

            string msg = string.Empty;

            string file = string.Concat(selectedRAM, ".ram");
            string pathFile = Path.Combine(AppFolders.AppDataPathToolsRAMDefs, file);

            if (!File.Exists(pathFile))
            {
                lblDictionary.ForeColor = Color.Yellow;
                txtbRAMInformation.ForeColor = Color.Yellow;
                msg = string.Concat("Unable to find RAM Model: ", pathFile);
                lblRAMInformation.Text = msg;
                MessageBox.Show(msg, "RAM Model Selected NOT found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboRAMDictionary.Focus();

                butStartRAMMapping.Visible = false;

                return;
            }

            GenericDataManger gDataMgr = new GenericDataManger();
            _dsRAM_Model = gDataMgr.LoadDatasetFromXml(pathFile);

            if (_dsRAM_Model == null)
            {
                lblRAMModel.ForeColor = Color.Yellow;
                txtbRAMInformation.ForeColor = Color.Yellow;
                msg = string.Concat("Error: ", gDataMgr.ErrorMessage);
                lblRAMInformation.Text = msg;
                MessageBox.Show(msg, "Unable to Read the Selected RAM Model", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboRAMModel.Focus();

                butStartRAMMapping.Visible = false;
                return;
            }

            RAMStartConfigMapping_OnOff();

            // Display RAM Model
            dvgModelItems.Visible = true;

            GenericDataManger gDMgr = new GenericDataManger();
            DataSet ds = gDMgr.LoadDatasetFromXml(pathFile);

            if (ds == null)
            {
                MessageBox.Show(gDMgr.ErrorMessage, "Unable to Retrieve the Selected RAM Model Items", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (ds.Tables["RAMDef"].Rows.Count == 0)
            {
                MessageBox.Show("The selected RAM Model does not have any items", "No Model Items Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            dvgModelItems.DataSource = ds.Tables["RAMDef"];

            dvgModelItems.Columns["RoleDescription"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dvgModelItems.Columns["UID"].Visible = false;
            dvgModelItems.Columns["RoleColor"].Visible = false;
            dvgModelItems.Columns["RoleDescription"].Visible = false;

            dvgModelItems.Columns["RoleNotation"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dvgModelItems.Columns["RoleNotation"].DefaultCellStyle.ForeColor = Color.Black;


            dvgModelItems.ColumnHeadersVisible = false;

            dvgModelItems.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dvgModelItems.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void SetRAMModelBackColor()
        {

            string colorHighlight = string.Empty;

            foreach (DataGridViewRow row in dvgModelItems.Rows)
            {
                if (row.Cells["RoleColor"].Value != null)
                {
                    colorHighlight = row.Cells["RoleColor"].Value.ToString();
                    row.Cells["RoleNotation"].Style.BackColor = Color.FromName(colorHighlight);
                }

            }

            //     dvgModelItems.RowHeadersVisible = false;


        }

        private void txtRAMCols_Leave(object sender, EventArgs e)
        {
            txtRAMCols.Text = txtRAMCols.Text.Trim().ToUpper();
            RAMStartConfigMapping_OnOff();
        }

        // Use event for changing Role Notation background color
        private void dgvRAMConfig_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control != null)
            {
                string notation = e.Control.Text;

                string color = string.Empty;
                string description = string.Empty;
                string name = string.Empty;

                int index = Get_RAM_Model_Role_Attributes(notation, out color, out description, out name);

                e.CellStyle.BackColor = Color.FromName(_RAM_ModelRoles_Color[index]);
                e.Control.BackColor = Color.FromName(_RAM_ModelRoles_Color[index]);
            }

            //ComboBox cb = e.Control as ComboBox;
            //if (cb != null)
            //{

            

            //    string notation = cb.Text;

            //    int selectedIndex = cb.SelectedIndex;

            //    if (selectedIndex == -1)
            //    {

            //        cb.BackColor = Color.White;
            //    }
            //    else
            //    {
            //        e.Style.BackColor = Color.FromName(_RAM_ModelRoles_Color[selectedIndex]);
            //        cb.BackColor = Color.FromName(_RAM_ModelRoles_Color[selectedIndex]);
            //    }


            //}
        }

 

        private void butTemplate_Click(object sender, EventArgs e)
        {
            _currentPanel = Panels.Template;
            PanelAdjustment();
        }

        private void lblTemplate_Click(object sender, EventArgs e)
        {
            _currentPanel = Panels.Template;
            PanelAdjustment();
        }

        private void butTempHeader_Click(object sender, EventArgs e)
        {
            _currentPanel = Panels.Header;
            PanelAdjustment();

        }

        private void lblTempHeader_Click(object sender, EventArgs e)
        {
            _currentPanel = Panels.Header;
            PanelAdjustment();

        }

        private void butDataMapping_Click(object sender, EventArgs e)
        {
            _currentPanel = Panels.Data_Mapping;
            PanelAdjustment();

        }

        private void lblDataMapping_Click(object sender, EventArgs e)
        {
            _currentPanel = Panels.Data_Mapping;
            PanelAdjustment();

        }

        private void butRAMMapping_Click(object sender, EventArgs e)
        {
            _currentPanel = Panels.RAM_Mapping;
            PanelAdjustment();

        }

        private void lblRAMMapping_Click(object sender, EventArgs e)
        {
            _currentPanel = Panels.RAM_Mapping;
            PanelAdjustment();

        }

        private void butSummary_Click(object sender, EventArgs e)
        {
            _currentPanel = Panels.Validation;
            PanelAdjustment();

        }

        private void lblSummary_Click(object sender, EventArgs e)
        {
            _currentPanel = Panels.Validation;
            PanelAdjustment();

        }

        //private void butCancel_Click(object sender, EventArgs e)
        //{
        //    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        //    this.Close();
        //}

        private void mtrb_Scroll(object sender, ScrollEventArgs e)
        {
            
            reoGridControl1.Worksheets[0].ScaleFactor = mtrb.Value / 10f;

            lblMatrixScale.Text = (reoGridControl1.Worksheets[0].ScaleFactor * 100) + "%";
        }

        private void dgvRAMConfig_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            //DataGridView dgv = sender as DataGridView;

            //if (!dgv.Columns[e.ColumnIndex].Name.Equals("Category"))
            //{
            //    if (e.Value != null)
            //    {
            //        string color = string.Empty;
            //        string notation = e.Value.ToString().Trim();
            //        string description = string.Empty;
            //        string name = string.Empty;

            //        int index = Get_RAM_Model_Role_Attributes(notation, out color, out description, out name);

            //        dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.FromName(color);

            //    }
            //}
        }

        private void dvgModelItems_Paint(object sender, PaintEventArgs e)
        {
            SetRAMModelBackColor();
        }

        private void dvgModelItems_SelectionChanged(object sender, EventArgs e)
        {
            dvgModelItems.ClearSelection();  
        }

        private void butStartRAMMapping_VisibleChanged(object sender, EventArgs e)
        {
            if (butStartRAMMapping.Visible)
                lblRAMInformation.Text = string.Concat("Click the ", butStartRAMMapping.Text, " button to map Roles to the Template Columns."); 
        }

        private void butCancel_Click_1(object sender, EventArgs e)
        {

            //Atebion.WorkGroups.Manager workgroupMgr;
            //workgroupMgr = new Atebion.WorkGroups.Manager(AppFolders.AppDataPath, AppFolders.UserName, AppFolders.AppDataPathUser);
            //workgroupMgr.ApplicationPath = Application.StartupPath;

            //workgroupMgr.ImportDefaultSettingFilesDicRAM();



            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

 

        private void cboAltColorRows_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = new Font("Arial", 9, FontStyle.Regular);
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
                g.FillRectangle(b, rect.X + 110, rect.Y + 5, rect.Width - 10, rect.Height - 10);
            }
        }

        private void cboWholeNoColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = new Font("Arial", 9, FontStyle.Regular);
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
                g.FillRectangle(b, rect.X + 110, rect.Y + 5, rect.Width - 10, rect.Height - 10);
            }
        }

        private void cmbboxClr_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = new Font("Arial", 9, FontStyle.Regular);
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
                g.FillRectangle(b, rect.X + 110, rect.Y + 5, rect.Width - 10, rect.Height - 10);
            }
        }

        private void cboSheetName_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtbSheetName.Text = cboSheetName.Text;
        }

        private void txtRAMCols_TextChanged(object sender, EventArgs e)
        {
            CheckDelimitedColsValue(true);
        }

        private void cboStartDataRow_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void cboRAMDictionary_SelectionChangeCommitted(object sender, EventArgs e)
        {

            //MessageBox.Show("Test", "Test", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtbRAMInformation.ForeColor = Color.SkyBlue;
            lblDictionary.ForeColor = Color.White;

            string selectedDic = cboRAMDictionary.Text;

            if (selectedDic.Length == 0)
                return;

            GetRAMDictionary(selectedDic);

            _RAM_DicCats_UID = null;
            _RAM_DicCats = null;

            List<string> dicCats = new List<string>();
            List<int> uids = new List<int>();

            foreach (DataRow row in _dsDictionary.Tables["Category"].Rows)
            {

                uids.Add(Convert.ToInt32(row["UID"].ToString()));
                dicCats.Add(row["Name"].ToString());
            }

            _RAM_DicCats_UID = uids.ToArray();
            _RAM_DicCats = dicCats.ToArray();

            if (_dsDictionary != null)
            {
                Populate_DataSetWithOutSaving();
                _currentPanel = Panels.RAM_Mapping;
                PanelAdjustment();
            }

            Build_RAM_Config();
            dgvRAMConfig.Visible = true;
        }

        //void cb_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    MessageBox.Show("Selected index changed");
        //}


    }
}
