using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace WorkgroupMgr
{
    public class MatrixSettings
    {
        public MatrixSettings(string matrixTempPathTemplates, string matrixName)
        {
            _matrixTempPathTemplates = matrixTempPathTemplates;
            _matrixName = matrixName;
        }

        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        private string _matrixTempPathTemplates = string.Empty;
        private string _matrixName = string.Empty;

        public DataSet GetSettings()
        {
            _ErrorMessage = string.Empty;

            string dataPathFile = Path.Combine(_matrixTempPathTemplates, _matrixName, "MatrixTempSettings.xml");
            if (!File.Exists(dataPathFile))
            {
                _ErrorMessage = string.Concat("Unable to locate Matrix Template Data Settings file: ", dataPathFile);
                return null;
            }

            DataSet ds = DataFunctions.LoadDatasetFromXml(dataPathFile);
            if (ds == null)
            {
                _ErrorMessage = DataFunctions._ErrorMessage;
                return null;
            }

            return ds;
        }
    }
}
