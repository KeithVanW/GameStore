using GameStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System;

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
            SeedGames(modelBuilder);
            SeedUsers(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Library> Libraries { get; set; }
    }
}
