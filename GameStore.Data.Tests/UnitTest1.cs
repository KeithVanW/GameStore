using FluentAssertions;
using GameStore.Data.Entities;
using Newtonsoft.Json;
using System.Text.Json;

namespace GameStore.Data.Tests
{
    public class SampleDataTests
    {
        [Fact]
        public void SerializeSampleDataFromJson()
        {
            // Arrange
            string directoryName = Directory.GetCurrentDirectory();

            string file = File.ReadAllText("sampledatagames.json");

            // Act
            List<Game> games = JsonConvert.DeserializeObject<List<Game>>(file);

            // Assert
            games[0].Price.Should().Be(19.99);
        }
    }
}