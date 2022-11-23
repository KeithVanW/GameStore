using AutoMapper;
using GameStore.Data.Entities;
using GameStore.Data.Service;

namespace GameStore.Data.Profiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<Game, GameDto>().ReverseMap();
        }
    }
}
