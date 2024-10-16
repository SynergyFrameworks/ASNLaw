using OrganizationService.Validation;
using Datalayer.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrganizationService.Model
{
    public class AddressInfo
    {
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
        public string State { get; set; }
        [Required]
        [MaxLength(10)]
        public string PostalCode { get; set; }
        [Required]
        [MaxLength(50)]
        public string CountryCode { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Guid[] Organizations { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [EitherOneIsRequired(nameof(Organizations), ErrorMessage = "Please define either a Organization or Client as the owner of the Address.")]
        public virtual Guid[] Clients { get; set; }
    }
}
