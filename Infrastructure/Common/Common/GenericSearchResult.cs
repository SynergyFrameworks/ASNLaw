using System.Collections.Generic;

namespace Infrastructure.Common
{
    public class GenericSearchResult<T>
    {
        public GenericSearchResult()
        {
            Results = new List<T>();
        }
        public int TotalCount { get; set; }
        public IList<T> Results { get; set; }
    }
}
