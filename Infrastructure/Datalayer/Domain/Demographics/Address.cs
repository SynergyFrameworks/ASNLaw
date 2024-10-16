using Datalayer.Contracts;
using Datalayer.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Datalayer.Domain.Demographics
{
    [Table("Addresses")]
    public class Address : Auditable, IEntity
    {
        
        [Key]
        [JsonIgnore]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string AddressType { get; set; }
        [Required]
        [MaxLength(100)]
        public string AddressLine1 { get; set; }
        [MaxLength(100)]
        public string AddressLine2 { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        [Required]
        [MaxLength(50)]
        public string StateCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string PostalCode { get; set; }
        [Required]
        [MaxLength(50)]
        public string CountryCode { get; set; }

        public ICollection<Organization> Organizations { get; set; }
        public ICollection<ASNClient> Clients { get; set; }
    }
}
