using Datalayer.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Domain.Group
{
    [Table("ASNTasks")]
    public class ASNTask : Auditable, IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string ShortName { get; set; }

        public ICollection<Series> Series { get; set; } = new HashSet<Series>();

        public ICollection<ProjectDocument> ProjectDocuments { get; set; } = new HashSet<ProjectDocument>();

        [ForeignKey(nameof(Module))]
        public Guid? ModuleId { get; set; }
        public Module Module { get; set; }

        [ForeignKey(nameof(Project))]
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
    }

}
