using Datalayer.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrganizationService.Model
{
    public class StorageProviderInfo : IEntity
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Guid Id { get; set; }

        [Required]
        public string ClientConnection { get; set; }

        [Required]
        [MaxLength(200)]
        public string DisplayName { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProviderName { get; set; }

        [Required]
        [MaxLength(200)]
        public string ProviderUser { get; set; }  //TODO: Might be removed, user should receive a prompt by the provider. Will be better off if we do not store the user's credentials of a third party.

        [Required]
        [MaxLength(200)]
        public string ProviderPassword { get; set; } //TODO: Might be removed, user should receive a prompt by the provider. Will be better off if we do not store the user's credentials of a third party.

        [Required]
        public Guid ClientId { get; set; }

        [Required]
        public Guid GroupId { get; set; }
    }
}
