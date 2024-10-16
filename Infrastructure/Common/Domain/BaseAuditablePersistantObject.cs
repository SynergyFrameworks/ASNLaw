using Infrastructure.Common.Persistence;
using Infrastructure.Common.Domain.Audit;
using System;

namespace Infrastructure.Common.Domain
{
    public class BaseAuditablePersistantObject
    {
        [NonEditable]
        public virtual Guid Id { get; set; }
        [NonEditable]
        public virtual AuditUser CreatedBy { get; set; }
        [NonEditable]
        public virtual DateTime CreatedDate { get; set; }
        [NonEditable]
        public virtual AuditUser LastModifiedBy { get; set; }
        [NonEditable]
        public virtual DateTime LastModifiedDate { get; set; }
    }
}
