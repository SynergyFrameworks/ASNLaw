namespace TelegramBotApi.Domain.ValueObjects
{
    public class ChatAction
    {
        public string Action { get; }

        private ChatAction(string action)
        {
            Action = action;
        }

        public static ChatAction Typing() => new ChatAction("typing");
        public static ChatAction UploadPhoto() => new ChatAction("upload_photo");
    }
}
