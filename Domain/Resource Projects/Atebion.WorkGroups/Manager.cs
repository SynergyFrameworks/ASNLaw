using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using Atebion.Common;
using Atebion.UserCard;
using System.Windows.Forms;

namespace Atebion.WorkGroups
{
    public class Manager
    {

        // <summary>
        /// For Local Workgroup
        /// </summary>
        /// <param name="PathLocalData"></param>
        /// <param name="UserName"></param>
        /// <param name="UserInfoPathFile"></param>
        public Manager(string PathLocalData, string UserName, string UserInfoPathFile)
        {
            //MessageBox.Show(PathLocalData + UserInfoPathFile);
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
        public Manager(string PathLocalData, string PathWorkgroupRoot, string UserName, string UserInfoPathFile)
        {
            _PathLocalData = PathLocalData;
            _UserName = UserName;
            _UserInfoPathFile = UserInfoPathFile;
            _PathWorkgroupRoot = PathWorkgroupRoot;

            _isWorkgroupLocal = false;
        }

        string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        private string _PathLocalData = string.Empty;
        public string PathLocalData
        {
            get { return _PathLocalData; }
            set
            {
                _PathLocalData = value;

            }
        }

        private string _ApplicationPath = string.Empty;
        public string ApplicationPath
        {
            set { _ApplicationPath = value; }
        }



        private string _UserName = string.Empty;

        private string _ApplicationPathWG = string.Empty;
        public string ApplicationPathWG
        {
            get { return _ApplicationPathWG; }
            set { _ApplicationPathWG = value; }
        }

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
                    _ProjectRootPath = Path.Combine(_PathLocalData, "Folders"); // Changed from Projects to Folders for 2.7.30.XX
                }
                else
                {
                    _ProjectRootPath = Path.Combine(_PathWorkgroupRoot, "Folders"); // Changed from Projects to Folders for 2.7.30.XX
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

        private string _PathLocalDictionaries = string.Empty;
        public string PathLocalDictionaries
        {
            get { return _PathLocalDictionaries; }
        }

        private string _PathLocalTasks = string.Empty;
        public string PathLocalTasks
        {
            get { return _PathLocalTasks; }
        }

        private string _PathLocalKeywordsGrp = string.Empty;
        public string PathLocalKeywordsGrp
        {
            get { return _PathLocalKeywordsGrp; }
        }


        private string _PathLocalTools = string.Empty;
        public string PathLocalTools
        {
            get { return _PathLocalTools; }
        }

        private string _PathLocalToolsAcroSeeker = string.Empty;
        public string PathLocalToolsAcroSeeker
        {
            get { return _PathLocalToolsAcroSeeker; }
        }

        private string _PathLocalToolsAcroSeekerDefLib = string.Empty;
        public string PathLocalToolsAcroSeekerDefLib
        {
            get { return _PathLocalToolsAcroSeekerDefLib; }
        }

        private string _PathLocalToolsAcroSeekerIgnoreLib = string.Empty;
        public string PathLocalToolsAcroSeekerIgnoreLib
        {
            get { return _PathLocalToolsAcroSeekerIgnoreLib; }
        }

        private string _PathLocalToolsExcelTemp = string.Empty;
        public string PathLocalToolsExcelTemp
        {
            get { return _PathLocalToolsExcelTemp; }
        }

        private string _PathLocalToolsExcelTempAR = string.Empty;
        public string PathLocalToolsExcelTempAR
        {
            get { return _PathLocalToolsExcelTempAR; }
        }

        private string _PathLocalToolsExcelTempConceptsDoc = string.Empty;
        public string PathLocalToolsExcelTempConceptsDoc
        {
            get { return _PathLocalToolsExcelTempConceptsDoc; }
        }

        private string _PathLocalToolsExcelTempConceptsDocs = string.Empty;
        public string PathLocalToolsExcelTempConceptsDocs
        {
            get { return _PathLocalToolsExcelTempConceptsDocs; }
        }

        private string _PathLocalToolsExcelTempDAR = string.Empty;
        public string PathLocalToolsExcelTempDAR
        {
            get { return _PathLocalToolsExcelTempDAR; }
        }

        private string _PathLocalToolsExcelTempDicDoc = string.Empty;
        public string PathLocalToolsExcelTempDicDoc
        {
            get { return _PathLocalToolsExcelTempDicDoc; }
        }

        private string _PathLocalToolsExcelTempDicDocs = string.Empty;
        public string PathLocalToolsExcelTempDicDocs
        {
            get { return _PathLocalToolsExcelTempDicDocs; }
        }

        private string _PathLocalToolsExcelTempDicRAM = string.Empty;
        public string PathLocalToolsExcelTempDicRAM
        {
            get { return _PathLocalToolsExcelTempDicRAM; }
        }

        private string _PathLocalToolsQC = string.Empty;
        public string PathLocalToolsQC
        {
            get { return _PathLocalToolsQC; }
        }

        private string _PathLocalToolsRAMDefs = string.Empty;
        public string PathLocalToolsRAMDefs
        {
            get { return _PathLocalToolsRAMDefs; }
        }

        private string _PathLocalWorkgroupList = string.Empty;
        public string PathLocalWorkgroupList  // Must run ValidateFix 1st
        {
            get { return _PathLocalWorkgroupList; }
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

        private string _PathDictionaries = string.Empty;
        public string PathDictionaries
        {
            get { return _PathDictionaries; }
        }

        private string _PathKeywordsGrp = string.Empty;
        public string PathKeywordsGrp
        {
            get { return _PathKeywordsGrp; }
        }

        private string _PathTools = string.Empty;
        public string PathTools
        {
            get { return _PathTools; }
        }

        private string _PathToolsAcroSeeker = string.Empty;
        public string PathToolsAcroSeeker
        {
            get { return _PathToolsAcroSeeker; }
        }

        private string _PathToolsAcroSeekerDefLib = string.Empty;
        public string PathToolsAcroSeekerDefLib
        {
            get { return _PathToolsAcroSeekerDefLib; }
        }

        private string _PathToolsAcroSeekerIgnoreLib = string.Empty;
        public string PathToolsAcroSeekerIgnoreLib
        {
            get { return _PathToolsAcroSeekerIgnoreLib; }
        }

        private string _PathToolsExcelTemp = string.Empty;
        public string PathToolsExcelTemp
        {
            get { return _PathToolsExcelTemp; }
        }

        private string _PathToolsExcelTempAR = string.Empty;
        public string PathToolsExcelTempAR
        {
            get { return _PathToolsExcelTempAR; }
        }

        private string _PathToolsExcelTempConceptsDoc = string.Empty;
        public string PathToolsExcelTempConceptsDoc
        {
            get { return _PathToolsExcelTempConceptsDoc; }
        }

        private string _PathToolsExcelTempConceptsDocs = string.Empty;
        public string PathToolsExcelTempConceptsDocs
        {
            get { return _PathToolsExcelTempConceptsDocs; }
        }

        private string _PathToolsExcelTempDAR = string.Empty;
        public string PathToolsExcelTempDAR
        {
            get { return _PathToolsExcelTempDAR; }
        }

        private string _PathToolsExcelTempDicDoc = string.Empty;
        public string PathToolsExcelTempDicDoc
        {
            get { return _PathToolsExcelTempDicDoc; }
        }

        private string _PathToolsExcelTempDicDocs = string.Empty;
        public string PathToolsExcelTempDicDocs
        {
            get { return _PathToolsExcelTempDicDocs; }
        }

        private string _PathToolsExcelTempDicRAM = string.Empty;
        public string PathToolsExcelTempDicRAM
        {
            get { return _PathToolsExcelTempDicRAM; }
        }

        private string _PathToolsQC = string.Empty;
        public string PathToolsQC
        {
            get { return _PathToolsQC; }
        }

        private string _PathToolsRAMDefs = string.Empty;
        public string PathToolsRAMDefs
        {
            get { return _PathToolsRAMDefs; }
        }




        private string _PathWorkgroup = string.Empty;
        public string PathWorkgroup  // Must run ValidateFix 1st
        {
            get { return _PathWorkgroup; }
        }

        private string _PathWorkgroupTasks = string.Empty;
        public string PathWorkgroupTasks
        {
            get { return _PathWorkgroupTasks; }
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
                string lastFolder = Directories.GetLastFolder(rootPath);
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


        public bool WorkgroupCreate(string rootPath, string workgroupName, string workgroupDescription, string WGRootFolder)
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

            // If WGRootFolder == "" Then load Default Settings, else import default files from installation
            if (WGRootFolder == string.Empty)
            {
                ImportDefaultSettingFiles();
            }
            else
            {
                ImportSettingFilesFromWG(WGRootFolder);
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
                    Atebion.UserCard.Manager userCardMgr = new Atebion.UserCard.Manager(_PathWorkgroupUsers);
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
            ApplicationPathWG = rootPath;
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

            DataSet ds = Files.LoadDatasetFromXml(_WorkgroupCataloguePathFile);
            if (ds == null)
            {
                _ErrorMessage = Files.ErrorMessage;
                return false;
            }

            // Is this workgroup already in the Workgroup Catalogue 
            if (FindValueInDataTable(ds.Tables[0], WorkgroupCatalogueFields.WorkgroupRootPath, rootPath))
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

                DataSet dsWorkgroupInfo = Files.LoadDatasetFromXml(workgroupPathFile);
                if (dsWorkgroupInfo == null)
                {
                    _ErrorMessage = Files.ErrorMessage;
                    return false;
                }

                row[WorkgroupCatalogueFields.WorkgroupName] = dsWorkgroupInfo.Tables[WorkgroupInfoFields.TableName].Rows[0][WorkgroupInfoFields.WorkgroupName].ToString();
                row[WorkgroupCatalogueFields.WorkgroupDescription] = dsWorkgroupInfo.Tables[WorkgroupInfoFields.TableName].Rows[0][WorkgroupInfoFields.WorkgroupDescription].ToString(); ;
            }
            row[WorkgroupCatalogueFields.WorkgroupRootPath] = rootPath;

            ds.Tables[0].Rows.Add(row);

            GenericDataManger gdManager = new GenericDataManger();

            gdManager.SaveDataXML(ds, _WorkgroupCataloguePathFile);

            if (gdManager.ErrorMessage.Length > 0)
            {
                _ErrorMessage = gdManager.ErrorMessage;

                return false;
            }

            // Copy User Infor to Workgroup
            if (!isLocal)
            {
                try
                {
                    
                    string userFile = Files.GetFileName(_UserInfoPathFile);
                    string user2PathFile = Path.Combine(_PathWorkgroupUsers, userFile);
                    
                    //drop fresh copy of user information
                    string destination = ApplicationPathWG + @"\Workgroup\Users\"+ user2PathFile;                   
                    File.Copy(_UserInfoPathFile, Path.Combine(ApplicationPathWG, destination), true);
                }
                catch (Exception ex)
                {
                    //_ErrorMessage = string.Concat("An Error occurred while copying your User Information file to a Workgroup.       Error: ", ex.Message);
                    //return false;
                }
            }

            return true;
        }

        public bool WorkgroupConnection_RemoveByPath(string workgroupRootPath, string workgroupName, out DataTable dt)
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
                    DataSet dsLocal = Files.LoadDatasetFromXml(_WorkgroupCataloguePathFile);
                    dt = dsLocal.Tables[WorkgroupCatalogueFields.TableName];
                    return true;
                }
            }


            DataSet ds = Files.LoadDatasetFromXml(_WorkgroupCataloguePathFile);
            if (ds == null)
            {
                _ErrorMessage = Files.ErrorMessage;
                dt = null;
                return false;
            }

            string tempPath = @"";
            for (int i = ds.Tables[WorkgroupCatalogueFields.TableName].Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = ds.Tables[WorkgroupCatalogueFields.TableName].Rows[i];
                if (dr["workgroupRootPath"] == tempPath)
                {
                    MessageBox.Show("orgPath:" + dr["workgroupRootPath"].ToString() + " tempPath:" + tempPath);
                    ds.Tables[WorkgroupCatalogueFields.TableName].Rows.Remove(dr);
                }
            }

            //string filter = string.Concat(WorkgroupCatalogueFields.WorkgroupRootPath, " = '", workgroupRootPath, "'");
            ////MessageBox.Show(workgroupRootPath + "--ttt---" + workgroupName+"---00---"+ filter);
            ////MessageBox.Show("ll"+ds.Tables[WorkgroupCatalogueFields.TableName].Rows.Count.ToString());
            //DataRow[] rows = ds.Tables[WorkgroupCatalogueFields.TableName].Select(filter);
            //if (rows.Length > 0)
            //{
            //    //MessageBox.Show("in"+rows.Length.ToString());
            //    rows[0].Delete();
            //}


            ds.Tables[WorkgroupCatalogueFields.TableName].AcceptChanges();
            ds.AcceptChanges();

            dt = ds.Tables[WorkgroupCatalogueFields.TableName];

            GenericDataManger gdManager = new GenericDataManger();

            gdManager.SaveDataXML(ds, _WorkgroupCataloguePathFile);


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
                    DataSet dsLocal = Files.LoadDatasetFromXml(_WorkgroupCataloguePathFile);
                    dt = dsLocal.Tables[WorkgroupCatalogueFields.TableName];
                    return true;
                }
            }


            DataSet ds = Files.LoadDatasetFromXml(_WorkgroupCataloguePathFile);
            if (ds == null)
            {
                _ErrorMessage = Files.ErrorMessage;
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

            GenericDataManger gdManager = new GenericDataManger();

            gdManager.SaveDataXML(ds, _WorkgroupCataloguePathFile);


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

            DataSet ds = Files.LoadDatasetFromXml(_WorkgroupCataloguePathFile);
            if (ds == null)
            {
                _ErrorMessage = Files.ErrorMessage;
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

            if (_PathLocalWorkgroup == string.Empty)
                SetLocalWorkspacePaths();

            _WorkgroupCataloguePathFile = Path.Combine(_PathLocalWorkgroup, _WORKGROUP_CATALOGUE);

            if (!File.Exists(_WorkgroupCataloguePathFile))
            {
                _ErrorMessage = string.Concat("Unable to file Workgroup Catalogue file: ", _WorkgroupCataloguePathFile);
                return null;
            }

            DataSet ds = Files.LoadDatasetFromXml(_WorkgroupCataloguePathFile);
            if (ds == null)
            {
                _ErrorMessage = Files.ErrorMessage;
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

                    bool hasCopied = CopyFile2NewLocation(_UserInfoPathFile, _PathWorkgroupUsers, Atebion.Common.Files.GetFileName(files[0]), "User Information");

                    if (!hasCopied)
                    {
                        _ErrorMessage = string.Concat("Unable to copy your User Information to the Workgroup.", Environment.NewLine, Environment.NewLine, Files.ErrorMessage);
                        return null;
                    }

                    //if (!Files.CopyFile2NewLocation(_UserInfoPathFile, _PathWorkgroupUsers, Files.GetFileName(files[0]), "User Information"))
                    //{
                    //    _ErrorMessage = string.Concat("Unable to copy your User Information to the Workgroup.", Environment.NewLine, Environment.NewLine, Files.ErrorMessage);
                    //    return null;
                    //}

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

        /// <summary>
        /// Imports setting files from a given Workgroup
        /// </summary>
        /// <param name="WGRootFolder">Workgroup root folder</param>
        /// <returns>Retuns True is no errors occured</returns>
        private int ImportSettingFilesFromWG(string WGRootFolder)
        {
            int filesCopied = 0;

            // --> Libraries
            string source_PathDictionaries = Path.Combine(WGRootFolder, "Dictionaries");
            filesCopied = Files.CopyFiles(source_PathDictionaries, _PathDictionaries);

            string source_PathKeywordsGrp = Path.Combine(WGRootFolder, "KeywordsGrp");
            filesCopied = Files.CopyFiles(source_PathKeywordsGrp, _PathKeywordsGrp);

            // --> Template Folders
            string source_PathTools = Path.Combine(WGRootFolder, "Tools");

            // AcroSeeker
            string source_PathToolsAcroSeeker = Path.Combine(source_PathTools, "AcroSeeker");

            string source_PathToolsAcroSeekerDefLib = Path.Combine(source_PathToolsAcroSeeker, "DefLib");
            filesCopied = filesCopied + Files.CopyFiles(source_PathToolsAcroSeekerDefLib, _PathToolsAcroSeekerDefLib);

            string source_PathToolsAcroSeekerIgnoreLib = Path.Combine(source_PathToolsAcroSeeker, "IgnoreLib");
            filesCopied = filesCopied + Files.CopyFiles(source_PathToolsAcroSeekerIgnoreLib, _PathToolsAcroSeekerIgnoreLib);

            string source_PathToolsExcelTemp = Path.Combine(source_PathTools, "ExcelTemp");

            string source_PathToolsExcelTempAR = Path.Combine(source_PathToolsExcelTemp, "AR");
            filesCopied = filesCopied + Files.CopyFiles(source_PathToolsExcelTempAR, _PathToolsExcelTempAR);

            string source_PathToolsExcelTempConceptsDoc = Path.Combine(source_PathToolsExcelTemp, "ConceptsDoc");
            filesCopied = filesCopied + Files.CopyFiles(source_PathToolsExcelTempConceptsDoc, _PathToolsExcelTempConceptsDoc);

            string source_PathToolsExcelTempConceptsDocs = Path.Combine(source_PathToolsExcelTemp, "ConceptsDocs");
            filesCopied = filesCopied + Files.CopyFiles(source_PathToolsExcelTempConceptsDocs, _PathToolsExcelTempConceptsDocs);

            string source_PathToolsExcelTempDAR = Path.Combine(source_PathToolsExcelTemp, "DAR");
            filesCopied = filesCopied + Files.CopyFiles(source_PathToolsExcelTempDAR, _PathToolsExcelTempDAR);

            string source_PathToolsExcelTempDicDoc = Path.Combine(source_PathToolsExcelTemp, "DicDoc");
            filesCopied = filesCopied + Files.CopyFiles(source_PathToolsExcelTempDicDoc, _PathToolsExcelTempDicDoc);

            string source_PathToolsExcelTempDicDocs = Path.Combine(source_PathToolsExcelTemp, "DicDocs");
            filesCopied = filesCopied + Files.CopyFiles(source_PathToolsExcelTempDicDocs, _PathToolsExcelTempDicDocs);

            string source_PathToolsExcelTempDicRAM = Path.Combine(source_PathToolsExcelTemp, "DicRAM");
            filesCopied = filesCopied + Files.CopyFiles(source_PathToolsExcelTempDicRAM, _PathToolsExcelTempDicRAM);

            // <-- Template Folders

            // RAM Models
            string source_PathToolsRAMDefs = Path.Combine(source_PathTools, "RAMDefs");
            filesCopied = filesCopied + Files.CopyFiles(source_PathToolsRAMDefs, _PathToolsRAMDefs);

            // Workgroup
            string source_PathWorkgroup = WGRootFolder;//Path.Combine(WGRootFolder, "Workgroup");
            string source_PathWorkgroup2 = Path.Combine(WGRootFolder, "Workgroup");

            // Tasks
            string source_PathWorkgroupTasks = Path.Combine(source_PathWorkgroup, "Tasks");
            filesCopied = filesCopied + Files.CopyFiles(source_PathWorkgroupTasks, _PathWorkgroupTasks);

            // --> Matrix Builder's Libraries and Templates
            string source_PathWorkgroupList = Path.Combine(source_PathWorkgroup2, "Lists");
            filesCopied = filesCopied + Files.CopyFiles(source_PathWorkgroupList, _PathWorkgroupList);

            string source_PathWorkgroupDocsType = Path.Combine(source_PathWorkgroup2, "DocsTypes");
            filesCopied = filesCopied + Files.CopyFiles(source_PathWorkgroupDocsType, _PathWorkgroupDocsType);

            string[] dataSourcesFolders;
            string sourceFolderName = string.Empty;
            string desFolder = string.Empty;

            // Matrix Templates and Storyboards
            string source_PathWorkgroupMatrixTemp = Path.Combine(source_PathWorkgroup2, "MatrixTemp");
            filesCopied = filesCopied + Files.CopyFiles(source_PathWorkgroupMatrixTemp, _PathWorkgroupMatrixTemp);

            // Matrix Templates and Storyboards
            dataSourcesFolders = Directory.GetDirectories(source_PathWorkgroupMatrixTemp);
            sourceFolderName = string.Empty;
            desFolder = string.Empty;
            foreach (string sourceFolder in dataSourcesFolders)
            {
                sourceFolderName = Path.GetDirectoryName(sourceFolder);
                desFolder = Path.Combine(_PathWorkgroupDataSources, sourceFolderName);
                filesCopied = filesCopied + Files.CopyFiles(sourceFolder, desFolder);
            }

            // Ref. Resources
            string source_PathWorkgroupDataSources = Path.Combine(source_PathWorkgroup2, "DataSources");
            filesCopied = filesCopied + Files.CopyFiles(source_PathWorkgroupDataSources, _PathWorkgroupDataSources);

            // Ref. Resources
            dataSourcesFolders = Directory.GetDirectories(source_PathWorkgroupDataSources);
            sourceFolderName = string.Empty;
            desFolder = string.Empty;
            foreach (string sourceFolder in dataSourcesFolders)
            {
                sourceFolderName = Path.GetDirectoryName(sourceFolder);
                desFolder = Path.Combine(_PathWorkgroupDataSources, sourceFolderName);
                filesCopied = filesCopied + Files.CopyFiles(sourceFolder, desFolder);
            }

            // <-- Matrix Builder

            return filesCopied;
        }

        /// <summary>
        /// Default Setting Files are included with the installation
        /// </summary>
        /// <returns>Retuns True is no errors occured</returns>
        public int ImportDefaultSettingFiles()
        {
            int filesCopied = 0;

            string settingsFolder = Path.Combine(_ApplicationPath, "Settings");
            if (!Directory.Exists(settingsFolder))
                return filesCopied;

            string dictionariesPath = Path.Combine(settingsFolder, "Dictionaries");
            string keywordsGrpPath = Path.Combine(settingsFolder, "KeywordsGrp");
            string tasksPath = Path.Combine(settingsFolder, "Tasks");

            string toolsPath = Path.Combine(settingsFolder, "Tools");

            // AcroSeeker
            string toolsAcroSeekerPath = Path.Combine(toolsPath, "AcroSeeker");
            string toolsAcroSeekerDefLibPath = Path.Combine(toolsAcroSeekerPath, "DefLib");
            string toolsAcroSeekerIgnoreLibPath = Path.Combine(toolsAcroSeekerPath, "IgnoreLib");

            // Excel Templates
            string toolsExcelTempPath = Path.Combine(toolsPath, "ExcelTemp");
            string toolsExcelTempARPath = Path.Combine(toolsExcelTempPath, "AR");
            string toolsExcelTempConceptsDocPath = Path.Combine(toolsExcelTempPath, "ConceptsDoc");
            string toolsExcelTempConceptsDocsPath = Path.Combine(toolsExcelTempPath, "ConceptsDocs");
            string toolsExcelTempDARPath = Path.Combine(toolsExcelTempPath, "DAR");
            string toolsExcelTempDicDocPath = Path.Combine(toolsExcelTempPath, "DicDoc");
            string toolsExcelTempDicDocsPath = Path.Combine(toolsExcelTempPath, "DicDocs");
            string toolsExcelTempDicRAMPath = Path.Combine(toolsExcelTempPath, "DicRAM");

            string toolsQCPath = Path.Combine(toolsPath, "QC");
            string toolsRAMDefsPath = Path.Combine(toolsPath, "RAMDefs");


            if (_isWorkgroupLocal)
            {
                filesCopied = Files.CopyReplaceFiles(dictionariesPath, _PathLocalDictionaries);
                filesCopied = filesCopied + Files.CopyReplaceFiles(keywordsGrpPath, _PathLocalKeywordsGrp);
                filesCopied = filesCopied + Files.CopyReplaceFiles(tasksPath, _PathLocalTasks);

                filesCopied = filesCopied + Files.CopyReplaceFiles(toolsAcroSeekerDefLibPath, _PathLocalToolsAcroSeekerDefLib);
                filesCopied = filesCopied + Files.CopyReplaceFiles(toolsAcroSeekerIgnoreLibPath, _PathLocalToolsAcroSeekerIgnoreLib);
                filesCopied = filesCopied + Files.CopyReplaceFiles(toolsExcelTempARPath, _PathLocalToolsExcelTempAR);
                filesCopied = filesCopied + Files.CopyReplaceFiles(toolsExcelTempConceptsDocPath, _PathLocalToolsExcelTempConceptsDoc);
                filesCopied = filesCopied + Files.CopyReplaceFiles(toolsExcelTempConceptsDocsPath, _PathLocalToolsExcelTempConceptsDocs);
                filesCopied = filesCopied + Files.CopyReplaceFiles(toolsExcelTempDARPath, _PathLocalToolsExcelTempDAR);
                filesCopied = filesCopied + Files.CopyReplaceFiles(toolsExcelTempDicDocPath, _PathLocalToolsExcelTempDicDoc);
                filesCopied = filesCopied + Files.CopyReplaceFiles(toolsExcelTempDicDocsPath, _PathLocalToolsExcelTempDicDocs);
                filesCopied = filesCopied + Files.CopyReplaceFiles(toolsExcelTempDicRAMPath, _PathLocalToolsExcelTempDicRAM);

                filesCopied = filesCopied + Files.CopyReplaceFiles(toolsQCPath, _PathLocalToolsQC);
                filesCopied = filesCopied + Files.CopyReplaceFiles(toolsRAMDefsPath, _PathLocalToolsRAMDefs);

            }
            else
            {
                filesCopied = Files.CopyReplaceFiles(dictionariesPath, _PathDictionaries);
                filesCopied = filesCopied + Files.CopyReplaceFiles(keywordsGrpPath, _PathKeywordsGrp);
                filesCopied = filesCopied + Files.CopyReplaceFiles(tasksPath, _PathWorkgroupTasks);

                filesCopied = filesCopied + Files.CopyReplaceFiles(toolsAcroSeekerDefLibPath, _PathToolsAcroSeekerDefLib);
                filesCopied = filesCopied + Files.CopyReplaceFiles(toolsAcroSeekerIgnoreLibPath, _PathToolsAcroSeekerIgnoreLib);
                filesCopied = filesCopied + Files.CopyReplaceFiles(toolsExcelTempARPath, _PathToolsExcelTempAR);
                filesCopied = filesCopied + Files.CopyReplaceFiles(toolsExcelTempConceptsDocPath, _PathToolsExcelTempConceptsDoc);
                filesCopied = filesCopied + Files.CopyReplaceFiles(toolsExcelTempConceptsDocsPath, _PathToolsExcelTempConceptsDocs);
                filesCopied = filesCopied + Files.CopyReplaceFiles(toolsExcelTempDARPath, _PathToolsExcelTempDAR);
                filesCopied = filesCopied + Files.CopyReplaceFiles(toolsExcelTempDicDocPath, _PathToolsExcelTempDicDoc);
                filesCopied = filesCopied + Files.CopyReplaceFiles(toolsExcelTempDicDocsPath, _PathToolsExcelTempDicDocs);
                filesCopied = filesCopied + Files.CopyReplaceFiles(toolsExcelTempDicRAMPath, _PathToolsExcelTempDicRAM);

                filesCopied = filesCopied + Files.CopyReplaceFiles(toolsQCPath, _PathToolsQC);
                filesCopied = filesCopied + Files.CopyReplaceFiles(toolsRAMDefsPath, _PathToolsRAMDefs);

            }

            return filesCopied;
        }
                
        private bool CopyFile2NewLocation(string sourceFolder, string destinationFolder, string fileName, string subject)
        {
            _ErrorMessage = string.Empty;

            string sourcePathFile = Path.Combine(sourceFolder, fileName);
            string destinationPathFile = Path.Combine(destinationFolder, fileName);

            if (!File.Exists(sourcePathFile))
            {
                _ErrorMessage = string.Concat("Unable to find ", subject, " file: ", sourcePathFile);
                return false;
            }

            File.Copy(sourcePathFile, destinationPathFile, true);

            if (!File.Exists(destinationPathFile))
            {
                _ErrorMessage = string.Concat("Unable to copy ", subject, " file to ", destinationFolder);
                return false;
            }

            return true;

        }

        // ToDo public bool WorkgroupConnection_Update ...

        public bool UseLocalWorkgroup()
        {
            if (!ValidateFix(true)) // check, fix, & set Local Workgroup folders
                return false;

            _PathWorkgroupRoot = _PathLocalData;

            if (ValidateFix(false)) // set Local Workgroup folders = Current Workgroup;
                return false;

            _isWorkgroupLocal = true;

            return true;
        }

        public bool ValidateFix(bool isLocal)
        {
            _ErrorMessage = string.Empty;

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

            if (_PathLocalData.Trim().Length == 0)
            {
                _ErrorMessage = "Local Data Path is not Defined";
                return false;
            }


            _PathLocalDictionaries = Path.Combine(_PathLocalData, "Dictionaries");
            if (!CheckCreateDir(_PathLocalDictionaries))
                return false;

            _PathLocalKeywordsGrp = Path.Combine(_PathLocalData, "KeywordsGrp");
            if (!CheckCreateDir(_PathLocalKeywordsGrp))
                return false;

            _PathLocalTasks = Path.Combine(_PathLocalData, "Tasks");
            if (!CheckCreateDir(_PathLocalTasks))
                return false;

            _PathLocalWorkgroup = Path.Combine(_PathLocalData, "Workgroup");
            if (!CheckCreateDir(_PathLocalWorkgroup))
                return false;

            // --> Template Folders
            _PathLocalTools = Path.Combine(_PathLocalData, "Tools");
            if (!CheckCreateDir(_PathLocalTools))
                return false;

            _PathLocalToolsAcroSeeker = Path.Combine(_PathLocalTools, "AcroSeeker");
            if (!CheckCreateDir(_PathLocalToolsAcroSeeker))
                return false;

            _PathLocalToolsAcroSeekerDefLib = Path.Combine(_PathLocalToolsAcroSeeker, "DefLib");
            if (!CheckCreateDir(_PathLocalToolsAcroSeekerDefLib))
                return false;

            _PathLocalToolsAcroSeekerIgnoreLib = Path.Combine(_PathLocalToolsAcroSeeker, "IgnoreLib");
            if (!CheckCreateDir(_PathLocalToolsAcroSeekerIgnoreLib))
                return false;

            _PathLocalToolsExcelTemp = Path.Combine(_PathLocalTools, "ExcelTemp");
            if (!CheckCreateDir(_PathLocalToolsExcelTemp))
                return false;

            _PathLocalToolsExcelTempAR = Path.Combine(_PathLocalToolsExcelTemp, "AR");
            if (!CheckCreateDir(_PathLocalToolsExcelTempAR))
                return false;

            _PathLocalToolsExcelTempConceptsDoc = Path.Combine(_PathLocalToolsExcelTemp, "ConceptsDoc");
            if (!CheckCreateDir(_PathLocalToolsExcelTempConceptsDoc))
                return false;

            _PathLocalToolsExcelTempConceptsDocs = Path.Combine(_PathLocalToolsExcelTemp, "ConceptsDocs");
            if (!CheckCreateDir(_PathLocalToolsExcelTempConceptsDocs))
                return false;

            _PathLocalToolsExcelTempDAR = Path.Combine(_PathLocalToolsExcelTemp, "DAR");
            if (!CheckCreateDir(_PathLocalToolsExcelTempDAR))
                return false;

            _PathLocalToolsExcelTempDicDoc = Path.Combine(_PathLocalToolsExcelTemp, "DicDoc");
            if (!CheckCreateDir(_PathLocalToolsExcelTempDicDoc))
                return false;

            _PathLocalToolsExcelTempDicDocs = Path.Combine(_PathLocalToolsExcelTemp, "DicDocs");
            if (!CheckCreateDir(_PathLocalToolsExcelTempDicDocs))
                return false;

            _PathLocalToolsExcelTempDicRAM = Path.Combine(_PathLocalToolsExcelTemp, "DicRAM");
            if (!CheckCreateDir(_PathLocalToolsExcelTempDicRAM))
                return false;

            _PathLocalToolsQC = Path.Combine(_PathLocalTools, "QC");
            if (!CheckCreateDir(_PathLocalToolsQC))
                return false;

            _PathLocalToolsRAMDefs = Path.Combine(_PathLocalTools, "RAMDefs");
            if (!CheckCreateDir(_PathLocalToolsRAMDefs))
                return false;

            // <-- Template Folders

            _PathLocalWorkgroupBackup = Path.Combine(_PathLocalWorkgroup, "Backup");
            if (!CheckCreateDir(_PathLocalWorkgroupBackup))
                return false;

            _PathLocalWorkgroupList = Path.Combine(_PathLocalWorkgroup, "Lists");
            if (!CheckCreateDir(_PathLocalWorkgroupList))
                return false;

            _PathLocalWorkgroupDocsType = Path.Combine(_PathLocalWorkgroup, "DocTypes");
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

            _PathDictionaries = Path.Combine(adjPathWorkgroupRoot, "Dictionaries");
            if (!CheckCreateDir(_PathDictionaries))
                return false;

            _PathKeywordsGrp = Path.Combine(adjPathWorkgroupRoot, "KeywordsGrp");
            if (!CheckCreateDir(_PathKeywordsGrp))
                return false;

            // --> Template Folders
            _PathTools = Path.Combine(adjPathWorkgroupRoot, "Tools");
            if (!CheckCreateDir(_PathTools))
                return false;

            _PathToolsAcroSeeker = Path.Combine(_PathTools, "AcroSeeker");
            if (!CheckCreateDir(_PathToolsAcroSeeker))
                return false;

            _PathToolsAcroSeekerDefLib = Path.Combine(_PathToolsAcroSeeker, "DefLib");
            if (!CheckCreateDir(_PathToolsAcroSeekerDefLib))
                return false;

            _PathToolsAcroSeekerIgnoreLib = Path.Combine(_PathToolsAcroSeeker, "IgnoreLib");
            if (!CheckCreateDir(_PathToolsAcroSeekerIgnoreLib))
                return false;

            _PathToolsExcelTemp = Path.Combine(_PathTools, "ExcelTemp");
            if (!CheckCreateDir(_PathToolsExcelTemp))
                return false;

            _PathToolsExcelTempAR = Path.Combine(_PathToolsExcelTemp, "AR");
            if (!CheckCreateDir(_PathToolsExcelTempAR))
                return false;

            _PathToolsExcelTempConceptsDoc = Path.Combine(_PathToolsExcelTemp, "ConceptsDoc");
            if (!CheckCreateDir(_PathToolsExcelTempConceptsDoc))
                return false;

            _PathToolsExcelTempConceptsDocs = Path.Combine(_PathToolsExcelTemp, "ConceptsDocs");
            if (!CheckCreateDir(_PathToolsExcelTempConceptsDocs))
                return false;

            _PathToolsExcelTempDAR = Path.Combine(_PathToolsExcelTemp, "DAR");
            if (!CheckCreateDir(_PathToolsExcelTempDAR))
                return false;

            _PathToolsExcelTempDicDoc = Path.Combine(_PathToolsExcelTemp, "DicDoc");
            if (!CheckCreateDir(_PathToolsExcelTempDicDoc))
                return false;

            _PathToolsExcelTempDicDocs = Path.Combine(_PathToolsExcelTemp, "DicDocs");
            if (!CheckCreateDir(_PathToolsExcelTempDicDocs))
                return false;

            _PathToolsExcelTempDicRAM = Path.Combine(_PathToolsExcelTemp, "DicRAM");
            if (!CheckCreateDir(_PathToolsExcelTempDicRAM))
                return false;

            // <-- Template Folders

            // RAM Models
            _PathToolsRAMDefs = Path.Combine(_PathTools, "RAMDefs");
            if (!CheckCreateDir(_PathToolsRAMDefs))
                return false;

            _PathToolsQC = Path.Combine(_PathTools, "QC");
            if (!CheckCreateDir(_PathToolsQC))
                return false;


            _PathWorkgroup = Path.Combine(adjPathWorkgroupRoot, "Workgroup"); // Workgroup folder
            if (!CheckCreateDir(_PathWorkgroup))
                return false;

            _PathWorkgroupTasks = Path.Combine(adjPathWorkgroupRoot, "Tasks"); // Tasks folder
            if (!CheckCreateDir(_PathWorkgroupTasks))
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

            GenericDataManger gdManager = new GenericDataManger();

            gdManager.SaveDataXML(ds, _WorkgroupInfoPathFile);
            if (Files.ErrorMessage.Length > 0)
            {
                _ErrorMessage = Files.ErrorMessage;
                return false;
            }

            string workgroupDescriptionPathFile = Path.Combine(_PathWorkgroup, _WORKGROUP_DESCRIP_FILENAME);
            Files.WriteStringToFile(WorkgroupDescription, workgroupDescriptionPathFile);


            return true;
        }

        public bool BackupFile(string sourceFolder, string backupFolder, string file, out string backupPathfile)
        {
            _ErrorMessage = string.Empty;

            string prefix = "0_";

            string backupFile = string.Concat(prefix, file);
            backupPathfile = Path.Combine(backupFolder, backupFile);
            if (File.Exists(backupPathfile))
            {
                for (int i = 1; i < 101; i++)
                {
                    prefix = string.Concat(i.ToString(), "_");
                    backupFile = string.Concat(prefix, file);
                    backupPathfile = Path.Combine(backupFolder, backupFile);
                    if (!File.Exists(backupPathfile))
                    {
                        break;
                    }

                }
            }

            string sourcePathFile = Path.Combine(sourceFolder, file);

            try
            {
                File.Copy(sourcePathFile, backupPathfile);
                return true;
            }
            catch (Exception ex)
            {
                _ErrorMessage = string.Concat("Notice: Unable to Backup file: ", file, Environment.NewLine, Environment.NewLine, ex.Message);
                return false;
            }
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

            BackupFile(_PathLocalWorkgroup, _PathLocalWorkgroupBackup, _WORKGROUP_CATALOGUE, out backupPathFile);

            DataSet ds = Files.LoadDatasetFromXml(_WorkgroupCataloguePathFile);
            if (ds == null)
            {
                _ErrorMessage = Files.ErrorMessage;
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

            GenericDataManger gdManager = new GenericDataManger();

            gdManager.SaveDataXML(ds, _WorkgroupCataloguePathFile);

            if (!File.Exists(_WorkgroupCataloguePathFile))
            {
                if (!RestoreLastBackup(_PathLocalWorkgroupBackup, _PathLocalWorkgroup, _WORKGROUP_CATALOGUE))
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

            Atebion.UserCard.Manager usrCardMgr = new Atebion.UserCard.Manager(_UserInfoPathFile);

            string[] files = usrCardMgr.GetUserInforFiles();

            if (files == null || files.Length == 0) // Added check for null
            {
                _ErrorMessage = string.Concat("Unable to find your User Information file at this location: ", _UserInfoPathFile);
                return false;
            }

            if (!CopyFile2NewLocation(_UserInfoPathFile, _PathWorkgroupUsers, Files.GetFileName(files[0]), "User Information"))
            {
                _ErrorMessage = string.Concat("Unable to copy your User Information to the Workgroup.", Environment.NewLine, Environment.NewLine, Files.ErrorMessage);
                return false;
            }


            return true;

        }

        private string GetLastBackupFile(string backupFolder, string file)
        {
            string prefix = "0_";

            string previousBackupPathFile = string.Empty;

            string backupFile = string.Concat(prefix, file);
            string backupPathfile = Path.Combine(backupFolder, backupFile);
            previousBackupPathFile = backupPathfile;
            if (File.Exists(backupPathfile))
            {
                for (int i = 1; i < 101; i++)
                {
                    prefix = string.Concat("_", i.ToString());
                    backupFile = string.Concat(prefix, backupFile);
                    backupPathfile = Path.Combine(backupFolder, backupFile);
                    if (!File.Exists(backupPathfile))
                    {
                        return previousBackupPathFile;
                    }

                    previousBackupPathFile = backupPathfile;

                }
            }
            else
            {
                return string.Empty;
            }

            return previousBackupPathFile;

        }

        private bool RestoreLastBackup(string backupFolder, string sourceFolder, string file)
        {
            //string prefix = "~";

            //string foundBackupFile = string.Empty;

            //string backupFile = string.Concat(prefix, file);
            //string backupPathfile = Path.Combine(backupFolder, backupFile);
            //if (File.Exists(backupPathfile))
            //{
            //    foundBackupFile = backupPathfile;
            //    for (int i = 0; i < 101; i++)
            //    {
            //        prefix += "~";
            //        backupPathfile = Path.Combine(backupFolder, backupFile);
            //        if (!File.Exists(backupPathfile))
            //        {
            //            break;
            //        }
            //        foundBackupFile = backupPathfile;
            //    }
            //}
            //else
            //{
            //    return false;
            //}

            string foundBackupFile = GetLastBackupFile(backupFolder, file);
            if (foundBackupFile == string.Empty)
                return false;

            string sourcePathFile = Path.Combine(sourceFolder, file);

            if (!File.Exists(sourcePathFile))
            {
                File.Copy(foundBackupFile, sourcePathFile); // Restore the last backup
                if (File.Exists(sourcePathFile))
                {
                    return true;
                }
            }

            return false;
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

            GenericDataManger gdManager = new GenericDataManger();

            gdManager.SaveDataXML(ds, _WorkgroupCataloguePathFile);

            if (File.Exists(_LocalWorkgroupPathFileDescrip))
            {
                WorkgroupConnection_Add(_PathLocalData, true);
                return true;
            }
            else
            {
                _ErrorMessage = Files.ErrorMessage;
                return false;
            }

        }

        public bool FindValueInDataTable(DataTable dt, string InField, string sValue)
        {
            DataRow[] foundValue = dt.Select(InField + " = '" + sValue + "'");
            if (foundValue.Length != 0)
            {
                return true;
            }

            return false;

        }



        #endregion
    }
}
