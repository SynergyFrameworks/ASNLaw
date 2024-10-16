using Datalayer.Domain;
using Datalayer.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Domain.Security
{
    [Table("AuthenticationProviders")]
    public class AuthenticationProvider : Auditable, IEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string DisplayName { get; set; }
        public string HandlerType { get; set; }
        public string Options { get; set; }

        public ICollection<ASNClient> Clients { get; set; }
        public ICollection<Organization> Organizations { get; set; }
    }
}
