using ToDoListFuckThis.Data;
using ToDoListFuckThis.Models;
using ToDoListFuckThis.Repository.IRepository;

namespace ToDoListFuckThis.Repository
{
    public class TodoSectionRepository : Repository<TodoSection>, ISectionRepository
    {
        private readonly ApplicationDbContext _context;
        public TodoSectionRepository(ApplicationDbContext db) : base(db)
        {
            _context = db;
        }
    }
}
