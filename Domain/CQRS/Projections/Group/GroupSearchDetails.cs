using Datalayer.Domain.Group;
using Infrastructure.CQRS.Models;
using System;
using System.Linq.Expressions;

namespace Infrastructure.CQRS.Projections
{
    public class GroupSearchDetails
    {
        public static Expression<Func<ASNGroup, GroupSearchResult>> GroupSearch
        {
            get =>
                o => new GroupSearchResult
                {
                    Id = o.Id,
                    Name = o.Name,
                    Description = o.Description,
                    ImageUrl = o.ImageUrl,
                    TeamId = o.TeamId,
                    CreatedBy = o.CreatedBy,
                    CreatedDate = o.CreatedDate,
                    ModifiedBy = o.ModifiedBy,
                    ModifiedDate = o.ModifiedDate,
                };
        }
    }
}
