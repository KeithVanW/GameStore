using GameStore.Data.Context;
using GameStore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data.Repositories
{
    public class CartRepo : ICartRepo
    {
        private readonly DataContext _dataContext;

        public CartRepo(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IEnumerable<CartEntity>> GetGamesByUserIdAsync(string userId)
        {
            IEnumerable<CartEntity> games = await _dataContext.Carts
                .Include(cart => cart.Game)
                .Where(x => x.UserId.Contains(userId))
                .ToListAsync();

            return games;
        }

        public async Task<int> AddGameToCart(CartEntity request)
        {
            _dataContext.Carts.Add(request);
            return await _dataContext.SaveChangesAsync();
        }

        public async Task<int> DeleteCart(string userId)
        {
            _dataContext.Carts.RemoveRange(_dataContext.Carts.Where(x => x.UserId == userId));
            return await _dataContext.SaveChangesAsync();
        }

        public async Task<int> DeleteSingleGame(CartEntity request)
        {
            _dataContext.Carts.Remove(request);
            return await _dataContext.SaveChangesAsync();
        }

        public async Task<Boolean> IsGameInCart(string userId ,int id)
        {
            return await _dataContext.Carts.AnyAsync(x => x.GameId == id & x.UserId == userId);
        }
    }
}
