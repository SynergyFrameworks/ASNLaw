using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace Domain.Common
{
    public class FileInformation
    {

        public FileInformation(string pathFile)
        {
            GetInformation(pathFile);

        }

        public FileInformation()
        {

        }

        private string _FileName = string.Empty;
        public string FileName { get { return _FileName; } }

        private string _CreationTime = string.Empty;
        public string CreationTime { get { return _CreationTime; } }

        private string _LastAccessTime = string.Empty;
        public string LastAccessTime { get { return _LastAccessTime; } }

        private string _LastWriteTime = string.Empty;
        public string LastWriteTime { get { return _LastWriteTime; } }

        private string _FileSize = string.Empty;
        public string FileSize { get { return _FileSize; } }

        private string _Extension = string.Empty;
        public string Extension { get { return _Extension; } }

        private string _CreationDate = string.Empty;
        public string CreationDate { get { return _CreationDate; } }


        public bool GetInformation(string pathFile)
        {
            ClearProperies();

            if (File.Exists(pathFile))
            {
                FileInfo TheFile = new FileInfo(pathFile);

                PopulateProperies(TheFile);
                return true;

            }
            else
                return false;
        }

        private void ClearProperies()
        {
            _FileName = string.Empty;
            _CreationTime = string.Empty;
            _LastAccessTime = string.Empty;
            _LastWriteTime = string.Empty;
            _FileSize = string.Empty;
            _Extension = string.Empty;
            _CreationDate = string.Empty;
        }

        protected void PopulateProperies(FileInfo TheFile)
        {
            _FileName = TheFile.Name;
            _CreationDate = TheFile.CreationTime.ToLongDateString();
            _CreationTime = TheFile.CreationTime.ToLongTimeString();
            _LastAccessTime = TheFile.LastAccessTime.ToLongDateString();
            _LastWriteTime = TheFile.LastWriteTime.ToLongDateString();
            _FileSize = TheFile.Length.ToString(); // In Bytes
            _Extension = TheFile.Extension;

        }
    }
}
