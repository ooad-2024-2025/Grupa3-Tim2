using System.ComponentModel.DataAnnotations;

namespace Slasticarna.Models
{
    public class StanjeNarudzbe
    {
        [Key]
        public int StanjeNarudzbeID { get; set; }

        public required string Naziv { get; set; }
    }
}
