using InoveFk.Core.Base;

namespace InoveFk.Core.Repository;

public interface IGenericRepository<T> where T : class 
{
    Task<T> GetByIdAsync(Guid id);
    Task<T> GetByIdDesactiveAsync(Guid id);
    Task<ListResult<T>> GetAsync(uint skip, uint take);
    Task AddAsync(T entity);
    void Delete(T entity);
    void Update(T entity);
}

