using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using Atebion.Common;

using unvell.ReoGrid;
using unvell.ReoGrid.Events;

namespace ProfessionalDocAnalyzer
{
    public partial class ucExcelTemplates : UserControl
    {
        public ucExcelTemplates()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        private const string _AR_Keywords = "Analysis Results Keywords";
        private const string _DA_Keywords = "Deep Analysis Results Keywords";
        private const string _AR_DicTerms = "Analysis Results Dictionary";
        private const string _AR_Concepts = "Analysis Results Concepts";
        private const string _AR_RAM = "Analysis Results RAM";

        private string _CurrentTemplateType = string.Empty;
        private string _TemplatesPath = string.Empty;
        private string _TemplateName = string.Empty;
        private string _TemplatePathFile = string.Empty;

        public void LoadData()
        {
            lstbTemplateTypes.Items.Clear();
            lstbTemplateName.Items.Clear();

            lstbTemplateTypes.Items.Add(_AR_Keywords);
            lstbTemplateTypes.Items.Add(_DA_Keywords);
            lstbTemplateTypes.Items.Add(_AR_DicTerms);
            lstbTemplateTypes.Items.Add(_AR_Concepts);
            lstbTemplateTypes.Items.Add(_AR_RAM);

            panTemplatePreview.Visible = false;
            reoGridControl1.Visible = false;
            splitContainer1.Visible = false;
        }


        public bool New()
        {
            frmExcelTemp frm = new frmExcelTemp();
            frmExcelTemp.TemplateType tempType = GetTemplateType();
            frm.LoadData(tempType);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                LoadData();
            }

            return true;
        }

        public void Download()
        {
            string tempPath = string.Empty;
            string fileType = string.Empty;

            // Concept Compare Documents and Dictionary Compare Documents are currently not supported to editing

            switch (_CurrentTemplateType)
            {
                case _AR_Keywords:
                    fileType = "AR";
                    tempPath = AppFolders.AppDataPathToolsExcelTempAR;
                    break;

                case _DA_Keywords:
                    fileType = "DAR";
                    tempPath = AppFolders.AppDataPathToolsExcelTempDAR;
                    break;

                case _AR_DicTerms:
                    fileType = "DicDoc";
                    tempPath = AppFolders.AppDataPathToolsExcelTempDicDoc;
                    break;

                case _AR_Concepts:
                    fileType = "ConceptsDoc";
                    tempPath = AppFolders.AppDataPathToolsExcelTempConceptsDocs;
                    break;

                case _AR_RAM:
                    fileType = "DicRAM";
                    tempPath = AppFolders.AppDataPathToolsExcelTempDicRAM;
                    break;

            }

            if (tempPath == string.Empty)
                return;

            frmDownLoad frm = new frmDownLoad();
            frm.LoadData(fileType, tempPath, ContentTypes.Excel_Templates);

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                LoadData();
            }
        }

        public bool Edit()
        {
            if (this.lstbTemplateTypes.SelectedIndex == -1)
            {
                string msg = "Please select a Template Type.";
                MessageBox.Show(msg, "No Template Type Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return false;
            }
            if (lstbTemplateName.Items.Count == 0)
            {
                string msg = "Either download or create a new Template.";
                MessageBox.Show(msg, "No Template Exists", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (lstbTemplateName.SelectedIndex == -1)
            {
                string msg = "Please select a Template.";
                MessageBox.Show(msg, "No Template Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            Cursor.Current = Cursors.WaitCursor; // Waiting

            _TemplateName = lstbTemplateName.Text;

            frmExcelTemp frm = new frmExcelTemp();
            frmExcelTemp.TemplateType tempType = GetTemplateType();

            frm.LoadData(tempType, _TemplateName);

            Cursor.Current = Cursors.Default; // Done

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                LoadData();
            }


            return true;
        }

        public bool Delete()
        {
            if (lstbTemplateName.Items.Count == 0)
                return false;

             if (lstbTemplateName.SelectedIndex == -1)
                return false;

             _TemplateName = lstbTemplateName.Text;

             string msg = string.Concat("Are you sure that you want to Delete the template '", _TemplateName, "' ?");
             if (MessageBox.Show(msg, "Confirm Delete Excel Template", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
             {
                 return false;
             }

             Cursor.Current = Cursors.WaitCursor; // Wait

             string file_xml = string.Concat(_TemplateName, ".xml");
             string file_xlsx = string.Concat(_TemplateName, ".xlsx");

             string pathFile_xml = string.Empty;
             string pathFileBackup_xml = string.Empty;

             string pathFile_xlsx = string.Empty;
             string pathFileBackup_xlsx = string.Empty;

             switch (_CurrentTemplateType)
             {
                 case _AR_Keywords:
                    pathFile_xml = Path.Combine(AppFolders.AppDataPathToolsExcelTempAR, file_xml);
                    pathFileBackup_xml = Path.Combine(AppFolders.AppDataPathToolsExcelTempARBackup, file_xml);

                    pathFile_xlsx = Path.Combine(AppFolders.AppDataPathToolsExcelTempAR, file_xlsx);
                    pathFileBackup_xlsx = Path.Combine(AppFolders.AppDataPathToolsExcelTempARBackup, file_xlsx);

                    break;

                 case _DA_Keywords:
                    pathFile_xml = Path.Combine(AppFolders.AppDataPathToolsExcelTempDAR, file_xml);
                    pathFileBackup_xml = Path.Combine(AppFolders.AppDataPathToolsExcelTempDARBackup, file_xml);

                    pathFile_xlsx = Path.Combine(AppFolders.AppDataPathToolsExcelTempDAR, file_xlsx);
                    pathFileBackup_xlsx = Path.Combine(AppFolders.AppDataPathToolsExcelTempDARBackup, file_xlsx);

                    break;

                 case _AR_DicTerms:
                    pathFile_xml = Path.Combine(AppFolders.AppDataPathToolsExcelTempDicDoc, file_xml);
                    pathFileBackup_xml = Path.Combine(AppFolders.AppDataPathToolsExcelTempDicDocBackup, file_xml);

                    pathFile_xlsx = Path.Combine(AppFolders.AppDataPathToolsExcelTempDicDoc, file_xlsx);
                    pathFileBackup_xlsx = Path.Combine(AppFolders.AppDataPathToolsExcelTempDicDocBackup, file_xlsx);

                    break;

                 case _AR_Concepts:
                    pathFile_xml = Path.Combine(AppFolders.AppDataPathToolsExcelTempConceptsDocs, file_xml);
                    pathFileBackup_xml = Path.Combine(AppFolders.AppDataPathToolsExcelTempConceptsDocBackup, file_xml);

                    pathFile_xlsx = Path.Combine(AppFolders.AppDataPathToolsExcelTempConceptsDocs, file_xlsx);
                    pathFileBackup_xlsx = Path.Combine(AppFolders.AppDataPathToolsExcelTempConceptsDocBackup, file_xlsx);

                    break;

                 case _AR_RAM:
                    pathFile_xml = Path.Combine(AppFolders.AppDataPathToolsExcelTempDicRAM, file_xml);
                    pathFileBackup_xml = Path.Combine(AppFolders.AppDataPathToolsExcelTempDicRAMBackup, file_xml);

                    pathFile_xlsx = Path.Combine(AppFolders.AppDataPathToolsExcelTempDicRAM, file_xlsx);
                    pathFileBackup_xlsx = Path.Combine(AppFolders.AppDataPathToolsExcelTempDicRAMBackup, file_xlsx);

                    break;

             }


             if (File.Exists(pathFileBackup_xml))
             {
                 File.Delete(pathFileBackup_xml);
             }

             if (File.Exists(pathFileBackup_xlsx))
             {
                 File.Delete(pathFileBackup_xlsx);
             }

             if (File.Exists(pathFile_xml)) // Added 08.15.2020
             {
                 File.Copy(pathFile_xml, pathFileBackup_xml); // Backup Template Settings file before Deleting it
                 File.Delete(pathFile_xml);
             }

             if (File.Exists(pathFile_xlsx)) // Added 08.15.2020
             {
                 File.Copy(pathFile_xlsx, pathFileBackup_xlsx); // Backup Excel Template file before Deleting it
                 File.Delete(pathFile_xlsx);
             }
             

             _TemplateName = string.Empty;

             PopulateTemplateNames();

             Cursor.Current = Cursors.Default; // Done

             return true;
        }

        private frmExcelTemp.TemplateType GetTemplateType()
        {
            switch (_CurrentTemplateType)
            {
                case _AR_Keywords:
                    return frmExcelTemp.TemplateType.AR_Keywords;
                
                case _DA_Keywords:
                    return frmExcelTemp.TemplateType.DR_Keywords;

                case _AR_DicTerms:
                    return frmExcelTemp.TemplateType.AR_Dic;

                case _AR_Concepts:
                    return frmExcelTemp.TemplateType.AR_Concepts;

                case _AR_RAM:
                    return frmExcelTemp.TemplateType.AR_RAM;
                   
            }

            return frmExcelTemp.TemplateType.AR_Keywords;
        }


        private void ucExcelTemplates_Load(object sender, EventArgs e)
        {

        }

        private void lstbTemplateTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            panTemplatePreview.Visible = false;
            reoGridControl1.Visible = false;
            lstbTemplateName.Items.Clear();

          //  splitContainer1.Visible = false;

            _CurrentTemplateType = lstbTemplateTypes.Text;

            switch (_CurrentTemplateType)
            {
                case _AR_Keywords:
                    _TemplatesPath = AppFolders.AppDataPathToolsExcelTempAR;
                    break;

                case _DA_Keywords:
                    _TemplatesPath = AppFolders.AppDataPathToolsExcelTempDAR;
                    break;

                case _AR_DicTerms:
                    _TemplatesPath = AppFolders.AppDataPathToolsExcelTempDicDoc;
                    break;

                case _AR_Concepts:
                    _TemplatesPath = AppFolders.AppDataPathToolsExcelTempConceptsDoc;
                    break;

                case _AR_RAM:
                    _TemplatesPath = AppFolders.AppDataPathToolsExcelTempDicRAM;
                    break;

            }

            int foundQty = PopulateTemplateNames();

            if (foundQty == -1)
            {
                string msg = string.Concat("The template path for ", _CurrentTemplateType, " has not been defined.");
                MessageBox.Show(msg, "Template Path Unknown", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (foundQty == 0)
            {
                string msg = string.Concat("No templates were found for ", _CurrentTemplateType, ".", Environment.NewLine, Environment.NewLine, "Suggest downloading templated by clicking the Download button below.");
                MessageBox.Show(msg, "No Templates", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            return;

        }

        private int PopulateTemplateNames()
        {
            lstbTemplateName.Items.Clear();

            if (_TemplatesPath.Length == 0)
                return -1;

            string[] files = Directory.GetFiles(_TemplatesPath, "*.xlsx");

            if (files.Length == 0)
                return 0;

            string fileName = string.Empty;

            foreach (string file in files)
            {
                fileName = Files.GetFileNameWOExt(file);

                lstbTemplateName.Items.Add(fileName);
            }

            splitContainer1.Visible = true;

            panTemplatePreview.Visible = false;
            reoGridControl1.Visible = false;

            return files.Length;

        }

        private void lstbTemplateName_SelectedIndexChanged(object sender, EventArgs e)
        {
            _TemplateName = lstbTemplateName.Text;

            string file = string.Concat(_TemplateName, ".xlsx");

            _TemplatePathFile = Path.Combine(_TemplatesPath, file);

            if (!File.Exists(_TemplatePathFile))
            {
                string msg = string.Concat("Unable to file template: ", _TemplatePathFile);
                MessageBox.Show(msg, "Template Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Cursor.Current = Cursors.WaitCursor; // Waiting

            reoGridControl1.Load(_TemplatePathFile);
            var worksheet = reoGridControl1.CurrentWorksheet;

            worksheet.ScaleFactor = 0.75f;

            worksheet.SetSettings(WorksheetSettings.Edit_Readonly, true);

            panTemplatePreview.Visible = true;
            reoGridControl1.Visible = true;

            Cursor.Current = Cursors.Default;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string tempPath = string.Empty;

            switch (_CurrentTemplateType)
            {
                case _AR_Keywords:
                    tempPath = AppFolders.AppDataPathToolsExcelTempAR;
                    break;

                case _DA_Keywords:
                    tempPath = AppFolders.AppDataPathToolsExcelTempDAR;
                    break;

                case _AR_DicTerms:
                    tempPath = AppFolders.AppDataPathToolsExcelTempDicDoc;
                    break;

                case _AR_Concepts:
                    tempPath = AppFolders.AppDataPathToolsExcelTempConceptsDocs;
                    break;

                case _AR_RAM:
                    tempPath = AppFolders.AppDataPathToolsExcelTempDicRAM;
                    break;

            }

            if (tempPath.Length > 0)
                System.Diagnostics.Process.Start("explorer.exe", tempPath);
        }
    }
}
