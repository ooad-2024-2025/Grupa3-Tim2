using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Slasticarna.Data;
using Slasticarna.Models;

namespace Slasticarna.Controllers
{
    public class ProizvodController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProizvodController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Proizvod
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Proizvodi.Include(p => p.Popust).Include(p => p.Vrsta);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Proizvod/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Proizvodi == null)
            {
                return NotFound();
            }

            var proizvod = await _context.Proizvodi
                .Include(p => p.Popust)
                .Include(p => p.Vrsta)
                .FirstOrDefaultAsync(m => m.ProizvodID == id);
            if (proizvod == null)
            {
                return NotFound();
            }
            ViewData["VrstaProizvoda"] = new SelectList(_context.VrsteProizvoda, "VrstaProizvodaID", "Naziv", proizvod.VrstaProizvoda);
            ViewData["PopustID"] = new SelectList(_context.Popusti
                .Select(p => new { p.PopustID, Naziv = p.IznosPopusta + "%" }),
                "PopustID", "Naziv", proizvod.PopustID);

            return View(proizvod);
        }

        // GET: Proizvod/Create
        public IActionResult Create()
        {
            ViewData["VrstaProizvoda"] = new SelectList(_context.VrsteProizvoda, "VrstaProizvodaID", "Naziv");
            ViewData["PopustID"] = new SelectList(_context.Popusti
                .Select(p => new { p.PopustID, Naziv = p.IznosPopusta + "%" }),
                "PopustID", "Naziv");

            return View();
        }

        // POST: Proizvod/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Proizvod proizvod, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                // Spremi sliku ako postoji
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                    var relativePath = Path.Combine("img", "products", fileName);
                    var fullPath = Path.Combine(wwwRootPath, relativePath);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    proizvod.Thumbnail = "/" + relativePath.Replace("\\", "/");
                }

                _context.Add(proizvod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["VrstaProizvoda"] = new SelectList(_context.VrsteProizvoda, "VrstaProizvodaID", "Naziv", proizvod.VrstaProizvoda);
            ViewData["PopustID"] = new SelectList(_context.Popusti.Select(p => new { p.PopustID, Naziv = p.IznosPopusta + "%" }), "PopustID", "Naziv", proizvod.PopustID);
            return View(proizvod);
        }


        // GET: Proizvod/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Proizvodi == null)
            {
                return NotFound();
            }

            var proizvod = await _context.Proizvodi.FindAsync(id);
            if (proizvod == null)
            {
                return NotFound();
            }
            ViewData["VrstaProizvoda"] = new SelectList(_context.VrsteProizvoda, "VrstaProizvodaID", "Naziv", proizvod.VrstaProizvoda);
            ViewData["PopustID"] = new SelectList(_context.Popusti
                .Select(p => new { p.PopustID, Naziv = p.IznosPopusta + "%" }),
                "PopustID", "Naziv", proizvod.PopustID);
            return View(proizvod);
        }

        // POST: Proizvod/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProizvodID,Naziv,Cijena,VrstaProizvoda,PopustID,Ocjena,NutritivnaVrijednost,NaStanju,Thumbnail")] Proizvod proizvod, IFormFile ImageFile)
        {
            if (id != proizvod.ProizvodID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // PREUZMI POSTOJEĆI proizvod iz baze
                    var existing = await _context.Proizvodi.FindAsync(id);
                    if (existing == null)
                        return NotFound();

                    // UPDATE polja
                    existing.Naziv = proizvod.Naziv;
                    existing.Cijena = proizvod.Cijena;
                    existing.VrstaProizvoda = proizvod.VrstaProizvoda;
                    existing.PopustID = proizvod.PopustID;
                    existing.Ocjena = proizvod.Ocjena;
                    existing.NutritivnaVrijednost = proizvod.NutritivnaVrijednost;
                    existing.NaStanju = proizvod.NaStanju;

                    // SLIKA: upload ako postoji
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

                        // Obrisi staru sliku ako postoji
                        if (!string.IsNullOrEmpty(existing.Thumbnail))
                        {
                            var oldPath = Path.Combine(wwwRootPath, existing.Thumbnail.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
                            if (System.IO.File.Exists(oldPath))
                                System.IO.File.Delete(oldPath);
                        }

                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                        var relativePath = Path.Combine("img", "products", fileName);
                        var fullPath = Path.Combine(wwwRootPath, relativePath);

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await ImageFile.CopyToAsync(stream);
                        }

                        existing.Thumbnail = "/" + relativePath.Replace("\\", "/");
                        _context.Entry(existing).Property(p => p.Thumbnail).IsModified = true;
                    }


                    _context.Update(existing);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProizvodExists(proizvod.ProizvodID))
                        return NotFound();
                    else
                        throw;
                }
            }

            ViewData["VrstaProizvoda"] = new SelectList(_context.VrsteProizvoda, "VrstaProizvodaID", "Naziv", proizvod.VrstaProizvoda);
            ViewData["PopustID"] = new SelectList(_context.Popusti
                .Select(p => new { p.PopustID, Naziv = p.IznosPopusta + "%" }),
                "PopustID", "Naziv", proizvod.PopustID);

            return View(proizvod);
        }



        // GET: Proizvod/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Proizvodi == null)
            {
                return NotFound();
            }

            var proizvod = await _context.Proizvodi
                .Include(p => p.Popust)
                .Include(p => p.Vrsta)
                .FirstOrDefaultAsync(m => m.ProizvodID == id);
            if (proizvod == null)
            {
                return NotFound();
            }

            return View(proizvod);
        }

        // POST: Proizvod/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Proizvodi == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Proizvodi'  is null.");
            }
            var proizvod = await _context.Proizvodi.FindAsync(id);
            if (proizvod != null)
            {
                _context.Proizvodi.Remove(proizvod);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProizvodExists(int id)
        {
            return (_context.Proizvodi?.Any(e => e.ProizvodID == id)).GetValueOrDefault();
        }
    }
}
