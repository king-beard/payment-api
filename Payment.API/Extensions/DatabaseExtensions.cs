using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Payment.API.Database;

namespace Payment.API.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddSQLDatabaseConfiguration(this IServiceCollection services,
        IConfiguration configuration)
        {
            string connectionString = Environment.GetEnvironmentVariable("ASPNETCORE_CONNECTION_STRING");

            services.AddDbContext<ApplicationDbContext>(
            options => options
                .UseNpgsql(connectionString, npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "public"))
                .UseSnakeCaseNamingConvention());

            return services;
        }
    }
}
