using System.ComponentModel.DataAnnotations;

namespace GameStore.Data.Entities
{
    public class Library
    {

        public string UserId { get; set; }
        public User User { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
