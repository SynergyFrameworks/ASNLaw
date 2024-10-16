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

namespace Atebion.Excel.Output
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
           get {return _ErrorMessage;}
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
            set {_Metadata_Date = value;}
        }

        private string _Metadata_ProjectName = string.Empty;
        public string Metadata_ProjectName
        {
            get { return _Metadata_ProjectName; }
            set {_Metadata_ProjectName = value;}
        }

        private string _Metadata_DocName = string.Empty;
        public string Metadata_DocName
        {
            get {return _Metadata_DocName;}
            set {_Metadata_DocName = value;}
        }

        private string _Metadata_YourName = string.Empty;
        public string Metadata_YourName
        {
            get {return _Metadata_YourName;}
            set {_Metadata_YourName = value;}
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



        public void ExportDAResults(DataTable dt, string headerCaptions, string exportPathFile, string DocName, string pathHTML, bool shadedBkgrdSec, bool useUIDCol)
        {
            FileInfo newFile = new FileInfo(exportPathFile);
            if (newFile.Exists)
            {
                newFile.Delete();  // ensures we create a new workbook
                newFile = new FileInfo(exportPathFile);
            }

            string[] Captions = headerCaptions.Split('|');

            //int startIndex = 0;
            //if (!useUIDCol)
            //    startIndex = 1;

            string captionValue = string.Empty;
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

             //   worksheet.HeaderFooter.FirstFooter.CenteredText = DocName; // 3.12.2017

                // get captions
                int textCol = -1;
                // string[] Captions = headerCaptions.Split('|');
                // Add captions
                for (int i = 0; i < Captions.Length; i++)
                {
                    captionValue = Captions[i];
                    worksheet.Cells[1, i + 1].Value = captionValue;

                    //worksheet.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid; // Test
                    //worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.Cornsilk); // Test


                    if (captionValue == "Text")
                    {
                        textCol = i;
                    }
                }

                bool isSegment = false;

                // Write DataTable to an Excel Sheet
                int excelRowNo = 2; // Start writing data to second excel row, header is first row
                string fieldValue = string.Empty;
                string cellID = string.Empty;
                foreach (DataRow row in dt.Rows)
                {

                    if (row["isSegment"].ToString() == "1")
                    {
                        isSegment = true;

                        // --> Shade segment text
                        using (var range = worksheet.Cells[excelRowNo, 1, excelRowNo, dt.Columns.Count - 1]) //Change: 04.05.2015 -- using (var range = worksheet.Cells[1, 1, 1, dt.Columns.Count + 1])
                        {
                            range.Style.Font.Bold = false;

                            if (shadedBkgrdSec)
                            {
                                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                            }

                            range.Style.Font.Color.SetColor(Color.Black);
                        }
                        //<--
                    }
                    else
                    {
                        isSegment = false;
                    }

                    for (int field = 0; field < dt.Columns.Count - 1; field++)
                    {
                        cellID = string.Concat(_columns[field], excelRowNo.ToString()); // Example: A2

                        fieldValue = RemoveInvalidCharacters(row[field].ToString());

                        ExcelRange range = worksheet.Cells[cellID];

                        if (field == textCol)
                        {
                            SetRichTextFromHtml(range, fieldValue, "Arial", 10);                      

                        }
                        else
                        {
                            // Example: worksheet.Cells["A2"].Value = "1.1";
                            worksheet.Cells[cellID].Value = fieldValue;
                        }

                        worksheet.Cells[cellID].Style.Border.BorderAround(ExcelBorderStyle.Thin); // 3.12.2017
                    }

                    excelRowNo++;
                }

                for (int i = 0; i < Captions.Length; i++)
                {
                    switch (Captions[i])
                    {
                        case "Lines":
                            worksheet.Column(i + 1).Width = 7;
                            break;
                        case "Number":
                            worksheet.Column(i + 1).Width = 8;
                            worksheet.Column(i + 1).Style.WrapText = true;
                            break;
                        case "NoCaption":
                        case "No and Caption": // Added 10.19.2013
                            worksheet.Column(i + 1).Width = 47;
                            break;
                        case "Caption":
                            worksheet.Column(i + 1).Width = 45;
                            worksheet.Column(i + 1).Style.WrapText = true;
                            break;
                        case "Keywords":
                            worksheet.Column(i + 1).Width = 15;
                            worksheet.Column(i + 1).Style.WrapText = true;
                            break;
                        case "Text":
                            worksheet.Column(i + 1).Width = 45;
                            worksheet.Column(i + 1).Style.WrapText = true;
                            break;
                        case "Notes":
                            worksheet.Column(i + 1).Width = 45;
                            worksheet.Column(i + 1).Style.WrapText = true;
                            break;


                    }
                }

                worksheet.PrinterSettings.Orientation = eOrientation.Landscape;


                //Format Header;
                using (var range = worksheet.Cells[1, 1, 1, dt.Columns.Count - 1]) //Change: 09.21.2013 -- using (var range = worksheet.Cells[1, 1, 1, dt.Columns.Count + 1])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }

                // Set Font
                var allCells = worksheet.Cells[1, 1, worksheet.Dimension.End.Row, worksheet.Dimension.End.Column];
                var cellFont = allCells.Style.Font;
                cellFont.SetFromFont(new Font("Arial", 10));

                allCells.Style.Border.BorderAround(ExcelBorderStyle.Thick); // 3.12.2017
                

                // Change the sheet view to show it in page layout mode
                worksheet.View.PageLayoutView = false;

                // set some document properties
                package.Workbook.Properties.Title = "Parsed Results";
                package.Workbook.Properties.Author = "Atebion LLC Document Analyzer";
                package.Workbook.Properties.Comments = string.Format("Collection of parsed segments from {0} ", DocName);

                // set some extended property values
                package.Workbook.Properties.Company = "Atebion LLC";

                string version = Assembly.GetEntryAssembly().GetName().Version.ToString();

                // set some custom property values
                package.Workbook.Properties.SetCustomPropertyValue("Version", version);
                package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", "ExportToExcel");
                // save new workbook 
                package.Save();

            }
        }

        /// <summary>
        /// Exports Analysis Results in an Excel file
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="headerCaptions">Delimited '|' Header Captions</param>
        /// <param name="exportPathFile">Exported Path & Filename</param>
        /// <param name="DocName">Document Name</param>
        /// <returns>Returns True if No Errors occurred</returns>
        //public bool ExportDAResults(DataTable dt, string headerCaptions, string exportPathFile, string DocName)
        //{
        //    if (DocName.Length > 31) // 31 char.s is Excel's max. limit
        //    {
        //        DocName = DocName.Substring(0, 30);
        //    }

        //    FileInfo newFile = new FileInfo(exportPathFile);
        //    if (newFile.Exists)
        //    {
        //        newFile.Delete();  // ensures we create a new workbook
        //        newFile = new FileInfo(exportPathFile);
        //    }

        //    string[] Captions = headerCaptions.Split('|');

        //    string captionValue = string.Empty;
        //    using (ExcelPackage package = new ExcelPackage(newFile))
        //    {
        //        // add a new worksheet to the empty workbook
        //        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(DocName);

        //        // get captions
        //        int textCol = -1;
        //        // string[] Captions = headerCaptions.Split('|');
        //        // Add captions
        //        for (int i = 1; i < Captions.Length; i++)
        //        {
        //            captionValue = Captions[i];
        //            worksheet.Cells[1, i].Value = captionValue;

        //            if (captionValue == "Text")
        //            {
        //                textCol = i;
        //            }
        //        }


        //        // Write DataTable to an Excel Sheet
        //        int excelRowNo = 2; // Start writing data to second excel row, header is first row
        //        string fieldValue = string.Empty;
        //        string cellID = string.Empty;
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            for (int field = 1; field < dt.Columns.Count; field++)
        //            {
        //                cellID = string.Concat(_columns[field - 1], excelRowNo.ToString()); // Example: A2

        //                fieldValue = RemoveInvalidCharacters(row[field].ToString());

        //                if (dt.Columns[field].Caption == "Text")
        //                {

        //                    ExcelRange range = worksheet.Cells[cellID];

        //                    if (_FontName == string.Empty)
        //                        _FontName = "Times New Roman";

        //                    if (_FontSize == null)
        //                        _FontSize = 12;

        //                    SetRichTextFromHtml(range, fieldValue, _FontName, _FontSize);
        //                }
        //                else
        //                {
        //                    // Example: worksheet.Cells["A2"].Value = "1.1";
        //                    worksheet.Cells[cellID].Value = fieldValue;
        //                }


        //            }

        //            excelRowNo++;
        //        }

        //        for (int i = 0; i < Captions.Length; i++)
        //        {
        //            switch (Captions[i])
        //            {
        //                case "Lines":
        //                    worksheet.Column(i).Width = 7;
        //                    break;
        //                case "Number":
        //                    worksheet.Column(i).Width = 8;
        //                    worksheet.Column(i).Style.WrapText = true;
        //                    break;
        //                case "NoCaption":
        //                case "No and Caption": // Added 10.19.2013
        //                    worksheet.Column(i).Width = 47;
        //                    break;
        //                case "Caption":
        //                    worksheet.Column(i).Width = 45;
        //                    worksheet.Column(i).Style.WrapText = true;
        //                    break;
        //                case "Keywords":
        //                    worksheet.Column(i).Width = 15;
        //                    worksheet.Column(i).Style.WrapText = true;
        //                    break;
        //                case "Text":
        //                    worksheet.Column(i).Width = 45;
        //                    worksheet.Column(i).Style.WrapText = true;
        //                    break;
        //                case "Notes":
        //                    worksheet.Column(i).Width = 45;
        //                    worksheet.Column(i).Style.WrapText = true;
        //                    break;


        //            }
        //        }

        //        worksheet.PrinterSettings.Orientation = eOrientation.Landscape;


        //        //Format Header;
        //        using (var range = worksheet.Cells[1, 1, 1, dt.Columns.Count]) //Change: 09.21.2013 -- using (var range = worksheet.Cells[1, 1, 1, dt.Columns.Count + 1])
        //        {
        //            range.Style.Font.Bold = true;
        //            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
        //            range.Style.Font.Color.SetColor(Color.White);
        //        }

        //        // Change the sheet view to show it in page layout mode
        //        worksheet.View.PageLayoutView = true;

        //        // set some document properties
        //        package.Workbook.Properties.Title = "Parsed Results";
        //        package.Workbook.Properties.Author = "Atebion LLC Document Analyzer";
        //        package.Workbook.Properties.Comments = string.Format("Collection of parsed segments from {0} ", DocName);

        //        // set some extended property values
        //        package.Workbook.Properties.Company = "Atebion LLC";

        //        string version = Assembly.GetEntryAssembly().GetName().Version.ToString();

        //        // set some custom property values
        //        package.Workbook.Properties.SetCustomPropertyValue("Version", version);
        //        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", "ExportToExcel");
        //        // save new workbook 
        //        package.Save();

        //    }


        //    return true;
        //}

        static public DataSet LoadDatasetFromXml(string fileName)
        {
            if (!File.Exists(fileName))
                return null;

            DataSet ds = new DataSet();
            FileStream fs = null;

            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                using (StreamReader reader = new StreamReader(fs))
                {
                    ds.ReadXml(reader);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Could NOT get data from file: " + fileName + " Error:" + e.Message);
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }

            return ds;
        }

        struct DataFieldColLoc
        {
            public int LineNo;
            public int Number;
            public int Caption;
            public int NoCaption;
            public int SegText;
            public int Keywords;
            public int Notes;
            public int SentText;
            public int Page;
        };

        struct MetadataLocations
        {
            public int ProjectNameCol;
            public int ProjectNameRow;
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

        struct DARCOptions
        {
            // Exclusions options for Sentences
            public bool Number_Excluded;
            public bool Caption_Excluded;
            public bool NoCaption_Excluded;

            // Segment/Paragraph Background color
            public bool SegPar_BkgrdColor_Use;
            public string SegPar_BkgrdColor;
        }

        private MetadataLocations _MetadataLocations;
        private DataFieldColLoc _DataFieldColLoc;
        private ColorSettings _ColorSettings;
        private DARCOptions _DARCOptions;

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

            if (!GetColRow(rowSettings[ExcelTemplateFields.LocDate].ToString(), out col, out row))
            {
                _ErrorMessage = "Excel Template Column 'Date' was not well formed. -- Fix selected Excel Template under Tools/Settings/Excel Templates.";
                return false;
            }

            _MetadataLocations.DateCol = col;
            _MetadataLocations.DateRow = row;


            if (!GetColRow(rowSettings[ExcelTemplateFields.LocDocName].ToString(), out col, out row))
            {
                _ErrorMessage = "Excel Template Column 'Document Name' was not well formed. -- Fix selected Excel Template under Tools/Settings/Excel Templates.";
                return false;
            }

            _MetadataLocations.DocNameCol = col;
            _MetadataLocations.DocNameRow = row;


            if (!GetColRow(rowSettings[ExcelTemplateFields.LocProjectName].ToString(), out col, out row))
            {
                _ErrorMessage = "Excel Template Column 'Project Name' was not well formed. -- Fix selected Excel Template under Tools/Settings/Excel Templates.";
                return false;
            }

            _MetadataLocations.ProjectNameCol = col;
            _MetadataLocations.ProjectNameRow = row;


            if (!GetColRow(rowSettings[ExcelTemplateFields.LocYourName].ToString(), out col, out row))
            {
                _ErrorMessage = "Excel Template Column 'Your Name' was not well formed. -- Fix selected Excel Template under Tools/Settings/Excel Templates.";
                return false;
            }

            _MetadataLocations.YourNameCol = col;
            _MetadataLocations.YourNameRow = row;

            return true;

        }

        private void Populate_DAROptions(DataRow rowSettings)
        {
            if (!string.IsNullOrEmpty(rowSettings[ExcelTemplateFields.ExcludeDARCaption].ToString()))
            {
                _DARCOptions.Caption_Excluded = (bool)rowSettings[ExcelTemplateFields.ExcludeDARCaption];
            }
            if (!string.IsNullOrEmpty(rowSettings[ExcelTemplateFields.ExcludeDARNoCaption].ToString()))
            {
                _DARCOptions.NoCaption_Excluded = (bool)rowSettings[ExcelTemplateFields.ExcludeDARNoCaption];
            }
            if(!string.IsNullOrEmpty(rowSettings[ExcelTemplateFields.ExcludeDARNumber].ToString()))
            {
                _DARCOptions.Number_Excluded = (bool)rowSettings[ExcelTemplateFields.ExcludeDARNumber];
            }

            if (!string.IsNullOrEmpty(rowSettings[ExcelTemplateFields.SegTextUseBkColor].ToString()))
            {
                _DARCOptions.SegPar_BkgrdColor_Use = (bool)rowSettings[ExcelTemplateFields.SegTextUseBkColor];
            }
            if (!string.IsNullOrEmpty(rowSettings[ExcelTemplateFields.SegTextBkColor].ToString()))
            {
                _DARCOptions.SegPar_BkgrdColor = rowSettings[ExcelTemplateFields.SegTextBkColor].ToString();
            }
        }

        private void Populate_DataFieldColLoc(DataRow rowSettings, bool settingsColExists)
        {
            _DataFieldColLoc.Caption = GetColNumber(rowSettings[ExcelTemplateFields.LocCaption].ToString());

            _DataFieldColLoc.Keywords = GetColNumber(rowSettings[ExcelTemplateFields.LocKeywords].ToString());
            _DataFieldColLoc.LineNo = GetColNumber(rowSettings[ExcelTemplateFields.LocLineNo].ToString());
            _DataFieldColLoc.Notes = GetColNumber(rowSettings[ExcelTemplateFields.LocNotes].ToString());
            _DataFieldColLoc.Number = GetColNumber(rowSettings[ExcelTemplateFields.LocNumber].ToString());
            _DataFieldColLoc.SegText = GetColNumber(rowSettings[ExcelTemplateFields.LocSegText].ToString());
            _DataFieldColLoc.SentText = GetColNumber(rowSettings[ExcelTemplateFields.LocSentText].ToString());
            _DataFieldColLoc.NoCaption = GetColNumber(rowSettings[ExcelTemplateFields.LocNoCaption].ToString());

            // Added 8.16.2018
            if (settingsColExists) 
            {
                _DataFieldColLoc.Page = GetColNumber(rowSettings[ExcelTemplateFields.LocPage].ToString());
            }
            else
            {
                _DataFieldColLoc.Page = -1;
            }
        }

        private bool CopyTemplate2ExportFile(string TemplateFile, string ExportFile)
        {
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


        public void ExportAResults(DataTable dt, string headerCaptions, string exportPathFile, string DocName) // Previously name was CreateXLS2
        {
            FileInfo newFile = new FileInfo(exportPathFile);
            if (newFile.Exists)
            {
                newFile.Delete();  // ensures we create a new workbook
                newFile = new FileInfo(exportPathFile);
            }

            string[] Captions = headerCaptions.Split('|');

            string captionValue = string.Empty;
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

             //   worksheet.HeaderFooter.FirstFooter.CenteredText = DocName; // 3.12.2017

                // get captions
                int textCol = -1;
                // string[] Captions = headerCaptions.Split('|');
                // Add captions
                for (int i = 1; i < Captions.Length; i++)
                {
                    captionValue = Captions[i];
                    worksheet.Cells[1, i].Value = captionValue;

                    if (captionValue == "Text")
                    {
                        textCol = i;
                    }
                }


                // Write DataTable to an Excel Sheet
                int excelRowNo = 2; // Start writing data to second excel row, header is first row
                string fieldValue = string.Empty;
                string cellID = string.Empty;
                foreach (DataRow row in dt.Rows)
                {
                    for (int field = 1; field < dt.Columns.Count; field++)
                    {
                        cellID = string.Concat(_columns[field - 1], excelRowNo.ToString()); // Example: A2

                        fieldValue = RemoveInvalidCharacters(row[field].ToString());

                        ExcelRange range = worksheet.Cells[cellID];

                        if (field == textCol)
                        {
                            SetRichTextFromHtml(range, fieldValue, "Arial", 10);

                        }
                        else
                        {
                            // Example: worksheet.Cells["A2"].Value = "1.1";
                            worksheet.Cells[cellID].Value = fieldValue;
                        }

                        worksheet.Cells[cellID].Style.Border.BorderAround(ExcelBorderStyle.Thin); // 3.12.2017

                    }

                    excelRowNo++;
                }

                for (int i = 0; i < Captions.Length; i++)
                {
                    switch (Captions[i])
                    {
                        case "Lines":
                            worksheet.Column(i).Width = 7;
                            break;
                        case "Number":
                            worksheet.Column(i).Width = 8;
                            worksheet.Column(i).Style.WrapText = true;
                            break;
                        case "NoCaption":
                        case "No and Caption": // Added 10.19.2013
                            worksheet.Column(i).Width = 47;
                            break;
                        case "Caption":
                            worksheet.Column(i).Width = 45;
                            worksheet.Column(i).Style.WrapText = true;
                            break;
                        case "Keywords":
                            worksheet.Column(i).Width = 15;
                            worksheet.Column(i).Style.WrapText = true;
                            break;
                        case "Text":
                            worksheet.Column(i).Width = 45;
                            worksheet.Column(i).Style.WrapText = true;
                            break;
                        case "Notes":
                            worksheet.Column(i).Width = 45;
                            worksheet.Column(i).Style.WrapText = true;
                            break;
                        case "Page": // Added 8.15.2018
                            worksheet.Column(i).Width = 8;
                            worksheet.Column(i).Style.WrapText = true;
                            break;

                    }
                }

                worksheet.PrinterSettings.Orientation = eOrientation.Landscape;


                //Format Header;
                using (var range = worksheet.Cells[1, 1, 1, dt.Columns.Count - 1]) //Change: 09.21.2013 -- using (var range = worksheet.Cells[1, 1, 1, dt.Columns.Count + 1])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }

                // Set Font
                var allCells = worksheet.Cells[1, 1, worksheet.Dimension.End.Row, worksheet.Dimension.End.Column];
                var cellFont = allCells.Style.Font;
                cellFont.SetFromFont(new Font("Arial", 10));


                allCells.Style.Border.BorderAround(ExcelBorderStyle.Thick); // 3.12.2017

  
                // Change the sheet view to show it in page layout mode
                worksheet.View.PageLayoutView = false;

                // set some document properties
                package.Workbook.Properties.Title = "Parsed Results";
                package.Workbook.Properties.Author = "Atebion LLC Document Analyzer";
                package.Workbook.Properties.Comments = string.Format("Collection of parsed segments from {0} ", DocName);

                // set some extended property values
                package.Workbook.Properties.Company = "Atebion LLC";

                string version = Assembly.GetEntryAssembly().GetName().Version.ToString();

                // set some custom property values
                package.Workbook.Properties.SetCustomPropertyValue("Version", version);
                package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", "ExportToExcel");
                // save new workbook 
                package.Save();

            }
        }


        public bool ExportAResults2MatrixTemplate(DataTable dt, int columnX, int dataRowStart, string FontName, short FontSize, string MatrixTemplate)
        {
            _ErrorMessage = string.Empty;

            // Get Settings
            //string file = string.Concat(TemplateName, ".xml");
            //string pathFile = Path.Combine(ARTempatePath, file);
         //   DataSet dsSettings = LoadDatasetFromXml(pathFile);

        //    DataRow rowSettings = dsSettings.Tables[0].Rows[0];

            //Populate_DataFieldColLoc(rowSettings);

            //if (!Populate_MetadataLocations(rowSettings))
            //{
            //    return false;
            //}

            //Populate_ColorDef(rowSettings);

            //string fileTemplate = string.Concat(TemplateName, ".xlsx");
            //string fileAR = string.Concat(resultFileName, ".xlsx");

            //string pathFileTemplate = Path.Combine(ARTempatePath, fileTemplate);
            //string pathFileAR = Path.Combine(ExportPath, fileAR);

            //if (!CopyTemplate2ExportFile(pathFileTemplate, pathFileAR))
            //{
            //    return false;
            //}

            FileInfo testTemplate = new FileInfo(MatrixTemplate);

            //string sheetName = rowSettings[ExcelTemplateFields.SheetName].ToString();

            // Create Excel EPPlus Package based on template stream
            using (ExcelPackage package = new ExcelPackage(testTemplate))
            {
               // string sheetName = package.Workbook.Worksheets[0].Name;
                // Grab the sheet with the template.
                ExcelWorksheet sheet = package.Workbook.Worksheets["Sheet1"]; // ToDo not sure if sheets are zero base or one base 
               // ExcelWorksheet sheet = package.Workbook.Worksheets[sheetName]; // Changed 12.18.2017

                if (sheet == null)
                {
                    _ErrorMessage = string.Concat("Excel Template 1st Sheet was not found.");
                    return false;
                }

                // Populate Header Fields
                //if (rowSettings[ExcelTemplateFields.LocDate].ToString().Length > 0)
                //{
                //    sheet.Cells[_MetadataLocations.DateRow, _MetadataLocations.DateCol].Value = _Metadata_Date;
                //}
                //if (rowSettings[ExcelTemplateFields.LocDocName].ToString().Length > 0)
                //{
                //    sheet.Cells[_MetadataLocations.DocNameRow, _MetadataLocations.DocNameCol].Value = _Metadata_DocName;
                //}
                //if (rowSettings[ExcelTemplateFields.LocProjectName].ToString().Length > 0)
                //{
                //    sheet.Cells[_MetadataLocations.ProjectNameRow, _MetadataLocations.ProjectNameCol].Value = _Metadata_ProjectName;
                //}
                //if (rowSettings[ExcelTemplateFields.LocYourName].ToString().Length > 0)
                //{
                //    sheet.Cells[_MetadataLocations.YourNameRow, _MetadataLocations.YourNameCol].Value = _Metadata_YourName;
                //}

                //List<int> wholeNoRows = new List<int>();
                //bool wholeNoColorUse = (bool)rowSettings[ExcelTemplateFields.WholeNoColorUse];

                //int dataRowStart = Convert.ToInt32(rowSettings[ExcelTemplateFields.DataRowStart].ToString());

                int i = dataRowStart; // row incremental 

                // Populate Excel Data Rows
                foreach (DataRow row in dt.Rows)
                {

                    //if (_DataFieldColLoc.Caption != -1)
                    //{
                    //    sheet.Cells[i, _DataFieldColLoc.Caption].Value = row[ARFields.Caption].ToString();
                    //}
                    //if (_DataFieldColLoc.Keywords != -1)
                    //{
                    //    sheet.Cells[i, _DataFieldColLoc.Keywords].Value = row[ARFields.Keywords].ToString();
                    //}
                    //if (_DataFieldColLoc.LineNo != -1)
                    //{
                    //    sheet.Cells[i, _DataFieldColLoc.LineNo].Value = row[ARFields.Lines].ToString();
                    //}

                    //if (_DataFieldColLoc.Number != -1)
                    //{
                    //    sheet.Cells[i, _DataFieldColLoc.Number].Value = row[ARFields.Number].ToString();
                    //    if (wholeNoColorUse)
                    //    {
                    //        if (DataFunctions.IsWholeNumber(row[ARFields.Number].ToString()))
                    //        {
                    //            wholeNoRows.Add(i);
                    //        }
                    //    }


                    //}
                    //if (_DataFieldColLoc.NoCaption != -1)
                    //{
                    //    sheet.Cells[i, _DataFieldColLoc.NoCaption].Value = row[ARFields.NoCaption].ToString();
                    //}

                    //if (_DataFieldColLoc.SegText != -1)
                    //{
                        ExcelRange range = sheet.Cells[i, columnX];

                        // Convert HTML text to Excel with Highlights converted to Red Bold font
                        SetRichTextFromHtml(range, row[ARFields.Text].ToString(), FontName, FontSize);
                        //range.Style.WrapText = true;

                        sheet.Cells[i, columnX].Style.WrapText = true;
                      //  sheet.Cells[i, columnX].AutoFitColumns();
                       
                    //}

                    // Add Notes
                    //if (_DataFieldColLoc.Notes != -1)
                    //{
                    //    sheet.Cells[i, _DataFieldColLoc.Notes].Value = row[ARFields.Notes].ToString();
                    //}
                    //else if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() != NotesEnbedded.None) // Enbedded Notes
                    //{
                    //    string note = row[ARFields.Notes].ToString();
                    //    if (note.Length > 0)
                    //    {
                    //        int col = -1;
                    //        if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() == NotesEnbedded.Caption)
                    //        {
                    //            col = _DataFieldColLoc.Caption;
                    //        }
                    //        if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() == NotesEnbedded.LineNo)
                    //        {
                    //            col = _DataFieldColLoc.LineNo;
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

                    sheet.Row(i).CustomHeight = false; // test auto-height

                    i++; // row number
                } // AR data row <--

                //// Alternating Color rows
                //if ((bool)rowSettings[ExcelTemplateFields.AltColorRowsUse])
                //{
                //    SetAltColorRows(sheet, rowSettings[ExcelTemplateFields.AltColorRows].ToString(), dataRowStart);
                //}

                //// Set Whole Number rows (e.g. 1.0, 2.0, 3.0 ...)          
                //if (wholeNoColorUse)
                //{
                //    foreach (int rowNo in wholeNoRows)
                //    {
                //        SetRowColor(sheet, rowNo, rowSettings[ExcelTemplateFields.WholeNoColor].ToString());
                //    }
                //}



                package.Save();


                //Process.Start(pathFileAR);

            }

            return true;
        }


        public bool ExportAResults2Template(DataTable dtAR, string TemplateName, string ARTempatePath, string ExportPath, string resultFileName)
        {
            _ErrorMessage = string.Empty;

            // Get Settings
            string file =string.Concat(TemplateName, ".xml");
            string pathFile = Path.Combine(ARTempatePath, file);
            if (!File.Exists(pathFile)) // ToDo make an auto-download of template ?
            {
                _ErrorMessage = string.Concat("Unable to find Excel Template: ", ARTempatePath);
                return false;
            }

            DataSet dsSettings = LoadDatasetFromXml(pathFile);

            DataRow rowSettings = dsSettings.Tables[0].Rows[0];

            bool settingsColExists = dsSettings.Tables[0].Columns.Contains(ExcelTemplateFields.LocPage); // Added 8.16.2018

            Populate_DataFieldColLoc(rowSettings, settingsColExists);

            if (!Populate_MetadataLocations(rowSettings))
            {
                return false;
            }

            bool pageColExists = dtAR.Columns.Contains("Page"); // Added 8.16.2018

            Populate_ColorDef(rowSettings);

            string fileTemplate = string.Concat(TemplateName, ".xlsx");
            string fileAR = string.Concat(resultFileName, ".xlsx");

            string pathFileTemplate = Path.Combine(ARTempatePath, fileTemplate);
            string pathFileAR = Path.Combine(ExportPath, fileAR);

            if (!CopyTemplate2ExportFile(pathFileTemplate, pathFileAR))
            {
                return false;
            }

           FileInfo testTemplate = new FileInfo(pathFileAR);

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

               List <int> wholeNoRows = new List<int>();
               bool wholeNoColorUse =  (bool)rowSettings[ExcelTemplateFields.WholeNoColorUse];
               
               int dataRowStart = Convert.ToInt32(rowSettings[ExcelTemplateFields.DataRowStart].ToString());

               int i = dataRowStart; // row incremental 

               // Populate Excel Data Rows
               foreach (DataRow row in dtAR.Rows)
               {
               
                   if (_DataFieldColLoc.Caption != -1)
                   {
                        sheet.Cells[i, _DataFieldColLoc.Caption].Value = row[ARFields.Caption].ToString();
                   }
                   if (_DataFieldColLoc.Keywords != -1)
                   {
                        sheet.Cells[i, _DataFieldColLoc.Keywords].Value = row[ARFields.Keywords].ToString();
                   }
                   if (_DataFieldColLoc.LineNo != -1)
                   {
                        sheet.Cells[i, _DataFieldColLoc.LineNo].Value = row[ARFields.Lines].ToString();
                   }

                   // Added 8.16.2018
                   if (pageColExists && settingsColExists)
                   {
                       if (_DataFieldColLoc.Page != -1)
                       {
                           sheet.Cells[i, _DataFieldColLoc.Page].Value = row[ARFields.Page].ToString();
                       }
                   }

                   if (_DataFieldColLoc.Number != -1)
                   {
                        sheet.Cells[i, _DataFieldColLoc.Number].Value = row[ARFields.Number].ToString();
                       if (wholeNoColorUse)
                       {
                          if (DataFunctions.IsWholeNumber(row[ARFields.Number].ToString()))
                          {
                              wholeNoRows.Add(i);
                          }
                       }


                   }
                   if (_DataFieldColLoc.NoCaption != -1)
                   {
                     //   sheet.Cells[i, _DataFieldColLoc.NoCaption].Value = row[ARFields.NoCaption].ToString();
                       sheet.Cells[i, _DataFieldColLoc.NoCaption].Value = string.Concat(row[ARFields.Number].ToString(), "  ", row[ARFields.Caption].ToString()); // Changed Tom Lipscomb 10.16.2020
                   }

                   if (_DataFieldColLoc.SegText != -1)
                   {
                       ExcelRange range = sheet.Cells[i, _DataFieldColLoc.SegText];
                       
                       // Convert HTML text to Excel with Highlights converted to Red Bold font
                       SetRichTextFromHtml(range, row[ARFields.Text].ToString(), rowSettings[ExcelTemplateFields.FontName].ToString(), short.Parse(rowSettings[ExcelTemplateFields.FontSize].ToString()) );                      
                   }

                   // Add Notes
                   if (_DataFieldColLoc.Notes != -1)
                   {
                        sheet.Cells[i, _DataFieldColLoc.Notes].Value = row[ARFields.Notes].ToString();
                   }
                   else if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() != NotesEnbedded.None) // Enbedded Notes
                   {
                       string note = row[ARFields.Notes].ToString();
                       if (note.Length > 0)
                       {
                           int col = -1;
                           if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() == NotesEnbedded.Caption)
                           {
                                col = _DataFieldColLoc.Caption;
                           }
                           if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() == NotesEnbedded.LineNo)
                           {
                                col = _DataFieldColLoc.LineNo;
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
               } // AR data row <--

               // Alternating Color rows
               if ((bool)rowSettings[ExcelTemplateFields.AltColorRowsUse])
               {
                  SetAltColorRows(sheet, rowSettings[ExcelTemplateFields.AltColorRows].ToString(), dataRowStart);
               }
               
               // Set Whole Number rows (e.g. 1.0, 2.0, 3.0 ...)          
               if (wholeNoColorUse)
               {
                    foreach (int rowNo in wholeNoRows)
                    {
                        SetRowColor(sheet, rowNo, rowSettings[ExcelTemplateFields.WholeNoColor].ToString());
                    }
               }

               
 
               package.Save();

                    
               Process.Start(pathFileAR);

           }

           return true;
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


        /// <summary>
        /// ToDo !!!
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool ExportDAResults2Template(DataTable dtDAR, string TemplateName, string DARTempatePath, string ExportPath, string resultFileName) // Was ExportDAResults2Template in an earlier version
        {
            _ErrorMessage = string.Empty;

            // Get Settings
            string file = string.Concat(TemplateName, ".xml");
            string pathFile = Path.Combine(DARTempatePath, file);
            DataSet dsSettings = LoadDatasetFromXml(pathFile);

            DataRow rowSettings = dsSettings.Tables[0].Rows[0];

            bool settingsColExists = dsSettings.Tables[0].Columns.Contains(ExcelTemplateFields.LocPage); // Added 8.17.2018

            Populate_DataFieldColLoc(rowSettings, settingsColExists);

            if (!Populate_MetadataLocations(rowSettings))
            {
                return false;
            }

            bool pageColExists = dtDAR.Columns.Contains("Page"); // Added 8.17.2018

            Populate_ColorDef(rowSettings);

            Populate_DAROptions(rowSettings);

            string fileTemplate = string.Concat(TemplateName, ".xlsx");
            string fileDAR = string.Concat(resultFileName, ".xlsx");

            string pathFileTemplate = Path.Combine(DARTempatePath, fileTemplate);
            string pathFileDAR = Path.Combine(ExportPath, fileDAR);

            if (!CopyTemplate2ExportFile(pathFileTemplate, pathFileDAR))
            {
                return false;
            }

            FileInfo testTemplate = new FileInfo(pathFileDAR);

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

                List<int> wholeNoRows = new List<int>();
                bool wholeNoColorUse = (bool)rowSettings[ExcelTemplateFields.WholeNoColorUse];

                int dataRowStart = Convert.ToInt32(rowSettings[ExcelTemplateFields.DataRowStart].ToString());

                bool sentenceDARrow = true;

                int i = dataRowStart; // row incremental 

                // Populate Excel Data Rows
                foreach (DataRow row in dtDAR.Rows)
                {
                    if (row["isSegment"].ToString() == "1")
                    {
                        sentenceDARrow = false;
                    }
                    else
                    {
                        sentenceDARrow = true;
                    }

                    // Caption
                    if (!_DARCOptions.Caption_Excluded) // If NOT exclusions for Sentence rows via Template Settings
                    {
                        if (_DataFieldColLoc.Caption != -1)
                        {
                            sheet.Cells[i, _DataFieldColLoc.Caption].Value = row[ARFields.Caption].ToString();
                        }
                    }
                    else
                    {
                        if (!sentenceDARrow)
                        {
                            if (_DataFieldColLoc.Caption != -1)
                            {
                                sheet.Cells[i, _DataFieldColLoc.Caption].Value = row[ARFields.Caption].ToString();         
                            }
                        }
                    }

                    // Keywords
                    if (_DataFieldColLoc.Keywords != -1)
                    {
                        sheet.Cells[i, _DataFieldColLoc.Keywords].Value = row[ARFields.Keywords].ToString();
                    }

                    // Added 8.17.2018
                    if (pageColExists && settingsColExists)
                    {
                        if (_DataFieldColLoc.Page != -1)
                        {
                            sheet.Cells[i, _DataFieldColLoc.Page].Value = row[ARFields.Page].ToString();
                        }
                    }
 
                    // Number
                    if (!_DARCOptions.Number_Excluded) // If NOT exclusions for Sentence rows via Template Settings
                    {
                        if (_DataFieldColLoc.Number != -1)
                        {
                            sheet.Cells[i, _DataFieldColLoc.Number].Value = row[ARFields.Number].ToString();
                            if (wholeNoColorUse)
                            {
                                if (DataFunctions.IsWholeNumber(row[ARFields.Number].ToString()))
                                {
                                    wholeNoRows.Add(i);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!sentenceDARrow)
                        {
                            if (_DataFieldColLoc.Number != -1)
                            {
                                sheet.Cells[i, _DataFieldColLoc.Number].Value = row[ARFields.Number].ToString();      
                            }
                        }
                    }

                    // No + Caption
                    if (!_DARCOptions.NoCaption_Excluded) // If NOT exclusions for Sentence rows via Template Settings
                    {
                        if (_DataFieldColLoc.NoCaption != -1)
                        {
                            sheet.Cells[i, _DataFieldColLoc.NoCaption].Value = row[ARFields.NoCaption].ToString();
                        }
                    }
                    else
                    {
                        if (!sentenceDARrow)
                        {
                            if (_DataFieldColLoc.NoCaption != -1)
                            {
                                sheet.Cells[i, _DataFieldColLoc.NoCaption].Value = row[ARFields.NoCaption].ToString();
                            }
                        }
                    }


                    // Segment
                    if (_DataFieldColLoc.SegText != -1)
                    {
                        ExcelRange range = sheet.Cells[i, _DataFieldColLoc.SegText];

                        // Convert HTML text to Excel with Highlights converted to Red Bold font
                        SetRichTextFromHtml(range, row[ARFields.Text].ToString(), rowSettings[ExcelTemplateFields.FontName].ToString(), short.Parse(rowSettings[ExcelTemplateFields.FontSize].ToString()));
                    }

                    // Sentence
                    if (_DataFieldColLoc.SentText != -1)
                    {
                        ExcelRange range = sheet.Cells[i, _DataFieldColLoc.SentText];

                        // Convert HTML text to Excel with Highlights converted to Red Bold font
                        SetRichTextFromHtml(range, row[ARFields.Text].ToString(), rowSettings[ExcelTemplateFields.FontName].ToString(), short.Parse(rowSettings[ExcelTemplateFields.FontSize].ToString()));
                    }

                    // Add Notes
                    if (_DataFieldColLoc.Notes != -1)
                    {
                        sheet.Cells[i, _DataFieldColLoc.Notes].Value = row[ARFields.Notes].ToString();
                    }
                    else if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() != NotesEnbedded.None) // Enbedded Notes
                    {
                        string note = row[ARFields.Notes].ToString();
                        if (note.Length > 0)
                        {
                            int col = -1;
                            if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() == NotesEnbedded.Caption)
                            {
                                col = _DataFieldColLoc.Caption;
                            }
                            if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() == NotesEnbedded.LineNo)
                            {
                                col = _DataFieldColLoc.LineNo;
                            }
                            if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() == NotesEnbedded.NoCaption)
                            {
                                col = _DataFieldColLoc.NoCaption;
                            }
                            if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() == NotesEnbedded.Number)
                            {
                                col = _DataFieldColLoc.Number;
                            }
                            if (rowSettings[ExcelTemplateFields.NotesEmbedded].ToString() == NotesEnbedded.SegSent)
                            {
                                if (sentenceDARrow) // if row is a sentence row then use sentence location
                                {
                                    col = _DataFieldColLoc.SentText;
                                }
                                else
                                {
                                    col = _DataFieldColLoc.SegText;
                                }
                            }
                            

                            if (col != -1)
                            {
                                sheet.Cells[i, col].AddComment(note, _Metadata_YourName);
                                sheet.Cells[i, col].Comment.AutoFit = true;
                            }
                        }
                    } // Notes < --

                    sheet.Row(i).CustomHeight = false; // auto-height

                    // Segment/Paragraph rows Background color
                    if (_DARCOptions.SegPar_BkgrdColor_Use) // Should set Background color for Segment/Paragraph rows?
                    {
                        if (!sentenceDARrow) // Not Sentence row, then it must be a Segment/Paragraph row
                        {
                            SetRowColor(sheet, i, rowSettings[ExcelTemplateFields.SegTextBkColor].ToString());
                        }
                    }


                    i++; // row number
                } // DAR data row <--

 
                package.Save();


                Process.Start(pathFileDAR);

            }

            return true;
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
