using Datalayer.Domain.Storage;
using System;
using System.Linq.Expressions;

namespace Infrastructure.CQRS.Projections
{
    public class DocumentSourceProjection
    {
        public static Expression<Func<DocumentSource, DocumentSource>> DocumentSourceInformation
        {
            get =>
                o => new DocumentSource
                {
                    Id = o.Id,
                    Description = o.Description,
                    InputFolder = o.InputFolder,
                    OutputFolder = o.OutputFolder,
                    StorageProviderId = o.StorageProviderId,
                    StorageProvider = o.StorageProvider,
                    CreatedBy = o.CreatedBy,
                    CreatedDate = o.CreatedDate,
                    ModifiedBy = o.ModifiedBy,
                    ModifiedDate = o.ModifiedDate,
                };
        }
    }
}
