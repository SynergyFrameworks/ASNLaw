namespace TelegramChatGptApi.Application.Interfaces
{
    public interface ITelegramService
    {
        Task SendMessageAsync(string chatId, string message);
        Task SendPhotoAsync(string chatId, string photoUrl, string? caption = null);
        Task SendVideoAsync(string chatId, string videoUrl, string? caption = null);
        Task SendDocumentAsync(string chatId, string documentUrl, string? caption = null);
        Task SendDocumentStreamAsync(string chatId, Stream documentStream, string fileName);
        Task<string> DownloadFileAsync(string fileId);
        Task SendAudioAsync(string chatId, string audioUrl, string? caption = null);
        Task<string> GetUpdatesAsync();
        Task DeleteMessageAsync(string chatId, string messageId);
    }
}