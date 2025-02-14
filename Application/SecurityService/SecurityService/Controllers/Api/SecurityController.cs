using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using OpenIddict.Abstractions;
using Scion.Infrastructure.Events;
using Scion.Infrastructure.Extensions;
using Scion.Infrastructure.Notifications;
using Scion.Infrastructure.Security;
using Scion.Infrastructure.Security.Events;
using Scion.Infrastructure.Security.Search;
using Scion.Infrastructure.Web.Model.Security;
using Scion.Infrastructure.Web.Security;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static OpenIddict.Abstractions.OpenIddictConstants;
using PlatformPermissions = Scion.Infrastructure.PlatformConstants.Security.Permissions;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Scion.Infrastructure.Web.Controllers.Api
{
    [Route("api/platform/security")]
    public class SecurityController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly Scion.Infrastructure.Security.AuthorizationOptions _securityOptions;
        private readonly UserOptionsExtended _userOptionsExtended;
        private readonly IPermissionsRegistrar _permissionsProvider;
        private readonly IUserSearchService _userSearchService;
        private readonly IRoleSearchService _roleSearchService;
        private readonly IPasswordValidator<ApplicationUser> _passwordValidator;
        private readonly IEmailSender _emailSender;
        private readonly IEventPublisher _eventPublisher;
        private readonly IUserApiKeyService _userApiKeyService;

        public SecurityController(
            SignInManager<ApplicationUser> signInManager,
            RoleManager<Role> roleManager,
            IPermissionsRegistrar permissionsProvider,
            IUserSearchService userSearchService,
            IRoleSearchService roleSearchService,
            IOptions<Scion.Infrastructure.Security.AuthorizationOptions> securityOptions,
            IOptions<UserOptionsExtended> userOptionsExtended,
            IPasswordValidator<ApplicationUser> passwordValidator,
            IEmailSender emailSender,
            IEventPublisher eventPublisher,
            IUserApiKeyService userApiKeyService)
        {
            _signInManager = signInManager;
            _securityOptions = securityOptions.Value;
            _userOptionsExtended = userOptionsExtended.Value;
            _passwordValidator = passwordValidator;
            _permissionsProvider = permissionsProvider;
            _roleManager = roleManager;
            _userSearchService = userSearchService;
            _roleSearchService = roleSearchService;
            _emailSender = emailSender;
            _eventPublisher = eventPublisher;
            _userApiKeyService = userApiKeyService;
        }

        private UserManager<ApplicationUser> UserManager => _signInManager.UserManager;
        private string CurrentUserName => User?.Identity?.Name;

        /// <summary>
        /// Sign in with user name and password
        /// </summary>
        /// <remarks>
        /// Verifies provided credentials and if succeeded returns full user details, otherwise returns 401 Unauthorized.
        /// </remarks>
        /// <param name="request">Login request.</param>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<SignInResult>> Login([FromBody] LoginRequest request)
        {
            SignInResult loginResult = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, request.RememberMe, true);
            if (loginResult.Succeeded)
            {
                ApplicationUser user = await UserManager.FindByNameAsync(request.UserName);
                await _eventPublisher.Publish(new UserLoginEvent(user));

                //Do not allow login to admin customers and rejected users
                if (await UserManager.IsInRoleAsync(user, PlatformConstants.Security.SystemRoles.Customer))
                {
                    return Ok(SignInResult.NotAllowed);
                }
            }

            return Ok(loginResult);
        }

        /// <summary>
        /// Sign out
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("logout")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Logout()
        {
            ApplicationUser user = await UserManager.FindByNameAsync(CurrentUserName);
            if (user != null)
            {
                await _signInManager.SignOutAsync();
                await _eventPublisher.Publish(new UserLogoutEvent(user));
            }

            return NoContent();
        }

        /// <summary>
        /// Get current user details
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("currentuser")]
        public async Task<ActionResult<UserDetail>> GetCurrentUser()
        {
            ApplicationUser user = await UserManager.FindByNameAsync(CurrentUserName);
            if (user == null)
            {
                return NotFound();
            }

            UserDetail result = new UserDetail
            {
                Id = user.Id,
                isAdministrator = user.IsAdministrator,
                UserName = user.UserName,
                PasswordExpired = user.PasswordExpired,
                DaysTillPasswordExpiry = PasswordExpiryHelper.ContDaysTillPasswordExpiry(user, _userOptionsExtended),
                Permissions = user.Roles.SelectMany(x => x.Permissions).Select(x => x.Name).Distinct().ToArray()
            };

            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        [Route("userinfo")]
        public async Task<ActionResult<Claim[]>> Userinfo()
        {
            ApplicationUser user = await UserManager.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest(new OpenIddictResponse
                {
                    Error = Errors.InvalidGrant,
                    ErrorDescription = "The user profile is no longer available."
                });
            }

            JObject claims = new JObject
            {
                //TODO: replace to PrinciplaClaims

                // Note: the "sub" claim is a mandatory claim and must be included in the JSON response.
                [Claims.Subject] = await UserManager.GetUserIdAsync(user)
            };

            if (User.HasClaim(Claims.Scope, Scopes.Email))
            {
                claims[Claims.Email] = await UserManager.GetEmailAsync(user);
                claims[Claims.EmailVerified] = await UserManager.IsEmailConfirmedAsync(user);
            }

            if (User.HasClaim(Claims.Scope, Scopes.Phone))
            {
                claims[Claims.PhoneNumber] = await UserManager.GetPhoneNumberAsync(user);
                claims[Claims.PhoneNumberVerified] = await UserManager.IsPhoneNumberConfirmedAsync(user);
            }

            if (User.HasClaim(Claims.Scope, Scopes.Roles))
            {
                claims["roles"] = JArray.FromObject(await UserManager.GetRolesAsync(user));
            }

            // Note: the complete list of standard claims supported by the OpenID Connect specification
            // can be found here: http://openid.net/specs/openid-connect-core-1_0.html#StandardClaims

            return Json(claims);
        }

        /// <summary>
        /// Get all registered permissions
        /// </summary>
        [HttpGet]
        [Route("permissions")]
        [Authorize(PlatformPermissions.SecurityQuery)]
        public ActionResult<Permission[]> GetAllRegisteredPermissions()
        {
            Permission[] result = _permissionsProvider.GetAllPermissions().ToArray();
            return Ok(result);
        }

        /// <summary>
        /// SearchAsync roles by keyword
        /// </summary>
        /// <param name="request">SearchAsync parameters.</param>
        [HttpPost]
        [Route("roles/search")]
        [Authorize(PlatformPermissions.SecurityQuery)]
        public async Task<ActionResult<RoleSearchResult>> SearchRoles([FromBody] RoleSearchCriteria request)
        {
            RoleSearchResult result = await _roleSearchService.SearchRolesAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Get role by ID
        /// </summary>
        /// <param name="roleName"></param>
        [HttpGet]
        [Route("roles/{roleName}")]
        [Authorize(PlatformPermissions.SecurityQuery)]
        public async Task<ActionResult<Role>> GetRole([FromRoute] string roleName)
        {
            Role result = await _roleManager.FindByNameAsync(roleName);
            return Ok(result);
        }

        /// <summary>
        /// Delete roles by ID
        /// </summary>
        /// <param name="roleIds">An array of role IDs.</param>
        [HttpDelete]
        [Route("roles")]
        [Authorize(PlatformPermissions.SecurityDelete)]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteRoles([FromQuery(Name = "ids")] string[] roleIds)
        {
            if (roleIds != null)
            {
                foreach (string roleId in roleIds)
                {
                    Role role = await _roleManager.FindByIdAsync(roleId);
                    if (role != null)
                    {
                        await _roleManager.DeleteAsync(role);
                    }
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Update an existing role or create new
        /// </summary>
        /// <param name="role"></param>
        [HttpPut]
        [Route("roles")]
        [Authorize(PlatformPermissions.SecurityUpdate)]
        public async Task<ActionResult<SecurityResult>> UpdateRole([FromBody] Role role)
        {
            bool roleExists = string.IsNullOrEmpty(role.Id)
                ? await _roleManager.RoleExistsAsync(role.Name)
                : await _roleManager.FindByIdAsync(role.Id) != null;

            IdentityResult identityResult = roleExists
                ? await _roleManager.UpdateAsync(role)
                : await _roleManager.CreateAsync(role);

            return Ok(identityResult.ToSecurityResult());
        }

        /// <summary>
        /// SearchAsync users by keyword
        /// </summary>
        /// <param name="criteria">Search criteria.</param>
        [HttpPost]
        [Route("users/search")]
        [Authorize(PlatformPermissions.SecurityQuery)]
        public async Task<ActionResult<UserSearchResult>> SearchUsers([FromBody] UserSearchCriteria criteria)
        {
            UserSearchResult result = await _userSearchService.SearchUsersAsync(criteria);
            return Ok(result);
        }

        /// <summary>
        /// Old SearchAsync users by keyword
        /// </summary>
        /// <param name="criteria">Search criteria.</param>
        /// <remarks>Obsolete, only for backward compatibility with V2</remarks>
        [HttpPost]
        [Route("users")]
        [Authorize(PlatformPermissions.SecurityQuery)]
        [Obsolete("PT-789 Left only for backward compatibility with V2")]
        public Task<ActionResult<UserSearchResult>> SearchUsersOld([FromBody] UserSearchCriteria criteria)
        {
            return SearchUsers(criteria);
        }

        /// <summary>
        /// Get user details by user name
        /// </summary>
        /// <param name="userName"></param>
        [HttpGet]
        [Route("users/{userName}")]
        [Authorize(PlatformPermissions.SecurityQuery)]
        public async Task<ActionResult<ApplicationUser>> GetUserByName([FromRoute] string userName)
        {
            ApplicationUser result = await UserManager.FindByNameAsync(userName);
            return Ok(result);
        }

        /// <summary>
        /// Get user details by user ID
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        [Route("users/id/{id}")]
        [Authorize(PlatformPermissions.SecurityQuery)]
        public async Task<ActionResult<ApplicationUser>> GetUserById([FromRoute] string id)
        {
            ApplicationUser result = await UserManager.FindByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Get user details by user email
        /// </summary>
        /// <param name="email"></param>
        [HttpGet]
        [Route("users/email/{email}")]
        [Authorize(PlatformPermissions.SecurityQuery)]
        public async Task<ActionResult<ApplicationUser>> GetUserByEmail([FromRoute] string email)
        {
            ApplicationUser result = await UserManager.FindByEmailAsync(email);
            return Ok(result);
        }

        /// <summary>
        /// Get user details by external login provider
        /// </summary>
        [HttpGet]
        [Route("users/login/external/{loginProvider}/{providerKey}")]
        [Authorize(PlatformPermissions.SecurityQuery)]
        public async Task<ActionResult<ApplicationUser>> GetUserByLogin([FromRoute] string loginProvider, [FromRoute] string providerKey)
        {
            ApplicationUser result = await UserManager.FindByLoginAsync(loginProvider, providerKey);
            return Ok(result);
        }

        /// <summary>
        /// Create new user
        /// </summary>
        [HttpPost]
        [Route("users/create")]
        [Authorize(PlatformPermissions.SecurityCreate)]
        public async Task<ActionResult<SecurityResult>> Create([FromBody] ApplicationUser newUser)
        {
            IdentityResult identityResult = string.IsNullOrEmpty(newUser.Password)
                ? await UserManager.CreateAsync(newUser)
                : await UserManager.CreateAsync(newUser, newUser.Password);

            return Ok(identityResult.ToSecurityResult());
        }

        /// <summary>
        /// Change password for current user.
        /// </summary>
        /// <param name="changePassword">Old and new passwords.</param>
        /// <returns>Result of password change</returns>
        [HttpPost]
        [Route("currentuser/changepassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public Task<ActionResult<SecurityResult>> ChangeCurrentUserPassword([FromBody] ChangePasswordRequest changePassword)
        {
            return ChangePassword(User.Identity.Name, changePassword);
        }

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="userName">user name</param>
        /// <param name="changePassword">Old and new passwords.</param>
        [HttpPost]
        [Route("users/{userName}/changepassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(PlatformPermissions.SecurityUpdate)]
        public async Task<ActionResult<SecurityResult>> ChangePassword([FromRoute] string userName, [FromBody] ChangePasswordRequest changePassword)
        {
            if (!IsUserEditable(userName))
            {
                return BadRequest(IdentityResult.Failed(new IdentityError
                {
                    Description = "It is forbidden to edit this user."
                }).ToSecurityResult());
            }

            ApplicationUser user = await UserManager.FindByNameAsync(userName);
            if (user == null)
            {
                return BadRequest(IdentityResult.Failed(new IdentityError { Description = "User not found." }).ToSecurityResult());
            }

            if (changePassword.OldPassword == changePassword.NewPassword)
            {
                return BadRequest(new SecurityResult { Errors = new[] { "You have used this password in the past. Choose another one." } });
            }

            IdentityResult result = await UserManager.ChangePasswordAsync(user, changePassword.OldPassword, changePassword.NewPassword);
            if (result.Succeeded && user.PasswordExpired)
            {
                user.PasswordExpired = false;
                await UserManager.UpdateAsync(user);
            }

            return Ok(result.ToSecurityResult());
        }

        /// <summary>
        /// Reset password confirmation
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="resetPasswordConfirm">Password reset information containing new password.</param>
        /// <returns>Result of password reset.</returns>
        [HttpPost]
        [Route("users/{userName}/resetpassword")]
        [Authorize(PlatformPermissions.SecurityUpdate)]
        public async Task<ActionResult<SecurityResult>> ResetPassword([FromRoute] string userName, [FromBody] ResetPasswordConfirmRequest resetPasswordConfirm)
        {
            ApplicationUser user = await UserManager.FindByNameAsync(userName);
            if (user is null)
            {
                return BadRequest(IdentityResult.Failed(new IdentityError { Description = "User not found" }).ToSecurityResult());
            }

            if (!IsUserEditable(user.UserName))
            {
                return BadRequest(IdentityResult.Failed(new IdentityError { Description = "It is forbidden to edit this user." }).ToSecurityResult());
            }

            string token = await UserManager.GeneratePasswordResetTokenAsync(user);

            IdentityResult result = await UserManager.ResetPasswordAsync(user, token, resetPasswordConfirm.NewPassword);
            if (result.Succeeded)
            {
                user = await UserManager.FindByNameAsync(userName);

                if (user.PasswordExpired != resetPasswordConfirm.ForcePasswordChangeOnNextSignIn)
                {
                    user.PasswordExpired = resetPasswordConfirm.ForcePasswordChangeOnNextSignIn;
                    await UserManager.UpdateAsync(user);
                }
            }

            return Ok(result.ToSecurityResult());
        }

        /// <summary>
        /// Reset password confirmation
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="resetPasswordConfirm">New password.</param>
        [HttpPost]
        [Route("users/{userId}/resetpasswordconfirm")]
        [AllowAnonymous]
        public async Task<ActionResult<SecurityResult>> ResetPasswordByToken([FromRoute] string userId, [FromBody] ResetPasswordConfirmRequest resetPasswordConfirm)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest(IdentityResult.Failed(new IdentityError { Description = "User not found" }).ToSecurityResult());
            }

            if (!IsUserEditable(user.UserName))
            {
                return BadRequest(IdentityResult.Failed(new IdentityError { Description = "It is forbidden to edit this user." }).ToSecurityResult());
            }

            IdentityResult result = await UserManager.ResetPasswordAsync(user, resetPasswordConfirm.Token, resetPasswordConfirm.NewPassword);
            if (result.Succeeded && user.PasswordExpired)
            {
                user.PasswordExpired = false;
                await UserManager.UpdateAsync(user);
            }

            return Ok(result.ToSecurityResult());
        }

        /// <summary>
        /// Validate password reset token
        /// </summary>
        [HttpPost]
        [Route("users/{userId}/validatepasswordresettoken")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> ValidatePasswordResetToken(string userId, [FromBody] ValidatePasswordResetTokenRequest resetPasswordToken)
        {
            ApplicationUser applicationUser = await UserManager.FindByIdAsync(userId);
            string tokenProvider = UserManager.Options.Tokens.PasswordResetTokenProvider;
            bool result = await UserManager.VerifyUserTokenAsync(applicationUser, tokenProvider, "ResetPassword", resetPasswordToken.Token);

            return Ok(result);
        }

        /// <summary>
        /// Send email with instructions on how to reset user password.
        /// </summary>
        /// <remarks>
        /// Verifies provided userName and (if succeeded) sends email.
        /// </remarks>
        [HttpPost]
        [Route("users/{loginOrEmail}/requestpasswordreset")]
        [AllowAnonymous]
        public async Task<ActionResult> RequestPasswordReset(string loginOrEmail)
        {
            ApplicationUser user = await UserManager.FindByNameAsync(loginOrEmail);
            if (user == null)
            {
                user = await UserManager.FindByEmailAsync(loginOrEmail);
            }

            //Do not permit rejected users and customers
            if (user?.Email != null && IsUserEditable(user.UserName) && !(await UserManager.IsInRoleAsync(user, PlatformConstants.Security.SystemRoles.Customer)))
            {
                string token = await UserManager.GeneratePasswordResetTokenAsync(user);
                string callbackUrl = $"{Request.Scheme}://{Request.Host}/#/resetpassword/{user.Id}/{token}";

                await _emailSender.SendEmailAsync(user.Email, "Reset password", callbackUrl.ToString());
            }

            return Ok();
        }

        [HttpPost]
        [Route("validatepassword")]
        [Authorize]
        public async Task<ActionResult<IdentityResult>> ValidatePassword([FromBody] string password)
        {
            ApplicationUser currentUser = await UserManager.FindByNameAsync(User.Identity.Name);
            IdentityResult result = await _passwordValidator.ValidateAsync(UserManager, currentUser, password);

            return Ok(result);
        }

        [HttpPost]
        [Route("validateuserpassword")]
        [Authorize(PlatformPermissions.SecurityUpdate)]
        public async Task<ActionResult<IdentityResult>> ValidateUserPassword([FromBody] ChangePasswordRequest validatePassword)
        {
            if (validatePassword == null)
            {
                throw new ArgumentNullException(nameof(validatePassword));
            }

            if (validatePassword.UserName.IsNullOrEmpty() || !IsUserEditable(validatePassword.UserName))
            {
                return BadRequest(IdentityResult.Failed(new IdentityError { Description = "It is forbidden to edit this user." }));
            }

            ApplicationUser user = await UserManager.FindByNameAsync(validatePassword.UserName);
            IdentityResult result = await _passwordValidator.ValidateAsync(UserManager, user, validatePassword.NewPassword);

            return Ok(result);
        }

        /// <summary>
        /// Update user details by user ID
        /// </summary>
        /// <param name="user">User details.</param>
        [HttpPut]
        [Route("users")]
        [Authorize(PlatformPermissions.SecurityUpdate)]
        public async Task<ActionResult<SecurityResult>> Update([FromBody] ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (!IsUserEditable(user.UserName))
            {
                return Ok(IdentityResult.Failed(new IdentityError { Description = "It is forbidden to edit this user." }).ToSecurityResult());
            }

            ApplicationUser applicationUser = await UserManager.FindByIdAsync(user.Id);
            if (applicationUser.EmailConfirmed != user.EmailConfirmed
                && !Request.HttpContext.User.HasGlobalPermission(PlatformPermissions.SecurityVerifyEmail))
            {
                return Unauthorized();
            }

            if (!applicationUser.Email.EqualsInvariant(user.Email))
            {
                // SetEmailAsync also: sets EmailConfirmed to false and updates the SecurityStamp
                await UserManager.SetEmailAsync(user, user.Email);
            }

            if (user.LastPasswordChangedDate != applicationUser.LastPasswordChangedDate)
            {
                user.LastPasswordChangedDate = applicationUser.LastPasswordChangedDate;
            }

            IdentityResult result = await UserManager.UpdateAsync(user);

            return Ok(result.ToSecurityResult());
        }

        /// <summary>
        /// Delete users by name
        /// </summary>
        /// <param name="names">An array of user names.</param>
        [HttpDelete]
        [Route("users")]
        [Authorize(PlatformPermissions.SecurityDelete)]
        public async Task<ActionResult> Delete([FromQuery] string[] names)
        {
            if (names == null)
            {
                throw new ArgumentNullException(nameof(names));
            }

            if (names.Any(x => !IsUserEditable(x)))
            {
                return BadRequest(new IdentityError() { Description = "It is forbidden to edit these users." });
            }

            foreach (string userName in names)
            {
                ApplicationUser user = await UserManager.FindByNameAsync(userName);
                if (user != null)
                {
                    IdentityResult result = await UserManager.DeleteAsync(user);
                    if (!result.Succeeded)
                    {
                        return BadRequest(result);
                    }
                }
            }

            return Ok(IdentityResult.Success);
        }

        /// <summary>
        /// Checks if user locked
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("users/{id}/locked")]
        [Authorize(PlatformPermissions.SecurityQuery)]
        public async Task<ActionResult<UserLockedResult>> IsUserLocked([FromRoute] string id)
        {
            UserLockedResult result = new UserLockedResult(false);
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                result.Locked = await UserManager.IsLockedOutAsync(user);
            }

            return Ok(result);
        }

        /// <summary>
        /// Lock user
        /// </summary>
        /// <param name="id">>User id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("users/{id}/lock")]
        [Authorize(PlatformPermissions.SecurityUpdate)]
        public async Task<ActionResult<SecurityResult>> LockUser([FromRoute] string id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await UserManager.SetLockoutEndDateAsync(user, DateTime.MaxValue);
                return Ok(result.ToSecurityResult());
            }

            return Ok(IdentityResult.Failed().ToSecurityResult());
        }

        /// <summary>
        /// Unlock user
        /// </summary>
        /// <param name="id">>User id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("users/{id}/unlock")]
        [Authorize(PlatformPermissions.SecurityUpdate)]
        public async Task<ActionResult<SecurityResult>> UnlockUser([FromRoute] string id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                await UserManager.ResetAccessFailedCountAsync(user);
                IdentityResult result = await UserManager.SetLockoutEndDateAsync(user, DateTimeOffset.MinValue);
                return Ok(result.ToSecurityResult());
            }

            return Ok(IdentityResult.Failed().ToSecurityResult());
        }

        [HttpGet]
        [Route("users/{id}/apikeys")]
        [Authorize(PlatformPermissions.SecurityQuery)]
        public async Task<ActionResult<UserApiKey[]>> GetUserApiKeys([FromRoute] string id)
        {
            UserApiKey[] result = await _userApiKeyService.GetAllUserApiKeysAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [Route("users/apikeys")]
        [Authorize(PlatformPermissions.SecurityUpdate)]
        public async Task<ActionResult<UserApiKey[]>> SaveUserApiKey([FromBody] UserApiKey userApiKey)
        {
            await _userApiKeyService.SaveApiKeysAsync(new[] { userApiKey });
            return Ok();
        }

        [HttpPut]
        [Route("users/apikeys")]
        [Authorize(PlatformPermissions.SecurityUpdate)]
        public Task<ActionResult<UserApiKey[]>> UpdateUserApiKey([FromBody] UserApiKey userApiKey)
        {
            return SaveUserApiKey(userApiKey);
        }

        [HttpDelete]
        [Route("users/apikeys")]
        [Authorize(PlatformPermissions.SecurityDelete)]
        public async Task<ActionResult<UserApiKey[]>> DeleteUserApiKeys([FromQuery] string[] ids)
        {
            await _userApiKeyService.DeleteApiKeysAsync(ids);
            return Ok();
        }

        /// <summary>
        /// Verify user email
        /// </summary>
        /// <param name="userId"></param>
        [HttpPost]
        [Route("users/{userId}/sendVerificationEmail")]
        [Authorize(PlatformPermissions.SecurityVerifyEmail)]
        public async Task<ActionResult> SendVerificationEmail([FromRoute] string userId)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest(IdentityResult.Failed(new IdentityError { Description = "User not found" }).ToSecurityResult());
            }

            if (!IsUserEditable(user.UserName))
            {
                return BadRequest(IdentityResult.Failed(new IdentityError { Description = "It is forbidden to edit this user." }).ToSecurityResult());
            }

            await _eventPublisher.Publish(new UserVerificationEmailEvent(user));

            return Ok();
        }

        #region PT-788 Obsolete methods

        [Obsolete("use /roles/search instead")]
        [HttpPost]
        [Route("roles")]
        [Authorize(PlatformPermissions.SecurityQuery)]
        public Task<ActionResult<RoleSearchResult>> SearchRolesObsolete([FromBody] RoleSearchCriteria request)
        {
            return SearchRoles(request);
        }

        /// <summary>
        /// Resets password for current user.
        /// </summary>
        /// <param name="resetPassword">Password reset information containing new password.</param>
        /// <returns>Result of password reset.</returns>
        [HttpPost]
        [Route("currentuser/resetpassword")]
        [AllowAnonymous]
        [Obsolete("use /currentuser/changepassword instead")]
        public Task<ActionResult<SecurityResult>> ResetCurrentUserPassword([FromBody] ResetPasswordConfirmRequest resetPassword)
        {
            return ResetPassword(User.Identity.Name, resetPassword);
        }

        #endregion PT-788 Obsolete methods

        private bool IsUserEditable(string userName)
        {
            return _securityOptions.NonEditableUsers?.FirstOrDefault(x => x.EqualsInvariant(userName)) == null;
        }
    }
}
