using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Parse.Contracts;
using Domain.Parse.Model;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace ParseService.Controllers
{
    [Route("api/parse")]
    public class ParseController : Controller
    {
        private readonly IParser _service;




        public ParseController(IParser service)
        {
            _service = service;


        }

        //[HttpGet, Route("{id}")]
        //public async Task<ActionResult<GetParseQuery.Result>> GetParse([FromRoute] Guid id, CancellationToken cancellationToken)
        //{
        //    GetParseQuery.Query query = new GetParseQuery.Query
        //    {
        //        ParseID = id
        //    };

        //    GetParseQuery.Result result = await _queryBus.Send(query, cancellationToken);

        //    return Ok(result);
        //}


        [HttpPost, Route("parselegal")]
        public async Task<IActionResult> CreateLegalParse(ParseArgs parseArgs, [FromForm] IFormFile file, CancellationToken cancellationToken)
        {

            if (ModelState.IsValid)
                return (IActionResult)Task.FromException<IActionResult>(exception: null);

            parseArgs.File = file;

            Progress<double> pi = new Progress<double>();
            ParseResult parse = await _service.ParseLegal(parseArgs, pi, cancellationToken);

            return Ok();


        }



        [HttpPost, Route("parsepharagraph")]
        public async Task<IActionResult> CreateParagraphParse(ParseArgs parseArgs, [FromForm] IFormFile file, CancellationToken cancellationToken)
        {

            if (ModelState.IsValid)
                return (IActionResult)Task.FromException<IActionResult>(exception: null);

            parseArgs.File = file;

            Progress<double> pi = new Progress<double>();
            return Ok(await _service.ParseParagraph(parseArgs, pi, cancellationToken).ConfigureAwait(false));


        }


    }
}