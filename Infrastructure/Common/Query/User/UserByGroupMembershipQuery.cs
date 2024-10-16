using Dapper;
using Infrastructure.Common.Persistence;
using Infrastructure.Query;

namespace Infrastructure.Query.User
{
    class UserByGroupMembershipQuery : SimpleQuery<UserByGroupMembership>
    {
        private string query;

        public override void SetQuery() => query = @"
                        SELECT 
                            u.USER_ID AS UserId,
                            u.FIRST_NAME AS FirstName, 
                            u.LAST_NAME AS LastName, 
                            u.EMAIL AS Email
                        FROM 
                            [group] g
                        INNER JOIN 
                                GROUP_MEMBERSHIP gm 
                                    on g.GROUP_ID = gm.GROUP_ID
                        INNER JOIN 
                                [user] u 
                                    on u.USER_ID = gm.USER_ID
                        WHERE
                       
                                g.Group_Name = @groupName
                            AND u.TENANT_ID = @tenantId
                    ";
    }
}
