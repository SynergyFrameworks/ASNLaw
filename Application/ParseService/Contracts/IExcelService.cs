using Microsoft.AspNetCore.Http;
using Domain.Parse.Model;
using System.Threading.Tasks;

namespace ParseService.Services
{
    public interface IExcelService
    {
        Task<ExcelResult> UploadExcel(IFormFile file);
    }
}