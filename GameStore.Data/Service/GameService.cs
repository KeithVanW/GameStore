using AutoMapper;
using GameStore.Data.Context;
using GameStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GameStore.Data.Service
{
    public class GameService : IGameService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public GameService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            LoadSampleData();
        }

        public async Task<GameDto?> GetSingleGame(int id)
        {
            Game game = await _dataContext.Games.FindAsync(id);

            if (game == null)
                return null;

            GameDto result = _mapper.Map<GameDto>(game);

            return result;
        }

        public async Task<IEnumerable<GameDto>> GetAllGames()
        {
            IEnumerable<Game> games = await _dataContext.Games.ToListAsync();
            IEnumerable<GameDto> result = games.Select(x => _mapper.Map<GameDto>(x));

            return result;
        }

        public async Task<IEnumerable<GameDto>> AddGame(GameDto game)
        {
            Game gameToAdd = _mapper.Map<Game>(game);

            _dataContext.Games.Add(gameToAdd);
            await _dataContext.SaveChangesAsync();

            IEnumerable<GameDto> result = await GetAllGames();

            return result;
        }

        public async Task<IEnumerable<GameDto>?> UpdateGame(int id, GameDto request)
        {
            Game? game = await _dataContext.Games.FindAsync(id);

            if (game == null)
                return null;

            //game = _mapper.Map<Game>(request);

            game.Name = request.Name;
            game.Developer = request.Developer;
            game.Platform = request.Platform;
            game.Genre = request.Genre;
            game.Price = request.Price;
            game.Description = request.Description;
            game.ImageURL = request.ImageURL;

            await _dataContext.SaveChangesAsync();

            return await GetAllGames();
        }

        public async Task<IEnumerable<GameDto>?> DeleteGame(int id)
        {
            Game? game = await _dataContext.Games.FindAsync(id);

            if (game == null)
                return null;

            _dataContext.Games.Remove(game);
            await _dataContext.SaveChangesAsync();

            return await GetAllGames();
        }

        private void LoadSampleData()
        {
            if (!_dataContext.Games.Any())
            {
                string file = File.ReadAllText("sampledatagames.json");
                List<Game>? games = JsonConvert.DeserializeObject<List<Game>>(file);
                _dataContext.AddRange(games);
                _dataContext.SaveChanges();
            }
        }
    }
}
