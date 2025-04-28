using Microsoft.EntityFrameworkCore;
using CryptoTracker.Domain.Entities;

namespace CryptoTracker.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Coin> Coins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Favorite> Favorites { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Coin>()
               .Property(c => c.PriceUsd)
               .HasPrecision(18, 2); // Hassasiyet ve ölçek belirleniyor (18 basamaktan 2'si ondalıklı)

            // Favorite tablosu için ilişkileri belirleyelim
            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Coin)
                .WithMany(c => c.Favorites)
                .HasForeignKey(f => f.CoinId);
        }

        // OnConfiguring'i yalnızca runtime'da kullanıyorsanız bırakabilirsiniz.
        // Eğer tasarım zamanında kullanacaksanız, IDesignTimeDbContextFactory kullanın.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;Database=CryptoTrackerDb;Trusted_Connection=True;", b => b.MigrationsAssembly("CryptoTracker.Infrastructure"));
            }
        }
    }
}
