using Dapper;
using Infrastructure.Common.Persistence;
using Infrastructure.Query;


namespace Infrastructure.Query
{
    public class DocumentQuery : SimpleQuery<Document>
    {
        public override void SetQuery()
        {
            query = @"
                        SELECT DISTINCT
	                        m.DOCUMENT_ID As DocumentId
                        FROM
	                        DOCUMENT_METADATA m
                        INNER JOIN
	                        DOCUMENT d ON m.DOCUMENT_ID = d.DOCUMENT_ID
                        WHERE
	                        m.[KEY] = @metadataKey
	                        AND m.VALUE in @metadataValues
	                        AND d.CONTAINER_ID in @containerIds
                    ";
        }
    }
}
