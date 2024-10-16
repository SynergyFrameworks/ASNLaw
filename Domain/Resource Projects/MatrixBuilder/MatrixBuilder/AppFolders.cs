using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace MatrixBuilder
{
    static class AppFolders
    {
        private static string _ErrorMessage = string.Empty;
        public static string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        private static string _UserName = string.Empty;
        public static string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

      //  private static string _UserInfoPathFile = string.Empty;
        public static string UserInfoPathFile
        {
            get { return _AppDataPathUser; }
            set { _AppDataPathUser = value; }
        }

        private static string _WorkgroupCurrent = string.Empty;
        public static string WorkgroupCurrent
        {
            get { return _WorkgroupCurrent; }
            set { _WorkgroupCurrent = value; }
        }

        private static string _ProjectCurrent = string.Empty;
        public static string ProjectCurrent
        {
            get { return _ProjectCurrent; }
            set { _ProjectCurrent = value; }
        }

        private static string _DocParsedSec = string.Empty;
        public static string DocParsedSec
        {
            get
            {
                if (_ProjectCurrent == string.Empty)
                    return string.Empty;

                _DocParsedSec = string.Concat(_ProjectCurrent, @"\ParseSec");

                if (!Directory.Exists(_DocParsedSec))
                {
                    _DocParsedSec = string.Empty;
                }

                return _DocParsedSec;
            }

        }

        private static string _DocParsedSecHTML = string.Empty;
        public static string DocParsedSecHTML
        {
            get
            {
                if (_DocParsedSec == string.Empty)
                    return string.Empty;

                _DocParsedSecHTML = string.Concat(@_DocParsedSec, @"\HTML");

                _ErrorMessage = Directories.DirExistsOrCreate(_DocParsedSecHTML);
                if (_ErrorMessage != string.Empty)
                {
                    _DocParsedSecHTML = string.Empty;

                }

                return _DocParsedSecHTML;
            }

        }



        private static string _AppDataPath = string.Empty;
        public static string AppDataPath
        {
            get
            {
                _AppDataPath = string.Concat(@Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"\Atebion DA");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath = string.Empty;

                }

                return _AppDataPath;
            }
        }

        private static string _AppDataPathUser = string.Empty;
        public static string AppDataPathUser
        {
            get
            {
                _AppDataPathUser = string.Concat(AppDataPath, @"\User");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPathUser);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPathUser = string.Empty;

                }

                return _AppDataPathUser;
            }
        }


    }
}
