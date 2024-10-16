using Datalayer.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Domain
{
    [Table("MailBoxes")]
    public class MailBox : Auditable, IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Server { get; set; }

        [Required]
        [MaxLength(100)]
        public string FromAddress { get; set; }

        [MaxLength(100)]
        public string AdminEmail { get; set; }

        [Required]
        [MaxLength(100)]
        public string ServerUserName { get; set; }

        [Required]
        [MaxLength(50)]
        public string ServerPassword { get; set; }

        [MaxLength(50)]
        public string ConnectionSecurity { get; set; }

        public ICollection<Datalayer.Domain.Organization> Organizations { get; set; } = new HashSet<Organization>();
        
        public ICollection<ASNClient> Clients { get; set; } = new HashSet<ASNClient>();
    }
}
