using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using TelegramChatGptApi.Application.Interfaces;
using TelegramChatGptApi.Application.Validations;

namespace TelegramChatGptApi.Presentation.Controllers
{
    [ApiController]
    [Route("api/bot")]
    public class TelelgramController : ControllerBase
    {
        private readonly ITelegramService _telegramService;
        private readonly IChatGptService _chatGptService;
        private readonly MessageRequestValidator _messageRequestValidator;
        public TelelgramController(ITelegramService telegramService, IChatGptService chatGptService, MessageRequestValidator messageRequestValidator)
        {
            _telegramService = telegramService;
            _chatGptService = chatGptService;
            _messageRequestValidator = messageRequestValidator;
        }

        [HttpPost("send-message")]
        public async Task<IActionResult> SendMessage(string chatId, string message)
        {
           
            
            
            //ValidationResult validationResult = _messageRequestValidator.ValidateAsync(message);

            //if (!validationResult.IsValid)
            //{ 
                
            //    throw new Exception();
            //            }
            

            await _telegramService.SendMessageAsync(chatId, message); 
            return Ok(new { chatId, message });
        }

        [HttpPost("send-picture")]
        public async Task<IActionResult> SendPhoto(string chatId, string photoUrl )
        {
            await _telegramService.SendPhotoAsync(chatId, photoUrl);
            return Ok(new { chatId, photoUrl});
        }


        [HttpGet("get-updates")]
        public async Task<IActionResult> GetUpdates()
        {
            var updates = await _telegramService.GetUpdatesAsync();
            return Ok(updates);
        }
    }
}
