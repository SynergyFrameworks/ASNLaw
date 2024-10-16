using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TelegramChatGptApi.Application.UseCases;

[ApiController]
[Route("api/generate-post")]
public class ContentGenerationController : ControllerBase
{
    private readonly GenerateMessageUseCase _generateMessageUseCase;
    private readonly GenerateDocumentUseCase _generateDocumentUseCase;
    private readonly GenerateImageUseCase _generateImageUseCase;
    private readonly GenerateVideoUseCase _generateVideoUseCase;
    private readonly ILogger<ContentGenerationController> _logger;

    public ContentGenerationController(GenerateMessageUseCase generateMessageUseCase,
                                       GenerateDocumentUseCase generateDocumentUseCase,
                                       GenerateImageUseCase generateImageUseCase,
                                       GenerateVideoUseCase generateVideoUseCase,
                                       ILogger<ContentGenerationController> logger)
    {
        _generateMessageUseCase = generateMessageUseCase;
        _generateDocumentUseCase = generateDocumentUseCase;
        _generateImageUseCase = generateImageUseCase;
        _generateVideoUseCase = generateVideoUseCase;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> GenerateAndPostContent([FromBody] GeneratePostRequest request)
    {
        _logger.LogInformation("Received request to generate and post content: {Request}", request);

        try
        {
            if (string.IsNullOrEmpty(request.ChatId))
            {
                return BadRequest(new { Status = "Error", Message = "ChatId cannot be null or empty" });
            }

            if (string.IsNullOrEmpty(request.UserPrompt))
            {
                return BadRequest(new { Status = "Error", Message = "UserPrompt cannot be null or empty" });
            }

            var contentTask = request.ContentType switch
            {
                "Message" => _generateMessageUseCase.ExecuteAsync(request.ChatId, request.UserPrompt),
                "Document" => _generateDocumentUseCase.ExecuteAsync(request.ChatId, request.UserPrompt),
                "Image" => _generateImageUseCase.ExecuteAsync(request.ChatId, request.UserPrompt),
                "Video" => _generateVideoUseCase.ExecuteAsync(request.ChatId, request.UserPrompt),
                _ => throw new ArgumentException("Invalid content type")
            };

            await contentTask;
            _logger.LogInformation("Content posted successfully: {Request}", request);
            return Ok(new { Status = "Content posted successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating and posting content: {Request}", request);
            return BadRequest(new { Status = "Error", Message = ex.Message });
        }
    }

}
