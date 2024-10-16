using Datalayer.Domain.Storage;
using System;
using System.Linq.Expressions;

namespace Infrastructure.CQRS.Projections
{
    public class StorageProviderProjection
    {
        public static Expression<Func<StorageProvider, StorageProvider>> StorageProviderInformation
        {
            get =>
                o => new StorageProvider
                {
                    Id = o.Id,
                    Client = o.Client,
                    ClientId = o.ClientId,
                    DisplayName = o.DisplayName,
                    Group = o.Group,
                    GroupId = o.GroupId,
                    ProviderName = o.ProviderName,
                    CreatedBy = o.CreatedBy,
                    CreatedDate = o.CreatedDate,
                    ModifiedBy = o.ModifiedBy,
                    ModifiedDate = o.ModifiedDate,
                };
        }
    }
}
