
using FluentValidation;
using Infrastructure.Common.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Parse.Model;
using System.Collections.Generic;
using Datalayer.Context;
using MongoDB.Bson;


namespace ParseService.Queries
{
    public class GetParseQuery
    {
        public class Query : IQuery<Result>
        {
            public Guid ParseID { get; init; }
            public Guid TaskID { get; init; }
            public Guid ProjectID { get; init; }
            public Guid UserId { get; init; }
            public string strContent { get; set; }

        }

        public class Result
        {
            public ObjectId ParseID { get; init; }
            public Guid TaskID { get; init; }
            public Guid ProjectID { get; init; }
            public Guid UserId { get; init; }
            public string[] Libraries { get; set; }
            public string strContent { get; set; }

            public ICollection<ParseSegmentResult> ParseResults { get; set; }

        }

        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(cmd => cmd.ParseID).NotEmpty();
                RuleFor(cmd => cmd.TaskID).NotEmpty();
                RuleFor(cmd => cmd.ProjectID).NotEmpty();
                RuleFor(cmd => cmd.UserId).NotEmpty();
                RuleFor(cmd => cmd.strContent).NotEmpty();
            }
        }

        public class Handler : IQueryHandler<Query, Result>
        {
            private readonly GlobalDbContext _db;

            public Handler(GlobalDbContext db)
            {
                _db = db;
            }
            public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
            {
                var Parse = await GetParse(query.ParseID);

                if (Parse is null)
                {
                    throw new ArgumentNullException($"Could not find {nameof(Parse)} with id '{query.ParseID}'");
                }

                return Parse;
            }

            private async Task<Result> GetParse(Guid id)
            {
                // Convert Guid to ObjectId
                var objectId = new ObjectId(id.ToString());

                // LINQ query
                var query = from parse in _db.ParseResults // Assuming ParseResults is your DbSet for ParseResult
                            where parse.ParseId == objectId
                            select new Result
                            {
                                ParseID = parse.ParseId,    // Keep this as ObjectId or map accordingly
                                TaskID = parse.TaskId,
                                ProjectID = parse.ProjectId,
                                UserId = parse.UserId,
                                Libraries = parse.Libraries.Values.Select(l => l.ToString()).ToArray(), // Assuming Libraries is a dictionary
                                ParseResults = parse.ParseSegmentResults // Assuming ParseSegmentResults is correct
                            };

                var result = await query.FirstOrDefaultAsync();

                return result;
            }


        }
    }
}
