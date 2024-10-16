
using Domain.Parse.Model;
using System.Threading.Tasks;

namespace ParseService.Contracts
{
    public interface IExcelMongoService
    {

        public Task CreateExcelDocument(ExcelResult document);
    }
}
