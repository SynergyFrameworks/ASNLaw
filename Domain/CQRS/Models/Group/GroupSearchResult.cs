using Datalayer.Contracts;
using Datalayer.Domain;
using System;

namespace Infrastructure.CQRS.Models
{
    public class GroupSearchResult : IEntity, IAuditable
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public Guid TeamId { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        public ModelState State { get; set; }

    }
}
