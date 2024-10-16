using Infrastructure.Common.Persistence;
using Dapper;

namespace Infrastructure.Query.Reports
{
    public class ReportCategoryQuery : BaseQuery<ReportCategory>
    {
        private const string query = @"
            SELECT
                rc.REPORT_CATEGORY_ID AS ReportCategoryId,
                rc.REPORT_CATEGORY_CODE AS Code,
                rc.REPORT_CATEGORY_DESCRIPTION AS Description,
                rc.DISPLAY_ORDER AS DisplayOrder,
                rc.CSS_STYLE AS CssStyle,
                rc.APPLICATION_ID,
                CONCAT('[',STUFF((
                    SELECT
                        ',{""ReportId"":""' +
                        CONVERT(NVARCHAR(50), r.REPORT_ID) +
                        '"",""Name"":""' +
                        r.REPORT_NAME +
                        '"",""Description"":""' +
                        COALESCE(r.REPORT_DESCRIPTION, '') +
                        '"",""RouteUri"":""' +
                        COALESCE(r.ROUTE_URI, '') +
                        '"",""DisplayOrder"":""' +
                        CONVERT(NVARCHAR(50), r.DISPLAY_ORDER) + '""}'
                    FROM
                        REPORT r WITH (NOLOCK)
                    WHERE
                        r.REPORT_CATEGORY_ID = rc.REPORT_CATEGORY_ID
                        AND r.TENANT_ID = @tenantId
                    ORDER BY
                        r.DISPLAY_ORDER
                FOR XML PATH, TYPE).value('.[1]', 'varchar(max)'), 1, 1, ''), ']') AS ReportJson
            FROM
                REPORT_CATEGORY rc WITH (NOLOCK)
            WHERE
                rc.APPLICATION_ID = @applicationId
                AND rc.TENANT_ID = @tenantId
                AND rc.INACTIVE_INDICATOR = 0
            ORDER BY
                rc.DISPLAY_ORDER";

        public override QueryInfo GetQuery(Criteria criteria)
        {
            if (criteria == null)
            {
                criteria = new Criteria();
            }

            return new QueryInfo
            {
                Query = query,
                Parameters = new DynamicParameters(criteria.Parameters)
            };
        }
    }
}
