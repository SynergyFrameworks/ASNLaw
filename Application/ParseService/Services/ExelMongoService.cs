//using Datalayer.Contracts;
//using Domain.Parse.Model;
//using ParseService.Contracts;
//using System.Threading.Tasks;

//namespace ParseService.Services
//{
//    public class ExcelMongoService : IExcelMongoService
//    {
//        private readonly IMongoRepository<ExcelResult> _repo;

//        public ExcelMongoService(IMongoRepository<ExcelResult> repo)
//        {
//            _repo = repo;
//        }

//        public Task CreateExcelDocument(ExcelResult document)
//        {
//            return _repo.InsertOneAsync(document);
//        }

//    }
//}
