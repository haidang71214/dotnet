using System.Linq.Expressions;
using UserManager.Entity;

namespace UserManager.repository.IRepository
{
    // cái này sẽ tạo ra những cái repossittory chung, đại khái là thế
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        Task SaveAsync();
    }
}
