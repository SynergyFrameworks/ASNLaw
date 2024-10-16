using MediatR;
using Microsoft.AspNetCore.Mvc;
using TelegramChatGptApi.Application.Commands;
using TelegramChatGptApi.Application.Querys;


namespace TelegramChatGptApi.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatChannelController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChatChannelController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateChatChannelCommand command)
    {
        var chatId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = chatId }, chatId);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateChatChannelCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var chat = await _mediator.Send(new GetChatChannelByIdQuery { Id = id });
        return Ok(chat);
    }
}
