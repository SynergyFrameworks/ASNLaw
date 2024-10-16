using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scion.Infrastructure.Common;
using Scion.Infrastructure.Domain.Reference;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scion.Infrastructure.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/platform/common")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class CommonController : Controller
    {
        private readonly ICountriesService _countriesService;

        public CommonController(ICountriesService countriesService)
        {
            _countriesService = countriesService;
        }


        [HttpGet]
        [Route("countries")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Country>>> GetCountries()
        {
            IList<Country> results = await _countriesService.GetCountriesAsync();
            return Ok(results);
        }

        [HttpGet("countries/{countryCode}/regions")]
        public async Task<ActionResult<CountryRegion[]>> GetCountryRegionsAsync(string countryCode)
        {
            IList<CountryRegion> results = await _countriesService.GetCountryRegionsAsync(countryCode);
            return Ok(results);
        }
    }
}
