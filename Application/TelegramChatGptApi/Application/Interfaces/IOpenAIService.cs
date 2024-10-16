namespace TelegramChatGptApi.Application.Interfaces
{
    public interface IOpenAIService
    {
        Task<string> GenerateCompletionAsync(string prompt);
        Task<IEnumerable<string>> GetHistoryAsync();
        Task<string> ProcessFileAsync(IFormFile file);
        IAsyncEnumerable<string> StreamResponseAsync(string prompt);
    }

}
