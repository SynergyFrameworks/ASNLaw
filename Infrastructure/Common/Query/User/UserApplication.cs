using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Query.User
{
    public class UserApplication: UserByUsername
    {
        //User
        public bool IsSystemUser { get; set; }
        public DateTime? LastLoggedInDate { get; set; }
        public string Picture { get; set; }

        //Application
        public Guid ApplicationId { get; set; }
        public string ApplicationName { get; set; }
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }
        public bool GroupMembershipIsInactive { get; set; }
        public string Settings { get; set; }
    }
}
