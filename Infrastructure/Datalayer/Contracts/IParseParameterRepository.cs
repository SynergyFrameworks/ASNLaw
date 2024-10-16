using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datalayer.Domain;

namespace Datalayer.Contracts
{
    public interface IParseParameterRepository
    {
        public  Task<List<Parameter>> GetAll();
        public Task<List<Parameter>> GetAll(string type);
    }
}
