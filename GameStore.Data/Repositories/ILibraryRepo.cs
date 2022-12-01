namespace GameStore.Data.Repositories
{
    public interface ILibraryRepo
    {
        Task<IEnumerable<GameDto>> GetGamesByUserIdAsync(string userId);

        Task<int> AddGamesToLibrary(string userId, int[] gameId);

        Task<int> DeleteSingleGame(string userId, int gameId);

        Task<int> DeleteLibrary(string userId);
    }
}
