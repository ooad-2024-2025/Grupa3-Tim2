namespace Slasticarna.Models
{

    public class PopustProizvod
    {
        public int? PopustID { get; set; }  // Postavi PopustID kao nullable
        public Popust Popust { get; set; }

        public int ProizvodID { get; set; }
        public Proizvod Proizvod { get; set; }
    }
}