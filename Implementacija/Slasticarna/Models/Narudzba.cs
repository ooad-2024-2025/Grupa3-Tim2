using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Slasticarna.Models
{
    public class Narudzba
    {
        [Key]
        public int NarudzbaID { get; set; }

        public double UkupnaCijena { get; set; }

        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan Vrijeme { get; set; }

        // KorisnikID je string jer Identity koristi string za primarni ključ
        [Required]
        public string KorisnikID { get; set; } = string.Empty;

        [ForeignKey("KorisnikID")]
        public ApplicationUser? Korisnik { get; set; }

        public int StanjeNarudzbe { get; set; }

        [ForeignKey("StanjeNarudzbe")]
        public StanjeNarudzbe? Stanje { get; set; }

        public ICollection<StavkaNarudzbe>? Stavke { get; set; }
    }
}
