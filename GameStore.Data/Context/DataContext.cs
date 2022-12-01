using GameStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GameStore.Data.Context
{
    public class DataContext : IdentityDbContext<UserEntity>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LibraryEntity>().HasKey(x => new { x.GameId, x.UserId });
            modelBuilder.Entity<CartEntity>().HasKey(x => new { x.GameId, x.UserId });
        }

        public DbSet<GameEntity> Games { get; set; }
        public DbSet<CartEntity> Carts { get; set; }
        public DbSet<LibraryEntity> Libraries { get; set; }
    }
}
