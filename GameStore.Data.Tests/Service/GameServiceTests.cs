using AutoMapper;
using FluentAssertions;
using GameStore.Data.Context;
using GameStore.Data.Entities;
using GameStore.Data.Profiles;
using GameStore.Data.Service;
using Moq;

namespace GameStore.Data.Tests.Service
{
    public class GameServiceTests
    {
        [Fact]
        public async Task GameService_GetGame_ReturnsGame()
        {
            // Arrange
            GameDto game = new GameDto
            {
                Name = "Among Us",
                Developer = "InnerSloth",
                Platform = "PC",
                Genre = "Strategy",
                Price = 19.99,
                Description = "Among Us is a party game of teamwork and betrayal. Crewmates work together to complete tasks before one or more Impostors can kill everyone aboard.",
                ImageURL = "test.jpg"
            };

            Mock<DataContext> context = new Mock<DataContext>();

            var myProfile = new GameProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            GameService service = new GameService(context.Object, mapper);

            // Act
            GameDto result = await service.GetSingleGame(1);

            // Assert
            result.Description.Should().Be(game.Description);
            result.Name.Should().Be(game.Name);
        }
    }
}