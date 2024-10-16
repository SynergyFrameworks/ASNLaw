using MediatR;
using TelegramChatGptApi.Application.DTOs;
using TelegramChatGptApi.Infrastructure.Context;

namespace TelegramChatGptApi.Application.Querys
{
    public class GetChatChannelByIdQuery : IRequest<ChatChannel>
    {
        public int Id { get; set; }
    }

    public class GetChatByIdQueryHandler : IRequestHandler<GetChatChannelByIdQuery, ChatChannel>
    {
        private readonly ApplicationDbContext _context;

        public GetChatByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ChatChannel> Handle(GetChatChannelByIdQuery request, CancellationToken cancellationToken)
        {
            var chat = await _context.ChatChannels.FindAsync(request.Id);
            if (chat == null)
            {
                throw new NotFoundException($"Chat with ID {request.Id} not found.");
            }

            return chat;
        }
    }

}
