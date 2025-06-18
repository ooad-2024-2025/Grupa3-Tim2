using System.Collections.Generic;

namespace Slasticarna.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int UkupnoNarudzbi { get; set; }
        public double UkupnoPrihoda { get; set; }
        public int BrojKupaca { get; set; }
        public int ProizvodiNaStanju { get; set; }
        public double ProsjecnaOcjena { get; set; }

        // Za graf dnevne prodaje
        public List<string> Datumi { get; set; }
        public List<double> DnevnaProdaja { get; set; }

        // Top 5 proizvoda
        public List<TopProizvod> TopProizvodi { get; set; }
    }

    public class TopProizvod
    {
        public string Naziv { get; set; }
        public double Cijena { get; set; }
        public double Ocjena { get; set; }
        public string? SlikaUrl { get; set; }
        public int Kolicina { get; set; }
    }
}
