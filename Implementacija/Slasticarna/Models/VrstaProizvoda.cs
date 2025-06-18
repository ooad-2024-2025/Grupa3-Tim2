using System.ComponentModel.DataAnnotations;

namespace Slasticarna.Models
{
    public class VrstaProizvoda
    {
        [Key]
        public int VrstaProizvodaID { get; set; }

        public required string Naziv { get; set; }
    }
}
