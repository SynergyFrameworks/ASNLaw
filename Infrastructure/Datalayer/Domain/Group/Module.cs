using Datalayer.Domain.Security;
using Datalayer.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Domain.Group
{
    [Table("Modules")]
    public class Module : Auditable, IEntity
    {
        public Module()
        {
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Version { get; set; }

        public string VersionTag { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string LicenseUrl { get; set; }

        public string ProjectUrl { get; set; }
     
        public bool RequireLicenseAcceptance { get; set; }

        public string Notes { get; set; }

        public string Tags { get; set; }

        public bool IsRemovable { get; set; }

        public bool IsInstalled { get; set; }

        public string Authors { get; set; }

        public string Owners { get; set; }

        public ICollection<ASNGroup> Groups { get; set; } = new HashSet<ASNGroup>();

        //public ICollection<Subscription> Subscriptions { get; set; } = new HashSet<Subscription>();
        public ICollection<Resource> Resources { get; set; } = new HashSet<Resource>();

    }
}
