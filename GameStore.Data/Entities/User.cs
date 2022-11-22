using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Data.Entities
{
    public class User: IdentityUser
    {
        public IList<Library> UserGames { get; set; }
        public IList<Cart> Cart { get; set; }
    }
}
