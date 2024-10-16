using Datalayer.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Domain.Group
{
    [Table("TaskSteps")]
    public class TaskStep : Auditable, IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(Task))]
        public Guid TaskId { get; set; }

        [Required]
        public int Sequence { get; set; }

        [Required]
        [MaxLength(100)]
        public string DisplayText { get; set; }

        [Required]
        [ForeignKey(nameof(Action))]
        public Guid ActionId { get; set; }

        public ASNTask Task { get; set; }

        public ASNAction Action { get; set; }
    }

}
