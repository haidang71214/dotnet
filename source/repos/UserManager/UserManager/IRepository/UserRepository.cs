using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UserManager.Data;
using UserManager.Entity;

namespace UserManager.IRepository
{
    public class UserRepository : IUserRepository
        // cái này sẽ tạo ra cái riêng của từng cái entity
        // thực hiện hàm
    {
        private readonly ApplicationDbContext _db;
        public UserRepository( ApplicationDbContext db ) { 
        _db = db;
        }
        // depencdency injection ở trên
        // chỗ ở dưới mình sẽ xử lí logic repository
        public async Task<Users> CreateAsync(Users entity)
        {
              await _db.user.AddAsync(entity); // await vì đây là bất đồng bộ, nó cần truy cập cơ sở dữ liệu trong 1 số trường hợp
            await SaveAsync();
            return entity;
        }

        public async Task DeleteAsync(Users entity)
        {
             _db.user.Remove(entity); // vì đây là đồng bộ, nó sẽ đánh dấu entity bị xóa trước khi save
            await SaveAsync();
        }

        public async Task<Users> GetAsync(Expression<Func<Users, bool>> filter = null, bool tracked = true) // điều kiện lọc, gọi đúng hơn là 1 CÂY BIỂU THỨC (EF CORE) phần tích cú pháp cây biểu thức để sinh ra câu lệnh truy vấn
        {
            IQueryable<Users> query = _db.user;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Users>> GetAllAsync(Expression<Func<Users,bool>> filter = null)
        {
            IQueryable<Users> query = _db.user;
            if (filter != null) {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
            
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
