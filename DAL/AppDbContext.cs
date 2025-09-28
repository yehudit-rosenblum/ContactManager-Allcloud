using Microsoft.EntityFrameworkCore;

namespace ContactManager.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }
        public DbSet<Contact> Contacts { get; set; } = null!;
    }
}
