namespace Infrastructure.Common.Domain.Users
{
    public class GroupMembership : BasePersistantObject
    {
        public virtual Group Group { get; set; }
        public virtual Group MemberGroup {get; set; }
        public virtual User User { get; set; }
    }
}
