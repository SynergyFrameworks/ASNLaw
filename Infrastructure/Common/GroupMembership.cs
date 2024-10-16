


using Infrastructure.Common.Domain;

namespace Infrastructure
{
    public class GroupMembership:BaseObject, IUpdateObject
    { 
        public Group MemberGroup { get; set; }
        public GroupUser User { get; set; }
        public string Name {
            get { return User.FirstName + " " + User.LastName; }
            set { }
        }
        public string Application { get { return "Scion Service"; } set { } }
        public bool IsSearchDriven
        {
            get { return false; }
            set { }
        }
    }
}