using Infrastructure.Common.Domain.Reference;
using Infrastructure.Common.Domain;
namespace Infrastructure.Common.Domain.Performance
{
    public class DomainProperty : BasePersistantObject
    {
        public virtual StandardProperty StandardProperty { get; set; }
        public virtual CustomProperty CustomProperty { get; set; }
        public virtual StandardDomain StandardDomain { get; set; }
        public virtual string ExportCode { get; set; }
        public virtual bool IsDisplayed { get; set; }
        public virtual string CodeOverride { get; set; }
        public virtual string NameOverride { get; set; }
        public virtual string DescriptionOverride { get; set; }

        public virtual BaseProperty GetProperty()
        {
            return (BaseProperty)StandardProperty ?? CustomProperty;
        }
    }
}
