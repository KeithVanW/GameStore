using GameStore.Data.Entities;

namespace GameStore.Data.Repositories;

public interface ICartRepo
{
    Task<IEnumerable<CartEntity>> GetGamesByUserIdAsync(string userId);

    Task<int> AddGameToCart(CartEntity request);

    Task<int> DeleteCart(string userId);

    Task<int> DeleteSingleGame(CartEntity request);

    Task<Boolean> IsGameInCart(string userId, int id);
}