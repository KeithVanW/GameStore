using Microsoft.AspNetCore.Identity;

namespace GameStore.Data.Entities
{
    public class UserEntity : IdentityUser
    {
        public IList<LibraryEntity> UserGames { get; set; }
        public IList<CartEntity> Cart { get; set; }
    }
}