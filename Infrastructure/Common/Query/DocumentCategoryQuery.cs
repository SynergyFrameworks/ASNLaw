using Dapper;
using Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Query
{
    class DocumentCategoryQuery : BaseQuery<DocumentCategory>
    {
        private const string Query = @"
                                        SELECT
	                                        dcr.DOCUMENT_CATEGORY_ID AS Id,
	                                        dcr.DOCUMENT_CATEGORY_CODE AS Code,
	                                        dcr.DOCUMENT_CATEGORY_DESCRIPTION AS [Description],
	                                        dcr.DOCUMENT_CATEGORY_TYPE AS [Type],
	                                        dcr.DISPLAY_ORDER AS DisplayOrder,
	                                        dcr.INACTIVE_INDICATOR AS IsInactive,
	                                        dcr.CREATED_DATE AS CreatedDate,
	                                        dcr.LAST_MODIFIED_DATE AS LastModifiedDate
                                        FROM
	                                        DOCUMENT_CATEGORY_REF dcr
                                        WHERE
	                                        dcr.tenant_id = @tenantId
	                                        AND (@subType IS NULL OR dcr.DOCUMENT_CATEGORY_TYPE = @subType)
                                        ";

        public override QueryInfo GetQuery(Criteria criteria)
        {
            RequiredParameters = new[] { "subType" };

            return new QueryInfo
            {
                Query = FilterAndSearchQuery(Query, criteria),
                Parameters = new DynamicParameters(criteria.Parameters)
            };
        }
    }
}
