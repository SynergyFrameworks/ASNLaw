using Hangfire;
using Hangfire.States;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scion.Infrastructure.Jobs;
using System.Linq;

namespace Scion.Infrastructure.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/platform/jobs")]
    [Authorize(PlatformConstants.Security.Permissions.BackgroundJobsManage)]
    public class JobsController : Controller
    {
        private static readonly string[] _finalStates = { DeletedState.StateName, FailedState.StateName, SucceededState.StateName };

        /// <summary>
        /// Get background job status
        /// </summary>
        /// <param name="id">Job ID.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public ActionResult<Job> GetStatus(string id)
        {
            Job result = GetJob(id);
            return Ok(result);
        }

        private static Job GetJob(string jobId)
        {
            Job result = new Job { Id = jobId };

            global::Hangfire.Storage.StateData state = JobStorage.Current.GetConnection().GetStateData(jobId);

            if (state != null)
            {
                result.State = state.Name;
            }

            result.Completed = (state == null || _finalStates.Contains(result.State));

            return result;
        }
    }
}
