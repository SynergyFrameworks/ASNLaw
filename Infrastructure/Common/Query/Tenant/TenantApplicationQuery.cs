using Dapper;
using Infrastructure.Common.Persistence;
using Infrastructure.Query.Tenant;

public class TenantApplicationQuery : BaseQuery<TenantApplication>
{
    private const string query = @"				
                                    SELECT
                                        a.APPLICATION_ID AS ApplicationId,
                                        a.APPLICATION_NAME AS Name,
                                        a.SETTINGS AS Settings,
                                        ta.TENANT_ID AS TenantId,
                                        ta.IS_ENABLED AS IsEnabled
                                    FROM
                                        TENANT_APPLICATION ta
                                        LEFT OUTER JOIN APPLICATION a ON ta.application_id = a.APPLICATION_ID
                                    WHERE
                                        TENANT_ID = @tenantId";


    public override QueryInfo GetQuery(Criteria criteria)
    {
        var formattedQuery = FilterAndSearchQuery(query, criteria);

        return new QueryInfo
        {
            Query = formattedQuery,
            Parameters = new DynamicParameters(criteria.Parameters)
        };
    }
}

