using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using TelegramChatGptApi.Application.Interfaces;
using TelegramBotApi.Application.DTOs;

namespace TelegramChatGptApi.Presentation.Controllers
{
    [ApiController]
    [Route("api/telegram-webhook")]
    public class TelegramWebhookController : ControllerBase
    {
        private readonly IChatGptService _chatGptService;
        private readonly ITelegramService _telegramService;
        private readonly IMongoDatabase _database;

        public TelegramWebhookController(
            IChatGptService chatGptService,
            ITelegramService telegramService,
            IMongoClient mongoClient)
        {
            _chatGptService = chatGptService;
            _telegramService = telegramService;
            _database = mongoClient.GetDatabase("TelegramBotDB");
        }

        [HttpPost]
        public async Task<IActionResult> HandleUpdate([FromBody] TelegramMessage update)
        {
            if (string.IsNullOrEmpty(update.Text) && string.IsNullOrEmpty(update.MediaUrl))
            {
                return Ok();
            }

            if (!string.IsNullOrEmpty(update.MediaUrl) && update.MediaType == "document")
            {
                await HandleDocumentUpload(update);
            }
            else if (!string.IsNullOrEmpty(update.Text))
            {
                await HandleQuestion(update);
            }

            return Ok();
        }

        private async Task HandleDocumentUpload(TelegramMessage message)
        {
            // Assuming the MediaUrl is the file ID for documents
            var fileId = message.MediaUrl;
            var filePath = await _telegramService.DownloadFileAsync(fileId);

            // TODO: Implement document parsing logic here
            var parsedContent = "Placeholder for parsed content";

            // Insert into MongoDB
            var collection = _database.GetCollection<BsonDocument>("documents");
            var document = new BsonDocument
            {
                { "ChatId", message.ChatId },
                { "FileName", Path.GetFileName(filePath) },
                { "Content", parsedContent }
            };
            await collection.InsertOneAsync(document);

            await _telegramService.SendMessageAsync(message.ChatId, "Document received and processed.");
        }

        private async Task HandleQuestion(TelegramMessage message)
        {
            var chatGptResponse = await _chatGptService.GetChatGptResponseAsync(message.Text);

            // chatGptResponse is already a string, so we can use it directly
            await _telegramService.SendMessageAsync(message.ChatId, chatGptResponse);
        }
    }
}