using Microsoft.AspNetCore.Mvc;

namespace LawUI.Controllers;

[ApiController]
[Route("api/chatrooms")]
public class ChatRoomsController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetChatRooms()
    {
        var chatRooms = new List<ChatRoom>
    {
        new ChatRoom { Id = "room1", Name = "General", Description = "General chat room" },
        new ChatRoom { Id = "room2", Name = "Tech", Description = "Technology discussions" },
        new ChatRoom { Id = "room3", Name = "Fun", Description = "Casual and fun topics" }
    };

        return Ok(chatRooms);
    }

    public class ChatRoom
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
