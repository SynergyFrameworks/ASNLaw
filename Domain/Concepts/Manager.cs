using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
//using OfficeOpenXml;
using System.Xml;
using System.Diagnostics;
using Domain.Common;


namespace Domain.Concepts
{
    public class Manager
    {
        public Manager(string ConceptAnalyzerRootPath)
        {
            _ConceptAnalyzerRootPath = ConceptAnalyzerRootPath;

            Validation_Start();

        }

        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        private string _ConceptAnalyzerRootPath = string.Empty;

        private bool Validation_Start()
        {
            _ErrorMessage = string.Empty;

            if (!Directory.Exists(_ConceptAnalyzerRootPath))
            {
                _ErrorMessage = "Concept Analyzer Root Path is not found.";
                return false;
            }

            AppFolders_CA.CARoot = _ConceptAnalyzerRootPath;

            //string s = AppFolders_CA.AppDataPath_Tools_CA_Use_Cases;
            //if (AppFolders_CA.ErrorMessage != string.Empty)
            //{
            //    _ErrorMessage = AppFolders_CA.ErrorMessage;
            //    return false;
            //}

            string s = AppFolders_CA.AppDataPath_Tools_CA_Projects;
            if (AppFolders_CA.ErrorMessage != string.Empty)
            {
                _ErrorMessage = AppFolders_CA.ErrorMessage;
                return false;
            }

            s = AppFolders_CA.AppDataPath_Tools_CA_Templates;
            if (AppFolders_CA.ErrorMessage != string.Empty)
            {
                _ErrorMessage = AppFolders_CA.ErrorMessage;
                return false;
            }

            s = AppFolders_CA.AppDataPath_Tools_CA_Templates_ConceptsDoc;
            if (AppFolders_CA.ErrorMessage != string.Empty)
            {
                _ErrorMessage = AppFolders_CA.ErrorMessage;
                return false;
            }

            s = AppFolders_CA.AppDataPath_Tools_CA_Templates_ConceptsDocs;
            if (AppFolders_CA.ErrorMessage != string.Empty)
            {
                _ErrorMessage = AppFolders_CA.ErrorMessage;
                return false;
            }

            //s = AppFolders_CA.AppDataPath_Tools_CA_Templates_DicDoc;
            //if (AppFolders_CA.ErrorMessage != string.Empty)
            //{
            //    _ErrorMessage = AppFolders_CA.ErrorMessage;
            //    return false;
            //}

            //s = AppFolders_CA.AppDataPath_Tools_CA_Templates_DicDocs;
            //if (AppFolders_CA.ErrorMessage != string.Empty)
            //{
            //    _ErrorMessage = AppFolders_CA.ErrorMessage;
            //    return false;
            //}

            return true;
        }

        //public void FindConcepts()
        //{
        //                        List<string> docConceptsFound = new List<string>();

        //            if (!_DocMgr.Summary_Generate(txtPathFile, DocInfoPath, GenSummaries, FindConcepts, out docConceptsFound))
        //            {

        //}
  


    }
}
