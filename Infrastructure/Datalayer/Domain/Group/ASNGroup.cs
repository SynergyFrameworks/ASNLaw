using Datalayer.Domain;
using Datalayer.Domain.Security;
using Datalayer.Domain.Storage;
using Datalayer.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Domain.Group
{
    [Table("ASNGroups")]
    public class ASNGroup : Auditable, IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [ForeignKey(nameof(Team))]
        public Guid TeamId { get; set; }

        public Team Team { get; set; }

        public ICollection<Resource> Resources { get; set; } = new HashSet<Resource>();

        public ICollection<User> Users { get; set; } = new HashSet<User>();

        public ICollection<Project> Projects { get; set; } = new HashSet<Project>();

        public ICollection<Module> Modules { get; set; } = new HashSet<Module>();

        public ICollection<GroupPermission> GroupPermissions { get; set; } = new HashSet<GroupPermission>();

        public ICollection<StorageProvider> StorageProviders { get; set; } = new HashSet<StorageProvider>();
    }
}
