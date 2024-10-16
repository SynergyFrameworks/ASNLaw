using Datalayer.Contracts;
using Datalayer.Domain;
using Datalayer.Domain.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Models
{
    public class UserSearchResult : IEntity, IAuditable
    {
        public Guid Id { get; set; }

        public Guid IdentityUserId { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string ImageUrl { get; set; }

        public bool IsActive { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
        public ModelState State { get; set; }

        public IList<UserOwner> UserOwners { get; set; }

    }
}
