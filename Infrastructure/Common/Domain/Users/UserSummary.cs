using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Domain.Users
{
    class UserSummary
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        /// <summary>
        /// Currently formatted as "FirstName LastName"
        /// </summary>
        public string Name { get; set; }

        public IList<UserGroup> Groups { get; set; }
    }
}
