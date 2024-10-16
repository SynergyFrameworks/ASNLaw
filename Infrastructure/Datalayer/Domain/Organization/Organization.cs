using Datalayer.Domain.Demographics;
using Datalayer.Domain.Security;
using Datalayer.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Datalayer.Domain
{ 
    [Table("Organizations")]
    public class Organization : Auditable, IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string Description { get; set; } 

        [MaxLength(256)]
        public string WebUrl { get; set; }

        [MaxLength(256)]
        public string ImageUrl { get; set; }

        [ForeignKey(nameof(User))]

        public Guid? SuperUserId { get; set; }

        public ICollection<ASNClient> Clients { get; set; } = new HashSet<ASNClient>();

        public ICollection<Phone> Phones { get; set; } = new HashSet<Phone>();

        public ICollection<Address> Addresses { get; set; } = new HashSet<Address>();

        public User User { get; set; }

        public ICollection<MailBox> MailBoxes { get; set; } = new HashSet<MailBox>();

        public ICollection<AuthenticationProvider> AuthenticationProviders { get; set; } = new HashSet<AuthenticationProvider>();
    }
}
