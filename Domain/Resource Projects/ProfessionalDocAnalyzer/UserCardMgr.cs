using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using Atebion.Common;

namespace ProfessionalDocAnalyzer
{
    public class UserCardMgr
    {
        private string _UserDir = AppFolders.AppDataPathUser; // added 03.09.2013



        private string _Message = string.Empty;
        public string Message
        {
            get { return _Message; }
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


        public string[] CountryList2() // Added 10.13.2014
        {
            string[] Countries = new string[] {"Afghanistan",
                                                "Albania",
                                                "Algeria",
                                                "Argentina",
                                                "Armenia",
                                                "Australia",
                                                "Austria",
                                                "Azerbaijan",
                                                "Bahrain",
                                                "Bangladesh",
                                                "Belarus",
                                                "Belgium",
                                                "Belize",
                                                "Bolivarian Republic of Venezuela",
                                                "Bolivia",
                                                "Bosnia and Herzegovina",
                                                "Botswana",
                                                "Brazil",
                                                "Brunei Darussalam",
                                                "Bulgaria",
                                                "Cambodia",
                                                "Cameroon",
                                                "Canada",
                                                "Caribbean",
                                                "Chile",
                                                "China",
                                                "Colombia",
                                                "Congo [DRC]",
                                                "Costa Rica",
                                                "Croatia",
                                                "Czech Republic",
                                                "Denmark",
                                                "Dominican Republic",
                                                "Ecuador",
                                                "Egypt",
                                                "El Salvador",
                                                "Eritrea",
                                                "Estonia",
                                                "Ethiopia",
                                                "Faroe Islands",
                                                "Finland",
                                                "France",
                                                "Georgia",
                                                "Germany",
                                                "Greece",
                                                "Greenland",
                                                "Guatemala",
                                                "Haiti",
                                                "Honduras",
                                                "Hong Kong",
                                                "Hong Kong SAR",
                                                "Hungary",
                                                "Iceland",
                                                "India",
                                                "Indonesia",
                                                "Iran",
                                                "Iraq",
                                                "Ireland",
                                                "Israel",
                                                "Italy",
                                                "Ivory Coast",
                                                "Jamaica",
                                                "Japan",
                                                "Jordan",
                                                "Kazakhstan",
                                                "Kenya",
                                                "Korea",
                                                "Kuwait",
                                                "Kyrgyzstan",
                                                "Lao PDR",
                                                "Latin America",
                                                "Latvia",
                                                "Lebanon",
                                                "Libya",
                                                "Liechtenstein",
                                                "Lithuania",
                                                "Luxembourg",
                                                "Macao SAR",
                                                "Macedonia (Former Yugoslav Republic of Macedonia)",
                                                "Malaysia",
                                                "Maldives",
                                                "Mali",
                                                "Malta",
                                                "Mexico",
                                                "Moldova",
                                                "Mongolia",
                                                "Montenegro",
                                                "Morocco",
                                                "Myanmar",
                                                "Nepal",
                                                "Netherlands",
                                                "New Zealand",
                                                "Nicaragua",
                                                "Nigeria",
                                                "Norway",
                                                "Oman",
                                                "Pakistan",
                                                "Panama",
                                                "Paraguay",
                                                "Peru",
                                                "Philippines",
                                                "Poland",
                                                "Portugal",
                                                "Principality of Monaco",
                                                "Puerto Rico",
                                                "Qatar",
                                                "Réunion",
                                                "Romania",
                                                "Russia",
                                                "Rwanda",
                                                "Saudi Arabia",
                                                "Senegal",
                                                "Serbia",
                                                "Serbia and Montenegro (Former)",
                                                "Singapore",
                                                "Slovakia",
                                                "Slovenia",
                                                "Somalia",
                                                "South Africa",
                                                "Spain",
                                                "Sri Lanka",
                                                "Sweden",
                                                "Switzerland",
                                                "Syria",
                                                "Taiwan",
                                                "Tajikistan",
                                                "Thailand",
                                                "Trinidad and Tobago",
                                                "Tunisia",
                                                "Turkey",
                                                "Turkmenistan",
                                                "U.A.E.",
                                                "Ukraine",
                                                "United Kingdom",
                                                "United States",
                                                "Uruguay",
                                                "Uzbekistan",
                                                "Vietnam",
                                                "Yemen",
                                                "Zimbabwe"
                                                            };


            return Countries;
        }

        public List<string> CountryList()
        {
            //Creating list
            List<string> CultureList = new List<string>();

            //getting  the specific  CultureInfo from CultureInfo class
            CultureInfo[] getCultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            foreach (CultureInfo getCulture in getCultureInfo)
            {
                try // Added 10.07.2014
                {
                    //creating the object of RegionInfo class
                    RegionInfo GetRegionInfo = new RegionInfo(getCulture.LCID);
                    //adding each county Name into the arraylist
                    if (!(CultureList.Contains(GetRegionInfo.EnglishName)))
                    {
                        CultureList.Add(GetRegionInfo.EnglishName);
                    }
                }
                catch (ArgumentException)
                {

                }
            }
            //sorting array by using sort method to get countries in order
            CultureList.Sort();
            //returning country list
            return CultureList;
        }


        public string[] GetUserInforFiles4App()
        {
            string[] files = GetUserInforFiles(_UserDir);

            return files;
        }

        public string[] GetUserInfor4CurrentWS()
        {
            if (AppFolders.AppDataPathUser == string.Empty)
            {
                _Message = "No User Data Path Found";
                return null;
            }

            //string wsPath = string.Concat(Global.WorkSpacePath, @"\Users");

            //Directories.DirExistsOrCreate(wsPath);

            string[] files = GetUserInforFiles(AppFolders.AppDataPathUser);

            return files;

        }


        private string[] GetUserInforFiles(string path)
        {
            _Message = Directories.DirExistsOrCreate(path);
            if (_Message != string.Empty)
                return null;

            string[] files = Directory.GetFiles(path);

            return files;
        }

        public bool CreateNewUser()
        {
            DataTable dt = createTable();
            if (dt == null)
            {
                _Message = "The new User Information is null.";
                return false;
            }

            _UniqueCode = DataFunctions.GetUniqueCode();

            DataSet ds = new DataSet();

            try
            {
                DataRow row = dt.NewRow();

                row[fUniqueCode] = _UniqueCode;
                row[fUserPrefix] = _UserPrefix;
                row[fUserFirstName] = _UserFirstName;
                row[fUserMiddleName] = _UserMiddleName;
                row[fUserLastName] = _UserLastName;
                row[fUserEmail] = _UserEmail;
                row[fUserPhone] = _UserPhone;
                row[fCompanyName] = _CompanyName;
                row[fUserTitle] = _UserTitle;
                row[fCompanyStAddress] = _CompanyStAddress;
                row[fCompanyCity] = _CompanyCity;
                row[fCountry] = _Country;
                row[fCompanyState] = _CompanyState;
                row[fCompanyZipCode] = _CompanyZipCode;
                row[fCompanyWebSite] = _CompanyWebSite;
                row[fPicFile] = _PicFile;
                row[fVersion] = _Version;
                row[fOptionField1] = _OptionField1;
                row[fOptionField2] = _OptionField2;
                row[fOptionField3] = _OptionField3;
                row[fOptionField4] = _OptionField4;
                row[fOptionField5] = _OptionField5;

                dt.Rows.Add(row);

                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                _Message = ex.Message;
                return false;
            }

            _Message = Directories.DirExistsOrCreate(_UserDir);
            if (_Message != string.Empty)
            {
                return false;
            }

            string fileName = GetUserFileName();
            string pathFile = string.Concat(_UserDir, @"\", fileName); // Atebion User ext.

            // pathFile = Files.CleanFileName(pathFile);



            GenericDataManger dataMgr = new GenericDataManger();

            dataMgr.SaveDataXML(ds, pathFile);

            // dataMgr.EncryptDataSet(ds, pathFile);


            return true;

        }

        public bool ReadUserFile(string file, bool isInWorkgroup)
        {
            string pathFile = file;

            //if (isInWorkgroup)
            //    pathFile = string.Concat(Global.WorkSpacePath, @"\", file);
            //else
            //    pathFile = string.Concat(_UserDir, @"\", file);

            GenericDataManger dataMgr = new GenericDataManger();

            // DataSet ds = dataMgr.DecryptDataSet(pathFile);

            DataSet ds = dataMgr.LoadDatasetFromXml(pathFile);


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

        public bool UpdateUserFile(string file, bool isInWorkgroup)
        {
            string pathFile = string.Empty;

            if (isInWorkgroup)
                pathFile = string.Concat(AppFolders.AppDataPathUser, @"\", file);
            else
                pathFile = file; // = string.Concat(_UserDir, @"\", file);

            GenericDataManger dataMgr = new GenericDataManger();

            // DataSet ds = dataMgr.DecryptDataSet(pathFile);

            DataSet ds = dataMgr.LoadDatasetFromXml(pathFile);

            if (!ds.Tables[0].Columns.Contains(fCountry))
            {
                DataTable dt = createTable();
                DataRow row = dt.NewRow();
                dt.Rows.Add(row);

                ds = null;
                ds = new DataSet();
                ds.Tables.Add(dt);


            }

            ds.Tables[0].Rows[0][fUserPrefix] = _UserPrefix;
            ds.Tables[0].Rows[0][fUserFirstName] = _UserFirstName;
            ds.Tables[0].Rows[0][fUserMiddleName] = _UserMiddleName;
            ds.Tables[0].Rows[0][fUserLastName] = _UserLastName;
            ds.Tables[0].Rows[0][fUserEmail] = _UserEmail;
            ds.Tables[0].Rows[0][fUserTitle] = _UserTitle;
            ds.Tables[0].Rows[0][fUserPhone] = _UserPhone;

            ds.Tables[0].Rows[0][fCompanyCity] = _CompanyCity;
            ds.Tables[0].Rows[0][fCompanyName] = _CompanyName;
            ds.Tables[0].Rows[0][fCompanyStAddress] = _CompanyStAddress;
            ds.Tables[0].Rows[0][fCountry] = _Country;
            ds.Tables[0].Rows[0][fCompanyState] = _CompanyState;
            ds.Tables[0].Rows[0][fCompanyWebSite] = _CompanyWebSite;
            ds.Tables[0].Rows[0][fCompanyZipCode] = _CompanyZipCode;


            ds.Tables[0].Rows[0][fVersion] = _Version++;

            ds.Tables[0].Rows[0][fPicFile] = _PicFile;

            ds.Tables[0].Rows[0][fOptionField1] = _OptionField1;
            ds.Tables[0].Rows[0][fOptionField2] = _OptionField2;
            ds.Tables[0].Rows[0][fOptionField3] = _OptionField3;
            ds.Tables[0].Rows[0][fOptionField4] = _OptionField4;
            ds.Tables[0].Rows[0][fOptionField5] = _OptionField5;


            //     dataMgr.EncryptDataSet(ds, pathFile);

            dataMgr.SaveDataXML(ds, pathFile);

            return true;

        }


        //public string GetUniqueCode()
        //{
        //    string allChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        //     string uniqueCode = "";

        //    System.Text.StringBuilder str = new System.Text.StringBuilder();

        //    //length of req key
        //    for (byte i = 1; i <= 10; i++)
        //    {
        //        int xx = 0;
        //        Random r = new Random();

        //        xx = r.Next() * (allChars.Length - 1);
        //        //number of rawchars
        //        str.Append(allChars.Trim()[xx]);
        //    }

        //    uniqueCode = str.ToString();

        //    return uniqueCode;

        //}

        private string GetUserFileName()
        {
            return string.Concat(_UserFirstName, "_", _UserLastName, "_", _UniqueCode, ".AUsr");
        }

        private DataTable createTable()
        {
            DataTable table = new DataTable("userInformation");

            table.Columns.Add(fUniqueCode, typeof(string));
            table.Columns.Add(fUserPrefix, typeof(string));
            table.Columns.Add(fUserFirstName, typeof(string));
            table.Columns.Add(fUserMiddleName, typeof(string));
            table.Columns.Add(fUserLastName, typeof(string));
            table.Columns.Add(fUserEmail, typeof(string));
            table.Columns.Add(fUserPhone, typeof(string));
            table.Columns.Add(fCompanyName, typeof(string));
            table.Columns.Add(fUserTitle, typeof(string));
            table.Columns.Add(fCompanyStAddress, typeof(string));
            table.Columns.Add(fCompanyCity, typeof(string));
            table.Columns.Add(fCountry, typeof(string));
            table.Columns.Add(fCompanyState, typeof(string));
            table.Columns.Add(fCompanyZipCode, typeof(string));
            table.Columns.Add(fCompanyWebSite, typeof(string));
            table.Columns.Add(fVersion, typeof(Int32));
            table.Columns.Add(fPicFile, typeof(string));
            table.Columns.Add(fOptionField1, typeof(string));
            table.Columns.Add(fOptionField2, typeof(string));
            table.Columns.Add(fOptionField3, typeof(string));
            table.Columns.Add(fOptionField4, typeof(string));
            table.Columns.Add(fOptionField5, typeof(string));


            return table;

        }

    }
}
