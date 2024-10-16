using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Query.Tenant
{
    public class TenantKeywordMapping
    {
        public Guid Id { get; set; }
        public string KeyName { get; set; }
        public string KeyNamePlural { get; set; }
        public string KeyNamePast { get; set; }
        public string PreferredName { get; set; }
        public string PreferredNamePlural { get; set; }
        public string PreferredNamePast { get; set; }

    }

}
