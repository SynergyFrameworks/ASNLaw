using Datalayer.Domain;
using System;
using System.Linq.Expressions;

namespace Infrastructure.CQRS.Projections
{
    public static class OrganizationProjectionDynamic
    {
        public static Expression<Func<Organization, dynamic>> OrganizationInformation
        {
            get =>
                o => new
                {
                    o.Id,
        
                    o.ImageUrl,
                    o.WebUrl,
                    o.Addresses,
                    o.Phones,
                   
                };
        }
    }
}
