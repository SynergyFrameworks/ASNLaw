using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Sorting
{
    public interface ISortingOption
    {
        string ColumnName { get; set; }
        bool IsColumnOrderDesc { get; set; }
    }
}
