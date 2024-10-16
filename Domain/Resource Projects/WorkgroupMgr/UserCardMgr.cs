using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace WorkgroupMgr
{
    class UserCardMgr
    {
        public UserCardMgr(string UserDir)
        {
            _UserDir = UserDir;
        }

        private string _UserDir = string.Empty; // AppFolders.AppDataPathUser; 

        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        private string _UniqueCode = string.Empty;
        public string UniqueCode
        {
            get { return _UniqueCode; }
        }

        public string UserMachineName
        {
            get { return Environment.MachineName; }
        }

        public string UserOSVersion
        {
            get { return Environment.OSVersion.ToString(); }
        }


        private string _UserPrefix = string.Empty;
        public string UserPrefix
        {
            get { return _UserPrefix; }
            set { _UserPrefix = value; }
        }

        private string _UserFirstName = string.Empty;
        public string UserFirstName
        {
            get { return _UserFirstName; }
            set { _UserFirstName = value; }
        }

        private string _UserMiddleName = string.Empty;
        public string UserMiddleName
        {
            get { return _UserMiddleName; }
            set { _UserMiddleName = value; }
        }

        private string _UserLastName = string.Empty;
        public string UserLastName
        {
            get { return _UserLastName; }
            set { _UserLastName = value; }
        }

        private string _UserEmail = string.Empty;
        public string UserEmail
        {
            get { return _UserEmail; }
            set { _UserEmail = value; }
        }

        private string _UserPhone = string.Empty;
        public string UserPhone
        {
            get { return _UserPhone; }
            set { _UserPhone = value; }
        }

        private string _CompanyName = string.Empty;
        public string CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }

        private string _UserTitle = string.Empty;
        public string UserTitle
        {
            get { return _UserTitle; }
            set { _UserTitle = value; }
        }

        private string _CompanyStAddress = string.Empty;
        public string CompanyStAddress
        {
            get { return _CompanyStAddress; }
            set { _CompanyStAddress = value; }
        }

        private string _Country = string.Empty;
        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }


        private string _CompanyCity = string.Empty;
        public string CompanyCity
        {
            get { return _CompanyCity; }
            set { _CompanyCity = value; }
        }

        private string _CompanyState = string.Empty;
        public string CompanyState
        {
            get { return _CompanyState; }
            set { _CompanyState = value; }
        }

        private string _CompanyZipCode = string.Empty;
        public string CompanyZipCode
        {
            get { return _CompanyZipCode; }
            set { _CompanyZipCode = value; }
        }

        private string _CompanyWebSite = string.Empty;
        public string CompanyWebSite
        {
            get { return _CompanyWebSite; }
            set { _CompanyWebSite = value; }
        }

        private string _PicFile = string.Empty;
        public string PicFile
        {
            get { return _PicFile; }
            set { _PicFile = value; }
        }

        private Int32 _Version = 0;
        public Int32 Version
        {
            get { return _Version; }
        }

        private string _OptionField1 = string.Empty;
        public string OptionField1
        {
            get { return _OptionField1; }
            set { _OptionField1 = value; }
        }

        private string _OptionField2 = string.Empty;
        public string OptionField2
        {
            get { return _OptionField2; }
            set { _OptionField2 = value; }
        }

        private string _OptionField3 = string.Empty;
        public string OptionField3
        {
            get { return _OptionField3; }
            set { _OptionField3 = value; }
        }

        private string _OptionField4 = string.Empty;
        public string OptionField4
        {
            get { return _OptionField4; }
            set { _OptionField4 = value; }
        }

        private string _OptionField5 = string.Empty;
        public string OptionField5
        {
            get { return _OptionField5; }
            set { _OptionField5 = value; }
        }



        // Data Fields
        private string fUniqueCode = "UniqueCode";
        private string fUserPrefix = "UserPrefix";
        private string fUserFirstName = "UserFirstName";
        private string fUserMiddleName = "UserMiddleName";
        private string fUserLastName = "UserLastName";
        private string fUserEmail = "UserEmail";
        private string fUserPhone = "UserPhone";
        private string fCompanyName = "CompanyName";
        private string fUserTitle = "UserTitle";
        private string fCompanyStAddress = "CompanyStAddress";
        private string fCompanyCity = "CompanyCity";
        private string fCompanyState = "CompanyState";
        private string fCompanyZipCode = "CompanyZipCode";
        private string fCountry = "Country";
        private string fCompanyWebSite = "CompanyWebSite";
        private string fVersion = "Version"; // to Increment with each change
        private string fPicFile = "PicFile";
        private string fOptionField1 = "OptionField1";
        private string fOptionField2 = "OptionField2";
        private string fOptionField3 = "OptionField3";
        private string fOptionField4 = "OptionField4";
        private string fOptionField5 = "OptionField5";



        public string[] GetUserInforFiles()
        {
            string[] files = GetUserInforFiles(_UserDir);

            return files;
        }


        private string[] GetUserInforFiles(string path)
        {
            if (!CheckCreateDir(path))
            {
                return null;
            }

            string[] files = Directory.GetFiles(path);

            return files;
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

        public bool ReadUserFile(string file)
        {
            string pathFile = file;

            DataSet ds = DataFunctions.LoadDatasetFromXml(pathFile);


            _UserPrefix = ds.Tables[0].Rows[0][fUserPrefix].ToString();
            _UserFirstName = ds.Tables[0].Rows[0][fUserFirstName].ToString();
            _UserMiddleName = ds.Tables[0].Rows[0][fUserMiddleName].ToString();
            _UserLastName = ds.Tables[0].Rows[0][fUserLastName].ToString();
            _UserEmail = ds.Tables[0].Rows[0][fUserEmail].ToString();
            _UserPhone = ds.Tables[0].Rows[0][fUserPhone].ToString();
            _UserTitle = ds.Tables[0].Rows[0][fUserTitle].ToString();

            _CompanyCity = ds.Tables[0].Rows[0][fCompanyCity].ToString();
            _CompanyName = ds.Tables[0].Rows[0][fCompanyName].ToString();
            _CompanyStAddress = ds.Tables[0].Rows[0][fCompanyStAddress].ToString();
            if (ds.Tables[0].Columns.Contains(fCountry))
                _Country = ds.Tables[0].Rows[0][fCountry].ToString();
            else
                _Country = string.Empty;

            _CompanyState = ds.Tables[0].Rows[0][fCompanyState].ToString();
            _CompanyWebSite = ds.Tables[0].Rows[0][fCompanyWebSite].ToString();
            _CompanyZipCode = ds.Tables[0].Rows[0][fCompanyZipCode].ToString();

            if (ds.Tables[0].Rows[0][fVersion].ToString() != string.Empty)
                _Version = Convert.ToInt32(ds.Tables[0].Rows[0][fVersion].ToString());

            _PicFile = ds.Tables[0].Rows[0][fPicFile].ToString();

            _OptionField1 = ds.Tables[0].Rows[0][fOptionField1].ToString();
            _OptionField2 = ds.Tables[0].Rows[0][fOptionField2].ToString();
            _OptionField3 = ds.Tables[0].Rows[0][fOptionField3].ToString();
            _OptionField4 = ds.Tables[0].Rows[0][fOptionField4].ToString();
            _OptionField5 = ds.Tables[0].Rows[0][fOptionField5].ToString();

            return true;

        }
    }
}
