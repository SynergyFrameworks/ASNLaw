using MediatR;
using TelegramChatGptApi.Application.DTOs;
using TelegramChatGptApi.Infrastructure.Context;

namespace TelegramChatGptApi.Application.Commands
{
    public class CreateChatChannelCommand : IRequest<int>
    {
        public string ChatId { get; set; }
        public string Name { get; set; }
    }

    public class CreateChatCommandHandler : IRequestHandler<CreateChatChannelCommand, int>
    {
        private readonly ApplicationDbContext _context;

        public CreateChatCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateChatChannelCommand request, CancellationToken cancellationToken)
        {
            var chat = new ChatChannel { ChatId = request.ChatId, Name = request.Name };
            _context.ChatChannels.Add(chat);
            await _context.SaveChangesAsync(cancellationToken);
            return chat.Id;
        }
    }

}
