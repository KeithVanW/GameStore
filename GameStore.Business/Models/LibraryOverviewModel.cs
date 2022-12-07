namespace GameStore.Business.Models
{
    public class LibraryOverviewModel
    {
        public string UserId { get; set; }
        public IEnumerable<GameModel> Games { get; set; }
        public double TotalPrice { get; set; }
    }
}