using GameStore.Data.Context;
using GameStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GameStore.Data.Service
{
    public class GameService : IGameService
    {
        private readonly DataContext _dataContext;

        public GameService(DataContext dataContext)
        {
            _dataContext = dataContext;
            LoadSampleData();
        }

        public async Task<Game?> GetSingleGame(int id)
        {
            Game? game = await _dataContext.Games.FindAsync(id);
            if (game == null) 
                return null;

            return game;
        }

        public async Task<IEnumerable<Game>> GetAllGames()
        {
            return await _dataContext.Games.ToListAsync();
        }

        public async Task<IEnumerable<Game>> AddGame(Game game)
        {
            _dataContext.Games.Add(game);
            await _dataContext.SaveChangesAsync();

            return await _dataContext.Games.ToListAsync();
        }

        public async Task<IEnumerable<Game>?> UpdateGame(int id, Game request)
        {
            var game = await _dataContext.Games.FindAsync(id);

            if (game == null)
                return null;

            game.Name = request.Name;
            game.Developer = request.Developer;
            game.Platform = request.Platform;
            game.Genre= request.Genre;
            game.Price = request.Price;
            game.Description = request.Description;
            game.ImageURL= request.ImageURL;
            //game.UserGames = request.UserGames;
            //game.Cart = request.Cart;

            await _dataContext.SaveChangesAsync();

            return await _dataContext.Games.ToListAsync();
        }

        public async Task<IEnumerable<Game>?> DeleteGame(int id)
        {
            Game? game = await _dataContext.Games.FindAsync(id);

            if (game == null)
                return null;

            _dataContext.Games.Remove(game);
            await _dataContext.SaveChangesAsync();

            return await _dataContext.Games.ToListAsync();
        }

        private void LoadSampleData()
        {
            if (_dataContext.Games.Count() == 0)
            {
                string file = System.IO.File.ReadAllText("sampledatagames.json");
                List<Game>? games = JsonConvert.DeserializeObject<List<Game>>(file);
                _dataContext.AddRange(games);
                _dataContext.SaveChanges();
            }
        }
    }
}
