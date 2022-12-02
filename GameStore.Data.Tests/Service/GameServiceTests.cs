using AutoMapper;
using FluentAssertions;
using GameStore.Business.Models;
using GameStore.Business.Profiles;
using GameStore.Business.Services;
using GameStore.Data.Entities;
using GameStore.Data.Repositories;
using Moq;

namespace GameStore.Business.Tests.Service
{
    public class GameServiceTests
    {
        [Fact]
        public async Task GameService_GetGame_ReturnsGame()
        {
            // Arrange
            var game = GetMockEntity();
            IMapper mapper = MockMapper();
            var repoMock = new Mock<IGameRepo>();

            // Setup mock to do something when a specific method is called
            repoMock.Setup(x => x.GetSingleGame(It.IsAny<int>())).ReturnsAsync(game);

            GameService service = new GameService(repoMock.Object, mapper);

            // Act
            GameModel result = await service.GetSingleGame(1);

            // Assert
            result.Description.Should().Be(game.Description);
            result.Name.Should().Be(game.Name);
        }

        [Fact]
        public async Task GameService_GetAllGames_ReturnsGames()
        {
            // Arrange
            var games = GetMockEntities();
            IMapper mapper = MockMapper();
            var repoMock = new Mock<IGameRepo>();

            repoMock.Setup(x => x.GetAllGames()).ReturnsAsync(games);

            GameService service = new GameService(repoMock.Object, mapper);

            // Act
            IEnumerable<GameModel> result = await service.GetAllGames();

            // Assert
            result.Should().HaveCount(3);

        }

        [Fact]
        public async Task GameService_AddGame_AddsGame()
        {
            // Arrange
            GameModel gameModel = GetMockModel();
            GameEntity gameEntity = GetMockEntity();
            IEnumerable<GameEntity> gameList= GetMockEntities();
            IMapper mapper = MockMapper();
            var repoMock = new Mock<IGameRepo>();

            repoMock.Setup(x => x.AddGame(gameEntity)).ReturnsAsync(1);
            repoMock.Setup(x => x.GetAllGames()).ReturnsAsync(gameList);

            GameService service = new GameService(repoMock.Object, mapper);

            // Act
            var result = await service.AddGame(gameModel);
            IEnumerable<GameModel> result2 = await service.GetAllGames();

            // Assert
            gameList.Should().Contain(gameEntity);
        }

        [Fact]
        public async Task GameService_UpdateGame_UpdatesGame()
        {
            // Arrange
            GameModel gameModel = GetMockModel();
            GameEntity gameEntity = GetMockEntity();
            IMapper mapper = MockMapper();
            var repoMock = new Mock<IGameRepo>();

            repoMock.Setup(x => x.UpdateGame(It.IsAny<int>(),gameEntity)).ReturnsAsync(1);

            GameService service = new GameService(repoMock.Object, mapper);

            // Act
            int result = await service.AddGame(gameModel);

            // Assert
            result.Should().Be(1);
        }

        private GameEntity GetMockEntity()
        {
            var game = new GameEntity
            {
                Name = "Among Us",
                Developer = "InnerSloth",
                Platform = "PC",
                Genre = "Strategy",
                Price = 19.99,
                Description =
                    "Among Us is a party game of teamwork and betrayal. Crewmates work together to complete tasks before one or more Impostors can kill everyone aboard.",
                ImageURL = "test.jpg"
            };
            return game;
        }
        private GameModel GetMockModel()
        {
            GameModel game = new GameModel
            {
                Name = "Among Us",
                Developer = "InnerSloth",
                Platform = "PC",
                Genre = "Strategy",
                Price = 19.99,
                Description = "Among Us is a party game of teamwork and betrayal. Crewmates work together to complete tasks before one or more Impostors can kill everyone aboard.",
                ImageURL = "test.jpg"
            };
            return game;

        }
        private IEnumerable<GameEntity> GetMockEntities() 
        {
            IEnumerable<GameEntity> games = new List<GameEntity>();
            games = games.Append(new GameEntity
            {
                Name = "Among Us",
                Developer = "InnerSloth",
                Platform = "PC",
                Genre = "Strategy",
                Price = 19.99,
                Description = "Among Us is a party game of teamwork and betrayal. Crewmates work together to complete tasks before one or more Impostors can kill everyone aboard.",
                ImageURL = "test.jpg"
            }).ToList();
            games = games.Append(new GameEntity
            {
                Name = "Nioh",
                Developer = "Koei Tecmo",
                Platform = "PC",
                Genre = "RPG",
                Price = 19.99,
                Description = "Ready to die? Experience the newest brutal action game from Team NINJA and Koei Tecmo Games. In the age of samurai, a lone traveler lands on the shores of Japan. He must fight his way through the vicious warriors and supernatural Yokai that infest the land in order to find that which he seeks. ",
                ImageURL = "test.jpg"
            }).ToList();
            games = games.Append(new GameEntity
            {
                Name = "Elden Ring",
                Developer = "From Software",
                Platform = "PC",
                Genre = "RPG",
                Price = 59.99,
                Description = "Rise, Tarnished, and be guided by grace to brandish the power of the Elden Ring and become an Elden Lord in the Lands Between.",
                ImageURL = "test.jpg"
            }).ToList();
            return games;
        }
        private IMapper MockMapper()
        {
            var myProfile = new GameProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);
            return mapper;
        }
    }
}