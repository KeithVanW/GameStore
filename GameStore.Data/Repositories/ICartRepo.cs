namespace GameStore.Data.Repositories
{
    public interface ICartRepo
    {
        Task<IEnumerable<GameDto>> GetGamesByUserIdAsync(string userId);

        Task<int> AddGameToCart(string userId, int gameId);

        Task<int> DeleteSingleGame(string userId, int gameId);

        Task<int> DeleteCart(string userId);
     }
}
