using ChatAI.Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatAI.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatMessageController : ControllerBase
    {
        private readonly IChatMessageService _chatMessageService;

        public ChatMessageController(IChatMessageService chatMessageService)
        {
            _chatMessageService = chatMessageService;
        }
        
        [HttpGet("{sessionId}")]
        public async Task<IActionResult> GetMessagesBySessionIdAsync(Guid sessionId)
        {
            var messages = await _chatMessageService.GetMessagesBySessionIdAsync(sessionId);
            if(messages == null)
            {
                return NotFound();
            }
            
            return Ok(messages);
        }
        
        [HttpPost("{sessionId}")]
        public async Task<IActionResult> SendMessageAsync(Guid sessionId, string userMessage)
        {
            var message = await _chatMessageService.SendMessageAsync(sessionId, userMessage);
            if(message == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
            return Ok(message);
        }
    }
}
