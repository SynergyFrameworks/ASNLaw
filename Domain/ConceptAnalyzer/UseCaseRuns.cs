using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
//using OfficeOpenXml;
using System.Xml;

using Domain.Common;


namespace Domain.ConceptAnalyzer
{
    public class UseCaseRuns
    {
        public UseCaseRuns(string UseCasePath)
        {
            _UseCasePath = UseCasePath;
        }

        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        private string _UseCasePath = string.Empty;

     //   private const string _XML_FILE = "UseCaseRuns.xml";

        private DataSet _ds;

        private GenericDataManger _DataMgr = new GenericDataManger();

        public void CreateEmptyDataTable_UseCaseRuns()
        {

            DataTable table = new DataTable(UseCaseRunsFieldConst.TableName);

            table.Columns.Add(UseCaseRunsFieldConst.UID, typeof(int));
         //   table.Columns.Add(UseCaseRunsFieldConst.UseCaseUID, typeof(int));
            table.Columns.Add(UseCaseRunsFieldConst.UseCaseName, typeof(string));

            table.Columns.Add(UseCaseRunsFieldConst.ProjectName, typeof(string));
            table.Columns.Add(UseCaseRunsFieldConst.AnalysisName, typeof(string));

            table.Columns.Add(UseCaseRunsFieldConst.CreatedBy, typeof(string));
            table.Columns.Add(UseCaseRunsFieldConst.DateCreated, typeof(DateTime));    

            if (_ds == null)
                _ds = new System.Data.DataSet();

            _ds.Tables.Add(table);

        }

        public bool AddNewUseCaseRun(string UseCaseName, string ProjectName, string AnalysisName)
        {
            _ErrorMessage = string.Empty;

            int uid = 0;

            string xmlPathFile = Path.Combine(_UseCasePath, UseCaseRunsFieldConst.XMLFile);
            if (!File.Exists(xmlPathFile))
            {
                CreateEmptyDataTable_UseCaseRuns();
            }
            else
            {
                _ds = Files.LoadDatasetFromXml(xmlPathFile);
                uid = DataFunctions.GetNewUID(_ds.Tables[0]);
            }

            if (_ds == null)
            {
                _ErrorMessage = string.Concat("Unable to read Use Case Runs XML File: ", xmlPathFile);
                return false;
            }

            DataRow rowNew = _ds.Tables[0].NewRow();
            rowNew[UseCaseRunsFieldConst.UID] = uid;
            rowNew[UseCaseRunsFieldConst.UseCaseName] = UseCaseName;
            rowNew[UseCaseRunsFieldConst.ProjectName] = ProjectName;
            rowNew[UseCaseRunsFieldConst.AnalysisName] = AnalysisName;
            rowNew[UseCaseRunsFieldConst.CreatedBy] = AppFolders_CA.UserName;
            DateTime now = DateTime.Now;
            rowNew[UseCaseRunsFieldConst.DateCreated] = now.ToLongDateString();

            _ds.Tables[0].Rows.Add(rowNew);

           ////// Files.SaveDataXML(_ds, xmlPathFile);

            return true;

        }

        public DataSet GetDataset()
        {
            _ErrorMessage = string.Empty;

            string pathFile = Path.Combine(_UseCasePath, UseCaseRunsFieldConst.XMLFile);


            if (!File.Exists(pathFile))
            {
                _ErrorMessage = string.Concat("Unable to find ", pathFile);
                return null;
            }

            _ds = Files.LoadDatasetFromXml(pathFile);

            return _ds;
        }

        public bool SaveDataset(DataSet ds)
        {
            _ds = ds;

            return SaveDataset();
        }

        public bool SaveDataset()
        {
            _ErrorMessage = string.Empty;


            string pathFile = Path.Combine(_UseCasePath, UseCaseRunsFieldConst.XMLFile);

            try
            {
                _DataMgr.SaveDataXML(_ds, pathFile);
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.Message;
            }

            if (_ErrorMessage == string.Empty)
                return true;
            else
                return false;
        }


    }
}
