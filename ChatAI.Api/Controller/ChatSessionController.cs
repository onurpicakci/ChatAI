using ChatAI.Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatAI.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatSessionController : ControllerBase
    {
        private readonly IChatSessionService _chatSessionService;

        public ChatSessionController(IChatSessionService chatSessionService)
        {
            _chatSessionService = chatSessionService;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateSessionAsync(Guid userId)
        {
            var chatSession = await _chatSessionService.CreateSessionAsync(userId);
            return Ok(chatSession);
        }
    }
}
