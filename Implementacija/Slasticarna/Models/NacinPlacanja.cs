using System.ComponentModel.DataAnnotations;

namespace Slasticarna.Models
{
    public class NacinPlacanja
    {
        [Key]
        public int NacinPlacanjaID { get; set; }

        public required string Naziv { get; set; }
    }
}
