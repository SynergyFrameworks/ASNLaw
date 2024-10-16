using Dapper;
using Infrastructure.Common.Persistence;
using Infrastructure.Query;

namespace Infrastructure.Query.User
{
    class UserGroupMembershipQuery : SimpleQuery<UserGroupMembership>
    {
        public override void SetQuery()
        {
            query = @"
                        SELECT 
                            g.GROUP_NAME AS GroupName
                        FROM 
                            [group] g
                        inner join GROUP_MEMBERSHIP gm on g.GROUP_ID = gm.GROUP_ID
                        inner join [user] u on u.USER_ID = gm.USER_ID
                        WHERE
                        (g.INACTIVE_INDICATOR is null or g.INACTIVE_INDICATOR = 0)
                        AND (gm.INACTIVE_INDICATOR is null or gm.INACTIVE_INDICATOR = 0)
                        AND u.USER_ID = @userId
                        AND u.TENANT_ID = @tenantId
                    ";
        }
    }
}
