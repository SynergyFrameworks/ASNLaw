using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using Atebion.Common;

namespace AcroParser2
{
    class DefinitionsLibs
    {
        public DefinitionsLibs(string LibsPath)
        {
            _AppDataPathDics = LibsPath;
        }


        private string _AppDataPathDics = string.Empty;
        private string _ErrorMessage = string.Empty;
        private string _PathFile = string.Empty;

        private GenericDataManger _DataMgr = new GenericDataManger();

        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }


        public DataSet CreateEmptyDS()
        {
            DataTable table = new DataTable("DefLib");

            table.Columns.Add(DefLibFieldConst.UID, typeof(int));
            table.Columns.Add(DefLibFieldConst.Acronym, typeof(string));
            table.Columns.Add(DefLibFieldConst.Definition, typeof(string));

            DataSet ds = new System.Data.DataSet();

            ds.Tables.Add(table);

            return ds;
        }

        public DataSet GetDataset(string DefLibName)
        {
            _PathFile = string.Concat(_AppDataPathDics, @"\", DefLibName, ".xml");
            if (!File.Exists(_PathFile))
            {
                _ErrorMessage = string.Concat("Unable to find ", _PathFile);
                return null;
            }

            DataSet ds = Files.LoadDatasetFromXml(_PathFile);

            return ds;
        }


        public bool SaveDataset(DataSet ds, string pathFile)
        {
            _ErrorMessage = string.Empty;

            try
            {
                
               _DataMgr.SaveDataXML(ds, pathFile);
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
