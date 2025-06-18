using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Slasticarna.Models
{
    public class Placanje
    {
        [Key]
        public int PlacanjeID { get; set; }

        [Required]
        public string KorisnikID { get; set; } = string.Empty;

        [ForeignKey("KorisnikID")]
        public ApplicationUser? Korisnik { get; set; }

        public int? PopustID { get; set; }

        [ForeignKey("PopustID")]
        public Popust? Popust { get; set; }

        public int NarudzbaID { get; set; }

        [ForeignKey("NarudzbaID")]
        public Narudzba? Narudzba { get; set; }

        public int NacinPlacanjaID { get; set; }

        [ForeignKey("NacinPlacanjaID")]
        public NacinPlacanja? NacinPlacanja { get; set; }
    }
}
