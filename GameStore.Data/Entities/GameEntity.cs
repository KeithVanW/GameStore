using System.ComponentModel.DataAnnotations;

namespace GameStore.Data.Entities
{
    public class GameEntity
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
        [StringLength(64)]
        public string Platform { get; set; }

        [Required]
        [StringLength(64)]
        public string Genre { get; set; }

        [Required]
        public double Price { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [StringLength(255)]
        public string ImageURL { get; set; }

        public IList<LibraryEntity> UserGames { get; set; } = new List<LibraryEntity>();


        public IList<CartEntity> Cart { get; set; } = new List<CartEntity>();
    }
}
