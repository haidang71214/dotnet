using System.Linq.Expressions;
using ToDoListFuckThis.Models;
using ToDoListFuckThis.Repository.IRepository;
using UserManager.Models;

namespace UserManager.repository.IRepository
{
    public interface IUserRepository : IRepository<Users>
    // khai báo hàm, khi làm thì sẽgoij cái này ra
    {
        Task<Users> GetByEmailAsync(string email);
    }
}
