using GameStore.Business.Models;

namespace GameStore.Business.Services
{
    public interface ILibraryService
    {
        Task<int> AddGamesToLibrary(string userId, int[] gameId);
        Task<int> DeleteLibrary(string userId);
        Task<int> DeleteSingleGame(string userId, int gameId);
        Task<IEnumerable<GameModel>> GetGamesByUserIdAsync(string userId);
    }
}