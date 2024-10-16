using Infrastructure.Common.Domain;
using System.Collections.Generic;



namespace Infrastructure
{
    public class Group:BaseObject, IUpdateObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsSystemGroup { get; set; }
        public IList<GroupMembership> GroupMembers { get; set; }
        public string Application { get { return Name; } set { } }  //figure what this is
        public bool IsSearchDriven
        {
            get { return false; }
            set { }
        }
       
    }
}