using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Atebion.Common;

namespace ProfessionalDocAnalyzer
{
    public class DocumentManager
    {
        public List<string> GetDocNames()
        {
            if (AppFolders.Project == string.Empty)
                return null;

            if (AppFolders.DocPath == string.Empty)
                return null;

            List<string> list = new List<string>();
            string lastFolderName = string.Empty;
            string s = string.Empty;

            string[] projectsPaths = Directory.GetDirectories(AppFolders.DocPath);

            for (int i = 0; i < projectsPaths.Length; i++)
            {
                s = projectsPaths[i];
                
                lastFolderName = Directories.GetLastFolder(projectsPaths[i]);
                if (lastFolderName.IndexOf("~") == -1)
                    list.Add(lastFolderName);
                

            }

            return list;
        }
    }
}
