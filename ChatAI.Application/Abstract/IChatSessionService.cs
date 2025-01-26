using ChatAI.Application.Dto.Chats;

namespace ChatAI.Application.Interface;

public interface IChatSessionService
{
    Task<ChatSessionDto> CreateSessionAsync(Guid userId);
}