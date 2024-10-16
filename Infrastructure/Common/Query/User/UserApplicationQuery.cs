using Dapper;
using Infrastructure.Common.Persistence;

namespace Infrastructure.Query.User
{
    public class UserApplicationQuery:BaseQuery<UserApplication>
    {
        private const string query = @"
						SELECT 
                               u.USER_ID               AS Id,
                               u.EMAIL                 AS Email,
                               u.FIRST_NAME            AS FirstName,
                               u.LAST_NAME             AS LastName,
                               u.USERNAME              AS Username,
                               u.SYSTEM_USER_INDICATOR AS IsSystemUser,
                               u.LAST_LOGGED_IN_DATE   AS LastLoggedInDate,
                               u.PICTURE               AS Picture,
                               a.APPLICATION_ID        AS ApplicationId,
                               g.GROUP_ID              AS GroupId,
                               a.APPLICATION_NAME      AS ApplicationName,
                               g.GROUP_NAME            AS GroupName,
                               a.SETTINGS              AS Settings,
                               gm.INACTIVE_INDICATOR   AS GroupMembershipIsInactive
                        FROM
                                    [USER] u WITH (NOLOCK)
                               LEFT OUTER JOIN
                                    GROUP_MEMBERSHIP gm WITH (NOLOCK)
                                        ON gm.USER_ID = u.USER_ID
                               LEFT OUTER JOIN
                                    [GROUP] g WITH (NOLOCK)
                                        ON g.GROUP_ID = gm.GROUP_ID
                               LEFT OUTER JOIN
                                    [APPLICATION] a WITH (NOLOCK)
                                        ON a.APPLICATION_ID = g.APPLICATION_ID
                               INNER JOIN
                                    [TENANT_APPLICATION] ta WITH (NOLOCK)
                                        ON ta.APPLICATION_ID = a.APPLICATION_ID
									    AND ta.APPLICATION_ID = g.APPLICATION_ID
									    AND ta.TENANT_ID = @tenantId
                        WHERE
                            u.USERNAME = @userName 
                            AND u.TENANT_ID = @tenantId
                            AND g.SYSTEM_GROUP_INDICATOR = 1
";
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
