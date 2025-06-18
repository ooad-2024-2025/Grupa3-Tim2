using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Slasticarna.Data;
using Slasticarna.Models;
using Slasticarna.Models.ViewModels;

public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IConverter _converter;
    private readonly ICompositeViewEngine _viewEngine;

    public AdminController(ApplicationDbContext context, IConverter converter, ICompositeViewEngine viewEngine)
    {
        _context = context;
        _converter = converter;
        _viewEngine = viewEngine;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.Role = "Šef"; // fiksirano jer se ovo koristi samo za admine

        ViewBag.UkupnoNarudzbi = await _context.Narudzbe.CountAsync();
        ViewBag.UkupniPrihod = await _context.Narudzbe.SumAsync(n => n.UkupnaCijena);
        ViewBag.BrojKupaca = await _context.AppUsers.CountAsync(u => u.VrstaKorisnika != "Šef");
        ViewBag.ProizvodaNaStanju = await _context.Proizvodi.CountAsync(p => p.NaStanju);
        ViewBag.ProsjecnaOcjena = _context.Ocjene.Any() ? await _context.Ocjene.AverageAsync(o => o.Value) : 0;

        ViewBag.TopProizvodi = await _context.StavkeNarudzbe
            .GroupBy(s => s.ProizvodID)
            .Select(g => new
            {
                ProizvodID = g.Key,
                Kolicina = g.Sum(x => x.Kolicina)
            })
            .OrderByDescending(x => x.Kolicina)
            .Take(5)
            .Join(_context.Proizvodi,
                s => s.ProizvodID,
                p => p.ProizvodID,
                (s, p) => new
                {
                    p.ProizvodID,
                    p.Naziv,
                    p.Cijena,
                    p.Ocjena,
                    p.Thumbnail,
                    s.Kolicina
                })
            .ToListAsync();



        var narudzbe = await _context.Narudzbe
    .Select(n => new { n.Datum, n.UkupnaCijena })
    .ToListAsync(); // ✅ uzmi sve u memoriju

        ViewBag.DnevnaProdaja = narudzbe
            .GroupBy(n => n.Datum.Date)
            .Select(g => new DnevnaProdajaDto
            {
                Dan = g.Key.ToString("yyyy-MM-dd"),
                Ukupno = g.Sum(x => x.UkupnaCijena)
            })
            .OrderBy(x => x.Dan)
            .ToList();


        return View("Index"); // koristiš isti view kao Home/Index
    }


    public async Task<IActionResult> ExportPdf()
    {
        var model = await GetDashboardViewModelAsync();
        var html = await RenderViewToStringAsync("Index");

        var doc = new HtmlToPdfDocument()
        {
            GlobalSettings = {
                PaperSize = PaperKind.A4,
                Orientation = Orientation.Portrait
            },
            Objects = {
                new ObjectSettings {
                    HtmlContent = html
                }
            }
        };

        var pdfBytes = _converter.Convert(doc);
        return File(pdfBytes, "application/pdf", "IzvjestajOProdaji.pdf");
    }

    private async Task<DashboardViewModel> GetDashboardViewModelAsync()
    {
        // zamijeni svojim upitima za statistike
        return new DashboardViewModel
        {
            UkupnoNarudzbi = await _context.Narudzbe.CountAsync(),
            UkupnoPrihoda = await _context.Narudzbe.SumAsync(n => n.UkupnaCijena),
            BrojKupaca = await _context.Users.CountAsync(),
            ProizvodiNaStanju = await _context.Proizvodi.CountAsync(p => p.NaStanju),
            ProsjecnaOcjena = (double)await _context.Proizvodi.AverageAsync(p => p.Ocjena)
        };
    }

    private async Task<string> RenderViewToStringAsync(string viewName)
    {
        var actionContext = new ActionContext(HttpContext, RouteData, ControllerContext.ActionDescriptor);
        var viewResult = _viewEngine.FindView(actionContext, viewName, false);
        if (viewResult.View == null)
            throw new ArgumentNullException($"{viewName} nije pronađen.");

        var narudzbe = await _context.Narudzbe
            .Select(n => new { n.Datum, n.UkupnaCijena })
            .ToListAsync();

        var dnevnaProdaja = narudzbe
            .GroupBy(n => n.Datum.Date)
            .Select(g => new
            {
                Dan = g.Key.ToString("yyyy-MM-dd"),
                Ukupno = g.Sum(x => x.UkupnaCijena)
            })
            .OrderBy(x => x.Dan)
            .ToList();

        var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
        {
            ["Role"] = "Šef",
            ["UkupnoNarudzbi"] = await _context.Narudzbe.CountAsync(),
            ["UkupniPrihod"] = await _context.Narudzbe.SumAsync(n => n.UkupnaCijena),
            ["BrojKupaca"] = await _context.AppUsers.CountAsync(u => u.VrstaKorisnika != "Šef"),
            ["ProizvodaNaStanju"] = await _context.Proizvodi.CountAsync(p => p.NaStanju),
            ["ProsjecnaOcjena"] = _context.Ocjene.Any() ? await _context.Ocjene.AverageAsync(o => o.Value) : 0,
            ["TopProizvodi"] = await _context.StavkeNarudzbe
                .GroupBy(s => s.ProizvodID)
                .Select(g => new { ProizvodID = g.Key, Kolicina = g.Sum(x => x.Kolicina) })
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
                .ToListAsync()
        };

        viewData["DnevnaProdaja"] = dnevnaProdaja;

        using var sw = new StringWriter();
        var viewContext = new ViewContext(
            actionContext,
            viewResult.View,
            viewData,
            TempData,
            sw,
            new HtmlHelperOptions()
        );

        await viewResult.View.RenderAsync(viewContext);
        return sw.ToString();
    }


}
