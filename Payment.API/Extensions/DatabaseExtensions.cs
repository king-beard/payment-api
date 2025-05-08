using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Payment.API.Database;

namespace Payment.API.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddSQLDatabaseConfiguration(this IServiceCollection services,
        IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("Database");
            services.AddDbContext<ApplicationDbContext>( options => 
                options.UseNpgsql(connectionString)
                       .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning)) 
            );

            return services;
        }
    }
}
