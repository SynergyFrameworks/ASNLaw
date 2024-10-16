using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Infrastructure.Permissions.Runtime.Client.AspNetCore
{
    /// <summary>
    /// Middleware to automatically turn application roles and permissions into claims
    /// </summary>
    public class PermissionClaimsMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionClaimsMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        public PermissionClaimsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context, IPermissionService client)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var policy = await client.EvaluateAsync(context.User);
                var permissionClaims = policy.Permissions.Select(x => new Claim("permission", x));
                var id = new ClaimsIdentity("PolicyServerMiddleware", "name", "permission");
                
                id.AddClaims(permissionClaims);
                context.User.AddIdentity(id);
            }

            await _next(context);
        }
    }
}