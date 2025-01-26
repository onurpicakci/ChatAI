using AutoMapper;
using ChatAI.Application.Dto.Chats;
using ChatAI.Application.Interface;
using ChatAI.Domain.Entity.Chat;
using ChatAI.Helper.ChatGpt;
using ChatAI.Persistence.Abstract;

namespace ChatAI.Application.Service;

public class ChatMessageService : BaseMapperService, IChatMessageService
{
    private readonly ChatGPTHelper _chatGptHelper;
    private readonly IChatMessageRepository _chatMessageRepository;

    public ChatMessageService(IChatMessageRepository chatAIRepository, ChatGPTHelper chatGptHelper, IMapper mapper) : base(mapper)
    {
        _chatMessageRepository = chatAIRepository;
        _chatGptHelper = chatGptHelper;
    }
    public async Task<IEnumerable<ChatMessageDto>> GetMessagesBySessionIdAsync(Guid sessionId)
    {
       var message = await _chatMessageRepository.GetAllAsync(x => x.ChatSessionId == sessionId);

       return MapList<ChatMessage, ChatMessageDto>(message);
    }

    public async Task<ChatMessageDto> SendMessageAsync(Guid sessionId, string userMessage)
    {
        var userChatMessage = new ChatMessageDto
        {
            ChatSessionId = sessionId,
            MessageContent = userMessage,
            Timestamp = DateTime.UtcNow,
            IsBotMessage = false
        };

        await _chatMessageRepository.AddAsync(Map<ChatMessageDto, ChatMessage>(userChatMessage));

        var response = await _chatGptHelper.GenerateResponseAsync(userMessage);

        if (string.IsNullOrEmpty(response))
        {
            throw new InvalidOperationException("No chatbot response received");
        }

        var botChatMessage = new ChatMessageDto
        {
            ChatSessionId = sessionId,
            MessageContent = response, 
            Timestamp = DateTime.UtcNow,
            IsBotMessage = true
        };

        await _chatMessageRepository.AddAsync(Map<ChatMessageDto, ChatMessage>(botChatMessage));

        return botChatMessage;
    }

}