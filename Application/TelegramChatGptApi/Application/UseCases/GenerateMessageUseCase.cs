using TelegramChatGptApi.Application.Interfaces;

namespace TelegramChatGptApi.Application.UseCases
{
    public class GenerateMessageUseCase
    {
        private readonly IChatGptService _chatGptService;
        private readonly ITelegramService _telegramService;
        private readonly ILogger<GenerateMessageUseCase> _logger;

        public GenerateMessageUseCase(IChatGptService chatGptService, ITelegramService telegramService, ILogger<GenerateMessageUseCase> logger)
        {
            _chatGptService = chatGptService;
            _telegramService = telegramService;
            _logger = logger;
        }
        public async Task ExecuteAsync(string chatId, string userMessage)
        {
            try
            {
                _logger.LogInformation("Generating response for user message: {UserMessage}", userMessage);
                var gptResponse = await _chatGptService.GetChatGptResponseAsync(userMessage);

                if (string.IsNullOrEmpty(gptResponse))
                {
                    _logger.LogWarning("Received empty response from ChatGPT.");
                    return;
                }

                _logger.LogInformation("Generated response: {GptResponse}", gptResponse);
                await _telegramService.SendMessageAsync(chatId, gptResponse);
                _logger.LogInformation("Message sent to chat {ChatId}", chatId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating and sending message for chat {ChatId}", chatId);
                throw;
            }
        }

    }
}
