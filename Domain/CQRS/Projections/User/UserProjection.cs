using System;
using System.Linq;
using System.Linq.Expressions;
using Datalayer.Domain.Security;

namespace Infrastructure.CQRS.Projections
{
    public class UserProjection
    {
        public static Expression<Func<Datalayer.Domain.Security.User, Datalayer.Domain.Security.User>> UserInformation
        {
            get =>
                o => new Datalayer.Domain.Security.User
                {
                    Id = o.Id,
                    IdentityUserId = o.IdentityUserId,
                    UserName=o.UserName,
                    Name = o.Name,
                    Email=o.Email,
                    ImageUrl = o.ImageUrl,
                    IsActive= o.IsActive,
                    Groups = o.Groups.Select(group => new Datalayer.Domain.Group.ASNGroup
                    {
                        Description = group.Description,
                        Id = group.Id,
                        ImageUrl = group.ImageUrl,
                        TeamId = group.TeamId,
                        Name = group.Name,
                        CreatedBy = group.CreatedBy,
                        CreatedDate = group.CreatedDate
                    }).AsParallel().ToList(),
                    Clients = o.Clients.Select(client => new Datalayer.Domain.ASNClient
                    {
                        Id = client.Id,
                        ClientNo = client.ClientNo,
                        Name = client.Name,
                        Description = client.Description,
                        CreatedBy = client.CreatedBy,
                        CreatedDate = client.CreatedDate
                    }).AsParallel().ToList(),
                    Subscriptions = o.Subscriptions,
                    CreatedBy = o.CreatedBy,
                    CreatedDate = o.CreatedDate,
                    ModifiedBy = o.ModifiedBy,
                    ModifiedDate = o.ModifiedDate,
                };
        }
    }
}
