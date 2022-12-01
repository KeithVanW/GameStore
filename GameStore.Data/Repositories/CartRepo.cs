﻿using GameStore.Data.Context;
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
            
            if (!games.Any())
            {
                return null;
            }

            //IEnumerable<GameEntity> result = games.Select(x => _mapper.Map<GameDto>(x.Game));

            return games;
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

            CartEntity cart = new CartEntity()
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
            CartEntity cart = new CartEntity()
            {
                GameId = gameId,
                UserId = userId
            };
            _dataContext.Carts.Remove(cart);
            return await _dataContext.SaveChangesAsync();
        }

    }
}
