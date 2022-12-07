namespace GameStore.Data.Entities
{
    public class LibraryEntity
    {
        public string UserId { get; set; }
        public UserEntity User { get; set; }
        public int GameId { get; set; }
        public GameEntity Game { get; set; }
    }
}