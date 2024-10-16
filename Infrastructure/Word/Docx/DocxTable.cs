using System.Collections.Generic;
using System.Linq;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace Word.Docx
{
    public class DocxTable : ITable
    {
        private Table _table;

        public DocxTable(Table table)
        {
            _table = table;
        }

        public IList<IRow> GetRows()
        {
            // Xceed's Table.Rows is already a collection, so we map it to DocxRow
            return _table.Rows.Select(r => new DocxRow(r)).Cast<IRow>().ToList();
        }

        public int Index
        {
            get { return _table.Index; }
        }

        public IRow InsertRow()
        {
            // Xceed has a method to insert a row
            var newRow = _table.InsertRow();
            return new DocxRow(newRow);
        }

        public IRow InsertRow(int index)
        {
            var newRow = _table.InsertRow(index);
            return new DocxRow(newRow);
        }

        public IRow CopyRow(int sourceRowIndex)
        {
            var sourceRow = _table.Rows[sourceRowIndex];
            var newRow = _table.InsertRow(sourceRow);
            return new DocxRow(newRow);
        }

        public IRow CopyRowToIndex(int sourceRowIndex, int destinationIndex)
        {
            var sourceRow = _table.Rows[sourceRowIndex];
            var newRow = _table.InsertRow(sourceRow, destinationIndex);
            return new DocxRow(newRow);
        }

        public void RemoveRow(int rowIndex)
        {
            _table.RemoveRow(rowIndex);
        }
    }
}
