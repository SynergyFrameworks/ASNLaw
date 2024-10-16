using Datalayer.Contracts;
using Datalayer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Models
{
    public class OrganizationSearchResult : IEntity, IAuditable
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<ASNClient> Clients { get; set; } = new List<ASNClient>();

        public DateTimeOffset CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        public ModelState State { get; set; } = ModelState.None;
    }
}
