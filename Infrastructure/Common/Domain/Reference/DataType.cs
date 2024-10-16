using System;

namespace Infrastructure.Common.Domain.Reference
{
    public class DataType : BasePersistantObject, IHaveUniqueKey
    {
        public virtual string Code { get; set; }
        public virtual string Description { get; set; }
        public virtual string Formatter { get; set; }
        public virtual string GetUniqueKey(bool hasIdKey = false)
        {
            throw new NotImplementedException();
        }
    }
}
