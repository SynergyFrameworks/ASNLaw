using Microsoft.AspNetCore.Mvc;
using TelegramChatGptApi.Application.Interfaces;

namespace TelegramChatGptApi.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpenAIController : ControllerBase
    {
        private readonly IOpenAIService _openAIService;

        public OpenAIController(IOpenAIService openAIService)
        {
            _openAIService = openAIService;
        }

        // POST request to generate a response based on a text prompt
        [HttpPost("generate")]
        public async Task<IActionResult> GenerateResponse([FromBody] string prompt)
        {
            var response = await _openAIService.GenerateCompletionAsync(prompt);
            return Ok(response);
        }

        // GET request to retrieve conversation history
        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            var history = await _openAIService.GetHistoryAsync();
            return Ok(history);
        }

        // POST request to process an uploaded file
        [HttpPost("file")]
        public async Task<IActionResult> ProcessFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty.");
            }

            var response = await _openAIService.ProcessFileAsync(file);
            return Ok(response);
        }

        // POST request for streaming OpenAI responses
        [HttpPost("stream")]
        public async Task StreamResponse([FromBody] string prompt)
        {
            Response.ContentType = "text/event-stream";
            await foreach (var token in _openAIService.StreamResponseAsync(prompt))
            {
                await Response.WriteAsync($"data: {token}\n\n");
                await Response.Body.FlushAsync(); // Ensure the client receives the data in real-time
            }
        }
    }

}
