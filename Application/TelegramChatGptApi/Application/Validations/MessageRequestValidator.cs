using FluentValidation;
using TelegramBotApi.Application.DTOs;

namespace TelegramChatGptApi.Application.Validations
{
    public class MessageRequestValidator : AbstractValidator<TelegramMessage>
    {

        public MessageRequestValidator() 
            {

            RuleFor(x => x.ChatId).NotEmpty().WithMessage("ChatId Must Be Specified");

            RuleFor(x => x.Text).NotEmpty().MinimumLength(10).WithMessage("You must Compose a Question that is greater than 10 Characters");
        
        
        }
    }
}
