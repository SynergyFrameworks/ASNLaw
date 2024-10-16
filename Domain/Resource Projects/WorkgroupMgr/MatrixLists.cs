﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;


namespace WorkgroupMgr
{
    public class MatrixLists
    {
        public MatrixLists(string matrixPath, string ListPath)
        {
            _MatrixPath = matrixPath;
            _ListPath = ListPath;

            ValidateFix();
        }

        private const string MATRIX_TEMPLATE_SETTINGS_XML = "MatrixTempSettings.xml"; // Holds the Matrix's Configuration Settings

        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        private string _MatrixPath = string.Empty;
        public string MatrixPath
        {
            get { return _MatrixPath; }
        }

        private string _ListPath = string.Empty;
        public string ListPath
        {
            get { return _ListPath; }
        }

        private DataTable _dtList;
        public DataTable ListDataTable
        {
            get { return _dtList; }
        }

        private ListMgr _ListMgr;


        public bool ValidateFix()
        {
            _ErrorMessage = string.Empty;

            if (!Directory.Exists(_MatrixPath))
            {
                _ErrorMessage = string.Concat("Unable to locate the Matrix's Data Folder: ", _MatrixPath);
                return false;
            }

            string pathBackup = Path.Combine(_MatrixPath, "Backup");
            if (!CheckCreateDir(pathBackup))
            {
                return false;
            }

            string pathFile = Path.Combine(_MatrixPath, MATRIX_TEMPLATE_SETTINGS_XML);
            if (!File.Exists(pathFile))
            {
                _ErrorMessage = string.Concat("Unable to find file '", MATRIX_TEMPLATE_SETTINGS_XML, "'.", Environment.NewLine, Environment.NewLine, "This file holds the Matrix's Template Configuration Settings.");
            }

           // pathFile = Path.Combine(_MatrixPath, MATRIX_TEMPLATE_XLSX);
            //if (!File.Exists(pathFile))
            //{
            //    _ErrorMessage = string.Concat("Unable to find file '", MATRIX_TEMPLATE_XLSX, "'.", Environment.NewLine, Environment.NewLine, "This file is the Excel Matrix Template file.");
            //}

            //string pathFile2 = Path.Combine(_MatrixPath, MATRIX_TEMPLATE_DATA_XLSX);
            //if (!File.Exists(pathFile2))
            //{
            //    try
            //    {
            //        File.Copy(pathFile, pathFile2);
            //    }
            //    catch (Exception ex)
            //    {
            //        _ErrorMessage = string.Concat("Unable to recover file '", MATRIX_TEMPLATE_DATA_XLSX, "'.", Environment.NewLine, Environment.NewLine, ex.Message);
            //        return false;
            //    }

            //}

            DataSet ds = GetSettings();
            if (ds == null)
            {
                return false;
            }

            return true;
        }

        public DataSet GetSettings()
        {
            _ErrorMessage = string.Empty;

            string dataPathFile = Path.Combine(_MatrixPath, MATRIX_TEMPLATE_SETTINGS_XML);
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

            if (ds != null)
                _dtList = ds.Tables["Lists"];
 
            return ds;
        }

        public string[] GetListItems(string ListName)
        {
            _ErrorMessage = string.Empty;

            if (_ListMgr == null)
            {
                _ListMgr = new ListMgr(_ListPath);
            }

            string[] items = _ListMgr.GetListItems(ListName);

            if (items == null)
            {
                _ErrorMessage = _ListMgr.ErrorMessage;
                return null;
            }

            return items;

        }

        /// <summary>
        /// Checks if folder exists, otherwise attempts to create folder
        /// </summary>
        /// <param name="dir">Folder</param>
        /// <returns>True if folder exists or was created. False if not able to create folder</returns>
        public bool CheckCreateDir(string dir)
        {
            _ErrorMessage = string.Empty;

            try
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.Message;
                return false;
            }

            return true;
        }
    }
}
