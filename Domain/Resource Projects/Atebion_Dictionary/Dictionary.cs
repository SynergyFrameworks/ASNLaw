using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using OfficeOpenXml;
using System.Xml;
using Atebion.Common;

namespace Atebion_Dictionary
{
    public class Dictionary
    {
        public Dictionary(string DictionaryRootPath)
        {
            _DictionaryRootPath = DictionaryRootPath;

            CheckDictionaryPath();
        }

        public Dictionary(string DictionaryRootPath, string DictionaryName)
        {
            _DictionaryRootPath = DictionaryRootPath;
            _DictionaryName = DictionaryName;

            CheckDictionaryExists();

        }

        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }

        }

        private string _DictionaryRootPath = string.Empty;

        private string _DictionaryName = string.Empty;

        public string DictionaryName
        {
            get { return _DictionaryName; }
            set 
            { 
                _DictionaryName = value;
                GetDataset();
            
            }
        }

        private DataSet _ds;
        public DataSet Dictionary_DataSet
        {
            get {
                    if (_ds == null)
                        _ds = GetDataset();

                    return _ds; 
                }
            set {
                    if (value == null)
                        return;

                    _ds = value;
                    SaveDicDataset(_ds, _Description);
                }
            
        }

        private string _Description = string.Empty;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private string _ExcelImportLog = string.Empty;
        public string ExcelImportLog
        {
            get { return _ExcelImportLog; }
        }

        private string _ExcelImportDups = string.Empty;
        public string ExcelImportDups
        {
            get { return _ExcelImportDups; }
        }


        private List<string> _Dictionaries = new List<string>();
        public string[] Get_Dictionaries()
        {
            if (!CheckDictionaryPath())
                return null;

            string[] files = Directory.GetFiles(_DictionaryRootPath, "*.dicx");

            List<string> dicNames = new List<string>();
            foreach (string file in files)
            {
                dicNames.Add(Files.GetFileNameWOExt(file));
            }

            return dicNames.ToArray(); ;
        }


        private GenericDataManger _DataMgr = new GenericDataManger();




        private bool CheckDictionaryPath()
        {
             if (!Directory.Exists(_DictionaryRootPath))
            {
                _ErrorMessage = string.Concat("Dictionary path is not found: ", _DictionaryRootPath);
                 return false;
            }

            return true;
        }

        public bool CheckDictionaryExists(string dictionaryName)
        {
            _DictionaryName = dictionaryName;

            return CheckDictionaryExists();
        }

        public bool CheckDictionaryExists()
        {
            if (!CheckDictionaryPath())
                return false;

            string file = string.Concat(_DictionaryName, ".dicx");
            string pathFile = Path.Combine(_DictionaryRootPath, file);

            if (!File.Exists(pathFile))
            {
                _ErrorMessage = string.Concat("Dictionary was not found: ", pathFile);
                 return false;
            }

            return true;
        }


        public void CreateEmptyDataTable_Dictionary()
        {      
            DataTable table = new DataTable(DictionaryFieldConst.TableName);

            table.Columns.Add(DictionaryFieldConst.UID, typeof(int));
            table.Columns.Add(DictionaryFieldConst.Category_UID, typeof(int));
            table.Columns.Add(DictionaryFieldConst.Item, typeof(string));
            table.Columns.Add(DictionaryFieldConst.Definition, typeof(string));
            table.Columns.Add(DictionaryFieldConst.Weight, typeof(double));
            table.Columns.Add(DictionaryFieldConst.HighlightColor, typeof(string));

            if (_ds == null)
                _ds = new System.Data.DataSet();

            _ds.Tables.Add(table);
        }

        public DataTable CreateEmptyDataTableTransformation_Dictionary()
        {
            DataTable table = new DataTable(DictionaryFieldConst.TableName);

            table.Columns.Add(DictionaryFieldConst.UID, typeof(int));
            table.Columns.Add(DictionaryFieldConst.Category_UID, typeof(int));
            table.Columns.Add(DictionaryFieldConst.Category, typeof(string));
            table.Columns.Add(DictionaryFieldConst.Item, typeof(string));
            table.Columns.Add(DictionaryFieldConst.Definition, typeof(string));
            table.Columns.Add(DictionaryFieldConst.Weight, typeof(double));
            table.Columns.Add(DictionaryFieldConst.HighlightColor, typeof(string));

            return table;
        }

        public DataTable CreateEmptyDataTableTransformation_DictionaryForExport()
        {
            DataTable table = new DataTable(DictionaryFieldConst.TableName);

            table.Columns.Add(DictionaryFieldConst.Category, typeof(string));
            table.Columns.Add(DictionaryFieldConst.Item, typeof(string));
            table.Columns.Add(DictionaryFieldConst.Definition, typeof(string));
            table.Columns.Add(DictionaryFieldConst.Weight, typeof(double));
            table.Columns.Add(SynonymsFieldConst.TableName, typeof(string));
            table.Columns.Add(DictionaryFieldConst.HighlightColor, typeof(string));

            return table;
        }


        public void CreateEmptyDataTable_Synonyms()
        {
            DataTable table = new DataTable(SynonymsFieldConst.TableName);

            table.Columns.Add(SynonymsFieldConst.UID, typeof(int));
            table.Columns.Add(SynonymsFieldConst.Dictionary_UID, typeof(int));
            table.Columns.Add(SynonymsFieldConst.Item, typeof(string));

            if (_ds == null)
                _ds = new System.Data.DataSet();

            _ds.Tables.Add(table);
        }

        public void CreateEmptyDataTable_Categories()
        {

            DataTable table = new DataTable(CategoryFieldConst.TableName);

            table.Columns.Add(CategoryFieldConst.UID, typeof(int));
            table.Columns.Add(CategoryFieldConst.Name, typeof(string));
            

            // Create Default Category
            DataRow row = table.NewRow();
            row[CategoryFieldConst.UID] = 0;
            row[CategoryFieldConst.Name] = "General"; // Default Category
            

            table.Rows.Add(row);

            if (_ds == null)
                _ds = new System.Data.DataSet();

            _ds.Tables.Add(table);
        
        }

        public DataTable CreateEmptyDataTable_DictionaryTrans()
        {
            DataTable table = new DataTable(DictionaryFieldConst.TableName);

            table.Columns.Add(DictionaryFieldConst.UID, typeof(int));
            table.Columns.Add(DictionaryFieldConst.Category, typeof(string));
            table.Columns.Add(DictionaryFieldConst.Item, typeof(string));
            table.Columns.Add(DictionaryFieldConst.Definition, typeof(string));
            table.Columns.Add(DictionaryFieldConst.Weight, typeof(double));


            return table;
        
        }

        public DataSet GetDataset_Empty()
        {
            _ErrorMessage = string.Empty;

            _ds = null;

            CreateEmptyDataTable_Dictionary();
            CreateEmptyDataTable_Categories();
            CreateEmptyDataTable_Synonyms();

            return _ds;
        }


        public DataSet GetDataset(string DictionaryName)
        {
            _DictionaryName = DictionaryName;

            return GetDataset();
        }

        public string GetDictionaryDescription(string dictionaryName)
        {
            _DictionaryName = dictionaryName;

             return GetDictionaryDescription();
        }

        public string GetDictionaryDescription()
        {
            _ErrorMessage = string.Empty;

            if (_DictionaryName == string.Empty)
            {
                _ErrorMessage = "Dictionary Name has not been defined";
                return string.Empty;
            }

            string file = string.Concat(_DictionaryName, ".txt");
            string pathFile = Path.Combine(_DictionaryRootPath, file);
            if (!File.Exists(pathFile))
            {
                _ErrorMessage = "Dictionary Description file was not found.";
                return string.Empty;
            }

            string description = Files.ReadFile(pathFile);

            return description;

        }

        public bool SaveDictionaryDescription(string description, string dictionaryName)
        {
            _ErrorMessage = string.Empty;

            if (dictionaryName == string.Empty)
            {
                _ErrorMessage = "Dictionary Name has been defined";
                return false;
            }

            _DictionaryName = dictionaryName;

            return SaveDictionaryDescription(description);
            
        }

        public bool SaveDictionaryDescription(string description)
        {
            _ErrorMessage = string.Empty;

            if (DictionaryName == string.Empty)
            {
                _ErrorMessage = "Dictionary Name has not been defined.";
                return false;

            }

            if (_DictionaryRootPath == string.Empty)
            {
                _ErrorMessage = "Dictionary Path has not been defined.";
                return false;

            }

            string file = string.Concat(_DictionaryName, ".txt");
            string pathFile = Path.Combine(_DictionaryRootPath, file);

            return Files.WriteStringToFile(description, pathFile, true);
   
        }

        public DataSet GetDataset()
        {
            _ErrorMessage = string.Empty;

            if (DictionaryName == string.Empty)
            {
                _ErrorMessage = "Dictionary Name has not been defined.";
                return null;

            }

            if (_DictionaryRootPath == string.Empty)
            {
                _ErrorMessage = "Dictionary Path has not been defined.";
                return null;

            }

            string file = string.Concat(_DictionaryName, ".dicx");
            string pathFile = Path.Combine(_DictionaryRootPath, file);


            if (!File.Exists(pathFile))
            {
                _ErrorMessage = string.Concat("Unable to find ", pathFile);
                return null;
            }

            _ds = Files.LoadDatasetFromXml(pathFile);

            return _ds;
        }

        public DataView GetDataView_Transformation(DataSet ds)
        {
            _ds = ds;

            return GetDataView_Transformation();
        }

        public DataView GetDataView_Transformation()
        {
            DataTable dtX = CreateEmptyDataTableTransformation_Dictionary();
            DataRow newRow;
            int lastCatUID = -1;
            string CatName = string.Empty;
            int currentCatUID = -1;
            
            _ds.Tables[DictionaryFieldConst.TableName].DefaultView.Sort = DictionaryFieldConst.Category_UID;

            // Populate dtX
            foreach (DataRow row in _ds.Tables[DictionaryFieldConst.TableName].Rows)
            {
                newRow = dtX.NewRow();
                newRow[DictionaryFieldConst.UID] = row[DictionaryFieldConst.UID];
                currentCatUID = Convert.ToInt32(row[DictionaryFieldConst.Category_UID].ToString());
                newRow[DictionaryFieldConst.Category_UID] = currentCatUID;
                newRow[DictionaryFieldConst.Item] = row[DictionaryFieldConst.Item];
                newRow[DictionaryFieldConst.Definition] = row[DictionaryFieldConst.Definition];
                newRow[DictionaryFieldConst.Weight] = row[DictionaryFieldConst.Weight];
                newRow[DictionaryFieldConst.HighlightColor] = row[DictionaryFieldConst.HighlightColor];


                if (currentCatUID != lastCatUID)
                    CatName = GetCategoryName(currentCatUID);

                newRow[DictionaryFieldConst.Category] = CatName;

                lastCatUID = currentCatUID;

                dtX.Rows.Add(newRow);
            }

            dtX.DefaultView.Sort = DictionaryFieldConst.Category;

            DataView dv = dtX.DefaultView;

            return dv;

        }

        public string[] GetCategories()
        {
            if (_ds == null)
                return null;

            List<string> categories = new List<string>();

            if (_ds.Tables[CategoryFieldConst.TableName].Rows.Count == 0)
                return null;

            foreach (DataRow row in _ds.Tables[CategoryFieldConst.TableName].Rows)
            {
                if (row[CategoryFieldConst.Name] != null)
                    categories.Add(row[CategoryFieldConst.Name].ToString());
            }

            return categories.ToArray();

        }

        public DataTable GetDataTable_TransformationForExport()
        {
            DataTable dtX = CreateEmptyDataTableTransformation_DictionaryForExport();
            DataRow newRow;
            int lastCatUID = -1;
            string CatName = string.Empty;
            int currentCatUID = -1;

            // _ds.Tables[DictionaryFieldConst.TableName].DefaultView.Sort = DictionaryFieldConst.Category_UID;

            bool isFirst = true;
            // Populate dtX
            foreach (DataRow row in _ds.Tables[DictionaryFieldConst.TableName].Rows)
            {
                if (isFirst)
                {
                    isFirst = false;
                  //  continue;
                }
                newRow = dtX.NewRow();
                //newRow[DictionaryFieldConst.UID] = row[DictionaryFieldConst.UID];
                currentCatUID = Convert.ToInt32(row[DictionaryFieldConst.Category_UID].ToString());
                //newRow[DictionaryFieldConst.Category_UID] = currentCatUID;
                newRow[DictionaryFieldConst.Item] = row[DictionaryFieldConst.Item];
                newRow[DictionaryFieldConst.Definition] = row[DictionaryFieldConst.Definition];
                newRow[DictionaryFieldConst.Weight] = row[DictionaryFieldConst.Weight];
                newRow[DictionaryFieldConst.HighlightColor] = row[DictionaryFieldConst.HighlightColor];

                if (currentCatUID != lastCatUID)
                    CatName = GetCategoryName(currentCatUID);

                newRow[DictionaryFieldConst.Category] = CatName;

                string[] SynName = GetSynonymsPerDicItem(Convert.ToInt32(row[DictionaryFieldConst.UID]), _ds);

                if (SynName != null)
                {
                    newRow[SynonymsFieldConst.TableName] = String.Join(",", SynName);
                }

                lastCatUID = currentCatUID;

                dtX.Rows.Add(newRow);
            }

            //dtX.DefaultView.Sort = DictionaryFieldConst.Category_UID;

            return dtX;

        }

        public int GetCategoryUID(string CategoryName)
        {
            string expression = string.Concat(CategoryFieldConst.Name, " = '", CategoryName, "'");
            
            DataRow[] foundRows = _ds.Tables[CategoryFieldConst.TableName].Select(expression);

            if (foundRows.Length > 0)
            {
                return Convert.ToInt32(foundRows[0][CategoryFieldConst.UID].ToString());
            }

            return -1;
        }

        public string GetCategoryName(int CatUID)
        {
            string expression = string.Concat(CategoryFieldConst.UID, " = ", CatUID);
            DataRow[] foundRows = _ds.Tables[CategoryFieldConst.TableName].Select(expression);

            if (foundRows.Length > 0)
            {
                return foundRows[0][CategoryFieldConst.Name].ToString();
            }

            return string.Empty;
        }

        public string[] GetWeightsRange()
        {
            
            List<string> weights = new List<string>();
            string weight = string.Empty;

            for (double i = 0; i < 1.01; i = i + .01)
            {
                weight = i.ToString();
                if (weight.Length > 4)
                {
                    weight = weight.Substring(0, 4);
                }

                weights.Add(weight);
            }

            return weights.ToArray();

        }

        private bool CheckDataSet()
        {
            _ErrorMessage = string.Empty;

            if (_ds == null)
            {
                if (_DictionaryName == string.Empty)
                {
                    _ErrorMessage = "Dictionary Path has not been defined.";
                    return false;
                }

                GetDataset();
                if (_ds == null)
                    return false;

            }

            return true;
        }

        public string[] GetDictionaryItemsPerCat(int catUID)
        {
            _ErrorMessage = string.Empty;

            if (!CheckDataSet())
            {
                return null;
            }

            List<string> items = new List<string>();
            

            string selectStatment = string.Concat(DictionaryFieldConst.Category_UID,  " = ", catUID, "");

            DataRow[] foundItems = _ds.Tables[DictionaryFieldConst.TableName].Select(selectStatment);
            if (foundItems.Length > 0)
            {
                foreach (DataRow row in foundItems)
                {
                    items.Add(row[DictionaryFieldConst.Item].ToString());
                }
            }

            return items.ToArray();
        }

        public bool CategoryUpdate(int catUID, string newName)
        {
            _ErrorMessage = string.Empty;

            if (!CheckDataSet())
            {
                return false;
            }

            string selectStatment = string.Concat(CategoryFieldConst.UID,  " = ", catUID, "");

            DataRow[] foundCategory = _ds.Tables[CategoryFieldConst.TableName].Select(selectStatment);
            if (foundCategory.Length > 0)
            {
                foundCategory[0][CategoryFieldConst.Name] = newName;
                _ds.AcceptChanges();
                return true;
            }

            _ErrorMessage = "Unable to find selected Category.";
            return false;
        }

        public bool CategoryRemove(int catUID)
        {
            _ErrorMessage = string.Empty;

            if (!CheckDataSet())
            {
                return false;
            }

            foreach (DataRow row in _ds.Tables[CategoryFieldConst.TableName].Rows)
            {
                if ( Convert.ToInt32(row[CategoryFieldConst.UID].ToString()) == catUID)
                {
                    row.Delete();
                    _ds.AcceptChanges();

                    return true;
                }
            }

            _ErrorMessage = "Unable to find selected Category.";

            return false;

        }


        public bool CategoryExists(string category, out int catUID)
        {
            _ErrorMessage = string.Empty;

            if (!CheckDataSet())
            {
                catUID = -1;
                return false;
            }

            string selectStatment = string.Concat(CategoryFieldConst.Name, " = '", category, "'");

            DataRow[] foundCategory = _ds.Tables[CategoryFieldConst.TableName].Select(selectStatment);
            if (foundCategory.Length > 0)
            {
                catUID = Convert.ToInt32(foundCategory[0][CategoryFieldConst.UID].ToString());
                return true;
            }

            catUID = -1;
            return false;
        }

        public bool SetNewCategory(string category, out int catUID)
        {
            if (CategoryExists(category, out catUID))
            {
                return false;
            }

            catUID = DataFunctions.FindMaxValue(_ds.Tables[CategoryFieldConst.TableName], CategoryFieldConst.UID);

            catUID = catUID + 1;

            DataRow newCatRow = _ds.Tables[CategoryFieldConst.TableName].NewRow();

            newCatRow[CategoryFieldConst.UID] = catUID;
            newCatRow[CategoryFieldConst.Name] = category;
            

            _ds.Tables[CategoryFieldConst.TableName].Rows.Add(newCatRow);

            _ds.AcceptChanges();

            return true;
        }

        public bool SetNewSynonymArray(int Dictionary_UID, string Synonyms)
        {
            if (Synonyms.IndexOf(',') != -1)
            {
                string[] synonymsArray = Synonyms.Split(',');
                foreach (string synonym in synonymsArray)
                {
                    if (!SynonymExists(Dictionary_UID, synonym))
                    {
                        SetNewSynonym(Dictionary_UID, synonym);
                    }
                }

                return true;
            }
            else
            {
                if (!SynonymExists(Dictionary_UID, Synonyms))
                {
                    return SetNewSynonym(Dictionary_UID, Synonyms);
                }
            }

            return false;
        }

        public bool SetNewSynonym(int Dictionary_UID, string Synonym)
        {
            _ErrorMessage = string.Empty;

            if (!CheckDataSet())
                return false;

            DataRow row = _ds.Tables[SynonymsFieldConst.TableName].NewRow();

            int UID = DataFunctions.FindMaxValue(_ds.Tables[SynonymsFieldConst.TableName], SynonymsFieldConst.UID);

            row[SynonymsFieldConst.UID] = UID;
            row[SynonymsFieldConst.Item] = Synonym;
            row[SynonymsFieldConst.Dictionary_UID] = Dictionary_UID;

            _ds.Tables[SynonymsFieldConst.TableName].Rows.Add(row);

            _ds.AcceptChanges();

            return true;

        }

        public bool RemoveSynonym(int Dictionary_UID, string Synonym)
        {
            _ErrorMessage = string.Empty;

            if (!CheckDataSet())
                return false;

            string selectStatment = string.Concat(SynonymsFieldConst.Dictionary_UID, " = ", Dictionary_UID, " AND ", SynonymsFieldConst.Item, " = '", Synonym, "'");
            
            DataRow[] foundSynonym = _ds.Tables[SynonymsFieldConst.TableName].Select(selectStatment);
            if (foundSynonym.Length > 0)
            {
                foundSynonym[0].Delete();

                _ds.AcceptChanges();

                return true;
            }
            else
            {
                _ErrorMessage = string.Concat("Unable to find synonym ", Synonym);
            }

            return false;

        }

        public bool SynonymExists(int Dictionary_UID, string Synonym)
        {
            _ErrorMessage = string.Empty;

            if (!CheckDataSet())
                return false;

            string selectStatment = string.Concat(SynonymsFieldConst.Dictionary_UID, " = ", Dictionary_UID, " AND ", SynonymsFieldConst.Item, " = '", Synonym, "'");

            DataRow[] foundSynonym = _ds.Tables[SynonymsFieldConst.TableName].Select(selectStatment);
            if (foundSynonym.Length > 0)
            {
                return true;
            }

            return false;

        }


        public string[] GetSynonymsPerDicItem(int Dictionary_UID, DataSet ds)
        {
            _ds = ds;

            return GetSynonymsPerDicItem(Dictionary_UID);
        }

        public string[] GetSynonymsPerDicItem(int Dictionary_UID)
        {
            _ErrorMessage = string.Empty;

            if (!CheckDataSet())
                return null;

            List<string> synonyms = new List<string>();

            if (_ds.Tables[SynonymsFieldConst.TableName].Rows.Count == 0)
                return null;

            string searchExpression = string.Concat(SynonymsFieldConst.Dictionary_UID, " = ", Dictionary_UID.ToString());
            DataRow[] foundRows =_ds.Tables[SynonymsFieldConst.TableName].Select(searchExpression);

            foreach (DataRow row in foundRows)
            {
                synonyms.Add(row[SynonymsFieldConst.Item].ToString());
            }

            return synonyms.ToArray();

        }


        public bool SaveDicDataset(DataSet ds, string DictionaryName, string Description)
        {
            _DictionaryName = DictionaryName;

            return SaveDicDataset(ds, Description);
        }

        public bool SaveDicDataset(DataSet ds, string Description)
        {
            _ErrorMessage = string.Empty;

            if (_DictionaryName == string.Empty)
                return false;

            string file = string.Concat(_DictionaryName, ".dicx");
            string pathFile = Path.Combine(_DictionaryRootPath, file);

            try
            {
                _DataMgr.SaveDataXML(ds, pathFile);
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.Message;
            }

            if (_ErrorMessage == string.Empty)
            {
                file = string.Concat(_DictionaryName, ".txt");
                pathFile = Path.Combine(_DictionaryRootPath, file);

                Files.WriteStringToFile(Description, pathFile, true);

                return true;
            }
            else
                return false;
        }

        public int ImportConcepts(string[] concepts, string DicName, string highlightColor)
        {
            _ErrorMessage = string.Empty;

            if (concepts.Length == 0)
            {
                _ErrorMessage = "No Concept given - Unable to create an Empty Dictionary";
                return -1;
            }

            if (DicName.Trim() == string.Empty)
            {
                _ErrorMessage = "No Dictionary Name was given - Unable to create a Dictionary without an Name";
                return -1;
            }

            _DictionaryName = DicName;

            _ds = GetDataset_Empty();
            if (_ds == null)
            {
                _ErrorMessage = "Unable to create an Empty Dictionary to populate.";
                return -1;
            }

            int i = 0;
            foreach (string concept in concepts)
            {
                DataRow newRow = _ds.Tables[DictionaryFieldConst.TableName].NewRow();

                newRow[DictionaryFieldConst.UID] = i;
                newRow[DictionaryFieldConst.Category_UID] = 0;
                newRow[DictionaryFieldConst.Item] = concept;
                newRow[DictionaryFieldConst.Definition] = string.Empty;
                newRow[DictionaryFieldConst.Weight] = 0;
                newRow[DictionaryFieldConst.HighlightColor] = highlightColor;

                _ds.Tables[DictionaryFieldConst.TableName].Rows.Add(newRow);

                i++;
            }

            if (!SaveDicDataset(_ds, DicName))
            {
                return -1;
            }

            return i;

        }

        public int ImportKeywordGroup(string KeywordGroupFilePath, string DicName)
        {
           
            _ErrorMessage = string.Empty;

            if (!File.Exists(KeywordGroupFilePath))
            {
                _ErrorMessage = string.Concat("Unable to find Keyword Group file: '", KeywordGroupFilePath, "'");
                return -1;
            }

            _DictionaryName = DicName;

            _ds = GetDataset_Empty();
            if (_ds == null)
            {
                _ErrorMessage = "Unable to create an Empty Dictionary to populate.";
                return -1;
            }

            DataSet dsKwG = Files.LoadDatasetFromXml(KeywordGroupFilePath);
            string keyword = string.Empty;
            string highlightColor = string.Empty;
            int i = 0;
            foreach (DataRow rowKwG in dsKwG.Tables[0].Rows)
            {
                DataRow newRow = _ds.Tables[DictionaryFieldConst.TableName].NewRow();
                keyword = rowKwG["Keyword"].ToString();
                highlightColor = rowKwG["Color"].ToString();

                newRow[DictionaryFieldConst.UID] = i;
                newRow[DictionaryFieldConst.Category_UID] = 0;
                newRow[DictionaryFieldConst.Item] = keyword;
                newRow[DictionaryFieldConst.Definition] = string.Empty;
                newRow[DictionaryFieldConst.Weight] = 0;
                newRow[DictionaryFieldConst.HighlightColor] = highlightColor;

                _ds.Tables[DictionaryFieldConst.TableName].Rows.Add(newRow);

                i++;
            }

            if (!SaveDicDataset(_ds, DicName))
            {
                return -1;
            }

            return i;
        }

        public int ImportExcelFile(string xlsxPathFile, string sheetName, string DicName)
        {
            _ErrorMessage = string.Empty;

            if (!CheckDataSet())
                return -1;

            string newFile = string.Concat(DicName, ".dicx");
            string newPathFile = Path.Combine(_DictionaryRootPath, newFile);

            if (File.Exists(newPathFile))
            {
                _ErrorMessage = string.Concat("Dictionary '", DicName, "' already exists!");
                return -5;
            }

            _DictionaryName = DicName;

            int count = 0;

            ExcelPackage ep = new ExcelPackage(new FileInfo(xlsxPathFile));
            ExcelWorksheet ws = ep.Workbook.Worksheets[sheetName];

            string category = string.Empty;         // Column A
            string term = string.Empty;             // Column B
            string definition = string.Empty;       // Column C
            string weight = string.Empty;           // Column D
            string synonyms = string.Empty;         // Column E
            string highlightColor = string.Empty;   // Column F

            int uid = -1;
            int catUID = -1;
            string lastCategoryName = string.Empty;
            double weightX = 0;

            bool endExcelRowLooping = false;

            if (ws == null)
            {
                _ErrorMessage = "Unable to open Excel file.    To Fix: Check 'Sheet Name' for correctness.";
                return -1;
            }

            for (int i = 1; i <= ws.Dimension.End.Row; i++)
            {
                for (int j = 1; j < 7; j++)  //for (int j = ws.Dimension.Start.Column; j <= ws.Dimension.End.Column; j++)
                {
                
                    //gm.cnst_first_nm = ws.Cells[i, j].Value.ToString();
                    switch (j)
                    {
                        case 1:
                            if (ws.Cells[i, j].Value == null)
                                category = string.Empty;
                            else
                                category = ws.Cells[i, j].Value.ToString();

                            break;
                        case 2:
                            if (ws.Cells[i, j].Value == null)
                            {
                                term = string.Empty;
                                endExcelRowLooping = true;
                            }
                            else
                                term = ws.Cells[i, j].Value.ToString();
                            break;
                        case 3:
                            if (ws.Cells[i, j].Value == null)
                                definition = string.Empty;
                            else
                                definition = ws.Cells[i, j].Value.ToString();
                            break;
                        case 4:
                            if (ws.Cells[i, j].Value == null)
                                weight = "0";
                            else
                                weight = ws.Cells[i, j].Value.ToString();
                            break;
                        case 5:
                            if (ws.Cells[i, j].Value == null)
                                synonyms = string.Empty;
                            else
                                synonyms = ws.Cells[i, j].Value.ToString();
                            break;
                        case 6:
                            if (ws.Cells[i, j].Value == null)
                                highlightColor = "YellowGreen";
                            else           
                                highlightColor = ws.Cells[i, j].Value.ToString();
                                 
                            break;
                    }

                    if (endExcelRowLooping)
                        break;
                }

                // ---- HighLight Color ---- per Category
                if (highlightColor.Trim().Length == 0)
                {
                    highlightColor = "YellowGreen";
                }

                // ---- Category ----
                if (category.Trim() == string.Empty)
                {
                    catUID = 0;
                }
                else
                {
                    if (lastCategoryName != category.Trim())
                    {
                        catUID = GetCategoryUID(category.Trim());
                        if (catUID == -1) // Not found
                        {
                            SetNewCategory(category.Trim(), out catUID);
                            if (catUID == -1) // should never occur
                                catUID = 0;
                        }
                    }         
                }


                // ---- Term ----
                if (term.Trim().Length > 0)
                {
                    if (weight.Trim().Length == 0)
                    {
                        weightX = 0;
                    }
                    else
                    {
                        try
                        {
                            weightX = Convert.ToDouble(weight.Trim());
                        }
                        catch
                        {
                            weightX = 0;
                        }
                    }


                        // ---- Dictionary UI -----
                    uid = DataFunctions.FindMaxValue(_ds.Tables[DictionaryFieldConst.TableName], DictionaryFieldConst.UID);

                    uid = uid + 1;

                    DataRow newDicRow = _ds.Tables[DictionaryFieldConst.TableName].NewRow();

                    newDicRow[DictionaryFieldConst.UID] = uid;
                    newDicRow[DictionaryFieldConst.Item] = term.Trim();
                    newDicRow[DictionaryFieldConst.Category_UID] = catUID;
                    newDicRow[DictionaryFieldConst.Definition] = definition;
                    newDicRow[DictionaryFieldConst.Weight] = weightX;
                    newDicRow[DictionaryFieldConst.HighlightColor] = highlightColor;

                    _ds.Tables[DictionaryFieldConst.TableName].Rows.Add(newDicRow);

                    _ds.AcceptChanges();

                    if (synonyms.Trim().Length > 0)
                    {
                        SetNewSynonymArray(uid, synonyms.Trim());
                    }

                    count++;
                }

                if (endExcelRowLooping)
                    break;          
            }

            _Description = string.Concat("Imported Excel file: ", xlsxPathFile);

            if (!SaveDicDataset(_ds, _Description))
            {
                return -1;
            }

            //_ExcelImportLog
            return count;
        }

        //public DataTable GetDictionary(string DictionaryName)
        //{
        //    DataSet ds = GetDataset();

        //   // DataTable dt = CreateEmptyDataTable_DictionaryTrans();

        //    //foreach (DataRow row in ds.Tables[DictionaryFieldConst.TableName].Rows)
        //    //{

        //    //}

            


        //}


        


    }
}
