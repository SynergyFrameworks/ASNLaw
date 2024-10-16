using Dapper;
using Infrastructure.Common.Persistence;

namespace Infrastructure.Query.Tenant
{
    public class TenantByUrlQuery:BaseQuery<TenantByUrl>
    {
        public override QueryInfo GetQuery(Criteria critera)
        {
            const string query = @"
                    SELECT
                        *
                    FROM (
                            SELECT 
                                tenant_id AS Id,
                                Name AS Name,
                                LEFT(SUBSTRING(base_url, 
                                    (CASE WHEN CHARINDEX('//',base_url)=0 
                                        THEN 5 
                                        ELSE  CHARINDEX('//',base_url) + 2
                                        END), 255),
                                    (CASE 
                                    WHEN CHARINDEX('/', SUBSTRING(base_url, CHARINDEX('//', base_url) + 2, 255))=0 
                                    THEN LEN(base_url) 
                                    else CHARINDEX('/', SUBSTRING(base_url, CHARINDEX('//', base_url) + 2, 255))- 1
                                    END)
                                ) AS 'BaseUrl'
                            FROM
                                TENANT
                        ) AS p
                    WHERE
                        p.BaseUrl = @baseUrl";
            return new QueryInfo
            {
                Query = query,
                Parameters = new DynamicParameters(critera.Parameters)
            };
        }
    }
}
