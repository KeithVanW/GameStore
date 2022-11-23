using GameStore.Data.Entities;

namespace GameStore.Data.Service
{
    public interface IGameService
    {
        Task<IEnumerable<GameDto>> GetAllGames();

        Task<GameDto?> GetSingleGame(int id);

        Task<IEnumerable<GameDto>> AddGame(GameDto game);

        Task<IEnumerable<GameDto>?> UpdateGame(int id, GameDto request);

        Task<int> DeleteGame(int id);
    }
}
