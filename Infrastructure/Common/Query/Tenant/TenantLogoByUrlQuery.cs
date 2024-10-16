using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Infrastructure.Common.Persistence;

namespace Infrastructure.Query.Tenant
{
    public class TenantLogoByUrlQuery:BaseQuery<TenantLogoByUrl>
    {
        public override QueryInfo GetQuery(Criteria critera)
        {
            const string query = @"select * from
                                    (
                                    SELECT 
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
                                            ) AS 'BaseUrl',
                                            Logo 
	                                    from tenant) AS p
	                                    where p.BaseUrl = @baseUrl";
            return new QueryInfo
            {
                Query = query,
                Parameters = new DynamicParameters(critera.Parameters)
            };
        }
    }
}
