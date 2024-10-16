using Domain.Parse.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Parse.Contracts
{

    public interface IParser
    {
  
        public Task<ParseResult> ParseLegal(ParseArgs parseLegal, IProgress<double> progress, CancellationToken cancellationToken = default(CancellationToken));
        public Task<ParseResult> ParseParagraph(ParseArgs parseParagraph, IProgress<double> progress, CancellationToken cancellationToken = default(CancellationToken));
      
    }
}