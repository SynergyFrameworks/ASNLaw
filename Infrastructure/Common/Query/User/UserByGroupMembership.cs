
using System;

namespace Infrastructure.Query.User
{
    public class UserByGroupMembership
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

    }
}
