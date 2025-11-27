using ToDoListFuckThis.Data;
using ToDoListFuckThis.Models;
using ToDoListFuckThis.Repository.IRepository;

namespace ToDoListFuckThis.Repository
{
    public class ProjectRepository : Repository<Projects>,IProjectRepository
    {
        private readonly ApplicationDbContext _context;
        public ProjectRepository(ApplicationDbContext db) : base(db)
        {
            _context = db;
        }

    }
}
