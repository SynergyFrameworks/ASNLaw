using System;

namespace Scion.Infrastructure.Web.Model
{
    public class LastModifiedResponse
    {
        public string Scope { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
