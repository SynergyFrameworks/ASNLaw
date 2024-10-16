using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using Atebion.Excel.Output;


namespace WorkgroupMgr
{
    public class MatrixAllocations
    {
        public MatrixAllocations(string matrixPath)
        {
            _MatrixPath = matrixPath;

            ValidateFix();
        }

        private string _MatrixPath = string.Empty;

        private const string _MATRIX_ALLOCTIONS_XML = "MatrixAllocations.xml";
        private const string MATRIX_TEMPLATE_XLSX = "MatrixTemp.xlsx";

        private const string _EXT_ALLOCATIONS = ".allc"; // A list of allocations for each Document type, List and Reference Resource

        private string _ExportPath = string.Empty;
        private string _AutoPopPath = string.Empty;

        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        public bool ValidateFix()
        {
            _ErrorMessage = string.Empty;

            if (!Directory.Exists(_MatrixPath))
            {
                _ErrorMessage = string.Concat("Unable to locate the Matrix's Data Folder: ", _MatrixPath);
                return false;
            }

            string pathBackup = Path.Combine(_MatrixPath, "Backup");
            if (!CheckCreateDir(pathBackup))
            {
                return false;
            }

            _ExportPath = Path.Combine(_MatrixPath, "Export");
            if (!CheckCreateDir(_ExportPath))
            {
                return false;
            }

            _AutoPopPath = Path.Combine(_MatrixPath, "AutoPopPath");
            if (!CheckCreateDir(_AutoPopPath))
            {
                return false;
            }


            return true;
        }

        public bool RemoveNewMatrixRow(int MatrixRemovedRowNumber)
        {
            _ErrorMessage = string.Empty;

            string MatrixAllocationsPathFile = Path.Combine(_MatrixPath, "MatrixAllocations.xml");

            if (!File.Exists(MatrixAllocationsPathFile))
            {
                _ErrorMessage = string.Concat("Unable to find Matrix Allocations XML file: ", MatrixAllocationsPathFile);
                return false;
            }

            // ---> Remove all allocations for the removed row <---
            string cellValue = string.Empty;
            string column = string.Empty;
            int matrixRowNo = -1;

            bool changeOccurred = false;

            DataSet ds = DataFunctions.LoadDatasetFromXml(MatrixAllocationsPathFile);
            DataSet dsClone = ds.Copy();
            int i = 0;
            foreach (DataRow row in dsClone.Tables[0].Rows)
            {
                cellValue = row[MatrixAllocationsFields.Cell].ToString();

                matrixRowNo = getRowNoFromCell(cellValue, out column);

                if (matrixRowNo == MatrixRemovedRowNumber)
                {
                    ds.Tables[0].Rows.RemoveAt(i);    
                    changeOccurred = true;

                    i--;
                }
                //else if (matrixRowNo > MatrixRemovedRowNumber)
                //{
                //    matrixRowNo = matrixRowNo - 1;
                //    cellValue = string.Concat(column, matrixRowNo.ToString());
                //    ds.Tables[0].Rows[i][MatrixAllocationsFields.Cell] = cellValue;
                //    changeOccurred = true;
                //}

                i++;
            }

            ds.AcceptChanges();
            i = 0;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                cellValue = row[MatrixAllocationsFields.Cell].ToString();

                matrixRowNo = getRowNoFromCell(cellValue, out column);

                if (matrixRowNo > MatrixRemovedRowNumber)
                {
                    matrixRowNo = matrixRowNo - 1;
                    cellValue = string.Concat(column, matrixRowNo.ToString());
                    ds.Tables[0].Rows[i][MatrixAllocationsFields.Cell] = cellValue;
                    changeOccurred = true;
                }

                i++;
            }

            if (!changeOccurred)
                return true;

            string pathBackup = Path.Combine(_MatrixPath, "Backup");
            string backupPathFile = string.Empty;

            string[] cellValues;
            string cellNotation = string.Empty;
            string uid = string.Empty;
            string fileName_allc = string.Empty;
            StringBuilder sbNewValues = new StringBuilder();
            string[] files_allc = Directory.GetFiles(_MatrixPath, "*.allc");
            if (files_allc.Length > 0)
            {
                foreach (string file_allc in files_allc)
                {
                    sbNewValues.Clear();
                    List<string> lstValues = Files.ReadFile2List(file_allc);

                    foreach (string allocation in lstValues)
                    {
                        if (allocation.IndexOf('|') > 0)
                        {
                            cellValues = allocation.Split('|');
                            uid = cellValues[0];
                            cellNotation = cellValues[1];
                            matrixRowNo = getRowNoFromCell(cellNotation, out column);
                            if (matrixRowNo != MatrixRemovedRowNumber)
                            {
                                if (matrixRowNo != MatrixRemovedRowNumber)
                                {

                                    if (matrixRowNo > MatrixRemovedRowNumber)
                                    {
                                        matrixRowNo = matrixRowNo - 1;
                                        cellNotation = string.Concat(uid, "|", column, matrixRowNo.ToString());
                                    }           
                                        sbNewValues.AppendLine(cellNotation);
                                }
                                
                            }
                            
                        }
                    }

                    fileName_allc = Files.GetFileName(file_allc);
                    if (!Files.BackupFile(_MatrixPath, pathBackup, fileName_allc, out backupPathFile))
                    {
                        _ErrorMessage = string.Concat(_ErrorMessage, "   Error: ", Files.ErrorMessage);
                    }
                    File.Delete(file_allc);
                    Files.WriteStringToFile(sbNewValues.ToString(), file_allc);

                }
            }


            if (!Files.BackupFile(_MatrixPath, pathBackup, "MatrixAllocations.xml", out backupPathFile))
            {
                _ErrorMessage = Files.ErrorMessage;
            }

            ds.AcceptChanges();
            if (!DataFunctions.SaveDataXML(ds, MatrixAllocationsPathFile))
            {
                _ErrorMessage = DataFunctions._ErrorMessage;
                return false;
            }


            return true;
        }

        public bool InsertNewMatrixRow(int NewMatrixRowNumber)
        {
            _ErrorMessage = string.Empty;

            string MatrixAllocationsPathFile = Path.Combine(_MatrixPath, "MatrixAllocations.xml");

            if (!File.Exists(MatrixAllocationsPathFile))
            {
                _ErrorMessage = string.Concat("Unable to find Matrix Allocations XML file: ", MatrixAllocationsPathFile);
                return false;
            }

            string cellValue = string.Empty;
            string column = string.Empty;
            int matrixRowNo = -1;

            bool changeOccurred = false;

            DataSet ds = DataFunctions.LoadDatasetFromXml(MatrixAllocationsPathFile);
            

           // DataSet dsClone = ds.Clone();
            int i = 0;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                cellValue = row[MatrixAllocationsFields.Cell].ToString();

                matrixRowNo = getRowNoFromCell(cellValue, out column);

                if (matrixRowNo >= NewMatrixRowNumber)
                {
                    matrixRowNo = matrixRowNo + 1;
                    cellValue = string.Concat(column, matrixRowNo.ToString());
                    ds.Tables[0].Rows[i][MatrixAllocationsFields.Cell] = cellValue;

                    changeOccurred = true;
                }

                i++;
            }

            if (!changeOccurred)
                return true;

            string pathBackup = Path.Combine(_MatrixPath, "Backup");
            string backupPathFile = string.Empty;

            string[] cellValues;
            string cellNotation = string.Empty;
            string uid = string.Empty;
            string fileName_allc = string.Empty;
            StringBuilder sbNewValues = new StringBuilder();
            string[] files_allc = Directory.GetFiles(_MatrixPath, "*.allc");
            if (files_allc.Length > 0)
            {
                foreach (string file_allc in files_allc)
                {
                    sbNewValues.Clear();
                    List<string> lstValues = Files.ReadFile2List(file_allc);
                   
                    foreach (string allocation in lstValues)
                    {
                        if(allocation.IndexOf('|') > 0)
                        {
                            cellValues = allocation.Split('|');
                            uid = cellValues[0];
                            cellNotation = cellValues[1];
                            matrixRowNo = getRowNoFromCell(cellNotation, out column);
                            if (matrixRowNo >= NewMatrixRowNumber)
                            {
                                matrixRowNo = matrixRowNo + 1;
                                cellNotation = string.Concat(uid, "|", column, matrixRowNo.ToString());    
                            }

                            sbNewValues.AppendLine(cellNotation);
                        }
                    }

                    fileName_allc = Files.GetFileName(file_allc);
                    if (!Files.BackupFile(_MatrixPath, pathBackup, fileName_allc, out backupPathFile))
                    {
                        _ErrorMessage = string.Concat(_ErrorMessage, "   Error: ", Files.ErrorMessage);
                    }
                    File.Delete(file_allc);
                    Files.WriteStringToFile(sbNewValues.ToString(), file_allc);

                }
            }
            

           
            
            if (!Files.BackupFile(_MatrixPath, pathBackup, "MatrixAllocations.xml", out backupPathFile))
            {
                _ErrorMessage = Files.ErrorMessage;
            }

            ds.AcceptChanges();
            if (!DataFunctions.SaveDataXML(ds, MatrixAllocationsPathFile))
            {
                _ErrorMessage = DataFunctions._ErrorMessage;
                return false;
            }

            return true;
        }

        private int getRowNoFromCell(string CellValue, out string column)
        {
            column = string.Empty;

            if (CellValue.Length < 2)
                return -1;

            string rowNo = CellValue.Substring(1, CellValue.Length - 1);
            if (!DataFunctions.IsNumeric(rowNo))
                return -1;

            column = CellValue.Substring(0, 1);

            return Convert.ToInt32(rowNo);
        }

        /// <summary>
        /// Auto-Populates a new matrix from a parsed document, typically for Federal RFPs this is Section L (instructions)
        /// </summary>
        /// <param name="Item">Holds DocType Item, e.g. "C", "L", "M"</param>
        /// <param name="Column">Excel column letter, e.g. "B"</param>
        /// <param name="Source">Either "Analysis Results" or "Deep Analysis"</param>
        /// <param name="ContentType">Either "Text", "Number", "Caption", "NumberCaption"</param>
        /// <param name="RowDataStarts">Row number where the data starts</param>
        /// <param name="SourceName">Document Name</param>
        /// <param name="DocCurrentPath"></param>
        /// <returns></returns>
        public bool AutoAllocateDocType(string Item, string Column, string Source, string ContentType, int RowDataStarts, string SourceName, string DocCurrentPath, string MatrixPath, string userName) // Added 12.16.2017
        {
            _ErrorMessage = string.Empty;

            DataSet dsAllocations = GetAllocations(); // Get existing Allocation DataSet or if it doesn't exists, then it will be created

            int startingRow = dsAllocations.Tables[MatrixAllocationsFields.TableName].Rows.Count + RowDataStarts;

            string pathXML = string.Empty;
            string pathparsedFiles = string.Empty;
            string pathFileAnalysis = string.Empty;

            // Get Path & Files based on source
            if (Source == "Analysis Results")
            {
                pathparsedFiles = Path.Combine(DocCurrentPath, "ParseSec");
                pathXML = Path.Combine(pathparsedFiles, "XML");
                pathFileAnalysis = Path.Combine(pathXML, "ParseResults.xml");
            }
            else // Deep Analysis
            {
                pathparsedFiles = Path.Combine(DocCurrentPath, "Deep Analytics", "Current", "Parse Sentences");
                pathXML = Path.Combine(DocCurrentPath, "Deep Analytics", "Current", "XML");
                pathFileAnalysis = Path.Combine(pathXML, "Sentences.xml");
            }

            // Validate existance of Paths and XML file
            if (Source != "Deep Analysis")
            {
                if (!Directory.Exists(pathparsedFiles))
                {
                    _ErrorMessage = string.Concat("Unable to find ", Source, " parsed files path: ", pathparsedFiles);
                    return false;
                }
            }
            if (!Directory.Exists(pathXML))
            {
                _ErrorMessage = string.Concat("Unable to find ", Source, " XML path: ", pathXML);
                return false;
            }
            if (!File.Exists(pathFileAnalysis))
            {
                _ErrorMessage = string.Concat("Unable to find ", Source, " file: ", pathFileAnalysis);
                return false;
            }

            DataSet ds = DataFunctions.LoadDatasetFromXml(pathFileAnalysis); // Get Analysis Results or 
            if (ds == null)
            {
                _ErrorMessage = string.Concat("Unable to load data for ", Source, " file: ", pathFileAnalysis, "  -- Error: ", DataFunctions._ErrorMessage);
                return false;
            }

            if (ds.Tables[0].Rows.Count == 0) // Check if there has been analysis
            {
                _ErrorMessage = string.Concat("No analysis data was found for ", Source, " file: ", pathFileAnalysis);
                return false;
            }

            string matrixDataPathFile = Path.Combine(MatrixPath, "MatrixTempData.xlsx");
            string matrixPathFile = Path.Combine(MatrixPath, "MatrixTemp.xlsx");

            if (!File.Exists(matrixDataPathFile))
            {
                if (File.Exists(matrixPathFile))
                {
                    File.Copy(matrixPathFile, matrixDataPathFile);
                }
                else
                {
                    _ErrorMessage = string.Concat("Unable to find new Matrix file: ", matrixPathFile);
                    return false;
                }
            }
            else
            {
                if (File.Exists(matrixDataPathFile))
                {
                    File.Copy(matrixDataPathFile, matrixPathFile);
                }
                else
                {
                    _ErrorMessage = string.Concat("Unable to find new Matrix Data file: ", matrixPathFile);
                    return false;
                }
            }

            System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();

            DataTable dtAutoPopulate4XLSX = CreateTable_AutoPopulateAllocations();

            string itemValue = string.Empty;
            string uid = string.Empty;
            string parsedFile = string.Empty;
            string parsedPathFile = string.Empty;
            string rtfText = string.Empty;

            int i = dsAllocations.Tables[MatrixAllocationsFields.TableName].Rows.Count;
            string cell = string.Empty;
            int xlsxRowNo = RowDataStarts;
            string content = string.Empty;

            string allocationBy = string.Concat("Auto-Allocation by ", userName);

            StringBuilder sbAllocations = new StringBuilder();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                uid = row[MatrixAllocationsFields.UID].ToString();

                content = string.Empty;   

                if (Source != "Analysis Results") // Deep Analysis
                {
                    content = row["Sentence"].ToString();

                    if (ContentType == "Text")
                    {
                        itemValue = content;
                    }
                    else if (ContentType == "Number")
                    {
                        itemValue = row["Number"].ToString();
                    }
                    else if (ContentType == "Caption")
                    {
                        itemValue = row["Caption"].ToString();
                    }
                    else if (ContentType == "No & Caption")
                    {
                        itemValue = string.Concat(row["Number"].ToString(), "  ", row["Caption"].ToString());
                    }
                }
                else // Analysis Results
                {
                    parsedFile = string.Concat(uid, ".rtf");
                    parsedPathFile = Path.Combine(pathparsedFiles, parsedFile);

                    if (File.Exists(parsedPathFile))
                    {
                        rtfText = System.IO.File.ReadAllText(parsedPathFile);
                        rtBox.Rtf = rtfText;
                        content = rtBox.Text.Trim();
                        
                    }
                    else
                    {
                        content = string.Concat("Error: Unable to find file: ", parsedPathFile);
                    }        

                    if (ContentType == "Text")
                    {
                        itemValue = content;
                    }
                    else if (ContentType == "Number")
                    {
                        itemValue = row["Number"].ToString();
                    }
                    else if (ContentType == "Caption")
                    {
                        itemValue = row["Caption"].ToString();
                    }
                    else if (ContentType == "No & Caption")
                    {
                        itemValue = string.Concat(row["Number"].ToString(), "  ", row["Caption"].ToString());
                    }
         
                    
                }

                //if (itemValue == string.Empty)
                //{
                //    content = row["Text"].ToString();
                //}



                // Populate DataTable for auto-populate the matrix
                DataRow rowXLSX = dtAutoPopulate4XLSX.NewRow();
                rowXLSX[MatrixAllocationsFields.Text] = itemValue;
                dtAutoPopulate4XLSX.Rows.Add(rowXLSX);

                // Populate the Settings DataSet
                cell = string.Concat(Column, xlsxRowNo.ToString());
                DataRow rowAllocation = dsAllocations.Tables[MatrixAllocationsFields.TableName].NewRow();
                rowAllocation[MatrixAllocationsFields.UID] = i;
                rowAllocation[MatrixAllocationsFields.SourceType] = "DocType";
                rowAllocation[MatrixAllocationsFields.SourceName] = SourceName;
                rowAllocation[MatrixAllocationsFields.SourceUID] = uid;
                rowAllocation[MatrixAllocationsFields.Cell] = cell;
                rowAllocation[MatrixAllocationsFields.Column] = Column;
                rowAllocation[MatrixAllocationsFields.Text] = itemValue;
                rowAllocation[MatrixAllocationsFields.DocTypeContentType] = Item;
                rowAllocation[MatrixAllocationsFields.DocTypeSource] = Source;
                rowAllocation[MatrixAllocationsFields.Content] = content;
                rowAllocation[MatrixAllocationsFields.AllocatedBy] = allocationBy;
                rowAllocation[MatrixAllocationsFields.AllocatedDateTime] = DateTime.Now;
                dsAllocations.Tables[MatrixAllocationsFields.TableName].Rows.Add(rowAllocation);

                // Populate for Allocations file
                sbAllocations.AppendLine(string.Concat(uid, "|", Column, xlsxRowNo.ToString()));

                xlsxRowNo++;
                i++;
            }

            // Populate Matrix (xlsx File)
            int xColumn = GetColumnNumber(Column);
            if (xColumn == -1)
            {
                _ErrorMessage = string.Concat("Unable to convert Letter column '", Column, "' to a numeric column.");
                return false;
            }

            short fontsize = 10;
            ExcelOutput excelOutput = new ExcelOutput();
            bool result = excelOutput.ExportAResults2MatrixTemplate(dtAutoPopulate4XLSX, xColumn, RowDataStarts, "Calibri", fontsize, matrixDataPathFile);
            if (!result)
            {
                _ErrorMessage = string.Concat("Unable to Auto-Allocate due to an error.  Error: ", excelOutput.ErrorMessage);
                return false;
            }
            File.Copy(matrixDataPathFile, matrixPathFile, true);


            // Save MatrixAllocations.xml
            string MatrixAllocationsPathFile = Path.Combine(MatrixPath, "MatrixAllocations.xml");
            if (File.Exists(MatrixAllocationsPathFile))
                Files.SetFileName2Obsolete(MatrixAllocationsPathFile);

            DataFunctions.SaveDataXML(dsAllocations, MatrixAllocationsPathFile);

            // Save Allocation File
            string allocationFile = string.Concat(SourceName, "_DocType.allc");
            string allocationPathFile = Path.Combine(MatrixPath, allocationFile);
            Files.WriteStringToFile(sbAllocations.ToString(), allocationPathFile);
            

            return true;
        }

        private int GetColumnNumber(string columnLetter)
        {
            string[] columns = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            int i = 1;
            foreach (string col in columns)
            {
                if (columnLetter == col)
                {
                    return i;
                }
                i++;
            }

            return -1;
        }


        public DataSet GetAllocations()
        {
            string pathFile = Path.Combine(_MatrixPath, _MATRIX_ALLOCTIONS_XML);
            if (File.Exists(pathFile))
            {
                DataSet ds = DataFunctions.LoadDatasetFromXml(pathFile);
                if (ds == null)
                {
                    _ErrorMessage = DataFunctions._ErrorMessage;
                    return null;
                }

                return ds;
            }
            else
            {
                DataTable dt = CreateTable_Allocations();
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);

                return ds;
            } 
        }

        public bool SaveAllocations(DataSet ds)
        {
            _ErrorMessage = string.Empty;

            string backupPathFile = string.Empty;
            string pathBackup = Path.Combine(_MatrixPath, "Backup");
            string pathAllocationFile = Path.Combine(_MatrixPath, _MATRIX_ALLOCTIONS_XML);
            if (CheckCreateDir(pathBackup))
            {
                if (File.Exists(pathAllocationFile))
                {
                    Files.BackupFile(_MatrixPath, pathBackup, _MATRIX_ALLOCTIONS_XML, out backupPathFile);
                    File.Delete(pathAllocationFile);
                }

                //DataSet ds = new DataSet();
                //ds.Tables.Add(dt);

                DataFunctions.SaveDataXML(ds, pathAllocationFile);

                return true;
            }

            return false;
        }

        public List<string> GetAllocationItems(string SourceName, string SourceType)
        {
            string file = string.Concat(SourceName, "_", SourceType, _EXT_ALLOCATIONS);
            string pathFile = Path.Combine(_MatrixPath, file);
            if (File.Exists(pathFile))
            {
                return Files.ReadFile2List(pathFile);
            }

           List<string> lst = new List<string>(); // if list file not found then return an empty list

           return lst;
        }

        public bool SaveAllocationItems(List<string> lst, string SourceName, string SourceType)
        {
            string backupPathFile = string.Empty;
            string file = string.Concat(SourceName, "_", SourceType, _EXT_ALLOCATIONS);
            string pathFile = Path.Combine(_MatrixPath, file);
            if (File.Exists(pathFile))
            {
                string pathBackup = Path.Combine(_MatrixPath, "Backup");
                Files.BackupFile(_MatrixPath, pathBackup, file, out backupPathFile);
                File.Delete(pathFile);
            }

            Files.WriteStringToFile(lst, pathFile);

            return true;
        }

        public string GetExportMatrix_New()
        {
            _ErrorMessage = string.Empty;

            string backupPathFile = string.Empty;

            if (!Files.BackupFile(_MatrixPath, _ExportPath, MATRIX_TEMPLATE_XLSX, out backupPathFile))
            {
                _ErrorMessage = Files.ErrorMessage;
                return string.Empty;
            }

            return backupPathFile;

        }

        private DataTable CreateTable_Allocations()
        {
            DataTable dt = new DataTable(MatrixAllocationsFields.TableName);

            dt.Columns.Add(MatrixAllocationsFields.UID, typeof(int));
            dt.Columns.Add(MatrixAllocationsFields.SourceType, typeof(string));
            dt.Columns.Add(MatrixAllocationsFields.SourceName, typeof(string));

            dt.Columns.Add(MatrixAllocationsFields.SourceUID, typeof(string));
            dt.Columns.Add(MatrixAllocationsFields.Cell, typeof(string));
            dt.Columns.Add(MatrixAllocationsFields.Column, typeof(string));
            dt.Columns.Add(MatrixAllocationsFields.Text, typeof(string));
            dt.Columns.Add(MatrixAllocationsFields.DocTypeContentType, typeof(string));
            dt.Columns.Add(MatrixAllocationsFields.DocTypeSource, typeof(string));
            dt.Columns.Add(MatrixAllocationsFields.Content, typeof(string)); // Used when Number and/or Caption is the Content Type, holds the actual text assocated with the Number and Caption -- Added 12.11.2017
            dt.Columns.Add(MatrixAllocationsFields.AllocatedBy, typeof(string));
            dt.Columns.Add(MatrixAllocationsFields.AllocatedDateTime, typeof(DateTime));

            return dt;
        }

        private DataTable CreateTable_AutoPopulateAllocations() // Added 12.13.2017
        {
            DataTable dt = new DataTable(MatrixAllocationsFields.TableName);
            dt.Columns.Add(MatrixAllocationsFields.Text, typeof(string));

            return dt;
        }




        /// <summary>
        /// Checks if folder exists, otherwise attempts to create folder
        /// </summary>
        /// <param name="dir">Folder</param>
        /// <returns>True if folder exists or was created. False if not able to create folder</returns>
        public bool CheckCreateDir(string dir)
        {
            _ErrorMessage = string.Empty;

            try
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.Message;
                return false;
            }

            return true;
        }
    }
}
