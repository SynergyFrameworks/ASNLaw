using Infrastructure.Common.Queries;
using Infrastructure.CQRS.Queries;
using Microsoft.AspNetCore.Mvc;
using ParseService.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParseService.Controllers
{
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IQueryBus _queryBus;

        public UserController(IQueryBus queryBus)
        {
            _queryBus = queryBus;
        }

        [HttpGet, Route("{userId}/Parses")]
        public async Task<ActionResult<GetParsesByUserIdQuery.Result>> GetParsesByUserId([FromRoute] Guid userId, CancellationToken cancellationToken)
        {
            var query = new GetParsesByUserIdQuery.Query
            {
                UserId = userId
            };

            var result = await _queryBus.Send(query, cancellationToken);

            return Ok(result);
        }
    }
}
