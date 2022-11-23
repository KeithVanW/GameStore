using GameStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GameStore.Data.Context
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Library>().HasKey(x => new { x.GameId, x.UserId });
            modelBuilder.Entity<Cart>().HasKey(x => new { x.GameId, x.UserId });
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Library> Libraries { get; set; }
    }
}
