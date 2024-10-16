using Datalayer.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Domain.Security
{
    [Table("Permissions")]
    public class Permission : Auditable, IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public bool CanRead { get; set; }

        [Required]
        public bool CanWrite { get; set; }

        [Required]
        public bool CanDelete { get; set; }

        [Required]
        public bool CanCreate { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string AspNetRole { get; set; }

        public ICollection<GroupPermission> GroupPermissions { get; set; } = new HashSet<GroupPermission>();

        public ICollection<User> Users { get; set; } = new HashSet<User>();

    }
}
