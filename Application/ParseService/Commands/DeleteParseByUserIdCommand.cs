using FluentValidation;
using Infrastructure.CQRS.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Common.Commands;
using Datalayer.Contracts;
using Datalayer.Context;

namespace ParseService.Commands
{
    public class DeleteParsesByUserIdCommand
    {
        public class Command : ICommand
        {
            public Guid UserId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(cmd => cmd.UserId).NotEmpty();
            }
        }

        public class Handler : ICommandHandler<Command>
        {
            private readonly IRepository<ParseAggregate> _repository;
            private readonly DatabaseContext _db;

            public Handler(IRepository<ParseAggregate> repository, DatabaseContext db)
            {
                _repository = repository;
                _db = db;
            }

            //public async Task<Unit> Handle(Command command, CancellationToken cancellationToken)
            //{
            //    var ParseIds = await GetParseIds(command.UserId);

            //    var Parses = await _repository.GetAll< >(ParseIds);


            //    foreach (var Parse in Parses)
            //    {
            //        Parse.DeleteParse(command.UserId);
            //    }

            //    await _repository.Delete(Parses);


            //    return Unit.Value;
            //}

            //private async Task<ICollection<Guid>> GetParseIds(Guid parseId)
            //{
            //    var query = from Parse in _db.Find
            //                where Parse.ParseId == parseId
            //                select Parse.ParseId;

            //    var result = await query.ToListAsync();

            //    return result;
            //}

            Task IRequestHandler<Command>.Handle(Command request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
