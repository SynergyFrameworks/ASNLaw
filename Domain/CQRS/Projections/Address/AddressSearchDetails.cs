using Datalayer.Domain.Demographics;
using Infrastructure.CQRS.Models;
using Infrastructure.CQRS.Models.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.CQRS.Projections.Addresses
{
    public class AddressSearchDetails
    {
        public static Expression<Func<Address, AddressSearchResult>> AddressSearch
        {
            get =>
              o =>
                   new AddressSearchResult
                   {
                       Id = o.Id,
                       AddressType = o.AddressType,
                       AddressLine1 = o.AddressLine1,
                       AddressLine2 = o.AddressLine2,
                       City = o.City,
                       StateCode = o.StateCode,
                       CountryCode = o.CountryCode,
                       PostalCode = o.PostalCode,
                       CreatedBy = o.CreatedBy,
                       CreatedDate = o.CreatedDate,
                       ModifiedBy = o.ModifiedBy,
                       ModifiedDate = o.ModifiedDate,
                       AddressOwners = o.Organizations.Select(o => new AddressOwner { Id = o.Id, Name = o.Name, OwnerType = Enums.OwnerType.Organization })
                       .Union(o.Clients.Select(o => new AddressOwner { Id = o.Id, Name = o.Name, OwnerType = Enums.OwnerType.Client })).ToList(),
                   };
        }
    }
}
