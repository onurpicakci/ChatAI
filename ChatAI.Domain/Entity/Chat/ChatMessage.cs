namespace ChatAI.Domain.Entity.Chat;

public class ChatMessage
{
    public Guid Id { get; set; }
    public Guid ChatSessionId { get; set; }
    public ChatSession ChatSession { get; set; }
    public string MessageContent { get; set; }
    public DateTime Timestamp { get; set; }
    public bool IsBotMessage { get; set; }
    
    public ChatMessage(Guid chatSessionId, string messageContent, bool isBotMessage)
    {
        if (chatSessionId == Guid.Empty)
        {
            throw new ArgumentException("Chat session id cannot be empty", nameof(chatSessionId));
        }
        
        if (string.IsNullOrWhiteSpace(messageContent))
        {
            throw new ArgumentException("Message content cannot be null or empty", nameof(messageContent));
        }

        ChatSessionId = chatSessionId;
        MessageContent = messageContent;
        IsBotMessage = isBotMessage;
        Timestamp = DateTime.UtcNow;
    }

    public ChatMessage(){}
}