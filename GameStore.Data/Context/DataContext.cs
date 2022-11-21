using GameStore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data.Context
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=GameStoreDB;Trusted_Connection=True;TrustServerCertificate=Yes;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Library>().HasKey(x => new { x.GameId, x.UserId });
            modelBuilder.Entity<Cart>().HasKey(x => new { x.GameId, x.UserId });
        }
        DbSet<User> Users { get; set; }
        DbSet<Game> Games { get; set; }
        DbSet<Cart> Carts { get; set; }
        DbSet<Library> Libraries { get; set; }
    }
}
