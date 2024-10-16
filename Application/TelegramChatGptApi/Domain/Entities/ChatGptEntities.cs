namespace TelegramChatGptApi.Domain.Entities
{
    public class ChatGptResponse
    {
        public ChatGptChoice[] Choices { get; set; }
    }

    public class ChatGptChoice
    {
        public ChatGptMessage Message { get; set; }
    }

    public class ChatGptMessage
    {
        public string Content { get; set; }
    }
}
