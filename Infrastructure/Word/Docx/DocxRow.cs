using System.Collections.Generic;
using System.Linq;
using Xceed.Words.NET;
using Xceed.Document.NET;

namespace Word.Docx
{
    public class DocxRow : IRow
    {
        private Row _row;

        public DocxRow(Row row)
        {
            _row = row;
        }

        public IList<ICell> GetCells()
        {
            // Map Xceed Row.Cells to your ICell implementation (DocxCell)
            return _row.Cells.Select(c => new DocxCell(c)).Cast<ICell>().ToList();
        }
    }
}
