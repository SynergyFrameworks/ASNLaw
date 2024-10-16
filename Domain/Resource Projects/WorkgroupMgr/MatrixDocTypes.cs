using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace WorkgroupMgr
{
    public class MatrixDocTypes
    {
        public MatrixDocTypes(string matrixPath)
        {
            _MatrixPath = matrixPath;

            ValidateFix();
        }

        private const string MATRIX_XML = "Matrix.xml"; // Holds the Matrix Metadata and the DocTypes associations
        private const string MATRIX_TEMPLATE_SETTINGS_XML = "MatrixTempSettings.xml"; // Holds the Matrix's Configuration Settings



        private const string MATRIX_TEMPLATE_XLSX = "MatrixTemp.xlsx"; // Copied for the orginal template is transformed to show the DocTypes allocations
        private const string MATRIX_TEMPLATE_DATA_XLSX = "MatrixTempData.xlsx"; // Hidden to the users and holds the data and pointers for DocType allocations

  
        private DataTable _dtDocTypeSettings;
        private DataTable _dtMetadata;

        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        private string _MatrixPath = string.Empty;
        public string MatrixPath
        {
            get { return _MatrixPath; }
        }

        public DataTable MetadataTable
        {
            get { return _dtMetadata; }
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

            string pathFile = Path.Combine(_MatrixPath, MATRIX_XML);
            if (!File.Exists(pathFile))
            {
                _ErrorMessage = string.Concat("Unable to find file '", MATRIX_XML, "'.", Environment.NewLine, Environment.NewLine, "This file holds the Matrix Metadata and the Document Types associations.");
            }

            pathFile = Path.Combine(_MatrixPath, MATRIX_TEMPLATE_SETTINGS_XML);
            if (!File.Exists(pathFile))
            {
                _ErrorMessage = string.Concat("Unable to find file '", MATRIX_TEMPLATE_SETTINGS_XML, "'.", Environment.NewLine, Environment.NewLine, "This file holds the Matrix's Template Configuration Settings.");
            }

            pathFile = Path.Combine(_MatrixPath, MATRIX_TEMPLATE_XLSX);
            if (!File.Exists(pathFile))
            {
                _ErrorMessage = string.Concat("Unable to find file '", MATRIX_TEMPLATE_XLSX, "'.", Environment.NewLine, Environment.NewLine, "This file is the Excel Matrix Template file.");
            }

            string pathFile2 = Path.Combine(_MatrixPath, MATRIX_TEMPLATE_DATA_XLSX);
            if (!File.Exists(pathFile2))
            {
                try
                {
                    File.Copy(pathFile, pathFile2);
                }
                catch (Exception ex)
                {
                    _ErrorMessage = string.Concat("Unable to recover file '", MATRIX_TEMPLATE_DATA_XLSX, "'.", Environment.NewLine, Environment.NewLine, ex.Message);
                    return false;
                }

            }

            DataSet ds = GetSettings();
            if (ds == null)
            {
                return false;
            }

            return true;
        }

        public DataSet GetSettings()
        {
            _ErrorMessage = string.Empty;

            string dataPathFile = Path.Combine(_MatrixPath, MATRIX_TEMPLATE_SETTINGS_XML);
            if (!File.Exists(dataPathFile))
            {
                _ErrorMessage = string.Concat("Unable to locate Matrix Template Data Settings file: ", dataPathFile);
                return null;
            }

            DataSet ds = DataFunctions.LoadDatasetFromXml(dataPathFile);
            if (ds == null)
            {
                _ErrorMessage = DataFunctions._ErrorMessage;
                return null;
            }

            if (ds != null)
            {
                _dtMetadata = ds.Tables[0];
                _dtDocTypeSettings = ds.Tables[1];
            }

            return ds;
        }

        public DataTable GetDocsAssocationsTable()
        {
            _ErrorMessage = string.Empty;

            string pathFile = Path.Combine(_MatrixPath, MATRIX_XML);
            DataSet ds = DataFunctions.LoadDatasetFromXml(pathFile);

            if (ds == null)
            {
                _ErrorMessage = DataFunctions._ErrorMessage;
                return null;
            }

            DataSet dsSettings = GetSettings();
            if (dsSettings == null)
            {
                return null;
            }

            DataTable dt = new DataTable(MatricesFields.DocsAssociationTableName);

            dt.Columns.Add(MatricesFields.DocTypeItem, typeof(string));
            dt.Columns.Add(MatricesFields.ProjectDocumentName, typeof(string));
            dt.Columns.Add(MatricesFields.Column, typeof(string));
            dt.Columns.Add(MatricesFields.DocTypeSource, typeof(string));
            dt.Columns.Add(MatricesFields.DocTypeContentType, typeof(string));

            string docTypeItem = string.Empty;
            foreach (DataRow row in ds.Tables[MatricesFields.DocsAssociationTableName].Rows)
            {
                docTypeItem = row[MatricesFields.DocTypeItem].ToString();

                foreach (DataRow settingsRow in _dtDocTypeSettings.Rows) // Reason we added the population of Settings during Run-Time is because in the Future the Project/Matrix Settings can be changed by the user
                {
                    if (settingsRow[DocTypesFields.Item] != null)
                    {
                        if (settingsRow[DocTypesFields.Item].ToString() == docTypeItem)
                        {
                            DataRow newRow = dt.NewRow();
                            newRow[MatricesFields.DocTypeItem] = row[MatricesFields.DocTypeItem];
                            newRow[MatricesFields.ProjectDocumentName] = row[MatricesFields.ProjectDocumentName];

                            newRow[MatricesFields.Column] = settingsRow[CommonFields.Column];
                            newRow[MatricesFields.DocTypeSource] = settingsRow[DocTypesFields.Source];
                            newRow[MatricesFields.DocTypeContentType] = settingsRow[DocTypesFields.ContentType];

                            dt.Rows.Add(newRow);
                        }

                    }
                }
            }
       
            return dt;

        }

        public string[] GetAllocatedUIDs(string DocName)
        {
            _ErrorMessage = string.Empty;

            string file = string.Concat(DocName, "_DocType.allc");
            string pathFile = Path.Combine(_MatrixPath, file);
            if (!File.Exists(pathFile))
            {
                _ErrorMessage = string.Concat("Unable to find Allocation file: ", pathFile);
                return null;
            }

            List<string> allocatedUIDs = new List<string>();

            string[] allocations = Files.ReadFile2Array(pathFile);
            string uid = string.Empty;
            string[] allocatedSplit;
            foreach (string allocated in allocations)
            {
                if (allocated.IndexOf('|') > -1)
                {
                    allocatedSplit = allocated.Split('|');
                    uid = allocatedSplit[0];
                    allocatedUIDs.Add(uid);
                }
            }

            return allocatedUIDs.ToArray();

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
