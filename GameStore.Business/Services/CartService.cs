using AutoMapper;
using GameStore.Business.Models;
using GameStore.Data.Entities;
using GameStore.Data.Repositories;

namespace GameStore.Business.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepo _cartRepo;
        private readonly IGameRepo _gameRepo;
        private readonly IMapper _mapper;

        public CartService(ICartRepo repo, IMapper mapper, IGameRepo gameRepo)
        {
            _cartRepo = repo;
            _mapper = mapper;
            _gameRepo = gameRepo;
        }

        public async Task<CartOverviewModel> GetGamesByUserIdAsync(string userId)
        {
            IEnumerable<GameEntity> entities = (await _cartRepo.GetGamesByUserIdAsync(userId)).Select(x => x.Game);
            IEnumerable<GameModel> models = entities.Select(x => _mapper.Map<GameModel>(x));

            CartOverviewModel model = new CartOverviewModel
            {
                UserId = userId,
                Games = models,
                TotalPrice = Math.Round(models.Sum(x => x.Price), 2)
            };

            return model;
        }

        public async Task<int> AddGameToCart(string userId, int game)
        {
            if (!await _gameRepo.DoesGameExist(game))
            {
                return -1;
            }

            if (await _cartRepo.IsGameInCart(userId, game))
            {
                return 0;
            }

            CartEntity entity = new CartEntity
            {
                UserId = userId,
                GameId = game
            };

            return await _cartRepo.AddGameToCart(entity);
        }

        public async Task<int> DeleteCart(string userId)
        {
            return await _cartRepo.DeleteCart(userId);
        }

        public async Task<int> DeleteSingleGame(string userId, int gameId)
        {
            if (!await _cartRepo.IsGameInCart(userId, gameId))
            {
                return -1;
            }

            CartEntity cart = new CartEntity()
            {
                GameId = gameId,
                UserId = userId
            };

            return await _cartRepo.DeleteSingleGame(cart);
        }
    }
}