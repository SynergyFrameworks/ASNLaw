using Infrastructure.CQRS.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.CQRS.Projections.User
{
    public class UserSearchDetails
    {
        public static Expression<Func<Datalayer.Domain.Security.User, UserSearchResult>> UserSearch
        {
            get =>
                o => new UserSearchResult
                {
                    Id = o.Id,
                    IdentityUserId = o.IdentityUserId,
                    UserName = o.UserName,
                    Name = o.Name,
                    Email = o.Email,
                    ImageUrl = o.ImageUrl,
                    IsActive = o.IsActive,
                    UserOwners = o.Groups.Select(o => new UserOwner { Id = o.Id, Name = o.Name, OwnerType = Enums.OwnerType.Group })
                       .Union(o.Clients.Select(o => new UserOwner { Id = o.Id, Name = o.Name, OwnerType = Enums.OwnerType.Client })).ToList(),
                    CreatedBy = o.CreatedBy,
                    CreatedDate = o.CreatedDate,
                    ModifiedBy = o.ModifiedBy,
                    ModifiedDate = o.ModifiedDate,
                };
        }
    }
}
