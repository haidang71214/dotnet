using Microsoft.EntityFrameworkCore;
using ToDoListFuckThis.Models;

namespace ToDoListFuckThis.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
// chỗ này để khai báo entity
        public DbSet<Users> user { get; set; }
        public DbSet<Projects> projects { get; set; }
        public DbSet<Todolists> todolists { get; set; }
        public DbSet<TodoSection> todoSections { get; set; }

        public DbSet<ProjectRoles> projectRoles { get; set; }
    }
}
