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
    public class ExportManager
    {
        private string _ExportFileName = string.Empty;
        private string _headerCaptions = string.Empty;
        private string _ColumnWidth = string.Empty;
        private string _TemplateName = string.Empty;

        private string _DocName = string.Empty;
        private DataTable _dt;
        private bool _isUseDefaultParseAnalysis = false;

        RichTextBox rtfExport = new RichTextBox();

     //   private bool _PageExists = false;
        private bool _FoundPageCol = false;

        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        private string _ExportPathFile = string.Empty;
        public string ExportPathFile
        {
            get { return _ExportPathFile; }
        }

        private bool _NoCaption_Use = false;
        public bool NoCaption_Use
        {
            get { return _NoCaption_Use; }
            set { _NoCaption_Use = value; }

        }

        private bool _QualityCheck_Use = false;
        public bool QualityCheck_Use
        {
            get { return _QualityCheck_Use; }
            set { _QualityCheck_Use = value; }
        }

        private bool _LineNos_Use = false;
        public bool LineNos_Use
        {
            get { return _LineNos_Use; }
            set { _LineNos_Use = value; }
        }

        private bool _Number_Use = false;
        public bool Number_Use
        {
            get { return _Number_Use; }
            set { _Number_Use = value; }
        }

        private bool _Caption_Use = false;
        public bool Caption_Use
        {
            get { return _Caption_Use; }
            set { _Caption_Use = value; }
        }

        private bool _Keywords_Use = false;
        public bool Keywords_Use
        {
            get { return _Keywords_Use; }
            set { _Keywords_Use = value; }
        }

        private bool _SegmentText_Use = false;
        public bool SegmentText_Use
        {
            get { return _SegmentText_Use; }
            set { _SegmentText_Use = value; }
        }

        private bool _Note_Use = false;
        public bool Note_Use
        {
            get { return _Note_Use; }
            set { _Note_Use = value; }
        }

        private bool _Page_Use = false;
        public bool Page_Use
        {
            get { return _Page_Use; }
            set { _Page_Use = value; }
        }

        private bool _LongSentences_Use = true;
        public bool LongSentences_Use
        {
            get { return _LongSentences_Use; }
            set { _LongSentences_Use = value; }
        }

        private bool _ComplexWords_Use = true;
        public bool ComplexWords_Use
        {
            get { return _ComplexWords_Use; }
            set { _ComplexWords_Use = value; }

        }

        private bool _PassiveVoice_Use = true;
        public bool PassiveVoice_Use
        {
            get { return _PassiveVoice_Use; }
            set { _PassiveVoice_Use = value; }
        }

        private bool _Adverbs_Use = true;
        public bool Adverbs_Use
        {
            get { return _Adverbs_Use; }
            set { Adverbs_Use = value; }
        }

        private bool _DictionaryTerms_Use = true;
        public bool DictionaryTerms_Use
        {
            get { return _DictionaryTerms_Use; }
            set { _DictionaryTerms_Use = value; }
        }

    

        private bool _FilterRows_All = true;
        public bool FilterRows_All
        {
            get { return _FilterRows_All; }
            set { _FilterRows_All = value; }
        }

        private bool _FilterRows_KeywordsOnly = false;
        public bool FilterRows_KeywordsOnly
        {
            get { return _FilterRows_KeywordsOnly; }
            set { _FilterRows_KeywordsOnly = value; }
        }

        private bool _FilterRows_NotesOnly = false;
        public bool FilterRows_NotesOnly
        {
            get { return _FilterRows_NotesOnly; }
            set { _FilterRows_NotesOnly = value; }
        }

        private bool _FilterRows_NotesAndKeywordsOnly = false;
        public bool FilterRows_NotesAndKeywordsOnly
        {
            get { return _FilterRows_NotesAndKeywordsOnly; }
            set { _FilterRows_NotesAndKeywordsOnly = value; }
        }


        private Modes _ExportType;
        public enum Modes
        {
            Excel = 0,
            Word = 1,
            HTML = 2,
            SharePoint = 3,
            RFP365 = 4,
            QCWord = 5
        }

        public string GetNewRptFileName(string ExportPath)
        {
            string dateTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string BASE_NAME = string.Concat(AppFolders.DocName, "_", dateTime);
            string filename = string.Empty;
            string pathFile = string.Empty;

            for (int i = 1; i < 32000; i++)
            {
                filename = string.Concat(BASE_NAME, i.ToString(), ".html");
                pathFile = Path.Combine(ExportPath, filename);
                if (!File.Exists(pathFile))
                {
                    filename = string.Concat(BASE_NAME, i.ToString(), ".docx");
                    pathFile = Path.Combine(ExportPath, filename);
                    if (!File.Exists(pathFile))
                    {
                        filename = string.Concat(BASE_NAME, i.ToString(), ".xlsx");
                        pathFile = Path.Combine(ExportPath, filename);
                        if (!File.Exists(pathFile))
                        {
                            return string.Concat(BASE_NAME, i.ToString());
                        }
                    }
                }
            }

            return string.Concat(BASE_NAME, "5000"); ;
        }

        public string GetNewRptFileName_Old(string ExportPath)
        {
            string BASE_NAME = string.Concat(AppFolders.DocName, "_");
            string filename = string.Empty;
            string pathFile = string.Empty;

            for (int i = 1; i < 32000; i++)
            {
                filename = string.Concat(BASE_NAME, i.ToString(), ".html");
                pathFile = Path.Combine(ExportPath, filename);
                if (!File.Exists(pathFile))
                {
                    filename = string.Concat(BASE_NAME, i.ToString(), ".docx");
                    pathFile = Path.Combine(ExportPath, filename);
                    if (!File.Exists(pathFile))
                    {
                        filename = string.Concat(BASE_NAME, i.ToString(), ".xlsx");
                        pathFile = Path.Combine(ExportPath, filename);
                        if (!File.Exists(pathFile))
                        {
                            return string.Concat(BASE_NAME, i.ToString());
                        }
                    }
                }
            }

            return string.Concat(BASE_NAME, "5000"); ;
        }

        public bool ExportQCAnalysisResults(string ProjectName, string DocName, DataTable dtQCAnalysisResults, DataSet dsQCIssues, string AnalysisParseSegPath, string ChartPicsPath, string RankAvg, string LSColor, string CWColor, string PVColor, string AColor, string DTColor, string RAvg, string LSTotal, string CWTotal, string PVTotal, string ATotal, string DTTotal)
        {
            _ErrorMessage = string.Empty;

            if (dtQCAnalysisResults == null)
            {
                _ErrorMessage = "The QC Analysis Result dataset is null.";
                return false;
            }

            if (dsQCIssues == null)
            {
                _ErrorMessage = "The QC Issue Found dataset is null.";
                return false;
            }

            // Get and Check Paths
            if (!Directory.Exists(AnalysisParseSegPath))
            {
                _ErrorMessage = string.Concat("Unable to find the Parse Segment folder: ", AnalysisParseSegPath);
                return false;
            }

            string AnalysisParseSegHTMLPath = Path.Combine(AnalysisParseSegPath, "HTML");
            if (!Directory.Exists(AnalysisParseSegHTMLPath))
            {
                try
                {
                    Directory.CreateDirectory(AnalysisParseSegHTMLPath);
                    if (!Directory.Exists(AnalysisParseSegHTMLPath))
                    {
                        _ErrorMessage = string.Concat("Unable to create the Parse Segment HTML folder: ", AnalysisParseSegHTMLPath);
                        return false;
                    }

                }
                catch(Exception ex)
                {
                    _ErrorMessage = string.Concat("Unable to create the Parse Segment HTML folder: ", AnalysisParseSegHTMLPath, Environment.NewLine, Environment.NewLine, "Error: ", ex.Message);
                        return false;
                }

            }

            // Convert Parse Segment RTF files into HTML files
            if (!CovertSegments2HTML(AnalysisParseSegPath, AnalysisParseSegHTMLPath, "10"))
            {
                return false;
            }

            string AnalysisParseSegNotesPath = Path.Combine(AnalysisParseSegPath, "Notes");
            string AnalysisParseSegNotesHTMLPath = Path.Combine(AnalysisParseSegNotesPath, "HTML");
            bool insertNotes = true;
            if (Directory.Exists(AnalysisParseSegNotesPath))
            {
                if (!Directory.Exists(AnalysisParseSegNotesHTMLPath))
                {
                    try
                    {
                        Directory.CreateDirectory(AnalysisParseSegNotesHTMLPath);
                        if (!Directory.Exists(AnalysisParseSegNotesHTMLPath))
                        {
                            insertNotes = false;
                        }
                    }
                    catch
                    {
                        insertNotes = false;
                    }
                }

                if (insertNotes)
                {
                    CovertSegments2HTML(AnalysisParseSegNotesPath, AnalysisParseSegNotesHTMLPath, "12");
                }

            }


            WordOXML word = new WordOXML();

            _headerCaptions = string.Empty;

            DataTable dtEmpty;

            dtEmpty = CreateNewQCDT();

            DataTable dtExport = PopulateQCDataTable(dtQCAnalysisResults, dtEmpty);

            string exportPath = Path.Combine(AnalysisParseSegPath, "Export");
            if (!Directory.Exists(exportPath))
            {
                try
                {
                    Directory.CreateDirectory(exportPath);
                }
                catch (Exception ex)
                {
                    _ErrorMessage = string.Concat("Unable to create Export folder: ", ex.Message, Environment.NewLine, Environment.NewLine, ex.Message);
                    return false;
                }
            }

            string exportFileName = GetNewRptFileName(exportPath);
            _ExportFileName = exportFileName;
            string exportFile = string.Concat(exportFileName, ".docx");

            _ExportPathFile = Path.Combine(exportPath, exportFile);
            

            if (File.Exists(_ExportPathFile))
            {
                _ErrorMessage = "Export file already exists.";

                return false;
            }

            string docName = Files.GetFileName(DocName);

            word.writeQCDocx(
                dtExport, 
                _headerCaptions, 
                _ExportPathFile, 
                docName, 
                ProjectName, 
                AnalysisParseSegHTMLPath,
                AnalysisParseSegNotesHTMLPath,
                _ColumnWidth, 
                ChartPicsPath,
                RankAvg,
                LSColor, 
                CWColor, 
                PVColor, 
                AColor, 
                DTColor, 
                RAvg, 
                LSTotal, 
                CWTotal, 
                PVTotal, 
                ATotal, 
                DTTotal);

            Process.Start(_ExportPathFile);

            return true;
        }

        public bool ExportAnalysisResults(string[] Docs, Modes ExportType, bool isUseDefaultParseAnalysis, string ExcelTemplateName)
        {
            _ErrorMessage = string.Empty;

            foreach (string DocName in Docs)
            {
                AppFolders.DocName = DocName;
                string s = string.Empty;
                string parseSecXMLPath = string.Empty;
                string exportPath = string.Empty;

                if (isUseDefaultParseAnalysis)
                {
                    s = AppFolders.DocParsedSec;
                    parseSecXMLPath = AppFolders.DocParsedSecXML;
                    exportPath = AppFolders.DocParsedSecExport;
                }
                else
                {
                    s = AppFolders.AnalysisParseSeg;
                    parseSecXMLPath = AppFolders.AnalysisXML;
                    exportPath = AppFolders.AnalysisParseSegExport;
                }

                string xmlPathFile = Path.Combine(parseSecXMLPath, "ParseResults.xml");
                if (isUseDefaultParseAnalysis)
                {
                    if (!File.Exists(xmlPathFile))
                    {
                        _ErrorMessage = string.Concat("Unable to find Analysis Results file: ", xmlPathFile);
                        return false;
                    }
                }
                else
                {
                    if (!File.Exists(xmlPathFile))
                    {
                        parseSecXMLPath = AppFolders.AnalysisParseSegXML;
                        xmlPathFile = Path.Combine(parseSecXMLPath, "ParseResults.xml");
                        if (!File.Exists(xmlPathFile))
                        {
                            _ErrorMessage = string.Concat("Unable to find Analysis Results file: ", xmlPathFile);
                            return false;
                        }
                    }
                }


                DataSet ds = Files.LoadDatasetFromXml(xmlPathFile);

                _FoundPageCol = ds.Tables[0].Columns.Contains("Page"); 


                string exportFileName = GetNewRptFileName(exportPath);

                if (!ExportAnalysisResults(_DocName, ds.Tables[0], exportFileName, ExportType, isUseDefaultParseAnalysis, ExcelTemplateName))
                {
                    return false;
                }

            }

            return true;
        }

        public bool ExportAnalysisResults(string DocName, DataTable dt, string exportFileName, Modes ExportType, bool isUseDefaultParseAnalysis, string ExcelTemplateName)
        {
            _DocName = DocName;
            _dt = dt;
            _ExportFileName = exportFileName;
            _TemplateName = ExcelTemplateName;
            _headerCaptions = string.Empty;
            _ColumnWidth = string.Empty;
            _isUseDefaultParseAnalysis = isUseDefaultParseAnalysis;



            _ErrorMessage = string.Empty;

            // -- Validate input parameters
            if (_ExportFileName == string.Empty)
            {
                _ErrorMessage = "An Export File Name is required before Analysis Results can be exported.";
                return false;
            }

            _ExportType = ExportType;
            //if (_ExportType == null)
            //{
            //    _ErrorMessage = "The Export Type has not been defined.";
            //    return false;
            //}



            switch (_ExportType)
            {
                case Modes.Excel:

                    if (isUseDefaultParseAnalysis)
                    {
                        CovertSegments2HTML();
                    }
                    else
                    {
                        CovertSegments2HTML(AppFolders.AnalysisParseSeg, AppFolders.AnalysisParseSegHTML);
                    }

                    //string excelFile = string.Concat(_ExportFileName, ".xlsx");
                    //if (isUseDefaultParseAnalysis)
                    //    _ExportPathFile = Path.Combine(AppFolders.DocParsedSecExport, excelFile);
                    //else
                    //    _ExportPathFile = Path.Combine(AppFolders.AnalysisParseSegExport, excelFile);


                    if (ExcelTemplateName == null || ExcelTemplateName == string.Empty)
                    {
                        if (!ExportToExcel())
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (!Export2ExcelTemplate())
                        {
                            return false;
                        }
                    }

                    break;
                case Modes.Word:
                    // Convert Parsed RTF files into HTML files 

                    //string docxFile = string.Concat(_ExportFileName, ".docx");
                    //if (_isUseDefaultParseAnalysis)
                    ////{
                    //    _ExportPathFile = Path.Combine(AppFolders.DocParsedSecExport, docxFile);
                    //}
                    //else
                    //{
                    //    _ExportPathFile = Path.Combine(AppFolders.AnalysisParseSegExport, docxFile);
                    //}

                    if (isUseDefaultParseAnalysis)
                    {
                        CovertSegments2HTML();
                    }
                    else
                    {
                        CovertSegments2HTML(AppFolders.AnalysisParseSeg, AppFolders.AnalysisParseSegHTML);
                    }

                    if (!ExportToWord())
                    {
                        return false;
                    }

                    break;

                case Modes.HTML:

                    //_ExportPathFile = string.Concat(AppFolders.DocParsedSecExport, @"\", _exportFileName, ".html");
                    //if (!CovertSegments2HTML())
                    //    return false;

                    if (isUseDefaultParseAnalysis)
                    {
                        CovertSegments2HTML();
                    }
                    else
                    {
                        CovertSegments2HTML(AppFolders.AnalysisParseSeg, AppFolders.AnalysisParseSegHTML);
                    }

                    break;
                case Modes.SharePoint:
                    // _ExportPathFile = string.Concat(AppFolders.DocParsedSecExport, @"\", _exportFileName, ".xlsx");
                    //if (FindDuplicatesNumberCaption())
                    //{
                    //    MessageBox.Show("Unable to export for SharePoint, Duplicates found for Number and Caption.", "Duplicates Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //    return false;
                    //}

                    if (isUseDefaultParseAnalysis)
                    {
                        CovertSegments2HTML();
                    }
                    else
                    {
                        CovertSegments2HTML(AppFolders.AnalysisParseSeg, AppFolders.AnalysisParseSegHTML);
                    }

                    break;
                case Modes.RFP365:
                    //   _ExportPathFile = string.Concat(AppFolders.DocParsedSecExport, @"\", _exportFileName, ".xlsx");
                    ExportToExcel();
                    break;

            }

            if (File.Exists(_ExportPathFile))
                Process.Start(_ExportPathFile);

            return true;
        }

        private bool ExportToHTML()
        {

            HTMLGen htmlGen = new HTMLGen();

            DataTable dtEmpty;

            if (_NoCaption_Use) 
                dtEmpty = CreateNewDT_NoCaption(); 
            else
                dtEmpty = CreateNewDT();

            DataTable dtExport = PopulateDataTable(_dt, dtEmpty);

            string exportFile = string.Concat(_ExportFileName, ".html");
            if (_isUseDefaultParseAnalysis)
            {
                _ExportPathFile = Path.Combine(AppFolders.DocParsedSecExport, exportFile);
            }
            else
            {
                _ExportPathFile = Path.Combine(AppFolders.AnalysisParseSegExport, exportFile);
            }

            if (File.Exists(_ExportPathFile))
            {
                _ErrorMessage = "Export file already exists.";
                return false;
            }

            htmlGen.WriteHTML2(dtExport, _headerCaptions, _ColumnWidth, _ExportPathFile, AppFolders.DocName, AppFolders.ProjectName, "Project", "Document");

            return true;

        }

        private bool ExportToWord()
        {
            WordOXML word = new WordOXML();

            _headerCaptions = string.Empty; 

            DataTable dtEmpty;

            if (_NoCaption_Use) 
                dtEmpty = CreateNewDT_NoCaption(); 
            else
                dtEmpty = CreateNewDT();

            DataTable dtExport = PopulateDataTable(_dt, dtEmpty);
            string exportFile = string.Concat(_ExportFileName, ".docx");
            if (_isUseDefaultParseAnalysis)
            {
                _ExportPathFile = Path.Combine(AppFolders.DocParsedSecExport, exportFile);
            }
            else
            {
                _ExportPathFile = Path.Combine(AppFolders.AnalysisParseSegExport, exportFile);
            }

            if (File.Exists(_ExportPathFile))
            {
                _ErrorMessage = "Export file already exists.";

                return false;
            }


            if (_QualityCheck_Use)
            {
                if (_isUseDefaultParseAnalysis)
                {
                    word.writeDocx4(dtExport, _headerCaptions, _ExportPathFile, AppFolders.DocName, AppFolders.ProjectName, AppFolders.DocParsedSecHTML, _ColumnWidth); // Added enbedded HTML parsed segements
                }
                else
                {
                    word.writeDocx4(dtExport, _headerCaptions, _ExportPathFile, AppFolders.DocName, AppFolders.ProjectName, AppFolders.AnalysisParseSegHTML, _ColumnWidth); // Added enbedded HTML parsed segements
                }
            }
            else
            {
                if (_isUseDefaultParseAnalysis)
                {
                    word.writeDocx2(dtExport, _headerCaptions, _ExportPathFile, AppFolders.DocName, AppFolders.ProjectName, AppFolders.DocParsedSecHTML, _ColumnWidth); // Added enbedded HTML parsed segements
                }
                else
                {
                    word.writeDocx2(dtExport, _headerCaptions, _ExportPathFile, AppFolders.DocName, AppFolders.ProjectName, AppFolders.AnalysisParseSegHTML, _ColumnWidth); // Added enbedded HTML parsed segements
                }
            }

            Application.DoEvents();
            word = null;

            return true;

        }


        private bool Export2ExcelTemplate()
        {
            ExcelOutput excelOutPut = new ExcelOutput();

            if (_TemplateName.Length == 0) // Should Never occur, otherwise Bad code
            {
                _ErrorMessage = "The Excel Template has not be defined.";
                return false;
            }

            string excelFile = string.Concat(_ExportFileName, ".xlsx");

            string exportPath = string.Empty;

            if (_isUseDefaultParseAnalysis)
            {
                exportPath = AppFolders.DocParsedSecExport;

                _ExportPathFile = Path.Combine(AppFolders.DocParsedSecExport, excelFile);
            }
            else
            {
                exportPath = AppFolders.AnalysisParseSegExport;

                _ExportPathFile = Path.Combine(AppFolders.AnalysisParseSegExport, excelFile);
            }


            if (File.Exists(_ExportPathFile))
            {
                _ErrorMessage = string.Concat("Export File Already Exists", Environment.NewLine, Environment.NewLine, "Please enter another Export File Name.");

                return false;
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
            excelOutPut.Metadata_DocName = AppFolders.DocName;
            excelOutPut.Metadata_ProjectName = AppFolders.ProjectName;
            excelOutPut.Metadata_YourName = AppFolders.UserName;

            _ExportPathFile = _ExportPathFile.Replace(".xlsx", ""); // excelOutPut.ExportAResults2Template will add ".xlsx" to the end of string

            
            if (!excelOutPut.ExportAResults2Template(dtEmpty, _TemplateName, AppFolders.AppDataPathToolsExcelTempAR, exportPath, _ExportPathFile))
            {
                _ErrorMessage = string.Concat(excelOutPut.ErrorMessage, Environment.NewLine, Environment.NewLine, "Unable to Generate an Excel Export via a Template");

                return false;
            }

            Application.DoEvents();
            excelOutPut = null;

            return true;

        }


        // Export to Excel without using a Template
        private bool ExportToExcel() 
        {

            DataTable dtEmpty;

            if (_NoCaption_Use) 
                dtEmpty = CreateNewDT_NoCaption(); 
            else
                dtEmpty = CreateNewDT();


            DataTable dtExport = PopulateDataTable(_dt, dtEmpty);

            string exportFile = string.Concat(_ExportFileName, ".xlsx");
            if (_isUseDefaultParseAnalysis)
            {
                _ExportPathFile = Path.Combine(AppFolders.DocParsedSecExport, exportFile);
            }
            else
            {
                _ExportPathFile = Path.Combine(AppFolders.AnalysisParseSegExport, exportFile);
            }


            if (File.Exists(_ExportPathFile))
            {
                string exFileMsg = "This file name already exist. Do you want to replace the existing file?";
                if (MessageBox.Show(exFileMsg, "File Already Exist", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    _ErrorMessage = "Parse results were not exported to an Excel file. Please enter another file name.";
                    return false;
                }

                if (Files.FileIsLocked(_ExportPathFile))
                {
                    _ErrorMessage = "Unable to replace the existing exported file because the file is opened by another application.";
                    return false;
                }
            }


            Atebion.Excel.Output.ExcelOutput excelOutput = new ExcelOutput();
            excelOutput.ExportAResults(dtExport, _headerCaptions, _ExportPathFile, _DocName);
            Application.DoEvents();
            excelOutput = null;

            return true;

        }

        private DataTable PopulateQCDataTable(DataTable sourceDT, DataTable exportDT)
        {

            string UID = string.Empty;

            foreach (DataRow rowSource in sourceDT.Rows)
            {
                UID = rowSource["UID"].ToString();

                DataRow rowExport = exportDT.NewRow();

                rowExport["UID"] = UID;

                rowExport["Rank"] = rowSource["Rank"];

                rowExport["Number"] = rowSource["Number"];
                rowExport["Caption"] = rowSource["Caption"];
                if (_Page_Use)
                    rowExport["Page"] = rowSource["Page"];

                rowExport["Readability"] = rowSource["Readability"];

                if (_Adverbs_Use)
                    rowExport["Adverbs"] = rowSource["Adverbs"];

                if (_LongSentences_Use)
                    rowExport["LongSentences"] = rowSource["LongSentences"];

                if (_ComplexWords_Use)
                    rowExport["ComplexWords"] = rowSource["ComplexWords"];

                if (_PassiveVoice_Use)
                    rowExport["PassiveVoice"] = rowSource["PassiveVoice"];

                if (_DictionaryTerms_Use)
                    rowExport["DictionaryTerms"] = rowSource["DictionaryTerms"];

                exportDT.Rows.Add(rowExport);

            }

            return exportDT;
        }

        private DataTable PopulateDataTable(DataTable sourceDT, DataTable exportDT)
        {
            // Use View because user may have split section(s), so the sections won't be in the correct order in the DataTable
            DataView view;
            view = sourceDT.DefaultView;
            view.Sort = "SortOrder ASC";

//_FoundPageCol = sourceDT.Columns.Contains("Page"); 

            bool includeRow = false;
            string noteFile = string.Empty;
            string notePathFile = string.Empty;

            foreach (DataRowView rowSource in view) // Loop thru view rows. 
            {
                string UID = rowSource["UID"].ToString();

                includeRow = false;

                // ---- Check Filter Options ----
                if (_FilterRows_All) // Include all rows
                {
                    includeRow = true;
                }
                else
                {
                    if (_FilterRows_KeywordsOnly) // Included Sections with only with Keywords
                    {
                        if (rowSource["Keywords"].ToString().Trim() != string.Empty)
                            includeRow = true;
                        else
                            includeRow = false;
                    }
                    else if (_FilterRows_NotesOnly) // Included Segments with only with Notes
                    {
                        noteFile = string.Concat(UID, "_1.rtf");
                        if (_isUseDefaultParseAnalysis)
                        {
                            notePathFile = string.Concat(AppFolders.DocParsedSecNotes, noteFile);
                        }
                        else
                        {
                            notePathFile = string.Concat(AppFolders.AnalysisParseSegNotes, noteFile);
                        }
                        if (File.Exists(notePathFile))
                            includeRow = true;
                        else
                            includeRow = false;
                    }
                    else if (_FilterRows_NotesAndKeywordsOnly) // Included Segments with only with both Keywords and Notes
                    {
                        if (rowSource["Keywords"].ToString().Trim() != string.Empty)
                        {
                            includeRow = true;
                        }
                        if (includeRow == false)
                        {
                            noteFile = string.Concat(UID, "_1.rtf");
                            if (_isUseDefaultParseAnalysis)
                            {
                                notePathFile = string.Concat(AppFolders.DocParsedSecNotes, noteFile);
                            }
                            else
                            {
                                notePathFile = string.Concat(AppFolders.AnalysisParseSegNotes, noteFile);
                            }
                            if (File.Exists(noteFile))
                            {
                                includeRow = true;
                            }

                        }
                    }
                }

                if (includeRow)
                {
                    DataRow rowExport = exportDT.NewRow();


                    rowExport["UID"] = UID; 

                    if (_QualityCheck_Use) 
                       rowExport["Quality"] = rowSource["Quality"];

                    if (_LineNos_Use) 
                        rowExport["Lines"] = string.Concat(rowSource["LineStart"].ToString(), "\\", rowSource["LineEnd"].ToString());

                    if (_Number_Use)
                        rowExport["Number"] = rowSource["Number"];

                    if (_Caption_Use)
                        rowExport["Caption"] = rowSource["Caption"];

                    if (_Keywords_Use)
                        rowExport["Keywords"] = rowSource["Keywords"];

                    if (_NoCaption_Use) 
                        rowExport["NoCaption"] = string.Concat(rowSource["Number"], " ", rowSource["Caption"]);

                    if (_SegmentText_Use)
                    {
                        if (_ExportType == Modes.Excel && _TemplateName == string.Empty)
                        {
                            string html_text = getTextHTML(UID); // Content between "<body>" and "</body>" tags
                            rowExport["Text"] = html_text.Trim(); // Content between "<body>" and "</body>" tags
                        }
                        else if (_SegmentText_Use)
                        {
                            string html_text = getTextHTML(UID); // Content between "<body>" and "</body>" tags
                            rowExport["Text"] = html_text.Trim();
                        }
                    }

                    if (_Note_Use)
                    {
                        string notesFile = string.Concat(UID, "_1.rtf");
                        string notesPathFile = string.Empty;
                        if (_isUseDefaultParseAnalysis)
                        {
                            notesPathFile = Path.Combine(AppFolders.DocParsedSecNotes, notesFile);
                        }
                        else
                        {
                            notesPathFile = Path.Combine(AppFolders.AnalysisParseSegNotes, notesFile);
                        }


                        if (File.Exists(notesPathFile))
                        {
                            rtfExport.LoadFile(notesPathFile);
                            rowExport["Notes"] = rtfExport.Text.Trim();
                        }
                    }

                    
                 //   if (_PageExists && foundPageCol)
                    if (_FoundPageCol)
                    {
                        if (_Page_Use)
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

            string uidFile = string.Concat(UID, ".html");
            string uidPathFile = string.Empty;
            if (_isUseDefaultParseAnalysis)
            {
                uidPathFile = Path.Combine(AppFolders.DocParsedSecHTML, uidFile);
            }
            else
            {
                uidPathFile = Path.Combine(AppFolders.AnalysisParseSegHTML, uidFile);
            }

            returnHTML = Files.ReadFile(uidPathFile);

            if (returnHTML.Length > 0) 
            {
                int StartBody = returnHTML.IndexOf("<body>") + 6;
                int EndBody = returnHTML.IndexOf("</body>") - StartBody;

                returnHTML = returnHTML.Substring(StartBody, EndBody);
            }

            return returnHTML;

        }


        private DataTable CreateNewQCDT()
        {
            DataTable table = new DataTable("QCReport");

            table.Columns.Add("UID", typeof(string));
            _headerCaptions = "UID";

            table.Columns.Add("Rank", typeof(string));
            _headerCaptions = " Rank ";
            _ColumnWidth = "2";

            table.Columns.Add("Number", typeof(string));
            _headerCaptions += "| Number ";
            _ColumnWidth += "|2";

            table.Columns.Add("Caption", typeof(string));
            _headerCaptions += "| Caption ";
            _ColumnWidth += "|9";

            //table.Columns.Add("Text", typeof(string));
            //_headerCaptions += "|Text";
            //_ColumnWidth += "|9";

            if (_Page_Use)
            {
                table.Columns.Add("Page", typeof(string));
                _headerCaptions += "| Page ";
                _ColumnWidth += "|2";
            }

            table.Columns.Add("Readability", typeof(double));
            _headerCaptions += "| Readability ";
            _ColumnWidth += "|2";

            if (_LongSentences_Use)
            {
                table.Columns.Add("LongSentences", typeof(int));
                _headerCaptions += "| Long Sentences ";
                _ColumnWidth += "|2";
            }

            if (_ComplexWords_Use)
            {
                table.Columns.Add("ComplexWords", typeof(int));
                _headerCaptions += "|Complex Words";
                _ColumnWidth += "|2";
            }

            if (_PassiveVoice_Use)
            {
                table.Columns.Add("PassiveVoice", typeof(int));
                _headerCaptions += "| Passive Voice ";
                _ColumnWidth += "|2";
            }

            if (_Adverbs_Use)
            {
                table.Columns.Add("Adverbs", typeof(int));
                _headerCaptions += "| Adverbs ";
                _ColumnWidth += "|2";
            }

            if (_DictionaryTerms_Use)
            {
                table.Columns.Add("DictionaryTerms", typeof(int));
                _headerCaptions += "| Dictionary Terms ";
                _ColumnWidth += "|2";
            }


            return table;

        }

        private DataTable CreateNewDT()
        {
            DataTable table = new DataTable();

            table.Columns.Add("UID", typeof(string)); 
            _headerCaptions = "UID";

            if (_QualityCheck_Use) 
            {
                    table.Columns.Add("Quality", typeof(string));
                    _headerCaptions = "Quality Check";
                    _ColumnWidth = "1";
            }

            if (_LineNos_Use) 
            {
                table.Columns.Add("Lines", typeof(string));
                if (_headerCaptions == string.Empty)
                    _headerCaptions = "Lines";
                else
                    _headerCaptions += "|Lines";

                _ColumnWidth = "1";
            }

            if (_Number_Use)
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

            if (_Caption_Use)
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
            if (_Keywords_Use)
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
            if (_SegmentText_Use)
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

                    if (_Note_Use)
                        _ColumnWidth += "|3";
                    else
                        _ColumnWidth += "|6";
                }
            }
            if (_Note_Use)
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
                    if (_SegmentText_Use)
                        _ColumnWidth += "|3";
                    else
                        _ColumnWidth += "|6";
                }
            }

         //   if (_PageExists)
            if (_FoundPageCol)
            {
                if (_Page_Use)
                {
                    table.Columns.Add("Page", typeof(string));
                    _headerCaptions += "|Page";
                    _ColumnWidth += "|3";
                }

            }


            return table;


        }

        private DataTable CreateNewDT_NoCaption() 
        {
            DataTable table = new DataTable();

            table.Columns.Add("UID", typeof(string)); 
            _headerCaptions = "UID";

            bool forSharePoint = false;
            if (_ExportType == Modes.SharePoint)
            {
                forSharePoint = true;
            }

            if (_QualityCheck_Use) 
            {
                table.Columns.Add("Quality", typeof(string));
                _headerCaptions = "Quality Check";
                _ColumnWidth = "1";
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

            if (_Keywords_Use)
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
            if (_SegmentText_Use)
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

                    if (_Note_Use)
                        _ColumnWidth += "|3";
                    else
                        _ColumnWidth += "|6";
                }
            }
            if (_Note_Use)
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
                    if (_SegmentText_Use)
                        _ColumnWidth += "|3";
                    else
                        _ColumnWidth += "|6";
                }
            }


            if (_FoundPageCol)
            {
                if (_Page_Use)
                {
                    _headerCaptions += "|Page";
                    _ColumnWidth += "|3";
                }

            }

            if (forSharePoint)
            {
                if (_LineNos_Use)
                {
                    table.Columns.Add("Lines", typeof(string));
                    _headerCaptions += "|Lines";
                    _ColumnWidth += "|1";
                }
            }
            else
            {
                if (_LineNos_Use)
                {
                    table.Columns.Add("Lines", typeof(string));
                    if (_headerCaptions == string.Empty)
                        _headerCaptions = "Lines";
                    else
                        _headerCaptions += "|Lines";

                    _ColumnWidth = "1";
                }
            }


            return table;
        }
   


        private bool CovertSegments2HTML()
        {
            return CovertSegments2HTML(AppFolders.DocParsedSec, AppFolders.DocParsedSecHTML);

            //// Convert Parsed RTF files into HTML files 
            //AtebionRTFf2HTMLf.Convert convert = new AtebionRTFf2HTMLf.Convert();

            //int qtyConverted = convert.ConvertFiles(AppFolders.DocParsedSec, AppFolders.DocParsedSecHTML);

            //if (convert.ErrorMessage != string.Empty)
            //{
            //    _ErrorMessage = convert.ErrorMessage;
            //    return false;
            //}

            //return false;
        }

        private bool CovertSegments2HTML(string ParseSecPath, string ParseSecHTMLPath)
        {
            //string[] filesRTF = Directory.GetFiles(ParseSecPath, "*.rtf");
            //string[] filesHTML = Directory.GetFiles(ParseSecHTMLPath, "*.html");

            //if (filesHTML.Length == filesRTF.Length) // 
            //    return true;


            // Convert Parsed RTF files into HTML files 
            AtebionRTFf2HTMLf.Convert convert = new AtebionRTFf2HTMLf.Convert();

            int qtyConverted = convert.ConvertFiles(ParseSecPath, ParseSecHTMLPath);

            if (convert.ErrorMessage != string.Empty)
            {
                _ErrorMessage = convert.ErrorMessage;
                return false;
            }


            return true;
        }

        private bool CovertSegments2HTML(string ParseSecPath, string ParseSecHTMLPath, string fontSize)
        {
            if (!CovertSegments2HTML(ParseSecPath, ParseSecHTMLPath))
                return false;

            if (fontSize == string.Empty)
                return true;

            string adjFontSize = string.Concat(fontSize.Trim(), "pt;");

            string[] files = Directory.GetFiles(ParseSecHTMLPath, "*.html");
            foreach (string file in files)
            {
                string text = File.ReadAllText(file);
                if (text.Trim().Length > 0) // Added for empty Notes
                {
                    text = text.Replace("8pt;", adjFontSize);
                    File.WriteAllText(file, text);
                }
            }

            return true;
        }

    }
}
