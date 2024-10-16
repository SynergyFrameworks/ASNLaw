using Datalayer.Contracts;
using FluentValidation;
using Infrastructure.Common.Commands;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParseService.Commands
{
    public class DeleteParseCommand
    {
        public class Command : ICommand
        {
            public Guid Id { get; set; }
            public Guid UserId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(cmd => cmd.Id).NotEmpty();
                RuleFor(cmd => cmd.UserId).NotEmpty();
            }
        }

        public class Handler : ICommandHandler<Command>
        {
            private readonly IRepository<ParseAggregate> _repository;

            public Handler(IRepository<ParseAggregate> repository)
            {
                _repository = repository;
            }
            //public async Task<Unit> Handle(Command command, CancellationToken cancellationToken)
            //{
            //    var Parse = await _repository.Find(command.Id);

            //    if (Parse is null)
            //    {
            //        throw new ArgumentNullException($"Could not find {nameof(Parse)} with id '{command.Id}'");
            //    }

            //    Parse.DeleteParse(command.UserId);

            //    await _repository.Delete(Parse);


            //    return Unit.Value;
            //}

            Task IRequestHandler<Command>.Handle(Command request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
