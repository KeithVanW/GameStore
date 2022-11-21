using GameStore.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Data.Entities
{
    public class Game
    {
        [Key]
        public int GameID { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [Required]
        [StringLength(64)]
        public string Developer { get; set; }

        [Required]
        public GamePlatform Platform { get; set; }

        [Required]
        public GameGenre Genre { get; set; }

        [Required]
        public double Price { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [StringLength(255)]
        public string ImageURL { get; set; }

        public IList<Library> UserGames { get; set; }


        public IList<Cart> Cart { get; set; }
    }
}
