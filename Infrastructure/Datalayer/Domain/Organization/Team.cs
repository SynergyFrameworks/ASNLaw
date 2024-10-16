using Datalayer.Contracts;
using Datalayer.Domain.Group;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Domain
{
    [Table("Teams")]
    public class Team : Auditable, IEntity
    {

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }


        [Required]
        [ForeignKey(nameof(Client))]
        public Guid ClientId { get; set; }

        public ASNClient Client { get; set; }

        public ICollection<ASNGroup> Groups { get; set; } = new HashSet<ASNGroup>();

    }
}
