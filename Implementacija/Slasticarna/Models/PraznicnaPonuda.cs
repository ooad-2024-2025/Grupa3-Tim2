namespace Slasticarna.Models
{
    public class PraznicnaPonuda
    {
        public int PraznicnaPonudaID { get; set; }
        public string Naziv { get; set; } = string.Empty; // npr. "Božićna ponuda", "Ramazanska ponuda"
        public DateTime DatumOd { get; set; }
        public DateTime DatumDo { get; set; }

        public ICollection<PraznicnaPonudaProizvod> Proizvodi { get; set; }
    }

}
