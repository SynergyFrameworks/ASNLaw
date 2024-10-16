using Datalayer.Domain;
using Datalayer.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Datalayer.Domain.Demographics
{
    [Table("Phones")]
    public class Phone : Auditable, IEntity
    {

        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public Guid Id { get; set; }

        [MaxLength(50)]
        public string PhoneType { get; set; }
        [MaxLength(15)]
        public string PhoneNumber{ get; set; }
        [MaxLength(20)]
        public string CountryPrefix { get; set; }

        public ICollection<Organization> Organizations { get; set; } = new HashSet<Organization>();
        public ICollection<ASNClient> Clients { get; set; } = new HashSet<ASNClient>();
    }
}
