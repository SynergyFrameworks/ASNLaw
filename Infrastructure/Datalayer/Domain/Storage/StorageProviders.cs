using Datalayer.Domain;
using Datalayer.Domain.Group;
using Datalayer.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Domain.Storage
{

    [Table("StorageProviders")]
    public class StorageProvider : Auditable, IEntity
    {

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProviderName { get; set; }

        [Required]
        [MaxLength(200)]
        public string DisplayName { get; set; }

        [Required]
        [MaxLength(200)]
        public string ProviderUser { get; set; }

        [Required]
        [MaxLength(200)]
        public string ProviderPassword { get; set; }

        [Required]
        [ForeignKey(nameof(ClientId))]
        public Guid ClientId { get; set; }

        public string ClientConnection { get; set; }

        [Required]
        [ForeignKey(nameof(GroupId))]
        public Guid GroupId { get; set; }

        public ASNGroup Group { get; set; }

        public ASNClient Client { get; set; }
    }
}
