using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using Atebion.Common;

namespace ProfessionalDocAnalyzer
{
    class KeywordsMgr
    {

        public KeywordsMgr(string path, string file)
        {
            _path = path;

            _file = file;

        }

        public KeywordsMgr(string path, bool CreateDefault)
        {
            _path = path;

            if (CreateDefault)
                CreateDefaultXML();
        }

        public KeywordsMgr(string path)
        {
            _path = path;

        }

        #region Private vars

        private string _path = string.Empty;
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        private string _file = string.Empty;

        private const string _DEFAULT_FILE = "Required.xml";

        private GenericDataManger _GenericDataManger;

        private DataSet _ds;


        private const string _Field_Keyword = "Keyword";

        public string Field_Keyword { get { return _Field_Keyword; } }

        #endregion

        #region Public Properies

        private string _errorMsg = string.Empty;

        public string ErrorMsg
        {
            get { return _errorMsg; }
        }


        public string DataFile
        {
            get { return _file; }
            set { _file = value; }
        }

        public DataSet Data
        {
            get { return _ds; }
            set { _ds = value; }
        }

        #endregion

        #region Public Functions
        public string[] GetFiles()
        {

            string msg = Directories.DirExistsOrCreate(_path);
            if (msg != string.Empty)
            {
                this._errorMsg = msg;
                return null;
            }

            string[] files = Directory.GetFiles(_path);

            int filesCount = files.Count();
            if (filesCount == 0) // No Files Found
            {
                if (!CreateDefaultXML())
                    return null;
            }

            int i = 0;
            string fileNameNoExt = string.Empty;
            string[] filesFound = new string[filesCount];

            foreach (string fileName in files)
            {
                fileNameNoExt = Files.GetFileNameWOExt(fileName);
                if (fileNameNoExt != string.Empty)
                {
                    filesFound[i] = fileNameNoExt;
                    i++;
                }
            }
            return filesFound;
        }

        public bool SaveDataXML(DataTable dt)
        {
            string pathFile = string.Concat(_path, @"\", this.DataFile);
            if (File.Exists(pathFile))
            {
                if (Files.FileIsLocked(pathFile))
                {
                    _errorMsg = string.Concat("File: ", this.DataFile, " is currently being used. Please try again in a moment.");
                }
            }

            try
            {
                dt.WriteXml(pathFile, XmlWriteMode.WriteSchema);
                return true;
            }
            catch (Exception ex)
            {
                _errorMsg = string.Concat("Error: ", ex.Message);
                return false;
            }

        }

        public bool ValueExists(DataTable dt, string primaryField)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (primaryField.ToUpper() == row[0].ToString().ToUpper())
                {
                    return true;
                }
            }

            return false;
        }

        public DataSet CreateEmptyDS()
        {
            DataSet ds = new System.Data.DataSet();

            DataTable dt = ds.Tables.Add("Data");
            dt.Columns.Add(_Field_Keyword, typeof(string));

            _ds = ds;
            return ds;
        }

        public bool LoadData()
        {
            if (_path == string.Empty)
            {
                _errorMsg = "No path has been defined.";
                return false;
            }

            if (_file == string.Empty)
            {
                CreateEmptyDS();
                return true;
            }

            string pathFile = string.Empty;

            try
            {
                pathFile = string.Concat(_path, @"\", _file);
                if (!File.Exists(pathFile))
                {
                    pathFile = string.Concat(_path, _file);
                    if (!File.Exists(pathFile))
                    {
                        _errorMsg = string.Concat("Unable to find Keywords file: ", _file);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                _errorMsg = ex.Message;
                return false;
            }

            try
            {

                _GenericDataManger = new GenericDataManger();

                _ds = _GenericDataManger.LoadDatasetFromXml(pathFile);
            }
            catch (Exception ex2)
            {
                _errorMsg = ex2.Message;
                return false;
            }

            if (_ds == null)
            {
                _errorMsg = _GenericDataManger.ErrorMessage;
                return false;
            }


            return true;
        }


        public bool ValueExistsOtherThanIndex(DataTable dt, string Value, int index) // Use for Updates
        {

            int i = 0;

            foreach (DataRow row in dt.Rows)
            {
                if (Value.ToUpper() == row[0].ToString().ToUpper())
                {
                    if (i != index)
                        return true;
                }

                i++;
            }

            return false;
        }
        #endregion

        #region Private Functions

        public bool CreateDefaultXML()
        {
            string path = string.Empty;

            if (_path == string.Empty)
                return false;

            if (_path.LastIndexOf(@"\") != (_path.Length - 1))
                path += string.Concat(_path, @"\");

            if (_file == string.Empty)
                _file = _DEFAULT_FILE;

            string pathFile = string.Concat(path, _file);

            if (File.Exists(pathFile))
                File.Delete(pathFile); // Should never occur

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                if (!Directory.Exists(path))
                    return false;
            }

            // Write the string to a file.
            System.IO.StreamWriter file = new System.IO.StreamWriter(pathFile);

            string lines = "<Data>";
            file.WriteLine(lines);
            lines = "  <xs:schema id=\"Data\" xmlns=\"\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">";
            file.WriteLine(lines);
            lines = "    <xs:element name=\"Data\" msdata:IsDataSet=\"true\" msdata:NameCurrentLocale=\"true\">";
            file.WriteLine(lines);
            lines = "      <xs:complexType>";
            file.WriteLine(lines);
            lines = "        <xs:choice minOccurs=\"0\" maxOccurs=\"unbounded\">";
            file.WriteLine(lines);
            lines = "          <xs:element name=\"LinkRecord\">";
            file.WriteLine(lines);
            lines = "            <xs:complexType>";
            file.WriteLine(lines);
            lines = "              <xs:sequence>";
            file.WriteLine(lines);
            lines = string.Concat("                <xs:element name=\"", _Field_Keyword, "\" type=\"xs:string\" />");
            file.WriteLine(lines);
            lines = "              </xs:sequence>";
            file.WriteLine(lines);
            lines = "            </xs:complexType>";
            file.WriteLine(lines);
            lines = "          </xs:element>";
            file.WriteLine(lines);
            lines = "        </xs:choice>";
            file.WriteLine(lines);
            lines = "      </xs:complexType>";
            file.WriteLine(lines);
            lines = "    </xs:element>";
            file.WriteLine(lines);
            lines = "  </xs:schema>";
            file.WriteLine(lines);

            lines = "  <LinkRecord>";
            file.WriteLine(lines);
            lines = string.Concat("    <", _Field_Keyword, ">must</", _Field_Keyword, ">");
            file.WriteLine(lines);
            lines = "  </LinkRecord>";
            file.WriteLine(lines);

            lines = "  <LinkRecord>";
            file.WriteLine(lines);
            lines = string.Concat("    <", _Field_Keyword, ">shall</", _Field_Keyword, ">");
            file.WriteLine(lines);
            lines = "  </LinkRecord>";
            file.WriteLine(lines);

            lines = "  <LinkRecord>";
            file.WriteLine(lines);
            lines = string.Concat("    <", _Field_Keyword, ">will</", _Field_Keyword, ">");
            file.WriteLine(lines);
            lines = "  </LinkRecord>";
            file.WriteLine(lines);

            lines = "  <LinkRecord>";
            file.WriteLine(lines);
            lines = string.Concat("    <", _Field_Keyword, ">should</", _Field_Keyword, ">");
            file.WriteLine(lines);
            lines = "  </LinkRecord>";
            file.WriteLine(lines);

            lines = "  <LinkRecord>";
            file.WriteLine(lines);
            lines = string.Concat("    <", _Field_Keyword, ">require</", _Field_Keyword, ">");
            file.WriteLine(lines);
            lines = "  </LinkRecord>";
            file.WriteLine(lines);

            lines = "  <LinkRecord>";
            file.WriteLine(lines);
            lines = string.Concat("    <", _Field_Keyword, ">required</", _Field_Keyword, ">");
            file.WriteLine(lines);
            lines = "  </LinkRecord>";
            file.WriteLine(lines);

            lines = "  <LinkRecord>";
            file.WriteLine(lines);
            lines = string.Concat("    <", _Field_Keyword, ">would</", _Field_Keyword, ">");
            file.WriteLine(lines);
            lines = "  </LinkRecord>";
            file.WriteLine(lines);

            lines = "  <LinkRecord>";
            file.WriteLine(lines);
            lines = string.Concat("    <", _Field_Keyword, ">ought</", _Field_Keyword, ">");
            file.WriteLine(lines);
            lines = "  </LinkRecord>";
            file.WriteLine(lines);

            lines = "  <LinkRecord>";
            file.WriteLine(lines);
            lines = string.Concat("    <", _Field_Keyword, ">necessity</", _Field_Keyword, ">");
            file.WriteLine(lines);
            lines = "  </LinkRecord>";
            file.WriteLine(lines);

            lines = "  <LinkRecord>";
            file.WriteLine(lines);
            lines = string.Concat("    <", _Field_Keyword, ">obligated</", _Field_Keyword, ">");
            file.WriteLine(lines);
            lines = "  </LinkRecord>";
            file.WriteLine(lines);

            lines = "  <LinkRecord>";
            file.WriteLine(lines);
            lines = string.Concat("    <", _Field_Keyword, ">obligation</", _Field_Keyword, ">");
            file.WriteLine(lines);
            lines = "  </LinkRecord>";
            file.WriteLine(lines);

            lines = "  <LinkRecord>";
            file.WriteLine(lines);
            lines = string.Concat("    <", _Field_Keyword, ">duty</", _Field_Keyword, ">");
            file.WriteLine(lines);
            lines = "  </LinkRecord>";
            file.WriteLine(lines);

            lines = "  <LinkRecord>";
            file.WriteLine(lines);
            lines = string.Concat("    <", _Field_Keyword, ">vital</", _Field_Keyword, ">");
            file.WriteLine(lines);
            lines = "  </LinkRecord>";
            file.WriteLine(lines);

            lines = "  <LinkRecord>";
            file.WriteLine(lines);
            lines = string.Concat("    <", _Field_Keyword, ">critical</", _Field_Keyword, ">");
            file.WriteLine(lines);
            lines = "  </LinkRecord>";
            file.WriteLine(lines);

            lines = "  <LinkRecord>";
            file.WriteLine(lines);
            lines = string.Concat("    <", _Field_Keyword, ">needed</", _Field_Keyword, ">");
            file.WriteLine(lines);
            lines = "  </LinkRecord>";
            file.WriteLine(lines);

            lines = "  <LinkRecord>";
            file.WriteLine(lines);
            lines = string.Concat("    <", _Field_Keyword, ">needed</", _Field_Keyword, ">");
            file.WriteLine(lines);
            lines = "  </LinkRecord>";
            file.WriteLine(lines);

            lines = "  <LinkRecord>";
            file.WriteLine(lines);
            lines = string.Concat("    <", _Field_Keyword, ">should</", _Field_Keyword, ">");
            file.WriteLine(lines);
            lines = "  </LinkRecord>";
            file.WriteLine(lines);

            lines = "</Data>";
            file.WriteLine(lines);

            file.Close();

            return true;

        }
        #endregion
    }

}
