using System;
using Infrastructure.Common.Domain.Users;

namespace Infrastructure.Common.Domain
{
    public class Activity : BasePersistantObject
    {
        public virtual string ActivityText { get; set; }        
        public virtual Guid ObjectId { get; set; }
        public virtual User User { get; set; }
        public virtual string ChangeReportJson { get; set; }
    }
}
