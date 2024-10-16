using System;
using Infrastructure.Common.Persistence;

namespace Infrastructure.Query.User
{
    public class ListUserQuery:BaseQuery<ListUser>
    {
        private const string Query = @"SELECT 
                                          u.user_id AS Id,
                                          CONCAT(u.first_name, ' ', u.LAST_NAME) AS Name,
                                          u.last_logged_in_date AS LastLoggedInDate,
                                          u.system_user_indicator AS IsSystemUser,
                                          u.username AS Username,
                                          u.password AS Password,
                                          u.first_name AS FirstName,
                                          u.last_name AS LastName,
                                          u.email AS Email,
                                          u.inactive_indicator AS IsInactive,
                                          u.picture AS Picture
                                     FROM [user] u WITH (NOLOCK)
                                     WHERE u.tenant_id = @tenantId
                                     ";
        private const string InactiveFilter = @"AND u.inactive_indicator = 0";
        public override QueryInfo GetQuery(Criteria critera)
        {
            var query = Convert.ToBoolean(critera.Parameters["includeInactive"]) ? Query : Query + InactiveFilter;
            return new QueryInfo
            {
                Query = query,
            };
        }
    }
}
