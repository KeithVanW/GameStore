using GameStore.Data.Entities;

namespace GameStore.Data.Repositories;

public interface ILibraryRepo
{
    Task<IEnumerable<LibraryEntity>> GetGamesByUserIdAsync(string userId);

    Task<int> AddGamesToLibrary(IEnumerable<LibraryEntity> entities);

    Task<int> DeleteLibrary(string userId);

    Task<int> DeleteSingleGame(LibraryEntity request);

    Task<bool> IsGameInLibrary(string userId, int gameId);
}