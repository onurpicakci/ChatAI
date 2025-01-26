namespace ChatAI.Domain.Entity.Chat;

public class ChatSession
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User.User User { get; set; }
    public string SessionId { get; set; }
    public DateTime StartTimestamp { get; set; }
    public DateTime EndTimestamp { get; set; }
    public bool IsActive { get; set; }
    
    public ICollection<ChatMessage> ChatMessages { get; set; }
}