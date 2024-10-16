using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace test
{

        class Files
        {

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

            public static DateTime GetLatestFileDatetime(string dir)
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
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }

                return string.Empty;
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


            //public static void ReadFileIntoControl(string pathFile, System.Windows.Forms.Control ctrl)
            //{
            //    if (!File.Exists(pathFile))
            //        return;

            //    StreamReader streamReader = new StreamReader(pathFile);
            //    string text = streamReader.ReadToEnd();
            //    streamReader.Close();

            //    ctrl.Text = text;
            //}

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

