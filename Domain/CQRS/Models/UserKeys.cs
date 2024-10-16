using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Models
{
    public class ClientUserKeys
    {
        public List<Guid> UserIds { get; set; }

        public Guid ClientId { get; set; }

        public DateTimeOffset ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
    }

    public class GroupUserKeys
    {
        public List<Guid> UserIds { get; set; }

        public Guid GroupId { get; set; }

        public DateTimeOffset ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
    }
}
