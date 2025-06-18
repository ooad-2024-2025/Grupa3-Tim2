using Microsoft.AspNetCore.Mvc.Rendering;

namespace Slasticarna.Models.ViewModels
{
    public class PraznicnaPonudaViewModel
    {
        public int PraznicnaPonudaID { get; set; }
        public string Naziv { get; set; }
        public DateTime DatumOd { get; set; }
        public DateTime DatumDo { get; set; }

        public List<int> OdabraniProizvodi { get; set; } = new();
        public List<SelectListItem> SviProizvodi { get; set; } = new();
    }

}
