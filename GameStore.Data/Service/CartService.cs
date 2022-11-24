using AutoMapper;
using GameStore.Data.Context;
using GameStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

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
            if (games == null)
            {
                return null;
            }

            IEnumerable<GameDto> result = games.Select(x => _mapper.Map<GameDto>(x.Game));

            return result;
        }

        public async Task<int> AddGameToCart(string userId, int gameId)
        {
            Cart cart = new Cart()
            {
                UserId = userId,
                GameId = gameId
            };

            _dataContext.Carts.Add(cart);
            return await _dataContext.SaveChangesAsync();
        }

        public Task<int> DeleteCart(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteSingleGame(string userId, int gameId)
        {
            throw new NotImplementedException();
        }

    }
}
