using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;


namespace WorkgroupMgr
{
    public class SBMgr
    {
        public SBMgr(string sbPath)
        {
            _SBPath = sbPath;

            LoadData();
        }

        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        private string _SBPath = string.Empty;
        public string SBPath
        {
            get { return _SBPath; }
        }

        private string _CurrentSBTempPath = string.Empty;
        public string CurrentSBTempPath
        {
            get { return _CurrentSBTempPath; }
        }

        private DataSet _dsSBTemps;

        private const string _DATA_FILE = "SBTemps.xml";
        private const string _SBTEMP_FILE = "SBTemp.docx";
        private const string _SBTEMP_VIEW_FILE = "SBTempView.docx"; // For viewing only, so a Locking Error will Not occur
        
        private string _DataPathFile = string.Empty;


        private bool LoadData()
        {
            _ErrorMessage = string.Empty;


            _DataPathFile = Path.Combine(_SBPath, _DATA_FILE);
            if (!File.Exists(_DataPathFile))
            {
                if (!CreateEmptyDataSet())
                {
                    return false;
                }
            }

            _dsSBTemps = DataFunctions.LoadDatasetFromXml(_DataPathFile);

            return true;
        }


        private bool CreateEmptyDataSet()
        {
            _ErrorMessage = string.Empty;

            if (File.Exists(_DataPathFile))
                return true;

            DataTable dtSBTemps = new DataTable(SBTempsFields.TableName);

            dtSBTemps.Columns.Add(SBTempsFields.UID, typeof(int));
            dtSBTemps.Columns.Add(SBTempsFields.Name, typeof(string));
            dtSBTemps.Columns.Add(SBTempsFields.Description, typeof(string));
            dtSBTemps.Columns.Add(SBTempsFields.OrgWordTempFile, typeof(string));
            dtSBTemps.Columns.Add(SBTempsFields.MatrixTemplate, typeof(string));
            dtSBTemps.Columns.Add(SBTempsFields.FieldsAdded, typeof(bool));
            dtSBTemps.Columns.Add(SBTempsFields.CreatedBy, typeof(string));
            dtSBTemps.Columns.Add(SBTempsFields.CreationDate, typeof(DateTime));

            DataSet ds = new DataSet();

            ds.Tables.Add(dtSBTemps);

            DataFunctions.SaveDataXML(ds, _DataPathFile);

            if (File.Exists(_DataPathFile))
            {
                return true;
            }
            else
            {
                _ErrorMessage = DataFunctions._ErrorMessage;
                return false;
            }

        }

        public string[] GetSBNames()
        {
            _ErrorMessage = string.Empty;

            List<string> names = new List<string>();

            if (_dsSBTemps == null)
            {
                LoadData();
                if (_dsSBTemps == null)
                {
                    _ErrorMessage = string.Concat("Unable to open Storyboard XML file: ", _DataPathFile);
                    return null;
                }
            }

            string name = string.Empty;

            foreach (DataRow row in _dsSBTemps.Tables[0].Rows)
            {
                name = row[SBTempsFields.Name].ToString();

                names.Add(name);
            }

            return names.ToArray();
        }

        public DataRow GetSBDataRow(string SBName)
        {
            string filter = string.Concat(SBTempsFields.Name, " = '", SBName, "'");
            DataRow[] rows = _dsSBTemps.Tables[0].Select(filter);

            if (rows.Length == 0)
            {
                _ErrorMessage = "Unable to copy Storyboard Template Record to Update.";
                return null;
            }

            return rows[0];

        }

        public string[] GetSBNames(string MatrixTemplateName)
        {
            _ErrorMessage = string.Empty;

            List<string> names = new List<string>();

            if (_dsSBTemps == null)
            {
                LoadData();
                if (_dsSBTemps == null)
                {
                    _ErrorMessage = string.Concat("Unable to open Storyboard XML file: ", _DataPathFile);
                    return null;
                }
            }

            string name = string.Empty;

            string filter = string.Concat(SBTempsFields.MatrixTemplate, " = '", MatrixTemplateName, "'");
            DataRow[] rows = _dsSBTemps.Tables[0].Select(filter);

            foreach (DataRow row in rows)
            {
                name = row[SBTempsFields.Name].ToString();

                names.Add(name);
            }

            return names.ToArray();
        }

        public bool DeleteSBTemplate(string Name)
        {
            _ErrorMessage = string.Empty;

            if (_dsSBTemps == null)
            {
                LoadData();
                if (_dsSBTemps == null)
                {
                    _ErrorMessage = string.Concat("Unable to open Storyboard XML file: ", _DataPathFile);
                    return false;
                }
            }

            string filter = string.Concat(SBTempsFields.Name, " = '", Name, "'");
            DataRow[] rows = _dsSBTemps.Tables[0].Select(filter);

            if (rows.Length == 0)
            {
                _ErrorMessage = "Unable to find Storyboard Template Record to Delete.";
                return false;
            }

            rows[0].Delete();

            _dsSBTemps.Tables[0].AcceptChanges();

            DataFunctions.SaveDataXML(_dsSBTemps, _DataPathFile);

            if (DataFunctions._ErrorMessage.Length > 0)
            {
                _ErrorMessage = DataFunctions._ErrorMessage;
                return false;
            }      

            string sbTempPath = Path.Combine(_SBPath, Name);

            string prifix = "~";
            string newPath = sbTempPath;

            while (Directory.Exists(newPath))
            {
                newPath = string.Concat(_SBPath, @"\", prifix, Name);
                if (Directory.Exists(newPath))
                {
                    prifix = "~" + prifix;
                }
                else
                {
                    break;
                }
            }

            try
            {
                Directory.Move(sbTempPath, newPath);
            }
            catch (Exception ex)
            {
                _ErrorMessage = string.Concat("An error occurred while attempting to remove a Storyboard.", Environment.NewLine, Environment.NewLine, "Error: ", ex.Message);
                return false;
            }

            return true;

        }

        public bool UpdateSBTemplate(string Name, string Description, string OrgWordTempFile, string MatrixTemplate, bool wasFieldsAdded, string user)
        {
            _ErrorMessage = string.Empty;

            if (_dsSBTemps == null)
            {
                LoadData();
                if (_dsSBTemps == null)
                {
                    _ErrorMessage = string.Concat("Unable to open Storyboard XML file: ", _DataPathFile);
                    return false;
                }
            }
                
            _CurrentSBTempPath = Path.Combine(_SBPath, Name);

            if (!Directory.Exists(_CurrentSBTempPath))
            {
                try
                {
                    Directory.CreateDirectory(_CurrentSBTempPath);
                }
                catch (Exception ex)
                {
                    _ErrorMessage = string.Concat("Unable to create Storyboard folder.  Error: ", ex.Message);
                    return false;
                }
            }

            if (OrgWordTempFile != string.Empty)
            {
                try // Copy the selected SB Template
                {
                    string filepath = Path.Combine(_CurrentSBTempPath, _SBTEMP_FILE);
                    File.Copy(OrgWordTempFile, filepath);

                    filepath = Path.Combine(_CurrentSBTempPath, _SBTEMP_VIEW_FILE);
                    File.Copy(OrgWordTempFile, filepath);

                }
                catch (Exception ex2)
                {
                    _ErrorMessage = string.Concat("Unable to copy Storyboard Template.  Error: ", ex2.Message);
                    return false;
                }
            }

            string filter = string.Concat(SBTempsFields.Name, " = '", Name, "'");
            DataRow[] rows = _dsSBTemps.Tables[0].Select(filter);

            if (rows.Length == 0)
            {
                _ErrorMessage = "Unable to copy Storyboard Template Record to Update.";
                return false;
            }

            rows[0][SBTempsFields.Description] = Description;
            rows[0][SBTempsFields.MatrixTemplate] = MatrixTemplate;
            rows[0][SBTempsFields.FieldsAdded] = wasFieldsAdded;
            if (OrgWordTempFile != string.Empty)
            {
                rows[0][SBTempsFields.OrgWordTempFile] = OrgWordTempFile;
            }
            rows[0][SBTempsFields.CreatedBy] = user;
            rows[0][SBTempsFields.CreationDate] = DateTime.Now;

            _dsSBTemps.AcceptChanges();
            
            DataFunctions.SaveDataXML(_dsSBTemps, _DataPathFile);

            if (DataFunctions._ErrorMessage.Length > 0)
            {
                _ErrorMessage = DataFunctions._ErrorMessage;
                return false;
            }

            return true;
        }

        public bool CreateSBTemplate(string Name, string Description, string OrgWordTempFile, string MatrixTempPathTempFile, string MatrixTemplate, bool wasFieldsAdded, string user)
        {
            _ErrorMessage = string.Empty;

            if (_dsSBTemps == null)
                if (!CreateEmptyDataSet())
                    return false;

            if (SBExists(Name))
            {
                _ErrorMessage = string.Concat("Storyboard Template with the name '", Name, "' already exists. Please enter another name.");
                return false;
            }

            if (!File.Exists(MatrixTempPathTempFile))
            {
                _ErrorMessage = string.Concat("Unable to find Storyboard Template: ", OrgWordTempFile);
                return false;
            }

            _CurrentSBTempPath = Path.Combine(_SBPath, Name);

            if (!Directory.Exists(_CurrentSBTempPath))
            {
                try
                {
                    Directory.CreateDirectory(_CurrentSBTempPath);
                }
                catch (Exception ex)
                {
                    _ErrorMessage = string.Concat("Unable to create Storyboard folder.  Error: ", ex.Message);
                    return false;
                }
            }

            try // Copy the selected SB Template
            {
                string filepath = Path.Combine(_CurrentSBTempPath, _SBTEMP_FILE);
                File.Copy(MatrixTempPathTempFile, filepath);

                filepath = Path.Combine(_CurrentSBTempPath, _SBTEMP_VIEW_FILE);
                File.Copy(MatrixTempPathTempFile, filepath);

            }
            catch (Exception ex2)
            {
                _ErrorMessage = string.Concat("Unable to copy Storyboard Template.  Error: ", ex2.Message);
                return false;
            }
            

            int uid = 0;

            if (_dsSBTemps.Tables[0].Rows.Count > 0)
                uid = DataFunctions.FindMaxValue(_dsSBTemps.Tables[0], RefResFields.UID) + 1;

            DataRow row = _dsSBTemps.Tables[0].NewRow();
            row[SBTempsFields.UID] = uid;
            row[SBTempsFields.Name] = Name;
            row[SBTempsFields.FieldsAdded] = wasFieldsAdded; // As fields added to the MS Word Template
            row[SBTempsFields.Description] = Description;
            row[SBTempsFields.MatrixTemplate] = MatrixTemplate;
            row[SBTempsFields.OrgWordTempFile] = OrgWordTempFile;
            row[SBTempsFields.CreatedBy] = user;
            row[SBTempsFields.CreationDate] = DateTime.Now;
       

            _dsSBTemps.Tables[0].Rows.Add(row);

            DataFunctions.SaveDataXML(_dsSBTemps, _DataPathFile);

            if (DataFunctions._ErrorMessage.Length > 0)
            {
                _ErrorMessage = DataFunctions._ErrorMessage;
                return false;
            }

            return true;
        }


        public string GetSBTempViewPathFile(string Name)
        {
            _ErrorMessage = string.Empty;

            _CurrentSBTempPath = Path.Combine(_SBPath, Name);

            string realPathFile = Path.Combine(_CurrentSBTempPath, _SBTEMP_FILE);  
            string viewPathFile = Path.Combine(_CurrentSBTempPath, _SBTEMP_VIEW_FILE);

            if (!File.Exists(realPathFile))
            {
                _ErrorMessage = string.Concat("Unable to find Storyboard Template: ", realPathFile);
                return string.Empty;
            }

            try
            {
                if (!File.Exists(viewPathFile))
                {
                    File.Copy(realPathFile, viewPathFile);
                }
                else
                {
                    DateTime ftimeReal = File.GetLastWriteTime(realPathFile);
                    DateTime ftimeView = File.GetLastWriteTime(viewPathFile);

                    if (ftimeReal != ftimeView)
                    {
                        File.Copy(realPathFile, viewPathFile, true);
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = string.Concat("An error occurred while getting the Storyboard View file.   Error: ", ex.Message);
                return string.Empty;
            }

            return viewPathFile;
        }

        public string GetSBTempPathFile(string Name)
        {
            _ErrorMessage = string.Empty;

            _CurrentSBTempPath = Path.Combine(_SBPath, Name);

            string filepath = Path.Combine(_CurrentSBTempPath, _SBTEMP_FILE);
            if (!File.Exists(filepath))
            {
                _ErrorMessage = string.Concat("Unable to find Storyboard Template: ", filepath);
                return string.Empty;
            }

            return filepath;
        }


        public bool SBExists(string Name)
        {

            if (_dsSBTemps == null)
                return false;

            if (_dsSBTemps.Tables[0].Rows.Count == 0)
                return false;

            string filter = string.Concat(SBTempsFields.Name, " = '", Name + "'");

            DataRow[] foundValue = _dsSBTemps.Tables[0].Select(filter);
            if (foundValue.Length != 0)
            {
                return true;
            }

            return false;
        }

    }
}
