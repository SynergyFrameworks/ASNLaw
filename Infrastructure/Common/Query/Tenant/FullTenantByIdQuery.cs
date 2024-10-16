using Dapper;
using Infrastructure.Common.Persistence;

namespace Infrastructure.Query.Tenant
{
    public class FullTenantByIdQuery : BaseQuery<FullTenant>
    {
        public override QueryInfo GetQuery(Criteria critera)
        {
            const string query = @"
                SELECT
                    tenant_id AS Id,
                    Name AS Name,
                    Base_url AS BaseUrl,
                    Logo AS Logo
                FROM
                    TENANT
                WHERE
                    tenant_id = @tenantId";
            return new QueryInfo
            {
                Query = query,
                Parameters = new DynamicParameters(critera.Parameters)
            };
        }
    }
}