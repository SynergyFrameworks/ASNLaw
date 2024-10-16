using Infrastructure.Common.Mapping;
using System.Collections.Generic;

namespace Infrastructure
{
    public class DynamicListPageInfo : BaseListPageInfo
    {
        public IDictionary<string, object> Totals { get; set; }

        public string SetType { get; set; }
        public IList<HeaderMetadata> Metadata { get; set; }
        public IList<IDictionary<string, object>> PageData { get; set; }
        public Dictionary<string, List<KeyValuePair<string, string>>> Filters { get; set; }
    }
}
