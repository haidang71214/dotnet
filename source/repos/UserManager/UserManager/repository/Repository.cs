using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UserManager.Data;
using UserManager.Entity;
using UserManager.repository.IRepository;

namespace UserManager.repository
{
    public class Repository<T> : IRepository<T> where T : class
    // cái này sẽ tạo ra cái riêng của từng cái entity
    // thực hiện hàm
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        // depencdency injection ở trên
        // chỗ ở dưới mình sẽ xử lí logic repository
        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity); // await vì đây là bất đồng bộ, nó cần truy cập cơ sở dữ liệu trong 1 số trường hợp
            await SaveAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            dbSet.Remove(entity); // vì đây là đồng bộ, nó sẽ đánh dấu entity bị xóa trước khi save
            await SaveAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true) // điều kiện lọc, gọi đúng hơn là 1 CÂY BIỂU THỨC (EF CORE) phần tích cú pháp cây biểu thức để sinh ra câu lệnh truy vấn
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();

        }
        public async Task UpdateAsync(T entity)
        {
            dbSet.Update(entity); // Đánh dấu entity là Modified
            await SaveAsync();    // Lưu thay đổi vào DB
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
