using System;
using System.Globalization;

namespace Infrastructure.Import.Model
{
    public class WorksheetCell
    {
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
        public object CellValue { get; set; }
        public WorksheetCellError Error { get; set; }

        public string GetStringValueOrNull()
        {
            try
            {
                if (CellValue == null || string.IsNullOrEmpty(CellValue.ToString()))
                    return null;
                return CellValue.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot read cell {this.GetExcelFormatRowColumn()} as a string: {ex.Message}");
            }
        }

        public DateTime? GetDateTimeValueOrNull()
        {
            if (CellValue == null)
                return null;
            try
            {
                if (CellValue is double)
                {
                    return DateTime.FromOADate((double)CellValue); //For format like 7/11/18 6:48 PM
                }
                else
                {
                    return DateTime.Parse(CellValue.ToString()); //For regular format like 6/30/2018
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot read cell {this.GetExcelFormatRowColumn()} as a date time: {ex.Message}");
            }
        }

        public decimal GetDecimalValue()
        {
            if (string.IsNullOrEmpty(CellValue?.ToString()))
                return 0;
            try
            {
                if (!(CellValue.ToString()).Contains("E"))
                    return decimal.Parse(CellValue.ToString());
                else
                    return decimal.Parse(CellValue.ToString(), NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot read cell {this.GetExcelFormatRowColumn()} as a number: {ex.Message}");
            }
        }

        public bool GetBoolValue(string stringForTrue)
        {
            if (CellValue == null)
                return false;

            return GetStringValueOrNull()?.Trim() == stringForTrue;
        }

        public int GetIntegerValue()
        {
            if (CellValue == null)
                return 0;
            try
            {
                return int.Parse(CellValue.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot read cell {this.GetExcelFormatRowColumn()} as a number: {ex.Message}");
            }
        }

        public int? GetNullableIntegerValue()
        {
            if (CellValue == null)
                return null;
            try
            {
                return int.Parse(CellValue.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot read cell {this.GetExcelFormatRowColumn()} as a number: {ex.Message}");
            }
        }

        public decimal? GetNullableDecimalValue()
        {
            if (CellValue == null)
                return null;
            try
            {
                if (!(CellValue.ToString()).Contains("E"))
                    return decimal.Parse(CellValue.ToString());
                else
                    return decimal.Parse(CellValue.ToString(), NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot read cell {this.GetExcelFormatRowColumn()} as a number: {ex.Message}");
            }
        }
    }
}
