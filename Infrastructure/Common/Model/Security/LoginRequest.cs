using Infrastructure.Common;

namespace Infrastructure.Model.Security
{
    public class LoginRequest : ValueObject
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
