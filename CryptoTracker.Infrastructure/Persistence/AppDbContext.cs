using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CryptoTracker.Domain.Entities;

namespace CryptoTracker.Infrastructure.Persistence
{
    public class AppDbContext:DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {
        }
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Coin> Coins { get; set; } = null;
        public DbSet<Favorite> Favorites { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}
