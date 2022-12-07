using GameStore.Business.Models;

namespace GameStore.Business.Services
{
    public interface ICartService
    {
        Task<int> AddGameToCart(string userId, int game);

        Task<int> DeleteCart(string userId);

        Task<int> DeleteSingleGame(string userId, int gameId);

        Task<CartOverviewModel> GetGamesByUserIdAsync(string userId);
    }
}