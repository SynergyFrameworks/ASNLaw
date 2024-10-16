using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Datalayer.Domain;

namespace ParseService.Contracts
{
    public interface IParseParameterService
    {
        public  Task<IList<Parameter>> GetAll(string type);
    }
}
