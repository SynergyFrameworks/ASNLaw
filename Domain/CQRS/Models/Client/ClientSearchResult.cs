using Datalayer.Contracts;
using Datalayer.Domain;
using Datalayer.Domain;
using System;
using System.Collections.Generic;

namespace Infrastructure.CQRS.Models
{
    public class ClientSearchResult : IEntity, IAuditable
    {
        public Guid Id { get; set; }

        public Guid OrganizationId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        public ICollection<Team> Teams { get; set; }

        public ModelState State { get; set; } = ModelState.None;
    }
}
