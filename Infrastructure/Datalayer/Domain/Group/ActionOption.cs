using Datalayer.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Domain.Group
{
    [Table("ActionOptions")]
    public class ActionOption : Auditable, IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(Action))]
        public Guid ActionId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Label { get; set; }

        public ASNAction Action { get; set; }

        public ICollection<OptionValue> OptionValues { get; set; }

    }
}
