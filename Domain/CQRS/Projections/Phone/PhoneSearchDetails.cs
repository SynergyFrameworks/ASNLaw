using Datalayer.Domain.Demographics;
using Infrastructure.CQRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.CQRS.Projections
{
    public class PhoneSearchDetails
    {
        public static Expression<Func<Phone, PhoneSearchResult>> PhoneSearch
        {
            get =>
              o =>
                   new PhoneSearchResult
                   {
                       Id = o.Id,
                       CountryPrefix = o.CountryPrefix,
                       PhoneNumber = o.PhoneNumber,
                       PhoneType = o.PhoneType,
                       PhoneOwners = o.Organizations.Select(o => new PhoneOwner { Id = o.Id, Name = o.Name, OwnerType = Enums.OwnerType.Organization })
                       .Union(o.Clients.Select(o => new PhoneOwner { Id = o.Id, Name = o.Name, OwnerType = Enums.OwnerType.Client })).ToList(),
                       CreatedBy = o.CreatedBy,
                       CreatedDate = o.CreatedDate,
                       ModifiedBy = o.ModifiedBy,
                       ModifiedDate = o.ModifiedDate,
                   };
        }
    }
}
