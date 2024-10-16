namespace TelegramBotApi.Application.DTOs
{
    public class TelegramMessage
    {
        public string ChatId { get; set; }
        public string Text { get; set; }
        public string? MediaUrl { get; set; }
        public string? MediaType { get; set; }
    }
}
