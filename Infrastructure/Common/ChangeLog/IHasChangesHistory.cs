using System.Collections.Generic;
using Infrastructure.Common.Domain.Contracts;

namespace Infrastructure.Common.ChangeLog
{
    public interface IHasChangesHistory : IEntity
    {
        ICollection<OperationLog> OperationsLog { get; set; }
    }
}
