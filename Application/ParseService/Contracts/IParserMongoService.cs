using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Datalayer.Contracts;
using Domain.Parse.Model;

namespace ParseService.Contracts
{
    public interface IParserMongoService
    {

        public void CreateDocument(ParseResult document);
    }
}
