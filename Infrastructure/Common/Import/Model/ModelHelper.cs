using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Import.Model
{
    public static class ModelHelper
    {
        public static string GetExcelFormatRowColumn(this WorksheetCell cell)
        {


            if (cell.ColumnNumber > 0 && cell.RowNumber > 0)
            {

                try
                {
                    int dividend = cell.ColumnNumber;
                    string columnName = string.Empty;
                    int modulo;
                    while (dividend > 0)
                    {
                        modulo = (dividend - 1) % 26;
                        columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                        dividend = (int)((dividend - modulo) / 26);
                    }
                    return $"[{cell.RowNumber},{cell.ColumnNumber}]";

                } catch(Exception ex)
                {
                    throw new Exception("Error getting cell row number and column number", ex);
                }
            }

            return $"[{cell.RowNumber},{cell.ColumnNumber}]";
        }
    }
}
