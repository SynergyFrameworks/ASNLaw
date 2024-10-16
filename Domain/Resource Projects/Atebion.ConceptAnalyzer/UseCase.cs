using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
//using OfficeOpenXml;
using System.Xml;

using Atebion.Common;



namespace Atebion.ConceptAnalyzer
{
    public class UseCase
    {
        public UseCase(string UseCasePath)
        {
            _UseCasePath = UseCasePath;
        }

        private string _ErrorMessage = string.Empty;

        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        private string _UseCasePath = string.Empty;

        private DataSet _ds;

        private GenericDataManger _DataMgr = new GenericDataManger();

        public DataTable CreateEmptyDataTable_Categories()
        {

            DataTable table = new DataTable(AnalysisUCaseFieldConst.TableNameAnalysis);

            table.Columns.Add(AnalysisUCaseFieldConst.UID, typeof(int));
            //     table.Columns.Add(AnalysisUCaseFieldConst.ProjectUID, typeof(int));
            table.Columns.Add(AnalysisUCaseFieldConst.Name, typeof(string));
            table.Columns.Add(AnalysisUCaseFieldConst.Description, typeof(string));
            table.Columns.Add(AnalysisUCaseFieldConst.Use_PDAProjects, typeof(bool));

            table.Columns.Add(AnalysisUCaseFieldConst.DictionaryName, typeof(string));

            table.Columns.Add(AnalysisUCaseFieldConst.ParseType, typeof(string)); // 0 = Legal, 1 = Paragraph
            table.Columns.Add(AnalysisUCaseFieldConst.Use_Dictionary, typeof(bool));
            table.Columns.Add(AnalysisUCaseFieldConst.DicFindWholewords, typeof(bool));
            table.Columns.Add(AnalysisUCaseFieldConst.DicFindSynonyms, typeof(bool));
            table.Columns.Add(AnalysisUCaseFieldConst.GenerateSummaries, typeof(bool));
            table.Columns.Add(AnalysisUCaseFieldConst.FindConcepts, typeof(bool));
            table.Columns.Add(AnalysisUCaseFieldConst.FindEmails, typeof(bool));
            table.Columns.Add(AnalysisUCaseFieldConst.FindDates, typeof(bool));
            table.Columns.Add(AnalysisUCaseFieldConst.FindURLs, typeof(bool));
            table.Columns.Add(AnalysisUCaseFieldConst.CreatedBy, typeof(string));
            table.Columns.Add(AnalysisUCaseFieldConst.DateCreated, typeof(DateTime));

            return table;

        }

        public bool CreateNewUseCase(string UseCaseName, string Description, string parseType, string Dictionary, string DictionariesPath, bool FindWholeWords, bool FindSynonyms, bool GenSummaries, bool FindConcepts, bool FindEmails, bool FindDates, bool FindURLs)
        {
            _ErrorMessage = string.Empty;

            if (UseCaseName.Length == 0)
            {
                _ErrorMessage = "Use Case Name was not defined.";
                return false;
            }


            int uid = 0;
            string usecasePathXMLFile = Path.Combine(_UseCasePath, AnalysisUCaseFieldConst.XMLUseCaseFile);
            if (!File.Exists(usecasePathXMLFile))
            {
                DataTable dt = CreateEmptyDataTable_Categories();
                _ds = new DataSet();
                _ds.Tables.Add(dt);
            }
            else
            {
                _ds = Files.LoadDatasetFromXml(usecasePathXMLFile);
                uid = DataFunctions.GetNewUID(_ds.Tables[0]);
            }

            DataRow rowUseCase = _ds.Tables[0].NewRow();
            rowUseCase[AnalysisUCaseFieldConst.UID] = uid;
            rowUseCase[AnalysisUCaseFieldConst.CreatedBy] = AppFolders_CA.UserName;
            DateTime now = DateTime.Now;
            rowUseCase[AnalysisUCaseFieldConst.DateCreated] = now.ToLongDateString();
            rowUseCase[AnalysisUCaseFieldConst.DicFindSynonyms] = FindSynonyms;
            rowUseCase[AnalysisUCaseFieldConst.DicFindWholewords] = FindWholeWords;
            rowUseCase[AnalysisUCaseFieldConst.DictionaryName] = Dictionary;
            rowUseCase[AnalysisUCaseFieldConst.FindConcepts] = FindConcepts;
            rowUseCase[AnalysisUCaseFieldConst.FindDates] = FindDates;
            rowUseCase[AnalysisUCaseFieldConst.FindEmails] = FindEmails;
            rowUseCase[AnalysisUCaseFieldConst.FindURLs] = FindURLs;
            rowUseCase[AnalysisUCaseFieldConst.GenerateSummaries] = GenSummaries;
            rowUseCase[AnalysisUCaseFieldConst.Name] = UseCaseName;
            rowUseCase[AnalysisUCaseFieldConst.Description] = Description;
            rowUseCase[AnalysisUCaseFieldConst.ParseType] = parseType;

            _ds.Tables[0].Rows.Add(rowUseCase);

            Files.SaveDataXML(_ds, usecasePathXMLFile);

            return true;

        }

        public string[] GetUseCases()
        {
            _ErrorMessage = string.Empty;

            string usecasePathXMLFile = Path.Combine(_UseCasePath, AnalysisUCaseFieldConst.XMLUseCaseFile);
            if (!File.Exists(usecasePathXMLFile))
            {
                _ErrorMessage = string.Concat("Unable to find Use Case catalog: ", usecasePathXMLFile);
                return null;
            }

            List<string> useCases = new List<string>();

            _ds = Files.LoadDatasetFromXml(usecasePathXMLFile);

            foreach (DataRow row in _ds.Tables[0].Rows)
            {
               useCases.Add(row[AnalysisUCaseFieldConst.Name].ToString());
            }

            return useCases.ToArray();

        }


        public DataRow GetUseCase(string UseCaseName)
        {
            _ErrorMessage = string.Empty;

            string usecasePathXMLFile = Path.Combine(_UseCasePath, AnalysisUCaseFieldConst.XMLUseCaseFile);
            if (!File.Exists(usecasePathXMLFile))
            {
                _ErrorMessage = string.Concat("Unable to find Use Case catalog: ", usecasePathXMLFile);
                return null;
            }

            _ds = Files.LoadDatasetFromXml(usecasePathXMLFile);

            foreach (DataRow row in _ds.Tables[0].Rows)
            {
                if (UseCaseName.ToLower() == row[AnalysisUCaseFieldConst.Name].ToString().ToLower())
                {
                    return row;
                }
            }

            return null;

        }

        public bool UseCaseExists(string UseCaseName)
        {
            _ErrorMessage = string.Empty;

            string usecasePathXMLFile = Path.Combine(_UseCasePath, AnalysisUCaseFieldConst.XMLUseCaseFile);
            if (!File.Exists(usecasePathXMLFile))
            {
                _ErrorMessage = string.Concat("Unable to find Use Case catalog: ", usecasePathXMLFile);
                return false;
            }

            _ds = Files.LoadDatasetFromXml(usecasePathXMLFile);

            foreach (DataRow row in _ds.Tables[0].Rows)
            {
                if (UseCaseName.ToLower() == row[AnalysisUCaseFieldConst.Name].ToString().ToLower())
                {
                    return true;
                }
            }

            return false;

        }



        //public DataSet GetDataset()
        //{
        //    _ErrorMessage = string.Empty;

        //    string pathFile = Path.Combine(_UseCasePath, _XML_FILE);


        //    if (!File.Exists(pathFile))
        //    {
        //        _ErrorMessage = string.Concat("Unable to find ", pathFile);
        //        return null;
        //    }

        //    _ds = Files.LoadDatasetFromXml(pathFile);

        //    return _ds;
        //}

        //public bool SaveDataset(DataSet ds)
        //{
        //    _ds = ds;

        //    return SaveDataset();
        //}

        //public bool SaveDataset()
        //{
        //    _ErrorMessage = string.Empty;


        //    string pathFile = Path.Combine(_UseCasePath, _XML_FILE);

        //    try
        //    {
        //        _DataMgr.SaveDataXML(_ds, pathFile);
        //    }
        //    catch (Exception ex)
        //    {
        //        _ErrorMessage = ex.Message;
        //    }

        //    if (_ErrorMessage == string.Empty)
        //        return true;
        //    else
        //        return false;
        //}


    }
}
