using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
//using OfficeOpenXml;
using System.Xml;
//using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;

using Domain.Common;
//using Domain_Dictionary;


namespace Domain.ConceptAnalyzer
{
    public class AnalysisDictionaries
    {
        public AnalysisDictionaries()
        {

        }

        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

      ///  private Domain_Dictionary.Dictionary _dicMgr;

        //     private string _UseCaseRootPath = string.Empty;

        //  private const string _XML_FILE = "AnalysisDictionaries.xml";

        //  private DataSet _dsDicAnalysis;

        //     private GenericDataManger _DataMgr = new GenericDataManger();

      /// <summary>
      ///  private RichTextBox _rtfCrtl = new RichTextBox();
      /// </summary>
      /// <returns></returns>

        public DataSet CreateEmpty_DicAnalysis_DataTable()
        {

            DataSet dsDicAnalysis = new System.Data.DataSet();


            DataTable table = new DataTable(DictionariesAnalysisFieldConst.TableName);

            table.Columns.Add(DictionariesAnalysisFieldConst.UID, typeof(int));
            table.Columns.Add(DictionariesAnalysisFieldConst.SegmentUID, typeof(int));
            table.Columns.Add(DictionariesAnalysisFieldConst.DictionaryName, typeof(string));
            table.Columns.Add(DictionariesAnalysisFieldConst.Category, typeof(string));
            table.Columns.Add(DictionariesAnalysisFieldConst.DicItem, typeof(string));
            table.Columns.Add(DictionariesAnalysisFieldConst.Definition, typeof(string));
            table.Columns.Add(DictionariesAnalysisFieldConst.Synonym, typeof(string));
            table.Columns.Add(DictionariesAnalysisFieldConst.Weight, typeof(double));
            table.Columns.Add(DictionariesAnalysisFieldConst.Count, typeof(int));
            table.Columns.Add(DictionariesAnalysisFieldConst.HighlightColor, typeof(string));


            dsDicAnalysis.Tables.Add(table);

            return dsDicAnalysis;

        }

        public DataSet CreateEmpty_DicAnalysisSummary_DataTable()
        {

            DataSet dsDicAnalysis = new System.Data.DataSet();


            DataTable table = new DataTable(DictionariesAnalysisFieldConst.TableNameSum);

            table.Columns.Add(DictionariesAnalysisFieldConst.UID, typeof(int));
            table.Columns.Add(DictionariesAnalysisFieldConst.Select, typeof(bool));
            table.Columns.Add(DictionariesAnalysisFieldConst.SegmentSumUIDs, typeof(string));
            table.Columns.Add(DictionariesAnalysisFieldConst.Category, typeof(string));
            table.Columns.Add(DictionariesAnalysisFieldConst.DicItem, typeof(string));
            table.Columns.Add(DictionariesAnalysisFieldConst.Count, typeof(int));
            table.Columns.Add(DictionariesAnalysisFieldConst.HighlightColor, typeof(string));


            dsDicAnalysis.Tables.Add(table);

            return dsDicAnalysis;

        }

        private string DefineField(string prefix, string doc)
        {
            string adjDoc = doc.Replace(' ', '_');

            string result = string.Concat(prefix, adjDoc);

            return result;

        }

        public DataSet CreateEmpty_DocsDicAnalysisSummary_DataTable(string[] docs)
        {
            _ErrorMessage = string.Empty;

            if (docs.Length == 0)
            {
                _ErrorMessage = "No Documents have been defined.";
                return null;
            }
            DataSet dsDocsDicAnalysis = new System.Data.DataSet();


            DataTable table = new DataTable(DocsDictionariesAnalysisFieldConst.TableName);

            table.Columns.Add(DocsDictionariesAnalysisFieldConst.UID, typeof(int));

            table.Columns.Add(DocsDictionariesAnalysisFieldConst.DictionaryName, typeof(string));
            table.Columns.Add(DocsDictionariesAnalysisFieldConst.Category, typeof(string));
            table.Columns.Add(DocsDictionariesAnalysisFieldConst.DicItem, typeof(string));
            table.Columns.Add(DocsDictionariesAnalysisFieldConst.Definition, typeof(string));
            table.Columns.Add(DocsDictionariesAnalysisFieldConst.Weight, typeof(double));
            table.Columns.Add(DocsDictionariesAnalysisFieldConst.HighlightColor, typeof(string));

            string totalWeightField = string.Empty;
            string countField = string.Empty;
            string synonymField = string.Empty;

            int x = 0;
            foreach (string doc in docs)
            {
                // totalWeightField = DefineField(DocsDictionariesAnalysisFieldConst.WeightTotal_, doc);
                totalWeightField = string.Concat(DocsDictionariesAnalysisFieldConst.WeightTotal_, x.ToString());
                table.Columns.Add(totalWeightField, typeof(double));

                // countField = DefineField(DocsDictionariesAnalysisFieldConst.Count_, doc);
                countField = string.Concat(DocsDictionariesAnalysisFieldConst.Count_, x.ToString());
                table.Columns.Add(countField, typeof(int));

                // synonymField = DefineField(DocsDictionariesAnalysisFieldConst.Synonym_, doc);
                synonymField = string.Concat(DocsDictionariesAnalysisFieldConst.Synonym_, x.ToString());
                table.Columns.Add(synonymField, typeof(string));

                x++;
            }

            dsDocsDicAnalysis.Tables.Add(table);

            return dsDocsDicAnalysis;

        }

        public DataSet CreateEmpty_DicAnalysis_Not_DataTable()
        {

            DataSet dsDicAnalysisNot = new System.Data.DataSet();


            DataTable table = new DataTable(DictionariesAnalysisFieldConst.XMLFileNot);

            table.Columns.Add(DictionariesAnalysisFieldConst.UID, typeof(int));
            table.Columns.Add(DictionariesAnalysisFieldConst.DictionaryName, typeof(string));
            table.Columns.Add(DictionariesAnalysisFieldConst.Category, typeof(string));
            table.Columns.Add(DictionariesAnalysisFieldConst.DicItem, typeof(string));
            table.Columns.Add(DictionariesAnalysisFieldConst.Definition, typeof(string));
            table.Columns.Add(DictionariesAnalysisFieldConst.Weight, typeof(int));

            dsDicAnalysisNot.Tables.Add(table);

            return dsDicAnalysisNot;

        }

        public DataSet Get_Documents_Dic_Summary(string ProjectName, string AnalysisName, string[] Docs, string Dictionary, string DictionariesPath, out string SumXRefPathFile, out DataSet dsDicDocsSum4Filtering)
        {
            _ErrorMessage = string.Empty;
            SumXRefPathFile = string.Empty;

            dsDicDocsSum4Filtering = null;

            if (Docs.Length == 0)
            {
                _ErrorMessage = "Documents have not been defined.";
                return null;
            }


            AppFolders_CA.ProjectName = ProjectName;
            string s = AppFolders_CA.Project;
            string projectFolder = AppFolders_CA.ProjectCurrent;

            AppFolders_CA.AnalysisName = AnalysisName;

            string analysisFolder = AppFolders_CA.AnalysisCurrent;


            DataSet dsDocsDicAnalysisSum;

            string totalDicSumPathFile = Path.Combine(analysisFolder, DocsDictionariesAnalysisFieldConst.xFilterFile);

            string docsDicAnalysisSumPathFile = Path.Combine(analysisFolder, DocsDictionariesAnalysisFieldConst.XMLFile);
            if (File.Exists(docsDicAnalysisSumPathFile))
            {
                dsDocsDicAnalysisSum = Files.LoadDatasetFromXml(docsDicAnalysisSumPathFile);
                dsDicDocsSum4Filtering = Files.LoadDatasetFromXml(totalDicSumPathFile);

                _ErrorMessage = Files.ErrorMessage;
                return dsDocsDicAnalysisSum;
            }


            dsDocsDicAnalysisSum = CreateEmpty_DocsDicAnalysisSummary_DataTable(Docs);

            dsDicDocsSum4Filtering = CreateEmpty_DicAnalysisSummary_DataTable();


           //// _dicMgr = new Domain_Dictionary.Dictionary(DictionariesPath, Dictionary);
            //_dicMgr.DictionaryName = Dictionary;
           //// DataSet dsDic = _dicMgr.Dictionary_DataSet;


           ////// DataView dv = dsDic.Tables[0].DefaultView;
           ///// dv.Sort = string.Concat(DictionaryFieldConst.Category_UID, ",", DictionaryFieldConst.Item);
           //// DataTable dtSorted = dv.ToTable();

            DataRow newRow;
            int i = 0;
            string category = string.Empty;

            // Populate Doucments Summary
            //////////foreach (DataRow rowDic in dtSorted.Rows)
            //////////{
            //////////    newRow = dsDocsDicAnalysisSum.Tables[0].NewRow();
            //////////    newRow[DocsDictionariesAnalysisFieldConst.UID] = i;
            //////////    ////////category = _dicMgr.GetCategoryName(Convert.ToInt32(rowDic[DictionaryFieldConst.Category_UID].ToString()));
            //////////    ////////newRow[DocsDictionariesAnalysisFieldConst.Category] = category;
            //////////    ////////newRow[DocsDictionariesAnalysisFieldConst.DictionaryName] = Dictionary;
            //////////    ////////newRow[DocsDictionariesAnalysisFieldConst.DicItem] = rowDic[DictionaryFieldConst.Item];
            //////////    ////////newRow[DocsDictionariesAnalysisFieldConst.Weight] = rowDic[DictionaryFieldConst.Weight];
            //////////    ////////newRow[DocsDictionariesAnalysisFieldConst.Definition] = rowDic[DictionaryFieldConst.Definition];
            //////////    ////////newRow[DocsDictionariesAnalysisFieldConst.HighlightColor] = rowDic[DictionaryFieldConst.HighlightColor];

            //////////    dsDocsDicAnalysisSum.Tables[0].Rows.Add(newRow);

            //////////    dsDocsDicAnalysisSum.AcceptChanges();

            //////////    newRow = dsDicDocsSum4Filtering.Tables[0].NewRow();
            //////////    newRow[DictionariesAnalysisFieldConst.UID] = i;
            //////////    newRow[DictionariesAnalysisFieldConst.Category] = category;
            //////////    ////////newRow[DictionariesAnalysisFieldConst.DicItem] = rowDic[DictionaryFieldConst.Item];
            //////////    //      newRow[DocsDictionariesAnalysisFieldConst.Weight] = rowDic[DictionaryFieldConst.Weight];
            //////////    //       newRow[DocsDictionariesAnalysisFieldConst.Definition] = rowDic[DictionaryFieldConst.Definition];
            //////////    ////////newRow[DictionariesAnalysisFieldConst.HighlightColor] = rowDic[DictionaryFieldConst.HighlightColor];
            //////////    newRow[DictionariesAnalysisFieldConst.Count] = 0;

            //////////    dsDicDocsSum4Filtering.Tables[0].Rows.Add(newRow);
            //////////    dsDicDocsSum4Filtering.AcceptChanges();

            //////////    i++;
            //////////}

            string analysisDocFolder = string.Empty;
            string analysisDocXMLFolder = string.Empty;
            string pathFile_DicAnalysisSummary = string.Empty;
            DataSet dsDicAnalysis;

            string dicItem = string.Empty;
            double weight;
            int count = 0;

            string fieldName_Count = string.Empty;
            string fieldName_Weight = string.Empty;

            StringBuilder sb = new StringBuilder();
            int x = 0;
            foreach (string doc in Docs)
            {
                AppFolders_CA.DocName = doc;
                analysisDocFolder = AppFolders_CA.AnalysisCurrentDocsDocName;
                analysisDocXMLFolder = AppFolders_CA.AnalysisXML;
                if (analysisDocXMLFolder == string.Empty)
                {
                    sb.AppendLine(string.Concat("Unable to find the Analysis Document XML folder: ", analysisDocXMLFolder));
                }
                else
                {
                    pathFile_DicAnalysisSummary = Path.Combine(analysisDocXMLFolder, DictionariesAnalysisFieldConst.XMLSumFile);
                    if (!File.Exists(pathFile_DicAnalysisSummary))
                    {
                        dsDicAnalysis = Get_Document_Dic_Summary(ProjectName, AnalysisName, doc);
                        // sb.AppendLine(string.Concat("Unable to find Dictionary Found Items Summary file: ", pathFile_DicAnalysisSummary));
                    }
                    else
                    {
                        dsDicAnalysis = Files.LoadDatasetFromXml(pathFile_DicAnalysisSummary);
                    }

                    if (dsDicAnalysis == null)
                    {
                        sb.AppendLine(string.Concat("Unable to load Dictionary Found Items Summary file: ", pathFile_DicAnalysisSummary));
                    }
                    else
                    {

                        foreach (DataRow rowDicAnalysis in dsDicAnalysis.Tables[0].Rows)
                        {
                            dicItem = rowDicAnalysis[DictionariesAnalysisFieldConst.DicItem].ToString();
                            count = Convert.ToInt32(rowDicAnalysis[DictionariesAnalysisFieldConst.Count].ToString());
                            // weight = Convert.ToDouble(rowDicAnalysis[DictionariesAnalysisFieldConst.Weight].ToString());

                            // Insert Summary Document Data into Documents Summary
                            if (!InsertDocData_DocsDicAnalysisSum(dsDocsDicAnalysisSum, dicItem, count, x.ToString()))
                            {
                                sb.AppendLine(string.Concat("Unable to populate Documents Summary - Dictionary Found Items for Document: ", doc, " with Item: ", dicItem));
                            }



                        } // Loop though each Document Summary Item

                    }

                }

                x++;
            } // Loop though each Doocument


            //dsDicDocsSum4Filtering

            GenericDataManger gdManager = new GenericDataManger();

            gdManager.SaveDataXML(dsDocsDicAnalysisSum, docsDicAnalysisSumPathFile);

            if (sb.ToString() != string.Empty)
            {
                string docsDicAnalysisSumErrPathFile = Path.Combine(analysisFolder, DocsDictionariesAnalysisFieldConst.ErrFile);
                Files.WriteStringToFile(sb.ToString(), docsDicAnalysisSumErrPathFile);
            }

            // summary Filtering DataSet -- Insert Total count for each Dictionary Item
            int docCount = Docs.Length;
            int qty = 0;
            int uid = -1;
            string countField = string.Empty;
            foreach (DataRow row in dsDocsDicAnalysisSum.Tables[0].Rows)
            {
                qty = 0;
                uid = Convert.ToInt32(row[DocsDictionariesAnalysisFieldConst.UID].ToString());
                for (int z = 0; z < docCount; z++)
                {
                    countField = string.Concat("Count_", z.ToString());

                    if (row[countField].ToString() != string.Empty)
                        qty = Convert.ToInt32(row[countField].ToString()) + qty;

                }

                dsDicDocsSum4Filtering.Tables[0].Rows[uid][DictionariesAnalysisFieldConst.Count] = qty;
                dsDicDocsSum4Filtering.AcceptChanges();

                //DataRow[] foundDicItems = dsDicDocsSum4Filtering.Tables[0].Select(string.Concat(DictionariesAnalysisFieldConst.UID, " = ", uid));
                //if (foundDicItems.Length != 0)
                //{
                //    foundDicItems[0][DictionariesAnalysisFieldConst.Count] = qty;
                //    dsDicDocsSum4Filtering.AcceptChanges();
                //}
            }


            gdManager.SaveDataXML(dsDicDocsSum4Filtering, totalDicSumPathFile);


            int y = 0;
            StringBuilder sbXRef = new StringBuilder();
            string value = string.Empty;
            foreach (string doc in Docs)
            {
                value = string.Concat(y.ToString(), "=", doc);
                sbXRef.AppendLine(value);
                y++;
            }

            SumXRefPathFile = Path.Combine(analysisFolder, DocsDictionariesAnalysisFieldConst.xRefFile);
            Files.WriteStringToFile(sbXRef.ToString(), SumXRefPathFile);

            Generate_Documents_Dic_Summary4RptChart(Docs, dsDocsDicAnalysisSum.Tables[0]); // Added 07.03.2019

            return dsDocsDicAnalysisSum;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Docs">String Array of Document Names</param>
        /// <param name="dtSource">DataTable is from DocsDicSumAnalysis.xml</param>
        private void Generate_Documents_Dic_Summary4RptChart(string[] Docs, DataTable dtSource)
        {
            List<double> weightTotal = new List<double>();
            for (int x = 0; x < Docs.Length; x++)
            {
                weightTotal.Add(0);
            }

            List<int> countTotal = new List<int>();
            for (int x = 0; x < Docs.Length; x++)
            {
                countTotal.Add(0);
            }

            string colName = string.Empty;
            double dbWeight = 0;
            int count = 0;

            foreach (DataRow row in dtSource.Rows)
            {

                for (int x = 0; x < Docs.Length; x++)
                {
                    colName = string.Concat("WeightTotal_", x.ToString());

                    if (row[colName] != null)
                    {
                        if (row[colName].ToString() != string.Empty)
                        {
                            dbWeight = Convert.ToDouble(row[colName].ToString());

                            weightTotal[x] = weightTotal[x] + dbWeight;
                        }
                    }

                    colName = string.Concat("Count_", x.ToString());

                    if (row[colName].ToString() != null)
                    {
                        if (row[colName].ToString() != string.Empty)
                        {
                            count = Convert.ToInt32(row[colName].ToString());

                            countTotal[x] = countTotal[x] + count;
                        }
                    }
                }
            }

            DataTable dtSums = new DataTable("Totals");

            dtSums.Columns.Add("Totals", typeof(string));
            for (int x = 0; x < Docs.Length; x++)
            {
                colName = string.Concat("WeightTotal_", x.ToString());
                dtSums.Columns.Add(colName, typeof(string));

                colName = string.Concat("Count_", x.ToString());
                dtSums.Columns.Add(colName, typeof(string));

            }
            dtSums.Columns.Add("Nothing", typeof(string));

            DataRow rowX = dtSums.NewRow();
            rowX["Totals"] = "Totals";
            for (int x = 0; x < Docs.Length; x++)
            {
                colName = string.Concat("WeightTotal_", x.ToString());
                rowX[colName] = weightTotal[x];

                colName = string.Concat("Count_", x.ToString());
                rowX[colName] = countTotal[x];

            }

            dtSums.Rows.Add(rowX);
            DataSet dsDocsTotals = new DataSet();
            dsDocsTotals.Tables.Add(dtSums);

            string pathFile = Path.Combine(AppFolders_CA.AnalysisCurrent, DocsDictionariesAnalysisFieldConst.xDocsTotalsFile);
            GenericDataManger gDataMgr = new GenericDataManger();

            gDataMgr.SaveDataXML(dsDocsTotals, pathFile);

        }

        private bool InsertDocData_DocsDicAnalysisSum(DataSet dsDocsDicAnalysisSum, string dicItem, int count, string x)
        {
            string searchExpression = string.Concat(DocsDictionariesAnalysisFieldConst.DicItem, " = '", dicItem, "'");
            DataRow[] row = dsDocsDicAnalysisSum.Tables[0].Select(searchExpression);

            if (searchExpression.Length == 0)
            {
                return false;
            }

            double weightTotal = Convert.ToDouble(row[0][DocsDictionariesAnalysisFieldConst.Weight].ToString()) * count;

            string fieldName_Count = string.Concat(DocsDictionariesAnalysisFieldConst.Count_, x);
            string fieldName_Weight = string.Concat(DocsDictionariesAnalysisFieldConst.WeightTotal_, x);

            row[0][fieldName_Count] = count;
            row[0][fieldName_Weight] = weightTotal;

            dsDocsDicAnalysisSum.Tables[0].AcceptChanges();

            return true;

        }

        public DataSet Get_Document_Dic_Summary(string ProjectName, string AnalysisName, string DocumentName)
        {
            _ErrorMessage = string.Empty;

            // Set Folders
            AppFolders_CA.ProjectName = ProjectName;
            string s = AppFolders_CA.Project;
            string projectFolder = AppFolders_CA.ProjectCurrent;

            AppFolders_CA.AnalysisName = AnalysisName;

            string analysisFolder = AppFolders_CA.AnalysisCurrent;


            AppFolders_CA.DocName = DocumentName;

            string analysisDocFolder = AppFolders_CA.AnalysisCurrentDocsDocName;

            string analysisDocXMLFolder = AppFolders_CA.AnalysisXML;
            if (analysisDocXMLFolder == string.Empty)
            {
                _ErrorMessage = string.Concat("Unable to find the Document Analysis XML folder: ", analysisDocXMLFolder);
                return null;
            }

            string pathFile_DicAnalysisSummary = Path.Combine(analysisDocXMLFolder, DictionariesAnalysisFieldConst.XMLSumFile);
            if (File.Exists(pathFile_DicAnalysisSummary))
            {
                DataSet dsDicAnalysis = Files.LoadDatasetFromXml(pathFile_DicAnalysisSummary);
                if (dsDicAnalysis == null)
                {
                    _ErrorMessage = string.Concat("Unable to load Dictionary Found Items Summary file: ", pathFile_DicAnalysisSummary);
                }

                return dsDicAnalysis;
            }
            else
            {

                string pathFile_DicAnalysis = Path.Combine(analysisDocXMLFolder, DictionariesAnalysisFieldConst.XMLFile);
                if (!File.Exists(pathFile_DicAnalysis))
                {
                    _ErrorMessage = string.Concat("Unble to find the Document Analysis Results file: ", pathFile_DicAnalysis);
                    return null;
                }

                DataSet dsDicAnalysis = Files.LoadDatasetFromXml(pathFile_DicAnalysis);

                if (dsDicAnalysis == null)
                {
                    _ErrorMessage = string.Concat("Unable to load Dictionary Found Items file: ", dsDicAnalysis);
                    return null;
                }


                DataSet dsDicSum = CreateEmpty_DicAnalysisSummary_DataTable();

                DataView dv = dsDicAnalysis.Tables[0].DefaultView;
                dv.Sort = string.Concat(DictionariesAnalysisFieldConst.Category, ",", DictionariesAnalysisFieldConst.DicItem);
                DataTable dtSorted = dv.ToTable();

                string item = string.Empty;
                string category = string.Empty;
                int count = 0;
                string highlightColor = string.Empty;
                string segUID = string.Empty;

                DataRow newRow = dsDicSum.Tables[0].NewRow();
                int i = 0;

                foreach (DataRow row in dtSorted.Rows)
                {
                    category = row[DictionariesAnalysisFieldConst.Category].ToString();
                    item = row[DictionariesAnalysisFieldConst.DicItem].ToString();
                    count = Convert.ToInt32(row[DictionariesAnalysisFieldConst.Count].ToString());
                    highlightColor = row[DictionariesAnalysisFieldConst.HighlightColor].ToString();
                    segUID = row[DictionariesAnalysisFieldConst.SegmentUID].ToString();
                    //DataRow[] Results = dsDicAnalysis.Tables[0].Select(string.Format("DicItem ='{0}'", dicItem.ToString().Replace("'", "''")));
                    DataRow[] foundDicItems = dsDicSum.Tables[0].Select(string.Concat(DictionariesAnalysisFieldConst.Category, " = '", category, "' and ", DictionariesAnalysisFieldConst.DicItem, " = '", item, "'"));
                    //DataRow[] foundDicItems = dsDicSum.Tables[0].Select(string.Concat(DictionariesAnalysisFieldConst.Category, " = '", category.ToString().Replace("'", "''"), "' and ", DictionariesAnalysisFieldConst.DicItem.ToString().Replace("'", "''"), " = '", item.ToString().Replace("'", "''"), "'"));
                    if (foundDicItems.Length == 0)
                    {
                        newRow = dsDicSum.Tables[0].NewRow();

                        newRow[DictionariesAnalysisFieldConst.UID] = i;
                        newRow[DictionariesAnalysisFieldConst.Select] = true;
                        newRow[DictionariesAnalysisFieldConst.SegmentSumUIDs] = segUID;
                        newRow[DictionariesAnalysisFieldConst.Category] = category;
                        newRow[DictionariesAnalysisFieldConst.DicItem] = item;
                        newRow[DictionariesAnalysisFieldConst.Count] = count;
                        newRow[DictionariesAnalysisFieldConst.HighlightColor] = highlightColor;

                        dsDicSum.Tables[0].Rows.Add(newRow);

                        i++;

                    }
                    else
                    {
                        count = Convert.ToInt32(foundDicItems[0][DictionariesAnalysisFieldConst.Count].ToString()) + count;
                        foundDicItems[0][DictionariesAnalysisFieldConst.Count] = count;
                        foundDicItems[0][DictionariesAnalysisFieldConst.SegmentSumUIDs] = string.Concat(foundDicItems[0][DictionariesAnalysisFieldConst.SegmentSumUIDs].ToString(), ", ", segUID);
                        dsDicSum.AcceptChanges();

                    }

                }

                GenericDataManger gdManager = new GenericDataManger();

                gdManager.SaveDataXML(dsDicSum, pathFile_DicAnalysisSummary);

                return dsDicSum;
            }

        }

        public string Get_Document_Dic_ItemsNotFound(string ProjectName, string AnalysisName, string DocumentName)
        {
            _ErrorMessage = string.Empty;

            // Set Folders
            AppFolders_CA.ProjectName = ProjectName;
            string s = AppFolders_CA.Project;
            string projectFolder = AppFolders_CA.ProjectCurrent;

            AppFolders_CA.AnalysisName = AnalysisName;

            string analysisFolder = AppFolders_CA.AnalysisCurrent;



            AppFolders_CA.DocName = DocumentName;

            string analysisDocFolder = AppFolders_CA.AnalysisCurrentDocsDocName;

            string analysisDocXMLFolder = AppFolders_CA.AnalysisXML;
            if (analysisDocXMLFolder == string.Empty)
            {
                _ErrorMessage = string.Concat("Unable to find the Document Analysis XML folder: ", analysisDocXMLFolder);
                return string.Empty;
            }

            string pathDicNotFoundFile = Path.Combine(analysisDocXMLFolder, DictionariesAnalysisFieldConst.XMLFileNot);
            if (!File.Exists(pathDicNotFoundFile))
            {
                _ErrorMessage = string.Concat("Unable to find the Document Dictionary Items Not Found file: ", analysisDocXMLFolder);
                return string.Empty;
            }

            DataSet ds = Files.LoadDatasetFromXml(pathDicNotFoundFile);
            if (ds == null)
            {
                _ErrorMessage = string.Concat("Unable to load the Document Dictionary Items Not Found file file: ", pathDicNotFoundFile);
                return string.Empty;
            }


            DataView dv = ds.Tables[0].DefaultView;
            dv.Sort = string.Concat(DictionariesAnalysisFieldConst.DicItem);
            DataTable dtSorted = dv.ToTable();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");

            int i = 1;
            string notFound = string.Empty;
            foreach (DataRow row in dtSorted.Rows)
            {
                notFound = String.Format("{0}\t{1}", i.ToString(), row[DictionariesAnalysisFieldConst.DicItem].ToString());
                sb.AppendLine(notFound);
                i++;
            }

            return sb.ToString();

        }


        public DataSet Get_Document_Dic_AnalysisResults(string ProjectName, string AnalysisName, string DocumentName, out string AnalysisResultPath, out string AnalysisResultsNotesPath)
        {
            _ErrorMessage = string.Empty;

            // Set Folders
            AppFolders_CA.ProjectName = ProjectName;
            string s = AppFolders_CA.Project;
            string projectFolder = AppFolders_CA.ProjectCurrent;

            AppFolders_CA.AnalysisName = AnalysisName;

            string analysisFolder = AppFolders_CA.AnalysisCurrent;

            AppFolders_CA.DocName = DocumentName;

            string analysisDocFolder = AppFolders_CA.AnalysisCurrentDocsDocName;

            AnalysisResultsNotesPath = AppFolders_CA.AnalysisNotes;

            AnalysisResultPath = AppFolders_CA.AnalysisParseSeg;



            string analysisDocXMLFolder = AppFolders_CA.AnalysisXML;
            if (analysisDocXMLFolder == string.Empty)
            {
                _ErrorMessage = string.Concat("Unable to find the Document Analysis XML folder: ", analysisDocXMLFolder);
                return null;
            }

            string pathAnalysisResultsFile = Path.Combine(analysisDocXMLFolder, ParseResultsFields.XMLFile);
            if (!File.Exists(pathAnalysisResultsFile))
            {
                _ErrorMessage = string.Concat("Unable to find the Document Analysis Results file: ", pathAnalysisResultsFile);
                return null;
            }

            DataSet ds = Files.LoadDatasetFromXml(pathAnalysisResultsFile);

            return ds;

        }

        private bool CheckFixDicAnalysisResults(string ParseResultsFile)
        {
            bool wasFixed = false;

            DataSet dsParseResults = Files.LoadDatasetFromXml(ParseResultsFile);

            if (!dsParseResults.Tables[0].Columns.Contains(ParseResultsFields.DictionaryItems))
            {
                dsParseResults.Tables[0].Columns.Add(ParseResultsFields.DictionaryItems, typeof(string));
                wasFixed = true;
            }

            if (!dsParseResults.Tables[0].Columns.Contains(ParseResultsFields.DictionaryDefinitions))
            {
                dsParseResults.Tables[0].Columns.Add(ParseResultsFields.DictionaryDefinitions, typeof(string));
                wasFixed = true;

            }

            if (!dsParseResults.Tables[0].Columns.Contains(ParseResultsFields.Weight))
            {
                dsParseResults.Tables[0].Columns.Add(ParseResultsFields.Weight, typeof(double));
                wasFixed = true;

            }

            if (!dsParseResults.Tables[0].Columns.Contains(ParseResultsFields.Dictionary))
            {
                dsParseResults.Tables[0].Columns.Add(ParseResultsFields.Dictionary, typeof(string));
                wasFixed = true;

            }


            if (!dsParseResults.Tables[0].Columns.Contains("Keywords"))
            {
                dsParseResults.Tables[0].Columns.Remove("Keywords");
                wasFixed = true;

            }

            if (wasFixed)
            {
                Common.GenericDataManger gMgr = new GenericDataManger();

                gMgr.SaveDataXML(dsParseResults, ParseResultsFile);

            }

            return wasFixed;

        }

        //////////public int Dictionary_Analysis(string AnalysisName, string DocumentName, string Dictionary, string DictionariesPath, string DocXMLPath, string DocParseSegPath, bool FindWholeWords, bool FindSynonyms, out int NotFoundDicItemsQty)
        //////////{
        //////////    _ErrorMessage = string.Empty;

        //////////    int count = -1;
        //////////    NotFoundDicItemsQty = -1;

        //////////    AppFolders_CA.AnalysisName = AnalysisName;
        //////////    AppFolders_CA.DocName = DocumentName;

        //////////    DataSet dsDicAnalysis = CreateEmpty_DicAnalysis_DataTable();

        //////////    string AnalysisDocParseSegPath = AppFolders_CA.AnalysisParseSeg;
        //////////    int segQty = Files.CopyFiles(DocParseSegPath, AnalysisDocParseSegPath);

        //////////    string ParseResultsFile = Path.Combine(DocXMLPath, ParseResultsFields.XMLFile);
        //////////    if (!File.Exists(ParseResultsFile))
        //////////    {
        //////////        _ErrorMessage = string.Concat("Unable to find the Parse Segments/Paragraphs file: ", ParseResultsFile);
        //////////        return count;
        //////////    }

        //////////    CheckFixDicAnalysisResults(ParseResultsFile);

        //////////    DataSet dsParseResults = Files.LoadDatasetFromXml(ParseResultsFile);


        //////////    string dictionaryFile = string.Concat(Dictionary, ".dicx");
        //////////    string dictionaryPathFile = Path.Combine(DictionariesPath, dictionaryFile);
        //////////    if (!File.Exists(dictionaryPathFile))
        //////////    {
        //////////        _ErrorMessage = string.Concat("Unable to find the Dictionary file: ", dictionaryPathFile);
        //////////        return count;
        //////////    }

        //////////    DataSet dsDic = Files.LoadDatasetFromXml(dictionaryPathFile);

        //////////    string uid_parseSeg = string.Empty;
        //////////    string fileParseSeg = string.Empty;
        //////////    string filePathParseSeg = string.Empty;
        //////////    string txtParseSeg = string.Empty;

        //////////    string uid_dic = string.Empty;
        //////////    string dicItem = string.Empty;
        //////////    string dicItems = string.Empty;
        //////////    string dicDefs = string.Empty;
        //////////    string dicColor = string.Empty;
        //////////    string dicDef = string.Empty;
        //////////    int dicCatUID = -1;
        //////////    string dicCat = string.Empty;
        //////////    double dicWeight = 0;
        //////////    double dicWeights = 0;
        //////////    int dicsFound = 0;
        //////////    int dicsFoundTotal = 0;

        //////////    string synonymItem = string.Empty;
        //////////    int synonymsFound = 0;
        //////////    int synonymsFoundTotal = 0;

        //////////    int DicAnalysis_UID = 0;

        //////////    string filter = string.Empty;

        //////////    _dicMgr = new Dictionary(DictionariesPath);
        //////////    _dicMgr.DictionaryName = Dictionary;

        //////////    foreach (DataRow rowParseSeg in dsParseResults.Tables[0].Rows)
        //////////    {
        //////////        uid_parseSeg = rowParseSeg[ParseResultsFields.UID].ToString();

        //////////        fileParseSeg = string.Concat(uid_parseSeg, ".rtf");
        //////////        filePathParseSeg = Path.Combine(AnalysisDocParseSegPath, fileParseSeg);

        //////////        if (!File.Exists(filePathParseSeg))
        //////////        {
        //////////            _ErrorMessage = string.Concat("Unable to find parse file: ", filePathParseSeg);
        //////////            return -1;
        //////////        }

        //////////        foreach (DataRow rowDicItem in dsDic.Tables[Domain_Dictionary.DictionaryFieldConst.TableName].Rows)
        //////////        {
        //////////            dicsFound = 0; // Reset to Default

        //////////            uid_dic = rowDicItem[DictionaryFieldConst.UID].ToString();
        //////////            dicItem = rowDicItem[DictionaryFieldConst.Item].ToString();
        //////////            dicColor = rowDicItem[DictionaryFieldConst.HighlightColor].ToString();
        //////////            dicDef = rowDicItem[DictionaryFieldConst.Definition].ToString();

        //////////            dicCatUID = Convert.ToInt32(rowDicItem[DictionaryFieldConst.Category_UID].ToString());
        //////////            dicCat = _dicMgr.GetCategoryName(dicCatUID);

        //////////            dicWeight = Convert.ToDouble(rowDicItem[DictionaryFieldConst.Weight].ToString());

        //////////            dicsFound = HighlightText2(filePathParseSeg, dicItem, FindWholeWords, dicColor, false);

        //////////            dicsFoundTotal = dicsFoundTotal + dicsFound;

        //////////            if (dicsFound > 0)
        //////////            {
        //////////                DataRow rowNewDicAnalysis = dsDicAnalysis.Tables[DictionariesAnalysisFieldConst.TableName].NewRow();
        //////////                rowNewDicAnalysis[DictionariesAnalysisFieldConst.UID] = DicAnalysis_UID;
        //////////                rowNewDicAnalysis[DictionariesAnalysisFieldConst.SegmentUID] = Convert.ToInt32(uid_parseSeg);
        //////////                rowNewDicAnalysis[DictionariesAnalysisFieldConst.DicItem] = dicItem;
        //////////                rowNewDicAnalysis[DictionariesAnalysisFieldConst.Synonym] = string.Empty;
        //////////                rowNewDicAnalysis[DictionariesAnalysisFieldConst.Count] = dicsFound;
        //////////                rowNewDicAnalysis[DictionariesAnalysisFieldConst.Definition] = dicDef;
        //////////                rowNewDicAnalysis[DictionariesAnalysisFieldConst.Weight] = dicWeight;
        //////////                rowNewDicAnalysis[DictionariesAnalysisFieldConst.DictionaryName] = Dictionary;



        //////////                rowNewDicAnalysis[DictionariesAnalysisFieldConst.Category] = dicCat;
        //////////                rowNewDicAnalysis[DictionariesAnalysisFieldConst.HighlightColor] = dicColor;

        //////////                dsDicAnalysis.Tables[DictionariesAnalysisFieldConst.TableName].Rows.Add(rowNewDicAnalysis);
        //////////                dsDicAnalysis.AcceptChanges();

        //////////                if (rowParseSeg[ParseResultsFields.DictionaryItems] == null)
        //////////                {
        //////////                    dicItems = string.Empty;
        //////////                }
        //////////                else
        //////////                {
        //////////                    dicItems = rowParseSeg[ParseResultsFields.DictionaryItems].ToString();
        //////////                }

        //////////                if (rowParseSeg[ParseResultsFields.DictionaryDefinitions] == null)
        //////////                {
        //////////                    dicDefs = string.Empty;
        //////////                }
        //////////                else
        //////////                {
        //////////                    dicDefs = rowParseSeg[ParseResultsFields.DictionaryDefinitions].ToString();
        //////////                }


        //////////                if (rowParseSeg[ParseResultsFields.Weight] == null)
        //////////                {
        //////////                    dicWeights = 0;
        //////////                }
        //////////                else
        //////////                {
        //////////                    if (rowParseSeg[ParseResultsFields.Weight].ToString().Trim().Length == 0)
        //////////                    {
        //////////                        dicWeights = 0;
        //////////                    }
        //////////                    else
        //////////                    {
        //////////                        dicWeights = Convert.ToDouble(rowParseSeg[ParseResultsFields.Weight].ToString());
        //////////                    }
        //////////                }


        //////////                rowParseSeg[ParseResultsFields.DictionaryItems] = string.Concat(dicItems, dicItem, " [", dicsFound.ToString(), "]", Environment.NewLine);
        //////////                rowParseSeg[ParseResultsFields.DictionaryDefinitions] = string.Concat(dicDefs, "(", dicCat, ") ", dicItem, " - ", dicDef, Environment.NewLine);
        //////////                rowParseSeg[ParseResultsFields.Dictionary] = Dictionary;
        //////////                rowParseSeg[ParseResultsFields.DictionaryCategory] = dicCat;
        //////////                rowParseSeg[ParseResultsFields.Weight] = Math.Round(dicWeights + (dicWeight * dicsFound), 2);
        //////////                dsParseResults.AcceptChanges();

        //////////                DicAnalysis_UID++;
        //////////            }

        //////////            if (FindSynonyms)
        //////////            {
        //////////                synonymsFound = 0; // Reset to Default

        //////////                filter = string.Concat(Domain_Dictionary.SynonymsFieldConst.Dictionary_UID, " = ", uid_dic);
        //////////                DataRow[] rowSynonyms = dsDic.Tables[Domain_Dictionary.SynonymsFieldConst.TableName].Select(filter);
        //////////                if (rowSynonyms.Length > 0)
        //////////                {
        //////////                    foreach (DataRow rowSynonym in rowSynonyms)
        //////////                    {
        //////////                        synonymItem = rowSynonym[Domain_Dictionary.SynonymsFieldConst.Item].ToString();

        //////////                        synonymsFound = HighlightText2(filePathParseSeg, synonymItem, FindWholeWords, dicColor, false);

        //////////                        if (synonymsFound > 0)
        //////////                        {
        //////////                            DataRow rowNewDicAnalysis = dsDicAnalysis.Tables[DictionariesAnalysisFieldConst.TableName].NewRow();
        //////////                            rowNewDicAnalysis[DictionariesAnalysisFieldConst.UID] = DicAnalysis_UID;
        //////////                            rowNewDicAnalysis[DictionariesAnalysisFieldConst.SegmentUID] = Convert.ToInt32(uid_parseSeg);
        //////////                            rowNewDicAnalysis[DictionariesAnalysisFieldConst.DicItem] = dicItem;
        //////////                            rowNewDicAnalysis[DictionariesAnalysisFieldConst.Synonym] = synonymItem;
        //////////                            rowNewDicAnalysis[DictionariesAnalysisFieldConst.Count] = synonymsFound;
        //////////                            rowNewDicAnalysis[DictionariesAnalysisFieldConst.Definition] = dicDef;
        //////////                            rowNewDicAnalysis[DictionariesAnalysisFieldConst.Weight] = dicWeight;
        //////////                            rowNewDicAnalysis[DictionariesAnalysisFieldConst.DictionaryName] = Dictionary;
        //////////                            rowNewDicAnalysis[DictionariesAnalysisFieldConst.Category] = dicCat;
        //////////                            rowNewDicAnalysis[DictionariesAnalysisFieldConst.HighlightColor] = dicColor;

        //////////                            dsDicAnalysis.Tables[DictionariesAnalysisFieldConst.TableName].Rows.Add(rowNewDicAnalysis);
        //////////                            dsDicAnalysis.AcceptChanges();


        //////////                            if (rowParseSeg[ParseResultsFields.DictionaryItems] == null)
        //////////                            {
        //////////                                dicItems = string.Empty;
        //////////                            }
        //////////                            else
        //////////                            {
        //////////                                dicItems = rowParseSeg[ParseResultsFields.DictionaryItems].ToString();
        //////////                            }

        //////////                            rowParseSeg[ParseResultsFields.DictionaryItems] = string.Concat(dicItems, dicItem, " <", synonymItem, " [", synonymsFound, "]>", Environment.NewLine);
        //////////                            rowParseSeg[ParseResultsFields.DictionaryDefinitions] = string.Concat(dicDefs, "(", dicCat, ") ", dicItem, " - ", dicDef, Environment.NewLine);
        //////////                            rowParseSeg[ParseResultsFields.Dictionary] = Dictionary;

        //////////                            rowParseSeg[ParseResultsFields.DictionaryCategory] = string.Concat(rowParseSeg[ParseResultsFields.DictionaryCategory].ToString(), ", ", dicItem, " <", synonymItem, "> - ", dicDef, Environment.NewLine);

        //////////                            rowParseSeg[ParseResultsFields.Weight] = Math.Round(dicWeights + (dicWeight * synonymsFound), 2); ;
        //////////                            dsParseResults.AcceptChanges();

        //////////                            synonymsFoundTotal = synonymsFoundTotal + synonymsFound;
        //////////                        }
        //////////                    }
        //////////                }
        //////////            }
        //////////        }
        //////////    }

        //////////    string analysisDicResultsPathFile = Path.Combine(AppFolders_CA.AnalysisXML, DictionariesAnalysisFieldConst.XMLFile);

        //////////    GenericDataManger gdManager = new GenericDataManger();

        //////////    gdManager.SaveDataXML(dsDicAnalysis, analysisDicResultsPathFile);
        //////////    if (!File.Exists(analysisDicResultsPathFile))
        //////////    {
        //////////        _ErrorMessage = string.Concat("Dictionary '", Dictionary, "' Analysis Results for document '", DocumentName, "' was not saved. -- Could not find file: ", analysisDicResultsPathFile);

        //////////        return -1;
        //////////    }

        //////////    ParseResultsFile = Path.Combine(AppFolders_CA.AnalysisXML, ParseResultsFields.XMLFile);
        //////////    gdManager.SaveDataXML(dsParseResults, ParseResultsFile);

        //////////    ToDo identfy not found
        //////////    NotFoundDicItemsQty = DicItemsNotFound(AnalysisName, DocumentName, Dictionary, DictionariesPath);

        //////////    count = dicsFoundTotal + synonymsFoundTotal;

        //////////    return count;
        //////////}

        public int DicItemsNotFound(string AnalysisName, string DocumentName, string Dictionary, string DictionariesPath)
        {
            int count = -1;

            DataSet dsNot = CreateEmpty_DicAnalysis_Not_DataTable();

            string analysisDicResultsPathFile = Path.Combine(AppFolders_CA.AnalysisXML, DictionariesAnalysisFieldConst.XMLFile);
            if (!File.Exists(analysisDicResultsPathFile))
            {
                _ErrorMessage = string.Concat("Unable to find the Dictionary Analysis file: ", analysisDicResultsPathFile);
                return count;
            }
            DataSet dsDicAnalysis = Files.LoadDatasetFromXml(analysisDicResultsPathFile);

            string dictionaryFile = string.Concat(Dictionary, ".dicx");
            string dictionaryPathFile = Path.Combine(DictionariesPath, dictionaryFile);
            if (!File.Exists(dictionaryPathFile))
            {
                _ErrorMessage = string.Concat("Unable to find the Dictionary file: ", dictionaryPathFile);
                return count;
            }
            DataSet dsDic = Files.LoadDatasetFromXml(dictionaryPathFile);


            string dicItem = string.Empty;
            string filter = string.Empty;
            int i = 0;
            int dicCatUID = -1;
            string dicCat = string.Empty;
            //////////foreach (DataRow rowDicItem in dsDic.Tables[0].Rows)
            //////////{
            //////////    dicItem = rowDicItem[Domain_Dictionary.DictionaryFieldConst.Item].ToString();
            //////////    dicItem = dicItem.Replace("'", @"\'");
            //////////    filter = string.Concat(DictionariesAnalysisFieldConst.DicItem, " = '", dicItem, "'");
            //////////    //DataRow[] rowwithDicItem = dsDicAnalysis.Tables[0].Select(filter);
            //////////    //DataRow[] Results = dsDicAnalysis.Tables[0].Select(string.Format("DicItem ='{0}'", dicItem.ToString().Replace("'", "''")));

            //////////    filter = string.Concat(DictionariesAnalysisFieldConst.DicItem, " = '", dicItem, "'");
            //////////    DataRow[] rowwithDicItem = dsDicAnalysis.Tables[0].Select(filter);
            //////////    if (rowwithDicItem.Length == 0)                    
            //////////    {
            //////////        DataRow rowNot = dsNot.Tables[0].NewRow();
            //////////        rowNot[DictionariesAnalysisFieldConst.UID] = i;
            //////////        rowNot[DictionariesAnalysisFieldConst.DictionaryName] = Dictionary;

            //////////        dicCatUID = Convert.ToInt32(rowDicItem[DictionaryFieldConst.Category_UID].ToString());
            //////////        dicCat = _dicMgr.GetCategoryName(dicCatUID);

            //////////        rowNot[DictionariesAnalysisFieldConst.Category] = dicCat;
            //////////        rowNot[DictionariesAnalysisFieldConst.DicItem] = rowDicItem[Domain_Dictionary.DictionaryFieldConst.Item].ToString();
            //////////        rowNot[DictionariesAnalysisFieldConst.Definition] = rowDicItem[Domain_Dictionary.DictionaryFieldConst.Definition].ToString();
            //////////        rowNot[DictionariesAnalysisFieldConst.Weight] = Convert.ToDouble(rowDicItem[Domain_Dictionary.DictionaryFieldConst.Weight].ToString());

            //////////        dsNot.Tables[0].Rows.Add(rowNot);

            //////////        i++;
            //////////    }
            //////////}

            count = dsNot.Tables[0].Rows.Count;

            if (count > 0)
            {
                GenericDataManger gdManager = new GenericDataManger();

                string analysisDicNotPathFile = Path.Combine(AppFolders_CA.AnalysisXML, DictionariesAnalysisFieldConst.XMLFileNot);
                gdManager.SaveDataXML(dsNot, analysisDicNotPathFile);
            }

            return count;

        }



        //////////private int HighlightText(string file, string word, string color, bool HighlightText)
        //////////{
        //////////    int count = 0;

        //////////    if (color == string.Empty)
        //////////        color = "YellowGreen";

        //////////    _rtfCrtl.Clear();

        //////////    if (!File.Exists(file))
        //////////    {
        //////////        return count;
        //////////    }

        //////////    _rtfCrtl.LoadFile(file, System.Windows.Forms.RichTextBoxStreamType.RichText);

        //////////    int s_start = _rtfCrtl.SelectionStart, startIndex = 0, index;

        //////////    while ((index = _rtfCrtl.Text.IndexOf(word, startIndex, StringComparison.CurrentCultureIgnoreCase)) != -1) // 01.04.2018 Added StringComparison.CurrentCultureIgnoreCase)) != -1)
        //////////    {
        //////////        _rtfCrtl.Select(index, word.Length);

        //////////        if (HighlightText) // Highlight Text
        //////////            _rtfCrtl.SelectionColor = Color.FromName(color);
        //////////        else // Highlight Background
        //////////            _rtfCrtl.SelectionBackColor = Color.FromName(color);

        //////////        startIndex = index + word.Length;
        //////////        count++;
        //////////    }

        //////////    if (count > 0)
        //////////        _rtfCrtl.SaveFile(file, System.Windows.Forms.RichTextBoxStreamType.RichText);


        //////////    return count;
        //////////}

        //////////private int HighlightText2(string file, string word, bool wholeWord, string color, bool HighlightText)
        //////////{
        //////////    int count = 0;

        //////////    if (color == string.Empty)
        //////////        color = "YellowGreen";

        //////////    _rtfCrtl.Clear();

        //////////    if (!File.Exists(file))
        //////////    {
        //////////        return count;
        //////////    }

        //////////    _rtfCrtl.LoadFile(file, System.Windows.Forms.RichTextBoxStreamType.RichText);

        //////////    string txt = _rtfCrtl.Text;

        //////////    string adjWord;
        //////////    if (wholeWord)
        //////////    {
        //////////        //  adjWord = "\\W?(" + word + ")\\W?";
        //////////        adjWord = @"\b(" + word + @")\b";
        //////////    }
        //////////    else
        //////////    {
        //////////        adjWord = word;
        //////////    }


        //////////    Regex regex = new Regex(adjWord, RegexOptions.IgnoreCase);

        //////////    MatchCollection matches = regex.Matches(txt);
        //////////    int index = -1;
        //////////    int startIndex = 0;

        //////////    count = matches.Count;

        //////////    if (count > 0)
        //////////    {
        //////////        // Test code
        //////////        //if (count > 1)
        //////////        //{
        //////////        //    bool what = true;
        //////////        //}

        //////////        foreach (Match match in matches)
        //////////        {
        //////////            index = match.Index;
        //////////            //if (wholeWord)
        //////////            //{
        //////////            //    _rtfCrtl.Select(index + 1, word.Length);
        //////////            //}
        //////////            //else
        //////////            //{
        //////////            _rtfCrtl.Select(index, word.Length);
        //////////            //}

        //////////            if (HighlightText) // Highlight Text
        //////////                _rtfCrtl.SelectionColor = Color.FromName(color);
        //////////            else // Highlight Background
        //////////                _rtfCrtl.SelectionBackColor = Color.FromName(color);

        //////////            startIndex = index + word.Length;
        //////////        }

        //////////        //for (int i = 0; i < count; i++)
        //////////        //{
        //////////        //    index = match.Captures[i].Index;
        //////////        //    _rtfCrtl.Select(index, word.Length);

        //////////        //    if (HighlightText) // Highlight Text
        //////////        //        _rtfCrtl.SelectionColor = Color.FromName(color);
        //////////        //    else // Highlight Background
        //////////        //        _rtfCrtl.SelectionBackColor = Color.FromName(color);

        //////////        //    startIndex = index + word.Length;

        //////////        //}

        //////////        _rtfCrtl.SaveFile(file, System.Windows.Forms.RichTextBoxStreamType.RichText);
        //////////    }


        //////////    return count;
        //////////}

    }
}
