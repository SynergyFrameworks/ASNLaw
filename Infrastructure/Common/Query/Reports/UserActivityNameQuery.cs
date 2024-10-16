using System.CodeDom;
using Dapper;
using Infrastructure.Common.Persistence;

namespace Infrastructure.Query.Performance.Reporting
{
    public class UserActivityNameQuery : BaseQuery<UserActivityName>
    {
        private const string Query1 = @"SELECT username, 
                                               user_Id AS UserId  
                                          FROM [user] u
                                          INNER JOIN [activity] a 
                                          ON a.created_by = u.[user_Id]
                                          WHERE u.tenant_id = @tenantId ";
        private const string Filter = @"  AND a.Created_Date >= @dateId
                                          AND a.Created_Date < dateadd(day, 1, CONVERT (datetime, @dateId))
                                           ";
        private const string Query2 = @"  GROUP BY username, User_id
                                          ORDER BY username";


        //private const string DateFilter = @"
        //                                   AND a.Created_Date = @dateId ";

        public override QueryInfo GetQuery(Criteria criteria)
        {
            var query = Query1;
            if (criteria.Parameters.ContainsKey("dateId")) 
            {
                query = query + Filter;
            }
            query = query + Query2;
            return new QueryInfo
            {
                Query = query,
                Parameters = new DynamicParameters(criteria.Parameters)
            };

        }
    }
}