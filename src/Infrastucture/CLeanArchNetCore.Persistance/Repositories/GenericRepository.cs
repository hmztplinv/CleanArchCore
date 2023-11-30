

using Microsoft.EntityFrameworkCore;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly CleanDatabaseContext _dbContext;

    public GenericRepository(CleanDatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(T entity)
    {
        await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync()
    {
        return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(q => q.Id==id);
    }

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }
}