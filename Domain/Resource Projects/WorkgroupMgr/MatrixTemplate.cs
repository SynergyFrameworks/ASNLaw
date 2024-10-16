using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace WorkgroupMgr
{
    public class MatrixTemplate
    {
        public MatrixTemplate(string docTypesPath, string listPath, string refResPath, string matrixPath, string matrixTempPathTemplates)
        {
            _docTypesPath = docTypesPath;
            _listPath = listPath;
            _refResPath = refResPath;
            _matrixTempPathTemplates = matrixTempPathTemplates;
            _matrixPath = matrixPath;

            BuildNewDs();

            _isNew = true;
        }

        public MatrixTemplate(string docTypesPath, string listPath, string refResPath, string matrixPath, string matrixTempPathTemplates, string matrixName)
        {
            _docTypesPath = docTypesPath;
            _listPath = listPath;
            _refResPath = refResPath;
            _matrixPath = matrixPath;
            _matrixTempPathTemplates = matrixTempPathTemplates;
            _matrixName = matrixName;

            _isNew = false;
        }

        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        private string _NoticeMessage = string.Empty;
        public string NoticeMessage
        {
            get { return _NoticeMessage; }
        }

        private bool _isNew = true;

        private string _docTypesPath = string.Empty;
        private string _listPath = string.Empty;
        private string _refResPath = string.Empty;
        private string _matrixPath = string.Empty;
        private string _matrixTempPathTemplates = string.Empty;
        private string _matrixName = string.Empty;

        private DataSet _dsMatrixTemplate;

        private DocTypesMgr _docTypeMgr;
        private ListMgr _listMgr;
        private RefResMgr _refResMgr;


        public string GetMatrixTemporaryPath()
        {
            _ErrorMessage = string.Empty;

            string pathWorkgroupMatrixTempTemp = Path.Combine(_matrixPath, "Temp"); // holds temporary files
            if (!Directory.Exists(pathWorkgroupMatrixTempTemp))
            {
                try
                {
                    Directory.CreateDirectory(pathWorkgroupMatrixTempTemp);
                    if (!Directory.Exists(pathWorkgroupMatrixTempTemp))
                    {
                        _ErrorMessage = "Unable to create Matrix Temporary folder.";
                        return string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    _ErrorMessage = string.Concat("Unable to create Matrix Temporary folder due to an error.    Error:", ex.Message);
                    return string.Empty;
                }
            }

            return pathWorkgroupMatrixTempTemp;
        }

        public bool MatrixNameExists(string MatrixName)
        {
            return Directory.Exists(Path.Combine(_matrixPath, MatrixName));
        }

        public string GetTemplatePathFile()
        {
            _ErrorMessage = string.Empty;

            if (_matrixName == string.Empty)
            {
                _ErrorMessage = "Matrix Name has not been loaded.";
                return string.Empty;
            }

            string templatePathFile = Path.Combine(_matrixTempPathTemplates, _matrixName, "MatrixTemp.xlsx");
            if (!File.Exists(templatePathFile))
            {
                _ErrorMessage = string.Concat("Unable to locate Excel Template file: ", templatePathFile);
                return string.Empty;
            }

            return templatePathFile;
        }

        public string GetSummary(string TemplateName)
        {
            string pathFile = Path.Combine(_matrixTempPathTemplates, TemplateName, "Summary.txt");

            return Files.ReadFile(pathFile);
                
        }

        public DataSet GetSettings()
        {
            MatrixSettings matrixSettings = new MatrixSettings(_matrixTempPathTemplates, _matrixName);

            DataSet ds = matrixSettings.GetSettings();
            if (ds == null)
            {
                _ErrorMessage = matrixSettings.ErrorMessage;
                return null;
            }

            return ds;

            //_ErrorMessage = string.Empty;

            //string dataPathFile = Path.Combine(_matrixTempPathTemplates, _matrixName,  "MatrixTempSettings.xml");
            //if (!File.Exists(dataPathFile))
            //{
            //    _ErrorMessage = string.Concat("Unable to locate Matrix Template Data Settings file: ", dataPathFile);
            //    return null;
            //}

            //DataSet ds = DataFunctions.LoadDatasetFromXml(dataPathFile);
            //if (ds == null)
            //{
            //    _ErrorMessage = DataFunctions._ErrorMessage;
            //    return null;
            //}

            //return ds;
        }

        public DataTable GetRefResNames()
        {
            _ErrorMessage = string.Empty;


            if (_dsMatrixTemplate != null)
                _dsMatrixTemplate.Tables[RefResFields.TableName].Rows.Clear();

            if (_refResMgr == null)
            {
                _refResMgr = new RefResMgr(_refResPath);


                string[] names = _refResMgr.GetRefResNames();
                // Check for Errors
                if (names == null)
                {
                    if (_refResMgr.ErrorMessage.Length > 0)
                    {
                        _ErrorMessage = _refResMgr.ErrorMessage;
                    }
                    else
                    {
                        _ErrorMessage = string.Concat("Unable to find Reference Resources in path: ", _listPath);
                    }

                    return null;
                }

                DataRow rowNew;
                foreach (string name in names)
                {

                    rowNew = _dsMatrixTemplate.Tables[RefResFields.TableName].NewRow();
                    rowNew[CommonFields.Selected] = false;
                    rowNew[CommonFields.Name] = name;
                    //rowNew[CommonFields.Column] = string.Empty;


                    _dsMatrixTemplate.Tables[RefResFields.TableName].Rows.Add(rowNew);
                }

                return _dsMatrixTemplate.Tables[RefResFields.TableName];
            }
  
            
            return null;
            
        }

        public DataTable GetListNames()
        {
            _ErrorMessage = string.Empty;

 
            if (_dsMatrixTemplate != null)
                _dsMatrixTemplate.Tables[ListFields.TableName].Rows.Clear();

            if (_listMgr == null)
            {
                _listMgr = new ListMgr(_listPath);
            }

            string[] listNames = _listMgr.GetListNames();
            // Check for Errors
            if (listNames == null)
            {
                if (_listMgr.ErrorMessage.Length > 0)
                {
                    _ErrorMessage = _listMgr.ErrorMessage;
                }
                else
                {
                    _ErrorMessage = string.Concat("Unable to find Lists in path: ", _listPath);
                }

                return null;
            }

            DataRow rowNew;
            foreach (string listName in listNames)
            {

                rowNew = _dsMatrixTemplate.Tables[ListFields.TableName].NewRow();
                rowNew[CommonFields.Selected] = false;
                rowNew[CommonFields.Name] = listName;
                //rowNew[CommonFields.Column] = string.Empty;


                _dsMatrixTemplate.Tables[ListFields.TableName].Rows.Add(rowNew);
            }

            return _dsMatrixTemplate.Tables[ListFields.TableName];
           
        }

        //MatrixTemplateFields

        public string[] GetDocTypesNames()
        {
            _ErrorMessage = string.Empty;

            if (_docTypeMgr == null)
                _docTypeMgr = new DocTypesMgr(_docTypesPath);

            string[] docTypeNames = _docTypeMgr.GetDocTypesNames();

            if (docTypeNames == null)
            {
                if (_docTypeMgr.ErrorMessage.Length > 0)
                {
                    _ErrorMessage = _docTypeMgr.ErrorMessage;                   
                }
            }

            return docTypeNames;
        }

        public string GetDocTypeDescription(string docTypeName)
        {
            if (_docTypeMgr == null)
                _docTypeMgr = new DocTypesMgr(_docTypesPath);

            string description = _docTypeMgr.GetDescription(docTypeName);
            return description;
        }

        public string[] ColumnOptions2 = { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        public string[] ColumnOptions = {"", "Any", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        public int[] RowNumbers = {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20};
        public string[] DocType_Source = {"", "Analysis Results", "Deep Analysis"};
        public string[] DocType_ContentType = {"", "Number", "Caption", "No & Caption", "Text" };


        public DataTable GetDocTypeItems(string docTypeName)
        {
            //if (_isNew)
            //{
                if (_dsMatrixTemplate == null)
                    _dsMatrixTemplate = BuildNewDs();

               // if (_dsMatrixTemplate != null)
                _dsMatrixTemplate.Tables[DocTypesFields.TableName].Rows.Clear();

                DataTable dtSource = _docTypeMgr.GetDocTypeItems(docTypeName);
                DataRow rowNew;

                foreach (DataRow row in dtSource.Rows)
                {
                    rowNew = _dsMatrixTemplate.Tables[DocTypesFields.TableName].NewRow();
                    rowNew[CommonFields.Selected] = false;
                    rowNew[DocTypesFields.Item] = row[DocTypesFields.Item];
                    rowNew[DocTypesFields.Description] = row[DocTypesFields.Description];
                    //rowNew[CommonFields.Column] = string.Empty;
                    //rowNew[DocTypesFields.Source] = string.Empty;
                    //rowNew[DocTypesFields.ContentType] = string.Empty;

                    _dsMatrixTemplate.Tables[DocTypesFields.TableName].Rows.Add(rowNew);
                }

                return _dsMatrixTemplate.Tables[DocTypesFields.TableName];
            //}
            //else
            //{

            //}

            return null;
        }


        public DataSet BuildNewDs()
        {
             _dsMatrixTemplate = new DataSet();

            DataTable dtMatrixTemplate = CreateTable_MatrixTemplate();
            DataTable dtDocTypes = CreateTable_DocTypes();
            DataTable dtLists = CreateTable_Lists();
            DataTable dtRefRes = CreateTable_RefRes();

            _dsMatrixTemplate.Tables.Add(dtMatrixTemplate);
            _dsMatrixTemplate.Tables.Add(dtDocTypes);
            _dsMatrixTemplate.Tables.Add(dtLists);
            _dsMatrixTemplate.Tables.Add(dtRefRes);


            return _dsMatrixTemplate;
        }

        //private bool PopulateNewData()
        //{
  
        //}

        //private bool PopulateNewLists()
        //{
        //    if (_listMgr == null)
        //        _listMgr = new ListMgr(_listPath);

        //    string[] listNames = _listMgr.GetListNames();
            

        //}

        public DataTable CreateTable_MatrixTemplate()
        {
            DataTable dt = new DataTable(MatrixTemplateFields.TableName);

        //    dt.Columns.Add(MatrixTemplateFields.UID, typeof(int));
            dt.Columns.Add(MatrixTemplateFields.Name, typeof(string));
            dt.Columns.Add(MatrixTemplateFields.Description, typeof(string));
            dt.Columns.Add(MatrixTemplateFields.OrgExcelTempFile, typeof(string));
            dt.Columns.Add(MatrixTemplateFields.RowDataStarts, typeof(int));
            dt.Columns.Add(MatrixTemplateFields.DocTypesSelected, typeof(string));

            return dt;
        }

        private DataTable CreateTable_DocTypes()
        {
            DataTable dt = new DataTable(DocTypesFields.TableName);

        //    dt.Columns.Add(DocTypesFields.UID, typeof(int));
            dt.Columns.Add(CommonFields.Selected, typeof(bool));
            dt.Columns.Add(DocTypesFields.Item, typeof(string));
            dt.Columns.Add(DocTypesFields.Description, typeof(string));
            //dt.Columns.Add(CommonFields.Column, typeof(string));
            //dt.Columns.Add(DocTypesFields.Source, typeof(string));
            //dt.Columns.Add(DocTypesFields.ContentType, typeof(string));
  
            return dt;
        }

        public DataTable CreateTable_DocTypes2()
        {
            DataTable dt = new DataTable(DocTypesFields.TableName);

            //    dt.Columns.Add(DocTypesFields.UID, typeof(int));
            dt.Columns.Add(CommonFields.Selected, typeof(bool));
            dt.Columns.Add(DocTypesFields.Item, typeof(string));
            dt.Columns.Add(DocTypesFields.Description, typeof(string));
            dt.Columns.Add(CommonFields.Column, typeof(string));
            dt.Columns.Add(DocTypesFields.Source, typeof(string));
            dt.Columns.Add(DocTypesFields.ContentType, typeof(string));

            return dt;
        }

        private DataTable CreateTable_Lists()
        {
            DataTable dt = new DataTable(ListFields.TableName);

        //    dt.Columns.Add(ListFields.UID, typeof(int));
            dt.Columns.Add(CommonFields.Selected, typeof(bool));
            dt.Columns.Add(CommonFields.Name, typeof(string));          
          //  dt.Columns.Add(CommonFields.Column, typeof(string));

            return dt;
        }

        public DataTable CreateTable_Lists2()
        {
            DataTable dt = new DataTable(ListFields.TableName);

            //    dt.Columns.Add(ListFields.UID, typeof(int));
            dt.Columns.Add(CommonFields.Selected, typeof(bool));
            dt.Columns.Add(CommonFields.Name, typeof(string));
            dt.Columns.Add(CommonFields.Column, typeof(string));

            return dt;
        }

        private DataTable CreateTable_RefRes()
        {
            DataTable dt = new DataTable(RefResFields.TableName);

       //     dt.Columns.Add(RefResFields.UID, typeof(int));
            dt.Columns.Add(CommonFields.Selected, typeof(bool));
            dt.Columns.Add(RefResFields.Name, typeof(string));
            //dt.Columns.Add(RefResFields.Description, typeof(string));
            //dt.Columns.Add(RefResFields.LocationType, typeof(string));
            //dt.Columns.Add(CommonFields.Column, typeof(string));

            return dt;
        }

        public DataTable CreateTable_RefRes2()
        {
            DataTable dt = new DataTable(RefResFields.TableName);

            //     dt.Columns.Add(RefResFields.UID, typeof(int));
            dt.Columns.Add(CommonFields.Selected, typeof(bool));
            dt.Columns.Add(RefResFields.Name, typeof(string));
            //dt.Columns.Add(RefResFields.Description, typeof(string));
            //dt.Columns.Add(RefResFields.LocationType, typeof(string));
            dt.Columns.Add(CommonFields.Column, typeof(string));

            return dt;
        }

        private string CheckCreateBackupFolder(string matrixTemplatePath)
        {
            _ErrorMessage = string.Empty;

            string backupFolder = Path.Combine(matrixTemplatePath, "Backup");
            if (!Directory.Exists(backupFolder))
            {
                try
                {
                    Directory.CreateDirectory(backupFolder);
                }
                catch (Exception ex)
                {
                    _ErrorMessage = string.Concat("Error: Unable to create the Matrix Template Backup folder: ", backupFolder, Environment.NewLine, Environment.NewLine, ex.Message);
                    return string.Empty;
                }
            }

            return backupFolder;
        }

        private bool BackupMatrix(string backupFolder, string file)
        {
            string prefix = "~";

            string backupFile = string.Concat(prefix, file);
            string backupPathfile = Path.Combine(backupFolder, backupFile);
            if (File.Exists(backupPathfile))
            {
                for (int i = 0; i < 101; i++)
                {
                    backupFile = string.Concat(prefix, backupFile);
                    backupPathfile = Path.Combine(backupFolder, backupFile);
                    if (!File.Exists(backupPathfile))
                    {
                        break;
                    }
                    prefix += "~";
                }
            }

            string matrixPathFile = Path.Combine(_matrixPath, file);

            try
            {
                File.Copy(matrixPathFile, backupPathfile);
                return true;
            }
            catch (Exception ex)
            {
                _NoticeMessage = string.Concat("Notice: Unable to Backup the Matrix Template.", Environment.NewLine, Environment.NewLine, ex.Message);
                return false;
            }
        }

        public bool SaveMatrixTemplate(string TemplateName, DataSet dsTemplateData, string TemplateFile, bool isNewExcelSelection, string MatrixTempDescription, string MatrixSummary)
        {
            _ErrorMessage = string.Empty;
            _NoticeMessage = string.Empty;

            string matrixTemplatePath = Path.Combine(_matrixTempPathTemplates, TemplateName);
            if (Directory.Exists(matrixTemplatePath))
            {
                // Backup
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(matrixTemplatePath);
                }
                catch (Exception ex)
                {
                    _ErrorMessage = string.Concat("Unable to create the new Matrix Template folder: ", matrixTemplatePath, Environment.NewLine, Environment.NewLine, "Error: ", ex.Message);
                    return false;
                }
            }

            // Check and/or Create a Backup folder
            string backupFolder = CheckCreateBackupFolder(matrixTemplatePath);
            if (backupFolder.Length == 0)
            {
                return false;
            }

            // Backup matrix files
            string[] files = Directory.GetFiles(matrixTemplatePath, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                BackupMatrix(backupFolder, file);
            }

            // Save Matrix Template Settings
            string xmlPathFile = Path.Combine(matrixTemplatePath, "MatrixTempSettings.xml");
            try
            {
                if (File.Exists(xmlPathFile))
                    File.Delete(xmlPathFile);
            }
            catch(Exception ex2)
            {
               _ErrorMessage = string.Concat("Unable to save Matrix Template settings due to an error.", Environment.NewLine, Environment.NewLine, "Error: ", ex2.Message);
                return false;
            }
            DataFunctions.SaveDataXML(dsTemplateData, xmlPathFile);
            if (!File.Exists(xmlPathFile))
            {
                _ErrorMessage = DataFunctions._ErrorMessage;
                return false;
            }

            // Copy Excel Matrix Template
            if (isNewExcelSelection)
            {
                string xlsxPathFile = Path.Combine(matrixTemplatePath, "MatrixTemp.xlsx");

                try
                {
                if (File.Exists(xlsxPathFile))
                    File.Delete(xlsxPathFile);

                File.Copy(TemplateFile, xlsxPathFile);
                }
                catch (Exception ex3)
                {
                    _ErrorMessage = string.Concat("Unable to save Matrix Template file due to an error.", Environment.NewLine, Environment.NewLine, "Error: ", ex3.Message);
                    return false;
                }
            }

            //// Save HTML
            //string htmlPathFile = Path.Combine(matrixTemplatePath, "MatrixTemp.html");
            //try
            //{
            //    if (File.Exists(htmlPathFile))
            //        File.Delete(htmlPathFile);

            //    Files.WriteStringToFile(TemplateHTML, htmlPathFile);
            //}
            //catch (Exception ex4)
            //{
            //    _ErrorMessage = string.Concat("Unable to save Matrix Template HTML file due to an error.", Environment.NewLine, Environment.NewLine, "Error: ", ex4.Message);
            //    return false;
            //}

            // Save Description file
            string txtPathFile = Path.Combine(matrixTemplatePath, "Description.txt");
            try
            {
                if (File.Exists(txtPathFile))
                    File.Delete(txtPathFile);

                Files.WriteStringToFile(MatrixTempDescription, txtPathFile);
            }
            catch (Exception ex5)
            {
                _ErrorMessage = string.Concat("Unable to save Matrix Template Description file due to an error.", Environment.NewLine, Environment.NewLine, "Error: ", ex5.Message);
                return false;
            }

            // Save Summary File
            txtPathFile = Path.Combine(matrixTemplatePath, "Summary.txt");
            try
            {
                if (File.Exists(txtPathFile))
                    File.Delete(txtPathFile);

                Files.WriteStringToFile(MatrixSummary, txtPathFile);
            }
            catch (Exception ex5)
            {
                _ErrorMessage = string.Concat("Unable to save Matrix Template Summary file due to an error.", Environment.NewLine, Environment.NewLine, "Error: ", ex5.Message);
                return false;
            }
            
            

            return true;
        }

    }
}
