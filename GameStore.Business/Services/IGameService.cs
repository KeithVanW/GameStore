using GameStore.Business.Models;

namespace GameStore.Business.Services;

public interface IGameService
{
    Task<GameModel> GetSingleGame(int id);
    Task<IEnumerable<GameModel>> GetAllGames();
    Task<int> AddGame(GameModel game);
    Task<int> UpdateGame(int id, GameModel game);
    Task<int> DeleteGame(int id);
    Task<IEnumerable<GameModel>> Search(string name);
}