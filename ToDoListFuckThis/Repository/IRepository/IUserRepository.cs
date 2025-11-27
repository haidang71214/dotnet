// IUserRepository.cs
using System.Linq.Expressions;
using ToDoListFuckThis.Models;
using ToDoListFuckThis.Repository.IRepository;

namespace UserManager.repository.IRepository
{
    public interface IUserRepository : IRepository<Users>
    {
        Task<Users?> GetByEmailAsync(string email);
        Task UpdateAsync(Users user);
    }
}