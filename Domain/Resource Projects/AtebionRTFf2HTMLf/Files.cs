using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AtebionRTFf2HTMLf
{
    static class Files
    {
        private static string _ErrorMessage = string.Empty;

        public static void ClearFolder(string FolderName)
        {
            DirectoryInfo dir = new DirectoryInfo(FolderName);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                ClearFolder(di.FullName);
                di.Delete();
            }
        }

        public static string GetFileNameWOExt(string fileName)
        {
            string xFileName = fileName;

            if (fileName == string.Empty)
                return fileName;

            int x = fileName.IndexOf(':'); // Path is included
            if (x > -1)
            {
                xFileName = GetFileName(fileName);
            }

            // Path Not included or Removed
            if (xFileName == string.Empty)
                return string.Empty;

            x = xFileName.IndexOf('.');
            if (x == -1)
                return xFileName;

            return xFileName.Substring(0, x);
        }

        public static string GetFileName(string pathFile)
        {
            string ext = string.Empty;
            return GetFileName(pathFile, out ext);
            //FileInfo objFile = new FileInfo(pathFile);
            //return objFile.Name;
        }

        public static string GetFileName(string pathFile, out string ext)
        {
            FileInfo objFile = new FileInfo(pathFile);
            ext = objFile.Extension;
            ext = ext.Replace(".", string.Empty);
            return objFile.Name;
        }

        public static string ReadFile(string pathFile)
        {
            if (!File.Exists(pathFile))
                return string.Empty;

            try
            {
                using (StreamReader sr = new StreamReader(pathFile))
                {
                    string line = sr.ReadToEnd();
                    return line;
                }
            }
            catch (Exception e)
            {  
                _ErrorMessage = e.Message;
            }

            return string.Empty;
        }

        public static bool WriteStringToFile(string lines, string pathFile, bool DeleteIfAlreadyExists)
        {
            if (File.Exists(pathFile))
                if (DeleteIfAlreadyExists)
                    File.Delete(pathFile);
                else
                    return true;

            System.IO.StreamWriter file = new System.IO.StreamWriter(pathFile);
            file.WriteLine(lines);

            file.Close();

            return true;
        }
    }
}
