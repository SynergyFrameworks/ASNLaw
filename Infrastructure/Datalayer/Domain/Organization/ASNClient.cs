using Datalayer.Domain.Demographics;
using Datalayer.Domain.Security;
using Datalayer.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Domain
{
    [Table("ASNClients")]
    public class ASNClient : Auditable, IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(20)]
        public string ClientNo { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string WebUrl { get; set; }

        [Required]
        [ForeignKey(nameof(Organization))]
        public Guid OrganizationId { get; set; }

        public Organization Organization { get; set; }

        public ICollection<Phone> Phones { get; set; } = new HashSet<Phone>();

        public ICollection<Address> Addresses { get; set; } = new HashSet<Address>();

        public ICollection<AuthenticationProvider> AuthenticationProviders { get; set; } = new HashSet<AuthenticationProvider>();

        public ICollection<MailBox> MailBoxes { get; set; } = new HashSet<MailBox>();

        public ICollection<Team> Teams { get; set; } = new HashSet<Team>();

        public ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
