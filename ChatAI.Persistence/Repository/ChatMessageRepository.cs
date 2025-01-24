using ChatAI.Domain.Entity.Chat;
using ChatAI.EFCore.DbContext;
using ChatAI.Persistence.Abstract;

namespace ChatAI.Persistence.Repository.Chat;

public class ChatMessageRepository : GenericRepository<ChatMessage>, IChatMessageRepository
{
    public ChatMessageRepository(ChatAIDbContext dbContext) : base(dbContext)
    {
    }
}