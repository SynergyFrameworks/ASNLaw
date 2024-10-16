using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;


namespace WorkgroupMgr
{
    public class Matrices
    {

        public Matrices(string projectRootFolder, string matrixTempPathTemplates, string projectName)
        {
            _ProjectRootPath = projectRootFolder;
            _ProjectName = projectName;
            _MatrixTempPathTemplates = matrixTempPathTemplates;

            ValidateFix();
        }

        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        private string _ProjectRootPath = string.Empty;
        public string ProjectRootPath
        {
            get { return _ProjectRootPath; }
        }

        private string _ProjectName = string.Empty;
        public string ProjectName
        {
            get { return _ProjectName; }
        }

        private string _MatrixTempPathTemplates = string.Empty;
        public string MatrixTempPathTemplates
        {
            get { return _MatrixTempPathTemplates; }
        }

        private string _MatricesPath = string.Empty;
        public string MatricesPath
        {
            get { return _MatricesPath; }
        }

        private string _CurrentMatrixPath = string.Empty;
        public string CurrentMatrixPath
        {
            get { return _CurrentMatrixPath; }
        }

        private string _CurrentTempSettingsFile = string.Empty;
        public string CurrentTempSettingsFile
        {
            get { return _CurrentTempSettingsFile; }
        }

        //private string _MatricesAutoPopPath = string.Empty;
        //public string MatricesAutoPopPath
        //{
        //    get { return _MatricesAutoPopPath; }
        //}

        private DataSet _dsMatrix;
        private const string _MATRIX_XMLFILE = "Matrix.xml";


        public bool ValidateFix()
        {
            _ErrorMessage = string.Empty;

            if (_ProjectRootPath == string.Empty)
            {
                _ErrorMessage = "Project Root Folder has not been defined.";
                return false;
            }

            _MatricesPath = Path.Combine(_ProjectRootPath, _ProjectName, "Matrices");

           return CheckCreateDir(_MatricesPath);
        }

        public string[] GetMatricesNames()
        {
            _ErrorMessage = string.Empty;

            if (!ValidateFix())
            {
                return null;
            }
       
            List<string> lst = new List<string>();

            try
            {
                string projectName = string.Empty;
                string[] dirs = Directory.GetDirectories(_MatricesPath);
                foreach (string dir in dirs)
                {
                    if (dir.IndexOf('~') == -1) // ~ are Deleted dirs
                    {
                        projectName = Files.GetLastFolder(dir);
                        lst.Add(projectName);
                    }
                }

            }
            catch (Exception ex)
            {
                _ErrorMessage = string.Concat("Unable to get Matrices due to an Error.", Environment.NewLine, Environment.NewLine, ex.Message);
                return null;
            }

            return lst.ConvertAll(x => x.ToString()).ToArray(); 
        }

        public bool MatrixExists(string MatrixName)
        {
            if (Directory.Exists(_CurrentMatrixPath))
            {              
                return true;
            }

            return false;
        }

        public DataSet GetMatrixSettings(string matrixName)
        {
            MatrixSettings matrixSettings = new MatrixSettings(_MatrixTempPathTemplates, matrixName);

            DataSet ds = matrixSettings.GetSettings();
            if (ds == null)
            {
                _ErrorMessage = matrixSettings.ErrorMessage;
                return null;
            }

            return ds;
        }
        
        public bool CreateMatrix(string MatrixName, string MatrixDescription, string MatrixTemplate, string user, DataTable dtDocAss)
        {
            _ErrorMessage = string.Empty;

          //  _CurrentMatrixPath = Path.Combine(_MatricesPath, _ProjectName, MatrixName);
            _CurrentMatrixPath = Path.Combine(_MatricesPath, MatrixName);

            if (MatrixExists(MatrixName))
            {
                _ErrorMessage = string.Concat("Matrix ", MatrixName, " already exists.");
                return false;
            }

            //_MatricesAutoPopPath = Path.Combine(_MatricesPath, "AutoPop"); // holds html files for auto-population
            //if (!CheckCreateDir(_MatricesAutoPopPath))
            //{
            //    _ErrorMessage = string.Concat("Unable to find or create folder: ", _MatricesAutoPopPath);
            //    return false;
            //}
            

            // Excel Matrix Template
            string TemplateFile = Path.Combine(_MatrixTempPathTemplates, MatrixTemplate, "MatrixTemp.xlsx");

            if (!File.Exists(TemplateFile))
            {
                _ErrorMessage = string.Concat("Unable to find Matrix Template file: ", TemplateFile);
                return false;
            }

            // Matrix Template Settings File
            string TemplateSettingsFile = Path.Combine(_MatrixTempPathTemplates, MatrixTemplate, "MatrixTempSettings.xml");
            if (!File.Exists(TemplateSettingsFile))
            {
                _ErrorMessage = string.Concat("Unable to find Matrix Template Settings file: ", TemplateSettingsFile);
                return false;
            }

            // Removed 03.30.2020 - Not used anymore -Matrix Template HTML file
            //string TemplateHTMLFile = Path.Combine(_MatrixTempPathTemplates, MatrixTemplate, "MatrixTemp.html");
            //if (!File.Exists(TemplateHTMLFile))
            //{
            //    _ErrorMessage = string.Concat("Unable to find Matrix Template HTML file: ", TemplateHTMLFile);
            //    return false;
            //}


            if (!CheckCreateDir(_CurrentMatrixPath))
                return false;

            _dsMatrix = CreateEmptyDataSet();

            DataRow mRow = _dsMatrix.Tables[MatricesFields.MetadataTableName].NewRow();
            mRow[MatricesFields.Name] = MatrixName;
            mRow[MatricesFields.Description] = MatrixDescription;
            mRow[MatricesFields.CreatedBy] = user;

            _dsMatrix.Tables[MatricesFields.MetadataTableName].Rows.Add(mRow);

            foreach (DataRow row in dtDocAss.Rows)
            {
                DataRow rowDocAss = _dsMatrix.Tables[MatricesFields.DocsAssociationTableName].NewRow();
            //    rowDocAss[MatricesFields.UID] = row[MatricesFields.UID];
                rowDocAss[MatricesFields.DocTypeItem] = row[MatricesFields.DocTypeItem];
                //rowDocAss[MatricesFields.Column] = row[MatricesFields.Column];
                //rowDocAss[MatricesFields.DocTypeDescription] = row[MatricesFields.DocTypeDescription];
                //rowDocAss[MatricesFields.DocTypeSource] = row[MatricesFields.DocTypeContentType];
                //rowDocAss[MatricesFields.DocTypeContentType] = row[MatricesFields.DocTypeContentType];
                rowDocAss[MatricesFields.ProjectDocumentName] = row[MatricesFields.ProjectDocumentName];

                _dsMatrix.Tables[MatricesFields.DocsAssociationTableName].Rows.Add(rowDocAss);
            }

            string pathXMLFile = Path.Combine(_CurrentMatrixPath, _MATRIX_XMLFILE);

            DataFunctions.SaveDataXML(_dsMatrix, pathXMLFile);

            if (!File.Exists(pathXMLFile))
            {
                _ErrorMessage = string.Concat("Your Matrix Settings were Not saved.", Environment.NewLine, Environment.NewLine, DataFunctions._ErrorMessage) ;
                return false;
            }

            // Copy the Excel Matrix Template
            string fileName = Files.GetFileName(TemplateFile);
            string pathTempFile = Path.Combine(_CurrentMatrixPath, fileName);

            try
            {
                File.Copy(TemplateFile, pathTempFile);
                if (!File.Exists(pathTempFile))
                {
                    System.Threading.Thread.Sleep(1000);
                    File.Copy(TemplateFile, pathTempFile);
                    if (!File.Exists(pathTempFile))
                    {
                        _ErrorMessage = string.Concat("Your Matrix Template was Not copied to the Martix folder.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = string.Concat("Your Matrix Template file was Not copied to the Martix folder.", Environment.NewLine, Environment.NewLine, ex.Message);
                return false;
            }

            // Copy Template Settings file           
            fileName = Files.GetFileName(TemplateFile);
            _CurrentTempSettingsFile = Path.Combine(_CurrentMatrixPath, "MatrixTempSettings.xml");

            try
            {
                File.Copy(TemplateSettingsFile, _CurrentTempSettingsFile);
                if (!File.Exists(_CurrentTempSettingsFile))
                {
                    System.Threading.Thread.Sleep(1000);
                    File.Copy(TemplateSettingsFile, _CurrentTempSettingsFile);
                    if (!File.Exists(_CurrentTempSettingsFile))
                    {
                        _ErrorMessage = string.Concat("Your Template Settings file was Not copied to the Martix folder.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = string.Concat("Your Matrix Settings file was Not copied to the Martix folder.", Environment.NewLine, Environment.NewLine, ex.Message);
                return false;
            }

            // Matrix HTML
            //TemplateHTMLFile
            //string pathFile = Path.Combine(_CurrentMatrixPath, "Matrix.html");
            //try
            //{
            //    File.Copy(TemplateHTMLFile, pathFile);
            //    if (!File.Exists(pathFile))
            //    {
            //        System.Threading.Thread.Sleep(1000);
            //        File.Copy(TemplateHTMLFile, pathFile);
            //        //if (!File.Exists(pathFile))
            //        //{
            //        //    _ErrorMessage = string.Concat("Your Template Settings file was Not copied to the Martix folder.");
            //        //    return false;
            //        //}
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //_ErrorMessage = string.Concat("Your Matrix Settings file was Not copied to the Martix folder.", Environment.NewLine, Environment.NewLine, ex.Message);
            //    //return false;
            //}


            // Write the Matrix Description to a text file
            string pathDescriptionFile = Path.Combine(_CurrentMatrixPath, "Description.txt");
            Files.WriteStringToFile(MatrixDescription, pathDescriptionFile);

            return true;
        }

        private DataSet CreateEmptyDataSet()
        {
            DataTable dtMetadata = new DataTable(MatricesFields.MetadataTableName);

            dtMetadata.Columns.Add(MatricesFields.Name, typeof(string));
            dtMetadata.Columns.Add(MatricesFields.Description, typeof(string));
            dtMetadata.Columns.Add(MatricesFields.CreatedBy, typeof(string));
            dtMetadata.Columns.Add(MatricesFields.CreationDate, typeof(DateTime));

            DataTable dtDocsAssociation = GetEmptyDocsAssociation();

            DataSet ds = new DataSet();

            ds.Tables.Add(dtMetadata);
            ds.Tables.Add(dtDocsAssociation);

            return ds;
        }

        public DataTable GetEmptyDocsAssociation()
        {
            DataTable dtDocsAssociation = new DataTable(MatricesFields.DocsAssociationTableName);
            dtDocsAssociation.Columns.Add(MatricesFields.UID, typeof(int));
            dtDocsAssociation.Columns.Add(MatricesFields.DocTypeItem, typeof(string));
            dtDocsAssociation.Columns.Add(MatricesFields.Column, typeof(string));
            dtDocsAssociation.Columns.Add(MatricesFields.Description, typeof(string));
            dtDocsAssociation.Columns.Add(MatricesFields.DocTypeSource, typeof(string));
            dtDocsAssociation.Columns.Add(MatricesFields.DocTypeContentType, typeof(string));
            dtDocsAssociation.Columns.Add(MatricesFields.ProjectDocumentName, typeof(string));

            return dtDocsAssociation;
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
