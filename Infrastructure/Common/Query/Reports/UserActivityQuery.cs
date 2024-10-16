using Dapper;
using Infrastructure.Common.Persistence;

namespace Infrastructure.Query.Performance.Reporting
{
    public class UserActivityQuery : BaseQuery<UserActivity>
    {
        //TODO: Implement server side search. Then USE GenerateSearchClause() from EL.
        private const string Query = @"select Username,
                                                        CASE 
                                                        when Parse.Parse_name is not null then concat(Activity_text, ' - ' , Parse.Parse_name) 
                                                        when task.task_name is not null then concat(Activity_text, ' - ', task.task_name) 
                                                        when investor.investor_name is not null then concat(Activity_text, ' - ', investor.investor_name)  
                                                        else Activity_text
                                                        END AS ActivityText,
                                                        a.Created_Date AS Date,
                                                        convert(char(10), a.Created_Date,108) AS Time
                                                        from [dbo].[USER] u
                                                        inner join ACTIVITY a on a.CREATED_BY = u.USER_ID
                                                        left outer join [dbo].[PARSE] on a.object_id = Parse.Parse_id
                                                        left outer join  [dbo].[TASK] on a.object_id = task.task_id
                                                        left outer join [dbo].[INVESTOR] on a.object_id = investor.investor_id
                                                        Where u.tenant_id = @tenantId";
        private const string UserFilter = @"
                                           and u.User_Id = @userId";
        private const string Date = @"
                                         and a.Created_Date <= @dateId ";


        public override QueryInfo GetQuery(Criteria criteria)
        {
            var query = Query;
            if (criteria.Parameters.ContainsKey("userId"))
            {
                query += UserFilter;
            }
            if (criteria.Parameters.ContainsKey("dateId"))
            {
                query += Date;
            }
            query += GenerateExtraClause(criteria);
            return new QueryInfo
            {
                Query = query,
                Parameters = new DynamicParameters(criteria.Parameters)
            };

        }
    }
}
