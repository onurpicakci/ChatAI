using ChatAI.Domain.Entity;

namespace ChatAI.Persistence.Interface;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByNameAsync(string name);
    Task<User> GetByEmailAsync(string email);
}