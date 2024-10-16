using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace WorkgroupMgr
{
    public class ListMgr
    {
        public ListMgr(string ListPath)
        {
            _ListPath = ListPath;
        }

        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        private string _NoticeMessage = string.Empty;
        public string NoticeMessage
        {
            get { return _NoticeMessage; }
        }


        private string _ListPath = string.Empty;
        public string ListPath
        {
            get { return _ListPath; }
        }

        private bool CreateSampleLists()
        {
           // string[] ReviewStatus = new string[] {"Not Reviewed", "Accepted", "Rejected", "Reopened"};
            string[] Boolean = new string[] { "Yes", "No" };
            CreateList("Boolean", "Yes or No - Example: Use for 'Is Compliant?' column", Boolean);

            string[] Compliant = new string[] { "Yes", "No", "Partial" };
            CreateList("Compliant", "Yes or No or Partial - Example: Use for 'Is Compliant' column", Compliant);

            string[] Priority = new string[] { "High", "Medium", "Low" };
            CreateList("Priority", "Priority of a requirement", Priority);
            
            string[] ReqReviewStatus = new string[] {"Proposed", "In Progress", "Drafted", "Approved", "Implemented", "Verified", "Deferred", "Deleted", "Rejected"};
            CreateList("Requirement Review Status", "Track the status of each requirement over time to monitor overall project status. Status tracking can help you avoid the pervasive 90% Done problem.", ReqReviewStatus);

            string[] StoryPoints = new string[] { "0.5", "1", "2", "3", "5", "8", "13", "20", "40", "100" };
            CreateList("Agile Story Points", "Story points rate the relative effort of work in a Fibonacci-like format.", StoryPoints);

            return true;
        }

        public string GetDescription(string listName)
        {
            string description = string.Empty;

            string file = string.Concat(listName, ".txt");
            string listPathFile = Path.Combine(_ListPath, file);

            if (File.Exists(listPathFile))
            {
                description = Files.ReadFile(listPathFile);
            }

            return description;

        }

        public bool ListNameExists(string listName)
        {
            string foundListName = string.Empty;

            string[] files = Directory.GetFiles(_ListPath, "*.xml", SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                foundListName = Files.GetFileNameWOExt(file);

                if (listName.Trim().ToLower() == foundListName.ToLower())
                {
                    return true;
                }

            }

            return false;
        }

        public string[] GetListNames()
        {
            _ErrorMessage = string.Empty;

            if (!Directory.Exists(_ListPath))
            {
                _ErrorMessage = string.Concat("Unable to find List folder: ", _ListPath);
                return null;
            }

            string[] files = Directory.GetFiles(_ListPath, "*.xml", SearchOption.TopDirectoryOnly);

            if (files.Length == 0) // then create sample Lists
            {
                CreateSampleLists();
                files = Directory.GetFiles(_ListPath, "*.xml", SearchOption.TopDirectoryOnly);
            }
            List<string> ListNames = new List<string>();
            string listName = string.Empty;

            foreach (string file in files)
            {
                listName = Files.GetFileNameWOExt(file);
                ListNames.Add(listName);
            }

            string[] arrListNames = ListNames.ToArray();

            return arrListNames;
        }

        public string[] GetListItems(string ListName)
        {
            _ErrorMessage = string.Empty;

            string file = string.Concat(ListName, ".xml");
            string listPathFile = Path.Combine(_ListPath, file);

            if (!File.Exists(listPathFile))
            {
                _ErrorMessage = string.Concat("Unable to find List data file: ", listPathFile);
                return null;
            }

            List<string> items = new List<string>();

            DataSet ds = DataFunctions.LoadDatasetFromXml(listPathFile);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                items.Add(row[ListFields.Item].ToString());
            }

            return items.ToArray();

        }

        /// <summary>
        /// Checks if the List's Backup exists, if Not, then attempts to create it.
        /// </summary>
        /// <returns>Returns the Backup path, unless an Error occurred, then it returns an Empty String</returns>
        private string CheckCreateBackupFolder()
        {
            _ErrorMessage = string.Empty;

            string backupFolder = Path.Combine(_ListPath, "Backup");
            if (!Directory.Exists(backupFolder))
            {
                try
                {
                    Directory.CreateDirectory(backupFolder);
                }
                catch (Exception ex)
                {
                    _ErrorMessage = string.Concat("Error: Unable to create the List Backup folder: ", backupFolder, Environment.NewLine, Environment.NewLine, ex.Message);
                    return string.Empty;
                }
            }

            return backupFolder;
        }

        private bool BackupList(string backupFolder, string file)
        {
            string prefix = "~";

            string backupFile = string.Concat(prefix, file);
            string backupPathfile = Path.Combine(backupFolder, backupFile);
            if (File.Exists(backupPathfile))
            {
                for (int i = 0; i < 101; i++)
                {
                    backupFile = string.Concat(prefix, backupFile);
                    backupPathfile = Path.Combine(backupFolder, backupFile);
                    if (!File.Exists(backupPathfile))
                    {
                        break;
                    }
                    prefix += "~";
                }
            }

            string listPathFile = Path.Combine(_ListPath, file);

            try
            {
                File.Copy(listPathFile, backupPathfile);
                return true;
            }
            catch (Exception ex)
            {
                _NoticeMessage = string.Concat("Notice: Unable to Backup the List.", Environment.NewLine, Environment.NewLine, ex.Message);
                return false;
            }
        }

        private bool RestoreLastBackup(string backupFolder, string file)
        {
            string prefix = "~";

            string foundBackupFile = string.Empty;

            string backupFile = string.Concat(prefix, file);
            string backupPathfile = Path.Combine(backupFolder, backupFile);
            if (File.Exists(backupPathfile))
            {
                foundBackupFile = backupPathfile;
                for (int i = 0; i < 101; i++)
                {
                    prefix += "~";
                    backupPathfile = Path.Combine(backupFolder, backupFile);
                    if (!File.Exists(backupPathfile))
                    {
                        break;
                    }
                    foundBackupFile = backupPathfile;
                }
            }
            else
            {
                return false;
            }


            string listPathFile = Path.Combine(_ListPath, file);

            if (!File.Exists(listPathFile))
            {
                File.Copy(foundBackupFile, listPathFile); // Restore the last backup
                if (File.Exists(listPathFile))
                {
                    return true;
                }
            }

            return false;
        }

        public bool DeleteList(string listName)
        {
            _ErrorMessage = string.Empty;

            string fileXML = string.Concat(listName, ".xml");
            string listPathFileXML = Path.Combine(_ListPath, fileXML);

            string fileTXT = string.Concat(listName, ".txt");
            string listPathFileTXT = Path.Combine(_ListPath, fileTXT);

            string backupFolder = CheckCreateBackupFolder();

            if (File.Exists(listPathFileXML))
            {
                if (backupFolder.Length == 0)
                {
                    return false;
                }

                if (!BackupList(backupFolder, fileXML))
                {
                    return false;
                }

                if (File.Exists(listPathFileTXT))
                {
                    BackupList(backupFolder, fileTXT);
                }


                try
                {
                    File.Delete(listPathFileXML); // Delete List
                    if (File.Exists(listPathFileTXT))
                    {
                        File.Delete(listPathFileTXT);
                    }                  
                }
                catch (Exception ex)
                {
                    _ErrorMessage = string.Concat("Error: Unable to Delete List", Environment.NewLine, Environment.NewLine, ex.Message);
                    return false;
                }
            }

            return true;
        }

        public bool CreateList(string listName, string description, string[] listItems)
        {
            _ErrorMessage = string.Empty;

            string file = string.Concat(listName, ".xml");
            string listPathFile = Path.Combine(_ListPath, file);

            string backupFolder = CheckCreateBackupFolder();

            if (File.Exists(listPathFile))
            {               
                if (backupFolder.Length == 0)
                {
                    return false;
                }

                if (!BackupList(backupFolder, file))
                {
                    return false;
                }

                try
                {
                    File.Delete(listPathFile); // Delete the old 
                }
                catch (Exception ex)
                {
                    _ErrorMessage = string.Concat("Error: Unable to save changes.", Environment.NewLine, Environment.NewLine, ex.Message);
                    return false;
                }
            }

            DataTable dt = new DataTable(ListFields.TableName);

            dt.Columns.Add(ListFields.UID, typeof(int));
            dt.Columns.Add(ListFields.Item, typeof(string));
           
            DataRow row;
            int i = 0;
            foreach (string item in listItems)
            {
                row = dt.NewRow();
                row[ListFields.UID] = i;
                row[ListFields.Item] = item.Trim();

                dt.Rows.Add(row);
                i++;
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);


            DataFunctions.SaveDataXML(ds, listPathFile);
            if (DataFunctions._ErrorMessage.Length > 0)
            {
                RestoreLastBackup(backupFolder, file);
  
                _ErrorMessage = string.Concat("Error: Unable to save changes.", Environment.NewLine, Environment.NewLine, DataFunctions._ErrorMessage);
                
                return false;
            }

            // Desription
          file = string.Concat(listName, ".txt");
          listPathFile = Path.Combine(_ListPath, file);
          if (File.Exists(listPathFile))
          {
              string description_old = Files.ReadFile(listPathFile);
              if (description != description_old)
              {
                  BackupList(backupFolder, file);
                  try
                  {
                      File.Delete(listPathFile);
                      Files.WriteStringToFile(description, listPathFile);
                  }
                  catch (Exception ex)
                  {
                      _ErrorMessage = string.Concat("Your List changes have been saved, but the Description was not saved due to an error.", Environment.NewLine, Environment.NewLine, ex.Message);
                      return false;
                  }

              }
          }
          else
          {
              Files.WriteStringToFile(description, listPathFile);
          }


            return true;
        }

    }
}
