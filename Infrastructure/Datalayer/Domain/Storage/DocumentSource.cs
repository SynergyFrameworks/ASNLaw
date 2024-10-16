using Datalayer.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Domain.Storage
{
    [Table("DocumentSources")]
   public class DocumentSource: Auditable, IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(StorageProvider))]
        public Guid StorageProviderId { get; set; }

        [Required]
        [MaxLength(500)]
        public string InputFolder { get; set; }
        [Required]
        [MaxLength(500)]
        public string OutputFolder { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        public StorageProvider StorageProvider { get; set; }
    }
}
