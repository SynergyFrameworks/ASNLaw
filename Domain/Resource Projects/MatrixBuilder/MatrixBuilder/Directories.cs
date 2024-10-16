﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Net;

using System.Runtime;
using System.Runtime.InteropServices;


namespace MatrixBuilder
{
    class Directories
    {
  
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

        public static string GetLastFolder(string path)
        {
            string lastFolder = path.Split(Path.DirectorySeparatorChar).Last();

            return lastFolder;
        }

        public static string DirExistsOrCreate(string path)
        {
            string returnValue = string.Empty;

            if (!Directory.Exists(path))
            {
                string _drive = string.Empty;
                if (DriveExists(path, out _drive))
                {
                    Directory.CreateDirectory(path);
                    if (!Directory.Exists(path))
                    {
                        returnValue = string.Concat("Unable to find folder path ", path);
                    }
                }
                else
                {
                    returnValue = string.Concat("Unable to locate Drive: ", _drive);
                }
            }

            return returnValue; // returns an Empty string if Directory exists or is created.
        }


        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);

        public static bool IsInternetAvailable()
        {
            int description;
            return InternetGetConnectedState(out description, 0);
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
    }
}
