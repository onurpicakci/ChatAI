using ChatAI.Application.Dto.Chats;
using ChatAI.Domain.Entity.Chat;

namespace ChatAI.Application.Interface;

public interface IChatMessageService
{
    Task<IEnumerable<ChatMessageDto>> GetMessagesBySessionIdAsync(Guid sessionId);
    Task<ChatMessageDto> SendMessageAsync(Guid sessionId, string userMessage);
}