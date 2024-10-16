using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Persistence
{
    public class TableQueryParameters
    {
        public enum Comparators { LessThan, GreaterThan, EqualTo, LessThanOrEqualTo, GreaterThanOrEqualTo, StartsWith }
        public string PropertyName { get; set; }
        public object Value { get; set; }
        public Comparators Comparator { get; set; }
    }
}
