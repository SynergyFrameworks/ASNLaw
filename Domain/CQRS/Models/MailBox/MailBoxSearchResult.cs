using Datalayer.Domain;
using Infrastructure.CQRS.Models.MailBox;
using System;
using System.Collections.Generic;

namespace Infrastructure.CQRS.Models
{
    public class MailBoxSearchResult
    {
        public Guid Id { get; set; }
        public string Server { get; set; }
        public string FromAddress { get; set; }
        public string ServerUserName { get; set; }
        public string ServerPassword { get; set; }
        public string ConnectionSecurity { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public ModelState State { get; set; } = ModelState.None;

        public ICollection<MailBoxOwner> MailBoxOwners { get; set; }

    }
}
