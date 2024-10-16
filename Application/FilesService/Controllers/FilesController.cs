using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Scion.FilesService.Contracts;
using Scion.FilesService.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilesService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly ILogger<FilesController> _logger;
        private readonly IFilesService _service;

        public FilesController(ILogger<FilesController> logger, IFilesService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IEnumerable<File> Get()
        {
            return _service.GetFiles();
        }

        [HttpGet]
        [Route("{fileId}")]
        public async Task<FileStreamResult> GetFile(string fileId)
        {
            var fileStream = await _service.GetFile(fileId);
            return File(fileStream, fileStream.ContentType, fileStream.SourceName);
        }

        [HttpPost]
        public async Task<ActionResult> Post(IFormFile file)
        {
            var result = await _service.UploadFile(file);
            return result.IsSuccess ? Ok() : StatusCode(StatusCodes.Status406NotAcceptable, result.Message);
        }

        [HttpDelete]
        [Route("{fileId}")]
        public async Task<ActionResult> Delete(string fileId)
        {
            var result = await _service.DeleteFile(fileId);
            return result.IsSuccess ? Ok() : StatusCode(StatusCodes.Status404NotFound, result.Message);
        }
    }
}
