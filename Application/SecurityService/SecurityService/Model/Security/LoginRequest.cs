using Scion.Infrastructure.Common;

namespace Scion.Infrastructure.Web.Model.Security
{
    public class LoginRequest : ValueObject
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
