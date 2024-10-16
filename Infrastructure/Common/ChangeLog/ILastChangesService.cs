using System;
using Infrastructure.Common.Domain.Contracts;

namespace Infrastructure.Common.ChangeLog
{
    public interface ILastChangesService
    {
        void Reset(string entityName);
        void Reset(IEntity entity);
        DateTimeOffset GetLastModifiedDate(string entityName);
    }
}
