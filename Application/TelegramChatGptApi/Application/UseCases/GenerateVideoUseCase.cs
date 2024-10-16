using TelegramChatGptApi.Application.Interfaces;

public class GenerateVideoUseCase
{
    private readonly IChatGptService _chatGptService;
    private readonly ITelegramService _telegramService;

    public GenerateVideoUseCase(IChatGptService chatGptService, ITelegramService telegramService)
    {
        _chatGptService = chatGptService;
        _telegramService = telegramService;
    }

    public async Task ExecuteAsync(string chatId, string userPrompt)
    {
        // Get GPT Response
        var gptResponse = await _chatGptService.GetChatGptResponseAsync(userPrompt);

        // Generate video from response or download an existing video based on text
        var videoPath = GenerateVideoFromText(gptResponse);

        // Send video to Telegram
        await _telegramService.SendVideoAsync(chatId, videoPath, "video");
        //await _telegramService.SendVideoStreamAsync(chatId, videoStream, $"{Guid.NewGuid()}.mp4");
    }

    private string GenerateVideoFromText(string text)
    {
        // Logic to generate video from text
        var videoName = $"{Guid.NewGuid()}.mp4";
        var videoPath = Path.Combine("GeneratedVideos", videoName);

        // Code to generate video from text (e.g., ffmpeg or similar tool)

        return videoPath;
    }
}
