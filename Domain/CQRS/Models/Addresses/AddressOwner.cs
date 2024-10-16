using Infrastructure.CQRS.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.CQRS.Models.Addresses
{
    public class AddressOwner
    {
        public Guid Id { get; set; }

        [NotMapped]
        public OwnerType OwnerType { get; set; }

        public string Name { get; set; }
    }
}
