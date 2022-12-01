using GameStore.Data.Context;
using GameStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GameStore.Data.Repositories
{
    public class GameRepo : IGameRepo
    {
        private readonly DataContext _dataContext;

        public GameRepo(DataContext dataContext)
        {
            _dataContext = dataContext;
            LoadSampleData();
        }

        public async Task<GameEntity> GetSingleGame(int id)
        {
            GameEntity game = await _dataContext.Games.FindAsync(id);
            return game;
        }

        public async Task<IEnumerable<GameEntity>> GetAllGames()
        {
            List<GameEntity> games = await _dataContext.Games.ToListAsync();

            if (!games.Any())
            {
                return null;
            }

            return games;
        }

        public async Task<int> AddGame(GameEntity game)
        {
            _dataContext.Games.Add(game);
            return await _dataContext.SaveChangesAsync();
        }

        public async Task<int> UpdateGame(int id, GameEntity request)
        {
            if (!_dataContext.Games.Any(x => x.GameID == id))
            {
                return -1;
            }

            _dataContext.Games.Update(request);

            return await _dataContext.SaveChangesAsync();
        }

        public async Task<int> DeleteGame(int id)
        {
            if (!_dataContext.Games.Any(x => x.GameID == id))
            {
                return -1;
            }

            GameEntity game = new GameEntity { GameID = id };

            _dataContext.Games.Remove(game);
            return await _dataContext.SaveChangesAsync();
        }

        private void LoadSampleData()
        {
            if (!_dataContext.Games.Any())
            {
                string file = File.ReadAllText("sampledatagames.json");
                List<GameEntity>? games = JsonConvert.DeserializeObject<List<GameEntity>>(file);
                _dataContext.AddRange(games);
                _dataContext.SaveChanges();
            }
        }

        public async Task<IEnumerable<GameEntity>> Search(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                List<GameEntity> games = await _dataContext.Games.Where(x => x.Name.Contains(searchString)).ToListAsync();
                return games;
            }

            return await GetAllGames();
        }
    }
}