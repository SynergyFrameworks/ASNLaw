using Datalayer.Domain;
using Datalayer.Domain;
using Infrastructure.CQRS.Models.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Models
{
    public class AddressSearchResult
    {
        public Guid Id { get; set; }


        public string AddressType { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string StateCode { get; set; }

        public string PostalCode { get; set; }

        public string CountryCode { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        public ModelState State { get; set; } = ModelState.None;

        public ICollection<AddressOwner> AddressOwners { get; set; }

    }
}
