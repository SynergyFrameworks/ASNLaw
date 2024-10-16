using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using OfficeOpenXml;
using System.Xml;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Diagnostics;
using OfficeOpenXml.Style;

using Atebion.Common;
using Atebion_Dictionary;


namespace Atebion.ConceptAnalyzer
{
    public class ExcelOutput
    {

        #region Properties

        private string _FileNameCaption = string.Empty;
        private string _FileNameRowCol = string.Empty; // Used for Template
        private string _FileNameValue = string.Empty;

        private string _DateCaption = string.Empty;
        private string _DateRowCol = string.Empty; // Used for Template
        private string _DateFormat = string.Empty; // Used for Template
        private DateTime _DateValue;

        private string _FontName = string.Empty;
        private short _FontSize;

        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        private string _Warning = string.Empty;
        public string Warning
        {
            get { return _Warning; }
        }

        // Metadata
        private string _Metadata_Date = string.Empty;
        public string Metadata_Date
        {
            get { return _Metadata_Date; }
            set { _Metadata_Date = value; }
        }

        private string _Metadata_ProjectName = string.Empty;
        public string Metadata_ProjectName
        {
            get { return _Metadata_ProjectName; }
            set { _Metadata_ProjectName = value; }
        }

        private string _Metadata_AnalysisName = string.Empty;
        public string Metadata_AnalysisName
        {
            get { return _Metadata_AnalysisName; }
            set { _Metadata_AnalysisName = value; }
        }

        private string _Metadata_DocName = string.Empty;
        public string Metadata_DocName
        {
            get { return _Metadata_DocName; }
            set { _Metadata_DocName = value; }
        }

        private string _Metadata_YourName = string.Empty;
        public string Metadata_YourName
        {
            get { return _Metadata_YourName; }
            set { _Metadata_YourName = value; }
        }


        public string FileNameCaption
        {
            get { return _FileNameCaption; }
            set { _FileNameCaption = value; }
        }

        public string FileNameRowCol
        {
            get { return _FileNameRowCol; }
            set { _FileNameRowCol = value; }
        }

        public string FileNameValue
        {
            get { return _FileNameValue; }
            set { _FileNameValue = value; }
        }

        public string DateCaption
        {
            get { return _DateCaption; }
            set { _DateCaption = value; }
        }

        public string DateRowCol
        {
            get { return _DateRowCol; }
            set { _DateRowCol = value; }
        }

        public string DateFormat
        {
            get { return _DateFormat; }
            set { _DateFormat = value; }
        }

        public DateTime DateValue
        {
            get { return _DateValue; }
            set { _DateValue = value; }
        }

        public string FontName
        {
            get { return _FontName; }
            set { _FontName = value; }
        }

        public short FontSize
        {
            get { return _FontSize; }
            set { _FontSize = value; }
        }


        #endregion

        private string[] _columns = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };


        private Atebion_Dictionary.Dictionary _DictionaryMgr;

        struct DataFieldColLoc
        {
            public int Number;
            public int Caption;
            public int NoCaption;
            public int PageNo;
            public int SegText;
            public int DicItems;
            public int DicCategory;
            public int DicDefs;
            public int DicWeight;
            public int Concepts;
            public int Notes;
            public int SentText;
        };

        struct MetadataLocations
        {
            public int ProjectNameCol;
            public int ProjectNameRow;
            public int AnalysisNameCol;
            public int AnalysisNameRow;
            public int DocNameCol;
            public int DocNameRow;
            public int DateCol;
            public int DateRow;
            public int YourNameCol;
            public int YourNameRow;

        };

        struct ColorSettings
        {
            public bool AltColorRowsUse;
            public string AltColorRows;
            public bool WholeNoColorUse;
            public string WholeNoColor;

            // ToDo add Seg color for DAR
        }

        //struct DARCOptions
        //{
            // Exclusions options for Sentences
            //public bool Number_Excluded;
            //public bool Caption_Excluded;
            //public bool NoCaption_Excluded;

            //// Segment/Paragraph Background color
            //public bool SegPar_BkgrdColor_Use;
            //public string SegPar_BkgrdColor;
        //}

        private MetadataLocations _MetadataLocations;
        private DataFieldColLoc _DataFieldColLoc;
        private ColorSettings _ColorSettings;
        //private DARCOptions _DARCOptions;

        private int GetColNumber(string s)
        {
            int colNo;

            if (s == string.Empty)
            {
                colNo = -1;
            }
            else
            {
                colNo = Array.IndexOf(_columns, s) + 1;
            }

            return colNo;
        }

        private string GetExcelCell(int rowNo, int columnNo)
        {
            string column = _columns[columnNo];

            string excelCell = string.Concat(column, rowNo.ToString());

            return excelCell;
        }

        private bool GetColRow(string ExcelLocation, out int col, out int row)
        {
            string sCol = string.Empty;

            col = -1;
            row = -1;

            try
            {
                if (ExcelLocation == string.Empty)
                {
                    return true;
                }
                else
                {
                    if (ExcelLocation.Length == 2)
                    {
                        sCol = ExcelLocation.Substring(0, 1);
                        col = GetColNumber(sCol);
                        row = Convert.ToInt32(ExcelLocation.Substring(1, 1));
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool Populate_ColorDef(DataRow rowSettings)
        {
            try
            {
                _ColorSettings.AltColorRowsUse = (bool)rowSettings[ExcelTemplateFields.AltColorRowsUse];
                _ColorSettings.AltColorRows = rowSettings[ExcelTemplateFields.AltColorRowsUse].ToString();

                _ColorSettings.WholeNoColorUse = (bool)rowSettings[ExcelTemplateFields.WholeNoColorUse];
                _ColorSettings.WholeNoColor = rowSettings[ExcelTemplateFields.WholeNoColor].ToString();
            }
            catch (Exception ex)
            {
                _ColorSettings.AltColorRowsUse = false;
                _ColorSettings.AltColorRows = string.Empty;

                _ColorSettings.WholeNoColorUse = false;
                _ColorSettings.WholeNoColor = string.Empty;

                return false;
            }

            return true;
        }



        private bool Populate_MetadataLocations(DataRow rowSettings)
        {
            int col = -1;
            int row = -1;

            string sCol = string.Empty;

            // Date
            if (!GetColRow(rowSettings[ExcelTemplateFields.LocDate].ToString(), out col, out row))
            {
                _ErrorMessage = "Excel Template Column 'Date' was not well formed. -- Fix selected Excel Template under Tools/Settings/Excel Templates.";
                return false;
            }

            _MetadataLocations.DateCol = col;
            _MetadataLocations.DateRow = row;


            // Document Name
            try
            {
                if (!GetColRow(rowSettings[ExcelTemplateFields.LocDocName].ToString(), out col, out row))
                {
                    _ErrorMessage = "Excel Template Column 'Document Name' was not well formed. -- Fix selected Excel Template under Tools/Settings/Excel Templates.";
                    return false;
                }

                _MetadataLocations.DocNameCol = col;
                _MetadataLocations.DocNameRow = row;
            }
            catch
            {

            }


            // Project Name
            if (!GetColRow(rowSettings[ExcelTemplateFields.LocProjectName].ToString(), out col, out row))
            {
                _ErrorMessage = "Excel Template Column 'Project Name' was not well formed. -- Fix selected Excel Template under Tools/Settings/Excel Templates.";
                return false;
            }

            _MetadataLocations.ProjectNameCol = col;
            _MetadataLocations.ProjectNameRow = row;


            // Analysis Name
            //try
            //{
            //    if (!GetColRow(rowSettings[ExcelTemplateFields.LocAnalysisName].ToString(), out col, out row))
            //    {
            //        _ErrorMessage = "Excel Template Column 'Project Name' was not well formed. -- Fix selected Excel Template under Tools/Settings/Excel Templates.";
            //        return false;
            //    }

            //    _MetadataLocations.AnalysisNameCol = col;
            //    _MetadataLocations.AnalysisNameRow = row;
            //}
            //catch
            //{
                
            //}


            // Your Name
            if (!GetColRow(rowSettings[ExcelTemplateFields.LocYourName].ToString(), out col, out row))
            {
                _ErrorMessage = "Excel Template Column 'Your Name' was not well formed. -- Fix selected Excel Template under Tools/Settings/Excel Templates.";
                return false;
            }

            _MetadataLocations.YourNameCol = col;
            _MetadataLocations.YourNameRow = row;

            return true;

        }

 

        private void Populate_DataFieldColLoc(DataRow rowSettings) // ToDo addmore fields
        {
            // Number
            if (rowSettings.Table.Columns.Contains(ExcelTemplateFields.LocNumber))
            {
                _DataFieldColLoc.Number = GetColNumber(rowSettings[ExcelTemplateFields.LocNumber].ToString());
            }
            else
            {
                _DataFieldColLoc.Number = -1;
            }

            // Cation
            if (rowSettings.Table.Columns.Contains(ExcelTemplateFields.LocCaption))
            {
                _DataFieldColLoc.Caption = GetColNumber(rowSettings[ExcelTemplateFields.LocCaption].ToString());
            }
            else
            {
                _DataFieldColLoc.Caption = -1;
            }

            // Number & Caption
            if (rowSettings.Table.Columns.Contains(ExcelTemplateFields.LocNoCaption))
            {
                _DataFieldColLoc.NoCaption = GetColNumber(rowSettings[ExcelTemplateFields.LocNoCaption].ToString());
            }
            else
            {
                _DataFieldColLoc.NoCaption = -1;
            }

            // Page Number
            if (rowSettings.Table.Columns.Contains(ExcelTemplateFields.LocPage))
            {
                _DataFieldColLoc.PageNo = GetColNumber(rowSettings[ExcelTemplateFields.LocPage].ToString());
            }
            else
            {
                _DataFieldColLoc.PageNo = -1;
            }

            // Concepts
            if (rowSettings.Table.Columns.Contains(ExcelTemplateFields.LocConcepts))
            {
                _DataFieldColLoc.Concepts = GetColNumber(rowSettings[ExcelTemplateFields.LocConcepts].ToString());
            }
            else
            {
                _DataFieldColLoc.Concepts = -1;
            }

            // Dictionary Items
            if (rowSettings.Table.Columns.Contains(ExcelTemplateFields.LocDics))
            {
                _DataFieldColLoc.DicItems = GetColNumber(rowSettings[ExcelTemplateFields.LocDics].ToString());
            }
            else
            {
                _DataFieldColLoc.DicItems = -1;
            }

            // Dictionary Definitions
            if (rowSettings.Table.Columns.Contains(ExcelTemplateFields.LocDicDefs))
            {
                _DataFieldColLoc.DicDefs = GetColNumber(rowSettings[ExcelTemplateFields.LocDicDefs].ToString());
            }
            else
            {
                _DataFieldColLoc.DicDefs = -1;
            }

            // Dictionary Category   
            if (rowSettings.Table.Columns.Contains(ExcelTemplateFields.LocDicCat))
            {
                _DataFieldColLoc.DicCategory = GetColNumber(rowSettings[ExcelTemplateFields.LocDicCat].ToString());
            }
            else
            {
                _DataFieldColLoc.DicCategory = -1;
            }

            // Dictionary Weights
            if (rowSettings.Table.Columns.Contains(ExcelTemplateFields.LocDicWeight))
            {
                _DataFieldColLoc.DicWeight = GetColNumber(rowSettings[ExcelTemplateFields.LocDicWeight].ToString());
            }
            else
            {
                _DataFieldColLoc.DicWeight = -1;
            }

            // Notes
            if (rowSettings.Table.Columns.Contains(ExcelTemplateFields.LocNotes))
            {
                _DataFieldColLoc.Notes = GetColNumber(rowSettings[ExcelTemplateFields.LocNotes].ToString());
            }
            else
            {
                _DataFieldColLoc.Notes = -1;
            }

            // Text
            if (rowSettings.Table.Columns.Contains(ExcelTemplateFields.LocSegText))
            {
                _DataFieldColLoc.SegText = GetColNumber(rowSettings[ExcelTemplateFields.LocSegText].ToString());
            }
            else
            {
                _DataFieldColLoc.SegText = -1;
            }


            
        }

        private bool CopyTemplate3ExportFile(string TemplateName, string resultFileName, string TempatePath, string ExportPath, out string ExportFile)
        {
            _ErrorMessage = string.Empty;

            string fileTemplate = string.Concat(TemplateName, ".xlsx");
            string fileAR = string.Concat(resultFileName, ".xlsx");

            string TemplateFile = Path.Combine(TempatePath, fileTemplate);
            ExportFile = Path.Combine(ExportPath, fileAR);

            // Replace if Exported file exists ...
            try
            {
                if (File.Exists(ExportFile))
                {
                    File.Delete(ExportFile);
                }
            }
            catch (Exception ex1)
            {
                _ErrorMessage = "Export file already exists and cannot be replaced.";
                return false;
            }

            try
            {
                // Copy Template to Export
                File.Copy(TemplateFile, ExportFile);
            }
            catch (Exception ex2)
            {
                _ErrorMessage = ex2.Message;
                return false;
            }

            return true;

        }


 

        public bool ExportConceptsDoc(DataTable dt, string TemplateName, string TempatePath, string ExportPath, string resultFileName)
        {
            _ErrorMessage = string.Empty;

            // Get Settings
            DataRow rowSettings = GetSettings(TemplateName, TempatePath);

            if (rowSettings == null)
                return false;

            Populate_DataFieldColLoc(rowSettings);

            Populate_MetadataLocations(rowSettings);

            string exportFile = string.Empty;
            if (!CopyTemplate3ExportFile(TemplateName, resultFileName, TempatePath, ExportPath, out exportFile))
            {
                return false;
            }

            FileInfo testTemplate = new FileInfo(exportFile);

            string sheetName = rowSettings[ExcelTemplateFields.SheetName].ToString();

            // Create Excel EPPlus Package based on template stream
            using (ExcelPackage package = new ExcelPackage(testTemplate))
            {
                // Grab the sheet with the template.
                ExcelWorksheet sheet = package.Workbook.Worksheets[sheetName];

                if (sheet == null)
                {
                    _ErrorMessage = string.Concat("Excel Template Sheet '", sheetName, "' was not found.");
                    return false;
                }

                // Populate Header Fields
                if (rowSettings[ExcelTemplateFields.LocDate].ToString().Length > 0)
                {
                    sheet.Cells[_MetadataLocations.DateRow, _MetadataLocations.DateCol].Value = _Metadata_Date;
                }
                if (rowSettings[ExcelTemplateFields.LocDocName].ToString().Length > 0)
                {
                    sheet.Cells[_MetadataLocations.DocNameRow, _MetadataLocations.DocNameCol].Value = _Metadata_DocName;
                }
                if (rowSettings[ExcelTemplateFields.LocProjectName].ToString().Length > 0)
                {
                    sheet.Cells[_MetadataLocations.ProjectNameRow, _MetadataLocations.ProjectNameCol].Value = _Metadata_ProjectName;
                }
                if (rowSettings[ExcelTemplateFields.LocYourName].ToString().Length > 0)
                {
                    sheet.Cells[_MetadataLocations.YourNameRow, _MetadataLocations.YourNameCol].Value = _Metadata_YourName;
                }
                //if (rowSettings[ExcelTemplateFields.LocAnalysisName].ToString().Length > 0)
                //{
                //    sheet.Cells[_MetadataLocations.AnalysisNameRow, _MetadataLocations.AnalysisNameCol].Value = _Metadata_AnalysisName;
                //}


                // Populate Excel Data Cells/Rows
                int dataRowStart = Convert.ToInt32(rowSettings[ExcelTemplateFields.DataRowStart].ToString());

                int i = dataRowStart; // row incremental 
  
                foreach (DataRow row in dt.Rows)
                {
                    // Concepts
                    if (_DataFieldColLoc.Concepts != -1)
                    {
                        sheet.Cells[i, _DataFieldColLoc.Concepts].Value = row[ReportConceptsDocFields.Concepts].ToString();
                    }
                    // Page Numbers
                    if (_DataFieldColLoc.PageNo != -1)
                    {
                        sheet.Cells[i, _DataFieldColLoc.PageNo].Value = row["Page"].ToString();
                    }
                    // Number
                    if (_DataFieldColLoc.Number != -1)
                    {
                        sheet.Cells[i, _DataFieldColLoc.Number].Value = row[ReportConceptsDocFields.Number].ToString();
                    }
                    // Caption
                    if (_DataFieldColLoc.Caption != -1)
                    {
                        sheet.Cells[i, _DataFieldColLoc.Caption].Value = row[ReportConceptsDocFields.Caption].ToString();
                    }
                    // Caption & Number
                    if (_DataFieldColLoc.NoCaption != -1)
                    {
                        sheet.Cells[i, _DataFieldColLoc.NoCaption].Value = row[ReportConceptsDocFields.NoCaption].ToString();
                    }
                    // Segment/Paragraph Text
                    if (_DataFieldColLoc.SegText != -1)
                    {
                        ExcelRange range = sheet.Cells[i, _DataFieldColLoc.SegText];

                        // Convert HTML text to Excel with Highlights converted to Red Bold font
                        SetRichTextFromHtml(range, row[ReportConceptsDocFields.Text].ToString(), rowSettings[ExcelTemplateFields.FontName].ToString(), short.Parse(rowSettings[ExcelTemplateFields.FontSize].ToString()));
                    }

                    // Add Notes
                    if (_DataFieldColLoc.Notes != -1)
                    {
                        sheet.Cells[i, _DataFieldColLoc.Notes].Value = row[ReportConceptsDocFields.Notes].ToString();
                    }
                    else if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() != NotesEnbedded.None) // Enbedded Notes
                    {
                        string note = row[ReportConceptsDocFields.Notes].ToString();
                        if (note.Length > 0)
                        {
                            int col = -1;
                            if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() == NotesEnbedded.Caption)
                            {
                                col = _DataFieldColLoc.Caption;
                            }
                            if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() == NotesEnbedded.NoCaption)
                            {
                                col = _DataFieldColLoc.NoCaption;
                            }
                            if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() == NotesEnbedded.Number)
                            {
                                col = _DataFieldColLoc.Number;
                            }
                            if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() == NotesEnbedded.SegPar)
                            {
                                col = _DataFieldColLoc.SegText;
                            }

                            if (col != -1)
                            {
                                sheet.Cells[i, col].AddComment(note, _Metadata_YourName);
                                sheet.Cells[i, col].Comment.AutoFit = true;
                            }
                        }
                    } // Notes < --

                    sheet.Row(i).CustomHeight = false; // test auto-height

                    i++; // row number

                } // Data Row

                package.Save();

                Process.Start(exportFile);
            }

            return true;
        }

        private string getNotesText(string path, string uid)
        {
            if (!Directory.Exists(path))
                return string.Empty;

            string file = string.Concat(uid.Trim(), ".rtf");
            string pathFile = Path.Combine(path, file);

            if (File.Exists(pathFile))
            {

                return Files.ReadRTFFile(pathFile);
            }

            return string.Empty;

        }

        public bool ExportConceptsDocs(DataTable dt, string[] Docs, string TemplateName, string TempatePath, string ExportPath, string resultFileName, bool ColorQty)
        {
            _ErrorMessage = string.Empty;

            // Get Settings
            DataRow rowSettings = GetSettings(TemplateName, TempatePath);

            if (rowSettings == null)
                return false;

            Populate_DataFieldColLoc(rowSettings); // ToDo

            Populate_MetadataLocations(rowSettings);

            string exportFile = string.Empty;
            if (!CopyTemplate3ExportFile(TemplateName, resultFileName, TempatePath, ExportPath, out exportFile))
            {
                return false;
            }

            FileInfo testTemplate = new FileInfo(exportFile);

            string sheetName = rowSettings[ExcelTemplateFields.SheetName].ToString();

            // Create Excel EPPlus Package based on template stream
            using (ExcelPackage package = new ExcelPackage(testTemplate))
            {
                // Grab the sheet with the template.
                ExcelWorksheet sheet = package.Workbook.Worksheets[sheetName];

                if (sheet == null)
                {
                    _ErrorMessage = string.Concat("Excel Template Sheet '", sheetName, "' was not found.");
                    return false;
                }

                // Insert Document Headers
                if (rowSettings[ExcelTemplateFields.HeaderRowStart].ToString().Length == 0)
                {
                    _ErrorMessage = "Header Documents Row is not defined.";
                    return false;
                }
                if (rowSettings[ExcelTemplateFields.Header1stColStart].ToString().Length == 0)
                {
                    _ErrorMessage = "Header Documents Columns are not defined.";
                    return false;
                }

                int y = 0;
                if (DataFunctions.IsNumeric(rowSettings[ExcelTemplateFields.Header1stColStart].ToString()))
                {
                    y = Convert.ToInt32(rowSettings[ExcelTemplateFields.Header1stColStart].ToString());
                }
                else
                {
                    y = GetColNumber(rowSettings[ExcelTemplateFields.Header1stColStart].ToString());
                }
                int firstDocCol = y;
                int x = Convert.ToInt32(rowSettings[ExcelTemplateFields.HeaderRowStart].ToString());

                int docsCount = Docs.Length;
                if (docsCount == 0)
                {
                    _ErrorMessage = "Documents are not defined.";
                    return false;
                }

                string docColName = string.Empty;

                foreach (string docName in Docs)
                {
                    sheet.Cells[x, y].Value = docName;

                    y++;
                }

                int notesCol = y;
                sheet.Cells[x, notesCol].Value = "Notes";


                // Populate Header Fields
                if (rowSettings[ExcelTemplateFields.LocDate].ToString().Length > 0)
                {
                    sheet.Cells[_MetadataLocations.DateRow, _MetadataLocations.DateCol].Value = _Metadata_Date;
                }
                if (rowSettings[ExcelTemplateFields.LocProjectName].ToString().Length > 0)
                {
                    sheet.Cells[_MetadataLocations.ProjectNameRow, _MetadataLocations.ProjectNameCol].Value = _Metadata_ProjectName;
                }
                if (rowSettings[ExcelTemplateFields.LocYourName].ToString().Length > 0)
                {
                    sheet.Cells[_MetadataLocations.YourNameRow, _MetadataLocations.YourNameCol].Value = _Metadata_YourName;
                }
                //if (rowSettings[ExcelTemplateFields.LocAnalysisName].ToString().Length > 0)
                //{
                //    sheet.Cells[_MetadataLocations.AnalysisNameRow, _MetadataLocations.AnalysisNameCol].Value = _Metadata_AnalysisName;
                //}


                // Populate Excel Data Cells/Rows
                int dataRowStart = Convert.ToInt32(rowSettings[ExcelTemplateFields.DataRowStart].ToString());

                int i = dataRowStart; // row incremental 
                string note = string.Empty;
                string uid = string.Empty;

                int qty = 0;

                foreach (DataRow row in dt.Rows)
                {
                    uid = row[ReportConceptsDocFields.UID].ToString();

                    // Concepts
                    if (_DataFieldColLoc.Concepts != -1)
                    {      
                        sheet.Cells[i, _DataFieldColLoc.Concepts].Value = row["Concept"].ToString();
                    }

                    sheet.Cells[i, notesCol].Value = row[ConceptsResultsFields.Notes].ToString(); // Insert notes

                    y = firstDocCol; // 1st Document Count Column Location
                    for (int yy = 0; yy < docsCount; yy++) // Loop Document Count columns, e.g. Count_0, Count_1, Count_2
                    {
                        docColName = string.Concat(ReportConceptsDocFields.Count, yy.ToString());
                        DataColumnCollection columns = dt.Columns;
                        if (!columns.Contains(docColName))
                        {
                            continue;
                        }
                        if (row[string.Concat(ReportConceptsDocFields.Count, yy.ToString())].ToString() == string.Empty)
                            qty = 0;
                        else
                            qty = Convert.ToInt32((row[string.Concat(ReportConceptsDocFields.Count, yy.ToString())].ToString()));

                       // sheet.Cells[i, y].Value = row[string.Concat(ReportConceptsDocFields.Count, yy.ToString())].ToString();
                        sheet.Cells[i, y].Value = qty;

                        if (ColorQty)
                        {
                            sheet.Cells[i, y].Style.Fill.PatternType = ExcelFillStyle.Solid;

                            if (qty == 0)
                                sheet.Cells[i, y].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                            else if (qty < 10)
                                sheet.Cells[i, y].Style.Fill.BackgroundColor.SetColor(Color.GreenYellow);
                            else if (qty > 9 && qty < 21)
                                sheet.Cells[i, y].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
                            else if (qty > 20 && qty < 41)
                            {
                                sheet.Cells[i, y].Style.Font.Color.SetColor(Color.White);
                                sheet.Cells[i, y].Style.Fill.BackgroundColor.SetColor(Color.Green);
                            }
                            else if (qty >= 41)
                            {
                                sheet.Cells[i, y].Style.Font.Color.SetColor(Color.White);
                                sheet.Cells[i, y].Style.Fill.BackgroundColor.SetColor(Color.DarkGreen);
                            }
                        }

                        y++;
                    }


                    i++;
                } // Rows

                package.Save();


                Process.Start(exportFile);

            }

            return true;
        }

        public bool ExportDicDoc(DataTable dt, string TemplateName, string TempatePath, string ExportPath, string resultFileName, bool UseColor)
        {
            _ErrorMessage = string.Empty;

            // Get Settings
            DataRow rowSettings = GetSettings(TemplateName, TempatePath);

            if (rowSettings == null)
                return false;

            Populate_DataFieldColLoc(rowSettings);

            Populate_MetadataLocations(rowSettings);
     

            string exportFile = string.Empty;
            if (!CopyTemplate3ExportFile(TemplateName, resultFileName, TempatePath, ExportPath, out exportFile))
            {
                return false;
            }

            FileInfo testTemplate = new FileInfo(exportFile);

            string sheetName = rowSettings[ExcelTemplateFields.SheetName].ToString();

            // Create Excel EPPlus Package based on template stream
            using (ExcelPackage package = new ExcelPackage(testTemplate))
            {
                // Grab the sheet with the template.
                ExcelWorksheet sheet = package.Workbook.Worksheets[sheetName];

                if (sheet == null)
                {
                    _ErrorMessage = string.Concat("Excel Template Sheet '", sheetName, "' was not found.");
                    return false;
                }

                // Populate Header Fields
                if (rowSettings[ExcelTemplateFields.LocDate].ToString().Length > 0)
                {
                    sheet.Cells[_MetadataLocations.DateRow, _MetadataLocations.DateCol].Value = _Metadata_Date;
                }
                if (rowSettings[ExcelTemplateFields.LocDocName].ToString().Length > 0)
                {
                    sheet.Cells[_MetadataLocations.DocNameRow, _MetadataLocations.DocNameCol].Value = _Metadata_DocName;
                }
                if (rowSettings[ExcelTemplateFields.LocProjectName].ToString().Length > 0)
                {
                    sheet.Cells[_MetadataLocations.ProjectNameRow, _MetadataLocations.ProjectNameCol].Value = _Metadata_ProjectName;
                }
                if (rowSettings[ExcelTemplateFields.LocYourName].ToString().Length > 0)
                {
                    sheet.Cells[_MetadataLocations.YourNameRow, _MetadataLocations.YourNameCol].Value = _Metadata_YourName;
                }
                //if (rowSettings[ExcelTemplateFields.LocAnalysisName].ToString().Length > 0)
                //{
                //    sheet.Cells[_MetadataLocations.AnalysisNameRow, _MetadataLocations.AnalysisNameCol].Value = _Metadata_AnalysisName;
                //}


                // Populate Excel Data Cells/Rows
                int dataRowStart = Convert.ToInt32(rowSettings[ExcelTemplateFields.DataRowStart].ToString());

                int i = dataRowStart; // row incremental 

                double weight = 0;
                bool grayBackground = false;

                foreach (DataRow row in dt.Rows)
                {
                    if (row[ReportDicDocFields.DicItems].ToString().Trim() == string.Empty)
                    {
                        grayBackground = true;
                    }
                    else
                    {
                        grayBackground = false;
                    }

                    // Caption
                    if (_DataFieldColLoc.Caption != -1)
                    {
                        sheet.Cells[i, _DataFieldColLoc.Caption].Value = row[ReportDicDocFields.Caption].ToString();
                       
                        if (grayBackground && UseColor)
                        {
                            sheet.Cells[i, _DataFieldColLoc.Caption].Style.Fill.PatternType = ExcelFillStyle.Solid;

                            sheet.Cells[i, _DataFieldColLoc.Caption].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        }
                       
                    }
                    // Dic Items
                    if (_DataFieldColLoc.DicItems != -1)
                    {
                        sheet.Cells[i, _DataFieldColLoc.DicItems].Value = row[ReportDicDocFields.DicItems].ToString();

                        if (grayBackground && UseColor)
                        {
                            sheet.Cells[i, _DataFieldColLoc.DicItems].Style.Fill.PatternType = ExcelFillStyle.Solid;

                            sheet.Cells[i, _DataFieldColLoc.DicItems].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        }
                    }
                    // Dic Def.s
                    if (_DataFieldColLoc.DicDefs != -1)
                    {
                        sheet.Cells[i, _DataFieldColLoc.DicDefs].Value = row[ReportDicDocFields.DicDefs].ToString();

                        if (grayBackground && UseColor)
                        {
                            sheet.Cells[i, _DataFieldColLoc.DicDefs].Style.Fill.PatternType = ExcelFillStyle.Solid;

                            sheet.Cells[i, _DataFieldColLoc.DicDefs].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        }
                    }
                    // Dic Weight
                    if (_DataFieldColLoc.DicWeight != -1)
                    {
                        if (row[ReportDicDocFields.WeightDoc].ToString() == string.Empty)
                        {
                            weight = 0;
                            if (grayBackground && UseColor)
                            {
                                sheet.Cells[i, _DataFieldColLoc.DicWeight].Style.Fill.PatternType = ExcelFillStyle.Solid;

                                sheet.Cells[i, _DataFieldColLoc.DicWeight].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                            }
                        }
                        else
                        {
                            weight = Convert.ToDouble(row[ReportDicDocFields.WeightDoc].ToString());
                        }

                        sheet.Cells[i, _DataFieldColLoc.DicWeight].Value = weight;

                        if (UseColor)
                        {
                            sheet.Cells[i, _DataFieldColLoc.DicWeight].Style.Fill.PatternType = ExcelFillStyle.Solid;

                            if (weight == 0)
                                sheet.Cells[i, _DataFieldColLoc.DicWeight].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                            else if (weight > 0 && weight < 1)
                                sheet.Cells[i, _DataFieldColLoc.DicWeight].Style.Fill.BackgroundColor.SetColor(Color.GreenYellow);
                            else if (weight > .99 && weight < 3)
                                sheet.Cells[i, _DataFieldColLoc.DicWeight].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
                            else if (weight > 2.99 && weight < 5)
                            {
                                sheet.Cells[i, _DataFieldColLoc.DicWeight].Style.Font.Color.SetColor(Color.White);
                                sheet.Cells[i, _DataFieldColLoc.DicWeight].Style.Fill.BackgroundColor.SetColor(Color.Green);
                            }
                            else if (weight >= 5)
                            {
                                sheet.Cells[i, _DataFieldColLoc.DicWeight].Style.Font.Color.SetColor(Color.White);
                                sheet.Cells[i, _DataFieldColLoc.DicWeight].Style.Fill.BackgroundColor.SetColor(Color.DarkGreen);
                            }
                        }
                    }
                    // Page Numbers
                    if (_DataFieldColLoc.PageNo != -1)
                    {
                        sheet.Cells[i, _DataFieldColLoc.PageNo].Value = row["PageNo"].ToString();

                        if (grayBackground && UseColor)
                        {
                            sheet.Cells[i, _DataFieldColLoc.PageNo].Style.Fill.PatternType = ExcelFillStyle.Solid;

                            sheet.Cells[i, _DataFieldColLoc.PageNo].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        }
                    }
                    // Number
                    if (_DataFieldColLoc.Number != -1)
                    {
                        sheet.Cells[i, _DataFieldColLoc.Number].Value = row[ReportDicDocFields.Number].ToString();

                        if (grayBackground && UseColor)
                        {
                            sheet.Cells[i, _DataFieldColLoc.Number].Style.Fill.PatternType = ExcelFillStyle.Solid;

                            sheet.Cells[i, _DataFieldColLoc.Number].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        }
                    }
                    // Caption & Number
                    if (_DataFieldColLoc.NoCaption != -1)
                    {
                        sheet.Cells[i, _DataFieldColLoc.NoCaption].Value = row[ReportDicDocFields.NoCaption].ToString();

                        if (grayBackground && UseColor)
                        {
                            sheet.Cells[i, _DataFieldColLoc.NoCaption].Style.Fill.PatternType = ExcelFillStyle.Solid;

                            sheet.Cells[i, _DataFieldColLoc.NoCaption].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        }
                    }
                    // Segment/Paragraph Text
                    if (_DataFieldColLoc.SegText != -1)
                    {
                        ExcelRange range = sheet.Cells[i, _DataFieldColLoc.SegText];

                        // Convert HTML text to Excel with Highlights converted to Red Bold font
                        SetRichTextFromHtml(range, row[ReportDicDocFields.Text].ToString().Trim(), rowSettings[ExcelTemplateFields.FontName].ToString(), short.Parse(rowSettings[ExcelTemplateFields.FontSize].ToString()));

                        if (grayBackground && UseColor)
                        {
                            sheet.Cells[i, _DataFieldColLoc.SegText].Style.Fill.PatternType = ExcelFillStyle.Solid;

                            sheet.Cells[i, _DataFieldColLoc.SegText].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        }
                    }

                    // Add Notes
                    if (_DataFieldColLoc.Notes != -1)
                    {
                        sheet.Cells[i, _DataFieldColLoc.Notes].Value = row[ReportDicDocFields.Notes].ToString();

                        if (grayBackground && UseColor)
                        {
                            sheet.Cells[i, _DataFieldColLoc.Notes].Style.Fill.PatternType = ExcelFillStyle.Solid;

                            sheet.Cells[i, _DataFieldColLoc.Notes].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        }
                    }
                    else if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() != NotesEnbedded.None) // Enbedded Notes
                    {
                        string note = row[ReportDicDocFields.Notes].ToString();
                        if (note.Length > 0)
                        {
                            int col = -1;
                            if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() == NotesEnbedded.Caption)
                            {
                                col = _DataFieldColLoc.Caption;
                            }
                            if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() == NotesEnbedded.NoCaption)
                            {
                                col = _DataFieldColLoc.NoCaption;
                            }
                            if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() == NotesEnbedded.Number)
                            {
                                col = _DataFieldColLoc.Number;
                            }
                            if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() == NotesEnbedded.SegPar)
                            {
                                col = _DataFieldColLoc.SegText;
                            }

                            if (col != -1)
                            {
                                sheet.Cells[i, col].AddComment(note, _Metadata_YourName);
                                sheet.Cells[i, col].Comment.AutoFit = true;
                            }
                        }
                    } // Notes < --

                    sheet.Row(i).CustomHeight = false; // test auto-height

                    i++; // row number

                }

                package.Save();


                Process.Start(exportFile);

            }

            return true;
        }

        public bool ExportDicDocRAM(DataTable dt, DataTable dtDicAnalysis, DataSet dsRAMDef, string DictionaryPath, string DictionaryName, string TemplateName, string TempatePath, string ExportPath, string resultFileName, bool UseColor)
        {
            _ErrorMessage = string.Empty;

            _DictionaryMgr = new Dictionary(DictionaryPath, DictionaryName);
            if (_DictionaryMgr == null)
            {
                _ErrorMessage = _DictionaryMgr.ErrorMessage;
                return false;
            }

            DataSet dsNotUsed = _DictionaryMgr.GetDataset(); // Activate Dataset in the Dictionary Manager
            if (dsNotUsed == null) // Check if activation worked
            {
                _ErrorMessage = _DictionaryMgr.ErrorMessage;
                return false;
            }

            // Get Settings
            DataRow rowSettings = GetSettings(TemplateName, TempatePath);

            if (rowSettings == null)
                return false;

            Populate_DataFieldColLoc(rowSettings);

            Populate_MetadataLocations(rowSettings);

            string exportFile = string.Empty;
            if (!CopyTemplate3ExportFile(TemplateName, resultFileName, TempatePath, ExportPath, out exportFile))
            {
                return false;
            }

            FileInfo testTemplate = new FileInfo(exportFile);

            string sheetName = rowSettings[ExcelTemplateFields.SheetName].ToString();

            // Create Excel EPPlus Package based on template stream
            using (ExcelPackage package = new ExcelPackage(testTemplate))
            {
                // Grab the sheet with the template.
                ExcelWorksheet sheet = package.Workbook.Worksheets[sheetName];

                if (sheet == null)
                {
                    _ErrorMessage = string.Concat("Excel Template Sheet '", sheetName, "' was not found.");
                    return false;
                }

                // Populate Header Fields
                if (rowSettings[ExcelTemplateFields.LocDate].ToString().Length > 0)
                {
                    sheet.Cells[_MetadataLocations.DateRow, _MetadataLocations.DateCol].Value = _Metadata_Date;
                }
                if (rowSettings[ExcelTemplateFields.LocDocName].ToString().Length > 0)
                {
                    sheet.Cells[_MetadataLocations.DocNameRow, _MetadataLocations.DocNameCol].Value = _Metadata_DocName;
                }
                if (rowSettings[ExcelTemplateFields.LocProjectName].ToString().Length > 0)
                {
                    sheet.Cells[_MetadataLocations.ProjectNameRow, _MetadataLocations.ProjectNameCol].Value = _Metadata_ProjectName;
                }
                if (rowSettings[ExcelTemplateFields.LocYourName].ToString().Length > 0)
                {
                    sheet.Cells[_MetadataLocations.YourNameRow, _MetadataLocations.YourNameCol].Value = _Metadata_YourName;
                }

                //if (rowSettings[ExcelTemplateFields.LocAnalysisName].ToString().Length > 0)
                //{
                //    sheet.Cells[_MetadataLocations.AnalysisNameRow, _MetadataLocations.AnalysisNameCol].Value = _Metadata_AnalysisName;
                //}


                // Populate Excel Data Cells/Rows
                int dataRowStart = Convert.ToInt32(rowSettings[ExcelTemplateFields.DataRowStart].ToString());

                int i = dataRowStart; // row incremental 

                double weight = 0;
                bool grayBackground = false;

                int segmentUID = -1;
                string dicCat = string.Empty;

                bool foundNotation = false;

                foreach (DataRow row in dt.Rows)
                {
                    segmentUID = Convert.ToInt32(row[ReportDicDocFields.UID].ToString());

                    foundNotation = PopulateRAM(segmentUID, i, sheet, dtDicAnalysis, dsRAMDef, UseColor);

                    if (!foundNotation)
                    {
                        grayBackground = true;
                    }
                    else
                    {
                        grayBackground = false;
                    }
                    
                    // Caption
                    if (_DataFieldColLoc.Caption != -1)
                    {
                        sheet.Cells[i, _DataFieldColLoc.Caption].Value = row[ReportDicDocFields.Caption].ToString();

                        if (grayBackground && UseColor)
                        {
                            sheet.Cells[i, _DataFieldColLoc.Caption].Style.Fill.PatternType = ExcelFillStyle.Solid;

                            sheet.Cells[i, _DataFieldColLoc.Caption].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        }

                    }
                    // Dic Category
                    if (_DataFieldColLoc.DicCategory != -1)
                    {
                        sheet.Cells[i, _DataFieldColLoc.DicItems].Value = row[ReportDicDocFields.Category].ToString();

                        if (grayBackground && UseColor)
                        {
                            sheet.Cells[i, _DataFieldColLoc.DicCategory].Style.Fill.PatternType = ExcelFillStyle.Solid;

                            sheet.Cells[i, _DataFieldColLoc.DicCategory].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        }
                    }
                    // Dic Items
                    if (_DataFieldColLoc.DicItems != -1)
                    {
                        sheet.Cells[i, _DataFieldColLoc.DicItems].Value = row[ReportDicDocFields.DicItems].ToString();

                        if (grayBackground && UseColor)
                        {
                            sheet.Cells[i, _DataFieldColLoc.DicItems].Style.Fill.PatternType = ExcelFillStyle.Solid;

                            sheet.Cells[i, _DataFieldColLoc.DicItems].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        }
                    }
                    // Dic Def.s
                    if (_DataFieldColLoc.DicDefs != -1)
                    {
                        sheet.Cells[i, _DataFieldColLoc.DicDefs].Value = row[ReportDicDocFields.DicDefs].ToString();

                        if (grayBackground && UseColor)
                        {
                            sheet.Cells[i, _DataFieldColLoc.DicDefs].Style.Fill.PatternType = ExcelFillStyle.Solid;

                            sheet.Cells[i, _DataFieldColLoc.DicDefs].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        }
                    }
                    // Dic Weight
                    if (_DataFieldColLoc.DicWeight != -1)
                    {
                        if (row[ReportDicDocFields.WeightDoc].ToString() == string.Empty)
                        {
                            weight = 0;
                            if (grayBackground && UseColor)
                            {
                                sheet.Cells[i, _DataFieldColLoc.DicWeight].Style.Fill.PatternType = ExcelFillStyle.Solid;

                                sheet.Cells[i, _DataFieldColLoc.DicWeight].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                            }
                        }
                        else
                        {
                            weight = Convert.ToDouble(row[ReportDicDocFields.WeightDoc].ToString());
                        }

                        sheet.Cells[i, _DataFieldColLoc.DicWeight].Value = weight;

                        if (UseColor)
                        {
                            sheet.Cells[i, _DataFieldColLoc.DicWeight].Style.Fill.PatternType = ExcelFillStyle.Solid;

                            if (weight == 0)
                                sheet.Cells[i, _DataFieldColLoc.DicWeight].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                            else if (weight > 0 && weight < 1)
                                sheet.Cells[i, _DataFieldColLoc.DicWeight].Style.Fill.BackgroundColor.SetColor(Color.GreenYellow);
                            else if (weight > .99 && weight < 3)
                                sheet.Cells[i, _DataFieldColLoc.DicWeight].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
                            else if (weight > 2.99 && weight < 5)
                            {
                                sheet.Cells[i, _DataFieldColLoc.DicWeight].Style.Font.Color.SetColor(Color.White);
                                sheet.Cells[i, _DataFieldColLoc.DicWeight].Style.Fill.BackgroundColor.SetColor(Color.Green);
                            }
                            else if (weight >= 5)
                            {
                                sheet.Cells[i, _DataFieldColLoc.DicWeight].Style.Font.Color.SetColor(Color.White);
                                sheet.Cells[i, _DataFieldColLoc.DicWeight].Style.Fill.BackgroundColor.SetColor(Color.DarkGreen);
                            }
                        }
                    }
                    // Page Numbers
                    if (_DataFieldColLoc.PageNo != -1)
                    {
                        sheet.Cells[i, _DataFieldColLoc.PageNo].Value = row["PageNo"].ToString();

                        if (grayBackground && UseColor)
                        {
                            sheet.Cells[i, _DataFieldColLoc.PageNo].Style.Fill.PatternType = ExcelFillStyle.Solid;

                            sheet.Cells[i, _DataFieldColLoc.PageNo].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        }
                    }
                    // Number
                    if (_DataFieldColLoc.Number != -1)
                    {
                        sheet.Cells[i, _DataFieldColLoc.Number].Value = row[ReportDicDocFields.Number].ToString();

                        if (grayBackground && UseColor)
                        {
                            sheet.Cells[i, _DataFieldColLoc.Number].Style.Fill.PatternType = ExcelFillStyle.Solid;

                            sheet.Cells[i, _DataFieldColLoc.Number].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        }
                    }
                    // Caption & Number
                    if (_DataFieldColLoc.NoCaption != -1)
                    {
                        sheet.Cells[i, _DataFieldColLoc.NoCaption].Value = string.Concat(row[ReportDicDocFields.Number].ToString(), "  ", row[ReportDicDocFields.Caption].ToString());

                        if (grayBackground && UseColor)
                        {
                            sheet.Cells[i, _DataFieldColLoc.NoCaption].Style.Fill.PatternType = ExcelFillStyle.Solid;

                            sheet.Cells[i, _DataFieldColLoc.NoCaption].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        }
                    }
                    // Segment/Paragraph Text
                    if (_DataFieldColLoc.SegText != -1)
                    {
                        ExcelRange range = sheet.Cells[i, _DataFieldColLoc.SegText];

                        // Convert HTML text to Excel with Highlights converted to Red Bold font
                        SetRichTextFromHtml(range, row[ReportDicDocFields.Text].ToString().Trim(), rowSettings[ExcelTemplateFields.FontName].ToString(), short.Parse(rowSettings[ExcelTemplateFields.FontSize].ToString()));

                        if (grayBackground && UseColor)
                        {
                            sheet.Cells[i, _DataFieldColLoc.SegText].Style.Fill.PatternType = ExcelFillStyle.Solid;

                            sheet.Cells[i, _DataFieldColLoc.SegText].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        }
                    }

                    // Add Notes
                    if (_DataFieldColLoc.Notes != -1)
                    {
                        sheet.Cells[i, _DataFieldColLoc.Notes].Value = row[ReportDicDocFields.Notes].ToString();

                        if (grayBackground && UseColor)
                        {
                            sheet.Cells[i, _DataFieldColLoc.Notes].Style.Fill.PatternType = ExcelFillStyle.Solid;

                            sheet.Cells[i, _DataFieldColLoc.Notes].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        }
                    }
                    else if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() != NotesEnbedded.None) // Enbedded Notes
                    {
                        string note = row[ReportDicDocFields.Notes].ToString();
                        if (note.Length > 0)
                        {
                            int col = -1;
                            if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() == NotesEnbedded.Caption)
                            {
                                col = _DataFieldColLoc.Caption;
                            }
                            if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() == NotesEnbedded.NoCaption)
                            {
                                col = _DataFieldColLoc.NoCaption;
                            }
                            if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() == NotesEnbedded.Number)
                            {
                                col = _DataFieldColLoc.Number;
                            }
                            if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() == NotesEnbedded.SegPar)
                            {
                                col = _DataFieldColLoc.SegText;
                            }

                            if (col != -1)
                            {
                                sheet.Cells[i, col].AddComment(note, _Metadata_YourName);
                                sheet.Cells[i, col].Comment.AutoFit = true;
                            }
                        }
                    } // Notes < --

                    sheet.Row(i).CustomHeight = false; // test auto-height

                    i++; // row number

                }

                package.Save();


                Process.Start(exportFile);

            }

            return true;
        }

        private bool PopulateRAM(int segmentUID, int sheetRowNo, ExcelWorksheet sheet, DataTable dtDicAnalysis, DataSet dsDictionary, bool UseColor)
        {
            bool foundNotation = false;

            string filter = string.Concat("SegmentUID = ", segmentUID.ToString());
            DataRow[] rows = dtDicAnalysis.Select(filter);

            int count = rows.Length;
            if (count == 0)
                return foundNotation;

            string DicCategory = string.Empty;
            int DicCategoryUID = -1;

            // RAM Details
            DataRow[] rowsRAM;
            string LocRole = string.Empty; // Example values: "E", "D", "F"
            int intLocRole = -1;
            string RoleNotation = string.Empty;
            string RoleColor = string.Empty;
            Color roleBkgrdColor;

            for (int i = 0; i < count; i++)
            {
                DicCategory = rows[i]["Category"].ToString();
                DicCategoryUID = _DictionaryMgr.GetCategoryUID(DicCategory);

                filter = string.Concat("DicCatUID = ", DicCategoryUID.ToString());
                rowsRAM = dsDictionary.Tables["RAMDef"].Select(filter);
                string notations = string.Empty;

                if (rowsRAM.Length > 0)
                {
                    foreach (DataRow rowRAM in rowsRAM)
                    {
                        LocRole = rowRAM["LocRole"].ToString();
                        RoleNotation = rowRAM["RoleNotation"].ToString();
                        RoleColor = rowRAM["RoleColor"].ToString();
                        roleBkgrdColor = Color.FromName(RoleColor);

                        intLocRole = GetColNumber(LocRole);
                        if (intLocRole != -1) // -1 = Not found
                        {
                            //if (sheet.Cells[intLocRole, sheetRowNo].Value != null || sheet.Cells[intLocRole, sheetRowNo].Value != string.Empty)
                            if (sheet.Cells[sheetRowNo, intLocRole].Text.Trim().Length > 0) 
                            {
                                notations = getCleanRoleNotations(sheet.Cells[sheetRowNo, intLocRole].Text, RoleNotation);
                                sheet.Cells[sheetRowNo, intLocRole].Value = notations;

                                //sheet.Cells[sheetRowNo, intLocRole].Value = string.Concat(sheet.Cells[sheetRowNo, intLocRole].Text, ", ", RoleNotation);
                            }
                            else
                            {
                                sheet.Cells[sheetRowNo, intLocRole].Value = RoleNotation;
                            }

                            if (UseColor)
                            {
                                sheet.Cells[sheetRowNo, intLocRole].Style.Fill.PatternType = ExcelFillStyle.Solid;

                                sheet.Cells[sheetRowNo, intLocRole].Style.Fill.BackgroundColor.SetColor(roleBkgrdColor);
                            }

                            foundNotation = true;
                        }
                    }

                }
                

            }

            return foundNotation;
        }

        private string getCleanRoleNotations(string currentValue, string newValue)
        {
            string returnValue = currentValue;

            if (currentValue.IndexOf(',') == -1)
            {
                if (currentValue.Trim() == newValue.Trim())
                {
                    return returnValue;
                }
                else
                {
                    returnValue = string.Concat(currentValue, ", ", newValue);
                    return returnValue;
                }
            }
            else
            {
                string[] values = currentValue.Split(',');
                foreach (string notation in values)
                {
                    if (notation.Trim() == newValue.Trim())
                    {
                        return returnValue;
                    }
                }

                returnValue = string.Concat(currentValue, ", ", newValue);
                return returnValue;
            }

        }

        public bool ExportDicDocs(DataTable dt, DataTable dtTotals, string[] Docs, string TemplateName, string TempatePath, string ExportPath, string resultFileName, bool ColorWeights)
        {
            _ErrorMessage = string.Empty;

            // Get Settings
            DataRow rowSettings = GetSettings(TemplateName, TempatePath);

            if (rowSettings == null)
                return false;

             Populate_DataFieldColLoc(rowSettings); 

            if (!Populate_MetadataLocations(rowSettings))
            {
                return false;
            }

            string exportFile = string.Empty;
            if (!CopyTemplate3ExportFile(TemplateName, resultFileName, TempatePath, ExportPath, out exportFile))
            {
                return false;
            }

            FileInfo testTemplate = new FileInfo(exportFile);

            string sheetName = rowSettings[ExcelTemplateFields.SheetName].ToString();
            string sheetNameChart = rowSettings[ExcelTemplateFields.SheetName_Chart].ToString();

            int x = 1;
            int y = 1;
            int firstDocCol = 0;
            int firstDataRow = 0;

            int docsCount = Docs.Length;
            if (docsCount == 0)
            {
                _ErrorMessage = "Documents are not defined.";
                return false;
            }

            // Create Excel EPPlus Package based on template stream
            using (ExcelPackage package = new ExcelPackage(testTemplate))
            {
                x = Convert.ToInt32(rowSettings[ExcelTemplateFields.DataRowStart_Chart].ToString());
                firstDataRow = x;

                // Summary Chart
                if (sheetNameChart != string.Empty)
                {
                    ExcelWorksheet sheetChart = package.Workbook.Worksheets[sheetNameChart];

                    if (sheetChart != null)
                    {
                        if (dtTotals != null)
                        {
                            // Count -----------------------------------------

                            int chartDataCol = 1;

                            sheetChart.Cells[x + 1, chartDataCol].Value = "Count";
                            sheetChart.Cells[x + 1, chartDataCol].Style.Font.Bold = true;
                            chartDataCol++;

                            sheetChart.Cells[x, chartDataCol - 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            sheetChart.Cells[x, chartDataCol - 1].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);

                            foreach (string docName in Docs)
                            {
                                sheetChart.Cells[x, chartDataCol].Value = docName;
                                sheetChart.Cells[x, chartDataCol].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheetChart.Cells[x, chartDataCol].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                                chartDataCol++;
                            }

                            x++;
                            y = 3;
                            chartDataCol = 2;
                            
                            DataRow rowTCount = dtTotals.Rows[0];
                            
                            foreach (string docName in Docs)
                            {
                                if (DataFunctions.IsNumeric(rowTCount[y -1].ToString()))
                                {
                                    sheetChart.Cells[x, chartDataCol].Value = Convert.ToInt32(rowTCount[y - 1].ToString());
                                }
                                y++;
                                y++;
                                chartDataCol++;
                            }

                            // Add Chart of type Pie.
                            var myChart = sheetChart.Drawings.AddChart("Chart", OfficeOpenXml.Drawing.Chart.eChartType.Doughnut);

                            // Define series for the chart
                            string captionStart = GetExcelCell(firstDataRow, 1);
                            string captionEnd = GetExcelCell(firstDataRow, Docs.Length);
                            string dataStart = GetExcelCell(firstDataRow +1, 1);
                            string dataEnd = GetExcelCell(firstDataRow + 1, Docs.Length);
                            string captionSeries = string.Concat(captionStart, ": ", captionEnd);    // Example: "C2: F2"
                            string dataSeries = string.Concat(dataStart, ": ", dataEnd);    // Example: "C3: F4"

                            var series = myChart.Series.Add(dataSeries, captionSeries);
                           // myChart.Border.Fill.Color = System.Drawing.Color.Green;
                            myChart.Title.Text = "Count";
                            myChart.SetSize(400, 400);

                            // Add to 6th row and to the 6th column
                            myChart.SetPosition(5, 0, 1, 0);



                            // Weight -----------------------------------------

                            x = x + 24;
                            y = 3;
                            chartDataCol = 1;

                            sheetChart.Cells[x + 1, chartDataCol].Value = "Weight";
                            sheetChart.Cells[x + 1, chartDataCol].Style.Font.Bold = true;
                            chartDataCol++;
                            firstDataRow = x;

                            sheetChart.Cells[x, chartDataCol - 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            sheetChart.Cells[x, chartDataCol - 1].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);

                            foreach (string docName in Docs)
                            {
                                sheetChart.Cells[x, chartDataCol].Value = docName;
                                sheetChart.Cells[x, chartDataCol].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                sheetChart.Cells[x, chartDataCol].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                                chartDataCol++;
                            }

                            x++;
                            y = 2;
                            chartDataCol = 2;

                           // rowTCount = dtTotals.Rows[0];

                            foreach (string docName in Docs)
                            {
                                if (DataFunctions.IsValidDouble(rowTCount[y - 1].ToString()))
                                {
                                    sheetChart.Cells[x, chartDataCol].Value = Convert.ToDouble(rowTCount[y - 1].ToString());
                                }
                                y++;
                                y++;
                                chartDataCol++;
                            }

                            // Add Chart of type Pie.
                            var myChart2 = sheetChart.Drawings.AddChart("Chart2", OfficeOpenXml.Drawing.Chart.eChartType.Doughnut);

                            // Define series for the chart
                            captionStart = GetExcelCell(firstDataRow, 1);
                            captionEnd = GetExcelCell(firstDataRow, Docs.Length);
                            dataStart = GetExcelCell(firstDataRow + 1, 1);
                            dataEnd = GetExcelCell(firstDataRow + 1, Docs.Length);
                            captionSeries = string.Concat(captionStart, ": ", captionEnd);    // Example: "C2: F2"
                            dataSeries = string.Concat(dataStart, ": ", dataEnd);    // Example: "C3: F4"

                            var series2 = myChart2.Series.Add(dataSeries, captionSeries);
                            // myChart.Border.Fill.Color = System.Drawing.Color.Green;
                            myChart2.Title.Text = "Weight";
                            myChart2.SetSize(400, 400);

                            
                            myChart2.SetPosition(x +1, 0, 1, 0);
                        }
                    }
                }


                // Grab the sheet with the template.
                ExcelWorksheet sheet = package.Workbook.Worksheets[sheetName];

                if (sheet == null)
                {
                    _ErrorMessage = string.Concat("Excel Template Sheet '", sheetName, "' was not found.");
                    return false;
                }


                // Populate Header Fields
                if (rowSettings[ExcelTemplateFields.LocDate].ToString().Length > 0)
                {
                    sheet.Cells[_MetadataLocations.DateRow, _MetadataLocations.DateCol].Value = _Metadata_Date;
                }
                if (rowSettings[ExcelTemplateFields.LocProjectName].ToString().Length > 0)
                {
                    sheet.Cells[_MetadataLocations.ProjectNameRow, _MetadataLocations.ProjectNameCol].Value = _Metadata_ProjectName;
                }
                if (rowSettings[ExcelTemplateFields.LocYourName].ToString().Length > 0)
                {
                    sheet.Cells[_MetadataLocations.YourNameRow, _MetadataLocations.YourNameCol].Value = _Metadata_YourName;
                }
                //if (rowSettings[ExcelTemplateFields.LocAnalysisName].ToString().Length > 0)
                //{
                //    sheet.Cells[_MetadataLocations.AnalysisNameRow, _MetadataLocations.AnalysisNameCol].Value = _Metadata_AnalysisName;
                //}


                // Insert Document Headers
                if (rowSettings[ExcelTemplateFields.HeaderRowStart].ToString().Length == 0)
                {
                    _ErrorMessage = "Header Documents Row is not defined.";
                    return false;
                }
                if (rowSettings[ExcelTemplateFields.Header1stColStart].ToString().Length == 0)
                {
                    _ErrorMessage = "Header Documents Columns are not defined.";
                    return false;
                }

                if (DataFunctions.IsNumeric(rowSettings[ExcelTemplateFields.Header1stColStart].ToString()))
                {
                    y = Convert.ToInt32(rowSettings[ExcelTemplateFields.Header1stColStart].ToString());
                }
                else
                {
                    y = GetColNumber(rowSettings[ExcelTemplateFields.Header1stColStart].ToString());
                }

                firstDocCol = y;
                x = Convert.ToInt32(rowSettings[ExcelTemplateFields.HeaderRowStart].ToString());


                string docColCountName = string.Empty;
                string docColWeightName = string.Empty;
                double dbWeight = 0;

                //sheet.Cells[x, y].Value = "Category";
                //y++;
                //sheet.Cells[x, y].Value = "Item";
                //y++;
                //sheet.Cells[x, y].Value = "Weight";
                //y++;

                foreach (string docName in Docs)
                {
                    sheet.Cells[x, y].Value = string.Concat(docName, " [Count]");
                    y++;

                    sheet.Cells[x, y].Value = string.Concat(docName, " [Weight]");
                    y++;
                }

                int notesCol = y;
                sheet.Cells[x, notesCol].Value = "Notes";

                // <-- Insert Headers


                // Populate Excel Data Cells/Rows
                int dataRowStart = Convert.ToInt32(rowSettings[ExcelTemplateFields.DataRowStart].ToString());

                int i = dataRowStart; // row incremental 
                int lastRowNo = 0;

              //  docsCount = docsCount * 2; // ?????

                foreach (DataRow row in dt.Rows)
                {
                    // Dictionary Category
                    if (_DataFieldColLoc.DicCategory != -1)
                    {
                        sheet.Cells[i, _DataFieldColLoc.DicCategory].Value = row[ReportDicDocFields.Category].ToString();
                    }

                    // Dictionary Items
                    if (_DataFieldColLoc.DicItems != -1)
                    {
                        sheet.Cells[i, _DataFieldColLoc.DicItems].Value = row[ReportDicDocFields.DicItem].ToString();
                    }

                    // Item Weight
                    if (_DataFieldColLoc.DicWeight != -1)
                    {
                        sheet.Cells[i, _DataFieldColLoc.DicWeight].Value = row[ReportDicDocFields.WeightDoc].ToString();
                    }

                    sheet.Cells[i, notesCol].Value = row[ConceptsResultsFields.Notes].ToString(); // Insert notes

                    y = firstDocCol; // 1st Document Count Column Location
                    for (int yy = 0; yy < docsCount; yy++) // Loop Document Count columns, e.g. Count_0, Count_1, Count_2
                    {
                        docColCountName = string.Concat(ReportDicDocFields.Count, yy.ToString());
                        sheet.Cells[i, y].Value = row[docColCountName].ToString();
                        y++;

                        docColWeightName = string.Concat(ReportDicDocFields.Weight, yy.ToString());
                        sheet.Cells[i, y].Value = row[docColWeightName].ToString();

                        if (ColorWeights)
                        {
                            sheet.Cells[i, y].Style.Fill.PatternType = ExcelFillStyle.Solid;

                            if (row[docColWeightName].ToString() == string.Empty)
                            {
                                dbWeight = 0;
                            }
                            else
                            {
                                dbWeight = Convert.ToDouble(row[docColWeightName].ToString());
                            }

                            if (dbWeight == 0)
                                sheet.Cells[i, y].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                            else if (dbWeight < 1)
                                sheet.Cells[i, y].Style.Fill.BackgroundColor.SetColor(Color.GreenYellow);
                            else if (dbWeight > .99 && dbWeight < 3)
                                sheet.Cells[i, y].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
                            else if (dbWeight > 2.99 && dbWeight < 5)
                            {
                                sheet.Cells[i, y].Style.Font.Color.SetColor(Color.White);
                                sheet.Cells[i, y].Style.Fill.BackgroundColor.SetColor(Color.Green);
                            }
                            else if (dbWeight >= 5)
                            {
                                sheet.Cells[i, y].Style.Font.Color.SetColor(Color.White);
                                sheet.Cells[i, y].Style.Fill.BackgroundColor.SetColor(Color.DarkGreen);
                            }
                        }

                        y++;
                        
                    }

                    // Add Notes
                    //if (_DataFieldColLoc.Notes != -1)
                    //{
                    //    sheet.Cells[i, _DataFieldColLoc.Notes].Value = row[ReportConceptsDocFields.Notes].ToString();
                    //}
                    //else if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() != NotesEnbedded.None) // Enbedded Notes
                    //{
                    //    string note = row[ReportConceptsDocFields.Notes].ToString();
                    //    if (note.Length > 0)
                    //    {
                    //        int col = -1;
                    //        if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() == NotesEnbedded.Caption)
                    //        {
                    //            col = _DataFieldColLoc.Caption;
                    //        }
                    //        if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() == NotesEnbedded.NoCaption)
                    //        {
                    //            col = _DataFieldColLoc.NoCaption;
                    //        }
                    //        if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() == NotesEnbedded.Number)
                    //        {
                    //            col = _DataFieldColLoc.Number;
                    //        }
                    //        if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() == NotesEnbedded.SegPar)
                    //        {
                    //            col = _DataFieldColLoc.SegText;
                    //        }

                    //        if (col != -1)
                    //        {
                    //            sheet.Cells[i, col].AddComment(note, _Metadata_YourName);
                    //            sheet.Cells[i, col].Comment.AutoFit = true;
                    //        }
                    //    }
                    //} // Notes < --

                    i++;
                    lastRowNo = i;

                } // Data rows

                //// Insert Summary Totals
                //if (dtTotals.Rows.Count > 0)
                //{
                //    lastRowNo++;

                //    DataRow row = dtTotals.Rows[0];
                    
                //    sheet.Cells[lastRowNo, _DataFieldColLoc.Concepts].Value = "Total";
                //    y = firstDocCol; // 1st Document Count Column Location
                //    for (int yy = 0; yy < docsCount; yy++) // Loop Document Count columns, e.g. Count_0, Count_1, Count_2
                //    {
                //        docColCountName = string.Concat(ReportDicDocFields.Count, yy.ToString());
                //        sheet.Cells[i, y].Value = row[ReportDicDocFields.Count].ToString();
                //        y++;

                //        docColWeightName = string.Concat(ReportDicDocFields.Weight, yy.ToString());
                //        sheet.Cells[i, y].Value = row[ReportDicDocFields.Weight].ToString();

                //        if (ColorWeights)
                //        {
                //            sheet.Cells[x, y].Style.Fill.PatternType = ExcelFillStyle.Solid;

                //            dbWeight = Convert.ToDouble(row[ReportDicDocFields.Weight].ToString());


                //            if (dbWeight < 1)
                //                sheet.Cells[x, y].Style.Fill.BackgroundColor.SetColor(Color.GreenYellow);
                //            else if (dbWeight > .99 && dbWeight < 3)
                //                sheet.Cells[x, y].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
                //            else if (dbWeight > 2.99 && dbWeight < 5)
                //            {
                //                sheet.Cells[x, y].Style.Font.Color.SetColor(Color.White);
                //                sheet.Cells[x, y].Style.Fill.BackgroundColor.SetColor(Color.Green);
                //            }
                //            else if (dbWeight >= 5)
                //            {
                //                sheet.Cells[x, y].Style.Font.Color.SetColor(Color.White);
                //                sheet.Cells[x, y].Style.Fill.BackgroundColor.SetColor(Color.DarkGreen);
                //            }
                //        }

                //        y++;

                //    }
                //}

                package.Save();


                Process.Start(exportFile);

            }

            return true;
        }


        private DataRow GetSettings(string TemplateName, string TempatePath)
        {
            _ErrorMessage = string.Empty;

            // Get Settings
            string file = string.Concat(TemplateName, ".xml");
            string pathFile = Path.Combine(TempatePath, file);

            if (!File.Exists(pathFile))
            {
                _ErrorMessage = String.Concat("Unable to locate Excel Template Settings XML file: ", pathFile);
                return null;
            }

            DataSet dsSettings = Files.LoadDatasetFromXml(pathFile);

            DataRow rowSettings = dsSettings.Tables[0].Rows[0];

            return rowSettings;

        }



   

        private void SetAltColorRows(ExcelWorksheet ws, string colorName, int StartRow)
        {
            Color rowColor = Color.FromName(colorName);

            for (int row = StartRow; row <= ws.Dimension.End.Row; row++)
            {
                int pos = row % 2;
                // ExcelRow = ws.Row(row);
                ExcelRange rowRange = ws.SelectedRange[row, 1, row, ws.Dimension.End.Column];
                ExcelFill RowFill = rowRange.Style.Fill;

                RowFill.PatternType = ExcelFillStyle.Solid;
                switch (pos)
                {
                    case 0:
                        RowFill.BackgroundColor.SetColor(System.Drawing.Color.White);

                        break;
                    case 1:
                        RowFill.BackgroundColor.SetColor(rowColor);
                        break;

                } // sheet.Dimension.End.Column
            }
        }

        private void SetRowColor(ExcelWorksheet ws, int row, string colorName)
        {
            Color rowColor = Color.FromName(colorName);

            ExcelRange rowRange = ws.SelectedRange[row, 1, row, ws.Dimension.End.Column];
            ExcelFill RowFill = rowRange.Style.Fill;
            RowFill.PatternType = ExcelFillStyle.Solid;

            RowFill.BackgroundColor.SetColor(rowColor);

        }

        private bool isSentenceDARrow(string Number)
        {
            if (Number.Contains("-S"))
            {
                return true;
            }

            return false;
        }


  


        private string RemoveInvalidCharacters(string DirtyString)
        {
            string cleanedString = Regex.Replace(DirtyString, @"[\u0000-\u0008]", "");
            cleanedString = Regex.Replace(cleanedString, @"[\u000B]", "");
            cleanedString = Regex.Replace(cleanedString, @"[\u000B]", "");
            cleanedString = Regex.Replace(cleanedString, @"[\u000C]", "");
            cleanedString = Regex.Replace(cleanedString, @"[\u000E-\u001F]", "");

            return cleanedString;
        }

        public void SetRichTextFromHtml(ExcelRange range, string html, string defaultFontName, short defaultFontSize)
        {
            // Reset the cell value, just in case there is an existing RichText value.
            range.Value = "";

            // Sanity check for blank values, skips creating Regex objects for performance.
            if (String.IsNullOrEmpty(html))
            {
                range.IsRichText = false;
                return;
            }

            // Change all BR tags to line breaks. http://epplus.codeplex.com/discussions/238692/
            // Cells with line breaks aren't necessarily considered rich text, so this is performed
            // before parsing the HTML tags.
            html = System.Text.RegularExpressions.Regex.Replace(html, @"<br[^>]*>", "\r\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            string tag;
            string text;
            ExcelRichText thisrt = null;
            bool isFirst = true;

            // Get all pairs of legitimate tags and the text between them. This loop will
            // only execute if there is at least one start or end tag.
            foreach (Match m in System.Text.RegularExpressions.Regex.Matches(html, @"<(/?[a-z]+)[^<>]*>([\s\S]*?)(?=</?[a-z]+[^<>]*>|$)", RegexOptions.Compiled | RegexOptions.IgnoreCase))
            {
                if (isFirst)
                {
                    // On the very first match, set up the initial rich text object with
                    // the defaults for the text BEFORE the match.
                    range.IsRichText = true;
                    thisrt = range.RichText.Add(CleanText(html.Substring(0, m.Index)));	// May be 0-length
                    thisrt.Size = defaultFontSize;										// Set the default font size
                    thisrt.FontName = defaultFontName;									// Set the default font name
                    isFirst = false;
                }
                // Get the tag and the block of text until the NEXT tag or EOS. If there are HTML entities
                // encoded, unencode them, they should be passed to RichText as normal characters (other
                // than non-breaking spaces, which should be replaced with normal spaces, they break Excel.
                tag = m.Groups[1].Captures[0].Value;
                text = CleanText(m.Groups[2].Captures[0].Value);

                // Test Code
                //if (text == "shall")
                //{
                //    string sx = m.Groups[1].Captures[0].Value;
                //}

                if (m.ToString().IndexOf("background-color:") > -1)
                {
                    if (m.ToString().IndexOf("background-color:#FFFFFF") == -1) // Not White background
                    {
                        tag = "background-color:";
                    }
                }

                if (thisrt.Text == "")
                {
                    // The most recent rich text block wasn't *actually* used last time around, so update
                    // the text and keep it as the "current" block. This happens with the first block if
                    // it starts with a tag, and may happen later if tags come one right after the other.
                    thisrt.Text = text;
                }
                else
                {
                    // The current rich text block has some text, so create a new one. RichText.Add()
                    // automatically applies the settings from the previous block, other than vertical
                    // alignment.
                    thisrt = range.RichText.Add(text);
                }


                // Override the settings based on the current tag, keep all other settings.
                SetStyleFromTag(tag, thisrt);
                // }
            }

            if (thisrt == null)
            {
                // No HTML tags were found, so treat this as a normal text value.
                range.IsRichText = false;
                range.Value = CleanText(html);
            }
            else if (String.IsNullOrEmpty(thisrt.Text))
            {
                // Rich text was found, but the last node contains no text, so remove it. This can happen if,
                // say, the end of the string is an end tag or unsupported tag (common).
                range.RichText.Remove(thisrt);

                // Failsafe -- the HTML may be just tags, no text, in which case there may be no rich text
                // directives that remain. If that is the case, turn off rich text and treat this like a blank
                // cell value.
                if (range.RichText.Count == 0)
                {
                    range.IsRichText = false;
                    range.Value = "";
                }

            }
        }

        private void SetStyleFromTag(string tag, ExcelRichText settings)
        {
            switch (tag.ToLower())
            {
                case "b":
                case "strong":
                    settings.Bold = true;
                    break;
                case "i":
                case "em":
                    settings.Italic = true;
                    break;
                case "u":
                    settings.UnderLine = true;
                    break;
                case "s":
                case "strike":
                    settings.Strike = true;
                    break;
                case "sup":
                    settings.VerticalAlign = ExcelVerticalAlignmentFont.Superscript;
                    break;
                case "sub":
                    settings.VerticalAlign = ExcelVerticalAlignmentFont.Subscript;
                    break;
                case "/b":
                case "/strong":
                    settings.Bold = false;
                    break;
                case "/i":
                case "/em":
                    settings.Italic = false;
                    break;
                case "/u":
                    settings.UnderLine = false;
                    break;
                case "background-color:":
                    settings.Color = Color.Red;
                    settings.Bold = true;
                    break;
                case "/span":
                    settings.Color = Color.Black;
                    settings.Bold = false;
                    break;
                case "/s":
                case "/strike":
                    settings.Strike = false;
                    break;
                case "/sup":
                case "/sub":
                    settings.VerticalAlign = ExcelVerticalAlignmentFont.None;
                    break;
                default:
                    // unsupported HTML, no style change
                    break;
            }
        }

        private static string CleanText(string s)
        {
            // Need to convert HTML entities (named or numbered) into actual Unicode characters
            s = System.Web.HttpUtility.HtmlDecode(s);
            // Remove any non-breaking spaces, kills Excel
            s = s.Replace("\u00A0", " ");
            return s;
        }

    }
}
