using System.Threading.Tasks;
using ParseService.Contracts;
using Domain.Parse.Model;
using Datalayer.Contracts;

namespace ParseService.Services
{
    public class ParserMongoService : IParserMongoService
    {
        private readonly IMongoRepository<IDocument> _mongo ;

        public ParserMongoService (IMongoRepository<IDocument> mongo)
        {
            _mongo = mongo;
        }

        public void CreateDocument(ParseResult document)
        {
            throw new System.NotImplementedException();
        }
    }


}
