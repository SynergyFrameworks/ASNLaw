using Datalayer.Contracts;
using Datalayer.Domain.Group;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Domain.Security
{
    [Table("Users")]
    public class User : Auditable, IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public Guid IdentityUserId { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string ImageUrl { get; set; }

        public bool IsActive { get; set; }

        public ICollection<UserSubscription> Subscriptions { get; set; } = new HashSet<UserSubscription>();

        public ICollection<ASNGroup> Groups { get; set; } = new HashSet<ASNGroup>();

        public ICollection<ASNClient> Clients { get; set; } = new HashSet<ASNClient>();

        public ICollection<Permission> Permissions { get; set; } = new HashSet<Permission>();
    }
}
