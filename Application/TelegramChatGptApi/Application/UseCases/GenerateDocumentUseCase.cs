using TelegramChatGptApi.Application.Interfaces;

public class GenerateDocumentUseCase
{
    private readonly IChatGptService _chatGptService;
    private readonly ITelegramService _telegramService;

    public GenerateDocumentUseCase(IChatGptService chatGptService, ITelegramService telegramService)
    {
        _chatGptService = chatGptService;
        _telegramService = telegramService;
    }

    public async Task ExecuteAsync(string chatId, string userPrompt)
    {
        // Get GPT Response
        var gptResponse = await _chatGptService.GetChatGptResponseAsync(userPrompt);

        // Determine file extension based on prompt or predefined logic (default to .txt)
        var fileExtension = ".txt"; // You can update this logic if different formats are needed

        // Generate a safe filename from userPrompt (sanitize input)
        var fileName = GenerateSafeFileName(userPrompt) + fileExtension;

        // Create and stream document
        using (var stream = new MemoryStream())
        {
            var writer = new StreamWriter(stream);
            await writer.WriteAsync(gptResponse);
            await writer.FlushAsync();
            stream.Position = 0; // Reset the position to the beginning of the stream

            // Send document stream to Telegram
            await _telegramService.SendDocumentStreamAsync(chatId, stream, fileName);
        }
    }

    private string CreateDocumentFromText(string content)
    {
        // Save response as a text file
        var fileName = $"{Guid.NewGuid()}.txt";
        var filePath = Path.Combine("GeneratedDocs", fileName);
        File.WriteAllText(filePath, content);
        return filePath;
    }


    private string GenerateSafeFileName(string userPrompt)
    {
        // Remove invalid characters and trim to a reasonable length
        var invalidChars = Path.GetInvalidFileNameChars();
        var safeFileName = new string(userPrompt.Where(ch => !invalidChars.Contains(ch)).ToArray());

        // Truncate the filename to prevent it from being too long (e.g., 50 characters max)
        return safeFileName.Length > 50 ? safeFileName.Substring(0, 50) : safeFileName;
    }
}
