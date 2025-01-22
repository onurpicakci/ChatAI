namespace ChatAI.Domain.Entity;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public IEnumerable<ChatSession>? ChatSessions { get; set; }
    
    public User(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or empty", nameof(name));
        }
        
        Name = name;
        
        if (!IsValidEmail(email))
        {
            throw new ArgumentException("Invalid email", nameof(email));
        }
        
        Email = email;
        CreatedAt = DateTime.UtcNow.Date;
    }
    
    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}