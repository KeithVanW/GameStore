using GameStore.Data.Entities;
using System.Reflection;

namespace GameStore.Data.Service
{
    public interface IGameService
    {
        Task<IEnumerable<GameDto>> GetAllGames();

        Task<GameDto> GetSingleGame(int id);

        Task<int> AddGame(GameDto game);

        Task<int> UpdateGame(int id, GameDto request);

        Task<int> DeleteGame(int id);
        Task<IEnumerable<GameDto>> Search(string name);
    }
}
