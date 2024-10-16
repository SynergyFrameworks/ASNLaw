using ParseService.Events;
using Infrastructure.Common.Commands;
using Infrastructure.Common.Events;
using ParseService.Commands;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Security.Events;

namespace ParseService.EventHandlers
{
    public class UserDeletedEventHandler : IEventHandler<UserChangedEvent, CancellationToken>
    {
        private readonly ICommandBus _commandBus;

        public UserDeletedEventHandler(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        public async Task Handle(UserChangedEvent @event)
        {
            var command = new DeleteParsesByUserIdCommand.Command
            {
                UserId = @event.Id
            };

            await _commandBus.Send(command);
        }
    }
}
