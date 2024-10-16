using Datalayer.Contracts;
using Datalayer.Domain;
using System;

namespace Infrastructure.CQRS.Models
{
    public class StorageProviderSearchResult : IEntity, IAuditable
    {
        public Guid Id { get; set; }

        public string DisplayName { get; set; }

        public string ProviderName { get; set; }

        public Guid GroupId { get; set; }

        public string GroupName { get; set; }
        
        public Guid ClientId { get; set; }

        public string ClientName { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        public ModelState State { get; set; } = ModelState.None;

    }
}
