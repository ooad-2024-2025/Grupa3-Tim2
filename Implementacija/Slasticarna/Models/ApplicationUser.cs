using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Slasticarna.Models
{
    public class ApplicationUser : IdentityUser
    {
        public required string Ime { get; set; }
        public required string Prezime { get; set; }

        public string? Adresa { get; set; }
        public string? BrTelefona { get; set; }
        public string VrstaKorisnika { get; set; } = "Korisnik";

        public string? AvatarPath { get; set; } = "/img/avatars/1.png";

        // Foreign key za popust (nullable)
        public int? PopustID { get; set; }

        [ForeignKey("PopustID")]
        public Popust? Popust { get; set; }
    }
}
