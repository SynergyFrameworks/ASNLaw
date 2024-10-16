using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

using Atebion.Common;



namespace Atebion.DeepAnalytics
{
    public static class AppFolders
    {
        private static string _ErrorMessage = string.Empty;
        public static string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        public static string AssemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        private static string _CurrentDocPath = string.Empty;
        public static string CurrentDocPath
        {
            set
            {
                _CurrentDocPath = value;
                ValidatePaths(_CurrentDocPath); // added 4.8.2015

            }
            get { return _CurrentDocPath; }
        }

        public static bool ValidatePaths(string currentDocPath)
        {
            if (_CurrentDocPath == string.Empty)
                return false;

                _CurrentDocPath = currentDocPath;

            string s = DeepAnalyticsPath; // activate DeepAnalyticsPath
            s = CurrentPath;
            s = InformationPath;
            s = ParseSentences;
            s = Export;
            s = HTML;
            s = Index;
            s = Index2;
            s = Keywords;
            s = Notes;
            s = Temp;
            s = XML;

            if (s == string.Empty)
            {
                return false;
            }

            return true;
        }


        private static string _DeepAnalyticsPath = string.Empty;
        public static string DeepAnalyticsPath
        {
            get
            {
                if (_CurrentDocPath == string.Empty)
                    return string.Empty;

                _DeepAnalyticsPath = string.Concat(@_CurrentDocPath, @"\Deep Analytics");

                _ErrorMessage = Directories.DirExistsOrCreate(_DeepAnalyticsPath);
                if (_ErrorMessage != string.Empty)
                {
                    _DeepAnalyticsPath = string.Empty;

                }


                return _DeepAnalyticsPath;
            }  
        }

        private static string _CurrentPath = string.Empty;
        public static string CurrentPath
        {
            get
            {
                if (_DeepAnalyticsPath == string.Empty)
                    return string.Empty;

                _CurrentPath = string.Concat(@DeepAnalyticsPath, @"\Current");

                _ErrorMessage = Directories.DirExistsOrCreate(_CurrentPath);
                if (_ErrorMessage != string.Empty)
                {
                    _CurrentPath = string.Empty;

                }

                return _CurrentPath;
            }
        }

        private static string _DocParsedSec = string.Empty;

        public static string DocParsedSec
        {
            get
            {
                if (CurrentDocPath == string.Empty)
                    return string.Empty;

                _DocParsedSec = string.Concat(@CurrentDocPath, @"\ParseSec");

                _ErrorMessage = Directories.DirExistsOrCreate(_DocParsedSec);
                if (_ErrorMessage != string.Empty)
                {
                    _DocParsedSec = string.Empty;

                }

                return _DocParsedSec;
            }

        }

        private static string _DocParsedSecKeywords = string.Empty; // 06.06.2014
        public static string DocParsedSecKeywords
        {
            get
            {
                if (DocParsedSec == string.Empty)
                    return string.Empty;

                _DocParsedSecKeywords = string.Concat(@DocParsedSec, @"\Keywords");

                _ErrorMessage = Directories.DirExistsOrCreate(_DocParsedSecKeywords);
                if (_ErrorMessage != string.Empty)
                {
                    _DocParsedSecKeywords = string.Empty;

                }

                return _DocParsedSecKeywords;
            }

        }

        private static string _InformationPath = string.Empty;
        public static string InformationPath
        {
            get
            {
                if (_CurrentPath == string.Empty)
                    return string.Empty;

                _InformationPath = string.Concat(@CurrentPath, @"\Information");

                _ErrorMessage = Directories.DirExistsOrCreate(_InformationPath);
                if (_ErrorMessage != string.Empty)
                {
                    _InformationPath = string.Empty;

                }

                return _InformationPath;
            }
        }

        private static string _ParseSentences = string.Empty;
        public static string ParseSentences
        {
            get
            {
                if (_CurrentPath == string.Empty)
                    return string.Empty;

                _ParseSentences = string.Concat(@CurrentPath, @"\Parse Sentences");

                _ErrorMessage = Directories.DirExistsOrCreate(_ParseSentences);
                if (_ErrorMessage != string.Empty)
                {
                    _ParseSentences = string.Empty;

                }

                return _ParseSentences;
            }
        }


        private static string _Export = string.Empty;
        public static string Export
        {
            get
            {
                if (@CurrentPath == string.Empty)
                    return string.Empty;

                _Export = string.Concat(@CurrentPath, @"\Export");

                _ErrorMessage = Directories.DirExistsOrCreate(_Export);
                if (_ErrorMessage != string.Empty)
                {
                    _Export = string.Empty;

                }

                return _Export;
            }
        }


        private static string _HTML = string.Empty;
        public static string HTML
        {
            get
            {
                if (@CurrentPath == string.Empty)
                    return string.Empty;

                _HTML = string.Concat(@CurrentPath, @"\HTML");

                _ErrorMessage = Directories.DirExistsOrCreate(_HTML);
                if (_ErrorMessage != string.Empty)
                {
                    _HTML = string.Empty;

                }

                return _HTML;
            }
        }

        // For Lucene search index
        private static string _Index2 = string.Empty;
        public static string Index2
        {
            get
            {
                if (@CurrentPath == string.Empty)
                    return string.Empty;

                _Index2 = string.Concat(@CurrentPath, @"\Index2");

                _ErrorMessage = Directories.DirExistsOrCreate(_HTML);
                if (_ErrorMessage != string.Empty)
                {
                    _Index2 = string.Empty;

                }

                return _Index2;
            }
        }

        // For Hoot search index
        public static string HootIndexPath = Path.Combine(@CurrentPath, "Index");

        private static string _Index = string.Empty;
        public static string Index
        {
            get
            {
                if (@CurrentPath == string.Empty)
                    return string.Empty;

                _Index = string.Concat(@CurrentPath, @"\Index");

                _ErrorMessage = Directories.DirExistsOrCreate(_Index);
                if (_ErrorMessage != string.Empty)
                {
                    _Index = string.Empty;

                }

                return _Index;
            }
        }

        private static string _Keywords = string.Empty;
        public static string Keywords
        {
            get
            {
                if (@CurrentPath == string.Empty)
                    return string.Empty;

                _Keywords = string.Concat(@CurrentPath, @"\Keywords");

                _ErrorMessage = Directories.DirExistsOrCreate(_Keywords);
                if (_ErrorMessage != string.Empty)
                {
                    _Keywords = string.Empty;

                }

                return _Keywords;
            }
        }

        private static string _Notes = string.Empty;
        public static string Notes
        {
            get
            {
                if (@CurrentPath == string.Empty)
                    return string.Empty;

                _Notes = string.Concat(@CurrentPath, @"\Notes");

                _ErrorMessage = Directories.DirExistsOrCreate(_Notes);
                if (_ErrorMessage != string.Empty)
                {
                    _Notes = string.Empty;

                }

                return _Notes;
            }
        }

        private static string _Temp = string.Empty;
        public static string Temp
        {
            get
            {
                if (@CurrentPath == string.Empty)
                    return string.Empty;

                _Temp = string.Concat(@CurrentPath, @"\Temp");

                _ErrorMessage = Directories.DirExistsOrCreate(_Temp);
                if (_ErrorMessage != string.Empty)
                {
                    _Temp = string.Empty;

                }

                return _Temp;
            }
        }

        private static string _XML = string.Empty;
        public static string XML
        {
            get
            {
                if (@CurrentPath == string.Empty)
                    return string.Empty;

                _XML = string.Concat(@CurrentPath, @"\XML");

                _ErrorMessage = Directories.DirExistsOrCreate(_XML);
                if (_ErrorMessage != string.Empty)
                {
                    _XML = string.Empty;

                }

                return _XML;
            }
        }

        private static string _Model = string.Empty;
        public static string Model
        {
            get
            {
                _Model = string.Concat(AssemblyFolder, @"\Model");

                _ErrorMessage = Directories.DirExistsOrCreate(_Model);
                if (_ErrorMessage != string.Empty)
                {
                    _Model = string.Empty;

                }

                return _Model;
            }
        }


    }
}
