using GameStore.Data.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreAPI.Controllers
{
    //[Authorize(Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly ILogger<GameController> _logger;

        public GameController(IGameService gameService, ILogger<GameController> logger)
        {
            _gameService = gameService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetSingleGame(int id)
        {
            GameDto result = await _gameService.GetSingleGame(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetAllGames()
        {
            IEnumerable<GameDto> result = await _gameService.GetAllGames();

            if (result == null) 
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Route("search/{name}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDto>>> Search([FromRoute] string name)
        {
            IEnumerable<GameDto> result = await _gameService.Search(name);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddGame(GameDto game)
        {
            int result = await _gameService.AddGame(game);

            if (result == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGame(int id, GameDto request)
        {
            int result = await _gameService.UpdateGame(id, request);

            if (result == 0)
            {
                return BadRequest();
            }
            if (result == -1)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGame(int id)
        {
            int result = await _gameService.DeleteGame(id);

            if (result == 0)
            {
                return BadRequest();
            }
            if (result == -1)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}