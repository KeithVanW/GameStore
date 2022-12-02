using AutoMapper;
using GameStore.Business.Models;
using GameStore.Data.Entities;
using GameStore.Data.Repositories;

namespace GameStore.Business.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepo _repo;
        private readonly IMapper _mapper;

        public GameService(IGameRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<GameModel> GetSingleGame(int id)
        {
            // Everything that has to do with calculations (conversions, math, validation, security etc. should be in the business layer)
            GameEntity entity = await _repo.GetSingleGame(id);
            GameModel model = _mapper.Map<GameModel>(entity);

            //model.PriceInDollar = model.Price * .95;

            // Additional calculations can be performed here. 
            // E.g. Convert price to $ and Lira
            // TODO: I want to be able to see Prices in DOllar and EURO's but only Euro can be stored to the DB.

            return model;
        }

        public async Task<IEnumerable<GameModel>> GetAllGames()
        {
            IEnumerable<GameEntity> entities = await _repo.GetAllGames();
            IEnumerable<GameModel> models = entities.Select(x => _mapper.Map<GameModel>(x));

            return models;
        }

        public async Task<int> AddGame(GameModel game)
        {
            GameEntity entity = _mapper.Map<GameEntity>(game);

            // TODO: Validation here
            //ValidateGame(game);
            // If(game == null) no game
            // If(game.price <= 0) no price provided etc.

            return await _repo.AddGame(entity);
        }

        private void ValidateGame(GameModel game)
        {
            // TODO: Validation here
            throw new NotImplementedException();
        }

        public async Task<int> UpdateGame(int id, GameModel game)
        {
            GameEntity entity = _mapper.Map<GameEntity>(game);
            return await _repo.UpdateGame(id, entity);
        }

        public async Task<int> DeleteGame(int id)
        {
            return await _repo.DeleteGame(id);
        }

        public async Task<IEnumerable<GameModel>> Search(string name)
        {
           IEnumerable<GameEntity> entities = await _repo.Search(name);
           IEnumerable<GameModel> models = entities.Select(x => _mapper.Map<GameModel>(x));

           return models;
        }

        public async Task<IEnumerable<GameModel>> SearchGenre(string genre)
        {
            IEnumerable<GameEntity> entities = await _repo.SearchGenre(genre);
            IEnumerable<GameModel> models = entities.Select(x => _mapper.Map<GameModel>(x));

            return models;
        }
    }
}
