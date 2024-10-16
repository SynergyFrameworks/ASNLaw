namespace TelegramBotApi.Domain.Entities
{
    public class TelegramMessage
    {
        public string ChatId { get; set; }
        public string Text { get; set; }
        public string? MediaUrl { get; set; }
        public string? MediaType { get; set; }  // photo, video, etc.

        public TelegramMessage(string chatId, string text, string? mediaUrl = null, string? mediaType = null)
        {
            ChatId = chatId;
            Text = text;
            MediaUrl = mediaUrl;
            MediaType = mediaType;
        }
    }
}
