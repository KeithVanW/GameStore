using GameStore.Data.Entities;

namespace GameStore.Data.Service
{
    public interface IGameService
    {
        Task<IEnumerable<Game>> GetAllGames();

        Task<Game?> GetSingleGame(int id);

        Task<IEnumerable<Game>> AddGame(Game game);

        Task<IEnumerable<Game>?> UpdateGame(int id, Game request);

        Task<IEnumerable<Game>?> DeleteGame(int id);
    }

    // List instead of Ienumerable???
}
