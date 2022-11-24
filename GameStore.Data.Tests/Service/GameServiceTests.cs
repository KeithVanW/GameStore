using FluentAssertions;
using GameStore.Data.Entities;
using Newtonsoft.Json;

namespace GameStore.Data.Tests.Service
{
    public class GameServiceTests
    {
        [Fact]
        public void GameService_LoadSampleData_ReturnsData()
        {
            // Arrange
            string file = File.ReadAllText("sampledatagames.json");
            // Act
            List<Game>? games = JsonConvert.DeserializeObject<List<Game>>(file);
            // Assert
            games.Should().HaveCountGreaterThan(0);
        }
    }
}