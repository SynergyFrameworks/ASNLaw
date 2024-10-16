using MediatR;
using TelegramChatGptApi.Application.DTOs;
using TelegramChatGptApi.Infrastructure.Context;

namespace TelegramChatGptApi.Application.Commands
{
    public class UpdateChatChannelCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string? ChatId { get; set; }
        public string? Name { get; set; }
    }

    public class UpdateChatCommandHandler : IRequestHandler<UpdateChatChannelCommand, Unit>
    {
        private readonly ApplicationDbContext _context;

        public UpdateChatCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateChatChannelCommand request, CancellationToken cancellationToken)
        {
            var chat = await _context.ChatChannels.FindAsync(request.Id);
            if (chat == null)
            {
                throw new NotFoundException($"Chat with ID {request.Id} not found.");
            }

            chat.ChatId = request.ChatId;
            chat.Name = request.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
