using Datalayer.Domain.Group;
using System;
using System.Linq.Expressions;

namespace Infrastructure.CQRS.Projections
{
    public class GroupProjection
    {
        public static Expression<Func<ASNGroup, ASNGroup>> GroupInformation
        {
            get =>
                o => new ASNGroup
                {
                    Id = o.Id,
                    Name = o.Name,
                    Description = o.Description,
                    ImageUrl = o.ImageUrl,
                    Team = o.Team,
                    Modules = o.Modules,
                    Projects = o.Projects,
                    Resources = o.Resources,
                    GroupPermissions = o.GroupPermissions,
                    StorageProviders = o.StorageProviders,
                    Users = o.Users,
                    CreatedBy = o.CreatedBy,
                    CreatedDate = o.CreatedDate
                };
        }
    }
}
