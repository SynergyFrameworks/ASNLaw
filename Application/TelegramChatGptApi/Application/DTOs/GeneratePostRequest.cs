public class GeneratePostRequest
{
    public string? ContentType { get; set; }  // "Message", "Document", "Image", "Video"
    public string? UserPrompt { get; set; }
    public string? ChatId { get; set; }  // Telegram Channel ID
}
