﻿using GameStore.Business.Models;
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
    public class LibraryController: ControllerBase
    {
        private readonly ILibraryRepo _libraryService;
        private readonly ILogger<LibraryController> _logger;
        private readonly UserManager<UserEntity> _userManager;

        public LibraryController(
            ILibraryRepo libraryService,
            ILogger<LibraryController> logger,
            UserManager<UserEntity> userManager)
        {
            _libraryService = libraryService;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameModel>>> GetAllGames()
        {

            string userId = await GetUserIdAsync();

            IEnumerable<GameModel> result = await _libraryService.GetGamesByUserIdAsync(userId);
            if (result == null)
            {
                return NotFound("No games in library");
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddGamesToLibrary([FromBody] int[] gameId)
        {
            string userId = await GetUserIdAsync();
            int result = await _libraryService.AddGamesToLibrary(userId, gameId);
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteLibrary()
        {
            string userId = await GetUserIdAsync();
            int result = await _libraryService.DeleteLibrary(userId);
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
        public async Task<ActionResult> DeleteSingleGameFromLibrary(int id)
        {
            string userId = await GetUserIdAsync();
            int result = await _libraryService.DeleteSingleGame(userId, id);
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
