// UserManager.repository/UserRepository.cs
using Microsoft.EntityFrameworkCore;
using UserManager.Data;
using UserManager.Entity;
using UserManager.repository.IRepository;
using System.Linq.Expressions;

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
                .FirstOrDefaultAsync(u => u.Name == email);
        }
    }
}