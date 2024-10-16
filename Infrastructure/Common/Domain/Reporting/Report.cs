using Infrastructure.Common.Domain.Users;
using System;

namespace Infrastructure.Common.Domain.Reporting
{
    public class Report : BasePersistantObject, IHaveUniqueKey
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string RouteUri { get; set; }
        public virtual Application Application { get; set; }
        public virtual ReportCategory Category { get; set; }
        public virtual int DisplayOrder { get; set; }
        public virtual string ExternalReportId { get; set; }
        public virtual string GetUniqueKey(bool hasIdKey = false)
        {
            throw new NotImplementedException();
        }
    }
}
