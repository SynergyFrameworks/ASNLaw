using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Models.MailBox
{
    public class ClientMailBoxKeys
    {
        public List<Guid> MailBoxIds { get; set; }

        public Guid ClientId { get; set; }

        public DateTimeOffset ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
    }

    public class OrganizationMailBoxKeys
    {
        public List<Guid> MailBoxIds { get; set; }

        public Guid OrganizationId { get; set; }

        public DateTimeOffset ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
    }
}
