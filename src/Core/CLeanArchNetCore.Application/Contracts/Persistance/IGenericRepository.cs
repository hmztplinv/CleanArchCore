public interface IGenericRepository<T> where T:class
{
    Task<List<T>> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
    Task<T> GetAsync();
    Task<T> GetByIdAsync(int id);
}

