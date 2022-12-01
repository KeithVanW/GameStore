using AutoMapper;
using GameStore.Data.Context;
using GameStore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data.Service
{
    public class LibraryService: ILibraryService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public LibraryService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GameDto>> GetGamesByUserIdAsync(string userId)
        {
            IEnumerable<Library> games = await _dataContext.Libraries
                .Include(library => library.Game)
                .Where(x => x.UserId.Contains(userId))
                .ToListAsync();
            if (games.Count() == 0)
            {
                return null;
            }

            IEnumerable<GameDto> result = games.Select(x => _mapper.Map<GameDto>(x.Game));

            return result;
        }

        public async Task<int> AddGamesToLibrary(string userId, int[] gameId)
        {
            List<int> gameIds = gameId.ToList();
            IList<Library> librariesToAdd = new List<Library>();
            foreach (int game in gameIds)
            {
                if (!_dataContext.Games.Any(x => x.GameID == game))
                {
                }
                else if (_dataContext.Libraries.Any(x => x.GameId == game & x.UserId == userId))
                {
                }
                else
                {
                    librariesToAdd.Add(new Library()
                    {
                        GameId = game, UserId = userId
                    });
                }
            }

            _dataContext.Libraries.AddRange(librariesToAdd);
            return await _dataContext.SaveChangesAsync();
        }

        public async Task<int> DeleteLibrary(string userId)
        {
            if (!_dataContext.Libraries.Any(x => x.UserId == userId))
            {
                return -1;
            }

            _dataContext.Libraries.RemoveRange(_dataContext.Libraries.Where(x => x.UserId == userId));
            return await _dataContext.SaveChangesAsync();
        }

        public async Task<int> DeleteSingleGame(string userId, int gameId)
        {
            if (!_dataContext.Libraries.Any(x => x.UserId == userId & x.GameId == gameId))
            {
                return -1;
            }
            Library library = new Library()
            {
                GameId = gameId,
                UserId = userId
            };
            _dataContext.Libraries.Remove(library);
            return await _dataContext.SaveChangesAsync();
        }
    }
}
