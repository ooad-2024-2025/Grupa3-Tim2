using Slasticarna.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Slasticarna.Models
{
    public class Proizvod
    {
        [Key]
        public int ProizvodID { get; set; }

        [Required]
        public required string Naziv { get; set; }

        public double Cijena { get; set; }

        public int VrstaProizvoda { get; set; }

        [ForeignKey("VrstaProizvoda")]
        public VrstaProizvoda? Vrsta { get; set; }

        public int? PopustID { get; set; }

        [ForeignKey("PopustID")]
        public Popust? Popust { get; set; }

        public double? Ocjena { get; set; }

        public int? NutritivnaVrijednost { get; set; }

        public bool NaStanju { get; set; }

        public string? Thumbnail { get; set; }

        public ICollection<StavkaNarudzbe>? Stavke { get; set; }
    }

}