using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Data.Entities
{
    public class UserEntity: IdentityUser
    {
        public IList<LibraryEntity> UserGames { get; set; }
        public IList<CartEntity> Cart { get; set; }
    }
}
