using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Slasticarna.Models
{
    public class Ocjena
    {
        [Key]
        public int OcjenaID { get; set; }

        public int Value { get; set; }

        public int NarudzbaID { get; set; }

        [ForeignKey("NarudzbaID")]
        public Narudzba? Narudzba { get; set; }

        [Required]
        public string KorisnikID { get; set; } = string.Empty;

        [ForeignKey("KorisnikID")]
        public ApplicationUser? Korisnik { get; set; }
    }
}
