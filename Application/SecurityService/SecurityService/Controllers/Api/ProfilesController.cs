using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Scion.Infrastructure.Common;
using Scion.Infrastructure.Modularity;
using Scion.Infrastructure.Security;
using Scion.Infrastructure.Settings;
using Scion.Infrastructure.Web.Model.Profiles;
using System.Threading.Tasks;

namespace Scion.Infrastructure.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/platform/profiles")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize]
    public class ProfilesController : Controller
    {
        private readonly ISettingsManager _settingsManager;
        private readonly ILocalModuleCatalog _moduleCatalog;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfilesController(UserManager<ApplicationUser> userManager, ISettingsManager settingsManager, ILocalModuleCatalog moduleCatalog)
        {
            _userManager = userManager;
            _settingsManager = settingsManager;
            _moduleCatalog = moduleCatalog;
        }

        /// <summary>
        /// Get current user profile
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("currentuser")]
        public async Task<ActionResult> GetCurrentUserProfileAsync()
        {
            ApplicationUser currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (currentUser != null)
            {
                UserProfile userProfile = AbstractTypeFactory<UserProfile>.TryCreateInstance();
                userProfile.Id = currentUser.Id;
                await _settingsManager.DeepLoadSettingsAsync(userProfile);
                return Ok(userProfile);
            }
            return Ok();
        }

        /// <summary>
        /// Update current user profile
        /// </summary>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("currentuser")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateCurrentUserProfileAsync([FromBody] UserProfile userProfile)
        {
            ApplicationUser currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (currentUser.Id != userProfile.Id)
            {
                return Unauthorized();
            }
            using (await AsyncLock.GetLockByKey(userProfile.ToString()).LockAsync())
            {
                await _settingsManager.DeepSaveSettingsAsync(userProfile);
            }
            return NoContent();
        }
    }
}
