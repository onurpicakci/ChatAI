using ChatAI.Application.Dto.User;

namespace ChatAI.Application.Dto.Chats;

public class ChatSessionDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserDto User { get; set; }
    public string SessionId { get; set; }
    public DateTime StartTimestamp { get; set; }
    public DateTime EndTimestamp { get; set; }
    public bool IsActive { get; set; }
}