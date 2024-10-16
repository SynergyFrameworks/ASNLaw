using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizationService.Model;
using Datalayer.Domain.Group;
using Infrastructure.CQRS.Contracts;
using Infrastructure.CQRS.Models;
using Infrastructure.CQRS.Projections;
using System;
using System.Threading.Tasks;
using static OrganizationService.PermissionNames;

namespace OrganizationService.Controllers
{
    [Route("api/series")]
    [ApiController]
    [Authorize]
    public class SeriesController : ControllerBase
    {
        private readonly IService<Series, DefaultSearch<SeriesSearchResult>> _seriesService;
        public SeriesController(IService<Series, DefaultSearch<SeriesSearchResult>> seriesService)
        {
            _seriesService = seriesService;
        }


        [HttpGet("{seriesId}")]
        [Authorize(SeriesPermissions.CAN_READ)]
        public async Task<IActionResult> Get(Guid seriesId)
        {
            return Ok(await _seriesService.Get(SeriesProjection.SeriesInformation, seriesId));
        }

        [HttpGet()]
        [Authorize(SeriesPermissions.CAN_READ)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _seriesService.Get(SeriesProjection.SeriesInformation, HttpContext.RequestAborted));
        }

        [HttpPost("query")]
        [Authorize(SeriesPermissions.CAN_READ)]
        public async Task<IActionResult> Query(DefaultSearch<SeriesSearchResult> search)
        {
            return Ok(await _seriesService.Query(search, HttpContext.RequestAborted));
        }

        [HttpPost]
        [Authorize(SeriesPermissions.CAN_CREATE)]
        public async Task<IActionResult> Post(SeriesInfo newSeries)
        {
            if (!ModelState.IsValid) return BadRequest();

            Series series = new Series()
            {
                Name = newSeries.Name,
                Description = newSeries.Description,
                ProjectId = newSeries.ProjectId,
                CreatedBy = "system",
                CreatedDate = DateTime.UtcNow
            };

            return Ok(await _seriesService.Add(series, HttpContext.RequestAborted));
        }

        [HttpPut]
        [Authorize(SeriesPermissions.CAN_WRITE)]
        public async Task<IActionResult> Put(SeriesInfo updateSeries)
        {
            if (!ModelState.IsValid && updateSeries.Id == Guid.Empty) return BadRequest();

            Series series = new Series();

            series.Id = updateSeries.Id;
            series.Name = updateSeries.Name;
            series.Description = updateSeries.Description;
            series.ProjectId = updateSeries.ProjectId;
            series.ModifiedBy = "system";
            series.ModifiedDate = DateTime.UtcNow;

            return Ok(await _seriesService.Update(series, HttpContext.RequestAborted));
        }

        [HttpDelete]
        [Route("{seriesId}")]
        [Authorize(SeriesPermissions.CAN_DELETE)]
        public async Task<IActionResult> Delete(Guid seriesId)
        {
            if (seriesId == Guid.Empty) return BadRequest();
            return Ok(await _seriesService.Delete(new Series { Id = seriesId, DeletedBy = "system", DeletedDate = DateTime.UtcNow }, HttpContext.RequestAborted));
        }
    }
}
