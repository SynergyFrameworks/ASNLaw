using Datalayer.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrganizationService.Model
{
    public class PermissionInfo : IEntity
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public bool CanRead { get; set; }

        [Required]
        public bool CanWrite { get; set; }

        [Required]
        public bool CanDelete { get; set; }

        [Required]
        public bool CanCreate { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string AspNetRole { get; set; }
    }
}
