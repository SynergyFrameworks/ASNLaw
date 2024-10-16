using System.Collections.Generic;

namespace Infrastructure.Import.Model
{
    public class WorksheetRow
    {
        public int RowNumber { get; set; }
        public List<WorksheetCell> Cells { get; set; }
        public List<WorksheetRowError> Errors { get; set; }

        public WorksheetRow(int rowNumber = 0)
        {
            RowNumber = rowNumber;
            Cells = new List<WorksheetCell>();
            Errors = new List<WorksheetRowError>();
        }

        public WorksheetCell GetCell(int columnNumber)
        {
            if (Cells == null || Cells.Count == 0 || columnNumber < 1)
                return null;
            return Cells.Find(cell => cell.ColumnNumber == columnNumber);
        }
    }
}
