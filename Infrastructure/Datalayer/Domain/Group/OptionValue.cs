using Datalayer.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Domain.Group
{
    [Table("OptionValues")]
    public class OptionValue : Auditable, IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ActionOptionId { get; set; }

        [Required]
        [MaxLength(100)]
        public string DisplayText { get; set; }

        [Required]
        [MaxLength(100)]
        public string Value { get; set; }

        [Required]
        [MaxLength(50)]
        public string DataType { get; set; }

        [ForeignKey(nameof(ActionOptionId))]
        public ActionOption Action { get; set; }

    }
}
