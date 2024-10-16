using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParseService.Contracts;
using ParseService.Services;
using Domain.Parse.Model;

namespace ParseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        private readonly IExcelService _service;
        public ExcelController(IExcelService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult> UploadExcel(IFormFile file)
        {

            await _service.UploadExcel(file);
            return Ok();
        }
    }
}
