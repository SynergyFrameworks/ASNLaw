using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Models.Addresses
{
    public class ClientAddressKeys
    {
        public List<Guid> AddressIds { get; set; }

        public Guid ClientId { get; set; }

        public DateTimeOffset ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
    }

    public class OrganizationAddressKeys
    {
        public List<Guid> AddressIds { get; set; }

        public Guid OrganizationId { get; set; }

        public DateTimeOffset ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
    }
}
