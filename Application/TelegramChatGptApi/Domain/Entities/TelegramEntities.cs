namespace TelegramChatGptBot.Domain.Entities
{
    public class TelegramUpdates
    {
        public TelegramUpdate[] Result { get; set; }
    }

    public class TelegramUpdate
    {
        public TelegramMessage Message { get; set; }
    }

    public class TelegramMessage
    {
        public TelegramChat Chat { get; set; }
        public string Text { get; set; }
    }

    public class TelegramChat
    {
        public string Id { get; set; }
    }
}
