using GameStore.Data.Entities;
using GameStore.Data.Service;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<IEnumerable<Game>> GetAllGames() 
        {
            IEnumerable<Game> games = await _gameService.GetAllGames();

            return games;
        }

        [HttpGet("{id}")]
        public async Task<Game> GetSingleGame(int id)
        {
            Game? result = await _gameService.GetSingleGame(id);
            if (result == null)
                return null;

            return result;
        }

        [HttpPost]
        public async Task<IEnumerable<Game>> AddGame(Game game)
        {
            IEnumerable<Game> result = await _gameService.AddGame(game);

            return result;
        }

        [HttpPut("{id}")]
        public async Task<IEnumerable<Game>> UpdateGame(int id, Game request)
        {
            IEnumerable<Game>? result = await _gameService.UpdateGame(id, request);
            if (result == null)
                return null;

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IEnumerable<Game>> DeleteGame(int id)
        {
            IEnumerable<Game>? result = await _gameService.DeleteGame(id);
            if (result == null)
                return null;

            return result;
        }

    }
}
