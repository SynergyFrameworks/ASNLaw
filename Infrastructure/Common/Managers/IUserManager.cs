using System;
using System.Collections.Generic;
using Infrastructure.Common.Domain.Users;
using Infrastructure.Query.Performance.Reporting;
using Infrastructure.Common.Persistence;
using Infrastructure.Query.User;


namespace Infrastructure.Users
{
    public interface IUserManager
    {
        IList<ListUser> GetUsers(bool includeInactive = false, int startPage = 0, int pageSize = 0);
        User GetUser(Guid id);
        User GetCurrentUser();
        User SaveUser(User user, bool updateClient = true);
        void DeactivateUser(Guid id);

        IList<Group> GetUserGroups(Guid id);
        IList<UserGroupMembership> GetUserGroupMemberships(Criteria criteria);
        IList<UserByGroupMembership> GetUserByGroupMembership(Criteria criteria);

        User GetUserByEmail(string email);

        [Obsolete("Please use GetUser(string username) in the UserManager going forward.")]
        User GetUserByUsername(string username);
        User GetSystemUser();
        IEnumerable<UserActivity> GetUserActivities(Criteria criteria);
        IEnumerable<UserActivityDate> GetUserDate(Criteria criteria);
        IEnumerable<UserActivityName> GetUserName(Criteria criteria);

        User AssignGroups(User user, List<Guid> GroupIds);

        void RemoveUserFromGroup(Guid userId, Guid groupId);
        void RestoreUser(Guid id);

        UserByUsername GetUser(string username);

      

    }
}
