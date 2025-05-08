using Microsoft.EntityFrameworkCore;
using Payment.API.Entities;

namespace Payment.API.Database
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Payments> Payment { get; set; }
        public DbSet<Status> Status { get; set; }
    }
}
