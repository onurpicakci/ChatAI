using ChatAI.Application.Interface;
using OpenAI.API;

namespace ChatAI.Application.Service;

public class ChatMessageService : IChatMessageService
{
    private readonly OpenAIAPI _openAI;
    private readonly IChatMessageService _chatMessageService;

    public ChatMessageService(OpenAIAPI openAı, IChatMessageService chatAıService)
    {
        _openAI = openAı;
        _chatMessageService = chatAıService;
    }

    public Task<string> GetChatGptResponseAsync(string userMessage, Guid chatSessionId, Guid userId)
    {
        throw new NotImplementedException();
    }
}