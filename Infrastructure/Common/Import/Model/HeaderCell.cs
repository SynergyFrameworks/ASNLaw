using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Import.Model
{
    public class HeaderCell
    {
        public int ColumnNumber { get; set; }
        public Type ColumnType { get; set; }
        public string ColumnName { get; set; }
    }
}
