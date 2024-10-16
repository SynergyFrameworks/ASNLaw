using System;
using OrganizationService.Validation;
using Datalayer.Contracts;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrganizationService.Model
{
    public class ASNUserInfo
    {
        public Guid Id { get; set; }

        public Guid IdentityUserId { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string ImageUrl { get; set; }

        public bool IsActive { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Guid[] Clients { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Guid[] Groups { get; set; }
    }
}
