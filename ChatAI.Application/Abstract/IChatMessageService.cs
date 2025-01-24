namespace ChatAI.Application.Interface;

public interface IChatMessageService
{
    Task<string> GetChatGptResponseAsync(string userMessage, Guid chatSessionId, Guid userId);
}