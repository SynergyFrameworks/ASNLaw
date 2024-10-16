using Dapper;
using Infrastructure.Common.Persistence;
using Infrastructure.Common;

namespace Infrastructure.Query.Tenant
{
    public class TenantByIdQuery : BaseQuery<Infrastructure.Common.Tenant>
    {
        public override QueryInfo GetQuery(Criteria critera)
        {
            const string query = @"
                SELECT
                    tenant_id AS Id,
                    Name AS Name,
                    Base_url AS BaseUrl
                FROM
                    TENANT
                WHERE
                    TENANT_ID = @specifiedTenantId";
            return new QueryInfo
            {
                Query = query,
                Parameters = new DynamicParameters(critera.Parameters)
            };
        }
    }
}