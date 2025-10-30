using Microsoft.EntityFrameworkCore;
using UserManager.Entity;

namespace UserManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { 
        }
        public DbSet<Users> user { get; set; }
    }
}
