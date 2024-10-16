using TelegramChatGptApi.Application.Interfaces;

public class GenerateImageUseCase
{
    private readonly IChatGptService _chatGptService;
    private readonly ITelegramService _telegramService;

    public GenerateImageUseCase(IChatGptService chatGptService, ITelegramService telegramService)
    {
        _chatGptService = chatGptService;
        _telegramService = telegramService;
    }

    public async Task ExecuteAsync(string chatId, string userPrompt)
    {
        // Get GPT Response
        var gptResponse = await _chatGptService.GetChatGptResponseAsync(userPrompt);

        // Generate image from the response
        var imagePath = GenerateImageFromText(gptResponse);

        // Send image to Telegram
        await _telegramService.SendPhotoAsync(chatId, imagePath);
    }

    private string GenerateImageFromText(string text)
    {
        // Logic to create image from text using any library (e.g., SkiaSharp)
        var imageName = $"{Guid.NewGuid()}.png";
        var imagePath = Path.Combine("GeneratedImages", imageName);

        // Code to generate image from text
        // Example: Use SkiaSharp or other graphics library to draw text onto an image

        return imagePath;
    }
}
