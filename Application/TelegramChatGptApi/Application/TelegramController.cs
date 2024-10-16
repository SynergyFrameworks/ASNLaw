using Microsoft.AspNetCore.Mvc;
using Telegram.BotAPI.GettingUpdates;

namespace TelegramChatGptApi.Application
{
    public class TelegramController : Controller
    {
        [HttpPost]
        public IActionResult Post(
        // The secret token is optional, but it's highly recommended to use it.
        [FromHeader(Name = "X-Telegram-Bot-Api-Secret-Token")] string secretToken,
        [FromBody] Update update)
        {
            if (update is null)
            {
                return BadRequest();
            }
            // Check if the secret token is valid
            // Process your update
            return Ok();
        }
    }
}
