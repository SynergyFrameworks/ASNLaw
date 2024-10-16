using System;
using Infrastructure.Common.Persistence;
using Infrastructure.Common.Domain.Contracts;

namespace Infrastructure.Common.Domain
{
    public class BasePersistantObject : BaseAuditablePersistantObject, IPersistent, IAuditable
    {
        public virtual bool IsInactive { get; set; }
        DateTimeOffset IAuditable.CreatedDate { get ; set ; }
        string IAuditable.CreatedBy { get ; set ; }
        public DateTimeOffset? ModifiedDate { get ; set ; }
        public string ModifiedBy { get ; set ; }

        //set by reflection
        private Guid TenantId;
    }
}
