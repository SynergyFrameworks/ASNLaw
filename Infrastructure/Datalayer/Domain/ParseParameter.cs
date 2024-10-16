using Datalayer.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace Datalayer.Domain
{
    public class Parameter : Auditable, IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(25)]
        [Required]
        public string Value { get; set; }

        public int Weight { get; set; }

        public string Type { get; set; }
    }
}
