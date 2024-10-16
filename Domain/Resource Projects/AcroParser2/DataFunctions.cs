using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Globalization;


namespace AcroParser2
{
    public static class DataFunctions
    {

        public static DataSet CreateDataSet(DataView dv)
        {
            DataSet ds = new DataSet();
            DataTable dt = dv.Table;
            ds.Tables.Add(dt);

            return ds;

        }

        public static string GetUniqueCode()
        {

            Guid id = Guid.NewGuid();

            return id.ToString(); ;

        }

        public static bool ColumnExists(string columnName, DataTable table) // added 05.02.2014
        {
            DataColumnCollection columns = table.Columns;

            if (columns.Contains(columnName))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool IsNumeric(string value)
        {
            int result;
            if (int.TryParse(value, out result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsNumeric2(string value)
        {
            return value.All(char.IsNumber);
        }

        public static bool isAcronymPlural(string Acronym)
        {
            bool acronymPlural = false;

            if (Acronym.Length == 0 || Acronym.Length < 3)
                return acronymPlural;

            if (Acronym.Contains("'s"))
            {
                return acronymPlural;
            }
            else if (Acronym.Contains("’s"))
            {
                return acronymPlural;
            }

            char c = Acronym[Acronym.Length - 1];

            if (Char.IsLower(c))
            {
                acronymPlural = true;
            }


            return acronymPlural;
        }

        public static ArrayList RemoveDups(ArrayList noDups)
        {
            ArrayList trimmage = new ArrayList();
            foreach (string strItem in noDups)
            {
                if (!trimmage.Contains(strItem.Trim()))
                {
                    trimmage.Add(strItem.Trim());
                }
            }
            return trimmage;
        }

        /// <summary>
        /// Finds the Arrary Index Number to the seached value
        /// </summary>
        /// <param name="sArrary">String Array to be sreached</param>
        /// <param name="sValue">The Value to be searched</param>
        /// <returns>Returns -1 if Not Found, otherwise if found it returns the Array Index Number</returns>
        public static int FindValueInArray(string[] sArrary, string sValue)
        {
            for (int i = 0; i < sArrary.Length; i++)
            {
                if (sValue == sArrary[i])
                    return i;
            }

            return -1;
        }

        /// <summary>
        ///  Filter with a RegEx should work to remove invalid characters
        ///  Reference: http://www.west-wind.com/weblog/posts/2012/Jan/02/XmlWriter-and-lower-ASCII-characters
        /// </summary>
        /// <param name="DirtyString"></param>
        /// <returns></returns>
        public static string RemoveInvalidCharacters(string DirtyString)
        {
            string cleanedString = Regex.Replace(DirtyString, @"[\u0000-\u0008,\u000B,\u000C,\u000E-\u001F]", "");

            return cleanedString;
        }

        public static string ReplaceInvalidCharacters4OpenXML(string DirtyString)
        {
            // string cleanedString = DirtyString;


            //Character Name	        Entity Reference	Character Reference	    Numeric Reference
            //Ampersand	                    &amp;	            &	                    &#38;#38;
            //Left angle bracket	        &lt;	            <	                    &#38;#60;
            //Right angle bracket	        &gt;	            >	                    &#62;
            //Straight quotation mark	    &quot;	            "	                    &#39;
            //Apostrophe	                &apos;	            '	                    &#34;


            // Use Numeric References for encoding in OpenXML
            string cleanedString = Regex.Replace(DirtyString, "&", "&#38;#38;");

            cleanedString = Regex.Replace(cleanedString, "<", "&#38;#60;");
            cleanedString = Regex.Replace(cleanedString, ">", "&#62;");
            cleanedString = Regex.Replace(cleanedString, "'", "&#34;");
            cleanedString = Regex.Replace(cleanedString, "\"", "&#39;");

            //  string cleanedString = System.Security.SecurityElement.Escape(DirtyString);

            //    string cleanedString = DirtyString.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;");

            //   string cleanedString = DirtyString.Replace("&", "&amp;");
            return cleanedString;
        }

        public static DataSet CreateDataSetFromDataGridView(DataGridView dataGridView1)
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                if (!dt.Columns.Contains(dataGridView1.Columns[i].HeaderText)) // Check for Dups -- Added 02.23.2013
                {
                    DataColumn column = new DataColumn(dataGridView1.Columns[i].HeaderText);
                    dt.Columns.Add(column);
                }
            }
            int noOfColumns = dataGridView1.Columns.Count;
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                //Create table and insert into cell value.
                DataRow dataRow = dt.NewRow();

                for (int i = 0; i < noOfColumns; i++)
                {
                    if (dr.Cells[i].Value == null)
                    {
                        dataRow[i] = string.Empty;
                    }
                    else
                    {
                        dataRow[i] = dr.Cells[i].Value.ToString();
                    }
                }

                dt.Rows.Add(dataRow);
            }


            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            return ds;
        }

        public static DataTable CreateDataTableFromDataGridView(DataGridView dgv, out string Headers, out string HeaderWidths)
        {
            int counter = 0;
            StringBuilder sbH = new StringBuilder();
            StringBuilder sbW = new StringBuilder();

            DataTable dt = new DataTable();
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.Visible && col.Width > 0)
                {
                    dt.Columns.Add(col.HeaderText);
                    if (counter == 0)
                    {
                        sbH.Append(col.HeaderText);
                        sbW.Append(col.Width.ToString());
                    }
                    else
                    {
                        sbH.Append("|" + col.HeaderText);
                        sbW.Append("|" + col.Width.ToString());
                    }

                    counter++;
                }


            }

            int i = 0;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                DataRow dRow = dt.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Visible && dgv.Columns[cell.ColumnIndex].Width > 0)
                    {
                        //dRow[cell.ColumnIndex] = cell.Value;
                        dRow[i] = cell.Value;
                        i++;
                    }
                }
                i = 0; // Reset to 1st Column
                dt.Rows.Add(dRow);
            }

            Headers = sbH.ToString();
            HeaderWidths = sbW.ToString();
            return dt;
        }

        public static DataTable RemoveHiddedenColsDataGridView(DataTable dt, DataGridView dgv)
        {
            DataTable adjDT = dt;

            for (int i = 0; i <= dgv.Rows.Count; i++)
            {
                if (i < adjDT.Columns.Count)
                {
                    if (!dgv.Columns[i].Visible)
                        adjDT.Columns.Remove(adjDT.Columns[i]);
                }
            }

            return adjDT;
        }

        public static int LoadComboBox(string dir, ComboBox cbo)
        {
            cbo.Items.Clear();

            if (!Directory.Exists(dir))
                return 0;

            string[] files = Directory.GetFiles(dir);

            int i = 0;
            string fileNameNoExt = string.Empty;
            string selPathFile = string.Empty;

            foreach (string fileName in files)
            {
                selPathFile = Files.GetFileName(fileName);
                if (selPathFile.IndexOf('~') < 0)
                {
                    cbo.Items.Add(selPathFile);
                    i++;
                }
            }

            return i;
        }


        public static List<string> FindDuplicates(List<string> lstValue) // added 10.13.2013
        {
            var lstDuplicates = new List<string>();
            var hashset = new HashSet<string>();
            foreach (var value in lstValue)
            {
                if (!hashset.Add(value))
                {
                    lstDuplicates.Add(value);
                }
            }

            return lstDuplicates;
        }

        public static string[] RemoveDuplicates(string[] items)
        {
            ArrayList noDupsArrList = new ArrayList();
            for (int i = 0; i <= items.Length - 1; i++)
            {
                if (!noDupsArrList.Contains(items[i].Trim()))
                {
                    noDupsArrList.Add(items[i].Trim());
                }
            }

            string[] uniqueItems = new string[noDupsArrList.Count];
            noDupsArrList.CopyTo(uniqueItems);
            return uniqueItems;
        }

        public static bool IsValidEmail(string email)
        {
            bool isvalid = false;

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
                isvalid = true;

            return isvalid;
        }

        private static string ConverArray2Delimted(string[] sArray, string delimted)
        {
            string returnValue = string.Empty;
            int count = 0;
            foreach (string s in sArray)
            {
                if (count == 0)
                    returnValue = s;
                else
                    returnValue = string.Concat(returnValue, "|", s);
            }

            return returnValue;
        }



        ///// <summary>
        ///// Not TESTED
        ///// </summary>
        ///// <param name="obDataView"></param>
        ///// <returns></returns>
        //public static DataTable CreateTable(DataView obDataView)
        //{
        //    if (null == obDataView)
        //    {
        //        throw new ArgumentNullException
        //        ("DataView", "Invalid DataView object specified");
        //    }

        //    DataTable obNewDt = obDataView.Table.Clone();
        //    int idx = 0;
        //    string[] strColNames = new string[obNewDt.Columns.Count];
        //    foreach (DataColumn col in obNewDt.Columns)
        //    {
        //        strColNames[idx++] = col.ColumnName;
        //    }

        //    IEnumerator viewEnumerator = obDataView.GetEnumerator();
        //    while (viewEnumerator.MoveNext())
        //    {
        //        DataRowView drv = (DataRowView)viewEnumerator.Current;
        //        DataRow dr = obNewDt.NewRow();
        //        try
        //        {
        //            foreach (string strName in strColNames)
        //            {
        //                dr[strName] = drv[strName];
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Trace.WriteLine(ex.Message);
        //        }
        //        obNewDt.Rows.Add(dr);
        //    }

        //    return obNewDt;
        //}

    }
}
