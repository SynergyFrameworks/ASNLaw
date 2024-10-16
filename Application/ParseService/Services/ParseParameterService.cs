using ParseService.Contracts;
using Datalayer.Contracts;
using Datalayer.Domain;
using Datalayer.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParseService.Services
{
    public class ParseParameterService : IParseParameterService
    {
        private IParseParameterRepository _parseParameterRepository;

        public ParseParameterService(IParseParameterRepository repo)
        {
            _parseParameterRepository = repo;
        }

        public async Task<IList<Parameter>> GetAll(string type)
        {
            return await _parseParameterRepository.GetAll(type);
        }

    }
}
