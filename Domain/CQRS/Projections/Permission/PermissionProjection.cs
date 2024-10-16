using Datalayer.Domain.Security;
using Datalayer.Domain.Storage;
using System;
using System.Linq.Expressions;

namespace Infrastructure.CQRS.Projections
{
    public class PermissionProjection
    {
        public static Expression<Func<Permission, Permission>> PermissionInformation
        {
            get =>
                o => new Permission
                {
                    Id = o.Id,
                    Name = o.Name,
                    Description = o.Description,
                    CanRead = o.CanRead,
                    CanDelete = o.CanDelete,
                    CanCreate = o.CanCreate,
                    CanWrite = o.CanWrite,
                    AspNetRole = o.AspNetRole,
                    GroupPermissions = o.GroupPermissions,
                    CreatedBy = o.CreatedBy,
                    CreatedDate = o.CreatedDate,
                    ModifiedBy = o.ModifiedBy,
                    ModifiedDate = o.ModifiedDate,
                };
        }
    }
}
