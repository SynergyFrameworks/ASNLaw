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
    public class UseCaseDictionaries
    {

        public UseCaseDictionaries(string UseCaseRootPath)
        {
            _UseCaseRootPath = UseCaseRootPath;
        }

        private string _ErrorMessage = string.Empty;

        private string _UseCaseRootPath = string.Empty;

        private const string _XML_FILE = "UseCaseDictionaries.xml";

        private DataSet _ds;

        private GenericDataManger _DataMgr = new GenericDataManger();

        public void CreateEmptyDataTable_Categories()
        {

            //DataTable table = new DataTable(DictionariesFieldConst.TableNameUseCase);

            //table.Columns.Add(DictionariesFieldConst.UID, typeof(int));
            //table.Columns.Add(DictionariesFieldConst.UseCaseUID, typeof(int));
            //table.Columns.Add(DictionariesFieldConst.DictionaryUID, typeof(int));
            //table.Columns.Add(DictionariesFieldConst.DictionaryName, typeof(string)); 


            //if (_ds == null)
            //    _ds = new System.Data.DataSet();

            //_ds.Tables.Add(table);

        }

        public DataSet GetDataset()
        {
            _ErrorMessage = string.Empty;

            string pathFile = Path.Combine(_UseCaseRootPath, _XML_FILE);


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


            string pathFile = Path.Combine(_UseCaseRootPath, _XML_FILE);

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
