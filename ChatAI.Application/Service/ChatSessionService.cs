using AutoMapper;
using ChatAI.Application.Dto.Chats;
using ChatAI.Application.Interface;
using ChatAI.Domain.Entity.Chat;
using ChatAI.Persistence.Abstract;

namespace ChatAI.Application.Service;

public class ChatSessionService : BaseMapperService, IChatSessionService
{
    private readonly IChatSessionRepository _chatSessionRepository;

    public ChatSessionService(IChatSessionRepository chatSessionRepository, IMapper mapper) : base(mapper)
    {
        _chatSessionRepository = chatSessionRepository;
    }


    public async Task<ChatSessionDto> CreateSessionAsync(Guid userId)
    {
        var chatSession = new ChatSessionDto
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            SessionId = Guid.NewGuid().ToString(),
            StartTimestamp = DateTime.UtcNow,
            IsActive = true
        };

        await _chatSessionRepository.AddAsync(Map<ChatSessionDto, ChatSession>(chatSession));

        return chatSession;
    }
}