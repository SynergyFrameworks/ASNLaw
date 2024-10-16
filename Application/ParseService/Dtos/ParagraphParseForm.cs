using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ParseService.Controllers
{
    public class ParagraphParseForm
    {
        [Required]
        [FromForm(Name = "FileToParse")]
        public IFormFile File { get; set; }


    }
}
