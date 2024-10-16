using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Runtime;
using System.Runtime.InteropServices;


namespace Atebion.Common
{
    public static class Directories
    {
        //public static bool DirHasInvalidChars(string path)
        //{
        //    string adjPath = path;

        //    if (adjPath.Substring(adjPath.Length - 1) != @"\")
        //        adjPath = string.Concat(adjPath, @"\");

        //    bool ret = false;
        //    if (!string.IsNullOrEmpty(path))
        //    {
        //        try
        //        {
        //            // Careful!
        //            //    Path.GetDirectoryName("C:\Directory\SubDirectory")
        //            //    returns "C:\Directory", which may not be what you want in
        //            //    this case. You may need to explicitly add a trailing \
        //            //    if path is a directory and not a file path. As written, 
        //            //    this function just assumes path is a file path.
        //            string fileName = System.IO.Path.GetFileName(path);
        //            string fileDirectory = System.IO.Path.GetDirectoryName(path);

        //            // we don't need to do anything else,
        //            // if we got here without throwing an 
        //            // exception, then the path does not
        //            // contain invalid characters
        //        }
        //        catch (ArgumentException)
        //        {
        //            // Path functions will throw this 
        //            // if path contains invalid chars
        //            ret = true;
        //        }
        //    }
        //    return ret;
        //}

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);

        public static string ErrorMessage = string.Empty;

        public static bool IsInternetAvailable()
        {
            int description;
            return InternetGetConnectedState(out description, 0);
        }

        public static string GetLastFolder(string path)
        {
            string lastFolder = path.Split(Path.DirectorySeparatorChar).Last();

            return lastFolder;
        }

        public static string GetLastFolder(string path, out string remainderPath)
        {
            string lastFolder = path.Split(Path.DirectorySeparatorChar).Last();

            remainderPath = path.Replace(lastFolder, "");

            return lastFolder;
        }

        public static string SetFolder2Obsolete(string path)
        {
            ErrorMessage = string.Empty;

            string remainderPath = string.Empty;

            string lastFolder = GetLastFolder(path, out remainderPath);

            string prifix = "~";

            string newFolder = string.Concat(prifix, lastFolder);
            string newPath = Path.Combine(remainderPath, newFolder);

            while (Directory.Exists(newPath))
            {
                newFolder = string.Concat(prifix, lastFolder);
                newPath = Path.Combine(remainderPath, newFolder);

                if (Directory.Exists(newPath))
                {
                    prifix = "~" + prifix;
                    newFolder = string.Concat(prifix, lastFolder);
                }
                else
                {
                    break;
                }
            }

            try
            {
                Directory.Move(path, newPath);
            }
            catch (Exception ex)
            {
                ErrorMessage  = ex.Message;
                return string.Empty;
            }

            return newPath;

        }


        public static bool DriveExists(string path, out string drive)
        {

            int x = path.IndexOf(":");
            if (x > 0 && path.Length > 2)
            {
                drive = path.Substring(0, 2);
                return Directory.Exists(@drive);
            }
            else
            {
                drive = "Error: No Drive Given";
                return false;
            }
        }

        public static bool DeleteDirectory(string target_dir)
        {
            try
            {
                string[] files = Directory.GetFiles(target_dir);
                string[] dirs = Directory.GetDirectories(target_dir);

                foreach (string file in files)
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                }

                foreach (string dir in dirs)
                {
                    DeleteDirectory(dir);
                }

                Directory.Delete(target_dir, false);
                return true;
            }
            catch
            {
                return false;
            }


        }

        public static string GetNextRevisionDir(string Dir2Rev, bool RenameDir, out string message)
        {
            message = string.Empty;

            string parentDir = Directory.GetParent(Dir2Rev).FullName;

            for (int i = 1; i < 32000; i++)
            {
                string xPath = string.Concat(parentDir, @"\", i.ToString());
                if (!Directory.Exists(xPath))
                {
                    if (!RenameDir)
                        return xPath;

                    try
                    {
                        // Rename Current dir to Revision dir
                        Directory.Move(Dir2Rev, xPath);
                        if (!Directory.Exists(xPath))
                        {
                            message = "Unable to replace the existing Document until all associated files have been closed.";
                            return string.Empty;
                        }

                        message = string.Concat("The existing Document file and all associated files (i.e. parsed segments & notes) have been saved as Revision ", i.ToString(), " and can be found in folder: ", xPath);

                    }
                    catch (Exception ex)
                    {
                        return string.Concat(ex.Message, " -- ", "Unable to replace the existing Document until all associated files have been closed.");
                    }
                }
            }

            message = "Exceeded Max.Revisions of 32,000. ";
            return string.Empty;
        }

        //public static string GetLastFolder(string path)
        //{
        //    string lastFolder = path.Split(Path.DirectorySeparatorChar).Last();

        //    return lastFolder;
        //}

        public static string DirExistsOrCreate(string path)
        {
            string returnValue = string.Empty;

            if (!Directory.Exists(path))
            {
                string _drive = string.Empty;
                if (DriveExists(path, out _drive))
                {
                    try // added 03.26.2020
                    {
                        Directory.CreateDirectory(path);
                        if (!Directory.Exists(path))
                        {
                            returnValue = string.Concat("Unable to find folder path ", path);
                        }
                    }
                    catch (Exception ex)
                    {
                        returnValue = string.Concat("Unable to create folder: ", path, " - Error: ", ex.Message);
                    }
                }
                else
                {
                    returnValue = string.Concat("Unable to locate Drive: ", _drive);
                }
            }

            return returnValue; // returns an Empty string if Directory exists or is created.
        }

        public static string[] GetSortedFolders(string dirPath)
        {
            List<string> data = new List<string>();

            string[] strDir = Directory.GetDirectories(dirPath);

            NumericComparer nc = new NumericComparer();
            Array.Sort(strDir, nc);
            foreach (var item in strDir)
            {
                data.Add(Path.GetFileName(item));
            }

            return data.ToArray();
        }
    }
}
