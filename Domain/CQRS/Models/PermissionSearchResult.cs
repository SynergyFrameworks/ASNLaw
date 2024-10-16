using Datalayer.Contracts;
using Datalayer.Domain;
using System;
using System.Text.Json.Serialization;

namespace Infrastructure.CQRS.Models
{
    public class PermissionSearchResult : IEntity, IAuditable
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool CanRead { get; set; }

        public bool CanDelete { get; set; }

        public bool CanCreate { get; set; }
        
        public bool CanWrite { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string ModifiedBy { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTimeOffset? ModifiedDate { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ModelState State { get; set; } = ModelState.None;
    }
}
