using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Atebion.Common;

namespace Atebion.Concepts
{
    class AppFolders_CA
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

        private static string _CARoot = string.Empty;
        public static string CARoot
        {
            get { return _CARoot; }
            set { _CARoot = value; }
        }

        private static string _CACurrentProject = string.Empty;
        public static string CACurrentProject
        {
            get { return _CACurrentProject; }
            set { _CACurrentProject = value; }
        }

        private static string _CACurrentDoc = string.Empty;
        public static string CACurrentDocument
        {
            get { return _CACurrentDoc; }
            set { _CACurrentDoc = value; }
        }

        //private static string _CACurrentDictionary = string.Empty;
        //public static string CACurrentDictionary
        //{
        //    get { return _CACurrentDictionary; }
        //    set { _CACurrentDictionary = value; }
        //}

        private static string _CAAnalysisName = string.Empty;
        public static string CAAnalysisName
        {
            get { return _CAAnalysisName; }
            set { _CAAnalysisName = value; }
        }


        private static string _AppDataPath_Tools_CA_Templates = string.Empty;
        public static string AppDataPath_Tools_CA_Templates
        {
            get
            {
                _AppDataPath_Tools_CA_Templates = Path.Combine(CARoot, "Templates");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_Templates);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_Templates = string.Empty;

                }

                return _AppDataPath_Tools_CA_Templates;
            }
        }

        private static string _AppDataPath_Tools_CA_Templates_ConceptsDoc = string.Empty;
        public static string AppDataPath_Tools_CA_Templates_ConceptsDoc
        {
            get
            {
                _AppDataPath_Tools_CA_Templates_ConceptsDoc = Path.Combine(AppDataPath_Tools_CA_Templates, "ConceptsDoc");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_Templates_ConceptsDoc);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_Templates_ConceptsDoc = string.Empty;

                }

                return _AppDataPath_Tools_CA_Templates_ConceptsDoc;
            }
        }

        private static string _AppDataPath_Tools_CA_Templates_ConceptsDocs = string.Empty;
        public static string AppDataPath_Tools_CA_Templates_ConceptsDocs
        {
            get
            {
                _AppDataPath_Tools_CA_Templates_ConceptsDocs = Path.Combine(AppDataPath_Tools_CA_Templates, "ConceptsDocs");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_Templates_ConceptsDocs);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_Templates_ConceptsDocs = string.Empty;

                }

                return _AppDataPath_Tools_CA_Templates_ConceptsDocs;
            }
        }

        //private static string _AppDataPath_Tools_CA_Templates_DicDoc = string.Empty;
        //public static string AppDataPath_Tools_CA_Templates_DicDoc
        //{
        //    get
        //    {
        //        _AppDataPath_Tools_CA_Templates_DicDoc = Path.Combine(AppDataPath_Tools_CA_Templates, "DicDoc");

        //        _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_Templates_DicDoc);
        //        if (_ErrorMessage != string.Empty)
        //        {
        //            _AppDataPath_Tools_CA_Templates_DicDoc = string.Empty;

        //        }

        //        return _AppDataPath_Tools_CA_Templates_DicDoc;
        //    }
        //}

        //private static string _AppDataPath_Tools_CA_Templates_DicDocs = string.Empty;
        //public static string AppDataPath_Tools_CA_Templates_DicDocs
        //{
        //    get
        //    {
        //        _AppDataPath_Tools_CA_Templates_DicDocs = Path.Combine(AppDataPath_Tools_CA_Templates, "DicDocs");

        //        _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_Templates_DicDocs);
        //        if (_ErrorMessage != string.Empty)
        //        {
        //            _AppDataPath_Tools_CA_Templates_DicDocs = string.Empty;

        //        }

        //        return _AppDataPath_Tools_CA_Templates_DicDocs;
        //    }
        //}



        //private static string _AppDataPath_Tools_CA_UseCases = string.Empty;
        //public static string AppDataPath_Tools_CA_Use_Cases
        //{
        //    get
        //    {
        //        _AppDataPath_Tools_CA_UseCases = Path.Combine(CARoot, "UseCases");

        //        _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_UseCases);
        //        if (_ErrorMessage != string.Empty)
        //        {
        //            _AppDataPath_Tools_CA_UseCases = string.Empty;

        //        }

        //        return _AppDataPath_Tools_CA_UseCases;
        //    }
        //}
        
        private static string _AppDataPath_Tools_CA_Projects = string.Empty;
        public static string AppDataPath_Tools_CA_Projects
        {
            get
            {
                _ErrorMessage = string.Empty;

                _AppDataPath_Tools_CA_Projects = Path.Combine(CARoot, "Projects");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_Projects);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_Projects = string.Empty;

                }

                return _AppDataPath_Tools_CA_Projects;
            }
        }

        private static string _AppDataPath_Tools_CA_CurrentProj = string.Empty;
        public static string AppDataPath_Tools_CA_CurrentProj
        {
            get
            {
                _ErrorMessage = string.Empty;

                if (CACurrentProject.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Project Not Defined.";
                    return string.Empty;
                }

                _AppDataPath_Tools_CA_CurrentProj = Path.Combine(AppDataPath_Tools_CA_Projects, CACurrentProject);

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_CurrentProj);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_CurrentProj = string.Empty;

                }

                return _AppDataPath_Tools_CA_CurrentProj;
            }
        }

        private static string _AppDataPath_Tools_CA_CurrentProj_Docs = string.Empty;
        public static string AppDataPath_Tools_CA_CurrentProj_Docs
        {
            get
            {
                _ErrorMessage = string.Empty;

                if (CACurrentProject.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Project Not Defined.";
                    return string.Empty;
                }

                _AppDataPath_Tools_CA_CurrentProj_Docs = Path.Combine(AppDataPath_Tools_CA_CurrentProj, "Docs");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_CurrentProj_Docs);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_CurrentProj_Docs = string.Empty;

                }

                return _AppDataPath_Tools_CA_CurrentProj_Docs;
            }
        }

        private static string _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc = string.Empty;
        public static string AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc
        {
            get
            {
                _ErrorMessage = string.Empty;

                if (CACurrentDocument.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Document has not been Defined.";
                    return string.Empty;
                }

                _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc = Path.Combine(AppDataPath_Tools_CA_CurrentProj_Docs, CACurrentDocument);

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc = string.Empty;

                }

                return _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc;
            }
        }

        private static string _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_XML = string.Empty;
        public static string AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_XML
        {
            get
            {
                _ErrorMessage = string.Empty;

                if (CACurrentDocument.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Document has not been Defined.";
                    return string.Empty;
                }

                _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_XML = Path.Combine(AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc, "XML");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_XML);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_XML = string.Empty;

                }

                return _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_XML;
            }
        }

        private static string _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_Info = string.Empty;
        public static string AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_Info
        {
            get
            {
                _ErrorMessage = string.Empty;

                if (CACurrentDocument.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Document has not been Defined.";
                    return string.Empty;
                }

                _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_Info = Path.Combine(AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc, "Info");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_Info);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_Info = string.Empty;

                }

                return _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_Info;
            }
        }

        private static string _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_Index2 = string.Empty;
        public static string AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_Index2
        {
            get
            {
                _ErrorMessage = string.Empty;

                if (CACurrentDocument.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Document has not been Defined.";
                    return string.Empty;
                }

                _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_Index2 = Path.Combine(AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc, "Index2");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_Index2);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_Index2 = string.Empty;

                }

                return _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_Index2;
            }
        }

        private static string _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_ParsePage = string.Empty;
        public static string AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_ParsePage
        {
            get
            {
                _ErrorMessage = string.Empty;

                if (CACurrentDocument.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Document has not been Defined.";
                    return string.Empty;
                }

                _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_ParsePage = Path.Combine(AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc, "ParsePage");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_ParsePage);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_ParsePage = string.Empty;

                }

                return _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_ParsePage;
            }
        }

        private static string _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_ParseSeg = string.Empty;
        public static string AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_ParseSeg
        {
            get
            {
                _ErrorMessage = string.Empty;

                if (CACurrentDocument.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Document has not been Defined.";
                    return string.Empty;
                }

                _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_ParseSeg = Path.Combine(AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc, "ParseSeg");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_ParseSeg);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_ParseSeg = string.Empty;

                }

                return _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_ParseSeg;
            }
        }

        private static string _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_ConceptParseSeg = string.Empty;
        public static string AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_ConceptParseSeg
        {
            get
            {
                _ErrorMessage = string.Empty;

                if (CACurrentDocument.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Document has not been Defined.";
                    return string.Empty;
                }

                _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_ConceptParseSeg = Path.Combine(AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc, "ConceptParseSeg");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_ConceptParseSeg);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_ConceptParseSeg = string.Empty;

                }

                return _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_ConceptParseSeg;
            }
        }

        private static string _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_Notes = string.Empty;
        public static string AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_Notes
        {
            get
            {
                _ErrorMessage = string.Empty;

                if (CACurrentDocument.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Document has not been Defined.";
                    return string.Empty;
                }

                _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_Notes = Path.Combine(AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc, "Notes");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_Notes);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_Notes = string.Empty;

                }

                return _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_Notes;
            }
        }

        private static string _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_HTML = string.Empty;
        public static string AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_HTML
        {
            get
            {
                _ErrorMessage = string.Empty;

                if (CACurrentDocument.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Document has not been Defined.";
                    return string.Empty;
                }

                _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_HTML = Path.Combine(AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc, "HTML");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_HTML);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_HTML = string.Empty;

                }

                return _AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc_HTML;
            }
        }

        private static string _AppDataPath_Tools_CA_CurrentProj_Doc_CurrentDoc_Reports = string.Empty;
        public static string AppDataPath_Tools_CA_CurrentProj_Doc_CurrentDoc_Reports
        {
            get
            {
                _ErrorMessage = string.Empty;

                if (CACurrentDocument.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Document has not been Defined.";
                    return string.Empty;
                }

                _AppDataPath_Tools_CA_CurrentProj_Doc_CurrentDoc_Reports = Path.Combine(AppDataPath_Tools_CA_CurrentProj_Docs_CurrentDoc, "Reports");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_CurrentProj_Doc_CurrentDoc_Reports);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_CurrentProj_Doc_CurrentDoc_Reports = string.Empty;

                }

                return _AppDataPath_Tools_CA_CurrentProj_Doc_CurrentDoc_Reports;
            }
        }

        private static string _AppDataPath_Tools_CA_CurrentProj_Doc_CurrentDoc_Reports_Concept = string.Empty;
        public static string AppDataPath_Tools_CA_CurrentProj_Doc_CurrentDoc_Reports_Concept
        {
            get
            {
                _ErrorMessage = string.Empty;

                if (CACurrentDocument.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Document has not been Defined.";
                    return string.Empty;
                }

                _AppDataPath_Tools_CA_CurrentProj_Doc_CurrentDoc_Reports_Concept = Path.Combine(AppDataPath_Tools_CA_CurrentProj_Doc_CurrentDoc_Reports, "Concepts");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_CurrentProj_Doc_CurrentDoc_Reports_Concept);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_CurrentProj_Doc_CurrentDoc_Reports_Concept = string.Empty;

                }

                return _AppDataPath_Tools_CA_CurrentProj_Doc_CurrentDoc_Reports_Concept;
            }
        }




        private static string _AppDataPath_Tools_CA_CurrentProj_Analysis = string.Empty;
        public static string AppDataPath_Tools_CA_CurrentProj_Analysis
        {
            get
            {
                _ErrorMessage = string.Empty;

                if (CACurrentProject.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Project Not Defined.";
                    return string.Empty;
                }

                _AppDataPath_Tools_CA_CurrentProj_Analysis = Path.Combine(AppDataPath_Tools_CA_CurrentProj, "Analysis"); 

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_CurrentProj_Analysis);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_CurrentProj_Analysis = string.Empty;

                }

                return _AppDataPath_Tools_CA_CurrentProj_Analysis;
            }
        }

        public static bool AnalysisNameExists(string AnalysisName)
        {
            string analysisPath = Path.Combine(AppDataPath_Tools_CA_CurrentProj_Analysis, AnalysisName);
            if (Directory.Exists(analysisPath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static string _AppDataPath_Tools_CA_CurrentProj_AnalysisName = string.Empty;
        public static string AppDataPath_Tools_CA_CurrentProj_AnalysisName
        {
            get
            {
                _ErrorMessage = string.Empty;

                if (CACurrentProject.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Project Not Defined.";
                    return string.Empty;
                }

                _AppDataPath_Tools_CA_CurrentProj_AnalysisName = Path.Combine(AppDataPath_Tools_CA_CurrentProj_Analysis, CAAnalysisName);

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_CurrentProj_AnalysisName);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_CurrentProj_AnalysisName = string.Empty;

                }

                return _AppDataPath_Tools_CA_CurrentProj_AnalysisName;
            }
        }

        private static string _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Docs = string.Empty;
        public static string AppDataPath_Tools_CA_CurrentProj_AnalysisName_Docs
        {
            get
            {
                _ErrorMessage = string.Empty;

                if (CACurrentProject.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Project Not Defined.";
                    return string.Empty;
                }

                _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Docs = Path.Combine(_AppDataPath_Tools_CA_CurrentProj_AnalysisName, "Docs");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_CurrentProj_AnalysisName_Docs);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Docs = string.Empty;

                }

                return _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Docs;
            }
        }

        private static string _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Reports = string.Empty;
        public static string AppDataPath_Tools_CA_CurrentProj_AnalysisName_Reports
        {
            get
            {
                _ErrorMessage = string.Empty;

                if (CACurrentProject.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Project has been Not Defined.";
                    return string.Empty;
                }

                if (CAAnalysisName.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Analysis has been Not Defined.";
                    return string.Empty;

                }


                _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Reports = Path.Combine(AppDataPath_Tools_CA_CurrentProj_AnalysisName, "Reports");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_CurrentProj_AnalysisName_Reports);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Reports = string.Empty;

                }

                return _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Reports;
            }
        }

        private static string _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Reports_Concepts = string.Empty;
        public static string AppDataPath_Tools_CA_CurrentProj_AnalysisName_Reports_Concepts
        {
            get
            {
                _ErrorMessage = string.Empty;

                if (CACurrentProject.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Project has been Not Defined.";
                    return string.Empty;
                }

                if (CAAnalysisName.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Analysis has been Not Defined.";
                    return string.Empty;

                }


                _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Reports_Concepts = Path.Combine(AppDataPath_Tools_CA_CurrentProj_AnalysisName_Reports, "Concepts");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_CurrentProj_AnalysisName_Reports_Concepts);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Reports_Concepts = string.Empty;

                }

                return _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Reports_Concepts;
            }
        }

        private static string _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Reports_Dics = string.Empty;
        public static string AppDataPath_Tools_CA_CurrentProj_AnalysisName_Reports_Dics
        {
            get
            {
                _ErrorMessage = string.Empty;

                if (CACurrentProject.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Project has been Not Defined.";
                    return string.Empty;
                }

                if (CAAnalysisName.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Analysis has been Not Defined.";
                    return string.Empty;

                }


                _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Reports_Dics = Path.Combine(AppDataPath_Tools_CA_CurrentProj_AnalysisName_Reports, "Dics");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_CurrentProj_AnalysisName_Reports_Dics);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Reports_Dics = string.Empty;

                }

                return _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Reports_Dics;
            }
        }


        private static string _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc = string.Empty;
        public static string AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc
        {
            get
            {
                _ErrorMessage = string.Empty;

                if (CACurrentProject.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Project Not Defined.";
                    return string.Empty;
                }

                if (CACurrentDocument.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Document Not Defined.";
                    return string.Empty;
                }

             //   string s = AppDataPath_Tools_CA_CurrentProj_AnalysisName;

                _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc = Path.Combine(AppDataPath_Tools_CA_CurrentProj_AnalysisName, CACurrentDocument);

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc = string.Empty;

                }

                return _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc;
            }
        }

        private static string _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_Reports = string.Empty;
        public static string AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_Reports
        {
            get
            {
                _ErrorMessage = string.Empty;

                if (CACurrentProject.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Project Not Defined.";
                    return string.Empty;
                }

                _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_Reports = Path.Combine(AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc, "Reports");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_Reports);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_Reports = string.Empty;

                }

                return _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_Reports;
            }
        }

        private static string _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_Reports_Dics = string.Empty;
        public static string AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_Reports_Dics
        {
            get
            {
                _ErrorMessage = string.Empty;

                if (CACurrentProject.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Project Not Defined.";
                    return string.Empty;
                }

                _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_Reports_Dics = Path.Combine(AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_Reports, "Dics");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_Reports_Dics);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_Reports_Dics = string.Empty;

                }

                return _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_Reports_Dics;
            }
        }

        private static string _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_Info = string.Empty;
        public static string AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_Info
        {
            get
            {
                _ErrorMessage = string.Empty;

                if (CACurrentProject.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Project Not Defined.";
                    return string.Empty;
                }

                _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_Info = Path.Combine(AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc, "Infor");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_Info);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_Info = string.Empty;

                }

                return _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_Info;
            }
        }

        private static string _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_AnalysisResults = string.Empty;
        public static string AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_AnalysisResults
        {
            get
            {
                _ErrorMessage = string.Empty;

                if (CACurrentProject.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Project Not Defined.";
                    return string.Empty;
                }

                _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_AnalysisResults = Path.Combine(AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc, "AnalysisResults");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_AnalysisResults);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_AnalysisResults = string.Empty;

                }

                return _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_AnalysisResults;
            }
        }

        private static string _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_AnalysisResults_HTML = string.Empty;
        public static string AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_AnalysisResults_HTML
        {
            get
            {
                _ErrorMessage = string.Empty;

                if (CACurrentProject.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Project Not Defined.";
                    return string.Empty;
                }

                _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_AnalysisResults_HTML = Path.Combine(AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_AnalysisResults, "HTML");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_AnalysisResults_HTML);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_AnalysisResults_HTML = string.Empty;

                }

                return _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_AnalysisResults_HTML;
            }
        }

        private static string _AppDataPath_Tools_CA_CurrentProj_AnalysisName_ParseResults_Doc_Reports = string.Empty;
        public static string AppDataPath_Tools_CA_CurrentProj_AnalysisName_ParseResults_Doc_Reports
        {
            get
            {
                _ErrorMessage = string.Empty;

                if (CACurrentProject.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Project Not Defined.";
                    return string.Empty;
                }

                _AppDataPath_Tools_CA_CurrentProj_AnalysisName_ParseResults_Doc_Reports = Path.Combine(AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc, "Reports");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_CurrentProj_AnalysisName_ParseResults_Doc_Reports);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_CurrentProj_AnalysisName_ParseResults_Doc_Reports = string.Empty;

                }

                return _AppDataPath_Tools_CA_CurrentProj_AnalysisName_ParseResults_Doc_Reports;
            }
        }

        private static string _AppDataPath_Tools_CA_CurrentProj_AnalysisName_ParseResults_Doc_HTML = string.Empty;
        public static string AppDataPath_Tools_CA_CurrentProj_AnalysisName_ParseResults_Doc_HTML
        {
            get
            {
                _ErrorMessage = string.Empty;

                if (CACurrentProject.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Project Not Defined.";
                    return string.Empty;
                }

                _AppDataPath_Tools_CA_CurrentProj_AnalysisName_ParseResults_Doc_HTML = Path.Combine(AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc, "HTML");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_CurrentProj_AnalysisName_ParseResults_Doc_HTML);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_CurrentProj_AnalysisName_ParseResults_Doc_HTML = string.Empty;

                }

                return _AppDataPath_Tools_CA_CurrentProj_AnalysisName_ParseResults_Doc_HTML;
            }
        }

        private static string _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_Notes = string.Empty;
        public static string AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_Notes
        {
            get
            {
                _ErrorMessage = string.Empty;

                if (CACurrentProject.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Project Not Defined.";
                    return string.Empty;
                }

                _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_Notes = Path.Combine(AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc, "Notes");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_Notes);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_Notes = string.Empty;

                }

                return _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_Notes;
            }
        }

        private static string _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_XML = string.Empty;
        public static string AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_XML
        {
            get
            {
                _ErrorMessage = string.Empty;

                if (CACurrentProject.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Project Not Defined.";
                    return string.Empty;
                }

                _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_XML = Path.Combine(AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc, "XML");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_XML);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_XML = string.Empty;

                }

                return _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_XML;
            }
        }

        private static string _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_ConceptParseSeg = string.Empty;
        public static string AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_ConceptParseSeg
        {
            get
            {
                _ErrorMessage = string.Empty;

                if (CACurrentProject.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Project Not Defined.";
                    return string.Empty;
                }

                _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_ConceptParseSeg = Path.Combine(AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc, "ConceptParseSeg");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_ConceptParseSeg);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_ConceptParseSeg = string.Empty;

                }

                return _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_ConceptParseSeg;
            }
        }

        private static string _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_ConceptParseSeg_HTML = string.Empty;
        public static string AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_ConceptParseSeg_HTML
        {
            get
            {
                _ErrorMessage = string.Empty;

                if (CACurrentProject.Trim().Length == 0)
                {
                    _ErrorMessage = "Current Project Not Defined.";
                    return string.Empty;
                }

                _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_ConceptParseSeg_HTML = Path.Combine(AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_ConceptParseSeg, "HTML");

                _ErrorMessage = Directories.DirExistsOrCreate(_AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_ConceptParseSeg_HTML);
                if (_ErrorMessage != string.Empty)
                {
                    _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_ConceptParseSeg_HTML = string.Empty;

                }

                return _AppDataPath_Tools_CA_CurrentProj_AnalysisName_Doc_ConceptParseSeg_HTML;
            }
        }
        


    }
}
