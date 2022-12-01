using GameStore.Data.Entities;

namespace GameStore.Data.Repositories;

public interface ILibraryRepo
{
    Task<IEnumerable<LibraryEntity>> GetGamesByUserIdAsync(string userId);
    Task<int> AddGamesToLibrary(string userId, int[] gameId);
    Task<int> DeleteLibrary(string userId);
    Task<int> DeleteSingleGame(string userId, int gameId);
}