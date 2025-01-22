using ChatAI.Domain.Entity;
using ChatAI.EFCore.DbContext;
using ChatAI.Persistence.Interface;
using Microsoft.EntityFrameworkCore;

namespace ChatAI.Persistence.Repository;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ChatAIDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<User> GetByNameAsync(string name)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Name == name);
    }

    public Task<User> GetByEmailAsync(string email)
    {
        return _dbSet.FirstOrDefaultAsync(x => x.Email == email);
    }
}