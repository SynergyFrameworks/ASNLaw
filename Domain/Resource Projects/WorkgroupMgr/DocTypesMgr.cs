using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;


namespace WorkgroupMgr
{
    public class DocTypesMgr
    {
        public DocTypesMgr(string DocTypePath)
        {
            _DocTypePath = DocTypePath;
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

        private string _DocTypePath = string.Empty;
        public string DocTypePath
        {
            get { return _DocTypePath; }
        }

        private bool CreateSampleDocTypes()
        {

            DataTable dt = new DataTable(DocTypesFields.TableName);

            dt.Columns.Add(DocTypesFields.UID, typeof(int));
            dt.Columns.Add(DocTypesFields.Item, typeof(string));
            dt.Columns.Add(DocTypesFields.Description, typeof(string));

            DataRow row;

            string item = string.Empty;
            string description = string.Empty;
            for (int i = 0; i < 14; i++)
            {
                switch (i)
                {
                    case 0:
                        item = "A";
                        description = "Solicitation/Contract Form";
                        break;

                    case 1:
                        item = "B";
                        description = "Supplies or Services and Prices/Costs";
                        break;

                    case 2:
                        item = "C";
                        description = "Description/Specifications/Work Statement - SOW: 1. Scope, 2. Reference Doc. 3. Requirements";
                        break;

                    case 3:
                        item = "D";
                        description = "Packaging and Marking";
                        break;

                    case 4:
                        item = "E";
                        description = "Inspection and Acceptance";
                        break;

                    case 5:
                        item = "F";
                        description = "Deliveries or Performance - Contract Delivery Dates, CLINs, Performance, Time Frame";
                        break;

                    case 6:
                        item = "G";
                        description = "Contract Administration Data";
                        break;

                    case 7:
                        item = "H";
                        description = "Special Contract Requirements - Security Clearances, Geographic Location, Unique Requirements";
                        break;

                    case 8:
                        item = "I";
                        description = "Contract Clauses - Clauses required by Procurement Regulations or Law which pertain to this Procurement";
                        break;

                    case 9:
                        item = "J";
                        description = "List of Attachments - List Contains: Security Form, CDRL, SOW, Specification - Financial Data: Sheet, Exhibits";
                        break;

                    case 10:
                        item = "K";
                        description = "Representations, certifications, and Other Statements of Offerors - Offeror’s Type of Business Buy American Act Provisions Cost Accounting Standards Notices, etc.";
                        break;

                    case 11:
                        item = "L";
                        description = "Instructions, Conditions, and Notices to Offerors - Type of Contract, Solicitation Definitions, Prop Requirments, Progress Payments, etc";
                        break;

                    case 12:
                         item = "L2";
                         description = "2nd 'L' – Often Section L is allocated as Number + Caption in one column and Text in another column.";
                        break;
                    case 13:
                        item = "M";
                        description = "Evaluation Factors for Award - How Proposal will be Evaluated";
                        break;
                }

                row = dt.NewRow();
                row[DocTypesFields.UID] = i;
                row[DocTypesFields.Item] = item;
                row[DocTypesFields.Description] = description;

                dt.Rows.Add(row);
                
            }

            bool returnValue = CreateDocType("Fed RFP Sections", "Contract Sections Part 1 Schedule per MIL-HDBK-245D", dt);

            return returnValue;
        }

        public string GetDescription(string docTypeName)
        {
            string description = string.Empty;

            string file = string.Concat(docTypeName, ".txt");
            string listPathFile = Path.Combine(_DocTypePath, file);

            if (File.Exists(listPathFile))
            {
                description = Files.ReadFile(listPathFile);
            }

            return description;
        }

        public bool DocTypeNameExists(string docTypeName)
        {
            string foundListName = string.Empty;

            string[] files = Directory.GetFiles(_DocTypePath, "*.xml", SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                foundListName = Files.GetFileNameWOExt(file);

                if (docTypeName.Trim().ToLower() == foundListName.ToLower())
                {
                    return true;
                }

            }

            return false;
        }


        public string[] GetDocTypesNames()
        {
            _ErrorMessage = string.Empty;

            if (!Directory.Exists(_DocTypePath))
            {
                _ErrorMessage = string.Concat("Unable to find List folder: ", _DocTypePath);
                return null;
            }

            string[] files = Directory.GetFiles(_DocTypePath, "*.xml", SearchOption.TopDirectoryOnly);

            if (files.Length == 0) // then create sample Lists
            {
                CreateSampleDocTypes();
                files = Directory.GetFiles(_DocTypePath, "*.xml", SearchOption.TopDirectoryOnly);
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

        public DataTable GetDocTypeItems(string docTypeName)
        {
            _ErrorMessage = string.Empty;

            string file = string.Concat(docTypeName, ".xml");
            string listPathFile = Path.Combine(_DocTypePath, file);

            if (!File.Exists(listPathFile))
            {
                _ErrorMessage = string.Concat("Unable to find Document Type data file: ", listPathFile);
                return null;
            }

            List<string> items = new List<string>();

            DataSet ds = DataFunctions.LoadDatasetFromXml(listPathFile);

            return ds.Tables[0];

        }

        /// <summary>
        /// Checks if the List's Backup exists, if Not, then attempts to create it.
        /// </summary>
        /// <returns>Returns the Backup path, unless an Error occurred, then it returns an Empty String</returns>
        private string CheckCreateBackupFolder()
        {
            _ErrorMessage = string.Empty;

            string backupFolder = Path.Combine(_DocTypePath, "Backup");
            if (!Directory.Exists(backupFolder))
            {
                try
                {
                    Directory.CreateDirectory(backupFolder);
                }
                catch (Exception ex)
                {
                    _ErrorMessage = string.Concat("Error: Unable to create the Document Type Backup folder: ", backupFolder, Environment.NewLine, Environment.NewLine, ex.Message);
                    return string.Empty;
                }
            }

            return backupFolder;
        }

        private bool BackupDocType(string backupFolder, string file)
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

            string listPathFile = Path.Combine(_DocTypePath, file);

            try
            {
                File.Copy(listPathFile, backupPathfile);
                return true;
            }
            catch (Exception ex)
            {
                _NoticeMessage = string.Concat("Notice: Unable to Backup the Document Type.", Environment.NewLine, Environment.NewLine, ex.Message);
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


            string listPathFile = Path.Combine(_DocTypePath, file);

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

        public bool DeleteDocType(string docTypeName)
        {
            _ErrorMessage = string.Empty;

            string fileXML = string.Concat(docTypeName, ".xml");
            string listPathFileXML = Path.Combine(_DocTypePath, fileXML);

            string fileTXT = string.Concat(docTypeName, ".txt");
            string listPathFileTXT = Path.Combine(_DocTypePath, fileTXT);

            string backupFolder = CheckCreateBackupFolder();

            if (File.Exists(listPathFileXML))
            {
                if (backupFolder.Length == 0)
                {
                    return false;
                }

                if (!BackupDocType(backupFolder, fileXML))
                {
                    return false;
                }

                if (File.Exists(listPathFileTXT))
                {
                    BackupDocType(backupFolder, fileTXT);
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
                    _ErrorMessage = string.Concat("Error: Unable to Delete Document Type", Environment.NewLine, Environment.NewLine, ex.Message);
                    return false;
                }
            }

            return true;
        }

        public DataTable GetEmptyDataTable()
        {
            DataTable dt = new DataTable(DocTypesFields.TableName);

            dt.Columns.Add(DocTypesFields.UID, typeof(int));
            dt.Columns.Add(DocTypesFields.Item, typeof(string));
            dt.Columns.Add(DocTypesFields.Description, typeof(string));

            return dt;
        }

        public bool CreateDocType(string docTypeName, string description, DataTable dtSource)
        {
            _ErrorMessage = string.Empty;

            string file = string.Concat(docTypeName, ".xml");
            string docTypePathFile = Path.Combine(_DocTypePath, file);

            string backupFolder = CheckCreateBackupFolder();

            if (File.Exists(docTypePathFile))
            {
                if (backupFolder.Length == 0)
                {
                    return false;
                }

                if (!BackupDocType(backupFolder, file))
                {
                    return false;
                }

                try
                {
                    File.Delete(docTypePathFile); // Delete the old 
                }
                catch (Exception ex)
                {
                    _ErrorMessage = string.Concat("Error: Unable to save changes.", Environment.NewLine, Environment.NewLine, ex.Message);
                    return false;
                }
            }

            DataTable dt = new DataTable(DocTypesFields.TableName);

            dt.Columns.Add(DocTypesFields.UID, typeof(int));
            dt.Columns.Add(DocTypesFields.Item, typeof(string));
            dt.Columns.Add(DocTypesFields.Description, typeof(string));

            //
            //

            DataRow row;
            int i = 0;
            foreach (DataRow rowSource in dtSource.Rows)
            {

                row = dt.NewRow();
                row[DocTypesFields.UID] = i;
                row[DocTypesFields.Item] = rowSource[DocTypesFields.Item];
                row[DocTypesFields.Description] = rowSource[DocTypesFields.Description];

                dt.Rows.Add(row);
                i++;
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);


            DataFunctions.SaveDataXML(ds, docTypePathFile);
            if (DataFunctions._ErrorMessage.Length > 0)
            {
                RestoreLastBackup(backupFolder, file);

                _ErrorMessage = string.Concat("Error: Unable to save changes.", Environment.NewLine, Environment.NewLine, DataFunctions._ErrorMessage);

                return false;
            }

            // Desription
            file = string.Concat(docTypeName, ".txt");
            docTypePathFile = Path.Combine(_DocTypePath, file);
            if (File.Exists(docTypePathFile))
            {
                string description_old = Files.ReadFile(docTypePathFile);
                if (description != description_old)
                {
                    BackupDocType(backupFolder, file);
                    try
                    {
                        File.Delete(docTypePathFile);
                        Files.WriteStringToFile(description, docTypePathFile);
                    }
                    catch (Exception ex)
                    {
                        _ErrorMessage = string.Concat("Your Document Types changes have been saved, but the Description was not saved due to an error.", Environment.NewLine, Environment.NewLine, ex.Message);
                        return false;
                    }

                }
            }
            else
            {
                Files.WriteStringToFile(description, docTypePathFile);
            }


            return true;
        }

    }
}
