using Infrastructure.Common.Persistence;
using Dapper;

namespace Infrastructure.Query.Reports
{
    public class ReportQuery : BaseQuery<Report>
    {
        private const string query = @"
            SELECT
                r.REPORT_ID AS ReportId,
                r.REPORT_NAME AS ReportName,
                r.REPORT_DESCRIPTION AS ReportDescription,
                r.ROUTE_URI AS RouteUri,
                r.EXTERNAL_REPORT_ID AS ExternalReportId,
                r.DISPLAY_ORDER AS ReportDisplayOrder,
                rc.REPORT_CATEGORY_ID AS ReportCategoryId,
                rc.REPORT_CATEGORY_CODE AS ReportCategoryName,
                rc.REPORT_CATEGORY_DESCRIPTION AS ReportCategoryDescription,
                rc.CSS_STYLE AS ReportCategoryCssStyle,
                rc.DISPLAY_ORDER AS ReportCategoryDisplayOrder,
                CONCAT('[',STUFF((
                    SELECT
                        ',{""ReportParameterId"":""' +
                        CONVERT(NVARCHAR(50), rp.REPORT_PARAMETER_ID) +
                        '"",""Name"":""' +
                        rp.REPORT_PARAMETER_NAME +
                        '"",""Description"":""' +
                        COALESCE(rp.REPORT_PARAMETER_DESCRIPTION, '') +
                        '"",""DataType"":""' +
                        dtr.DATA_TYPE_CODE +
                        '"",""StringValue"":""' +
                        COALESCE(rp.STRING_VALUE, '') +
                        '"",""DecimalValue"":""' +
                        COALESCE(CONVERT(NVARCHAR(50), rp.DECIMAL_VALUE), '') +
                        '"",""IntegerValue"":""' +
                        COALESCE(CONVERT(NVARCHAR(50), rp.INTEGER_VALUE), '') +
                        '"",""DisplayOrder"":""' +
                        CONVERT(NVARCHAR(50), rp.DISPLAY_ORDER) + '""}'
                    FROM
                        REPORT_PARAMETER rp WITH (NOLOCK)
                        INNER JOIN DATA_TYPE_REF dtr WITH (NOLOCK)
                            ON rp.DATA_TYPE_ID = dtr.DATA_TYPE_ID
                    WHERE
                        rp.REPORT_ID = r.REPORT_ID
                        AND rp.TENANT_ID = @tenantId
                    ORDER BY
                        rp.DISPLAY_ORDER
                FOR XML PATH, TYPE).value('.[1]', 'varchar(max)'), 1, 1, ''), ']') AS ParameterJson
            FROM
                REPORT r WITH (NOLOCK)
                INNER JOIN REPORT_CATEGORY rc WITH (NOLOCK)
                    ON r.REPORT_CATEGORY_ID = rc.REPORT_CATEGORY_ID
            WHERE
                rc.INACTIVE_INDICATOR = 0
                AND r.INACTIVE_INDICATOR = 0
                AND r.TENANT_ID = @tenantId
                AND (@routeUri IS NULL OR r.ROUTE_URI = @routeUri)
                AND (@applicationId IS NULL OR rc.APPLICATION_ID = @applicationId)

            ORDER BY
                rc.DISPLAY_ORDER,
                r.DISPLAY_ORDER";

        public override QueryInfo GetQuery(Criteria criteria)
        {
            RequiredParameters = new[] { "routeUri", "applicationId" };
            if (criteria == null)
            {
                criteria = new Criteria();
            }
            EnsureDefaultParameters(criteria);

            return new QueryInfo
            {
                Query = query,
                Parameters = new DynamicParameters(criteria.Parameters)
            };
        }
    }
}
