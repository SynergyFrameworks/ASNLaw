using System.Collections.Generic;
using Infrastructure.Common;

namespace Infrastructure.Model.Security
{
    public class UserDetail : Entity
    {
        public IList<string> Permissions { get; set; } = new List<string>();
        public string UserName { get; set; }
        public bool isAdministrator { get; set; }
        public bool PasswordExpired { get; set; }
    }
}
