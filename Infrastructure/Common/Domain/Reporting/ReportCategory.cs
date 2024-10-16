using Infrastructure.Common.Domain.Users;
using System;

namespace Infrastructure.Common.Domain.Reporting
{
    public class ReportCategory : BasePersistantObject, IHaveUniqueKey
    {
        public virtual string Code { get; set; }
        public virtual string Description { get; set; }
        public virtual string CssStyle { get; set; }
        public virtual int DisplayOrder { get; set; }
        public virtual Application Application { get; set; }
        public virtual string GetUniqueKey(bool hasIdKey = false)
        {
            throw new NotImplementedException();
        }
    }
}
