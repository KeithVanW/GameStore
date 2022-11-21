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
            SeedGames(modelBuilder);
            SeedUsers(modelBuilder);
        }
        DbSet<User> Users { get; set; }
        DbSet<Game> Games { get; set; }
        DbSet<Cart> Carts { get; set; }
        DbSet<Library> Libraries { get; set; }

        private void SeedGames(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().HasData(
                new Game {GameID = 1,Name = "Among us", Developer = "InnerSloth", Genre = GameGenre.Strategy, Platform = Enum.GamePlatform.PC, Price = 19.99, Description = "1 player is the imposter, the others do tasks.", ImageURL = "test"},
                new Game {GameID = 2, Name = "Apex Legends", Developer = "Respawn Entertainment", Genre = GameGenre.FPS, Platform = Enum.GamePlatform.PC, Price = 0, Description = "Battle royal shooter thing", ImageURL = "test" },
                new Game {GameID = 3, Name = "Battlefield 2042", Developer = "Dice", Genre = GameGenre.FPS, Platform = Enum.GamePlatform.PC, Price = 59.99, Description = "Online shooter", ImageURL = "test" });
        }

        private void SeedUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User {UserID = 1 ,Name = "Keith", Password = "1234", Email = "Keith@email.com"},
                new User {UserID = 2, Name = "Emlyn", Password = "4321", Email = "Emlyn@email.com" });
        }
    }
}
