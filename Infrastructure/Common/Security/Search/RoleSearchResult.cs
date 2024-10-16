using System.Collections.Generic;
using Infrastructure.Common;

namespace Infrastructure.Security.Search
{
    public class RoleSearchResult : GenericSearchResult<Role>
    {
        public IList<Role> Roles => Results;
    }
}
