using System;
using System.Linq.Expressions;
using Datalayer.Domain.Demographics;

namespace Infrastructure.CQRS.Projections.Addresses
{
    public class AddressProjection
    {
        public static Expression<Func<Address, Address>> AddressInformation
        {
            get =>
                o => new Address
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
                };
        }
    }
}
