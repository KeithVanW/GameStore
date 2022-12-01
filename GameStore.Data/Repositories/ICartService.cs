namespace GameStore.Data.Service
{
    public interface ICartService
    {
        Task<IEnumerable<GameDto>> GetGamesByUserIdAsync(string userId);

        Task<int> AddGameToCart(string userId, int gameId);

        Task<int> DeleteSingleGame(string userId, int gameId);

        Task<int> DeleteCart(string userId);
     }
}
