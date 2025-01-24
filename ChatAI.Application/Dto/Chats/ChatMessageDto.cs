namespace ChatAI.Application.Dto.Chats;

public class ChatMessageDto
{
    public Guid Id { get; set; }
    public Guid ChatSessionId { get; set; }
    public ChatSessionDto ChatSession { get; set; }
    public string MessageContent { get; set; }
    public DateTime Timestamp { get; set; }
    public bool IsBotMessage { get; set; }
}