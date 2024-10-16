using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace test
{
    class Directories
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

        public static string GetLastFolder(string path)
        {
            string lastFolder = path.Split(Path.DirectorySeparatorChar).Last();

            return lastFolder;
        }

        public string GetNextRevisionDir(string Dir2Rev, bool RenameDir, out string message)
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

        public static string DirExistsOrCreate(string path)
        {
            string returnValue = string.Empty;

            if (!Directory.Exists(path))
            {
                string _drive = string.Empty;
                if (Directories.DriveExists(path, out _drive))
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
    }
}
