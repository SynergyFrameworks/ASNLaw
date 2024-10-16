using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Datalayer.Context;
using Datalayer.Contracts;
using Datalayer.Domain;
using Datalayer.Enum;

namespace Datalayer.Repositories
{

    public class ParseParameterRepository : IParseParameterRepository
    {
        private readonly GlobalDbContext _dbContext;

        public ParseParameterRepository(GlobalDbContext context)
        {
            _dbContext = context;
        }

        public async Task<List<Parameter>> GetAll()
        {
            return await _dbContext.Set<Parameter>().OrderBy(s => s.Weight).ToListAsync();
        }

        public async Task<List<Parameter>> GetAll(string type)
        {
            return await _dbContext.Set<Parameter>().Where(x => x.Type == type).OrderBy(s => s.Weight).ToListAsync();
        }

        //// Implement GetByIds
        //public async Task<List<ParseAggregate>> GetByIds(IEnumerable<Guid> ids)
        //{
        //    return await _dbContext.Set<ParseAggregate>().Where(p => ids.Contains(p.ParseId)).ToListAsync();
        //}
    }

}
