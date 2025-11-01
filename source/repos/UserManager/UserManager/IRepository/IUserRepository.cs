using System.Linq.Expressions;
using UserManager.Entity;

namespace UserManager.IRepository
{
    public interface IUserRepository
        // khai báo hàm, khi làm thì sẽgoij cái này ra
    {
        Task<List<Users>?> GetAllAsync(Expression<Func<Users,bool>> filter = null);
        Task<Users?> GetAsync(Expression<Func<Users,bool>> filter = null,bool tracked=true);
        Task<Users?> CreateAsync(Users entity);
        Task DeleteAsync(Users entity);

        Task SaveAsync();
    }
}
