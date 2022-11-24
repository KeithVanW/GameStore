using GameStore.Data.Entities;
using GameStore.Data.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController: ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ILogger<CartController> _logger;
        private readonly UserManager<User> _userManager;

        public CartController(
            ICartService cartService,
            ILogger<CartController> logger,
            UserManager<User> userManager)
        {
            _cartService = cartService;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetAllGames()
        {
            string userName = User.Identity.Name;
            User user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return NotFound("User not found");
            }

            IEnumerable<GameDto> result = await _cartService.GetGamesByUserIdAsync(user.Id);
            if (result == null)
            {
                return NotFound("No games in cart");
            }

            return Ok(result);
        }
    }
}
