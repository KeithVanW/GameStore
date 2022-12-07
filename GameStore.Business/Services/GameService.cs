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
            GameEntity entity = await _repo.GetSingleGame(id);
            GameModel model = _mapper.Map<GameModel>(entity);

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

            return await _repo.AddGame(entity);
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