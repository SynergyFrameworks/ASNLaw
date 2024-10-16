using Dapper;
using Infrastructure.Common.Persistence;

namespace Infrastructure.Query
{
    public class ActivityByObjectQuery:BaseQuery<ActivityByObject>
    {
        private const string Query = @"SELECT a.activity_id AS Id,
	                                          a.activity_text AS ActivityText,
                                              a.CHANGE_REPORT As ChangeReportJson,
	                                          a.[object_id] AS ObjectId,
                                              a.Created_date AS CreatedDate,
	                                          CONCAT(u.first_name,' ',u.last_name) AS CreatedBy,
                                              u.picture AS Picture
	                                     FROM activity a
	                                     INNER JOIN [user] u
	                                     ON u.user_id = a.created_by
                                         WHERE a.[object_id] = @objectId
                                       ";

        public override QueryInfo GetQuery(Criteria criteria)
        {
            var query = Query;
            query += GenerateSearchClause(criteria);
            query += GenerateExtraClause(criteria);

            return new QueryInfo
            {
                Query = query,
                Parameters = new DynamicParameters(criteria.Parameters)
            };
        }

        public override QueryInfo GetCountQuery(Criteria criteria)
        {
            var query = @"SELECT COUNT(1) AS Count
                            FROM activity
                            WHERE [object_id] = @objectId";
            return new QueryInfo
            {
                Query = query,
                Parameters = new DynamicParameters(criteria.Parameters)
            };
        }
    }
}
