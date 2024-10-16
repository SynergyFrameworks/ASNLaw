using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Infrastructure.Common.Persistence;

namespace Infrastructure.Query.Tenant
{
    public class TenantKeywordMappingQuery : BaseQuery<TenantKeywordMapping>
    {
        public override QueryInfo GetQuery(Criteria critera)
        {
            const string query = @"
                SELECT
	                [KEYWORD_MAPPING_ID] AS Id,
	                [KEY_NAME] AS KeyName,
	                [KEY_NAME_PLURAL] AS KeyNamePlural,
                    [KEY_NAME_PAST] AS KeyNamePast,
	                [PREFERRED_NAME] AS PreferredName,
	                [PREFERRED_NAME_PLURAL] AS PreferredNamePlural,
                    [PREFERRED_NAME_PAST] AS PreferredNamePast
                FROM 
	                [KEYWORD_MAPPING]
                WHERE
	                [TENANT_ID] = @tenant_Id";

            return new QueryInfo
            {
                Query = query,
                Parameters = new DynamicParameters(critera.Parameters)
            };
        }
    }
}