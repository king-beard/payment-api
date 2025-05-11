using Microsoft.EntityFrameworkCore;
using Payment.API.Entities;

namespace Payment.API.Database
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Payments> Payment { get; set; }
        public DbSet<Statuss> Status { get; set; }
        public DbSet<Clients> Client { get; set; }
        public DbSet<Shops> Shop { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
        }
    }
}
