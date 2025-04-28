using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CryptoTracker.API.Data;

namespace CryptoTracker.API.Data
{
    public class IdentityAppDbContext : IdentityDbContext<ApplicationUser> // IdentityDbContext kullanıyoruz
    {

        public IdentityAppDbContext(DbContextOptions<IdentityAppDbContext> options)
            : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;Database=CryptoTrackerDb;Trusted_Connection=True;", b => b.MigrationsAssembly("CryptoTracker.Infrastructure"));
            }
        }

        // Veritabanındaki diğer tablolarınızı burada ekleyebilirsiniz.
        // Örneğin:
        // public DbSet<Coin> Coins { get; set; }
    }
}
