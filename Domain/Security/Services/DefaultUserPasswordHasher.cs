using Microsoft.AspNetCore.Identity;
using Scion.Infrastructure.Security;

namespace Scion.Business.Security.Services
{
    public class DefaultUserPasswordHasher : PasswordHasher<ApplicationUser>, IUserPasswordHasher
    {
    }
}
