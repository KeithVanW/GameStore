using AutoMapper;
using GameStore.Business.Models;
using GameStore.Data.Entities;

namespace GameStore.Business.Profiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<GameEntity, GameModel>();

            CreateMap<GameModel, GameEntity>()
                .ForMember(x => x.GameID, opt => opt.Ignore());
        }
    }
}