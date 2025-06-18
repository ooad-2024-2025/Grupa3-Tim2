using System.ComponentModel.DataAnnotations;

namespace Slasticarna.Models
{
    public class Popust
    {
        [Key]
        public int PopustID { get; set; }

        public int VrstaProizvoda { get; set; }

        public double IznosPopusta { get; set; }

        public required string KodZaPopust { get; set; }

        // Jedan popust može imati više proizvoda
        public ICollection<PopustProizvod> PopustProizvodi { get; set; }
    }
}