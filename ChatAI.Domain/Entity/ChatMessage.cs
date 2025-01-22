namespace ChatAI.Domain.Entity;

public class ChatMessage
{
    public Guid Id { get; set; }
    public Guid ChatSessionId { get; set; }
    public ChatSession ChatSession { get; set; }
    public string MessageContent { get; set; }
    public DateTime Timestamp { get; set; }
    public bool IsBotMessage { get; set; }
    
    public ChatMessage(string messageContent, bool isBotMessage)
    {
        if (string.IsNullOrWhiteSpace(messageContent))
        {
            throw new ArgumentException("Message content cannot be null or empty", nameof(messageContent));
        }
        
        MessageContent = messageContent;
        IsBotMessage = isBotMessage;
    }
}