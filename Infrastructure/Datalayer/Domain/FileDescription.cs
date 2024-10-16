using Datalayer.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace Datalayer.Domain
{
    public class FileDescription : Auditable, IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid StreamId { get; set; }

        [MaxLength(100)]
        [Required]
        public string FileName { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [MaxLength(100)]
        [Required]
        public string Location { get; set; }

        [Required]
        public string ContentType { get; set; }

        [MaxLength(4)]
        public string Extension { get; set; }
        
    }
}
