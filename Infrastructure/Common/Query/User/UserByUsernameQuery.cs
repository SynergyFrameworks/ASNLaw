using Dapper;
using Infrastructure.Common.Persistence;

namespace Infrastructure.Query.User
{
    public class UserByUsernameQuery: BaseQuery<UserByUsername>
    {
        private const string query = @"
						SELECT 
                               u.user_id    AS Id, 
                               u.email      AS Email, 
                               u.first_name AS FirstName, 
                               u.last_name  AS LastName, 
                               u.username   AS Username 
                        FROM   
                               [user] u WITH (nolock) 
                        WHERE  
                               u.username = @userName 
                               AND u.tenant_id = @tenantId  ";
        public override QueryInfo GetQuery(Criteria criteria)
        {
            return new QueryInfo
            {
                Query = query,
                Parameters = new DynamicParameters(criteria.Parameters)
            };
        }
    }
}
