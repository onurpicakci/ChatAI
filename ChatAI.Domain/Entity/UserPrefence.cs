namespace ChatAI.Domain.Entity;

public class UserPrefence
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public string Language { get; set; }
    public string Theme { get; set; }
    
    public UserPrefence(string language, string theme)
    {
        if (string.IsNullOrWhiteSpace(language))
        {
            throw new ArgumentException("Language cannot be null or empty", nameof(language));
        }
        
        if (string.IsNullOrWhiteSpace(theme))
        {
            throw new ArgumentException("Theme cannot be null or empty", nameof(theme));
        }
        
        Language = language;
        Theme = theme;
    }
}