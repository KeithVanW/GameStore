using AutoMapper;
using GameStore.Data.Entities;

namespace GameStore.Data.Profiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<GameEntity, GameDto>();


            CreateMap<GameDto, GameEntity>()
                .ForMember(x => x.GameID, opt => opt.Ignore());
        }
    }
}
