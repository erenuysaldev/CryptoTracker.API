using CryptoTracker.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;



namespace CryptoTracker.Infrastructure.Persistence
{
    public class DataSeeder
    {
        public static async Task SeedDataAsync(IServiceProvider serviceProvider, UserManager<IdentityUser> userManager)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var identityContext = scope.ServiceProvider.GetRequiredService<IdentityAppDbContext>();

            // Veritabanı her zaman temizlensin, geliştirme aşamasında faydalı olabilir.
            if (context.Database.IsSqlServer())
            {
                await context.Database.MigrateAsync();
            }

            // Admin kullanıcısı kontrolü ve eklenmesi
            var defaultUser = await userManager.FindByEmailAsync("admin@crypto.com");

            if (defaultUser == null)
            {
                var adminUser = new IdentityUser
                {
                    UserName = "admin@crypto.com",
                    Email = "admin@crypto.com"
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");

                if (result.Succeeded)
                {
                    // Admin rolünü atama işlemi
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Örnek coinler ekleyelim
            if (!context.Coins.Any())
            {
                context.Coins.AddRange(
                    new Coin { Name = "Bitcoin", Symbol = "BTC", PriceUsd = 45000 },
                    new Coin { Name = "Ethereum", Symbol = "ETH", PriceUsd = 3000 },
                    new Coin { Name = "Ripple", Symbol = "XRP", PriceUsd = 0.95M }
                );

                await context.SaveChangesAsync();
            }

            // Eğer favorites tablosu boşsa örnek veriler ekleyelim
            if (!context.Favorites.Any())
            {
                var user = await userManager.FindByEmailAsync("admin@crypto.com");

                if (user != null)
                {
                    var bitcoin = await context.Coins.FirstOrDefaultAsync(c => c.Symbol == "BTC");
                    var ethereum = await context.Coins.FirstOrDefaultAsync(c => c.Symbol == "ETH");

                    if (bitcoin != null && ethereum != null)
                    {
                        context.Favorites.AddRange(
                            new Favorite { UserId = user.Id, CoinId = bitcoin.Id },
                            new Favorite { UserId = user.Id, CoinId = ethereum.Id }
                        );


                        await context.SaveChangesAsync();
                    }

                }
            }
        }
    }
}
