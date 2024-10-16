using System.Collections.Generic;
using System.Linq;
using Infrastructure.Common.Extensions;
using Infrastructure.Common.Domain.Reference;

namespace Infrastructure.Common.Domain.Performance
{
    public class ExportMapping : BasePersistantObject
    {
        public virtual DomainProperty DomainProperty { get; set; }
        public virtual ExportType ExportType { get; set; }
        public virtual StandardPeriodRange StandardPeriodRange { get; set; }
        public virtual bool IsDebt { get; set; }
        public virtual bool IsEquity { get; set; }
        public virtual string CodeOverride { get; set; }
        public virtual string ExportCodeOverride { get; set; }
        public virtual string NameOverride { get; set; }
        public virtual string DescriptionOverride { get; set; }
        public virtual string Granularity { get; set; }
        public virtual string Cardinality { get; set; }
        public virtual string TableName { get; set; }
        public virtual string ColumnName { get; set; }
        public virtual MappingSection MappingSection { get; set; }

        public virtual string DomainModelPropertyName
        {
            get
            {
                return CodeOverride ??
                       DomainProperty.CodeOverride ??
                       DomainProperty.StandardProperty.BlankIfNull().Code;
            }
        }

        public virtual string ExportCode
        {
            get { return ExportCodeOverride ?? DomainProperty.ExportCode; }
        }

        public virtual string Section
        {
            get
            {
                List<string> list = MappingSection != null  && MappingSection.Code != null ? MappingSection.Code.Split('|').ToList() : new List<string>();
                return list.Count > 1 ? list[1] : list.Any() ? list[0] : "";
            }
        }

        public virtual string ObjectName
        {
            get { return TableName == null ? null : TableName.ToTitleCase().Replace("_",""); }
        }
    }
}
