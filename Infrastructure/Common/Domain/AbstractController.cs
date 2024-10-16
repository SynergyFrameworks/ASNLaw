using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Domain
{
    public class AbstractController: ControllerBase
    {
        protected IActionResult handleException()
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
