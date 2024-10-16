using Datalayer.Domain;
using Datalayer.Domain.Group;
using Datalayer.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Datalayer.Domain.Security
{
    [Table("Subscriptions")]
    public class Subscription : Auditable, IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int SubLevel { get; set; }

        public string Key { get; set; }

        [ForeignKey(nameof(Module))]
        public Guid ModuleId { get; set; }

        [ForeignKey(nameof(ServiceContract))]
        public Guid ServiceContractId { get; set; }

        public Module Module { get; set; }

        public ServiceContract ServiceContract { get; set; }
    }
}
