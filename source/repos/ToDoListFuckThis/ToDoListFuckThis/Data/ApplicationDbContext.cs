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
    }
}
