using Dapper;
using Infrastructure.Common.Persistence;
using Infrastructure.Common;  // Import the namespace where Tenant is located

namespace Infrastructure.Query.Tenant
{
    public class TenantByNameQuery : BaseQuery<TenantByName>
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
                    Name = @Name";
            return new QueryInfo
            {
                Query = query,
                Parameters = new DynamicParameters(critera.Parameters)
            };
        }
    }
}