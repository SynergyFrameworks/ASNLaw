using Datalayer.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Domain
{
    [Table("ServiceContracts")]
    public class ServiceContract : Auditable, IEntity
    {
        [Key]
        public Guid Id { get; set; }


        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public string ContractType { get; set; }
    }
}
