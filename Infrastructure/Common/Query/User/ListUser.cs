using Infrastructure.Common.Persistence;
using System;

namespace Infrastructure.Query.User
{
    public class ListUser
    {
        public Guid Id { get; set; }
        public DateTime? LastLoggedInDate { get; set; }
        [ResultSetFilter]
        public bool IsSystemUser { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [ResultSetFilter]
        public bool IsInactive { get; set; }
        public string Picture { get; set; }
    }
}
