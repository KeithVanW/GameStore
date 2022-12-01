using GameStore.Data.Context;
using GameStore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data.Repositories
{
    public class LibraryRepo : ILibraryRepo
    {
        private readonly DataContext _dataContext;

        public LibraryRepo(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IEnumerable<LibraryEntity>> GetGamesByUserIdAsync(string userId)
        {
            IEnumerable<LibraryEntity> games = await _dataContext.Libraries
                .Include(library => library.Game)
                .Where(x => x.UserId.Contains(userId))
                .ToListAsync();

            return games;
        }

        public async Task<int> AddGamesToLibrary(IEnumerable<LibraryEntity> entities)
        {
            _dataContext.Libraries.AddRange(entities);
            return await _dataContext.SaveChangesAsync();
        }

        public async Task<int> DeleteLibrary(string userId)
        {
            _dataContext.Libraries.RemoveRange(_dataContext.Libraries.Where(x => x.UserId == userId));
            return await _dataContext.SaveChangesAsync();
        }

        public async Task<int> DeleteSingleGame(LibraryEntity request)
        {
            _dataContext.Libraries.Remove(request);
            return await _dataContext.SaveChangesAsync();
        }

        public async Task<bool> IsGameInLibrary(string userId, int gameId)
        {
            return await _dataContext.Libraries.AnyAsync(x => x.GameId == gameId & x.UserId == userId);
        }
    }
}
