using System;
using Infrastructure.Common.Domain.Reference;

namespace Infrastructure.Common.Domain.Performance
{
    public class CustomValue : BasePersistantObject
    {
        public virtual string Value { get; set; }
        public virtual CustomProperty CustomProperty { get; set; }
        public virtual Guid RelatedObjectId { get; set; }
    }
}
