using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Atebion.Common;

namespace ProfessionalDocAnalyzer
{
    static public class CurrentSettings
    {


        private static bool _TrainMe = false; // Added 11.18.2015
        public static bool TrainMe
        {
            get { return _TrainMe; }
            set { _TrainMe = value; }
        }

        private static string _Project = string.Empty;

        public static string Project
        {
            get { return _Project; }
            set { _Project = value; }
        }

        private static string _Document = string.Empty;
        public static string Document
        {
            get { return _Document; }
            set { _Document = value; }
        }

        private static bool _QualityAnalyzerLic = false; // Added 06.02.2014
        public static bool QualityAnalyzerLic
        {
            get { return _QualityAnalyzerLic; }
            set { _QualityAnalyzerLic = value; }
        }

        public static string configSecUserSettings
        {
            get { return "UserSettings"; }
        }

        public static string configKeyHideLastestRelease
        {
            get { return "HideLastestRelease"; }
        }

        public static string configKeyLatestRelease
        {
            get { return "LatestRelease"; }
        }

        public static string configSecQC
        {
            get { return "QtyCheck"; }
        }

        public static string configKeySentenceLength
        {
            get { return "SentenceLength"; }
        }

        public static string configKeySentenceColor
        {
            get { return "SentenceColor"; }
        }

        public static string configKeyUseDefaultInstQuestParameters
        {
            get { return "UseDefaultInstQuestParameters"; }
        }


        public static void SetAppInIFile(string sectionName, string keyName, string keyValue)
        {
            string file = AppFolders.InIFile;

            if (!File.Exists(file))
            {
                string lines = string.Concat("[", sectionName, "] ", "\r\n");
                lines = lines + string.Concat(keyName, "=", keyValue, "\r\n");

                Files.WriteStringToFile(lines, file, true);
            }
            else
            {

                IniFile inifile = new IniFile();

                inifile.Load(file);

                IniFile.IniSection sec = inifile.GetSection(sectionName);
                if (sec == null)
                {
                    inifile.AddSection(sectionName).AddKey(keyName).Value = keyValue;
                    inifile.Save(file);
                }
                else
                {
                    IniFile.IniSection.IniKey key = sec.GetKey(keyName);
                    if (key == null)
                    {
                        inifile.AddSection(sectionName).AddKey(keyName).Value = keyValue;                      
                        inifile.Save(file);
                    }
                    else
                    {
                        key.SetValue(keyValue);
                        inifile.Save(file);
                    }
                }
            }

        }


    }
}
