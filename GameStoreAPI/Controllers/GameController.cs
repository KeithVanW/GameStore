using GameStore.Data.Context;
using GameStore.Data.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreAPI.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<GameDto> GetSingleGame(int id)
        {
            GameDto? result = await _gameService.GetSingleGame(id);

            if (result == null)
                return null;

            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<GameDto>> GetAllGames() 
        {
            IEnumerable<GameDto> result = await _gameService.GetAllGames();

            return result;
        }

        [HttpPost]
        public async Task<IEnumerable<GameDto>> AddGame(GameDto game)
        {
            IEnumerable<GameDto> result = await _gameService.AddGame(game);

            return result;
        }

        [HttpPut("{id}")]
        public async Task<IEnumerable<GameDto>> UpdateGame(int id, GameDto request)
        {
            IEnumerable<GameDto>? result = await _gameService.UpdateGame(id, request);
            if (result == null)
                return null;

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IEnumerable<GameDto>> DeleteGame(int id)
        {
            IEnumerable<GameDto>? result = await _gameService.DeleteGame(id);
            if (result == null)
                return null;

            return result;
        }
    }
}