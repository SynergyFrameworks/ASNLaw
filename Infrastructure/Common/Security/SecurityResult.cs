using System.Collections.Generic;

namespace Infrastructure.Security
{
    public class SecurityResult
    {
        public bool Succeeded { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
