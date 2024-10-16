using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

using Atebion.Common;

namespace ProfessionalDocAnalyzer
{
    public partial class frmExport : MetroFramework.Forms.MetroForm
    {
        public frmExport()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();        
        }

        private DataTable _dt;
        private DataTable _dtSum;
        private string _ProjectName = string.Empty;
        private string _AnalysisName = string.Empty;
        private string _DocumentName = string.Empty;
        private string[] _Docs;
        private Atebion.ConceptAnalyzer.Analysis _CAMgr;
        private string _TempPath = string.Empty;
        private string _ReportPath = string.Empty;
        private string[] _xmlTemplates;

        private string _ErrorMessage = string.Empty;

        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        private ReportType _currentReportType;
        private enum ReportType
        {
            ConceptsDoc = 0,
            ConceptsDocs = 1,
            ExportDicDoc = 2,
            ExportDicDocs = 3

        }

        public bool Load_ExportConceptsDoc(Atebion.ConceptAnalyzer.Analysis CAMgr, DataTable dt, string ProjectName, string AnalysisName, string DocumentName)
        {
            _ErrorMessage = string.Empty;

            _CAMgr = CAMgr;
            _dt = dt;
            _ProjectName = ProjectName;
            _AnalysisName = AnalysisName;
            _DocumentName = DocumentName;

            _TempPath = AppFolders.AppDataPathToolsExcelTempConceptsDoc;

            _xmlTemplates = Directory.GetFiles(_TempPath, "*.xml");

            cboExcelTemplate.Items.Clear();

            string TemplateName = string.Empty;
            foreach (string template in _xmlTemplates)
            {
                TemplateName = Files.GetFileNameWOExt(template);
                if (TemplateName.Trim() != string.Empty)
                    cboExcelTemplate.Items.Add(TemplateName);
            }

            if (cboExcelTemplate.Items.Count == 0) // Added 03.10.2020
            {
                string msg = string.Concat("No Excel templates were found.", Environment.NewLine, Environment.NewLine, "Please go to Settings and either Download a template or create a new template.");
                MessageBox.Show(msg, "No Excel Templates Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            cboExcelTemplate.SelectedIndex = 0;

            if (_xmlTemplates.Length > 1)
            {
                lblExcelTemplate.Visible = true;
                cboExcelTemplate.Visible = true;
            }

            chkbWeightColors.Visible = false;

            // Get Next Report Name
            txtExportFileName.Text = _CAMgr.GetNext_ExportTemps_ConceptsDoc_ReportName(_ProjectName, _DocumentName, out _ReportPath);

            _currentReportType = ReportType.ConceptsDoc;


            return true;
        }

        public bool Load_ExportConceptsDocs(Atebion.ConceptAnalyzer.Analysis CAMgr, DataTable dt, string[] Docs, string ProjectName, string AnalysisName)
        {
            _ErrorMessage = string.Empty;

            _CAMgr = CAMgr;
            _dt = dt;
            _ProjectName = ProjectName;
            _AnalysisName = AnalysisName;
            _Docs = Docs;

            _TempPath = AppFolders.AppDataPathToolsExcelTempConceptsDocs;

            //_xmlTemplates = _CAMgr.Get_ExportTemps_ConceptsDocs(out _TempPath, exeTempPath);
            //if (_xmlTemplates.Length == 0)
            //{
            //    _xmlTemplates = _CAMgr.Get_ExportTemps_ConceptsDocs(out _TempPath, exeTempPath); // Try again
            //    if (_xmlTemplates.Length == 0)
            //    {
            //        _ErrorMessage = string.Concat("Unable to locate the Excel Concept Documents Comparison export template in folder: ", _TempPath);
            //        return false;
            //    }
            //}

            _xmlTemplates = Directory.GetFiles(_TempPath, "*.xml");

            string TemplateName = string.Empty;
            foreach (string template in _xmlTemplates)
            {
                TemplateName = Files.GetFileNameWOExt(template);
                cboExcelTemplate.Items.Add(TemplateName);
            }

            cboExcelTemplate.SelectedIndex = 0;

            if (_xmlTemplates.Length > 1)
            {
                lblExcelTemplate.Visible = true;
                cboExcelTemplate.Visible = true;
            }

            chkbWeightColors.Visible = true;
            bool useColor = chkbWeightColors.Checked;

            // Get Next Report Name
            txtExportFileName.Text = _CAMgr.GetNext_ExportTemps_ConceptsDocs_ReportName(_ProjectName, _AnalysisName, out _ReportPath );

            _currentReportType = ReportType.ConceptsDocs;

            return true;
        }

        public bool Load_ExportDicDoc(Atebion.ConceptAnalyzer.Analysis CAMgr, DataTable dt, string ProjectName, string AnalysisName, string DocumentName)
        {
            _ErrorMessage = string.Empty;

            _CAMgr = CAMgr;
            _dt = dt;
            _ProjectName = ProjectName;
            _AnalysisName = AnalysisName;
            _DocumentName = DocumentName;


            _TempPath = AppFolders.AppDataPathToolsExcelTempDicDoc;

            //_xmlTemplates = _CAMgr.Get_ExportTemps_DicDoc(out _TempPath, exeTempPath);
            //if (_xmlTemplates.Length == 0)
            //{
            //    _xmlTemplates = _CAMgr.Get_ExportTemps_DicDoc(out _TempPath, exeTempPath); // Try Again
            //    if (_xmlTemplates.Length == 0)
            //    {
            //        _ErrorMessage = string.Concat("Unable to locate the Excel Dictionary Document export template in folder: ", _TempPath);
            //        return false;
            //    }
            //}

            _xmlTemplates = Directory.GetFiles(_TempPath, "*.xml");

            string TemplateName = string.Empty;
            foreach (string template in _xmlTemplates)
            {
                TemplateName = Files.GetFileNameWOExt(template);
                cboExcelTemplate.Items.Add(TemplateName);
            }

            cboExcelTemplate.SelectedIndex = 0;

            if (_xmlTemplates.Length > 1)
            {
                lblExcelTemplate.Visible = true;
                cboExcelTemplate.Visible = true;
            }

            chkbWeightColors.Visible = true;

            txtExportFileName.Text = _CAMgr.GetNext_ExportTemps_DicDoc_ReportName(_ProjectName, _AnalysisName, _DocumentName, out _ReportPath);

            _currentReportType = ReportType.ExportDicDoc;

            return true;
        }

        public bool Load_ExportDicDocs(Atebion.ConceptAnalyzer.Analysis CAMgr, DataTable dt, DataTable dtSum, string[] Docs, string ProjectName, string AnalysisName)
        {
            _ErrorMessage = string.Empty;

            _CAMgr = CAMgr;
            _dt = dt;
            _ProjectName = ProjectName;
            _AnalysisName = AnalysisName;
            _dtSum = dtSum;
            _Docs = Docs;

            _TempPath = AppFolders.AppDataPathToolsExcelTempDicDocs;

            //_xmlTemplates = _CAMgr.Get_ExportTemps_DicDocs(out _TempPath, exeTempPath);
            //if (_xmlTemplates.Length == 0)
            //{
            //    _xmlTemplates = _CAMgr.Get_ExportTemps_DicDocs(out _TempPath, exeTempPath);
            //    if (_xmlTemplates.Length == 0)
            //    {
            //        _ErrorMessage = string.Concat("Unable to locate the Excel Dictionary Documents Comparison export template in folder: ", _TempPath);
            //        return false;
            //    }
            //}

            _xmlTemplates = Directory.GetFiles(_TempPath, "*.xml");

            string TemplateName = string.Empty;
            foreach (string template in _xmlTemplates)
            {
                TemplateName = Files.GetFileNameWOExt(template);
                cboExcelTemplate.Items.Add(TemplateName);
            }

            cboExcelTemplate.SelectedIndex = 0;

            if (_xmlTemplates.Length > 1)
            {
                lblExcelTemplate.Visible = true;
                cboExcelTemplate.Visible = true;
            }

            chkbWeightColors.Visible = true;

            _currentReportType = ReportType.ExportDicDocs;

            txtExportFileName.Text = _CAMgr.GetNext_ExportTemps_DicDocs_ReportName(_ProjectName, _AnalysisName, out _ReportPath);

            return true;
        }

        private bool Validate()
        {
            string templateName = this.cboExcelTemplate.Text;

            string reportName = this.txtExportFileName.Text;

            if (templateName == string.Empty)
            {
                MessageBox.Show("Please enter an Export Report Name.", "Report Name Not Defined", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            switch (_currentReportType)
            {
                case ReportType.ConceptsDoc:
                    if (_CAMgr.ReportConceptsDocExists(reportName, _ProjectName, _DocumentName))
                    {
                        MessageBox.Show("Please enter another Report Name.", "Report Name Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    break;

                case ReportType.ConceptsDocs:
                    if (_CAMgr.ReportConceptsDocsExists(reportName, _ProjectName, _AnalysisName))
                    {
                        MessageBox.Show("Please enter another Report Name.", "Report Name Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }

                    break;

                case ReportType.ExportDicDoc:
                    if (_CAMgr.ReportDicsDocExists(reportName, _ProjectName, _AnalysisName, _DocumentName))
                    {
                        MessageBox.Show("Please enter another Report Name.", "Report Name Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }

                    break;

                case ReportType.ExportDicDocs:
                    if (_CAMgr.ReportDicsDocsExists(reportName, _ProjectName, _AnalysisName))
                    {
                        MessageBox.Show("Please enter another Report Name.", "Report Name Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    break;

            }

            return true;
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            if (!Validate())
                return;
            string templateName = this.cboExcelTemplate.Text;
            string resultsFileName = this.txtExportFileName.Text.Trim();

            Cursor.Current = Cursors.WaitCursor; // Waiting

            switch (_currentReportType)
            {
                case ReportType.ConceptsDoc:

                    if (!_CAMgr.ExportConceptsDoc(_dt, templateName, _TempPath, resultsFileName, _ProjectName, _DocumentName))
                    {
                        Cursor.Current = Cursors.Default; // Back to normal
                        MessageBox.Show(_CAMgr.ErrorMessage, "Unable to Create an Excel Export", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    break;

                case ReportType.ConceptsDocs:
                    if (!_CAMgr.ExportConceptsDocs(_dt, _Docs, templateName, _TempPath, resultsFileName, _ProjectName, _AnalysisName, chkbWeightColors.Checked))
                    {
                        Cursor.Current = Cursors.Default; // Back to normal
                        MessageBox.Show(_CAMgr.ErrorMessage, "Unable to Create an Excel Export", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    break;

                case ReportType.ExportDicDoc:
                    if (!_CAMgr.ExportDicDoc(_dt, templateName, _TempPath, resultsFileName, _ProjectName, _AnalysisName, _DocumentName, chkbWeightColors.Checked))
                    {
                        Cursor.Current = Cursors.Default; // Back to normal
                        MessageBox.Show(_CAMgr.ErrorMessage, "Unable to Create an Excel Export", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    break;

                case ReportType.ExportDicDocs:
                    bool weightedcolors = this.chkbWeightColors.Checked;
                    if (!_CAMgr.ExportDicDocs(_dt, _dtSum, _Docs, templateName, _TempPath, resultsFileName, _ProjectName, _AnalysisName, weightedcolors))
                    {
                        Cursor.Current = Cursors.Default; // Back to normal
                        MessageBox.Show(_CAMgr.ErrorMessage, "Unable to Create an Excel Export", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    break;

            }

            Cursor.Current = Cursors.Default; // Back to normal

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
