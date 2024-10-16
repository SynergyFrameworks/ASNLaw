using Domain.Parse.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Parse
{
    public interface IParse
    {
        Task<ICollection<ParseSegmentResult>> Parse(ParseArgs parse, IProgress<double> progress, CancellationToken cancellationToken);
    }
}