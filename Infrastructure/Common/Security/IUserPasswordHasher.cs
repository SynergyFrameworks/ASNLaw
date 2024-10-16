using System;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Security
{
    /// <summary>
    /// Basic interface for platform password hashers
    /// </summary>
    [Obsolete("Use IPasswordHasher<ApplicationUser> instead. UserPasswordsHistory is available from ISecurityRepository")]
    public interface IUserPasswordHasher : IPasswordHasher<ApplicationUser>
    {
    }
}
