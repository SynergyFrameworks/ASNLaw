using Dapper;
using Infrastructure.Common.Persistence;
using Infrastructure.Query.Settings;

public class ApplicationViewQuery : BaseQuery<ApplicationView>
{
    private const string query = @"
					SELECT 
                        [VIEW].VIEW_ID As Id,
	                    [VIEW].VIEW_NAME AS Name
                    FROM 
                        [VIEW] LEFT OUTER JOIN
                        [TENANT_HIDDEN_VIEW] on [VIEW].[VIEW_ID] = [TENANT_HIDDEN_VIEW].[VIEW_ID] AND [TENANT_HIDDEN_VIEW].[TENANT_ID] = @tenantId
                    WHERE
	                    APPLICATION_ID = @applicationId AND [TENANT_HIDDEN_VIEW].[TENANT_ID] is null";



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

