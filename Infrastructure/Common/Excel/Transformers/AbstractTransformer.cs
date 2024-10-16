using System;
using System.Collections.Generic;
using System.IO;
using Excel;

namespace Excel.Transformers
{
    public abstract class AbstractTransformer : IExcelTransformer
    {
        public IExcelManager ExcelManager { get; set; }
        public abstract object ParseExcelFile(Stream file, Dictionary<string, object> data = null);

        public virtual string CreateParsingErrorMessage(string innerMessage, int rowIndex, Exception ex, int colIndex = -1)
        {
            return colIndex == -1 ? 
                $"Error parsing {innerMessage} on row {rowIndex}: {ex.Message.TrimEnd(',', ' ', '.')}" :
                $"Error parsing {innerMessage} on row {rowIndex}, column {colIndex}: {ex.Message.TrimEnd(',', ' ', '.')}";
        }
    }
}
