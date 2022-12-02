using GameStore.Business.Models;
using GameStore.Business.Services;
using GameStore.Data.Entities;
using GameStore.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly UserManager<UserEntity> _userManager;

        public CartController(
            ICartService cartService,
            UserManager<UserEntity> userManager)
        {
            _cartService = cartService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<CartOverviewModel>> GetAllGames()
        {
            string userId = await GetUserIdAsync();

            CartOverviewModel result = await _cartService.GetGamesByUserIdAsync(userId);
            if (result == null)
            {
                return NotFound("No games in cart");
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddGameToCart([FromBody] int gameId)
        {
            string userId = await GetUserIdAsync();
            int result = await _cartService.AddGameToCart(userId, gameId);

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

        [HttpDelete]
        public async Task<ActionResult> DeleteCart()
        {
            string userId = await GetUserIdAsync();
            int result = await _cartService.DeleteCart(userId);

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
        public async Task<ActionResult> DeleteSingleGameFromCart(int id)
        {
            string userId = await GetUserIdAsync();
            int result = await _cartService.DeleteSingleGame(userId, id);

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

        private async Task<string> GetUserIdAsync()
        {
            string userName = User.Identity.Name;
            UserEntity user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return null;
            }
            return user.Id;
        }
    }
}