using Microsoft.EntityFrameworkCore;
using ToDoListFuckThis.Data;
using ToDoListFuckThis.Models;
using ToDoListFuckThis.Repository.IRepository;

namespace ToDoListFuckThis.Repository
{
    public class TodoListRepository : Repository<Todolists>, ITodoRepository
    {
        private readonly ApplicationDbContext _context;
        public TodoListRepository(ApplicationDbContext db) : base(db)
        {
            _context = db;
        }



        public async Task<List<Todolists>> GetTodolistsByUserIdAsync(Guid userId)
        {
            
            return await _context.todolists
                .Where(t => t.User.Id == userId) 
                .ToListAsync();
        }
    }
}
