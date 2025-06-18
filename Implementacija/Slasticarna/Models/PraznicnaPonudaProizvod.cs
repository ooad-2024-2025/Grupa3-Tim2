namespace Slasticarna.Models
{
    public class PraznicnaPonudaProizvod
    {
        public int PraznicnaPonudaID { get; set; }
        public PraznicnaPonuda PraznicnaPonuda { get; set; }

        public int ProizvodID { get; set; }
        public Proizvod Proizvod { get; set; }
    }

}
