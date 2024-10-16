using Dapper;

namespace Infrastructure.Common.Persistence
{
    public class QueryInfo
    {
        public string Query { get; set; }
        public DynamicParameters Parameters { get; set; }
        public int? CommandTimeout { get; set; }
    }
}
