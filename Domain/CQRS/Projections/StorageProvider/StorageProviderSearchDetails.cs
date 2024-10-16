using Datalayer.Domain.Storage;
using Infrastructure.CQRS.Models;
using System;
using System.Linq.Expressions;

namespace Infrastructure.CQRS.Projections
{
    public class StorageProviderSearchDetails
    {
        public static Expression<Func<StorageProvider, StorageProviderSearchResult>> StorageProviderSearch
        {
            get =>
                o => new StorageProviderSearchResult
                {
                    Id = o.Id,
                    DisplayName = o.DisplayName,
                    ClientId = o.ClientId,
                    ClientName = o.Client.Name,
                    GroupId = o.GroupId,
                    GroupName = o.Group.Name,
                    ProviderName = o.ProviderName,
                    CreatedBy = o.CreatedBy,
                    CreatedDate = o.CreatedDate,
                    ModifiedBy = o.ModifiedBy,
                    ModifiedDate = o.ModifiedDate,
                };
        }
    }
}
