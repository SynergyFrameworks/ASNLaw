using System.Collections.Generic;

namespace Infrastructure.Common.Mapping
{
    public class DynamicResultSet
    {
        public string SetType { get; set; }
        public IList<HeaderMetadata> Metadata { get; set; }
        public IList<IDictionary<string, object>> Data { get; set; }
    }
}
