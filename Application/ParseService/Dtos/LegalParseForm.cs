using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ParseService.Controllers
{
    public class LegalParseForm
    {
        [Required]
        [FromForm(Name = "FileToParse")]
        public IFormFile File { get; set; }


    }
}
