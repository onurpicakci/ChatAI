using ChatAI.EFCore.DbContext;
using ChatAI.Persistence.Interface;
using Microsoft.EntityFrameworkCore;

namespace ChatAI.Persistence.Repository;

public class GenericRepository<T> : IRepository<T> where T : class
{
    protected readonly ChatAIDbContext _dbContext;
    protected readonly DbSet<T> _dbSet;
    
    public GenericRepository(ChatAIDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}
