using GameStore.Data.Entities;

namespace GameStore.Data.Repositories;

public interface IGameRepo
{
    Task<GameEntity> GetSingleGame(int id);
    Task<IEnumerable<GameEntity>> GetAllGames();
    Task<int> AddGame(GameEntity game);
    Task<int> UpdateGame(int id, GameEntity request);
    Task<int> DeleteGame(int id);
    Task<IEnumerable<GameEntity>> Search(string searchString);
    Task<IEnumerable<GameEntity>> SearchGenre(string searchString);
    Task<Boolean> DoesGameExist(int id);
}