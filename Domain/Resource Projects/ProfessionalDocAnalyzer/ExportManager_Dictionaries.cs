using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

using Atebion.Common;

namespace ProfessionalDocAnalyzer
{
    class ExportManager_Dictionaries
    {
        private DataTable _dt;
        private DataTable _dtSum;
        private string _ProjectName = string.Empty;
        private string _DocumentName = string.Empty;
        private string _AnalysisName = string.Empty;
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

        private string _ResultsFileName = string.Empty;
        public string ResultsFileName
        {
            get { return _ResultsFileName; }
        }

        public bool Generate_ExportDicDoc(Atebion.ConceptAnalyzer.Analysis CAMgr, DataTable dt, string ProjectName, string AnalysisName, string DocumentName, string TemplateName, bool WeightColors)
        {
            _ErrorMessage = string.Empty;

            _CAMgr = CAMgr;
            _dt = dt;
            _ProjectName = ProjectName;
            _AnalysisName = AnalysisName;
            _DocumentName = DocumentName;

            string exeTempPath = AppFolders.AppDataPathToolsExcelTempDicDoc;

            _xmlTemplates = _CAMgr.Get_ExportTemps_DicDoc(out _TempPath, exeTempPath);
            if (_xmlTemplates.Length == 0)
            {
                _xmlTemplates = _CAMgr.Get_ExportTemps_DicDoc(out _TempPath, exeTempPath); // Try Again
                if (_xmlTemplates.Length == 0)
                {
                    _ErrorMessage = string.Concat("Unable to locate the Excel Dictionary Document export template in folder: ", _TempPath);
                    return false;
                }
            }

            //string TemplateName = string.Empty;
            //foreach (string template in _xmlTemplates)
            //{
            //    TemplateName = Files.GetFileNameWOExt(template);
            //    cboExcelTemplate.Items.Add(TemplateName);
            //}

            //cboExcelTemplate.SelectedIndex = 0;

            //if (_xmlTemplates.Length > 1)
            //{
            //    lblExcelTemplate.Visible = true;
            //    cboExcelTemplate.Visible = true;
            //}

            //chkbWeightColors.Visible = true;

            _ResultsFileName = _CAMgr.GetNext_ExportTemps_DicDoc_ReportName(_ProjectName, _AnalysisName, _DocumentName, out _ReportPath);

           // _currentReportType = ReportType.ExportDicDoc;

            if (!_CAMgr.ExportDicDoc(_dt, TemplateName, _TempPath, _ResultsFileName, _ProjectName, _AnalysisName, _DocumentName, WeightColors))
            {
                Cursor.Current = Cursors.Default; // Back to normal
                _ErrorMessage = _CAMgr.ErrorMessage;
                return false;
            }

            return true;
        }
    }
}
