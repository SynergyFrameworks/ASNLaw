using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace WorkgroupMgr
{
    public class RefResMgr
    {
        public RefResMgr(string PathInternalWorkgroupDataSources)
        {
            _PathInternalWorkgroupDataSources = PathInternalWorkgroupDataSources;

            LoadData();
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

        private string _PathInternalWorkgroupDataSources = string.Empty; // 
        private const string _DATA_FILE = "RefRes.xml";
        private string _DataPathFile = string.Empty;

     //   private DataTable _dtRef;
        private DataSet _dsRefRes;


        private bool LoadData()
        {
            _ErrorMessage = string.Empty;

            if (_PathInternalWorkgroupDataSources == string.Empty)
            {
                _ErrorMessage = "Workgroup Internal Reference Resource folder is not defined.";
                return false;
            }

            if (!Directory.Exists(_PathInternalWorkgroupDataSources))
            {
                _ErrorMessage = "Cannot connect to Workgroup Internal Reference Resource or it does not exits.";
                return false;
            }

            _DataPathFile = Path.Combine(_PathInternalWorkgroupDataSources, _DATA_FILE);
            if (!File.Exists(_DataPathFile))
            {
                if (!CreateEmptyDataSet())
                {
                    return false;
                }
            }

            _dsRefRes = DataFunctions.LoadDatasetFromXml(_DataPathFile);

            if (_dsRefRes.Tables[0].Rows.Count == 0)
            {
                CreateSamples();
            }

            return true;
        }


        private bool CreateEmptyDataSet()
        {
            _ErrorMessage = string.Empty;

            if (File.Exists(_DataPathFile))
                return true;

            DataTable dtRef = new DataTable(RefResFields.TableName);

             dtRef.Columns.Add(RefResFields.UID, typeof(int));
             dtRef.Columns.Add(RefResFields.Name, typeof(string));
             dtRef.Columns.Add(RefResFields.Description, typeof(string));
             dtRef.Columns.Add(RefResFields.LocationType, typeof(string));
             dtRef.Columns.Add(RefResFields.Path, typeof(string));

             DataSet ds = new DataSet();

             ds.Tables.Add(dtRef);

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

       
        private bool CreateSamples()
        {
            _ErrorMessage = string.Empty;

            if (_dsRefRes.Tables[0].Rows.Count > 0)
                return true; // Sample NOT Needed

            string refResName = "Sample Win Themes";
            if (!CreateRefRes(refResName, "Example of Reference Resources", RefResFields.LocationType_Internal, string.Empty))
            {
                return false;
            }

            CreateRefResContent(refResName, "Performance", "Our approach improves performance by utilizing optimizing data indexing.");
            CreateRefResContent(refResName, "Implementation", "Our approach will produce benefits for you quicker by completing the implementation earlier.");
            CreateRefResContent(refResName, "Transition", "Because our available resources, we can accomplish the transition faster.");
            CreateRefResContent(refResName, "Licensing Terms", "Our licensing terms offer several advantages to you over those of our competition.");

            return true;
        }

        public bool RefResExists(string Name)
        {
            if (_dsRefRes == null)
                return false;

            if (_dsRefRes.Tables[0].Rows.Count == 0)
                return false;

            string filter = string.Concat(RefResFields.Name, " = '", Name + "'");

            //string filter = string.Empty;
            //if (LocationType == RefResFields.LocationType_Internal)
            //{
            //    filter = string.Concat(RefResFields.Name, " = '", Name + "' AND ", RefResFields.LocationType, " = '", RefResFields.LocationType_Internal);
            //}
            //else
            //{
            //    filter = string.Concat(RefResFields.Name, " = '", Name + "' AND ", RefResFields.Path, " = '", RefResFields.Path);
            //}

            DataRow[] foundValue = _dsRefRes.Tables[0].Select(filter);
            if (foundValue.Length != 0)
            {
                return true;
            }

            return false;
        }

        public bool CreateRefRes(string Name, string Description, string LocationType, string RefResPath)
        {
            _ErrorMessage = string.Empty;

            if (_dsRefRes == null)
                if (!CreateEmptyDataSet())
                    return false;

            if (RefResExists(Name))
            {
                _ErrorMessage = string.Concat("Reference Resource with the name '", Name, "' already exists. Please enter another name.");
                return false;
            }
       
            string internalNewRefResPath = string.Empty;
            

            if (LocationType == RefResFields.LocationType_Internal)
            {
                internalNewRefResPath = Path.Combine(_PathInternalWorkgroupDataSources, Name);
   
                if (!Directory.Exists(internalNewRefResPath))
                {
                    try
                    {
                        Directory.CreateDirectory(internalNewRefResPath);
                    }
                    catch (Exception ex)
                    {
                        _ErrorMessage = string.Concat("Unable to create Workgroup Internal Reference Resource folder.  Error: ", ex.Message);
                        return false;
                    }
                }
            }

            int uid = 0;

            if (_dsRefRes.Tables[0].Rows.Count > 0)
                uid = DataFunctions.FindMaxValue(_dsRefRes.Tables[0], RefResFields.UID) + 1;

            DataRow row = _dsRefRes.Tables[0].NewRow();
            row[RefResFields.UID] = uid;
            row[RefResFields.Name] = Name;
            row[RefResFields.Description] = Description;
            row[RefResFields.LocationType] = LocationType;
            row[RefResFields.Path] = RefResPath;

            _dsRefRes.Tables[0].Rows.Add(row);

            DataFunctions.SaveDataXML(_dsRefRes, _DataPathFile);

            if (DataFunctions._ErrorMessage.Length > 0)
            {
                _ErrorMessage = DataFunctions._ErrorMessage;
                return false;
            }

            return true;

        }

        public bool DeleteRefResContent(string RefResName, string ContentName)
        {
            _ErrorMessage = string.Empty;

            string path = GetRefResContentPath(RefResName);
            if (path == string.Empty)
                return false;

            string file = string.Concat(ContentName, ".txt");
           
            string  backupPath = CheckCreateBackupFolder(path);
            BackupContent(backupPath, path, file);

            string pathFile = Path.Combine(path, file);

            if (!File.Exists(pathFile))
            {
                _ErrorMessage = string.Concat("Unable to find Reference Resource Content file: ", pathFile);
                return false;
            }

            try
            {
                File.Delete(pathFile);
            }
            catch (Exception ex)
            {
                _ErrorMessage = string.Concat("Unable to remove Reference Resource Content '", ContentName, "' due to an error.      Error: ", ex.Message);
                return false;
            }

            return true;
        }

        public bool CreateRefResContent(string RefResName, string ContentName, string ContentText)
        {
            _ErrorMessage = string.Empty;

            string path = GetRefResContentPath(RefResName);
            if (path == string.Empty)
                return false;

            if (!Files.IsValidFilename(ContentName))
            {
                _ErrorMessage = @"Unable to save Reference Resource Content due to invaild charater(s) in the Name, such as <, >, :, /, \, |, ? and *";
                return false;
            }

            string file = string.Concat(ContentName, ".txt");
            string pathFile = Path.Combine(path, file);

            if (File.Exists(pathFile))
            {
                    string  backupPath = CheckCreateBackupFolder(path);
                    BackupContent(backupPath, path, file);
                    File.Delete(pathFile);
            }

            try
            {
                Files.WriteStringToFile(ContentText, pathFile);
            }
            catch (Exception ex)
            {
                _ErrorMessage = string.Concat("Unable to save Reference Resource Content due to an Error.   Error: ", ex.Message);
                return false;
            }

            return true;

        }

        public string[] GetRefResContentNames(string RefResName)
        {
            string path = GetRefResContentPath(RefResName);
            if (path == string.Empty)
            {
                return null;
            }

            string[] files = Directory.GetFiles(path, "*.txt", SearchOption.TopDirectoryOnly);
            List<string> fileNames = new List<string>();
            string fileName = string.Empty;

            foreach (string file in files)
            {
                fileName = Files.GetFileNameWOExt(file);
                if (fileName.IndexOf('~') == -1)
                    fileNames.Add(fileName);
            }

            return fileNames.ToArray();

        }

        public string GetRefResContentText(string RefResName, string ContentName)
        {
            _ErrorMessage = string.Empty;

            string path = GetRefResContentPath(RefResName);
            if (path == string.Empty)
            {
                return string.Empty;
            }

            string file = string.Concat(ContentName, ".txt");
            string pathFile = Path.Combine(path, file);

            if (!File.Exists(pathFile))
            {
                _ErrorMessage = string.Concat("Unable to find Reference Resource Content file: ", pathFile);
                return string.Empty;
            }

            string contentText = Files.ReadFile(pathFile);

            return contentText;

        }

        public string GetRefResContentPath(string RefResName)
        {
            _ErrorMessage = string.Empty;

            string path = string.Empty;
     
            DataRow row = GetRefRes(RefResName);

            if (row == null)
            {
                _ErrorMessage = string.Concat("Unable to find Reference Resource '", RefResName, "'.");
                return string.Empty; 
            }

  
            if (row[RefResFields.LocationType].ToString() == RefResFields.LocationType_Internal)
            {
                path = Path.Combine(_PathInternalWorkgroupDataSources, RefResName);
            }
            else
            {
                path = row[RefResFields.Path].ToString();

                if (path == string.Empty)
                {
                    _ErrorMessage = string.Concat("Reference Resource '", RefResName, "' path has not been defined.");
                    return string.Empty;
                }
            }

            if (!Directory.Exists(path))
            {
                _ErrorMessage = string.Concat("Reference Resource '", RefResName, "' path could not be found or you don’t have permission to access folder '", path, "'.");
                return string.Empty;
            }

            return path;
        }

        public bool DeleteRefRes(string Name)
        {
            _ErrorMessage = string.Empty;

            if (_dsRefRes == null)
                return false;

            if (_dsRefRes.Tables[0].Rows.Count == 0)
                return false;
            try
            {
                foreach (DataRow row in _dsRefRes.Tables[0].Rows)
                {
                    if (row[RefResFields.Name].ToString() == Name)
                    {
                        _dsRefRes.Tables[0].Rows.Remove(row);
                        _dsRefRes.AcceptChanges();

                        DataFunctions.SaveDataXML(_dsRefRes, _DataPathFile);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = string.Concat("Unable to remove Reference Resource '", Name, "' due to an error.    Error: ", ex.Message);
                return false;
            }

            return false;
        }

        public string[] GetRefResNames()
        {
            if (_dsRefRes == null)
                return null;

            if (_dsRefRes.Tables.Count == 0)
            {
                if (!CreateSamples())
                {
                    return null;
                }
            }

            if (_dsRefRes.Tables[0].Rows.Count == 0)
            {
                if (!CreateSamples())
                {
                    return null;
                }
            }

            List<string> refResNames = new List<string>();
            foreach (DataRow row in _dsRefRes.Tables[0].Rows)
            {
                refResNames.Add(row[RefResFields.Name].ToString());
            }

            return refResNames.ToArray();
        }

        public DataRow GetRefRes(string Name)
        {
            if (_dsRefRes == null)
                return null;

            if (_dsRefRes.Tables[0].Rows.Count == 0)
                return null;

            string filter = string.Empty;
 
                filter = string.Concat(RefResFields.Name, " = '", Name + "'");

            DataRow[] foundValue = _dsRefRes.Tables[0].Select(filter);

            if (foundValue.Length > 0)
            {
                return foundValue[0];
            }

            return null;

        }


        private string CheckCreateBackupFolder(string contentPath)
        {
            _ErrorMessage = string.Empty;

            string backupFolder = Path.Combine(contentPath, "Backup");
            if (!Directory.Exists(backupFolder))
            {
                try
                {
                    Directory.CreateDirectory(backupFolder);
                }
                catch (Exception ex)
                {
                    _ErrorMessage = string.Concat("Error: Unable to create the Reference Resource Content Backup folder: ", backupFolder, Environment.NewLine, Environment.NewLine, ex.Message);
                    return string.Empty;
                }
            }

            return backupFolder;
        }

        private bool BackupContent(string backupFolder, string contentPath, string file)
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

            string pathFile = Path.Combine(contentPath, file);

            try
            {
                File.Copy(pathFile, backupPathfile);
                return true;
            }
            catch (Exception ex)
            {
                _NoticeMessage = string.Concat("Notice: Unable to Backup Reference Resource Content.", Environment.NewLine, Environment.NewLine, ex.Message);
                return false;
            }
        }

    }
}
