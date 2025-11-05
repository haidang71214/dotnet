using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ToDoListFuckThis.Data;
using ToDoListFuckThis.Repository.IRepository;

namespace ToDoListFuckThis.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        //injection
        private readonly ApplicationDbContext _db;
        private readonly DbSet<T> dbSet;
        public Repository(ApplicationDbContext db) {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        // logic
        public async Task<T> CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
            return entity;

        }

        public async Task DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            await SaveAsync();

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
        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true) // điều kiện lọc, gọi đúng hơn là 1 CÂY BIỂU THỨC (EF CORE) phần tích cú pháp cây biểu thức để sinh ra câu lệnh truy vấn
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }
        //logic phải save
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            dbSet.Update(entity); // Đánh dấu entity là Modified
            await SaveAsync();    // Lưu thay đổi vào DB
        }
    }
}
