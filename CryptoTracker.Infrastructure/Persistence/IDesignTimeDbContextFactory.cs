using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CryptoTracker.Infrastructure.Persistence
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Bağlantı dizesini buraya ekleyin
            var connectionString = "Server=(LocalDB)\\MSSQLLocalDB;Database=CryptoTrackerDb;Trusted_Connection=True;";

            optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("CryptoTracker.Infrastructure"));

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
