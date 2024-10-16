using System.Collections.Generic;
using Infrastructure.Common;

namespace Infrastructure.Security.Search
{
    public class UserSearchResult : GenericSearchResult<ApplicationUser>
    {
        public IList<ApplicationUser> Users => Results;
    }
}
