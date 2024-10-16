using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Infrastructure.Common.Caching;
using Infrastructure.Common;
using Infrastructure.Common.Events;
using Infrastructure.Common.Extensions;
using Infrastructure.Security.Caching;
using Infrastructure.Security.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
    public class CustomUserManager : AspNetUserManager<ApplicationUser>
    {
        private readonly IPlatformMemoryCache _memoryCache;
        private readonly RoleManager<Role> _roleManager;
        private readonly IEventPublisher _eventPublisher;

        public CustomUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher,
                                 IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
                                 ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services,
                                 ILogger<UserManager<ApplicationUser>> logger, RoleManager<Role> roleManager, IPlatformMemoryCache memoryCache, IEventPublisher eventPublisher)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _memoryCache = memoryCache;
            _roleManager = roleManager;
            _eventPublisher = eventPublisher;
        }

        public override async Task<ApplicationUser> FindByLoginAsync(string loginProvider, string providerKey)
        {
            string cacheKey = CacheKey.With(GetType(), nameof(FindByLoginAsync), loginProvider, providerKey);
            ApplicationUser result = await _memoryCache.GetOrCreateExclusiveAsync(cacheKey, async (cacheEntry) =>
            {
                ApplicationUser user = await base.FindByLoginAsync(loginProvider, providerKey);
                if (user != null)
                {
                    await LoadUserDetailsAsync(user);
                    cacheEntry.AddExpirationToken(SecurityCache.CreateChangeTokenForUser(user));
                }
                return user;
            }, cacheNullValue: false);

            return result;
        }

        public override async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            string cacheKey = CacheKey.With(GetType(), nameof(FindByEmailAsync), email);
            ApplicationUser result = await _memoryCache.GetOrCreateExclusiveAsync(cacheKey, async (cacheEntry) =>
            {
                ApplicationUser user = await base.FindByEmailAsync(email);
                if (user != null)
                {
                    await LoadUserDetailsAsync(user);
                    cacheEntry.AddExpirationToken(SecurityCache.CreateChangeTokenForUser(user));
                }
                return user;
            }, cacheNullValue: false);
            return result;
        }

        public override async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            string cacheKey = CacheKey.With(GetType(), nameof(FindByNameAsync), userName);
            ApplicationUser result = await _memoryCache.GetOrCreateExclusiveAsync(cacheKey, async (cacheEntry) =>
            {
                ApplicationUser user = await base.FindByNameAsync(userName);
                if (user != null)
                {
                    await LoadUserDetailsAsync(user);
                    cacheEntry.AddExpirationToken(SecurityCache.CreateChangeTokenForUser(user));
                }
                return user;
            }, cacheNullValue: false);
            return result;
        }

        public override async Task<ApplicationUser> FindByIdAsync(string userId)
        {
            string cacheKey = CacheKey.With(GetType(), nameof(FindByIdAsync), userId);
            ApplicationUser result = await _memoryCache.GetOrCreateExclusiveAsync(cacheKey, async (cacheEntry) =>
            {
                ApplicationUser user = await base.FindByIdAsync(userId);
                if (user != null)
                {
                    await LoadUserDetailsAsync(user);
                    cacheEntry.AddExpirationToken(SecurityCache.CreateChangeTokenForUser(user));
                }
                return user;
            }, cacheNullValue: false);
            return result;
        }

        public override async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string newPassword)
        {
            //It is important to call base.FindByIdAsync method to avoid of update a cached user.
            ApplicationUser existUser = await base.FindByIdAsync(user.Id);

            IdentityResult result = await base.ResetPasswordAsync(existUser, token, newPassword);
            if (result == IdentityResult.Success)
            {
                SecurityCache.ExpireUser(user);
            }

            return result;
        }

        public override async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword)
        {
            IdentityResult result = await base.ChangePasswordAsync(user, currentPassword, newPassword);
            if (result == IdentityResult.Success)
            {
                SecurityCache.ExpireUser(user);
            }

            return result;
        }

        public override async Task<IdentityResult> DeleteAsync(ApplicationUser user)
        {
            List<GenericChangedEntry<ApplicationUser>> changedEntries = new List<GenericChangedEntry<ApplicationUser>>
            {
                new GenericChangedEntry<ApplicationUser>(user, EntryState.Deleted)
            };
            await _eventPublisher.Publish(new UserChangingEvent(changedEntries));
            IdentityResult result = await base.DeleteAsync(user);
            if (result.Succeeded)
            {
                await _eventPublisher.Publish(new UserChangedEvent(changedEntries));
                SecurityCache.ExpireUser(user);
            }
            return result;
        }

        protected override async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
        {
            ApplicationUser existentUser = await LoadExistingUser(user);

            //We cant update not existing user
            if (existentUser == null)
            {
                return IdentityResult.Failed(ErrorDescriber.DefaultError());
            }

            List<GenericChangedEntry<ApplicationUser>> changedEntries = new List<GenericChangedEntry<ApplicationUser>>
            {
                new GenericChangedEntry<ApplicationUser>(user, existentUser, EntryState.Modified)
            };

            await _eventPublisher.Publish(new UserChangingEvent(changedEntries));

            //We need to use Patch method to update already tracked by DbContent entity, unless the UpdateAsync for passed user will throw exception
            //"The instance of entity type 'ApplicationUser' cannot be tracked because another instance with the same key value for {'Id'} is already being tracked. When attaching existing entities, ensure that only one entity instance with a given key value is attached"
            user.Patch(existentUser);

            IdentityResult result = await base.UpdateUserAsync(existentUser);

            if (result.Succeeded)
            {
                await _eventPublisher.Publish(new UserChangedEvent(changedEntries));
                SecurityCache.ExpireUser(existentUser);
            }

            return result;
        }

        public override async Task<IdentityResult> UpdateAsync(ApplicationUser user)
        {
            IdentityResult result = await base.UpdateAsync(user);

            if (result.Succeeded && user.Roles != null)
            {
                IList<string> targetRoles = (await GetRolesAsync(user));
                var sourceRoles = user.Roles.Select(x => x.Name);

                //Add
                foreach (var newRole in sourceRoles.Except(targetRoles))
                {
                    await AddToRoleAsync(user, newRole);
                }

                //Remove
                foreach (var removeRole in targetRoles.Except(sourceRoles))
                {
                    await RemoveFromRoleAsync(user, removeRole);
                }
            }

            return result;
        }

        public override async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            List<GenericChangedEntry<ApplicationUser>> changedEntries = new List<GenericChangedEntry<ApplicationUser>>
            {
                new GenericChangedEntry<ApplicationUser>(user, EntryState.Added)
            };
            await _eventPublisher.Publish(new UserChangingEvent(changedEntries));
            IdentityResult result = await base.CreateAsync(user);
            if (result.Succeeded)
            {
                await _eventPublisher.Publish(new UserChangedEvent(changedEntries));
                if (!user.Roles.IsNullOrEmpty())
                {
                    //Add
                    foreach (Role newRole in user.Roles)
                    {
                        await AddToRoleAsync(user, newRole.Name);
                    }
                }
                SecurityCache.ExpireUser(user);
            }
            return result;
        }



        /// <summary>
        /// Load detailed user information: Roles, external logins, claims (permissions)
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        protected virtual async Task LoadUserDetailsAsync(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.Roles = new List<Role>();
            foreach (string roleName in await base.GetRolesAsync(user))
            {
                Role role = await _roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    user.Roles.Add(role);
                }
            }

            // Read claims and convert to permissions (compatibility with v2)
            user.Permissions = user.Roles.SelectMany(x => x.Permissions).Select(x => x.Name).Distinct().ToArray();

            // Read associated logins (compatibility with v2)
            IList<UserLoginInfo> logins = await base.GetLoginsAsync(user);
            user.Logins = logins.Select(x => new ApplicationUserLogin() { LoginProvider = x.LoginProvider, ProviderKey = x.ProviderKey }).ToArray();
        }

        /// <summary>
        /// Finds existing user and loads its details
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Returns null, if no user found, otherwise user with details.</returns>
        protected virtual async Task<ApplicationUser> LoadExistingUser(ApplicationUser user)
        {
            ApplicationUser result = null;

            if (!string.IsNullOrEmpty(user.Id))
            {
                //It is important to call base.FindByIdAsync method to avoid of update a cached user.
                result = await base.FindByIdAsync(user.Id);
            }
            if (result == null)
            {
                //It is important to call base.FindByNameAsync method to avoid of update a cached user.
                result = await base.FindByNameAsync(user.UserName);
            }

            if (result != null)
            {
                await LoadUserDetailsAsync(result);
            }

            return result;
        }
    }
}
