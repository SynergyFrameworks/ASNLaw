using Datalayer.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Domain.Group
{
    [Table("Resources")]
    public class Resource : Auditable, IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string ResourceType { get; set; }

        [Required]
        public string JsonValues { get; set; }

        [ForeignKey(nameof(Module))]
        public Guid? ModuleId { get; set; }

        public Module Module { get; set; }



    }
}
