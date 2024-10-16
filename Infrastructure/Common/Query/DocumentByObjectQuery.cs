using Dapper;
using Infrastructure.Common.Persistence;

namespace Infrastructure.Query
{
    public class DocumentByObjectQuery : BaseQuery<DocumentByObject>
    {
        private const string query = @"
            SELECT
                d.DOCUMENT_ID AS Id,
                d.DOCUMENT_NAME AS Name,
                d.DOCUMENT_DESCRIPTION AS Description,
                d.DOCUMENT_MIME_TYPE AS MimeType,
                d.DOCUMENT_FILE_NAME AS FileName,
                CASE WHEN d.DOCUMENT_CATEGORY_ID IS NULL THEN 'General' ELSE dc.DOCUMENT_CATEGORY_DESCRIPTION END AS Category,
                d.DOCUMENT_CATEGORY_ID AS CategoryId,
                u.USERNAME AS CreatedBy,
                d.CREATED_DATE AS CreatedDate,
                d.File_Type AS FileType,
                d.URL AS Url,
                d.EXTERNAL_DOCUMENT_ID AS ExternalId,
                d.FIRST_DOWNLOADED_DATE AS DownloadedDate,
                d.ACCEPT_DATE AS AcceptedDate,
                CONCAT(acceptedu.FIRST_NAME, ' ', acceptedu.LAST_NAME) As AcceptedByName
            FROM
                DOCUMENT d WITH (NOLOCK)
                INNER JOIN [user] u WITH (NOLOCK) ON u.USER_ID = d.CREATED_BY
                LEFT OUTER JOIN DOCUMENT_CATEGORY_REF dc ON dc.DOCUMENT_CATEGORY_ID = d.DOCUMENT_CATEGORY_ID
                LEFT OUTER JOIN [USER] acceptedu ON acceptedu.USER_ID = d.ACCEPTED_BY AND acceptedu.TENANT_ID = @tenantId
            WHERE
                d.CONTAINER_ID = @containerId 
                AND d.tenant_id = @tenantId  
                AND d.inactive_indicator = 0";

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
}
