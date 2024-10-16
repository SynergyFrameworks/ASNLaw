using log4net;
using Newtonsoft.Json;
using Infrastructure.Common.Domain.Users;
using Infrastructure.Common.Extensions;
using Infrastructure.Common.Persistence;
using Infrastructure.Common.Persistence.Extensions;
using Infrastructure.Query.Performance.Reporting;
using Infrastructure.Query.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Infrastructure.Common;

namespace Infrastructure.Users
{
    public class UserManager : BaseDataReadManager, IUserManager
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(UserManager));
        public IEntityManager EntityManager { get; set; }
        public IReadOnlyDataManager ReadOnlyDataManager { get; set; }

        public IList<ListUser> GetUsers(bool includeInactive = false, int startPage = 0, int pageSize = 0)
        {
            return ReadOnlyDataManager.FindAll<ListUser>(new Criteria { Parameters = new SortedDictionary<string, object> { { "includeInactive", includeInactive } } });
        }


        public User GetUser(Guid id)
        {
            try
            {
                return EntityManager.Find<User>(id);
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("Error finding User {0}. {1}", id, ex);
                return null;
            }
        }


        public User GetCurrentUser()
        {
            Guid userId = Thread.CurrentThread.GetUserId();
            return GetUser(userId);
        }


        public IList<Group> GetUserGroups(Guid id)
        {
            try
            {
                Log.InfoFormat("Retreiving Groups for user {0}", id);
                List<Group> list = EntityManager.FindAllByNamedQuery<GroupMembership>("FindByUser",
                                                                       new Dictionary<string, object> { { "userId", id } })
                                 .Select(m => m.MemberGroup).ToList();
                return list;
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("Error retreviving groups for user {0}. {1}", id, ex);
                throw;
            }
        }


        public IList<UserGroupMembership> GetUserGroupMemberships(Criteria criteria)
        {
            return ReadOnlyDataManager.FindAll<UserGroupMembership>(criteria);
        }


        public IList<UserByGroupMembership> GetUserByGroupMembership(Criteria criteria)
        {
            return ReadOnlyDataManager.FindAll<UserByGroupMembership>(criteria);
        }

        //
        public IList<string> GetUserGroupNames(Guid id)
        {
            var list = GetUserGroups(id);

            return list.Select(group => group.Name).ToList();
        }


        public User SaveUser(User user, bool updateClient = true)
        {
            try
            {
                Tenant currentTenant = EntityManager.Find<Tenant>(Thread.CurrentThread.GetAssignedTenantId());
                //var existingUser = true;
                if (user.Id == Guid.Empty)
                {
                    User exists = null;
                    try
                    {
                        exists = EntityManager.FindByNamedQuery<User>("FindByUsername",
                            new Dictionary<string, object>
                            {
                                {"username", user.Username}
                            });
                        user.Id = exists.Id;
                    }
                    catch (Exception)
                    {
                        Log.InfoFormat("Username {0} not found, creating new user", user.Username);
                    }

                    if (exists == null && currentTenant != null)
                    {
                        //existingUser = false;
                        user.Tenant = currentTenant;
                    }
                }

                user = EntityManager.CreateOrUpdate(user);
                return user;
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("Error saving User {0}. {1}", user.Username, ex);
                throw;
            }

        }


        public void DeactivateUser(Guid id)
        {
            try
            {
                Log.InfoFormat("Deactivating User {0}", id);
                User user = GetUser(id);
                user.IsInactive = true;
                SaveUser(user);
            }
            catch (Exception ex)
            {
                Log.InfoFormat("Error inactiving user: {0}.  {1}", id, ex);
                throw;
            }
        }


        public User GetUserByEmail(string email)
        {
            try
            {
                return EntityManager.FindByNamedQuery<User>("FindByEmail", new Dictionary<string, object> { { "email", email } });
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("Error retreiving user by email {0}. {1}", email, ex);
                return null;
            }
        }


        public User GetUserByUsername(string username)
        {
            try
            {
                return EntityManager.FindByNamedQuery<User>("FindByUsername", new Dictionary<string, object> { { "username", username } });
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("Error retreiving user by username {0}. {1}", username, ex);
                return null;
            }
        }


        public User GetSystemUser()
        {
            return GetUserByUsername("SYSTEM");
        }

        public IEnumerable<UserActivity> GetUserActivities(Criteria criteria)
        {
            return ReadOnlyDataManager.FindAll<UserActivity>(criteria);
        }


        public IEnumerable<UserActivityDate> GetUserDate(Criteria criteria)
        {
            return ReadOnlyDataManager.FindAll<UserActivityDate>(criteria);
        }

        public IEnumerable<UserActivityName> GetUserName(Criteria criteria)
        {
            return ReadOnlyDataManager.FindAll<UserActivityName>(criteria);
        }


        public void RemoveUserFromGroup(Guid userId, Guid groupId)
        {
            Group group = EntityManager.Find<Group>(groupId);
            GroupMembership member = group.GroupMembers.FirstOrDefault(g => g.User.Id == userId);
            if (member != null)
            {
                group.GroupMembers.Remove(member);
               // EntityManager.CreateOrUpdate(group);
            }
        }


        public void RestoreUser(Guid userId)
        {
            User user = EntityManager.Find<User>(userId);
            user.IsInactive = false;
            EntityManager.CreateOrUpdate(user);
        }

        public User AssignGroups(User user, List<Guid> groupIds)   ///TODO: 
        {
           User existing = EntityManager.Find<User>(user.Id);
           Tenant currentTenant = EntityManager.Find<Tenant>(Thread.CurrentThread.GetAssignedTenantId());
           User usernameExist = GetUserByUsername(user.Username);

            if (existing != null)
            {
                existing.UpdateEditableFields(user);
                user = EntityManager.CreateOrUpdate(existing);
            }
            else if (usernameExist != null && usernameExist.Tenant == currentTenant && user.Id.Equals(new Guid())) //this filters when userId does not exist but trying to add new user with existing username and tenant combination
            {
                throw new Exception($"Username: {user.Username} already exist in our database.");
            }
            else
            {
                user = EntityManager.CreateOrUpdate(user);
            }
            EntityManager.Flush();

            if (groupIds == null || !groupIds.Any())
                return user;
            IList<Group> groups = EntityManager.FindAllByNamedQuery<Group>("FindAllByGroupIds", new Dictionary<string, object> { { "groupIds", groupIds } });
            foreach (Group group in groups)
            {
                bool isMember = group.GroupMembers.Any(m => m.User.Id == user.Id);
                if (!isMember)
                {

                    GroupUser userforgroup = new GroupUser() { 
                    Id = user.Id,
                     FirstName = user.FirstName,
                     LastName= user.LastName
                    
                    };
                    GroupMembership member = new GroupMembership()
                    {
                        MemberGroup = group,
                        User = userforgroup
                    };
                    group.GroupMembers.Add(member);
                   // EntityManager.CreateOrUpdate(group);
                }
            }
            return user;
        }

        //public User GetUserApplicationsByUsername(string username)
        //{
        //    try
        //    {
        //        Criteria criteria = new Criteria()
        //        {
        //            Parameters = new SortedDictionary<string, object>
        //            {
        //                { "Username",username}
        //            }
        //        };

        //        IList<UserApplication> userApplications = ReadOnlyDataManager.FindAll<UserApplication>(criteria);

        //        if (userApplications == null || !userApplications.Any())
        //        {
        //            throw new Exception($"User {username} not found in our database.");
        //        }

        //        User user = userApplications.GroupBy(u => new { u.Id, u.Email, u.FirstName, u.Username, u.LastName, u.IsSystemUser, u.LastLoggedInDate, u.Picture }).Select(i => new User
        //        {
        //            Id = i.Key.Id,
        //            Username = i.Key.Username,
        //            FirstName = i.Key.FirstName,
        //            LastName = i.Key.LastName,
        //            IsSystemUser = i.Key.IsSystemUser,
        //            LastLoggedInDate = i.Key.LastLoggedInDate,
        //            Picture = i.Key.Picture,
        //            Applications = i.Select(t => new Application
        //            {
        //                Id = t.ApplicationId,
        //                Name = t.ApplicationName,
        //                Group = new Group
        //                {
        //                    Id = t.GroupId,
        //                    Name = t.GroupName,
        //                    IsInactive = t.GroupMembershipIsInactive
        //                },
        //                ApplicationSettings = t.Settings == null ? new Dictionary<string, object>() : JsonConvert.DeserializeObject<Dictionary<string, object>>(t.Settings.ToString().Replace("\r\n", string.Empty))
        //            }).Where(e => e.Id != Guid.Empty && !e.Group.IsInactive).ToList(),
        //        }).First();

        //        return user;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.ErrorFormat("Error retreiving user and user's applications by username {0}. {1}", username, ex);
        //        return null;
        //    }
        //}

        public UserByUsername GetUser(string username)
        {
            Criteria criteria = new Criteria()
            {
                Parameters = new SortedDictionary<string, object>
                    {
                        { "userName",username}
                    }
            };
            return ReadOnlyDataManager.Find<UserByUsername>(criteria);
        }



    }
}
