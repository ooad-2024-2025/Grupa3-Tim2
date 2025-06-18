using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Slasticarna.Data;
using Slasticarna.Models;
using Slasticarna.Models.ViewModels;

public class PraznicnaPonudaController : Controller
{
    private readonly ApplicationDbContext _context;

    public PraznicnaPonudaController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var ponude = await _context.PraznicnePonude
            .Include(p => p.Proizvodi)
            .ThenInclude(pp => pp.Proizvod)
            .ToListAsync();

        return View(ponude);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.SviProizvodi = await _context.Proizvodi.ToListAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(PraznicnaPonudaViewModel model)
    {
        if (ModelState.IsValid)
        {
            var nova = new PraznicnaPonuda
            {
                Naziv = model.Naziv,
                DatumOd = model.DatumOd,
                DatumDo = model.DatumDo,
                Proizvodi = model.OdabraniProizvodi.Select(pid => new PraznicnaPonudaProizvod
                {
                    ProizvodID = pid
                }).ToList()
            };

            _context.Add(nova);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.SviProizvodi = await _context.Proizvodi.ToListAsync();
        return View(model);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var ponuda = await _context.PraznicnePonude.FindAsync(id);
        if (ponuda != null)
        {
            _context.PraznicnePonude.Remove(ponuda);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}
