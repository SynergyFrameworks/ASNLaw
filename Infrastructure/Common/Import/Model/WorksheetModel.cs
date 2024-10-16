using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Import.Model
{
    public class WorksheetModel
    {
        public int WorksheetNumber { get; set; }
        public string Name { get; set; }
        public List<HeaderCell> Headers { get; set; } //column headers

        public List<WorksheetRow> Rows { get; set; } //rows, each row has a list of cells
        public int HeaderRowNumber { get; set; }
        public int DataRowStartNumber { get; set; }
        public List<WorksheetModelError> WorksheetErrors { get; set; }

        public WorksheetModel(int headerRowNumber = 1, int dataRowStartNumber = 2)
        {
            HeaderRowNumber = headerRowNumber;
            DataRowStartNumber = dataRowStartNumber;
            Headers = new List<HeaderCell>();
            Rows = new List<WorksheetRow>();
            WorksheetErrors = new List<WorksheetModelError>();
        }

        /// <summary>
        /// This method returns the last row number of this worksheet. For example
        /// if the data entry starts at row 2, it has 10 rows of records, then the the ending row number will be 11.
        /// </summary>
        public int GetDataRowEndingNumber()
        {
            if (Rows == null || Rows.Count == 0)
                return DataRowStartNumber;
            return DataRowStartNumber + Rows.Count - 1;
        }

        public WorksheetRow GetRow(int rowNumber)
        {
            if (HasNoDataRows() || InValidRowNumber(rowNumber))
                return null;
            return Rows.Find(row => row.RowNumber == rowNumber);
        }

        public WorksheetCell GetCell(int rowNumber, int columnNumber)
        {
            if (HasNoDataRows() || InValidRowNumber(rowNumber) || columnNumber < 1)
                return null;
            WorksheetRow row = GetRow(rowNumber);
            if (row == null)
                return null;
            return row.GetCell(columnNumber);
        }

        public WorksheetCell GetCell(int rowNumber, string columnName)
        {
            if (HasNoDataRows() || InValidRowNumber(rowNumber) || string.IsNullOrEmpty(columnName))
                return null;
            HeaderCell headerCell = Headers.Find(header => header.ColumnName == columnName);
            if (headerCell == null || headerCell.ColumnNumber < 1)
                return null;
            return GetCell(rowNumber, headerCell.ColumnNumber);
        }

        public List<WorksheetCell> GetCellsInColumn(int columnNumber)
        {
            if (columnNumber < 1)
                return null;
            var cells = new List<WorksheetCell>();
            int dataRowEndingNumber = GetDataRowEndingNumber();
            for (int rowIndex = DataRowStartNumber; rowIndex <= dataRowEndingNumber; rowIndex++)
            {
                cells.Add(GetCell(rowIndex, columnNumber));
            }
            return cells;
        }

        public HeaderCell GetHeaderCell(int columnNumber)
        {
            if (HasNoDataRows())
                return null;
            if (columnNumber < 1)
                return null;
            return Headers.Find(header => header.ColumnNumber == columnNumber);
        }

        public HeaderCell GetHeaderCell(string columnName)
        {
            if (HasNoDataRows())
                return null;
            if (string.IsNullOrEmpty(columnName))
                return null;
            return Headers.Find(header => header.ColumnName == columnName);
        }

        private bool InValidRowNumber(int rowNumber)
        {
            return rowNumber < DataRowStartNumber || rowNumber > GetDataRowEndingNumber();
        }

        private bool IsHeadersEmpty()
        {
            return Headers == null || Headers.Count == 0;
        }

        public bool HasNoDataRows()
        {
            return Rows == null || Rows.Count == 0;
        }
        /// <summary>
        /// checks type consistency for each column and set the column type for each column if it's consistent
        /// </summary> 
        public void ReinforceColumnType()
        {
            foreach (HeaderCell headerCell in Headers)
            {
                ReinforceColumnTypeForHeaderCell(headerCell);
            }
        }

        /// <summary>
        /// For a given column, checks type consistency and set the column type
        /// </summary>
        public void ReinforceColumnTypeForHeaderCell(HeaderCell headerCell)
        {
            //check types, determining the majority type
            var cellsInColumn = GetCellsInColumn(headerCell.ColumnNumber);
            if (cellsInColumn == null)
                return;

            var notNullCells = cellsInColumn.Where(cell => cell?.CellValue != null).ToList();
            if (notNullCells.Count == 0)
                return;

            var types = notNullCells.Select(cell => cell.CellValue.GetType()).ToList();
            var distinctTypes = types.Distinct().ToList();
            var columnType = distinctTypes[0];

            if (distinctTypes.Count > 1)
            {//if more then one type presents in these rows, choose the most frequent one
                var distinctTypeOccurenceCounts = new List<int>();
                foreach (Type distinctType in distinctTypes)
                {
                    distinctTypeOccurenceCounts.Add(types.Count(t => t.Equals(distinctType)));
                }
                var maxIndex = distinctTypeOccurenceCounts.IndexOf(distinctTypeOccurenceCounts.Max());
                columnType = distinctTypes[maxIndex];
            }

            headerCell.ColumnType = columnType;
        }
    }
}
