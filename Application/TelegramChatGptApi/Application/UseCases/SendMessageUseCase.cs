using TelegramChatGptApi.Application.Interfaces;

namespace TelegramBotApi.Application.UseCases
{
    public class SendMessageUseCase
    {
        private readonly ITelegramService _telegramService;

        public SendMessageUseCase(ITelegramService telegramService)
        {
            _telegramService = telegramService;
        }

        public async Task ExecuteAsync(string chatId, string message, string? mediaUrl = null, string? mediaType = null)
        {
            if (!string.IsNullOrEmpty(mediaUrl) && !string.IsNullOrEmpty(mediaType))
            {
                await _telegramService.SendVideoAsync(chatId, mediaUrl, mediaType);
            }
            else
            {
                await _telegramService.SendMessageAsync(chatId, message);
            }
        }
    }
}
