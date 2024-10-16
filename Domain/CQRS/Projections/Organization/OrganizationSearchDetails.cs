using Datalayer.Domain;
using Infrastructure.CQRS.Models;
using System;
using System.Linq.Expressions;


namespace Infrastructure.CQRS.Projections
{
    public class OrganizationSearchDetails
    {
        public static Expression<Func<Organization, OrganizationSearchResult>> OrganizationSearch
        {
            get =>
                o => new OrganizationSearchResult
                {
                    Id = o.Id,
                    Name = o.Name,
                    Description = o.Description,
                    Clients = o.Clients,
                    CreatedBy = o.CreatedBy,
                    CreatedDate = o.CreatedDate,
                    ModifiedBy = o.ModifiedBy,
                    ModifiedDate = o.ModifiedDate,
                };
        }
    }
}
