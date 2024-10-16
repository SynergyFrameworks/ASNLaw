using System;
using System.Collections.Generic;

namespace Scion.Infrastructure.Web.Model
{
    public class ChangedEntitiesRequest
    {
        public IEnumerable<string> EntityNames { get; set; }
        public DateTime ModifiedSince { get; set; }
    }
}
