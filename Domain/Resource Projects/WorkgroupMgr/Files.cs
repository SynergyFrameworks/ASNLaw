using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;


namespace WorkgroupMgr
{
    public static class Files
    {
        public static string ErrorMessage = string.Empty;

        public static bool CopyFile2NewLocation(string sourceFolder, string destinationFolder, string fileName, string subject)
        {
            ErrorMessage = string.Empty;

            string sourcePathFile = Path.Combine(sourceFolder, fileName);
            string destinationPathFile = Path.Combine(destinationFolder, fileName);

            if (!File.Exists(sourcePathFile))
            {
                ErrorMessage = string.Concat("Unable to find ", subject, " file: ", sourcePathFile);
                return false;
            }

            File.Copy(sourcePathFile, destinationPathFile, true);

            if (!File.Exists(destinationPathFile))
            {
                ErrorMessage = string.Concat("Unable to copy ", subject, " file to ", destinationFolder);
                return false;
            }

            return true;

        }

        public static bool BackupFile(string sourceFolder, string backupFolder, string file, out string backupPathfile)
        {
            ErrorMessage = string.Empty;

            string prefix = "0_";

            string backupFile = string.Concat(prefix, file);
            backupPathfile = Path.Combine(backupFolder, backupFile);
            if (File.Exists(backupPathfile))
            {
                for (int i = 1; i < 101; i++)
                {
                    prefix = string.Concat(i.ToString(), "_");
                    backupFile = string.Concat(prefix, file);
                    backupPathfile = Path.Combine(backupFolder, backupFile);
                    if (!File.Exists(backupPathfile))
                    {
                        break;
                    }
                    
                }
            }

            string sourcePathFile = Path.Combine(sourceFolder, file);

            try
            {
                File.Copy(sourcePathFile, backupPathfile);
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Concat("Notice: Unable to Backup file: ", file, Environment.NewLine, Environment.NewLine, ex.Message);
                return false;
            }
        }

        public static string GetLastBackupFile(string backupFolder, string file)
        {
            string prefix = "0_";

            string previousBackupPathFile = string.Empty;

            string backupFile = string.Concat(prefix, file);
            string backupPathfile = Path.Combine(backupFolder, backupFile);
            previousBackupPathFile = backupPathfile;
            if (File.Exists(backupPathfile))
            {
                for (int i = 1; i < 101; i++)
                {
                    prefix = string.Concat("_", i.ToString());
                    backupFile = string.Concat(prefix, backupFile);
                    backupPathfile = Path.Combine(backupFolder, backupFile);
                    if (!File.Exists(backupPathfile))
                    {
                        return previousBackupPathFile;
                    }

                    previousBackupPathFile = backupPathfile;

                }
            }
            else
            {
                return string.Empty;
            }

            return previousBackupPathFile;
           
        }

        public static bool RestoreLastBackup(string backupFolder, string sourceFolder, string file)
        {
            //string prefix = "~";

            //string foundBackupFile = string.Empty;

            //string backupFile = string.Concat(prefix, file);
            //string backupPathfile = Path.Combine(backupFolder, backupFile);
            //if (File.Exists(backupPathfile))
            //{
            //    foundBackupFile = backupPathfile;
            //    for (int i = 0; i < 101; i++)
            //    {
            //        prefix += "~";
            //        backupPathfile = Path.Combine(backupFolder, backupFile);
            //        if (!File.Exists(backupPathfile))
            //        {
            //            break;
            //        }
            //        foundBackupFile = backupPathfile;
            //    }
            //}
            //else
            //{
            //    return false;
            //}

            string foundBackupFile = GetLastBackupFile(backupFolder, file);
            if (foundBackupFile == string.Empty)
                return false;

            string sourcePathFile = Path.Combine(sourceFolder, file);

            if (!File.Exists(sourcePathFile))
            {
                File.Copy(foundBackupFile, sourcePathFile); // Restore the last backup
                if (File.Exists(sourcePathFile))
                {
                    return true;
                }
            }

            return false;
        }

        public static void UnlockAllFiles(string path)
        {
            string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            foreach (string file in files)
            {


                //if (System.IO.File.GetAttributes(file) == FileAttributes.ReadOnly)
                //{
                System.IO.File.SetAttributes(file, FileAttributes.Normal);
                //}
            }

        }

        /// <summary>
        /// Checks if a file is locked.
        /// </summary>
        /// <param name="filePath">Path of the file</param>
        /// <returns>Returns true if file is in use; else returns false</returns>
        public static bool FileIsLocked(string filePath)
        {
            bool isfileLocked = false;
            System.IO.FileStream fileStream = null;
            try
            {
                fileStream = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            }
            catch (System.IO.IOException)
            {
                isfileLocked = true;
            }
            finally
            {
                if (null != fileStream)
                {
                    fileStream.Close();
                }
            }

            return isfileLocked;
        }


        public static void ReplaceTextInFile(string originalFile, string outputFile, string[] searchTerm, string[] replaceTerm) // Added 04.11.2013
        {
            string tempLineValue;
            using (FileStream inputStream = File.OpenRead(originalFile))
            {
                using (StreamReader inputReader = new StreamReader(inputStream))
                {
                    using (StreamWriter outputWriter = File.AppendText(outputFile))
                    {
                        while (null != (tempLineValue = inputReader.ReadLine()))
                        {
                            for (int i = 0; i < searchTerm.Length; i++)
                            {
                                tempLineValue = tempLineValue.Replace(searchTerm[i], replaceTerm[i]);
                            }

                            outputWriter.WriteLine(tempLineValue);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Finds Qty of Files
        /// </summary>
        /// <param name="dir">Path</param>
        /// <param name="FileType">File Type Example: "*.docx"</param>
        /// <returns>Return the total count of files per File Type</returns>
        public static int GetFilesCount(string dir, string FileType)
        {
            if (dir == string.Empty)
                return 0;

            string[] files = Directory.GetFiles(dir, FileType);

            return files.Length;
        }

        public static string GetLatestFile(string dir)
        {
            string[] files = System.IO.Directory.GetFiles(dir);
            System.IO.FileInfo finf;
            DateTime lastDate = new DateTime();
            string lastFile = string.Empty;
            foreach (string f in files)
            {
                finf = new System.IO.FileInfo(f);
                if (finf.CreationTime > lastDate)
                {
                    lastDate = finf.CreationTime;
                    lastFile = f;
                }
            }

            return lastFile;
        }

        //public static FileInfo GetNewestFile(DirectoryInfo directory)
        //{
        //    return directory.GetFiles()
        //        .Union(directory.GetDirectories().Select(d => GetNewestFile(d)))
        //        .OrderByDescending(f => (f == null ? DateTime.MinValue : f.LastWriteTime))
        //        .FirstOrDefault();
        //}

        public static DateTime GetLatestFileDatetime(string dir)
        {
            string[] files = System.IO.Directory.GetFiles(dir, "*.rtf", SearchOption.TopDirectoryOnly);
            System.IO.FileInfo finf;
            DateTime lastDate = new DateTime();
            string lastFile = string.Empty;
            int i = 0;
            foreach (string f in files)
            {
                finf = new System.IO.FileInfo(f);
                if (i == 0)
                {
                    lastDate = finf.LastWriteTime;
                }
                else if (finf.CreationTime > lastDate)
                {
                    lastDate = finf.LastWriteTime;
                    lastFile = f;
                }

                i++;
            }

            return lastDate;
        }



        /// <summary>
        /// Return Files without path
        /// </summary>
        /// <param name="dir">Path</param>
        /// <param name="FileType">File Type Example: "*.docx"</param>
        /// <returns>String Array of File Name w/ Ext.</returns>
        public static string[] GetFilesFromDir(string dir, string FileType)
        {
            string[] files = Directory.GetFiles(dir, FileType);
            string file = string.Empty;
            string[] fileNames = new string[files.Length];
            int i = 0;
            foreach (string pathFile in files)
            {
                file = GetFileName(pathFile);
                fileNames[i] = file;
                i++;
            }

            return fileNames;
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

        public static string SetFileName2Obsolete(string OldFile)
        {
            string path = Path.GetDirectoryName(OldFile);

            string file = GetFileName(OldFile);
            string terminator = "~";

            string newFileName = string.Empty;

            bool notDone = false;

            if (File.Exists(OldFile))
            {
                do
                {
                    newFileName = string.Concat(path, @"\", terminator, file);
                    if (!File.Exists(newFileName))
                    {
                        try
                        {
                            File.Move(OldFile, newFileName);
                            return newFileName;
                        }
                        catch (IOException ex)
                        {
                            return ex.Message;
                        }
                    }
                    terminator = string.Concat("~", terminator);

                } while (notDone);
            }


            return string.Empty;
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
            if (!File.Exists(pathFile)) // Added 10/24/2015
            {
                ext = string.Empty;
                return string.Empty;
            }

            FileInfo objFile = new FileInfo(pathFile);
            ext = objFile.Extension;
            ext = ext.Replace(".", string.Empty);
            return objFile.Name;
        }

        public static string[] ReadFile2Array(string pathFile) // Added 01.01.2015
        {
            List<string> fileContent = new List<string>();

            TextReader tr = new StreamReader(pathFile);

            string currentLine = string.Empty;

            while ((currentLine = tr.ReadLine()) != null)
            {
                fileContent.Add(currentLine);
            }

            tr.Close();

            string[] lines = fileContent.ToArray();

            return lines;
        }

        public static List<string> ReadFile2List(string pathFile) 
        {
            List<string> fileContent = new List<string>();

            TextReader tr = new StreamReader(pathFile);

            string currentLine = string.Empty;

            while ((currentLine = tr.ReadLine()) != null)
            {
                fileContent.Add(currentLine);
            }

            tr.Close();

            return fileContent;
        }


        public static string ReadFile(string pathFile)
        {
            ErrorMessage = string.Empty;

            if (!File.Exists(pathFile))
            {
                ErrorMessage = string.Concat("Unable to find file: ", pathFile);
                return string.Empty; // ToDo Write to _ErrorMessage
            }

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
                ErrorMessage = string.Concat("Unable to read file: ", pathFile, Environment.NewLine, Environment.NewLine, e.Message);               
            }

            return string.Empty;
        }

        public static string GetLastFolder(string path)
        {
            string lastFolder = path.Split(Path.DirectorySeparatorChar).Last();

            return lastFolder;
        }

        //public static void ReadFileIntoControl(string pathFile, System.Windows.Forms.Control ctrl)
        //{
        //    if (!File.Exists(pathFile))
        //        return;

        //    StreamReader streamReader = new StreamReader(pathFile);
        //    string text = streamReader.ReadToEnd();
        //    streamReader.Close();

        //    ctrl.Text = text;
        //}

        public static bool WriteStringToFile(List<string> lst, string pathFile)
        {

            System.IO.StreamWriter file = new System.IO.StreamWriter(pathFile);
            foreach (string line in lst)
            {
                file.WriteLine(line);
            }

            file.Close();

            return true;
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

        public static bool WriteStringToFile(string lines, string pathFile)
        {
            if (File.Exists(pathFile))
            {
                SetFileName2Obsolete(pathFile);
            }


            System.IO.StreamWriter file = new System.IO.StreamWriter(pathFile);
            file.WriteLine(lines);

            file.Close();

            return true;
        }

        public static string CleanFileName(string fileName)
        {
            return Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty));
        }

        public static bool IsValidFilename(string testName)
        {
            Regex containsABadCharacter = new Regex("[" + Regex.Escape(System.IO.Path.GetInvalidPathChars().ToString()) + "]");
            if (containsABadCharacter.IsMatch(testName))
            { return false; };

            // other checks for UNC, drive-path format, etc

            return true;
        }

        public static string RemoveInvalidChar(string xString)
        {
            // Check for invalid chars

            string xyz = xString;

            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            foreach (char c in invalid)
            {
                xyz = xyz.Replace(c.ToString(), string.Empty);
            }

            return xyz;
        }

        public static string GetDir(string pathFile)
        {
            return Path.GetDirectoryName(pathFile);
        }
    }
}
