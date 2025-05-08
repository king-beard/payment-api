using Microsoft.EntityFrameworkCore;
using Payment.API.Entities;

namespace Payment.API.Database
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Payments> Payment { get; set; }
        public DbSet<Status> Status { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    List<Status> statusList = [
        //        new Status{ Id = Guid.NewGuid(), Prefix = "PENDING", Description = "Pendiente" },
        //        new Status{ Id = Guid.NewGuid(), Prefix = "APRROVED", Description = "Aprobado" },
        //        new Status{ Id = Guid.NewGuid(), Prefix = "CANCEL", Description = "Cancelado" }
        //    ];
        //    modelBuilder.Entity<Status>().HasData(statusList);

        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.HasDefaultSchema("public");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.HasDefaultSchema("public");
        }
    }
}
