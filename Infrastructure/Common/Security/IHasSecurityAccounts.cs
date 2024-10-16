using System.Collections.Generic;
using Infrastructure.Common.Domain.Contracts;

namespace Infrastructure.Security
{
    public interface IHasSecurityAccounts : IEntity
    {
        /// <summary>
        /// All security accounts 
        /// </summary>
        ICollection<ApplicationUser> SecurityAccounts { get; set; }
    }
}
