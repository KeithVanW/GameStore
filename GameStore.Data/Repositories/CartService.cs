using AutoMapper;
using GameStore.Data.Context;
using GameStore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data.Service
{
    public class CartService : ICartService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public CartService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GameDto>> GetGamesByUserIdAsync(string userId)
        {
            IEnumerable<Cart> games = await _dataContext.Carts
                .Include(cart => cart.Game)
                .Where(x => x.UserId.Contains(userId))
                .ToListAsync();
            if (games.Count() == 0)
            {
                return null;
            }

            IEnumerable<GameDto> result = games.Select(x => _mapper.Map<GameDto>(x.Game));

            return result;
        }

        public async Task<int> AddGameToCart(string userId, int gameId)
        {
            if (!_dataContext.Games.Any(x => x.GameID == gameId))
            {
                return -1;
            }
            if (_dataContext.Carts.Any(x => x.GameId == gameId & x.UserId == userId))
            {
                return 0;
            }

            Cart cart = new Cart()
            {
                UserId = userId,
                GameId = gameId
            };

            _dataContext.Carts.Add(cart);
            return await _dataContext.SaveChangesAsync();
        }

        public async Task<int> DeleteCart(string userId)
        {
            if (!_dataContext.Carts.Any(x => x.UserId == userId))
            {
                return -1;
            }

            _dataContext.Carts.RemoveRange(_dataContext.Carts.Where(x => x.UserId == userId));
            return await _dataContext.SaveChangesAsync();
        }

        public async Task<int> DeleteSingleGame(string userId, int gameId)
        {
            if (!_dataContext.Carts.Any(x => x.UserId == userId & x.GameId == gameId))
            {
                return -1;
            }
            Cart cart = new Cart()
            {
                GameId = gameId,
                UserId = userId
            };
            _dataContext.Carts.Remove(cart);
            return await _dataContext.SaveChangesAsync();
        }

    }
}
