using Dapper;
using Infrastructure.Common.Persistence;
using Infrastructure.Query.Settings;

public class ApplicationViewItemQuery : BaseQuery<ApplicationViewItem>
{
    private const string query = @"
					SELECT 
                        VI.VIEW_ITEM_ID As Id,
	                    VI.VIEW_ITEM_NAME AS Name,
	                    COALESCE(VIT.VIEW_ITEM_TENANT_VALUE, VI.VIEW_ITEM_DEFAULT_VALUE) AS Value
                    FROM 
                        [VIEW_ITEM] VI LEFT OUTER JOIN
                        [VIEW_ITEM_TENANT] VIT ON VIT.VIEW_ITEM_ID = VI.VIEW_ITEM_ID AND VIT.TENANT_ID = @tenantId 
                    WHERE
	                    VIEW_ID = @viewId";


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

