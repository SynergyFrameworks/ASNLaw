using System;

namespace Infrastructure.Common.Domain.Audit
{
    public class AuditUser 
    {
        public virtual Guid Id { get; set; }

        public virtual string Username { get; set; }        
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }

        public virtual string Name
        {
            get
            {
                return FirstName + ' ' + LastName;
            }
        }
    }
}
