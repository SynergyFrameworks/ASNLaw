using Infrastructure.Common.Persistence;
using System;

namespace Infrastructure.Query.Tenant
{
    public class TenantApplication
    {
        public virtual Guid ApplicationId { get; set; }
        public virtual String Name { get; set; }
        public virtual String Settings { get; set; }
        public virtual Guid TenantId { get; set; }     
        public virtual int IsEnabled { get; set; }

    }
}
