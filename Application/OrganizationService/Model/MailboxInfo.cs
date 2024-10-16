using OrganizationService.Validation;
using Datalayer.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrganizationService.Model
{
    public class MailBoxInfo : IEntity
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Server { get; set; }

        [Required]
        [MaxLength(200)]
        public string FromAddress { get; set; }

        [Required]
        [MaxLength(100)]
        public string AdminEmail { get; set; }

        [Required]
        [MaxLength(100)]
        public string ServerUserName { get; set; }

        [Required]
        [MaxLength(50)]
        public string ServerPassword { get; set; }

        [Required]
        public string ConnectionSecurity { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Guid[] Organizations { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [EitherOneIsRequired(nameof(Organizations), ErrorMessage = "Please define either a Organization or Client as the owner of the Address.")]
        public virtual Guid[] Clients { get; set; }


    }
}
