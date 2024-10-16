using System.Collections.Generic;

namespace Infrastructure.Security
{
    public interface ISupportSecurityScopes
    {
        IEnumerable<string> Scopes { get; set; }
    }
}
