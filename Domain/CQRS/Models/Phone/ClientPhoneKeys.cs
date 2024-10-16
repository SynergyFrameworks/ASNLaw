using Datalayer.Contracts;
using Datalayer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Models.Phone
{
    public class ClientPhoneKeys
    {
        public List<Guid> PhoneIds { get; set; }
        
        public Guid ClientId { get; set; }
        
        public DateTimeOffset ModifiedDate { get; set; }
        
        public string ModifiedBy { get; set; }
    }

    public class OrganizationPhoneKeys
    {
        public List<Guid> PhoneIds { get; set; }

        public Guid OrganizationId { get; set; }

        public DateTimeOffset ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
    }
}
