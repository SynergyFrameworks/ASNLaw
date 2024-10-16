using Datalayer.Domain.Security;
using Datalayer.Domain.Storage;
using Infrastructure.CQRS.Models;
using System;
using System.Linq.Expressions;

namespace Infrastructure.CQRS.Projections
{
    public class PermissionSearchDetails
    {
        public static Expression<Func<Permission, PermissionSearchResult>> RuleSearch
        {
            get =>
                o => new PermissionSearchResult
                {
                    Id = o.Id,
                    Name = o.Name,
                    Description = o.Description,
                    CanRead = o.CanRead,
                    CanDelete = o.CanDelete,
                    CanCreate = o.CanCreate,
                    CanWrite = o.CanWrite,
                    CreatedBy = o.CreatedBy,
                    CreatedDate = o.CreatedDate,
                };
        }
    }
}
