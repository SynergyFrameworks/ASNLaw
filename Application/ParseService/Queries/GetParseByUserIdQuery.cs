
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Common.Queries;
using Datalayer.Context;

namespace ParseService.Queries
{
    public class GetParsesByUserIdQuery
    {
        public class Query : IQuery<Result>
        {
            public Guid UserId { get; set; }
        }

        public class Result
        {
            public ICollection<ParseItem> Parses { get; set; }
        }

        public class ParseItem
        {
            public Guid ParseID { get; set; }
            public Guid TaskID { get; set; }
            public Guid ProjectID { get; set; }
            public Guid UserId { get; set; }
        }

        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(cmd => cmd.UserId).NotEmpty();
            }
        }

        public class Handler : IQueryHandler<Query, Result>
        {
            private readonly GlobalDbContext _gdb;

            public Handler(GlobalDbContext db)
            {
                _gdb = db;
            }

            public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
            {
                var Parses = await GetParses(query.UserId, cancellationToken);

                var result = new Result
                {
                    Parses = Parses
                };

                return result;
            }

            private async Task<ICollection<ParseItem>> GetParses(Guid userId, CancellationToken cancellationToken)
            {
                var query = from Parse in _gdb.Set<ParseAggregate>()
                            where Parse.UserId == userId
                            select new ParseItem
                            {
                                ParseID = Parse.ParseId,
                                TaskID = Parse.TaskId,
                                ProjectID = Parse.ProjectId
                            };

                var result = await query.ToListAsync(cancellationToken);

                return result;
            }

        }
    }
}
