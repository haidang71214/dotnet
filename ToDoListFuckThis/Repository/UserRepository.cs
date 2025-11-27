// UserManager.repository/UserRepository.cs
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ToDoListFuckThis.Data;
using ToDoListFuckThis.Models;
using ToDoListFuckThis.Repository;
using ToDoListFuckThis.Repository.IRepository;

using UserManager.Models;
using UserManager.repository.IRepository;

namespace UserManager.repository
{
    public class UserRepository : Repository<Users>, IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Users> GetByEmailAsync(string email)
        {
            return await _db.user
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}