using ChatAI.Domain.Entity;
namespace ChatAI.EFCore.DbContext;
using Microsoft.EntityFrameworkCore;

public class ChatAIDbContext : DbContext
{
    public ChatAIDbContext(DbContextOptions<ChatAIDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<ChatMessage> Chats { get; set; }
    public DbSet<ChatSession> ChatSessions { get; set; }
    public DbSet<UserPrefence> UserPrefences { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<ChatMessage>().ToTable("Chats");
        modelBuilder.Entity<ChatSession>().ToTable("ChatSessions");
        modelBuilder.Entity<UserPrefence>().ToTable("UserPrefences");
        
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
        
        modelBuilder.Entity<ChatSession>()
            .HasOne(cs => cs.User)
            .WithMany(u => u.ChatSessions)
            .HasForeignKey(cs => cs.UserId);
        
    }
}