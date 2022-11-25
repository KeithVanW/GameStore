using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Data.Service
{
    public interface ILibraryService
    {
        Task<IEnumerable<GameDto>> GetGamesByUserIdAsync(string userId);

        Task<int> AddGamesToLibrary(string userId, int[] gameId);

        Task<int> DeleteSingleGame(string userId, int gameId);

        Task<int> DeleteLibrary(string userId);
    }
}
