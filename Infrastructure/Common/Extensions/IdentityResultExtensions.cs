using System.Linq;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity;
using Infrastructure.Security;

namespace Infrastructure.Common.Extensions
{
    public static class IdentityResultExtensions
    {
        public static SecurityResult ToSecurityResult(this IdentityResult identityResult)
        {
            return new SecurityResult()
            {
                Succeeded = identityResult.Succeeded,
                Errors = identityResult.Errors.Select(x => x.Description)
            };
        }
    }
}
