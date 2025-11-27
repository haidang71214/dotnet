using System.Linq.Expressions;

namespace ToDoListFuckThis.Repository.IRepository
{
    // cái đéo gì kế thừa
    public interface IRepository<T> where T:class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T,bool>>? filter = null, string? includeProperties = null);
        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null,
            bool tracked = true,
               string? includeProperties = null);

        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveAsync(); // này để lưu
    }
}
