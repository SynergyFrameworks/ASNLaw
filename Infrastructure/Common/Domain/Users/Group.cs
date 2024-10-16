using System.Collections.Generic;

namespace Infrastructure.Common.Domain.Users
{
    public class Group : BasePersistantObject
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual bool IsSystemGroup { get; set; }
        public virtual IList<GroupMembership> GroupMembers { get; set; }

        public virtual Application Application { get; set; }
        public virtual bool CanDelete { get; set; }
    }
}
