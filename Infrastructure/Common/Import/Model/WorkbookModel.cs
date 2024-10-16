using System.Collections.Generic;

namespace Infrastructure.Import.Model
{
    public class WorkbookModel
    {
        public List<WorksheetModel> Worksheets { get; set; }
        public List<ParseError> WorkbookErrors { get; set; }
        public int HeaderRowNumber { get; set; }
        public int DataRowStartNumber { get; set; }
        public string FileType { get; set; }
        public string Name { get; set; }

        public WorkbookModel()
        {
            Worksheets = new List<WorksheetModel>();
            WorkbookErrors = new List<ParseError>();
        }

        public WorksheetModel GetWorksheet(int worksheetNumber)
        {
            if (IsWorksheetsEmpty() || worksheetNumber < 1)
                return null;
            return Worksheets.Find(worksheet => worksheet.WorksheetNumber == worksheetNumber);
        }

        public WorksheetModel GetWorksheet(string worksheetName)
        {
            if (IsWorksheetsEmpty() || string.IsNullOrEmpty(worksheetName))
                return null;
            return Worksheets.Find(worksheet => worksheet.Name == worksheetName);
        }

        public WorksheetCell GetCell(int worksheetNumber, int rowNumber, int columnNumber)
        {
            if (worksheetNumber < 1 || rowNumber < 1 || columnNumber < 1)
                return null;
            WorksheetModel worksheet = GetWorksheet(worksheetNumber);
            if (worksheet == null)
                return null;
            return worksheet.GetCell(rowNumber, columnNumber);
        }

        public WorksheetCell GetCell(int worksheetNumber, int rowNumber, string columnName)
        {
            if (worksheetNumber < 1 || rowNumber < 1 || string.IsNullOrEmpty(columnName))
                return null;
            WorksheetModel worksheet = GetWorksheet(worksheetNumber);
            if (worksheet == null)
                return null;
            return worksheet.GetCell(rowNumber, columnName);
        }

        public bool IsWorksheetsEmpty()
        {
            return Worksheets == null || Worksheets.Count == 0;
        }
    }
}
