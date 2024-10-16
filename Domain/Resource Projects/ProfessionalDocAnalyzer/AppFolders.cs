using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Atebion.Common;

namespace ProfessionalDocAnalyzer
{
    static class AppFolders
    {

        private static string _ErrorMessage = string.Empty;
        public static string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        private static bool _UseHootSearchEngine = false;
        public static bool UseHootSearchEngine
        {
            get { return _UseHootSearchEngine; }
            set { _UseHootSearchEngine = value; }
        }

        private static string _UserName = string.Empty;
        public static string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }


        private static string _ConnectWorkgroupPath = string.Empty;
        public static string ConnectWorkgroupPath
        {
            set { _ConnectWorkgroupPath = value; }
            get { return _ConnectWorkgroupPath; }
        }

        private static string _WorkgroupName = string.Empty;
        public static string WorkgroupName
        {
            set { _WorkgroupName = value; }
            get { return _WorkgroupName; }
        }

        private static string _ProjectName = string.Empty;
        public static string ProjectName
        {
            set { _ProjectName = value; }
            get { return _ProjectName; }
        }

        private static string _WorkgroupRootPath = string.Empty;
        public static string WorkgroupRootPath
        {
            set { _WorkgroupRootPath = value; }
            get { return _WorkgroupRootPath; }
        }

        private static string _TaskName = string.Empty;
        public static string TaskName
        {
            set { _TaskName = value; }
            get { return _TaskName; }
        }

        private static string _Task = string.Empty;
        public static string Task
        {
            set { _Task = value; }
            get { return _Task; }
        }


  

        private static string _Project = string.Empty;
        public static string Project
        {
            get
            {
                string s = AppDataPath;
                _Project = Path.Combine(@_AppDataPath, "Projects");
                _ErrorMessage = Directories.DirExistsOrCreate(_Project);
                if (_ErrorMessage != string.Empty)
                {
                    _Project = string.Empty;

                }
                return _Project;
            }
        }

        private static string _ProjectCurrent = string.Empty;
        public static string ProjectCurrent
        {
            get
            {
                if (ProjectName == string.Empty)
                    return string.Empty;

                _ProjectCurrent = Path.Combine(_Project, _ProjectName);
                _ErrorMessage = Directories.DirExistsOrCreate(_ProjectCurrent);
                if (_ErrorMessage != string.Empty)
                {
                    _ProjectCurrent = string.Empty;

                }

                return _ProjectCurrent;
            }
        }

        // copy document files here before a document name has been define
        private static string _ProjectCurrentTemp = string.Empty;
        public static string ProjectCurrentTemp
        {
            get
            {
                if (_ProjectCurrent == string.Empty)
                    return string.Empty;

                _ProjectCurrentTemp = Path.Combine(_ProjectCurrent, "Temp");
                _ErrorMessage = Directories.DirExistsOrCreate(_ProjectCurrentTemp);
                if (_ErrorMessage != string.Empty)
                {
                    _ProjectCurrentTemp = string.Empty;

                }

                return _ProjectCurrentTemp;
            }
        }

  
        private static string _AnalysisPath = string.Empty;
        public static string AnalysisPath
        {
            get
            {
                if (ProjectCurrent == string.Empty)
                    return string.Empty;

                _AnalysisPath = Path.Combine(ProjectCurrent, "Analysis");

                _ErrorMessage = Directories.DirExistsOrCreate(_AnalysisPath);
                if (_ErrorMessage != string.Empty)
                {
                    _AnalysisPath = string.Empty;
                    return _AnalysisPath;

                }
                return _AnalysisPath;
            }

        }

        private static string _AnalysisName = string.Empty;
        public static string AnalysisName
        {
            set
            {
                _AnalysisName = value;
                // Create subfolders
                string s = string.Empty;
                s = AnalysisCurrent;
                s = AnalysisCurrentCompareDocsReport;
                s = AnalysisCurrentDocs;
                s = AnalysisCurrentDocsDocName;
                s = AnalysisParseSeg;
                s = AnalysisHTML;
                s = AnalysisInfor;
                s = AnalysisIndex2;
                s = AnalysisNotes;
                s = AnalysisXML;
                s = AnalysisReports;
            }

            get { return _AnalysisName; }
        }


        private static string _AnalysisCurrent = string.Empty;
        public static string AnalysisCurrent
        {
            get
            {
                if (AnalysisName == string.Empty)
                    return string.Empty;

                _AnalysisCurrent = Path.Combine(AnalysisPath, AnalysisName);

                _ErrorMessage = Directories.DirExistsOrCreate(_AnalysisCurrent);
                if (_ErrorMessage != string.Empty)
                {
                    _AnalysisCurrent = string.Empty;
                    return _AnalysisCurrent;

                }

                return _AnalysisCurrent;
            }
        }

        private static string _AnalysisCurrentDiffMods = string.Empty;
        public static string AnalysisCurrentDiffMods
        {
            get
            {
                if (AnalysisName == string.Empty)
                    return string.Empty;

                _AnalysisCurrentDiffMods = Path.Combine(_AnalysisCurrent, "Mods");

                _ErrorMessage = Directories.DirExistsOrCreate(_AnalysisCurrentDiffMods);
                if (_ErrorMessage != string.Empty)
                {
                    _AnalysisCurrentDiffMods = string.Empty;
                    return string.Empty;

                }

                return _AnalysisCurrentDiffMods;
            }
        }

        private static string _AnalysisCurrentDiffModsPart = string.Empty;
        public static string AnalysisCurrentDiffModsPart
        {
            get
            {
                if (AnalysisName == string.Empty)
                    return string.Empty;

                _AnalysisCurrentDiffModsPart = Path.Combine(AnalysisCurrentDiffMods, "Part");

                _ErrorMessage = Directories.DirExistsOrCreate(_AnalysisCurrentDiffModsPart);
                if (_ErrorMessage != string.Empty)
                {
                    _AnalysisCurrentDiffModsPart = string.Empty;
                    return string.Empty;

                }

                return _AnalysisCurrentDiffModsPart;
            }
        }

        private static string _AnalysisCurrentDiffModsWhole = string.Empty;
        public static string AnalysisCurrentDiffModsWhole
        {
            get
            {
                if (AnalysisName == string.Empty)
                    return string.Empty;

                _AnalysisCurrentDiffModsWhole = Path.Combine(AnalysisCurrentDiffMods, "Whole");

                _ErrorMessage = Directories.DirExistsOrCreate(_AnalysisCurrentDiffModsWhole);
                if (_ErrorMessage != string.Empty)
                {
                    _AnalysisCurrentDiffModsWhole = string.Empty;
                    return string.Empty;

                }

                return _AnalysisCurrentDiffModsWhole;
            }
        }

        private static string _AnalysisCurrentDiffNotes = string.Empty;
        public static string AnalysisCurrentDiffNotes
        {
            get
            {
                if (AnalysisName == string.Empty)
                    return string.Empty;

                _AnalysisCurrentDiffNotes = Path.Combine(AnalysisCurrent, "Notes");

                _ErrorMessage = Directories.DirExistsOrCreate(_AnalysisCurrentDiffNotes);
                if (_ErrorMessage != string.Empty)
                {
                    _AnalysisCurrentDiffNotes = string.Empty;
                    return string.Empty;

                }

                return _AnalysisCurrentDiffNotes;
            }
        }

        private static string _AnalysisCurrentDiffNotesHTML = string.Empty;
        public static string AnalysisCurrentDiffNotesHTML
        {
            get
            {
                if (AnalysisName == string.Empty)
                    return string.Empty;

                _AnalysisCurrentDiffNotesHTML = Path.Combine(AnalysisCurrentDiffNotes, "HTML");

                _ErrorMessage = Directories.DirExistsOrCreate(_AnalysisCurrentDiffNotesHTML);
                if (_ErrorMessage != string.Empty)
                {
                    _AnalysisCurrentDiffNotesHTML = string.Empty;
                    return string.Empty;

                }

                return _AnalysisCurrentDiffNotesHTML;
            }
        }

        private static string _AnalysisCurrentCompareDocsReport = string.Empty;
        public static string AnalysisCurrentCompareDocsReport
        {
            get
            {
                if (AnalysisName == string.Empty)
                    return string.Empty;

                _AnalysisCurrentCompareDocsReport = Path.Combine(_AnalysisCurrent, "CompareReports");

                _ErrorMessage = Directories.DirExistsOrCreate(_AnalysisCurrentCompareDocsReport);
                if (_ErrorMessage != string.Empty)
                {
                    _AnalysisCurrentCompareDocsReport = string.Empty;
                    return _AnalysisCurrentCompareDocsReport;

                }

                return _AnalysisCurrentCompareDocsReport;
            }
        }

        private static string _AnalysisCurrentDocs = string.Empty;
        public static string AnalysisCurrentDocs
        {
            get
            {
                if (AnalysisName == string.Empty)
                    return string.Empty;

                _AnalysisCurrentDocs = Path.Combine(_AnalysisCurrent, "Docs");

                _ErrorMessage = Directories.DirExistsOrCreate(_AnalysisCurrentDocs);
                if (_ErrorMessage != string.Empty)
                {
                    _AnalysisCurrentDocs = string.Empty;
                    return _AnalysisCurrent;

                }

                return _AnalysisCurrentDocs;
            }
        }

        private static string _AnalysisCurrentDocsDocName = string.Empty;
        public static string AnalysisCurrentDocsDocName
        {
            get
            {
                if (AnalysisName == string.Empty)
                    return string.Empty;

                if (DocName == string.Empty)
                    return string.Empty;


                _AnalysisCurrentDocsDocName = Path.Combine(_AnalysisCurrentDocs, DocName);

                _ErrorMessage = Directories.DirExistsOrCreate(_AnalysisCurrentDocsDocName);
                if (_ErrorMessage != string.Empty)
                {
                    _AnalysisCurrentDocsDocName = string.Empty;
                    return _AnalysisCurrent;

                }

                return _AnalysisCurrentDocsDocName;
            }
        }




        private static string _AnalysisParseSeg = string.Empty;
        public static string AnalysisParseSeg
        {
            get
            {
                if (AnalysisCurrentDocsDocName == string.Empty)
                    return string.Empty;

                _AnalysisParseSeg = Path.Combine(AnalysisCurrentDocsDocName, "ParseSeg");

                _ErrorMessage = Directories.DirExistsOrCreate(_AnalysisParseSeg);
                if (_ErrorMessage != string.Empty)
                {
                    _AnalysisParseSeg = string.Empty;
                    return _AnalysisParseSeg;

                }

                return _AnalysisParseSeg;
            }
        }

        // Don't think this is used -- ToDo check
        private static string _AnalysisParseSegQtyChk = string.Empty;
        public static string AnalysisParseSegQtyChk
        {
            get
            {
                if (AnalysisParseSeg == string.Empty)
                    return string.Empty;

                _AnalysisParseSegQtyChk = Path.Combine(AnalysisParseSeg, "QC");

                _ErrorMessage = Directories.DirExistsOrCreate(_AnalysisParseSegQtyChk);
                if (_ErrorMessage != string.Empty)
                {
                    _AnalysisParseSegQtyChk = string.Empty;
                    return _AnalysisParseSegQtyChk;

                }

                return _AnalysisParseSegQtyChk;
            }
        }

        private static string _AnalysisParseSegLSQC = string.Empty;
        public static string AnalysisParseSegLSQC
        {
            get
            {
                if (AnalysisParseSeg == string.Empty)
                    return string.Empty;

                _AnalysisParseSegLSQC = Path.Combine(AnalysisParseSeg, "LSQC");

                _ErrorMessage = Directories.DirExistsOrCreate(_AnalysisParseSegLSQC);
                if (_ErrorMessage != string.Empty)
                {
                    _AnalysisParseSegLSQC = string.Empty;
                    return _AnalysisParseSegLSQC;

                }

                return _AnalysisParseSegLSQC;
            }
        }

        // Keywords
        private static string _AnalysisParseSegKeywords = string.Empty;
        public static string AnalysisParseSegKeywords
        {
            get
            {
                if (AnalysisParseSeg == string.Empty)
                    return string.Empty;

                _AnalysisParseSegKeywords = Path.Combine(AnalysisCurrentDocsDocName, "Keywords");

                _ErrorMessage = Directories.DirExistsOrCreate(_AnalysisParseSegKeywords);
                if (_ErrorMessage != string.Empty)
                {
                    _AnalysisParseSegKeywords = string.Empty;
                    return _AnalysisParseSegKeywords;

                }

                return _AnalysisParseSegKeywords;
            }
        }

        private static string _DictionariesPath = string.Empty;
        public static string DictionariesPath
        {
            get
            {

                _DictionariesPath = Path.Combine(@AppDataPath, "Dictionaries");

                _ErrorMessage = Directories.DirExistsOrCreate(_DictionariesPath);
                if (_ErrorMessage != string.Empty)
                {
                    _DictionariesPath = string.Empty;

                }

                return _DictionariesPath;
            }

        }

        private static string _DictionariesRemovedPath = string.Empty;
        public static string DictionariesRemovedPath
        {
            get
            {

                _DictionariesRemovedPath = Path.Combine(DictionariesPath, "Removed");

                _ErrorMessage = Directories.DirExistsOrCreate(_DictionariesRemovedPath);
                if (_ErrorMessage != string.Empty)
                {
                    _DictionariesRemovedPath = string.Empty;

                }

                return _DictionariesRemovedPath;
            }

        }

        private static string _AnalysisParseSegExport = string.Empty;
        public static string AnalysisParseSegExport
        {
            get
            {
                if (AnalysisParseSeg == string.Empty)
                    return string.Empty;

                _AnalysisParseSegExport = Path.Combine(AnalysisParseSeg, "Export");

                _ErrorMessage = Directories.DirExistsOrCreate(_AnalysisParseSegExport);
                if (_ErrorMessage != string.Empty)
                {
                    _AnalysisParseSegExport = string.Empty;
                    return _AnalysisParseSegExport;

                }

                return _AnalysisParseSegExport;
            }
        }

        private static string _AnalysisParseSegXML = string.Empty;
        public static string AnalysisParseSegXML
        {
            get
            {
                if (AnalysisParseSeg == string.Empty)
                    return string.Empty;

                _AnalysisParseSegXML = Path.Combine(AnalysisParseSeg, "XML");

                _ErrorMessage = Directories.DirExistsOrCreate(_AnalysisParseSegXML);
                if (_ErrorMessage != string.Empty)
                {
                    _AnalysisParseSegXML = string.Empty;
                    return _AnalysisParseSegXML;

                }

                return _AnalysisParseSegXML;
            }
        }

        private static string _AnalysisParseSegIndex2 = string.Empty;
        public static string AnalysisParseSegIndex2
        {
            get
            {
                if (AnalysisParseSeg == string.Empty)
                    return string.Empty;

                _AnalysisParseSegIndex2 = Path.Combine(AnalysisParseSeg, "Index2");

                _ErrorMessage = Directories.DirExistsOrCreate(_AnalysisParseSegIndex2);
                if (_ErrorMessage != string.Empty)
                {
                    _AnalysisParseSegIndex2 = string.Empty;
                    return _AnalysisParseSegIndex2;

                }

                return _AnalysisParseSegIndex2;
            }
        }

        private static string _AnalysisParseSegNotes = string.Empty;
        public static string AnalysisParseSegNotes
        {
            get
            {
                if (AnalysisParseSeg == string.Empty)
                    return string.Empty;

                _AnalysisParseSegNotes = Path.Combine(AnalysisParseSeg, "Notes");

                _ErrorMessage = Directories.DirExistsOrCreate(_AnalysisParseSegNotes);
                if (_ErrorMessage != string.Empty)
                {
                    _AnalysisParseSegNotes = string.Empty;
                    return _AnalysisParseSegNotes;

                }

                return _AnalysisParseSegNotes;
            }
        }

        private static string _AnalysisParseSegHTML = string.Empty;
        public static string AnalysisParseSegHTML
        {
            get
            {
                if (AnalysisParseSeg == string.Empty)
                    return string.Empty;

                _AnalysisParseSegHTML = Path.Combine(AnalysisParseSeg, "HTML");

                _ErrorMessage = Directories.DirExistsOrCreate(_AnalysisParseSegHTML);
                if (_ErrorMessage != string.Empty)
                {
                    _AnalysisParseSegHTML = string.Empty;
                    return _AnalysisParseSegHTML;

                }

                return _AnalysisParseSegHTML;
            }
        }

        private static string _AnalysisHTML = string.Empty;
        public static string AnalysisHTML
        {
            get
            {
                if (AnalysisCurrent == string.Empty)
                    return string.Empty;

                _AnalysisHTML = Path.Combine(AnalysisCurrentDocsDocName, "HTML");

                _ErrorMessage = Directories.DirExistsOrCreate(_AnalysisHTML);
                if (_ErrorMessage != string.Empty)
                {
                    _AnalysisHTML = string.Empty;
                    return _AnalysisHTML;

                }

                return _AnalysisHTML;
            }
        }

        private static string _AnalysisIndex2 = string.Empty;
        public static string AnalysisIndex2
        {
            get
            {
                if (AnalysisCurrent == string.Empty)
                    return string.Empty;

                _AnalysisIndex2 = Path.Combine(AnalysisCurrentDocsDocName, "Index2");

                _ErrorMessage = Directories.DirExistsOrCreate(_AnalysisIndex2);
                if (_ErrorMessage != string.Empty)
                {
                    _AnalysisIndex2 = string.Empty;
                    return _AnalysisIndex2;

                }

                return _AnalysisIndex2;
            }
        }

        private static string _AnalysisInfor = string.Empty;
        public static string AnalysisInfor
        {
            get
            {
                if (AnalysisCurrent == string.Empty)
                    return string.Empty;

                _AnalysisInfor = Path.Combine(AnalysisCurrentDocsDocName, "Infor");

                _ErrorMessage = Directories.DirExistsOrCreate(_AnalysisInfor);
                if (_ErrorMessage != string.Empty)
                {
                    _AnalysisInfor = string.Empty;
                    return _AnalysisInfor;

                }

                return _AnalysisInfor;
            }
        }

        private static string _AnalysisNotes = string.Empty;
        public static string AnalysisNotes
        {
            get
            {
                if (AnalysisCurrent == string.Empty)
                    return string.Empty;

                _AnalysisNotes = Path.Combine(AnalysisCurrentDocsDocName, "Notes");

                _ErrorMessage = Directories.DirExistsOrCreate(_AnalysisNotes);
                if (_ErrorMessage != string.Empty)
                {
                    _AnalysisNotes = string.Empty;
                    return _AnalysisNotes;

                }

                return _AnalysisNotes;
            }
        }

        private static string _AnalysisXML = string.Empty;
        public static string AnalysisXML
        {
            get
            {
                if (AnalysisCurrent == string.Empty)
                    return string.Empty;

                _AnalysisXML = Path.Combine(AnalysisCurrentDocsDocName, "XML");

                _ErrorMessage = Directories.DirExistsOrCreate(_AnalysisXML);
                if (_ErrorMessage != string.Empty)
                {
                    _AnalysisXML = string.Empty;
                    return _AnalysisXML;

                }

                return _AnalysisXML;
            }
        }

        private static string _AnalysisReports = string.Empty;
        public static string AnalysisReports
        {
            get
            {
                if (AnalysisCurrent == string.Empty)
                    return string.Empty;

                _AnalysisReports = Path.Combine(AnalysisCurrentDocsDocName, "Reports");

                _ErrorMessage = Directories.DirExistsOrCreate(_AnalysisReports);
                if (_ErrorMessage != string.Empty)
                {
                    _AnalysisReports = string.Empty;
                    return _AnalysisReports;

                }

                return _AnalysisReports;
            }
        }
        



        private static string _DocName = string.Empty;
        public static string DocName
        {
            set
            {
                _DocName = value;
                string s = CurrentDocPath;
                s = DocPath;
                s = DocParsedSecDiff;
                s = DocParsedSec;
                s = DocInformation;
                s = DocParsedSecIndex2;
                s = DocParsedSecExport;
                s = DocParsedSecNotes;
                s = DocParsedSecXML;
                s = DocParsedSecTemp;

            }
            get { return _DocName; }
        }

        private static string _docNamePath = string.Empty;

        public static string DocNamePath
        {
            get
            {
                if (DocName == string.Empty)
                    return string.Empty;

                _docNamePath = Path.Combine(@DocPath, DocName);

                _ErrorMessage = Directories.DirExistsOrCreate(_docNamePath);
                if (_ErrorMessage != string.Empty)
                {
                    _docNamePath = string.Empty;
                    return _docNamePath;

                }
                return _docNamePath;
            }

        }

        private static string _DocPathNextVersion = string.Empty;
        public static string GetDocPathNextVersion()
        {
            if (DocName == string.Empty)
                return string.Empty;

            string docNamePath = Path.Combine(DocPath, _DocName);
            string newVersionDir = string.Empty;
            
            for (int i = 1; i <37000; i++)
            {
                newVersionDir = Path.Combine(docNamePath, i.ToString());
                if (!Directory.Exists(newVersionDir))
                {
                    return newVersionDir;
                }
            }

            return string.Empty;
        }

        private static string _CurrentDocPath = string.Empty;

        public static string CurrentDocPath
        {
            get
            {
                if (DocName == string.Empty)
                    return string.Empty;

                string docNamePath = Path.Combine(DocPath, _DocName);

                _ErrorMessage = Directories.DirExistsOrCreate(docNamePath);
                if (_ErrorMessage != string.Empty)
                {
                    _CurrentDocPath = string.Empty;
                    return _CurrentDocPath;

                }

                _CurrentDocPath = Path.Combine(docNamePath, "Current");

                _ErrorMessage = Directories.DirExistsOrCreate(_CurrentDocPath);
                if (_ErrorMessage != string.Empty)
                {
                    _CurrentDocPath = string.Empty;

                }

                return _CurrentDocPath;
            }

        }

        private static string _DocParsedSecDiff = string.Empty;

        public static string DocParsedSecDiff
        {
            get
            {
                if (CurrentDocPath == string.Empty)
                    return string.Empty;

                _DocParsedSecDiff = Path.Combine(@CurrentDocPath, "Diff");

                _ErrorMessage = Directories.DirExistsOrCreate(_DocParsedSecDiff);
                if (_ErrorMessage != string.Empty)
                {
                    _DocParsedSecDiff = string.Empty;

                }

                return _DocParsedSecDiff;
            }

        }

        private static string _DocParsedSecKeywords = string.Empty; // 06.06.2014
        public static string DocParsedSecKeywords
        {
            get
            {
                if (DocParsedSec == string.Empty)
                    return string.Empty;

                _DocParsedSecKeywords = Path.Combine(DocParsedSec, "Keywords");

                _ErrorMessage = Directories.DirExistsOrCreate(_DocParsedSecKeywords);
                if (_ErrorMessage != string.Empty)
                {
                    _DocParsedSecKeywords = string.Empty;

                }

                return _DocParsedSecKeywords;
            }

        }


        private static string _DocParsedSecXML = string.Empty;

        public static string DocParsedSecXML
        {
            get
            {
                if (DocParsedSec == string.Empty)
                    return string.Empty;

                _DocParsedSecXML = Path.Combine(DocParsedSec, "XML");

                _ErrorMessage = Directories.DirExistsOrCreate(_DocParsedSecXML);
                if (_ErrorMessage != string.Empty)
                {
                    _DocParsedSecXML = string.Empty;

                }

                return _DocParsedSecXML;
            }

        }

        private static string _DocParsedSecXML_Hold = string.Empty;
        public static string DocParsedSecXML_Hold
        {
            get
            {
                if (DocParsedSec == string.Empty)
                    return string.Empty;

                _DocParsedSecXML_Hold = Path.Combine(DocParsedSec, "XML_Hold");

                _ErrorMessage = Directories.DirExistsOrCreate(_DocParsedSecXML_Hold);
                if (_ErrorMessage != string.Empty)
                {
                    _DocParsedSecXML_Hold = string.Empty;

                }

                return _DocParsedSecXML_Hold;
            }

        }

        private static string _DocParsedSecXML_Hold_Legal = string.Empty;
        public static string DocParsedSecXML_Hold_Legal
        {
            get
            {
                if (DocParsedSecXML_Hold == string.Empty)
                    return string.Empty;

                _DocParsedSecXML_Hold_Legal = Path.Combine(DocParsedSecXML_Hold, "Legal");

                _ErrorMessage = Directories.DirExistsOrCreate(_DocParsedSecXML_Hold_Legal);
                if (_ErrorMessage != string.Empty)
                {
                    _DocParsedSecXML_Hold_Legal = string.Empty;

                }

                return _DocParsedSecXML_Hold_Legal;
            }

        }

        private static string _DocParsedSecXML_Hold_Paragraph = string.Empty;
        public static string DocParsedSecXML_Hold_Paragraph
        {
            get
            {
                if (DocParsedSecXML_Hold == string.Empty)
                    return string.Empty;

                _DocParsedSecXML_Hold_Paragraph = Path.Combine(DocParsedSecXML_Hold, "Paragraph");

                _ErrorMessage = Directories.DirExistsOrCreate(_DocParsedSecXML_Hold_Paragraph);
                if (_ErrorMessage != string.Empty)
                {
                    _DocParsedSecXML_Hold_Paragraph = string.Empty;

                }

                return _DocParsedSecXML_Hold_Paragraph;
            }

        }


        private static string _DocParsedSecQtyChk = string.Empty;

        public static string DocParsedSecQtyCh
        {
            get
            {
                if (DocParsedSec == string.Empty)
                    return string.Empty;

                _DocParsedSecQtyChk = Path.Combine(@DocParsedSec, "QC");

                _ErrorMessage = Directories.DirExistsOrCreate(_DocParsedSecQtyChk);
                if (_ErrorMessage != string.Empty)
                {
                    _DocParsedSecQtyChk = string.Empty;

                }

                return _DocParsedSecQtyChk;
            }

        }

        private static string _DocParsedSecQtyChkRpt = string.Empty;

        public static string DocParsedSecQtyChkRpt
        {
            get
            {
                if (DocParsedSec == string.Empty)
                    return string.Empty;

                _DocParsedSecQtyChkRpt = Path.Combine(DocParsedSecQtyCh, "Reports");

                _ErrorMessage = Directories.DirExistsOrCreate(_DocParsedSecQtyChkRpt);
                if (_ErrorMessage != string.Empty)
                {
                    _DocParsedSecQtyChkRpt = string.Empty;

                }

                return _DocParsedSecQtyChkRpt;
            }

        }


        private static string _DocParsedSecIndex = string.Empty; // Use for hooter index

        public static string DocParsedSecIndex
        {
            get
            {
                if (DocParsedSec == string.Empty)
                    return string.Empty;

                _DocParsedSecIndex = Path.Combine(DocParsedSec, "Index");

                _ErrorMessage = Directories.DirExistsOrCreate(_DocParsedSecIndex);
                if (_ErrorMessage != string.Empty)
                {
                    _DocParsedSecIndex = string.Empty;

                }

                return _DocParsedSecIndex;
            }

        }


        private static string _DocParsedSecIndex2 = string.Empty; // Use for Lucene Indexer

        public static string DocParsedSecIndex2
        {
            get
            {
                if (DocParsedSec == string.Empty)
                    return string.Empty;

                _DocParsedSecIndex2 = Path.Combine(DocParsedSec, "Index2");

                _ErrorMessage = Directories.DirExistsOrCreate(_DocParsedSecIndex2);
                if (_ErrorMessage != string.Empty)
                {
                    _DocParsedSecIndex2 = string.Empty;

                }

                return _DocParsedSecIndex2;
            }

        }

        private static string _DocParsedSecExport = string.Empty;

        public static string DocParsedSecExport
        {
            get
            {
                if (DocParsedSec == string.Empty)
                    return string.Empty;

                _DocParsedSecExport = Path.Combine(DocParsedSec, "Export");

                _ErrorMessage = Directories.DirExistsOrCreate(_DocParsedSecExport);
                if (_ErrorMessage != string.Empty)
                {
                    _DocParsedSecExport = string.Empty;

                }

                return _DocParsedSecExport;
            }

        }


        private static string _DocParsedSecNotes = string.Empty;

        public static string DocParsedSecNotes
        {
            get
            {
                if (DocParsedSec == string.Empty)
                    return string.Empty;

                _DocParsedSecNotes = Path.Combine(DocParsedSec, "Notes");

                _ErrorMessage = Directories.DirExistsOrCreate(_DocParsedSecNotes);
                if (_ErrorMessage != string.Empty)
                {
                    _DocParsedSecNotes = string.Empty;

                }

                return _DocParsedSecNotes;
            }

        }


        private static string _DocParsedSecNotesHTML = string.Empty;

        public static string DocParsedSecNotesHTML
        {
            get
            {
                if (DocParsedSec == string.Empty)
                    return string.Empty;

                _DocParsedSecNotesHTML = Path.Combine(DocParsedSecNotes, "HTML");

                _ErrorMessage = Directories.DirExistsOrCreate(_DocParsedSecNotesHTML);
                if (_ErrorMessage != string.Empty)
                {
                    _DocParsedSecNotesHTML = string.Empty;

                }

                return _DocParsedSecNotesHTML;
            }

        }


        private static string _DocParsedSecTemp = string.Empty;

        public static string DocParsedSecTemp
        {
            get
            {
                if (DocParsedSec == string.Empty)
                    return string.Empty;

                _DocParsedSecTemp = Path.Combine(DocParsedSec, "Temp");

                _ErrorMessage = Directories.DirExistsOrCreate(_DocParsedSecTemp);
                if (_ErrorMessage != string.Empty)
                {
                    _DocParsedSecTemp = string.Empty;

                }

                return _DocParsedSecTemp;
            }

        }


        private static string _DocParsedSec = string.Empty;

        public static string DocParsedSec
        {
            get
            {
                if (CurrentDocPath == string.Empty)
                    return string.Empty;

                _DocParsedSec = Path.Combine(CurrentDocPath, "ParseSec");

                _ErrorMessage = Directories.DirExistsOrCreate(_DocParsedSec);
                if (_ErrorMessage != string.Empty)
                {
                    _DocParsedSec = string.Empty;

                }

                return _DocParsedSec;
            }

        }


        private static string _DocParsedSec_Hold = string.Empty;
        public static string DocParsedSec_Hold
        {
            get
            {
                if (CurrentDocPath == string.Empty)
                    return string.Empty;

                _DocParsedSec_Hold = Path.Combine(CurrentDocPath, "ParseSec_Hold");

                _ErrorMessage = Directories.DirExistsOrCreate(_DocParsedSec_Hold);
                if (_ErrorMessage != string.Empty)
                {
                    _DocParsedSec_Hold = string.Empty;

                }

                return _DocParsedSec_Hold;
            }

        }

        private static string _DocParsedSec_Hold_Logic = string.Empty;
        public static string DocParsedSec_Hold_Logic
        {
            get
            {
                if (DocParsedSec_Hold == string.Empty)
                    return string.Empty;

                _DocParsedSec_Hold_Logic = Path.Combine(DocParsedSec_Hold, "Logic");

                _ErrorMessage = Directories.DirExistsOrCreate(_DocParsedSec_Hold_Logic);
                if (_ErrorMessage != string.Empty)
                {
                    _DocParsedSec_Hold_Logic = string.Empty;

                }

                return _DocParsedSec_Hold_Logic;
            }

        }

        private static string _DocParsedSec_Hold_Logic_XML = string.Empty;
        public static string DocParsedSec_Hold_Logic_XML
        {
            get
            {
                if (DocParsedSec_Hold_Logic == string.Empty)
                    return string.Empty;

                _DocParsedSec_Hold_Logic_XML = Path.Combine(DocParsedSec_Hold_Logic, "XML");

                _ErrorMessage = Directories.DirExistsOrCreate(_DocParsedSec_Hold_Logic_XML);
                if (_ErrorMessage != string.Empty)
                {
                    _DocParsedSec_Hold_Logic_XML = string.Empty;

                }

                return _DocParsedSec_Hold_Logic_XML;
            }

        }

        private static string _DocParsedSec_Hold_Paragraph = string.Empty;
        public static string DocParsedSec_Hold_Paragraph
        {
            get
            {
                if (DocParsedSec_Hold == string.Empty)
                    return string.Empty;

                _DocParsedSec_Hold_Paragraph = Path.Combine(DocParsedSec_Hold, "Paragraph");

                _ErrorMessage = Directories.DirExistsOrCreate(_DocParsedSec_Hold_Paragraph);
                if (_ErrorMessage != string.Empty)
                {
                    _DocParsedSec_Hold_Paragraph = string.Empty;

                }

                return _DocParsedSec_Hold_Paragraph;
            }

        }

        private static string _DocParsedSec_Hold_Paragraph_XML = string.Empty;
        public static string DocParsedSec_Hold_Paragraph_XML
        {
            get
            {
                if (DocParsedSec_Hold_Paragraph == string.Empty)
                    return string.Empty;

                _DocParsedSec_Hold_Paragraph_XML = Path.Combine(DocParsedSec_Hold_Paragraph, "XML");

                _ErrorMessage = Directories.DirExistsOrCreate(_DocParsedSec_Hold_Paragraph_XML);
                if (_ErrorMessage != string.Empty)
                {
                    _DocParsedSec_Hold_Paragraph_XML = string.Empty;

                }

                return _DocParsedSec_Hold_Paragraph_XML;
            }

        }



        // Added 11.02.2013
        private static string _DocParsedSecHTML = string.Empty;
        public static string DocParsedSecHTML
        {
            get
            {
                if (_DocParsedSec == string.Empty)
                    return string.Empty;

                _DocParsedSecHTML = Path.Combine(_DocParsedSec, "HTML");

                _ErrorMessage = Directories.DirExistsOrCreate(_DocParsedSecHTML);
                if (_ErrorMessage != string.Empty)
                {
                    _DocParsedSecHTML = string.Empty;

                }

                return _DocParsedSecHTML;
            }

        }


        private static string _DocPath = string.Empty;

        public static string DocPath
        {
            get
            {
                _DocPath = Path.Combine(ProjectCurrent, "Docs");

                _ErrorMessage = Directories.DirExistsOrCreate(_DocPath);
                if (_ErrorMessage != string.Empty)
                {
                    _DocPath = string.Empty;

                }

                return _DocPath;
            }

        }

        private static string _DocInformation = string.Empty;

        public static string DocInformation
        {
            get
            {
                if (CurrentDocPath == string.Empty)
                    return string.Empty;

                _DocInformation = Path.Combine(CurrentDocPath, "Information");

                _ErrorMessage = Directories.DirExistsOrCreate(_DocInformation);
                if (_ErrorMessage != string.Empty)
                {
                    _DocInformation = string.Empty;

                }

                return _DocInformation;
            }
        }

        // ParsePage

        private static string _DocParsePage = string.Empty;

        public static string DocParsePage
        {
            get
            {
                if (CurrentDocPath == string.Empty)
                    return string.Empty;

                _DocParsePage = Path.Combine(CurrentDocPath, "ParsePage");

                _ErrorMessage = Directories.DirExistsOrCreate(_DocParsePage);
                if (_ErrorMessage != string.Empty)
                {
                    _DocParsePage = string.Empty;

                }

                return _DocParsePage;
            }
        }


        private static string _AppModel = string.Empty;
        public static string AppModel
        {
            get
            {
                _AppModel = Path.Combine(Application.StartupPath, "Model");

                if (!Directory.Exists(_AppModel))
                {
                    _AppModel = string.Empty;
                }


                return _AppModel;
            }
        }

        public static string InIFile 
        {
            get { return Path.Combine(@AppDataPath, "DA.ini"); }
        }

        // Use for copying Keywords to user data folder, 1st time users
        private static string _AppKeywordGrp = string.Empty;
        public static string AppKeywordGrp
        {
            get
            {
                _AppKeywordGrp = Path.Combine(@Application.StartupPath, "KeywordsGrp");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppKeywordGrp);
                if (_ErrorMessage != string.Empty)
                {
                    _AppKeywordGrp = string.Empty;

                }

                return _AppKeywordGrp;
            }
        }

        private static bool _IsLocal = true;
        public static bool IsLocal
        {
            get { return _IsLocal; }
            set { _IsLocal = value; }
        }


        public static void SetRootFolder(string rootFolder, bool isLocal)
        {
            if (rootFolder == string.Empty)
            {
                _IsLocal = true;
                return;
            }

            _IsLocal = isLocal;
            _AppDataPath = rootFolder;

        }

        private static string _AppDataPath_Local = string.Empty;
        public static string AppDataPath_Local
        {
            get
            {
                _AppDataPath_Local = string.Concat(@Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"\Atebion DA");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Local);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Local = string.Empty;

                }

                return _AppDataPath_Local;
            }
        }

        private static string _AppDataPath = string.Empty;
        public static string AppDataPath
        {
            get
            {
                if (_IsLocal)
                    _AppDataPath = AppDataPath_Local;

                if (_AppDataPath == string.Empty)
                    _AppDataPath = AppDataPath_Local;

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
                _AppDataPathUser = Path.Combine(AppDataPath_Local, "User");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPathUser);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPathUser = string.Empty;

                }

                return _AppDataPathUser;
            }
        }

 
        private static string _KeywordGrpPath = string.Empty;
        public static string KeywordGrpPath
        {
            get
            {

                _KeywordGrpPath = Path.Combine(AppDataPath, "KeywordsGrp");

                _ErrorMessage = Directories.DirExistsOrCreate(_KeywordGrpPath);
                if (_ErrorMessage != string.Empty)
                {
                    _KeywordGrpPath = string.Empty;

                }

                return _KeywordGrpPath;
            }

        }

        private static string _AppDataPathTools = string.Empty;
        public static string AppDataPathTools
        {
            get
            {
                _AppDataPathTools = Path.Combine(AppDataPath, "Tools");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPathTools);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPathTools = string.Empty;

                }

                return _AppDataPathTools;
            }
        }

        private static string _AppDataPathToolsAcroSeeker = string.Empty;
        public static string AppDataPathToolsAcroSeeker
        {
            get
            {
                _AppDataPathToolsAcroSeeker = Path.Combine(AppDataPathTools, "AcroSeeker");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPathToolsAcroSeeker);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPathToolsAcroSeeker = string.Empty;

                }

                return _AppDataPathToolsAcroSeeker;
            }
        }

        
        private static string _AppDataPathToolsAcroSeekerDefLib = string.Empty;
        public static string AppDataPathToolsAcroSeekerDefLib
        {
            get
            {
                _AppDataPathToolsAcroSeekerDefLib = Path.Combine(AppDataPathToolsAcroSeeker, "DefLib");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPathToolsAcroSeekerDefLib);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPathToolsAcroSeekerDefLib = string.Empty;

                }

                return _AppDataPathToolsAcroSeekerDefLib;
            }
        }

        private static string _AppDataPathToolsAcroSeekerIgnoreLib = string.Empty;
        public static string AppDataPathToolsAcroSeekerIgnoreLib
        {
            get
            {
                _AppDataPathToolsAcroSeekerIgnoreLib = Path.Combine(AppDataPathToolsAcroSeeker, "IgnoreLib");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPathToolsAcroSeekerIgnoreLib);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPathToolsAcroSeekerIgnoreLib = string.Empty;

                }

                return _AppDataPathToolsAcroSeekerIgnoreLib;
            }
        }

        // QC Settings
        private static string _AppDataPathToolsQC = string.Empty;
        public static string AppDataPathToolsQC
        {
            get
            {
                _AppDataPathToolsQC = Path.Combine(AppDataPathTools, "QC");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPathToolsQC);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPathToolsQC = string.Empty;

                }

                return _AppDataPathToolsQC;
            }
        }


        // Responsibility Assignment Matrix
        private static string _AppDataPathToolsRAMDefs = string.Empty;
        public static string AppDataPathToolsRAMDefs
      {
            get
            {
                _AppDataPathToolsRAMDefs = Path.Combine(AppDataPathTools, "RAMDefs");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPathToolsRAMDefs);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPathToolsRAMDefs = string.Empty;

                }

                return _AppDataPathToolsRAMDefs;
            }
        }

        // Responsibility Assignment Matrix - Removed Path
        private static string _AppDataPathToolsRAMDefs_Removed = string.Empty;
        public static string AppDataPathToolsRAMDefs_Removed
        {
            get
            {
                _AppDataPathToolsRAMDefs_Removed = Path.Combine(AppDataPathToolsRAMDefs, "Removed");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPathToolsRAMDefs_Removed);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPathToolsRAMDefs_Removed = string.Empty;

                }

                return _AppDataPathToolsRAMDefs_Removed;
            }
        }


        
        private static string _AppDataPathToolsExcelTemp = string.Empty;
        public static string AppDataPathToolsExcelTemp
        {
            get
            {
                _AppDataPathToolsExcelTemp = Path.Combine(AppDataPathTools, "ExcelTemp");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPathToolsExcelTemp);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPathToolsExcelTemp = string.Empty;

                }

                return _AppDataPathToolsExcelTemp;
            }
        }

        //// Excel Templates for Concepts Results
        //private static string _AppDataPathToolsExcelTempConcepts = string.Empty;
        //public static string AppDataPathToolsExcelTempConcepts
        //{
        //    get
        //    {
        //        _AppDataPathToolsExcelTempConcepts = Path.Combine(AppDataPathToolsExcelTemp, "Concepts");

        //        _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPathToolsExcelTempConcepts);
        //        if (_ErrorMessage != string.Empty)
        //        {
        //            _AppDataPathToolsExcelTempConcepts = string.Empty;

        //        }

        //        return _AppDataPathToolsExcelTempConcepts;
        //    }
        //}
        
        // Excel Templates for Analysis Results
        private static string _AppDataPathToolsExcelTempAR = string.Empty;
        public static string AppDataPathToolsExcelTempAR
        {
            get
            {
                _AppDataPathToolsExcelTempAR = Path.Combine(AppDataPathToolsExcelTemp, "AR");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPathToolsExcelTempAR);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPathToolsExcelTempAR = string.Empty;

                }

                return _AppDataPathToolsExcelTempAR;
            }
        }

        // Added 2.26.2017
        // Excel Templates for Deep Analysis Results
        private static string _AppDataPathToolsExcelTempDAR = string.Empty;
        public static string AppDataPathToolsExcelTempDAR
        {
            get
            {
                _AppDataPathToolsExcelTempDAR = Path.Combine(AppDataPathToolsExcelTemp, "DAR");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPathToolsExcelTempDAR);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPathToolsExcelTempDAR = string.Empty;

                }

                return _AppDataPathToolsExcelTempDAR;
            }
        }

        private static string _AppDataPathToolsExcelTempConceptsDoc = string.Empty; // Concepts Report Path
        public static string AppDataPathToolsExcelTempConceptsDoc
        {
            get
            {
                _AppDataPathToolsExcelTempConceptsDoc = Path.Combine(AppDataPathToolsExcelTemp, "ConceptsDoc");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPathToolsExcelTempConceptsDoc);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPathToolsExcelTempConceptsDoc = string.Empty;

                }

                return _AppDataPathToolsExcelTempConceptsDoc;
            }
        }

        private static string _AppDataPathToolsExcelTempConceptsDocBackup = string.Empty; // Concepts Report Path
        public static string AppDataPathToolsExcelTempConceptsDocBackup
        {
            get
            {
                _AppDataPathToolsExcelTempConceptsDocBackup = Path.Combine(_AppDataPathToolsExcelTempConceptsDoc, "Backup");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPathToolsExcelTempConceptsDocBackup);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPathToolsExcelTempConceptsDocBackup = string.Empty;

                }

                return _AppDataPathToolsExcelTempConceptsDocBackup;
            }
        }

        private static string _AppDataPathToolsExcelTempConceptsDocs = string.Empty; // Compare Concepts Report Path
        public static string AppDataPathToolsExcelTempConceptsDocs
        {
            get
            {
                _AppDataPathToolsExcelTempConceptsDocs = Path.Combine(AppDataPathToolsExcelTemp, "ConceptsDocs");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPathToolsExcelTempConceptsDocs);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPathToolsExcelTempConceptsDocs = string.Empty;

                }

                return _AppDataPathToolsExcelTempConceptsDocs;
            }
        }

        private static string _AppDataPathToolsExcelTempDicDoc = string.Empty; // Dictionary Report Path
        public static string AppDataPathToolsExcelTempDicDoc
        {
            get
            {
                _AppDataPathToolsExcelTempDicDoc = Path.Combine(AppDataPathToolsExcelTemp, "DicDoc");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPathToolsExcelTempDicDoc);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPathToolsExcelTempDicDoc = string.Empty;

                }

                return _AppDataPathToolsExcelTempDicDoc;
            }
        }

        private static string _AppDataPathToolsExcelTempDicDocBackup = string.Empty; // Dictionary Report Path
        public static string AppDataPathToolsExcelTempDicDocBackup
        {
            get
            {
                _AppDataPathToolsExcelTempDicDocBackup = Path.Combine(_AppDataPathToolsExcelTempDicDoc, "Backup");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPathToolsExcelTempDicDocBackup);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPathToolsExcelTempDicDocBackup = string.Empty;

                }

                return _AppDataPathToolsExcelTempDicDocBackup;
            }
        }

        private static string _AppDataPathToolsExcelTempDicRAM = string.Empty; // Dictionary Report Path
        public static string AppDataPathToolsExcelTempDicRAM
        {
            get
            {
                _AppDataPathToolsExcelTempDicRAM = Path.Combine(AppDataPathToolsExcelTemp, "DicRAM");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPathToolsExcelTempDicRAM);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPathToolsExcelTempDicRAM = string.Empty;

                }

                return _AppDataPathToolsExcelTempDicRAM;
            }
        }

        private static string _AppDataPathToolsExcelTempDicRAMBackup = string.Empty; // BackUp for Dictionary Report Path
        public static string AppDataPathToolsExcelTempDicRAMBackup
        {
            get
            {
                _AppDataPathToolsExcelTempDicRAMBackup = Path.Combine(_AppDataPathToolsExcelTempDicRAM, "Backup");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPathToolsExcelTempDicRAMBackup);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPathToolsExcelTempDicRAMBackup = string.Empty;

                }

                return _AppDataPathToolsExcelTempDicRAMBackup;
            }
        }

        private static string _AppDataPathToolsExcelTempCSVDoc = string.Empty; // CSV Report Path
        public static string AppDataPathToolsExcelTempCSVDoc
        {
            get
            {
                _AppDataPathToolsExcelTempCSVDoc = Path.Combine(AppDataPathToolsExcelTemp, "CSVDoc");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPathToolsExcelTempCSVDoc);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPathToolsExcelTempCSVDoc = string.Empty;

                }

                return _AppDataPathToolsExcelTempCSVDoc;
            }
        }

        private static string _AppDataPathToolsExcelTempFARDoc = string.Empty; // CSV Report Path
        public static string AppDataPathToolsExcelTempFARDoc
        {
            get
            {
                _AppDataPathToolsExcelTempFARDoc = Path.Combine(AppDataPathToolsExcelTemp, "FARDoc");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPathToolsExcelTempFARDoc);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPathToolsExcelTempFARDoc = string.Empty;

                }

                return _AppDataPathToolsExcelTempFARDoc;
            }
        }

        private static string _AppDataPathToolsExcelTempDicDocs = string.Empty; // Compare Dictionary Report Path
        public static string AppDataPathToolsExcelTempDicDocs
        {
            get
            {
                _AppDataPathToolsExcelTempDicDocs = Path.Combine(AppDataPathToolsExcelTemp, "DicDocs");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPathToolsExcelTempDicDocs);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPathToolsExcelTempDicDocs = string.Empty;

                }

                return _AppDataPathToolsExcelTempDicDocs;
            }
        }

        
        private static string _AppDataPathToolsExcelTempARBackup = string.Empty;
        public static string AppDataPathToolsExcelTempARBackup
        {
            get
            {
                _AppDataPathToolsExcelTempARBackup = Path.Combine(_AppDataPathToolsExcelTempAR, "Backup");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPathToolsExcelTempARBackup);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPathToolsExcelTempARBackup = string.Empty;

                }

                return _AppDataPathToolsExcelTempARBackup;
            }
        }

        
        private static string _AppDataPathToolsExcelTempDARBackup = string.Empty;
        public static string AppDataPathToolsExcelTempDARBackup
        {
            get
            {
                _AppDataPathToolsExcelTempDARBackup = Path.Combine(_AppDataPathToolsExcelTempDAR, "Backup");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPathToolsExcelTempDARBackup);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPathToolsExcelTempDARBackup = string.Empty;

                }

                return _AppDataPathToolsExcelTempDARBackup;
            }
        }

        
        private static string _AppDataPathToolsExcelTempTemp = string.Empty;
        public static string AppDataPathToolsExcelTempTemp
        {
            get
            {
                _AppDataPathToolsExcelTempTemp = Path.Combine(AppDataPathToolsExcelTemp, "Temp");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPathToolsExcelTempTemp);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPathToolsExcelTempTemp = string.Empty;

                }

                return _AppDataPathToolsExcelTempTemp;
            }
        }
        
    

    }
}
