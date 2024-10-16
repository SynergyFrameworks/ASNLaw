using System;

namespace Infrastructure.Model
{
    public class LastModifiedResponse
    {
        public string Scope { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
