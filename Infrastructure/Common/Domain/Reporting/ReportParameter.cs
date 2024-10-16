using Infrastructure.Common.Domain.Reference;
using System;

namespace Infrastructure.Common.Domain.Reporting
{
    public class ReportParameter : BasePersistantObject, IHaveUniqueKey
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual DataType DataType { get; set; }
        public virtual Report Report { get; set; }
        public virtual bool BooleanValue { get; set; }
        public virtual DateTime DateValue { get; set; }
        public virtual string StringValue { get; set; }
        public virtual double DecimalValue { get; set; }
        public virtual int IntegerValue { get; set; }
        public virtual int DisplayOrder { get; set; }
        public virtual string GetUniqueKey(bool hasIdKey = false)
        {
            throw new NotImplementedException();
        }
    }
}
