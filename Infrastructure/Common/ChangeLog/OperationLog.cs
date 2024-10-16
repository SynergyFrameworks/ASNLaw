using System;
using Infrastructure.Common;
using Infrastructure.Common.Domain.Contracts;
using Infrastructure.Common.Events;

namespace Infrastructure.Common.ChangeLog
{
    public class OperationLog : AuditableEntity, ICloneable
    {
        public string ObjectType { get; set; }

        public string ObjectId { get; set; }

        public EntryState OperationType { get; set; }

        public string Detail { get; set; }

        public virtual OperationLog FromChangedEntry<T>(GenericChangedEntry<T> changedEntry) where T : IEntity
        {
            if (changedEntry == null)
            {
                throw new ArgumentNullException(nameof(changedEntry));
            }

            ObjectId = changedEntry.OldEntry.Id.ToString();
            ObjectType = changedEntry.OldEntry.GetType().Name;
            OperationType = changedEntry.EntryState;

            return this;
        }

        #region ICloneable members

        public virtual object Clone()
        {
            return MemberwiseClone() as OperationLog;
        }

        #endregion
    }
}
