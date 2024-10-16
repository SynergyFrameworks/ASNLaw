using Microsoft.AspNetCore.Mvc;
using Telegram.BotAPI.GettingUpdates;
using TelegramChatGptApi.Application.Interfaces;

namespace TelegramWebhookApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelegramWebhookController : ControllerBase
    {
        private readonly IChatGptService _chatGptService;
        private readonly ITelegramService _telegramService;

        public TelegramWebhookController(IChatGptService chatGptService, ITelegramService telegramBotService)
        {
            _chatGptService = chatGptService;
            _telegramService = telegramBotService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            // Check if the update contains a message
            if (update.Message != null)
            {
                var messageText = update.Message.Text;
                var chatId = update.Message.Chat.Id;
                string chatIdString = chatId.ToString();
                // Forward message to ChatGPT
                var chatGptResponse = await _chatGptService.GetChatGptResponseAsync(messageText);

                // Send response back to Telegram chat
                // Send response back to Telegram chat
                await _telegramService.SendMessageAsync(chatIdString, chatGptResponse);
            }

            // Always respond with 200 OK to confirm receipt of update
            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> PostFile([FromBody] Update update)
        {
            // Handle text messages
            if (update.Message != null)
            {
                var chatId = update.Message.Chat.Id;
                string chatIdString = chatId.ToString();
                // Check if the message contains a document
                if (update.Message.Document != null)
                {
                    var fileId = update.Message.Document.FileId;

                    // Download the file from Telegram
                    var filePath = await _telegramService.DownloadFileAsync(fileId);

                    // Optionally, process the file (e.g., OCR, analyze the content) using ChatGPT or another service

                    // Send the file back to the user (or send a response from ChatGPT)
                    await _telegramService.SendDocumentAsync(chatIdString, filePath, update.Message.Document.FileName);
                }
                else if (!string.IsNullOrEmpty(update.Message.Text))
                {
                    // Forward the text to ChatGPT
                    var chatGptResponse = await _chatGptService.GetChatGptResponseAsync(update.Message.Text);

                    // Send response back to Telegram chat
                    await _telegramService.SendDocumentAsync(chatIdString, chatGptResponse);
                }
            }

            return Ok();
        }
    }
}




//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using TelegramChatGptBot.Domain.Entities;

//namespace TelegramWebhookApi.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class TelegramWebhookController : ControllerBase
//    {
//        [HttpPost]
//        public IActionResult Post([FromBody] object update)
//        {
//            // Deserialize the incoming update (received as JSON)
//            var jsonUpdate = JsonConvert.DeserializeObject(update.ToString());

//            // Process the update (For example, log or handle messages)
//            Console.WriteLine(jsonUpdate);

//            // Respond to Telegram (must be 200 OK, else Telegram will retry)
//            return Ok();
//        }

//        //public IActionResult Post([FromBody] object update)
//        //{
//        //    var jsonUpdate = JsonConvert.DeserializeObject<TelegramUpdate>(update.ToString());

//        //    if (jsonUpdate.Message != null)
//        //    {
//        //        var chatId = jsonUpdate.Message.Chat.Id;
//        //        var messageText = jsonUpdate.Message.Text;

//        //        // Process the message here
//        //        Console.WriteLine($"Message received: {messageText}");
//        //    }

//        //    return Ok();
//        //}



//    }
//}
////var telegramService = new TelegramWebhookService();
////await telegramService.SetWebhookAsync("https://your-public-url/api/TelegramWebhook");
