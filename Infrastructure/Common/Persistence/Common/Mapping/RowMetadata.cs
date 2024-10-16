using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Persistence.Common.Mapping
{
   public class RowMetadata
    {
        public int ColumnIndex { get; set; }
        public int RowIndex { get; set; }
        public string RangeValue { get; set; }
    }
}
