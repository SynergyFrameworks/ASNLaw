using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace Domain.Common
{
    public class NonStaticFunctions
    {

        public int FindMaxValue(DataTable dt, string ColumnName)
        {
            int maxVal = 0;

            string filter = string.Concat(ColumnName, "= Max(", ColumnName, ")"); // Example: "ROWNUM= MAX(ROWNUM)"
            DataRow[] dr = dt.Select(filter);

            if (dr != null && dr.Length > 0)
            {
                // Console.WriteLine(dr[0]["RowNum"]);
                maxVal = Convert.ToInt32(dr[0][ColumnName]);
            }

            return maxVal;
        }

        public DataRow[] GetDupsRows(DataTable dt, string xValue, string ColumnName)
        {
            try
            {
                //  DataRow[] DupsRows = dt.Select(string.Format("{0} LIKE '%{1}%'", ColumnName, xValue));
                DataRow[] DupsRows = dt.Select(string.Format("{0} = '{1}'", ColumnName, xValue)); // Changed 2.15.2017

                return DupsRows;
            }
            catch
            {
                return null;
            }
        }

    }
}
