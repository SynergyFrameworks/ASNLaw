using Datalayer.Contracts;
using Datalayer.Domain;
using Infrastructure.CQRS.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Infrastructure.CQRS.Models
{

    public class PhoneSearchResult : IEntity, IAuditable
    {
        public Guid Id { get; set; }

        public string CountryPrefix { get; set; }

        public string PhoneNumber { get; set; }

        public string PhoneType { get; set; }

        public IList<PhoneOwner> PhoneOwners {get; set;}
        
        public DateTimeOffset CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ModelState State { get; set; } = ModelState.None;

    }
}
