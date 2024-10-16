using System;
using System.Collections.Generic;
using Infrastructure.Common;

namespace Infrastructure.Security
{
    public class UserSearchCriteria : SearchCriteriaBase
    {
        public string MemberId { get; set; }
        public IList<string> MemberIds { get; set; }
        public DateTime? ModifiedSinceDate { get; set; }
        //Search users by their role names
        public string[] Roles { get; set; }
        //TODO: Update UI pagination to use Skip and Take properties
    }
}
