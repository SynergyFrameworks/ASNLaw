using Infrastructure.Common.Persistence;
using Dapper;

namespace Infrastructure.Query.Reports
{
    public class PowerBIReportQuery : BaseQuery<PowerBIReport>
    {
        private const string query = @"
            SELECT
                r.REPORT_ID AS ReportId,
                r.REPORT_NAME AS ReportName,
                r.EXTERNAL_REPORT_ID AS ExternalReportId,
                r.REPORT_WORKSPACE_ID AS WorkspaceId
            FROM
                REPORT r WITH (NOLOCK)
                INNER JOIN REPORT_CATEGORY rc WITH (NOLOCK) ON r.REPORT_CATEGORY_ID = rc.REPORT_CATEGORY_ID
            WHERE
                rc.INACTIVE_INDICATOR = 0
                AND r.INACTIVE_INDICATOR = 0
                AND r.TENANT_ID = @tenantId
                AND rc.REPORT_CATEGORY_CODE = 'Power BI Reports'
                AND (@externalReportId IS NULL OR r.EXTERNAL_REPORT_ID = @externalReportId)";

        public override QueryInfo GetQuery(Criteria criteria)
        {
            RequiredParameters = new[] { "externalReportId" };
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
