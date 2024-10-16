using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;


namespace WorkgroupMgr
{
    public class Workgroups
    {
        /// <summary>
        /// For Local Workgroup
        /// </summary>
        /// <param name="PathLocalData"></param>
        /// <param name="UserName"></param>
        /// <param name="UserInfoPathFile"></param>
        public Workgroups(string PathLocalData, string UserName, string UserInfoPathFile)
        {
            _PathLocalData = PathLocalData;
            _UserName = UserName;
            _UserInfoPathFile = UserInfoPathFile;

            _isWorkgroupLocal = true;

            ValidateFix(true);
        }

        /// <summary>
        /// For Non-Local Workgroup
        /// </summary>
        /// <param name="PathLocalData"></param>
        /// <param name="PathWorkgroupRoot"></param>
        /// <param name="UserName"></param>
        /// <param name="UserInfoPathFile"></param>
        public Workgroups(string PathLocalData, string PathWorkgroupRoot, string UserName, string UserInfoPathFile)
        {
            _PathLocalData = PathLocalData;
            _UserName = UserName;
            _UserInfoPathFile = UserInfoPathFile;
            _PathWorkgroupRoot = PathWorkgroupRoot;

            _isWorkgroupLocal = false;
        }

        #region Message Properties

        string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        string _NoticeMessage = string.Empty;
        public string NoticeMessage
        {
            get { return _NoticeMessage; }
        }

        #endregion


        private string _PathLocalData = string.Empty;
        public string PathLocalData
        {
            get { return _PathLocalData; }
            set 
            { 
                _PathLocalData = value;
                
            }
        }

        private string _UserName = string.Empty;

        private string _UserInfoPathFile = string.Empty;
        public string UserInfoPathFile
        {
            get { return _UserInfoPathFile; }
            set { _UserInfoPathFile = value; }
        }

        #region Project Paths

        private string _ProjectRootPath = string.Empty;
        public string ProjectRootPath
        {
            get
            {
                if (_isWorkgroupLocal)
                {
                    _ProjectRootPath = Path.Combine(_PathLocalData, "Projects");
                }
                else
                {
                    _ProjectRootPath = Path.Combine(_PathWorkgroupRoot, "Projects");
                }

                return _ProjectRootPath;
            }
        }


        #endregion

        #region Local Workgroup Properties and Var.s

        private const string _WORKGROUP_DESCRIP_FILENAME = "WorkgroupDescrip.txt";

        private const string _WORKGROUP_CATALOGUE = "WorkgroupCatalogue.xml";

        private const string _WORKGROUP_INFO = "WorkgroupInfo.xml";

        private const string _LOCAL_DESCRIPTION = "The Local Workgroup is the Default Workgroup and is auto-generated. All Local Workgroup data are stored on your computer.";

        private string _WorkgroupCataloguePathFile = string.Empty;
        private string _WorkgroupInfoPathFile = string.Empty;

        private string _LocalWorkgroupPathFileDescrip = string.Empty;


        private string _PathLocalWorkgroup = string.Empty; 
        public string PathLocalWorkgroup  // Must run ValidateFix 1st
        {
            get { return _PathLocalWorkgroup; }
        }

        private string _PathLocalWorkgroupBackup = string.Empty;

        private string _PathLocalWorkgroupList = string.Empty;
        public string PathLocalWorkgroupList  // Must run ValidateFix 1st
        {
            get {return _PathLocalWorkgroupList;}
        }

        private string _PathLocalWorkgroupDocsType = string.Empty;
        public string PathLocalWorkgroupDocsType // Must run ValidateFix 1st
        {
            get { return _PathLocalWorkgroupDocsType; }
        }

        private string _PathLocalWorkgroupDataSources = string.Empty;
        public string PathLocalWorkgroupDataSources // Must run ValidateFix 1st
        {
            get { return _PathLocalWorkgroupDataSources; }
        }

        private string _PathLocalWorkgroupMatrixTemp = string.Empty;
        public string PathLocalWorkgroupMatrixTemp // Must run ValidateFix 1st
        {
            get { return _PathLocalWorkgroupMatrixTemp; }
        }

        private string _PathLocalWorkgroupMatrixTemplates = string.Empty;
        public string PathLocalWorkgroupMatrixTemplates
        {
            get { return _PathLocalWorkgroupMatrixTemplates; }
        }

        private string _PathLocalWorkgroupMatrixTempTemp = string.Empty;
        public string PathLocalWorkgroupMatrixTempTemp // Must run ValidateFix 1st
        {
            get { return _PathLocalWorkgroupMatrixTempTemp; }
        }

        private string _PathLocalWorkgroupMatrixTempSB = string.Empty;
        public string PathLocalWorkgroupMatrixTempSB // Must run ValidateFix 1st
        {
            get { return _PathLocalWorkgroupMatrixTempSB; }
        }

        #endregion

        #region Workgroup Properties & Vars

 

        private string _PathWorkgroupRoot = string.Empty;
        public string PathCurrent
        {
            get { return _PathWorkgroupRoot; }
            set
            {
                _PathWorkgroupRoot = value;
            }
        }

        private string _PathWorkgroup = string.Empty;
        public string PathWorkgroup  // Must run ValidateFix 1st
        {
            get { return _PathWorkgroup; }
        }

        private string _PathWorkgroupUsers = string.Empty;
        public string PathWorkgroupUsers
        {
            get { return _PathWorkgroupUsers; }
        }

        private string _PathWorkgroupList = string.Empty;
        public string PathWorkgroupList  // Must run ValidateFix 1st
        {
            get { return _PathWorkgroupList; }
        }

        private string _PathWorkgroupDocsType = string.Empty;
        public string PathWorkgroupDocsType // Must run ValidateFix 1st
        {
            get { return _PathWorkgroupDocsType; }
        }

        private string _PathWorkgroupDataSources = string.Empty;
        public string PathWorkgroupDataSources // Must run ValidateFix 1st
        {
            get { return _PathWorkgroupDataSources; }
        }

        private string _PathWorkgroupMatrixTemp = string.Empty;
        public string PathWorkgroupMatrixTemp // Must run ValidateFix 1st
        {
            get { return _PathWorkgroupMatrixTemp; }
        }

        private string _PathWorkgroupMatrixTemplates = string.Empty;
        public string PathWorkgroupMatrixTemplates
        {
            get { return _PathWorkgroupMatrixTemplates; }
        }

        private string _PathWorkgroupMatrixTempTemp = string.Empty;
        public string PathWorkgroupMatrixTempTemp // Must run ValidateFix 1st
        {
            get { return _PathWorkgroupMatrixTempTemp; }
        }

        private string _PathWorkgroupMatrixTempSB = string.Empty;
        public string PathWorkgroupMatrixTempSB // Must run ValidateFix 1st
        {
            get { return _PathWorkgroupMatrixTempSB; }
        }
        #endregion

        #region Flags

        private bool _isWorkgroupLocal = true;

        #endregion 

        #region public functions

        public bool WorkgroupExists(string rootPath) // Not for Local Workgroup
        {
            string workgroupPath = Path.Combine(rootPath, "Workgroup");
            if (Directory.Exists(workgroupPath))
            {
                return true;
            }
            else
            {
                string lastFolder = Files.GetLastFolder(rootPath);
                if (lastFolder.ToLower() == "workgroup")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        
        public bool WorkgroupCreate(string rootPath, string workgroupName, string workgroupDescription)
        {
            _PathWorkgroupRoot = rootPath;
            _isWorkgroupLocal = false;

            ValidateFix(true); // Check and set local workgroup paths
            ValidateFix(false); 

            if (!WorkgroupInfo_CreateNew(workgroupName, workgroupDescription)) // The Workgroup Information file is stored at the Workgroup folder level
            {
                return false;
            }

            ValidateFix(false); // run it again to fix any issue not fixed in the previous run

            if (!WorkgroupCatalogue_Add(workgroupName, workgroupDescription, rootPath))
            {
                return false;
            }

            return true;
        }

        public string GetUserProfile(string userName)
        {
            _ErrorMessage = string.Empty;

            if (_PathWorkgroupUsers == string.Empty)
            {
                _ErrorMessage = "Workgroup User information path not given.";
                return string.Empty;
            }

            if (!Directory.Exists(_PathWorkgroupUsers))
            {
                _ErrorMessage = string.Concat("Unable to file Workgroup User Information path: ", _PathWorkgroupUsers);
                return string.Empty;
            }


            string[] files = Directory.GetFiles(_PathWorkgroupUsers);
            if (files.Length == 0)
            {
                _ErrorMessage = string.Concat("No User Information files were Not found at folder: ", _UserInfoPathFile);
                return string.Empty; 
            }

            string adjUserName = userName.Replace(" ", "_");
            foreach (string file in files)
            {
                if (file.IndexOf(adjUserName) > -1)
                {
                    UserCardMgr userCardMgr = new UserCardMgr(_PathWorkgroupUsers);
                    if (!userCardMgr.ReadUserFile(file))
                    {
                        _ErrorMessage = userCardMgr.ErrorMessage;
                        return string.Empty;
                    }

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(" ");
                    sb.AppendLine(string.Concat(userCardMgr.UserTitle));
                    sb.AppendLine(string.Concat(userCardMgr.UserEmail));
                    sb.AppendLine(string.Concat(userCardMgr.UserPhone));

                    return sb.ToString();
                }
            }

            _ErrorMessage = string.Concat("Unable to find user profile for user: ", userName, " within path: ", _PathWorkgroupUsers);

            return string.Empty;
        }
      

        /// <summary>
        /// Adds an existing Workgroup to an User's list of Workgroups
        /// </summary>
        /// <param name="rootPath">The path prior to Workgroups</param>
        /// <param name="workgroupName">Name of workgroup</param>
        /// <param name="description">Description of workgroup</param>
        /// <returns>True if workgroup as added to user's list. Otherwise False</returns>
        public bool WorkgroupConnection_Add(string rootPath, bool isLocal)
        {
            _ErrorMessage = string.Empty;
            if (_WorkgroupCataloguePathFile.Length == 0)
            {
                _ErrorMessage = "Workgroup Catalogue Not Defined";
                return false;
            }

            // Does the Workgroup Catalogue file exists?
            if (!File.Exists(_WorkgroupCataloguePathFile))
            {
                if (!WorkgroupCatalogue_CreateCheckLocal())
                    return false;
                else
                    return true;
            }

            if (_UserInfoPathFile == string.Empty)
            {
                _ErrorMessage = "User information path not given.";
                return false;
            }

            if (_UserInfoPathFile.IndexOf(".AUsr") == -1)
            {
               string[] files = Directory.GetFiles(_UserInfoPathFile);
               if (files.Length == 0)
               {
                   string.Concat("No Information file was Not found at folder: ", _UserInfoPathFile);
                   return false;
               }

               _UserInfoPathFile = files[0];
            }

            if (!File.Exists(_UserInfoPathFile))
            {
                _ErrorMessage = string.Concat("Your User Information file was Not found.", Environment.NewLine, Environment.NewLine, "File: ", _UserInfoPathFile);
                return false;
            }

            DataSet ds = DataFunctions.LoadDatasetFromXml(_WorkgroupCataloguePathFile);
            if (ds == null)
            {
                _ErrorMessage = DataFunctions._ErrorMessage;
                return false;
            }

            // Is this workgroup already in the Workgroup Catalogue 
            if (DataFunctions.FindValueInDataTable(ds.Tables[0], WorkgroupCatalogueFields.WorkgroupRootPath, rootPath))
            {
                return true;
            }

            int uid = 0;

            DataRow row = ds.Tables[0].NewRow();
            if (ds.Tables[0].Rows.Count > 0)
            {
                uid = DataFunctions.FindMaxValue(ds.Tables[0], WorkgroupCatalogueFields.UID) + 1;
            }
            row[WorkgroupCatalogueFields.UID] = uid;

            if (isLocal)
            {
                row[WorkgroupCatalogueFields.WorkgroupName] = "Local";
                row[WorkgroupCatalogueFields.WorkgroupDescription] = _LOCAL_DESCRIPTION;
            }
            else
            {

                string workgroupPath = Path.Combine(rootPath, "Workgroup");
                if (!Directory.Exists(workgroupPath))
                {
                    _ErrorMessage = string.Concat("Unable to find Workgroup folder: ", workgroupPath);
                    return false;
                }

                string workgroupPathFile = Path.Combine(workgroupPath, "WorkgroupInfo.xml");
                if (!File.Exists(workgroupPathFile))
                {
                    _ErrorMessage = string.Concat("Unable to find Workgroup Information File: ", workgroupPathFile);
                    return false;
                }

                DataSet dsWorkgroupInfo = DataFunctions.LoadDatasetFromXml(workgroupPathFile);
                if (dsWorkgroupInfo == null)
                {
                    _ErrorMessage = DataFunctions._ErrorMessage;
                    return false;
                }

                row[WorkgroupCatalogueFields.WorkgroupName] = dsWorkgroupInfo.Tables[WorkgroupInfoFields.TableName].Rows[0][WorkgroupInfoFields.WorkgroupName].ToString();
                row[WorkgroupCatalogueFields.WorkgroupDescription] = dsWorkgroupInfo.Tables[WorkgroupInfoFields.TableName].Rows[0][WorkgroupInfoFields.WorkgroupDescription].ToString();;
            }
            row[WorkgroupCatalogueFields.WorkgroupRootPath] = rootPath;

            ds.Tables[0].Rows.Add(row);

            DataFunctions.SaveDataXML(ds, _WorkgroupCataloguePathFile);

            if (DataFunctions._ErrorMessage.Length > 0)
            {
                _ErrorMessage = DataFunctions._ErrorMessage;

                return false;
            }

            // Copy User Infor to Workgroup
            if (!isLocal)
            {
                try
                {
                    string userFile = Files.GetFileName(_UserInfoPathFile);
                    string user2PathFile = Path.Combine(_PathWorkgroupUsers, userFile);

                    if (!File.Exists(user2PathFile))
                    {
                        File.Copy(_UserInfoPathFile, user2PathFile);
                    }
                }
                catch (Exception ex)
                {
                    _ErrorMessage = string.Concat("An Error occurred while copying your User Information file to a Workgroup.       Error: ", ex.Message);
                    return false;
                }
            }

            return true;
        }

        public bool WorkgroupConnection_Remove(string workgroupName, out DataTable dt)
        {
            _ErrorMessage = string.Empty;

            if (workgroupName == "Local")
            {
                _ErrorMessage = "Unable to disconnect form Local Workgroup.";
                dt = null;
                return false;
            }

            // Does the Workgroup Catalogue file exists?
            if (!File.Exists(_WorkgroupCataloguePathFile))
            {
                if (!WorkgroupCatalogue_CreateCheckLocal())
                {
                    DataSet dsLocal = DataFunctions.LoadDatasetFromXml(_WorkgroupCataloguePathFile);
                    dt = dsLocal.Tables[WorkgroupCatalogueFields.TableName];
                    return true;
                }                
            }


            DataSet ds = DataFunctions.LoadDatasetFromXml(_WorkgroupCataloguePathFile);
            if (ds == null)
            {
                _ErrorMessage = DataFunctions._ErrorMessage;
                dt = null;
                return false;
            }

            string filter = string.Concat(WorkgroupCatalogueFields.WorkgroupName, " = '", workgroupName, "'");

            DataRow[] rows = ds.Tables[WorkgroupCatalogueFields.TableName].Select(filter);
            if (rows.Length > 0)
            {
                rows[0].Delete();
            }


            ds.Tables[WorkgroupCatalogueFields.TableName].AcceptChanges();
            ds.AcceptChanges();

            dt = ds.Tables[WorkgroupCatalogueFields.TableName];

            DataFunctions.SaveDataXML(ds, _WorkgroupCataloguePathFile);

          
            return true;
        }

        public bool WorkgroupConnection_Remove(int uid, bool isLocal)
        {
            _ErrorMessage = string.Empty;
            if (_WorkgroupCataloguePathFile.Length == 0)
            {
                _ErrorMessage = "Workgroup Catalogue Not Defined";
                return false;
            }

            // Does the Workgroup Catalogue file exists?
            if (!File.Exists(_WorkgroupCataloguePathFile))
            {
                if (WorkgroupCatalogue_CreateCheckLocal())
                    return true;              
            }

            DataSet ds = DataFunctions.LoadDatasetFromXml(_WorkgroupCataloguePathFile);
            if (ds == null)
            {
                _ErrorMessage = DataFunctions._ErrorMessage;
                return false;
            }

            for (int i = ds.Tables[0].Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = ds.Tables[0].Rows[i];
                if ((int)dr[WorkgroupCatalogueFields.UID] == uid)
                {
                    dr.Delete();
                    return true;
                }
            }

            // Remove User from Workgroup
            if (!isLocal)
            {
                string userFile = Files.GetFileName(_UserInfoPathFile);
                string userPathFile = Path.Combine(_PathWorkgroupUsers, userFile);

                if (File.Exists(userPathFile))
                {
                    File.Delete(userPathFile);
                }
            }

            return false;

        }

        public DataTable GetWorkGroupList()
        {
            _ErrorMessage = string.Empty;

            _WorkgroupCataloguePathFile = Path.Combine(_PathLocalWorkgroup, _WORKGROUP_CATALOGUE);

            if (!File.Exists(_WorkgroupCataloguePathFile))
            {
                _ErrorMessage = string.Concat("Unable to file Workgroup Catalogue file: ", _WorkgroupCataloguePathFile);
                return null;
            }

            DataSet ds = DataFunctions.LoadDatasetFromXml(_WorkgroupCataloguePathFile);
            if (ds == null)
            {
                _ErrorMessage = DataFunctions._ErrorMessage;
                return null;
            }

            return ds.Tables[0];
        }

        public List<string> GetWorkgroupMembers()
        {
            _ErrorMessage = string.Empty;

            if (!SetWorkspacePaths())
            {
                return null;
            }

            List<string> membersList = new List<string>();
            try
            {
                string[] files = Directory.GetFiles(_PathWorkgroupUsers, "*.AUsr");

                if (files.Length == 0)
                {
                    files = Directory.GetFiles(_UserInfoPathFile, "*.AUsr");
                    if (files.Length == 0)
                    {
                        _ErrorMessage = string.Concat("Unable to find your User Information file at this location: ", _UserInfoPathFile);
                        return null;
                    }

                    if (!Files.CopyFile2NewLocation(_UserInfoPathFile, _PathWorkgroupUsers, Files.GetFileName(files[0]), "User Information"))
                    {
                        _ErrorMessage = string.Concat("Unable to copy your User Information to the Workgroup.", Environment.NewLine, Environment.NewLine, Files.ErrorMessage);
                        return null;
                    }

                    files = Directory.GetFiles(_PathWorkgroupUsers, "*.AUsr");
                }


                string fileName = string.Empty;
                string memberName = string.Empty;
                foreach (string pathFiles in files)
                {
                    fileName = Files.GetFileNameWOExt(pathFiles);
                    string[] prts = fileName.Split('_');
                    if (prts.Length > 1)
                    {
                        for (int i = 0; i < prts.Length - 1; i++)
                        {
                            if (i == 0)
                            {
                                memberName = prts[0];
                            }
                            else
                            {
                                memberName = string.Concat(memberName, " ", prts[i]);
                            }
                        }
                        membersList.Add(string.Concat(memberName, "|", pathFiles));
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = string.Concat("An Error occurred while getting Workgroup Memebers.  Error: ", ex.Message);
                return null;
            }
            
            return membersList;
        }




        // ToDo public bool WorkgroupConnection_Update ...

        public bool UseLocalWorkgroup()
        {
            if (!ValidateFix(true)) // check, fix, & set Local Workgroup folders
                return false;

            _PathWorkgroupRoot = _PathLocalData;

           if ( ValidateFix(false)) // set Local Workgroup folders = Current Workgroup;
               return false;

            _isWorkgroupLocal = true;

            return true;
        }

        public bool ValidateFix(bool isLocal)
        {
            _ErrorMessage = string.Empty;
            _NoticeMessage = string.Empty;

            if (isLocal)
            {
                if (!SetLocalWorkspacePaths())
                {
                    return false;
                }

                if (!LocalWorkgroupDescripExistsFix())
                    return false;

               
            }
            else // A shared workgroup
            {
                if (!SetWorkspacePaths())
                {
                    return false;
                }            
            }

            if (!WorkgroupCatalogue_CreateCheckLocal())
            {
                return false;
            }


            return true;

        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool LocalWorkgroupDescripExistsFix()
        {
            _ErrorMessage = string.Empty;

            if (!File.Exists(_LocalWorkgroupPathFileDescrip))
            {

                try
                {
                    Files.WriteStringToFile(_LOCAL_DESCRIPTION, _LocalWorkgroupPathFileDescrip);
                }
                catch (Exception ex)
                {
                    string.Concat("Error: Unable to create Local Workgroup Description File.", Environment.NewLine, Environment.NewLine, ex.Message);
                    return true;
                }
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

        /// <summary>
        /// Checks if Local Workspace folders exits, if not will attempt to create it. 
        /// If unable to create a folder, the Error Message var is populated with the error message and returns False
        /// </summary>
        /// <returns></returns>
        private bool SetLocalWorkspacePaths()
        {
            _ErrorMessage = string.Empty;
            _NoticeMessage = string.Empty;

            if (_PathLocalData.Trim().Length == 0)
            {
                _ErrorMessage = "Local Data Path is not Defined";
                return false;
            }

            _PathLocalWorkgroup = Path.Combine(_PathLocalData, "Workgroup");
            if (!CheckCreateDir(_PathLocalWorkgroup))
                return false;

            _PathLocalWorkgroupBackup = Path.Combine(_PathLocalWorkgroup, "Backup");
            if (!CheckCreateDir(_PathLocalWorkgroupBackup))
                return false;

            _PathLocalWorkgroupList = Path.Combine(_PathLocalWorkgroup, "Lists");
            if (!CheckCreateDir(_PathLocalWorkgroupList))
                return false;

            _PathLocalWorkgroupDocsType  = Path.Combine(_PathLocalWorkgroup, "DocTypes");
            if (!CheckCreateDir(_PathLocalWorkgroupDocsType))
                return false;

            _PathLocalWorkgroupDataSources = Path.Combine(_PathLocalWorkgroup, "DataSources");
            if (!CheckCreateDir(_PathLocalWorkgroupDataSources))
                return false;

            _PathLocalWorkgroupMatrixTemp = Path.Combine(_PathLocalWorkgroup, "MatrixTemp"); // holds Matrix Template
            if (!CheckCreateDir(_PathLocalWorkgroupMatrixTemp))
                return false;

            _PathLocalWorkgroupMatrixTempTemp = Path.Combine(_PathLocalWorkgroupMatrixTemp, "Temp"); // Holds temporary files
            if (!CheckCreateDir(_PathLocalWorkgroupMatrixTempTemp))
                return false;

            _PathLocalWorkgroupMatrixTemplates = Path.Combine(_PathLocalWorkgroupMatrixTemp, "Templates"); // Holds temporary files
            if (!CheckCreateDir(_PathLocalWorkgroupMatrixTemplates))

            _PathLocalWorkgroupMatrixTempSB = Path.Combine(_PathLocalWorkgroupMatrixTemp, "MatrixTempSB");
            _PathLocalWorkgroupMatrixTempSB = Path.Combine(_PathLocalWorkgroupMatrixTemp, "MatrixTempSB"); // Strange error, had to place this code twice to work, has returning an emplay string
            if (!CheckCreateDir(_PathLocalWorkgroupMatrixTempSB))
                return false;

            _LocalWorkgroupPathFileDescrip = Path.Combine(_PathLocalWorkgroup, _WORKGROUP_DESCRIP_FILENAME);
            

            return true;
        }

        /// <summary>
        /// Checks if Workspace folders exits, if not will attempt to create it. 
        /// If unable to create a folder, the Error Message var is populated with the error message and returns False
        /// </summary>
        /// <returns></returns>
        private bool SetWorkspacePaths()
        {
            _ErrorMessage = string.Empty;
            _NoticeMessage = string.Empty;

            const string LONG_PATH_PREFIX = ""; // @"\\?\"; this was causing an error
            
            if (_PathWorkgroupRoot.Trim().Length == 0) // Root folder
            {
                _ErrorMessage = "Workspace Root Path is not Defined";
                return false;
            }

            // To specify an extended-length path, use the "\\?\" prefix. For example, "\\?\D:\very long path".
            //   Maximum total path length of 32,767 characters
            // See https://msdn.microsoft.com/en-us/library/windows/desktop/aa365247%28v=vs.85%29.aspx?f=255&MSPPError=-2147217396
            string adjPathWorkgroupRoot = string.Concat(LONG_PATH_PREFIX, _PathWorkgroupRoot);

            _PathWorkgroup = Path.Combine(adjPathWorkgroupRoot, "Workgroup"); // Workgroup folder
            if (!CheckCreateDir(_PathWorkgroup))
                return false;

            _PathWorkgroupUsers = Path.Combine(_PathWorkgroup, "Users");
            if (!CheckCreateDir(_PathWorkgroupUsers))
                return false;

            _PathWorkgroupList = Path.Combine(_PathWorkgroup, "Lists");
            if (!CheckCreateDir(_PathWorkgroupList))
                return false;

            _PathWorkgroupDocsType = Path.Combine(_PathWorkgroup, "DocTypes");
            if (!CheckCreateDir(_PathWorkgroupDocsType))
                return false;

            _PathWorkgroupDataSources = Path.Combine(_PathWorkgroup, "DataSources");
            if (!CheckCreateDir(_PathWorkgroupDataSources))
                return false;

            _PathWorkgroupMatrixTemp = Path.Combine(_PathWorkgroup, "MatrixTemp"); // holdes Matrix Template
            if (!CheckCreateDir(_PathWorkgroupMatrixTemp))
                return false;

            _PathWorkgroupMatrixTemplates = Path.Combine(_PathWorkgroupMatrixTemp, "Templates"); // Holds temporary files
            if (!CheckCreateDir(_PathWorkgroupMatrixTemplates))
                return false;
                
            _PathWorkgroupMatrixTempTemp = Path.Combine(_PathWorkgroup, "Temp"); // holds temporary files
            if (!CheckCreateDir(_PathWorkgroupMatrixTempTemp))
                return false;

            _PathWorkgroupMatrixTempSB = Path.Combine(_PathWorkgroupMatrixTemp, "MatrixTempSB");
            if (_PathWorkgroupMatrixTempSB.Length > 260)
            {
                _ErrorMessage = string.Concat("Unable to use this Workgroup.", Environment.NewLine, Environment.NewLine,
                    "It is estimated that this Workgroup will exceeded Windows' O/S maximum path length limits of 260 characters.",
                    Environment.NewLine, Environment.NewLine, "Suggest that your team use another shared location.");

                return false;
            }
            if (!CheckCreateDir(_PathWorkgroupMatrixTempSB))
                return false;



            return true;
        }

        #region Data Functions
        /// <summary>
        /// Generates a New Workgroup Information Data file
        /// </summary>
        /// <param name="WorkgroupName"></param>
        /// <param name="WorkgroupDescription"></param>
        /// <returns></returns>
        private bool WorkgroupInfo_CreateNew(string WorkgroupName, string WorkgroupDescription)
        {
            _ErrorMessage = string.Empty;
            
            _WorkgroupInfoPathFile = Path.Combine(_PathWorkgroup, _WORKGROUP_INFO);
            
            if (File.Exists(_WorkgroupInfoPathFile))
            {
                return true;
            }

            DataTable dt = new DataTable(WorkgroupInfoFields.TableName);

            dt.Columns.Add(WorkgroupInfoFields.UID, typeof(int));
            dt.Columns.Add(WorkgroupInfoFields.WorkgroupName, typeof(string));
            dt.Columns.Add(WorkgroupInfoFields.WorkgroupDescription, typeof(string));
            dt.Columns.Add(WorkgroupInfoFields.CreatedBy, typeof(string));
            dt.Columns.Add(WorkgroupInfoFields.CreationDate, typeof(DateTime));

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
           
            DataRow row = dt.NewRow();

            row[WorkgroupInfoFields.UID] = 0;
            row[WorkgroupInfoFields.WorkgroupName] = WorkgroupName;
            row[WorkgroupInfoFields.WorkgroupDescription] = WorkgroupDescription;
            row[WorkgroupInfoFields.CreatedBy] = _UserName;
            row[WorkgroupInfoFields.CreationDate] = DateTime.Now;

            dt.Rows.Add(row);

            DataFunctions.SaveDataXML(ds, _WorkgroupInfoPathFile);
            if (DataFunctions._ErrorMessage.Length > 0)
            {
                _ErrorMessage = DataFunctions._ErrorMessage;
                return false;
            }

            string workgroupDescriptionPathFile = Path.Combine(_PathWorkgroup, _WORKGROUP_DESCRIP_FILENAME);
            Files.WriteStringToFile(WorkgroupDescription, workgroupDescriptionPathFile);
            

            return true;
        }

        public bool WorkgroupCatalogue_Add(string WorkgroupName, string WorkgroupDescription, string WorkgroupRootPath)
        {
            _ErrorMessage = string.Empty;

            if (_WorkgroupCataloguePathFile == string.Empty)
                _WorkgroupCataloguePathFile = Path.Combine(_PathLocalWorkgroup, _WORKGROUP_CATALOGUE);


            if (!File.Exists(_WorkgroupCataloguePathFile))
            {
                _ErrorMessage = string.Concat("Unable to find your Workgroups Catalogue file: ", _ErrorMessage);
                return false;
            }

            string backupPathFile = string.Empty;

            Files.BackupFile(_PathLocalWorkgroup, _PathLocalWorkgroupBackup, _WORKGROUP_CATALOGUE, out backupPathFile);

            DataSet ds = DataFunctions.LoadDatasetFromXml(_WorkgroupCataloguePathFile);
            if (ds == null)
            {
                _ErrorMessage = DataFunctions._ErrorMessage;
                return false;
            }

            int newUID = DataFunctions.GetNewUID(ds.Tables[WorkgroupCatalogueFields.TableName]);

            DataRow row = ds.Tables[WorkgroupCatalogueFields.TableName].NewRow();

            row[WorkgroupCatalogueFields.UID] = newUID;
            row[WorkgroupCatalogueFields.WorkgroupName] = WorkgroupName;
            row[WorkgroupCatalogueFields.WorkgroupDescription] = WorkgroupDescription;
            row[WorkgroupCatalogueFields.WorkgroupRootPath] = WorkgroupRootPath;

            ds.Tables[WorkgroupCatalogueFields.TableName].Rows.Add(row);

            File.Delete(_WorkgroupCataloguePathFile);

            DataFunctions.SaveDataXML(ds, _WorkgroupCataloguePathFile);

            if (!File.Exists(_WorkgroupCataloguePathFile))
            {
                if (!Files.RestoreLastBackup(_PathLocalWorkgroupBackup, _PathLocalWorkgroup, _WORKGROUP_CATALOGUE))
                {
                    _ErrorMessage = "Your Workgroup was created, but your Workgroup Catalog was not updated and was not Restored due to an Error.";
                    return false;
                }
                else
                {
                    _ErrorMessage = "Your Workgroup was created, but your Workgroup Catalog was not updated due to an Error.";
                    return false;
                }
            }

            UserCardMgr usrCardMgr = new UserCardMgr(_UserInfoPathFile);

            string[] files = usrCardMgr.GetUserInforFiles();

            if (files.Length == 0)
            {
                _ErrorMessage = string.Concat("Unable to find your User Information file at this location: ", _UserInfoPathFile); 
                return false;
            }

            if (!Files.CopyFile2NewLocation(_UserInfoPathFile, _PathWorkgroupUsers, Files.GetFileName(files[0]), "User Information"))
            {
                _ErrorMessage = string.Concat("Unable to copy your User Information to the Workgroup.", Environment.NewLine, Environment.NewLine, Files.ErrorMessage);
                return false;
            }

   
            return true;

        }

  

        /// <summary>
        /// Listing of an user's Workgroup
        /// </summary>
        /// <returns>True if XML WorkgroupCatalogue exists or was created. False if was unable to create. </returns>
        private bool WorkgroupCatalogue_CreateCheckLocal()
        {
            _ErrorMessage = string.Empty;

            SetLocalWorkspacePaths();

            _WorkgroupCataloguePathFile = Path.Combine(_PathLocalWorkgroup, _WORKGROUP_CATALOGUE);

            if (File.Exists(_WorkgroupCataloguePathFile))
                return true;


            DataTable dt = new DataTable(WorkgroupCatalogueFields.TableName);

            dt.Columns.Add(WorkgroupCatalogueFields.UID, typeof(int));
            dt.Columns.Add(WorkgroupCatalogueFields.WorkgroupName, typeof(string));
            dt.Columns.Add(WorkgroupCatalogueFields.WorkgroupDescription, typeof(string));
            dt.Columns.Add(WorkgroupCatalogueFields.WorkgroupRootPath, typeof(string));

            DataRow row = dt.NewRow();

            row[WorkgroupCatalogueFields.UID] = 0;
            row[WorkgroupCatalogueFields.WorkgroupName] = "Local";
            row[WorkgroupCatalogueFields.WorkgroupDescription] = _LOCAL_DESCRIPTION;
            row[WorkgroupCatalogueFields.WorkgroupRootPath] = _PathLocalData;

            dt.Rows.Add(row);

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            DataFunctions.SaveDataXML(ds, _WorkgroupCataloguePathFile);

            if (File.Exists(_LocalWorkgroupPathFileDescrip))
            {
                WorkgroupConnection_Add(_PathLocalData, true);
                return true;
            }
            else
            {
                _ErrorMessage = DataFunctions._ErrorMessage;
                return false;
            }

        }

  



        #endregion 
    }
}
