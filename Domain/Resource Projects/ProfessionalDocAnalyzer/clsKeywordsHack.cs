using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using Atebion.Common;

namespace ProfessionalDocAnalyzer
{
    class clsKeywordsHack
    {

        private string _ErrorMessage = string.Empty;

        public bool KeywordsOutput() //
        {
            bool returnResults = false;

            string[] KeywordsUsed = GetKeywords();
            DataSet dsKeyWords = CreateDataset_FoundKeywords();
            DataSet dsAnalysisResults = GetAnalysisResuts();

            if (dsAnalysisResults == null)
            {
                return false;
            }

            string keyword1 = string.Empty;
            string keyword2 = string.Empty;
            string keywords = string.Empty;

            string[] keywordsParsed;

            int x = -1;

            // Loop through keywords used
            foreach (string keyword in KeywordsUsed)
            {
                keyword1 = string.Concat(keyword, " [");
                keyword2 = string.Concat(keyword, "  [");

                // Loop through Analysis Results
                foreach (DataRow row in dsAnalysisResults.Tables[0].Rows)
                {
                    keywords = row["Keywords"].ToString().Trim();
                    if (keywords.Length > 0)
                    {
                        x = keywords.LastIndexOf(',');
                        if (x > -1)
                        {
                            keywordsParsed = keywords.Split(',');
                            // Loop through Keywords found for the selected segment
                            foreach (string xKeyword in keywordsParsed)
                            {
                                if (keyword1 == xKeyword.Trim())
                                {
                                    // What to do?
                                }
                                if (keyword2 == xKeyword.Trim())
                                {
                                    // What to do?
                                }
                            }
                        }
                        else
                        {
                             if (keyword1 == keywords.Trim())
                            {
                                // What to do?
                            }
                            if (keyword2 == keywords.Trim())
                            {
                                // What to do?
                            }

                        }

                    }
                }
               

            }


            return returnResults;
        }


        public bool KeywordsInput() //
        {
            bool returnResults = false;



            return returnResults;
        }

        private DataSet GetAnalysisResuts()
        {
            _ErrorMessage = string.Empty;

            string parsedFile = string.Concat(AppFolders.DocParsedSecXML, @"\ParseResults.xml");

            if (!File.Exists(parsedFile))
            {
                _ErrorMessage = string.Concat("Analysis Results file not found: ", parsedFile);
  
                return null;
            }

            
            DataSet dsParseResults = Files.LoadDatasetFromXml(parsedFile);

            return dsParseResults;
        }

        public string[] GetKeywords()
        {
            // Get Keywords -- Added 06.06.2014
            string[] inKeywords = new string[0]; // empty string array

            if (File.Exists(AppFolders.AnalysisParseSegKeywords + @"\Keywords.txt"))
            {
                inKeywords = File.ReadAllLines(AppFolders.AnalysisParseSegKeywords + @"\Keywords.txt", Encoding.UTF8);

            }
            else if ((File.Exists(AppFolders.DocParsedSecKeywords + @"\Keywords.txt")))
            {
                inKeywords = File.ReadAllLines(AppFolders.DocParsedSecKeywords + @"\Keywords.txt", Encoding.UTF8);
            }

            return inKeywords;
        }
        public string[] GetKeywords(string path)
        {
            // Get Keywords -- Added 06.06.2014
            string[] inKeywords = new string[0]; // empty string array

            if (File.Exists(path + @"\Keywords.txt"))
            {
                inKeywords = File.ReadAllLines(path + @"\Keywords.txt", Encoding.UTF8);

            }

            return inKeywords;
        }

        private DataSet CreateDataset_FoundKeywords()
        {
            // Create a new DataTable.
            DataTable table = new DataTable("FoundKeywords");

            // Declare variables for DataColumn and DataRow objects.
            DataColumn column = null;

            // Create new DataColumn, set DataType, ColumnName 
            // and add to DataTable.    
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = NP_Keywords.UID;
            column.ReadOnly = false;
            column.Unique = false;

            // Add the Column to the DataColumnCollection.
            table.Columns.Add(column);

            // Create new DataColumn, set DataType, ColumnName 
            // and add to DataTable.    
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = NP_Keywords.KeyWord;
            column.ReadOnly = false;
            column.Unique = false;

            // Add the Column to the DataColumnCollection.
            table.Columns.Add(column);

            // Create new DataColumn, set DataType, ColumnName 
            // and add to DataTable.    
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = NP_Keywords.Count;
            column.ReadOnly = false;
            column.Unique = false;

            // Add the Column to the DataColumnCollection.
            table.Columns.Add(column);

            // Create new DataColumn, set DataType, ColumnName 
            // and add to DataTable.    
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = NP_Keywords.SegmentUID;
            column.ReadOnly = false;
            column.Unique = false;

            // Add the Column to the DataColumnCollection.
            table.Columns.Add(column);

            // Make the UID column the primary key column.
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = table.Columns[NP_Keywords.UID];
            table.PrimaryKey = PrimaryKeyColumns;

            // Instantiate the DataSet variable.
            DataSet dataSet = null;
            dataSet = new DataSet();

            // Add the new DataTable to the DataSet.
            dataSet.Tables.Add(table);

            //Return dataset
            return dataSet;

        }

        public class NP_Keywords
        {
            public const string UID = "UID";
            public const string KeyWord = "Keyword";
            public const string SegmentUID = "SegmentUID";
            public const string Count = "Count";

        }

    }
}
