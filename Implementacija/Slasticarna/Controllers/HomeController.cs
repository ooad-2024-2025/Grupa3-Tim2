// HomeController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Slasticarna.Data;
using Slasticarna.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Slasticarna.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["appName"] = "CakeFactory";

            var userJson = HttpContext.Session.GetString("AppUser");
            if (!string.IsNullOrEmpty(userJson))
            {
                var user = JsonSerializer.Deserialize<ApplicationUser>(userJson);
                ViewBag.Role = user?.VrstaKorisnika;

                if (user?.VrstaKorisnika == "Šef")
                {
                    ViewData["Title"] = "Dashboard - Analytics";

                    ViewBag.UkupnoNarudzbi = _context.Narudzbe.Count();
                    ViewBag.UkupniPrihod = _context.Narudzbe.Sum(n => n.UkupnaCijena);
                    ViewBag.BrojKupaca = _context.AppUsers.Count(u => u.VrstaKorisnika != "Šef");
                    ViewBag.ProizvodaNaStanju = _context.Proizvodi.Count(p => p.NaStanju);

                    ViewBag.topProizvodi = _context.StavkeNarudzbe
                        .GroupBy(s => s.ProizvodID)
                        .Select(g => new {
                            ProizvodID = g.Key,
                            Kolicina = g.Sum(x => x.Kolicina)
                        })
                        .OrderByDescending(x => x.Kolicina)
                        .Take(5)
                        .Join(_context.Proizvodi,
                            s => s.ProizvodID,
                            p => p.ProizvodID,
                            (s, p) => new {
                                p.ProizvodID,
                                p.Naziv,
                                p.Cijena,
                                p.Ocjena,
                                p.Thumbnail,
                                s.Kolicina
                            })
                        .ToList();

                    ViewBag.ProsjecnaOcjena = _context.Ocjene.Any() ? _context.Ocjene.Average(o => o.Value) : 0;

                    ViewBag.DnevnaProdaja = _context.Narudzbe
                        .GroupBy(n => n.Datum.Date)
                        .AsEnumerable()
                        .Select(g => new DnevnaProdajaDto
                        {
                            Dan = g.Key.ToString("yyyy-MM-dd"),
                            Ukupno = g.Sum(x => x.UkupnaCijena)
                        })
                        .OrderBy(x => x.Dan)
                        .ToList();

                }
                else
                {
                    ViewData["Title"] = "Home Page";
                }
            }
            else
            {
                ViewBag.Role = "Gost";
                ViewData["Title"] = "Home Page";
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}