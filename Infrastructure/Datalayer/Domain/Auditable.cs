using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Datalayer.Contracts;

namespace Datalayer.Domain
{
    public abstract class Auditable : IAuditable
    {
        [Required]
        public DateTimeOffset CreatedDate { get; set; }

        [MaxLength(50)]
        [Required]
        public string CreatedBy { get; set; }


        public DateTimeOffset? ModifiedDate { get; set; }

        [MaxLength(50)]
        public string ModifiedBy { get; set; }

        public DateTimeOffset? DeletedDate { get; set; }

        [MaxLength(50)]
        public string DeletedBy { get; set; }

        [NotMapped]
        public ModelState State { get; set; }
    }

    public enum ModelState
    {
        None,
        Added,
        Deleted,
        Updated
    }
}
