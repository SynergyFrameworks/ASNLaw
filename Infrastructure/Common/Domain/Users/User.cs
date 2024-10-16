using Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;

namespace Infrastructure.Common.Domain.Users
{
    public class User : BasePersistantObject
    {
        public virtual string Username { get; set; }

        public virtual string Password { get; set; }
        public virtual string Email { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        [NonEditable]
        public virtual bool IsSystemUser { get; set; }
        [NonEditable]
        public virtual DateTime? LastLoggedInDate { get; set; }
        [NonEditable]
        //public virtual Employee Employee { get; set; }
        public virtual string Picture
        {
            get;
            set;
        }

        [NonEditable]
        public virtual Tenant Tenant { get; set; }
        public List<Application> Applications { get; internal set; }
        public IList<object> Tenants { get; internal set; }

        public User() { }

        //public User(UserSummary userSummary)
        //{
        //    Id = userSummary.Id;

        //    Username = string.IsNullOrEmpty(userSummary.Username) ? (Id.Equals(new Guid()) ? userSummary.Email : userSummary.Username) : userSummary.Username;
        //    FirstName = userSummary.FirstName;
        //    LastName = userSummary.LastName;
        //    Email = userSummary.Email;
        //}
    }


}
