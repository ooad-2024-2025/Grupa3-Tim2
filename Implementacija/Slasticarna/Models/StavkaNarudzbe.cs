using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Slasticarna.Models
{
    public class StavkaNarudzbe
    {
        [Key]
        public int StavkaID { get; set; }

        public int Kolicina { get; set; }

        public int NarudzbaID { get; set; }
        [ForeignKey("NarudzbaID")]
        public Narudzba? Narudzba { get; set; }

        public int ProizvodID { get; set; }
        [ForeignKey("ProizvodID")]
        public Proizvod? Proizvod { get; set; }
    }
}
