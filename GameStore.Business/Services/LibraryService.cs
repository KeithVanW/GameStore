using AutoMapper;
using GameStore.Business.Models;
using GameStore.Data.Entities;
using GameStore.Data.Repositories;

namespace GameStore.Business.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly ILibraryRepo _libraryRepo;
        private readonly IGameRepo _gameRepo;
        private readonly IMapper _mapper;

        public LibraryService(ILibraryRepo library, IGameRepo gameRepo, IMapper mapper)
        {
            _libraryRepo = library;
            _gameRepo = gameRepo;
            _mapper = mapper;
        }


        public async Task<LibraryOverviewModel> GetGamesByUserIdAsync(string userId)
        {
            IEnumerable<GameEntity> entities = (await _libraryRepo.GetGamesByUserIdAsync(userId)).Select(x => x.Game);
            IEnumerable<GameModel> models = entities.Select(x => _mapper.Map<GameModel>(x));

            LibraryOverviewModel model = new LibraryOverviewModel 
            { 
                UserId = userId,
                Games = models,
                TotalPrice = models.Sum(x => x.Price)
            };

            return model;
        }

        public async Task<int> AddGamesToLibrary(string userId, int[] gameId)
        {
            List<int> gameIds = gameId.ToList();
            IList<LibraryEntity> librariesToAdd = new List<LibraryEntity>();

            foreach (int game in gameIds)
            {
                if (!await _gameRepo.DoesGameExist(game))
                {
                }
                else if (await _libraryRepo.IsGameInLibrary(userId, game))
                {
                }
                else
                {
                    librariesToAdd.Add(new LibraryEntity()
                    {
                        GameId = game,
                        UserId = userId
                    });
                }
            }

            return await _libraryRepo.AddGamesToLibrary(librariesToAdd);
        }

        public async Task<int> DeleteLibrary(string userId)
        {
            return await _libraryRepo.DeleteLibrary(userId);
        }

        public async Task<int> DeleteSingleGame(string userId, int gameId)
        {
            if (!await _libraryRepo.IsGameInLibrary(userId, gameId))
            {
                return -1;
            }

            LibraryEntity cart = new LibraryEntity()
            {
                GameId = gameId,
                UserId = userId
            };

            return await _libraryRepo.DeleteSingleGame(cart);
        }
    }
}
