using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scion.FilesService.Contracts
{
    public class FileResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
