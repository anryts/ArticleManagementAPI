namespace ArticleAPI.Data.Repositories.Interfaces;

public interface IBaseRepository<T> where T : class
{
    public  Task<T?> GetByIdAsync(Guid id);
    public Task<IEnumerable<T?>> GetAllAsync();
    Task<IEnumerable<T?>> GetByQueryAsync(Func<IQueryable<T>, IQueryable<T>> query);
    public Task<T?> GetByQueryAsync(Func<IQueryable<T>, Task<T?>> query);
    public Task AddAsync(T entity);
    public Task UpdateAsync(T entity);
    public Task UpdateAsync(IEnumerable<T> entities);
    public Task DeleteAsync(T entity);
    public Task DeleteAsync(IEnumerable<T> entities);
    public Task SaveChangesAsync();
}