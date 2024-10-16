using Datalayer.Domain.Storage;
using Datalayer.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Datalayer.Domain.Group
{
    [Table("ProjectDocuments")]
    public class ProjectDocument : Auditable, IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(Project))]
        public Guid? ProjectId { get; set; }

        public Project Project { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public int Size { get; set; }

        [Required]
        [MaxLength(5)]
        public string Extension { get; set; }

        [Required]
        public bool IsOutput { get; set; }

        [Required]
        [ForeignKey(nameof(DocumentSource))]
        public Guid DocumentSourceId { get; set; }
        
        public DocumentSource DocumentSource { get; set; }

        public ICollection<ASNTask> ASNTasks { get; set; } = new HashSet<ASNTask>();
    }
}
