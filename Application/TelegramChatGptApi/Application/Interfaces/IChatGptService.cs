namespace TelegramChatGptApi.Application.Interfaces
{
    public interface IChatGptService
    {
     Task<string> GetChatGptResponseAsync(string userMessage);
     Task GetChatGptStreamResponseAsync(string userMessage, Action<string> onStreamData, CancellationToken cancellationToken = default);
    
    }
}
