using Datalayer.Domain;
using Infrastructure.CQRS.Models;
using System;
using System.Linq.Expressions;

namespace Infrastructure.CQRS.Projections
{
    public class TeamSearchDetails
    {
        public static Expression<Func<Team , TeamSearchResult>> TeamSearch
        {
            get =>
                o => new TeamSearchResult
                {
                    Id = o.Id,
                    Name = o.Name,
                    Description = o.Description,
                    Client = o.Client,
                    Groups = o.Groups,
                    CreatedBy = o.CreatedBy,
                    CreatedDate = o.CreatedDate,
                    ModifiedBy = o.ModifiedBy,
                    ModifiedDate = o.ModifiedDate,

                };
        }
    }

}
