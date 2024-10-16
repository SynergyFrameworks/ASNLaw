using Infrastructure.Common.Persistence;
using Dapper;

namespace Infrastructure.Query.User
{
    public class GroupUserQuery : BaseQuery<GroupUser>
    {
        private const string query = @"
			SELECT
                u.USER_ID AS Id,
                CONCAT(u.first_name, ' ', u.LAST_NAME) AS Name,
                u.LAST_LOGGED_IN_DATE AS LastLoggedInDate,
                u.SYSTEM_USER_INDICATOR AS IsSystemUser,
                u.USERNAME AS Username,
                u.FIRST_NAME AS FirstName,
                u.LAST_NAME AS LastName,
                u.EMAIL AS Email,
                u.INACTIVE_INDICATOR AS IsInactive,
                u.PICTURE AS Picture
            FROM
				[USER] u WITH (NOLOCK)
				INNER JOIN GROUP_MEMBERSHIP gm WITH (NOLOCK) ON gm.USER_ID = u.USER_ID
				INNER JOIN [GROUP] g WITH (NOLOCK) ON g.GROUP_ID = gm.GROUP_ID
            WHERE
				u.tenant_id = @tenantId
				AND (@groupId IS NULL OR gm.GROUP_ID = @groupId)
				AND (@includeInactive IS NULL OR @includeInactive = 1 OR u.INACTIVE_INDICATOR = 0)
				AND (@groupName IS NULL OR @groupName = g.GROUP_NAME)
				AND (@groupId IS NOT NULL OR @groupName IS NOT NULL)";
        
        public override QueryInfo GetQuery(Criteria critera)
        {
            RequiredParameters = new string[] { "includeInactive", "groupId", "groupName" };
            if (critera == null)
                critera = new Criteria();
            var formattedQuery = FilterAndSearchQuery(query, critera);
            return new QueryInfo
            {
                Query = query,
                Parameters = new DynamicParameters(critera.Parameters)
            };
        }
    }
}
