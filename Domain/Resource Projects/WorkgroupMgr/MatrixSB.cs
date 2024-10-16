using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using Atebion.Word.Template;

namespace WorkgroupMgr
{
    public class MatrixSB
    {
        public MatrixSB(string sbTemplatePath, string matrixPath)
        {
            _SBTemplatePath = sbTemplatePath;
            _MatrixPath = matrixPath;

            ValidateFix();

        }

        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        private string _SBTemplatePath = string.Empty;
        public string SBTemplatePath
        {
            get { return _SBTemplatePath; }
        }

        private string _MatrixPath = string.Empty;
        public string MatrixPath
        {
            get { return _MatrixPath; }
        }

        private string _MatrixSBPath = string.Empty;
        public string MatrixSBPath
        {
            get { return _MatrixSBPath; }
        }

        private string _MatrixSBPathBackup = string.Empty;
        public string MatrixSBPathBackup
        {
            get { return _MatrixSBPathBackup; }
        }

        private DataSet _dsSBMatrix;
        public DataSet dsSBMatrix
        {
            get { return _dsSBMatrix; }
        }

        private int _NewSBUID;
        public int NewSBUID
        {
            get {return _NewSBUID;}
        }

        private string _NewSBFile_docx = string.Empty;
        public string NewSBFile_docx
        {
            get { return _NewSBFile_docx; }
        }



        private const string SB_MATRIX_XML = "SBMatrix.xml";
        private string _Path_SB_MATRIX_XML = string.Empty;

        public string[] Columns = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };


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

            _MatrixSBPath = Path.Combine(_MatrixPath, "Storyboards");
            if (!Directory.Exists(_MatrixSBPath))
            {
                try
                {
                    Directory.CreateDirectory(_MatrixSBPath);

                }
                catch (Exception ex)
                {
                    _ErrorMessage = string.Concat("Unable to create Matrix Storyboard folder: ", _MatrixSBPath, "    Error: ", ex.Message);
                    return false;
                }
            }

            _MatrixSBPathBackup = Path.Combine(_MatrixSBPath, "Backup");
            if (!Directory.Exists(_MatrixSBPathBackup))
            {
                try
                {
                    Directory.CreateDirectory(_MatrixSBPathBackup);

                }
                catch (Exception ex2)
                {
                    _ErrorMessage = string.Concat("Unable to create Matrix Storyboard Backup folder: ", _MatrixSBPathBackup, "    Error: ", ex2.Message);
                    return false;
                }
            }

            _Path_SB_MATRIX_XML = Path.Combine(_MatrixSBPath, SB_MATRIX_XML);
            if (!File.Exists(_Path_SB_MATRIX_XML))
            {
                CreateEmptyDataSet();
                if (!File.Exists(_Path_SB_MATRIX_XML))
                {
                    _ErrorMessage = DataFunctions._ErrorMessage;
                    return false;
                }
            }
            else
            {
                _dsSBMatrix = DataFunctions.LoadDatasetFromXml(_Path_SB_MATRIX_XML);
            }



            return true;
        }

        public bool SBExists(string SBName)
        {
            string[] files = Directory.GetFiles(_MatrixSBPath, "*.docx");

            foreach (string file in files)
            {
                if (SBName.ToUpper() == file.ToUpper())
                {
                    return true;
                }
            }

            return false;
        }

        public bool SBFileExists(string fileName)
        {

            string pathFile = Path.Combine(_MatrixSBPath, fileName);
            return File.Exists(pathFile);

            //string existingFileName = string.Empty;

            //string[] files = Directory.GetFiles(_MatrixSBPath);

            //foreach (string file in files)
            //{
            //    existingFileName = Files.GetFileName(file);
            //    if (fileName == existingFileName)
            //    {
            //        return true;
            //    }
            //}

          //  return false;
        }

        public string GetNextSBName()
        {
            string newFile = string.Empty;
            string newFileName = string.Empty;

            for (int i = 1; i < 32766; i++)
            {
                newFile = string.Concat("SB_", i.ToString(), ".docx");
                if (!SBFileExists(newFile))
                {
                    newFileName = Files.GetFileNameWOExt(newFile);

                    return newFileName;
                }

            }

            return newFile;
        }

        public string[] GetSBNames(string MatrixTemplateName)
        {
            SBMgr sbMgr = new SBMgr(_SBTemplatePath);

            string[] SBNames = sbMgr.GetSBNames(MatrixTemplateName);
            if (SBNames == null)
            {
                if (sbMgr.ErrorMessage.Length > 0)
                {
                    _ErrorMessage = sbMgr.ErrorMessage;
                }
                else
                {
                    _ErrorMessage = string.Concat("Unable to find any Storyboard Templates associated with the Matrix Template '", MatrixTemplateName, "'");
                }

                return null;
            }

            return SBNames;
        }

        public string[] GetMatrixRows(int SBUID)
        {
            List<string> matrixRows = new List<string>();
            string filter = string.Concat(MatrixSBFields.SB_UID, " = ", SBUID);
            DataRow[] rows = _dsSBMatrix.Tables[MatrixSBFields.TableName_SBMatrixRows].Select(filter);

            string rowNo = string.Empty;
            foreach (DataRow row in rows)
            {
                rowNo = row[MatrixSBFields.MatrixRow].ToString();
                matrixRows.Add(rowNo);
            }

            return matrixRows.ToArray();
        }

        public string GetSBViewFileDocx(int SBUID)
        {
            _ErrorMessage = string.Empty;

            string filter = string.Concat(MatrixSBFields.SB_UID, " = ", SBUID);
            DataRow[] rows = _dsSBMatrix.Tables[MatrixSBFields.TableName_SBs].Select(filter);

            if (rows.Length == 0)
            {
                _ErrorMessage = string.Concat("Unable to find the selected Storyboard UID: ", SBUID.ToString());
                return string.Empty;
            }

            string SBRealName = rows[0][MatrixSBFields.Name].ToString();
            string SBRealFile = string.Concat(SBRealName, ".docx");
        
            string SBViewFile = string.Concat(SBRealName, "_View.docx");

            string SBRealPathFile = Path.Combine(_MatrixSBPath, SBRealFile);
            if (!File.Exists(SBRealPathFile))
            {
                _ErrorMessage = string.Concat("Unable to find Storybord document: ", SBRealPathFile);
                return string.Empty;
            }

            string SBViewPathFile = Path.Combine(_MatrixSBPath, SBViewFile);

            bool updateViewFile = false;
            if (!File.Exists(SBViewPathFile))
            {
                updateViewFile = true;
            }
            else
            {
                DateTime ftimeReal = File.GetLastWriteTime(SBRealPathFile);
                DateTime ftimeView = File.GetLastWriteTime(SBViewPathFile);

                if (ftimeReal != ftimeView)
                {
                    updateViewFile = true;
                }
            }

            if (updateViewFile)
            {
                try
                {
                    //if (File.Exists(SBViewPathFile))
                    //{
                    //    File.Delete(SBViewPathFile);
                    //}

                    File.Copy(SBRealPathFile, SBViewPathFile, true);
                }
                catch (Exception ex)
                {
                    _ErrorMessage = string.Concat("An error occurred while updating the Storyboard file.    Error: ", ex.Message);
                    return string.Empty;
                }
            }

            return SBViewPathFile;
            
        }

        public string GetSBFileDocx(int SBUID)
        {
            _ErrorMessage = string.Empty;

            string filter = string.Concat(MatrixSBFields.SB_UID, " = ", SBUID);
            DataRow[] rows = _dsSBMatrix.Tables[MatrixSBFields.TableName_SBs].Select(filter);

            if (rows.Length == 0)
            {
                _ErrorMessage = string.Concat("Unable to find the selected Storyboard UID: ", SBUID.ToString());
                return string.Empty;
            }

            string SBRealName = rows[0][MatrixSBFields.Name].ToString();
            string SBRealFile = string.Concat(SBRealName, ".docx");

            string SBRealPathFile = Path.Combine(_MatrixSBPath, SBRealFile);
            if (!File.Exists(SBRealPathFile))
            {
                _ErrorMessage = string.Concat("Unable to find Storybord document: ", SBRealPathFile);
                return string.Empty;
            }

            return SBRealPathFile;

        }

        private void CreateEmptyDataSet()
        {
            _dsSBMatrix = new DataSet();

            DataTable dtSB = new DataTable(MatrixSBFields.TableName_SBs);

            dtSB.Columns.Add(MatrixSBFields.SB_UID, typeof(int));
            dtSB.Columns.Add(MatrixSBFields.Name, typeof(string));
            dtSB.Columns.Add(MatrixSBFields.SBTemplateName, typeof(string));
            dtSB.Columns.Add(MatrixSBFields.Description, typeof(string));
            dtSB.Columns.Add(MatrixSBFields.CreatedBy, typeof(string));
            dtSB.Columns.Add(MatrixSBFields.CreatedDateTime, typeof(DateTime));

            DataTable dtSBMatrix = new DataTable(MatrixSBFields.TableName_SBMatrixRows);
            dtSBMatrix.Columns.Add(MatrixSBFields.SBMatrix_UID, typeof(int));
            dtSBMatrix.Columns.Add(MatrixSBFields.SB_UID, typeof(int));
            dtSBMatrix.Columns.Add(MatrixSBFields.MatrixRow, typeof(int));

            _dsSBMatrix.Tables.Add(dtSB);
            _dsSBMatrix.Tables.Add(dtSBMatrix);

            _dsSBMatrix.Relations.Add(
                _dsSBMatrix.Tables[MatrixSBFields.TableName_SBs].Columns[MatrixSBFields.SB_UID],
                _dsSBMatrix.Tables[MatrixSBFields.TableName_SBMatrixRows].Columns[MatrixSBFields.SB_UID]);

            DataFunctions.SaveDataXML(_dsSBMatrix, _Path_SB_MATRIX_XML);

        }


        public DataTable GetJoinTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(MatrixSBFields.SB_UID, typeof(int));
            dt.Columns.Add(MatrixSBFields.SBMatrix_UID, typeof(int));
            dt.Columns.Add(MatrixSBFields.Name, typeof(string));
            dt.Columns.Add(MatrixSBFields.MatrixRow, typeof(int));
            dt.Columns.Add(MatrixSBFields.Description, typeof(string));
            dt.Columns.Add(MatrixSBFields.CreatedBy, typeof(string));
            dt.Columns.Add(MatrixSBFields.CreatedDateTime, typeof(DateTime));

            var result = from dataRows1 in _dsSBMatrix.Tables[MatrixSBFields.TableName_SBs].AsEnumerable()
                         join dataRows2 in _dsSBMatrix.Tables[MatrixSBFields.TableName_SBMatrixRows].AsEnumerable()
                         on dataRows1[MatrixSBFields.SB_UID] equals dataRows2[MatrixSBFields.SB_UID]

                        select dt.LoadDataRow(new object[]
                        {
                        dataRows1[MatrixSBFields.SB_UID],
                        dataRows2[MatrixSBFields.SBMatrix_UID],
                        dataRows1[MatrixSBFields.Name],
                        dataRows1[MatrixSBFields.Description],
                        dataRows1[MatrixSBFields.CreatedBy],
                        dataRows1[MatrixSBFields.CreatedDateTime],
                        dataRows2[MatrixSBFields.MatrixRow]
                        }, false);

            dt = result.CopyToDataTable();

            return dt;

        }

        

        public DataTable GetJoinTable_All()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(MatrixSBFields.SB_UID, typeof(int));
            dt.Columns.Add(MatrixSBFields.SBMatrix_UID, typeof(int));
            dt.Columns.Add(MatrixSBFields.Name, typeof(string));
            dt.Columns.Add(MatrixSBFields.MatrixRow, typeof(int));
            dt.Columns.Add(MatrixSBFields.Description, typeof(string));
            dt.Columns.Add(MatrixSBFields.CreatedBy, typeof(string));
            dt.Columns.Add(MatrixSBFields.CreatedDateTime, typeof(DateTime));

            var result = from dataRows1 in _dsSBMatrix.Tables[MatrixSBFields.TableName_SBs].AsEnumerable()
                         join dataRows2 in _dsSBMatrix.Tables[MatrixSBFields.TableName_SBMatrixRows].AsEnumerable()
                         on dataRows1[MatrixSBFields.SB_UID] equals dataRows2[MatrixSBFields.SB_UID]

                         select dt.LoadDataRow(new object[]
                        {
                        dataRows1[MatrixSBFields.SB_UID],
                        dataRows2[MatrixSBFields.SBMatrix_UID],
                        dataRows1[MatrixSBFields.Name],
                        dataRows1[MatrixSBFields.Description],
                        dataRows1[MatrixSBFields.CreatedBy],
                        dataRows1[MatrixSBFields.CreatedDateTime],
                        dataRows2[MatrixSBFields.MatrixRow]
                        }, false);

            dt = result.CopyToDataTable();

            return dt;

        }

        public DataTable GetJoinTable_SB(int sb_UID)
        {

            DataTable dt = GetJoinTable_All();

            var rows = from row in dt.AsEnumerable()
                             where row.Field<int>(MatrixSBFields.SB_UID) == sb_UID
                             select row;

            DataTable dt2 = rows.CopyToDataTable();

            return dt2;

        }

        public DataTable GetJoinTable_Matrix(int matrixRow)
        {

            DataTable dt = GetJoinTable_All();

            var rows = from row in dt.AsEnumerable()
                       where row.Field<int>(MatrixSBFields.MatrixRow) == matrixRow
                       select row;

            DataTable dt2 = rows.CopyToDataTable();

            return dt2;

        }

        public DataTable GetFieldsEmptyTable()
        {
            DataTable dt = new DataTable();

            string[] fields = GetAvailableFields();

            foreach (string field in fields)
            {
                dt.Columns.Add(field, typeof(string));
            }

            return dt;
        }


        public string[] GetAvailableFields()
        {
            string availableFields = "DocAnalyzer_ProjName|MatrixColumn_A|MatrixColumn_B|MatrixColumn_C|MatrixColumn_D|MatrixColumn_E|MatrixColumn_F|MatrixColumn_G|MatrixColumn_H|MatrixColumn_I|MatrixColumn_J|MatrixColumn_K|MatrixColumn_L|MatrixColumn_M|MatrixColumn_N|MatrixColumn_O|MatrixColumn_P|MatrixColumn_Q|MatrixColumn_R|MatrixColumn_S|MatrixColumn_T|MatrixColumn_U|MatrixColumn_V|MatrixColumn_W|MatrixColumn_X|MatrixColumn_Y|MatrixColumn_Z";

            string[] fields = availableFields.Split('|');

            return fields;
        }

        public DataRow GetSBDataRow(int SBUID)
        {
            _ErrorMessage = string.Empty;

            string filter = string.Concat(MatrixSBFields.SB_UID, " = ", SBUID);
            DataRow[] rows2 = _dsSBMatrix.Tables[MatrixSBFields.TableName_SBs].Select(filter);
            if (rows2.Length > 0)
            {
                return rows2[0]; // Should only be one row
            }

            _ErrorMessage = string.Concat("Unable to find Storyboard data for SB UID: ", SBUID.ToString());
            return null;
        }

        public string[] GetRowsAlreadyUsed(string[] SelectedRowNos, int Except4SB_UID)
        {
            List<string> lstFoundUsedRows = new List<string>();

            int rowNumber;
            string filter = string.Empty;
            int SBUID;
            string SBName = string.Empty;
            DataRow sbRow;

            foreach (string matrixRow in SelectedRowNos)
            {
                rowNumber = Convert.ToInt32(matrixRow);
                filter = string.Concat(MatrixSBFields.MatrixRow, " = ", rowNumber, " AND ", MatrixSBFields.SB_UID, " <> ", Except4SB_UID);
                DataRow[] rows = _dsSBMatrix.Tables[MatrixSBFields.TableName_SBMatrixRows].Select(filter);
                if (rows.Length != 0)
                {
                    foreach (DataRow row in rows)
                    {
                        SBUID = (int)row[MatrixSBFields.SB_UID];
                        sbRow = GetSBDataRow(SBUID);
                        SBName = sbRow[MatrixSBFields.Name].ToString();

                        lstFoundUsedRows.Add(string.Concat(rowNumber.ToString(), "|", SBName));
                    }
                }
            }

            return lstFoundUsedRows.ToArray();
        }

        public string[] GetRowsAlreadyUsed(string[] SelectedRowNos)
        {
            List<string> lstFoundUsedRows = new List<string>();
            
            int rowNumber;
            string filter = string.Empty;
            int SBUID;
            string SBName = string.Empty;
            DataRow sbRow;

            foreach (string matrixRow in SelectedRowNos)
            {
                rowNumber = Convert.ToInt32(matrixRow);
                filter = string.Concat(MatrixSBFields.MatrixRow, " = ", rowNumber);
                DataRow[] rows = _dsSBMatrix.Tables[MatrixSBFields.TableName_SBMatrixRows].Select(filter);
                if (rows.Length != 0)
                {
                    foreach (DataRow row in rows)
                    {
                        SBUID = (int) row[MatrixSBFields.SB_UID];
                        sbRow = GetSBDataRow(SBUID);
                        SBName = sbRow[MatrixSBFields.Name].ToString();

                        lstFoundUsedRows.Add(string.Concat(rowNumber.ToString(), "|", SBName));
                    }
                }
            }

            return lstFoundUsedRows.ToArray();
        }

        public bool UpdateSB(int SBUID, string SBName, DataTable dtSBData, string SBTemplateName, string description, string[] SelectedRowNos, string user)
        {
            _ErrorMessage = string.Empty;

            string sbTemplateFile = "SBTemp.docx";
            string sbTemplatePathFile = Path.Combine(_SBTemplatePath, SBTemplateName, sbTemplateFile);

            // Check of SB Template Exists
            if (!File.Exists(sbTemplatePathFile))
            {
                _ErrorMessage = string.Concat("Unable to find the selected Storyboard Template: ", sbTemplatePathFile);
                return false;
            }

            string backupPathFile = string.Empty;
            string sbFile = string.Concat(SBName, ".docx");
            _NewSBFile_docx = Path.Combine(_MatrixSBPath, sbFile);

            // Check if SB already exists, if so back it up and delete
            if (File.Exists(_NewSBFile_docx))
            {
                Files.BackupFile(_MatrixSBPath, _MatrixSBPathBackup, sbFile, out backupPathFile);
                try
                {
                    File.Delete(_NewSBFile_docx);
                }
                catch (Exception ex)
                {
                    string errMsg = string.Concat("Unable to remove a previous version of a Storyboard. The file may be locked open. Please close document is open     Error: ", ex.Message);
                    return false;

                    // ToDo add lock check -- see: https://stackoverflow.com/questions/317071/how-do-i-find-out-which-process-is-locking-a-file-using-net 
                }
            }

            string sbViewFile = string.Concat(SBName, "_View.docx");
            string sbVewFile_docx = Path.Combine(_MatrixSBPath, sbViewFile);
            if (File.Exists(sbVewFile_docx))
            {
                try
                {
                    File.Delete(sbViewFile);
                }
                catch (Exception ex)
                {
                    string errMsg = string.Concat("Unable to remove a previous version of a View-Only Storyboard. The view file may be locked open. Please close document is open     Error: ", ex.Message);
                    return false;

                    // ToDo add lock check -- see: https://stackoverflow.com/questions/317071/how-do-i-find-out-which-process-is-locking-a-file-using-net 
                }
            }

            WordDataMerge word = new WordDataMerge(sbTemplatePathFile, _NewSBFile_docx);
            word.DataSource = dtSBData;
            word.GenerateWordFile();


            // Get DataSet
            _dsSBMatrix = DataFunctions.LoadDatasetFromXml(_Path_SB_MATRIX_XML);
            if (_dsSBMatrix == null)
            {
                _ErrorMessage = DataFunctions._ErrorMessage;
                return false;
            }

            // Get Selected SB Row
            string filter = string.Concat(MatrixSBFields.SB_UID, " = ", SBUID);
            DataRow[] rows = _dsSBMatrix.Tables[MatrixSBFields.TableName_SBs].Select(filter);
            if (rows.Length == 0)
            {
                _ErrorMessage = string.Concat("Unable to find Storyboard data for SB UID: ", SBUID.ToString());
                return false;
            }

            // Update selected SB row
            rows[0][MatrixSBFields.SBTemplateName] = SBTemplateName;
            rows[0][MatrixSBFields.Description] = description;
            rows[0][MatrixSBFields.CreatedBy] = user;
            rows[0][MatrixSBFields.CreatedDateTime] = DateTime.Now;

            _dsSBMatrix.Tables[MatrixSBFields.TableName_SBs].AcceptChanges();


            // Find and Remove Previous Matrix Rows allocations
            DataRow[] rows2 = _dsSBMatrix.Tables[MatrixSBFields.TableName_SBMatrixRows].Select(filter);
            if (rows2.Length > 0)
            {
                foreach (DataRow xrow in rows2)
                {
                    xrow.Delete();
                }
            }

            _dsSBMatrix.AcceptChanges();
            int sbMatrixRowUID;

            // Insert the new Matrix Rows allocations
            DataRow rowMatrix;
            foreach (string matrixRow in SelectedRowNos)
            {
                rowMatrix = _dsSBMatrix.Tables[MatrixSBFields.TableName_SBMatrixRows].NewRow();
                sbMatrixRowUID = DataFunctions.GetNewUID(_dsSBMatrix.Tables[MatrixSBFields.TableName_SBMatrixRows], MatrixSBFields.SBMatrix_UID);
                rowMatrix[MatrixSBFields.SBMatrix_UID] = sbMatrixRowUID;
                rowMatrix[MatrixSBFields.SB_UID] = SBUID;
                rowMatrix[MatrixSBFields.MatrixRow] = Convert.ToInt32(matrixRow);

                _dsSBMatrix.Tables[MatrixSBFields.TableName_SBMatrixRows].Rows.Add(rowMatrix);
            }

            _dsSBMatrix.AcceptChanges();

            // Save all changes back to the SB XML file
            DataFunctions.SaveDataXML(_dsSBMatrix, _Path_SB_MATRIX_XML);
            if (DataFunctions._ErrorMessage.Length > 0)
            {
                _ErrorMessage = string.Concat("An error occurred while saving Storyboard data.   Error: ", DataFunctions._ErrorMessage);
                return false;
            }

            return true;

        }

        public bool GenerateSB(DataTable dtSBData, string SBName, string SBTemplateName, string description, string[] SelectedRowNos, string user)
        {
            _ErrorMessage = string.Empty;

            string sbTemplateFile = "SBTemp.docx";
            string sbTemplatePathFile = Path.Combine(_SBTemplatePath, SBTemplateName, sbTemplateFile);

            // Check of SB Template Exists
            if (!File.Exists(sbTemplatePathFile))
            {
                _ErrorMessage = string.Concat("Unable to find the selected Storyboard Template: ", sbTemplatePathFile);
                return false;
            }

            string backupPathFile = string.Empty;
            string sbFile = string.Concat(SBName, ".docx");
            _NewSBFile_docx = Path.Combine(_MatrixSBPath, sbFile);
            
            // Check if SB already exists, if so back it up and delete
            if (File.Exists(_NewSBFile_docx))
            {
                Files.BackupFile(_MatrixSBPath, _MatrixSBPathBackup, sbFile, out backupPathFile);
                try
                {
                    File.Delete(_NewSBFile_docx);
                }
                catch (Exception ex)
                {
                    string errMsg = string.Concat("Unable to remove a previous version of a Storyboard. The file may be locked open. Please close document is open     Error: ", ex.Message);
                    return false;

                    // ToDo add lock check -- see: https://stackoverflow.com/questions/317071/how-do-i-find-out-which-process-is-locking-a-file-using-net 
                }
            }

            WordDataMerge word = new WordDataMerge(sbTemplatePathFile, _NewSBFile_docx);
            word.DataSource = dtSBData;
            word.GenerateWordFile();

            
            _dsSBMatrix = DataFunctions.LoadDatasetFromXml(_Path_SB_MATRIX_XML);
            if (_dsSBMatrix == null)
            {
                _ErrorMessage = DataFunctions._ErrorMessage;
                return false;
            }

            DataRow row;
            DataRow rowMatrix;

            int sbMatrixRowUID;
            string filter = string.Empty;

            bool sbExists = SBExists(SBName);
            if (sbExists) // edit
            {
                filter = string.Concat(MatrixSBFields.Name, " = '", SBName, "'");
                DataRow[] rows = _dsSBMatrix.Tables[MatrixSBFields.TableName_SBs].Select(filter);
                if (rows.Length == 0) // New or Not found
                {
                    row = _dsSBMatrix.Tables[MatrixSBFields.TableName_SBs].NewRow();
                    _NewSBUID = DataFunctions.GetNewUID(_dsSBMatrix.Tables[MatrixSBFields.TableName_SBs], MatrixSBFields.SB_UID);
                    row[MatrixSBFields.SB_UID] = _NewSBUID;
                    row[MatrixSBFields.Name] = SBName;
                }
                else // edit
                {
                    row = rows[0];
                    _NewSBUID = (int)row[MatrixSBFields.SB_UID];
                }

                filter = string.Concat(MatrixSBFields.SB_UID, " = ", _NewSBUID);
                DataRow[] rows2 = _dsSBMatrix.Tables[MatrixSBFields.TableName_SBMatrixRows].Select(filter);
                if (rows2.Length > 0)
                {
                    foreach (DataRow xrow in rows2)
                    {
                        xrow.Delete();
                    }
                }

                _dsSBMatrix.AcceptChanges();

                //table.Select(filter).Delete();
            }
            else // New
            {
                row = _dsSBMatrix.Tables[MatrixSBFields.TableName_SBs].NewRow();

                _NewSBUID = DataFunctions.GetNewUID(_dsSBMatrix.Tables[MatrixSBFields.TableName_SBs], MatrixSBFields.SB_UID);
                row[MatrixSBFields.SB_UID] = _NewSBUID;
                row[MatrixSBFields.Name] = SBName;

            }

            row[MatrixSBFields.Description] = description;
            row[MatrixSBFields.SBTemplateName] = SBTemplateName;
            row[MatrixSBFields.CreatedBy] = user;
            row[MatrixSBFields.CreatedDateTime] = DateTime.Now;

            _dsSBMatrix.Tables[MatrixSBFields.TableName_SBs].Rows.Add(row);

            foreach (string matrixRow in SelectedRowNos)
            {
                rowMatrix = _dsSBMatrix.Tables[MatrixSBFields.TableName_SBMatrixRows].NewRow();
                sbMatrixRowUID = DataFunctions.GetNewUID(_dsSBMatrix.Tables[MatrixSBFields.TableName_SBMatrixRows], MatrixSBFields.SBMatrix_UID);
                rowMatrix[MatrixSBFields.SBMatrix_UID] = sbMatrixRowUID;
                rowMatrix[MatrixSBFields.SB_UID] = _NewSBUID;
                rowMatrix[MatrixSBFields.MatrixRow] = Convert.ToInt32(matrixRow);

                _dsSBMatrix.Tables[MatrixSBFields.TableName_SBMatrixRows].Rows.Add(rowMatrix);
            }

            _dsSBMatrix.AcceptChanges();

            DataFunctions.SaveDataXML(_dsSBMatrix, _Path_SB_MATRIX_XML);
            if (DataFunctions._ErrorMessage.Length > 0)
            {
                _ErrorMessage = string.Concat("An error occurred while saving Storyboard data.   Error: ", DataFunctions._ErrorMessage);
                return false;
            }

            return true;
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
