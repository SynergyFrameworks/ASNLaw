using System.ComponentModel.DataAnnotations;
using Scion.Infrastructure.Common;

namespace Scion.Business.Security.Model
{
    public class UserPasswordHistoryEntity : AuditableEntity
    {
        [StringLength(128)]
        public string UserId { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }
}
