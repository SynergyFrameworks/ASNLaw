using Datalayer.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Domain.Group
{
    [Table("Projects")]
    public class Project : Auditable, IEntity
    {

        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(450)]
        public string Name { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Column("GroupId")]
        [ForeignKey(nameof(ASNGroup))]
        public Guid GroupId { get; set; }

        public string ImageUrl { get; set; }

        public ASNGroup ASNGroup { get; set; }

        public ICollection<ProjectDocument> ProjectDocuments { get; set; }

        public ICollection<Series> Series { get; set; }

        public ICollection<ASNTask> Tasks { get; set; }
    }
}
