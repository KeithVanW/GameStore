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

        private static GameEntity GetMockEntity()
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

        private static IMapper MockMapper()
        {
            var myProfile = new GameProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);
            return mapper;
        }
    }
}