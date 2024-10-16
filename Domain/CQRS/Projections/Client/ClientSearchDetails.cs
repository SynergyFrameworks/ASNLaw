using Datalayer.Domain;
using Infrastructure.CQRS.Models;
using System;
using System.Linq.Expressions;

namespace Infrastructure.CQRS.Projections
{
    public class ClientSearchDetails
    {
        public static Expression<Func<ASNClient, ClientSearchResult>> ClientSearch
        {
            get =>
                o => new ClientSearchResult
                {
                    Id = o.Id,
                    Name = o.Name,
                    Description = o.Description,
                    Teams = o.Teams,
                    OrganizationId = o.OrganizationId,
                    CreatedBy = o.CreatedBy,
                    CreatedDate = o.CreatedDate,
                    ModifiedBy = o.ModifiedBy,
                    ModifiedDate = o.ModifiedDate,
                };
        }
    }

}
