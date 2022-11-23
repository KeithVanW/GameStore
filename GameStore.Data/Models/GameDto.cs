namespace GameStore.Data.Service
{
    public class GameDto
    {
        public int GameID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Developer { get; set; } = string.Empty;
        public string Platform { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public double Price { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        public string ImageURL { get; set; } = string.Empty;
    }
}
