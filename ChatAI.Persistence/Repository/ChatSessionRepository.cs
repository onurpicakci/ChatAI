using ChatAI.Domain.Entity.Chat;
using ChatAI.EFCore.DbContext;
using ChatAI.Persistence.Abstract;

namespace ChatAI.Persistence.Repository.Chat;

public class ChatSessionRepository : GenericRepository<ChatSession>, IChatSessionRepository
{
    public ChatSessionRepository(ChatAIDbContext dbContext) : base(dbContext)
    {
    }
}