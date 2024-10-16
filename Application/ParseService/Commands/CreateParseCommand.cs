using Datalayer.Contracts;
using FluentValidation;
using Infrastructure.Common.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Domain.Parse.Model.ParseTypeDef;

namespace ParseService.Commands
{
    public class CreateParseCommand
    {
        public class Command : ICommand<Result>
        {
            public Guid ParseId { get; init; }
            public ParseType ParseType { get; set; }
            public Guid UserId { get; set; }
            public Guid TaskId { get; set; }
            public Guid ProjectId { get; set; }
            public Guid TenantId { get; set; }
            public int DateTimeUTC { get; set; }

            //public IDictionary<String, Library> Libraries { get; init; }

        }

        public class Result
        {
            public Guid ParseId { get; init; }
            public ParseType ParseType { get; init; }
            public Guid UserId { get; set; }
            public Guid TaskId { get; set; }
            public Guid ProjectId { get; set; }
            public Guid TenantId { get; set; }
            public DateTime DateTimeUTC { get; set; }

            // public IDictionary<String, Library> Libraries { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(e => e.ParseId).NotEmpty();
                RuleFor(e => e.ParseType).NotEmpty();
                RuleFor(e => e.UserId).NotEmpty();
                RuleFor(e => e.TaskId).NotEmpty();
                RuleFor(e => e.ProjectId).NotEmpty();
                RuleFor(e => e.TenantId).NotEmpty();
                RuleFor(e => e.DateTimeUTC).GreaterThanOrEqualTo(2020);
            }
        }

        public class Handler : ICommandHandler<Command, Result>
        {
            private readonly IRepository<ParseAggregate> _repository;

            public Handler(IRepository<ParseAggregate> repository)
            {
                _repository = repository;
            }
            public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
            {
                ParseAggregate Parse = ParseAggregate.CreateParse(command.TaskId, command.ProjectId, command.UserId, command.TenantId);

                await _repository.Add(Parse);

                Result result = new Result
                {
                    ParseId = Parse.Id,
                    UserId = Parse.UserId,
                    TaskId = Parse.TaskId,
                    ProjectId = Parse.ParseId,
                    TenantId = Parse.TenantId,
                    DateTimeUTC = Parse.DateTimeUTC
                };

                return result;
            }
        }
    }
}
