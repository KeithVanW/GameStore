using GameStore.Data.Entities;

namespace GameStore.Data.Repositories;

public interface ICartRepo
{
    Task<IEnumerable<CartEntity>> GetGamesByUserIdAsync(string userId);
    Task<int> AddGameToCart(string userId, int gameId);
    Task<int> DeleteCart(string userId);
    Task<int> DeleteSingleGame(string userId, int gameId);
}