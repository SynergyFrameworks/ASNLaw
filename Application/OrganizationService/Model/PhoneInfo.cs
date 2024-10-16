using OrganizationService.Validation;
using Datalayer.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrganizationService.Model
{
    public class PhoneInfo : IEntity
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Guid Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string PhoneType { get; set; }

        [MaxLength(15)]
        [Required]
        public string PhoneNumber { get; set; }

        [MaxLength(20)]
        public string CountryPrefix { get; set; } = "1";

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Guid[] Organizations { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [EitherOneIsRequired(nameof(Organizations), ErrorMessage = "Please define either a Organization or Client as the owner of the Phone number.")]
        public virtual Guid[] Clients { get; set; }
    }
}
