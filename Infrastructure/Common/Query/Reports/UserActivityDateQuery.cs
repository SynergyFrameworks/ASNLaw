using Dapper;
using Infrastructure.Common.Persistence;

namespace Infrastructure.Query.Performance.Reporting
{
    public class UserActivityDateQuery : BaseQuery<UserActivityDate>
    {
        private const string Query = @"SELECT DISTINCT(CAST(a.created_date AS VARCHAR (12))) AS [Date]
                                         FROM Activity a
                                         WHERE a.tenant_id = @tenantId
                                            {0}
                                         ORDER BY [Date] desc";


        private const string UserFilter = @"
                                           AND a.Created_By = @userId";

        public override QueryInfo GetQuery(Criteria criteria)
        {
            var query = Query;
            var filter = "";
            if (criteria.Parameters.ContainsKey("userId"))
            {
                filter += UserFilter;
            }
            query = string.Format(query, filter);
            return new QueryInfo
            {
                Query = query,
                Parameters = new DynamicParameters(criteria.Parameters)
            };

        }
    }
}