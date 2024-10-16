using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Domain.Parse.Contracts;
using Domain.Parse.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Domain.Parse
{
    [SimpleJob(RunStrategy.Monitoring, targetCount: 10, id: "ParsingJob")]
    [MinColumn, Q1Column, Q3Column, MaxColumn]
    public class Parser : IParser
    {

        [Benchmark]
        public async Task<ParseResult> ParseLegal(ParseArgs parseArgs, IProgress<double> progress, CancellationToken cancellationToken = default(CancellationToken))
        {

            ParseLegal legalParser = new ParseLegal();
            ICollection<ParseSegmentResult> result = await legalParser.Parse(parseArgs, progress).ConfigureAwait(false);


            var parseResult = new ParseResult()
            {
                TransactionId = new Guid(),
                DateTimeUTC = DateTime.UtcNow,
                TaskId = parseArgs.TaskID,
                ProjectId = parseArgs.ProjectID,
                UserId = parseArgs.UserId,
                ParseSegmentResults = result,

            };
            return parseResult;
        }

        [Benchmark]
        public async Task<ParseResult> ParseParagraph(ParseArgs parseArgs, IProgress<double> progress, CancellationToken cancellationToken = default(CancellationToken))
        {

            var TxtContent = parseArgs.txtContent;
            var ID = Guid.NewGuid();
            ParseParagraph paragraphParser = new ParseParagraph();
            ICollection<ParseSegmentResult> result = await paragraphParser.Parse(parseArgs, progress).ConfigureAwait(false); ;


            var parseResult = new ParseResult()
            {
                TransactionId = ID,
                DateTimeUTC = DateTime.UtcNow,
                TaskId = parseArgs.TaskID,
                ProjectId = parseArgs.ProjectID,
                UserId = parseArgs.ProjectID,
                ParseSegmentResults = result,


            };
            return parseResult;
        }

    }

}







