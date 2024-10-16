using Infrastructure.Common;

namespace Infrastructure.Security
{
    public class ApplicationUserLogin : ValueObject
    {
        public virtual string LoginProvider { get; set; }
        public virtual string ProviderKey { get; set; }
    }
}
